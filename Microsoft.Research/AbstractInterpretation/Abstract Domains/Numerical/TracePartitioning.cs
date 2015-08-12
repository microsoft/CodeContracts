// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    /// <summary>
    /// We keep a bounded disjunction of abstract domains
    /// </summary>
    public class BoundedDisjunction<Variable, Expression>
      : INumericalAbstractDomain<Variable, Expression>
    {
        #region Private state
        private readonly int MaxDisjuncts = 8; // TODO Want to make it a command line option
        private INumericalAbstractDomain<Variable, Expression>[] disjuncts;

        private INumericalAbstractDomain<Variable, Expression> FirstDisjunct
        {
            get
            {
                return disjuncts[0];
            }
        }

        #endregion

        #region Constructor

        public BoundedDisjunction(INumericalAbstractDomain<Variable, Expression> initial)
        {
            disjuncts = new INumericalAbstractDomain<Variable, Expression>[1];
            disjuncts[0] = initial;
        }

        private BoundedDisjunction(INumericalAbstractDomain<Variable, Expression>[] domains)
        {
            if (domains.Length <= MaxDisjuncts)
            {
                disjuncts = domains;
            }
            else
            {
                disjuncts = new INumericalAbstractDomain<Variable, Expression>[1] { SmashTogether(domains) };
            }
        }

        #endregion

        #region INumericalAbstractDomain<Variable, Expression>Members

        public DisInterval BoundsFor(Expression v)
        {
            var result = FirstDisjunct.BoundsFor(v);

            for (int i = 1; i < disjuncts.Length; i++)
            {
                result = (DisInterval)result.Join(disjuncts[i].BoundsFor(v));
            }

            return result;
        }

        public DisInterval BoundsFor(Variable v)
        {
            var result = FirstDisjunct.BoundsFor(v);

            for (int i = 1; i < disjuncts.Length; i++)
            {
                result = (DisInterval)result.Join(disjuncts[i].BoundsFor(v));
            }

            return result;
        }

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

        public void AssumeInDisInterval(Variable x, DisInterval value)
        {
            for (int i = 0; i < disjuncts.Length; i++)
            {
                disjuncts[i].AssumeInDisInterval(x, value);
            }
        }

        public void AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
            for (int i = 0; i < disjuncts.Length; i++)
            {
                disjuncts[i].AssumeDomainSpecificFact(fact);
            }
        }

        public INumericalAbstractDomain<Variable, Expression> TestTrueGeqZero(Expression exp)
        {
            var result = new INumericalAbstractDomain<Variable, Expression>[disjuncts.Length];

            for (int i = 0; i < disjuncts.Length; i++)
            {
                result[i] = disjuncts[i].TestTrueGeqZero(exp);
            }

            return new BoundedDisjunction<Variable, Expression>(result);
        }

        public INumericalAbstractDomain<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
        {
            var result = new INumericalAbstractDomain<Variable, Expression>[disjuncts.Length];

            for (int i = 0; i < disjuncts.Length; i++)
            {
                result[i] = disjuncts[i].TestTrueLessThan(exp1, exp2);
            }

            return new BoundedDisjunction<Variable, Expression>(result);
        }

        public INumericalAbstractDomain<Variable, Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            var result = new INumericalAbstractDomain<Variable, Expression>[disjuncts.Length];

            for (int i = 0; i < disjuncts.Length; i++)
            {
                result[i] = disjuncts[i].TestTrueLessEqualThan(exp1, exp2);
            }

            return new BoundedDisjunction<Variable, Expression>(result);
        }

        public INumericalAbstractDomain<Variable, Expression> TestTrueEqual(Expression exp1, Expression exp2)
        {
            var result = new INumericalAbstractDomain<Variable, Expression>[disjuncts.Length];

            for (int i = 0; i < disjuncts.Length; i++)
            {
                result[i] = disjuncts[i].TestTrueEqual(exp1, exp2);
            }

            return new BoundedDisjunction<Variable, Expression>(result);
        }

        public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            var result = new INumericalAbstractDomain<Variable, Expression>[disjuncts.Length];

            for (int i = 0; i < disjuncts.Length; i++)
            {
                result[i] = disjuncts[i].RemoveRedundanciesWith(oracle);
            }

            return new BoundedDisjunction<Variable, Expression>(result);
        }

        public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
        {
            FlatAbstractDomain<bool> result = CheckOutcome.Bottom;
            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfGreaterEqualThanZero(exp));
            }

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
        {
            FlatAbstractDomain<bool> result = CheckOutcome.Bottom;
            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfLessThan(e1, e2));
            }

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
        {
            FlatAbstractDomain<bool> result = CheckOutcome.Bottom;
            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfLessThan_Un(e1, e2));
            }

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Variable v1, Variable v2)
        {
            FlatAbstractDomain<bool> result = CheckOutcome.Bottom;
            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfLessThan(v1, v2));
            }

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
        {
            var result = CheckOutcome.Bottom;
            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfLessEqualThan(e1, e2));
            }

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2)
        {
            var result = CheckOutcome.Bottom;
            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfLessThanIncomplete(e1, e2));
            }

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
        {
            FlatAbstractDomain<bool> result = CheckOutcome.Bottom;
            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfLessEqualThan_Un(e1, e2));
            }

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
        {
            FlatAbstractDomain<bool> result = CheckOutcome.Bottom;
            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfEqual(e1, e2));
            }

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
        {
            return this.CheckIfEqual(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfEqualThan(Expression e1, Expression e2)
        {
            FlatAbstractDomain<bool> result = CheckOutcome.Bottom;
            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfEqual(e1, e2));
            }

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable v1, Variable v2)
        {
            var result = CheckOutcome.Bottom;
            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfLessEqualThan(v1, v2));
            }

            return result;
        }



        public FlatAbstractDomain<bool> CheckIfNonZero(Expression e)
        {
            FlatAbstractDomain<bool> result = CheckOutcome.Bottom;
            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfNonZero(e));
            }

            return result;
        }

        #endregion

        #region IAbstractDomain Members

        public bool IsBottom
        {
            get
            {
                for (int i = 0; i < disjuncts.Length; i++)
                {
                    if (!disjuncts[i].IsBottom)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool IsTop
        {
            get
            {
                for (int i = 0; i < disjuncts.Length; i++)
                {
                    if (disjuncts[i].IsTop)
                    {
                        INumericalAbstractDomain<Variable, Expression>[] newDisjunct = new INumericalAbstractDomain<Variable, Expression>[1];
                        newDisjunct[0] = disjuncts[i];
                        disjuncts = newDisjunct;

                        return true;
                    }
                }
                return false;
            }
        }

        public bool LessEqual(IAbstractDomain a)
        {
            bool r;
            if (AbstractDomainsHelper.TryTrivialLessEqual(this, a, out r))
            {
                return r;
            }

            INumericalAbstractDomain<Variable, Expression> leftSmashed = SmashTogether(this, true);
            INumericalAbstractDomain<Variable, Expression> rightSmashed = SmashTogether(a, true);

            return leftSmashed.LessEqual(rightSmashed);
        }

        static private INumericalAbstractDomain<Variable, Expression> SmashTogether(IAbstractDomain boundedDisjunction, bool p)
        {
            INumericalAbstractDomain<Variable, Expression> asNumericalDomain = (INumericalAbstractDomain<Variable, Expression>)boundedDisjunction;

            Debug.Assert(boundedDisjunction != null);

            if (p)
            { // It is a 
                BoundedDisjunction<Variable, Expression> asDisjunction = asNumericalDomain as BoundedDisjunction<Variable, Expression>;

                Debug.Assert(asDisjunction != null);

                return SmashTogether(asDisjunction.disjuncts);
            }
            else
            {
                return asNumericalDomain;
            }
        }

        private static INumericalAbstractDomain<Variable, Expression> SmashTogether(INumericalAbstractDomain<Variable, Expression>[] asDisjunction)
        {
            IAbstractDomain result = asDisjunction[0];

            for (int i = 1; i < asDisjunction.Length; i++)
            {
                result = result.Join(asDisjunction[i]);
            }

            return (INumericalAbstractDomain<Variable, Expression>)result;
        }

        public IAbstractDomain Bottom
        {
            get
            {
                return new BoundedDisjunction<Variable, Expression>((INumericalAbstractDomain<Variable, Expression>)this.FirstDisjunct.Bottom);
            }
        }

        public IAbstractDomain Top
        {
            get
            {
                return new BoundedDisjunction<Variable, Expression>((INumericalAbstractDomain<Variable, Expression>)this.FirstDisjunct.Top);
            }
        }

        public IAbstractDomain Join(IAbstractDomain a)
        {
            IAbstractDomain result;
            if (AbstractDomainsHelper.TryTrivialJoin(this, a, out result))
            {
                return result;
            }

            BoundedDisjunction<Variable, Expression> asDisjunctionDomain = a as BoundedDisjunction<Variable, Expression>;

            Debug.Assert(asDisjunctionDomain != null);

            INumericalAbstractDomain<Variable, Expression>[] joinDisjuncts = new INumericalAbstractDomain<Variable, Expression>[disjuncts.Length + asDisjunctionDomain.disjuncts.Length];

            // Copy both the domains
            for (int i = 0; i < disjuncts.Length; i++)
            {
                joinDisjuncts[i] = disjuncts[i];
            }

            for (int i = 0; i < asDisjunctionDomain.disjuncts.Length; i++)
            {
                joinDisjuncts[disjuncts.Length + i] = asDisjunctionDomain.disjuncts[i];
            }

            return new BoundedDisjunction<Variable, Expression>(joinDisjuncts);
        }

        public IAbstractDomain Meet(IAbstractDomain a)
        {
            IAbstractDomain result;
            if (AbstractDomainsHelper.TryTrivialMeet(this, a, out result))
            {
                return result;
            }

            BoundedDisjunction<Variable, Expression> asDisjunctionDomain = a as BoundedDisjunction<Variable, Expression>;

            Debug.Assert(asDisjunctionDomain != null);

            INumericalAbstractDomain<Variable, Expression> left = SmashTogether(disjuncts);
            INumericalAbstractDomain<Variable, Expression> right = SmashTogether(asDisjunctionDomain.disjuncts);

            return new BoundedDisjunction<Variable, Expression>((INumericalAbstractDomain<Variable, Expression>)left.Meet(right));
        }

        public IAbstractDomain Widening(IAbstractDomain prev)
        {
            IAbstractDomain result;
            if (AbstractDomainsHelper.TryTrivialJoin(this, prev, out result))
            {
                return result;
            }

            BoundedDisjunction<Variable, Expression> asDisjunctionDomain = prev as BoundedDisjunction<Variable, Expression>;

            Debug.Assert(asDisjunctionDomain != null);

            INumericalAbstractDomain<Variable, Expression> left = SmashTogether(disjuncts);
            INumericalAbstractDomain<Variable, Expression> right = SmashTogether(asDisjunctionDomain.disjuncts);

            return new BoundedDisjunction<Variable, Expression>((INumericalAbstractDomain<Variable, Expression>)left.Widening(right));
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            INumericalAbstractDomain<Variable, Expression>[] cloned = new INumericalAbstractDomain<Variable, Expression>[disjuncts.Length];

            for (int i = 0; i < disjuncts.Length; i++)
            {
                cloned[i] = (INumericalAbstractDomain<Variable, Expression>)disjuncts[i].Clone();
            }

            return new BoundedDisjunction<Variable, Expression>(cloned);
        }

        #endregion

        #region IPureExpressionAssignments<Expression> Members

        public List<Variable> Variables
        {
            get
            {
                var result = new Set<Variable>();
                for (int i = 0; i < disjuncts.Length; i++)
                {
                    result.AddRange(disjuncts[i].Variables);
                }

                return result.ToList();
            }
        }

        public void AddVariable(Variable var)
        {
            for (int i = 0; i < disjuncts.Length; i++)
            {
                disjuncts[i].AddVariable(var);
            }
        }

        public void Assign(Expression x, Expression exp)
        {
            for (int i = 0; i < disjuncts.Length; i++)
            {
                disjuncts[i].Assign(x, exp);
            }
        }

        public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
        {
            for (int i = 0; i < disjuncts.Length; i++)
            {
                disjuncts[i].Assign(x, exp, preState);
            }
        }

        public void ProjectVariable(Variable var)
        {
            for (int i = 0; i < disjuncts.Length; i++)
            {
                disjuncts[i].ProjectVariable(var);
            }
        }

        public void RemoveVariable(Variable var)
        {
            for (int i = 0; i < disjuncts.Length; i++)
            {
                disjuncts[i].RemoveVariable(var);
            }
        }

        public void RenameVariable(Variable OldName, Variable NewName)
        {
            for (int i = 0; i < disjuncts.Length; i++)
            {
                disjuncts[i].RenameVariable(OldName, NewName);
            }
        }

        #endregion

        #region IPureExpressionTest<Expression> Members

        public IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
        {
            INumericalAbstractDomain<Variable, Expression>[] result = new INumericalAbstractDomain<Variable, Expression>[disjuncts.Length];

            for (int i = 0; i < disjuncts.Length; i++)
            {
                result[i] = (INumericalAbstractDomain<Variable, Expression>)disjuncts[i].TestTrue(guard);
            }

            return Reduce(result);
        }

        public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
        {
            INumericalAbstractDomain<Variable, Expression>[] result = new INumericalAbstractDomain<Variable, Expression>[disjuncts.Length];

            for (int i = 0; i < disjuncts.Length; i++)
            {
                result[i] = (INumericalAbstractDomain<Variable, Expression>)disjuncts[i].TestFalse(guard);
            }

            return Reduce(result);
        }

        public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
        {
            FlatAbstractDomain<bool> result = CheckOutcome.Bottom;

            for (int i = 0; i < disjuncts.Length; i++)
            {
                result = result.Join(disjuncts[i].CheckIfHolds(exp));
            }

            return result;
        }

        #endregion

        #region IAssignInParallel<Expression> Members

        public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
        {
            for (int i = 0; i < disjuncts.Length; i++)
            {
                disjuncts[i].AssignInParallel(sourcesToTargets, convert);
            }
        }

        #endregion

        #region To
        public T To<T>(IFactory<T> factory)
        {
            if (this.IsBottom)
                return factory.Constant(false);

            if (this.IsTop)
                return factory.Constant(true);

            T result = factory.IdentityForOr;

            for (int i = 0; i < disjuncts.Length; i++)
            {
                T atom = disjuncts[i].To(factory);
                result = factory.Or(result, atom);
            }

            return result;
        }

        #endregion

        public string Statistics
        {
            get
            {
                return string.Empty;
            }
        }

        public override string ToString()
        {
            if (this.IsTop)
            {
                return "Top";
            }
            else if (disjuncts.Length == 1)
            {
                return this.FirstDisjunct.ToString();
            }
            else
            {
                return "Bounded disjunction with " + disjuncts.Length + " disjuncts";
            }
        }

        public string ToString(Expression exp)
        {
            return "< missing expression decoder >";
        }

        /// <summary>
        /// For the moment the reduction just get rid of the bottom elements
        /// </summary>
        private BoundedDisjunction<Variable, Expression> Reduce(INumericalAbstractDomain<Variable, Expression>[] domains)
        {
            bool[] isBottom = new bool[domains.Length];
            int countBottom = 0;

            for (int i = 0; i < domains.Length; i++)
            {
                if (domains[i].IsBottom)
                {
                    isBottom[i] = true;
                    countBottom++;
                }
            }

            if (countBottom == 0 || countBottom == domains.Length)
            {
                return new BoundedDisjunction<Variable, Expression>(domains);
            }
            else
            {
                INumericalAbstractDomain<Variable, Expression>[] tmp = new INumericalAbstractDomain<Variable, Expression>[domains.Length - countBottom];

                for (int i = 0, k = 0; i < domains.Length; i++)
                {
                    if (!isBottom[i])
                    {
                        tmp[k] = domains[i];
                        k++;
                    }
                }

                return new BoundedDisjunction<Variable, Expression>(tmp);
            }
        }

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

        #region INumericalAbstractDomainQuery<Variable,Expression> Members

        public Variable ToVariable(Expression exp)
        {
            return default(Variable);
        }

        #endregion
    }
}
