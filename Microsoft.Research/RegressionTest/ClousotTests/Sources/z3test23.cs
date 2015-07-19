// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;


public class ConstructorTestDerived
{
    private ConstructorTestDerived next;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 61, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 74, MethodILOffset = 0)]
    public void M(ConstructorTestDerived obj)
    {
        Contract.Requires(obj != null);
        Contract.Assert(this != null);

        var last = obj;
        while (obj != null)
        {
            last = obj;
            obj = obj.next;
        }

        Contract.Assert(last != null);
        Contract.Assert(this != null);
    }
}
