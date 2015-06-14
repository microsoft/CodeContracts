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
  [ContractVerification(true)]
  public class SimpleOverriddenMethodPreconditionDispatcher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
    : IOverriddenMethodPreconditionsDispatcher
  {

    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.mdDecoder != null);
      Contract.Invariant(this.definitionsToExpressions != null);
      Contract.Invariant(this.inferenceSitesToExpressions != null);
    }


    #region Internal State

    private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
    private readonly Dictionary<Method, List<BoxedExpression>> definitionsToExpressions;
    private readonly Dictionary<Method, List<BoxedExpression>> inferenceSitesToExpressions;

    #endregion

    public Method CurrentMethod { get; set; }

    public BoxedExpressionTransformer<Void> BoxedExpressionTransformer { get; set; }

    public SimpleOverriddenMethodPreconditionDispatcher(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      Contract.Requires(mdDecoder != null);

      this.mdDecoder = mdDecoder;
      this.definitionsToExpressions = new Dictionary<Method, List<BoxedExpression>>();
      this.inferenceSitesToExpressions = new Dictionary<Method, List<BoxedExpression>>();
    }

    public void AddPotentialPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> preconditions)
    {
      var newPre = preconditions.Distinct().ToList().ConvertAll(exp => this.BoxedExpressionTransformer.Visit(exp, new Void()));

      this.inferenceSitesToExpressions.AddOrUpdate(this.CurrentMethod, newPre);

      foreach (var m in this.mdDecoder.OverriddenAndImplementedMethods(CurrentMethod))
      {
        this.definitionsToExpressions.AddOrUpdate(m, newPre);
      }
    }

    public int SuggestPotentialPreconditions(IOutput output)
    {
      var i = 0;
      foreach (var pair in this.definitionsToExpressions)
      {
        var suggestionStr = "Contract.Requires({0})";
        SuggestPotentialPreconditions(output, APC.Dummy, suggestionStr, pair.Key, pair.Value.Distinct());

        i += pair.Value.Count;
      }

      Contract.Assert(i >= 0);

      return i;
    }

    public int SuggestPotentialPreconditionsFor(IOutput output, APC pc, Method m)
    {
      if(output == null)
      {
        return 0;
      }

      List<BoxedExpression> expressions;

      if (this.inferenceSitesToExpressions.TryGetValue(m, out expressions))
      {
        Contract.Assume(expressions != null);

        var currentType = this.mdDecoder.FullName(this.mdDecoder.DeclaringType(m));
        var methodName = this.mdDecoder.FullName(m);
        var requiresStr = string.Format("Contract.Requires(!(this is {0}) || {1})", currentType, "{0}");
        var baseMethod = this.mdDecoder.OverriddenAndImplementedMethods(m).First();

        this.SuggestPotentialPreconditions(output, pc, requiresStr, baseMethod, expressions.Distinct());

        return expressions.Count;
      }

      return 0;
    }

    private int SuggestPotentialPreconditions(IOutput output, APC pc, string requiresStr, Method baseMethod, IEnumerable<BoxedExpression> conditions)
    {
      Contract.Requires(output != null);
      Contract.Requires(requiresStr != null);

      var i = 0;
      var baseMethodName = this.mdDecoder.FullName(baseMethod);
//      var suggestion = string.Format("requires for base method {0}", baseMethodName);
      var suggestion = ClousotSuggestion.Kind.RequiresOnBaseMethod.Message(baseMethodName);

      foreach (var p in conditions)
      {
        var pre = string.Format(requiresStr, p.ToString());
        output.Suggestion(ClousotSuggestion.Kind.RequiresOnBaseMethod, suggestion, pc, pre, null, ClousotSuggestion.ExtraSuggestionInfo.None);
        i++;
      }

      return i;
    }


  }
}
