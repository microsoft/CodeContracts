// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

internal class Test
{
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: first != 0", PrimaryILOffset = 13, MethodILOffset = 17)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = "requires is false: second != 0", PrimaryILOffset = 31, MethodILOffset = 17)]
    public static void Main(string[] args)
    {
        Decimal first = 5;
        Decimal second = 0;

        Console.WriteLine(Add(first, second));
    }

    [ClousotRegressionTest]
    public static Decimal Add(Decimal first, Decimal second)
    {
        Contract.Requires(first != 0);
        Contract.Requires(second != 0);
        Contract.Ensures(Contract.Result<Decimal>() == first + second);

        return first + second;
    }
}

