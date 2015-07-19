// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Configuration;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

internal class Test
{
    public static string Bug
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference 'value'", PrimaryILOffset = 32, MethodILOffset = 0)]
        get
        {
            string value = ConfigurationManager.AppSettings["bug"];
            while (value.StartsWith("/"))
            {
                value = value.Substring(1);
            }
            while (value.EndsWith("/"))
            {
                value = value.Substring(0, value.Length - 1);
            }

            return value;
        }
    }
}

