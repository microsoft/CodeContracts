// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
    bool CheckAssumptionsAndContraddictions { get; }

    bool CheckExistentials { get; }

    bool CheckInferredRequires { get; }

    ExpressionCacheMode ExpCaching { get; }

    bool UseWeakestPreconditions { get; }

    bool UseZ3 { get; }

    bool ShowUnprovenObligations { get; }

    bool ShowPaths { get; }

    int MaxPathSize { get; }

    int Steps { get; }

    bool ShowInvariants { get; }
  }
}
