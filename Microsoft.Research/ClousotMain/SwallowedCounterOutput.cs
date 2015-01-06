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
using System.Diagnostics.Contracts;
using System.Collections.Generic;

namespace Microsoft.Research.CodeAnalysis
{
  [ContractVerification(true)]
  class SwallowedCounterOutput<Method, Assembly> : DerivableOutputFullResults<Method, Assembly>
  {
    #region Object Invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.swallowed != null);
    }

    #endregion

    private SwallowedBuckets swallowed;

    public override int SwallowedMessagesCount(ProofOutcome outcome)
    { 
      return this.swallowed.GetCounter(outcome);  
    }

    public SwallowedCounterOutput(IOutputFullResults<Method, Assembly> inner)
      : base(inner)
    {
      Contract.Requires(inner != null);

      this.swallowed = new SwallowedBuckets();
    }

    #region IOutputResults Members

    public override bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      var result = base.EmitOutcome(witness, format, args);

      if (!result)
      {
        this.swallowed.UpdateCounter(witness.Outcome);
      }

      return result;
    }

    public override bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      var result = base.EmitOutcomeAndRelated(witness, format, args);

      if (!result)
      {
        this.swallowed.UpdateCounter(witness.Outcome);
      }

      return result;
    }

    #endregion
  }
}
