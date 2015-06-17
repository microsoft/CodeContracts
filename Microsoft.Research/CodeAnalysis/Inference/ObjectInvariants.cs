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
  [ContractClass(typeof(IObjectInvarariantDispatcherContracts))]
  public interface IObjectInvariantDispatcher
  {
    /// <summary>
    /// Add the object invariants to the list of current object invariants
    /// </summary>
    ProofOutcome AddObjectInvariants(ProofObligation obl, IEnumerable<BoxedExpression> objectInvariants, ProofOutcome originalOutcome);

    /// <summary>
    /// Returns the list of object invariants for this method
    /// </summary>
    IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GenerateObjectInvariants();

    /// <summary>
    /// Suggest the object invariants.
    /// Returns how many object invariants have been suggested
    /// </summary>
    int SuggestObjectInvariants();

    /// <summary>
    /// Infer the object invariants.
    /// Returns how many object invariants have been propagated to the callers
    /// </summary>
    /// <param name="asInvariant">if true, install as an invariant, otherwise as an assume</param>
    int PropagateObjectInvariants(bool asInvariant);
  }

  #region Contracts
 
  [ContractClassFor(typeof(IObjectInvariantDispatcher))]
  abstract class IObjectInvarariantDispatcherContracts : IObjectInvariantDispatcher
  {
    public ProofOutcome AddObjectInvariants(ProofObligation obl, IEnumerable<BoxedExpression> objectInvariants, ProofOutcome originalOutcome)
    {
      Contract.Requires(obl != null);
      Contract.Requires(objectInvariants != null);

      return ProofOutcome.Top;
    }

    public IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GenerateObjectInvariants()
    {
      Contract.Ensures(Contract.Result<IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>>>() != null);

      return null;
    }

    public int SuggestObjectInvariants()
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      return 0;
    }

    public int PropagateObjectInvariants(bool asInvariant)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      return 0;
    }
  }

  #endregion

  public class ObjectInvariantDispatcherProfiler : IObjectInvariantDispatcher
  {

    #region statics

    [ThreadStatic]
    static int retained;
    [ThreadStatic]
    static int generated;

    #endregion

    #region Private state

    readonly IObjectInvariantDispatcher inner;
    bool statsEmitted;

    #endregion

    #region Constructor

    public ObjectInvariantDispatcherProfiler(IObjectInvariantDispatcher inner)
    {
      Contract.Requires(inner != null);

      this.inner = inner;
      this.statsEmitted = false;
    }
    
    #endregion

    #region Implementation
    public ProofOutcome AddObjectInvariants(ProofObligation obl, IEnumerable<BoxedExpression> objectInvariants, ProofOutcome originalOutcome)
    {
      generated += objectInvariants.Count();
      return this.inner.AddObjectInvariants(obl, objectInvariants, originalOutcome);
    }

    public IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GenerateObjectInvariants()
    {
      var result = this.inner.GenerateObjectInvariants();
      AddStatisticsForObjectInvariants(result.Count());

      return result;
    }

    public int SuggestObjectInvariants()
    {
      return AddStatisticsForObjectInvariants(this.inner.SuggestObjectInvariants());
    }

    public int PropagateObjectInvariants(bool asInvariant)
    {
      return AddStatisticsForObjectInvariants(this.inner.PropagateObjectInvariants(asInvariant));
    }

    #endregion

    #region Dumping
    public static void DumpStatistics(IOutput output)
    {
      Contract.Requires(output != null);

      output.WriteLine("Inferred {0} object invariants", generated);
      output.WriteLine("Retained {0} object invariants after filtering", retained);
    }

    #endregion

    #region Private

    private int AddStatisticsForObjectInvariants(int p)
    {
      if (!this.statsEmitted)
      {
        retained += p;
        this.statsEmitted = true;
      }

      return p;
    }
    #endregion
  }
}