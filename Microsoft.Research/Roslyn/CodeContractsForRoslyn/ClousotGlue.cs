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
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Cci;
using Microsoft.Cci.Analysis;
using Microsoft.Cci.Contracts;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;

namespace RoslynToCCICodeModel {

  using ClousotExtension;
  using Microsoft.CodeAnalysis;
  using Microsoft.CodeAnalysis.CodeActions;
  using Microsoft.CodeAnalysis.CSharp;
  using Microsoft.CodeAnalysis.CSharp.Syntax;
  using Microsoft.CodeAnalysis.Formatting;
  using Microsoft.CodeAnalysis.Text;
  using Microsoft.VisualStudio.Shell;
  using Microsoft.Research.CodeAnalysis.Caching;
    using CSharp2CCI;
    using System.Threading.Tasks;

  public enum ContractKind { Precondition, Postcondition, ObjectInvariant, Assume, CodeFix, AbstractState }

  public struct ClousotOutput {
    public enum ExtraInfo { None, Warning, Error, IsExtractMethodSuggestion, IsInvariantAtSuggestion }

    public DiagnosticSeverity Kind
    {
      get {
        switch (this.extraInfo) {
          case ExtraInfo.Warning:
                return DiagnosticSeverity.Warning;
          case ExtraInfo.Error:
            return DiagnosticSeverity.Error;
          default:
            return DiagnosticSeverity.Info;
        }
      }
    }
    public readonly ProofOutcome? outcome;
    public readonly string Message;
    public readonly Tuple<int, int> startPoint;
    public readonly TextSpan Span;
    public readonly CodeAction action;
    public readonly IEnumerable<APC> RelatedLocationsWithSourceContext;
    public bool IsExtractMethodSuggestion { get { return this.extraInfo == ExtraInfo.IsExtractMethodSuggestion; } }
    public bool IsInvariantAtSuggestion { get { return this.extraInfo == ExtraInfo.IsInvariantAtSuggestion; } }

    private readonly ExtraInfo extraInfo;

    public ClousotOutput(ProofOutcome? outcome, string msg, TextSpan span, Tuple<int, int> startPoint, CodeAction action, ExtraInfo extraInfo, IEnumerable<APC> relatedLocationsWithSourceContext = null) {
      this.outcome = outcome;
      this.Message = msg;
      this.Span = span;
      this.startPoint = startPoint;
      this.action = action;
      this.extraInfo = extraInfo;
      this.RelatedLocationsWithSourceContext = relatedLocationsWithSourceContext;
    }

    public override string ToString() {
      return "(" + this.outcome + ", " + this.Message + ", " + this.Span + ", " + this.action + "," + this.IsExtractMethodSuggestion + ")";
    }
  }


  public class ClousotGlue {

    public const string ASKCLOUSOTTAG = "__Ask_Clousot__";

    private ClousotGlueHost host;
    //private Clousot.IndividualMethodAnalyzer<ILocalDefinition, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, IPropertyDefinition, IEventDefinition, TypeReferenceAdaptor, ICustomAttribute, IAssemblyReference> methodAnalyzer;
    private List<ClousotOutput> analysisResults;
    private SourceLocationProvider sourceLocationProvider;

    private IClousotCacheFactory cacheFactory = new UndisposableMemoryCacheFactory();

    /// <summary>
    /// Checks the <paramref name="compilationUnitDeclaration"/> for syntax and semantic
    /// errors and returns an empty enumerable if any are found.
    /// </summary>
    public IEnumerable<ClousotOutput> AnalyzeMeAUnit(
      Microsoft.CodeAnalysis.Document document,
      CompilationUnitSyntax compilationUnitDeclaration,
      CancellationToken cancellationToken,
      ClousotOptions options,
      string[] extraOptions,
      bool showOnlyAnswersToAskClousot = false
      ) {

      if (options == null)
        options = new ClousotOptions();

      // Don't do anything if there are syntactic errors.
      if (compilationUnitDeclaration.ContainsDiagnostics) return null;

      var semanticModel = document.GetSemanticModel(cancellationToken);
      // Don't do anything if there are semantic errors.
      var diagnostics = semanticModel.GetDiagnostics(cancellationToken);
      if (diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error || d.IsWarningAsError)) return null;

      this.host = new ClousotGlueHost((document.Project).MetadataReferences);

      this.sourceLocationProvider = new SourceLocationProvider();
      string exceptionMessage = null;
      IAssemblyReference ar = null;
      try {
        // Constructs a full metadata model for the assembly the semantic model is from
        var transformer = new NodeVisitor(this.host, semanticModel, sourceLocationProvider);

        cancellationToken.ThrowIfCancellationRequested();
        // Constructs the method bodies all of the methods in the compilation unit
        // var tree = document.GetSyntaxTree(cancellationToken);
        var tree2 = transformer.Visit(compilationUnitDeclaration);
        ar = tree2 as IAssemblyReference;

        Utils.DumpCCI(tree2 as Microsoft.Cci.MutableCodeModel.Assembly);
        cancellationToken.ThrowIfCancellationRequested();

      } catch (ConverterException e) {
        exceptionMessage = e.Message;
      }
      if (exceptionMessage != null) {
        return new[] { new ClousotOutput(null, exceptionMessage, compilationUnitDeclaration.GetFirstToken().Span, null, (CodeAction)null, ClousotOutput.ExtraInfo.None) };
      }

      var spanToMethodMap = MethodSpanFinder.GetMethodSpans(compilationUnitDeclaration);

      Utils.Trace("Thread {0} attempting lock at {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
      lock (this) { // Clousot is single-threaded
        try {
          Utils.Trace("Thread {0} obtained lock at {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
          if (cancellationToken.IsCancellationRequested) {
            Utils.Trace("Thread {0} cancelled on lock acquire", Thread.CurrentThread.ManagedThreadId);
            cancellationToken.ThrowIfCancellationRequested();
          } else {
            Utils.Trace("Thread {0} proceeding after lock acquire", Thread.CurrentThread.ManagedThreadId);
          }
          var cciProvider = Microsoft.Cci.Analysis.CciILCodeProvider.CreateCodeProvider(host);
          var unit = ar;
          cancellationToken.ThrowIfCancellationRequested();
          this.host.RegisterUnit(unit.ResolvedUnit);
          cciProvider.MetaDataDecoder.RegisterSourceLocationProvider(unit, sourceLocationProvider);
          var metadataDecoder = cciProvider.MetaDataDecoder;
          var contractDecoder = cciProvider.ContractDecoder;

          var defaultargs = new string[] {
          "-nonnull",
          "-bounds",
          "-arithmetic",
          "-sortwarns:-",
          //"-arrays", 
          "-cache",
          //"-suggest=methodensures",
          "-suggest=propertyensures",
          "-suggest=objectinvariants",
          //"-infer=requires",
          //"-infer=objectinvariants",
          //"-clearcache",
          //"-prefrompost"
        };
          var codefixesargs = new string[] {
          "-nonnull",
          "-bounds",
          "-arithmetic",
          "-suggest=codefixes",
          "-cache",
          "-libpaths:\"c:\\program files (x86)\\Microsoft\\Contracts\\Contracts\\.NetFramework\\v4.0\"",
          //"-suggest=methodensures",
          "-suggest=objectinvariants",
          "-infer=objectinvariants",
          "-suggest=assumes",
          "-premode=backwards",
        };
          var args = codefixesargs;
          if (extraOptions != null)
            args = args.Concat(extraOptions).ToArray();

          var w = (options == null || String.IsNullOrWhiteSpace(options.WarningLevel))
            ? "low"
            : options.WarningLevel;

          var warninglevel = String.Format("-warninglevel={0}", w);
          var x = new string[] { warninglevel, };
          args = args.Concat(x).ToArray();

          if (options != null) {
            var otherOptions = options.OtherOptions;
            if (!String.IsNullOrWhiteSpace(otherOptions)) {
              var otherOpts = otherOptions.Split(' ');
              args = args.Concat(otherOpts).ToArray();
            }
          }

          this.analysisResults = new List<ClousotOutput>();
          var output = new RoslynOutput(this, showOnlyAnswersToAskClousot, this.analysisResults, document, spanToMethodMap, null);
          var methodAnalyzer = Clousot.GetIndividualMethodAnalyzer(metadataDecoder, contractDecoder, args, output,
            new[] { this.cacheFactory }, cancellationToken);
          foreach (var path in methodAnalyzer.Options.libPaths) {
            host.AddLibPath(path);
          }
          cancellationToken.ThrowIfCancellationRequested();
          methodAnalyzer.AnalyzeAssembly(ar);
        } finally {
          if (cancellationToken.IsCancellationRequested) {
            Utils.Trace("Thread {0} releasing lock (canceled) at {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
          } else {
            Utils.Trace("Thread {0} releasing lock at {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
          }
        }
      }

      return this.analysisResults;

    }

    [Import]
    internal SVsServiceProvider ServiceProvider = null;


    private IEnumerable<IPrimarySourceLocation> GetPrimarySourceLocationsForDelegate(IUnitReference unit, IEnumerable<Microsoft.Cci.ILocation> location, bool exact) {
      return this.sourceLocationProvider.GetPrimarySourceLocationsFor(location);
    }

    public class RoslynOutput : IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>, IOutputFullResultsFactory<MethodReferenceAdaptor, IAssemblyReference> {


      private readonly bool ShowOnlyAnswersToAskClousot;
      private readonly ClousotGlue glue;
      private int i;
      private ILogOptions options;
      private List<ClousotOutput> results;
      private Microsoft.CodeAnalysis.Document document;
      private Dictionary<TextSpan, BaseMethodDeclarationSyntax> span2Method;
      private TextWriter tw;

      public RoslynOutput(
        ClousotGlue glue,
        bool ShowOnlyAnswersToAskClousot,
        List<ClousotOutput> results,
        Microsoft.CodeAnalysis.Document document,
        Dictionary<TextSpan, BaseMethodDeclarationSyntax> span2Method,
        string/*?*/ outputFile
        ) {

        this.glue = glue;
        this.ShowOnlyAnswersToAskClousot = ShowOnlyAnswersToAskClousot;
        this.results = results;
        this.document = document;
        this.span2Method = span2Method;
        if (outputFile != null) {
          this.tw = new StreamWriter(outputFile);
        }
      }

      void IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.EndAssembly() {
        // does nothing
      }

      void IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.EndMethod(APC methodEntry, bool ignoreMethodEntryAPC)
      {
        // does nothing
      }

      string IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.Name {
        get {
          return "ClousotGlue.RoslynOutput";
        }
      }

      void IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.StartAssembly(IAssemblyReference assembly) {
        this.results.Clear();
      }

      void IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.StartMethod(MethodReferenceAdaptor method) {
        //this.results.Clear();
      }

      int IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.SwallowedMessagesCount(ProofOutcome outcome) {
        // does nothing
        return 0;
      }

      void IOutputResults.Close() {
        if (tw != null && tw != Console.Out) {
          tw.Flush();
          tw.Close();
        }
      }

      bool IOutputResults.EmitOutcome(Witness witness, string format, params object[] args) {


        if (this.ShowOnlyAnswersToAskClousot) {
          return false;
        }

        string tag;
        ClousotOutput.ExtraInfo info = ClousotOutput.ExtraInfo.None;
        switch (witness.Outcome) {
          case ProofOutcome.Top:
            tag = "warning";
            info = ClousotOutput.ExtraInfo.Warning;
            break;
          case ProofOutcome.False:
            info = ClousotOutput.ExtraInfo.Error;
            tag = "error";
            break;
          default: tag = ""; break;
        }
        CodeAction action = null; // No action for a warning/error. Suggestions have actions.
        var msg = String.Format("{0}: {1}", tag, String.Format(format, args));
        var startIndex = 0;
        var length = 0;
        var pc = witness.PC.PrimaryMethodLocation();
        if (pc.HasRealSourceContext) {
          startIndex = pc.Block.SourceStartIndex(pc);
          length = pc.Block.SourceLength(pc);

          var startLine = pc.Block.SourceStartLine(pc);
          var startColumn = pc.Block.SourceStartColumn(pc);

          this.results.Add(new ClousotOutput(witness.Outcome, msg, new TextSpan(startIndex, length), new Tuple<int, int>(startLine, startColumn),
            action, info,
            witness.PC.NonPrimaryRelatedLocations().Where(relatedPC => relatedPC.HasRealSourceContext)
            ));
          return true;
        }
        if (pc.InsideEnsuresInMethod) { // postcondition failures don't have good locations
          MethodReferenceAdaptor meth;
          var ok = pc.TryGetContainingMethod(out meth);
          if (ok && IteratorHelper.EnumerableIsNotEmpty(meth.reference.Locations)) {
            var l = IteratorHelper.First(meth.reference.Locations) as ISourceLocation;
            if (l != null) {
              startIndex = l.StartIndex;
              length = l.Length;
              var s = new TextSpan(startIndex, length);
              BaseMethodDeclarationSyntax m;
              if (this.span2Method.TryGetValue(s, out m)) {
                List<TextSpan> returnSpans = null;
                if (m is MethodDeclarationSyntax) // otherwise it is a constructor
                  returnSpans = ReturnStatementFinder.GetReturnStatementSpans(m as MethodDeclarationSyntax);
                if (returnSpans == null || returnSpans.Count == 0) {
                  if (m.Body == null) {
                    this.results.Add(new ClousotOutput(witness.Outcome, msg, m.GetLastToken().Span, null, action, info));
                    return true;
                  } else {
                    // just use method's closing curly
                    this.results.Add(new ClousotOutput(witness.Outcome, msg, m.Body.CloseBraceToken.Span, null, action, info));
                    return true;
                  }
                } else {
                  foreach (var returnSpan in returnSpans)
                    this.results.Add(new ClousotOutput(witness.Outcome, msg, returnSpan, null, action, info));
                  return true;
                }
              }
            }
          }
        }
        return false;
      }

      bool IOutputResults.EmitOutcomeAndRelated(Witness witness, string format, params object[] args) {

        // Instead of two different results, combine the non-primary information (e.g., the postcondition
        // that is violated) with the primary information (e.g., the return statement that is violating
        // the postcondition).
        var pc = witness.PC;
        var startIndex = pc.Block.SourceStartIndex(pc);
        var length = pc.Block.SourceLength(pc);
        var relatedText = this.document.GetText().GetSubText(new TextSpan(startIndex, length)).ToString();

        if (this.ShowOnlyAnswersToAskClousot) {
          if (relatedText.Contains(ASKCLOUSOTTAG)) {
            this.results.Add(new ClousotOutput(witness.Outcome, string.Format(format, args), new TextSpan(startIndex, length), null, null, ClousotOutput.ExtraInfo.None));
          }
        }

        // not clear how to add related location to the primary. For now just emit the primary one.
        else {
          //foreach (var related in witness.PC.NonPrimaryRelatedLocations())
          //{
          //  if (related.HasRealSourceContext)
          //  {
          //    format += "\n" + relatedText;
          //  }
          //}
          ((IOutputResults)this).EmitOutcome(witness, format, args);
        }
        return true;
      }

      int IOutputResults.RegressionErrors() {
        // does nothing
        return 0;
      }

      void IOutputResults.FinalStatistic(string assemblyName, string message) {
        // does nothing
      }

      bool IOutputResults.IsMasked(Witness witness) {
        // does nothing
        return false;
      }

      ILogOptions IOutputResults.LogOptions {
        get { return this.options; }
      }

      void IOutputResults.Statistic(string format, params object[] args) {
        // does nothing
      }

      void IOutput.EmitError(System.CodeDom.Compiler.CompilerError error) {
        // does nothing
      }

      IFrameworkLogOptions IOutput.LogOptions {
        get { return this.options; }
      }

      void IOutput.Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extra)
      {
        var isExtractMethodSuggestion = kind.Contains("for the extracted method");
        var isInvariantAtSuggestion = kind.Contains("abstract state");

        Contract.Assert(!isExtractMethodSuggestion || isExtractMethodSuggestion != isInvariantAtSuggestion);

        var extraInfo = ClousotOutput.ExtraInfo.None;

        if (isExtractMethodSuggestion) {
          extraInfo = ClousotOutput.ExtraInfo.IsExtractMethodSuggestion;
        }
        if (isInvariantAtSuggestion) {
          extraInfo = ClousotOutput.ExtraInfo.IsInvariantAtSuggestion;
        }

        var msg = String.Format("Suggestion: {0}", suggestion);

        if (kind.Contains("Code fix") && pc.HasRealSourceContext) {
          CodeFix.CodeFixInfo codeFixInfo = CodeFix.CodeFixInfo.Replace;
          var si = pc.Block.SourceStartIndex(pc);
          var l = pc.Block.SourceLength(pc);
          var fixIndex = suggestion.IndexOf("Fix:");
          if (fixIndex == -1) { // TODO: cater to other kinds of fixes (adds, delete)
            return;
          }
          var newText = suggestion.Substring(fixIndex + 5);
          bool isAfter = false;
          if (newText.StartsWith("Add Contract.")) {
            codeFixInfo = CodeFix.CodeFixInfo.InsertBeforeStatement;
            newText = newText.Substring(4);
          } else if (newText.StartsWith("Add (after) Contract.")) {
            codeFixInfo = CodeFix.CodeFixInfo.InsertAfterStatement;
            newText = newText.Substring(12);
            isAfter = true;
          } else if (newText.StartsWith("Add &&")) {
            codeFixInfo = CodeFix.CodeFixInfo.ExtendCondition;
            newText = newText.Substring(7);
          }
          var sp = new TextSpan(si, l);
          var a = new CodeFix(this.document, sp, newText, codeFixInfo);
          this.results.Add(new ClousotOutput(null /* no outcome */, msg, new TextSpan(si, l), null, a, ClousotOutput.ExtraInfo.None));

          if (isAfter && newText.Contains("Assume") && this.options.InferAssumesForBaseLining) {
            // also add a semantic suppression action
            var suppress = new SuppressFix(this.glue, this.document, sp, newText);
            // no additional message, so null
            this.results.Add(new ClousotOutput(null, null, sp, null, suppress, ClousotOutput.ExtraInfo.None));
          }
          return;
        }

        // TODO: It would be better to have a different way to tell the issue provider
        // that this issue is related to the method as a whole and not any particular
        // location so that it could decide on what span to attach the issue to.
        var startIndex = 0;
        var length = 0;
        MethodReferenceAdaptor meth;
        var ok = pc.TryGetContainingMethod(out meth);
        if (ok && IteratorHelper.EnumerableIsNotEmpty(meth.reference.Locations)) {
          var l = IteratorHelper.First(meth.reference.Locations) as ISourceLocation;
          if (l != null) {
            startIndex = l.StartIndex;
            length = l.Length;
          }
        }
        var span = new TextSpan(startIndex, length); // should be the span of the method name
        var st = this.document.GetSyntaxTree();
        var roslynLocation = st.GetLocation(span);
        BaseMethodDeclarationSyntax method;
        if (this.span2Method.TryGetValue(span, out method)) {
          if (!isExtractMethodSuggestion && !isInvariantAtSuggestion) {
            if (method.ContainsSuggestion(suggestion)) {
              return;
            }

            var ckind = kind.ToContractKind();
            var action = new ContractInjector(this.document, ckind, suggestion, method);
            this.results.Add(new ClousotOutput(null /* no outcome */, msg, span, null, (CodeAction)action, extraInfo));
          } else {
            this.results.Add(new ClousotOutput(null /* no outcome */, msg, span, null, (CodeAction)null, extraInfo));
          }
        } else {
          this.results.Add(new ClousotOutput(null /* no outcome */, msg, span, null, (CodeAction)null, extraInfo));
        }
      }

      void ISimpleLineWriter.WriteLine(string format, params object[] args) {
        if (this.tw != null)
          tw.WriteLine(format, args);
        this.i++;
      }

      IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference> IOutputFullResultsFactory<MethodReferenceAdaptor, IAssemblyReference>.GetOutputFullResultsProvider(ILogOptions options) {
        this.options = options;
        return this;
      }
    }

    internal class ClousotGlueHost : Microsoft.Cci.Analysis.HostEnvironment {

      public ClousotGlueHost(IEnumerable<MetadataReference> references) {
        foreach (var r in references) {
          this.LoadUnitFrom(r.Display);
        }
      }

      /// <summary>
      /// Indicates whether IL locations should be preserved up into the code model
      /// by decompilers using this host.
      /// </summary>
      public override bool PreserveILLocations { get { return true; } }

      public void RegisterUnit(IUnit unit) {
        if (!this.unit2ContractExtractor.ContainsKey(unit.UnitIdentity)) {
          var contractMethods = new ContractMethods(this);
          var lazyContractProviderForLoadedUnit = new LazyContractExtractor(this, unit, contractMethods, false);
          var codeContractsExtractor = new CodeContractsContractExtractor(this, lazyContractProviderForLoadedUnit);
          this.unit2ContractExtractor.Add(unit.UnitIdentity, codeContractsExtractor);
        }
      }

      public override void ResolvingAssemblyReference(IUnit referringUnit, Microsoft.Cci.AssemblyIdentity referencedAssembly) {
        base.ResolvingAssemblyReference(referringUnit, referencedAssembly);
      }

      public override IAssembly LoadAssembly(Microsoft.Cci.AssemblyIdentity assemblyIdentity) {
        return base.LoadAssembly(assemblyIdentity);
      }

      protected override Microsoft.Cci.AssemblyIdentity Probe(string probeDir, Microsoft.Cci.AssemblyIdentity referencedAssembly) {
        return base.Probe(probeDir, referencedAssembly);
      }
    }

    private class ContractInjector : CodeAction {
      Microsoft.CodeAnalysis.Document document;
      string suggestion;
      private BaseMethodDeclarationSyntax oldNode;
      readonly private ContractKind kind;

      public ContractInjector(Microsoft.CodeAnalysis.Document document, ContractKind kind, string suggestion, BaseMethodDeclarationSyntax oldNode) {
        this.document = document;
        this.kind = kind;
        this.suggestion = suggestion;
        this.oldNode = oldNode;
      }

      protected async override Task<Document> GetChangedDocumentAsync(CancellationToken cancellationToken)
      {

        // create the new method
        var stmt = SyntaxFactory.ParseStatement(this.suggestion + "\r\n"); // need the newline or else the injected statement doesn't go on its own line!
        var newMethod = this.oldNode.InsertStatements(stmt, this.kind);

        // Nothing has changed, so there is nothing to suggest
        if (newMethod == this.oldNode)
          return null;

        // update the document
        var syntaxTree = (SyntaxTree)document.GetSyntaxTree(cancellationToken);
        var newRoot = syntaxTree.GetRoot().ReplaceNode(this.oldNode, newMethod);

        // return the update
        return this.document.WithSyntaxRoot(newRoot);
      }

      public override string Description {
        get { return String.Format("Add contract ({0})", this.kind); }
      }


    }

    private class CodeFix : CodeAction {

      public enum CodeFixInfo { InsertBeforeStatement, InsertAfterStatement, AddSameExpression, Replace, ExtendCondition, }

      private Microsoft.CodeAnalysis.Document document;
      private string suggestion;
      private TextSpan span;
      private string description = "SPA Code Fix";
      private CodeFix.CodeFixInfo codeFixInfo;

      public CodeFix(Microsoft.CodeAnalysis.Document iDocument, TextSpan span, string suggestion, CodeFix.CodeFixInfo codeFixInfo = CodeFix.CodeFixInfo.Replace) {
        this.document = iDocument;
        this.span = span;
        this.suggestion = suggestion;
        this.codeFixInfo = codeFixInfo;
      }

      public override string Description {
        get { return this.description; }
      }

      protected override async Task<Document> GetChangedDocumentAsync(CancellationToken cancellationToken)
      {
        #region Parse the suggested fix
        var replacement = SyntaxFactory.ParseExpression(this.suggestion);
        if (replacement.ContainsDiagnostics) {
          this.description = String.Format("{0}: Parse error on '{1}'", this.description, this.suggestion);
          return null;
        }
        #endregion

        SyntaxNode replacementNode = replacement;

        #region Find the first node that corresponds to the span of the suggested fix
        var tree = (SyntaxTree)document.GetSyntaxTree();
        var root = tree.GetRoot();
        var oldEs = from node in root.DescendantNodes()
                    where node.Span.Equals(this.span)
                    select node;
        if (oldEs.Count() == 0) {
          this.description = String.Format("{0}: Couldn't find node with expected span", this.description);
          return null;
        }
        var nodeToReplace = oldEs.First();
        #endregion

        if (this.codeFixInfo == CodeFixInfo.InsertBeforeStatement || this.codeFixInfo == CodeFixInfo.InsertAfterStatement) {
          var ok = false;
          StatementSyntax statement = nodeToReplace.FirstAncestorOrSelf<StatementSyntax>();
          var replacementStatement = SyntaxFactory.ExpressionStatement(replacement).WithTrailingTrivia(SyntaxFactory.EndOfLine(""));
          var closestBlock = statement.FirstAncestorOrSelf<BlockSyntax>();
          if (closestBlock != null) {
            var index = closestBlock.Statements.IndexOf(statement);
            if (index != -1) {
              var newStatements = closestBlock.Statements.ToList();
              if (this.codeFixInfo == CodeFixInfo.InsertBeforeStatement)
              {
                newStatements.Insert(index, replacementStatement);
              }
              else
              {
                newStatements.Insert(index + 1, replacementStatement);
              }
              var ss = new SyntaxList<StatementSyntax>();
              foreach (var s in newStatements)
                ss = ss.Add(s);
              nodeToReplace = closestBlock;
              replacementNode = closestBlock.WithStatements(ss);
              ok = true;
            }
            if (!ok) {
              replacementNode = SyntaxFactory.Block(statement, replacementStatement);
            }
          }
        }
        else if (this.codeFixInfo == CodeFixInfo.ExtendCondition)
        {
          replacementNode = SyntaxFactory.BinaryExpression(SyntaxKind.LogicalAndExpression, (ExpressionSyntax)nodeToReplace, (ExpressionSyntax)replacementNode);
        }
        var argSyntax = nodeToReplace as ArgumentSyntax;
        if (argSyntax != null && !(replacementNode is ArgumentSyntax))
        {
          nodeToReplace = argSyntax.Expression;
        }

        #region Create transform and return it
        SyntaxNode newRoot = null;
        // REVIEW: Is there a better way to know if the new expression can replace the old one?
        try {
          //newRoot = root.ReplaceNode(nodeToReplace, Formatter.Annotation.AddAnnotationTo(replacementNode));
          newRoot = root.ReplaceNode(nodeToReplace, replacementNode);
        }
        catch
        {
          this.description = String.Format("{0}: Parsed suggestion '{1}' doesn't agree with found node type", this.description, this.suggestion);
          return null;
        }
        if (newRoot == null) return null;
        return this.document.WithSyntaxRoot(newRoot);
        //return editFactory.CreateTreeTransformEdit(this.document.Project.Solution, tree, newRoot);
        #endregion
      }

    }
    private class SuppressFix : CodeAction
    {
      private Microsoft.CodeAnalysis.Document document;
      private TextSpan span;
      private string description = "Suppress semantically";
      private ClousotGlue glue;
      private string msg;

      public SuppressFix(ClousotGlue glue, Microsoft.CodeAnalysis.Document iDocument, TextSpan span, string msg)
      {
        this.document = iDocument;
        this.span = span;
        this.glue = glue;
        this.msg = msg;
      }

      public override string Description
      {
        get { return this.description; }
      }

      public override async System.Threading.Tasks.Task<IEnumerable<CodeActionOperation>> GetOperationsAsync(CancellationToken cancellationToken)
      {
          var tree = (SyntaxTree)document.GetSyntaxTree();
          var root = tree.GetRoot() as CompilationUnitSyntax;
          if (root == null)
          {
              this.description = String.Format("{0}: Syntax tree for document was not a compilation unit", this.description);
              return null;
          }
          return new List<CodeActionOperation>(){
              new SuppressNotifyWrapper(() => AnalyzeToFillCacheWithSuppressions(this.document, root))
          };


      }
      protected override async Task<Document> GetChangedDocumentAsync(CancellationToken cancellationToken)
      {
        SyntaxNode replacementNode;

        #region Find the first node that corresponds to the span of the suggested fix
        var tree = (SyntaxTree)document.GetSyntaxTree();
        var root = tree.GetRoot() as CompilationUnitSyntax;
        if (root == null)
        {
          this.description = String.Format("{0}: Syntax tree for document was not a compilation unit", this.description);
          return null;
        }
        var oldEs = from node in root.DescendantNodes()
                    where node.Span.Equals(this.span)
                    select node;
        if (oldEs.Count() == 0)
        {
          this.description = String.Format("{0}: Couldn't find node with expected span", this.description);
          return null;
        }
        var nodeToReplace = oldEs.First();
        #endregion

        {
          StatementSyntax statement = nodeToReplace.FirstAncestorOrSelf<StatementSyntax>();
          var existingTrivia = statement.GetTrailingTrivia().ToList();
          existingTrivia.Add(SyntaxFactory.Comment("// semantic suppression applied"));
          existingTrivia.Add(SyntaxFactory.EndOfLine(""));
          var replacementStatement = statement.WithTrailingTrivia(existingTrivia);

          nodeToReplace = statement;
          replacementNode = replacementStatement;
        }


        #region Create transform and return it
        SyntaxNode newRoot = null;
        // REVIEW: Is there a better way to know if the new expression can replace the old one?
        try
        {
          //newRoot = root.ReplaceNode(nodeToReplace, Formatter.Annotation.AddAnnotationTo(replacementNode));
          newRoot = root.ReplaceNode(nodeToReplace, replacementNode);
        }
        catch
        {
          return null;
        }
        if (newRoot == null) return null;
        return this.document.WithSyntaxRoot(newRoot);
        //var temp = new CodeActionEdit(this.document.WithSyntaxRoot(newRoot));
        //return new CodeActionEdit(temp.PreOperation, temp.UpdatedSolution, new SuppressNotifyWrapper(temp.PostOperation, () => AnalyzeToFillCacheWithSuppressions(this.document, root)));
        #endregion
      }

      class SuppressNotifyWrapper : CodeActionOperation
      {
        CodeActionOperation underlying;
        private Action postAction;

        public SuppressNotifyWrapper(CodeActionOperation underlying, Action postaction)
        {
          Contract.Requires(postAction != null);

          this.underlying = underlying;
          this.postAction = postaction;
        }
        public SuppressNotifyWrapper(Action postaction)
        {
            Contract.Requires(postAction != null);

            this.underlying = null;
            this.postAction = postaction;
        }

        protected override void Apply(Workspace workspace, CancellationToken cancellationToken)
        {
          if (this.underlying != null) base.Apply(workspace, cancellationToken);
          postAction();
        }

        //public object GetPreview(Workspace workspace, CancellationToken cancellationToken)
        //{
        //  if (this.underlying != null) return underlying.GetPreview(workspace, cancellationToken);
        //  return null;
        //}
      }

      private void AnalyzeToFillCacheWithSuppressions(Document document, CompilationUnitSyntax root)
      {
        var thread = new Thread(() =>
        {
          try
          {
            CancellationToken dummy;

            this.glue.AnalyzeMeAUnit(document, root, dummy, AssemblyCodeIssueProvider.options, new string[] { "-saveSemanticBaseline:demo" }, true);
          }
          catch { }
        });
        thread.Start();
      }

    }

  }

  public class ClousotGlue2 {

    private ClousotGlueHost host;
    private CciILCodeProvider cciProvider;
    private Clousot.IndividualMethodAnalyzer<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, IPropertyDefinition, IEventDefinition, TypeReferenceAdaptor, ICustomAttribute, IAssemblyReference> methodAnalyzer;
    private List<Tuple<string, TextSpan>> analysisResults = new List<Tuple<string, TextSpan>>();
    private SourceLocationProvider sourceLocationProvider;

    public ClousotGlue2() {
      CancellationToken cancellationToken = new CancellationToken();
      Initialize(cancellationToken);
    }

    private void Initialize(CancellationToken cancellationToken) {
      this.host = new ClousotGlueHost();
      this.cciProvider = Microsoft.Cci.Analysis.CciILCodeProvider.CreateCodeProvider(host);
      var metadataDecoder = this.cciProvider.MetaDataDecoder;
      var contractDecoder = this.cciProvider.ContractDecoder;
      var args = new string[] { "-nonnull", "-bounds", "-arithmetic", "-sortwarns:-" };
      var output = new RoslynOutput(this.analysisResults);
      this.methodAnalyzer = Clousot.GetIndividualMethodAnalyzer(metadataDecoder, contractDecoder, args, output, null, cancellationToken);
    }

    public static void ConvertRoslynToCCI(
      IMetadataHost host,
      SyntaxTree tree,
      SemanticModel semanticModel,
      out IModule module,
      out ISourceLocationProvider sourceLocationProvider) {

      sourceLocationProvider = new SourceLocationProvider();

      var transformer = new NodeVisitor(host, semanticModel, sourceLocationProvider);
      var assembly = (IModule)transformer.Visit(tree.GetRoot());
      module = assembly;
    }

    public IEnumerable<Tuple<string, TextSpan>> AnalyzeMeAMethod(
      Microsoft.CodeAnalysis.Document document,
      MethodDeclarationSyntax methodDeclaration,
      CancellationToken cancellationToken) {

      var semanticModel = document.GetSemanticModel(cancellationToken);
      var semanticNode = semanticModel.GetDeclaredSymbol(methodDeclaration);

      Microsoft.Cci.IMethodReference cciMethod = null;
      this.sourceLocationProvider = new SourceLocationProvider();
      string exceptionMessage = null;
      try {
        // Constructs a full metadata model for the assembly the semantic model is from
        var transformer = new NodeVisitor(this.host, semanticModel, sourceLocationProvider);

        // Constructs the method body for just this one method
        cciMethod = transformer.Visit(methodDeclaration) as Microsoft.Cci.IMethodReference;

      } catch (ConverterException e) {
        exceptionMessage = e.Message;
      }
      if (exceptionMessage != null) {
        yield return Tuple.Create(exceptionMessage, methodDeclaration.Span);
        yield break;
      }
      var unit = TypeHelper.GetDefiningUnitReference(cciMethod.ContainingType);
      this.host.RegisterUnit(unit.ResolvedUnit);
      this.cciProvider.MetaDataDecoder.RegisterSourceLocationProvider(unit, sourceLocationProvider);

      this.methodAnalyzer.Analyze(MethodReferenceAdaptor.AdaptorOf(cciMethod));

      foreach (var result in this.analysisResults) {
        yield return result;
      }

    }

    public IEnumerable<Tuple<string, TextSpan>> AnalyzeMeAUnit(
      Microsoft.CodeAnalysis.Document document,
      CompilationUnitSyntax compilationUnitDeclaration,
      CancellationToken cancellationToken) {

      var semanticModel = document.GetSemanticModel(cancellationToken);
      var semanticNode = semanticModel.GetDeclaredSymbol(compilationUnitDeclaration);

      this.sourceLocationProvider = new SourceLocationProvider();
      string exceptionMessage = null;
      IAssemblyReference ar = null;
      try {
        // Constructs a full metadata model for the assembly the semantic model is from
        var transformer = new NodeVisitor(this.host, semanticModel, sourceLocationProvider);

        // Constructs the method bodies all of the methods in the compilation unit
        var tree = document.GetSyntaxTree(cancellationToken);
        var tree2 = transformer.Visit(compilationUnitDeclaration);
        ar = tree2 as IAssemblyReference;

      } catch (ConverterException e) {
        exceptionMessage = e.Message;
      }
      if (exceptionMessage != null) {
        yield return Tuple.Create(exceptionMessage, compilationUnitDeclaration.GetFirstToken().Span);
        yield break;
      }
      var unit = ar;
      this.host.RegisterUnit(unit.ResolvedUnit);
      this.cciProvider.MetaDataDecoder.RegisterSourceLocationProvider(unit, sourceLocationProvider);

      this.methodAnalyzer.AnalyzeAssembly(ar);

      foreach (var result in this.analysisResults) {
        yield return result;
      }

    }

    private IEnumerable<IPrimarySourceLocation> GetPrimarySourceLocationsForDelegate(IUnitReference unit, IEnumerable<Microsoft.Cci.ILocation> location, bool exact) {
      return this.sourceLocationProvider.GetPrimarySourceLocationsFor(location);
    }

    public class RoslynOutput : IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>, IOutputFullResultsFactory<MethodReferenceAdaptor, IAssemblyReference> {
      private int i;
      private ILogOptions options;
      private List<Tuple<string, TextSpan>> results;

      public RoslynOutput(List<Tuple<string, TextSpan>> results) {
        this.results = results;
      }

      void IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.EndAssembly() {

      }

      void IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.EndMethod(APC methodEntry, bool ignoreMethodEntryAPC)
      {
      }

      string IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.Name {
        get { throw new NotImplementedException(); }
      }

      void IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.StartAssembly(IAssemblyReference assembly) {
        this.results.Clear();
      }

      void IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.StartMethod(MethodReferenceAdaptor method) {
        //this.results.Clear();
      }

      int IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference>.SwallowedMessagesCount(ProofOutcome outcome) {
        throw new NotImplementedException();
      }

      void IOutputResults.Close() {
      }

      bool IOutputResults.EmitOutcome(Witness witness, string format, params object[] args) {
        string tag;
        switch (witness.Outcome) {
          case ProofOutcome.Top: tag = "warning"; break;
          case ProofOutcome.False: tag = "error"; break;
          default: tag = ""; break;
        }
        var msg = String.Format("{0}: {1}", tag, String.Format(format, args));
        var startIndex = 0;
        var length = 0;
        var pc = witness.PC;
        if (pc.HasRealSourceContext) {
          startIndex = pc.Block.SourceStartIndex(pc);
          length = pc.Block.SourceLength(pc);
        }
        this.results.Add(Tuple.Create(msg, new TextSpan(startIndex, length)));
        return true;
      }

      bool IOutputResults.EmitOutcomeAndRelated(Witness witness, string format, params object[] args) {
        /* var result = */
        ((IOutputResults)this).EmitOutcome(witness, format, args);
        foreach (var related in witness.PC.NonPrimaryRelatedLocations()) {
          // added warning so that VS shows it
          // only show if related is real source context
          if (related.HasRealSourceContext) {
            var msg = String.Format("{0}: warning : location related to previous warning", related.PrimarySourceContext());
            var pc = witness.PC;
            var startIndex = pc.Block.SourceStartIndex(pc);
            var length = pc.Block.SourceLength(pc);
            this.results.Add(Tuple.Create(msg, new TextSpan(startIndex, length)));
          }
        }

        return true;
      }

      int IOutputResults.RegressionErrors() {
        throw new NotImplementedException();
      }

      void IOutputResults.FinalStatistic(string assemblyName, string message) {
        throw new NotImplementedException();
      }

      bool IOutputResults.IsMasked(Witness witness) {
        throw new NotImplementedException();
      }

      ILogOptions IOutputResults.LogOptions {
        get { return this.options; }
      }

      void IOutputResults.Statistic(string format, params object[] args) {
        throw new NotImplementedException();
      }

      void IOutput.EmitError(System.CodeDom.Compiler.CompilerError error) {
        throw new NotImplementedException();
      }

      IFrameworkLogOptions IOutput.LogOptions {
        get { return this.options; }
      }

      void IOutput.Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extra)        
      {
        var msg = String.Format("Suggestion: {0}", suggestion);

        // TODO: It would be better to have a different way to tell the issue provider
        // that this issue is related to the method as a whole and not any particular
        // location so that it could decide on what span to attach the issue to.
        var startIndex = 0;
        var length = 0;
        MethodReferenceAdaptor meth;
        var ok = pc.TryGetContainingMethod(out meth);
        if (ok && IteratorHelper.EnumerableIsNotEmpty(meth.reference.Locations)) {
          var l = IteratorHelper.First(meth.reference.Locations) as ISourceLocation;
          if (l != null) {
            startIndex = l.StartIndex;
            length = l.Length;
          }
        }
        this.results.Add(Tuple.Create(msg, new TextSpan(startIndex, length)));
      }

      void ISimpleLineWriter.WriteLine(string format, params object[] args)
      {
        this.i++;
        //throw new NotImplementedException();
      }

      IOutputFullResults<MethodReferenceAdaptor, IAssemblyReference> IOutputFullResultsFactory<MethodReferenceAdaptor, IAssemblyReference>.GetOutputFullResultsProvider(ILogOptions options) {
        this.options = options;
        return this;
      }
    }

    internal class ClousotGlueHost : Microsoft.Cci.Analysis.HostEnvironment {

      /// <summary>
      /// Indicates whether IL locations should be preserved up into the code model
      /// by decompilers using this host.
      /// </summary>
      public override bool PreserveILLocations { get { return true; } }

      public void RegisterUnit(IUnit unit) {
        if (!this.unit2ContractExtractor.ContainsKey(unit.UnitIdentity)) {
          var contractMethods = new ContractMethods(this);
          var lazyContractProviderForLoadedUnit = new LazyContractExtractor(this, unit, contractMethods, false);
          var codeContractsExtractor = new CodeContractsContractExtractor(this, lazyContractProviderForLoadedUnit);
          this.unit2ContractExtractor.Add(unit.UnitIdentity, codeContractsExtractor);
        }
      }


    }


  }
}