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
  public class ExternallyVisibleMethodsOnlyOutput<Method, Assembly> : DerivableOutputFullResults<Method, Assembly>
  {
    #region Object invariant
    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.IsVisibleOutsideTheAssembly != null);
    }
    #endregion

    readonly private Predicate<Method> IsVisibleOutsideTheAssembly;
    private bool isMethodOk;

    public ExternallyVisibleMethodsOnlyOutput(IOutputFullResults<Method, Assembly> inner, Predicate<Method> IsVisibleOutsideTheAssembly)
      : base(inner)
    {
      Contract.Requires(inner != null);
      Contract.Requires(IsVisibleOutsideTheAssembly != null);

      this.IsVisibleOutsideTheAssembly = IsVisibleOutsideTheAssembly;
      this.isMethodOk = false;
    }

    public override void StartMethod(Method method)
    {
      base.StartMethod(method);
      this.isMethodOk = IsVisibleOutsideTheAssembly(method);
    }

    public override string Name
    {
      get 
      {
        return "Showing only externally visible methods";
      }
    }

    public override bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      if (this.isMethodOk)
      {
        return base.EmitOutcome(witness, format, args);
      }
      return false;
    }

    public override bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      if (this.isMethodOk)
      {
        return base.EmitOutcomeAndRelated(witness, format, args);
      }
      return false;
    }

    public override bool IsMasked(Witness witness)
    {
      if (!this.isMethodOk)
        return true;

      return base.IsMasked(witness);
    }

    public override void WriteLine(string format, params object[] args)
    {
      if (this.isMethodOk)
      {
        base.WriteLine(format, args);
      }
    }

    public override void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      if (this.isMethodOk)
      {
        base.Suggestion(type, kind, pc, suggestion, causes, extraInfo);
      }
    }

    public override void EmitError(System.CodeDom.Compiler.CompilerError error)
    {
      if (this.isMethodOk)
      {
        base.EmitError(error);
      }
    }
  }
}
