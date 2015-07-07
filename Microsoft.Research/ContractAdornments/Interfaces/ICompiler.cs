// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ContractAdornments.Interfaces
{
    using Microsoft.VisualStudio.Text;

    public interface ICompiler
    {
        object GetCompilation(ITextBuffer textBuffer);
    }
}
