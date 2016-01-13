using System;
using System.Collections.Generic;
using System.Compiler;
using System.Linq;
using System.Text;

namespace Microsoft.Contracts.Foxtrot
{
    public sealed class PostRewriteChecker : Inspector
    {
        private Member currentMember;

        public override void VisitProperty(Property property)
        {
            bool exchanged = false;
            if (this.currentMember == null)
            {
                this.currentMember = property;
                exchanged = true;
            }

            base.VisitProperty(property);

            if (exchanged)
            {
                this.currentMember = null;
            }
        }

        public override void VisitEvent(Event evnt)
        {
            bool exchanged = false;
            if (this.currentMember == null)
            {
                this.currentMember = evnt;
                exchanged = true;
            }

            base.VisitEvent(evnt);

            if (exchanged)
            {
                this.currentMember = null;
            }
        }

        public override void VisitMethod(Method method)
        {
            bool exchanged = false;
            if (this.currentMember == null)
            {
                this.currentMember = method;
                exchanged = true;
            }

            base.VisitMethod(method);

            if (exchanged)
            {
                this.currentMember = null;
            }
        }

        public override void VisitMemberBinding(MemberBinding memberBinding)
        {
            base.VisitMemberBinding(memberBinding);

            if (this.currentMember == null)
            {
                return;
            }

            Method method = memberBinding.BoundMember as Method;
            if (method == null)
            {
                return;
            }
            
            if (!method.DeclaringType.Namespace.Matches(ContractNodes.ContractNamespace))
            {
                return;
            }

            if (!method.DeclaringType.Name.Matches(ContractNodes.ContractClassName))
            {
                return;
            }

            if (method.DeclaringType.DeclaringModule.Name != "mscorlib" &&
                method.DeclaringType.DeclaringModule.Name != "Microsoft.Contracts")
            {
                return;
            }

            if (method.Name.Matches(ContractNodes.RequiresName) ||
                method.Name.Matches(ContractNodes.EnsuresName) ||
                method.Name.Matches(ContractNodes.EnsuresOnThrowName) ||
                method.Name.Matches(ContractNodes.InvariantName) ||
                method.Name.Matches(ContractNodes.OldName) ||
                method.Name.Matches(ContractNodes.ResultName) ||
                method.Name.Matches(ContractNodes.ValueAtReturnName) ||
                method.Name.Matches(ContractNodes.EndContractBlockName))
            {

                string message = string.Format("Not rewritten call to contract method '{0}' has been detected in member '{1}'.", method.FullName, this.currentMember.FullName);
                throw new RewriteException(message);
            }
        }
    }
}
