// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Removes some attributes we don't want in the rewritten assembly
    /// </summary>
    internal class FilterForRuntime : StandardVisitor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private ContractNodes _contractNodes;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private ContractNodes _targetContractNodes;

        public FilterForRuntime(ContractNodes contractNodes, ContractNodes targetContractNodes)
        {
            _contractNodes = contractNodes;
            _targetContractNodes = targetContractNodes;
        }

        public override AttributeNode VisitAttributeNode(AttributeNode attribute)
        {
            if (attribute == null) return null;

            if (attribute.Type.Namespace != null && ContractNodes.ContractNamespace.Matches(attribute.Type.Namespace))
            {
                switch (attribute.Type.Name.Name)
                {
                    case "ContractClassAttribute":
                    case "ContractInvariantMethodAttribute":
                    case "ContractClassForAttribute":
                    case "ContractVerificationAttribute":
                    case "ContractPublicPropertyNameAttribute":
                    case "ContractArgumentValidatorAttribute":
                    case "ContractAbbreviatorAttribute":
                    case "ContractOptionAttribute":
                    case "ContractRuntimeIgnoredAttribute":
                    case "PureAttribute":
                        return attribute;
                    default:
                        return null;
                        // Don't propagate any other attributes from System.Diagnostics.Contracts, they might be types defined only in mscorlib.contracts.dll
                }
            }

            return base.VisitAttributeNode(attribute);
        }

        public override Method VisitMethod(Method method)
        {
            if (method == null) return null;
            this.VisitAttributeList(method.Attributes);
            this.VisitAttributeList(method.ReturnAttributes);
            // don't visit further into the method.
            return method;
        }

        internal AssemblyNode TransformForTarget(AssemblyNode assemblyToVisit)
        {
            return this.VisitAssembly(assemblyToVisit);
        }
    }
}