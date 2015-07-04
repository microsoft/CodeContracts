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
using System.Diagnostics.Contracts;
using Microsoft.VisualStudio.Text;
using Microsoft.Cci.Contracts;
using Microsoft.Cci;
using System.Runtime.InteropServices;
using UtilitiesNamespace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Compilation = Microsoft.CodeAnalysis.Workspace;
using CSharpMember = Microsoft.CodeAnalysis.ISymbol;
using ParseTree = Microsoft.CodeAnalysis.SyntaxTree;
using ParseTreeNode = Microsoft.CodeAnalysis.SyntaxNode;
using Position = Microsoft.CodeAnalysis.Text.LinePosition;
using SourceFile = Microsoft.CodeAnalysis.Document;

using InvocationExpressionSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.InvocationExpressionSyntax;
using ObjectCreationExpressionSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ObjectCreationExpressionSyntax;
using RefKind = Microsoft.CodeAnalysis.RefKind;
using SemanticModel = Microsoft.CodeAnalysis.SemanticModel;
using SymbolKind = Microsoft.CodeAnalysis.SymbolKind;
using TypeKind = Microsoft.CodeAnalysis.TypeKind;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.Threading;
using Microsoft.VisualStudio.Shell;

namespace ContractAdornments {
  public static class IntellisenseContractsHelper {

    public static CSharpMember GetSemanticMember(this ParseTreeNode parseTreeNode, Compilation comp, SourceFile sourceFile) {
      Contract.Requires(parseTreeNode != null);
      Contract.Requires(comp != null);
      Contract.Requires(sourceFile != null);
      Contract.Ensures(Contract.Result<CSharpMember>() == null ||
                       Contract.Result<CSharpMember>().Kind == SymbolKind.Method || 
                       Contract.Result<CSharpMember>().Kind == SymbolKind.Property);

      // The CSharp model can throw internal exceptions at unexpected moments, so we filter them here:
      try
      {
        SemanticModel semanticModel =  ThreadHelper.JoinableTaskFactory.Run(() => sourceFile.GetSemanticModelAsync());

        CSharpMember semanticMember = semanticModel.GetDeclaredSymbol(parseTreeNode);
        if (semanticMember != null)
          goto Success;

        //Can we get our expression tree?
        semanticMember = semanticModel.GetSymbolInfo(parseTreeNode).Symbol;
        if (semanticMember == null)
          return null;

      Success:
        //Is our semanticMember a method?
        if (semanticMember == null || !(semanticMember.Kind == SymbolKind.Method || semanticMember.Kind == SymbolKind.Property))
          return null;

        //Unistantiate
        semanticMember = semanticMember.Uninstantiate();

        return semanticMember;
      }
      catch (ArgumentException)
      {
        return null;
      }
    }
    public static ParseTreeNode GetAnyCallNodeAboveTriggerPoint(ITrackingPoint triggerPoint, ITextSnapshot snapshot, ParseTree parseTree) {
      Contract.Requires(snapshot != null);
      Contract.Requires(parseTree != null);
      Contract.Requires(triggerPoint != null);

      SyntaxNode syntaxRoot;
      if (!parseTree.TryGetRoot(out syntaxRoot))
        return null;

      var leafNode = syntaxRoot.FindToken(triggerPoint.GetPosition(snapshot), false);
      if (leafNode.IsKind(SyntaxKind.None))
        return null;

      //Is anyone in our ancestry a call node?
      var nodeInQuestion = leafNode.Parent;
      ParseTreeNode ptn = null;

      while (nodeInQuestion != null) {
        //Is the node in question a node call?
        var asCall = nodeInQuestion as InvocationExpressionSyntax;
        if (asCall != null) {
          ptn = asCall;
          break;
        }

        var asCtorCall = nodeInQuestion as ObjectCreationExpressionSyntax;
        if (asCtorCall != null) {
          ptn = asCtorCall;
          break;
        }

        //Climb higher up our ancestry for the next iteration
        nodeInQuestion = nodeInQuestion.Parent;
      }

      //Did we successfully find a call node?
      return ptn;
    }
    public static ParseTreeNode GetTargetAtTriggerPoint(ITrackingPoint triggerPoint, ITextSnapshot snapshot, ParseTree parseTree)
    {
      Contract.Requires(snapshot != null);
      Contract.Requires(parseTree != null);
      Contract.Requires(triggerPoint != null);

      SyntaxNode syntaxRoot;
      if (!parseTree.TryGetRoot(out syntaxRoot))
        return null;

      var leafToken = syntaxRoot.FindToken(triggerPoint.GetPosition(snapshot), false);
      if (leafToken.IsKind(SyntaxKind.None))
        return null;

      var leafNode = leafToken.Parent;

      var asPropDecl = leafNode as PropertyDeclarationSyntax;
      if (asPropDecl != null)
      {
          return asPropDecl;
      }

      //Is our leaf node a name?
      var asName = leafNode as IdentifierNameSyntax;
      if (asName == null)
        return null;

      //Is anyone in our ancestry a call node?
      var nodeInQuestion = leafNode;
      var lastNonBinaryOperatorNode = leafNode;
      var lastNode = leafNode;
      while (nodeInQuestion != null) {

        //Is the node in question a node call?
        var asCall = nodeInQuestion as InvocationExpressionSyntax;
        if (asCall != null) {

          //Make sure we aren't on the right side of the call
          MemberAccessExpressionSyntax memberAccessExpression = asCall.Expression as MemberAccessExpressionSyntax;
          if (memberAccessExpression == null || memberAccessExpression.Name == leafNode)
            return null;

          return asCall;
        }

        var asProp = nodeInQuestion as MemberAccessExpressionSyntax;
        if (asProp != null)
        {
            //Make sure we aren't on the left side of the dot
            if (asProp.Name == lastNode && asProp.Name is IdentifierNameSyntax)
            {
                return asProp;
            }
        }

        var asCtorCall = nodeInQuestion as ObjectCreationExpressionSyntax;
        if (asCtorCall != null)
        {
            return asCtorCall;
        }

        var declNode = nodeInQuestion as MemberDeclarationSyntax;
        if (declNode != null)
        {
            if (lastNode == leafNode) { return declNode; }
            return null;
        }

#warning What exactly is this doing?
#if false
        NamedAssignmentNode na = nodeInQuestion as NamedAssignmentNode;
        if (na != null)
        {
            if (na.Identifier == asName)
            {
                return na;
            }
        }
#endif

        //Is our parent a binary operator?
        var asBinaryOperator = nodeInQuestion as BinaryExpressionSyntax;
        if (asBinaryOperator != null) {

          //Make sure we are always on the rightmost of any binary operator
          if (asBinaryOperator.Right != lastNonBinaryOperatorNode)
            return null;

        } else
          lastNonBinaryOperatorNode = nodeInQuestion;

        //Climb higher up our ancestry for the next iteration
        lastNode = nodeInQuestion;
        nodeInQuestion = nodeInQuestion.Parent;
      }

      //Did we successfully find a call node?
      if (nodeInQuestion == null)
        return null;
      if (nodeInQuestion.Kind() != SyntaxKind.InvocationExpression)
        return null;
      var callNode = nodeInQuestion as InvocationExpressionSyntax;

      return callNode;
    }

    public static string FormatContracts(IMethodContract methodContracts) {

      //Did we get proper contracts?
      if (methodContracts == null) {

        //Return a message saying we failed to get contracts
        return "(Failed to properly get contracts)";
      }

      //Do we have any contracts?
      if (IsEmptyContract(methodContracts))
      {
        //Return a message saying we don't have any contracts
        return null;
      }

      //Initialize our string builder
      var sb = new StringBuilder();

      //Append the 'Contracts' header
      sb.Append("Contracts: ");
      sb.Append('\n');

      //Append purity
      if (methodContracts.IsPure) {
        sb.Append("    ");
        sb.Append("[Pure]");
        sb.Append('\n');
      }

      FormatPrePostThrows(methodContracts, sb);

      //Can we get our result from the string builder?
      var result = sb.ToString();

      //Trim the new lines from the end
      result = result.TrimEnd('\n');

      result = SmartFormat(result);

      //Return our result
      return result;
    }

    private static bool IsEmptyContract(IMethodContract methodContracts)
    {
      Contract.Ensures(Contract.Result<bool>() || methodContracts != null);

      return methodContracts == null
              || (methodContracts == ContractDummy.MethodContract)
              || (!methodContracts.IsPure
                  && methodContracts.Preconditions.Count() < 1
                  && methodContracts.Postconditions.Count() < 1
                  && methodContracts.ThrownExceptions.Count() < 1);
    }

    static string IndentString = Environment.NewLine + "             ";

    private static void FormatPrePostThrows(IMethodContract methodContracts, StringBuilder sb)
    {
      Contract.Requires(methodContracts != null);
      Contract.Requires(sb != null);

      if (methodContracts.Postconditions != null)
      {
          //Append our preconditions
          foreach (var pre in methodContracts.Preconditions)
          {
              if (pre == null) continue;
              if (pre.OriginalSource == null) continue;
              sb.Append("    ");
              sb.Append("requires ");
              sb.Append(pre.OriginalSource.Replace(Environment.NewLine, IndentString));
              sb.Append('\n');
          }
      }

      if (methodContracts.Postconditions != null)
      {
          //Append our postconditions
          foreach (var post in methodContracts.Postconditions)
          {
              if (post == null) continue;
              if (post.OriginalSource == null) continue;
              sb.Append("    ");
              sb.Append("ensures ");
              sb.Append(post.OriginalSource.Replace(Environment.NewLine, IndentString));
              sb.Append('\n');
          }
      }

      if (methodContracts.ThrownExceptions != null)
      {
          //Append our on throw conditions
          foreach (var onThrow in methodContracts.ThrownExceptions)
          {
              if (onThrow == null) continue;
              if (onThrow.ExceptionType == null) continue;
              if (onThrow.Postcondition == null) continue;
              if (onThrow.Postcondition.OriginalSource == null) continue;

              sb.Append("    ");
              sb.Append("ensures on throw of ");
              var onThrowType = TypeHelper.GetTypeName(onThrow.ExceptionType, NameFormattingOptions.OmitContainingType | NameFormattingOptions.OmitContainingNamespace);
              sb.Append(onThrowType);
              sb.Append(" that ");
              sb.Append(onThrow.Postcondition.OriginalSource.Replace(Environment.NewLine, IndentString));
              sb.Append('\n');
          }
      }
    }

    private static string SmartFormat(string result)
    {
        if (string.IsNullOrEmpty(result)) return result;
        if (ContractsPackageAccessor.Current.VSOptionsPage != null && ContractsPackageAccessor.Current.VSOptionsPage.SmartFormatting)
        {
            var startTime = DateTime.Now;

            var trySmartReplace = result;

            try
            {
                //Simplify how contracts look
                trySmartReplace = trySmartReplace.SmartReplace("Contract.OldValue<{0}>({1})", "old({1})");
                trySmartReplace = trySmartReplace.SmartReplace("Contract.OldValue({0})", "old({0})");
                trySmartReplace = trySmartReplace.SmartReplace("Contract.Result<{0}>()", "result");
                trySmartReplace = trySmartReplace.SmartReplace("Contract.ValueAtReturn<{0}>({1})", "out({1})");
                trySmartReplace = trySmartReplace.SmartReplace("Contract.ValueAtReturn({0})", "out({0})");
                trySmartReplace = trySmartReplace.SmartReplace("Contract.ForAll<{0}>({1})", "forall({1})");
                trySmartReplace = trySmartReplace.SmartReplace("Contract.ForAll({0})", "forall({0})");
            }
            catch (Exception)
            {
                trySmartReplace = null;
                ContractsPackageAccessor.Current.Logger.WriteToLog("Error: Smart formatting failed!");
                ContractsPackageAccessor.Current.Logger.WriteToLog(result);
            }

            if (trySmartReplace != null)
                result = trySmartReplace;

            var elapsedTime = DateTime.Now - startTime;
            ContractsPackageAccessor.Current.Logger.WriteToLog("\t(Smart formatting took " + elapsedTime.Milliseconds + "ms)");
        }
        return result;
    }

    internal static string FormatPropertyContracts(IMethodContract getter, IMethodContract setter)
    {
        //Initialize our string builder
        var sb = new StringBuilder();

        //Append the 'Contracts' header
        //sb.Append("Contracts: ");
        //sb.Append('\n');

        if (!IsEmptyContract(getter))
        {
            sb.Append("get ");
            sb.Append('\n');
            FormatPrePostThrows(getter, sb);
        }
        if (!IsEmptyContract(setter))
        {
            sb.Append("set ");
            sb.Append('\n');
            FormatPrePostThrows(setter, sb);
        }
        //Can we get our result from the string builder?
        var result = sb.ToString();

        if (result == "") return null;

        //Trim the new lines from the end
        result = result.TrimEnd('\n');

        result = SmartFormat(result);

        //Return our result
        return result;
    }
  }
}
