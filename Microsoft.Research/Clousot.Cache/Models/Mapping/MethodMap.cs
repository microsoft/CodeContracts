// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching.Models.Mapping
{
    public class MethodMap : EntityTypeConfiguration<Method>
    {
        [ContractVerification(false)] // Too many external unknown
        public MethodMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Hash)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(400);

            this.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(CacheUtils.MaxMethodLength);

            this.Property(t => t.InferredExprHash)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.InferredExprString).HasMaxLength(null);
            this.Property(t => t.InferredExpr).HasMaxLength(null);

            // Table & Column Mappings
            this.ToTable("Methods");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Hash).HasColumnName("Hash");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.PureParametersMask).HasColumnName("PureParametersMask");
            this.Property(t => t.StatsTop).HasColumnName("StatsTop");
            this.Property(t => t.StatsBottom).HasColumnName("StatsBottom");
            this.Property(t => t.StatsTrue).HasColumnName("StatsTrue");
            this.Property(t => t.StatsFalse).HasColumnName("StatsFalse");
            this.Property(t => t.SwallowedTop).HasColumnName("SwallowedTop");
            this.Property(t => t.SwallowedBottom).HasColumnName("SwallowedBottom");
            this.Property(t => t.SwallowedTrue).HasColumnName("SwallowedTrue");
            this.Property(t => t.SwallowedFalse).HasColumnName("SwallowedFalse");
            this.Property(t => t.Contracts).HasColumnName("Contracts");
            this.Property(t => t.MethodInstructions).HasColumnName("MethodInstructions");
            this.Property(t => t.ContractInstructions).HasColumnName("ContractInstructions");
            this.Property(t => t.Timeout).HasColumnName("Timeout");
            this.Property(t => t.InferredExpr).HasColumnName("InferredExpr");
            this.Property(t => t.InferredExprHash).HasColumnName("InferredExprHash");
            this.Property(t => t.InferredExprString).HasColumnName("InferredExprString");
        }
    }
}
