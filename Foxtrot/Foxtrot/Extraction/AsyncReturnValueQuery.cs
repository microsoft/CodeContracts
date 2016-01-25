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

            // For async methods calledMethod (Contract.Result) would be called in the generated generic state machine,
            // and calledMethod.ReturnType would be generic type argument and actualResultType can be generic method argument
            // (if async postcondition declared in the generic method from non-generic class).
            // In this case calledMethod.ReturnType would be != this.actualResultType
            // and different comparison logic should be used (reflected in EquivalentGenercTypes method).
            if (this.actualResultType != null && contractNodes.IsResultMethod(template) &&
                (calledMethod.ReturnType == this.actualResultType || EquivalentGenericTypes(calledMethod, actualResultType)))
            {
                // using Contract.Result<T>() in a Task<T> returning method, this is a shorthand for
                // Contract.Result<Task<T>>().Result
                this.foundReturnValueTaskResult = true;
                return;
            }

            base.VisitMethodCall(call);
        }

        private static bool EquivalentGenericTypes(Method calledMethod, TypeNode actualResultType)
        {
            if (calledMethod.IsGeneric && actualResultType.IsTemplateParameter)
            {
                // Relatively naive implementation for equality, but still correct
                // and should not lead to false positives.
                return calledMethod.ReturnType.FullName == actualResultType.FullName;
            }

            return false;
        }
    }
}