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
      /// <summary>
      /// Analysis that determines how an expressions refines to an array
      /// </summary>
      [ContractVerification(true)]
      public class ArrayExpressionRefinementPlugIn :
        GenericPlugInAnalysisForComposedAnalysis
      {
        #region Invariant
        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(this.Id >= 0);
        }

        #endregion

        #region Constructor

        public ArrayExpressionRefinementPlugIn(
          int id, string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          ILogOptions options,
          Predicate<APC> cachePCs
          )
          : base(id, methodName, mdriver, new PlugInAnalysisOptions(options), cachePCs)
        {
        }

        #endregion

        #region Select
        [Pure]
        public ArrayTracking Select(ArrayState state)
        {
          Contract.Requires(state != null);
          Contract.Requires(this.Id < state.PluginsCount);

          Contract.Ensures(Contract.Result<ArrayTracking>() != null);

          var untyped = state.PluginAbstractStateAt(this.Id);
          var selected = untyped as ArrayTracking;

          Contract.Assume(selected != null);

          return selected;
        }
        #endregion

        #region Overridden

        public override ArrayState.AdditionalStates Kind
        {
          get { return ArrayState.AdditionalStates.ArrayRefinement; }
        }

        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> InitialState
        {
          get { return new ArrayTracking(this.ExpressionManager); }
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, ArrayState> fixpoint)
        {
          return new ArrayExpressionRefinementFactQuery(this, fixpoint);
        }

        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> AssignInParallel(
          Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap,
          Converter<BoxedVariable<Variable>, BoxedExpression> convert, List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>> equalities,
          ArrayState state)
        {
          Contract.Assume(state != null);
          Contract.Assume(refinedMap != null, "missing preconditions in base class");
          Contract.Assume(convert != null, "missing preconditions in base class");

          var mySubState = Select(state);
          mySubState.AssignInParallel(refinedMap, convert);

          return mySubState;
        }

        #endregion

        #region Transfer functions

        #region Ldelem

        public override ArrayState Ldelem(
          APC pc, Type type, Variable dest, Variable array, Variable index, ArrayState data)
        {
          Contract.Assume(data != null);

          var mySubState = Select(data);

          var boxedArray = new BoxedVariable<Variable>(array);
          var boxedIndex = new BoxedVariable<Variable>(index);

          var symbolicConditions = SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>.Unknown;

          ScalarFromArrayTracking prevInfo;
          if (mySubState.TryGetValue(new BoxedVariable<Variable>(dest), out prevInfo))
          {
            Contract.Assert(prevInfo.Conditions != null);
            symbolicConditions = symbolicConditions.Meet(prevInfo.Conditions);
          }

          var isUnModifiedArrayElement = data.IsUnmodifiedArrayElementFromEntry(boxedArray, ToBoxedExpression(pc, index));

          var newRelation = new ScalarFromArrayTracking(boxedArray, boxedIndex, isUnModifiedArrayElement, symbolicConditions);

          mySubState[new BoxedVariable<Variable>(dest)] = newRelation;

          return data.UpdatePluginAt(this.Id, mySubState);
        }

        #endregion

        #region Stelem

        public override ArrayState Stelem(APC pc, Type type, Variable array, Variable index, Variable value, ArrayState data)
        {
          Contract.Assume(data != null);

          var mySubState = Select(data);

          var boxedArray = new BoxedVariable<Variable>(array);
          var boxedIndex = new BoxedVariable<Variable>(index);


          if (mySubState.IsNormal())
          {
            var toRemove = new List<BoxedVariable<Variable>>();
            foreach (var pair in mySubState.Elements)
            {
              var el = pair.Value;
              if (el.IsNormal() && el.Left.IsNormal() && el.Left.Contains(boxedArray) && el.Right.IsNormal() && el.Right.Contains(boxedIndex))
              {
                toRemove.Add(pair.Key);
              }
            }

            foreach (var k in toRemove)
            {
              mySubState.RemoveElement(k);
            }
          }

          var isUnModifiedArrayElement = data.IsUnmodifiedArrayElementFromEntry(boxedArray, ToBoxedExpression(pc, index));

          var newRelation = new ScalarFromArrayTracking(boxedArray, boxedIndex, isUnModifiedArrayElement, SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>.Unknown);

          mySubState[new BoxedVariable<Variable>(value)] = newRelation;

          return data.UpdatePluginAt(this.Id, mySubState);
        }
        
        #endregion

        #region Renaming

        public override ArrayState HelperForAssignInParallel(
          ArrayState state,
           Pair<APC, APC> edge, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap,
            Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          Contract.Assume(state != null);

          var mySubState = Select(state);
          mySubState.AssignInParallel(refinedMap, convert);

          return state.UpdatePluginAt(this.Id, mySubState);
        }

        #endregion

        #region Assume
        public override ArrayState Assume(APC pc, string tag, Variable source, object provenance, ArrayState data)
        {
          Contract.Assume(data != null);

          var sourceExp = BoxedExpression.Convert(this.MethodDriver.Context.ExpressionContext.Refine(pc, source), this.MethodDriver.ExpressionDecoder); // Force conversion, as the expression may be very large!

          if (sourceExp == null)
          {
            return data;
          }
          
          var mySubState = Select(data);

          #region Get the predicate P(a[exp]), for some 'a' and 'exp'

          var variablesInExp = sourceExp.Variables<Variable>().ConvertAll(v => new BoxedVariable<Variable>(v));

          var toUpdate = new List<BoxedVariable<Variable>>();

          foreach (var pair in mySubState.Elements)
          {
            if (variablesInExp.Contains(pair.Key))
            {
              toUpdate.Add(pair.Key);
            }
          }

          #region Handle equalities

          Variable v1, v2;
          if (sourceExp.IsCheckExp1EqExp2(out v1, out v2) // direct syntactical equality
            ||
            (tag == "false" && sourceExp.IsCheckExp1NotEqExp2(out v1, out v2)) // double negation
            )
          {
            return data.UpdatePluginAt(this.Id, mySubState.TestTrueEqual(new BoxedVariable<Variable>(v1), new BoxedVariable<Variable>(v2)));
          }

          #endregion

          // Shortcut: build the negation 'sourceExp == 0'
          if (tag == "false")
          {
            sourceExp = BoxedExpression.Binary(BinaryOperator.Ceq, sourceExp, BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
          }

          foreach (var key in toUpdate)
          {
            Contract.Assume(mySubState[key] != null);
            mySubState[key] = mySubState[key].AddCondition(key, sourceExp);
          }

          #endregion

          return data;

        }
        #endregion

        #region Call
        public override ArrayState Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, ArrayState data)
        {
          if (!this.MethodDriver.ExpressionLayer.ContractDecoder.IsPure(method))
          {
            var mySubState = Select(data);

            if (mySubState.IsNormal())
            {
              var result = mySubState.Top;
              var atLeastOneExcluded = false;

              var argsAsBoxedVariables = new List<BoxedVariable<Variable>>();
              foreach (var arg in args.Enumerate())
              {
                argsAsBoxedVariables.Add(new BoxedVariable<Variable>(arg));
              }

              foreach (var el in mySubState.Elements)
              {
                var arrays = el.Value.Left;
                if (arrays.IsNormal() && !arrays.Values.Intersect(argsAsBoxedVariables).Any())
                {
                  result.AddElement(el.Key, el.Value);
                }
                else
                {
                  atLeastOneExcluded = true;
                }
              }

              if (atLeastOneExcluded)
              {
                return data.UpdatePluginAt(this.Id, result);
              }
            }
          }
          return data;
        }
        #endregion

        #endregion

        #region Inference of Postconditions
        public override bool SuggestAnalysisSpecificPostconditions(
          ContractInferenceManager inferenceManager,
          IFixpointInfo<APC, ArrayState> fixpointInfo, List<BoxedExpression> postconditions)
        {
          Contract.Assert(this.MethodDriver.CFG != null);
          var exitPC = this.MethodDriver.CFG.NormalExit;
          
          ArrayState exitState;
          if (fixpointInfo.PostState(exitPC, out exitState))
          {
            var mySubState = Select(exitState);
            foreach (var pair in mySubState.Elements)
            {
              // Try to get a variable name for it
              var variable = ReadVariableInPostState(exitPC, pair.Key);

              if (variable == null)
              {
                // we fail to get a name in the post state, let us see if it is a constant
                Int64 k;
                var intv = exitState.Numerical.BoundsFor(pair.Key);
                if (intv.IsSingleton && intv.LowerBound.TryInt64(out k))
                {
                  variable = BoxedExpression.Const(k, this.DecoderForMetaData.System_Int64, this.DecoderForMetaData);
                }
                else
                {
                  continue;
                }
              }

              Contract.Assert(variable != null);

              Contract.Assume(pair.Value != null);

              if (pair.Value.IsNormal() && pair.Value.IsUnmodifiedFromEntry.IsTrue())
              {
                foreach (var arrayBoxed in pair.Value.Left.Values)
                {
                  Contract.Assume(arrayBoxed != null);

                  var arrayType = GetTypeOrObject(exitPC, arrayBoxed);

                  var array = ReadVariableInPostState(exitPC, arrayBoxed);
                  if (array != null)
                  {
                    // If we have one or more definite values for the index expression, we use them
                    if (pair.Value.Right.IsNormal())
                    {
                      foreach (var indexBoxed in pair.Value.Right.Values)
                      {
                        BoxedExpression newPost = null;

                        Contract.Assume(indexBoxed != null);
                        var index = ReadVariableInPostState(exitPC, indexBoxed);

                        if (index == null)
                        {
                          newPost = GenerateTheExists(exitPC, variable, arrayType, array);
                        }
                        else
                        {
                          #region Generate arr[index] == variable
                          var arrayLoad = new BoxedExpression.ArrayIndexExpression<Type>(array, index, arrayType);

                          newPost = BoxedExpression.Binary(BinaryOperator.Ceq, arrayLoad, variable);
                          #endregion
                        }

                        postconditions.AddIfNotNull(newPost);
                      }
                    }
                      // else, if we know it is top, we can simp,y project over it
                    else if(pair.Value.Right.IsTop)
                    {
                      postconditions.AddIfNotNull( GenerateTheExists(exitPC, variable, arrayType, array));
                    }
                  }
                }
              }
            }
          }

          return false;
        }

        private BoxedExpression GenerateTheExists(APC exitPC, BoxedExpression variable, Type arrayType, BoxedExpression array)
        {
          Contract.Requires(variable != null);
          Contract.Requires(array != null);

          BoxedExpression newPost = null;

          var boundedVariable = BoxedExpression.Var("__j__");

          var lowerBound = BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData);
          Variable arrayVar, arrayLength;
          if (array.TryGetFrameworkVariable(out arrayVar)
            && this.Context.ValueContext.TryGetArrayLength(exitPC, arrayVar, out arrayLength))
          {
            var upperBound = BoxedExpression.Var(arrayLength, this.Context.ValueContext.VisibleAccessPathListFromPost(exitPC, arrayLength));

            // arr[boundedVariable] == variable
            var arrayLoad = new BoxedExpression.ArrayIndexExpression<Type>(array, boundedVariable, arrayType);

            var eq = BoxedExpression.Binary(BinaryOperator.Ceq, arrayLoad, variable);

            newPost = new ExistsIndexedExpression(null, boundedVariable, lowerBound, upperBound, eq);
          }

          return newPost;
        }

        #endregion

        #region FactQuery

        public class ArrayExpressionRefinementFactQuery
          : IFactQuery<BoxedExpression, Variable>
        {
          #region Invariant

          [ContractInvariantMethod]
          void ObjectInvariant()
          {
            Contract.Invariant(this.analysis != null);
            Contract.Invariant(this.fixpoint != null);
          }

          #endregion

          #region State

          private readonly ArrayExpressionRefinementPlugIn analysis;
          private readonly IFixpointInfo<APC, ArrayState> fixpoint;

          #endregion

          #region Constructor

          public ArrayExpressionRefinementFactQuery(ArrayExpressionRefinementPlugIn analysis, IFixpointInfo<APC, ArrayState> fixpoint)
          {
            Contract.Requires(analysis != null);
            Contract.Requires(fixpoint != null);

            this.fixpoint = fixpoint;
            this.analysis = analysis;
          }

          #endregion

          #region IFactQuery<BoxedExpression,Variable> Members

          public ProofOutcome IsNull(APC pc, BoxedExpression expr)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsNonNull(APC pc, BoxedExpression expr)
          {
            return ProofOutcome.Top;
          }

          // only knows how to answer questions in the form a == b
          public ProofOutcome IsTrue(APC pc, BoxedExpression condition)
          {
            BinaryOperator bop;
            BoxedExpression left, right;
            ArrayState preState;
            if (condition.IsBinaryExpression(out bop, out left, out right) && 
              (bop == BinaryOperator.Ceq || bop == BinaryOperator.Cne_Un)
              && this.fixpoint.PreState(pc, out  preState))
            {
                var mySubState = analysis.Select(preState);

                Variable s1, s2;

                if (left.TryGetFrameworkVariable(out s1) && right.TryGetFrameworkVariable(out s2))
                {
                  var s1Boxed = new BoxedVariable<Variable>(s1);
                  var s2Boxed = new BoxedVariable<Variable>(s2);

                  ScalarFromArrayTracking value1, value2;

                  if (mySubState.TryGetValue(s1Boxed, out value1) && value1.IsNormal() 
                    && mySubState.TryGetValue(s2Boxed, out value2) && value2.IsNormal())
                  {
                    BoxedVariable<Variable> arr1, arr2;
                    BoxedVariable<Variable> index1, index2;

                    if (value1.Left.IsSingleton(out arr1) && value2.Left.IsSingleton(out arr2) && arr1.Equals(arr2))
                    {
                      if (value1.Right.IsSingleton(out index1) && value2.Right.IsSingleton(out index2) && index1.Equals(index2))
                      {
                        if (bop == BinaryOperator.Ceq)
                        {
                          return ProofOutcome.True;
                        }
                        else
                        {
                          Contract.Assert(bop == BinaryOperator.Cne_Un);
                          return ProofOutcome.False;
                        }
                      }
                    }
                  }
                }
              }
            return ProofOutcome.Top;
          }

          public ProofOutcome IsTrue(APC pc, Question question)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsTrueImply(APC pc, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsGreaterEqualToZero(APC pc, BoxedExpression expr)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsLessThan(APC pc, BoxedExpression left, BoxedExpression right)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsNonZero(APC pc, BoxedExpression condition)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome HaveSameFloatType(APC pc, BoxedExpression left, BoxedExpression right)
          {
            return ProofOutcome.Top;
          }

          public bool TryGetFloatType(APC pc, BoxedExpression exp, out ConcreteFloat type)
          {
            type = default(ConcreteFloat);
            return false;
          }

          public IEnumerable<Variable> LowerBounds(APC pc, BoxedExpression exp, bool strict)
          {
            yield break;
          }

          public IEnumerable<Variable> UpperBounds(APC pc, BoxedExpression exp, bool strict)
          {
            yield break;
          }

          public IEnumerable<BoxedExpression> LowerBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
          {
            yield break;
          }

          public IEnumerable<BoxedExpression> UpperBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
          {
            yield break;
          }

          public Pair<long, long> BoundsFor(APC pc, BoxedExpression exp)
          {
            return new Pair<long, long>(Int64.MinValue, Int64.MaxValue);
          }

          public ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object type)
          {
            return ProofOutcome.Top;
          }

          public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filterVariables, bool replaceVarsWithAccessPaths = true)
          {
            return FList<BoxedExpression>.Empty;
          }

          #endregion

          #region IFactBase<Variable> Members

          public ProofOutcome IsNull(APC pc, Variable value)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsNonNull(APC pc, Variable value)
          {
            return ProofOutcome.Top;
          }

          public bool IsUnreachable(APC pc)
          {
            return false;
          }
          #endregion

          #region ToString

          public override string ToString()
          {
            return "Array expression refinement fact query";
          }

          #endregion
        }
      
        
        #endregion


      }

    }
  }
}