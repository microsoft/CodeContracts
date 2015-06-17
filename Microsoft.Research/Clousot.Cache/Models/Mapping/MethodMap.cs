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
