// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Research.CodeAnalysis
{
    public struct ContractDensity
    {
        /// <summary>
        /// Contains all contracts, not just Assert
        /// </summary>
        private ulong contracts;

        private ulong methodInstructions;

        private ulong contractInstructions;

        public ContractDensity(ulong methodInstructions, ulong contractInstructions, ulong contracts)
        {
            this.contracts = contracts;
            this.methodInstructions = methodInstructions;
            this.contractInstructions = contractInstructions;
        }

        public float Density
        {
            get
            {
                if (methodInstructions == 0) return 0;
                return ((float)contractInstructions) / methodInstructions;
            }
        }

        public ulong Contracts { get { return contracts; } }
        public ulong MethodInstructions { get { return methodInstructions; } }
        public ulong ContractInstructions { get { return contractInstructions; } }

        public void Add(ContractDensity other)
        {
            methodInstructions += other.methodInstructions;
            contractInstructions += other.contractInstructions;
            contracts += other.contracts;
        }
    }
}