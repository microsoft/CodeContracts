using System.Collections.Generic;
using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    internal sealed class CollectBoundVariables : Inspector
    {
        public readonly List<Parameter> FoundVariables = null;
        public readonly List<Expression> FoundReferences = null;
        private readonly List<Parameter> BoundVars;

        public CollectBoundVariables(List<Parameter> boundVars)
        {
            Contract.Requires(boundVars != null);

            this.FoundVariables = new List<Parameter>(boundVars.Count);
            this.FoundReferences = new List<Expression>(boundVars.Count);
            this.BoundVars = boundVars;
        }

        public override void VisitParameter(Parameter parameter)
        {
            if (parameter == null) return;
            if (this.BoundVars.Contains(parameter) && !this.FoundVariables.Contains(parameter))
            {
                this.FoundVariables.Add(parameter);
                this.FoundReferences.Add(parameter);
            }
        }

        public override void VisitMemberBinding(MemberBinding memberBinding)
        {
            if (memberBinding == null) return;

            if (memberBinding.TargetObject.NodeType == NodeType.This
                || memberBinding.TargetObject.NodeType == NodeType.Local)
            {
                // search in list of parameters to see if any have the same name as the bound member
                foreach (Parameter p in this.BoundVars)
                {
                    if (p.Name.UniqueIdKey == memberBinding.BoundMember.Name.UniqueIdKey)
                    {
                        if (!this.FoundVariables.Contains(p))
                        {
                            this.FoundVariables.Add(p);
                            this.FoundReferences.Add(memberBinding);
                        }
                    }
                }
            }

            base.VisitMemberBinding(memberBinding);
        }
    }
}