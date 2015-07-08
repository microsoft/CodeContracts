using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    internal class AsyncReturnValueQuery : InspectorIncludingClosures
    {
        private bool foundReturnValueTaskResult;
        private ContractNodes contractNodes;
        private TypeNode actualResultType;

        public AsyncReturnValueQuery(ContractNodes contractNodes, Method currentMethod, TypeNode actualResultType)
        {
            this.contractNodes = contractNodes;
            this.CurrentMethod = currentMethod;
            this.actualResultType = actualResultType;
        }

        /// <summary>
        /// actualReturn type is null if Task is not generic, otherwise ,the Task result type.
        /// </summary>
        public static bool Contains(Node node, ContractNodes contractNodes, Method currentMethod,
            TypeNode actualReturnType)
        {
            var v = new AsyncReturnValueQuery(contractNodes, currentMethod, actualReturnType);

            v.Visit(node);

            return v.foundReturnValueTaskResult;
        }

        public override void VisitMethodCall(MethodCall call)
        {
            MemberBinding mb = call.Callee as MemberBinding;
            if (mb == null) return;

            Method calledMethod = mb.BoundMember as Method;
            if (calledMethod == null) return;

            Method template = calledMethod.Template;

            if (template != null && template.Name.Name == "get_Result" && template.DeclaringType.Name.Name == "Task`1")
            {
                // check if callee is result
                var innercall = mb.TargetObject as MethodCall;
                if (innercall != null)
                {
                    MemberBinding mb2 = innercall.Callee as MemberBinding;
                    if (mb2 != null)
                    {
                        Method calledMethod2 = mb2.BoundMember as Method;
                        if (calledMethod2 != null)
                        {
                            Method template2 = calledMethod2.Template;
                            if (contractNodes.IsResultMethod(template2))
                            {
                                this.foundReturnValueTaskResult = true;
                                //return new ReturnValue(calledMethod.DeclaringType.TemplateArguments[0]);
                                return;
                            }
                        }
                    }
                }
            }

            if (this.actualResultType != null && contractNodes.IsResultMethod(template) &&
                calledMethod.ReturnType == this.actualResultType)
            {
                // using Contract.Result<T>() in a Task<T> returning method, this is a shorthand for
                // Contract.Result<Task<T>>().Result
                this.foundReturnValueTaskResult = true;
                return;
            }

            base.VisitMethodCall(call);
        }
    }
}