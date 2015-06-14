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

// *****************************************************
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.Research.AskCodeContracts
{
  [Export(typeof(IVsTextViewCreationListener))]
  [ContentType("CSharp")]
  [ContentType("RoslynCSharp")]
  [ContentType("Basic")]
  [ContentType("RoslynVisualBasic")]
  [ContentType("Text")]
  [TextViewRole(PredefinedTextViewRoles.Editable)]
  internal sealed class VsTextViewListener : IVsTextViewCreationListener
  {
    private readonly IVsEditorAdaptersFactoryService editorAdaptersFactory;

    [ImportingConstructor]
    public VsTextViewListener(
        IVsEditorAdaptersFactoryService editorAdaptersFactory)
    {
      this.editorAdaptersFactory = editorAdaptersFactory;
    }

    public void VsTextViewCreated(IVsTextView textViewAdapter)
    {
      var wpfTextView = editorAdaptersFactory.GetWpfTextView(textViewAdapter);

      // The lifetime of CommandFilter is the married the view
      wpfTextView.Properties.AddProperty(
          typeof(CommandFilter),
          new CommandFilter(textViewAdapter, wpfTextView));
    }
  }
}
