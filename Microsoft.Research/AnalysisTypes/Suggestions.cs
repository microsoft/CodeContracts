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
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.Research.CodeAnalysis
{

  public static class ClousotSuggestion
  {
    public class ExtraSuggestionInfo
    {
      static public ExtraSuggestionInfo None
      {
        get
        {
          Contract.Ensures(Contract.Result<ExtraSuggestionInfo>() != null);

          return new ExtraSuggestionInfo();
        }
      }

      static public ExtraSuggestionInfo ForObjectInvariant(string suggestedCode, string typeDocumentID)
      {
        Contract.Requires(suggestedCode != null);
        Contract.Requires(typeDocumentID != null);
        Contract.Ensures(Contract.Result<ExtraSuggestionInfo>() != null);

        var result = new ExtraSuggestionInfo();
        result.SuggestedCode = suggestedCode;
        result.TypeDocumentId = typeDocumentID;

        return result;
      }

      public string CalleeDocumentId;
      public string CalleeMemberKind;
      public string CalleeIsDeclaredInTheSameAssembly;
      public string TypeDocumentId;
      public string SuggestedCode;

      #region API
      public static ExtraSuggestionInfo FromStream(IEnumerable<string> enumerable)
      {
        Contract.Ensures(Contract.Result<ExtraSuggestionInfo>() != null);

        if (enumerable == null)
          return new ExtraSuggestionInfo();

        var fields = new List<string>();
        var values = new List<string>();

        var count = 0;
        foreach (var str in enumerable)
        {
          if (count % 2 == 0)
            fields.Add(str);
          else
            values.Add(str);
          count++;
        }
        if (fields.Count != values.Count)
        {
          return ExtraSuggestionInfo.None;
        }

        var freshObj = new ExtraSuggestionInfo();
        var type = typeof(ExtraSuggestionInfo);

        for (var i = 0; i < fields.Count; i++)
        {
          var fi = type.GetField(fields[i], PublicInstances());
          Contract.Assume(fi != null);
          fi.SetValue(freshObj, values[i]);
        }

        return freshObj;
      }

      public static ExtraSuggestionInfo FromStream(string str)
      {
        Contract.Ensures(Contract.Result<ExtraSuggestionInfo>() != null);
        if (string.IsNullOrEmpty(str))
          return FromStream((IEnumerable<string>) null);

        var deserializer = new XmlSerializer(typeof(string[]));
        var obj = deserializer.Deserialize(new StringReader(str));

        return FromStream(obj as string[]);
      }

      public IEnumerable<string> ToCollection()
      {
        Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);

        foreach(var fi in PublicFields())
        {
          yield return fi.Name;
          var value = fi.GetValue(this);
          yield return value != null ? value.ToString() : null;
        }
      }

      public string Serialize()
      {
        Contract.Ensures(Contract.Result<string>() != null);

        var serializer = new XmlSerializer(typeof(string[]));
        var str = new StringWriter();
        serializer.Serialize(str, this.ToCollection().ToArray());

        return str.ToString();
      }

      public IEnumerable<Tuple<string, string>> GetXml()
      {
        Contract.Ensures(Contract.Result<IEnumerable<Tuple<string, string>>>() != null);

        foreach(var fi in PublicFields())
        {
          var value = fi.GetValue(this);
          if(value != null)
          {
            yield return new Tuple<string, string>(fi.Name, value.ToString());
          }
        }
      }

      [Pure]
      static private BindingFlags PublicInstances()
      {
        return BindingFlags.Public | BindingFlags.Instance;
      }

      [Pure]
      static private IEnumerable<FieldInfo> PublicFields()
      {
        return typeof(ExtraSuggestionInfo).GetFields(PublicInstances());
      }

      #endregion
    }

    public enum Kind : byte
    {
      AbstractState,
      AssertToContract,
      Action,
      AssumeOnEntry,
      AssumeOnCallee,
      AssumptionCanBeProven,
      CalleeInvariant,
      CodeFix,
      Ensures,
      EnsuresExtractedMethod,
      EnsuresNecessary,
      InterMethodTrace,
      ObjectInvariant,
      ReadonlyField,
      Requires,
      RequiresCanBeProven,
      RequiresOnBaseMethod,
      RequiresIsRedundant,
      RequiresExtractedMethod,
      SuppressionWarning,
      UnusedSuppressWarning,
      NotInteresting,
    }

    public enum Ensures 
    {
      None,
      NonNullReturnOnly,
      NonNull,
      All
    }

    public static string Message(this Kind suggestion, string extra = null)
    {
      Contract.Ensures(Contract.Result<System.String>() != null);
      switch(suggestion)
      {
        case Kind.AbstractState:
          return "Suggested abstract state";

        case Kind.Action:
          return "action";

        case Kind.AssertToContract:
          return "code improvement";

        case Kind.AssumeOnCallee:
        case Kind.AssumeOnEntry:
          return "assume";

        case Kind.AssumptionCanBeProven:
          return "assumption";

        case Kind.CalleeInvariant:
          return "callee invariant";

        case Kind.CodeFix:
          return "Code fix";
          
        case Kind.Ensures:
          return "ensures";

        case Kind.EnsuresExtractedMethod:
          return "Postcondition for the extracted method ";

        case Kind.EnsuresNecessary:
          return string.Format("ensures for member {0}", extra);

        case Kind.InterMethodTrace:
          return "inter-method trace";

        case Kind.NotInteresting:
          return "";

        case Kind.ObjectInvariant:
          return "invariant";

        case Kind.ReadonlyField:
          return "readonly field";

        case Kind.Requires:
        case Kind.RequiresCanBeProven:
        case Kind.RequiresIsRedundant:
          return "requires";

        case Kind.RequiresOnBaseMethod:
          return string.Format("requires for base method {0}", extra);

        case Kind.RequiresExtractedMethod:
          return "Precondition for the extracted method ";

        case Kind.SuppressionWarning:
          return "suppression ";

        case Kind.UnusedSuppressWarning:
          return "Unused SuppressWarning";

        default:
          Contract.Assert(false);
          return "unknown";
      }
    }

  }
}
