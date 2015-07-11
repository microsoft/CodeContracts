// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    public sealed class ContractAssumeAssertStatement : ExpressionStatement
    {
        public readonly string SourceText;

        public ContractAssumeAssertStatement(Expression expression, SourceContext sctx, string sourceText)
            : base(expression, sctx)
        {
            this.SourceText = sourceText;
        }
    }
}