// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// The implementation for the reduced product of intervals and symbolic weak upper bounds
// This has got the official name of "Pentagons"

// Those two options are really used to run the experiments in the Pentagons paper

// #define EXPENSIVE_JOIN
// #define CARTESIAN

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.CodeAnalysis;
using System.Linq;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    /// <summary> 
    /// The main class for intervals (of rationals) with symbolic upper bounds
    /// </summary>
    [ContractVerification(true)]
    public class Pentagons<Variable, Expression>
      : ReducedCartesianAbstractDomain<IIntervalAbstraction<Variable, Expression>, WeakUpperBounds<Variable, Expression>>,
        INumericalAbstractDomain<Variable, Expression>
    {
        #region Invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(expManager != null);
        }

        #endregion

        #region Statistics
        static public string Statistics
        {
            get
            {
#if TRACE_PERFORMANCE
                string maxSizeforFunctionalDomain = MaxSizesAnalysisFrom(IntervalEnvironment<Variable, Expression>.MaxSizes);
                return maxSizeforFunctionalDomain.ToString();
#else
                return "Performance tracing is off";
#endif
            }
        }

        private static string MaxSizesAnalysisFrom(IDictionary<int, int> iDictionary)
        {
            if (iDictionary == null)
                return "";

            int max = Int32.MinValue;
            int sum = 0;
            var occurrences = new Dictionary<int, int>();

            foreach (int o in iDictionary.Keys)
            {
                if (iDictionary[o] > max)
                {
                    max = iDictionary[o];
                }

                sum += iDictionary[o];

                // Update the occurrences count
                if (occurrences.ContainsKey(iDictionary[o]))
                {
                    occurrences[iDictionary[o]]++;
                }
                else
                {
                    occurrences[iDictionary[o]] = 1;
                }
            }

            var averageSize = iDictionary.Count != 0 ? ((double)sum) / iDictionary.Count : Double.PositiveInfinity;

            return "Max size : " + max + Environment.NewLine + "Average size: " + averageSize;
        }

        #endregion

        #region Private state

        private readonly ExpressionManagerWithEncoder<Variable, Expression> expManager;

        #endregion

        #region (private) Constructor
        /// <summary>
        /// Please note that the decoder MUST be already be set for the <code>left</code> and <code>right</code> abstract domains
        /// </summary>
        public Pentagons(
          IIntervalAbstraction<Variable, Expression> left,
          WeakUpperBounds<Variable, Expression> right,
          ExpressionManagerWithEncoder<Variable, Expression> expManager)
          : base(left, right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);
            Contract.Requires(expManager != null);

            this.expManager = expManager;
        }
        #endregion

        #region (protected) Getters
        protected ExpressionManagerWithEncoder<Variable, Expression> ExpressionManager
        {
            get
            {
                Contract.Ensures(Contract.Result<ExpressionManagerWithEncoder<Variable, Expression>>() != null);

                return expManager;
            }
        }
        #endregion

        #region Factories

        [Pure]
        override protected ReducedCartesianAbstractDomain<IIntervalAbstraction<Variable, Expression>, WeakUpperBounds<Variable, Expression>>
          Factory(IIntervalAbstraction<Variable, Expression> left, WeakUpperBounds<Variable, Expression> right)
        {
            return this.FactoryOfPentagons(left, right);
        }

        [Pure]
        private Pentagons<Variable, Expression> FactoryOfPentagons(
          IIntervalAbstraction<Variable, Expression> left, WeakUpperBounds<Variable, Expression> right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<Pentagons<Variable, Expression>>() != null);

            return new Pentagons<Variable, Expression>(left, right, expManager);
        }

        /// <summary>
        /// This reduction considers the pairs (x, y)  of intervals and add to the weak upper bounds all those such that x &lt; y
        /// </summary>
        [Pure]
        public override ReducedCartesianAbstractDomain<IIntervalAbstraction<Variable, Expression>, WeakUpperBounds<Variable, Expression>>
          Reduce(IIntervalAbstraction<Variable, Expression> left, WeakUpperBounds<Variable, Expression> right)
        {
            return this.Factory(left, right);
        }

        #endregion

        #region Implementation of the abstract methods

        override public bool LessEqual(IAbstractDomain a)
        {
            bool result;
            if (AbstractDomainsHelper.TryTrivialLessEqual(this, a, out result))
            {
                return result;
            }

            var r = a as Pentagons<Variable, Expression>;

            Contract.Assume(r != null);

            if (!this.Left.LessEqual(r.Left))
            {
                return false;
            }

            return this.Right.LessEqual(r.Right);
        }

        /// <summary>
        /// This is a version of the join which causes a partial propagation of the information from Intervals to Symbolic upper bounds
        /// </summary>
        /// <param name="a">The other element</param>
        sealed public override IAbstractDomain Join(IAbstractDomain a)
        {
            if (this.IsBottom)
                return a;
            if (a.IsBottom)
                return this;
            if (this.IsTop)
                return this;
            if (a.IsTop)
                return a;

            var r = a as Pentagons<Variable, Expression>;

            Contract.Assume(r != null);

            // These two lines have a weak notion of closure, which essentially avoids dropping "x < y" if it is implied by the intervals abstract domain                
            // It seems that it is as precise as the expensive join

            var joinRightPart = this.Right.Join(r.Right, this.Left, r.Left);
            var joinLeftPart = this.Left.Join(r.Left);

            return this.Factory(joinLeftPart, joinRightPart);
        }

        /// <summary>
        /// The pairwise widening
        /// </summary>
        public override IAbstractDomain Widening(IAbstractDomain prev)
        {
            if (this.IsBottom)
                return prev;
            if (prev.IsBottom)
                return this;

            var asIntWSUB = prev as Pentagons<Variable, Expression>;
            Contract.Assume(asIntWSUB != null);

            var widenLeft = this.Left.Widening(asIntWSUB.Left);
            var widenRight = this.Right.Widening(asIntWSUB.Right);

            return this.Factory(widenLeft, widenRight);
        }

        #endregion

        #region INumericalAbstractDomain<Variable, Expression>Members

        /// <summary>
        /// Dispatch the assigment to the underlying abstract domains
        /// </summary>
        /// <param name="sourcesToTargets"></param>
        public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
        {
            // NOTE NOTE that we first assign in the right (so to assign in the pre-state!!!) and then on the left
            this.Right.AssignInParallel(sourcesToTargets, convert, this.Left);

            this.Left.AssignInParallel(sourcesToTargets, convert);
        }

        /// <returns>
        /// An interval that contains the upper bounds for <code>v</code>
        /// </returns>
        public DisInterval BoundsFor(Expression v)
        {
            return this.Left.BoundsFor(v);
        }

        public DisInterval BoundsFor(Variable v)
        {
            return this.Left.BoundsFor(v);
        }

        public List<Pair<Variable, Int32>> IntConstants
        {
            get
            {
                return this.Left.IntConstants;
            }
        }

        public IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
        {
            return this.Right.LowerBoundsFor(v, strict).Union(this.Left.LowerBoundsFor(v, strict));
        }

        public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
        {
            return this.Right.UpperBoundsFor(v, strict).Union(this.Left.UpperBoundsFor(v, strict));
        }

        public IEnumerable<Variable> EqualitiesFor(Variable v)
        {
            return this.Right.EqualitiesFor(v);
        }

        public IEnumerable<Expression> LowerBoundsFor(Variable v, bool strict)
        {
            return this.Right.LowerBoundsFor(v, strict).Union(this.Left.LowerBoundsFor(v, strict));
        }

        public IEnumerable<Expression> UpperBoundsFor(Variable v, bool strict)
        {
            return this.Right.UpperBoundsFor(v, strict).Union(this.Left.UpperBoundsFor(v, strict));
        }

        public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
        {
            return this.Left.CheckIfGreaterEqualThanZero(exp).Meet(Right.CheckIfGreaterEqualThanZero(exp));
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
        {
            var checkOnLeft = this.Left.CheckIfLessThan(e1, e2);
            var checkOnRight = this.Right.CheckIfLessThan(e1, e2, this.Left);

            return checkOnLeft.Meet(checkOnRight);
        }

        public FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2)
        {
            return this.CheckIfLessThan(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
        {
            var checkOnLeft = this.Left.CheckIfLessThan_Un(e1, e2);
            var checkOnRight = this.Right.CheckIfLessThan_Un(e1, e2);

            return checkOnLeft.Meet(checkOnRight);
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Variable v1, Variable v2)
        {
            var checkOnLeft = this.Left.CheckIfLessThan(v1, v2);
            var checkOnRight = this.Right.CheckIfLessThan(v1, v2);

            return checkOnLeft.Meet(checkOnRight);
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
        {
            var checkOnLeft = this.Left.CheckIfLessEqualThan(e1, e2);
            var checkOnRight = this.Right.CheckIfLessEqualThan(e1, e2, this.Left);

            return checkOnLeft.Meet(checkOnRight);
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
        {
            var checkOnLeft = this.Left.CheckIfLessEqualThan_Un(e1, e2);
            var checkOnRight = this.Right.CheckIfLessEqualThan_Un(e1, e2);

            return checkOnLeft.Meet(checkOnRight);
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable v1, Variable v2)
        {
            var checkOnLeft = this.Left.CheckIfLessEqualThan(v1, v2);
            var checkOnRight = this.Right.CheckIfLessEqualThan(v1, v2);

            return checkOnLeft.Meet(checkOnRight);
        }

        public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
        {
            var c1 = CheckIfLessEqualThan(e1, e2);
            var c2 = CheckIfLessEqualThan(e2, e1);

            if (c1.IsNormal())
            {
                if (c2.IsNormal())
                {
                    return new FlatAbstractDomain<bool>(c1.BoxedElement && c2.BoxedElement);
                }
                else
                {
                    var lt = CheckIfLessThan(e1, e2);
                    if (lt.IsTrue())  // e1 < e2
                    {
                        return CheckOutcome.False;
                    }
                    else if (lt.IsFalse()) // e1 <= e2 & !(e1 < e2) 
                    {
                        return CheckOutcome.True;
                    }
                }
            }
            else if (c2.IsNormal())
            {
                var gt = CheckIfLessThan(e2, e1);
                if (gt.IsTrue())    // e1 > e2
                {
                    return CheckOutcome.False;
                }
                else if (gt.IsFalse())  // e1 >= e2 & !(e1 > e2)
                {
                    return CheckOutcome.True;
                }
            }

            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
        {
            return this.CheckIfEqual(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfNonZero(Expression e)
        {
            var checkOnLeft = this.Left.CheckIfNonZero(e);
            var checkOnRight = this.Right.CheckIfNonZero(e);

            return checkOnLeft.Meet(checkOnRight);
        }

        public Variable ToVariable(Expression exp)
        {
            return this.ExpressionManager.Decoder.UnderlyingVariable(exp);
        }

        #endregion

        #region IPureExpressionAssignmentsWithForward<Expression> Members

        public void AssumeInDisInterval(Variable x, DisInterval value)
        {
            this.Left.AssumeInDisInterval(x, value);
        }

        void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
            this.AssumeDomainSpecificFact(fact);
        }

        public void Assign(Expression x, Expression exp)
        {
            if (x == null || exp == null)
                return;

            this.Assign(x, exp, TopNumericalDomain<Variable, Expression>.Singleton);
        }

        public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
        {
            // Infer some x >= 0 invariants which requires the information from the two domains
            // The information should be inferred in the pre-state
            List<Expression> geqZero;
            // if (this.ExpressionManager.Encoder!= null)
            {
                geqZero = new GreaterEqualThanZeroConstraints<Variable, Expression>(this.ExpressionManager).InferConstraints(x, exp, preState);
                Contract.Assert(Contract.ForAll(geqZero, condition => condition != null));
            }
            /*      else
                  {
                    geqZero = new List<Expression>();
                    // F: we do not decompile ForAll in the case below
                    Contract.Assume(Contract.ForAll(geqZero, condition => condition != null));
                  }
            */
            Contract.Assert(Contract.ForAll(geqZero, condition => condition != null));

            this.Right.Assign(x, exp, preState);
            this.Left.Assign(x, exp, preState);

            // The information is assumed in the post-state...
            foreach (var condition in geqZero)
            {
                this.TestTrue(condition);
            }
        }

        #endregion

        #region IPureExpressionAssignments<Expression> Members

        public List<Variable> Variables
        {
            get
            {
                return this.Left.Variables.SetUnion(this.Right.Variables);
            }
        }

        public void AddVariable(Variable var)
        {
            this.Left.AddVariable(var);
            this.Right.AddVariable(var);
        }

        public void ProjectVariable(Variable var)
        {
            this.Left.ProjectVariable(var);
            this.Right.ProjectVariable(var);
        }

        public void RemoveVariable(Variable var)
        {
            this.Left.RemoveVariable(var);
            this.Right.RemoveVariable(var);
        }

        public void RenameVariable(Variable OldName, Variable NewName)
        {
            this.Left.RenameVariable(OldName, NewName);
            this.Right.RenameVariable(OldName, NewName);
        }

        #endregion

        #region IPureExpressionTest<Expression> Members

        IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestTrue(Expression guard)
        {
            return this.TestTrue(guard);
        }

        public Pentagons<Variable, Expression> TestTrue(Expression guard)
        {
            Contract.Requires(guard != null);

            Contract.Ensures(Contract.Result<Pentagons<Variable, Expression>>() != null);

            var resultLeft = this.Left.TestTrue(guard);
            var resultRight = this.Right.TestTrue(guard);

            return this.FactoryOfPentagons(resultLeft, resultRight);
        }

        IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestFalse(Expression guard)
        {
            return this.TestFalse(guard);
        }

        public Pentagons<Variable, Expression> TestFalse(Expression guard)
        {
            Contract.Requires(guard != null);

            Contract.Ensures(Contract.Result<Pentagons<Variable, Expression>>() != null);

            var resultLeft = this.Left.TestFalse(guard);
            var resultRight = this.Right.TestFalse(guard);

            return this.FactoryOfPentagons(resultLeft, resultRight);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueGeqZero(Expression exp)
        {
            return this.TestTrueGeqZero(exp);
        }

        public Pentagons<Variable, Expression> TestTrueGeqZero(Expression exp)
        {
            Contract.Requires(exp != null);

            var newLeft = this.Left.TestTrueGeqZero(exp);
            var newRight = this.Right; // We abstract them away

            return this.FactoryOfPentagons(newLeft, newRight);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessThan(Expression exp1, Expression exp2)
        {
            return this.TestTrueLessThan(exp1, exp2);
        }

        public Pentagons<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
        {
            Contract.Requires(exp1 != null);
            Contract.Requires(exp2 != null);
            Contract.Ensures(Contract.Result<Pentagons<Variable, Expression>>() != null);

            var newLeft = this.Left.TestTrueLessThan(exp1, exp2);
            var newRight = this.Right.TestTrueLessThan(exp1, exp2);

            return this.FactoryOfPentagons(newLeft, newRight);
        }

        public Pentagons<Variable, Expression> TestTrueLessThan(Variable v1, Variable v2)
        {
            var newLeft = this.Left.TestTrueLessThan(v1, v2);
            var newRight = this.Right.TestTrueLessThan(v1, v2);

            return this.FactoryOfPentagons(newLeft, newRight);
        }


        public Pentagons<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2,
          WeakUpperBoundsEqual<Variable, Expression> oracleDomain)
        {
            Contract.Requires(exp1 != null);
            Contract.Requires(exp2 != null);
            Contract.Requires(oracleDomain != null);

            Contract.Ensures(Contract.Result<Pentagons<Variable, Expression>>() != null);


            var newLeft = this.Left.TestTrueLessThan(exp1, exp2);
            var newRight = this.Right.TestTrueLessThan(exp1, exp2, oracleDomain);

            return this.FactoryOfPentagons(newLeft, newRight);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            return this.TestTrueLessEqualThan(exp1, exp2);
        }

        public Pentagons<Variable, Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            Contract.Requires(exp1 != null);
            Contract.Requires(exp2 != null);

            Contract.Ensures(Contract.Result<Pentagons<Variable, Expression>>() != null);

            var newLeft = this.Left.TestTrueLessEqualThan(exp1, exp2);
            var newRight = this.Right.TestTrueLessEqualThan(exp1, exp2);

            return this.FactoryOfPentagons(newLeft, newRight);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueEqual(Expression exp1, Expression exp2)
        {
            return this.TestTrueEqual(exp1, exp2);
        }

        public INumericalAbstractDomain<Variable, Expression> TestTrueEqual(Expression exp1, Expression exp2)
        {
            Contract.Assume(exp1 != null);
            Contract.Assume(exp2 != null);

            var resultLeft = this.Left.TestTrueEqual(exp1, exp2);
            var resultRight = this.Right.TestTrueEqual(exp1, exp2);

            return this.FactoryOfPentagons(resultLeft, resultRight);
        }

        public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
        {
            if (CommonChecks.CheckTrivialEquality(exp, this.ExpressionManager.Decoder))
            {
                return CheckOutcome.True;
            }

            var checkLeft = this.Left.CheckIfHolds(exp);
            var checkRight = this.Right.CheckIfHolds(exp, null);

            return checkLeft.Meet(checkRight);
        }
        #endregion

        #region Remove redundancies
        public Pentagons<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            Contract.Requires(oracle != null);

            var left = this.Left.RemoveRedundanciesWith(oracle);
            var right = this.Right.RemoveRedundanciesWith(this.Right).RemoveRedundanciesWith(oracle);

            return this.FactoryOfPentagons(left, right);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            return this.RemoveRedundanciesWith(oracle);
        }

        #endregion

        #region ToString
        public string ToString(Expression exp)
        {
            //      if (this.expManager.Decoder != null)
            {
                return ExpressionPrinter.ToString(exp, expManager.Decoder);
            }
            /*    else
                {
                  return "< missing expression decoder >";
                }*/
        }
        #endregion

        #region Floating point types

        public void SetFloatType(Variable v, ConcreteFloat f)
        {
            // does nothing
        }

        public FlatAbstractDomain<ConcreteFloat> GetFloatType(Variable v)
        {
            return FloatTypes<Variable, Expression>.Unknown;
        }

        #endregion
    }
}
