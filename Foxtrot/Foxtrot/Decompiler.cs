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
using System.Text;
using System.Compiler;
using System.Diagnostics; // needed for Debug.Assert (etc.)

namespace Microsoft.Contracts.Foxtrot {
  class TypeReconstructorAndExpressionSimplifier : StandardVisitor {
    private NodeType InvertComparisonOperator(NodeType Operator) {
      switch (Operator) {
        case NodeType.Eq: return NodeType.Ne;
        case NodeType.Ge: return NodeType.Lt;
        case NodeType.Gt: return NodeType.Le;
        case NodeType.Le: return NodeType.Gt;
        case NodeType.Lt: return NodeType.Ge;
        case NodeType.Ne: return NodeType.Eq;
      }
      return Operator;
    }
    internal static Expression ReconstructBooleanExpression(Expression e) {
      if (e.Type == SystemTypes.Boolean) {
        return e;
      }
      if (e.Type.IsPrimitiveInteger) {
        return new BinaryExpression(e, Literal.Int32Zero, NodeType.Ne, SystemTypes.Boolean);
      } else if (e.Type.IsReferenceType) {
        return new BinaryExpression(e, Literal.Null, NodeType.Ne, SystemTypes.Boolean);
      }
      return e;
    }
    public override Expression VisitExpression(Expression expression) {
      if (expression == null) return null;
      Expression e = base.VisitExpression(expression);
      Debug.Assert(e.Type != null || e.NodeType == NodeType.Pop);
      return e;
    }
    public override Expression VisitUnaryExpression(UnaryExpression unaryExpression) {
      unaryExpression.Operand = VisitExpression(unaryExpression.Operand);
      if (unaryExpression.NodeType == NodeType.LogicalNot && unaryExpression.Operand.Type.IsPrimitiveInteger) {
        return new BinaryExpression(unaryExpression.Operand, Literal.Int32Zero, NodeType.Eq, SystemTypes.Boolean);
      }
      if (unaryExpression.Type == null) {
        switch (unaryExpression.NodeType) {
          case NodeType.LogicalNot:
            unaryExpression.Operand = TypeReconstructorAndExpressionSimplifier.ReconstructBooleanExpression(unaryExpression.Operand);
            Debug.Assert(unaryExpression.Operand.Type == SystemTypes.Boolean);
            unaryExpression.Type = SystemTypes.Boolean;
            break;
          default:
            break;
        }
      }
      return unaryExpression;
    }
    public override Expression VisitBinaryExpression(BinaryExpression binaryExpression) {
      Expression binExp = base.VisitBinaryExpression(binaryExpression);
      BinaryExpression e = binExp as BinaryExpression;
      if (e == null) return binExp;
      switch (e.NodeType) {
        case NodeType.Ceq:
          e.NodeType = NodeType.Eq;
          if (e.Operand1.Type == SystemTypes.Boolean && e.Operand2.Type.IsPrimitiveInteger) {
            Expression l = Decompiler.SimplifyLiteral(e.Operand2);
            if (l == Literal.False) {
              e.Operand1.NodeType = this.InvertComparisonOperator(e.Operand1.NodeType);
              return e.Operand1;
            } else if (l == Literal.True) {
              return e.Operand1;
            }
          } else if (e.Operand2.Type == SystemTypes.Boolean && e.Operand1.Type.IsPrimitiveInteger) {
            Expression l = Decompiler.SimplifyLiteral(e.Operand1);
            if (l == Literal.False) {
              e.Operand2.NodeType = this.InvertComparisonOperator(e.Operand2.NodeType);
              return e.Operand2;
            } else if (l == Literal.True) {
              return e.Operand2;
            }
          }
          break;
        case NodeType.Clt:
          e.NodeType = NodeType.Lt;
          break;
        case NodeType.Cgt:
          e.NodeType = NodeType.Gt;
          break;
        default:
          break;
      }
      // When the NodeType was something like "ceq", then the type is int, but it should be a boolean
      switch (e.NodeType) {
        case NodeType.Eq:
        case NodeType.Ne:
        case NodeType.Lt:
        case NodeType.Le:
        case NodeType.Gt:
        case NodeType.Ge:
          e.Type = SystemTypes.Boolean;
          break;
      }
      return e;
    }
    public override Expression VisitMemberBinding(MemberBinding memberBinding) {
      Expression e = base.VisitMemberBinding(memberBinding);
      if (memberBinding.TargetObject == null && memberBinding.BoundMember is InstanceInitializer) {
        // This is seen if there is a call to new T(...) within an expression
        if (e.Type == null) {
          InstanceInitializer ctor = memberBinding.BoundMember as InstanceInitializer;
          e.Type = ctor.DeclaringType;
        }
      }
      return e;
    }
  }
  public sealed class Decompiler : StandardVisitor{
    private ContractNodes contractNodes;
    private Local localToUseForResult;
    public Decompiler(ContractNodes cns) {
      this.contractNodes = cns;
    }
    public Decompiler(ContractNodes cns, Local l) {
      this.contractNodes = cns;
      this.localToUseForResult = l;
    }
    public Decompiler(AssemblyNode assemblyNode) {
      this.contractNodes = ContractNodes.GetContractNodes(assemblyNode, null);
    }

    private TrivialHashtable block2Index;
    private BlockList blocks;
    private Method currentMethod;
    public Expression DecompileBooleanExpression(BlockExpression be) {
      if (be == null) return null;
      TypeReconstructorAndExpressionSimplifier tr = new TypeReconstructorAndExpressionSimplifier();
      be = tr.VisitBlockExpression(be) as BlockExpression;
      if (be == null) return null;
      Block b = be.Block;
      #region Make sure the block expression has the right shape
      foreach (Statement s in b.Statements) {
        Block b_prime = s as Block;
        Debug.Assert(b_prime != null);
        Debug.Assert(HelperMethods.IsBasicBlock(b_prime));
      }
      #endregion
      Block firstBlock = (Block)b.Statements[0];
      block2Index = new TrivialHashtable(b.Statements.Count);
      blocks = new BlockList(b.Statements.Count);
      for (int i = 0, n = b.Statements.Count; i < n; i++) {
        block2Index[b.Statements[i].UniqueKey] = i;
        blocks.Add(b.Statements[i] as Block);
      }
      Expression e = DecompileBooleanExpression(firstBlock);
      // BUGBUG: this isn't a good place to do this because then it gets done for all contracts,
      // not just postconditions!! Plus, we could just leave Result in the contracts and let the
      // BPL translation recognize it.
      //ReplaceResult repResult = new ReplaceResult(this.localToUseForResult, this.contractNodes.ResultTemplate,
      //  this.contractNodes.ParameterTemplate);
      //e = repResult.VisitExpression(e);
      return e;
    }
    private Expression HackyMungeForTernaryExpressions(TernaryExpression te) {
      if (te == null) return te;
      Expression thenBranch = te.Operand2;
      Expression elseBranch = te.Operand3;
      if (thenBranch is TernaryExpression)
        thenBranch = HackyMungeForTernaryExpressions(thenBranch as TernaryExpression);
      if (elseBranch is TernaryExpression)
        elseBranch = HackyMungeForTernaryExpressions(elseBranch as TernaryExpression);
      if (thenBranch == Literal.Null)
        thenBranch = Literal.True;
      else if (thenBranch is Construct && thenBranch.Type == SystemTypes.Exception)
        thenBranch = Literal.False;
      if (elseBranch == Literal.Null)
        elseBranch = Literal.True;
      else if (elseBranch is Construct && elseBranch.Type == SystemTypes.Exception)
        elseBranch = Literal.False;
      te.Operand2 = thenBranch;
      te.Operand3 = elseBranch;
      return te;
    }
    internal static Expression SimplifyLiteral(Expression e) {
      Literal l = e as Literal;
      if (l == null) {
        return e;
      }
      if (l.Value is int) {
        int x = (int)l.Value;
        return x == 1 ? Literal.True : Literal.False;
      } else if (l.Value == Literal.Int32One) {
        return Literal.True;
      } else if (l.Value == Literal.Int32Zero) {
        return Literal.False;
      } else {
        return e;
      }
    }
    private static Expression Conjunction(Expression x, Expression y) {
      Expression x_prime = SimplifyLiteral(x);
      Expression y_prime = SimplifyLiteral(y);
      if (x_prime == Literal.True) {
        return y_prime;
      } else if (x_prime == Literal.False) {
        return Literal.False;
      } else {
        return new BinaryExpression(x_prime, y_prime, NodeType.And, SystemTypes.Boolean);
      }
    }
    private static Expression Disjunction(Expression x, Expression y) {
      Expression x_prime = SimplifyLiteral(x);
      Expression y_prime = SimplifyLiteral(y);
      if (x_prime == Literal.True) {
        return Literal.True;
      } else if (x_prime == Literal.False) {
        return y_prime;
      } else {
        return new BinaryExpression(x_prime, y_prime, NodeType.Or, SystemTypes.Boolean);
      }
    }
    /// <summary>
    /// Forms the ternary expression "P ? Q : R".
    /// It performs some simplifications:
    /// P ? true : R ==> P || R
    /// P ? Q : true ==> !P || Q
    /// P ? false : R ==> !P && R
    /// P ? Q : false ==> P && Q
    /// </summary>
    /// <param name="P"></param>
    /// <param name="Q"></param>
    /// <param name="R"></param>
    /// <returns></returns>
    private static Expression Ternary(Expression P, Expression Q, Expression R) {
      Expression Q_prime = SimplifyLiteral(Q);
      Expression R_prime = SimplifyLiteral(R);
      Expression P_prime = P;
      if (P.Type != null && P.Type != SystemTypes.Boolean) {
        P_prime = TypeReconstructorAndExpressionSimplifier.ReconstructBooleanExpression(P);
      }
      if (Q_prime == Literal.True) {
        return new BinaryExpression(P_prime, R, NodeType.LogicalOr, SystemTypes.Boolean);
      } else if (R_prime == Literal.True) {
        return new BinaryExpression(new UnaryExpression(P_prime, NodeType.LogicalNot, SystemTypes.Boolean), Q, NodeType.LogicalOr, SystemTypes.Boolean);
      } else if (Q_prime == Literal.False) {
        return new BinaryExpression(new UnaryExpression(P_prime, NodeType.LogicalNot, SystemTypes.Boolean), R, NodeType.LogicalAnd, SystemTypes.Boolean);
      } else if (R_prime == Literal.False) {
        return new BinaryExpression(P_prime, Q, NodeType.LogicalAnd, SystemTypes.Boolean);
      } else {
        return new TernaryExpression(P_prime, Q, R, NodeType.Conditional, Q.Type);
      }
    }
    private Expression DecompileBooleanExpression(Block b) {
      ExpressionList eList = new ExpressionList();
      Expression e = null;
      int i = 0;
      while (i < b.Statements.Count) {
        if (b.Statements[i] is Branch) break;
        Statement s = b.Statements[i];
        i++;
        if (s == null || s.NodeType == NodeType.Nop) continue;
        ExpressionStatement es = s as ExpressionStatement;
        if (es != null) {
          MethodCall mc = es.Expression as MethodCall;
          if (mc != null) {
            MemberBinding mb = mc.Callee as MemberBinding;
            if (mb != null) {
              Method m = mb.BoundMember as Method;
              if (m != null) {
                if (this.contractNodes.IsPlainPrecondition(m) || this.contractNodes.IsPostcondition(m)) {
                  eList.Add(mc.Operands[0]);
                } else {
                  eList.Add(es.Expression);
                }
              }
            } else {
              eList.Add(es.Expression);
            }
          } else {
            eList.Add(SimplifyLiteral(es.Expression));
          }
        }
      }
      if (i == b.Statements.Count) { // no branch at end of block
        return eList[0];
      }
      Branch br = b.Statements[b.Statements.Count - 1] as Branch;
      if (br.Condition == null) { // unconditional branch
        return eList[0];
      }
      e = TypeReconstructorAndExpressionSimplifier.ReconstructBooleanExpression(br.Condition); // should be: e = Combine(e, br.Condition);
      Expression trueBranch = DecompileBooleanExpression(br.Target);
      Expression falseBranch = DecompileBooleanExpression(blocks[((int)block2Index[b.UniqueKey]) + 1]);
      Expression result = Ternary(e, trueBranch, falseBranch);
      return result;
    }
    public override Method VisitMethod(Method method) {
      this.currentMethod = method;
      this.localToUseForResult =
           method.ReturnType == SystemTypes.Void ?
             null :
             new Local(Identifier.For("return value"), method.ReturnType);
      Method m = base.VisitMethod(method);
      this.currentMethod = null;
      return m;
    }
    public override RequiresPlain VisitRequiresPlain(RequiresPlain plain) {
      BlockExpression be = plain.Condition as BlockExpression;
      if (be != null && be.Type == SystemTypes.Void){ // then it was put here by the extractor?
        Expression e = DecompileBooleanExpression(be);
        Debug.Assert(e != null);
        return new RequiresPlain(e);
      } else {
        return plain;
      }
    }
    public override RequiresOtherwise VisitRequiresOtherwise(RequiresOtherwise otherwise) {
      BlockExpression be = otherwise.Condition as BlockExpression;
      if (be != null && be.Type == SystemTypes.Void) { // then it was put here by the extractor?
        Expression e = DecompileBooleanExpression(be);
        Debug.Assert(e != null);
        TernaryExpression te = e as TernaryExpression;
        Debug.Assert(te != null);
        e = this.HackyMungeForTernaryExpressions(te);
        e.Type = SystemTypes.Boolean;
        te = e as TernaryExpression;
        if (te != null && Literal.IsNullLiteral(te.Operand2)) {
          return new RequiresOtherwise(te.Operand1, new Construct(new MemberBinding(null, SystemTypes.Exception.GetConstructor()), null));
        } else {
          return otherwise;
        }
      } else {
        return otherwise;
      }
    }
    public override EnsuresNormal VisitEnsuresNormal(EnsuresNormal normal) {
      BlockExpression be = normal.PostCondition as BlockExpression;
      if (be != null && be.Type == SystemTypes.Void) { // then it was put here by the extractor?
        Expression e = DecompileBooleanExpression(be);
        Debug.Assert(e != null);
        return new EnsuresNormal(e);
      } else {
        return normal;
      }
    }
  }

  /// <summary>
  /// Decompiles and collapses ternary and short-circuit binary expressions into their pre-normalized form.
  /// </summary>
  public class Abnormalizer : StandardVisitor {

    /// <summary>
    /// Abnormalizes all of the normalized expressions in a method body.
    /// </summary>
    /// <param name="method">Method to abnormalize.</param>
    /// <returns>The same method with the expressions abnormalized in place.</returns>
    public override Method VisitMethod(Method method) {
      Trace.Write("Abnormalizing method " + method.ToString());

      // First, create a graph of the blocks in this method.
      BlockGraph graph = new BlockGraph(method);

      // Keep iterating over the method while simplifications can be made.
      bool simplified;
      do {
        simplified = false;

        foreach (BlockNode node in graph) {
          simplified |= AbnormalizeExpression(node);
          simplified |= CollapseDoubleBranches(node);
          simplified |= InlineExpressionStatement(node);
        }

        if (simplified) {
          method.IsNormalized = false; ;
          method.DeclaringType.IsNormalized = false;
          method.DeclaringType.DeclaringModule.IsNormalized = false;

          Trace.Write(".");
        }
      }
      while (simplified);

      Trace.WriteLine("");

      return base.VisitMethod(method);
    }

    /// <summary>
    /// Looks for normalized ternary expression patterns and creates TernaryExpressions.
    /// </summary>
    /// <param name="node">Node that possibly begins a normalized ternary expression.</param>
    /// <returns>True if a ternary expression was abnormalized; false otherwise.</returns>
    private static bool AbnormalizeExpression(BlockNode node) {
      // f(a ? b : c) is:             f(a && b) is:                f(a || b) is:
      // [     ...     ]              [      ...     ]             [     ...     ]
      // [if (a) branch]              [if (!a) branch]             [if (a) branch]
      //     / \                          / \                          / \
      //    /a  \fallthrough             /!a \fallthrough             /a  \fallthrough
      //   /     \                      /     \                      /     \
      // [b]     [c]                  [0]     [b]                  [1]     [b]
      //   \     /                      \     /                      \     /
      //    \   /                        \   /                        \   /
      //     \ /                          \ /                          \ /
      //  [f(pop)]                     [f(pop)]                     [f(pop)]
      //  [ .... ]                     [ .... ]                     [ .... ]

      BlockGraph graph = node.Graph;

      // First check the structure of head.
      if (node.Outgoing.Count == 2 &&
          node.Outgoing[0].Condition != null &&
          node.Outgoing[1].Condition == null &&
          node.Outgoing[0].Target != null &&
          node.Outgoing[1].Target != null) {

        // Then check the structure of the tails.
        BlockNode node0 = graph[node.Outgoing[0].Target];
        BlockNode node1 = graph[node.Outgoing[1].Target];
        if (node0.Outgoing.Count == 1 &&
            node1.Outgoing.Count == 1 &&
            node0.Outgoing[0].Target == node1.Outgoing[0].Target &&
            node0.Block.Statements != null &&
            node1.Block.Statements != null &&
            node0.Block.Statements.Count > 0 &&
            node1.Block.Statements.Count > 0 &&
            (node0.Block.Statements.Count < 2 ||
             node0.Block.Statements[1] is Branch) &&
            (node1.Block.Statements.Count < 2 ||
             node1.Block.Statements[1] is Branch)) {

          // The structure matches, so analyze the conditions.
          Expression condition = node.Outgoing[0].Condition;
          ExpressionStatement trueStatement = node0.Block.Statements.Count > 0 ? node0.Block.Statements[0] as ExpressionStatement : null;
          ExpressionStatement falseStatement = node1.Block.Statements.Count > 0 ? node1.Block.Statements[0] as ExpressionStatement : null;
          Expression trueExpression = trueStatement != null ? trueStatement.Expression : null;
          Expression falseExpression = falseStatement != null ? falseStatement.Expression : null;
          if (trueExpression != null &&
              trueExpression.Type != null &&
              falseExpression != null &&
              falseExpression.Type != null) {

            // Fix up the type information on boolean operators.
            NodeType[] booleanNodeTypes = {
              NodeType.Ceq,
              NodeType.Cgt,
              NodeType.Clt,
              NodeType.Cgt_Un,
              NodeType.Clt_Un,
              NodeType.Eq,
              NodeType.Gt,
              NodeType.Ge,
              NodeType.Lt,
              NodeType.Le,
              NodeType.LogicalAnd,
              NodeType.LogicalOr,
              NodeType.LogicalEqual,
              NodeType.LogicalImply,
              NodeType.LogicalNot
            };
            if (Array.IndexOf(booleanNodeTypes, condition.NodeType) >= 0)
              condition.Type = SystemTypes.Boolean;
            if (Array.IndexOf(booleanNodeTypes, trueExpression.NodeType) >= 0)
              trueExpression.Type = SystemTypes.Boolean;
            if (Array.IndexOf(booleanNodeTypes, falseExpression.NodeType) >= 0)
              falseExpression.Type = SystemTypes.Boolean;

            // The expressions are valid, so see what kind of abnormal expression to create.
            Expression abnormal = null;

            // Check for short-circuit boolean binary expressions.
            Literal literal = trueExpression as Literal;
            if (literal != null && falseExpression.Type == SystemTypes.Boolean) {
              if (0.Equals(literal.Value)) { // !trueExp && falseExp
                
                abnormal = new BinaryExpression(
                  Abnormalizer.LogicallyNegate(condition),
                  falseExpression,
                  NodeType.LogicalAnd,
                  SystemTypes.Boolean,
                  condition.SourceContext);
              
              } else if (1.Equals(literal.Value)) { // trueExp || falseExp
                
                abnormal = new BinaryExpression(
                  condition,
                  falseExpression,
                  NodeType.LogicalOr,
                  SystemTypes.Boolean,
                  condition.SourceContext);
              }
            }

            // Possibly merge it as part of an existing ternary expression.
            if (abnormal == null) {
              TernaryExpression ternary = falseExpression as TernaryExpression;
              if (ternary != null &&
                  ternary.NodeType == NodeType.Conditional) {
                if (trueExpression == ternary.Operand3) {

                  // It's a logical && embedded in a ternary expression.
                  abnormal = new TernaryExpression(
                    new BinaryExpression(
                      Abnormalizer.LogicallyNegate(condition),
                      ternary.Operand1,
                      NodeType.LogicalAnd,
                      SystemTypes.Boolean,
                      condition.SourceContext),
                    ternary.Operand2,
                    ternary.Operand3,
                    ternary.NodeType,
                    ternary.Type);

                } else if (trueExpression == ternary.Operand2) {

                  // It's a logical || embedded in a ternary expression.
                  abnormal = new TernaryExpression(
                    new BinaryExpression(
                      condition,
                      ternary.Operand1,
                      NodeType.LogicalOr,
                      SystemTypes.Boolean,
                      condition.SourceContext),
                    ternary.Operand2,
                    ternary.Operand3,
                    ternary.NodeType,
                    ternary.Type);
                }
              }
            }

            // If all else fails, treat it as a ternary expression.
            if (abnormal == null) {
              // Infer the correct type of the expression.
              TypeNode type = null;
              if (Literal.IsNullLiteral(trueExpression))
                type = falseExpression.Type;
              else if (Literal.IsNullLiteral(falseExpression))
                type = trueExpression.Type;
              else if (trueExpression.Type.IsAssignableTo(falseExpression.Type))
                type = falseExpression.Type;
              else if (falseExpression.Type.IsAssignableTo(trueExpression.Type))
                type = trueExpression.Type;
              else // Instead of finding the greatest common denominator, just use object.
                type = SystemTypes.Object;

              abnormal = new TernaryExpression(
                condition,
                trueExpression,
                falseExpression,
                NodeType.Conditional,
                type);
            }

            Debug.Assert(abnormal.Type != null, "We should never create an expression with a null type.");

            // Try and get the best source context for the abnormal expression.
            if (abnormal.SourceContext.Document == null)
              abnormal.SourceContext = node.Outgoing[0].SourceContext;
            if (abnormal.SourceContext.Document == null)
              abnormal.SourceContext = condition.SourceContext;
            if (abnormal.SourceContext.Document == null)
              abnormal.SourceContext = trueExpression.SourceContext;
            if (abnormal.SourceContext.Document == null)
              abnormal.SourceContext = falseExpression.SourceContext;
            if (abnormal.SourceContext.Document == null)
              abnormal.SourceContext = trueStatement.SourceContext;
            if (abnormal.SourceContext.Document == null)
              abnormal.SourceContext = falseStatement.SourceContext;
            if (abnormal.SourceContext.Document == null)
              abnormal.SourceContext = node.Block.SourceContext;
            // TODO: Keep looking more places for the elusive source context.
            
            // Replace the branch with the abnormal expression and unconditional jump.
            RemoveStatements(node.Block.Statements, node.Outgoing[0]);
            node.Block.Statements.Add(new ExpressionStatement(abnormal, abnormal.SourceContext));
            node.Block.Statements.Add(node0.Outgoing[0]);
            node.Refresh();

            // Remove the tail blocks if they are no longer used.
            if (node0.Incoming.Count == 0)
              graph.Remove(node0);
            if (node1.Incoming.Count == 0)
              graph.Remove(node1);

            return true;
          }
        }
      }

      return false;
    }

    /// <summary>
    /// Logically negates an expression, cancelling out double negations if possible.
    /// </summary>
    /// <param name="expression">Expression to negate.</param>
    /// <returns>The negated expression.</returns>
    private static Expression LogicallyNegate(Expression expression) {
      UnaryExpression unary = expression as UnaryExpression;
      if (unary != null &&
          unary.NodeType == NodeType.LogicalNot)
        return unary.Operand;
      return new UnaryExpression(expression, NodeType.LogicalNot, SystemTypes.Boolean, expression.SourceContext);
    }

    /// <summary>
    /// Collapses unnecessary double-branches into a single branch.
    /// </summary>
    /// <param name="node">Node that possibly begins a double-branch.</param>
    /// <returns>True if a double Branch was collapsed; false otherwise.</returns>
    private static bool CollapseDoubleBranches(BlockNode node) {
      // Sometimes a method body can have blocks like:
      // [ .... ]
      // [branch]
      //    |
      //    |
      //    |
      // [branch]
      //    |
      //    |
      //    |
      // [ .... ]
      // 
      // We collapse these into:
      // [ .... ]
      // [branch]
      //    |
      //    |
      //    |
      // [ .... ]

      if (node.Outgoing.Count == 1) {
        BlockNode target = node.Graph[node.Outgoing[0].Target];
        if (target.Outgoing.Count == 1 &&
            (target.Block.Statements.Count == 0 ||
             target.Block.Statements.Count == 1 && target.Block.Statements[0] is Branch)) {
          RemoveStatements(node.Block.Statements, node.Outgoing[0]);
          node.Block.Statements.Add(target.Outgoing[0]);
          node.Refresh();

          if (target.Incoming.Count == 0)
            node.Graph.Remove(target);

          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Collapses ExpressionStatements followed by a pop into inline expressions.
    /// </summary>
    /// <param name="node">Node that possibly contains an ExpressionStatement and Branch followed by a Pop.</param>
    /// <returns>True if an ExpressionStatement was inlined, false otherwise.</returns>
    private static bool InlineExpressionStatement(BlockNode node) {
      // Sometimes a method body can have blocks like:
      // [ .... ]
      // [ expr ]
      // [branch]
      //    |
      //    |
      //    |
      // [f(pop)]
      // [ .... ]
      // 
      // We collapse these into:
      // [ .... ]
      // [branch]
      //    |
      //    |
      //    |
      // [f(expr)]
      // [ ..... ]

      if (node.Outgoing.Count == 1 &&
          node.Block.Statements.Count > 0 &&
          node.Graph[node.Outgoing[0].Target].Incoming.Count == 1 &&
          node.Outgoing[0].Target.Statements != null &&
          node.Outgoing[0].Target.Statements.Count > 0) {

        // Find the last expression.
        ExpressionStatement expressionStatement = null;
        for (int s = node.Block.Statements.Count - 1; s >= 0; s--) {
          expressionStatement = node.Block.Statements[s] as ExpressionStatement;
          if (node.Block.Statements[s] != null &&
              node.Block.Statements[s] != node.Outgoing[0])
            break;
        }

        if (expressionStatement != null &&
            expressionStatement.Expression != null) {
          // Attempt to replace the the first pop in the target block.
          PopReplacer replacer = new PopReplacer(expressionStatement.Expression);
          replacer.Visit(node.Outgoing[0].Target);
          if (replacer.Replaced > 0) {
            RemoveStatements(node.Block.Statements, expressionStatement);
            return true;
          }
        }
      }

      return false;
    }

    /// <summary>
    /// Removes statements from a list of statements.
    /// </summary>
    /// <param name="statements">Statements to remove from.</param>
    /// <param name="remove">Statement(s) to remove.</param>
    private static void RemoveStatements(StatementList statements, params Statement[] remove) {
      int count = 0;
      for (int s = 0; s < statements.Count; s++, count++) {
        for (int r = 0; r < remove.Length; r++) {
          if (statements[count] == remove[r]) {
            s++;
            break;
          }
        }
        if (s >= statements.Count)
          break;
        if (count != s)
          statements[count] = statements[s];
      }
      statements.Count = count;
    }

    /// <summary>
    /// Block graph of a method that contains strongly linked <see cref="BlockNode"/>s.
    /// </summary>
    private class BlockGraph : IEnumerable<BlockNode> {
      private Dictionary<Block, BlockNode> nodes = new Dictionary<Block, BlockNode>();
      private Method method;

      public BlockGraph(Method method) {
        this.method = method;

        foreach (Block block in method.Body.Statements)
          if (!this.nodes.ContainsKey(block))
            this.nodes[block] = new BlockNode(this, block);

        foreach (BlockNode node in this.nodes.Values)
          node.Refresh();
      }

      public BlockNode this[Block block] {
        get {
          BlockNode node;
          if (!this.nodes.TryGetValue(block, out node))
            this.nodes[block] = node = new BlockNode(this, block);
          return node;
        }
      }

      public Block GetNextBlock(Block block) {
        for (int b = 0; b < this.method.Body.Statements.Count; b++)
          if (this.method.Body.Statements[b] == block)
            for (b = b + 1; b < this.method.Body.Statements.Count; b++)
              if (this.method.Body.Statements[b] != null)
                return (Block)this.method.Body.Statements[b];
        return null;
      }

      public void Remove(BlockNode node) {
        Debug.Assert(node.Incoming.Count == 0);
        RemoveStatements(this.method.Body.Statements, node.Block);

        node.Block.Statements = new StatementList();
        node.Refresh();
      }

      #region IEnumerable<BlockNode> Members

      public IEnumerator<BlockNode> GetEnumerator() {
        return this.nodes.Values.GetEnumerator();
      }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
        return GetEnumerator();
      }

      #endregion
    }

    /// <summary>
    /// Block node in the graph that contains a block and its incoming and outgoing branches.
    /// </summary>
    private class BlockNode {
      public readonly BlockGraph Graph;
      public readonly Block Block;

      public readonly List<Branch> Incoming = new List<Branch>();
      public readonly List<Branch> Outgoing = new List<Branch>();

      public BlockNode(BlockGraph graph, Block block) {
        this.Graph = graph;
        this.Block = block;
      }

      public void Refresh() {
        // Remove the outgoing branches.
        foreach (Branch branch in this.Outgoing)
          this.Graph[branch.Target].Incoming.Remove(branch);
        this.Outgoing.Clear();

        // Assume the block falls through if it's not the last block in the method body.
        Block next = this.Graph.GetNextBlock(this.Block);

        // Look for branches.
        for (int s = 0; s < this.Block.Statements.Count; s++) {
          Branch branch = this.Block.Statements[s] as Branch;
          if (branch != null) {
            if (branch.Condition == null)
              next = null;

            // Add the branch to the outgoing branches of this node and incoming branches of the target node.
            this.Outgoing.Add(branch);
            this.Graph[branch.Target].Incoming.Add(branch);
          }
        }

        if (next != null) {
          // Add the fallthrough to the outgoing branches of this node and incoming branches of the target node.
          Branch branch = new Branch(null, next);
          this.Outgoing.Add(branch);
          this.Graph[branch.Target].Incoming.Add(branch);
        }
      }
    }

    /// <summary>
    /// Replaces pops with expressions.
    /// </summary>
    private class PopReplacer : StandardVisitor {
      private Stack<Expression> replacements;
      private int replaced = 0;

      public PopReplacer(params Expression[] replacements) {
        this.replacements = new Stack<Expression>(replacements);
      }

      public override ExpressionList VisitExpressionList(ExpressionList expressions) {
        if (expressions == null) return null;
        for (int i = expressions.Count - 1, n = 0; i >= n; i--)
          expressions[i] = this.VisitExpression(expressions[i]);
        return expressions;
      }

      public override Expression VisitExpression(Expression expression) {
        if (replacements.Count > 0 &&
            expression != null &&
            expression.GetType().TypeHandle.Value == typeof(Expression).TypeHandle.Value &&
            expression.NodeType == NodeType.Pop) {
          replaced++;
          return replacements.Pop();
        }

        return base.VisitExpression(expression);
      }

      public int Replaced {
        get {
          return replaced;
        }
      }
    }
  }
}
