// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#undef DEBUG_ARRAY_ANALYSIS

using System;
using System.Linq;
using Microsoft.Research.AbstractDomains;
using System.Collections.Generic;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Numerical;

using System.Diagnostics.Contracts;

using Microsoft.Research.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      public const int ArrayAnalysisID = -1;
      
      public class ArrayAnalysisPlugIn :
          ArrayAnalysisBasePlugIn<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>>
      {
        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(this.cachedBinaryOps != null);
        }

        readonly private Dictionary<Variable, STuple<BinaryOperator, Variable, int>> cachedBinaryOps;

        public ArrayAnalysisPlugIn(
          string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
          ILogOptions options,
          Predicate<APC> cachePCs
          )
          : base(ArrayAnalysisID, methodName, driver, options, cachePCs)
        {
          Contract.Requires(methodName != null);
          Contract.Requires(driver != null);
          Contract.Requires(options != null);

          this.cachedBinaryOps = new Dictionary<Variable, STuple<BinaryOperator, Variable, int>>();

          // We pass the pointer to the renamer here. 
          // It is ugly, but it is the easiest thing to do without changing interfaces, project/physical locations for BoxedExpressions, etc.
          SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>.ExpressionRenamer = BoxedExpressionsExtensions.Rename;
          SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>.VariableReplacer = BoxedExpressionsExtensions.ReplaceVariable;
        }

        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> 
          InitialState
        {
          get 
          {
            Contract.Ensures(Contract.Result<IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>>() != null);

            return new ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
          }
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, ArrayState> fixpoint)
        {
          Contract.Ensures(Contract.Result<IFactQuery<BoxedExpression, Variable>>() != null);

          var forAll = new ForAllFactQuery<ArrayState>(this.Decoder, this.Encoder, this.Options, fixpoint, this.Context, this.DecoderForMetaData, this.MethodDriver.AsForAllIndexed);

          if (this.Options.LogOptions.CheckExistentials)
          {
            var exists = new ExistsFactQuery<ArrayState>(this.Decoder, this.Encoder, this.Options, fixpoint, this.Context, this.DecoderForMetaData, this.MethodDriver.AsExistsIndexed);

            var quantifier = new ComposedQuantifierFactQuery<ArrayState>(forAll, exists, this.Decoder, this.Encoder, this.Options, fixpoint, this.Context, this.DecoderForMetaData,
              this.MethodDriver.AsForAllIndexed, this.MethodDriver.AsExistsIndexed);

            return quantifier;
          }
          else
          {
            return forAll;
          }
        }

        public override ArrayState.AdditionalStates Kind
        {
          get 
          { 
            return ArrayState.AdditionalStates.ArrayValues; 
          }
        }

        #region Assign In parallel
        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>
          AssignInParallel(
          Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap,
          Converter<BoxedVariable<Variable>, BoxedExpression> convert,
           List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>> equalities,
          ArrayState state)
        {
          var result = state.Array;

          // 1. Inject all the information from the Numerical domain
          result = result.TestTrueInformationForTheSegments(state.Numerical);

          // 2. Do the renaming
          result.AssignInParallel(refinedMap, convert);          

          // 3. Add the new equalities
          foreach(var pair in equalities)
          {
            var tmp = result.TestTrueEqualAsymmetric(pair.One, pair.Two);
            result = tmp.TestTrueEqualAsymmetric(pair.One.PlusOne(), pair.Two.PlusOne());
          }
          return result;
        }
        #endregion

        #region Transfer functions

        #region Unary
        public override ArrayState Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, ArrayState data)
        {
          if (op == UnaryOperator.Conv_i4)
          {
            return data.UpdateArray(data.Array.TestTrueEqualAsymmetric(ToNormalizedExpression(dest), ToNormalizedExpression(source)));
          }

          return data;
        }
        #endregion

        #region Binary
        public override ArrayState 
          Binary
          (APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, 
          ArrayState data)
        {
          // HACK HACK: we explicitely remeber the binary exps until we do not have a better way to look for sv correponding to expressions
          // We need it in order to get the indexing with postfix increments to work
          Type t;
          object value;
          if (op == BinaryOperator.Add && this.Context.ValueContext.IsConstant(pc, s2, out t, out value) && value is Int32)
          {
            // dest = s1 + value, so we store s1 -> <dest, value>
            cachedBinaryOps[s1] = new STuple<BinaryOperator, Variable, Int32>(op, dest, (Int32)value);
          }
          else if (cachedBinaryOps.ContainsKey(s1))
          {
            cachedBinaryOps.Remove(s1);
          }

          if (op != BinaryOperator.Add && op != BinaryOperator.Sub)
          {
            return data;
          }

          Variable var;
          int k;

          if (TryMatchVariableConstant(pc, op, Strip(pc, s1), Strip(pc, s2), data.Numerical, out var, out k) && k != Int32.MinValue)
          {
            var normExpression = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(var), k);
            var normVariable = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(dest));

            var arrays = data.Array.TestTrueEqualAsymmetric(normVariable, normExpression);

            normExpression = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(dest), -k);
            normVariable = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(var));

            var arrays2 = arrays.TestTrueEqualAsymmetric(normExpression, normVariable);
            var arrays3 = arrays2.TestTrueEqualAsymmetric(normVariable, normExpression);

            return data.UpdateArray(arrays3);


            //return data.UpdateArray(data.Array.TestTrueEqualAsymmetric(normExpression, normVariable));
          }

          return data;
        }

        #endregion
        
        #region New Array
        public override ArrayState
          Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList lengths,
          ArrayState state)
        {
          // Only single dimensional arrays for the moment
          if (lengths.Count == 1)
          {
            var init = this.DecoderForMetaData.System_Int32.Equals(type) || this.DecoderForMetaData.IsReferenceType(type)
              ? new NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>(DisInterval.For(0))
              : NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unknown;

            var lowerBound = new Set<NormalizedExpression<BoxedVariable<Variable>>>() { NormalizedExpression<BoxedVariable<Variable>>.For(0) };
            var upperBound = new Set<NormalizedExpression<BoxedVariable<Variable>>>() { NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(lengths[0])) };

            int lengthValue;
            if (this.IsConstantInt32(pc, lengths[0], out lengthValue))
            {
              // Special case for new T[0]
              if (lengthValue == 0)
              {
                var bottomSegment = new ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>
                  (new SegmentLimit<BoxedVariable<Variable>>(lowerBound, false),
                  init,
                  new SegmentLimit<BoxedVariable<Variable>>(upperBound, true),
                  NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unreached, this.ExpressionManager);

                state.Array.AddElement(new BoxedVariable<Variable>(dest), bottomSegment);

                return state;
              };

              upperBound.Add(NormalizedExpression<BoxedVariable<Variable>>.For(lengthValue));
            }

            var questionMark = state.Numerical.CheckIfLessThan( BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData),ToBoxedExpression(pc, lengths[0])).IsTrue() 
              ? false : true; 

            var segment = new ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>(
              new SegmentLimit<BoxedVariable<Variable>>(lowerBound, false),
              init,
              new SegmentLimit<BoxedVariable<Variable>>(upperBound, questionMark),
              NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unreached, this.ExpressionManager);

            state.Array.AddElement(new BoxedVariable<Variable>(dest), segment);
          }

          return state;
        }
        #endregion

        #region Method entry
        public override ArrayState
          Entry(APC pc, Method method, ArrayState state)
        {
          // We materialize all the arrays in the parameters...
          foreach (var param in this.DecoderForMetaData.Parameters(method).Enumerate())
          {
            Variable symb;
            var postPC = Context.MethodContext.CFG.Post(pc);
            if (this.Context.ValueContext.TryParameterValue(postPC, param, out symb))
            {
              var paramType = this.DecoderForMetaData.ParameterType(param);
              MaterializeIfArray(postPC, state, symb,
                this.DecoderForMetaData.IsMain(method)
                ? new NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>(DisInterval.For(1))
                : new NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>(DisInterval.UnknownInterval),
                paramType);

              MaterializeIfEnumerable(postPC, state, symb, paramType);
            }
          }

          return state;
        }
        #endregion

        #region Ldelem
        public override ArrayState Ldelem
          (APC pc, Type type, Variable dest, Variable array, Variable index, ArrayState data)
        {
          var indexExp = this.ToBoxedExpression(pc, index);

          // In the array analysis we do not care of conversions
          UnaryOperator uop;
          BoxedExpression tmpExp;
          if (indexExp.IsUnaryExpression(out uop, out tmpExp) && uop.IsConversionOperator())
          {
              indexExp = tmpExp;
          }

          ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> segment;
          if (data.Array.TryGetValue(new BoxedVariable<Variable>(array), out segment))
          {
            var indexNormExp = indexExp.ToNormalizedExpression<Variable>();

            if (indexNormExp == null)
            {
              var join = segment.JoinAll(NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unreached);

              if (join.Interval.IsNormal())
              {
                data.Numerical.AssumeInDisInterval(new BoxedVariable<Variable>(dest), join.Interval);
              }
              return data;
            }

            IAbstractDomain value;
            if (segment.TryGetAbstractValue(indexNormExp, data.Numerical, out value))
            {
              var valueAsNRelationalValue = value as NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>;
              if (valueAsNRelationalValue != null)
              {
                #region 1. Updated the environment with the information from the array
                data.Numerical.AssumeInDisInterval(new BoxedVariable<Variable>(dest), valueAsNRelationalValue.Interval);

                if (data.HasNonNullInfo)
                {
                  data = AssumeNonNullInfo(dest, valueAsNRelationalValue, data);
                }

                if (valueAsNRelationalValue.StrictUpperBounds.IsNormal())
                {
                  var destExp = this.ToBoxedExpression(pc, dest);
                  var newNumerical = data.Numerical.TestTrueLessThan(destExp, valueAsNRelationalValue.StrictUpperBounds, this.Encoder);

                  data = data.UpdateNumerical(newNumerical);
                }

                if (valueAsNRelationalValue.WeakUpperBounds.IsNormal())
                {
                  var destExp = this.ToBoxedExpression(pc, dest);
                  var newNumerical = data.Numerical.TestTrueLessEqualThan(destExp, valueAsNRelationalValue.WeakUpperBounds, this.Encoder);

                  data = data.UpdateNumerical(newNumerical);
                }

                // Special case for boxing
                Variable unbox;
                if (this.Context.ValueContext.TryUnbox(pc, dest, out unbox))
                {
                  var exp1 = this.ToBoxedExpression(pc, dest);
                  var exp2 = this.ToBoxedExpression(pc, unbox);
                  var newNumerical = data.Numerical.TestTrueEqual(exp1, exp2);

                  data = data.UpdateNumerical(newNumerical);
                }
                #endregion

                #region 2. Update the array with the new information
                
                var newNRValue = valueAsNRelationalValue.TestTrueEqual(new BoxedVariable<Variable>(dest));
               
                
                ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> newSegment;
                if (segment.TryAssumeAbstractValue(indexNormExp, newNRValue, data.Numerical, out newSegment))
                {
                  data.Array.Update(new BoxedVariable<Variable>(array), newSegment);
                }
                 
                #endregion
              }
              else
              {
                Contract.Assume(false); // Should be unreachable
              }
            }

          }

          return data;
        }

        private ArrayState AssumeNonNullInfo
          (Variable dest, NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression> value, ArrayState data)
        {
          Contract.Requires(data != null);
          Contract.Requires(value != null);
          Contract.Ensures(Contract.Result<ArrayState>() != null);

          var valueAsInterval = value.Interval;

          if (valueAsInterval.IsNormal)
          {
            var nnState = data.NonNull;

            if (valueAsInterval.IsZero)
            {
              nnState = Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Analysis.AssumeNull(dest, nnState);

              return data.UpdateNonNull(nnState);
            }
            else if(valueAsInterval.IsNotZero)
            {
              nnState = Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Analysis.AssumeNonNull(dest, nnState);

              return data.UpdateNonNull(nnState);
            }
          }

          return data;
        }

        #endregion

        #region Stelem
        [ContractVerification(true)]
        public override ArrayState 
          Stelem(APC pc, Type type, Variable array, Variable index, Variable value, 
          ArrayState state)
        {
          var indexExp = this.ToBoxedExpression(pc, index);

          // In the array analysis we do not care of conversions
          UnaryOperator uop;
          BoxedExpression tmpExp;
          if (indexExp.IsUnaryExpression(out uop, out tmpExp) && uop.IsConversionOperator())
          {
              indexExp = tmpExp;
          }

          #region Get the abstract, non-relational,  r-value

          var valueVar = new BoxedVariable<Variable>(value);

          // Get a numerical approximation
          var valueIntv = state.Numerical.BoundsFor(valueVar);

          if (valueIntv.IsTop)
          {
            if (state.HasNonNullInfo)
            {
              valueIntv = state.IntervalIfNotNull(value, ToBoxedExpression(pc, value));
            }
            if (valueIntv.IsTop)
            {
              Variable unbox;
              if (this.Context.ValueContext.TryUnbox(pc, value, out unbox))
              {
                valueIntv = state.Numerical.BoundsFor(new BoxedVariable<Variable>(unbox));
              }
            }
          }

          // Get the equality
          var equality = new SetOfConstraints<BoxedVariable<Variable>>(valueVar);

          // No inequality
          var inequality = SetOfConstraints<BoxedVariable<Variable>>.Unknown;

          // Get the weak upper bounds 
          var upperBoundsWeak = ToSetOfConstraints(state.Numerical.UpperBoundsFor(valueVar, false));

          // Get the strict upper bounds
          var upperBoundsStrict = ToSetOfConstraints(state.Numerical.UpperBoundsFor(valueVar, true));

          // Get the existential
          SetOfConstraints<BoxedVariable<Variable>> existential;
          Pair<BoxedVariable<Variable>, BoxedVariable<Variable>> arrayLoad;

          // Two conditions: 
            // 1. We can refine the sv to a load elem
            // 2. The value of the array is unmodified since its materialization
          if (state.CanRefineToArrayLoad(valueVar, out arrayLoad) 
            && !state.IsUnmodifiedArrayElementFromEntry(arrayLoad.One, indexExp))
          {
            existential = new SetOfConstraints<BoxedVariable<Variable>>(arrayLoad.One);
          }
          else
          {
            existential = SetOfConstraints<BoxedVariable<Variable>>.Unknown;
          }

          // Get the symbolic expressions
          var symbolicExpressions = state.SymbolicConditions(valueVar);

          if (symbolicExpressions.IsNormal())
          {
            var slack = this.Encoder.FreshVariable<object>();
            symbolicExpressions = new SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>(
              slack, symbolicExpressions.Conditions.RenameVariable(value, slack));
          }

          var nonRelationalValue = new NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>(
            valueIntv, symbolicExpressions, equality, inequality, upperBoundsWeak, upperBoundsStrict, existential);

          valueIntv = null; // F: doing it to find bugs in the case we are re-using it
          upperBoundsStrict = null;
          existential = null;
          #endregion

          // The array state already contains the array, or not
          ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> segment;
          if (state.Array.TryGetValue(new BoxedVariable<Variable>(array), out segment))
          {
            #region We already have an abstraction for the array

            ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> newSegment;

            var indexNormExp = indexExp.ToNormalizedExpression<Variable>();

            if (indexNormExp == null)
            {
              // Try to find a numerical bounds
              var indexValue = state.Numerical.BoundsFor(indexExp).AsInterval;
              if (indexValue.IsSingleton && indexValue.LowerBound.IsInteger)
              {
                indexNormExp = NormalizedExpression<BoxedVariable<Variable>>.For((Int32)indexValue.LowerBound);

                Contract.Assert(indexNormExp != null);
              }
              else
              {
                // We have a range of addresses
                Int32 low, upp;
                if (indexValue.IsFiniteAndInt32(out low, out upp))
                {
                  var lowExp = NormalizedExpression<BoxedVariable<Variable>>.For(low);
                  var uppExp = NormalizedExpression<BoxedVariable<Variable>>.For(upp);

                  if (segment.TrySetAbstractValue(lowExp, uppExp, nonRelationalValue, state.Numerical, out newSegment))
                  {
                    state.Array.Update(new BoxedVariable<Variable>(array), newSegment);

                    return state;
                  }
                }

                if (!nonRelationalValue.IsTop)
                {
                  var smashedSegment = segment.SmashAndWeakUpdateWith(nonRelationalValue);

                  state.Array.Update(new BoxedVariable<Variable>(array), smashedSegment);
                }
                else
                {
                  state.Array.RemoveElement(new BoxedVariable<Variable>(array));
                }

                return state;
              }
            }

            if (segment.TrySetAbstractValue(indexNormExp, nonRelationalValue, state.Numerical, out newSegment))
            {
              // We explicty add the constant for indexNormExp, if we have one
              Rational val;
              if (state.Numerical.BoundsFor(indexExp).TryGetSingletonValue(out val) && val.IsInteger)
              {
                var k = (Int32)val;
                // Add k == indexNormExp
                newSegment = newSegment.TestTrueEqualAsymmetric(NormalizedExpression<BoxedVariable<Variable>>.For(k), indexNormExp);
              }

              // Add the sv = constants informations
              newSegment = newSegment.TestTrueWithIntConstants(state.Numerical.IntConstants.ToNormalizedExpressions());

              // HACK HACK HACK: Add the postfix increment indexing if any (see test case Split)
              // f: This should be go away when we will have more interesting 
              STuple<BinaryOperator, Variable, int> binaryEquivalent;
              if (this.cachedBinaryOps.TryGetValue(index, out binaryEquivalent) && binaryEquivalent.One == BinaryOperator.Add && binaryEquivalent.Three == 1)
              {
                // the map entry "index -> <+, exp, i>" encodes the Binary instruction "exp = index + 1" 
                // as a consequence, we want to add the equality 
                var tempBe = NormalizedExpression<BoxedVariable<Variable>>.For(ToBoxedVariable(index), 1);
                newSegment = newSegment.TestTrueEqualAsymmetric(
                  NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(binaryEquivalent.Two)), tempBe);
              }

              state.Array.Update(new BoxedVariable<Variable>(array), newSegment);
            }
            else // For some reason, we were unable to set the abstract value, so we smash all the array to one
            {
              if (!nonRelationalValue.IsTop)
              {
                var smashedSegment = segment.SmashAndWeakUpdateWith(nonRelationalValue);
                state.Array.Update(new BoxedVariable<Variable>(array), smashedSegment);
              }
              else
              {
                state.Array.RemoveElement(new BoxedVariable<Variable>(array));
              }
            }
            #endregion
          }
          else
          {
            #region The array is not in scope
            Variable length;

            if (this.Context.ValueContext.TryGetArrayLength(this.Context.MethodContext.CFG.Post(pc), array, out length))
            {
              if (!nonRelationalValue.IsTop
                && state.Numerical.CheckIfGreaterEqualThanZero(indexExp).IsTrue()
                && state.Numerical.CheckIfLessThan(indexExp, this.ToBoxedExpression(pc, length)).IsTrue())
              {
                // we know the segmentation: {0} Top { index }? value { index + 1 } Top{ length }?
                // we try to get rid of some '?'

                var lowerBound = new Set<NormalizedExpression<BoxedVariable<Variable>>>() { NormalizedExpression<BoxedVariable<Variable>>.For(0) };
                var split = new Set<NormalizedExpression<BoxedVariable<Variable>>>() { NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(index)) };
                var splitPlusOne = new Set<NormalizedExpression<BoxedVariable<Variable>>>() { NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(index), 1) };
                var upperBound = new Set<NormalizedExpression<BoxedVariable<Variable>>>() { NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(length)) };

                // Construct the limits and the elements
                var limits = new NonNullList<SegmentLimit<BoxedVariable<Variable>>>();
                var elements = new NonNullList<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>>();


                #region Prefix
                var splitConditional = state.Numerical.CheckIfHolds(this.BoxedExpression_BinaryEqZero(indexExp));  // 0 == index ?

                if (splitConditional.IsTrue())  // {0, index} valueIntv
                {
                  lowerBound.AddRange(split);   // { 0, index }
                  split = null;                 // Not needed anymore
                  limits.Add(new SegmentLimit<BoxedVariable<Variable>>(lowerBound, false));           // { 0, index }
                }
                else   //  {0} [-oo, +oo] {index}[?] valueIntv 
                {
                  limits.Add(new SegmentLimit<BoxedVariable<Variable>>(lowerBound, false));
                  elements.Add(NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unknown);
                  limits.Add(new SegmentLimit<BoxedVariable<Variable>>(split, !splitConditional.IsFalse()));
                }

                #endregion

                elements.Add(nonRelationalValue);

                #region Postfix
                var lengthExp = this.ToBoxedExpression(pc, length);
                if(lengthExp == null)
                {
                  return state; // give up
                }

                var indexPlusOne = BoxedExpression_PlusOne(indexExp);

                var splitPlusOneConditionalEq = state.Numerical.CheckIfHolds(BoxedExpression_Eq(indexPlusOne, lengthExp));

                if (splitPlusOneConditionalEq.IsTrue())    // index + 1 == length implies    { index + 1, length} 
                {
                  upperBound.AddRange(splitPlusOne);      // { index +1, length }
                  splitPlusOne = null;                    // We do not need it anymore

                  limits.Add(new SegmentLimit<BoxedVariable<Variable>>(upperBound, false));
                }
                else // { index +1 } [-oo, +oo] { length}[?]
                {
                  var splitPlusOneConditional = state.Numerical.CheckIfLessThan(indexPlusOne, lengthExp);   // index + 1 < length ?

                  limits.Add(new SegmentLimit<BoxedVariable<Variable>>(splitPlusOne, false));           // { index + 1 }
                  elements.Add(NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unknown);
                  limits.Add(new SegmentLimit<BoxedVariable<Variable>>(upperBound, !splitPlusOneConditional.IsTrue()));             // { length }[?]                       
                }
                #endregion

                var newSegment = new ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>(
                  limits, elements,
                  NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unreached, this.ExpressionManager);

                state.Array.AddElement(new BoxedVariable<Variable>(array), newSegment);
              }
            }
            #endregion
          }

          return state;
        }
        #endregion

        #region Stloc

        public override ArrayState Stloc(APC pc, Local local, Variable source, ArrayState data)
        {
          var resultState = base.Stloc(pc, local, source, data);

          return resultState.UpdateArray(ArrayCounterInitialization(pc, local, source, resultState, resultState.Array));
        }

         #endregion

        #region Call
        public override ArrayState Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, ArrayState data)
        {
          var resultState = data;

          MaterializeIfArray(Context.MethodContext.CFG.Post(pc), resultState, dest, NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unknown, this.DecoderForMetaData.ReturnType(method));

          var methodname = method.ToString();

          #region Array Methods
          if (methodname.Contains("System.Array"))
          {
            if (methodname.Contains("Copy"))
            {
              if (args.Count == 3)
              {
                resultState = HandleArrayCopy(pc, args[0], args[1], args[2], resultState);
              }
            }
          }

          #endregion

          #region Linq Methods
          else if (methodname.Contains("System.Linq.Enumerable"))
          {
            if (methodname.Contains("Min") && args.Count == 1 /* no selectors*/)
            {
              resultState = HandleMinMax(true, pc, dest, args[0], resultState);
            }

            if (methodname.Contains("Max") && args.Count == 1 /* no selectors*/)
            {
              resultState = HandleMinMax(false, pc, dest, args[0], resultState);
            }

            if (methodname.Contains("Sum") && args.Count == 1 /* no selectors */)
            {
              resultState = HandleSum(pc, dest, args[0], resultState);
            }
          }
          else
          {
            if (!this.DecoderForContracts.IsPure(method))
            {
              // Havoc all the arrays in the arguments that are not marked as pure
              for (var pos = this.DecoderForMetaData.IsStatic(method) ? 0 : 1; pos < args.Count; pos++)
              {
                Variable arrayValue, index;
                // An array is passed. Havoc the content
                if (resultState.Array.ContainsKey(new BoxedVariable<Variable>(args[pos])) && !this.DecoderForMetaData.IsPure(method, pos))
                {
                  resultState.Array.RemoveVariable(new BoxedVariable<Variable>(args[pos]));
                }
                // an array is passed by ref: Havoc the array
                else if (this.Context.ValueContext.TryGetArrayFromElementAddress(pc, args[pos], out arrayValue, out index))
                {
                  resultState.Array.RemoveVariable(new BoxedVariable<Variable>(arrayValue));
                }
              }
            }
          }
          #endregion

          return resultState;
        }
        #endregion

        #region Assume

        public override ArrayState Assume(APC pc, string tag, Variable source, object provenance, ArrayState data)
        {
          var comparison = this.ToBoxedExpression(pc, source);
          var resultState = data;

          // Special case for Assume ForAll(... )
          if (tag != "false")
          {
            var forall = this.MethodDriver.AsForAllIndexed(pc, source);

            if (forall != null)
            {
              resultState = this.AssumeForAll(pc, forall, resultState);
            }
          }

          resultState = resultState.UpdateArray(resultState.Array.ReduceWith(resultState.Numerical));

          #region Checks an element against a sv

          resultState = AssumeWithArrayRefinement(pc, tag != "false", comparison, resultState);

          #endregion

          #region Assume may have created new equalities that we want to propagate to the Array abstract state

          var equalities = EqualitiesImpliedByTest(pc, tag, source, resultState.Numerical);          

          if (equalities.Count > 0)
          {            
            foreach (var pair in equalities)
            {
              var array = resultState.Array.TestTrueEqualAsymmetric(pair.One, pair.Two);
              resultState = resultState.UpdateArray(array.TestTrueEqualAsymmetric(pair.Two, pair.One));            
            }

          }

          #endregion

          #region Handling of array equalities
          {
            BinaryOperator bop;
            BoxedExpression leftExp, rightExp;
            Variable leftVar, rightVar;

            if (comparison.IsBinaryExpression(out bop, out leftExp, out rightExp)
              &&
              ((tag != "false" && bop == BinaryOperator.Ceq) || (tag == "false" && bop == BinaryOperator.Cne_Un))
              &&
                leftExp.TryGetFrameworkVariable<Variable>(out leftVar) && rightExp.TryGetFrameworkVariable<Variable>(out rightVar))
            {
              var boxedLeftVar = new BoxedVariable<Variable>(leftVar);
              var boxedRightVar = new BoxedVariable<Variable>(rightVar);

              var newArray = resultState.Array.TestTrueEqual(boxedLeftVar, boxedRightVar);

              resultState = resultState.UpdateArray(newArray);
            }
          }

          #endregion

          #region Handling of Boxing
          {
            BinaryOperator op;
            BoxedExpression left, right;
            if (comparison.IsBinaryExpression(out op, out left, out right) && op == BinaryOperator.Ceq)
            {
              Variable vLeft, vRight;
              if (left.TryGetFrameworkVariable(out vLeft) && right.TryGetFrameworkVariable(out vRight))
              {
                Variable unbox;
                if (this.Context.ValueContext.TryUnbox(pc, vLeft, out unbox))
                {
                  resultState = resultState.UpdateNumerical(resultState.Numerical.TestTrueEqual(left, this.ToBoxedExpression(pc, unbox)));
                }
                if (this.Context.ValueContext.TryUnbox(pc, vRight, out unbox))
                {
                  resultState = resultState.UpdateNumerical(resultState.Numerical.TestTrueEqual(right, this.ToBoxedExpression(pc, unbox)));
                }
              }
            }
          }
          #endregion

          return resultState;
        }

        [ContractVerification(true)]
        private List<PairNonNull<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>>
          EqualitiesImpliedByTest(APC pc, string tag, Variable source, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> oracle)
        {
          Contract.Requires(tag != null);
          Contract.Requires(oracle != null);

          var refined = this.ToBoxedExpression(pc, source);
          var equalities = new List<PairNonNull<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>>();

          var op = tag == "false" ? BinaryOperator.Cne_Un : BinaryOperator.Ceq;

          if (refined.IsBinary && refined.BinaryOp.IsComparisonBinaryOperator())
          {
            if (
              refined.BinaryOp == op                                                   /* Syntactic equality */
              || oracle.CheckIfEqual(refined.BinaryLeft, refined.BinaryRight).IsTrue() /* Semantic equality, for instance at loop exits */
              )
            {
              Variable leftVar, rightVar;

              if (refined.BinaryLeft.TryGetFrameworkVariable(out leftVar) && refined.BinaryRight.TryGetFrameworkVariable(out rightVar))
              {
                var leftBox = new BoxedVariable<Variable>(leftVar);
                var rightBox = new BoxedVariable<Variable>(rightVar);
                var leftExp = NormalizedExpression<BoxedVariable<Variable>>.For(leftBox);
                var rightExp = NormalizedExpression<BoxedVariable<Variable>>.For(rightBox);

                equalities.AddIfNotNull(leftExp, rightExp);

                // Adding the syntactically equal variables
                // We should keep it because we can have a case "a == b + 1" 
                // The EqualitiesFor expression below only returns the variables that the abstract domain knows are equal
                RecurseEqualities(refined.BinaryLeft, refined.BinaryRight, equalities);   

                // Adding the semantically equal variables
                foreach (var eq in oracle.EqualitiesFor(leftBox))
                {
                  equalities.AddIfNotNull(leftExp, NormalizedExpression<BoxedVariable<Variable>>.For(eq));
                }

                foreach (var eq in oracle.EqualitiesFor(rightBox))
                {
                  equalities.AddIfNotNull(rightExp, NormalizedExpression<BoxedVariable<Variable>>.For(eq));
                }
              }
            }
          }

          return equalities;
        }

        /// <summary>
        /// Given two expressions we know are equal at a given program point, we visit them to infer other equalities
        /// </summary>
        [ContractVerification(false)] // F: some issue with Old here that we should investigate
        private void RecurseEqualities(BoxedExpression left, BoxedExpression right,
          List<PairNonNull<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>> equalities)
        {
          Contract.Requires(left != null);
          Contract.Requires(right != null);
          Contract.Requires(equalities != null);

          Contract.Ensures(equalities.Count >= Contract.OldValue(equalities.Count)); // we can only add

          // 1. Match (conv)a ==  b

          UnaryOperator dummyOp;
          BoxedExpression castedExp;
          if (left.IsCastExpression(out dummyOp, out castedExp))
          {
            var eq1 = right.ToNormalizedExpression<Variable>();
            var eq2 = castedExp.ToNormalizedExpression<Variable>();

            equalities.AddIfNotNull(eq1, eq2);

            RecurseEqualities(castedExp, right, equalities);
          }

            // Match  a == (conv) b
          if (right.IsCastExpression(out dummyOp, out castedExp))
          {
            var eq1 = left.ToNormalizedExpression<Variable>();
            var eq2 = castedExp.ToNormalizedExpression<Variable>();
            
            equalities.AddIfNotNull(eq1, eq2);

            RecurseEqualities(left, castedExp, equalities);
           }

          // 2. Match a + b == c
          BoxedExpression binaryExp, nonBinaryExp;

          if (left.IsBinary && !right.IsBinary)
          {
            binaryExp = left;
            nonBinaryExp = right;
          }
          else if (!left.IsBinary && right.IsBinary)
          {
            binaryExp = right;
            nonBinaryExp = left;
          }
          else
          {
            return;
          }

          // F: set to null because we do not want to use them anymore
          left = right = default(BoxedExpression);

          if (binaryExp.BinaryOp != BinaryOperator.Add)
          {
            return;
          }

          // 3. Match a + k = c (i.e. one is a constant)

          Variable a, b, c;
          if (nonBinaryExp.TryGetFrameworkVariable(out c))
          {
            int k1, k2;
            var b1 = binaryExp.BinaryLeft.IsConstantInt(out k1);
            var b2 = binaryExp.BinaryRight.IsConstantInt(out k2);

            if (b1)
            {
              // k1 + k2 = c
              if (b2)
              {
                var leftExp = NormalizedExpression<BoxedVariable<Variable>>.For(k1 + k2); // Ignore overflows, it's on purpose
                var rigthExp = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(c));

                equalities.AddIfNotNull(leftExp, rigthExp);
              }
              // k1 + b = c
              else if (binaryExp.BinaryRight.TryGetFrameworkVariable(out b))
              {
                var leftExp = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(b), k1);
                var rigthExp = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(c));

                equalities.AddIfNotNull(leftExp, rigthExp);
              }
            }
            // a + k2 = c
            else if (b2)
            {
              if (binaryExp.BinaryLeft.TryGetFrameworkVariable(out a))
              {
                var leftExp = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(a), k2);
                var rigthExp = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(c));

                equalities.AddIfNotNull(leftExp, rigthExp);
              }
            }
          }
        }

        #endregion

        #region Assert

        public override ArrayState Assert(APC pc, string tag, Variable condition, object provenance, ArrayState data)
        {
          return this.Assume(pc, "assert", condition, provenance, data);
        }
        #endregion

        #region Return

#if false && DEBUG
        [ThreadStatic]
        private static int count = 0;

        public override ArrayState Return(APC pc, Variable source, ArrayState data)
        {
          if (data.Array.IsNormal())
          {
            foreach (var pair in data.Array.Elements)
            {
              foreach (var segment in pair.Value.Elements)
              {
                if (segment.Existential.IsNormal() || segment.SymbolicConditions.IsNormal())
                {
                  Console.WriteLine("{0} {1}: {2}", this.Context.MethodContext.CurrentMethod.ToString(), ++count, pair.Value.ToString());
                  break;
                }
              }
            }
          }

          return data;
        }
#endif        
        #endregion

        #endregion

        #region Handling of Contract.ForAll
        public ArrayState AssumeForAll(
          APC pc, ForAllIndexedExpression forall, ArrayState data)
        {
          Contract.Requires(data != null);

          // F: those are readonly-fields to which we cannot attach postcondition
          Contract.Assume(forall.LowerBound != null);
          Contract.Assume(forall.UpperBound != null);

          // Shortcut for ForAll(false => ...)
          if (forall.LowerBound.Equals(forall.UpperBound) || data.Numerical.CheckIfLessEqualThan(forall.UpperBound, forall.LowerBound).IsTrue())
          {
            return data;
          }

          NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression> intv;
          BoxedExpression array, otherArray;
          Variable arrayVar;

          if (TryInferNonRelationalProperty(forall.BoundVariable, forall.Body, data.Numerical, out array, out intv) 
            && array.TryGetFrameworkVariable(out arrayVar))
          {
            Variable arrayLen;
            if (this.Context.ValueContext.TryGetArrayLength(this.Context.MethodContext.CFG.Post(pc), arrayVar, out arrayLen))
            {
              Contract.Assume(arrayLen != null); // Clousot does not know arrayLen is a struct here

              ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> arraySegmentation;

              if (TryCreateArraySegment(forall.LowerBound, forall.UpperBound, arrayLen, intv, data.Numerical, out arraySegmentation))
              {
                ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> prevVal;
                if (data.Array.TryGetValue(ToBoxedVariable(arrayVar), out prevVal))
                {
                  var meet = prevVal.Meet(arraySegmentation);
                  data.Array.Update(ToBoxedVariable(arrayVar), meet);
                }
                else
                {
                  data.Array.AddElement(ToBoxedVariable(arrayVar), arraySegmentation);
                }
              }
            }
          }
          else if (TryInferSegmentEquality(forall.BoundVariable, forall.Body, data, out array, out otherArray, out intv))
          {
            // TODO
          }

          return data;
        }

          #endregion

        #region Specialized Handling of Framework methods

        /// <summary>
        /// Built-it support for Array.Copy.
        /// Please note that source and dest are swapped w.r.t. the usual Visitor to match the syntax of Array.Copy(...)
        /// </summary>
        private
          ArrayState
          HandleArrayCopy(APC pc, Variable source, Variable dest, Variable length,
          ArrayState data)
        {
          ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> sourceSegmentation, destSegmentation;
          if (data.Array.TryGetValue(new BoxedVariable<Variable>(source), out sourceSegmentation))
          {
            if (!data.Array.TryGetValue(new BoxedVariable<Variable>(dest), out destSegmentation))
            {
              destSegmentation = MaterializeArray(
                this.Context.MethodContext.CFG.Post(pc), data, source,
                NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unknown);
              
              if (destSegmentation == null)
              {
                return data;
              }
            }

            Contract.Assert(this.cacheManager != null);

            data.Array[new BoxedVariable<Variable>(dest)] = destSegmentation.CopyFrom(sourceSegmentation, this.ToBoxedExpression(pc, length), data.Numerical);
          }

          Contract.Assert(this.cacheManager != null);
          return data;
        }

        private
          ArrayState
          HandleMinMax(bool isMin, APC pc, Variable dest, Variable array,
            ArrayState data)
        {
          ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> segmentation;
          if (data.Array.TryGetValue(new BoxedVariable<Variable>(array), out segmentation))
          {
            var joinAll = segmentation.JoinAll(NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unreached);
            int low, upp;
            if (joinAll.Interval.AsInterval.IsFiniteAndInt32(out low, out upp))
            {
              if (isMin)
              {
                data.Numerical.AssumeInDisInterval(new BoxedVariable<Variable>(dest), DisInterval.For(low, low));
              }
              else
              {
                data.Numerical.AssumeInDisInterval(new BoxedVariable<Variable>(dest), DisInterval.For(upp, upp));
              }

            }
          }

          return data;
        }

        private ArrayState
          HandleSum(APC pc, Variable dest, Variable array,
          ArrayState data)
        {
          ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> segmentation;
          if (data.Array.TryGetValue(new BoxedVariable<Variable>(array), out segmentation))
          {
            var result = DisInterval.For(0);
            foreach (var el in segmentation.Elements)
            {
              result += el.Interval;

              // Shortcut
              if (!result.IsNormal())
              {
                return data;
              }
            }

            if (result.IsNormal())
            {
              data.Numerical.AssumeInDisInterval(new BoxedVariable<Variable>(dest), result);
            }
          }

          return data;
        }
        #endregion
        
        #region Array materialization

        public void MaterializeIfEnumerable(APC postPC, ArrayState state, Variable arrayValue, Type paramType)
        {
          if (this.DecoderForMetaData.Name(paramType).Contains("Enumerable"))
          {
            Variable modelArray;
            if (this.Context.ValueContext.TryGetModelArray(postPC, arrayValue, out modelArray))
            {
              MaterializeArray(postPC, state, modelArray, new NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>(DisInterval.UnknownInterval));
            }
          }
        }

        public void MaterializeIfArray(
          APC postPC, 
          ArrayState oracle, 
          Variable arrayValue,
          NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression> initialValue, Type paramType) 
        {
          Contract.Requires(initialValue != null);

          if (this.DecoderForMetaData.IsArray(paramType))
          {
            MaterializeArray(postPC, oracle, arrayValue, initialValue);
          }
        }

        /// <returns>The materialized segment, or null if it failed</returns>
        public
          ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>
          MaterializeArray(
          APC postPC, 
          ArrayState state,
          Variable arrayValue, NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression> initialValue) 
        {
          Contract.Requires(initialValue != null);

          var boxSym = new BoxedVariable<Variable>(arrayValue);
          ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> arraySegment;

          if (state.Array.TryGetValue(boxSym, out arraySegment)) return arraySegment; // already materialized

          Variable array_Length;
          if (this.Context.ValueContext.TryGetArrayLength(postPC, arrayValue, out array_Length))
          {
            var isNonEmpty = state.Numerical.CheckIfNonZero(this.ToBoxedExpression(postPC, array_Length)).IsTrue();

            var limits = new NonNullList<SegmentLimit<BoxedVariable<Variable>>>() 
                { 
                  new SegmentLimit<BoxedVariable<Variable>>(NormalizedExpression<BoxedVariable<Variable>>.For(0), false),                     // { 0 }
                  new SegmentLimit<BoxedVariable<Variable>>(NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(array_Length)), !isNonEmpty) // { symb.Length }
                };

            var elements = new NonNullList<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>>() { initialValue };

            var newSegment = new ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>(
              limits, elements,
              NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unreached, this.ExpressionManager);

            state.Array.AddElement(new BoxedVariable<Variable>(arrayValue), newSegment);

            return newSegment;
          }

          return null;
        }

      #endregion

        #region Postcondtion suggestion

        public override bool SuggestAnalysisSpecificPostconditions(ContractInferenceManager inferenceManager, 
          IFixpointInfo<APC, ArrayState> fixpointInfo, List<BoxedExpression> postconditions)
        {
          ArrayState postState;
          var exitPC = this.MethodDriver.CFG.NormalExit;
          if (fixpointInfo.PreState(exitPC, out postState))
          {
            var myState = Select(postState);
            if(myState.IsNormal())
            {         
              var md = this.DecoderForMetaData;
              var boundVar = BoxedExpression.VarBoundedInQuantifier("__k__");
              var zero = BoxedExpression.Const(0, md.System_Int32, md);
              var expInPostStateReader = new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(this.Context, md);
              var expFactory = new BoxedExpressionFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>
                (boundVar, boundVar, v => this.ToBoxedExpression(exitPC, v), md, null);

              foreach (var arr in myState.Elements)
              {
                Variable arrVar;
                Variable arrLength;
                if (arr.Value.IsNormal() && arr.Key.TryUnpackVariable(out arrVar) && this.Context.ValueContext.TryGetArrayLength(exitPC, arrVar, out arrLength))
                {
                  Details details;

                  var arrayType = this.Context.ValueContext.GetType(exitPC, arrVar);
                  if(!arrayType.IsNormal
                    || !md.IsArray(arrayType.Value) // we add this check because we may fail the ElementType below, when, e.g., an array is casted into a collection and then returned 
                    )
                  {
                    continue;
                  }

                  var arrayElementsType = md.ElementType(arrayType.Value);

                  var arrExp = GetExpressionWithPath(exitPC, arrVar);
                  if (arrExp != null)
                  {
                    var arrLengthPath = this.Context.ValueContext.VisibleAccessPathListFromPost(exitPC, arrLength);
                    if (arrLengthPath != null)
                    {
                      var arrLengthExp = BoxedExpression.Var(arrLength, arrLengthPath);

                      // We only propagate properties on whole arrays
                      var join = arr.Value.JoinAll(NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unreached);
                      if (join.IsNormal())
                      {
                        foreach (var exp in join.To(expFactory).SplitConjunctions())
                        {
                          var expInPost = expInPostStateReader.ExpressionInPostState(exp, true, out details);
                          if (expInPost != null)
                          {
                            expInPost = expInPost.Substitute(boundVar, new BoxedExpression.ArrayIndexExpression<Type>(arrExp, boundVar, arrayElementsType));

                            if (md.IsReferenceType(arrayElementsType)) // produce e.g., arr[__k__] != null instead of arr[__k__] != 0
                            {
                              expInPost = expInPost.Substitute(zero, BoxedExpression.Const(null, arrayElementsType, md));
                            }

                            var quantified = new ForAllIndexedExpression(null, boundVar, zero, arrLengthExp, expInPost);

                            postconditions.Add(quantified);
                          }
                        }
                      }

                    }
                  }
                }
              }
            }
          }
          return false;

        }

        private BoxedExpression GetExpressionWithPath(APC exitPC, Variable arrVar)
        {
          var vc = this.Context.ValueContext;
          var arrayPath = vc.VisibleAccessPathListFromPost(exitPC, arrVar);

          if (arrayPath != null)
          {
            return BoxedExpression.Var(arrVar, arrayPath);
          }

          Variable retVar;
          if (vc.TryResultValue(exitPC, out retVar) && retVar.Equals(arrVar))
          {
            return BoxedExpression.Result<Type>(this.DecoderForMetaData.ReturnType(this.MethodDriver.CurrentMethod));
          }

          return null;
        }
        
        #endregion

        #region Utils


        /// <summary>
        /// Builds the expression exp == 0
        /// </summary>
        protected BoxedExpression BoxedExpression_BinaryEqZero(BoxedExpression exp)
        {
          Contract.Ensures(Contract.Result<BoxedExpression>() != null);

          return BoxedExpression.Binary(
            BinaryOperator.Ceq, 
            exp, 
            BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
        }

        /// <summary>
        /// Builds the expression exp + 1
        /// </summary>
        protected BoxedExpression BoxedExpression_PlusOne(BoxedExpression exp)
        {
          Contract.Requires(exp != null);
          Contract.Ensures(Contract.Result<BoxedExpression>() != null);

          return BoxedExpression.Binary(BinaryOperator.Add, exp, BoxedExpression.Const(1, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
        }

        protected BoxedExpression BoxedExpression_Eq(BoxedExpression left, BoxedExpression right)
        {
          Contract.Requires(left != null);
          Contract.Requires(right != null);
          Contract.Ensures(Contract.Result<BoxedExpression>() != null);

          return BoxedExpression.Binary(BinaryOperator.Ceq, left, right);
        }

        static protected SetOfConstraints<BoxedVariable<Variable>> ToSetOfConstraints(IEnumerable<BoxedExpression> expressions)
        {
          Contract.Requires(expressions != null);
          Contract.Ensures(Contract.Result<SetOfConstraints<BoxedVariable<Variable>>>() != null);

          var converted = new Set<BoxedVariable<Variable>>();

          foreach (var exp in expressions)
          {
            Variable v;
            if (exp.TryGetFrameworkVariable(out v))
            {
              converted.Add(new BoxedVariable<Variable>(v));
            }
          }

          return new SetOfConstraints<BoxedVariable<Variable>>(converted);
        }

        #endregion
      }
    }
  }
}