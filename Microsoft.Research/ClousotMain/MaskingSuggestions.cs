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
  public class MaskingSuggestions<Method, Assembly> : DerivableOutputFullResults<Method, Assembly>
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.methodName != null);
    }

    private readonly string methodName;
    private bool ok;

    public MaskingSuggestions(string methodName, IOutputFullResults<Method, Assembly> inner)
      : base(inner)
    {
      Contract.Requires(methodName != null);
      Contract.Requires(inner != null);

      this.methodName = methodName;
      this.ok = false;
    }

    public override void StartMethod(Method method)
    {
      if (method.ToString().Contains(this.methodName))
      {
        ok = true;
        base.StartMethod(method);
      }
    }

    public override void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC = false)
    {
      if (ok)
      {
        base.EndMethod(methodEntry, ignoreMethodEntryAPC);
        ok = false;
      }
    }

    public override int SwallowedMessagesCount(ProofOutcome outcome)
    {
      if (ok)
      {
        return base.SwallowedMessagesCount(outcome);
      }
      return 0;
    }

    public override string Name
    {
      get { return "Masking Suggestions";  }
    }

    public override bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      if (ok)
      {
        base.EmitOutcome(witness, format, args);
      }
      return false;
    }

    public override bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      if (ok)
      {
        base.EmitOutcomeAndRelated(witness, format, args);
      }
      return false;
    }

    public override void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      if (ok)
      {
        base.Suggestion(type, kind, pc, suggestion, causes, extraInfo);
      }
    }

    public override void EmitError(System.CodeDom.Compiler.CompilerError error)
    {
      if (ok)
      {
        base.EmitError(error);
      }
    }
  }
}
