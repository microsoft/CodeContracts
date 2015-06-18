// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ContractAdornments.Interfaces
{
    public interface IContractOptionsPage
    {
        bool SmartFormatting
        {
            get;
        }

        bool SyntaxColoring
        {
            get;
        }

        bool CollapseMetadataContracts
        {
            get;
        }

        bool Caching
        {
            get;
        }

#if false
        bool InheritanceOnMethods
        {
            get;
        }

        bool InheritanceOnProperties
        {
            get;
        }
#endif
    }
}
