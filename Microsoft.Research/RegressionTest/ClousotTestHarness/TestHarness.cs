// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.ClousotRegression;

namespace Microsoft.Research.ClousotRegression
{
    public enum ProofOutcome { Top = 0, Bottom, True, False }


    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ClousotRegressionTestAttribute : Attribute
    {
        /// <summary>
        /// Default, valid for all configurations
        /// </summary>
        public ClousotRegressionTestAttribute()
        {
        }

        /// <summary>
        /// Valid when conditional symbol is defined for regression run
        /// </summary>
        public ClousotRegressionTestAttribute(string conditionalSymbol)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = true)]
    public class RegressionOutcomeAttribute : Attribute
    {
        /// <summary>What outcome the proof obligation has</summary>
        public ProofOutcome Outcome { get; set; }
        /// <summary>The associated message</summary>
        public string Message { get; set; }
        /// <summary>The primary PC IL offset</summary>
        public int PrimaryILOffset { get; set; }
        /// <summary>The IL offset within this method</summary>
        public int MethodILOffset { get; set; }

        /// <summary>
        /// Stores outcomes on a method.
        /// </summary>
        public RegressionOutcomeAttribute()
        {
        }

        /// <summary>
        /// Stores global error outcomes on assembly
        /// </summary>
        /// <param name="expectedMessage"></param>
        public RegressionOutcomeAttribute(string expectedMessage)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = true)]
    public class RegressionReanalysisCountAttribute : Attribute
    {
        public int Count { get; set; }

        /// <summary>
        /// Stores outcomes on a method.
        /// </summary>
        public RegressionReanalysisCountAttribute()
        {
            this.Count = 0;
        }

        /// <summary>
        /// Stores global error outcomes on assembly
        /// </summary>
        /// <param name="expectedMessage"></param>
        public RegressionReanalysisCountAttribute(int count)
        {
            this.Count = count;
        }
    }
}
