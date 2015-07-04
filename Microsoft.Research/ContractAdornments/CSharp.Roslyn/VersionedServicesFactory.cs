// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ContractAdornments.CSharp
{
    using System;
    using System.Collections.Generic;
    using Adornments;
    using ContractAdornments.Interfaces;
    using Microsoft.VisualStudio.Language.Intellisense;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.CodeAnalysis.Text;

    internal sealed class VersionedServicesFactory : IVersionedServicesFactory
    {
        public ICompilerHost CreateCompilerHost()
        {
            return new RoslynCompilerHost();
        }

        public ITextViewTracker CreateTextViewTracker(IWpfTextView textView, IProjectTracker projectTracker, VSTextProperties vsTextProperties)
        {
            return new TextViewTracker(textView, projectTracker, vsTextProperties);
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

        private sealed class RoslynCompilerHost : ICompilerHost
        {
            public IEnumerable<ICompiler> Compilers
            {
                get
                {
                    yield return new RoslynCompiler();
                }
            }
        }

        private sealed class RoslynCompiler : ICompiler
        {
            public object GetCompilation(ITextBuffer textBuffer)
            {
                return textBuffer.GetWorkspace();
            }
        }
    }
}
