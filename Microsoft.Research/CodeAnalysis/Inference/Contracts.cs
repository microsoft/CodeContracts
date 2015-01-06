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
using Microsoft.Research.CodeAnalysis.Expressions;

namespace Microsoft.Research.CodeAnalysis
{
  [ContractVerification(true)]
  public class ContractInferenceManager
  {
    #region Object Invariant

    [ContractInvariantMethod]
    private void ObjectInvariantMethod()
    {
      Contract.Invariant(this.PreCondition != null);
      Contract.Invariant(this.Assumptions != null);
      Contract.Invariant(this.OverriddenMethodPreconditionDispatcher != null);
      Contract.Invariant(this.Output != null);
    }
    
    #endregion

    #region State

    readonly public bool CanAddPreconditions;
    readonly public PreconditionInferenceManager PreCondition;
    readonly public IObjectInvariantDispatcher ObjectInvariant;
    readonly public IPostconditionDispatcher PostCondition;
    readonly public IAssumeDispatcher Assumptions;
    readonly public ICodeFixesManager CodeFixesManager;
    readonly public IOverriddenMethodPreconditionsDispatcher OverriddenMethodPreconditionDispatcher;

    readonly public IOutput Output;

    #endregion

    #region Constructor

    public ContractInferenceManager(bool CanAddPreconditions, IOverriddenMethodPreconditionsDispatcher overriddenMethodPreconditionsDispatcher, PreconditionInferenceManager precondition, IObjectInvariantDispatcher invariants, IPostconditionDispatcher postcondition, IAssumeDispatcher assumptions, ICodeFixesManager codefixesManager, IOutput output)
    {
      Contract.Requires(overriddenMethodPreconditionsDispatcher != null);
      Contract.Requires(precondition != null);
      Contract.Requires(invariants != null);
      Contract.Requires(postcondition  != null);
      Contract.Requires(assumptions != null);
      Contract.Requires(codefixesManager != null);
      Contract.Requires(output != null);

      this.CanAddPreconditions = CanAddPreconditions;
      this.OverriddenMethodPreconditionDispatcher = overriddenMethodPreconditionsDispatcher;
      this.PreCondition = precondition;
      this.ObjectInvariant = invariants;
      this.PostCondition = postcondition;
      this.Assumptions = assumptions;
      this.CodeFixesManager = codefixesManager;
      this.Output = output;
    }

    #endregion

    #region AddPreconditionOrAssume

    public ProofOutcome AddPreconditionOrAssume(ProofObligation obl, IEnumerable<BoxedExpression> conditions, ProofOutcome outcome = ProofOutcome.Top)
    {
      Contract.Requires(conditions != null);
      Contract.Requires(obl != null);

      if (this.CanAddPreconditions)
      {
        Contract.Assume(this.PreCondition.Dispatch != null);
        return this.PreCondition.Dispatch.AddPreconditions(obl, conditions, outcome);
      }
      else
      {
        this.Assumptions.AddEntryAssumes(obl, conditions);
        this.OverriddenMethodPreconditionDispatcher.AddPotentialPreconditions(obl, conditions);

        return outcome;
      }
    }

    #endregion

    #region Questions

    public bool SuggestInferredRequires
    {
      get
      {
        return this.Output.LogOptions.SuggestRequires;
      }
    }

    public bool SuggestInferredEnsures(bool isProperty)
    {
      return this.Output.LogOptions.SuggestEnsures(isProperty);
    }

    public bool PropagateInferredRequires(bool isProperty)
    {
      return this.Output.LogOptions.PropagateInferredRequires(isProperty);
    }

    public bool PropagateInferredEnsures(bool isProperty)
    {
      return this.Output.LogOptions.PropagateInferredEnsures(isProperty);
    }

    public bool InferPreconditionsFromPostconditions
    {
      get
      {
        return this.Output.LogOptions.InferPreconditionsFromPostconditions;
      }
    }

    public bool CheckFalsePostconditions
    {
      get
      {
        return this.Output.LogOptions.CheckFalsePostconditions;
      }
    }

    #endregion
  }
}
   