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

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using Adornments;
using ContractAdornments.Interfaces;
using Microsoft.RestrictedUsage.CSharp.Core;
using Microsoft.RestrictedUsage.CSharp.Extensions;
using Microsoft.RestrictedUsage.CSharp.Syntax;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

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

    public static SnapshotSpan Convert(this CSharpSpan span, ITextSnapshot snapshot) {
      Contract.Requires(snapshot != null);
      var startIndex = snapshot.GetPositionFromLineColumn(span.Start.Line, span.Start.Character);
      var endIndex = snapshot.GetPositionFromLineColumn(span.End.Line, span.End.Character);
      // still need to do range check: have seen this be too long
      var len = endIndex - startIndex;
      var maxLen = snapshot.Length - startIndex;
      var usableLength = len > maxLen ? maxLen : len; // min(len,maxLen)
      return new SnapshotSpan(snapshot, startIndex, usableLength);
    }
    
    public static Position Convert(this ITrackingPoint point, ITextSnapshot snapshot) {
      Contract.Requires(point != null);
      
      var snapshotPoint = point.GetPoint(snapshot);
      var line = snapshotPoint.GetContainingLine();
      if (line == null) return default(Position);
      int lineIndex = line.LineNumber;
      int charIndex = snapshotPoint.Position - line.Start.Position;
      return new Position(lineIndex, charIndex);
    }

    public static int GetPositionFromLineColumn(this ITextSnapshot snapshot, int line, int column) {
      Contract.Requires(snapshot != null);

      var textline = snapshot.GetLineFromLineNumber(line);
      if (textline == null) return 0;
      return textline.Start.Position + column;
    }

    public static string GetName(this PropertyDeclarationNode @this, ITextSnapshot snapshot) {
      Contract.Requires(@this != null);
      Contract.Requires(snapshot != null);

      var nameNode = @this.MemberName;
      if (nameNode != null)
      {
        var nameNodeSpan = nameNode.GetSpan();
        return nameNodeSpan.Convert(snapshot).GetText();
      }

      return null;
    }
    public static string GetName(this MethodDeclarationNode @this, ITextSnapshot snapshot) {
      Contract.Requires(@this != null);
      Contract.Requires(snapshot != null);

      var nameNode = @this.MemberName;
      if (nameNode != null)
      {
        var nameNodeSpan = nameNode.GetSpan();
        return nameNodeSpan.Convert(snapshot).GetText();
      }

      return null;
    }
    public static string GetName(this TypeBaseNode @this, ITextSnapshot snapshot)
    {
      Contract.Requires(snapshot != null);

      var span = @this.GetSpan();
      var nSpan = span.Convert(snapshot);
      var text = nSpan.GetText();
      if (text != null)
      {
        return text;
      }
      return null;
    }
    public static bool IsModelReady(this ParseTree parseTree) {
      Contract.Requires(parseTree != null);

      try {
        if (parseTree == null)
          return false;

        var rootNode = parseTree.RootNode;

      } catch (InvalidOperationException e) {
        if (e.Message.Contains(ContractsPackageAccessor.InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate))
          return false;
        else
          throw e;
      } catch (COMException e) {
        if (e.Message.Contains(ContractsPackageAccessor.COMExceptionMessage_BindingFailed))
          return false;
        else
          throw e;
      }

      return true;
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