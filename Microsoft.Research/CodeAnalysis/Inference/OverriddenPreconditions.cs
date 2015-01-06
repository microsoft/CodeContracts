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
  /// <summary>
  /// The manager for candidate postconditions
  /// </summary>
  [ContractClass(typeof(IOverriddenMethodPreconditionsDispatcherContracts))]
  public interface IOverriddenMethodPreconditionsDispatcher
  {
    /// <summary>
    /// Add the assume to the list of current assumptions
    /// </summary>
    void AddPotentialPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> preconditions);

    /// <summary>
    /// Suggest the assumptions.
    /// Returns how many assumptions have been suggested
    /// </summary>
    int SuggestPotentialPreconditions(IOutput output);
  }

  #region Contracts

  [ContractClassFor(typeof(IOverriddenMethodPreconditionsDispatcher))]
  abstract class IOverriddenMethodPreconditionsDispatcherContracts : IOverriddenMethodPreconditionsDispatcher
  {
    public void AddPotentialPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> assumes)
    {
      Contract.Requires(obl != null);
      Contract.Requires(assumes != null);
    }

    public int SuggestPotentialPreconditions(IOutput output)
    {
      Contract.Requires(output != null);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return 0;
    }
  }

  #endregion

  [ContractVerification(true)]
  public class OverriddenMethodPreconditionsDispatcherProfiler
    : IOverriddenMethodPreconditionsDispatcher
  {
    #region Object Invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.inner != null);
    }

    #endregion

    #region Statics

    [ThreadStatic]
    static private int generated;
    [ThreadStatic]
    static private int retained;

    #endregion

    #region State

    private readonly IOverriddenMethodPreconditionsDispatcher inner;
    private bool profilingAlreadyCollected;

    #endregion

    #region Constructor

    public OverriddenMethodPreconditionsDispatcherProfiler(IOverriddenMethodPreconditionsDispatcher dispatcher)
    {
      Contract.Requires(dispatcher != null);

      this.inner = dispatcher;
      this.profilingAlreadyCollected = false;
    }

    #endregion

    #region Implementation of IPostCondtionDispatcher
    public void AddPotentialPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> assumes)
    {
      generated += assumes.Count();
      this.inner.AddPotentialPreconditions(obl, assumes);
    }

    public int SuggestPotentialPreconditions(IOutput output)
    {
      return RecordProfilingInformation(this.inner.SuggestPotentialPreconditions(output));
    }

    #endregion

    #region Dumping
    public static void DumpStatistics(IOutput output)
    {
      Contract.Requires(output != null);

      if (generated != 0)
      {
        output.WriteLine("Detected {0} preconditions for base methods to suggest", generated);
      }
    }

    #endregion

    #region Profiling

    private int RecordProfilingInformation(int howMany)
    {
      Contract.Requires(howMany >= 0);
      Contract.Ensures(retained >= Contract.OldValue(retained));
      Contract.Ensures(Contract.Result<int>() == howMany);

      if (!this.profilingAlreadyCollected)
      {
        retained += howMany;
        this.profilingAlreadyCollected = true;
      }

      return howMany;
    }

    #endregion
  }
}
