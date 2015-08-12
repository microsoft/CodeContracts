// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Research.CodeAnalysis
{
    /// <summary>
    /// Factor out all the thresholds used in Clousot
    /// </summary>
    public static class Thresholds
    {
        public static class Renamings
        {
            public const double TooManyRenamings = 200;
            public const double TooManyVariableRenamingsonAverage = 300;
        }
    }
}
