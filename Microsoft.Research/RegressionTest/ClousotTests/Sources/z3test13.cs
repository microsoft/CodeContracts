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
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 2, MethodILOffset = 55)]
    public static int M(uint x)
    {
        Contract.Ensures(false);

        if (x > 1000) throw new Exception();

        uint y = x + 1;

        if (y > x) throw new Exception();

        return 0;
    }
}
