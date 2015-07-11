// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#region Using directives

using System;

#endregion

namespace Microsoft.Glee
{
    public static class LpFactory
    {
        //it makes the constructor not collable from the outside
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1807:AvoidUnnecessaryStringCreation", MessageId = "lp")]
        public static Optimization.LinearProgramInterface CreateLP()
        {
            string lp = Environment.GetEnvironmentVariable("LP", EnvironmentVariableTarget.User);
            if (lp != null && lp.ToLower() == "lp")
                return new Optimization.LP(true); //true is to look for zero columns
#if DEBUGGEE
            else if (lp != null && lp.ToLower() == "test")
                return new Microsoft.Glee.Optimization.LPTestSolver(lookForZeroColumns);
#endif

            return new Optimization.RevisedSimplexMethod();
        }
    }
}
