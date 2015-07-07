// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using ContractAdornments.Interfaces;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace ContractAdornments {
  [Export(typeof(ISignatureHelpSourceProvider))]
  [Name("Code Contracts SignatureHelpSourceProvider")]
  [Order(After = "Default Signature Help Source")]
  [Order(After = "Shim Signature Help Source")]
  [ContentType("Code")]
  class SignatureHelpSourceProvider : ISignatureHelpSourceProvider {

    public ISignatureHelpSource TryCreateSignatureHelpSource(ITextBuffer textBuffer) {
      Contract.Assume(textBuffer != null);

      if (VSServiceProvider.Current == null || VSServiceProvider.Current.ExtensionHasFailed) {
        //If the VSServiceProvider is not initialize, we can't do anything.
        return null;
      }

      return VSServiceProvider.Current.Logger.PublicEntry<ISignatureHelpSource>(() => {

        if (VSServiceProvider.Current.VSOptionsPage != null && !VSServiceProvider.Current.VSOptionsPage.SignatureHelp)
          return null;

        //Can we get the TextViewTracker?
        ITextViewTracker textViewTracker;
        if (TextViewTrackerAccessor.TryGetTextViewTracker(textBuffer, out textViewTracker))
          return VSServiceProvider.Current.GetVersionedServicesFactory().CreateSignatureHelpSource(textBuffer, textViewTracker);
        else
          return null;

      }, "TryCreateSignatureHelpSource");
    }
  }
}
