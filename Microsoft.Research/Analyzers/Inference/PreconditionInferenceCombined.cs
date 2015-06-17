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
using System.Collections.ObjectModel;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public class PreconditionInferenceCombined 
    : IPreconditionInference
  {
    #region Object invariant
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.inferencers != null);
    }
    #endregion

    #region state
    readonly private ReadOnlyCollection<IPreconditionInference> inferencers;
    #endregion

    #region Constructor
    public PreconditionInferenceCombined(List<IPreconditionInference> inferencers)
    {
      Contract.Requires(inferencers != null);
      Contract.Requires(inferencers.Count > 0);

      this.inferencers = inferencers.AsReadOnly();
    }
    #endregion

    #region Implementation
    public bool TryInferConditions(ProofObligation obl, ICodeFixesManager codefixesManager, out InferredConditions preConditions)
    {
      preConditions = null;
      foreach (var inferencer in this.inferencers)
      {
        if (inferencer == null)
          continue;

        InferredConditions tmp;
        if (inferencer.TryInferConditions(obl, codefixesManager, out tmp))
        {
          Contract.Assert(tmp != null);
          if (preConditions == null)
          {
            preConditions = tmp;
          }
          else
          {
            preConditions.AddRange(tmp);
          }
        }
      }

      return preConditions != null;
    }

    public bool ShouldAddAssumeFalse
    {
      get { return inferencers.Any(inferencer => inferencer.ShouldAddAssumeFalse); }
    }

    #endregion

  }
}
