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
using System.Diagnostics.Contracts;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;

namespace RoslynToCCICodeModel {

    public class MethodSpanFinder : CSharpSyntaxWalker
    {

    private Dictionary<TextSpan, BaseMethodDeclarationSyntax> span2Method = new Dictionary<TextSpan, BaseMethodDeclarationSyntax>();

    private MethodSpanFinder() {
    }

    public static Dictionary<TextSpan, BaseMethodDeclarationSyntax> GetMethodSpans(CompilationUnitSyntax compilationUnit)
    {
      var msf = new MethodSpanFinder();
      msf.Visit(compilationUnit);
      return msf.span2Method;
    }

    public static Dictionary<TextSpan, BaseMethodDeclarationSyntax> GetMethodSpans(SyntaxNode syntaxNode)
    {
      var msf = new MethodSpanFinder();
      msf.Visit(syntaxNode);
      return msf.span2Method;
    }
    
    public override void VisitMethodDeclaration(MethodDeclarationSyntax node) {
      var span = node.Identifier.Span;
      this.span2Method.Add(span, node);
      return;
    }

    public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
    {
      var span = node.Identifier.Span;
      this.span2Method.Add(span, node);
      return;
    }

    // TODO: ConversionOperator, Destructor, Operator
  }

    public class EnclosingMethodFinder : CSharpSyntaxWalker
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.span2Methods != null);
    }

    private readonly TextSpanDictionary<BaseMethodDeclarationSyntax> span2Methods;

    private EnclosingMethodFinder() 
    {
      this.span2Methods = new TextSpanDictionary<BaseMethodDeclarationSyntax>();
    }

    public static TextSpanDictionary<BaseMethodDeclarationSyntax> GetMethodSpans<S>(S node)
      where S: SyntaxNode
    {
      var visitor = new EnclosingMethodFinder();
      visitor.Visit(node);

      return visitor.span2Methods;
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
      this.span2Methods.Add(node.FullSpan, node);
    }

    public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
    {
      this.span2Methods.Add(node.FullSpan, node);
    }
  }

  public class TextSpanDictionary<V>
  {
    private readonly Dictionary<TextSpan, V> info;

    public TextSpanDictionary()
    {
      this.info = new Dictionary<TextSpan, V>();
    }

    public void Add(TextSpan k, V v)
    {
      this.info.Add(k, v);
    }

    public bool TryGetValue(TextSpan  k, out V v)
    {
      return this.info.TryGetValue(k, out v);
    }

    public bool TryGetValueContainingSpan(TextSpan span, out V v)
    {
      foreach (var pair in this.info)
      {
        if (pair.Key.Contains(span))
        {
          v = pair.Value;
          return true;
        }
      }
      v = default(V);
      return false;
    }
  }

  internal class ReturnStatementFinder : CSharpSyntaxWalker {

    private List<TextSpan> spans = new List<TextSpan>();

    private ReturnStatementFinder() {
    }

    public static List<TextSpan> GetReturnStatementSpans(MethodDeclarationSyntax method) {
      var rsf = new ReturnStatementFinder();
      rsf.Visit(method);
      return rsf.spans;
    }

    public override void VisitReturnStatement(ReturnStatementSyntax node) {
      this.spans.Add(node.Span);
    }
  }
}