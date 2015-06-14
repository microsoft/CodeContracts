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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  partial class VersionResult
  {
    public long AllWarnings { get; protected set; }
    public long StatsAll { get; protected set; }
    public long SwallowedWarnings { get; protected set; }
    public long DisplayedWarnings { get; protected set; }
    public double ContractsPerMethod { get; protected set; }
    public double ContractDensity { get; protected set; }

    public VersionResult()
    { }

    public VersionResult(long version, IEnumerable<MethodModel> M)
    {
      Contract.Requires(M != null);

      foreach (var m in M)
        this.Add(m);

      this.Version = version;
      this.Methods = M.Count();
      this.Complete();
    }

    private void Add(MethodModel m)
    {
      Contract.Requires(m != null);
      Contract.Requires(m.Outcomes != null);
      Contract.Requires(m.Suggestions != null);

      this.ContractInstructions += m.ContractInstructions;
      this.Contracts += m.Contracts;
      this.MethodInstructions += m.MethodInstructions;
      this.Outcomes += m.Outcomes.Count();
      this.StatsBottom += m.StatsBottom;
      this.StatsFalse += m.StatsFalse;
      this.StatsTop += m.StatsTop;
      this.StatsTrue += m.StatsTrue;
      this.Suggestions += m.Suggestions.Count();
      this.SwallowedBottom += m.SwallowedBottom;
      this.SwallowedFalse += m.SwallowedFalse;
      this.SwallowedTop += m.SwallowedTop;
      this.SwallowedTrue += m.SwallowedTrue;
      this.Timeout += m.Timeout ? 1 : 0;

      this.HasWarnings += m.StatsBottom + m.StatsFalse + m.StatsTop > 0 ? 1 : 0;
      this.ZeroTop += m.StatsTop == 0 ? 1 : 0;
    }

    internal void Complete()
    {
      this.AllWarnings = this.StatsBottom + this.StatsFalse + this.StatsTop;
      this.StatsAll = this.AllWarnings + this.StatsTrue;
      this.SwallowedWarnings = this.SwallowedBottom + this.SwallowedFalse + this.SwallowedTop;
      this.DisplayedWarnings = this.AllWarnings - this.SwallowedWarnings;
      this.ContractsPerMethod = SafeDiv(this.Contracts, this.Methods);
      this.ContractDensity = SafeDiv(this.ContractInstructions, this.MethodInstructions);
    }

    private static double SafeDiv(long x, long y, double def = default(double))
    {
      return y == 0 ? def : (double)x / (double)y;
    }

    static public VersionResult Create(long version, IEnumerable<MethodModel> M)
    {
      Contract.Requires(M != null);
      Contract.Ensures(Contract.Result<VersionResult>() != null);

      return new VersionResult(version, M);
    }
  }
}
