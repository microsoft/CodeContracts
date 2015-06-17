namespace ContractAdornments.CSharp
{
    using System.Collections.Generic;
    using System.Linq;
    using Adornments;
    using ContractAdornments.Interfaces;
    using Microsoft.RestrictedUsage.CSharp.Compiler;
    using Microsoft.RestrictedUsage.CSharp.Compiler.IDE;
    using Microsoft.VisualStudio.Language.Intellisense;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;

    internal sealed class VersionedServicesFactory : IVersionedServicesFactory
    {
        public ICompilerHost CreateCompilerHost()
        {
            return new CompilerHostShim(new IDECompilerHost());
        }

        public ITextViewTracker CreateTextViewTracker(IWpfTextView textView, IProjectTracker projectTracker, VSTextProperties vsTextProperties)
        {
            return new TextViewTracker(textView, projectTracker, vsTextProperties);
        }

        public IContractsProvider CreateContractsProvider(IProjectTracker projectTracker)
        {
            return new ContractsProvider(projectTracker);
        }


        public IQuickInfoSource CreateQuickInfoSource(ITextBuffer textBuffer, ITextViewTracker textViewTracker)
        {
            return new QuickInfoSource(textBuffer, textViewTracker);
        }

        public ISignatureHelpSource CreateSignatureHelpSource(ITextBuffer textBuffer, ITextViewTracker textViewTracker)
        {
            return new SignatureHelpSource(textBuffer, textViewTracker);
        }

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
    }
}
