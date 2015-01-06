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
using System.ComponentModel.Composition;
//using System.Linq;
using System.Threading;
using RoslynToCCICodeModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CodeActions.Providers;

namespace ClousotExtension {
  [ExportCodeIssueProvider("ClousotExtension", LanguageNames.CSharp)]
  public class AssemblyCodeIssueProvider : ICodeIssueProvider {

    private ClousotOptions options;

    [ImportingConstructor]
    public AssemblyCodeIssueProvider(ClousotOptions options) {
      this.connectionToClousot = new RoslynToCCICodeModel.ClousotGlue();
      this.options = options;
    }

    private RoslynToCCICodeModel.ClousotGlue connectionToClousot;

    public IEnumerable<System.Type> SyntaxNodeTypes {
      get {
        yield return typeof(CompilationUnitSyntax);
      }
    }

    public IEnumerable<int> SyntaxTokenKinds { get { return null; } }



    public IEnumerable<Microsoft.CodeAnalysis.CodeActions.CodeIssue> GetIssues(Document document, CommonSyntaxNode syntax, CancellationToken cancellationToken)
    {

      var compilationUnitDeclaration = (CompilationUnitSyntax)syntax;

      IEnumerable<ClousotOutput> results = null;
      try
      {
//        results = this.connectionToClousot.AnalyzeMeAUnit(document, compilationUnitDeclaration, cancellationToken, this.options, new string[] { "-cache" });
        results = this.connectionToClousot.AnalyzeMeAUnit(document, compilationUnitDeclaration, cancellationToken, this.options, null);
      }
      catch
      {
        // whatever happened, it couldn't have been that important...
        // just swallow the exception and return nothing
        yield break;
      }
      if (results == null) yield break;

      foreach (var r in GetIssuesInternal(results, compilationUnitDeclaration))
      {
        yield return r;
      }
    }

    private IEnumerable<Microsoft.CodeAnalysis.CodeActions.CodeIssue> GetIssuesInternal(IEnumerable<ClousotOutput> results, CompilationUnitSyntax compilationUnitDeclaration)
    {
      IEnumerator<ClousotOutput> enumerator;

      try
      {
        enumerator = results.GetEnumerator();
      }
      catch
      {
        yield break;
      }

      bool hasNext;

      try
      {
        hasNext = enumerator.MoveNext();
      }
      catch
      {
        yield break;
      }

      while (hasNext)
      {
        var result = enumerator.Current;
        
        //foreach (var result in results )
        {
          var msg = result.Message;
          var span = result.Span;
          var action = result.action;
          if (span.IsEmpty)
          {
            msg = "Unknown location: " + msg;
            span = compilationUnitDeclaration.GetFirstToken().Span;
          }
          if (action == null)
            yield return new Microsoft.CodeAnalysis.CodeActions.CodeIssue(Microsoft.CodeAnalysis.CodeActions.CodeIssueKind.Warning, span, msg);
          else
            yield return new Microsoft.CodeAnalysis.CodeActions.CodeIssue(Microsoft.CodeAnalysis.CodeActions.CodeIssueKind.Warning, span, msg, action);
        }
        try
        {
          hasNext = enumerator.MoveNext();
        }
        catch
        {
          yield break;
        }
      }
    }

    #region Unimplemented ICodeIssueProvider members

    public IEnumerable<Microsoft.CodeAnalysis.CodeActions.CodeIssue> GetIssues(Microsoft.CodeAnalysis.Document document, CommonSyntaxToken syntax, CancellationToken cancellationToken) {
      throw new NotImplementedException();
    }

    public IEnumerable<Microsoft.CodeAnalysis.CodeActions.CodeIssue> GetIssues(Microsoft.CodeAnalysis.Document document, CommonSyntaxTrivia syntax, CancellationToken cancellationToken) {
      throw new NotImplementedException();
    }

    #endregion
  }

  
  //[ExportSyntaxNodeCodeIssueProvider("ClousotExtension", LanguageNames.CSharp, typeof(MethodDeclarationSyntax))]
  //public class MethodCodeIssueProvider : ICodeIssueProvider {

  //  private RoslynToCCICodeModel.ClousotGlue connectionToClousot;

  //  public MethodCodeIssueProvider() {
  //    this.connectionToClousot = new RoslynToCCICodeModel.ClousotGlue();
  //  }

  //  public IEnumerable<CodeIssue> GetIssues(IDocument document, CommonSyntaxNode syntax, CancellationToken cancellationToken) {

  //    // Don't even look at the method if it has syntax errors.
  //    if (syntax.HasDiagnostics) yield break;

  //    var methodDeclaration = (MethodDeclarationSyntax)syntax;

  //    var semanticModel = document.GetSemanticModel(cancellationToken);

  //    // Don't even look at the method if it has semantic errors.
  //    var diagnostics = semanticModel.GetDiagnostics(cancellationToken);
  //    if (0 < diagnostics.Count()) yield break;

  //    foreach (var result in this.connectionToClousot.AnalyzeMeAMethod(document, methodDeclaration, cancellationToken)) {
  //      var msg = result.Item1;
  //      var span = result.Item2;
  //      if (span.IsEmpty) {
  //        msg = "Unknown location: " + msg;
  //        span = methodDeclaration.Identifier.Span;
  //      }
  //      yield return new CodeIssue(CodeIssue.Severity.Warning, span, msg);
  //    }
  //  }

  //  #region Unimplemented ICodeIssueProvider members

  //  public IEnumerable<CodeIssue> GetIssues(IDocument document, CommonSyntaxToken syntax, CancellationToken cancellationToken) {
  //    throw new NotImplementedException();
  //  }

  //  public IEnumerable<CodeIssue> GetIssues(IDocument document, CommonSyntaxTrivia syntax, CancellationToken cancellationToken) {
  //    throw new NotImplementedException();
  //  }

  //  #endregion
  //}
}
