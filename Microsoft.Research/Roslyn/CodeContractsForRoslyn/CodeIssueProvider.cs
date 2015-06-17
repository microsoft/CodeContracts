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

using System.Threading;
using RoslynToCCICodeModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ClousotExtension {
    
    [ExportDiagnosticProvider("ClousotExtension", LanguageNames.CSharp)]
  //[ExportCodeFixProvider("ClousotExtension", LanguageNames.CSharp)]
  public class AssemblyCodeIssueProvider : IDiagnosticProvider/*, ICodeFixProvider*/ {

    internal static ClousotOptions options;

    [ImportingConstructor]
    public AssemblyCodeIssueProvider(ClousotOptions options) {
      this.connectionToClousot = new RoslynToCCICodeModel.ClousotGlue();
      AssemblyCodeIssueProvider.options = options;
    }

    private RoslynToCCICodeModel.ClousotGlue connectionToClousot;

    //public IEnumerable<System.Type> SyntaxNodeTypes {
    //  get {
    //    yield return typeof(CompilationUnitSyntax);
    //  }
    //}

    bool IDiagnosticProvider.IsSupported(DiagnosticCategory category)
    {
        return category == DiagnosticCategory.SemanticInDocument;
    }

    public async Task<System.Collections.Generic.IEnumerable<Diagnostic>> GetSemanticDiagnosticsAsync(Document document, TextSpan span, CancellationToken cancellationToken)
    {
        var diagnostics = new List<Diagnostic>();

        var model = await document.GetSemanticModelAsync(cancellationToken);
        var root = await document.GetSyntaxRootAsync(cancellationToken);

        CompilationUnitSyntax compilationUnitDeclaration = root as CompilationUnitSyntax;
        if (compilationUnitDeclaration == null)
            return diagnostics;

        IEnumerable<ClousotOutput> results = null;
        try
        {
            await Task.Run(() => results = this.connectionToClousot.AnalyzeMeAUnit(document, compilationUnitDeclaration, cancellationToken, options, null));
        }
        catch (OperationCanceledException)
        {
            throw; // tell Roslyn
        }
        catch (Exception)
        {
            // whatever happened, it couldn't have been that important...
            // just swallow the exception and return nothing
            return null;
        }
        if (results == null) return null;
        foreach (var r in results)
        {
          var location = Location.Create(document.GetSyntaxTree(), r.Span);
          var d = Diagnostic.Create(
              id: "Clousot",
              kind: "Clousot",
              message: r.Message,
              severity: r.Kind,
              location: location
              );
          diagnostics.Add(d);
        }
        return diagnostics;
    }

    #region Unimplmented members of IDiagnosticProvider
    Task<IEnumerable<Diagnostic>> IDiagnosticProvider.GetProjectDiagnosticsAsync(Project project, CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }

    Task<IEnumerable<Diagnostic>> IDiagnosticProvider.GetSyntaxDiagnosticsAsync(Document document, CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }
    #endregion

    public IEnumerable<int> SyntaxTokenKinds { get { return null; } }

    //    public async Task<IEnumerable<Microsoft.CodeAnalysis.CodeActions.CodeAction>> GetFixesAsync(Document document, TextSpan span, IEnumerable<Diagnostic> diagnostics, CancellationToken cancellationToken)
    //{
    //    var compilationUnitDeclaration = node as CompilationUnitSyntax;

    //    if (compilationUnitDeclaration == null) return null;

    //    IEnumerable<ClousotOutput> results = null;
    //    try
    //    {
    //        await Task.Run(() => results = this.connectionToClousot.AnalyzeMeAUnit(document, compilationUnitDeclaration, cancellationToken, options, null));
    //    }
    //    catch (OperationCanceledException)
    //    {
    //        throw; // tell Roslyn
    //    }
    //    catch (Exception)
    //    {
    //        // whatever happened, it couldn't have been that important...
    //        // just swallow the exception and return nothing
    //        return null;
    //    }
    //    if (results == null) return null;

    //    var issues = GetIssuesInternal(results, compilationUnitDeclaration, cancellationToken);
    //    return issues;
    //}

        //private IEnumerable<Microsoft.CodeAnalysis.CodeActions.CodeAction> GetIssuesInternal(IEnumerable<ClousotOutput> results, CompilationUnitSyntax compilationUnitDeclaration, CancellationToken cancellationToken)
        //{
        //    IEnumerator<ClousotOutput> enumerator;

        //    try
        //    {
        //        enumerator = results.GetEnumerator();
        //    }
        //    catch
        //    {
        //        yield break;
        //    }

        //    bool hasNext;

        //    try
        //    {
        //        hasNext = enumerator.MoveNext();
        //    }
        //    catch
        //    {
        //        yield break;
        //    }

        //    while (hasNext)
        //    {
        //        if (cancellationToken.IsCancellationRequested) yield break;

        //        var result = enumerator.Current;

        //        //foreach (var result in results )
        //        {
        //            var msg = result.Message;
        //            var span = result.Span;
        //            var action = result.action;
        //            if (span.IsEmpty)
        //            {
        //                msg = "Unknown location: " + msg;
        //                span = compilationUnitDeclaration.GetFirstToken().Span;
        //            }
        //            if (action == null)
        //                yield return CodeAction.Create(result.Kind, span, msg);
        //            else
        //                yield return CodeAction.Create(result.Kind, span, msg, action);
        //        }
        //        try
        //        {
        //            hasNext = enumerator.MoveNext();
        //        }
        //        catch
        //        {
        //            yield break;
        //        }
        //    }
        //}

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
