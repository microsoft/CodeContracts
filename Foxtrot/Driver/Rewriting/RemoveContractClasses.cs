using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// There is no need in the rewritten assembly for the contract classes.
    /// And they just cause problems if there is a disagreement between a
    /// contract class that is in the real assembly and the one in the reference
    /// assembly. (Admittedly a low probability event, but it has occurred in our
    /// hand-generated reference assemblies for the framework.)
    /// </summary>
    internal sealed class RemoveContractClasses : Inspector
    {
        private static void ScrubAttributeList(AttributeList attributes)
        {
            if (attributes == null) return;
            for (int i = 0, n = attributes.Count; i < n; i++)
            {
                if (attributes[i] == null) continue;

                if (attributes[i].Type == null) continue;

                if (ContractNodes.ContractClassAttributeName.Matches(attributes[i].Type.Name))
                {
                    attributes[i] = null;
                }
            }
        }

        public override void VisitTypeNode(TypeNode typeNode)
        {
            if (typeNode == null) return;

            ScrubAttributeList(typeNode.Attributes);

            this.VisitMemberList(typeNode.Members);
        }

        public override void VisitMemberList(MemberList members)
        {
            if (members == null) return;

            for (int i = 0, n = members.Count; i < n; i++)
            {
                var type = members[i] as TypeNode;

                if (type == null) continue;

                Class c = type as Class;
                if (c != null && HelperMethods.IsContractTypeForSomeOtherType(c))
                {
                    members[i] = null;
                }
                else
                {
                    // for nested types
                    this.VisitTypeNode(type);
                }
            }
        }

        public override void VisitTypeNodeList(TypeNodeList types)
        {
            if (types == null) return;

            for (int i = 0, n = types.Count; i < n; i++)
            {
                var type = types[i];
                if (type == null) continue;

                Class c = type as Class;
                if (c != null && HelperMethods.IsContractTypeForSomeOtherType(c))
                {
                    types[i] = null;
                }
                else
                {
                    // for nested types
                    this.VisitTypeNode(type);
                }
            }
        }
    }
}