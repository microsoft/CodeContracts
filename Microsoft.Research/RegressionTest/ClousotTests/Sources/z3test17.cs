// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

public static class Test
{
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 42, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 54, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 84, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 99, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 114, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 129, MethodILOffset = 0)]
    public static void M(ref int x, ref int y)
    {
        Contract.Requires(x < 50);

        if (x > 25)
        {
            y = x - 24;
            Contract.Assert(y >= 0);
            Contract.Assert(y < 50);
        }
        else
        {
            y = x + 25;
            Contract.Assert(y >= 0, "not true if x < -25");
            Contract.Assert(y <= 50);
        }

        Contract.Assert(y >= 0);
        Contract.Assert(y <= 50);
    }
}
