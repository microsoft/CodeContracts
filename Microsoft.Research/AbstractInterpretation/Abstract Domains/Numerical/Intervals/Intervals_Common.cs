// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// The implementation of the intervals

using System;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    [ContractVerification(true)]
    [ContractClass(typeof(IntervalBaseTypeContracts<,>))]
    abstract public class IntervalBase<IType, NType>
      : IAbstractDomain
      where IType : IntervalBase<IType, NType>
    {
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(!object.Equals(this.lowerBound, null));
            Contract.Invariant(!object.Equals(this.upperBound, null));
        }

        protected /*readonly*/ NType lowerBound;
        protected /*readonly*/ NType upperBound;

        protected IntervalBase(NType lower, NType upper)
        {
            Contract.Requires(!object.Equals(lower, null));
            Contract.Requires(!object.Equals(upper, null));

            this.lowerBound = lower;
            this.upperBound = upper;
        }


        #region Common Interval helper methods
        public NType UpperBound
        {
            get
            {
                Contract.Ensures(!object.Equals(Contract.Result<NType>(), null));

                return this.upperBound;
            }
        }

        public NType LowerBound
        {
            get
            {
                Contract.Ensures(!object.Equals(Contract.Result<NType>(), null));

                return this.lowerBound;
            }
        }

        public bool IsSingleton
        {
            get
            {
                return this.IsNormal() && lowerBound.Equals(upperBound);
            }
        }

        /// <summary>
        /// Is this a singleton? If it is, then its value is v
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool TryGetSingletonValue(out NType v)
        {
            Contract.Ensures(!Contract.Result<bool>() || !(object.Equals(Contract.ValueAtReturn(out v), null)));

            if (this.IsNormal && this.IsSingleton)
            {
                v = this.LowerBound;
                return true;
            }
            else
            {
                v = default(NType);
                return false;
            }
        }

        #endregion

        #region Type information

        abstract public bool IsInt32 { get; }

        abstract public bool IsInt64 { get; }

        #endregion

        #region Unknown intervals, etc.

        public bool IsFinite
        {
            get
            {
                return this.IsNormal && !this.IsLowerBoundMinusInfinity && !this.IsUpperBoundPlusInfinity;
            }
        }

        #endregion

        #region To be overridden

        abstract public bool IsLowerBoundMinusInfinity
        {
            get;
        }

        abstract public bool IsUpperBoundPlusInfinity
        {
            get;
        }

        abstract public bool IsNormal
        {
            get;
        }

        [Pure]
        abstract public IType ToUnsigned();

        [Pure]
        abstract public bool LessEqual(IType a);

        abstract public bool IsBottom { get; }

        abstract public bool IsTop { get; }

        abstract public IType Bottom { get; }

        abstract public IType Top { get; }

        [Pure]
        abstract public IType Join(IType a);

        [Pure]
        abstract public IType Meet(IType a);

        [Pure]
        abstract public IType Widening(IType a);

        [Pure]
        abstract public IType DuplicateMe();

        #endregion

        #region Code to factor out the interface calls

        IAbstractDomain IAbstractDomain.Bottom
        {
            get
            {
                return this.Bottom;
            }
        }

        IAbstractDomain IAbstractDomain.Top
        {
            get
            {
                return this.Top;
            }
        }

        bool IAbstractDomain.LessEqual(IAbstractDomain a)
        {
            return this.LessEqual((IType)a);
        }

        IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
        {
            return this.Join((IType)a);
        }

        IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
        {
            return this.Meet((IType)a);
        }

        IAbstractDomain IAbstractDomain.Widening(IAbstractDomain/*!*/ prev)
        {
            return this.Widening((IType)prev);
        }
        #endregion

        #region To<>

        public virtual T To<T>(IFactory<T> factory)
        {
            T varName;
            if (this.IsBottom)
            {
                return factory.Constant(false);
            }
            else if (this.IsTop)
            {
                return factory.Constant(true);
            }
            else if (factory.TryGetName(out varName))
            {
            }

            return factory.Constant(true);
        }

        public override string ToString()
        {
            return "[" + this.LowerBound + ", " + this.upperBound + "]" + (this.IsBottom ? "_|_" : "");
        }

        public object Clone()
        {
            return this.DuplicateMe();
        }
        #endregion
    }


    [ContractClassFor(typeof(IntervalBase<,>))]
    internal abstract class IntervalBaseTypeContracts<IType, NType>
      : IntervalBase<IType, NType>
      where IType : IntervalBase<IType, NType>
    {
        public IntervalBaseTypeContracts(NType lower, NType upper) : base(lower, upper) { }

        abstract public override bool IsInt32 { get; }

        abstract public override bool IsInt64 { get; }

        abstract public override bool IsLowerBoundMinusInfinity { get; }

        abstract public override bool IsUpperBoundPlusInfinity { get; }

        abstract public override bool IsNormal { get; }

        public override bool LessEqual(IType a)
        {
            Contract.Requires(a != null);

            return default(bool);
        }

        abstract public override bool IsBottom { get; }

        abstract public override bool IsTop { get; }

        public override IType Bottom
        {
            get
            {
                Contract.Ensures(Contract.Result<IType>() != null);

                return default(IType);
            }
        }

        public override IType Top
        {
            get
            {
                Contract.Ensures(Contract.Result<IType>() != null);

                return default(IType);
            }
        }

        public override IType Join(IType a)
        {
            Contract.Requires(a != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Meet(IType a)
        {
            Contract.Requires(a != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Widening(IType a)
        {
            Contract.Requires(a != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType DuplicateMe()
        {
            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }
    }
}