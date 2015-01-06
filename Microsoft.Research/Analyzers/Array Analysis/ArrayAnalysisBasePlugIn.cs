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
      abstract public class ArrayAnalysisBasePlugIn<AbstractDomain> :
        GenericPlugInAnalysisForComposedAnalysis
        where AbstractDomain : class, IAbstractDomainForArraySegmentationAbstraction<AbstractDomain, BoxedVariable<Variable>>
      {

        #region Constructor

        public ArrayAnalysisBasePlugIn(int id, string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
          ILogOptions options,
          Predicate<APC> cachePCs)
          : base(id, methodName, driver, new PlugInAnalysisOptions(options), cachePCs)
        {
        }

        #endregion

        #region Select

        [ContractVerification(false)]
        protected ArraySegmentationEnvironment<AbstractDomain, BoxedVariable<Variable>, BoxedExpression> Select(ArrayState state)
        {
          Contract.Ensures(state != null);
          Contract.Ensures(Contract.Result<ArraySegmentationEnvironment<AbstractDomain, BoxedVariable<Variable>, BoxedExpression>>() != null);

          return state.PluginAbstractStateAt(this.Id) as ArraySegmentationEnvironment<AbstractDomain, BoxedVariable<Variable>, BoxedExpression>;
        }

        #endregion

        #region Abstract methods

        abstract override public IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> InitialState
        {
          get;
        }

        abstract override public ArrayState.AdditionalStates Kind
        {
          get;
        }

        abstract override public IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> AssignInParallel(Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap, Converter<BoxedVariable<Variable>, BoxedExpression> convert, List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>> equalities, ArrayState state);

        #endregion

        #region Shared methods for the subclasses

        #region Generics

        protected bool TryInferSegmentEquality(BoxedExpression index, BoxedExpression body,
          ArrayState state,
          out BoxedExpression leftArray, out BoxedExpression rightArray, out AbstractDomain meet)
        {
          BinaryOperator bop;
          BoxedExpression.ArrayIndexExpression<Type> left, right;
          if (body.TryFindArrayExpBinOpArrayExp(index, out bop, out left, out right) && bop == BinaryOperator.Ceq)
          {
            // we know we have the expression "left == right"
            leftArray = left.Array;
            rightArray = right.Array;
          }

          leftArray = rightArray = default(BoxedExpression);
          meet = default(AbstractDomain);
          return false;
        }

        protected bool TryCreateArraySegment(BoxedExpression low, BoxedExpression upp, Variable arrayLen,
          AbstractDomain intv,
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> numDom,
          out ArraySegmentation<AbstractDomain, BoxedVariable<Variable>, BoxedExpression> arraySegmentation)
        {
          #region Contracts

          Contract.Requires(low != null);
          Contract.Requires(upp != null);
          Contract.Requires(intv != null);
          Contract.Requires(arrayLen != null);
          Contract.Requires(numDom != null);

          Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out arraySegmentation) != null);

          #endregion

          var lowerBounds = new Set<NormalizedExpression<BoxedVariable<Variable>>>();

          lowerBounds.AddIfNotNull(low.ToNormalizedExpression<Variable>());

          var upperBounds = new Set<NormalizedExpression<BoxedVariable<Variable>>>();

          upperBounds.AddIfNotNull(upp.ToNormalizedExpression<Variable>());
          upperBounds.AddIfNotNull(upp.ToNormalizedExpression<Variable>(true));
          upperBounds.AddIfNotNull(upp.Simplify(this.DecoderForMetaData).ToNormalizedExpression<Variable>());
          upperBounds.AddIfNotNull(this.Decoder.Stripped(upp).ToNormalizedExpression<Variable>());

          if (lowerBounds.Count == 0 || upperBounds.Count == 0)
          {
            arraySegmentation = null;
            return false;
          }

          var segments = new NonNullList<SegmentLimit<BoxedVariable<Variable>>>();

          var elements = new NonNullList<AbstractDomain>();

          #region Build the prefix

          // Check if low is zero
          int lowValue;
          if (low.IsConstantInt(out lowValue))
          {
            if (lowValue < 0)
            {
              arraySegmentation = default(ArraySegmentation<AbstractDomain, BoxedVariable<Variable>, BoxedExpression>);
              return false;
            }

            // { 0 } Top { lowValue }
            if (lowValue > 0)
            {
              segments.Add(new SegmentLimit<BoxedVariable<Variable>>(NormalizedExpression<BoxedVariable<Variable>>.For(0), false));
              elements.Add((AbstractDomain)intv.Top);
            }

            //  .. { lowValue } intv             
            segments.Add(new SegmentLimit<BoxedVariable<Variable>>(lowerBounds, false));
            elements.Add(intv);
          }
          else if (numDom.CheckIfGreaterEqualThanZero(low).IsTrue())
          {
            // { 0 } Top { low }?
            segments.Add(new SegmentLimit<BoxedVariable<Variable>>(NormalizedExpression<BoxedVariable<Variable>>.For(0), false));
            elements.Add((AbstractDomain)intv.Top);

            // intv { upp }?
            segments.Add(new SegmentLimit<BoxedVariable<Variable>>(lowerBounds, true)); // F: we can improve precision by asking if low != 0
            elements.Add(intv);
          }
          else
          {
            arraySegmentation = default(ArraySegmentation<AbstractDomain, BoxedVariable<Variable>, BoxedExpression>);
            return false;
          }

          #endregion

          #region Build the suffix
          // ... { upperBounds }
          if (arrayLen.Equals(upp.UnderlyingVariable) || arrayLen.Equals(this.Decoder.Stripped(upp).UnderlyingVariable))
          {
            segments.Add(new SegmentLimit<BoxedVariable<Variable>>(upperBounds, false));
          }
          else  // ... { upperBounds } Top { arrayLen }?
          {
            segments.Add(new SegmentLimit<BoxedVariable<Variable>>(upperBounds, false));
            elements.Add((AbstractDomain)intv.Top);
            segments.Add(
              new SegmentLimit<BoxedVariable<Variable>>(
                NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(arrayLen)), true));
          }
          #endregion

          arraySegmentation = new ArraySegmentation<AbstractDomain, BoxedVariable<Variable>, BoxedExpression>(
            segments, elements,
            (AbstractDomain)intv.Bottom, this.ExpressionManager);

          return true;
        }

        #endregion

        #region NonRelationalValueAbstraction specific
        static protected NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression> Box(DisInterval intv)
        {
          Contract.Requires(intv != null);
          Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>>() != null);

          return new NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>(intv);
        }

        protected bool TryInferNonRelationalProperty(BoxedExpression index, BoxedExpression body,
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> dom,
          out BoxedExpression array, out NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression> elementsProperty)
        {
          Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out array) != null);
          Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out elementsProperty) != null);

          BoxedExpression.ArrayIndexExpression<Type> arrayExp;
          if (body.TryFindArrayExp(index, out arrayExp))
          {
            array = arrayExp.Array;
            Contract.Assert(array != null);

            var slackVar = new BoxedVariable<Variable>(true);
            var slackExp = BoxedExpression.Var(slackVar);
            var renamedBody = body.Substitute(arrayExp, slackExp);
            var nonTrivial = false;

            var symbolicConditions = SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>.Unknown;
            var equalities = SetOfConstraints<BoxedVariable<Variable>>.Unknown;
            var disequalities = SetOfConstraints<BoxedVariable<Variable>>.Unknown;
            var weakUpperBounds = SetOfConstraints<BoxedVariable<Variable>>.Unknown;
            var strictUpperBounds = SetOfConstraints<BoxedVariable<Variable>>.Unknown;
            var existential = SetOfConstraints<BoxedVariable<Variable>>.Unknown; 

            #region Look for an interval
            var augmentedDom = dom.TestTrue(renamedBody) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
            var intv = augmentedDom.BoundsFor(slackVar);

            if (intv.IsNormal)
            {
              nonTrivial = true;
            }

            // TODO: upgrade the non-relational values to disintervals, to avoid those checks
            if (intv.IsTop && augmentedDom.CheckIfNonZero(slackExp).IsTrue())
            {
              intv = DisInterval.For(1);
              nonTrivial = true;
            }
            #endregion

            #region Look for equalities

            BoxedExpression left, right;
            if (renamedBody.IsCheckExp1EqExp2(out left, out right))
            {
              Variable eq;
              // a[i] == v
              if (
                (left.Equals(slackExp) && right.TryGetFrameworkVariable(out eq))
                ||
                (right.Equals(slackExp) && left.TryGetFrameworkVariable(out eq))
                )
              {
                equalities = new SetOfConstraints<BoxedVariable<Variable>>(ToBoxedVariable(eq));
                nonTrivial = true;
              }
            }

            #endregion

            if (nonTrivial)
            {
              elementsProperty = new NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>(
                intv, symbolicConditions, equalities, disequalities, weakUpperBounds, strictUpperBounds, existential);

              return true;
            }
            else
            {
              elementsProperty = default(NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>);
              return false;
            }
          }

          array = default(BoxedExpression);
          elementsProperty = default(NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>);
          return false;
        }

        #endregion

        #region Assumptions

        [ContractVerification(true)]
        protected ArrayState AssumeWithArrayRefinement(APC pc, bool positive, BoxedExpression comparison, ArrayState prevState)
        {
          Contract.Requires(comparison != null);
          Contract.Requires(prevState != null);
          Contract.Ensures(Contract.Result<ArrayState>() != null);

          // Default NonRelational values
          var interval = DisInterval.UnknownInterval;
          var symbExpression = SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>.Unknown;
          var equalities = SetOfConstraints<BoxedVariable<Variable>>.Unknown;
          var disequalities = SetOfConstraints<BoxedVariable<Variable>>.Unknown;
          var weakUpperBounds = SetOfConstraints<BoxedVariable<Variable>>.Unknown;
          var strictUpperBounds = SetOfConstraints<BoxedVariable<Variable>>.Unknown;
          var existential = SetOfConstraints<BoxedVariable<Variable>>.Unknown;

          Pair<BoxedVariable<Variable>, BoxedVariable<Variable>> refined;
          Variable left, right;
          int k;

          #region left != right ?
          if (positive && comparison.IsCheckExp1NotEqExp2(out left, out right))
          {
            // refined.One[refined.Two] != right
            var b = prevState.CanRefineToArrayLoad(new BoxedVariable<Variable>(left), out refined);
            if (!b)
            {
              // left != refined.One[refined.Two] 
              b = prevState.CanRefineToArrayLoad(new BoxedVariable<Variable>(right), out refined);

              // Swap roles
              var tmp = left;
              left = right;
              right = tmp;
            }

            if (b)
            {
              // invariant: "left" is the one to refine. 

              // TODO: right now we are ignoring the case when both are arrays

              interval = prevState.IntervalIfNotNull(left);

              var ael = new NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>
              (interval, symbExpression, equalities, 
              new SetOfConstraints<BoxedVariable<Variable>>(new BoxedVariable<Variable>(right)), 
              weakUpperBounds, strictUpperBounds, existential);

              var index = ToBoxedExpression(pc, refined.Two).ToNormalizedExpression<Variable>();

              return UpdateArrayAbstractValue(pc, refined.One, index, ael, prevState);
            }
          }
          #endregion

          #region left != k ?
          BinaryOperator bop;
          if (comparison.IsCheckExpOpConst(out bop, out left, out k)
            && prevState.CanRefineToArrayLoad(new BoxedVariable<Variable>(left), out refined))
          {
            switch (bop)
            {
              case BinaryOperator.Ceq:
                {
                  interval = positive ? DisInterval.For(k) : DisInterval.NotInThisInterval(DisInterval.For(k));
                  break;
                }

              case BinaryOperator.Cne_Un:
                {
                  interval = positive ? DisInterval.NotInThisInterval(DisInterval.For(k)) : DisInterval.For(k);
                  break;
                }

              default:
                {
                  return prevState;
                }
            }

            var ael = new NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>
              (interval, symbExpression, equalities, disequalities, weakUpperBounds, strictUpperBounds, existential);

            var index = ToBoxedExpression(pc, refined.Two).Stripped().ToNormalizedExpression<Variable>();

            return UpdateArrayAbstractValue(pc, refined.One, index, ael, prevState);
          }
          #endregion

          #region left == 0
          if (comparison.TryGetFrameworkVariable(out left)
            && prevState.CanRefineToArrayLoad(new BoxedVariable<Variable>(left), out refined))
          {
            interval = !positive ? DisInterval.Zero : DisInterval.NotZero;

            var ael = new NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>
              (interval, symbExpression, equalities, disequalities, weakUpperBounds, strictUpperBounds, existential);

            var index = ToBoxedExpression(pc, refined.Two).Stripped().ToNormalizedExpression<Variable>();

            return UpdateArrayAbstractValue(pc, refined.One, index, ael, prevState);
          }
          #endregion

          return prevState;
        }

        protected ArrayState UpdateArrayAbstractValue(APC pc, BoxedVariable<Variable> arr, NormalizedExpression<BoxedVariable<Variable>> index, 
          NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression> ael, ArrayState prevState)
        {
          Contract.Requires(prevState != null);
          Contract.Ensures(Contract.Result<ArrayState>() != null);

          ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>
            prev, updated;

          if (prevState.Array.TryGetValue(arr, out prev))
          {
            if (index != null)
            {
              IAbstractDomain prevAel;

              if (prev.TryGetAbstractValue(index, prevState.Numerical, out prevAel)
                && prevAel is NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>)
              {
                ael = ael.Meet(prevAel as NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>);
              }

              if (prev.TrySetAbstractValue(index, ael, prevState.Numerical, out updated))
              {
                prevState.Array[arr] = updated;
              }
            }
          }

          return prevState;
        }
        
        #endregion

        #endregion
      }
    }
  }
}