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

namespace Microsoft.Research.CodeAnalysis
{
  public static class StringConstants
  {
    public const string ClousotWait = "wait";
    public const string Cloudot = "cloudot";
    public const string DoNotUseCloudot = "+cloudot";

    public const string StringSeparator = "$%^";

    public static class Contract
    {
      public const string ContractsNamespace = "System.Diagnostics.Contracts";
      public const string Result = "Contract.Result"; 
      public const string EnsuresWithHole = "Contract.Ensures({0});";
    }

    public static class XML
    {
      public const string Root = "CCCheckOutput";
      public const string Assembly = "Assembly";
      public const string Method = "Method";
      public const string Message = "Message";
      public const string Suggestion = "Suggestion";
      public const string SuggestionKind = "SuggestionKind";
      public const string Suggested = "Suggested";
      public const string Statistics = "Statistics";
      public const string FinalStatistics = "FinalStatistic";
      public const string Check = "Check";
      public const string Kind = "Kind";
      public const string Name = "Name";
      public const string Trace = "Trace";
      public const string Result = "Result";
      public const string Score = "Score";
      public const string RelatedLocation = "RelatedLocation";
      public const string SourceLocation = "SourceLocation";
      public const string Justification = "Justification";
      public const string Feature = "Feature";
      public const string SuggestionExtraInfo = "SuggestionExtraInfo";

      public const string Interface = "Interface";
      public const string ProperMember = "Member";
      public const string Extern = "Extern";
      public const string Abstract = "Abstract";
      public const string ConfidenceLevel = "ConfidenceLevel";
      public const string SuggestionTurnedIntoWarning = "SuggestionTurnedIntoWarning_";
    }
  
  }

}
