namespace ContractAdornments.Interfaces
{
    using Adornments;
    using Microsoft.VisualStudio.Language.Intellisense;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;

    public interface IVersionedServicesFactory
    {
        ICompilerHost CreateCompilerHost();

        ITextViewTracker CreateTextViewTracker(IWpfTextView textView, IProjectTracker projectTracker, VSTextProperties vsTextProperties);

        IContractsProvider CreateContractsProvider(IProjectTracker projectTracker);

        IQuickInfoSource CreateQuickInfoSource(ITextBuffer textBuffer, ITextViewTracker textViewTracker);

        ISignatureHelpSource CreateSignatureHelpSource(ITextBuffer textBuffer, ITextViewTracker textViewTracker);
    }
}
