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

using System.Collections.Generic;
using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Used to perform the translation from Contract.OldValue -> OldValue expression and
    /// same for Result and ValueAtReturn in the extracted contract and its used closure methods.
    /// </summary>
    internal class ExtractionFinalizer : VisitorIncludingClosures
    {
        private ContractNodes contractNodes;
        private Method declaringMethod;

        public ExtractionFinalizer(ContractNodes contractNodes)
        {
            this.contractNodes = contractNodes;
        }

        private StatementList currentSL;
        private int currentSLindex;

        public override StatementList VisitStatementList(StatementList statements)
        {
            var oldSL = this.currentSL;
            var oldSLi = this.currentSLindex;
            this.currentSL = statements;

            try
            {
                if (statements == null) return null;
                for (int i = 0, n = statements.Count; i < n; i++)
                {
                    this.currentSLindex = i;
                    statements[i] = (Statement) this.Visit(statements[i]);
                }
                return statements;
            }
            finally
            {
                this.currentSLindex = oldSLi;
                this.currentSL = oldSL;
            }
        }

        public override MethodContract VisitMethodContract(MethodContract contract)
        {
            this.declaringMethod = contract.DeclaringMethod;

            return base.VisitMethodContract(contract);
        }

        public override Expression VisitMethodCall(MethodCall call)
        {
            MemberBinding mb = call.Callee as MemberBinding;
            if (mb == null) return call;

            Method calledMethod = mb.BoundMember as Method;
            if (calledMethod == null) return call;

            Method template = calledMethod.Template;

            if (contractNodes.IsOldMethod(template))
            {
                OldExpression oe = new OldExpression(ExtractOldExpression(call));
                oe.Type = call.Type;
                return oe;
            }

            if (contractNodes.IsValueAtReturnMethod(template))
            {
                return new AddressDereference(call.Operands[0], calledMethod.TemplateArguments[0], call.SourceContext);
            }

            if (contractNodes.IsResultMethod(template))
            {
                // check if we are in an Task returning method
                if (this.declaringMethod != null && this.declaringMethod.ReturnType != null)
                {
                    var rt = this.declaringMethod.ReturnType;
                    var templ = rt.Template;

                    if (templ != null && templ.Name.Name == "Task`1" && rt.TemplateArguments != null &&
                        rt.TemplateArguments.Count == 1)
                    {
                        var targ = rt.TemplateArguments[0];
                        if (calledMethod.TemplateArguments[0] == targ)
                        {
                            // use of ReturnValue<T>() instead of ReturnValue<Task<T>>().Result
                            var retExp = new ReturnValue(rt, call.SourceContext);
                            var resultProp = rt.GetProperty(Identifier.For("Result"));
                            if (resultProp != null && resultProp.Getter != null)
                            {
                                return new MethodCall(new MemberBinding(retExp, resultProp.Getter), new ExpressionList());
                            }
                        }
                    }
                }

                return new ReturnValue(calledMethod.ReturnType, call.SourceContext);
            }

            return base.VisitMethodCall(call);
        }

        private Expression ExtractOldExpression(MethodCall call)
        {
            var cand = call.Operands[0];

            if (this.currentSL != null)
            {
                var locs = FindLocals.Get(cand);
                if (locs.Count > 0)
                {
                    // find the instructions that set these locals
                    var assignments = new List<Statement>();
                    for (int i = this.currentSLindex - 1; i >= 0; i--)
                    {
                        var a = this.currentSL[i] as AssignmentStatement;
                        if (a == null) continue;

                        var loc = a.Target as Local;
                        if (loc == null) continue;

                        if (locs.Contains(loc))
                        {
                            assignments.Add(a);
                            this.currentSL[i] = null;
                            locs.Remove(loc);
                        }

                        if (locs.Count == 0) break;
                    }
                    assignments.Reverse();

                    var be = new StatementList();
                    assignments.ForEach(astmt => be.Add(astmt));

                    be.Add(new ExpressionStatement(cand));
                    var sc = cand.SourceContext;

                    cand = new BlockExpression(new Block(be));
                    cand.SourceContext = sc;

                    if (locs.Count > 0)
                    {
                        // warn that we couldn't extract the local
                    }
                }
            }

            return cand;
        }
    }
}