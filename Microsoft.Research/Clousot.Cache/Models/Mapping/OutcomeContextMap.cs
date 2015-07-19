// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching.Models.Mapping
{
    public class OutcomeContextMap : EntityTypeConfiguration<OutcomeContext>
    {
        [ContractVerification(false)] // Too many external unknown

        public OutcomeContextMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("OutcomeContexts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OutcomeId).HasColumnName("OutcomeId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.AssociatedInfo).HasColumnName("AssociatedInfo");

            // Relationships
            this.HasRequired(t => t.Outcome)
                .WithMany(t => t.OutcomeContexts)
                .HasForeignKey(d => d.OutcomeId);
        }
    }
}
