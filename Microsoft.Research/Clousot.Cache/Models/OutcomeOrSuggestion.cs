// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microsoft.Research.CodeAnalysis.Caching.Models
{
    public abstract class OutcomeOrSuggestion
    {
        public OutcomeOrSuggestion()
        {
        }

        public long Id { get; set; }
        public long MethodId { get; set; }
        public string Message { get; set; }
        public int SubroutineLocalId { get; set; }
        public int BlockIndex { get; set; }
        public int ApcIndex { get; set; }
        public virtual Method Method { get; set; }

        [NotMapped]
        public abstract IEnumerable<ContextEdge> ContextEdges { get; }
    }
}
