// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching.Models.Mapping
{
    public class VersionResultMap : EntityTypeConfiguration<VersionResult>
    {
        [ContractVerification(false)] // Too many external unknown
        public VersionResultMap()
        {
            // Primary Key
            this.HasKey(t => t.Version);

            // Properties
            this.Property(t => t.Version)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VersionResults");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.Methods).HasColumnName("Methods");
            this.Property(t => t.ContractInstructions).HasColumnName("ContractInstructions");
            this.Property(t => t.Contracts).HasColumnName("Contracts");
            this.Property(t => t.MethodInstructions).HasColumnName("MethodInstructions");
            this.Property(t => t.Outcomes).HasColumnName("Outcomes");
            this.Property(t => t.StatsBottom).HasColumnName("StatsBottom");
            this.Property(t => t.StatsFalse).HasColumnName("StatsFalse");
            this.Property(t => t.StatsTop).HasColumnName("StatsTop");
            this.Property(t => t.StatsTrue).HasColumnName("StatsTrue");
            this.Property(t => t.Suggestions).HasColumnName("Suggestions");
            this.Property(t => t.SwallowedBottom).HasColumnName("SwallowedBottom");
            this.Property(t => t.SwallowedFalse).HasColumnName("SwallowedFalse");
            this.Property(t => t.SwallowedTop).HasColumnName("SwallowedTop");
            this.Property(t => t.SwallowedTrue).HasColumnName("SwallowedTrue");
            this.Property(t => t.Timeout).HasColumnName("Timeout");
            this.Property(t => t.HasWarnings).HasColumnName("HasWarnings");
            this.Property(t => t.ZeroTop).HasColumnName("ZeroTop");
        }
    }
}
