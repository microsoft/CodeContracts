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
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  partial class OutcomeContextEdgeModel
  {
  }

  partial class SuggestionContextEdgeModel
  {
  }

  partial class OutcomeModel
  {

    public ProofOutcome ProofOutcome { get { return (ProofOutcome)this.ProofOutcomeByte; } set { this.ProofOutcomeByte = (byte)value; } }

    public WarningType WarningType { get { return (WarningType)this.WarningTypeByte; } set { this.WarningTypeByte = (byte)value; } }

  }

  partial class OutcomeContextModel
  {
    public WarningContext WarningContext
    {
      get { return new WarningContext((WarningContext.ContextType)this.TypeByte, this.AssociatedInfo); }
      set { this.TypeByte = (byte)value.Type; this.AssociatedInfo = value.AssociatedInfo; }
    }
  }

  partial class SuggestionModel
  {
  }

  partial class MethodModel
  {
    public AnalysisStatistics Statistics { 
      get {
        var res = new AnalysisStatistics {
          Bottom = (uint)this.StatsBottom,
          Top = (uint)this.StatsTop,
          True = (uint)this.StatsTrue,
          False = (uint)this.StatsFalse,
          Total = (uint)(this.StatsBottom + this.StatsTop + this.StatsTrue + this.StatsFalse)
        };
        return res;
      }
      set {
        this.StatsBottom = (int)value.Bottom;
        this.StatsFalse = (int)value.False;
        this.StatsTrue = (int)value.True;
        this.StatsTop = (int)value.Top;
      }
    }

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
        this.SwallowedTop = value.GetCounter(ProofOutcome.Top);
        this.SwallowedBottom = value.GetCounter(ProofOutcome.Bottom);
        this.SwallowedTrue = value.GetCounter(ProofOutcome.True);
        this.SwallowedFalse = value.GetCounter(ProofOutcome.False);
      }
    }
  }




  partial class MethodModel
  {
    #region pre/post conditions setter/serialization
    #endregion
  }

}