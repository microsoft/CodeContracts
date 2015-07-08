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

using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Visitor is used for invariants only!
    /// </summary>
    internal sealed class RewriteInvariant : StandardVisitor
    {
        private readonly RuntimeContractMethods rcm;
        private Literal sourceTextOfInvariant = null;

        // Requires:
        //  replacementMethod.Keys == replacementExceptions.Keys
        //  
        public RewriteInvariant(RuntimeContractMethods rcm)
        {
            this.rcm = rcm;
        }

        // Requires:
        //  statement.Expression is MethodCall
        //  statement.Expression.Callee is MemberBinding
        //  statement.Expression.Callee.BoundMember is Method
        //  statement.Expression.Callee.BoundMember == "Requires" or "Ensures"
        //
        //  inline  <==> replacementMethod == null
        //  replacementMethod != null
        //           ==> replacementMethod.ReturnType == methodToReplace.ReturnType
        //               && replacementMethod.Parameters.Count == 1
        //               && methodToReplace.Parameters.Count == 1
        //               && replacementMethod.Parameters[0].Type == methodToReplace.Parameters[0].Type
        //
        private static Statement RewriteContractCall(
            ExpressionStatement statement,
            Method /*!*/ methodToReplace,
            Method /*?*/ replacementMethod,
            Literal /*?*/ sourceTextToUseAsSecondArg)
        {
            Contract.Requires(statement != null);

            MethodCall call = statement.Expression as MethodCall;

            if (call == null || call.Callee == null)
            {
                return statement;
            }

            MemberBinding mb = call.Callee as MemberBinding;
            if (mb == null)
            {
                return statement;
            }

            Method m = mb.BoundMember as Method;
            if (m == null)
            {
                return statement;
            }

            if (m != methodToReplace)
            {
                return statement;
            }

            mb.BoundMember = replacementMethod;

            if (call.Operands.Count == 3)
            {
                // then the invariant was found in a reference assembly
                // it already has all of its arguments
                return statement;
            }

            if (call.Operands.Count == 1)
            {
                call.Operands.Add(Literal.Null);
            }

            Literal extraArg = sourceTextToUseAsSecondArg;
            if (extraArg == null)
            {
                extraArg = Literal.Null;
                //extraArg = new Literal("No other information available", SystemTypes.String);
            }

            call.Operands.Add(extraArg);

            return statement;
        }

        public override Statement VisitExpressionStatement(ExpressionStatement statement)
        {
            Method contractMethod = Rewriter.ExtractCallFromStatement(statement);

            if (contractMethod == null)
            {
                return base.VisitExpressionStatement(statement);
            }

            if (this.rcm.ContractNodes.IsInvariantMethod(contractMethod))
            {
                return RewriteContractCall(statement, contractMethod, this.rcm.InvariantMethod, this.sourceTextOfInvariant);
            }

            return base.VisitExpressionStatement(statement);
        }

        public override Invariant VisitInvariant(Invariant @invariant)
        {
            if (@invariant == null) return null;

            SourceContext sctx = @invariant.SourceContext;
            if (invariant.SourceConditionText != null)
            {
                this.sourceTextOfInvariant = invariant.SourceConditionText;
            }
            else if (sctx.IsValid && sctx.Document.Text != null && sctx.Document.Text.Source != null)
            {
                this.sourceTextOfInvariant = new Literal(sctx.Document.Text.Source, SystemTypes.String);
            }
            else
            {
                this.sourceTextOfInvariant = Literal.Null;
                //this.sourceTextOfInvariant = new Literal("No other information available", SystemTypes.String);
            }

            return base.VisitInvariant(@invariant);
        }
    }
}