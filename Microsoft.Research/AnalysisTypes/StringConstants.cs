// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Research.CodeAnalysis
{
    public static class StringConstants
    {
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
