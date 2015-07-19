// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;


public class ConstructorTestDerived
{
    private object data;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: this.data != null", PrimaryILOffset = 26, MethodILOffset = 49)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 43, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 15, MethodILOffset = 49)]
    public void M(object obj, bool flag)
    {
        Contract.Requires(obj != null);
        Contract.Ensures(data != null);

        if (flag)
        {
            data = obj;
        }
    }
}
