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