// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

internal class Test
{
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 16, MethodILOffset = 23)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: second != 0", PrimaryILOffset = 37, MethodILOffset = 23)]
    public static void Main(string[] args)
    {
        double first = 5;
        double second = 0;

        Console.WriteLine(Add(first, second));
    }

    public static double Add(double first, double second)
    {
        Contract.Requires(first != 0);
        Contract.Requires(second != 0);
        Contract.Ensures(Contract.Result<double>() == first + second);

        return first + second;
    }
}

