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