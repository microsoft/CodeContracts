// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.Research.AbstractDomains.Numerical;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.AbstractDomains
{
    [ContractClass(typeof(IFactoryContracts<>))]
    public interface IFactory<T>
    {
        IFactory<T> FactoryWithName(T name);

        T IdentityForAnd { get; }
        T IdentityForOr { get; }
        T IdentityForAdd { get; }

        T Null { get; }

        T Constant(Int32 constant);
        T Constant(Int64 constant);
        T Constant(Rational constant);
        T Constant(bool constant);
        T Constant(double constant);

        T Variable(object variable);

        T Add(T left, T right);
        T Sub(T left, T right);
        T Mul(T left, T right);
        T Div(T left, T right);

        T EqualTo(T left, T right);
        T NotEqualTo(T left, T right);
        T LessThan(T left, T right);
        T LessEqualThan(T left, T right);

        T And(T left, T right);
        T Or(T left, T right);

        T ArrayIndex(T array, T index);

        T ForAll(T inf, T sup, T body);
        T Exists(T inf, T sup, T body);

        bool TryGetName(out T name);
        bool TryGetBoundVariable(out T boundVariable);
        bool TryArrayLengthName(T array, out T name);

        List<T> SplitAnd(T t);
    }

    #region Contracts

    [ContractClassFor(typeof(IFactory<>))]
    internal abstract class IFactoryContracts<T>
    : IFactory<T>
    {
        #region IFactory<T> Members

        public IFactory<T> FactoryWithName(T name)
        {
            Contract.Ensures(Contract.Result<IFactory<T>>() != null);

            throw new NotImplementedException();
        }

        public T Constant(int constant)
        {
            throw new NotImplementedException();
        }

        public T Constant(long constant)
        {
            throw new NotImplementedException();
        }

        public T Constant(Rational constant)
        {
            throw new NotImplementedException();
        }

        public T Constant(bool constant)
        {
            throw new NotImplementedException();
        }

        public T Constant(double constant)
        {
            throw new NotImplementedException();
        }

        public T Variable(object variable)
        {
            throw new NotImplementedException();
        }

        public T Add(T left, T right)
        {
            throw new NotImplementedException();
        }

        public T Sub(T left, T right)
        {
            throw new NotImplementedException();
        }

        public T Mul(T left, T right)
        {
            throw new NotImplementedException();
        }

        public T Div(T left, T right)
        {
            throw new NotImplementedException();
        }

        public T EqualTo(T left, T right)
        {
            throw new NotImplementedException();
        }

        public T NotEqualTo(T left, T right)
        {
            throw new NotImplementedException();
        }

        public T LessThan(T left, T right)
        {
            throw new NotImplementedException();
        }

        public T LessEqualThan(T left, T right)
        {
            throw new NotImplementedException();
        }

        public T And(T left, T right)
        {
            throw new NotImplementedException();
        }

        public T Or(T left, T right)
        {
            throw new NotImplementedException();
        }

        public T IdentityForAnd
        {
            get { throw new NotImplementedException(); }
        }

        public T IdentityForOr
        {
            get { throw new NotImplementedException(); }
        }

        public T IdentityForAdd
        {
            get { throw new NotImplementedException(); }
        }

        public T ArrayIndex(T array, T Index)
        {
            throw new NotImplementedException();
        }

        public T ForAll(T inf, T sup, T body)
        {
            Contract.Requires(inf != null);
            Contract.Requires(sup != null);
            Contract.Requires(body != null);

            throw new NotImplementedException();
        }

        public T Exists(T inf, T sup, T body)
        {
            Contract.Requires(inf != null);
            Contract.Requires(sup != null);
            Contract.Requires(body != null);

            throw new NotImplementedException();
        }

        public bool TryGetName(out T name)
        {
            throw new NotImplementedException();
        }

        public bool TryArrayLengthName(T array, out T name)
        {
            throw new NotImplementedException();
        }

        public bool TryGetBoundVariable(out T name)
        {
            throw new NotImplementedException();
        }

        public List<T> SplitAnd(T t)
        {
            Contract.Ensures(Contract.Result<List<T>>() != null);

            throw new NotImplementedException();
        }

        #endregion

        #region IFactory<T> Members


        public T Null
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }

    #endregion
}