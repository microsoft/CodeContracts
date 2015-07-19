// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Research.CodeAnalysis.Caching.Models
{
    /// <summary>
    /// Maps method full names to method hash identifying the model to use for the semantic baseline
    /// </summary>
    public class BaselineMethod
    {
        public byte[] MethodFullNameHash { get; set; }
        /// <summary>
        /// Baselines are named so we can have many different baselines
        /// </summary>
        public string BaselineId { get; set; }
        /// <summary>
        /// Target method holding baseline
        /// </summary>
        public long MethodId { get; set; }
        public virtual Method Method { get; set; }
    }
}
