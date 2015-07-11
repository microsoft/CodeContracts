// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        private ContractNodes _contractNodes;
        private Method _declaringMethod;

        public ExtractionFinalizer(ContractNodes contractNodes)
        {
            _contractNodes = contractNodes;
        }

        private StatementList _currentSL;
        private int _currentSLindex;

        public override StatementList VisitStatementList(StatementList statements)
        {
            var oldSL = _currentSL;
            var oldSLi = _currentSLindex;
            _currentSL = statements;

            try
            {
                if (statements == null) return null;
                for (int i = 0, n = statements.Count; i < n; i++)
                {
                    _currentSLindex = i;
                    statements[i] = (Statement)this.Visit(statements[i]);
                }
                return statements;
            }
            finally
            {
                _currentSLindex = oldSLi;
                _currentSL = oldSL;
            }
        }

        public override MethodContract VisitMethodContract(MethodContract contract)
        {
            _declaringMethod = contract.DeclaringMethod;

            return base.VisitMethodContract(contract);
        }

        public override Expression VisitMethodCall(MethodCall call)
        {
            MemberBinding mb = call.Callee as MemberBinding;
            if (mb == null) return call;

            Method calledMethod = mb.BoundMember as Method;
            if (calledMethod == null) return call;

            Method template = calledMethod.Template;

            if (_contractNodes.IsOldMethod(template))
            {
                OldExpression oe = new OldExpression(ExtractOldExpression(call));
                oe.Type = call.Type;
                return oe;
            }

            if (_contractNodes.IsValueAtReturnMethod(template))
            {
                return new AddressDereference(call.Operands[0], calledMethod.TemplateArguments[0], call.SourceContext);
            }

            if (_contractNodes.IsResultMethod(template))
            {
                // check if we are in an Task returning method
                if (_declaringMethod != null && _declaringMethod.ReturnType != null)
                {
                    var rt = _declaringMethod.ReturnType;
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

            if (_currentSL != null)
            {
                var locs = FindLocals.Get(cand);
                if (locs.Count > 0)
                {
                    // find the instructions that set these locals
                    var assignments = new List<Statement>();
                    for (int i = _currentSLindex - 1; i >= 0; i--)
                    {
                        var a = _currentSL[i] as AssignmentStatement;
                        if (a == null) continue;

                        var loc = a.Target as Local;
                        if (loc == null) continue;

                        if (locs.Contains(loc))
                        {
                            assignments.Add(a);
                            _currentSL[i] = null;
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