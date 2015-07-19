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
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 2, MethodILOffset = 56)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
    public static int M(ref S x)
    {
        Contract.Ensures(false);

        if (x.a > 0)
        {
            throw new Exception();
        }
        if (x.a <= 0)
        {
            throw new Exception();
        }

        return 0;
    }
}
