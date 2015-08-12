// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

public static class Test
{
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 40, MethodILOffset = 0)]

    public static void M(int x, int y)
    {
        Contract.Requires(y > 50);

        for (int i = x; i > 0; i--)
        {
            x--;
        }

        Contract.Assert(y > 50);
    }
}
