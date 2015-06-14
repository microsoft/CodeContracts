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
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class Analyzers
  {
    abstract public class ValueAnalysisOptions<Options>
      : OptionParsing, IValueAnalysisOptions
      where Options : ValueAnalysisOptions<Options>
    {
      readonly protected ILogOptions logoptions;      

      public ValueAnalysisOptions(ILogOptions logoptions)
      {
        this.logoptions = logoptions;

        InitOptions();
      }

      private void InitOptions()
      {
        this.type = DomainKind.PentagonsKarrLeq;
        this.reduction = ReductionAlgorithm.Simplex;
        this.ch = false;
        this.infOct = false;
        this.mpw = true;
        this.tp = false;
        this.diseq = true;
        this.noObl = false;
        //this.precisionlevel = 2;
      }

      #region Options (public fields)

      [OptionWitness]
      public DomainKind type;

      [OptionWitness]
      [OptionDescription("Reduction algorithm used by subpolyhedra")]
      public ReductionAlgorithm reduction;

      [OptionWitness]
      [OptionDescription("Max number of pair of equalities that can be propagated by karr")]
      public int maxeqpairs = 25;
      
      [OptionWitness]
      [OptionDescription("SubPolyhedra only : use 2D convex hulls to infer constraints")]
      public bool ch;

      [OptionWitness]
      [OptionDescription("SubPolyhedra only : infer octagonal constraints")]
      public bool infOct;

      [OptionWitness]
      [OptionDescription("Subpolyhedra only: threshold to skip equalities inference in renaming")]
      public int renamingThreshold = 50;

      [OptionWitness]
      [OptionDescription("Use widening with thresholds")]
      public bool mpw;

      [OptionWitness]
      [OptionDescription("Use trace partitioning")]
      public bool tp;

      [OptionWitness]
      [OptionDescription("Track Numerical Disequalities")]
      public bool diseq;

      [OptionWitness]
      [OptionValueOverridesFamily]
      [OptionDescription("No proof obligations for bounds")]
      public bool noObl = false;


#if DEBUG
      [OptionWitness]
      [OptionDescription("For regression only: Throw a fixpoint timeout exception")]
      public bool crashWithTimeout = false;
#endif

      /*
      [OptionWitness]
      [OptionDescription("0 - low, 1 - medium, 2 - high, ..")]
      [OptionFor("precisionleveloptions")]
      public int precisionlevel;
      
      static public readonly string[] precisionleveloptions = new string[] 
        {
          "type=Pentagons",
          "type=PentagonsKarrLeq",
          "type=subpolyhedra reduction=none",
          "type=subpolyhedra reduction=fast",
          "type=subpolyhedra reduction=fast infoct",
          "type=subpolyhedra reduction=simplex infoct ch"
        };
      */
      #endregion

      public DomainKind Type { get { return this.type; } }

      public int ClosurePairs { get { return this.maxeqpairs; } }

      public int SubpolyhedraRenamingThreshold { get { return this.renamingThreshold; } }

      public bool Use2DConvexHull { get { return this.ch; } }
      public bool InferOctagonConstraints { get { return this.infOct; } }
      public bool UseTracePartitioning { get { return this.tp; } }
      public bool TrackDisequalities { get { return this.diseq; } }
      public bool TracePartitionAnalysis { get { return this.logoptions.TracePartitionAnalysis; } }
      public bool TraceNumericalAnalysis { get { return this.logoptions.TraceNumericalAnalysis; } }
      public bool TraceChecks { get { return this.logoptions.TraceChecks; } }

      public bool UseMorePreciseWidening { get { return this.mpw; } }
      public ILogOptions LogOptions
      {
        get
        {
          Contract.Ensures(Contract.Result<ILogOptions>() != null);
          return this.logoptions;
        }
      }
      public bool NoProofObligations { get { return this.noObl; } }
      public ReductionAlgorithm Algorithm { get { return this.reduction; } }

#if DEBUG
      public bool CrashWithTimeOut { get { return this.crashWithTimeout; } }
#endif
      internal void ForceValueOfOverriddenArguments(List<Options> list)
      {
        if (list.Count == 0)
          return;

        var dominant = list[0];

        foreach (var f in this.GetType().GetFields())
        {
          foreach (var a in f.GetCustomAttributes(true))
          {
            if (a is OptionValueOverridesFamily)
            {
              var value = f.GetValue(dominant);
              // Overwrites whatever value is there
              f.SetValue(this, value);
            }
          }
        }
      }


      protected override bool ParseUnknown(string option, string[] args, ref int index, string optionEqualsArgument)
      {
        return false;
      }

      protected override bool UseDashOptionPrefix
      {
        get
        {
          return false;
        }
      }

    }
  }
}
