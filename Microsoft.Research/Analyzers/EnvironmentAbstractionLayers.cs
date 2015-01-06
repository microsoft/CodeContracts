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

// this file contains the abstraction layers for the environment, for error reporting, analysis statistics and output of postconditions

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.DataStructures;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.ClousotPulse.Messages;

namespace Microsoft.Research.CodeAnalysis
{
  public static class AnalysisStatisticsExtensions
  {
    
    public static ClousotAnalysisResults EmitStats(this AnalysisStatistics @this, int swallowedTop, int swallowedBottom, int swallowedFalse, string assemblyName, IOutputResults output)
    {
      Contract.Requires(swallowedTop >= 0);
      Contract.Requires(swallowedBottom >= 0);
      Contract.Requires(swallowedFalse >= 0);
      Contract.Requires(assemblyName != null);
      Contract.Ensures(Contract.Result<ClousotAnalysisResults>() != null);

      var result = new ClousotAnalysisResults();

      if (@this.Total > 0)
      {
        var True = @this.True;
        var Top = Math.Max(@this.Top - swallowedTop, 0);
        var Bottom = Math.Max(@this.Bottom - swallowedBottom, 0);
        var False = Math.Max(@this.False - swallowedFalse, 0);

        var Total = True + Top + Bottom + False;

        var masked = swallowedTop + swallowedFalse + swallowedBottom;

        Contract.Assert(masked >= 0);

        Contract.Assert(Top >= 0);
        Contract.Assert(Bottom >= 0);
        Contract.Assert(False >= 0);

        var stats = String.Format("Checked {0} assertion{1}: {2}{3}{4}{5}{6}",
          @this.Total.ToString(),
          @this.Total > 1 ? "s" : "",
          True > 0 ? True + " correct " : "",
          Top > 0 ? Top + " unknown " : "",
          Bottom > 0 ? Bottom + " unreached " : "",
          False > 0 ? False + " false" : "",
          masked > 0 ? "(" + masked + " masked)" : "");

        output.FinalStatistic(assemblyName,stats);

        double precision = Total != 0 ? True / (double)Total : 1.0;
        output.Statistic("Validated: {0,6:P1}", precision);

        // for scripts parsing msbuild output
        output.WriteLine(stats);

        // Update the result
        result.Total = @this.Total;
        result.True = True;
        result.False = False;
        result.Bottom = Bottom;
        result.Top = Top;
        result.Masked = masked;
      }
      else
      {
        output.FinalStatistic(assemblyName, "Checked 0 assertions.");
      }

      return result;
    }
  }
}
