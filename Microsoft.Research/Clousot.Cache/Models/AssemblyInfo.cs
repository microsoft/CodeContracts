// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Microsoft.Research.CodeAnalysis.Caching.Models
{
    public class AssemblyInfo
    {
        public AssemblyInfo()
        {
            this.Methods = new List<Method>();
        }

        public System.Guid AssemblyId { get; set; }
        public string Name { get; set; }
        public System.DateTime Created { get; set; }
        public bool IsBaseLine { get; set; }
        public string SourceControlInfo { get; set; }
        public virtual ICollection<Method> Methods { get; set; }
    }
}
