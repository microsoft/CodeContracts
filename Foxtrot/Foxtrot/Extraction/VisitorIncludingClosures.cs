// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    internal class VisitorIncludingClosures : StandardVisitor
    {
        private Method _currentMethod;

        protected Method CurrentMethod
        {
            get { return _currentMethod; }
            set
            {
                _currentMethod = value;

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

        private Stack<Method> _delegates = new Stack<Method>();

        public virtual void VisitAnonymousDelegate(Method method)
        {
            if (method == null) return;

            _delegates.Push(method);
            try
            {
                this.VisitBlock(method.Body);
            }
            finally
            {
                _delegates.Pop();
            }
        }
    }
}