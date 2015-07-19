// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    [ContractVerification(true)]
    public class PentagonsPlus<Variable, Expression>
      : ReducedNumericalDomains<WeakUpperBoundsEqual<Variable, Expression>, Pentagons<Variable, Expression>, Variable, Expression>
    {
        #region Constructor

        public PentagonsPlus(
          WeakUpperBoundsEqual<Variable, Expression> left, Pentagons<Variable, Expression> right,
          ExpressionManagerWithEncoder<Variable, Expression> expManager)
          : base(left, right, expManager)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);
            Contract.Requires(expManager != null);
        }

        #endregion

        #region Assign

        public override void Assign(Expression x, Expression exp)
        {
            // F: I am lazy...
            Contract.Assume(x != null);
            Contract.Assume(exp != null);

            // Infer some x >= 0 invariants which requires the information from the two domains
            // The information should be inferred in the pre-state
            var geqZero = new GreaterEqualThanZeroConstraints<Variable, Expression>(this.ExpressionManager).InferConstraints(x, exp, this);

            this.Left.Assign(x, exp, this);
            this.Right.Assign(x, exp);

            // The information is assumed in the post-state...
            foreach (var condition in geqZero)
            {
                this.TestTrue(condition);
            }
        }

        public override void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
        {
            var right = this.Right;

            this.AssignInParallel(this.Left, right.Right, sourcesToTargets, right.Left as INumericalAbstractDomain<Variable, Expression>);

            right.Left.AssignInParallel(sourcesToTargets, convert);
        }

        private void AssignInParallel
          (WeakUpperBoundsEqual<Variable, Expression> wubeq, WeakUpperBounds<Variable, Expression> wub,
          Dictionary<Variable, FList<Variable>> sourcesToTargets, INumericalAbstractDomain<Variable, Expression> oracleDomain)
        {
            Contract.Requires(wubeq != null);
            Contract.Requires(wub != null);

            // adding the domain-generated variables to the map as identity
            var oldToNewMap = new Dictionary<Variable, FList<Variable>>(sourcesToTargets);

            if (!wubeq.IsTop)
            {
                foreach (var e in wubeq.SlackVariables)
                {
                    oldToNewMap[e] = FList<Variable>.Cons(e, FList<Variable>.Empty);
                }
            }

            if (!wub.IsTop)
            {
                foreach (var e in wub.SlackVariables)
                {
                    oldToNewMap[e] = FList<Variable>.Cons(e, FList<Variable>.Empty);
                }
            }

            // when x has several targets including itself, the canonical element shouldn't be itself
            foreach (var sourceToTargets in sourcesToTargets)
            {
                var source = sourceToTargets.Key;
                var targets = sourceToTargets.Value;

                Contract.Assume(targets != null);

                if (targets.Length() > 1 && targets.Head.Equals(source))
                {
                    var tail = targets.Tail;
                    Contract.Assert(tail != null);

                    var newTargets = FList<Variable>.Cons(tail.Head, FList<Variable>.Cons(source, tail.Tail));
                    oldToNewMap[source] = newTargets;
                }
            }

            AssignInParallelWUBSpecific(wub, oldToNewMap);
            AssignInParallelWUBEQSpecific(wubeq, sourcesToTargets, oldToNewMap);
        }

        private void AssignInParallelWUBSpecific(WeakUpperBounds<Variable, Expression> wub, Dictionary<Variable, FList<Variable>> oldToNewMap)
        {
            Contract.Requires(wub != null);
            Contract.Requires(oldToNewMap != null);

            var newMappings = new Dictionary<Variable, List<Variable>>(wub.Count);

            foreach (var oldLeft_pair in wub.Elements)
            {
                if (!oldToNewMap.ContainsKey(oldLeft_pair.Key))
                {
                    continue;
                }

                var target = oldToNewMap[oldLeft_pair.Key];
                Contract.Assume(target != null);

                var newLeft = target.Head; // our canonical element

                var oldBounds = oldLeft_pair.Value;
                if (!oldBounds.IsNormal())
                {
                    continue;
                }

                foreach (var oldRight in oldBounds.Values)
                {
                    FList<Variable> olds;

                    if (oldToNewMap.TryGetValue(oldRight, out olds))
                    {
                        Contract.Assume(olds != null);
                        // This case is so so common that we want to specialize it
                        if (olds.Length() == 1)
                        {
                            var newRight = olds.Head; // our canonical element
                            AddUpperBound(newLeft, newRight, newMappings);
                        }
                        else
                        {
                            foreach (var newRight in olds.GetEnumerable())
                            {
                                AddUpperBound(newLeft, newRight, newMappings);
                            }
                        }
                    }
                }

                // There are some more elements
                for (var list = oldToNewMap[oldLeft_pair.Key].Tail; list != null; list = list.Tail)
                {
                    List<Variable> targets;
                    if (newMappings.TryGetValue(newLeft, out targets))
                    {
                        Contract.Assume(targets != null);
                        foreach (var con in targets)
                        {
                            AddUpperBound(list.Head, con, newMappings);
                        }
                    }
                }
            }

            // Update
            var newConstraints = new Dictionary<Variable, SetOfConstraints<Variable>>(wub.Count);

            foreach (var pair in newMappings)
            {
                Contract.Assume(pair.Value != null);
                newConstraints.Add(pair.Key, new SetOfConstraints<Variable>(pair.Value));
            }

            wub.SetElements(newConstraints);
        }

        private void AssignInParallelWUBEQSpecific(
          WeakUpperBoundsEqual<Variable, Expression> wubeq, Dictionary<Variable, FList<Variable>> sourcesToTargets,
          Dictionary<Variable, FList<Variable>> oldToNewMap)
        {
            Contract.Requires(wubeq != null);
            Contract.Requires(sourcesToTargets != null);
            Contract.Requires(oldToNewMap != null);

            var newMappings = new Dictionary<Variable, List<Variable>>(wubeq.Count);

            foreach (var oldLeft_Pair in wubeq.Elements)
            {
                FList<Variable> targets;
                if (oldToNewMap.TryGetValue(oldLeft_Pair.Key, out targets))
                {
                    Contract.Assume(targets != null);

                    var oldBounds = oldLeft_Pair.Value;
                    if (!oldBounds.IsNormal())
                    {
                        continue;
                    }

                    var newLeft = targets.Head; // our canonical element

                    foreach (var oldRight in oldBounds.Values)
                    {
                        FList<Variable> olds;
                        if (oldToNewMap.TryGetValue(oldRight, out olds))
                        {
                            Contract.Assume(olds != null);

                            var newRight = olds.Head; // our canonical element
                            AddUpperBound(newLeft, newRight, newMappings);
                        }
                    }
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
                Contract.Assume(targets != null);

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

            var result = new List<Pair<Variable, SetOfConstraints<Variable>>>();

            // now add the new mappings
            foreach (var pair in newMappings)
            {
                var bounds = pair.Value;
                Contract.Assume(bounds != null);
                if (bounds.Count == 0)
                {
                    continue;
                }

                var newBoundsFromClosure = new Set<Variable>(bounds);

                foreach (var upp in bounds)
                {
                    List<Variable> values;
                    if (!upp.Equals(pair.Key) && newMappings.TryGetValue(upp, out values))
                    {
                        Contract.Assume(values != null);
                        newBoundsFromClosure.AddRange(values);
                    }
                }

                result.Add(pair.Key, new SetOfConstraints<Variable>(newBoundsFromClosure, false));
            }

            wubeq.SetElements(result);
        }


        static internal void AddUpperBound(Variable key, Variable upperBound,
          Dictionary<Variable, List<Variable>> map)
        {
            Contract.Requires(map != null);

            List<Variable> bounds;
            if (!map.TryGetValue(key, out bounds))
            {
                bounds = new List<Variable>();
                map.Add(key, bounds);
            }
            Contract.Assume(bounds != null); // If we have it in the dictionary, then it is != null

            bounds.Add(upperBound);
        }

        #endregion

        #region Abstract domain operations 

        public override IAbstractDomain Join(IAbstractDomain a)
        {
            if (this.IsBottom)
                return a;
            if (a.IsBottom)
                return this;
            if (this.IsTop)
                return this;
            if (a.IsTop)
                return a;

            var r = a as PentagonsPlus<Variable, Expression>;

            Contract.Assume(r != null, "Error cannot compare a cartesian abstract element with a " + a.GetType());

            // These two lines have a weak notion of closure, which essentially avoids dropping "x <= y" if it is implied by the intervals abstract domain            
            // It seems that it is as precise as the expensive join

            var joinLeftPart = this.Left.Join(r.Left, this.Right.Left, r.Right.Left);
            Contract.Assert(joinLeftPart != null);
            var joinRightPart = this.Right.Join(r.Right) as Pentagons<Variable, Expression>;
            Contract.Assume(joinRightPart != null);

            return new PentagonsPlus<Variable, Expression>(joinLeftPart, joinRightPart, this.ExpressionManager);
        }

        public override FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
        {
            var left = this.Left.CheckIfGreaterEqualThanZero(exp);
            if (!left.IsTop)
            {
                return left;
            }

            return this.Right.CheckIfGreaterEqualThanZero(exp);
        }

        public override FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
        {
            var left = Left.CheckIfLessThan(e1, e2, Right);

            if (!left.IsTop)
            {
                return left;
            }

            return Right.CheckIfLessThan(e1, e2);
        }

        public override FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
        {
            var left = Left.CheckIfLessThan_Un(e1, e2);

            if (!left.IsTop)
            {
                return left;
            }

            return Right.CheckIfLessThan_Un(e1, e2);
        }

        public override FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
        {
            var left = Left.CheckIfLessEqualThan(e1, e2);

            if (!left.IsTop)
            {
                return left;
            }

            return Right.CheckIfLessEqualThan(e1, e2);
        }

        public override FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
        {
            var left = Left.CheckIfLessEqualThan_Un(e1, e2);
            if (!left.IsTop)
            {
                return left;
            }

            return Right.CheckIfLessEqualThan_Un(e1, e2);
        }

        public override IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
        {
            Expression e1, e2;

            var newLeft = this.Left;
            var newRight = this.Right;

            if (this.IsLessThanPositive(guard, out e1, out e2))
            {
                // We have e1 < e2. If we already know that e1 >= e2. then we have a contraddiction
                if (this.CheckIfLessEqualThan(e2, e1).IsTrue())
                {
                    var bott = this.Bottom as PentagonsPlus<Variable, Expression>;
                    Contract.Assume(bott != null);

                    return bott;
                }

                var decoder = this.ExpressionManager.Decoder;
                var e1Var = decoder.UnderlyingVariable(e1);
                var e2Var = decoder.UnderlyingVariable(e2);

                SetOfConstraints<Variable> upperBounds;
                if (this.Left.TryGetValue(e2Var, out upperBounds))
                { // if guard == "e1 < e2" and we know that "e2 <= e3", then we also have "e1 < e3"
                    if (upperBounds.IsNormal())
                    {
                        foreach (var e3 in upperBounds.Values)
                        {
                            newRight = newRight.TestTrueLessThan(e1Var, e3);
                        }
                    }
                }
            }
            else if (this.TryMatchNotEqualTo(guard, out e1, out e2))
            {
                newRight = HelperForTestTrueNotEqual(e1, e2, newRight);
            }

            newLeft = newLeft.TestTrue(guard);
            newRight = newRight.TestTrue(guard);

            return new PentagonsPlus<Variable, Expression>(newLeft, newRight, this.ExpressionManager);
        }

        public override IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
        {
            Expression e1, e2;

            var newLeft = this.Left;
            var newRight = this.Right;

            if (this.TryMatchEqualTo(guard, out e1, out e2))
            { // if "e1 != e2", then we can restrain "e1 <= e2" (resp "e2 <= e1") to "e1 < e2" (resp "e2 < e1")
                newRight = HelperForTestTrueNotEqual(e1, e2, newRight);
            }

            newLeft = newLeft.TestFalse(guard);
            newRight = newRight.TestFalse(guard);

            return new PentagonsPlus<Variable, Expression>(newLeft, newRight, this.ExpressionManager);
        }

        public override INumericalAbstractDomain<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
        {
            var withPentagons = this as PentagonsPlus<Variable, Expression>;
            var newLeft = this.Left;

            var newRight = withPentagons.Right.TestTrueLessThan(exp1, exp2, this.Left);
            newLeft = newLeft.TestTrueLessThan(exp1, exp2);

            Contract.Assert(withPentagons.ExpressionManager.Encoder != null);
            return new PentagonsPlus<Variable, Expression>(newLeft, newRight, withPentagons.ExpressionManager);
        }

        [Pure]
        public override ReducedCartesianAbstractDomain<WeakUpperBoundsEqual<Variable, Expression>, Pentagons<Variable, Expression>>
          Reduce(WeakUpperBoundsEqual<Variable, Expression> left, Pentagons<Variable, Expression> right)
        {
            return this.Factory(left, right);
        }

        [Pure]
        protected override ReducedCartesianAbstractDomain<WeakUpperBoundsEqual<Variable, Expression>, Pentagons<Variable, Expression>>
          Factory(WeakUpperBoundsEqual<Variable, Expression> left, Pentagons<Variable, Expression> right)
        {
            Contract.Ensures(Contract.Result<ReducedCartesianAbstractDomain<WeakUpperBoundsEqual<Variable, Expression>, Pentagons<Variable, Expression>>>() != null);

            return new PentagonsPlus<Variable, Expression>(left, right, this.ExpressionManager);
        }

        #endregion

        #region Private methods

        private Pentagons<Variable, Expression> HelperForTestTrueNotEqual(Expression e1, Expression e2, Pentagons<Variable, Expression> newRight)
        {
            Contract.Requires(newRight != null);
            Contract.Ensures(Contract.Result<Pentagons<Variable, Expression>>() != null);

            var decoder = this.ExpressionManager.Decoder;

            // Get rid of the conversion operators
            e1 = decoder.Stripped(e1);
            e2 = decoder.Stripped(e2);

            var e1Var = decoder.UnderlyingVariable(e1);
            var e2Var = decoder.UnderlyingVariable(e2);

            SetOfConstraints<Variable> upperBounds;
            if (this.Left.TryGetValue(e1Var, out upperBounds))
            {
                if (upperBounds.IsNormal() && upperBounds.Contains(e2Var))
                { // if "e1 <= e2" then "e2 < e1"
                    newRight = newRight.TestTrueLessThan(e1, e2);
                }
            }

            if (this.Left.TryGetValue(e2Var, out upperBounds))
            {
                if (upperBounds.IsNormal() && upperBounds.Contains(e1Var))
                { // if "e2 <= e1" then "e2 < e1"
                    newRight = newRight.TestTrueLessThan(e2, e1);
                }
            }

            return newRight;
        }

        private bool IsLessThanPositive(Expression guard, out Expression e1, out Expression e2)
        {
            ExpressionOperator op;
            e1 = default(Expression);
            e2 = default(Expression);

            var decoder = this.ExpressionManager.Decoder;

            switch (decoder.OperatorFor(guard))
            {
                case ExpressionOperator.Equal:
                case ExpressionOperator.Equal_Obj:
                    if (decoder.Match_E1relopE2eq0(decoder.LeftExpressionFor(guard), decoder.RightExpressionFor(guard), out op, out e1, out e2))
                    { // here guard is "(e1 op e2) == 0"
                        return this.IsLessThanNegative(decoder.LeftExpressionFor(guard), out e1, out e2);
                    }
                    else
                    {
                        return false;
                    }

                case ExpressionOperator.GreaterThan:
                    {
                        e1 = decoder.RightExpressionFor(guard);
                        e2 = decoder.LeftExpressionFor(guard);

                        return true;
                    }
                case ExpressionOperator.LessThan:
                    {
                        e1 = decoder.LeftExpressionFor(guard);
                        e2 = decoder.RightExpressionFor(guard);
                        return true;
                    }

                case ExpressionOperator.GreaterThan_Un:
                case ExpressionOperator.LessThan_Un:
                default:
                    {
                        return false;
                    }
            }
        }

        private bool IsLessThanNegative(Expression guard, out Expression e1, out Expression e2)
        {
            ExpressionOperator op;
            e1 = default(Expression);
            e2 = default(Expression);

            var decoder = this.ExpressionManager.Decoder;

            switch (decoder.OperatorFor(guard))
            {
                case ExpressionOperator.Equal:
                case ExpressionOperator.Equal_Obj:
                    if (decoder.Match_E1relopE2eq0(decoder.LeftExpressionFor(guard), decoder.RightExpressionFor(guard), out op, out e1, out e2))
                    { // the guard is "(e1 op e2) == 0"
                        return this.IsLessThanPositive(decoder.LeftExpressionFor(guard), out e1, out e2);
                    }
                    else
                    {
                        return false;
                    }

                case ExpressionOperator.GreaterEqualThan:
                    {          // !(a >= b) == (a < b)
                        e1 = decoder.LeftExpressionFor(guard);
                        e2 = decoder.RightExpressionFor(guard);

                        return true;
                    }

                case ExpressionOperator.LessEqualThan:
                    {
                        // !(a <= b) == (a > b) == (b < a)
                        e1 = decoder.RightExpressionFor(guard);
                        e2 = decoder.LeftExpressionFor(guard);

                        return true;
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        private bool TryMatchEqualTo(Expression guard, out Expression e1, out Expression e2)
        {
            var decoder = this.ExpressionManager.Decoder;

            switch (decoder.OperatorFor(guard))
            {
                case ExpressionOperator.Equal:
                case ExpressionOperator.Equal_Obj:
                    {
                        e1 = decoder.Stripped(this.ExpressionManager.Decoder.LeftExpressionFor(guard));
                        e2 = decoder.Stripped(this.ExpressionManager.Decoder.RightExpressionFor(guard));
                        return true;
                    }

                default:
                    {
                        e1 = e2 = default(Expression);
                        return false;
                    }
            }
        }

        private bool TryMatchNotEqualTo(Expression guard, out Expression e1, out Expression e2)
        {
            var decoder = this.ExpressionManager.Decoder;

            var op_guard = decoder.OperatorFor(guard);
            switch (op_guard)
            {
                case ExpressionOperator.Equal:
                case ExpressionOperator.Equal_Obj:
                    {
                        ExpressionOperator op;
                        if (decoder.Match_E1relopE2eq0(decoder.LeftExpressionFor(guard), decoder.RightExpressionFor(guard), out op, out e1, out e2))
                        {
                            if (op == op_guard)
                            {
                                return true;
                            }
                        }
                        goto default;
                    }

                case ExpressionOperator.NotEqual:
                    {
                        e1 = decoder.LeftExpressionFor(guard);
                        e2 = decoder.RightExpressionFor(guard);

                        return true;
                    }

                default:
                    {
                        e1 = e2 = default(Expression);
                        return false;
                    }
            }
        }

        #endregion
    }
}
