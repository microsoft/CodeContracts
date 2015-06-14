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
using System.IO;

namespace Microsoft.Research.DataStructures {

  public interface ILogOptions {

    /// <summary>
    /// Print tracing information during DFA steps
    /// </summary>
    bool TraceDFA { get; }

    /// <summary>
    /// Trace the heap analysis phase of the framework
    /// </summary>
    bool TraceHeapAnalysis { get; }

    /// <summary>
    /// Trace the expression analysis phase of the framework
    /// </summary>
    bool TraceExpressionAnalysis { get; }

    /// <summary>
    /// Trace the egraph operations of the framework
    /// </summary>
    bool TraceEGraph { get; }

    /// <summary>
    /// True if analysis framework should print IL of CFGs
    /// </summary>
    bool PrintIL { get; }

    /// <summary>
    /// depending on options, tells whether or not to print certain outcomes.
    /// </summary>
    bool PrintOutcome(ProofOutcome outcome);

    /// <summary>
    /// Controls whether or not to print suggestions
    /// </summary>
    bool PrintSuggestions { get; }

    bool IsSilentOutput { get; }
    bool IsNormalOutput { get; }
    bool IsVerboseOutput { get; }

    bool UseDropURL { get; }
    string BuildMachineURL { get; }
    string DropURL { get; }

    TextWriter LogFile { get; }
    bool LogXML { get; }

    /// <summary>
    /// True if we should try to propagate inferred pre-conditions.
    /// </summary>
    bool PropagateInferredRequires { get; }
  }

}
