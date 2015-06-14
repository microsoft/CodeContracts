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
