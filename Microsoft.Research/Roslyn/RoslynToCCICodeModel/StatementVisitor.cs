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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp.Semantics;
using Microsoft.Cci;
using Microsoft.Cci.MutableCodeModel;
using System.Diagnostics.Contracts;
using R = Microsoft.CodeAnalysis.Common.Symbols;
using Microsoft.CodeAnalysis.Common;
using Microsoft.CodeAnalysis.Common.Semantics;

namespace CSharp2CCI {
  
  internal class StatementVisitor : Microsoft.CodeAnalysis.CSharp.SyntaxVisitor<IStatement> {

    #region Fields

    private IMetadataHost host;
    CommonSemanticModel semanticModel;
    ReferenceMapper mapper;
    IMethodDefinition method;
    SyntaxTree tree;
    ExpressionVisitor expressionVisitor;

    #endregion

    public StatementVisitor(IMetadataHost host, CommonSemanticModel semanticModel, ReferenceMapper mapper, IMethodDefinition method, ExpressionVisitor expressionVisitor)
    {
      this.host = host;
      this.semanticModel = semanticModel;
      this.mapper = mapper;
      this.method = method;
      this.tree = semanticModel.SyntaxTree as SyntaxTree;
      this.expressionVisitor = expressionVisitor;
    }

    public override IStatement VisitBlock(BlockSyntax node) {
      var statements = new List<IStatement>();
      BlockStatement bs = new BlockStatement() {
        Statements = statements,
      };
      statements.Add(
        new EmptyStatement() { Locations = Helper.SourceLocation(this.tree, node.OpenBraceToken), }
        );
      foreach (var s in node.Statements) {
        var s_prime = this.Visit(s);
        statements.Add(s_prime);
      }
      statements.Add(
        new EmptyStatement() { Locations = Helper.SourceLocation(this.tree, node.CloseBraceToken), }
        );
      return bs;
    }

    public override IStatement VisitBreakStatement(BreakStatementSyntax node) {
      return new BreakStatement();
    }

    public override IStatement VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node) {
      return this.Visit(node.Declaration);
    }

    public override IStatement VisitElseClause(ElseClauseSyntax node) {
      return this.Visit(node.Statement);
    }

    public override IStatement VisitEmptyStatement(EmptyStatementSyntax node) {
      return CodeDummy.LabeledStatement;
    }

    public override IStatement VisitExpressionStatement(ExpressionStatementSyntax node) {
      var s = new ExpressionStatement() {
        Expression = this.expressionVisitor.Visit(node.Expression),
        Locations = Helper.SourceLocation(this.tree, node),
      };
      return s;
    }

    public override IStatement VisitForStatement(ForStatementSyntax node) {

      var newInitializers = new List<IStatement>();

      if (node.Declaration != null) {
        var loopLocals = node.Declaration.Variables;
        foreach (var variableDecl in loopLocals) {
          var s = (Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol)this.semanticModel.GetDeclaredSymbol(variableDecl);
          var local = new LocalDefinition() {
            Name = this.host.NameTable.GetNameFor(s.Name),
            MethodDefinition = this.method,
            Type = this.mapper.Map(s.Type),
          };
          this.expressionVisitor.RegisterLocal(s, local);
          if (variableDecl.Initializer != null) {
            var initialValue = variableDecl.Initializer.Value;
            newInitializers.Add(
              new LocalDeclarationStatement() {
                InitialValue = this.expressionVisitor.Visit(initialValue),
                LocalVariable = local,
              }
              );
          }
        }
      }

      var forStmt = new ForStatement() {
        Body = this.Visit(node.Statement),
        InitStatements = newInitializers,
        Locations = Helper.SourceLocation(this.tree, node),
      };
      if (node.Condition != null)
        forStmt.Condition = this.expressionVisitor.Visit(node.Condition);
      var newIncrementors = new List<IStatement>();
      foreach (var incrementor in node.Incrementors){
        var incr_prime = this.expressionVisitor.Visit(incrementor);
        newIncrementors.Add(
          new ExpressionStatement() {
            Expression = incr_prime,
          });
      }
      forStmt.IncrementStatements = newIncrementors;
      return forStmt;
    }

    public override IStatement VisitForEachStatement(ForEachStatementSyntax node) {
      var s = this.semanticModel.GetDeclaredSymbol(node) as Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol;
      if (s != null) {
        var local = new LocalDefinition() {
          Name = this.host.NameTable.GetNameFor(s.Name),
          MethodDefinition = this.method,
          Type = this.mapper.Map(s.Type),
        };


        this.expressionVisitor.RegisterLocal(s, local);
        var body = this.Visit(node.Statement);
        var foreachStmt = new ForEachStatement() {
          Body = this.Visit(node.Statement),
          Collection = this.expressionVisitor.Visit(node.Expression),
          Variable = local,
          Locations = Helper.SourceLocation(this.tree, node),
        };

        if (!(foreachStmt.Collection.Type is IArrayTypeReference)) {
          var msg = String.Format("VisitForEachStatement can't handle the type '{0}'", TypeHelper.GetTypeName(foreachStmt.Collection.Type, NameFormattingOptions.None));
          throw new InvalidDataException(msg);
        }
        
        return foreachStmt;
      }
      throw new InvalidDataException("VisitForEachStatement couldn't find something to return");
    }


    private Dictionary<object, ILabeledStatement> labelTable = new Dictionary<object, ILabeledStatement>();
    public override IStatement VisitGotoStatement(GotoStatementSyntax node) {
      var e = node.Expression as IdentifierNameSyntax;
      ILabeledStatement l;
      if (!this.labelTable.TryGetValue(e.Identifier.Value, out l)) {
        l = new LabeledStatement() {
          Label = this.host.NameTable.GetNameFor(e.GetText().ToString()),
          Locations = Helper.SourceLocation(this.tree, node),
        };
        this.labelTable.Add(e.Identifier.Value, l);
      }
      return new GotoStatement() {
        TargetStatement = l,
      };
    }

    public override IStatement VisitIfStatement(IfStatementSyntax node) {
      var e = this.expressionVisitor.Visit(node.Condition);
      var thenStmt = this.Visit(node.Statement);
      var cond = new ConditionalStatement() {
        Condition = e,
        Locations = Helper.SourceLocation(this.tree, node), //.Condition),
        TrueBranch = thenStmt,
      };
      IStatement elseStmt = new EmptyStatement();
      if (node.Else != null) {
        elseStmt = this.Visit(node.Else);
      }
      cond.FalseBranch = elseStmt;
      return cond;
    }

    public override IStatement VisitLabeledStatement(LabeledStatementSyntax node) {
      ILabeledStatement l;
      var id = node.Identifier;
      if (!this.labelTable.TryGetValue(id.Value, out l)) {
        l = new LabeledStatement() {
          Label = this.host.NameTable.GetNameFor(id.ValueText),
          Locations = Helper.SourceLocation(this.tree, node),
        };
        this.labelTable.Add(id.Value, l);
      }
      var s_prime = this.Visit(node.Statement);
      return new BlockStatement() {
        Statements = new List<IStatement>(){ l, s_prime, },
      };
    }

    public override IStatement VisitReturnStatement(ReturnStatementSyntax node) {
      var r = new ReturnStatement() {
        Locations = Helper.SourceLocation(this.tree, node),
      };
      IExpression e = null;
      if (node.Expression != null) {
        e = this.expressionVisitor.Visit(node.Expression);
        r.Expression = e;
      }
      return r;
    }

    public override IStatement VisitSwitchStatement(SwitchStatementSyntax node) {
      var e = this.expressionVisitor.Visit(node.Expression);
      var cases = new List<ISwitchCase>();
      foreach (var switchCase in node.Sections) {
        var labels = switchCase.Labels;
        var i = 0;
        foreach (var l in labels) {
          var c = new SwitchCase();
          if (++i == labels.Count) { // last label gets the body
            var stmts = new List<IStatement>();
            foreach (var s in switchCase.Statements) {
              stmts.Add(this.Visit(s));
            }
            c.Body = stmts;
          }
          if (l.CaseOrDefaultKeyword.Kind == SyntaxKind.DefaultKeyword) {
            c.Expression = CodeDummy.Constant;
          } else {
            // Default case is represented by a dummy Expression, so just don't set the Expression property
            System.Diagnostics.Debug.Assert(l.Value != null);
            var switchCaseExpression = this.expressionVisitor.Visit(l.Value);
            c.Expression = (ICompileTimeConstant)switchCaseExpression;
          }
          cases.Add(c);
        }
      }
      var switchStmt = new SwitchStatement() {
        Cases = cases,
        Expression = e,
        Locations = Helper.SourceLocation(this.tree, node),
      };
      return switchStmt;
    }

    // TODO: Wait until Roslyn gets this implemented
    public override IStatement VisitSwitchSection(SwitchSectionSyntax node) {
      throw new InvalidDataException("VisitSwitchSection: should never be reached, should be handled as part of VisitSwitchStatement");
    }

    // TODO: Wait until Roslyn gets this implemented
    public override IStatement VisitTryStatement(TryStatementSyntax node) {
      var catches = new List<ICatchClause>();
      foreach (var c in node.Catches) {
        var c_prime = new CatchClause() {
          //Body = MkBlockStatement(this.Visit(c.Block, arg)),
        };
        if (c.Declaration != null) {
          var cDecl = c.Declaration;
        }
      }
      var tcf = new TryCatchFinallyStatement() {
        CatchClauses = catches,
        Locations = Helper.SourceLocation(this.tree, node),
        TryBody = Helper.MkBlockStatement(this.Visit(node.Block)),
      };
      if (node.Finally != null) {
        tcf.FinallyBody = Helper.MkBlockStatement(this.Visit(node.Finally.Block));
      }
      return tcf;
    }

    public override IStatement VisitVariableDeclaration(VariableDeclarationSyntax node) {
      ITypeReference t = null;
      if (!node.Type.IsVar) {
        var o = this.semanticModel.GetTypeInfo(node.Type);
        t = this.mapper.Map(o.Type);
      }
      var decls = new List<IStatement>();
      foreach (var v in node.Variables) {
        var s = (Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol) this.semanticModel.GetDeclaredSymbol(v);
        IExpression initialValue = null;
        if (v.Initializer != null) {
          var evc = v.Initializer.Value;
          initialValue = this.expressionVisitor.Visit(evc);
        }
        var local = new LocalDefinition() {
            Name = this.host.NameTable.GetNameFor(s.Name),
            MethodDefinition = this.method,
          };
        this.expressionVisitor.RegisterLocal(s, local);
        if (t != null) {
          local.Type = t;
        } else if (initialValue != null) {
          local.Type = initialValue.Type;
        }
        var ldc = new LocalDeclarationStatement() {
          InitialValue = initialValue != null ? initialValue : null,
          LocalVariable = local,
          Locations = Helper.SourceLocation(this.tree, node),
        };
        decls.Add(ldc);
      }
      if (decls.Count == 1) {
        return decls[0];
      } else {
        var bs = new BlockStatement() {
          Statements = decls,
        };
        return bs;
      }
    }

    public override IStatement VisitWhileStatement(WhileStatementSyntax node) {
      var e = this.expressionVisitor.Visit(node.Condition);
      var s = this.Visit(node.Statement);
      return new WhileDoStatement() {
        Body = s,
        Condition = e,
        Locations = Helper.SourceLocation(this.tree, node.Condition),
      };
    }

    public override IStatement VisitThrowStatement(ThrowStatementSyntax node)
    {
      var e = this.expressionVisitor.Visit(node.Expression);
      return new ThrowStatement()
      {
        Exception = e,
        Locations = Helper.SourceLocation(this.tree, node.Expression),
      };
    }

    public override IStatement DefaultVisit(SyntaxNode node) {
      // If you hit this, it means there was some sort of CS construct
      // that we haven't written a conversion routine for.  Simply add
      // it above and rerun.
      var typeName = node.GetType().ToString();
      var msg = String.Format("Was unable to convert a {0} node to CCI", typeName);
      throw new ConverterException(msg);
    }
  
  }

}