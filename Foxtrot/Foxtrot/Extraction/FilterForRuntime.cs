using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Removes some attributes we don't want in the rewritten assembly
    /// </summary>
    internal class FilterForRuntime : StandardVisitor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")] private ContractNodes contractNodes;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")] private ContractNodes targetContractNodes;

        public FilterForRuntime(ContractNodes contractNodes, ContractNodes targetContractNodes)
        {
            this.contractNodes = contractNodes;
            this.targetContractNodes = targetContractNodes;
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