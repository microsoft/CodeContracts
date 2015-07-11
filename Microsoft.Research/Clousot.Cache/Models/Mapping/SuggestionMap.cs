// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching.Models.Mapping
{
    public class SuggestionMap : EntityTypeConfiguration<Suggestion>
    {
        [ContractVerification(false)] // Too many external unknown

        public SuggestionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Kind)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Message)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Suggestions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MethodId).HasColumnName("MethodId");
            this.Property(t => t.Kind).HasColumnName("Kind");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.SubroutineLocalId).HasColumnName("SubroutineLocalId");
            this.Property(t => t.BlockIndex).HasColumnName("BlockIndex");
            this.Property(t => t.ApcIndex).HasColumnName("ApcIndex");

            // Relationships
            this.HasRequired(t => t.Method)
                .WithMany(t => t.Suggestions)
                .HasForeignKey(d => d.MethodId);
        }
    }
}
