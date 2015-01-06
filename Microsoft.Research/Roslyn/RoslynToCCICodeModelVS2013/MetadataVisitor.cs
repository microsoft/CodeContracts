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
using Microsoft.Cci.Contracts;
using Microsoft.Cci.MutableContracts;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace CSharp2CCI {

  public partial class NodeVisitor : CSharpSyntaxVisitor<object> {

    #region Fields
    private readonly SemanticModel semanticModel;
    private IMetadataHost host;
    private INameTable nameTable;
    protected System.Collections.Stack path = new System.Collections.Stack();
    private ReferenceMapper mapper;
    private IMethodDefinition/*?*/ entryPoint = null;
    ISourceLocationProvider/*?*/ sourceLocationProvider;

    IContractMethods contractMethods;

    #endregion

    public NodeVisitor(IMetadataHost host, SemanticModel semanticModel, ISourceLocationProvider/*?*/ sourceLocationProvider) {
      this.host = host;
      this.nameTable = host.NameTable;
      this.semanticModel = semanticModel;
      this.sourceLocationProvider = sourceLocationProvider;

      this.contractMethods = new ContractMethods(host);
      // translate the entire metadata model for the assembly
      // the actual visit is just to get method bodies for any methods
      // defined in the cone that is visited.
      this.mapper = ReferenceMapper.TranslateAssembly(host, semanticModel.Compilation.Assembly);
    }

    public override object VisitCompilationUnit(CompilationUnitSyntax node) {
      foreach (var member in node.Members) {
        this.Visit(member); // don't care about return value, just calling it to navigate down to method bodies
      }
      var a= this.mapper.Map(this.semanticModel.Compilation.Assembly);

      var assembly = (Assembly)a;
      if (this.entryPoint != null) {
        assembly.Kind = ModuleKind.ConsoleApplication;
        assembly.EntryPoint = this.entryPoint;
      }

      return a;
    }
    public override object VisitNamespaceDeclaration(NamespaceDeclarationSyntax node) {
      foreach (var member in node.Members) {
        this.Visit(member); // don't care about return value, just calling it to navigate down to method bodies
      }
      var roslynNamespace = (INamespaceSymbol) this.semanticModel.GetDeclaredSymbol(node);
      return this.mapper.Map(roslynNamespace);
    }

    public override object VisitClassDeclaration(ClassDeclarationSyntax node) {
      foreach (var member in node.Members) {
        this.Visit(member); // don't care about return value, just calling it to navigate down to method bodies
      }
      var roslynType = (ITypeSymbol) this.semanticModel.GetDeclaredSymbol(node);
      return this.mapper.Map(roslynType);
    }

    public override object VisitPropertyDeclaration(PropertyDeclarationSyntax node) {
      return null; // don't need to do anything: properties get visited as part of walking the semantic model
    }

    public override object VisitFieldDeclaration(FieldDeclarationSyntax node) {
      return null; // don't need to do anything: fields get visited as part of walking the semantic model
      // TODO: what about any initializer?
    }

    public override object VisitEnumDeclaration(EnumDeclarationSyntax node) {
      foreach (var member in node.Members) {
        this.Visit(member);
      }
      var roslynEnum = this.semanticModel.GetDeclaredSymbol(node) as INamedTypeSymbol;
      return this.mapper.Map(roslynEnum);
    }

    private object VisitMethod(BaseMethodDeclarationSyntax node){
      IMethodSymbol o = this.semanticModel.GetDeclaredSymbol(node) as IMethodSymbol;
      // TODO: What if o isn't a MethodSymbol?

      var m = (MethodDefinition)this.mapper.Map(o);

      if (node.Body == null) {
        m.IsAbstract = true;
      } else {
        try
        {
          var s = GetMethodBody(m, node, true);
          var body = new SourceMethodBody(this.host, this.sourceLocationProvider, null)
          {
            LocalsAreZeroed = true,
            MethodDefinition = m,
            Block = s,
          };
          body.TrackExpressionSourceLocations = true;

          //var mcAndBody = ContractExtractor.SplitMethodBodyIntoContractAndCode(this.host, body, null /*pdbReader*/);
          //body.Block = mcAndBody.BlockStatement;

          m.Body = body;
        }
        catch
        {
          m.IsAbstract = true; // pretend if we can't translate it.
        }
      }
      return m;
    }
    public override object VisitMethodDeclaration(MethodDeclarationSyntax node) {
      var m = (MethodDefinition) this.VisitMethod(node);
      if (this.entryPoint == null && !m.IsAbstract && m.IsStatic &&
        (TypeHelper.TypesAreEquivalent(m.Type, this.host.PlatformType.SystemVoid) || TypeHelper.TypesAreEquivalent(m.Type, this.host.PlatformType.SystemInt32)) &&
        (m.ParameterCount == 0 || (m.ParameterCount == 1 && IsStringArray(IteratorHelper.First(m.Parameters).Type)))
        )
        this.entryPoint = m;
      return m;
    }
    public override object VisitConstructorDeclaration(ConstructorDeclarationSyntax node) {
      return this.VisitMethod(node);
    }

    public BlockStatement GetMethodBody(IMethodDefinition cciMethod, BaseMethodDeclarationSyntax roslynMethod, bool extractContracts) {
      Contract.Requires(roslynMethod.Body != null);
      var expressionVisitor = new ExpressionVisitor(this.host, this.semanticModel, this.mapper, cciMethod);
      var sv = new StatementVisitor(this.host, this.semanticModel, this.mapper, cciMethod, expressionVisitor);


      var block = roslynMethod.Body;

      var statements = new List<IStatement>();

      if (roslynMethod.CSharpKind() == SyntaxKind.ConstructorDeclaration) {
        var roslynCtor = roslynMethod as ConstructorDeclarationSyntax;
        var es = new List<IExpression>();
        var ps = new List<IParameterTypeInformation>();
        ITypeReference ctorClass = null;

        if (roslynCtor.Initializer != null) {
          switch (roslynCtor.Initializer.ThisOrBaseKeyword.CSharpKind()) {
            case SyntaxKind.BaseKeyword:
              ctorClass = cciMethod.ContainingTypeDefinition.BaseClasses.First(); // safe to assume there is always at least one? what if there are more than one?
              break;
            case SyntaxKind.ThisKeyword:
              ctorClass = cciMethod.ContainingType;
              break;
            default:
              var msg = String.Format("Was unable to convert {0} which was used for a base/this ctor call in {1}",
                roslynCtor.Initializer.ThisOrBaseKeyword, MemberHelper.GetMethodSignature(cciMethod));
              throw new ConverterException(msg);
          }
          foreach (var a in roslynCtor.Initializer.ArgumentList.Arguments) {
            var e = expressionVisitor.Visit(a);
            es.Add(e);
          }
        } else {
          // Create a call to the nullary base constructor
          ctorClass = cciMethod.ContainingTypeDefinition.BaseClasses.First(); // safe to assume there is always at least one? what if there are more than one?
        }
        var thisRef = new ThisReference() { Type = cciMethod.ContainingType, };
        var ctorRef = new Microsoft.Cci.MutableCodeModel.MethodReference() {
          CallingConvention = CallingConvention.HasThis,
          ContainingType = ctorClass,
          GenericParameterCount = 0,
          InternFactory = this.host.InternFactory,
          Name = nameTable.Ctor,
          Parameters = ps,
          Type = this.host.PlatformType.SystemVoid,
        };
        statements.Add(
          new ExpressionStatement() {
            Expression = new MethodCall() {
              MethodToCall = ctorRef,
              IsStaticCall = false,
              ThisArgument = thisRef,
              Type = this.host.PlatformType.SystemVoid,
              Arguments = es,
            }
          }
          );
      }

      statements.Add(
        new EmptyStatement() { Locations = Helper.SourceLocation(this.semanticModel.SyntaxTree, block.OpenBraceToken), }
        );

      foreach (var s in block.Statements) {
        var s_prime = sv.Visit(s);
        statements.Add(s_prime);
      }

      // REVIEW: need to do this only if debugging info is needed?
      // (But if it isn't done, then there can be unreachable nops
      // corresponding to close curlys for blocks because of return
      // statements within the block. When that happens at the method
      // level, then peverify is unhappy with the assembly.)
      //if (false) {
      //  // commented this out because it is needed only if this is being used
      //  // to generate IL that in the end should pass peverify (and run).
      //  statements = CreateUnifiedReturnPoint(statements, cciMethod.Type);
      //}

      BlockStatement bs = new BlockStatement() {
        Statements = statements,
      };
      return bs;
    }

    private List<IStatement> CreateUnifiedReturnPoint(List<IStatement> statements, ITypeReference returnType) {
      var returnLabel = new LabeledStatement() { Statement = new EmptyStatement() };
      var localForReturnValue =
        new LocalDefinition() {
          Name = this.nameTable.GetNameFor("_returnValue"),
          Type = returnType,
        };
      var rr = new ReplaceReturns(this.host, localForReturnValue, returnLabel);
      statements = rr.Rewrite(statements);
      statements.Add(returnLabel);
      var returnStatement = new ReturnStatement();
      if (returnType.TypeCode != PrimitiveTypeCode.Void)
        returnStatement.Expression = new BoundExpression() { Definition = localForReturnValue };
      statements.Add(returnStatement);
      return statements;
    }

    private bool IsStringArray(ITypeReference typeReference) {
      IArrayTypeReference atr = typeReference as IArrayTypeReference;
      if (atr == null) return false;
      return TypeHelper.TypesAreEquivalent(atr.ElementType, this.host.PlatformType.SystemString);
    }

    public override object DefaultVisit(SyntaxNode node) {
      // If you hit this, it means there was some sort of CS construct
      // that we haven't written a conversion routine for.  Simply add
      // it above and rerun.
      var typeName = node.GetType().ToString();
      var msg = String.Format("Was unable to convert a {0} node to CCI", typeName);
      throw new ConverterException(msg);
    }

    private sealed class ReplaceReturns : CodeRewriter {
      private LocalDefinition result;
      private LabeledStatement newExit;
      internal ReplaceReturns(IMetadataHost host, LocalDefinition r, LabeledStatement b)
        : base(host) {
        this.result = r;
        this.newExit = b;
      }

      public override IStatement Rewrite(IReturnStatement returnStatement) {
        if (returnStatement.Expression == null)
          return new GotoStatement() { TargetStatement = this.newExit, };
        List<IStatement> stmts = new List<IStatement>();
        stmts.Add(MakeAssignmentStatement(this.result, returnStatement.Expression, returnStatement.Locations));
        stmts.Add(new GotoStatement() { TargetStatement = newExit });
        return new BlockStatement() { Statements = stmts };
      }
      private static IStatement MakeAssignmentStatement(object target, IExpression source, IEnumerable<Microsoft.Cci.ILocation> locations) {
        return new ExpressionStatement() {
          Expression = new Assignment() {
            Target = new TargetExpression() {
              Definition = target,
            },
            Source = source,
            Type = source.Type,
          },
          Locations = new List<ILocation>(locations),
        };
      }

    }

  }

  public class ConverterException : Exception {
    public ConverterException(string s) : base(s) { }
  }

}
