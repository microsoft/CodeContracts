// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Microsoft.Research.CodeAnalysis.Caching.Models
{
    public class IdHashTimeToMethod
    {
        public byte[] MethodIdHash { get; set; }
        public System.DateTime Time { get; set; }
        public long MethodId { get; set; }
        public virtual Method Method { get; set; }
    }
}
