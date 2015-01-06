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
using System.Linq;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  [ContractVerification(true)]
  class QuantitativeOutput<Method, Assembly> : DerivableOutputFullResults<Method, Assembly>
  {
    #region Object invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.warnings != null);
      Contract.Invariant(this.warningLevel != null);
      Contract.Invariant(this.swallowedCount != null);
      Contract.Invariant(this.scoresManager != null);
      Contract.Invariant(this.finalStatsMessage == null || this.finalStatsAssembly != null);
    }
    #endregion
  
    // warninglevel is used to filter the warnings according their score, and the thresholds.
    // We keep a function because the warning level can be changed dynamically
    private readonly Func<WarningLevelOptions> warningLevel;

    private readonly WarningScoresManager scoresManager;
    private readonly SortedDictionary<double, List<Bucket>> warnings;
    private readonly bool sortWarnings;
    private readonly int maxWarnings;
    private readonly bool showScores;
    private readonly bool showJustification;

    /// <summary>
    /// We count how many warnings we emitted and how many were signaled
    /// </summary>
    private int emitCount;
    private int outcomeCount;
    private SwallowedBuckets swallowedCount;

    /// <summary>
    /// Used here so we can emit the final stats after all the errors
    /// </summary>
    private string finalStatsAssembly;
    private string finalStatsMessage;

    #region Constructor
    public QuantitativeOutput(
      bool SortWarnings, bool ShowScores, bool showJustification,
      Func<WarningLevelOptions> WarningLevel, WarningScoresManager scoresManager,
      int MaxWarnings, IOutputFullResults<Method, Assembly> output)
      : base(output)
    {
      Contract.Requires(scoresManager != null);
      Contract.Requires(output != null);

      this.sortWarnings = SortWarnings;
      this.showJustification = showJustification;
      this.showScores = ShowScores;
      this.warningLevel = WarningLevel;
      this.maxWarnings = MaxWarnings;
      this.scoresManager = scoresManager;
      this.warnings = new SortedDictionary<double, List<Bucket>>(new InverseNaturalOrder());
      this.emitCount = 0;
      this.outcomeCount = 0;
      this.swallowedCount = new SwallowedBuckets();
    }
    #endregion

    #region IOutputFullResults<Method> Members

    public override int SwallowedMessagesCount(ProofOutcome outcome)
    {
      return this.swallowedCount.GetCounter(outcome);
    }

    #endregion

    #region IOutputResults Members

    public override bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      return EmitOutcomeInternal(Bucket.Kind.EmitOutcome, witness, format, args);
    }

    public override bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      return EmitOutcomeInternal(Bucket.Kind.EmitOutcomeAndRelated, witness, format, args);      
    }

    private bool EmitOutcomeInternal(Bucket.Kind kind, Witness witness, string format, params object[] args)
    {
      Contract.Requires(witness != null);
      Contract.Requires(format != null);

      // First we check if the score of the witness is too low.
      // If it is, then we ignore this warning message at all
      if (!this.HasGoodScore(witness.GetScore(this.scoresManager)))
      {
        this.swallowedCount.UpdateCounter(witness.Outcome);
        return false;
      }

      this.outcomeCount++;

      // Prefix the Justification
      if (this.showJustification)
      {
        format = String.Format("(justification: {0}) {1}", witness.GetJustificationString(this.scoresManager), format);
      }
      
      // Prefix the Score
      if (this.showScores)
      {
        format = String.Format("(score: {0}) {1}", witness.GetScore(this.scoresManager).ToString(), format);
      }

      if (this.sortWarnings)
      {
        var bucket = new Bucket(kind, witness, format, args);

        Insert(witness.GetScore(this.scoresManager), bucket);
      }
      else
      {
        if (this.emitCount <= this.maxWarnings)
        {
          this.emitCount++;

          return kind == Bucket.Kind.EmitOutcome ?
            base.EmitOutcome(witness, format, args) :
            base.EmitOutcomeAndRelated(witness, format, args);
        }
      }

      return true;
    }

    private void Insert(double Score, Bucket bucket)
    {
      if (this.warnings.ContainsKey(Score))
      {
        Contract.Assume(this.warnings[Score] != null);
        this.warnings[Score].Add(bucket);
      }
      else
      {
        this.warnings[Score] = new List<Bucket>() { bucket };
      }
    }

    public override void FinalStatistic(string assemblyName, string msg)
    {
      // hold for emission after errors
      this.finalStatsAssembly = assemblyName;
      this.finalStatsMessage = msg;
    }

    public override void Close()
    {
      this.Flush();

      base.Close();
    }

    private void Flush()
    {
      if (this.sortWarnings)
      {
        var emitted = 0;
        var toSkip = new List<string>();

        foreach (var pair in this.warnings)
        {
          Contract.Assume(pair.Value != null);


          foreach (var bucket in pair.Value)
          {
            // At most MaxWarnings
            if (emitted >= this.maxWarnings)
            {
              goto end;
            }

            var warningIncrease = 1;
            var format = bucket.Format;

            // Group together the bottoms at the same PC
            if (bucket.Witness.Outcome == ProofOutcome.Bottom)
            {
              var pc = bucket.Witness.PC.PrimaryMethodLocation().PrimarySourceContext();
              if (pc != null)
              {
                if (!toSkip.Contains(pc)) 
                {

                  toSkip.Add(pc);

                  warningIncrease = pair.Value.Where(b => b.Witness.Outcome == ProofOutcome.Bottom && pc.Equals(b.Witness.PC.PrimaryMethodLocation().PrimarySourceContext())).Count();

                  if (warningIncrease > 1)
                  {
                    format = string.Format("{0} ({1} more unreached assertion(s) at the same location)", format, warningIncrease);
                  }
                }
                else
                {// We already emitted a warning for this source location, so we give up
                  continue;
                }
              }
            }

            emitted += warningIncrease;

            switch (bucket.CallKind)
            {
              case Bucket.Kind.EmitOutcome:
                base.EmitOutcome(bucket.Witness, format, bucket.Args);
                break;

              case Bucket.Kind.EmitOutcomeAndRelated:
                base.EmitOutcomeAndRelated(bucket.Witness, format, bucket.Args);
                break;

              default:
#if DEBUG
                Contract.Assert(false);
#endif
                // do nothing ... Should be unreachable
                break;
            }
          }
        }
      }

    end: ;
      EmitFinalStats();
    }

    private void EmitFinalStats()
    {
      if (finalStatsMessage != null)
      {
        base.FinalStatistic(this.finalStatsAssembly, this.finalStatsMessage);
        if (this.outcomeCount > this.maxWarnings)
        {
          base.FinalStatistic(this.finalStatsAssembly, "(Additional warnings not shown. Increase -maxWarnings to show.)");
        }
      }
    }

    #endregion

    #region Filtering Logic

    [ContractVerification(true)]
    [Pure]
    private bool HasGoodScore(double score)
    {
      switch (this.warningLevel())
      {
        case WarningLevelOptions.full:
          return true;

        case WarningLevelOptions.mediumlow:
          return score > this.scoresManager.MEDIUMLOWSCORE;

        case WarningLevelOptions.medium:
          return score > this.scoresManager.MEDIUMSCORE;

        case WarningLevelOptions.low:
          return score > this.scoresManager.LOWSCORE;

        default:
          // Should be unreachable
          Contract.Assume(false);
          return true;
      }
    }

    #endregion

    internal struct Bucket
    {
      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(witness != null);
        Contract.Invariant(format != null);
      }

      public enum Kind { EmitOutcome, EmitOutcomeAndRelated }

      readonly public Kind CallKind;
      readonly private Witness witness;
      readonly private string format;
      readonly public object[] Args;

      public Witness Witness 
      { 
        get 
        {
          Contract.Ensures(Contract.Result<Witness>() != null);

          return this.witness; 
        } 
      }

      public string Format
      {
        get
        {
          Contract.Ensures(Contract.Result<string>() != null);

          return this.format;
        }
      }

      public Bucket(Kind callkind, Witness witness, string format, object[] args)
      {
        Contract.Requires(witness != null);
        Contract.Requires(format != null);

        this.CallKind = callkind;
        this.witness = witness;
        this.format = format;
        this.Args = args;
      }
    }

    internal class InverseNaturalOrder : IComparer<double>
    {
      #region IComparer<double> Members

      public int Compare(double x, double y)
      {
        return x == y ? 0 : ((x < y) ? 1 : -1);
      }

      #endregion
    }
  }
}
