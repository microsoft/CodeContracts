// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

public static class Test
{
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 84, MethodILOffset = 0)]

    public static void M(int x, int y)
    {
        int a;
        if (x > 0)
        {
            if (y > 0)
            {
                a = 1;
            }
            else
            {
                a = 2;
            }
        }
        else
        {
            if (y > 0)
            {
                a = 3;
            }
            else
            {
                a = 4;
            }
        }

        Contract.Assume(x <= 0 || y > 0);
        Contract.Assert(a != 2);
    }
}
