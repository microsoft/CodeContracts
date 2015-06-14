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
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Research.CodeAnalysis
{
  public enum MethodClassification { NewMethod, IdenticalMethod, ChangedMethod };


  [ContractVerification(true)]
  public class AnalysisStatisticsDB
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.totalStatistics != null);
      Contract.Invariant(this.statisticsTime != null);
    }

    private readonly Dictionary<object, AnalysisStatistics> totalStatistics;
    private readonly List<MethodAnalysisTimeStatistics> statisticsTime; 

    public AnalysisStatisticsDB()
    {
      this.totalStatistics = new Dictionary<object, AnalysisStatistics>();
      this.statisticsTime = new List<MethodAnalysisTimeStatistics>();
    }

    public void Add(object method, AnalysisStatistics stat, MethodClassification methodKind)
    {
      this.totalStatistics[method] = stat;
    }

    public void Add(MethodAnalysisTimeStatistics methodStat)
    {
      this.statisticsTime.Add(methodStat);
    }
    
    public int MethodsWithZeroWarnings()
    {
      return this.totalStatistics.Values.Where(s => s.True == s.Total).Count();
    }

    public AnalysisStatistics OverallStatistics()
    {
      var result = new AnalysisStatistics();
      ComputeStatistics(this.totalStatistics.Values, out result.Total, out result.True, out result.Top, out result.Bottom, out result.False);

      return result;
    }

    public IEnumerable<MethodAnalysisTimeStatistics> MethodsByAnalysisTime(int howMany = 10)
    {
      Contract.Ensures(Contract.Result<IEnumerable<MethodAnalysisTimeStatistics>>() != null);

      return this.statisticsTime.OrderByDescending(stat => stat.OverallTime).Take(howMany); 
    }

    public override string ToString()
    {
      uint Total, True, Top, Bottom, False;
      ComputeStatistics(this.totalStatistics.Values, out Total, out True, out Top, out Bottom, out False);

      return AnalysisStatistics.PrettyPrint(Total, True, Top, Bottom, False);
    }

    private void ComputeStatistics(IEnumerable<AnalysisStatistics> values, out uint Total, out uint True, out uint Top, out uint Bottom, out uint False)
    {
      Contract.Requires(values != null);
      Contract.Ensures(Contract.ValueAtReturn(out Total) == Contract.ValueAtReturn(out True) + Contract.ValueAtReturn(out Top) + Contract.ValueAtReturn(out Bottom)+Contract.ValueAtReturn(out False));

      Total = True = Top = Bottom = False = 0;
      foreach (var value in values)
      {
        Contract.Assume(value.Total == value.Top + value.Bottom + value.True + value.False);

        Total += value.Total;
        Top += value.Top;
        Bottom += value.Bottom;
        True += value.True;
        False += value.False;
      }
    }

  }

  [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
  [ContractVerification(true)]
  public struct AnalysisStatistics
  {
    #region Invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.Total >= 0);
      Contract.Invariant(this.Top >= 0);
      Contract.Invariant(this.Bottom >= 0);
      Contract.Invariant(this.True >= 0);
      Contract.Invariant(this.False >= 0);

      Contract.Invariant(this.Total == this.Top + this.Bottom + this.True + this.False);
    }
    #endregion

    public uint Total;
    public uint Top;
    public uint Bottom;
    public uint True;
    public uint False;
    public int InferredPreconditions;
    public int InferredInvariants;
    public int InferredEntryAssumes;

    public void Add(AnalysisStatistics right)
    {
      Contract.Assert(right.True >= 0);
      Contract.Assert(right.Top >= 0);
      Contract.Assert(right.False >= 0);
      Contract.Assert(right.Bottom >= 0);
      Contract.Assume(right.Total == right.True + right.Top + right.False + right.Bottom);

      this.Total += right.Total;
      this.Top += right.Top;
      this.Bottom += right.Bottom;
      this.True += right.True;
      this.False += right.False;
    }

    public void Add(ProofOutcome color, uint howMany = 1)
    {
      Contract.Requires(howMany > 0);

      switch (color)
      {
        case ProofOutcome.Bottom:
          this.Bottom += howMany;
          break;
        case ProofOutcome.Top:
          this.Top += howMany;
          break;
        case ProofOutcome.True:
          this.True += howMany;
          break;
        case ProofOutcome.False:
          this.False += howMany;
          break;
      }
      this.Total += howMany;
    }

    override public string ToString()
    {
      return PrettyPrint(this.Total, this.True, this.Top, this.Bottom, this.False);
    }

    internal static string PrettyPrint(uint Total, uint True, uint Top, uint Bottom, uint False)
    {
      Contract.Requires(Total == True + Top + Bottom + False);

      string cp, pc, pt, pu, pf;

      if (Total > 0)
      {
        cp = "Checked " + Total + " assertion" + (Total > 1 ? "s" : "") + ": ";
        pc = True > 0 ? True + " correct " : "";
        pt = Top > 0 ? Top + " unknown " : "";
        pu = Bottom > 0 ? Bottom + " unreached " : "";
        pf = False > 0 ? False + " false " : "";
      }
      else
      {
        cp = "Checked 0 assertions.";
        pc = pt = pu = pf = "";
      }

      var precision = Total > 0 ? ((True + Bottom) * 100) / (double)(Total) : -1;

      var result = cp + pc + pt + pu + pf;

      if (precision >= 0)
      {
        var tmp = Math.Truncate(precision * 10) / 10;
        result = result + Environment.NewLine + "Validated: " + tmp + "%";
      }

      return result;
    }
  }

  [ContractVerification(true)]
  public struct MethodAnalysisTimeStatistics
  {
    readonly public int MethodNumber;
    readonly public string MethodName;
    readonly public TimeSpan OverallTime;
    readonly public TimeSpan CFGConstructionTime;

    public MethodAnalysisTimeStatistics(int methodNumber, string methodName, TimeSpan overall, TimeSpan CFGConstructionTime)
    {
      this.MethodNumber = methodNumber;
      this.MethodName = methodName;
      this.OverallTime = overall;
      this.CFGConstructionTime = CFGConstructionTime;
    }

    public override string ToString()
    {
      return string.Format("Overall = {0} ({1} in CFG Construction)", this.OverallTime, this.CFGConstructionTime); 
    }
  }
}