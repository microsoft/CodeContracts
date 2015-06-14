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
using Microsoft.CodeAnalysis.CSharp.Semantics;
using Microsoft.Cci;
using Microsoft.Cci.MutableCodeModel;
using System.Diagnostics.Contracts;
using Microsoft.CodeAnalysis.Common.Semantics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using R = Microsoft.CodeAnalysis.CSharp.Symbols;
using RC = Microsoft.CodeAnalysis.Common;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Common;

namespace CSharp2CCI {

  internal class ExpressionVisitor : SyntaxVisitor<IExpression> {

    #region Fields

    private IMetadataHost host;
    CommonSemanticModel semanticModel;
    ReferenceMapper mapper;
    private IMethodDefinition method;
    Dictionary<Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol, ILocalDefinition> locals = new Dictionary<Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol, ILocalDefinition>(); // TODO: need to make this a stack of local scopes!
    Dictionary<IMethodDefinition, List<IParameterDefinition>> parameters = new Dictionary<IMethodDefinition, List<IParameterDefinition>>(); // TODO: Clean up entries when not needed?
    private RC.CommonSyntaxTree tree;
    #endregion

    public ExpressionVisitor(IMetadataHost host, CommonSemanticModel semanticModel, ReferenceMapper mapper, IMethodDefinition method)
    {
      this.host = host;
      this.semanticModel = semanticModel;
      this.mapper = mapper;
      this.method = method;
      this.parameters.Add(method, new List<IParameterDefinition>(method.Parameters));
      this.tree = semanticModel.SyntaxTree;
    }

    internal void RegisterLocal(Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol localSymbol, ILocalDefinition localDefinition) {
      this.locals[localSymbol] = localDefinition;
    }

    /// <summary>
    /// Returns a constant one to be used in pre-/post-fix increment/decrement operations
    /// </summary>
    private string LocalNumber(){
      var s = localNumber.ToString();
      localNumber++;
      return s;
    }
    int localNumber = 0;
    /// <summary>
    /// Tracks whether the left-hand side of an assignment is being translated.
    /// Makes a difference whether a property should be translated as a setter
    /// or a getter.
    /// </summary>
    private bool lhs;
    
    object GetConstantOneOfMatchingTypeForIncrementDecrement(ITypeDefinition targetType) {
      if (TypeHelper.TypesAreEquivalent(targetType, this.host.PlatformType.SystemChar))
        return (char)1;
      else if (TypeHelper.TypesAreEquivalent(targetType, this.host.PlatformType.SystemInt8))
        return (sbyte)1;
      else if (TypeHelper.TypesAreEquivalent(targetType, this.host.PlatformType.SystemUInt8))
        return (byte)1;
      else if (TypeHelper.TypesAreEquivalent(targetType, this.host.PlatformType.SystemInt16))
        return (short)1;
      else if (TypeHelper.TypesAreEquivalent(targetType, this.host.PlatformType.SystemUInt16))
        return (ushort)1;
      return 1;
    }

    public override IExpression VisitLiteralExpression(LiteralExpressionSyntax node) {
      var o = this.semanticModel.GetTypeInfo(node);
      switch (node.Kind) {
        case SyntaxKind.NumericLiteralExpression:
        case SyntaxKind.CharacterLiteralExpression:
        case SyntaxKind.FalseLiteralExpression:
        case SyntaxKind.StringLiteralExpression:
        case SyntaxKind.TrueLiteralExpression:
          return new CompileTimeConstant() { Type = mapper.Map(o.Type), Value = node.Token.Value, };
        case SyntaxKind.NullLiteralExpression:
          return new CompileTimeConstant() { Type = this.host.PlatformType.SystemObject, Value = null, };
        default:
          throw new InvalidDataException("VisitPrimaryExpression passed an unknown node kind: " + node.Kind);
      }
    }

    public override IExpression VisitThisExpression(ThisExpressionSyntax node) {
      var o = this.semanticModel.GetTypeInfo(node);
      return new ThisReference() { Type = this.mapper.Map(o.Type), };
    }

    public override IExpression VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node) {

      var o = this.semanticModel.GetSymbolInfo(node);
      var s = o.Symbol as MethodSymbol;
      if (s != null) {
        var m = (MethodDefinition)this.mapper.TranslateMetadata(s);
        var anonDelParameters = new List<IParameterDefinition>(m.Parameters);
        this.parameters.Add(m, anonDelParameters);

        IBlockStatement body;
        var b = node.Body;
        if (b is ExpressionSyntax) {
          var e = this.Visit(b);
          body = new BlockStatement() {
            Statements = new List<IStatement>() { new ReturnStatement() { Expression = e, } },
          };
        } else if (b is BlockSyntax) {
          var sv = new StatementVisitor(this.host, this.semanticModel, this.mapper, m, this);
          var b2 = (IBlockStatement) sv.Visit(b);
          body = b2;
        } else {
          throw new InvalidDataException("VisitSimpleLambdaExpression: unknown type of body");
        }
        var anon = new AnonymousDelegate() {
          Body = body,
          CallingConvention = s.IsStatic ? CallingConvention.HasThis : CallingConvention.Default,
          Parameters = anonDelParameters,
          ReturnType = m.Type,
          Type = this.mapper.Map(this.semanticModel.GetTypeInfo(node).ConvertedType),
        };
        return anon;
      }
      throw new InvalidDataException("VisitSimpleLambdaExpression couldn't find something to return");
    }

    public override IExpression VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node) {
      var o = this.semanticModel.GetTypeInfo(node);
      var t = this.mapper.Map(o.Type);
      var operand = this.Visit(node.Operand);
      UnaryOperation op = null;
      switch (node.Kind) {
        case SyntaxKind.BitwiseNotExpression:
          op = new OnesComplement();
          break;
          case SyntaxKind.UnaryMinusExpression:
          op = new UnaryNegation();
          op.Type = operand.Type;
          break;

        case SyntaxKind.PreDecrementExpression:
        case SyntaxKind.PreIncrementExpression:
          BinaryOperation bo;
          if (node.OperatorToken.Kind == SyntaxKind.MinusMinusToken)
            bo = new Subtraction();
          else
            bo = new Addition();
          object one = GetConstantOneOfMatchingTypeForIncrementDecrement(operand.Type.ResolvedType); // REVIEW: Do we really need to resolve?
          bo.LeftOperand = operand;
          bo.RightOperand = new CompileTimeConstant() { Type = operand.Type, Value = one, };
          bo.Type = operand.Type;
          var assign = new Assignment() {
            Source = bo,
            Target = Helper.MakeTargetExpression(operand),
            Type = operand.Type,
          };
          return assign;

        case SyntaxKind.LogicalNotExpression:
          op = new LogicalNot();
          break;

        default:
          var typeName = node.GetType().ToString();
          var msg = String.Format("Was unable to convert a {0} node to CCI because the kind '{1}' wasn't handled",
            typeName, node.Kind.ToString());
          throw new ConverterException(msg);
      }
      op.Operand = operand;
      return op;
    }

    public override IExpression VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node) {
      var e = this.Visit(node.Operand);
      switch (node.OperatorToken.Kind) {
        case SyntaxKind.MinusMinusToken:
        case SyntaxKind.PlusPlusToken:
          var stmts = new List<IStatement>();
          var temp = new LocalDefinition() {
            MethodDefinition = this.method,
            Name = this.host.NameTable.GetNameFor("__temp" + LocalNumber()),
            Type = e.Type,
          };
          stmts.Add(
            new LocalDeclarationStatement(){
               InitialValue = e,
               LocalVariable = temp,
            });

          BinaryOperation bo;
          if (node.OperatorToken.Kind == SyntaxKind.MinusMinusToken)
            bo = new Subtraction();
          else
            bo = new Addition();
          object one = GetConstantOneOfMatchingTypeForIncrementDecrement(e.Type.ResolvedType); // REVIEW: Do we really need to resolve?
          bo.LeftOperand = e;
          bo.RightOperand = new CompileTimeConstant() { Type = e.Type, Value = one, };
          bo.Type = e.Type;
          var assign = new Assignment() {
            Source = bo,
            Target = Helper.MakeTargetExpression(e),
            Type = e.Type,
          };
          stmts.Add(
            new ExpressionStatement() {
              Expression = assign,
            });
          var blockExpression = new BlockExpression() {
            BlockStatement = new BlockStatement() {
              Statements = stmts,
            },
            Expression = new BoundExpression() { Definition = temp, Instance = null, Type = temp.Type },
            Type = e.Type,
          };

          return blockExpression;

        default:
          throw new InvalidDataException("VisitPostfixUnaryExpression: unknown operator token '" + node.OperatorToken.ValueText);
      }
    }

    public override IExpression VisitParenthesizedExpression(ParenthesizedExpressionSyntax node) {
      return this.Visit(node.Expression);
    }

    public override IExpression VisitObjectCreationExpression(ObjectCreationExpressionSyntax node) {
      var o = this.semanticModel.GetSymbolInfo(node);
      var s = o.Symbol;
      if (s != null) {
        MethodSymbol ms = s as MethodSymbol;
        var mr = this.mapper.Map(ms);
        var e = new CreateObjectInstance() {
          Locations = Helper.SourceLocation(this.tree, node),
          MethodToCall = mr,
          Type = mr.ContainingType,
        };
        return e;
      }
      throw new InvalidDataException("VisitObjectCreationExpression couldn't find something to return");
    }

    public override IExpression VisitMemberAccessExpression(MemberAccessExpressionSyntax node) {
      var o = this.semanticModel.GetSymbolInfo(node);
      IExpression instance = null;
      BoundExpression be = null;
      var onLHS = this.lhs;
      this.lhs = false; // only the right-most member is a l-value: all other member accesses are for r-value.
      var s = o.Symbol;
      if (s != null) {
        switch (s.Kind) {
          case CommonSymbolKind.Method:
            R.MethodSymbol ms = (R.MethodSymbol)s;
            instance = null;
            if (!s.IsStatic) {
              instance = this.Visit(node.Expression);
            }
            var mr = this.mapper.Map(ms);
            be = new BoundExpression() {
              Definition = mr,
              Instance = instance,
              Locations = Helper.SourceLocation(this.tree, node),
              Type = mr.Type,
            };
            return be;

          case CommonSymbolKind.Field:
            FieldSymbol fs = (FieldSymbol)s;
            instance = null;
            if (!fs.IsStatic) {
              instance = this.Visit(node.Expression);
            }
            var fr = this.mapper.Map(fs);
            // Certain fields represent compile-time constants
            // REVIEW: Is this the right place to do this?
            // TODO: All the rest of the constants...
            if (fr.ContainingType.InternedKey == this.host.PlatformType.SystemInt32.InternedKey) {
              if (fr.Name.Value.Equals("MinValue")) {
                return new CompileTimeConstant() { Type = fr.Type, Value = Int32.MinValue, };
              } else if (fr.Name.Value.Equals("MaxValue")) {
                return new CompileTimeConstant() { Type = fr.Type, Value = Int32.MaxValue, };
              }
            }

            be = new BoundExpression() {
              Definition = fr,
              Instance = instance,
              Locations = Helper.SourceLocation(this.tree, node),
              Type = fr.Type,
            };
            return be;

          case CommonSymbolKind.Property:
            R.PropertySymbol ps = (R.PropertySymbol)s;
            instance = null;
            if (!ps.IsStatic) {
              instance = this.Visit(node.Expression);
            }
            var accessor = onLHS ? this.mapper.Map(ps.SetMethod) : this.mapper.Map(ps.GetMethod);
            if (!onLHS && MemberHelper.GetMemberSignature(accessor, NameFormattingOptions.None).Contains("Length")) {
              return new VectorLength() {
                Locations = Helper.SourceLocation(this.tree, node),
                Type = accessor.Type,
                Vector = instance,
              };
            }
            return new MethodCall() {
              MethodToCall = accessor,
              IsStaticCall = ps.IsStatic,
              Locations = Helper.SourceLocation(this.tree, node),
              ThisArgument = instance,
              Type = accessor.Type,
            };

          default:
            throw new InvalidDataException("VisitMemberAccessExpression: uknown definition kind: " + s.Kind);
        }
      }
      throw new InvalidDataException("VisitMemberAccessExpression couldn't find something to return");
    }

    [ContractVerification(false)]
    public override IExpression VisitInvocationExpression(InvocationExpressionSyntax node) {
      var o = this.semanticModel.GetSymbolInfo(node);
      var args = new List<IExpression>();
      foreach (var a in node.ArgumentList.Arguments) {
        var a_prime = this.Visit(a);
        args.Add(a_prime);
      }
      var s = o.Symbol;
      if (s != null) {
        MethodSymbol ms = s as MethodSymbol;
        IMethodReference mtc = this.mapper.Map(ms);
        IExpression thisArg;
        thisArg = null;
        if (!ms.IsStatic) {
          var be = (BoundExpression) this.Visit(node.Expression);
          thisArg = be.Instance;
        }
        //if (MemberHelper.GetMethodSignature(mtc, NameFormattingOptions.None).Contains("Length")) {
        //  return new VectorLength() {
        //    Type = mtc.Type,
        //    Vector = thisArg,
        //  };
        //}

        var mc = new MethodCall() {
          Arguments = args,
          IsStaticCall = ms.IsStatic,
          Locations = Helper.SourceLocation(this.tree, node),
          MethodToCall = mtc,
          ThisArgument = thisArg,
          Type = mtc.Type,
        };

        return mc;
      }
      throw new InvalidDataException("VisitInvocationExpression couldn't find something to return");
    }

    public override IExpression VisitIdentifierName(IdentifierNameSyntax node) {
      var o = this.semanticModel.GetSymbolInfo(node);

      var s = o.Symbol;
      if (s != null) {
        switch (s.Kind) {

          case CommonSymbolKind.Field:
            var f = this.mapper.Map(s as FieldSymbol);
            var t = f.ContainingType;
            return new BoundExpression() {
              Definition = f,
              Instance = s.IsStatic ? null : new ThisReference() { Type = t, },
              Type = f.Type,
            };

          case CommonSymbolKind.Parameter:
            var p = s as ParameterSymbol;
            var m = (IMethodDefinition) this.mapper.Map(p.ContainingSymbol as MethodSymbol);
            var p_prime = this.parameters[m][p.Ordinal];
            if (p_prime.IsIn) {
              return new BoundExpression() {
                Definition = p_prime,
                Instance = null,
                Locations = Helper.SourceLocation(this.tree, node),
                Type = p_prime.Type,
              };
            } else if (p_prime.IsOut || p_prime.IsByReference) {
              var locs = Helper.SourceLocation(this.tree, node);
              return new AddressDereference() {
                Address = new BoundExpression() {
                  Definition = p_prime,
                  Instance = null,
                  Locations = locs,
                  Type = Microsoft.Cci.Immutable.ManagedPointerType.GetManagedPointerType(p_prime.Type, this.host.InternFactory),
                },
                Locations = locs,
                Type = p_prime.Type,
              };
            } else {
              throw new InvalidDataException("VisitIdentifierName given a parameter that is neither in, out, nor ref: " + p.Name);
            }

          case CommonSymbolKind.Local:
            var l = s as LocalSymbol;
            var l_prime = this.locals[l];
            return new BoundExpression() {
              Definition = l_prime,
              Instance = null,
              Locations = Helper.SourceLocation(this.tree, node),
              Type = l_prime.Type,
            };

          case CommonSymbolKind.Method:
            var m2 = this.mapper.Map(s as MethodSymbol);
            var t2 = m2.ContainingType;
            return new BoundExpression() {
              Definition = m2,
              Instance = s.IsStatic ? null : new ThisReference() { Type = t2, },
              Type = t2,
            };

          default:
            throw new InvalidDataException("VisitIdentifierName passed an unknown symbol kind: " + s.Kind);
        }
      }
      return CodeDummy.Expression;
    }

    public override IExpression VisitElementAccessExpression(ElementAccessExpressionSyntax node) {
      var indices = new List<IExpression>();
      foreach (var i in node.ArgumentList.Arguments) {
        var i_prime = this.Visit(i);
        indices.Add(i_prime);
      }
      var indexedObject = this.Visit(node.Expression);
      // Can index an array
      var arrType = indexedObject.Type as IArrayTypeReference;
      if (arrType != null) {
        return new ArrayIndexer() {
          IndexedObject = indexedObject,
          Indices = indices,
          Locations = Helper.SourceLocation(this.tree, node),
          Type = arrType.ElementType,
        };
      }
      // Otherwise the indexer represents a call to the setter or getter for the default indexed property
      // This shows that the target of the translator should really be the Ast model and not the CodeModel
      // because it already handles this sort of thing.
      if (TypeHelper.TypesAreEquivalent(indexedObject.Type, this.host.PlatformType.SystemString)) {
        return new MethodCall(){
          Arguments = indices,
          IsVirtualCall = true,
          Locations = Helper.SourceLocation(this.tree, node),
          MethodToCall = TypeHelper.GetMethod(this.host.PlatformType.SystemString.ResolvedType, this.host.NameTable.GetNameFor("get_Chars"), this.host.PlatformType.SystemInt32),
          ThisArgument = indexedObject,
          Type = this.host.PlatformType.SystemChar,
        };
      }
      throw new InvalidDataException("VisitElementAccessExpression couldn't find something to return");
    }

    public override IExpression VisitConditionalExpression(ConditionalExpressionSyntax node) {
      var o = this.semanticModel.GetTypeInfo(node);
      var t = this.mapper.Map(o.Type);
      return new Conditional() {
        Condition = this.Visit(node.Condition),
        Locations = Helper.SourceLocation(this.tree, node),
        ResultIfFalse = this.Visit(node.WhenFalse),
        ResultIfTrue = this.Visit(node.WhenTrue),
        Type = t,
      };
    }

    public override IExpression VisitCastExpression(CastExpressionSyntax node) {
      var o = this.semanticModel.GetTypeInfo(node);
      var t = this.mapper.Map(o.Type);
      var result = new Microsoft.Cci.MutableCodeModel.Conversion() {
        ValueToConvert = this.Visit(node.Expression),
        Type = t,
        TypeAfterConversion = t,
      };
      return result;
    }

    public override IExpression VisitBinaryExpression(BinaryExpressionSyntax node) {
      var o = this.semanticModel.GetTypeInfo(node);
      var t = this.mapper.Map(o.Type);
      if (node.Kind == SyntaxKind.AssignExpression)
        this.lhs = true;
      var left = this.Visit(node.Left);
      this.lhs = false;
      var right = this.Visit(node.Right);
      BinaryOperation op = null;
      var locs = Helper.SourceLocation(this.tree, node);
      switch (node.Kind) {

        case SyntaxKind.AddAssignExpression: {
            var a = new Assignment() {
              Locations = locs,
              Source = new Addition() { 
                 LeftOperand = left,
                 RightOperand =right,
              },
              Target = Helper.MakeTargetExpression(left),
              Type = t,
            };
            return a;
          }

        case SyntaxKind.AddExpression:
          op = new Addition();
          break;

        case SyntaxKind.AssignExpression: {
            var mc = left as MethodCall;
            if (mc != null) {
              // then this is really o.P = e for some property P
              // and the property access has been translated into a call
              // to set_P.
              mc.Arguments = new List<IExpression> { right, };
              return mc;
            }
            var be = left as BoundExpression;
            if (be != null) {
              var a = new Assignment() {
                Locations = locs,
                Source = right,
                Target = new TargetExpression() {
                  Definition = be.Definition,
                  Instance = be.Instance,
                  Type = be.Type,
                },
                Type = t,
              };
              return a;
            }
            var arrayIndexer = left as ArrayIndexer;
            if (arrayIndexer != null) {
              var a = new Assignment() {
                Locations = locs,
                Source = right,
                Target = new TargetExpression() {
                  Definition = arrayIndexer,
                  Instance = arrayIndexer.IndexedObject,
                  Type = right.Type,
                },
                Type = t,
              };
              return a;
            }
            var addressDereference = left as AddressDereference;
            if (addressDereference != null) {
              var a = new Assignment() {
                Locations = locs,
                Source = right,
                Target = new TargetExpression() {
                  Definition = addressDereference,
                  Instance = null,
                  Type = t,
                },
                Type = t,
              };
              return a;
            }
            throw new InvalidDataException("VisitBinaryExpression: Can't figure out lhs in assignment" + left.Type.ToString());
          }

        case SyntaxKind.BitwiseAndExpression: op = new BitwiseAnd(); break;
        case SyntaxKind.BitwiseOrExpression: op = new BitwiseOr(); break;
        case SyntaxKind.DivideExpression: op = new Division(); break;
        case SyntaxKind.EqualsExpression: op = new Equality(); break;
        case SyntaxKind.ExclusiveOrExpression: op = new ExclusiveOr(); break;
        case SyntaxKind.GreaterThanExpression: op = new GreaterThan(); break;
        case SyntaxKind.GreaterThanOrEqualExpression: op = new GreaterThanOrEqual(); break;
        case SyntaxKind.LeftShiftExpression: op = new LeftShift(); break;
        case SyntaxKind.LessThanExpression: op = new LessThan(); break;
        case SyntaxKind.LessThanOrEqualExpression: op = new LessThanOrEqual(); break;

        case SyntaxKind.LogicalAndExpression:
          return new Conditional() {
            Condition = left,
            Locations = locs,
            ResultIfTrue = right,
            ResultIfFalse = new CompileTimeConstant() { Type = t, Value = false },
            Type = t,
          };

        case SyntaxKind.LogicalOrExpression:
          return new Conditional() {
            Condition = left,
            Locations = Helper.SourceLocation(this.tree, node),
            ResultIfTrue = new CompileTimeConstant() { Type = t, Value = true },
            ResultIfFalse = right,
            Type = t,
          };

        case SyntaxKind.ModuloExpression: op = new Modulus(); break;
        case SyntaxKind.MultiplyExpression: op = new Multiplication(); break;
        case SyntaxKind.NotEqualsExpression: op = new NotEquality(); break;
        case SyntaxKind.RightShiftExpression: op = new RightShift(); break;

        case SyntaxKind.SubtractAssignExpression: {
            var a = new Assignment() {
              Locations = locs,
              Source = new Subtraction() { 
                 LeftOperand = left,
                 RightOperand = right,
              },
              Target = Helper.MakeTargetExpression(left),
              Type = t,
            };
            return a;
          }

        case SyntaxKind.MultiplyAssignExpression:
          {
            var a = new Assignment()
            {
              Locations = locs,
              Source = new Multiplication()
              {
                LeftOperand = left,
                RightOperand = right,
              },
              Target = Helper.MakeTargetExpression(left),
              Type = t,
            };
            return a;
          }

        case SyntaxKind.DivideAssignExpression:
          {
            var a = new Assignment()
            {
              Locations = locs,
              Source = new Division()
              {
                LeftOperand = left,
                RightOperand = right,
              },
              Target = Helper.MakeTargetExpression(left),
              Type = t,
            };
            return a;
          }

        case SyntaxKind.ModuloAssignExpression:
          {
            var a = new Assignment()
            {
              Locations = locs,
              Source = new Modulus()
              {
                LeftOperand = left,
                RightOperand = right,
              },
              Target = Helper.MakeTargetExpression(left),
              Type = t,
            };
            return a;
          }

        case SyntaxKind.SubtractExpression: op = new Subtraction(); break;

        default:
          throw new InvalidDataException("VisitBinaryExpression: unknown node = " + node.Kind);
      }
      op.Locations = locs;
      op.LeftOperand = left;
      op.RightOperand = right;
      op.Type = t;
      return op;
    }

    // TODO: Handle multi-dimensional arrays
    public override IExpression VisitArrayCreationExpression(ArrayCreationExpressionSyntax node) {
      var o = this.semanticModel.GetSymbolInfo(node);
      var s = o.Symbol;
      var arrayType = node.Type;
      var elementType = this.mapper.Map(this.semanticModel.GetTypeInfo(arrayType.ElementType).Type);
      var arrayOfType = Microsoft.Cci.Immutable.Vector.GetVector(elementType, this.host.InternFactory);
      List<IExpression> inits = null;
      int size = 0;
      if (node.Initializer != null) {
        inits = new List<IExpression>();
        foreach (var i in node.Initializer.Expressions) {
          var e = this.Visit(i);
          inits.Add(e);
          size++;
        }
      }
      var result = new CreateArray() {
        ElementType = elementType,
        Rank = 1,
        Type = arrayOfType,
      };
      if (inits != null) {
        result.Initializers = inits;
        result.Sizes = new List<IExpression> { new CompileTimeConstant() { Value = size, }, };
      } else {
        var rankSpecs = arrayType.RankSpecifiers;
        foreach (var r in rankSpecs) {
          foreach (var rs in r.Sizes) {
            var e = this.Visit(rs);
            result.Sizes = new List<IExpression> { e, };
            break;
          }
          break;
        }
      }
      return result;
    }

    public override IExpression VisitArgument(ArgumentSyntax node) {
      var a = this.Visit(node.Expression);
      if (node.RefOrOutKeyword.Kind != SyntaxKind.None) {
        object def = a;
        IExpression instance = null;
        var be = a as IBoundExpression;
        if (be != null) { // REVIEW: Maybe it should always be a bound expression?
          def = be.Definition;
          instance = be.Instance;
        }
        a = new AddressOf() {
          Expression = new AddressableExpression() {
            Definition = def,
            Instance = instance,
            Locations = new List<ILocation>(a.Locations),
          },
          Locations = new List<ILocation>(a.Locations),
          Type = Microsoft.Cci.Immutable.ManagedPointerType.GetManagedPointerType(a.Type, this.host.InternFactory),
        };
      }
      return a;
    }

    public override IExpression DefaultVisit(SyntaxNode node) {
      // If you hit this, it means there was some sort of CS construct
      // that we haven't written a conversion routine for.  Simply add
      // it above and rerun.
      var typeName = node.GetType().ToString();
      var msg = String.Format("Was unable to convert a {0} node to CCI", typeName);
      throw new ConverterException(msg);
    }

  }

}