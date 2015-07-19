// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching.Models.Mapping
{
    public class SuggestionContextEdgeMap : EntityTypeConfiguration<SuggestionContextEdge>
    {
        [ContractVerification(false)] // Too many external unknown

        public SuggestionContextEdgeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Tag)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SuggestionContextEdges");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SuggestionId).HasColumnName("SuggestionId");
            this.Property(t => t.Block1SubroutineLocalId).HasColumnName("Block1SubroutineLocalId");
            this.Property(t => t.Block1Index).HasColumnName("Block1Index");
            this.Property(t => t.Block2SubroutineLocalId).HasColumnName("Block2SubroutineLocalId");
            this.Property(t => t.Block2Index).HasColumnName("Block2Index");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.Rank).HasColumnName("Rank");

            // Relationships
            this.HasRequired(t => t.Suggestion)
                .WithMany(t => t.SuggestionContextEdges)
                .HasForeignKey(d => d.SuggestionId);
        }
    }
}
