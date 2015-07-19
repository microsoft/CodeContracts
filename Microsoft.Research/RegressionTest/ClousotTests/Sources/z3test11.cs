// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

public struct S
{
    public int a;
}

public static class Test
{
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"ensures unreachable", PrimaryILOffset = 2, MethodILOffset = 58)]
    public static int M(S x)
    {
        Contract.Ensures(false);

        if (x.a == 0)
        {
            throw new Exception();
        }
        if (x.a != 0)
        {
            throw new Exception();
        }

        return 0;
    }
}
