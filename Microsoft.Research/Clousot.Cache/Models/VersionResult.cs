// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Microsoft.Research.CodeAnalysis.Caching.Models
{
    public class VersionResult
    {
        public long Version { get; set; }
        public long Methods { get; set; }
        public long ContractInstructions { get; set; }
        public long Contracts { get; set; }
        public long MethodInstructions { get; set; }
        public long Outcomes { get; set; }
        public long StatsBottom { get; set; }
        public long StatsFalse { get; set; }
        public long StatsTop { get; set; }
        public long StatsTrue { get; set; }
        public long Suggestions { get; set; }
        public long SwallowedBottom { get; set; }
        public long SwallowedFalse { get; set; }
        public long SwallowedTop { get; set; }
        public long SwallowedTrue { get; set; }
        public long Timeout { get; set; }
        public long HasWarnings { get; set; }
        public long ZeroTop { get; set; }
    }
}
