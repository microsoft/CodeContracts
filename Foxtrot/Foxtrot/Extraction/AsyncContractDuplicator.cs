// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    internal class AsyncContractDuplicator : Duplicator
    {
        private TypeNode _parentType;

        public AsyncContractDuplicator(TypeNode parentType, Module module)
            : base(module, parentType)
        {
            _parentType = parentType;
        }

        public override Expression VisitLocal(Local local)
        {
            if (HelperMethods.IsClosureType(_parentType, local.Type))
            {
                // don't copy local as we need to share this closure local
                return local;
            }

            return base.VisitLocal(local);
        }
    }
}