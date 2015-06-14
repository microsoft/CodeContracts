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
        public ProofOutcome ProofOutcome { 
          get {
            Contract.Assume(Enum.IsDefined(typeof(ProofOutcome), this.ProofOutcomeByte));
            return (ProofOutcome)this.ProofOutcomeByte; }
          set { this.ProofOutcomeByte = (byte)value; }
        }

        [NotMapped]
        public WarningType WarningType
        {
          get {
            Contract.Assume(Enum.IsDefined(typeof(WarningType), this.WarningTypeByte));
            return (WarningType)this.WarningTypeByte; }
          set { this.WarningTypeByte = (byte)value; }
        }

    }
}
