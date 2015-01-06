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
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.CodeAnalysis.CSharp.Semantics;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Research.CodeAnalysis;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using CS = Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.Common.Symbols;
using Microsoft.CodeAnalysis.Common;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.ComponentModelHost;
using ClousotExtension;
using RoslynToCCICodeModel;

namespace Microsoft.Research.AskCodeContracts
{
  // Link with the VS UI
  internal sealed partial class CommandFilter : IOleCommandTarget
  {
    #region State

    private readonly IVsTextView vsTextView;
    private readonly IWpfTextView wpfTextView;
    private readonly IOleCommandTarget nextCommandTarget;
    private readonly ClousotOptions options;

    static private string lastQuery = null; // Made it static, so to keep it alive over different invocations

    #endregion

    #region Constructor

    public CommandFilter(
        IVsTextView vsTextView,
        IWpfTextView wpfTextView)
    {
      this.vsTextView = vsTextView;
      this.wpfTextView = wpfTextView;

      this.options = new ClousotOptions(); // TODO!!!!

      // Add command filter to IVsTextView. If something goes wrong, throw.
      int returnValue = vsTextView.AddCommandFilter(this, out nextCommandTarget);
      Marshal.ThrowExceptionForHR(returnValue);
    }

    #endregion

    #region Constants

    public const string ASKCLOUSOTTAG = "__Ask_Clousot__";

    public string CLOUSOT_QUERY_ASSERTION
    {
      get
      {
        return string.Format("Contract.Assert({0}, \"{1}\");", "{0}", ClousotGlue.ASKCLOUSOTTAG);
      }
    }

    public const string CLOUSOT_QUERY_ASSUMPTION = "Contract.Assume({0});";
    public const string CLOUSOT_DUMMY_METHODNAME_FOR_INVARIANT = "___ClousotInvariantAt";
    public const string CLOUSOT_QUERY_PRECONDITION = "Contract.Requires({0});";
    public const string USING_CONTRACTS = "using System.Diagnostics.Contracts;";

    #endregion

    #region Implementation of IOleCommandTarget

    // TODO: The BufferIsCSharp and BufferIsBasic functions below won't handle inherited content types or projection buffer scenarios properly.
    private bool BufferIsBasic()
    {
      var contentTypeName = wpfTextView.TextBuffer.ContentType.TypeName;
      return contentTypeName == "Basic" || contentTypeName == "RoslynVisualBasic";
    }

    private bool BufferIsCSharp()
    {
      var contentTypeName = wpfTextView.TextBuffer.ContentType.TypeName;
      return contentTypeName == "CSharp" || contentTypeName == "RoslynCSharp" || contentTypeName.Contains("C#");
    }

    private Span GetSelectedSpan()
    {
      return wpfTextView.Selection.SelectedSpans[0].Span;
    }

    public int QueryStatus(ref Guid pguidCmdGroup, uint commandCount, OLECMD[] prgCmds, IntPtr commandText)
    {

      if (pguidCmdGroup == Guids.AskCodeContractsCommandSetId)
      {
        switch (prgCmds[0].cmdID)
        {
          case CommandIDs.AnalyzeVBCommandId:
            if (BufferIsBasic())
            {
              prgCmds[0].cmdf = (uint)(OLECMDF.OLECMDF_ENABLED | OLECMDF.OLECMDF_SUPPORTED);
            }

            return VSConstants.S_OK;
          case CommandIDs.AnalyzeCSharpCommandId:
            if (BufferIsCSharp())
            {
              prgCmds[0].cmdf = (uint)(OLECMDF.OLECMDF_ENABLED | OLECMDF.OLECMDF_SUPPORTED);
            }

            return VSConstants.S_OK;
        }
      }

      return nextCommandTarget.QueryStatus(ref pguidCmdGroup, commandCount, prgCmds, commandText);
    }

    public int Exec(ref Guid pguidCmdGroup, uint commandId, uint executeInformation, IntPtr pvaIn, IntPtr pvaOut)
    {
      if (pguidCmdGroup == Guids.AskCodeContractsCommandSetId)
      {
        try
        {
          switch (commandId)
          {
            default:
              Trace.WriteLine(string.Format("Selected our entry, commandID: {0}", commandId));
              
              var myDialogBox = new IsTrue(wpfTextView, DoTheWork, DoTheWorkToInferInvariants, DoTheWorkToSearchTheCallers, lastQuery);
              myDialogBox.Show();

              return VSConstants.S_OK;
          }
        }
        catch (Exception ex)
        {
          throw new Exception("Error converting code.", ex);
        }
      }

      return nextCommandTarget.Exec(ref pguidCmdGroup, commandId, executeInformation, pvaIn, pvaOut);
    }
    #endregion

    #region Logic for query with the assumptions

    private ProofOutcome? DoTheWork(string query, IEnumerable<CodeAssumption> assumptions)
    {
      // We should move this in the UI
      var selectionStart = this.wpfTextView.Selection.Start.Position.Position;
      var selectionEnd = this.wpfTextView.Selection.End.Position.Position;

      if (selectionStart == selectionEnd)
      {
        Trace.WriteLine(string.Format("Selection: {0}", selectionStart));

        var container = wpfTextView.TextBuffer.AsTextContainer();
        //var text = wpfTextView.TextBuffer.CurrentSnapshot.AsText();
        EditorWorkspace workspace;
        EditorWorkspace.TryGetWorkspace(container, out workspace);
        //var workspaceDiscovery = Helper.GetMefService<IWorkspaceDiscoveryService>();
        //var workspace = workspaceDiscovery.GetWorkspace(container);

        if (workspace != null)
        {
          Document doc;
          DocumentId docId;
          if (workspace.TryGetDocumentId(container, out docId))
          {
            doc = Helper.OpenDocument(workspace, docId);
            var syntax = doc.GetSyntaxTree();
            var source = syntax.GetText().ToString();

            var newText = GenerateInstrumentedSourceText(query, assumptions, selectionStart, source);

            var newSolution = doc.Project.Solution.WithDocumentText(doc.Id, new StringText(newText));
            //var newSolution = doc.Project.Solution.UpdateDocument(doc.Id, new StringText(newText));
            var locDocument = newSolution.GetDocument(doc.Id);
            var newTree = (Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)locDocument.GetSyntaxTree().GetRoot();
            var adornedSource = GenerateAdornedSource(doc, selectionStart);

            if (!newTree.ContainsDiagnostics)
            {
              try
              {
                var clousotConnection = new RoslynToCCICodeModel.ClousotGlue();
                var run = clousotConnection.AnalyzeMeAUnit(locDocument,
                  newTree,
                  new System.Threading.CancellationToken(),
                  this.options,
                  new string[] { "-warnscores:false", "-sortwarns:false", "-show:validations", "-show:unreached" },
                  true // we want to filter all the messages but the answer of Clousot
                  );

                foreach (var r in run)
                {
                  Trace.WriteLine(string.Format("{0}: {1}", r.Message, r.outcome));

                  // if we have a non-null, then this is the outcome
                  // We can have outcome == null for e.g., suggestions. 
                  // In this case we simply ignore them
                  if (r.outcome.HasValue)
                  {
                    return r.outcome;
                  }
                }
              }
              catch (Exception e)
              {
                Trace.WriteLine(string.Format("Some exception was thrown inside Clousot...\nDetails:\n{0}", e.ToString()));
              }
            }
            else
            {
              Trace.WriteLine("Compilation error from Roslyn");
            }
          }
        }

      }
      else
      {
        Trace.WriteLine(string.Format("start/end do not match {0} {1}", selectionStart, selectionEnd));
      }

      return null;
    }

    private string GenerateInstrumentedSourceText(string query, IEnumerable<CodeAssumption> assumptions, int selectionStart, string source)
    {
      var assert = string.Format(CLOUSOT_QUERY_ASSERTION, query);

      Trace.WriteLine(string.Format("Generated Assertion: {0}", assert));

      // sort the assumptions
      var sortedAssumptions = from a in assumptions
                              orderby a.Position
                              select a;

      // add the assumptions
      var newSource = source;

      var offset = 0;
      var queryoffset = 0;
      foreach (var assumption in sortedAssumptions)
      {
        var textAssumption = string.Format(CLOUSOT_QUERY_ASSUMPTION, assumption.Condition);
        newSource = newSource.Insert(offset + assumption.Position, textAssumption);

        offset += textAssumption.Length;
        if (assumption.Position <= selectionStart)
        {
          queryoffset = offset;
        }

        Trace.WriteLine(string.Format("New source text after adding the assumption {0}: {1}", assumption.Condition, newSource));
      }

      // add the query as an assert
      newSource = newSource.Insert(queryoffset + selectionStart, assert);

      Trace.WriteLine(string.Format("New source text : {0}", newSource));

      return newSource;
    }



    #endregion

    #region Logic for the invariant suggestion

    // TODO: refactor this code to share with DoTheWork
    private string DoTheWorkToInferInvariants()
    {
      // We should move this in the UI
      var selectionStart = this.wpfTextView.Selection.Start.Position.Position;
      var selectionEnd = this.wpfTextView.Selection.End.Position.Position;

      if (selectionStart == selectionEnd)
      {
        Trace.WriteLine(string.Format("Selection: {0}", selectionStart));

        var container = wpfTextView.TextBuffer.AsTextContainer();
        //var text = wpfTextView.TextBuffer.CurrentSnapshot.AsText();
        EditorWorkspace workspace;
        EditorWorkspace.TryGetWorkspace(container, out workspace);
        //var workspaceDiscovery = Helper.GetMefService<IWorkspaceDiscoveryService>();
        //var workspace = workspaceDiscovery.GetWorkspace(container);

        if (workspace != null)
        {
          Document doc;
          DocumentId docId;
          if (workspace.TryGetDocumentId(container, out docId))
          {
            doc = Helper.OpenDocument(workspace, docId);
            var syntax = doc.GetSyntaxTree();
            var source = syntax.GetText().ToString();

            // Generate the new text
            var newText = GenerateAdornedSource(doc, selectionStart);

            var newSolution = doc.Project.Solution.WithDocumentText(doc.Id, new StringText(newText));
            //var newSolution = doc.Project.Solution.UpdateDocument(doc.Id, new StringText(newText));
            var locDocument = newSolution.GetDocument(doc.Id);
            var newTree = (CompilationUnitSyntax)locDocument.GetSyntaxTree().GetRoot();

            if (!newTree.ContainsDiagnostics)
            {
              try
              {
                Trace.WriteLine(string.Format("Annotated tree for the state:\n{0}", newText));

                var clousotConnection = new RoslynToCCICodeModel.ClousotGlue();
                var run = clousotConnection.AnalyzeMeAUnit(locDocument,
                  newTree,
                  new System.Threading.CancellationToken(),
                  this.options,
                  new string[] { "-warnscores:false", "-sortwarns:false", "-suggest:!!", "-invariantsuggestmode:true" },
                  true // we want to filter all the messages but the answer of Clousot
                  );

                foreach (var r in run)
                {
                  Trace.WriteLine(string.Format("{0}: {1}", r.Message, r.outcome));

                  if (r.IsInvariantAtSuggestion)
                  {
                    return PrettyFormat(r.Message);
                  }
                }
              }
              catch (Exception e)
              {
                Trace.WriteLine(string.Format("Some exception was thrown inside Clousot...\nDetails:\n{0}", e.ToString()));
              }
            }
            else
            {
              Trace.WriteLine("Compilation error from Roslyn");
            }
          }
        }

      }
      else
      {
        Trace.WriteLine(string.Format("start/end do not match {0} {1}", selectionStart, selectionEnd));
      }

      return null;
    }

    #region Utils
    private string GenerateAdornedSource(Document doc, int position)
    {
      var parameters = GetParameters(doc, position);
      var dummyDeclaration = GenerateDummyDeclaration(parameters.Item1);
      var dummyCall = GenerateDummyCall(parameters.Item2);

      var originalSource = doc.GetSyntaxTree().GetText().ToString();

      // Insert the dummy declaration
      var method = doc.GetSyntaxTree().GetRoot().FindToken(position).Parent.FirstAncestorOrSelf<MethodDeclarationSyntax>();
      if (method != null)
      {
        // Add the dummy method declaration after the current method
        var newSource = originalSource.Insert(method.Span.End, "\n" + dummyDeclaration);

        // Insert the call in the method
        var addMethodCall = newSource.Insert(position, dummyCall);

        // Insert the using Contracts
        return addMethodCall.Insert(0, USING_CONTRACTS);
      }

      return null;
    }

    /// <returns>
    /// First component are the formal parameters (with type), second are the actual parameters
    /// </returns>
    private Tuple<string, string> GetParameters(Document doc, int position)
    {
      // Get the locals, parameters, fields at position "selectionStart"
      var semanticModel = doc.GetSemanticModel();
      var symbols = semanticModel.LookupSymbols(position).Where(s => s.Kind == Microsoft.CodeAnalysis.Common.CommonSymbolKind.Local || s.Kind == Microsoft.CodeAnalysis.Common.CommonSymbolKind.Field || s.Kind == Microsoft.CodeAnalysis.Common.CommonSymbolKind.Parameter);

      var parameters = new List<Tuple<ITypeSymbol, ISymbol>>();

      // Get the symbols at that position
      foreach (var s in symbols)
      {
        ITypeSymbol t;
        if (s.TryGetType(out t))
        {
          parameters.Add(new Tuple<ITypeSymbol, ISymbol>(t, s));
        }
      }

      // Generate the parameters
      var formalParameters = new StringBuilder();
      var actualParameters = new StringBuilder();
      for (var i = 0; i < parameters.Count; i++)
      {
        var pair = parameters[i];
        var comma = i < parameters.Count - 1 ? ", " : "";
        var typeName = !string.IsNullOrEmpty(pair.Item1.Name) ? pair.Item1.Name : "object";
        formalParameters.AppendFormat("{0} {1}{2}", typeName, pair.Item2.Name, comma);
        actualParameters.AppendFormat("{0}{1}", pair.Item2.Name, comma);
      }

      return new Tuple<string, string>(formalParameters.ToString().AddParentheses(), actualParameters.ToString().AddParentheses());
    }

    private string GenerateDummyDeclaration(string parameters)
    {
      return string.Format("[Pure] void {0}{1} {2}", CLOUSOT_DUMMY_METHODNAME_FOR_INVARIANT, parameters, "{ }");
    }

    private string GenerateDummyCall(string parameters)
    {
      return string.Format("{0}{1};", CLOUSOT_DUMMY_METHODNAME_FOR_INVARIANT, parameters);
    }

    private string PrettyFormat(string input)
    {
      if (input == null)
      {
        return input;
      }
      var suggestionStr = "Suggestion:";

      var result = input;

      // remove suggestion prefix
      if (result.Contains(suggestionStr))
      {
        result = result.Remove(0, suggestionStr.Length);
      }

      // replace <= by ≤ etc.
      result = result.Replace("<=", "≤").Replace(">=", "≥").Replace("==", "=").Replace("!=", "≠");

      return result;
    }

    #endregion

    #endregion

    #region Logic for the caller search

    private IEnumerable<SearchResult> DoTheWorkToSearchTheCallers(string query)
    {
      // We should move this in the UI
      var selectionStart = this.wpfTextView.Selection.Start.Position.Position;
      var selectionEnd = this.wpfTextView.Selection.End.Position.Position;

      var results = new List<SearchResult>();

      if (selectionStart == selectionEnd)
      {
        Trace.WriteLine(string.Format("Selection: {0}", selectionStart));

        var container = wpfTextView.TextBuffer.AsTextContainer();
        EditorWorkspace workspace;
        EditorWorkspace.TryGetWorkspace(container, out workspace);

        if (workspace != null)
        {
          Document doc;
          DocumentId docId;
          if (workspace.TryGetDocumentId(container, out docId))
          {
            doc = Helper.OpenDocument(workspace, docId);
            var span = new Microsoft.CodeAnalysis.Text.TextSpan(selectionStart, 0);
            var syntaxNode = doc.GetSyntaxTree().GetRoot() as SyntaxNode;
            if (syntaxNode != null)
            {
              var methodFinder = EnclosingMethodFinder.GetMethodSpans(syntaxNode);
              BaseMethodDeclarationSyntax method;
              if (methodFinder.TryGetValueContainingSpan(span, out method))
              {
                // Add the precondition
                var preAsStr = string.Format(CLOUSOT_QUERY_PRECONDITION, query);
                var pre = SyntaxFactory.ParseStatement(preAsStr + Environment.NewLine + Environment.NewLine); // F: adding two newlines as Roslyn for some reason pretty print the text in a different way, and loses one line at the end of the file
                var newMethod = method.InsertStatements(pre, ContractKind.Precondition);
                var newRoot = ((SyntaxNode)doc.GetSyntaxTree().GetRoot()).ReplaceNode(method, newMethod);

                // Create the new Roslyn environment
                var newSolution = doc.Project.Solution.WithDocumentText(doc.Id, new StringText(newRoot.GetText().ToString()));
                //var newSolution = doc.Project.Solution.UpdateDocument(doc.Id, new StringText(newRoot.GetText()));
                var locDocument = newSolution.GetDocument(doc.Id);
                var newTree = (CompilationUnitSyntax)locDocument.GetSyntaxTree().GetRoot();

                try
                {
                  Trace.WriteLine(string.Format("Annotated tree for the state:\n{0}", newRoot.GetText()));

                  var run = new RoslynToCCICodeModel.ClousotGlue().AnalyzeMeAUnit(locDocument,
                    newTree,
                    new System.Threading.CancellationToken(),
                    this.options,
                    new string[] { "-warnscores:false", "-sortwarns:false", "-suggest:!!", "-show", "validations" },
                    false
                    );

                  foreach (var r in run)
                  {
                    Trace.WriteLine(string.Format("{0}: {1}", r.Message, r.outcome));

                    if (r.outcome.HasValue &&  // it is not a suggestion
                      r.startPoint != null &&
                      r.Message.Contains("Contract.Requires") && r.Message.Contains(query)) // we should do better than chekcing the string. This is very weak
                    {
                      BaseMethodDeclarationSyntax caller;
                      if (methodFinder.TryGetValueContainingSpan(r.Span, out caller))
                      {
                        var pair = r.startPoint;
                        results.Add(new SearchResult(pair.Item1, pair.Item2, caller, r.outcome.Value));
                      }
                      else
                      {
                        Trace.WriteLine("Failed to find the containing method");
                      }
                    }
                  }
                }
                catch (Exception e)
                {
                  Trace.WriteLine(string.Format("Some exception was thrown inside Clousot...\nDetails:\n{0}", e.ToString()));
                }

              }
            }
          }
        }
      }
      return results.Distinct();

    }

    #endregion
  }

  public struct SearchResult
  {
    readonly public BaseMethodDeclarationSyntax Method;
    readonly public int Line;
    readonly public int Column;
    readonly public ProofOutcome Outcome;

    public SearchResult(int line, int col, BaseMethodDeclarationSyntax method, ProofOutcome outcome)
    {
      this.Line = line;
      this.Column = col;
      this.Method = method;
      this.Outcome = outcome;
    }

    public override string ToString()
    {
      string name;
      var tryMethod = this.Method as MethodDeclarationSyntax;
      if (tryMethod != null)
      {
        name = tryMethod.Identifier.ValueText;
      }
      else
      {
        var tryConstructor = this.Method as ConstructorDeclarationSyntax;
        if (tryConstructor != null)
        {
          name = tryConstructor.Identifier.ValueText;
        }
        else
        {
          name = "<unknown name>";
        }
      }

      return string.Format("({0}, {1}): Method {2}", this.Line, this.Column, name);
    }
  }

  public static class Helper
  {
    private static Microsoft.VisualStudio.OLE.Interop.IServiceProvider globalServiceProvider;
    private static Microsoft.VisualStudio.OLE.Interop.IServiceProvider GlobalServiceProvider
    {
      get
      {
        if (globalServiceProvider == null)
        {
          globalServiceProvider = (Microsoft.VisualStudio.OLE.Interop.IServiceProvider)Package.GetGlobalService(
              typeof(Microsoft.VisualStudio.OLE.Interop.IServiceProvider));
        }

        return globalServiceProvider;
      }
    }


    private static object GetService(
        Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider, Guid guidService, bool unique)
    {
      var guidInterface = VSConstants.IID_IUnknown;
      var ptr = IntPtr.Zero;
      object service = null;

      if (serviceProvider.QueryService(ref guidService, ref guidInterface, out ptr) == 0 &&
          ptr != IntPtr.Zero)
      {
        try
        {
          if (unique)
          {
            service = Marshal.GetUniqueObjectForIUnknown(ptr);
          }
          else
          {
            service = Marshal.GetObjectForIUnknown(ptr);
          }
        }
        finally
        {
          Marshal.Release(ptr);
        }
      }

      return service;
    }

    private static TServiceInterface GetService<TServiceInterface, TService>(
        Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider)
      where TServiceInterface : class
      where TService : class
    {
      return (TServiceInterface)GetService(serviceProvider, typeof(TService).GUID, false);
    }

    public static TServiceInterface GetMefService<TServiceInterface>() where TServiceInterface : class
    {
      TServiceInterface service = null;
      var componentModel = GetService<IComponentModel, SComponentModel>(GlobalServiceProvider);

      if (componentModel != null)
      {
        service = componentModel.GetService<TServiceInterface>();
      }

      return service;
    }

    public static Document OpenDocument(EditorWorkspace workspace, DocumentId documentId)
    {
      if (!workspace.IsDocumentOpen(documentId))
      {
        workspace.OpenDocument(documentId);
        Debug.Assert(workspace.IsDocumentOpen(documentId), "Could not open document.");
      }

      return workspace.CurrentSolution.GetDocument(documentId);
    }

  }
}