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
using RoslynToCCICodeModel;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace ClousotExtension
{
  [ContractVerification(true)]
  public static class ISymbolExtensions
  {
    public static bool TryGetType(this ISymbol symbol, out ITypeSymbol type)
    {
      Contract.Requires(symbol != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out type) != null);

      var asLocal = symbol as ILocalSymbol;
      if (asLocal != null)
      {
        type = asLocal.Type;
        Contract.Assume(type != null, "Missing contract on Roslyn API");
        return true;
      }
      var asParam = symbol as IParameterSymbol;
      if (asParam != null)
      {
        type = asParam.Type;
        Contract.Assume(type != null, "Missing contract on Roslyn API");
        return true;
      }
      var asField = symbol as IFieldSymbol;
      if (asField != null)
      {
        type = asField.Type;
        Contract.Assume(type != null, "Missing contract on Roslyn API");
        return true;
      }

      type = null;
      return false;
    }
  }

  public static class StatementSyntaxExtensions
  {
    static public bool IsPrecondition(this StatementSyntax statementSyntax)
    {
      var exprStmt = statementSyntax as ExpressionStatementSyntax;
      if (exprStmt == null) return false;
      var expr = exprStmt.Expression as InvocationExpressionSyntax;
      if (expr == null) return false;
      var method = expr.Expression as MemberAccessExpressionSyntax;
      if (method == null) return false;
      return (method.Name.Identifier.ValueText.Equals("Requires"));
    }

    static public bool IsPostcondition(this StatementSyntax statementSyntax)
    {
      var exprStmt = statementSyntax as ExpressionStatementSyntax;
      if (exprStmt == null) return false;
      var expr = exprStmt.Expression as InvocationExpressionSyntax;
      if (expr == null) return false;
      var method = expr.Expression as MemberAccessExpressionSyntax;
      if (method == null) return false;
      return (method.Name.Identifier.ValueText.Equals("Ensures"));
    }
  }

  public static class MethodDeclarationSyntaxExtensions
  {
    static public TMethodDeclarationSyntax InsertStatements<TMethodDeclarationSyntax>(this TMethodDeclarationSyntax method, StatementSyntax statement, ContractKind kind, bool avoidDuplicates = true)
      where TMethodDeclarationSyntax : BaseMethodDeclarationSyntax
    {
      Contract.Requires(statement != null);

      if (method.Body == null) return null;
      var oldStatements = method.Body.Statements;
      var newStatements = new List<StatementSyntax>();
      var conditionText = statement.GetText();
      
      var n = oldStatements.Count;
      var i = 0;
      // Take a prefix of the oldStatments before inserting "statement".
      if (kind == ContractKind.Precondition)
      {
        // prefix = "precondition" ==> all existing preconditions
        while (i < n)
        {
          if (!oldStatements[i].IsPrecondition()) break;

          if (avoidDuplicates && oldStatements[i].GetText() == conditionText) return method;
          
          newStatements.Add(oldStatements[i]);
          i++;
        }
        newStatements.Add(statement);
        while (i < n)
        {
          newStatements.Add(oldStatements[i]);
          i++;
        }
      }
      else if (kind == ContractKind.Postcondition)
      {
        // prefix = "postcondition" ==> all existing contracts
        while (i < n && (oldStatements[i].IsPostcondition() || oldStatements[i].IsPrecondition()))
        {
          if (avoidDuplicates && oldStatements[i].IsPostcondition() && oldStatements[i].GetText() == conditionText) return method;

          newStatements.Add(oldStatements[i]);
          i++;
        }
        while (i < n)
        {
          if (!oldStatements[i].IsPostcondition()) break;

          if (avoidDuplicates && oldStatements[i].GetText() == conditionText) return method;

          newStatements.Add(oldStatements[i]);
          i++;
        }
        newStatements.Add(statement);
        while (i < n)
        {
          newStatements.Add(oldStatements[i]);
          i++;
        }
      }
      else
      {
        newStatements.Add(statement);
        newStatements.AddRange(oldStatements);
      }
      var x = SyntaxFactory.List<StatementSyntax>(newStatements);

      if (method is ConstructorDeclarationSyntax)
      {
        var methodConstructor = method as ConstructorDeclarationSyntax;
        return SyntaxFactory.ConstructorDeclaration(
          attributeLists: methodConstructor.AttributeLists,
          modifiers: methodConstructor.Modifiers,
          identifier: methodConstructor.Identifier,
          parameterList: method.ParameterList,
          initializer: null,
          body: SyntaxFactory.Block(statements: x)
        ) as TMethodDeclarationSyntax;
      }
      if (method is ConversionOperatorDeclarationSyntax)
      {
        throw new NotImplementedException();
      }
      if (method is DestructorDeclarationSyntax)
      {
        throw new NotImplementedException();
      }
      if (method is MethodDeclarationSyntax)
      {
        var methodMethod = method as MethodDeclarationSyntax;
        return SyntaxFactory.MethodDeclaration(
          attributeLists: methodMethod.AttributeLists,
          modifiers: methodMethod.Modifiers,
          returnType: methodMethod.ReturnType,
          explicitInterfaceSpecifier: methodMethod.ExplicitInterfaceSpecifier,
          identifier: methodMethod.Identifier,
          typeParameterList: methodMethod.TypeParameterList,
          parameterList: method.ParameterList,
          constraintClauses: methodMethod.ConstraintClauses,
          body: SyntaxFactory.Block(statements: x)
        ) as TMethodDeclarationSyntax;
      }
      if (method is OperatorDeclarationSyntax)
      {
        throw new NotImplementedException();
      }
      throw new NotSupportedException();
    }

    static public bool ContainsSuggestion(this BaseMethodDeclarationSyntax method, string suggestion)
    {
      if (method == null || method.Body == null)
      {
        return false;
      }

      // Smarter way is to look up to the end of contracts
      foreach (var statement in method.Body.Statements)
      {
        if (statement.ToString() == suggestion)
          return true;
      }

      return false;

    }
  }

  public static class StringExtensions
  {
    public static string AddParentheses(this string s, char open = '(', char close = ')')
    {
      if (s == null)
      {
        return s;
      }
      return string.Format("{0} {1} {2}", open, s, close); 
    }

    public static ContractKind ToContractKind(this string s)
    {
      s = s.ToLower();
      switch (s)
      {
        case "assume":
          return ContractKind.Assume;

        case "code fix":
          return ContractKind.CodeFix;

        case "requires":
          return ContractKind.Precondition;

        case "ensures":
          return ContractKind.Postcondition;

        case "invariant":
          return ContractKind.ObjectInvariant;
      }

      if (s.Contains("assume"))
        return ContractKind.Assume;

      if (s.Contains("code fix"))
        return ContractKind.CodeFix;

      if(s.Contains("precondition"))
        return ContractKind.Precondition;

      if(s.Contains("postcondition"))
        return ContractKind.Postcondition;

      if(s.Contains("invariant"))
        return ContractKind.ObjectInvariant;

      if (s.Contains("abstract state"))
        return ContractKind.AbstractState;


      throw new InvalidOperationException();
    }

    public static bool TryParseSuggestion(this string s, out ContractKind kind, out string condition)
    {
      if (s == null || !s.ToLower().Contains("suggestion"))
      {
        kind = default(ContractKind);
        condition = null;
        return false;
      }

      var where = s.LastIndexOf(':')+1;
      if (where > 0)
      {
        condition = s.Substring(where);

        if (s.Contains("Requires"))
        {
          kind = ContractKind.Precondition;
          return true;

        }
        else if (s.Contains("Ensures"))
        {
          kind = ContractKind.Postcondition;
          return true;
        }
        else if (s.Contains("Invariant"))
        {
          kind = ContractKind.ObjectInvariant;
          return true;
        }
        else
        {
          kind = default(ContractKind);
          condition = null;
          return false;
        }
      }

      kind = default(ContractKind);
      condition = null;
      return false;
    }
  }

  public static class DocumentExtensions
  {
      public static SyntaxNode GetSyntaxRoot(this Document d, CancellationToken token)
      {
          return d.GetSyntaxRootAsync(token).Result;
      }
      public static SyntaxNode GetSyntaxRoot(this Document d)
      {
          return d.GetSyntaxRootAsync().Result;
      }
      public static SyntaxTree GetSyntaxTree(this Document d, CancellationToken token)
      {
          return d.GetSyntaxTreeAsync(token).Result;
      }

      public static SemanticModel GetSemanticModel(this Document d, CancellationToken token)
      {
          return d.GetSemanticModelAsync(token).Result;
      }

      public static SourceText GetText(this Document d)
      {
          return d.GetTextAsync().Result;
      }
  }

}
