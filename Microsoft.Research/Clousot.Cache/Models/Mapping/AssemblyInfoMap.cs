// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching.Models.Mapping
{
    public class AssemblyInfoMap : EntityTypeConfiguration<AssemblyInfo>
    {
        [ContractVerification(false)] // Too many unknowns
        public AssemblyInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.AssemblyId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("AssemblyInfo");
            this.Property(t => t.AssemblyId).HasColumnName("AssemblyId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.IsBaseLine).HasColumnName("IsBaseLine");
            this.Property(t => t.SourceControlInfo).HasColumnName("SourceControlInfo");

            this.HasMany(t => t.Methods).WithMany(m => m.Assemblies);
        }
    }
}
