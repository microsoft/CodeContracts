// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Research.CodeAnalysis.Caching.Models
{
    public class ContextEdge
    {
        public long Id { get; set; }
        public int Block1SubroutineLocalId { get; set; }
        public int Block1Index { get; set; }
        public int Block2SubroutineLocalId { get; set; }
        public int Block2Index { get; set; }
        public string Tag { get; set; }
        public int Rank { get; set; }
    }
}
