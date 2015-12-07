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

using System;
// needed for defining exception .ctors
using System.CodeDom.Compiler; // needed for CompilerError
// needed for Debug.Assert (etc.)
using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    [ContractVerification(true)]
    public static class Extractor
    {
        // Public API

        /// <summary>
        /// Modifies <paramref name="assembly"/> by extracting any Code Contracts
        /// from the method bodies in the assembly. 
        /// <param name="assembly">
        /// The assembly to which the contracts will be added (i.e., the AST for the assembly
        /// will be enriched with method contracts and object invariants).
        /// </param>
        /// <param name="referenceAssembly">
        /// When not null, then the contracts are extracted from this assembly and then copied over
        /// to <paramref name="assembly"/>. Note that in that case, no contracts are extracted
        /// directly from <paramref name="assembly"/>: all of the contracts in <paramref name="assembly"/>
        /// after this method returns came from this assembly.
        /// </param>
        /// <param name="contracts">
        /// When not null, this will be used for the definitions of the contract methods (and
        /// attributes) in <paramref name="assembly"/> (or <paramref name="referenceAssembly"/>, if that
        /// is not null). When null, then we will make an effort to find the contract methods. In
        /// either case, the definitions might be found within <paramref name="assembly"/> (or
        /// <paramref name="referenceAssembly"/>) anyway.
        /// </param>
        /// <param name="targetContractNodes">
        /// This parameter is used for the definitions of the contract methods (and
        /// attributes) that are the definitions that are embedded within the contracts,
        /// e.g., the definition of the ForAll method are found in the return value.
        /// It may be the same as <paramref name="contracts"/>. If it is passed in as null,
        /// then it will be filled in with the nodes used by the Extractor.
        /// </param>
        /// <param name="useClousotExtractor">
        /// When true, use a "Clousot" extractor, which does some extra processing needed for static
        /// analysis.
        /// </param>
        /// <returns>
        /// True iff extraction was possible. (I.e., a set of contractNodes was found to use for
        /// the extraction --- it does *not* guarantee that any contracts were found in the assembly.)
        /// </returns>
        /// </summary>
        ///
        [ContractVerification(false)]
        public static bool ExtractContracts(AssemblyNode /*!*/ assembly,
            AssemblyNode /*?*/ referenceAssembly,
            ContractNodes /*?*/ contracts,
            ContractNodes /*?*/ backupContracts,
            ContractNodes /*?*/ targetContractNodes,
            out ContractNodes /*?*/ contractNodesUsedToExtract,
            Action<CompilerError> /*?*/ errorHandler,
            bool useClousotExtractor)
        {
            Contract.Requires(assembly != null);

            AssemblyNode assemblyToVisit = referenceAssembly ?? assembly;

            // Try to use supplied contracts, if present. But don't just try extracting and somehow
            // figuring out if any contracts had been present. Instead, see if:
            //   a) the contract methods are defined in the assembly we are extracting from, or
            //   b) the assembly reference microsoft.contracts.dll (the backup contracts and we found that assembly)
            //   c) the assembly we are extracting from has an external reference to the assembly
            //      the supplied contract methods came from.
            //   d) the contracts found in mscorlib
            //

            // see if the assembly references the backup contracts (Microsoft.Contracts.dll)
            contractNodesUsedToExtract = IdentifyContractAssemblyIfReferenced(backupContracts, assemblyToVisit);

            // see if assembly defines the contracts itself
            if (contractNodesUsedToExtract == null)
            {
                contractNodesUsedToExtract = ContractNodes.GetContractNodes(assemblyToVisit, errorHandler);
            }

            // see if the assembly references the supplied contract assembly
            if (contractNodesUsedToExtract == null)
            {
                contractNodesUsedToExtract = IdentifyContractAssemblyIfReferenced(contracts, assemblyToVisit);
            }

            // see if the contracts are in the system assembly
            if (contractNodesUsedToExtract == null && assemblyToVisit != SystemTypes.SystemAssembly)
            {
                contractNodesUsedToExtract = ContractNodes.GetContractNodes(SystemTypes.SystemAssembly, errorHandler);
            }

            if (contractNodesUsedToExtract == null) return false;

            var fSharp = false;

            // TODO: Thread the program options through here somehow and let this be specified as an option
            Contract.Assume(assemblyToVisit.Attributes != null);
            foreach (var attr in assemblyToVisit.Attributes)
            {
                Contract.Assume(attr != null);
                Contract.Assume(attr.Type != null);
                Contract.Assume(attr.Type.Name != null);
                Contract.Assume(attr.Type.Name.Name != null);

                if (attr.Type.Name.Name.Contains("FSharpInterfaceDataVersionAttribute"))
                {
                    fSharp = true;
                    break;
                }
            }

            var ultimateTarget = (referenceAssembly != null) ? assembly : null;

            Contract.Assert(assembly != null);

            ExtractorVisitor ev = useClousotExtractor
                ? new ClousotExtractor(contractNodesUsedToExtract, ultimateTarget, assembly, errorHandler)
                : new ExtractorVisitor(contractNodesUsedToExtract, ultimateTarget, assembly, false, fSharp);

            ev.Visit(assemblyToVisit);

            if (!useClousotExtractor)
            {
                FilterForRuntime eoar = new FilterForRuntime(contractNodesUsedToExtract, targetContractNodes);
                assemblyToVisit = eoar.TransformForTarget(assemblyToVisit);
            }

            if (referenceAssembly != null)
            {
                CopyOutOfBandContracts coob = new CopyOutOfBandContracts(assembly, referenceAssembly,
                    contractNodesUsedToExtract, targetContractNodes);
                coob.VisitAssembly(referenceAssembly);
            }

            return true;
        }

        private static ContractNodes IdentifyContractAssemblyIfReferenced(ContractNodes contracts, AssemblyNode assemblyToVisit)
        {
            Contract.Requires(assemblyToVisit != null);

            if (contracts != null)
            {
                AssemblyNode assemblyContractsLiveIn = contracts.ContractClass == null
                    ? null
                    : contracts.ContractClass.DeclaringModule as AssemblyNode;

                if (assemblyContractsLiveIn != null)
                {
                    if (assemblyContractsLiveIn == assemblyToVisit)
                    {
                        return contracts;
                    }
                    
                    string nameOfAssemblyContainingContracts = assemblyContractsLiveIn.Name;
                    
                    Contract.Assume(assemblyToVisit.AssemblyReferences != null);
                    
                    foreach (var ar in assemblyToVisit.AssemblyReferences)
                    {
                        Contract.Assume(ar != null);

                        if (ar.Name == nameOfAssemblyContainingContracts)
                        {
                            // just do name matching to avoid loading the referenced assembly
                            return contracts;
                        }
                    }
                }
            }

            return null;
        }
    }
}


