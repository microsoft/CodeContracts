// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Microsoft.Research.CodeAnalysis.Caching.Models.Mapping
{
    internal class BaselineMethodMap : EntityTypeConfiguration<BaselineMethod>
    {
        [ContractVerification(false)] // Too many external unknown
        public BaselineMethodMap()
        {
            this.Property(t => t.MethodFullNameHash)
              .IsRequired();
            this.Property(t => t.BaselineId)
              .IsRequired()
              .HasMaxLength(100);


            // Primary Key
            this.HasKey(t => new
            {
                t.MethodFullNameHash,
                t.BaselineId
            });


            this.Property(t => t.MethodId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("BaselineMethod");
            this.Property(t => t.MethodFullNameHash).HasColumnName("MethodFullNameHash");
            this.Property(t => t.MethodId).HasColumnName("MethodId");

            // Relationships
            this.HasRequired(t => t.Method)
                .WithMany()
                .HasForeignKey(d => d.MethodId);
        }
    }
}
