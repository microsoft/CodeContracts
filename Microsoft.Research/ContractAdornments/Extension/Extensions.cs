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

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System.Diagnostics.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Adornments;
using ContractAdornments.Interfaces;
using System.Runtime.InteropServices;

namespace ContractAdornments {
  static class HelperExtensions {
    public static string GetFileName(this ITextView @this) {
      Contract.Requires(@this != null);

      if (@this.TextBuffer == null) return null;
      return @this.TextBuffer.GetFileName();
    }
    public static string GetFileName(this ITextBuffer @this) {
      Contract.Requires(@this != null);
      ITextDocument doc;
      if (@this.Properties == null) return null;
      if (@this.Properties.TryGetProperty<ITextDocument>(typeof(ITextDocument), out doc)) {
        Contract.Assume(doc != null);
        if (doc.FilePath == null) { return null; }
        return doc.FilePath.ToLower();
      }
      return null;
    }

    public static void RefreshLineTransformer(this ITextView textView) {
      Contract.Requires(textView != null);

      if (textView.TextViewLines == null) return;
      var line = textView.TextViewLines.FirstVisibleLine;
      if (line == null) return;
      textView.DisplayTextLineContainingBufferPosition(line.Start, line.Top - textView.ViewportTop, ViewRelativePosition.Top);
    }
  }
  public static class AdornmentOptionsHelper {
    public static AdornmentOptions GetAdornmentOptions(IContractOptionsPage options) {
      var result = AdornmentOptions.None;

      if (options == null)
        return result;

      if (options.SmartFormatting)
        result = result | AdornmentOptions.SmartFormatting;

      if (options.SyntaxColoring)
        result = result | AdornmentOptions.SyntaxColoring;

      if (options.CollapseMetadataContracts)
      {
        result = result | AdornmentOptions.CollapseWithRegion;
      }
      return result;
    }
  }
}