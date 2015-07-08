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

using System.CodeDom.Compiler;
using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Represents an error based on CCI source context information.
    /// </summary>
    [ContractVerification(true)]
    public class Error : CompilerError
    {
        public Error(int errorCode, string error, SourceContext context)
        {
            Contract.Requires(error != null);

            if (context.IsValid)
            {
                // F: Why context.Document != null? Added the assumption
                Contract.Assume(context.Document != null);

                this.FileName = context.Document.Name;
            }
            else
            {
                this.FileName = " "; // non-empty to prevent VS/msbuild from prepending EXEC 
            }

            this.Line = context.StartLine;
            this.Column = context.StartColumn;
            this.ErrorNumber = "CC" + errorCode;
            this.ErrorText = error;
        }
    }
}