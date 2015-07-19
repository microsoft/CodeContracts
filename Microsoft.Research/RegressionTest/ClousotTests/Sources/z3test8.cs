// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

public static class Test
{
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"ensures (always false) may be reachable: false", PrimaryILOffset = 2, MethodILOffset = 17)]
    public static int M(int x)
    {
        Contract.Ensures(false);

        int y = x;
        return y % 5;
    }
}
