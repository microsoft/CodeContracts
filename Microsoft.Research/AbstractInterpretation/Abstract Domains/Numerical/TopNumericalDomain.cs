// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    /// <summary>
    /// A numerical abstract domain that does nothing
    /// </summary>  
    [ContractVerification(true)]
    public class TopNumericalDomain<Variable, Expression> :
      INumericalAbstractDomain<Variable, Expression>,
      IIntervalAbstraction<Variable, Expression>
    {
        #region Private static state
        static private TopNumericalDomain<Variable, Expression> cached;
        #endregion


        /// <summary>
        /// The only instance of the TopNumericalDomain
        /// </summary>
        public static TopNumericalDomain<Variable, Expression> Singleton
        {
            get
            {
                Contract.Ensures(Contract.Result<TopNumericalDomain<Variable, Expression>>() != null);

                if (cached == null)
                {
                    cached = new TopNumericalDomain<Variable, Expression>();
                }

                return cached;
            }
        }

        #region Private constructor
        private TopNumericalDomain()
        {
            // do nothing
        }
        #endregion

        #region INumericalAbstractDomain<Variable, Expression>Members

        public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> c)
        {
        }

        public void AssumeInDisInterval(Variable x, DisInterval value)
        {
            // does nothing
        }

        virtual public void AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
        }

        public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            return this;
        }

        public DisInterval BoundsFor(Expression v)
        {
            return DisInterval.UnknownInterval;
        }

        public DisInterval BoundsFor(Variable v)
        {
            return DisInterval.UnknownInterval;
        }

        public FlatAbstractDomain<bool> CheckIfNonZero(Expression exp)
        {
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
        {
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
        {
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Variable v1, Variable v2)
        {
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
        {
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2)
        {
            return CheckOutcome.Top;
        }
        public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
        {
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
        {
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
        {
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
        {
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable v1, Variable v2)
        {
            return CheckOutcome.Top;
        }

        #endregion

        #region IAbstractDomain Members

        public bool IsBottom
        {
            get { return false; }
        }

        public bool IsTop
        {
            get { return true; }
        }

        public bool LessEqual(IAbstractDomain a)
        {
            return a == this;
        }

        public IAbstractDomain Bottom
        {
            get { return this; }
        }

        public IAbstractDomain Top
        {
            get { return this; }
        }

        public IAbstractDomain Join(IAbstractDomain a)
        {
            return this;
        }

        public IAbstractDomain Meet(IAbstractDomain a)
        {
            return this;
        }

        public IAbstractDomain Widening(IAbstractDomain prev)
        {
            return this;
        }

        public T To<T>(IFactory<T> factory)
        {
            return factory.Constant(true);
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this;
        }

        #endregion

        #region IPureExpressionAssignmentsWithForward<Expression> Members

        public void Assign(Expression x, Expression exp)
        {
            // do nothing
        }

        public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
        {
            // do nothing
        }

        #endregion

        #region IPureExpressionAssignments<Expression> Members

        public List<Variable> Variables
        {
            get { return new List<Variable>(); }
        }

        public void AddVariable(Variable var)
        {
        }

        public void ProjectVariable(Variable var)
        {
        }

        public void RemoveVariable(Variable var)
        {
        }

        public void RenameVariable(Variable OldName, Variable NewName)
        {
        }

        #endregion

        #region IPureExpressionTest<Expression> Members

        public IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
        {
            return this;
        }

        public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
        {
            return this;
        }

        public INumericalAbstractDomain<Variable, Expression> TestTrueGeqZero(Expression/*!*/ exp)
        {
            return this;
        }

        public INumericalAbstractDomain<Variable, Expression> TestTrueLessThan(Expression/*!*/ exp1, Expression/*!*/ exp2)
        {
            return this;
        }

        public INumericalAbstractDomain<Variable, Expression> TestTrueLessEqualThan(Expression/*!*/ exp1, Expression/*!*/ exp2)
        {
            return this;
        }

        public INumericalAbstractDomain<Variable, Expression> TestTrueEqual(Expression/*!*/ exp1, Expression/*!*/ exp2)
        {
            return this;
        }

        public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
        {
            return CheckOutcome.Top;
        }

        #endregion

        #region To

        public string ToString(Expression exp)
        {
            return "< missing expression decoder >";
        }

        public override string ToString()
        {
            return "TopAD";
        }
        #endregion

        #region INumericalAbstractDomain<Variable, Expression>Members

        public List<Pair<Variable, Int32>> IntConstants
        {
            get
            {
                return new List<Pair<Variable, Int32>>();
            }
        }

        public IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
        {
            yield break; //not implemented
        }

        public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
        {
            yield break; //not implemented
        }

        public IEnumerable<Expression> LowerBoundsFor(Variable v, bool strict)
        {
            yield break; //not implemented
        }

        public IEnumerable<Expression> UpperBoundsFor(Variable v, bool strict)
        {
            yield break; //not implemented
        }

        public IEnumerable<Variable> EqualitiesFor(Variable v)
        {
            yield break; //not implemented
        }

        #endregion

        #region INumericalAbstractDomainQuery<Variable,Expression> Members

        public Variable ToVariable(Expression exp)
        {
            // This abstract domain knows nothing, not even how to read the variable underlying an expression
            return default(Variable);
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

        #region IIntervalAbstraction<Variable,Expression> Members

        bool IIntervalAbstraction<Variable, Expression>.LessEqual(IIntervalAbstraction<Variable, Expression> other)
        {
            return this.LessEqual(other);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.Join(IIntervalAbstraction<Variable, Expression> other)
        {
            var result = this.Join(other) as IIntervalAbstraction<Variable, Expression>;

            Contract.Assume(result != null);

            return result;
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.Widening(IIntervalAbstraction<Variable, Expression> other)
        {
            var result = this.Widening(other) as IIntervalAbstraction<Variable, Expression>;

            Contract.Assume(result != null);

            return result;
        }

        List<Pair<Variable, Int32>> IIntervalAbstraction<Variable, Expression>.IntConstants
        {
            get { return new List<Pair<Variable, Int32>>(); }
        }

        void IIntervalAbstraction<Variable, Expression>.AssumeInDisInterval(Variable x, DisInterval value)
        {
            this.AssumeInDisInterval(x, value);
        }

        void IIntervalAbstraction<Variable, Expression>.Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
        {
            this.Assign(x, exp, preState);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            var result = this.RemoveRedundanciesWith(oracle) as IIntervalAbstraction<Variable, Expression>;
            Contract.Assume(result != null);

            return result;
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrue(Expression exp)
        {
            var result = this.TestTrue(exp) as IIntervalAbstraction<Variable, Expression>;
            Contract.Assume(result != null);

            return result;
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestFalse(Expression exp)
        {
            var result = this.TestFalse(exp) as IIntervalAbstraction<Variable, Expression>;
            Contract.Assume(result != null);

            return result;
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessThan(Expression exp1, Expression exp2)
        {
            var result = this.TestTrueLessThan(exp1, exp2) as IIntervalAbstraction<Variable, Expression>;
            Contract.Assume(result != null);

            return result;
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessThan(Variable v1, Variable v2)
        {
            return this;
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            var result = this.TestTrueLessEqualThan(exp1, exp2) as IIntervalAbstraction<Variable, Expression>;
            Contract.Assume(result != null);

            return result;
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessEqualThan(Variable v1, Variable v2)
        {
            return this;
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueEqual(Expression exp1, Expression exp2)
        {
            var result = this.TestTrueEqual(exp1, exp2) as IIntervalAbstraction<Variable, Expression>;
            Contract.Assume(result != null);

            return result;
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueGeqZero(Expression exp)
        {
            var result = this.TestTrueGeqZero(exp) as IIntervalAbstraction<Variable, Expression>;
            Contract.Assume(result != null);

            return result;
        }

        #endregion
    }
}
