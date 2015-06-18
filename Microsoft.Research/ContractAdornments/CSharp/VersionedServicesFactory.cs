// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ContractAdornments.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Adornments;
    using ContractAdornments.Interfaces;
    using Microsoft.VisualStudio.Language.Intellisense;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;

#if !ROSLYN
    using Microsoft.RestrictedUsage.CSharp.Compiler;
    using Microsoft.RestrictedUsage.CSharp.Compiler.IDE;
#endif

    internal sealed class VersionedServicesFactory : IVersionedServicesFactory
    {
        public ICompilerHost CreateCompilerHost()
        {
#if ROSLYN
            throw new NotSupportedException();
#else
            return new CompilerHostShim(new IDECompilerHost());
#endif
        }

        public ITextViewTracker CreateTextViewTracker(IWpfTextView textView, IProjectTracker projectTracker, VSTextProperties vsTextProperties)
        {
#if ROSLYN
            throw new NotSupportedException();
#else
            return new TextViewTracker(textView, projectTracker, vsTextProperties);
#endif
        }

        public IContractsProvider CreateContractsProvider(IProjectTracker projectTracker)
        {
#if ROSLYN
            throw new NotSupportedException();
#else
            return new ContractsProvider(projectTracker);
#endif
        }


        public IQuickInfoSource CreateQuickInfoSource(ITextBuffer textBuffer, ITextViewTracker textViewTracker)
        {
#if ROSLYN
            throw new NotSupportedException();
#else
            return new QuickInfoSource(textBuffer, textViewTracker);
#endif
        }

        public ISignatureHelpSource CreateSignatureHelpSource(ITextBuffer textBuffer, ITextViewTracker textViewTracker)
        {
#if ROSLYN
            throw new NotSupportedException();
#else
            return new SignatureHelpSource(textBuffer, textViewTracker);
#endif
        }

#if !ROSLYN
        private class CompilerHostShim : ICompilerHost
        {
            private readonly IDECompilerHost _compilerHost;

            public CompilerHostShim(IDECompilerHost compilerHost)
            {
                _compilerHost = compilerHost;
            }

            public IEnumerable<ICompiler> Compilers
            {
                get
                {
                    return _compilerHost.Compilers.Select(i => new CompilerShim(i));
                }
            }
        }

        private class CompilerShim : ICompiler
        {
            private readonly Compiler _compiler;

            public CompilerShim(Compiler compiler)
            {
                _compiler = compiler;
            }

            public object GetCompilation()
            {
                return _compiler.GetCompilation();
            }
        }
#endif
    }
}
