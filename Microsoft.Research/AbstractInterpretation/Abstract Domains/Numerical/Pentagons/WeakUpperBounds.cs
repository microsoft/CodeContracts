// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    /// <summary>
    /// The abstract domain to keep track of relations in the form of "x &lt; y".
    /// It does not (always) close, so it is less precise than octagons
    /// </summary>
    [ContractVerification(true)]
    public class WeakUpperBounds<Variable, Expression> :
        FunctionalAbstractDomain<WeakUpperBounds<Variable, Expression>, Variable, SetOfConstraints<Variable>>,
          INumericalAbstractDomain<Variable, Expression>
    {
        #region Invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.expManager != null);
            Contract.Invariant(constraintsForAssignment != null);
        }

        #endregion

        #region Private state

        readonly protected ExpressionManagerWithEncoder<Variable, Expression> expManager;
        readonly private List<IConstraintsForAssignment<Variable, Expression, INumericalAbstractDomainQuery<Variable, Expression>>> constraintsForAssignment;

        #endregion

        #region Constructor

        public WeakUpperBounds(ExpressionManagerWithEncoder<Variable, Expression> expManager)
        {
            Contract.Requires(expManager != null);

            this.expManager = expManager;

            constraintsForAssignment = new List<IConstraintsForAssignment<Variable, Expression, INumericalAbstractDomainQuery<Variable, Expression>>>()
      {
        new LessThanConstraints<Variable, Expression>(expManager)
      };
        }

        private WeakUpperBounds(WeakUpperBounds<Variable, Expression> other)
          : base(other)
        {
            Contract.Requires(other != null);

            Contract.Assume(other.constraintsForAssignment != null);
            Contract.Assume(other.expManager != null);

            this.expManager = other.expManager;
            constraintsForAssignment = other.constraintsForAssignment;
        }

        #endregion

        #region Cloning

        override public object Clone()
        {
            return this.DuplicateMe();
        }

        private WeakUpperBounds<Variable, Expression> DuplicateMe()
        {
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            return new WeakUpperBounds<Variable, Expression>(this);
        }

        /// <summary>
        /// Do the assignment, improving the precision using the oracle domain, which gives informations on the pre-state
        /// </summary>
        public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> oracleDomain)
        {
            var LTConstraints = new List<Expression>();

            // Discover new constraints
            foreach (var constraintsFetcher in constraintsForAssignment)
            {
                Contract.Assume(constraintsFetcher != null);

                var constrs = constraintsFetcher.InferConstraints(x, exp, oracleDomain);
                LTConstraints.AddRange(constrs);
            }

            // Assume them
            foreach (var newConstraint in LTConstraints)
            {
                this.TestTrue(newConstraint);
            }
        }

        public void Assign(Expression x, Expression exp)
        {
            Contract.Assume(x != null);
            Contract.Assume(exp != null);

            this.Assign(x, exp, TopNumericalDomain<Variable, Expression>.Singleton);
        }

        /// <summary>
        /// As we keep no information on bounds, the only thing to do is to return the top interval
        /// </summary>
        public DisInterval BoundsFor(Expression v)
        {
            return DisInterval.UnknownInterval;
        }

        public DisInterval BoundsFor(Variable v)
        {
            return DisInterval.UnknownInterval;
        }

        public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            return CheckIfHolds(exp, TopNumericalDomain<Variable, Expression>.Singleton);
        }

        public FlatAbstractDomain<bool> CheckIfHolds(Expression exp, INumericalAbstractDomain<Variable, Expression> oracleDomain)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            Contract.Assume(exp != null);

            return new WUBCheckIfHoldsVisitor(this.expManager.Decoder).Visit(exp, this);
        }

        public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            Polynomial<Variable, Expression> pol;

            if (Polynomial<Variable, Expression>.TryToPolynomialForm(exp, this.expManager.Decoder, out pol))
            {
                Variable x, y;
                Rational k;

                if (pol.TryMatch_XMinusYPlusK(out x, out y, out k))
                {
                    var lt = this.CheckIfLessThan(y, x);

                    if (lt.IsNormal() && lt.BoxedElement && k >= -1)
                    { // if we have to check x - y + k >= 0, and we know that k >= -1 and x - y > 0, then this is true
                        return lt;
                    }
                }
            }
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfNonZero(Expression e)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            // TODO
            return CheckOutcome.Top;
        }

        /// <summary>
        /// Check if the expression <code>e1</code> is strictly smaller than <code>e2</code>
        /// </summary>
        public FlatAbstractDomain<bool> CheckIfLessThan(Variable e1, Variable e2)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            SetOfConstraints<Variable> bounds;
            if (this.TryGetValue(e1, out bounds))
            {
                if (bounds.IsBottom)
                {
                    return CheckOutcome.True;
                }
                else if (bounds.Contains(e2))
                {
                    return CheckOutcome.True;
                }

                if (bounds.IsNormal())
                {
                    foreach (var other in bounds.Values)
                    {
                        SetOfConstraints<Variable> otherBounds;
                        if (this.TryGetValue(other, out otherBounds))
                        {
                            if (otherBounds.IsNormal() && otherBounds.Contains(e2))
                            { // e1 < other , other < e2 => e1 < e2
                                return CheckOutcome.True;
                            }
                        }
                    }
                }
            }
            else if (this.TryGetValue(e2, out bounds))
            {
                if (bounds.IsBottom)
                {
                    return CheckOutcome.True;
                }
                else if (bounds.Contains(e1))
                {
                    return CheckOutcome.False;
                }
            }

            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
        {
            if (e1 == null || e2 == null)
            {
                return CheckOutcome.Top;
            }

            var decoder = this.expManager.Decoder;

            e1 = decoder.Stripped(e1);
            e2 = decoder.Stripped(e2);

            var e1Var = decoder.UnderlyingVariable(e1);
            var e2Var = decoder.UnderlyingVariable(e2);

            var result = this.CheckIfLessThan(e1Var, e2Var);

            if (result.IsNormal())
            {
                return result;
            }

            Polynomial<Variable, Expression> e1LTe2;

            if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessThan, e1, e2, decoder, out e1LTe2))
            {
                Contract.Assume(e1LTe2.Relation == ExpressionOperator.LessThan, "Exprecting a LT");

                if (e1LTe2.Left.Length == 2 && e1LTe2.IsOctagonForm)
                {
                    Contract.Assume(e1LTe2.Degree == 1); // We know it has 2 variables, and unary coefficients, hence the polynomial degree is 1

                    Contract.Assert(e1LTe2.Right != null); // Adding the assertion as the backward visit does not go far enough

                    Contract.Assume(e1LTe2.Right.Length > 0);

                    var monomial0 = e1LTe2.Left[0];
                    var monomial1 = e1LTe2.Left[1];

                    Contract.Assume(monomial0.Degree == 1);
                    Contract.Assume(monomial1.Degree == 1);

                    var k1 = monomial0.K;
                    var x1 = monomial0.VariableAt(0);

                    var k2 = monomial1.K;
                    var x2 = monomial1.VariableAt(0);

                    var k = e1LTe2.Right[0].K;

                    bool isImplied = false;

                    if (k >= 0)
                    {
                        if (k1 == 1 && k2 == -1)  // x1 - x2 < 1, that is x1 < x2 + 1 
                        {
                            isImplied = Check(x1, x2);
                        }
                        else if (k1 == -1 && k2 == 1) // -x1 + x2 < 1, that is x2 < x1 + 1
                        {
                            isImplied = Check(x2, x1);
                        }
                    }

                    if (isImplied)
                    {
                        return CheckOutcome.True;
                    }
                }
                else
                {
                    Polynomial<Variable, Expression> e1AsPolynomial;
                    // Try to see if e1 == "a1 x1 + a2 x2 + ... " is such that each xi < e2, and 0<= sum ai <= 1 and \forall i. ai > 0
                    if (Polynomial<Variable, Expression>.TryToPolynomialForm(e1, decoder, out e1AsPolynomial) && e1AsPolynomial.Degree == 1)
                    {
                        var sum = Rational.For(0);

                        // Checks two things: 
                        //  1. All the x are < e2
                        //  2. \forall ai. ai >= 0 && 0<= a1 + a2 + ... + an <= 1
                        foreach (var m in e1AsPolynomial.Left)
                        {
                            if (!m.IsConstant)
                            {
                                var x = m.VariableAt(0);

                                // Checks that x < e2
                                SetOfConstraints<Variable> values;
                                if (!(this.TryGetValue(x, out values) && values.IsNormal() && values.Contains(e2Var)))
                                {
                                    return CheckOutcome.Top;
                                }
                            }

                            if (m.K <= 0)
                            {
                                return CheckOutcome.Top;
                            }
                            else
                            {
                                try
                                {
                                    sum += m.K;
                                }
                                catch (ArithmeticExceptionRational)
                                {
                                    return CheckOutcome.Top;
                                }
                            }
                        }

                        if (sum <= 1)
                        {
                            Contract.Assume(sum >= 0, "At this point the sum of the values must be positive");
                            return CheckOutcome.True;
                        }
                    }
                }
            }

            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2)
        {
            return this.CheckIfLessThan(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            return this.CheckIfLessEqualThan(e1, e2);
        }

        /// <summary>
        /// Check if the expression <code>e1</code> is strictly smaller than <code>e2</code> using, 
        /// if needed the abstract domain <code>oracleDomain</code> to refine the information
        /// </summary>
        public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2,
          IIntervalAbstraction<Variable, Expression> oracleDomain)
        {
            Contract.Requires(oracleDomain != null);
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            if (e1 == null || e2 == null)
            {
                return CheckOutcome.Top;
            }

            var decoder = this.expManager.Decoder;

            e1 = decoder.Stripped(e1);
            e2 = decoder.Stripped(e2);

            var directTry = CheckIfLessThan(e1, e2);
            if (!directTry.IsTop)
            {
                return directTry;
            }

            var result = CommonChecks.CheckLessThan(e1, e2, oracleDomain, decoder);

            if (!result.IsTop)
            {
                return result;
            }

            switch (this.expManager.Decoder.OperatorFor(e1))
            {
                case ExpressionOperator.Modulus:
                    {
                        // It is " (e1Left % e1Right) < e2 "
                        var e1Left = decoder.LeftExpressionFor(e1);
                        var e1Right = decoder.RightExpressionFor(e1);
                        var e1RightStripped = decoder.Stripped(e1Right);

                        if (e2.Equals(e1RightStripped))
                        {
                            var valFore1Left = oracleDomain.BoundsFor(e1Left);

                            if (valFore1Left.LowerBound >= 0 || oracleDomain.BoundsFor(e2).LowerBound >= 0)
                            {
                                return CheckOutcome.True;
                            }
                        }
                    }
                    break;

                case ExpressionOperator.ShiftRight:
                    {
                        // It is "(e1Left >> e2Left) < e2" 
                        var e1Left = decoder.LeftExpressionFor(e1);
                        var e1Right = decoder.RightExpressionFor(e1);

                        if (oracleDomain.BoundsFor(e1Left).LowerBound >= 0)
                        {
                            return this.CheckIfLessThan(e1Left, e2, oracleDomain);   // check if e1Left < e2
                        }
                    }
                    break;

                case ExpressionOperator.Addition:
                    {
                        // it is (e1Left + e1right) < e2
                        var e1Left = decoder.LeftExpressionFor(e1);
                        var e1Right = decoder.RightExpressionFor(e1);

                        int k;
                        // Try the transitive closure of length 1 (see the check k == 1 !!!)
                        // this is useful to speed up checks in the array analysis
                        if (decoder.IsConstantInt(e1Right, out k) && k == 1)
                        {
                            // it is (e1Left + 1) < e2
                            foreach (var upp in this.UpperBoundsFor(e1Left, true))
                            {
                                if (this.UpperBoundsFor(upp, true).Contains(e2))
                                    return CheckOutcome.True;
                            }
                        }
                    }
                    break;
            }

            return CheckOutcome.Top;
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
            return LowerBoundsFor(this.expManager.Decoder.UnderlyingVariable(v), strict);
        }

        public IEnumerable<Expression> LowerBoundsFor(Variable vVar, bool strict)
        {
            foreach (var exp_pair in this.Elements)
            {
                var valueSet = exp_pair.Value;
                Contract.Assume(valueSet != null);

                if (!valueSet.IsTop && valueSet.Contains(vVar))
                {
                    yield return this.expManager.Encoder.VariableFor(exp_pair.Key);
                }
            }
        }

        public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
        {
            return UpperBoundsFor(this.expManager.Decoder.UnderlyingVariable(v), strict);
        }

        public IEnumerable<Expression> UpperBoundsFor(Variable vVar, bool strict)
        {
            SetOfConstraints<Variable> bounds;
            if (this.TryGetValue(vVar, out bounds) && !bounds.IsTop)
            {
                foreach (var upp in bounds.Values)
                {
                    yield return this.expManager.Encoder.VariableFor(upp);
                }
            }
        }

        public IEnumerable<Variable> EqualitiesFor(Variable v)
        {
            return new Set<Variable>();
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            e1 = this.expManager.Decoder.Stripped(e1);
            e2 = this.expManager.Decoder.Stripped(e2);

            var result = this.HelperForCheckLessEqualThan(e1, e2);
            if (result.IsTop)
            {
                Polynomial<Variable, Expression> tmp;
                if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessEqualThan, e1, e2, this.expManager.Decoder, out tmp))
                {
                    if (new VisitorForCheckLessEqualThan().Visit(tmp, this, ref result))
                    {
                        return result;
                    }
                }
            }
            return result;
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
        {
            return this.CheckIfLessEqualThan(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
        {
            FlatAbstractDomain<bool> c1, c2;

            if ((c1 = CheckIfLessEqualThan(e1, e2)).IsNormal() && (c2 = CheckIfLessEqualThan(e2, e1)).IsNormal())
            {
                return new FlatAbstractDomain<bool>(c1.BoxedElement && c2.BoxedElement);
            }

            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
        {
            return this.CheckIfEqual(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable e1, Variable e2)
        {
            SetOfConstraints<Variable> upp;
            if (this.TryGetValue(e1, out upp))
            {
                if (upp.IsNormal() && upp.Contains(e2))
                {
                    return CheckOutcome.True;
                }
            }

            return CheckOutcome.Top;
        }


        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2,
          IIntervalAbstraction<Variable, Expression> oracleDomain)
        {
            Contract.Requires(oracleDomain != null);
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            if (e1 == null || e2 == null)
            {
                return CheckOutcome.Top;
            }

            var decoder = this.expManager.Decoder;

            e1 = decoder.Stripped(e1);
            e2 = decoder.Stripped(e2);

            var res = this.CheckIfLessThan(e1, e2, oracleDomain);

            if (!res.IsNormal())
            {
                Polynomial<Variable, Expression> pol;
                if (Polynomial<Variable, Expression>.TryToPolynomialForm(e1, decoder, out pol))
                {
                    Variable x;
                    Rational k;
                    if (pol.TryMatch_YPlusK(out x, out k) && k == 1)
                    {
                        var e2Exp = decoder.UnderlyingVariable(e2);

                        res = this.CheckIfLessThan(x, e2Exp);
                        if (!res.IsBottom)
                            return res;
                        else // if (res.BoxedElement == false)
                            return CheckOutcome.Top;
                    }
                }
                if (Polynomial<Variable, Expression>.TryToPolynomialForm(e2, decoder, out pol))
                {
                    Variable x;
                    Rational k;
                    if (pol.TryMatch_YMinusK(out x, out k) && k == -1)
                    {
                        var e1Exp = decoder.UnderlyingVariable(e1);
                        res = this.CheckIfLessThan(e1Exp, x);
                        if (!res.IsBottom)
                            return res;
                        else // if (res.BoxedElement == false)
                            return CheckOutcome.Top;
                    }
                }
                return res;
            }
            else if (res.IsTrue())
            {
                return res;
            }
            else
            {
                Contract.Assume(res.IsFalse());
                // we know e1 >= e2, let us check if e1 > e2
                if (this.CheckIfLessThan(e2, e1).IsTrue())
                {
                    return CheckOutcome.False;
                }

                return CheckOutcome.Top;
            }
        }

        [ContractVerification(false)] // Turning off verification of this method, as it is not more really interesting: it is never called in actual runs of CLousot (only in some regression)
        public void AssignInParallel(
          Dictionary<Variable, FList<Variable>> sourcesToTargets,
          Converter<Variable, Expression> convert, IIntervalAbstraction<Variable, Expression> oracleDomain)
        {
            // Recall that we are solving the following problem. We have a new alphabet of variables V and an old alphabet of variables V'.
            // The current state holds constraints over old variables v'1 <= v'2.
            //
            // The sourcesToTargets map represents assignments from old variables to new variables. An entry
            //   v' -> v1, v2, ... vn represents the assignments v1 := v', v2 := v', ... vn := v'
            //
            // Thus, if the current state holds  v'1 <= v'2, and we have
            //   v'1 -> v11, v12, ..., v1n
            //   v'2 -> v21, v22, ..., v2m
            //
            // Then "all" information about v'1 <= v'2 expressed in the new state would be the conjunction of constraints
            //   v1i <= v2j, for i=1..n and j=1..m
            //
            // Clearly, we don't generally want to produce this quadratic number of constraints (although in practice it is rare
            // to see more than 1 new variable assigned the same old variable).
            //
            // Thus, in practice, we might pick a "canonical" new name for an old variable, say the first in the list (v11 and v21).
            // This would then give us a single new constraint for v'1 <= v'2, namely  v11 <= v21.
            //
            // To compute this, we use the sourcesToTargets map as our canonical map M from old to new, by simply using the first in the list
            // as the canonical new variable.

            // Then it is simple to produce the new state: iterate over all constraints v'1 <= v'2 in the old state, and simply put
            //   M(v'1) <= M(v'2) into the new state.

            // The fact that we have to do this computation "in-place" is annoying. It would be simpler to produce a new state.
            // Thus, we will compute the new mappings on the side, then clear the current state, then add the new mappings back.

            // adding the domain-generated variables to the map as identity
            var oldToNewMap = new Dictionary<Variable, FList<Variable>>(sourcesToTargets);
            if (!this.IsTop)
            {
                var decoder = this.expManager.Decoder;

                foreach (var v in this.Variables)
                {
                    if (decoder.IsSlackVariable(v))
                    {
                        oldToNewMap.Add(v, FList<Variable>.Cons(v, FList<Variable>.Empty));
                    }
                }
            }

            // when x has several targets including itself, the canonical element shouldn't be itself
            foreach (var sourceToTargets in sourcesToTargets)
            {
                var source = sourceToTargets.Key;
                var targets = sourceToTargets.Value;
                if (targets.Length() > 1 && targets.Head.Equals(source))
                {
                    var newTargets = FList<Variable>.Cons(targets.Tail.Head, FList<Variable>.Cons(source, targets.Tail.Tail));
                    oldToNewMap[source] = newTargets;
                }
            }

            // The invariant is \forall x. newConstraints[x].EmbeddedValues_Unsafe == newMappings[x]
            var newMappings = new Dictionary<Variable, Set<Variable>>(this.Count);
            var newConstraints = new Dictionary<Variable, SetOfConstraints<Variable>>(this.Count);

            foreach (var oldLeft_pair in this.Elements)
            {
                if (!oldToNewMap.ContainsKey(oldLeft_pair.Key))
                {
                    continue;
                }

                Variable newLeft = oldToNewMap[oldLeft_pair.Key].Head; // our canonical element

                var oldBounds = oldLeft_pair.Value;
                if (!oldBounds.IsNormal())
                {
                    continue;
                }

                foreach (var oldRight in oldBounds.Values)
                {
                    if (!oldToNewMap.ContainsKey(oldRight))
                    {
                        continue;
                    }

                    // This case is so so common that we want to specialize it
                    if (oldToNewMap[oldRight].Length() == 1)
                    {
                        var newRight = oldToNewMap[oldRight].Head; // our canonical element
                        AddUpperBound(newLeft, newRight, newMappings, newConstraints);
                    }
                    else
                    {
                        foreach (var newRight in oldToNewMap[oldRight].GetEnumerable())
                        {
                            AddUpperBound(newLeft, newRight, newMappings, newConstraints);
                        }
                    }
                }

                // There are some more elements
                for (var list = oldToNewMap[oldLeft_pair.Key].Tail; list != null; list = list.Tail)
                {
                    if (newConstraints.ContainsKey(newLeft))
                    {
                        foreach (var con in newConstraints[newLeft].Values)
                        {
                            AddUpperBound(list.Head, con, newMappings, newConstraints);
                        }
                    }
                }
            }

            // Update
            this.SetElements(newConstraints);
        }

        /// <summary>
        /// Add or materialize the map entry and add the value
        /// </summary>
        [ContractVerification(false)]
        static internal void AddUpperBound(Variable key, Variable upperBound,
          Dictionary<Variable, Set<Variable>> map, Dictionary<Variable, SetOfConstraints<Variable>> newCostraints)
        {
            Contract.Requires(map != null);
            Contract.Requires(newCostraints != null);

            Set<Variable> bounds;
            if (!map.TryGetValue(key, out bounds))
            {
                bounds = new Set<Variable>();
                map.Add(key, bounds);
            }
            Contract.Assume(bounds != null);

            bounds.Add(upperBound);
            newCostraints[key] = new SetOfConstraints<Variable>(bounds, false);
        }

        protected override WeakUpperBounds<Variable, Expression> Factory()
        {
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            return new WeakUpperBounds<Variable, Expression>(this.expManager);
        }

        #endregion

        #region INumericalAbstractDomain<Variable, Expression>Members

        /// <summary>
        /// The assignment in parallel.
        /// It does not use any knowledge on the value of the expressions
        /// </summary>
        /// <param name="sourcesToTargets"></param>
        public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
        {
            this.AssignInParallel(sourcesToTargets, convert, TopNumericalDomain<Variable, Expression>.Singleton);
        }

        #endregion

        #region INumericalAbstractDomainQuery<Variable,Expression> Members

        public Variable ToVariable(Expression exp)
        {
            return this.expManager.Decoder.UnderlyingVariable(exp);
        }

        #endregion

        #region Particular cases for IAbstractDomain

        override public WeakUpperBounds<Variable, Expression> Join(WeakUpperBounds<Variable, Expression> right)
        {
            // Here we do not have trivial joins as we want to join maps of different cardinality
            if (this.IsBottom)
                return right;
            if (right.IsBottom)
                return this;

            var result = Factory();

            foreach (var pair in this.Elements)       // For all the elements in the intersection do the point-wise join
            {
                SetOfConstraints<Variable> right_x;
                if (right.TryGetValue(pair.Key, out right_x))
                {
                    Contract.Assume(pair.Value != null);

                    var intersection = pair.Value.Join(right_x);

                    if (intersection.IsNormal())
                    {
                        // We keep in the map only the elements that are != top and != bottom

                        result[pair.Key] = intersection;
                    }
                }
            }

            return result;
        }

        public WeakUpperBounds<Variable, Expression> Join(WeakUpperBounds<Variable, Expression> right,
          IIntervalAbstraction<Variable, Expression> intervalsForThis, IIntervalAbstraction<Variable, Expression> intervalsForRight)
        {
            Contract.Requires(right != null);
            Contract.Requires(intervalsForThis != null);
            Contract.Requires(intervalsForRight != null);

            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            // Here we do not have trivial joins as we want to join maps of different cardinality
            if (this.IsBottom)
                return right;
            if (right.IsBottom)
                return this;

            var result = this.Factory();

            foreach (var pair in this.Elements)       // For all the elements in the intersection do the point-wise join
            {
                var newValues = new Set<Variable>();

                SetOfConstraints<Variable> otherBounds;
                if (right.TryGetValue(pair.Key, out otherBounds))
                {
                    Contract.Assume(pair.Value != null);

                    var intersection = pair.Value.Join(otherBounds);
                    if (!intersection.IsTop)
                    {
                        newValues.AddRange(intersection.Values);
                    }

                    // Foreach x < y
                    if (otherBounds.IsNormal())
                    {
                        foreach (var y in otherBounds.Values)
                        {
                            if (newValues.Contains(y))
                            {
                                continue;
                            }

                            var res = intervalsForThis.CheckIfLessThan(pair.Key, y);

                            if (!res.IsNormal())
                            {
                                continue;
                            }

                            if (res.BoxedElement)    // If the intervals imply this relation, just keep it
                            {
                                newValues.Add(y);   // Add x < y
                            }
                        }
                    }
                }

                // Foreach x < y
                if (pair.Value.IsNormal())
                {
                    foreach (var y in pair.Value.Values)
                    {
                        if (newValues.Contains(y))
                        {
                            continue;
                        }

                        var res = intervalsForRight.CheckIfLessThan(pair.Key, y);

                        if (!res.IsNormal())
                        {
                            continue;
                        }

                        if (res.BoxedElement)    // If the intervals imply this relation, just keep it
                        {
                            newValues.Add(y);   // Add x < y everywhere
                            right.TestTrueLessThan(pair.Key, y);
                        }
                    }
                }
                if (!newValues.IsEmpty)
                {
                    result[pair.Key] = new SetOfConstraints<Variable>(newValues, false);
                }
            }

            foreach (var pair in right.Elements)
            {
                if (this.ContainsKey(pair.Key))
                {
                    // Case already handled
                    continue;
                }

                var newValues = new Set<Variable>();

                // Foreach x < y
                if (pair.Value.IsNormal())
                {
                    foreach (var y in pair.Value.Values)
                    {
                        var res = intervalsForThis.CheckIfLessThan(pair.Key, y);

                        if (!res.IsNormal())
                        {
                            continue;
                        }

                        if (res.BoxedElement)    // If the intervals imply this relation, just keep it
                        {
                            newValues.Add(y);   // Add x < y
                            this.TestTrueLessThan(pair.Key, y);
                        }
                    }

                    if (!newValues.IsEmpty)
                    {
                        result[pair.Key] = new SetOfConstraints<Variable>(newValues, false);
                    }
                }
            }

            return result;
        }

        #endregion

        #region IPureExpressionAssignmentsWithForward<Expression> Members

        public void AssumeInDisInterval(Variable x, DisInterval value)
        {
            if (this.ContainsKey(x))
                this.RemoveVariable(x);
        }

        #endregion

        #region IPureExpressionAssignments<Expression> Members

        public List<Variable> Variables
        {
            get
            {
                var result = new List<Variable>();
                var uppBounds = new Set<Variable>();

                foreach (var x in this.Elements)
                {
                    Contract.Assume(x.Value != null);

                    result.Add(x.Key);
                    if (!x.Value.IsTop)
                    {
                        uppBounds.AddRange(x.Value.Values);
                    }
                }

                result.AddRange(uppBounds);

                return result;
            }
        }

        public IEnumerable<Variable> SlackVariables
        {
            get
            {
                var decoder = this.expManager.Decoder;

                var seen = new Set<Variable>();

                foreach (var x_pair in this.Elements)
                {
                    if (decoder.IsSlackVariable(x_pair.Key))
                    {
                        seen.Add(x_pair.Key);
                        yield return x_pair.Key;
                    }

                    if (!x_pair.Value.IsTop)
                    {
                        foreach (var y in x_pair.Value.Values)
                        {
                            if (decoder.IsSlackVariable(y) && !seen.Contains(y))
                            {
                                seen.Add(y);
                                yield return y;
                            }
                        }
                    }
                }
            }
        }

        public void AddVariable(Variable var)
        {
            // Does nothing, as we assume variables initialized to top
        }

        public void ProjectVariable(Variable var)
        {
            this.RemoveElement(var);

            var toUpdate = new List<Pair<Variable, SetOfConstraints<Variable>>>();

            foreach (var pair in this.Elements)
            {
                if (pair.Value.IsNormal() && pair.Value.Contains(var))
                {
                    var s = new Set<Variable>(pair.Value.Values);
                    s.Remove(var);
                    toUpdate.Add(pair.Key, new SetOfConstraints<Variable>(s, false));
                }
            }

            foreach (var pair in toUpdate)
            {
                this[pair.One] = pair.Two;
            }
        }

        public void RemoveVariable(Variable var)
        {
            this.ProjectVariable(var);
        }

        public void RenameVariable(Variable OldName, Variable NewName)
        {
            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);

                if (!pair.Value.IsTop)
                {
                    var oldVal = pair.Value;
                    if (oldVal.Contains(OldName))
                    {
                        var newVal = new Set<Variable>(oldVal.Values);
                        newVal.Remove(OldName);
                        newVal.Add(NewName);
                        this[pair.Key] = new SetOfConstraints<Variable>(newVal, false);
                    }
                }
            }
        }

        #endregion

        #region IPureExpressionTest<Expression> Members

        IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestTrue(Expression guard)
        {
            return this.TestTrue(guard);
        }

        public WeakUpperBounds<Variable, Expression> TestTrue(Expression guard)
        {
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            return ProcessTestTrue(guard);
        }

        IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestFalse(Expression guard)
        {
            return this.TestFalse(guard);
        }

        void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
            this.AssumeDomainSpecificFact(fact);
        }

        public WeakUpperBounds<Variable, Expression> TestFalse(Expression guard)
        {
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            var encoder = this.expManager.Encoder;
            //      if (encoder != null)
            {
                var notGuard = encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Not, guard);
                return TestTrue(notGuard);
            }
            /*    else
                {
                  return ProcessTestFalse(guard);
                }
             */
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            return this.RemoveRedundanciesWith(oracle);
        }

        public WeakUpperBounds<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            Contract.Requires(oracle != null);
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            var result = this.DuplicateMe();
            //      if (this.expManager.Encoder != null)
            {
                foreach (var pair in this.Elements)
                {
                    if (!pair.Value.IsNormal())
                    {
                        result[pair.Key] = pair.Value;
                    }
                    else
                    {
                        var newExp = new List<Variable>();
                        foreach (var exp in pair.Value.Values)
                        {
                            // x <= exp
                            var lt = oracle.CheckIfLessThan(pair.Key, exp);

                            if (!lt.IsNormal() || !lt.BoxedElement)
                            { // Then it is not implied by oracle
                                newExp.Add(exp);
                            }
                        }
                        if (newExp.Count > 0)
                        {
                            result[pair.Key] = new SetOfConstraints<Variable>(newExp);
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        #region WeakUpperBounds-Specific Methods

        public INumericalAbstractDomain<Variable, Expression> TestTrueGeqZero(Expression exp)
        {
            return this;
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessThan(Expression exp1, Expression exp2)
        {
            return this.TestTrueLessThan(exp1, exp2);
        }

        public WeakUpperBounds<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
        {
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            return this.TestTrueLessThan(exp1, exp2, new WeakUpperBoundsEqual<Variable, Expression>(this.expManager));
        }

        public WeakUpperBounds<Variable, Expression> TestTrueLessThan(Variable v1, Variable v2)
        {
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            SetOfConstraints<Variable> upp;
            if (this.TryGetValue(v1, out upp) && upp.IsNormal())
            {
                var newElements = new Set<Variable>(upp.Values);
                newElements.Add(v2);
                upp = new SetOfConstraints<Variable>(newElements, false);
                newElements = null; // F: just want to make sure that we do not reuse it, as we passed the ownership
            }
            else
            {
                upp = new SetOfConstraints<Variable>(v2);
            }

            this[v1] = upp;

            return this;
        }

        public WeakUpperBounds<Variable, Expression> TestTrueLessThan(Expression e1, Expression e2,
          WeakUpperBoundsEqual<Variable, Expression> oracleDomain)
        {
            Contract.Requires(oracleDomain != null);
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            var decoder = this.expManager.Decoder;

            var e1Var = decoder.UnderlyingVariable(e1);
            var e2Var = decoder.UnderlyingVariable(e2);
            var e2Stripped = decoder.Stripped(e2);
            var e2StrippedVar = decoder.UnderlyingVariable(e2Stripped);

            if (decoder.IsVariable(e1))
            {      // If e1 is a variable, then we add transitively all the constraints of e2
                AddAssumptionsTestTrueLessThanVariable(e1Var, e2, e2Var, e2StrippedVar, oracleDomain);
            }
            else
            {
                AddAssumptionsTestTrueLessThanNonVariableCase(e1, e2, e1Var, e2Var, e2StrippedVar);
            }

            if (this[e1Var].IsNormal())
            {
                var toBeUpdated = new List<Pair<Variable, SetOfConstraints<Variable>>>();

                // "e1 < e2", so we search all the variables to see if "e0 < e1"
                foreach (var pair in this.Elements)
                {
                    if (pair.Value.IsNormal())
                    {
                        if (pair.Value.Contains(e1Var))
                        {
                            var values = new Set<Variable>(pair.Value.Values);
                            values.AddRange(this[e1Var].Values);

                            toBeUpdated.Add(pair.Key, new SetOfConstraints<Variable>(values, false));
                        }
                    }
                }

                if (toBeUpdated.Count > 0)
                {
                    foreach (var pair in toBeUpdated)
                    {
                        this[pair.One] = pair.Two;
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// We add the assumptions e1Var \lt e2Var, e2StripperVar
        /// </summary>
        private void AddAssumptionsTestTrueLessThanVariable(
          Variable e1Var, Expression e2, Variable e2Var, Variable e2StrippedVar,
          WeakUpperBoundsEqual<Variable, Expression> oracleDomain)
        {
            Contract.Requires(oracleDomain != null);

            var newValues = new Set<Variable>();

            newValues.Add(e2Var);  // e1 < e2 
            newValues.Add(e2StrippedVar);   // Add "e1 < e2'", if e2 was in the form "(int32) e2'" or similar 

            SetOfConstraints<Variable> this_e1;
            if (this.TryGetValue(e1Var, out this_e1) && !this_e1.IsTop)
            {                   // e1 < "all the values before"
                newValues.AddRange(this_e1.Values);
            }

            SetOfConstraints<Variable> this_e2;
            if (this.TryGetValue(e2Var, out this_e2) && !this_e2.IsTop)
            {                   // e1 < "all the values e2 is smaller than"
                newValues.AddRange(this_e2.Values);
            }

            SetOfConstraints<Variable> oracleDomain_e2;
            if (oracleDomain.TryGetValue(e2Var, out oracleDomain_e2) && !oracleDomain_e2.IsTop)
            {                   // e1 < "all the values e2 is smaller than or equal to"
                newValues.AddRange(oracleDomain_e2.Values);
            }

            var decoder = this.expManager.Decoder;
            // e1 < x - y
            if (decoder.IsBinaryExpression(e2) && decoder.OperatorFor(e2) == ExpressionOperator.Subtraction)
            {
                var left = decoder.LeftExpressionFor(e2);
                var right = decoder.RightExpressionFor(e2);

                int v;
                if (decoder.IsConstantInt(right, out v) && v > 0)
                {
                    newValues.Add(decoder.UnderlyingVariable(left));
                    newValues.Add(decoder.UnderlyingVariable(decoder.Stripped(left)));
                }
            }

            this[e1Var] = new SetOfConstraints<Variable>(newValues, false);
        }

        /// <summary>
        /// e1 is not a variable, We try to construct a polynomial out of it and then infer new relations in the form x lt y
        /// </summary>
        private void AddAssumptionsTestTrueLessThanNonVariableCase(Expression e1, Expression e2, Variable e1Var, Variable e2Var, Variable e2StrippedVar)
        {
            Contract.Requires(!this.expManager.Decoder.IsVariable(e1));

            // e1Var < e2Var and e2StrippedVar
            if (!this.ContainsKey(e1Var))
            {
                var tmp = new Set<Variable>() { e2Var, e2StrippedVar };

                this[e1Var] = new SetOfConstraints<Variable>(tmp, false);
            }

            // Infer constraints from a more concrete form (polynomial)
            Polynomial<Variable, Expression> e1LTe2;
            if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessThan, e1, e2, this.expManager.Decoder, out e1LTe2))
            {
                Rational k1, k2, k;
                Variable x1, x2;

                if (e1LTe2.TryMatch_k1XPlusk2YLessThanK(out k1, out x1, out k2, out x2, out k))
                {
                    if (k <= 0)
                    {
                        if (k1 == 1 && k2 == -1)   // x1 - x2 < k <= 0 => x1 < x2
                        {
                            this.UpdateConstraintsFor(x1, x2);
                        }
                        else if (k1 == -1 && k2 == 1) // -x1 + x2 < k <= 0 => x2 < x1
                        {
                            this.UpdateConstraintsFor(x2, x1);
                        }
                    }
                }
            }
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            return this.TestTrueLessEqualThan(exp1, exp2);
        }

        public WeakUpperBounds<Variable, Expression> TestTrueLessEqualThan(Expression e1, Expression e2)
        {
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            Polynomial<Variable, Expression> p1, p2;

            if (Polynomial<Variable, Expression>.TryToPolynomialForm(e1, this.expManager.Decoder, out p1)
              && Polynomial<Variable, Expression>.TryToPolynomialForm(e2, this.expManager.Decoder, out p2))
            {
                Variable x, y;
                Rational k;

                var decoder = this.expManager.Decoder;

                // Consider the case "e1 <= y - k"
                if (p2.TryMatch_YMinusK(out y, out k))
                {
                    // Assume e1 < y
                    var e1Var = decoder.UnderlyingVariable(e1);
                    var result = this.HelperForLessThan(e1Var, y);

                    var e1StrippedVar = decoder.UnderlyingVariable(decoder.Stripped(e1));
                    result = this.HelperForLessThan(e1StrippedVar, y);

                    return result;
                }
                // Consider the case "x + k <= e2"
                if (p1.TryMatch_YPlusK(out x, out k))
                {
                    if (k > 0)
                    {
                        var e2Var = decoder.UnderlyingVariable(e2);

                        return this.HelperForLessThan(x, e2Var);
                    }
                }

                // Consider the case "e1 <= y"
                if (p2.TryMatch_Y(out y))
                {
                    SetOfConstraints<Variable> value;
                    // If we know that "y < z", then also "e1 < z"
                    if (this.TryGetValue(y, out value) && value.IsNormal())
                    {
                        var e1Var = decoder.UnderlyingVariable(e1);
                        this[e1Var] = value;

                        return this;
                    }
                }

                Rational k1, k2;
                Variable x1, x2;
                Polynomial<Variable, Expression> combined;
                if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessEqualThan, p1, p2, out combined) && combined.TryMatch_k1XPlusk2YLessThanK(out k1, out x1, out k2, out x2, out k))
                { // k1 x1 + k2 x2 < k
                    if (k1 < 0 && k2 > 0)
                    {
                        var tmp = k1;
                        k1 = k2;
                        k2 = tmp;

                        var tmpExp = x1;
                        x1 = x2;
                        x2 = tmpExp;
                    }

                    if (k1 == 1 && k2 == -1 && k <= 0)
                    {
                        // "x1 - x2 < k <= 0" that is "x1 < x2" 
                        return this.HelperForLessThan(x1, x2);
                    }
                }
            }

            return this;
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueEqual(Expression exp1, Expression exp2)
        {
            return this.TestTrueEqual(exp1, exp2);
        }

        public WeakUpperBounds<Variable, Expression> TestTrueEqual(Expression e1, Expression e2)
        {
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            Polynomial<Variable, Expression> p1, p2;

            if (Polynomial<Variable, Expression>.TryToPolynomialForm(e1, this.expManager.Decoder, out p1)
              && Polynomial<Variable, Expression>.TryToPolynomialForm(e2, this.expManager.Decoder, out p2))
            {
                // match p1 with " x + 1 ", and p2 with " y "
                if (p1.IsLinearOctagon && p2.IsLinearOctagon)
                {
                    if (p1.Left.Length == 1 && p2.Left.Length == 2)
                    {
                        var tmp = p1; p1 = p2; p2 = tmp;
                    }

                    // Here we want p1.Left.Count == 2. If this is not the case, we just drop this info
                    if (p1.Left.Length == 2 && p1.Left[0].Degree == 1 && p1.Left[1].IsConstant
                      && p2.Left.Length == 1 && p2.Left[0].Degree == 1)
                    {
                        var x = p1.Left[0].VariableAt(0);
                        var k = p1.Left[1].K;
                        var y = p2.Left[0].VariableAt(0);

                        if (k > 0)
                        { // Then we know that " x + k = y, k > 0 " implies " x < y "  
                            return this.HelperForLessThan(x, y);
                        }
                    }
                }
            }

            return this;
        }

        #endregion

        #region Protected methods

        private bool Check(Variable x1, Variable x2)
        {
            SetOfConstraints<Variable> value;

            if (this.TryGetValue(x1, out value))
            {
                if (value.IsBottom)
                {
                    return true;
                }
                else if (!value.IsTop)
                {
                    return value.Contains(x2);
                }
            }

            return false;
        }

        virtual protected WeakUpperBounds<Variable, Expression> ProcessTestTrue(Expression guard)
        {
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            var decoder = this.expManager.Decoder;

            switch (decoder.OperatorFor(guard))
            {
                #region all the cases
                case ExpressionOperator.Equal:
                case ExpressionOperator.Equal_Obj:
                    {
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);
                        var leftVar = decoder.UnderlyingVariable(left);
                        var rightVar = decoder.UnderlyingVariable(right);

                        SetOfConstraints<Variable> v1, v2;
                        var b1 = this.TryGetValue(leftVar, out v1);
                        var b2 = this.TryGetValue(rightVar, out v2);
                        if (b1 && b2)
                        {
                            var intersection = v1.Meet(v2);
                            if (v1.IsBottom)
                            {
                                return this.Bottom;
                            }
                            else
                            {
                                this[leftVar] = intersection;
                                this[rightVar] = intersection;

                                return this;
                            }
                        }
                        else if (b1 && v1.IsNormal())
                        {
                            this[rightVar] = v1;

                            return this;
                        }
                        else if (b2 && v2.IsNormal())
                        {
                            this[leftVar] = v2;

                            return this;
                        }
                        else
                        {
                            // Try to figure out if it is in the form of (e11 rel e12) == 0
                            Expression e11, e12;
                            ExpressionOperator op;
                            if (decoder.Match_E1relopE2eq0(left, right, out op, out e11, out e12))
                            {
                                return ProcessTestFalse(left);
                            }
                        }

                        return this;
                    }

                case ExpressionOperator.GreaterEqualThan:
                    // Does nothing ...
                    return this;

                case ExpressionOperator.GreaterThan:
                    {
                        // "left > right", so we add the constraint
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);
                        return this.TestTrueLessThan(right, left);
                    }

                case ExpressionOperator.LessEqualThan:
                    {
                        // "left <= right"
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);
                        return this.TestTrueLessEqualThan(left, right);
                    }

                case ExpressionOperator.LessThan:
                    // "left < right"
                    {
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);

                        return this.TestTrueLessThan(left, right);
                    }

                // We abstract away the symbolic reasoning for unsigned as it may be incorrect
                // Example "sv1 CLT_Un sv2" and sv2 is -1, so it should be understood as sv1 CLT_Un UInt.MaxValue - this reasoning is done by intervals
                case ExpressionOperator.LessThan_Un:
                case ExpressionOperator.LessEqualThan_Un:
                case ExpressionOperator.GreaterThan_Un:
                case ExpressionOperator.GreaterEqualThan_Un:
                    {
                        return this;
                    }

                case ExpressionOperator.LogicalAnd:
                    {
                        var leftDomain = this.DuplicateMe();
                        var rightDomain = this.DuplicateMe();

                        leftDomain = leftDomain.ProcessTestTrue(decoder.LeftExpressionFor(guard));
                        rightDomain = rightDomain.ProcessTestTrue(decoder.RightExpressionFor(guard));

                        return leftDomain.Meet(rightDomain);
                    }

                case ExpressionOperator.LogicalOr:
                    {
                        var leftDomain = this.DuplicateMe();
                        var rightDomain = this.DuplicateMe();

                        leftDomain = leftDomain.ProcessTestTrue(decoder.LeftExpressionFor(guard));
                        rightDomain = rightDomain.ProcessTestTrue(decoder.RightExpressionFor(guard));

                        return leftDomain.Join(rightDomain);
                    }

                case ExpressionOperator.Not:
                    {
                        return this.ProcessTestFalse(this.expManager.Decoder.LeftExpressionFor(guard));
                    }

                case ExpressionOperator.Addition:
                case ExpressionOperator.Constant:
                case ExpressionOperator.ConvertToInt32:
                case ExpressionOperator.ConvertToUInt8:
                case ExpressionOperator.ConvertToUInt16:
                case ExpressionOperator.ConvertToUInt32:
                case ExpressionOperator.ConvertToFloat32:
                case ExpressionOperator.ConvertToFloat64:
                case ExpressionOperator.Division:
                case ExpressionOperator.And:
                case ExpressionOperator.Or:
                case ExpressionOperator.Modulus:
                case ExpressionOperator.ShiftLeft:
                case ExpressionOperator.ShiftRight:
                case ExpressionOperator.SizeOf:
                case ExpressionOperator.Multiplication:
                case ExpressionOperator.NotEqual:
                case ExpressionOperator.Subtraction:
                case ExpressionOperator.UnaryMinus:
                case ExpressionOperator.Xor:
                case ExpressionOperator.Unknown:
                case ExpressionOperator.Variable:
                    return this;

                default:
                    return this;
                    #endregion
            }
        }

        virtual protected WeakUpperBounds<Variable, Expression> ProcessTestFalse(Expression guard)
        {
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            var decoder = this.expManager.Decoder;

            switch (decoder.OperatorFor(guard))
            {
                #region all the cases

                case ExpressionOperator.LogicalAnd:
                    {
                        var leftDomain = this.DuplicateMe();
                        var rightDomain = this.DuplicateMe();

                        leftDomain = leftDomain.ProcessTestFalse(decoder.LeftExpressionFor(guard));
                        rightDomain = rightDomain.ProcessTestFalse(decoder.RightExpressionFor(guard));

                        return leftDomain.Join(rightDomain);
                    }

                case ExpressionOperator.And:
                case ExpressionOperator.Addition:
                case ExpressionOperator.Constant:
                case ExpressionOperator.ConvertToInt32:
                case ExpressionOperator.ConvertToUInt8:
                case ExpressionOperator.ConvertToUInt16:
                case ExpressionOperator.ConvertToUInt32:
                case ExpressionOperator.ConvertToFloat32:
                case ExpressionOperator.ConvertToFloat64:
                case ExpressionOperator.Division:
                case ExpressionOperator.Modulus:
                case ExpressionOperator.Multiplication:
                case ExpressionOperator.NotEqual:
                case ExpressionOperator.Or:
                case ExpressionOperator.ShiftLeft:
                case ExpressionOperator.ShiftRight:
                case ExpressionOperator.SizeOf:
                case ExpressionOperator.Subtraction:
                case ExpressionOperator.UnaryMinus:
                case ExpressionOperator.Unknown:
                case ExpressionOperator.Xor:
                case ExpressionOperator.Variable:
                    return this;

                case ExpressionOperator.Equal:
                case ExpressionOperator.Equal_Obj:
                    return TestTrueNotEqual(decoder.LeftExpressionFor(guard), decoder.RightExpressionFor(guard));

                case ExpressionOperator.GreaterEqualThan:
                    {
                        // !(left >= right) is equivalent to (left < right)
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);

                        return this.TestTrueLessThan(left, right);
                    }

                case ExpressionOperator.GreaterThan:
                    {
                        // !(left > right) is equivalent to left <= right
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);

                        return this.TestTrueLessEqualThan(left, right);
                    }

                case ExpressionOperator.LessEqualThan:
                    {
                        // !(left <= right) is equivalent to (left > right)
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);

                        return this.TestTrueLessThan(right, left);
                    }

                case ExpressionOperator.LessThan:
                    {
                        // !(left < right) is (left >= right) which is (right <= left)
                        var left = this.expManager.Decoder.LeftExpressionFor(guard);
                        var right = this.expManager.Decoder.RightExpressionFor(guard);

                        return this.TestTrueLessEqualThan(right, left);
                    }

                // We abstract away the symbolic reasoning for unsigned as it may be incorrect
                // Example "sv1 CLT_Un sv2" and sv2 is -1, so it should be understood as sv1 CLT_Un UInt.MaxValue - this reasoning is done by intervals
                case ExpressionOperator.LessThan_Un:
                case ExpressionOperator.LessEqualThan_Un:
                case ExpressionOperator.GreaterThan_Un:
                case ExpressionOperator.GreaterEqualThan_Un:
                    {
                        return this;
                    }

                case ExpressionOperator.LogicalOr:
                    {
                        var leftDomain = this.DuplicateMe();
                        var rightDomain = this.DuplicateMe();

                        leftDomain = leftDomain.ProcessTestFalse(decoder.LeftExpressionFor(guard));
                        rightDomain = rightDomain.ProcessTestFalse(decoder.RightExpressionFor(guard));

                        return leftDomain.Meet(rightDomain);
                    }

                case ExpressionOperator.Not:
                    {
                        return this.ProcessTestTrue(decoder.LeftExpressionFor(guard));
                    }
                default:
                    return this;
                    #endregion
            }
        }

        private WeakUpperBounds<Variable, Expression> HelperForLessThan(Variable v1, Variable v2)
        {
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            return this.HelperForLessThan(v1, v2, new WeakUpperBoundsEqual<Variable, Expression>(this.expManager));
        }

        private WeakUpperBounds<Variable, Expression> HelperForLessThan(Variable v1, Variable v2, WeakUpperBoundsEqual<Variable, Expression> oracleDomain)
        {
            Contract.Requires(oracleDomain != null);
            Contract.Ensures(Contract.Result<WeakUpperBounds<Variable, Expression>>() != null);

            var newValues = new Set<Variable>();

            newValues.Add(v2);  // v1 < v2 

            SetOfConstraints<Variable> this_e1;
            if (this.TryGetValue(v1, out this_e1) && !this_e1.IsTop)
            {                   // e1 < "all the values before"
                newValues.AddRange(this_e1.Values);
            }

            SetOfConstraints<Variable> this_e2;
            if (this.TryGetValue(v2, out this_e2) && !this_e2.IsTop)
            {                   // e1 < "all the values e2 is smaller than"
                newValues.AddRange(this_e2.Values);
            }

            SetOfConstraints<Variable> oracleDomain_e2;
            if (oracleDomain.TryGetValue(v2, out oracleDomain_e2) && !oracleDomain_e2.IsTop)
            {                   // e1 < "all the values e2 is smaller than or equal to"
                newValues.AddRange(oracleDomain_e2.Values);
            }

            var newConstraintsForV1 = new SetOfConstraints<Variable>(newValues, false);
            this[v1] = newConstraintsForV1;

            if (newConstraintsForV1.IsNormal())
            {
                var toBeUpdated = new List<Pair<Variable, SetOfConstraints<Variable>>>();

                // "e1 < e2", so we search all the variables to see if "e0 < e1"
                foreach (var pair in this.Elements)
                {
                    if (pair.Value.IsNormal())
                    {
                        if (pair.Value.Contains(v1))
                        {
                            var values = new Set<Variable>(pair.Value.Values);
                            values.AddRange(newConstraintsForV1.Values);

                            toBeUpdated.Add(pair.Key, new SetOfConstraints<Variable>(values, false));
                        }
                    }
                }

                if (toBeUpdated.Count > 0)
                {
                    foreach (var pair in toBeUpdated)
                    {
                        this[pair.One] = pair.Two;
                    }
                }
            }

            return this;
        }

        public WeakUpperBounds<Variable, Expression> TestTrueNotEqual(Expression e1, Expression e2)
        {
            // If it is in the for "op(e11, e12) relSym k" where relSym is a relational symbol, and k a constant 
            if (this.expManager.Decoder.OperatorFor(e1).IsRelationalOperator() && this.expManager.Decoder.IsConstant(e2))
            {
                int v;

                if (this.expManager.Decoder.IsConstantInt(e2, out v))
                {
                    if (v == 0)
                    { // that is !!(e1)
                        return this.TestTrue(e1);
                    }
                    else if (v == 1)
                    { // that is !e1
                        return this.TestFalse(e1);
                    }
                }
            }

            return this;
        }

        #endregion

        #region Private and Protected methods

        /// <summary>
        /// Add the constraint <code>x &lt; y ></code>to this environment.
        /// If <code>x</code> already has some constraints related to him, we make the union of constraints (but we do not propagate)
        /// </summary>
        private void UpdateConstraintsFor(Variable x, Variable y)
        {
            var newValues = new Set<Variable>();

            SetOfConstraints<Variable> this_x;
            if (this.TryGetValue(x, out this_x) && !this_x.IsTop)
            {
                newValues.AddRange(this_x.Values);
            }
            newValues.Add(y);

            this[x] = new SetOfConstraints<Variable>(newValues, false);  // Do the update
        }

        #endregion

        #region ToString
        protected override string ToLogicalFormula(Variable d, SetOfConstraints<Variable> c)
        {
            if (!c.IsNormal())
            {
                return null;
            }

            var result = new List<string>();

            var niceD = ExpressionPrinter.ToString(d, this.expManager.Decoder);
            foreach (var y in c.Values)
            {
                string niceY = ExpressionPrinter.ToString(y, this.expManager.Decoder);
                result.Add(ExpressionPrinter.ToStringBinary(ExpressionOperator.LessThan, niceD, niceY));
            }

            return ExpressionPrinter.ToLogicalFormula(ExpressionOperator.LogicalAnd, result);
        }

        [ContractVerification(false)]
        protected override T To<T>(Variable d, SetOfConstraints<Variable> c, IFactory<T> factory)
        {
            if (c.IsTop)
                return factory.Constant(true);
            if (c.IsBottom)
                return factory.Constant(false);

            var result = factory.IdentityForAnd;
            var x = factory.Variable(d);

            // We know the constraints are x < y
            foreach (var y in c.Values)
            {
                T atom = factory.LessThan(x, factory.Variable(y));
                result = factory.And(result, atom);
            }

            return result;
        }

        public override string ToString()
        {
            if (this.IsTop)
                return "Top";

            if (this.IsBottom)
                return "Bottom";

            var result = new StringBuilder();
            result.AppendLine("WUB:");
            foreach (var x_pair in this.Elements)
            {
                Contract.Assume(x_pair.Value != null);

                if (!x_pair.Value.IsTop)
                {
                    var niceX = ExpressionPrinter.ToString(x_pair.Key, this.expManager.Decoder);
                    var niceVal = ToString(x_pair.Value.Values, x_pair.Value.Count);
                    result.AppendLine(niceX + " < " + niceVal + " ");
                }
            }

            return result.ToString();
        }

        private string ToString(IEnumerable<Variable> set, int count)
        {
            Contract.Requires(set != null);
            Contract.Ensures(Contract.Result<string>() != null);

            var result = new StringBuilder();
            result.Append("{");
            int c = 0;

            foreach (var x in set)
            {
                var xAsString = ExpressionPrinter.ToString(x, this.expManager.Decoder);

                result.Append(xAsString);

                if (c < count - 1)
                {
                    result.Append(" ,  ");
                }

                c++;
            }

            result.Append("}");

            return result.ToString();
        }

        public string ToString(Expression exp)
        {
            //if (this.expManager.Decoder != null)
            {
                return ExpressionPrinter.ToString(exp, this.expManager.Decoder);
            }
            //      else
            //     {
            //      return "< missing expression decoder >";
            //     }
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

        private class VisitorForCheckLessEqualThan
          : TryVisitAtMostTwoMonomials<Variable, Expression, WeakUpperBounds<Variable, Expression>, FlatAbstractDomain<bool>>
        {
            private WeakUpperBounds<Variable, Expression> leq;

            public VisitorForCheckLessEqualThan()
            {
                leq = null;
            }

            sealed public override bool Visit(Polynomial<Variable, Expression> exp, WeakUpperBounds<Variable, Expression> input, ref FlatAbstractDomain<bool> result)
            {
                Contract.Ensures(result != null);

                leq = input;
                result = CheckOutcome.Top;

                if (leq.IsTop)
                {
                    return false;
                }

                bool success = this.Dispatch(exp, ref result);

                leq = null;

                // F: this seems a bug: the postcondition for Dispatch is not taken into account here, and we can prove it
                // However, the postcondition result != null is not proven!
                Contract.Assert(result != null);

                return success;
            }

            protected override bool VisitMinusXMinusYLessEqualThan(Variable x, Variable y, Rational k, ref FlatAbstractDomain<bool> result)
            {
                result = CheckOutcome.Top;
                return false;
            }

            protected override bool VisitXMinusYLessEqualThan(Variable x, Variable y, Rational k, ref FlatAbstractDomain<bool> result)
            {
                if (leq == null)
                {
                    return false;
                }
                if (k.IsZero)
                {
                    result = leq.CheckIfLessThan(x, y);
                    return true;
                }
                else if (k == -1)
                {
                    result = leq.CheckIfLessThan(x, y);
                    return true;
                }
                else
                {
                    result = CheckOutcome.Top;
                    return false;
                }
            }

            protected override bool VisitXYLessEqualThanK(Variable x, Variable y, Rational k, ref FlatAbstractDomain<bool> result)
            {
                result = CheckOutcome.Top;
                return false;
            }

            protected override bool VisitXLessEqualThan(Rational a, Variable x, Rational k, ref FlatAbstractDomain<bool> result)
            {
                result = CheckOutcome.Top;
                return false;
            }

            protected override bool VisitMinusXLessEqualThan(Rational a, Variable x, Rational k, ref FlatAbstractDomain<bool> result)
            {
                result = CheckOutcome.Top;
                return false;
            }
        }

        private class WUBCheckIfHoldsVisitor
          : CheckIfHoldsVisitor<WeakUpperBounds<Variable, Expression>, Variable, Expression>
        {
            public WUBCheckIfHoldsVisitor(IExpressionDecoder<Variable, Expression> decoder)
              : base(decoder)
            {
                Contract.Requires(decoder != null);
            }

            public override FlatAbstractDomain<bool> VisitEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                var resultLeft = Domain.CheckIfLessEqualThan(left, right);

                if (resultLeft.IsNormal())
                {
                    var resultRight = Domain.CheckIfLessEqualThan(right, left);

                    if (resultRight.IsNormal())
                    {
                        return resultLeft.Join(resultRight);
                    }

                    return resultRight;
                }

                return resultLeft;
            }

            public override FlatAbstractDomain<bool> VisitEqual_Obj(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                return VisitEqual(left, right, original, data);
            }

            public override FlatAbstractDomain<bool> VisitLessEqualThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                return Domain.CheckIfLessEqualThan(left, right);
            }

            public override FlatAbstractDomain<bool> VisitLessThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                return Domain.CheckIfLessThan(left, right);
            }

            public override FlatAbstractDomain<bool> VisitNotEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                var check1 = this.VisitLessThan(left, right, original, data);
                if (check1.IsNormal())
                {
                    return new FlatAbstractDomain<bool>(true);
                }

                var check2 = this.VisitLessThan(right, left, original, data);
                if (check2.IsNormal())
                {
                    return new FlatAbstractDomain<bool>(true);
                }

                return Default(data);
            }
        }
    }

    [ContractClass(typeof(TryVisitAtMostTwoMonomialsContracts<,,,>))]
    internal abstract class TryVisitAtMostTwoMonomials<Variable, Expression, In, Data>
    {
        /// <summary>
        /// We want it to be used by the subclasses, but not called from clients
        /// </summary>
        protected bool Dispatch(Polynomial<Variable, Expression> exp, ref Data result)
        {
            Contract.Ensures(result != null);

            Variable x, y;
            Rational a, b, k;

            if (exp.TryMatch_k1XLessEqualThank2(out a, out x, out k))
            { // a * x <= k
                if (a < 0)
                {
                    return this.VisitMinusXLessEqualThan(a, x, k, ref result);
                }
                else if (a > 0)
                {
                    return this.VisitXLessEqualThan(a, x, k, ref result);
                }
                else // a == 0
                {
                    return Default(ref result);
                }
            }
            else if (exp.TryMatch_k1XLessThank2(out a, out x, out k))
            { // "a * x < k " == "a * x  <= k-1"
                if (a < 0)
                {
                    return this.VisitMinusXLessEqualThan(a, x, k - 1, ref result);
                }
                else if (a > 0)
                {
                    return this.VisitXLessEqualThan(a, x, k - 1, ref result);
                }
                else
                {
                    return Default(ref result);
                }
            }
            else if (exp.TryMatch_k1XPlusk2YLessEqualThanK(out a, out x, out b, out y, out k))
            { // "a * x + b * y <= k" 
                if (a == 1 && b == 1)
                {
                    return VisitXYLessEqualThanK(x, y, k, ref result);
                }
                else if (a == 1 && b == -1)
                {
                    return this.VisitXMinusYLessEqualThan(x, y, k, ref result);
                }
                else if (a == -1 && b == 1)
                {
                    return this.VisitXMinusYLessEqualThan(y, x, k, ref result);
                }
                else if (a == -1 && b == -1)
                {
                    return this.VisitMinusXMinusYLessEqualThan(x, y, k, ref result);
                }
                else
                {
                    // Todo: Do we want a visitor for other cases here?
                    return Default(ref result);
                }
            }
            else if (exp.TryMatch_k1XPlusk2YLessThanK(out a, out x, out b, out y, out k))
            { // "a * x + b * y < k" is "a * x + b * y <=  k-1"
                if (a == 1 && b == 1)
                {
                    return this.VisitXYLessEqualThanK(x, y, k - 1, ref result);
                }
                else if (a == 1 && b == -1)
                {
                    return this.VisitXMinusYLessEqualThan(x, y, k - 1, ref result);
                }
                else if (a == -1 && b == 1)
                {
                    return this.VisitXMinusYLessEqualThan(y, x, k - 1, ref result);
                }
                else if (a == -1 && b == -1)
                {
                    return this.VisitMinusXMinusYLessEqualThan(x, y, k - 1, ref result);
                }
                else
                {
                    // Todo: Do we want a visitor for other cases here?
                    return Default(ref result);
                }
            }
            else
            {
                return Default(ref result);
            }
        }

        abstract public bool Visit(Polynomial<Variable, Expression> exp, In input, ref Data result);

        abstract protected bool VisitMinusXMinusYLessEqualThan(Variable x, Variable y, Rational k, ref Data result);

        abstract protected bool VisitXMinusYLessEqualThan(Variable x, Variable y, Rational k, ref Data result);

        abstract protected bool VisitXYLessEqualThanK(Variable x, Variable y, Rational k, ref Data result);

        abstract protected bool VisitXLessEqualThan(Rational a, Variable x, Rational k, ref Data result);

        abstract protected bool VisitMinusXLessEqualThan(Rational a, Variable x, Rational k, ref Data result);

        private bool Default(ref Data result)
        {
            return false;
        }
    }

    #region Contracts
    [ContractClassFor(typeof(TryVisitAtMostTwoMonomials<,,,>))]
    internal abstract class TryVisitAtMostTwoMonomialsContracts<Variable, Expression, In, Data> : TryVisitAtMostTwoMonomials<Variable, Expression, In, Data>
    {
        public override bool Visit(Polynomial<Variable, Expression> exp, In input, ref Data result)
        {
            Contract.Requires(input != null);
            Contract.Ensures(result != null);

            throw new NotImplementedException();
        }

        protected override bool VisitMinusXMinusYLessEqualThan(Variable x, Variable y, Rational k, ref Data result)
        {
            Contract.Requires(!object.Equals(k, null));

            throw new NotImplementedException();
        }

        protected override bool VisitXMinusYLessEqualThan(Variable x, Variable y, Rational k, ref Data result)
        {
            Contract.Requires(!object.Equals(k, null));

            throw new NotImplementedException();
        }

        protected override bool VisitXYLessEqualThanK(Variable x, Variable y, Rational k, ref Data result)
        {
            Contract.Requires(!object.Equals(k, null));

            throw new NotImplementedException();
        }

        protected override bool VisitXLessEqualThan(Rational a, Variable x, Rational k, ref Data result)
        {
            Contract.Requires(!object.Equals(k, null));

            throw new NotImplementedException();
        }

        protected override bool VisitMinusXLessEqualThan(Rational a, Variable x, Rational k, ref Data result)
        {
            Contract.Requires(!object.Equals(k, null));

            throw new NotImplementedException();
        }
    }
    #endregion
}
