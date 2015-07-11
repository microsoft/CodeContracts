// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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