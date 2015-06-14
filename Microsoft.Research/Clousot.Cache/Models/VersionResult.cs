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
