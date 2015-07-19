// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

public static class Test
{
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"ensures unreachable", PrimaryILOffset = 2, MethodILOffset = 46)]
    public static int M(int x)
    {
        Contract.Ensures(false);

        if (x == 0)
        {
            throw new Exception();
        }
        if (x != 0)
        {
            throw new Exception();
        }

        return 0;
    }
}
