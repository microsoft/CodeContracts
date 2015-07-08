// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ContractAdornments
{
    using System.Diagnostics.Contracts;
    using Adornments;
    using ContractAdornments.Interfaces;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;

    public static class TextViewTrackerAccessor
    {
        public static readonly object TextViewTrackerKey = typeof(ITextViewTracker);

        [ContractVerification(false)]
        public static ITextViewTracker GetOrCreateTextViewTracker(IWpfTextView textView, IProjectTracker projectTracker, VSTextProperties vsTextProperties)
        {
            Contract.Requires(textView != null);
            Contract.Requires(projectTracker != null);
            Contract.Ensures(Contract.Result<ITextViewTracker>() != null);
            return textView.TextBuffer.Properties.GetOrCreateSingletonProperty<ITextViewTracker>(TextViewTrackerAccessor.TextViewTrackerKey, delegate
            {
                return ContractsPackageAccessor.Current.GetVersionedServicesFactory().CreateTextViewTracker(textView, projectTracker, vsTextProperties);
            });
        }

        public static bool TryGetTextViewTracker(ITextBuffer textBuffer, out ITextViewTracker textViewTracker)
        {
            Contract.Requires(textBuffer != null);
            if (textBuffer.Properties == null)
            {
                textViewTracker = null;
                return false;
            }

            return textBuffer.Properties.TryGetProperty(TextViewTrackerAccessor.TextViewTrackerKey, out textViewTracker);
        }
    }
}
