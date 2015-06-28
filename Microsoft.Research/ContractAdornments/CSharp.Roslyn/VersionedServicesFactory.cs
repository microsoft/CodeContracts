// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ContractAdornments.CSharp
{
    using System;
    using Adornments;
    using ContractAdornments.Interfaces;
    using Microsoft.VisualStudio.Language.Intellisense;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;

    internal sealed class VersionedServicesFactory : IVersionedServicesFactory
    {
        public ICompilerHost CreateCompilerHost()
        {
            throw new NotSupportedException();
        }

        public ITextViewTracker CreateTextViewTracker(IWpfTextView textView, IProjectTracker projectTracker, VSTextProperties vsTextProperties)
        {
            throw new NotSupportedException();
        }

        public IContractsProvider CreateContractsProvider(IProjectTracker projectTracker)
        {
            throw new NotSupportedException();
        }


        public IQuickInfoSource CreateQuickInfoSource(ITextBuffer textBuffer, ITextViewTracker textViewTracker)
        {
            throw new NotSupportedException();
        }

        public ISignatureHelpSource CreateSignatureHelpSource(ITextBuffer textBuffer, ITextViewTracker textViewTracker)
        {
            throw new NotSupportedException();
        }
    }
}
