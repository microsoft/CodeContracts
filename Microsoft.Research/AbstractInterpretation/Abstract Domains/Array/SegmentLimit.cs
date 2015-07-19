// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using System.Text;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains
{
    /// <summary>
    /// The limit for an array segment.
    /// Should be immutable
    /// </summary>
    [ContractVerification(true)]
    public class SegmentLimit<Variable>
      : IAbstractDomain, IEnumerable<NormalizedExpression<Variable>>
    {
        #region Private State

        readonly private Set<NormalizedExpression<Variable>> expressions; // \gamma(...) = \forall x, y \in expressions. \gamma(x) == \gamma(y)

        private Set<NormalizedExpression<Variable>> Expressions
        {
            get
            {
                Contract.Ensures(Contract.Result<Set<NormalizedExpression<Variable>>>() != null);

                return expressions;
            }
        }

        readonly public bool IsConditional;

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(expressions != null);
        }

        #endregion

        #region Constructor

        public SegmentLimit(NormalizedExpression<Variable> expression, bool isConditional)
          : this(new Set<NormalizedExpression<Variable>>() { expression }, isConditional)
        {
            Contract.Requires(expression != null);
        }

        public SegmentLimit(Set<NormalizedExpression<Variable>> expressions, bool isConditional)
        {
            Contract.Requires(expressions != null);
            // F: TODO
            //Contract.Requires(Contract.ForAll(expressions, exp => exp != null));

            this.expressions = expressions;
            this.IsConditional = isConditional;
        }

        public SegmentLimit(SegmentLimit<Variable> expressions, bool isConditional)
          : this(new Set<NormalizedExpression<Variable>>(expressions.Expressions), isConditional)  // F: TODO : Share the set
        {
            Contract.Requires(expressions != null);
        }

        #endregion

        #region Utility 

        /// <summary>
        /// Returns true iff <code>this</code> segment limit contains <code>exp</code>
        /// </summary>
        [Pure]
        public bool Contains(NormalizedExpression<Variable> exp)
        {
            return expressions.Contains(exp);
        }

        [ContractVerification(true)]
        public SegmentLimit<Variable> Add(NormalizedExpression<Variable> element)
        {
            Contract.Ensures(Contract.Result<SegmentLimit<Variable>>() != null);

            var newSet = new Set<NormalizedExpression<Variable>>();
            newSet.AddRange(expressions);
            newSet.Add(element);

            return new SegmentLimit<Variable>(newSet, this.IsConditional);
        }

        [ContractVerification(true)]
        public SegmentLimit<Variable> Add(Set<NormalizedExpression<Variable>> elements)
        {
            Contract.Requires(elements != null);
            Contract.Ensures(Contract.Result<SegmentLimit<Variable>>() != null);

            var newSet = new Set<NormalizedExpression<Variable>>();
            newSet.AddRange(expressions);
            newSet.AddRange(elements);

            return new SegmentLimit<Variable>(newSet, this.IsConditional);
        }


        [Pure]
        public SegmentLimit<Variable> KeepOnlyConstantExpressionIfAny()
        {
            Contract.Ensures(Contract.Result<SegmentLimit<Variable>>() != null);

            if (this.IsEmpty)
                return this;

            foreach (var e in expressions)
            {
                Contract.Assume(e != null);

                int dummy;
                if (e.IsConstant(out dummy))
                    return new SegmentLimit<Variable>(e, this.IsConditional);
            }

            return new SegmentLimit<Variable>(new Set<NormalizedExpression<Variable>>(), true);
        }

        public bool IsEmpty
        {
            get
            {
                return expressions.Count == 0;
            }
        }

        #endregion

        #region AssignInParallel
        public SegmentLimit<Variable> AssignInParallel<Expression>(Dictionary<Variable, FList<Variable>> sourceToTargets, Converter<Variable, Expression> convert,
          IExpressionDecoder<Variable, Expression> decoder)
        {
            #region Contracts
            Contract.Requires(sourceToTargets != null);
            Contract.Requires(convert != null);
            Contract.Requires(decoder != null);

            Contract.Ensures(Contract.Result<SegmentLimit<Variable>>() != null);
            #endregion

            var newSet = new Set<NormalizedExpression<Variable>>();

            foreach (var x in expressions)
            {
                Contract.Assume(x != null);

                FList<Variable> targets;

                int value;
                Variable var;
                if (x.IsConstant(out value))
                {
                    newSet.Add(x);
                }
                else if (x.IsVariable(out var))
                {
                    if (sourceToTargets.TryGetValue(var, out targets))
                    {
                        Contract.Assume(targets != null);

                        foreach (var newName in targets.GetEnumerable())
                        {
                            newSet.Add(NormalizedExpression<Variable>.For(newName));
                        }
                    }
                }
                else if (x.IsAddition(out var, out value))
                {
                    if (sourceToTargets.TryGetValue(var, out targets))
                    {
                        Contract.Assume(targets != null);
                        foreach (var newName in targets.GetEnumerable())
                        {
                            newSet.Add(NormalizedExpression<Variable>.For(newName, value));
                        }
                    }

                    // This is a special case to handle renaming for a[p++] = ...
                    // We have (var + value) --> var
                    Variable source;
                    if (IsATarget(var, sourceToTargets, out source))
                    {
                        Variable v;
                        int k;
                        if (decoder.TryMatchVarPlusConst(convert(source), out v, out k) && v.Equals(var) && k == value)
                        {
                            newSet.Add(NormalizedExpression<Variable>.For(var));
                        }
                    }
                }
            }

            return new SegmentLimit<Variable>(newSet, this.IsConditional);
        }

        private bool IsATarget(Variable var, Dictionary<Variable, FList<Variable>> sourceToTargets, out Variable source)
        {
            Contract.Requires(sourceToTargets != null);

            foreach (var pair in sourceToTargets)
            {
                var m = pair.Value;
                Contract.Assume(m != null);

                foreach (var t in m.GetEnumerable())
                {
                    if (t.Equals(var))
                    {
                        source = pair.Key;
                        return true;
                    }
                }
            }

            source = default(Variable);
            return false;
        }

        #endregion

        #region Inclusions, equality, etc.

        [Pure]
        static public bool HaveTheSameElements(SegmentLimit<Variable> left, SegmentLimit<Variable> right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return left.expressions.Equals(right.expressions);
        }

        [Pure]
        static public bool IsSubsetOf(SegmentLimit<Variable> left, SegmentLimit<Variable> right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return left.Expressions.IsSubset(right.Expressions);
        }

        [Pure]
        public bool IsSubSetOf(SegmentLimit<Variable> right)
        {
            Contract.Requires(right != null);

            return IsSubsetOf(this, right);
        }

        [Pure]
        public Set<NormalizedExpression<Variable>> Intersection(SegmentLimit<Variable> right)
        {
            Contract.Requires(right != null);
            Contract.Ensures(Contract.Result<Set<NormalizedExpression<Variable>>>() != null);

            return expressions.Intersection(right.Expressions);
        }

        [Pure]
        public Set<NormalizedExpression<Variable>> Union(SegmentLimit<Variable> right)
        {
            Contract.Requires(right != null);
            Contract.Ensures(Contract.Result<Set<NormalizedExpression<Variable>>>() != null);

            return expressions.Union(right.Expressions);
        }

        [Pure]
        public Set<NormalizedExpression<Variable>> Difference(SegmentLimit<Variable> right)
        {
            Contract.Requires(right != null);
            Contract.Ensures(Contract.Result<Set<NormalizedExpression<Variable>>>() != null);

            return expressions.Difference(right.Expressions);
        }

        [Pure]
        public Set<NormalizedExpression<Variable>> Difference(Set<NormalizedExpression<Variable>> right)
        {
            Contract.Requires(right != null);
            Contract.Ensures(Contract.Result<Set<NormalizedExpression<Variable>>>() != null);

            return expressions.Difference(right);
        }

        #endregion

        #region IAbstractDomain Members

        virtual public bool IsBottom
        {
            get
            {
                return false;
            }
        }

        public bool IsTop
        {
            get
            {
                return /*this.expressions != null && */expressions.Count == 0;
            }
        }

        bool IAbstractDomain.LessEqual(IAbstractDomain a)
        {
            var other = a as SegmentLimit<Variable>;

            Contract.Assume(other != null);

            return this.LessEqual(other);
        }

        [Pure]
        public bool LessEqual(SegmentLimit<Variable> other)
        {
            Contract.Requires(other != null);

            if (this.IsBottom)
                return true;

            if (other.IsBottom)
                return false;


            return (!this.IsConditional || other.IsConditional) && expressions.IsSubset(other.Expressions);
        }

        IAbstractDomain IAbstractDomain.Bottom
        {
            get { return this.Bottom; }
        }

        public SegmentLimit<Variable> Bottom
        {
            get
            {
                Contract.Ensures(Contract.Result<SegmentLimit<Variable>>() != null);

                return new BottomSegmentLimit();
            }
        }

        IAbstractDomain IAbstractDomain.Top
        {
            get { return this.Top; }
        }

        public SegmentLimit<Variable> Top
        {
            get
            {
                Contract.Ensures(Contract.Result<SegmentLimit<Variable>>() != null);

                return new SegmentLimit<Variable>(new Set<NormalizedExpression<Variable>>(), true);
            }
        }

        IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
        {
            var tmp = a as SegmentLimit<Variable>;

            Contract.Assume(tmp != null);

            return this.Join(tmp);
        }

        [Pure]
        public SegmentLimit<Variable> Join(SegmentLimit<Variable> other)
        {
            Contract.Requires(other != null);
            Contract.Ensures(Contract.Result<SegmentLimit<Variable>>() != null);

            SegmentLimit<Variable> result;
            if (AbstractDomainsHelper.TryTrivialJoin(this, other, out result))
            {
                return result;
            }

            var set = expressions.Intersection(other.Expressions);
            var isconditional = this.IsConditional || other.IsConditional;

            return new SegmentLimit<Variable>(set, isconditional);
        }

        IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
        {
            var tmp = a as SegmentLimit<Variable>;

            Contract.Assume(tmp != null);

            return this.Meet(tmp);
        }

        [Pure]
        public SegmentLimit<Variable> Meet(SegmentLimit<Variable> other)
        {
            Contract.Requires(other != null);

            Contract.Ensures(Contract.Result<SegmentLimit<Variable>>() != null);

            SegmentLimit<Variable> result;
            if (AbstractDomainsHelper.TryTrivialMeet(this, other, out result))
            {
                return result;
            }

            var set = expressions.Union(other.Expressions);
            var isconditional = this.IsConditional && other.IsConditional;

            return new SegmentLimit<Variable>(set, isconditional);
        }

        IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
        {
            var other = prev as SegmentLimit<Variable>;

            Contract.Assume(other != null);

            return this.Widening(other);
        }

        [Pure]
        [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-other != null")]
        public SegmentLimit<Variable> Widening(SegmentLimit<Variable> other)
        {
            Contract.Requires(other != null);

            Contract.Ensures(Contract.Result<SegmentLimit<Variable>>() != null);

            // F: ... We assume finitely many expressions ...
            return this.Join(other);
        }

        public T To<T>(IFactory<T> factory)
        {
            return factory.IdentityForAnd;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this;
        }

        #endregion

        #region ToString
        public override string ToString()
        {
            if (this.IsBottom)
                return "bott";
            if (this.IsTop)
                return "{}?";

            var res = new StringBuilder();

            res.Append(expressions.ToString());
            if (this.IsConditional)
            {
                res.Append('?');
            }

            return "{ " + res.ToString() + "}";
        }
        #endregion

        #region IEnumerable<NormalizedExpression<Variable>> Members

        public IEnumerator<NormalizedExpression<Variable>> GetEnumerator()
        {
            return expressions.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Look up Utils
        [Pure]
        public bool ContainsConstant
        {
            get
            {
                foreach (var e in expressions)
                {
                    Contract.Assume(e != null);

                    int dummy;
                    if (e.IsConstant(out dummy))
                        return true;
                }

                return false;
            }
        }

        /// <returns>
        /// true if this segment limit is the successor of the other
        /// </returns>
        [Pure]
        public bool IsSuccessorOf(SegmentLimit<Variable> segmentLimit)
        {
            Contract.Requires(segmentLimit != null);

            foreach (var limit in segmentLimit)
            {
                Contract.Assume(limit != null);

                if (this.Contains(limit.PlusOne()))
                {
                    return true;
                }
            }

            return false;
        }

        [Pure]
        internal bool IsAtLeftOf(SegmentLimit<Variable> segmentLimit)
        {
            Contract.Requires(segmentLimit != null);

            int vThis, vSegmentLimit;
            if (this.TryFindConstant(out vThis) && segmentLimit.TryFindConstant(out vSegmentLimit))
            {
                return vThis < vSegmentLimit;
            }

            return false;
        }

        [Pure]
        public bool TryFindConstant(out int val)
        {
            foreach (var limit in expressions)
            {
                if (limit != null && limit.IsConstant(out val))
                {
                    return true;
                }
            }

            val = default(Int32);
            return false;
        }
        #endregion

        #region Bottom Segment
        private class BottomSegmentLimit : SegmentLimit<Variable>
        {
            [ContractVerification(false)]
            public BottomSegmentLimit()
              : base((SegmentLimit<Variable>)null, false)    // F: Here we are violating the base class invariant...
            { }

            public override bool IsBottom
            {
                get
                {
                    return true;
                }
            }

            public override string ToString()
            {
                return "Bottom segment";
            }
        }
        #endregion

        public Set<NormalizedExpression<Variable>> MinusOne()
        {
            Contract.Ensures(Contract.Result<Set<NormalizedExpression<Variable>>>() != null);

            var result = new Set<NormalizedExpression<Variable>>();

            if (this.IsNormal())
            {
                foreach (var limit in expressions)
                {
                    if (limit != null)
                    {
                        result.Add(limit.MinusOne());
                    }
                }

                return result;
            }

            return result;
        }
    }
}