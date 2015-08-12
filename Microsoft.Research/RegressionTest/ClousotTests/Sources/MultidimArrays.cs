// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

internal class Test
{
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    public static void Bug1(int x, int y)
    {
        var myval = new double[x, 100];
        Contract.Assert(myval != null); // <--- assert unproven
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
    public static void Bug2(int x, int y)
    {
        var myval = new double[234, 100];
        Contract.Assert(myval != null);
    }
}
