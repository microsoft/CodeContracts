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