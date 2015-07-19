// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching.Models.Mapping
{
    public class IdHashTimeToMethodMap : EntityTypeConfiguration<IdHashTimeToMethod>
    {
        [ContractVerification(false)] // Too many external unknown
        public IdHashTimeToMethodMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MethodIdHash, t.Time, t.MethodId });

            // Properties
            this.Property(t => t.MethodIdHash)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.MethodId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("IdHashTimeToMethod");
            this.Property(t => t.MethodIdHash).HasColumnName("MethodIdHash");
            this.Property(t => t.Time).HasColumnName("Time");
            this.Property(t => t.MethodId).HasColumnName("MethodId");

            // Relationships
            this.HasRequired(t => t.Method)
                .WithMany(t => t.IdHashTimeToMethods)
                .HasForeignKey(d => d.MethodId);
        }
    }
}
