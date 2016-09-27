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
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace Microsoft.Research.CodeAnalysis
{
    [ContractVerification(true)]
    class SourceFileFinder
    {
        public SourceFileFinder(IEnumerable<string> originalSourcePaths, IEnumerable<string> alternativeSourcePaths)
        {
            Contract.Requires(originalSourcePaths != null);
            Contract.Requires(alternativeSourcePaths != null);

            this.originalSourcePaths = originalSourcePaths;
            this.alternativeSourcePaths = alternativeSourcePaths;
        }

        readonly IEnumerable<string> originalSourcePaths;
        readonly IEnumerable<string> alternativeSourcePaths;
        const StringComparison Comparison = StringComparison.OrdinalIgnoreCase; // Ignore case in Windows. Re-evalute this for Mono/.NET Core

        public string Find(string originalPath)
        {
            if (originalPath == null || File.Exists(originalPath))
            {
                return originalPath;
            }

            foreach (var originalSourcePath in originalSourcePaths)
            {
                // Find the source path that this is a sub-path of.
                if (!originalPath.StartsWith(originalSourcePath, Comparison))
                {
                    continue;
                }

                // Extract the path relative to the source path;
                var relativePath = originalPath.Substring(originalSourcePath.Length);

                foreach (var alternativeSourcePath in alternativeSourcePaths)
                {
                    // Graft the relative path onto the alternative source path, as though the file
                    // exists within that source tree instead.
                    var alternativePath = alternativeSourcePath + relativePath;

                    if (File.Exists(alternativePath))
                    {
                        return alternativePath;
                    }
                }
            }

            return originalPath;
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(originalSourcePaths != null);
            Contract.Invariant(alternativeSourcePaths != null);
        }
    }
}
