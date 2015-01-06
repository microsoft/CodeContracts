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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Cci;
using Microsoft.Cci.MutableCodeModel;
using System.Diagnostics.Contracts;

namespace CSharp2CCI {

  /// <summary>
  /// A thin wrapper for Roslyn source locations
  /// </summary>
  public sealed class SourceLocationWrapper : IPrimarySourceLocation, IDocument, IPrimarySourceDocument {

    SyntaxTree tree;
    Microsoft.CodeAnalysis.Text.TextSpan span;

    public SourceLocationWrapper(SyntaxTree tree, Microsoft.CodeAnalysis.Text.TextSpan span) {
      this.tree = tree;
      this.span = span;
    }

    #region IPrimarySourceLocation members
    public int EndColumn {
      get { return this.tree.GetLineSpan(this.span, true).EndLinePosition.Character + 1; }
    }

    public int EndLine {
      get { return this.tree.GetLineSpan(this.span, true).EndLinePosition.Line + 1;  }
    }

    public IPrimarySourceDocument PrimarySourceDocument {
      get { return this; }
    }

    public int StartColumn {
      get { return this.tree.GetLineSpan(this.span, true).StartLinePosition.Character + 1; }
    }

    public int StartLine {
      get { return this.tree.GetLineSpan(this.span, true).StartLinePosition.Line + 1; }
    }

    public bool Contains(ISourceLocation location) {
      throw new NotImplementedException();
    }

    public int CopyTo(int offset, char[] destination, int destinationOffset, int length) {
      throw new NotImplementedException();
    }

    public int EndIndex {
      get { return this.span.End; }
    }

    public int Length {
      get { return this.span.Length; }
    }

    public ISourceDocument SourceDocument {
      get { throw new NotImplementedException(); }
    }

    public string Source {
      get { return this.tree.GetText().GetSubText(this.span).ToString(); }
    }

    public int StartIndex {
      get { return this.span.Start; }
    }

    public IDocument Document {
      get { return this; }
    }
    #endregion

    #region IDocument members
    // TODO: Need the full location
    public string Location {
      get { return this.tree.FilePath; }
    }

    public IName Name {
      get { throw new NotImplementedException(); }
    }
    #endregion

    #region IPrimarySourceDocument
    // TODO
    public Guid DocumentType {
      get { return Guid.Empty; }
    }

    // TODO
    public Guid Language {
      get { return Guid.Empty; }
    }

    // TODO
    public Guid LanguageVendor {
      get { return Guid.Empty; }
    }

    public IPrimarySourceLocation PrimarySourceLocation {
      get { throw new NotImplementedException(); }
    }

    public IPrimarySourceLocation GetPrimarySourceLocation(int position, int length) {
      throw new NotImplementedException();
    }

    public void ToLineColumn(int position, out int line, out int column) {
      throw new NotImplementedException();
    }


    public ISourceLocation GetCorrespondingSourceLocation(ISourceLocation sourceLocationInPreviousVersionOfDocument) {
      throw new NotImplementedException();
    }

    public ISourceLocation GetSourceLocation(int position, int length) {
      throw new NotImplementedException();
    }

    public string GetText() {
      throw new NotImplementedException();
    }

    public bool IsUpdatedVersionOf(ISourceDocument sourceDocument) {
      throw new NotImplementedException();
    }

    public string SourceLanguage {
      get { throw new NotImplementedException(); }
    }

    public ISourceLocation SourceLocation {
      get { throw new NotImplementedException(); }
    }
    #endregion
  }

  public sealed class SourceLocationProvider : ISourceLocationProvider {

    public IEnumerable<IPrimarySourceLocation> GetPrimarySourceLocationsFor(IEnumerable<ILocation> locations) {
      foreach (ILocation location in locations) {
        foreach (IPrimarySourceLocation psloc in this.GetPrimarySourceLocationsFor(location))
          yield return psloc;
      }
    }

    public IEnumerable<IPrimarySourceLocation> GetPrimarySourceLocationsFor(ILocation location) {
      IPrimarySourceLocation/*?*/ psloc = location as IPrimarySourceLocation;
      if (psloc != null) {
        IIncludedSourceLocation /*?*/ iloc = psloc as IncludedSourceLocation;
        if (iloc != null)
          yield return new OriginalSourceLocation(iloc);
        else
          yield return psloc;
      } else {
        IDerivedSourceLocation/*?*/ dsloc = location as IDerivedSourceLocation;
        if (dsloc != null) {
          foreach (IPrimarySourceLocation psl in dsloc.PrimarySourceLocations) {
            IIncludedSourceLocation /*?*/ iloc = psl as IncludedSourceLocation;
            if (iloc != null)
              yield return new OriginalSourceLocation(iloc);
            else
              yield return psl;
          }
        }
        var esloc = location as IExpressionSourceLocation;
        if (esloc != null) {
          yield return esloc.PrimarySourceLocation;
        }
      }
    }

    public IEnumerable<IPrimarySourceLocation> GetPrimarySourceLocationsForDefinitionOf(ILocalDefinition localDefinition) {
      LocalDefinition locDef = localDefinition as LocalDefinition;
      return Enumerable<IPrimarySourceLocation>.Empty;
    }

    public string GetSourceNameFor(ILocalDefinition localDefinition, out bool isCompilerGenerated) {
      isCompilerGenerated = localDefinition.Name == Dummy.Name;
      return localDefinition.Name.Value;
    }

  }

}
