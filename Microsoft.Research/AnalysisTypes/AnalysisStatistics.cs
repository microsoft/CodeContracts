// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            Contract.Invariant(totalStatistics != null);
            Contract.Invariant(statisticsTime != null);
        }

        private readonly Dictionary<object, AnalysisStatistics> totalStatistics;
        private readonly List<MethodAnalysisTimeStatistics> statisticsTime;

        public AnalysisStatisticsDB()
        {
            totalStatistics = new Dictionary<object, AnalysisStatistics>();
            statisticsTime = new List<MethodAnalysisTimeStatistics>();
        }

        public void Add(object method, AnalysisStatistics stat, MethodClassification methodKind)
        {
            totalStatistics[method] = stat;
        }

        public void Add(MethodAnalysisTimeStatistics methodStat)
        {
            statisticsTime.Add(methodStat);
        }

        public int MethodsWithZeroWarnings()
        {
            return totalStatistics.Values.Where(s => s.True == s.Total).Count();
        }

        public AnalysisStatistics OverallStatistics()
        {
            var result = new AnalysisStatistics();
            ComputeStatistics(totalStatistics.Values, out result.Total, out result.True, out result.Top, out result.Bottom, out result.False);

            return result;
        }

        public IEnumerable<MethodAnalysisTimeStatistics> MethodsByAnalysisTime(int howMany = 10)
        {
            Contract.Ensures(Contract.Result<IEnumerable<MethodAnalysisTimeStatistics>>() != null);

            return statisticsTime.OrderByDescending(stat => stat.OverallTime).Take(howMany);
        }

        public override string ToString()
        {
            uint Total, True, Top, Bottom, False;
            ComputeStatistics(totalStatistics.Values, out Total, out True, out Top, out Bottom, out False);

            return AnalysisStatistics.PrettyPrint(Total, True, Top, Bottom, False);
        }

        private void ComputeStatistics(IEnumerable<AnalysisStatistics> values, out uint Total, out uint True, out uint Top, out uint Bottom, out uint False)
        {
            Contract.Requires(values != null);
            Contract.Ensures(Contract.ValueAtReturn(out Total) == Contract.ValueAtReturn(out True) + Contract.ValueAtReturn(out Top) + Contract.ValueAtReturn(out Bottom) + Contract.ValueAtReturn(out False));

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
        private void ObjectInvariant()
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