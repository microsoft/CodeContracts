// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace FrancescoGenericRepro
{
    internal class Program
    {
        private static void Main(string[] args)
        {
        }
    }



    #region I contract binding
    [ContractClass(typeof(IContract<>))]
    public partial interface I<T>
    {
        void M(T t);
    }

    [ContractClassFor(typeof(I<>))]
    internal abstract class IContract<T> : I<T>
    {
        public void M(T t)
        {
            Contract.Requires(t != null);
        }
    }

    #endregion

    internal class C<X, T> : I<T>
      where X : class
    {
        public void M(T t2)
        {
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 19)]
        public void Test(T t3)
        {
            Contract.Assume(t3 != null);

            M(t3);
        }
    }


    #region J contract binding
    [ContractClass(typeof(JContract))]
    public partial interface J
    {
        void M<T>(T x);
    }

    [ContractClassFor(typeof(J))]
    internal abstract class JContract : J
    {
        public void M<T>(T x2)
        {
            Contract.Requires(x2 != null);
        }
    }
    #endregion

    internal class D<X> : J
      where X : class
    {
        public void M<T>(T x3)
        {
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 19)]
        public void Test(X x4)
        {
            Contract.Assume(x4 != null);

            M(x4);
        }
    }


    internal class A<X>
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 33, MethodILOffset = 39)]
        public virtual X M(X x1)
        {
            Contract.Requires(x1 != null);
            Contract.Ensures(Contract.Result<X>() != null);
            return x1;
        }
    }

    internal class B<Y, X> : A<X>
      where Y : class
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 33, MethodILOffset = 1)]
        public override X M(X x1)
        {
            return x1;
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 19)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 37, MethodILOffset = 0)]
        public void Test(X x2)
        {
            Contract.Assume(x2 != null);
            var result = M(x2);
            Contract.Assert(result != null);
        }
    }

    internal class C<Z, Y, X> : B<Y, X>
      where Y : class
      where Z : class
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 2)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 33, MethodILOffset = 9)]
        public override X M(X x1)
        {
            var result = base.M(x1);
            return result;
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 19)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 37, MethodILOffset = 0)]
        new public void Test(X x2)
        {
            Contract.Assume(x2 != null);

            var result = M(x2);
            Contract.Assert(result != null);
        }
    }

    internal class D : C<string, string, int>
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 15)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 33, MethodILOffset = 22)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 8, MethodILOffset = 22)]
        public override int M(int x1)
        {
            Contract.Ensures(Contract.Result<int>() > 0);

            var result = base.M(x1);
            return 1;
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 11)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
        new public void Test(int x2)
        {
            Contract.Requires(x2 == 0);

            var result = M(x2);
            // ensures specialization needs to kick in
            Contract.Assert(result > 0);
        }
    }


    internal class Recursive<This>
      where This : Recursive<This>
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 16, MethodILOffset = 27)]
        private This GetInstance()
        {
            Contract.Ensures(Contract.Result<This>() != null);
            return (This)this;
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 32, MethodILOffset = 0)]
        public void Test()
        {
            var result = GetInstance();

            result.AddSomething();

            Contract.Assert(result != null);
        }

        private void AddSomething()
        {
        }
    }
}
