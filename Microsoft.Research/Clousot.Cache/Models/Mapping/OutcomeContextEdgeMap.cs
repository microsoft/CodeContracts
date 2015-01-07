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

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching.Models.Mapping
{
    public class OutcomeContextEdgeMap : EntityTypeConfiguration<OutcomeContextEdge>
    {

      [ContractVerification(false)] // Too many external unknown
        public OutcomeContextEdgeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Tag)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("OutcomeContextEdges");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OutcomeId).HasColumnName("OutcomeId");
            this.Property(t => t.Block1SubroutineLocalId).HasColumnName("Block1SubroutineLocalId");
            this.Property(t => t.Block1Index).HasColumnName("Block1Index");
            this.Property(t => t.Block2SubroutineLocalId).HasColumnName("Block2SubroutineLocalId");
            this.Property(t => t.Block2Index).HasColumnName("Block2Index");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.Rank).HasColumnName("Rank");

            // Relationships
            this.HasRequired(t => t.Outcome)
                .WithMany(t => t.OutcomeContextEdges)
                .HasForeignKey(d => d.OutcomeId);

        }
    }
}
