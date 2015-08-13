// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Research.CodeAnalysis
{
    public interface ILogOptions : IFrameworkLogOptions
    {
        /// <summary>
        /// depending on options, tells whether or not to print certain outcomes.
        /// </summary>
        bool PrintOutcome(ProofOutcome outcome);

        bool ShowInferenceTrace { get; }

        bool IsRegression { get; }

        /// <summary>
        /// True iff we want Clousot to check if Assume can be proven, and hence converted into asserts
        /// </summary>
        bool CheckAssumptions { get; }

        /// <summary>
        /// True iff we want Clousot to check if a condition is always true or false
        /// </summary>
        bool CheckConditions { get; }

        /// <summary>
        /// True iff we want Clousot to check if Assume can be proven, and hence converted into asserts
        /// OR 
        /// iff we want Clousot to check if Assume is in contraddiction with what is known
        /// </summary>
        bool CheckAssumptionsAndContradictions { get; }

        bool CheckExistentials { get; }

        bool CheckInferredRequires { get; }

        ExpressionCacheMode ExpCaching { get; }

        bool UseWeakestPreconditions { get; }

        bool ShowUnprovenObligations { get; }

        bool ShowPaths { get; }

        int MaxPathSize { get; }

        int Steps { get; }

        bool ShowInvariants { get; }

        bool CheckEntryContradictions { get; }

        bool WarningsAsErrors { get; }
    }
}
