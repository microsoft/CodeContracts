using System.Collections.Generic;
using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    internal sealed class SubstituteParameters : StandardVisitor
    {
        private readonly TrivialHashtable map;
        private readonly List<Parameter> parameters;

        public SubstituteParameters(TrivialHashtable map, List<Parameter> parameters)
        {
            this.map = map;
            this.parameters = parameters;
        }

        public override Expression VisitMemberBinding(MemberBinding memberBinding)
        {
            if (memberBinding.TargetObject != null &&
                (memberBinding.TargetObject.NodeType == NodeType.This
                 || memberBinding.TargetObject.NodeType == NodeType.Local))
            {
                // search in list of parameters to see if any have the same name as the bound member
                foreach (Parameter p in this.parameters)
                {
                    if (p.Name.UniqueIdKey == memberBinding.BoundMember.Name.UniqueIdKey)
                    {
                        return (Expression) this.map[p.UniqueKey];
                    }
                }

                return base.VisitMemberBinding(memberBinding);
            }
            
            return base.VisitMemberBinding(memberBinding);
        }

        public override Expression VisitParameter(Parameter parameter)
        {
            if (parameter == null) return null;

            object result = map[parameter.UniqueKey];
            if (result != null) return (Expression) result;

            return base.VisitParameter(parameter);
        }
    }
}