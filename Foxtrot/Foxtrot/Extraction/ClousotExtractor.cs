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
using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    internal class ClousotExtractor : ExtractorVisitor
    {
        public ClousotExtractor(ContractNodes contractNodes, AssemblyNode ultimateTargetAssembly,
            AssemblyNode realAssembly, Action<System.CodeDom.Compiler.CompilerError> errorHandler)
            : base(contractNodes, ultimateTargetAssembly, realAssembly)
        {
            Contract.Requires(contractNodes != null);
            Contract.Requires(realAssembly != null);
        }

        public override Method VisitMethod(Method method)
        {
            if (method == null) return null;

            if (visitedMethods.ContainsKey(method))
                return method;

            base.VisitMethod(method);

            method.SetDelayedContract((m, dummy) =>
            {
                // cleanup contracts for clousot
                this.CleanupRequires(method);
                this.CleanupEnsures(method);
                // this.CleanupBody(method.Body);
            });

            return method;
        }

        protected override Statement ExtraAssumeFalseOnThrow()
        {
            return 
                new ExpressionStatement(
                    new MethodCall(
                        new MemberBinding(null, this.contractNodes.AssumeMethod), new ExpressionList(Literal.False), 
                        NodeType.Call));
        }

        public override EnsuresExceptional VisitEnsuresExceptional(EnsuresExceptional exceptional)
        {
            // since clousot doesn't handle exceptional post yet, we filter them out here.
            // TODO: refactor Ensures into two lists: normal and exceptional
            return null;
        }

        /// <summary>
        /// Visits the ensures clauses to clean up Old expressions
        /// </summary>
        /// <param name="m"></param>
        public void CleanupEnsures(Method m)
        {
            if (m.Contract != null)
            {
                this.VisitEnsuresList(m.Contract.Ensures);
                this.VisitEnsuresList(m.Contract.AsyncEnsures);
                this.VisitEnsuresList(m.Contract.ModelEnsures);
            }
        }

        protected override bool IncludeModels
        {
            get { return true; }
        }

        /// <summary>
        /// Visits the requires clauses to clean up overloaded calls
        /// </summary>
        public void CleanupRequires(Method m)
        {
            if (m.Contract != null)
            {
                this.VisitRequiresList(m.Contract.Requires);
            }
        }

        private bool insideInvariant;

        /// <summary>
        /// Performs a bunch of transformations that the basic Extractor doesn't seem to do:
        ///  - Turns calls to Old contract method into OldExpressions
        ///  - Turns references to "result" into ResultExpressions
        ///  - 
        /// </summary>
        public override Expression VisitMethodCall(MethodCall call)
        {
            MemberBinding mb = call.Callee as MemberBinding;
            if (mb == null) return call;

            Method calledMethod = mb.BoundMember as Method;
            if (calledMethod == null) return call;

            Method template = calledMethod.Template;
            if (contractNodes.IsOldMethod(template))
            {
                Expression result = base.VisitMethodCall(call);

                call = result as MethodCall;
                if (call == null) return result;

                result = new OldExpression(call.Operands[0]);
                result.Type = call.Type;

                return result;
            }

            if (contractNodes.IsValueAtReturnMethod(template))
            {
                return new AddressDereference(call.Operands[0], calledMethod.TemplateArguments[0], call.SourceContext);
            }

            if (calledMethod.Parameters != null)
            {
                if (calledMethod.Parameters.Count == 0)
                {
                    if (contractNodes.IsResultMethod(template))
                    {
                        return new ReturnValue(calledMethod.ReturnType, call.SourceContext);
                    }
                }
                else if (insideInvariant &&
                         contractNodes.IsInvariantMethod(calledMethod))
                {
                    Expression condition = call.Operands[0];
                    condition.SourceContext = call.SourceContext;

                    return VisitExpression(condition);
                }
            }

            return base.VisitMethodCall(call);
        }

        public override Invariant VisitInvariant(Invariant invariant)
        {
            this.insideInvariant = true;

            try
            {
                return base.VisitInvariant(invariant);
            }
            finally
            {
                this.insideInvariant = false;
            }
        }
    }
}