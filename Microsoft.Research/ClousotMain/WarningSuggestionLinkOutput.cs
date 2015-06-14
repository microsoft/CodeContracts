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
  public class WarningSuggestionLinkOutput<Method, Assembly> : DerivableOutputFullResults<Method, Assembly>
  {
    #region state

    private readonly SuppressWarningsManager<Method> suppressWarningManager;
    private Dictionary<uint, Info> DB;

    #endregion

    public WarningSuggestionLinkOutput(IOutputFullResults<Method, Assembly> inner)
      : base(inner)
    {
      Contract.Requires(inner != null);

      this.suppressWarningManager = new SuppressWarningsManager<Method>();
      this.DB = null;
    }

    public override bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      if (witness.SourceProofObligationID.HasValue)
      {
        this.DB[witness.SourceProofObligationID.Value] = new Info(false, new Tuple<Witness,string,object[]>(witness, format, args));

        return true; // ???
      }
      else
      {
        return base.EmitOutcome(witness, format, args);
      }
    }

    public override bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      if (witness.SourceProofObligationID.HasValue)
      {
        this.DB[witness.SourceProofObligationID.Value] = new Info(true, new Tuple<Witness, string, object[]>(witness, format, args));

        return true; // ???
      }
      else
      {
        return base.EmitOutcomeAndRelated(witness, format, args);
      }
    }

    public override void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      if (causes == null)
      {
        base.Suggestion(type, kind, pc, suggestion, causes, extraInfo);
      }
      else
      {
        var suggestionTuple = new Tuple<string, APC, string, ClousotSuggestion.ExtraSuggestionInfo>(kind, pc, suggestion, extraInfo);
        foreach (var id in causes)
        {
          Info info;
          if (this.DB.TryGetValue(id, out info))
          {
            // Works with side effects
            info.Add(suggestionTuple);
          }
        }
      }
    }

    public override void StartMethod(Method method)
    {
      this.DB = new Dictionary<uint, Info>();
      base.StartMethod(method);
    }

    public override void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC = false)
    {
      foreach (var pair in this.DB)
      {
        var info = pair.Value;
        var oblID = info.Msg.Item1.SourceProofObligationID.HasValue
          ? new List<uint>() { info.Msg.Item1.SourceProofObligationID.Value }
          : null;

        bool ok;

        // 1. Emit the outcome
        if (info.IsOutcomeAndRelated)
        {
          ok = base.EmitOutcomeAndRelated(info.Msg.Item1, info.Msg.Item2, info.Msg.Item3);
        }
        else
        {
          ok = base.EmitOutcomeAndRelated(info.Msg.Item1, info.Msg.Item2, info.Msg.Item3);
        }

        if (!ok)
        {
          continue;
        }

        // 2. Emit the suppression string
        base.Suggestion(ClousotSuggestion.Kind.SuppressionWarning, ClousotSuggestion.Kind.SuppressionWarning.Message(), 
          methodEntry,
          this.suppressWarningManager.ComputeSuppressMessage(info.Msg.Item1), oblID, ClousotSuggestion.ExtraSuggestionInfo.None);

        // 3. Emit the fixes
        foreach (var suggestion in info.Suggestions)
        {
          base.Suggestion(ClousotSuggestion.Kind.Action, ClousotSuggestion.Kind.Action.Message(), 
            suggestion.Item2, suggestion.Item3, oblID, suggestion.Item4);
        }
      }
      
      this.DB = null;
      base.EndMethod(methodEntry, ignoreMethodEntryAPC);
    }

    public override string Name
    {
      get { return "WarningSuggestionLinkOutput"; }
    }

    class Info
    {
      public readonly Tuple<Witness, string, object[]> Msg;
      public readonly bool IsOutcomeAndRelated;
      public readonly List<Tuple<string, APC, string, ClousotSuggestion.ExtraSuggestionInfo>> Suggestions;

      public Info(bool IsOutcomeAndRelated, Tuple<Witness, string, object[]> Msg)
        : this(IsOutcomeAndRelated, Msg, new List<Tuple<string, APC, string, ClousotSuggestion.ExtraSuggestionInfo>>())
      {
      }

      private Info(bool IsOutcomeAndRelated, Tuple<Witness, string, object[]> Msg, List<Tuple<string, APC, string, ClousotSuggestion.ExtraSuggestionInfo>> Suggestions)
      {
        this.IsOutcomeAndRelated = IsOutcomeAndRelated;
        this.Msg = Msg;
        this.Suggestions = Suggestions;
      }

      public void Add(Tuple<string, APC, string, ClousotSuggestion.ExtraSuggestionInfo> suggestion)
      {
        this.Suggestions.Add(suggestion);
      }
    }
  }
}
