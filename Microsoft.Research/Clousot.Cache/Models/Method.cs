// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.Research.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching.Models
{
    public class Method
    {
        public Method()
        {
            this.Assemblies = new List<AssemblyInfo>();
            this.IdHashTimeToMethods = new List<IdHashTimeToMethod>();
            this.Outcomes = new List<Outcome>();
            this.Suggestions = new List<Suggestion>();
        }

        public long Id { get; set; }
        public byte[] Hash { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public long PureParametersMask { get; set; }
        public int StatsTop { get; set; }
        public int StatsBottom { get; set; }
        public int StatsTrue { get; set; }
        public int StatsFalse { get; set; }
        public int SwallowedTop { get; set; }
        public int SwallowedBottom { get; set; }
        public int SwallowedTrue { get; set; }
        public int SwallowedFalse { get; set; }
        public long Contracts { get; set; }
        public long MethodInstructions { get; set; }
        public long ContractInstructions { get; set; }
        public bool Timeout { get; set; }
        public byte[] InferredExpr { get; set; }
        public byte[] InferredExprHash { get; set; }
        public string InferredExprString { get; set; }
        public virtual ICollection<AssemblyInfo> Assemblies { get; set; }
        public virtual ICollection<IdHashTimeToMethod> IdHashTimeToMethods { get; set; }
        public virtual ICollection<Outcome> Outcomes { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }

        [NotMapped]
        public AnalysisStatistics Statistics
        {
            get
            {
                var res = new AnalysisStatistics
                {
                    Bottom = (uint)this.StatsBottom,
                    Top = (uint)this.StatsTop,
                    True = (uint)this.StatsTrue,
                    False = (uint)this.StatsFalse,
                    Total = (uint)(this.StatsBottom + this.StatsTop + this.StatsTrue + this.StatsFalse)
                };
                return res;
            }
            set
            {
                this.StatsBottom = (int)value.Bottom;
                this.StatsFalse = (int)value.False;
                this.StatsTrue = (int)value.True;
                this.StatsTop = (int)value.Top;
            }
        }

        [NotMapped]
        public ContractDensity ContractDensity
        {
            get
            {
                return new ContractDensity(
                  (ulong)this.MethodInstructions,
                  (ulong)this.ContractInstructions,
                  (ulong)this.Contracts);
            }
            set
            {
                this.MethodInstructions = (long)value.MethodInstructions;
                this.ContractInstructions = (long)value.ContractInstructions;
                this.Contracts = (long)value.Contracts;
            }
        }

        [NotMapped]
        public SwallowedBuckets Swallowed
        {
            get
            {
                return new SwallowedBuckets(
                  outcome =>
                  {
                      switch (outcome)
                      {
                          case ProofOutcome.Top:
                              return this.SwallowedTop;
                          case ProofOutcome.Bottom:
                              return this.SwallowedBottom;
                          case ProofOutcome.True:
                              return this.SwallowedTrue;
                          case ProofOutcome.False:
                              return this.SwallowedFalse;
                          default:
                              throw new ArgumentException();
                      }
                  });
            }
            set
            {
                Contract.Requires(value != null);

                this.SwallowedTop = value.GetCounter(ProofOutcome.Top);
                this.SwallowedBottom = value.GetCounter(ProofOutcome.Bottom);
                this.SwallowedTrue = value.GetCounter(ProofOutcome.True);
                this.SwallowedFalse = value.GetCounter(ProofOutcome.False);
            }
        }
    }
}
