// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    /// <summary>
    /// The abstract domain to keep track of relations in the form of "x \leq y".
    /// It does not (always) close, so it is less precise than octagons
    /// </summary>
    [ContractVerification(true)]
    public class WeakUpperBoundsEqual<Variable, Expression> :
        FunctionalAbstractDomain<WeakUpperBoundsEqual<Variable, Expression>, Variable, SetOfConstraints<Variable>>,
          INumericalAbstractDomain<Variable, Expression>
    {
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(constraintsForAssignment != null);
            Contract.Invariant(this.ExpressionManager != null);
        }


        #region Private state

        readonly protected ExpressionManagerWithEncoder<Variable, Expression> ExpressionManager;

        private int closureSteps;
        [ThreadStatic]
        private static int defaultClosureSteps;
        public static int DefaultClosureSteps
        {
            get { return defaultClosureSteps; }
            set { defaultClosureSteps = value; }
        }

        readonly private List<IConstraintsForAssignment<Variable, Expression, INumericalAbstractDomainQuery<Variable, Expression>>> constraintsForAssignment;

        #endregion

        #region Constructor

        public WeakUpperBoundsEqual(ExpressionManagerWithEncoder<Variable, Expression> expManager)
        {
            Contract.Requires(expManager != null);

            this.ExpressionManager = expManager;

            constraintsForAssignment = new List<IConstraintsForAssignment<Variable, Expression, INumericalAbstractDomainQuery<Variable, Expression>>>()
      {
        new LessEqualThanConstraints<Variable, Expression>(this.ExpressionManager),
        new LessThanConstraints<Variable, Expression>(this.ExpressionManager),
        new GreaterEqualThanZeroConstraints<Variable, Expression>(this.ExpressionManager)
      };
        }

        private WeakUpperBoundsEqual(WeakUpperBoundsEqual<Variable, Expression> other)
          : base(other)
        {
            Contract.Requires(other != null);

            // F: We do not assume the object invariant for now

            Contract.Assume(other.ExpressionManager != null);
            Contract.Assume(other.constraintsForAssignment != null);

            this.ExpressionManager = other.ExpressionManager;
            constraintsForAssignment = other.constraintsForAssignment;
        }

        #endregion

        public override object Clone()
        {
            return this.DuplicateMe();
        }

        public WeakUpperBoundsEqual<Variable, Expression> DuplicateMe()
        {
            return new WeakUpperBoundsEqual<Variable, Expression>(this);
        }

        #region IPureExpressionAssignmentsWithForward<Expression> Members
        /// <summary>
        /// Do the assignment, improving the precision using the oracle domain
        /// </summary>
        public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> oracleDomain)
        {
            var LEQConstraints = new Set<Expression>();

            // Discover new constraints
            foreach (var constraintsFetcher in constraintsForAssignment)
            {
                Contract.Assume(constraintsFetcher != null);

                LEQConstraints.AddRange(constraintsFetcher.InferConstraints(x, exp, oracleDomain));
            }

            // Assume them
            foreach (var newConstraint in LEQConstraints)
            {
                this.TestTrue(newConstraint);
            }
        }

        public void Assign(Expression x, Expression exp)
        {
            Contract.Assume(x != null);
            Contract.Assume(exp != null);

            Assign(x, exp, TopNumericalDomain<Variable, Expression>.Singleton);
        }

        #endregion

        #region Easy ones

        public DisInterval BoundsFor(Expression exp)
        {
            Expression left, right;

            switch (this.ExpressionManager.Decoder.OperatorFor(exp))
            {
                case ExpressionOperator.Subtraction:
                    left = this.ExpressionManager.Decoder.LeftExpressionFor(exp);
                    right = this.ExpressionManager.Decoder.RightExpressionFor(exp);

                    var isPositive = this.CheckIfLessEqualThan(right, left); // right <= left

                    if (isPositive.IsNormal())
                    {
                        return isPositive.BoxedElement ? DisInterval.Positive : DisInterval.Negative;
                    }
                    else
                    {
                        return DisInterval.UnknownInterval;
                    }

                default:
                    return DisInterval.UnknownInterval;
            }
        }

        public DisInterval BoundsFor(Variable v)
        {
            return this.BoundsFor(this.ExpressionManager.Encoder.VariableFor(v));
        }

        public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
        {
            return this.CheckIfHolds(exp, TopNumericalDomain<Variable, Expression>.Singleton);
        }

        public FlatAbstractDomain<bool> CheckIfHolds(Expression exp, INumericalAbstractDomain<Variable, Expression> oracleDomain)
        {
            var operatorForExp = this.ExpressionManager.Decoder.OperatorFor(exp);

            if (operatorForExp.IsBinary() && operatorForExp.IsRelationalOperator())
            {
                Expression left, right;

                left = this.ExpressionManager.Decoder.LeftExpressionFor(exp);
                right = this.ExpressionManager.Decoder.RightExpressionFor(exp);

                switch (operatorForExp)
                {
                    case ExpressionOperator.Equal:
                    case ExpressionOperator.Equal_Obj:
                        ExpressionOperator op;
                        Expression e11, e12;
                        if (this.ExpressionManager.Decoder.Match_E1relopE2eq0(left, right, out op, out e11, out e12))
                        {
                            return ChechIfHoldsNegative(op, e11, e12);
                        }
                        break;

                    case ExpressionOperator.GreaterThan:
                    case ExpressionOperator.GreaterThan_Un:
                        return CheckIfLessThan(right, left);

                    case ExpressionOperator.LessThan:
                    case ExpressionOperator.LessThan_Un:
                        return CheckIfLessThan(left, right);

                    case ExpressionOperator.LessEqualThan:
                    case ExpressionOperator.LessEqualThan_Un:
                        return CheckIfLessEqualThan(left, right);

                    case ExpressionOperator.GreaterEqualThan:
                    case ExpressionOperator.GreaterEqualThan_Un:
                        return CheckIfLessEqualThan(right, left);

                    default:
                        return CheckOutcome.Top;
                }
            }

            return CheckOutcome.Top;
        }

        private FlatAbstractDomain<bool> ChechIfHoldsNegative(ExpressionOperator op, Expression a, Expression b)
        {
            switch (op)
            {
                case ExpressionOperator.GreaterEqualThan:
                case ExpressionOperator.GreaterEqualThan_Un:
                    // !(a >= b) == a < b
                    return CheckIfLessThan(a, b);

                case ExpressionOperator.GreaterThan:
                case ExpressionOperator.GreaterThan_Un:
                    // !(a > b) == a <= b
                    return CheckIfLessEqualThan(a, b);

                case ExpressionOperator.LessEqualThan:
                case ExpressionOperator.LessEqualThan_Un:
                    // !(a <= b) == a > b =+ b < a
                    return CheckIfLessThan(b, a);

                case ExpressionOperator.LessThan:
                case ExpressionOperator.LessThan_Un:
                    // !(a < b) =+ a >= b == b <= a 
                    return CheckIfLessEqualThan(b, a);

                default:
                    return CheckOutcome.Top;
            }
        }

        /// <summary>
        /// Add or materialize the map entry and add the value
        /// </summary>
        static internal void AddUpperBound(Variable key, Variable upperBound, Dictionary<Variable, Set<Variable>> map)
        {
            Contract.Requires(map != null);

            Set<Variable> bounds;
            if (!map.TryGetValue(key, out bounds))
            {
                bounds = new Set<Variable>();
                map[key] = bounds;
            }
            Contract.Assume(bounds != null);

            bounds.Add(upperBound);
        }

        public FlatAbstractDomain<bool> CheckIfNonZero(Expression exp)
        {
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
        {
            Polynomial<Variable, Expression> pol;
            Variable x, y;
            Rational k;

            if (/*this.ExpressionManager.Encoder != null && */
              Polynomial<Variable, Expression>.TryToPolynomialForm(exp, this.ExpressionManager.Decoder, out pol)
              && pol.TryMatch_XMinusYPlusK(out x, out y, out k))
            {
                var xExp = this.ExpressionManager.Encoder.VariableFor(x);
                var yExp = this.ExpressionManager.Encoder.VariableFor(y);

                var lt = this.CheckIfLessEqualThan(yExp, xExp);

                if (lt.IsNormal() && lt.BoxedElement == true && k >= -1)
                { // if we have to check x - y + k >= 0, and we know that k >= -1 and x - y > 0, then this is true
                    return lt;    // that is true
                }
            }

            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2, INumericalAbstractDomain<Variable, Expression> oracleDomain)
        {
            Contract.Requires(oracleDomain != null);

            if (this.IsTop)
            {
                return CheckOutcome.Top;
            }

            Polynomial<Variable, Expression> pol;
            if (Polynomial<Variable, Expression>.TryToPolynomialForm(e1, this.ExpressionManager.Decoder, out pol))
            {
                Variable x, y;
                Rational k;
                if (pol.TryMatch_XMinusYPlusK(out x, out y, out k))
                {
                    var xExp = this.ExpressionManager.Encoder.VariableFor(x);

                    if (this.CheckIfLessEqualThan(xExp, e2).IsTrue() && oracleDomain.BoundsFor(y).LowerBound > k)
                    {
                        return CheckOutcome.True;
                    }
                }
                else if (pol.TryMatch_YMinusK(out x, out k))
                {
                    var xExp = this.ExpressionManager.Encoder.VariableFor(x);

                    if (this.CheckIfLessEqualThan(xExp, e2).IsTrue())
                    {
                        return CheckOutcome.True;
                    }
                }
            }

            switch (this.ExpressionManager.Decoder.OperatorFor(e2))
            {
                case ExpressionOperator.Addition:
                    {
                        // try to match the case e1 < e2 + k, with k > 0. In this case we can check if e1 <= e2
                        var e2Left = this.ExpressionManager.Decoder.LeftExpressionFor(e2);
                        var e2Right = this.ExpressionManager.Decoder.RightExpressionFor(e2);
                        int k;
                        if (this.ExpressionManager.Decoder.IsConstantInt(e2Right, out k) && k > 0)
                        {
                            if (this.CheckIfLessEqualThan(e1, e2Left).IsTrue())
                            {
                                return CheckOutcome.True;
                            }
                            // if the check if false, we cannot say
                        }
                    }
                    break;
            }

            return CheckOutcome.Top;
        }

        /// <summary>
        /// Check if the expression <code>e1</code> is strictly smaller than <code>e2</code> using, 
        /// if needed the abstract domain <code>oracleDomain</code> to refine the information
        /// </summary>
        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2, INumericalAbstractDomain<Variable, Expression> oracleDomain)
        {
            // First we try it without the oracle domain
            var directTry = CheckIfLessEqualThan(e1, e2);

            if (directTry.IsTop)
            {
                return CommonChecks.CheckLessEqualThan(e1, e2, oracleDomain, this.ExpressionManager.Decoder);
            }

            return directTry;
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
            return this.LowerBoundsFor(this.ExpressionManager.Decoder.UnderlyingVariable(v), strict);
        }

        public IEnumerable<Expression> LowerBoundsFor(Variable v, bool strict)
        {
            var result = new Set<Expression>();

            if (strict)
            {
                return result;
            }

            foreach (var pair in this.Elements)
            {
                var valueSet = pair.Value;
                if (valueSet.IsNormal() && valueSet.Contains(v))
                {
                    result.Add(this.ExpressionManager.Encoder.VariableFor(pair.Key));
                }
            }

            return result;
        }

        public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
        {
            return UpperBoundsFor(this.ExpressionManager.Decoder.UnderlyingVariable(v), strict);
        }

        public IEnumerable<Expression> UpperBoundsFor(Variable v, bool strict)
        {
            var result = new Set<Expression>();

            SetOfConstraints<Variable> value;
            if (!strict && this.TryGetValue(v, out value) && value.IsNormal())
            {
                foreach (var x in value.Values)
                {
                    result.Add(this.ExpressionManager.Encoder.VariableFor(x));
                }
            }

            return result;
        }

        public IEnumerable<Variable> EqualitiesFor(Variable v)
        {
            var result = new Set<Variable>();

            SetOfConstraints<Variable> upperBounds;
            if (this.TryGetValue(v, out upperBounds) && upperBounds.IsNormal())
            {
                foreach (var x in upperBounds.Values)
                {
                    SetOfConstraints<Variable> other;
                    if (!x.Equals(v) && this.TryGetValue(x, out other) && other.IsNormal() && other.Contains(v))
                    {
                        result.Add(x);
                    }
                }
            }

            return result;
        }

        ///<summary>By default, it does nothing</summary>
        public void AssumeInDisInterval(Variable x, DisInterval value)
        {
            // does nothing
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
        {
            return CheckIfLessThan(e1, e2, TopNumericalDomain<Variable, Expression>.Singleton);
        }

        public FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2)
        {
            return this.CheckIfLessThan(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Variable v1, Variable v2)
        {
            return this.CheckIfLessThan(this.ExpressionManager.Encoder.VariableFor(v1), this.ExpressionManager.Encoder.VariableFor(v2));
        }

        public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
        {
            return this.CheckIfLessThan(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
        {
            if (this.IsTop)
            {
                return CheckOutcome.Top;
            }

            var decoder = this.ExpressionManager.Decoder;

            var stripped1Var = decoder.UnderlyingVariable(decoder.Stripped(e1));

            SetOfConstraints<Variable> value;
            if (this.TryGetValue(stripped1Var, out value) && value.IsNormal())
            {
                if (value.Contains(decoder.UnderlyingVariable(e2)))
                {
                    return CheckOutcome.True;
                }

                if (value.Contains(decoder.UnderlyingVariable(decoder.Stripped(e2))))
                {
                    return CheckOutcome.True;
                }

                foreach (var eTmp in value.Values)
                {
                    SetOfConstraints<Variable> bounds;
                    if (this.TryGetValue(eTmp, out bounds) &&
                      (bounds.Contains(decoder.UnderlyingVariable(e2)) || bounds.Contains(decoder.UnderlyingVariable(decoder.Stripped(e2)))))
                    { // e1 <= eTmp , eTmp <= e2 => e1 <= e2
                        return CheckOutcome.True;
                    }
                }
            }

            if (this.TryGetValue(decoder.UnderlyingVariable(e2), out value) && value.IsNormal())
            {
                if (this.CheckIfLessThan(e2, decoder.Stripped(e1), TopNumericalDomain<Variable, Expression>.Singleton) == CheckOutcome.True)
                {
                    return CheckOutcome.False;
                }
            }

            // This is a useful case when the underlying framework has not assigned a value to the variable
            Variable v1, v2;
            int k1, k2;
            if (decoder.TryMatchVarPlusConst(e1, out v1, out k1) && decoder.TryMatchVarPlusConst(e2, out v2, out k2) && k1 == k2)
            {
                return this.CheckIfLessEqualThan(v1, v2);
            }

            switch (decoder.OperatorFor(e2))
            {
                case ExpressionOperator.Addition:
                    {
                        // try to match e1 <= e21 + k, k > 0
                        var e2Left = decoder.LeftExpressionFor(e2);
                        var e2Right = decoder.RightExpressionFor(e2);
                        int k;
                        if (decoder.IsConstantInt(e2Right, out k) && k > 0)
                        {
                            if (this.CheckIfLessEqualThan(e1, e2Left).IsTrue())
                            {
                                return CheckOutcome.True;
                            }
                        }
                    }
                    break;
            }

            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable v1, Variable v2)
        {
            return this.CheckIfLessEqualThan(this.ExpressionManager.Encoder.VariableFor(v1), this.ExpressionManager.Encoder.VariableFor(v2));
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
        {
            return this.CheckIfLessEqualThan(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThanIncomplete(Expression e1, Expression e2)
        {
            if (this.IsTop)
            {
                return CheckOutcome.Top;
            }

            var decoder = this.ExpressionManager.Decoder;

            var stripped1Var = decoder.UnderlyingVariable(decoder.Stripped(e1));

            SetOfConstraints<Variable> value;
            if (this.TryGetValue(stripped1Var, out value) && value.IsNormal())
            {
                if (value.Contains(decoder.UnderlyingVariable(e2)))
                {
                    return CheckOutcome.True;
                }

                if (value.Contains(decoder.UnderlyingVariable(decoder.Stripped(e2))))
                {
                    return CheckOutcome.True;
                }
            }

            if (this.TryGetValue(decoder.UnderlyingVariable(e2), out value) && value.IsNormal())
            {
                if (this.CheckIfLessThan(e2, decoder.Stripped(e1), TopNumericalDomain<Variable, Expression>.Singleton) == CheckOutcome.True)
                {
                    return CheckOutcome.False;
                }
            }

            switch (decoder.OperatorFor(e2))
            {
                case ExpressionOperator.Addition:
                    {
                        // try to match e1 <= e21 + k, k > 0
                        var e2Left = decoder.LeftExpressionFor(e2);
                        var e2Right = decoder.RightExpressionFor(e2);
                        int k;
                        if (decoder.IsConstantInt(e2Right, out k) && k > 0)
                        {
                            if (this.CheckIfLessEqualThanIncomplete(e1, e2Left).IsTrue())
                            {
                                return CheckOutcome.True;
                            }
                        }
                    }
                    break;
            }

            return CheckOutcome.Top;
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
            FlatAbstractDomain<bool> c1, c2;

            if ((c1 = CheckIfLessEqualThan(e1, e2)).IsNormal() && (c2 = CheckIfLessEqualThan(e2, e1)).IsNormal())
            {
                return new FlatAbstractDomain<bool>(c1.BoxedElement && c2.BoxedElement);
            }

            return CheckOutcome.Top;
        }

        override protected WeakUpperBoundsEqual<Variable, Expression> Factory()
        {
            return new WeakUpperBoundsEqual<Variable, Expression>(this.ExpressionManager);
        }
        #endregion

        #region INumericalAbstractDomain<Variable, Expression> Members


        /// <summary>
        /// Perform the parallel assignment, and improved the precision using the information from the oracle domain
        /// </summary>
        /// <param name="sourcesToTargets"></param>
        [SuppressMessage("Microsoft.Contracts", "Assert-1-0")]
        public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
        {
            Debug.Assert(false); // Should not be called in the current Clousot setting

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
            //


            // The fact that we have to do this computation "in-place" is annoying. It would be simpler to produce a new state.
            // Thus, we will compute the new mappings on the side, then clear the current state, then add the new mappings back.

            // adding the domain-generated variables to the map as identity
            var oldToNewMap = new Dictionary<Variable, FList<Variable>>(sourcesToTargets);
            if (!this.IsTop)
            {
                foreach (var e in this.Variables)
                {
                    if (this.ExpressionManager.Decoder.IsSlackVariable(e))
                        oldToNewMap.Add(e, FList<Variable>.Cons(e, FList<Variable>.Empty));
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

            var newMappings = new Dictionary<Variable, Set<Variable>>(this.Count);

            foreach (var oldLeft_Pair in this.Elements)
            {
                if (!oldToNewMap.ContainsKey(oldLeft_Pair.Key))
                {
                    continue;
                }

                var targets = oldToNewMap[oldLeft_Pair.Key];
                var newLeft = targets.Head; // our canonical element

                var oldBounds = oldLeft_Pair.Value;
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

                    var newRight = oldToNewMap[oldRight].Head; // our canonical element
                    AddUpperBound(newLeft, newRight, newMappings);
                }
            }

            // Precision improvements:
            //
            // Consider:
            //   if (x < y) x = y;
            //   Debug.Assert(x >= y);
            //
            // This is an example where at the end of the then branch, we have a single old variable being assigned to new new variables:
            //   x := y'  and y := y'
            // Since in this branch, we obviously have y' => y', the new renamed state should have y => x and x => y. That way, at the join,
            // the constraint x >= y is retained.
            // 
            foreach (var pair in sourcesToTargets)
            {
                var targets = pair.Value;
                var newCanonical = targets.Head;
                targets = targets.Tail;
                while (targets != null)
                {
                    // make all other targets equal to canonical (rather than n^2 combinations)
                    AddUpperBound(newCanonical, targets.Head, newMappings);
                    AddUpperBound(targets.Head, newCanonical, newMappings);
                    targets = targets.Tail;
                }
            }

            // now clear the current state
            this.ClearElements();

            // now add the new mappings
            foreach (var key in newMappings.Keys)
            {
                var bounds = newMappings[key];

                if (bounds.Count == 0)
                    continue;

                var newBoundsFromClosure = new Set<Variable>();

                foreach (var upp in bounds)
                {
                    if (!upp.Equals(key) && newMappings.ContainsKey(upp))
                    {
                        newBoundsFromClosure.AddRange(newMappings[upp]);
                    }
                }

                bounds.AddRange(newBoundsFromClosure);

                this.AddElement(key, new SetOfConstraints<Variable>(bounds, false));
            }
        }

        public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            var result = this.DuplicateMe();

            // if (this.ExpressionManager.Encoder != null)
            {
                foreach (var pair in this.Elements)
                {
                    if (pair.Value.IsNormal())
                    {
                        result.AddElement(pair.Key, pair.Value);
                    }
                    else
                    {
                        var newExp = new Set<Variable>();
                        foreach (var val in pair.Value.Values)
                        {
                            // x <= x 
                            if (pair.Value.Equals(val))
                            {
                                continue;
                            }

                            // x <= exp
                            var leq = oracle.CheckIfLessEqualThan(pair.Key, val);

                            if (!leq.IsNormal() || !leq.BoxedElement)
                            { // Then it is not implied by oracle
                                newExp.Add(val);
                            }
                        }
                        if (newExp.Count > 0)
                        {
                            result.AddElement(pair.Key, new SetOfConstraints<Variable>(newExp, false));
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        #region INumericalAbstractDomainQuery<Variable,Expression> Members

        public Variable ToVariable(Expression exp)
        {
            return this.ExpressionManager.Decoder.UnderlyingVariable(exp);
        }

        #endregion

        #region Particular cases for IAbstractDomain

        public override WeakUpperBoundsEqual<Variable, Expression> Join(WeakUpperBoundsEqual<Variable, Expression> right)
        {
            // Here we do not have trivial joins as we want to join maps of different cardinality
            if (this.IsBottom)
                return right;
            if (right.IsBottom)
                return this;

            var result = Factory();

            foreach (var pair in this.Elements)       // For all the elements in the intersection do the point-wise join
            {
                SetOfConstraints<Variable> intersection;

                SetOfConstraints<Variable> right_x;
                if (right.TryGetValue(pair.Key, out right_x))
                {
                    // Implementation of transitive closure using the steps option
                    closureSteps = DefaultClosureSteps;

                    Contract.Assume(pair.Value != null);

                    var closedleft = new Set<Variable>(pair.Value.Values);
                    var closedright = new Set<Variable>(right_x.Values);

                    while (closureSteps > 0)
                    {
                        // here we do a transitive closure step
                        var templeft = new Set<Variable>(closedleft);
                        var tempright = new Set<Variable>(closedright);

                        foreach (var e in closedleft)
                        {
                            SetOfConstraints<Variable> this_e;
                            if (this.TryGetValue(e, out this_e) && !this_e.IsTop)
                            {
                                var tempSet = new Set<Variable>(this_e.Values);
                                tempSet.Remove(pair.Key);
                                templeft.AddRange(tempSet);
                            }
                        }

                        foreach (var e in closedright)
                        {
                            SetOfConstraints<Variable> right_e;
                            if (right.TryGetValue(e, out right_e) && !right_e.IsTop)
                            {
                                var tempSet = new Set<Variable>(right_e.Values);
                                tempSet.Remove(pair.Key);
                                tempright.AddRange(tempSet);
                            }
                        }

                        closedleft = templeft;
                        closedright = tempright;

                        closureSteps--;
                    }

                    intersection = new SetOfConstraints<Variable>(closedleft.Intersection(closedright), false);

                    if (intersection.IsNormal())
                    {
                        // We keep in the map only the elements that are != top and != bottom
                        result[pair.Key] = intersection;
                    }
                }
            }

            return result;
        }

        public WeakUpperBoundsEqual<Variable, Expression> Join(WeakUpperBoundsEqual<Variable, Expression> right,
           IIntervalAbstraction<Variable, Expression> intervalsForThis, IIntervalAbstraction<Variable, Expression> intervalsForRight)
        {
            Contract.Requires(right != null);
            Contract.Requires(intervalsForThis != null);
            Contract.Requires(intervalsForRight != null);

            // Here we do not have trivial joins as we want to join maps of different cardinality
            if (this.IsBottom)
                return right;
            if (right.IsBottom)
                return this;

            var result = this.Factory();

            foreach (var pair in this.Elements)       // For all the elements in the intersection do the point-wise join
            {
                var intersection = new SetOfConstraints<Variable>(new Set<Variable>(), false);
                var newValues = new Set<Variable>();

                SetOfConstraints<Variable> right_x;
                if (right.TryGetValue(pair.Key, out right_x))
                {
                    Contract.Assume(pair.Value != null);

                    intersection = pair.Value.Join(right_x);
                    if (!intersection.IsTop)
                    {
                        newValues.AddRange(intersection.Values);
                    }

                    // Foreach x <= y
                    if (right_x.IsNormal())
                    {
                        foreach (var y in right_x.Values)
                        {
                            if (newValues.Contains(y))
                            {
                                continue;
                            }

                            var res = intervalsForThis.CheckIfLessEqualThan(pair.Key, y);

                            if (!res.IsNormal())
                            {
                                continue;
                            }

                            if (res.BoxedElement)    // If the intervals imply this relation, just keep it
                            {
                                newValues.Add(y);   // Add x <= y
                            }
                        }
                    }
                }

                // Foreach x <= y
                if (pair.Value.IsNormal())
                {
                    foreach (var y in pair.Value.Values)
                    {
                        if (newValues.Contains(y))
                        {
                            continue;
                        }

                        var res = intervalsForRight.CheckIfLessEqualThan(pair.Key, y);

                        if (!res.IsNormal())
                        {
                            continue;
                        }

                        if (res.BoxedElement)    // If the intervals imply this relation, just keep it
                        {
                            newValues.Add(y);   // Add x <= y everywhere
                            right.TestTrueLessEqualThan(pair.Key, y);
                        }
                    }
                }

                if (!newValues.IsEmpty)
                {
                    result[pair.Key] = new SetOfConstraints<Variable>(newValues, false);
                }
            }

            foreach (var x_pair in right.Elements)
            {
                if (this.ContainsKey(x_pair.Key))
                {
                    // Case already handled
                    continue;
                }

                var newValues = new Set<Variable>();

                // Foreach x <= y
                if (x_pair.Value.IsNormal())
                {
                    foreach (var y in x_pair.Value.Values)
                    {
                        var res = intervalsForThis.CheckIfLessEqualThan(x_pair.Key, y);

                        if (!res.IsNormal())
                        {
                            continue;
                        }

                        if (res.BoxedElement)    // If the intervals imply this relation, just keep it
                        {
                            newValues.Add(y);   // Add x <= y
                            this.TestTrueLessEqualThan(x_pair.Key, y);
                        }
                    }

                    if (!newValues.IsEmpty)
                    {
                        result[x_pair.Key] = new SetOfConstraints<Variable>(newValues, false);
                    }
                }
            }

            return result;
        }

        #endregion

        #region IPureExpressionAssignments<Expression> Members

        public List<Variable> Variables
        {
            get
            {
                var result = new List<Variable>();

                var inside = new Set<Variable>();

                foreach (var x_pair in this.Elements)
                {
                    result.Add(x_pair.Key);

                    Contract.Assume(x_pair.Value != null);

                    if (!x_pair.Value.IsTop)
                    {
                        inside.AddRange(x_pair.Value.Values);
                    }
                }

                result.AddRange(inside);

                return result;
            }
        }

        public IEnumerable<Variable> SlackVariables
        {
            get
            {
                var seen = new Set<Variable>();
                var decoder = this.ExpressionManager.Decoder;

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
            var toUpdate = new Dictionary<Variable, SetOfConstraints<Variable>>(this.Count);

            var forVar = new Set<Variable>();

            SetOfConstraints<Variable> value;
            if (this.TryGetValue(var, out value) && value.IsNormal())
            {
                forVar = new Set<Variable>(value.Values);
            }

            foreach (var x_Pair in this.Elements)
            {
                var constraints = x_Pair.Value;

                if (!constraints.IsNormal() || !constraints.Contains(var))
                {
                    continue;
                }

                var newConstraints = new Set<Variable>(constraints.Values);
                newConstraints.AddRange(forVar);
                newConstraints.Remove(var);

                toUpdate[x_Pair.Key] = new SetOfConstraints<Variable>(newConstraints);
            }

            foreach (var x_pair in toUpdate)
            {
                this[x_pair.Key] = x_pair.Value;
            }

            this.RemoveElement(var);
        }

        public void RenameVariable(Variable OldName, Variable NewName)
        {
            foreach (var x_pair in this.Elements)
            {
                if (x_pair.Value.IsNormal())
                {
                    var oldVal = x_pair.Value;
                    if (oldVal.Contains(OldName))
                    {
                        var newVal = new Set<Variable>(oldVal.Values);
                        newVal.Remove(OldName);
                        newVal.Add(NewName);
                        this[x_pair.Key] = new SetOfConstraints<Variable>(newVal, false);
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

        public WeakUpperBoundsEqual<Variable, Expression> TestTrue(Expression guard)
        {
            Contract.Ensures(Contract.Result<WeakUpperBoundsEqual<Variable, Expression>>() != null);

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

        public WeakUpperBoundsEqual<Variable, Expression> TestFalse(Expression guard)
        {
            Contract.Ensures(Contract.Result<WeakUpperBoundsEqual<Variable, Expression>>() != null);

            //      if (this.ExpressionManager.Encoder != null)
            {
                var notGuard = this.ExpressionManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Not, guard);
                return TestTrue(notGuard);
            }
            /*
                else
                {
                  return ProcessTestFalse(guard);
                }
             */
        }

        /// <summary>
        /// Does nothing
        /// </summary>
        public INumericalAbstractDomain<Variable, Expression> TestTrueGeqZero(Expression exp)
        {
            return this;
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueEqual(Expression exp1, Expression exp2)
        {
            return this.TestTrueEqual(exp1, exp2);
        }

        #endregion

        #region WeakUpperBounds-Specific Methods

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessThan(Expression e1, Expression e2)
        {
            return this.HelperForLessThan(e1, e2);
        }

        public WeakUpperBoundsEqual<Variable, Expression> TestTrueLessThan(Expression e1, Expression e2)
        {
            Contract.Ensures(Contract.Result<WeakUpperBoundsEqual<Variable, Expression>>() != null);

            return this.HelperForLessThan(e1, e2);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessEqualThan(Expression e1, Expression e2)
        {
            return this.TestTrueLessEqualThan(e1, e2);
        }

        public WeakUpperBoundsEqual<Variable, Expression> TestTrueLessEqualThan(Expression e1, Expression e2)
        {
            Contract.Ensures(Contract.Result<WeakUpperBoundsEqual<Variable, Expression>>() != null);

            return this.HelperForLessEqualThan(e1, e2);
        }

        public WeakUpperBoundsEqual<Variable, Expression> TestTrueLessEqualThan(Variable e1, Variable e2)
        {
            Contract.Ensures(Contract.Result<WeakUpperBoundsEqual<Variable, Expression>>() != null);

            return this.TestTrueLessEqualThan(this.ExpressionManager.Encoder.VariableFor(e1), this.ExpressionManager.Encoder.VariableFor(e2));
        }

        public WeakUpperBoundsEqual<Variable, Expression> TestTrueEqual(Expression e1, Expression e2)
        {
            return this.HelperForEqual(e1, e2);
        }

        #endregion

        #region Protected methods

        protected WeakUpperBoundsEqual<Variable, Expression> ProcessTestTrue(Expression guard)
        {
            Contract.Ensures(Contract.Result<WeakUpperBoundsEqual<Variable, Expression>>() != null);

            var decoder = this.ExpressionManager.Decoder;

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
                        if (this.TryGetValue(leftVar, out v1) && this.TryGetValue(rightVar, out v2))
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
                        else
                        {
                            // Try to figure out if it is in the form of (e11 rel e12) == 0
                            Expression e11, e12;
                            ExpressionOperator op;
                            if (decoder.Match_E1relopE2eq0(left, right, out op, out e11, out e12))
                            {
                                return ProcessTestFalse(left);
                            }
                            else
                            {
                                // F: We do not want this domain to track arbitrary equalities (because of performances), 
                                // so if we do not understand the equality, we abstract it
                                return this;
                            }
                        }
                    }

                case ExpressionOperator.GreaterEqualThan:
                    {
                        // left >= right, so we want to add right <= left
                        var left = this.ExpressionManager.Decoder.LeftExpressionFor(guard);
                        var right = this.ExpressionManager.Decoder.RightExpressionFor(guard);
                        return this.TestTrueLessEqualThan(right, left);
                    }

                case ExpressionOperator.GreaterThan:
                    {
                        // "left > right", so we add the constraint
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);
                        return HelperForLessThan(right, left);
                    }

                case ExpressionOperator.LessEqualThan:
                    {
                        // "left <= right"
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);
                        return HelperForLessEqualThan(left, right);
                    }

                case ExpressionOperator.LessThan:
                    {
                        // "left < right"
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);
                        return HelperForLessThan(left, right);
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
                        return this.ProcessTestFalse(decoder.LeftExpressionFor(guard));
                    }

                default:
                    {
                        return this;
                    }

                    #endregion
            }
        }

        protected WeakUpperBoundsEqual<Variable, Expression> ProcessTestFalse(Expression guard)
        {
            Contract.Ensures(Contract.Result<WeakUpperBoundsEqual<Variable, Expression>>() != null);

            var decoder = this.ExpressionManager.Decoder;

            switch (this.ExpressionManager.Decoder.OperatorFor(guard))
            {
                #region all the cases

                case ExpressionOperator.Equal:
                case ExpressionOperator.Equal_Obj:
                    return HelperForNotEqual(decoder.LeftExpressionFor(guard), decoder.RightExpressionFor(guard));

                case ExpressionOperator.GreaterEqualThan:
                    {
                        // !(left >= right) is equivalent to (left < right)
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);
                        return HelperForLessThan(left, right);
                    }

                case ExpressionOperator.GreaterThan:
                    {
                        // !(left > right) is equivalent to left <= right
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);
                        return HelperForLessEqualThan(left, right);
                    }

                case ExpressionOperator.LessEqualThan:
                    {
                        // !(left <= right) is equivalent to (left > right)
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);
                        return HelperForLessThan(right, left);
                    }

                case ExpressionOperator.LessThan:
                    {
                        // !(left < right) is equivalent to (left >= right) that is (right <= left)
                        var left = decoder.LeftExpressionFor(guard);
                        var right = decoder.RightExpressionFor(guard);
                        return this.HelperForLessEqualThan(right, left);
                    }

                case ExpressionOperator.GreaterThan_Un:
                case ExpressionOperator.LessThan_Un:
                case ExpressionOperator.LessEqualThan_Un:
                case ExpressionOperator.GreaterEqualThan_Un:
                    {
                        return this;
                    }

                case ExpressionOperator.LogicalAnd:
                    {
                        var leftDomain = this.DuplicateMe();
                        var rightDomain = this.DuplicateMe();

                        leftDomain = leftDomain.ProcessTestFalse(decoder.LeftExpressionFor(guard));
                        rightDomain = rightDomain.ProcessTestFalse(decoder.RightExpressionFor(guard));

                        return leftDomain.Join(rightDomain);
                    }

                case ExpressionOperator.LogicalOr:
                    {
                        var leftDomain = this.DuplicateMe();
                        var rightDomain = this.DuplicateMe();

                        leftDomain = leftDomain.ProcessTestFalse(decoder.LeftExpressionFor(guard)) as WeakUpperBoundsEqual<Variable, Expression>;
                        rightDomain = rightDomain.ProcessTestFalse(decoder.RightExpressionFor(guard)) as WeakUpperBoundsEqual<Variable, Expression>;

                        return leftDomain.Meet(rightDomain);
                    }

                case ExpressionOperator.Not:
                    {
                        var left = this.ExpressionManager.Decoder.LeftExpressionFor(guard);
                        return this.ProcessTestTrue(left);
                    }

                default:
                    return this;

                    #endregion
            }
        }

        /// <summary>
        /// Handle the case <code>e1 &lt; e2</code> as it was <code>e1 &lt;= e2</code>
        /// </summary>
        private WeakUpperBoundsEqual<Variable, Expression> HelperForLessThan(Expression e1, Expression e2)
        {
            Contract.Ensures(Contract.Result<WeakUpperBoundsEqual<Variable, Expression>>() != null);

            var decoder = this.ExpressionManager.Decoder;

            if (decoder.IsBinaryExpression(e2) && decoder.OperatorFor(e2) == ExpressionOperator.Addition)
            {
                var exp = decoder.RightExpressionFor(e2);
                int value;
                if (decoder.IsConstantInt(exp, out value) && value == 1)
                {
                    return HelperForLessEqualThan(e1, decoder.LeftExpressionFor(e2));
                }
            }

            if (decoder.IsBinaryExpression(e1) && decoder.OperatorFor(e1) == ExpressionOperator.Subtraction)
            {
                var exp = decoder.RightExpressionFor(e1);
                int value;
                if (decoder.IsConstantInt(exp, out value) && value == 1)
                {
                    return HelperForLessEqualThan(decoder.LeftExpressionFor(e1), e2);
                }
            }

            return HelperForLessEqualThan(e1, e2);
        }

        private WeakUpperBoundsEqual<Variable, Expression> HelperForLessEqualThan(Expression e1, Expression e2)
        {
            Contract.Ensures(Contract.Result<WeakUpperBoundsEqual<Variable, Expression>>() != null);

            var decoder = this.ExpressionManager.Decoder;

            var e1Var = decoder.UnderlyingVariable(e1);
            var e2Var = decoder.UnderlyingVariable(e2);

            if (!decoder.IsSlackOrFrameworkVariable(e1Var) || !decoder.IsSlackOrFrameworkVariable(e2Var))
            {
                return this;
            }

            SetOfConstraints<Variable> this_e1;
            if (this.TryGetValue(e1Var, out this_e1) && !this_e1.IsTop)
            {
                var newConstraints = new Set<Variable>(this_e1.Values);
                newConstraints.Add(e2Var);

                this[e1Var] = new SetOfConstraints<Variable>(newConstraints, false);
            }

            var newValues = new Set<Variable>();

            var stripped = decoder.Stripped(e1);
            var strippedVar = decoder.UnderlyingVariable(stripped);

            if (decoder.IsVariable(stripped) || decoder.IsConstant(stripped))
            {
                newValues.Add(e2Var);  // e1 <= e2
                newValues.Add(decoder.UnderlyingVariable(decoder.Stripped(e2)));   // Add "e1 <= e2'", if e2 was in the form "(int32) e2'" or similar 

                SetOfConstraints<Variable> this_stripped;
                if (this.TryGetValue(strippedVar, out this_stripped) && !this_stripped.IsTop)
                {                   // e1 <= "all the values before"
                    newValues.AddRange(this_stripped.Values);
                }

                SetOfConstraints<Variable> this_e2;
                if (this.TryGetValue(e2Var, out this_e2) && !this_e2.IsTop)
                {                   // e1 <= "all the values e2 is smaller than"
                    newValues.AddRange(this_e2.Values);
                }

                this[strippedVar] = new SetOfConstraints<Variable>(newValues, false);
            }
            else if (!this.ContainsKey(strippedVar))
            {
                var tmp = new Set<Variable>() { e2Var, decoder.UnderlyingVariable(decoder.Stripped(e2)) };

                this[strippedVar] = new SetOfConstraints<Variable>(tmp, false);
            }

            Polynomial<Variable, Expression> e1LEQe2;
            if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessEqualThan, e1, e2, decoder, out e1LEQe2))
            {
                Rational k1, k2, k;
                Variable x1, x2;

                if (e1LEQe2.TryMatch_k1XPlusk2YLessThanK(out k1, out x1, out k2, out x2, out k))
                {
                    if (k <= 0)
                    {
                        if (k1 == 1 && k2 == -1)   // x1 - x2 <= k <= 0 => x1 < x2
                        {
                            this.UpdateConstraintsFor(x1, x2);
                        }
                        else if (k1 == -1 && k2 == 1) // -x1 + x2 <= k <= 0 => x2 < x1
                        {
                            this.UpdateConstraintsFor(x2, x1);
                        }
                    }

                    if (k == 1)
                    {
                        if (k1 == 1 && k2 == -1)   // x1 - x2 < 1  => x1 < x2 +1 => x1 <= x2 
                        {
                            this.UpdateConstraintsFor(x1, x2);
                        }
                        else if (k1 == -1 && k2 == 1)
                        {
                            this.UpdateConstraintsFor(x2, x1);
                        }
                    }
                }
            }

            var valuesOfStripped = this[strippedVar];
            if (valuesOfStripped.IsNormal())
            {
                var toBeUpdated = new List<Pair<Variable, SetOfConstraints<Variable>>>();

                // "e1 <= e2", so we search all the variables to see if "e0 <= e1"
                foreach (var e0_pair in this.Elements)
                {
                    if (e0_pair.Value.IsNormal())
                    {
                        if (e0_pair.Value.Contains(strippedVar))
                        {
                            var values = new Set<Variable>(e0_pair.Value.Values);
                            values.AddRange(valuesOfStripped.Values);

                            toBeUpdated.Add(e0_pair.Key, new SetOfConstraints<Variable>(values, false));
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

        private WeakUpperBoundsEqual<Variable, Expression> HelperForNotEqual(Expression e1, Expression e2)
        {
            Contract.Ensures(Contract.Result<WeakUpperBoundsEqual<Variable, Expression>>() != null);

            int value;
            // If it is in the for "op(e11, e12) relSym k" where relSym is a relational symbol, and k a constant 
            if (this.ExpressionManager.Decoder.OperatorFor(e1).IsRelationalOperator() && this.ExpressionManager.Decoder.IsConstantInt(e2, out value))
            {
                if (value == 0)
                { // that is !!(e1)
                    return this.TestTrue(e1);
                }

                if (value == 1)
                { // that is !e1
                    return this.TestFalse(e1);
                }
            }

            return this;
        }

        private WeakUpperBoundsEqual<Variable, Expression> HelperForEqual(Expression e1, Expression e2)
        {
            var result = this.HelperForLessEqualThan(e1, e2).HelperForLessEqualThan(e2, e1);

            // The code below is to share the constraints between e1 and e2
            var e1Var = this.ExpressionManager.Decoder.UnderlyingVariable(e1);
            var e2Var = this.ExpressionManager.Decoder.UnderlyingVariable(e2);

            SetOfConstraints<Variable> e1Leq, e2Leq;
            if (result.TryGetValue(e1Var, out e1Leq) && result.TryGetValue(e2Var, out e2Leq))
            {
                var meet = e1Leq.Meet(e2Leq);

                result[e1Var] = meet;
                result[e2Var] = meet;
            }

            return result;
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

            SetOfConstraints<Variable> bounds;
            if (this.TryGetValue(x, out bounds) && !bounds.IsTop)
            {
                newValues.AddRange(bounds.Values);
            }
            newValues.Add(y);

            this[x] = new SetOfConstraints<Variable>(newValues, false);  // Do the update
        }

        #endregion

        #region ToLogicalFormula
        protected override string ToLogicalFormula(Variable d, SetOfConstraints<Variable> c)
        {
            if (!c.IsNormal())
            {
                return null;
            }

            var result = new List<string>();

            var niceD = ExpressionPrinter.ToString(d, this.ExpressionManager.Decoder);
            foreach (var y in c.Values)
            {
                var niceY = ExpressionPrinter.ToString(y, this.ExpressionManager.Decoder);
                result.Add(ExpressionPrinter.ToStringBinary(ExpressionOperator.LessEqualThan, niceD, niceY));
            }

            return ExpressionPrinter.ToLogicalFormula(ExpressionOperator.LogicalAnd, result);
        }

        protected override T To<T>(Variable d, SetOfConstraints<Variable> c, IFactory<T> factory)
        {
            // F: this are preconditions, but I am lazy...
            Contract.Assume(c != null);

            if (c.IsTop)
                return factory.Constant(true);
            if (c.IsBottom)
                return factory.Constant(false);

            var result = factory.IdentityForAnd;

            var x = factory.Variable(d);

            // We know the constraints are x <= y
            foreach (var y in c.Values)
            {
                T atom = factory.LessEqualThan(x, factory.Variable(y));
                result = factory.And(result, atom);
            }

            return result;
        }
        #endregion

        #region ToString

        public override string ToString()
        {
            if (this.IsTop)
                return "Top";

            if (this.IsBottom)
                return "Bottom";

            var result = new StringBuilder();
            result.AppendLine("WUB=:");

            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);

                if (!pair.Value.IsTop)
                {
                    var niceX = ExpressionPrinter.ToString(pair.Key, this.ExpressionManager.Decoder);
                    var niceVal = pair.Value.ToString();
                    result.AppendLine(niceX + " <= " + niceVal + " ");
                }
            }

            return result.ToString();
        }

        public string ToString(Expression exp)
        {
            // if (this.ExpressionManager.Decoder != null)
            {
                return ExpressionPrinter.ToString(exp, this.ExpressionManager.Decoder);
            }

            /*else
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
