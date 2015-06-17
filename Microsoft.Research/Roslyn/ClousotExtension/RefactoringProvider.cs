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
using System.Threading;
using System.ComponentModel.Composition;
using RoslynToCCICodeModel;
using System.Diagnostics;
using ClousotExtension;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CodeActions.Providers;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.ExtractMethod;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.CSharp.Semantics;
using System.Windows.Media;
using Microsoft.CodeAnalysis.Common.Semantics;

namespace ClousotExtension {

  [ExportCodeRefactoringProvider("ClousotExtractMethod", LanguageNames.CSharp)]
  class ClousotExtractMethod : ICodeRefactoringProvider {
    private ClousotOptions options;

    [ImportingConstructor]
    public ClousotExtractMethod(ClousotOptions options) {
      this.options = options;
    }

    public CodeRefactoring GetRefactoring(Microsoft.CodeAnalysis.Document document, TextSpan textSpan, CancellationToken cancellationToken) {

      var tree = (SyntaxTree)document.GetSyntaxTree(cancellationToken);
      var semanticModel = (SemanticModel)document.GetSemanticModel(cancellationToken);

      var methodExtractor = new MethodExtractor(semanticModel, document, textSpan, this.options);
      var newDocument = methodExtractor.GetRefactoredDocument(cancellationToken);
      if (newDocument == null) return null;
      var action = new ClousotExtractMethodAction(newDocument);
      return new CodeRefactoring(new[] { action });
    }
  }

  class ClousotExtractMethodAction : ICodeAction {
    private Document d;

    public ClousotExtractMethodAction(Document d) {
      this.d = d;

      this.Description = "Extract method with Contracts";
      this.Icon = null;
    }

    public ImageSource Icon { get; private set; }
    public string Description { get; private set; }

    public CodeActionEdit GetEdit(CancellationToken cancellationToken) {
      return new CodeActionEdit(this.d);
    }
  }
  class MethodExtractor {

    private readonly SemanticModel semanticModel;
    private readonly Microsoft.CodeAnalysis.Document document;
    private readonly TextSpan textSpan;
    private ClousotOptions options;

    public MethodExtractor(SemanticModel semanticModel, Document document, TextSpan textSpan, ClousotOptions options) {
      this.semanticModel = semanticModel;
      this.document = document;
      this.textSpan = textSpan;
      this.options = options;
    }

    public Document GetRefactoredDocument_new(CancellationToken cancellationToken) {

      lock (this) {
        var start = DateTime.Now;

        Utils.Trace(string.Format("Trying to extract method, span: {0}", this.textSpan));

        var localDocument = this.document;

        var extracted = ExtractMethodService.ExtractMethod(localDocument, this.textSpan, new ExtractMethodOptions(true), cancellationToken);
        Utils.TraceTime("Roslyn extract method", DateTime.Now - start);
        if (!extracted.Succeeded) {
          Utils.Trace("Method Extraction failed!");
          return null;
        }
        Utils.Trace("Extract method: ok");
        var clock = DateTime.Now;

        var root = extracted.Document.GetSyntaxRoot(cancellationToken);
        var closestEnclosingStatement = extracted.InvocationNameToken.Parent.FirstAncestorOrSelf<StatementSyntax>();
        if (closestEnclosingStatement == null) return null;

        var newMethod = extracted.MethodDeclarationNode as MethodDeclarationSyntax;
        if (newMethod == null) return null;
        var body = newMethod.Body;


        Utils.Trace("Generating the Precondition marker");

        var actualParameters = "";
        var formalParameters = "";

        var ps = newMethod.ParameterList.Parameters;
        for (int i = 0, j = 0, n = ps.Count; i < n; i++) {
          var p = ps[i];
          {
            var modifiers = "";

            foreach (var modifier in p.Modifiers) {
              modifiers = modifiers + modifier.ValueText + " ";
            }

            // Avoid the refs when emitting the condition
            if (!String.IsNullOrEmpty(modifiers)) {
              if (0 < j) {
                actualParameters += ",";
                formalParameters += ",";
              }

              var name = p.Identifier.ValueText;
              actualParameters += name;
              formalParameters += p.Type + " " + name;

              j++;
            }
          }
        }

        // todo: just use invocation, but replace the method name with __PreconditionMarker!
        var preconditionMarkerString = "__PreconditionMarker(" + actualParameters + ");";
        var preconditionMarker = Formatter.Annotation.AddAnnotationTo(SyntaxFactory.ParseStatement(preconditionMarkerString));

        var postconditionMarker = Formatter.Annotation.AddAnnotationTo(SyntaxFactory.ParseStatement("Console.WriteLine(5);\n"));


        var b = SyntaxFactory.Block(preconditionMarker, closestEnclosingStatement, postconditionMarker);


        //var finalRoot = ((SyntaxNode)root).ReplaceNode(
        //     closestEnclosingStatement,
        //     b.WithAdditionalAnnotations(CodeActionAnnotations.CreateRenameAnnotation())
        //     );

        Utils.Trace("Adding dummy methods");

        var preconditionMarkerDefString = "[Pure]\npublic void __PreconditionMarker(" + formalParameters + "){}\n";
        var preconditionMarkerDef = SyntaxFactory.MethodDeclaration(null, new SyntaxTokenList(), SyntaxFactory.ParseTypeName("void"), null, SyntaxFactory.ParseToken("__PreconditionMarker"), null, SyntaxFactory.ParseParameterList("(" + formalParameters + ")"), null, SyntaxFactory.Block());

        var r1 = (SyntaxNode)root;
        var classDef = closestEnclosingStatement.FirstAncestorOrSelf<TypeDeclarationSyntax>();
        var r2 = new AddMarkerMethods(classDef, preconditionMarkerDef).Visit(r1);
        var r3 = r2.ReplaceNode(
             closestEnclosingStatement,
             b.WithAdditionalAnnotations(CodeActionAnnotations.CreateRenameAnnotation())
             );
        return this.document.WithSyntaxRoot(r3);

 
#if false



        var resultingTree = (SyntaxNode)extracted.MethodDeclarationNode;

        var newMethodIdentified = (SyntaxToken)extracted.InvocationNameToken;


        var oldTree = localDocument.GetSyntaxTree(cancellationToken);
        if (oldTree == null) return null;
        
        Utils.Trace("Got the original syntax tree");
        var oldRoot = oldTree.GetRoot(cancellationToken) as SyntaxNode;



#endif
      }
    }

    public Document GetRefactoredDocument(CancellationToken cancellationToken) {
      lock (this) {
        var start = DateTime.Now;

        Utils.Trace(string.Format("Trying to extract method, span: {0}", this.textSpan));

        var localDocument = document;

        var extracted = ExtractMethodService.ExtractMethod(localDocument, this.textSpan, new ExtractMethodOptions(true), cancellationToken);

        Utils.TraceTime("Roslyn extract method", DateTime.Now - start);

        if (!extracted.Succeeded) {
          Utils.Trace("Method Extraction failed!");
          return null;
        }
        Utils.Trace("Extract method: ok");

        var clock = DateTime.Now;

        var resultingTree = localDocument.GetSyntaxTree(cancellationToken).GetRoot(cancellationToken);

        var newMethodSyntax = extracted.MethodDeclarationNode as MethodDeclarationSyntax;
        if (newMethodSyntax == null) return null;

        var body = newMethodSyntax.Body;

        var compilationUnitDeclaration = resultingTree as CompilationUnitSyntax;
        if (compilationUnitDeclaration == null) return null;
        var oldTree = localDocument.GetSyntaxTree(cancellationToken);
        if (oldTree == null)
          return null;

        Utils.Trace("Got the original syntax tree");

        var oldRoot = oldTree.GetRoot() as SyntaxNode;

        // find the smallest containing statement (and the
        Utils.Trace("Searching for the statement containing the new call");
        StatementSyntax containingStatement = extracted.InvocationNameToken.Parent.FirstAncestorOrSelf<StatementSyntax>();
        if (containingStatement == null) {
          Utils.Trace("No containing statement found. Aborting");
          return null;
        }
        Utils.Trace("Containing statement found");

        Utils.Trace("Searching for the right spots where to place the placeholder calls");

        var oldText = oldTree.GetText().ToString();
        var beforeRefactoringText = oldText.Substring(0, containingStatement.Span.Start - 1);
        var afterRefactoringText = oldText.Substring(containingStatement.Span.Start + containingStatement.Span.Length);

        //var sub = oldText.Substring(containingStatement.Span.Start, containingStatement.Span.Length);
        var sub = oldText.Substring(this.textSpan.Start, this.textSpan.Length);

        Utils.Trace("Generating the Precondition marker");

        var actualParameters = "";
        var formalParameters = "";

        var ps = newMethodSyntax.ParameterList.Parameters;
        for (int i = 0, j = 0, n = ps.Count; i < n; i++) {
          var p = ps[i];
          {
            var modifiers = "";

            foreach (var modifier in p.Modifiers) {
              modifiers = modifiers + modifier.ValueText + " ";
            }

            // Avoid the refs when emitting the condition
            if (!String.IsNullOrEmpty(modifiers)) {
              if (0 < j) {
                actualParameters += ",";
                formalParameters += ",";
              }

              var name = p.Identifier.ValueText;
              actualParameters += name;
              formalParameters += p.Type + " " + name;

              j++;
            }
          }
        }

        // todo: just use invocation, but replace the method name with __PreconditionMarker!
        var preconditionMarker = "__PreconditionMarker(" + actualParameters + ");";

        Utils.Trace("Generating the Postcondition marker");

        var postconditionMarker = "__PostconditionMarker(" + (actualParameters.Length > 0 ? actualParameters : "");

        BinaryExpressionSyntax assignment = null;
        var invocationExpression = extracted.InvocationNameToken.Parent.Parent as InvocationExpressionSyntax;
        if (invocationExpression != null) {
          var expression = invocationExpression.Parent.FirstAncestorOrSelf<ExpressionSyntax>();
          if (expression != null && expression.Kind == SyntaxKind.AssignExpression) {
            // TODO: what about += or local declarations? should they be included?
            assignment = (BinaryExpressionSyntax)expression;
          }
        }

        if (assignment == null) { // then the extraction was at the statement level
          postconditionMarker += (actualParameters.Length > 0 ? "," : "") + "false";
        } else {
          postconditionMarker += (actualParameters.Length > 0 ? "," : "") + assignment.Left.GetText() + ", true";
        }
        postconditionMarker += ");";

        Utils.Trace("Searching for the enclosing method");

        // now need to climb up to enclosing method so definitions of the markers can be inserted
        MethodDeclarationSyntax containingMethod = containingStatement.FirstAncestorOrSelf<MethodDeclarationSyntax>();
        if (containingMethod == null) {
          Utils.Trace("No enclosing method found: Aborting");
          return null;
        }

        Utils.Trace("Enclosing method found");

        Utils.Trace("Computing string positions for dummy methods");

        var beforeMethodText = oldText.Substring(0, containingMethod.Span.Start - 1);
        var inMethodBeforeRefactoringText = oldText.Substring(containingMethod.Span.Start, this.textSpan.Start - containingMethod.Span.Start);
        var remaining = oldText.Substring(this.textSpan.End);

        Utils.Trace("Adding dummy methods");

        var preconditionMarkerDef = "[Pure]\npublic void __PreconditionMarker(" + formalParameters + "){}\n";

        // It is generic only if there is a parameter representing the return value
        var postconditionMarkerDef = assignment != null ?
          "[Pure]\npublic void __PostconditionMarker<T>(" + formalParameters + (formalParameters.Length > 0 ? "," : "") + "T x,bool returnValue){}\n"
          :
          "[Pure]\npublic void __PostconditionMarker(" + formalParameters + (formalParameters.Length > 0 ? "," : "") + " bool returnValue = false){}\n"
          ;

        var newProg = String.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}",
          beforeMethodText, preconditionMarkerDef, postconditionMarkerDef,
          inMethodBeforeRefactoringText, preconditionMarker, sub, postconditionMarker,
          remaining);

        var newSolution = localDocument.Project.Solution.WithDocumentText(document.Id, new StringText(newProg));
        //var newSolution = localDocument.Project.Solution.UpdateDocument(document.Id, new StringText(newProg));
        localDocument = newSolution.GetDocument(document.Id);
        var newTree = localDocument.GetSyntaxTree();

        Utils.Trace("Dumping annotated tree");

        Utils.DumpCSFile(@"AnnotatedTree.cs", newTree.GetText().ToString(), @"Program with marker explicits");

        var newRoot = (CompilationUnitSyntax)newTree.GetRoot();
        if (newRoot.ContainsDiagnostics) return null;

        Utils.Trace("Searching the extracted method");

        MethodDeclarationSyntax newExtractedMethod, originalMethod;
        newExtractedMethod = extracted.MethodDeclarationNode as MethodDeclarationSyntax;
        if (newExtractedMethod == null) {
          Utils.Trace("Extracted method not found: aborting");
          return null;
        }

        Utils.Trace("Extracted method found");

        originalMethod = newExtractedMethod;


        // Getting the name of the method
        MethodDeclarationSyntax methodToAnalyze = containingStatement.FirstAncestorOrSelf<MethodDeclarationSyntax>();
        if (methodToAnalyze == null) {
          Utils.Trace("Extractee method not found: aborting");

          return null;
        }
        Utils.Trace("Extractee method found.");

        Utils.TraceTime("Generating annotated tree", DateTime.Now - clock);
        clock = DateTime.Now; // reset the clock

        // now let Clousot see the rewritten unit

        Utils.Trace("Running Clousot to extract <Ps, Qs>");

        newExtractedMethod = ExtractAndAddPsQs(ref cancellationToken, localDocument, newRoot, newExtractedMethod);

        Utils.TraceTime("Running Clousot to extract <Ps, Qs>", DateTime.Now - clock);
        clock = DateTime.Now;

        if (newExtractedMethod == null) {
          Utils.Trace("Aborting: failed to infer <Ps, Qs>");

          return null;
        }

        Utils.Trace("Checking we have new contracts");

#if DEBUG || TRACE
        // If we added some contract
        if (newExtractedMethod == originalMethod) {
          Utils.Trace("no new contract, but we continue to get the <Pm, Qm>");
          //return null;
        }
#endif

        Utils.Trace("Creating a new tree");

        var refactoredTree = (SyntaxNode) extracted.Document.GetSyntaxRoot().ReplaceNode(originalMethod, Formatter.Annotation.AddAnnotationTo(newExtractedMethod));
          //(SyntaxNode)resultingTree.ReplaceNode(originalMethod, Formatter.Annotation.AddAnnotationTo(newExtractedMethod));

        Utils.DumpCSFile(@"RefactoredProgram.cs", refactoredTree.ToFullString(), "Program with extracted contracts (Ps, Qs)");
        //Utils.DumpCSFile(@"RefactoredProgram.cs", refactoredTree.GetFullText(), "Program with extracted contracts (Ps, Qs)");

        var annotatedMethod = ExtractAndAddPmQm(ref cancellationToken, localDocument, refactoredTree, newExtractedMethod);

        if (annotatedMethod == null) {
          Utils.Trace("Aborting: failed to annotate the method");
          return null;
        }

        Utils.TraceTime("Running Clousot to extract <Pm, Qm>", DateTime.Now - clock);

        if (annotatedMethod != newExtractedMethod) {
          Utils.Trace("Found new contracts, updating the method");
          Utils.TraceTime("Done!", DateTime.Now - start);

          refactoredTree = (SyntaxNode)extracted.Document.GetSyntaxRoot().ReplaceNode(originalMethod, Formatter.Annotation.AddAnnotationTo(annotatedMethod));
        }
        Utils.TraceTime("Done!", DateTime.Now - start);

        return localDocument.WithSyntaxRoot(refactoredTree);
        //return this.editFactory.CreateTreeTransformEdit(localDocument.Project.Solution, localDocument.GetSyntaxTree(), refactoredTree);
      }
    }

    private MethodDeclarationSyntax ExtractAndAddPsQs(ref CancellationToken cancellationToken, Microsoft.CodeAnalysis.Document localDocument, CompilationUnitSyntax newRoot, MethodDeclarationSyntax newExtractedMethod)
    {
      // Let Clousot see the rewritten unit

      var connectionToClousot = new RoslynToCCICodeModel.ClousotGlue();
      foreach (var result in connectionToClousot.AnalyzeMeAUnit(localDocument, newRoot, cancellationToken, this.options,
        new string[] { "-extractmethodmode", "-arrays:arraypurity", "-suggest:requires" /*"-memberNameSelect",*/ }))
      {
        Utils.Trace("Getting Clousot Result");

        String condition;
        ContractKind kind;
        if (result.IsExtractMethodSuggestion && result.Message.TryParseSuggestion(out kind, out condition))
        {
          Utils.Trace("Inserting Clousot result. New Condition: " + condition);

          // Parse the condition
          var newContract = SyntaxFactory.ParseStatement(condition + Environment.NewLine);

          newExtractedMethod = newExtractedMethod.InsertStatements(newContract, kind);
        }
        else
        {
          Utils.Trace("Skipped Clousot output : " + result.Message);
        }
      }

      return newExtractedMethod;
    }

    private MethodDeclarationSyntax ExtractAndAddPmQm(ref CancellationToken cancellationToken,
      Microsoft.CodeAnalysis.Document localDocument, SyntaxNode oldRoot, MethodDeclarationSyntax newExtractedMethod) {
      var newText = oldRoot.ToFullString();

      Utils.DumpCSFile("BeforePmQm.cs", newText);

      var newSolution = localDocument.Project.Solution.WithDocumentText(localDocument.Id, new StringText(newText));
      //var newSolution = localDocument.Project.Solution.UpdateDocument(localDocument.Id, new StringText(newText));
      var newLocalDocument = newSolution.GetDocument(localDocument.Id);

      localDocument = null; // to be sure we do not use it

      var newTree = newLocalDocument.GetSyntaxTree();

      Utils.Trace("Running Clousot to infer <Pm, Qm> and <Pr, Qr>");

      var newRoot = (CompilationUnitSyntax)newTree.GetRoot();
      if (newRoot.ContainsDiagnostics) {
        Utils.Trace("Aborting: the new tree has syntax errors");

        return null;
      }

      // Let Clousot see the rewritten unit
      var connectionToClousot = new RoslynToCCICodeModel.ClousotGlue();
      foreach (var result in connectionToClousot.AnalyzeMeAUnit(newLocalDocument, newRoot, cancellationToken, this.options,
        new string[] { "-extractmethodmoderefine", newExtractedMethod.Identifier.ValueText, "-premode", "combined", "-suggest", "methodensures", "-suggest:requires" }))
      {
        Utils.Trace("Getting Clousot Result");

        String condition;
        ContractKind kind;
        if (result.Message.TryParseSuggestion(out kind, out condition)) {
          Utils.Trace("Inserting Clousot result. New Condition: " + condition);

          // Parse the condition
          var newContract = SyntaxFactory.ParseStatement(condition + Environment.NewLine);

          if (!newContract.ContainsDiagnostics) {
            newExtractedMethod = newExtractedMethod.InsertStatements(newContract, kind);
          } else {
            Utils.Trace("\tskipped, as it contains syntax errors");
          }
        }
      }

      return newExtractedMethod;
    }


  }

  public class AddMarkerMethods : SyntaxRewriter {
    private MethodDeclarationSyntax pre;
//    private MethodDeclarationSyntax post;
    private TypeDeclarationSyntax type;
    public AddMarkerMethods(TypeDeclarationSyntax type, MethodDeclarationSyntax pre/*, MethodDeclarationSyntax post*/)
        {
            this.type = type;
            this.pre = pre;
            //this.post = post;
        }

    public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node) {
      if (node == this.type) {
        return node.WithMembers(node.Members.Add(this.pre));
      }
      return base.VisitClassDeclaration(node);
    }

  }

}