// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.Research.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microsoft.Research.CodeAnalysis.Caching.Models
{
    public class OutcomeContext
    {
        public long Id { get; set; }
        public long OutcomeId { get; set; }
        public byte Type { get; set; }
        public int AssociatedInfo { get; set; }
        public virtual Outcome Outcome { get; set; }

        [NotMapped]
        public WarningContext WarningContext
        {
            get { return new WarningContext((WarningContext.ContextType)this.Type, this.AssociatedInfo); }
            set { this.Type = (byte)value.Type; this.AssociatedInfo = value.AssociatedInfo; }
        }
    }
}
