// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.CodeAnalysis.Caching.Models
{
    public class Outcome : OutcomeOrSuggestion
    {
        public Outcome()
        {
            this.OutcomeContextEdges = new List<OutcomeContextEdge>();
            this.OutcomeContexts = new List<OutcomeContext>();
        }

        public bool Related { get; set; }
        public byte ProofOutcomeByte { get; set; }
        public byte WarningTypeByte { get; set; }
        public virtual ICollection<OutcomeContextEdge> OutcomeContextEdges { get; set; }
        public virtual ICollection<OutcomeContext> OutcomeContexts { get; set; }


        [NotMapped]
        public override IEnumerable<ContextEdge> ContextEdges { get { return this.OutcomeContextEdges; } }

        [NotMapped]
        public ProofOutcome ProofOutcome
        {
            get
            {
                Contract.Assume(Enum.IsDefined(typeof(ProofOutcome), this.ProofOutcomeByte));
                return (ProofOutcome)this.ProofOutcomeByte;
            }
            set { this.ProofOutcomeByte = (byte)value; }
        }

        [NotMapped]
        public WarningType WarningType
        {
            get
            {
                Contract.Assume(Enum.IsDefined(typeof(WarningType), this.WarningTypeByte));
                return (WarningType)this.WarningTypeByte;
            }
            set { this.WarningTypeByte = (byte)value; }
        }
    }
}
