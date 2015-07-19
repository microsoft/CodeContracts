// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

public static class Test
{
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (in unbox)", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 21, MethodILOffset = 46)]
    public static int M(int x)
    {
        Contract.Requires(x < 50);
        Contract.Ensures(Contract.Result<int>() < 50);

        var o = (object)(x);

        var y = (int)o;

        return y;
    }
}
