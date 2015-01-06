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
using Microsoft.Research.CodeAnalysis.Inference;

namespace Microsoft.Research.CodeAnalysis
{
  [ContractVerification(true)]
  public class SimpleObjectInvariantDispatcher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    : SimpleDispatcherBase<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    , IObjectInvariantDispatcher
    where LogOptions : IFrameworkLogOptions
    where Variable : IEquatable<Variable>
    where Expression : IEquatable<Expression>
    where Type : IEquatable<Type>
  {
    #region Invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.objectInvariants != null);
    }

    #endregion

    #region State

    protected override string ContractTemplate { get { return "Contract.Invariant({0});"; } }

    readonly private InferenceDB objectInvariants;
    
    #endregion

    #region Constructor

    public SimpleObjectInvariantDispatcher(
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
      IOutputResults output, bool allowDisjunctivePreconditions)
      : base(mdriver, output, allowDisjunctivePreconditions)
    {
      Contract.Requires(mdriver != null);
      Contract.Requires(output != null);

      this.objectInvariants = new InferenceDB(exp => exp.Simplify(mdriver.MetaDataDecoder), exp => true);
    }

    #endregion

    #region IPreconditionDispatcher Members


    private bool CheckObjectInvariant(BoxedExpression simplified, ProofOutcome originalOutcome, out ProofOutcome outcome)
    {
      outcome = originalOutcome;
      return true;
    }

    public ProofOutcome AddObjectInvariants(ProofObligation obl, IEnumerable<BoxedExpression> objectInvariants, ProofOutcome originalOutcome)
    {
      return this.GenericAddPreconditions(obl, objectInvariants, originalOutcome,
        this.objectInvariants, this.CheckObjectInvariant);
    }

    public IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GenerateObjectInvariants()
    {
      var md = this.mdriver;
      var mdd = md.MetaDataDecoder;
 
      // No suggestions for structs
      if(mdd.IsStruct(mdd.DeclaringType(md.Context.MethodContext.CurrentMethod)))
      {
        return new KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>[0];
      }

      return this.objectInvariants.GenerateConditions();
    }

    public int SuggestObjectInvariants()
    {
      var md = this.mdriver;
      var pc = md.CFG.GetPCForMethodEntry();
      var typeId = md.MetaDataDecoder.DocumentationId(md.MetaDataDecoder.DeclaringType(md.CurrentMethod));
      Contract.Assume(typeId != null);
      var count = 0;
      foreach (var pobjinv in this.GenerateObjectInvariants())
      {
        var objinv = pobjinv.Key;
        var provenance = pobjinv.Value;
        var contract = MakeconditionString(objinv.ToString());
        var extraInfo = ClousotSuggestion.ExtraSuggestionInfo.ForObjectInvariant(contract, typeId);
        
        output.Suggestion(ClousotSuggestion.Kind.ObjectInvariant, ClousotSuggestion.Kind.ObjectInvariant.Message(),
          pc, contract, this.objectInvariants.CausesFor(objinv), extraInfo);
        count++;
      }

      return count;
    }

    public int PropagateObjectInvariants(bool asInvariant)
    {
      var count = 0;
      foreach (var pobjinv in this.GenerateObjectInvariants())
      {
        var objinv = pobjinv.Key;
        var provenance = pobjinv.Value;
        if (asInvariant)
        {
          this.mdriver.AddObjectInvariant(objinv, provenance);
          count++;
        }
        else
        {
          this.mdriver.AddEntryAssume(objinv, provenance);
        }
      }

      return count;
    }

    #endregion
  }
}
