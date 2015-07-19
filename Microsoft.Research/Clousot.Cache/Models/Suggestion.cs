// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microsoft.Research.CodeAnalysis.Caching.Models
{
    public class Suggestion : OutcomeOrSuggestion
    {
        public Suggestion()
        {
            this.SuggestionContextEdges = new List<SuggestionContextEdge>();
        }

        public string Kind { get; set; }

        // Using "byte" as type because the Entity Framework fails to create the column if we use ClousotSuggestion.Kind instead
        public byte Type { get; set; }

        // Serialize as strings
        public string ExtraInfo { get; set; }

        public virtual ICollection<SuggestionContextEdge> SuggestionContextEdges { get; set; }

        [NotMapped]
        public override IEnumerable<ContextEdge> ContextEdges { get { return this.SuggestionContextEdges; } }
    }
}
