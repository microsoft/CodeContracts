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
    internal class VisitorIncludingClosures : StandardVisitor
    {
        private Method currentMethod;

        protected Method CurrentMethod
        {
            get { return this.currentMethod; }
            set
            {
                this.currentMethod = value;

                if (value != null)
                {
                    this.CurrentType = value.DeclaringType;
                }
                else
                {
                    this.CurrentType = null;
                }
            }
        }

        protected TypeNode CurrentType { get; set; }

        public override Method VisitMethod(Method method)
        {
            if (method == null) return null;

            var savedCurrentMethod = this.CurrentMethod;

            this.CurrentMethod = method;
            try
            {
                return base.VisitMethod(method);
            }
            finally
            {
                this.CurrentMethod = savedCurrentMethod;
            }
        }

        public override MethodContract VisitMethodContract(MethodContract contract)
        {
            if (contract == null) return null;

            var savedCurrentMethod = this.CurrentMethod;
            this.CurrentMethod = contract.DeclaringMethod;

            try
            {
                return base.VisitMethodContract(contract);
            }
            finally
            {
                this.CurrentMethod = savedCurrentMethod;
            }
        }

        /// <summary>
        /// Need to visit closures as well
        /// </summary>
        /// <param name="cons"></param>
        /// <returns></returns>
        public override Expression VisitConstruct(Construct cons)
        {
            if (cons.Type.IsDelegateType())
            {
                UnaryExpression ue = cons.Operands[1] as UnaryExpression;
                if (ue == null) goto JustVisit;

                MemberBinding mb = ue.Operand as MemberBinding;
                if (mb == null) goto JustVisit;

                Method m = mb.BoundMember as Method;

                m = HelperMethods.Unspecialize(m);

                if (HelperMethods.IsAnonymousDelegate(m, this.CurrentType))
                {
                    this.VisitAnonymousDelegate(m);
                }
            }

            JustVisit:
            
            return base.VisitConstruct(cons);
        }

        private Stack<Method> delegates = new Stack<Method>();

        public virtual void VisitAnonymousDelegate(Method method)
        {
            if (method == null) return;

            delegates.Push(method);
            try
            {
                this.VisitBlock(method.Body);
            }
            finally
            {
                delegates.Pop();
            }
        }
    }
}