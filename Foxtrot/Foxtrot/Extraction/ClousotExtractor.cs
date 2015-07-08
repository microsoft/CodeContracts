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

#if true // ExtractorVisitor now does this

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

#endif
    }
}