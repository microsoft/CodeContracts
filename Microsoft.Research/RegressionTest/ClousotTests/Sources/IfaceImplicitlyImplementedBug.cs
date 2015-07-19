// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ClousotTests
{
    using System;
    using System.Diagnostics.Contracts;
    using Microsoft.Research.ClousotRegression;

    [ContractClass(typeof(JContracts))]
    public interface J
    {
        bool B();
    }
    [ContractClassFor(typeof(J))]
    public abstract class JContracts : J
    {
        public bool B()
        {
            Contract.Ensures(Contract.Result<bool>());
            throw new NotImplementedException();
        }
    }
    public interface K : J
    {
        new bool B();
    }

    public class Test
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
        public void M1(J j)
        {
            Contract.Requires(j != null);
            Contract.Assert(j.B());
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 20, MethodILOffset = 0)]
        public void M2(K k)
        {
            Contract.Requires(k != null);
            Contract.Assert(k.B());
        }
    }
}
