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
using Microsoft.Research.DataStructures;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public enum SourceLanguage { CSharp, VB }


  #region ExpressionInPreState

  public enum ExpressionInPreStateKind { MethodPrecondition, ObjectInvariant, Assume, Any, All=Assume, Bot=Any, Top=All }


  public class ExpressionInPreStateInfo
  {
    public readonly bool hasAccessPath;
    public readonly bool hasVariables;
    public readonly bool hasOnlyImmutableVariables;
    public ExpressionInPreStateKind kind { get; private set; }

    public ExpressionInPreStateInfo(bool hasAccessPath, bool hasVariables, bool hasOnlyImmutableVariables, ExpressionInPreStateKind kind)
    {
      Contract.Ensures(this.kind == kind);

      this.hasAccessPath = hasAccessPath;
      this.hasVariables = hasVariables;
      this.hasOnlyImmutableVariables = hasOnlyImmutableVariables;
      this.kind = kind;
    }

    public ExpressionInPreStateInfo(ExpressionInPreStateKind kind, bool hasOnlyImmutableVariables = false)
      : this(false, false, hasOnlyImmutableVariables, kind)
    { }

    #region Combine

    static public ExpressionInPreStateInfo Combine(ExpressionInPreStateInfo info1, ExpressionInPreStateInfo info2)
    {
      Contract.Requires(info1 != null);
      Contract.Requires(info2 != null);
      Contract.Ensures(Contract.Result<ExpressionInPreStateInfo>() != null);

      ExpressionInPreStateKind kindResult = info1.kind.Join(info2.kind);
      return new ExpressionInPreStateInfo(info1.hasAccessPath || info2.hasAccessPath, info1.hasVariables || info2.hasVariables, info1.hasOnlyImmutableVariables && info2.hasOnlyImmutableVariables, kindResult);
    }

    static public ExpressionInPreStateInfo Combine(ExpressionInPreStateInfo info1, ExpressionInPreStateInfo info2, ExpressionInPreStateInfo info3)
    {
      Contract.Requires(info1 != null);
      Contract.Requires(info2 != null);
      Contract.Requires(info3 != null);
      Contract.Ensures(Contract.Result<ExpressionInPreStateInfo>() != null);

      var tmp = Combine(info1, info2);
      return Combine(tmp, info3);
    }

    #endregion
  }

  public enum ConditionKind
  {
    Requires,
    ObjectInvariant,
    EntryAssume,
    CalleeAssume
  }

  [ContractClass(typeof(IInferredConditionContracts))]
  public interface IInferredCondition
  {
    BoxedExpression Expr { get; }
    ConditionKind Kind { get; }
    bool IsSufficientForTheWarning { get; }
  }

  #region Contracts
  [ContractClassFor(typeof(IInferredCondition))]
  abstract class IInferredConditionContracts : IInferredCondition
  {
    public BoxedExpression Expr { get; private set; }
    public ConditionKind Kind { get; private set; }
    public bool IsSufficientForTheWarning { get; private set; }

    [ContractInvariantMethod]
    void ContractInvariant()
    {
      Contract.Invariant(this.Expr != null);
    }
  }
  #endregion

  public class SimpleInferredConditionAtEntry : IInferredCondition
  {
    public BoxedExpression Expr { get; private set; }
    public ConditionKind Kind { get; private set; }
    public bool IsSufficientForTheWarning { get; private set; }
    public SimpleInferredConditionAtEntry(BoxedExpression expr, ExpressionInPreStateKind kind, bool isSufficient)
    {
      this.Expr = expr;
      this.Kind = kind.ToConditionKind();
      this.IsSufficientForTheWarning = isSufficient;
    }

    [Pure]
    public override string ToString()
    {
      return this.Expr != null ? this.Expr.ToString() : "null";
    }
  }

  public class InferredCalleeCondition<Method> : IInferredCondition
  {    
    public BoxedExpression Expr { get; private set; }
    public Method Callee { get; private set; }
    public APC CalleePC { get; private set; }

    public InferredCalleeCondition(BoxedExpression expr, APC callePC, Method callee)
    {
      this.Expr = expr;
      this.CalleePC = callePC;
      this.Callee = callee;
    }

    public ConditionKind Kind
    {
      get { return ConditionKind.CalleeAssume; }
    }

    public bool IsSufficientForTheWarning
    {
      get { return false; }
    }
  }

  public class InferredCalleeNecessaryConditionCandidate<Method> : IInferredCondition
  {
    public BoxedExpression Expr { get; private set; }
    public Method Callee { get; private set; }
    public APC CalleePC { get; private set; }

    public InferredCalleeNecessaryConditionCandidate(BoxedExpression expr, APC callePC, Method callee)
    {
      Contract.Requires(expr != null);

      this.Expr = expr;
      this.CalleePC = callePC;
      this.Callee = callee;
    }

    public ConditionKind Kind
    {
      get { return ConditionKind.CalleeAssume; }
    }

    public bool IsSufficientForTheWarning
    {
      get { return false; }
    }
  }

  public class ExpressionInPreState : ExpressionInPreStateInfo
  {

    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.expr != null);
    }


    public BoxedExpression expr { get; private set; }

    public ExpressionInPreState(BoxedExpression expr, ExpressionInPreStateInfo info)
      : base(info.hasAccessPath, info.hasVariables, info.hasOnlyImmutableVariables, info.kind)
    {
      Contract.Requires(expr != null);
      Contract.Requires(info != null);

      Contract.Ensures(this.kind == info.kind);

      this.expr = expr;
    }

    [Pure]
    public override string ToString()
    {
      return this.expr.ToString();
    }
  }

  public class InferredConditions : List<IInferredCondition>
  {
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(Contract.ForAll(this, pre => pre != null));
    }

    public InferredConditions(IEnumerable<IInferredCondition> collection)
      : base(collection == null ? null : collection.Where(pre => pre != null))
    { }

#if false
    public InferredPreconditions(IEnumerable<BoxedExpression> collection, ExpressionInPreStateKind kind, bool isSufficient)
      : this(collection == null ? null : collection.Where(be => be != null).Select(be => new SimpleInferredPrecondition(be, kind, isSufficient)))
    { }
#endif

    public bool HasASufficientCondition
    {
      get
      {
        return this.Any(c => c.IsSufficientForTheWarning);
      }
    }

    public Set<WarningContext> PushToContractManager(ContractInferenceManager inferenceManager, bool addFalseEntryAssume, ProofObligation obl, ref ProofOutcome outcome, ILogOptions logOptions)
    {
      Contract.Requires(inferenceManager != null);
      Contract.Requires(obl != null);
      Contract.Requires(logOptions != null);

      Contract.Ensures(Contract.Result<Set<WarningContext>>() != null);

      IEnumerable<BoxedExpression> suggestedPreconditions, objectInvariants, entryAssumes;
      IEnumerable<IInferredCondition> calleeAssumes;


      this.Split(out suggestedPreconditions, out objectInvariants, out entryAssumes, out calleeAssumes);

      if (addFalseEntryAssume)
      {
        entryAssumes = entryAssumes.Concat(new BoxedExpression[] { BoxedExpression.ConstFalse });
      }

      var entryOutcome = outcome;
      var result = new Set<WarningContext>();

      if (objectInvariants.Any())
      {
        outcome = inferenceManager.ObjectInvariant.AddObjectInvariants(obl, objectInvariants, logOptions.PropagateObjectInvariants
              ? (objectInvariants.Where(exp => this.IsSufficient(exp)).Any() ? ProofOutcome.True : outcome)
              : outcome);

        result.Add(new WarningContext(WarningContext.ContextType.InferredObjectInvariant, objectInvariants.Count()));
      }
      if (suggestedPreconditions.Any())
      {
        outcome = inferenceManager.AddPreconditionOrAssume(obl, suggestedPreconditions, logOptions.PropagateInferredRequires(true)
              ? 
                (suggestedPreconditions
                .Where(exp => (entryOutcome != ProofOutcome.False || !exp.IsConstantFalse()) && this.IsSufficient(exp))
                .Any() ? ProofOutcome.True : outcome)
              : 
                outcome);
        result.Add(new WarningContext(WarningContext.ContextType.InferredPrecondition, suggestedPreconditions.Count()));
      }
      if (entryAssumes.Any())
      {
        inferenceManager.Assumptions.AddEntryAssumes(obl, entryAssumes);

        result.Add(new WarningContext(WarningContext.ContextType.InferredEntryAssume, entryAssumes.Count()));
      }

      if (calleeAssumes.Any())
      {
        var warningContexts = inferenceManager.Assumptions.AddCalleeAssumes(obl, calleeAssumes);

        result.AddRange(warningContexts);
      }

      if (this.HasASufficientCondition && this.All(cond => !cond.Expr.IsConstantFalse()))
      {
        result.Add(new WarningContext(WarningContext.ContextType.InferredConditionIsSufficient));
      }

      if (this.Any(
            cond =>
              {
                BinaryOperator bop; BoxedExpression dummy1, dummy2;

                return cond.Expr.IsBinaryExpression(out bop, out dummy1, out dummy2) && bop == BinaryOperator.LogicalOr;
              }))
      {
        result.Add(new WarningContext(WarningContext.ContextType.InferredConditionContainsDisjunction));
      }

      return result;
    }


    private void Split(
      out IEnumerable<BoxedExpression> suggestedPreconditions, 
      out IEnumerable<BoxedExpression> objectInvariants, 
      out IEnumerable<BoxedExpression> entryAssumes,
      out IEnumerable<IInferredCondition> calleeAssumes
      )
    {
      Contract.Ensures(Contract.ValueAtReturn(out suggestedPreconditions) != null);
      Contract.Ensures(Contract.ValueAtReturn(out objectInvariants) != null);
      Contract.Ensures(Contract.ValueAtReturn(out entryAssumes) != null);
      Contract.Ensures(Contract.ValueAtReturn(out calleeAssumes) != null);
      Contract.Ensures(Contract.ForAll(Contract.ValueAtReturn(out suggestedPreconditions), be => be != null));
      Contract.Ensures(Contract.ForAll(Contract.ValueAtReturn(out objectInvariants), be => be != null));

      suggestedPreconditions = this.Where(cond => cond.Kind == ConditionKind.Requires).Select(cond => cond.Expr);
      objectInvariants = this.Where(cond => cond.Kind == ConditionKind.ObjectInvariant).Select(cond => cond.Expr);
      entryAssumes = this.Where(cond=> cond.Kind == ConditionKind.EntryAssume).Select(cond => cond.Expr);
      calleeAssumes = this.Where(cond => cond.Kind == ConditionKind.CalleeAssume);
    }

    public bool IsSufficient(BoxedExpression exp)
    {
      return exp != null && this.Where(pre => pre.Expr.Equals(exp)).Any(pre => pre.IsSufficientForTheWarning);
    }
  }

  public static class ExpressionInPreStateExtensions
  {
    [Pure]
    public static InferredConditions AsInferredPreconditions(this IEnumerable<IInferredCondition> collection)
    {
      Contract.Ensures(collection == null || Contract.Result<InferredConditions>() != null);
      if (collection == null)
        return null;
      return new InferredConditions(collection);
    }

    [Pure]
    public static InferredConditions AsInferredPreconditions(this IEnumerable<ExpressionInPreState> collection, bool isSufficient)
    {
      Contract.Requires(collection != null);
      Contract.Ensures(Contract.Result<InferredConditions>() != null);

      return new InferredConditions(collection.Select(expr => new SimpleInferredConditionAtEntry(expr.expr, expr.kind, isSufficient)));
    }

    /// <summary>kind1 is included in kind2 (normal domain leq)</summary>
    [Pure]
    static public bool IsIncludedIn(this ExpressionInPreStateKind kind1, ExpressionInPreStateKind kind2)
    {
      return (kind1 == ExpressionInPreStateKind.Any || kind2 == ExpressionInPreStateKind.Assume) || kind1 == kind2;
    }

    [Pure]
    public static ExpressionInPreStateKind Join(this ExpressionInPreStateKind kind1, ExpressionInPreStateKind kind2)
    {
      if (kind1 == ExpressionInPreStateKind.Any) return kind2;
      if (kind2 == ExpressionInPreStateKind.Any) return kind1;
      if (kind2 == kind1) return kind1;
      return ExpressionInPreStateKind.Assume;
    }

    [Pure]
    public static ConditionKind ToConditionKind(this ExpressionInPreStateKind kind)
    {
      switch (kind)
      {
        case ExpressionInPreStateKind.Any:
        case ExpressionInPreStateKind.MethodPrecondition:
          return ConditionKind.Requires;
        case ExpressionInPreStateKind.Assume:
          return ConditionKind.EntryAssume;
        case ExpressionInPreStateKind.ObjectInvariant:
          return ConditionKind.ObjectInvariant;
        default:
          throw new NotImplementedException();
      }
    }
  }

  #endregion


  public class PreconditionSuggestion
  {
    class InvertComparison
    {
      private readonly bool tryInvert;
      private bool hasBeenInverted;

      public InvertComparison(bool tryInvert)
      {
        this.tryInvert = tryInvert;
      }

      public static InvertComparison NoInversion = new InvertComparison(false); // Thread-safe

      public bool TryInvertComparison { get { return this.tryInvert; } }

      public bool HasBeenInverted { get { return this.hasBeenInverted; } }

      public void DischargeInversion()
      {
        if (this.tryInvert)
        {
          this.hasBeenInverted = true;
        }
        else
        {
          throw new InvalidOperationException("Can't invert what needs no inversion");
        }
      }

    }

    class PreconditionTransformer<Local, Parameter, Method, Field, Property, Event, Expression, Type, Dest, Attribute, Assembly>
        : IVisitValueExprIL<Expression, Type, Expression, Dest, InvertComparison, BoxedExpression>
      where Type : IEquatable<Type>
    {
      APC conditionPC;
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Dest> context;
      bool hasVariables;
      public bool HasVariables { get { return this.hasVariables; } }

      /// <summary>
      /// </summary>
      /// <param name="conditionPC">PC where condition is being asserted</param>
      /// <param name="context"></param>
      /// <param name="mdDecoder"></param>
      public PreconditionTransformer(
        APC conditionPC,
        IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Dest> context,
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder
      )
      {
        this.conditionPC = conditionPC;
        this.context = context;
        this.mdDecoder = mdDecoder;
      }

      #region IVisitValueExprIL<Label,Type,Source,Dest,StringBuilder,bool> Members

      public BoxedExpression Recurse(Expression expr)
      {
        return Recurse(expr, InvertComparison.NoInversion);
      }

      private BoxedExpression Recurse(Expression expr, InvertComparison tryInvert)
      {
        return this.context.ExpressionContext.Decode<InvertComparison, BoxedExpression, PreconditionTransformer<Local, Parameter, Method, Field, Property, Event, Expression, Type, Dest, Attribute, Assembly>>(expr, this, tryInvert);
      }

      public BoxedExpression SymbolicConstant(Expression pc, Dest symbol, InvertComparison tryInvert)
      {
        FList<PathElement> path = context.ValueContext.VisibleAccessPathListFromPre(context.MethodContext.CFG.EntryAfterRequires, symbol);
        if (path != null)
        {
          this.hasVariables = true;
          return BoxedExpression.Var(symbol, path);
        }
        path = context.ValueContext.VisibleAccessPathListFromPre(conditionPC, symbol);
        if (path != null)
        {
          this.hasVariables = true;
          return BoxedExpression.Var(symbol, path);
        }
        return null;
      }

      public BoxedExpression Binary(Expression pc, BinaryOperator op, Dest dest, Expression s1, Expression s2, InvertComparison tryInvert)
      {
        Contract.Assume(tryInvert != null);

        BoxedExpression left = null;
        var right = Recurse(s2);
        if (right == null) return null;
        switch (op)
        {
          case BinaryOperator.Ceq:
          case BinaryOperator.Cobjeq:
            if (context.ExpressionContext.IsZero(s2))
            {
              InvertComparison tryInvert2 = new InvertComparison(true);
              BoxedExpression left2 = Recurse(s1, tryInvert2);
              if (left2 == null) return null;
              if (tryInvert2.HasBeenInverted)
              {
                // We know that left is a comparison. If outer wanted us to invert too, we cancel it here
                if (tryInvert.TryInvertComparison)
                {
                  left = Recurse(s1);
                  if (left == null) return null;
                  tryInvert.DischargeInversion();
                  return left;
                }
                return left2;
              }
            }
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              left = Recurse(s1);
              if (left == null) return null;
              return BoxedExpression.Binary(BinaryOperator.Cne_Un, left, right);
            }
            goto default;

          case BinaryOperator.Cge:
            left = Recurse(s1);
            if (left == null) return null;
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return BoxedExpression.Binary(BinaryOperator.Clt, left, right);
            }
            goto default;

          case BinaryOperator.Cge_Un:
            left = Recurse(s1);
            if (left == null) return null;
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return BoxedExpression.Binary(BinaryOperator.Clt_Un, left, right);
            }
            goto default;

          case BinaryOperator.Cgt:
            left = Recurse(s1);
            if (left == null) return null;
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return BoxedExpression.Binary(BinaryOperator.Cle, left, right);
            }
            goto default;

          case BinaryOperator.Cgt_Un:
            left = Recurse(s1);
            if (left == null) return null;
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return BoxedExpression.Binary(BinaryOperator.Cle_Un, left, right);
            }
            goto default;

          case BinaryOperator.Cle:
            left = Recurse(s1);
            if (left == null) return null;
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return BoxedExpression.Binary(BinaryOperator.Cgt, left, right);
            }
            goto default;

          case BinaryOperator.Cle_Un:
            left = Recurse(s1);
            if (left == null) return null;
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return BoxedExpression.Binary(BinaryOperator.Cgt_Un, left, right);
            }
            goto default;

          case BinaryOperator.Clt:
            left = Recurse(s1);
            if (left == null) return null;
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return BoxedExpression.Binary(BinaryOperator.Cge, left, right);
            }
            goto default;

          case BinaryOperator.Clt_Un:
            left = Recurse(s1);
            if (left == null) return null;
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return BoxedExpression.Binary(BinaryOperator.Cge_Un, left, right);
            }
            goto default;

          case BinaryOperator.Cne_Un:
            left = Recurse(s1);
            if (left == null) return null;
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return BoxedExpression.Binary(BinaryOperator.Ceq, left, right);
            }
            goto default;

          default:
            if (left == null) left = Recurse(s1);
            if (left == null) return null;
            return BoxedExpression.Binary(op, left, right);
        }
      }

      public BoxedExpression Isinst(Expression pc, Type type, Dest dest, Expression obj, InvertComparison data)
      {
        if (!this.mdDecoder.IsAsVisibleAs(type, context.MethodContext.CurrentMethod)) return null;
        BoxedExpression arg = Recurse(obj);
        if (arg == null) return null;
        return ClousotExpression<Type>.MakeIsInst(type, arg);
      }

      public BoxedExpression Ldconst(Expression pc, object constant, Type type, Dest dest, InvertComparison data)
      {
        Contract.Assume(!this.mdDecoder.Equal(type, this.mdDecoder.System_Int32) || constant is Int32);

        return BoxedExpression.Const(constant, type, this.mdDecoder);
      }

      public BoxedExpression Ldnull(Expression pc, Dest dest, InvertComparison data)
      {
        return BoxedExpression.Const(null, default(Type), this.mdDecoder);
      }

      public BoxedExpression Sizeof(Expression pc, Type type, Dest dest, InvertComparison data)
      {
        return ClousotExpression<Type>.MakeSizeOf(type, this.mdDecoder.TypeSize(type));
      }

      public BoxedExpression Unary(Expression pc, UnaryOperator op, bool overflow, bool unsigned, Dest dest, Expression source, InvertComparison data)
      {
        BoxedExpression arg = Recurse(source);
        if (arg == null) return null;
        return ClousotExpression<Type>.MakeUnary(op, arg);
      }

      public BoxedExpression Box(Expression pc, Type type, Dest dest, Expression source, InvertComparison data)
      {
        // TODO: once we have boxed expressions with Box, we can fill this in.
        return null;
      }

      #endregion
    }

    class ExpressionDecoder<Local, Parameter, Method, Field, Property, Event, Expression, Type, Dest, Attribute, Assembly>
        : IVisitValueExprIL<Expression, Type, Expression, Dest, InvertComparison, string>
      where Type : IEquatable<Type>
    {
      #region Object invariant
      [ContractInvariantMethod]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.mdDecoder != null);
        Contract.Invariant(this.context != null);
      }
      #endregion

      readonly APC AtPC;
      readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
      readonly IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Dest> context;
      public bool HasVariables;

      public ExpressionDecoder(
        APC atPC,
        IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Dest> context,
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder
      )
      {
        Contract.Requires(context != null);
        Contract.Requires(mdDecoder != null);

        this.AtPC = atPC;
        this.context = context;
        this.mdDecoder = mdDecoder;
      }

      #region IVisitValueExprIL<Label,Type,Source,Dest,StringBuilder,bool> Members

      private string Recurse(Expression expr)
      {
        return Recurse(expr, InvertComparison.NoInversion);
      }

      private string Recurse(Expression expr, InvertComparison tryInvert)
      {
        return this.context.ExpressionContext.Decode<InvertComparison, string, ExpressionDecoder<Local, Parameter, Method, Field, Property, Event, Expression, Type, Dest, Attribute, Assembly>>(expr, this, tryInvert);
      }

      public string SymbolicConstant(Expression pc, Dest symbol, InvertComparison tryInvert)
      {
        HasVariables = true;
        string path = context.ValueContext.AccessPath(AtPC, symbol);
        return path;
      }

      public string Binary(Expression pc, BinaryOperator op, Dest dest, Expression s1, Expression s2, InvertComparison tryInvert)
      {
        Contract.Assume(tryInvert != null);

        string left = Recurse(s1);
        if (left == null) return null;
        string right = Recurse(s2);
        if (right == null) return null;
        switch (op)
        {
          case BinaryOperator.Add:
          case BinaryOperator.Add_Ovf:
          case BinaryOperator.Add_Ovf_Un:
            return String.Format("({0} + {1})", left, right);

          case BinaryOperator.And:
            return String.Format("({0} & {1})", left, right);

          case BinaryOperator.Ceq:
          case BinaryOperator.Cobjeq:
            if (right == "0")
            {
              InvertComparison tryInvert2 = new InvertComparison(true);
              string left2 = Recurse(s1, tryInvert2);
              if (tryInvert2.HasBeenInverted)
              {
                // We know that left is a comparison. If outer wanted us to invert too, we cancel it here
                if (tryInvert.TryInvertComparison)
                {
                  tryInvert.DischargeInversion();
                  return left;
                }
                return left2;
              }
            }
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return String.Format("({0} != {1})", left, right);
            }
            return String.Format("({0} == {1})", left, right);

          case BinaryOperator.Cge:
          case BinaryOperator.Cge_Un:
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return String.Format("({0} < {1})", left, right);
            }
            return String.Format("({0} >= {1})", left, right);

          case BinaryOperator.Cgt:
          case BinaryOperator.Cgt_Un:
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return String.Format("({0} <= {1})", left, right);
            }
            return String.Format("({0} > {1})", left, right);

          case BinaryOperator.Cle:
          case BinaryOperator.Cle_Un:
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return String.Format("({0} > {1})", left, right);
            }
            return String.Format("({0} <= {1})", left, right);

          case BinaryOperator.Clt:
          case BinaryOperator.Clt_Un:
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return String.Format("({0} >= {1})", left, right);
            }
            return String.Format("({0} < {1})", left, right);

          case BinaryOperator.Cne_Un:
            if (tryInvert.TryInvertComparison)
            {
              tryInvert.DischargeInversion();
              return String.Format("({0} == {1})", left, right);
            }
            return String.Format("({0} != {1})", left, right);

          case BinaryOperator.Div:
          case BinaryOperator.Div_Un:
            return String.Format("({0} / {1})", left, right);

          case BinaryOperator.Mul:
          case BinaryOperator.Mul_Ovf:
          case BinaryOperator.Mul_Ovf_Un:
            return String.Format("({0} * {1})", left, right);

          case BinaryOperator.Or:
            return String.Format("({0} | {1})", left, right);

          case BinaryOperator.Rem:
          case BinaryOperator.Rem_Un:
            return String.Format("({0} % {1})", left, right);

          case BinaryOperator.Shl:
            return String.Format("({0} << {1})", left, right);

          case BinaryOperator.Shr:
          case BinaryOperator.Shr_Un:
            return String.Format("({0} >> {1})", left, right);

          case BinaryOperator.Sub:
          case BinaryOperator.Sub_Ovf:
          case BinaryOperator.Sub_Ovf_Un:
            return String.Format("({0} - {1})", left, right);

          case BinaryOperator.Xor:
            return String.Format("({0} ^ {1})", left, right);

          default:
            throw new NotImplementedException("new binary operator?");
        }
      }

      public string Isinst(Expression pc, Type type, Dest dest, Expression obj, InvertComparison data)
      {
        string arg = Recurse(obj);
        if (arg == null) return null;
        return String.Format("({0} as {1})", arg, mdDecoder.Name(type));
      }

      public string Ldconst(Expression pc, object constant, Type type, Dest dest, InvertComparison data)
      {
        return constant.ToString();
      }

      public string Ldnull(Expression pc, Dest dest, InvertComparison data)
      {
        return "null";
      }

      public string Sizeof(Expression pc, Type type, Dest dest, InvertComparison data)
      {
        return String.Format("sizeof({0})", mdDecoder.FullName(type));
      }

      [ContractVerification(true)]
      public string Unary(Expression pc, UnaryOperator op, bool overflow, bool unsigned, Dest dest, Expression source, InvertComparison data)
      {
        string arg = Recurse(source);
        if (arg == null) return null;
        switch (op)
        {
          case UnaryOperator.Conv_i:
            return arg;

          case UnaryOperator.Conv_i1:
            return String.Format("(System.Int8){0}", arg);

          case UnaryOperator.Conv_i2:
            return String.Format("(System.Int16){0}", arg);

          case UnaryOperator.Conv_i4:
            return String.Format("(System.Int32){0}", arg);

          case UnaryOperator.Conv_i8:
            return String.Format("(System.Int64){0}", arg);

          case UnaryOperator.Conv_r_un:
            return arg;

          case UnaryOperator.Conv_r4:
            return String.Format("(System.Single){0}", arg);

          case UnaryOperator.Conv_r8:
            return String.Format("(System.Double){0}", arg);

          case UnaryOperator.Conv_u:
            return arg;

          case UnaryOperator.Conv_u1:
            return String.Format("(System.UInt8){0}", arg);

          case UnaryOperator.Conv_u2:
            return String.Format("(System.UInt16){0}", arg);

          case UnaryOperator.Conv_u4:
            return String.Format("(System.UInt32){0}", arg);

          case UnaryOperator.Conv_u8:
            return String.Format("(System.UInt64){0}", arg);

          case UnaryOperator.Neg:
            return String.Format("(-{0})", arg);

          case UnaryOperator.Not:
            return String.Format("(!{0})", arg);

          case UnaryOperator.WritableBytes:
            return String.Format("System.Diagnostics.Contracts.Contract.WritableBytes({0})", arg);

          default:
            return "<unknown unary operator?>";
          //Contract.Assert(false); // should be unreachable
          //throw new NotImplementedException("new unary operator?");
        }
      }

      public string Box(Expression pc, Type type, Dest dest, Expression source, InvertComparison data)
      {
        string arg = Recurse(source);
        if (arg == null) return null;
        return String.Format("(box {0} to {1})", arg, mdDecoder.Name(type));
      }

      #endregion
    }

    public static BoxedExpression ExpressionInPreState<Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Expression, Attribute, Assembly>
      (
      APC at,
      Expression condition,
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, SymbolicValue> context,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      out bool hasVariables
    )
      where Type : IEquatable<Type>
    {
      var preTrans = new PreconditionTransformer<Local, Parameter, Method, Field, Property, Event, Expression, Type, SymbolicValue, Attribute, Assembly>(at, context, mdDecoder);

      var result = preTrans.Recurse(condition);
      hasVariables = preTrans.HasVariables;

      return result;
    }

    /// <summary>
    /// Computes a BoxedExpression with internal accesspaths that are valid in the pre-state of the
    /// current method and these paths are "visible" according to the visibility rules of pre-conditions.
    /// </summary>
    /// <param name="hasVariables">true if expression contains any variables at all</param>
    /// <param name="conditionPC">the pc at which the condition is being tested</param>
    /// <returns>null if not expressible in pre-state</returns>    
    public static ExpressionInPreState ExpressionInPreState<Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Expression, Attribute, Assembly>(
      BoxedExpression condition,
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, SymbolicValue> context,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      APC conditionPC,
      ExpressionInPreStateKind allowedKinds
    )
      where Type : IEquatable<Type>
    {
      Contract.Requires(mdDecoder != null);

      continuations = new List<Tuple<BoxedExpression, BoxedExpression, BoxedExpression>>();

      ExpressionInPreStateInfo info;
      var boundVariables = new Set<BoxedExpression>();

      var allowObjectInvariants = ExpressionInPreStateKind.ObjectInvariant.IsIncludedIn(allowedKinds) && !mdDecoder.IsConstructor(context.MethodContext.CurrentMethod);
       
      var result = ExpressionInPreStateInternal(condition, context, mdDecoder, out info, conditionPC, allowObjectInvariants, boundVariables);

      if (result != null && continuations.Count > 0)
      {
        result = ApplyExistential(result);
      }

      continuations = null;

      if (info != null && info.kind.IsIncludedIn(allowedKinds))
      {
        return result == null ? null : new ExpressionInPreState(result, info);
      }
      return null;
    }

    private static class BoxedExpressionsUtils // taken from BoxedExpressionsUtils, TODO: solve dependencies
    {
      public static List<BoxedExpression> SplitConjunctions(BoxedExpression be)
      {
        Contract.Requires(be != null);
        Contract.Ensures(Contract.Result<List<BoxedExpression>>() != null);

        var result = new List<BoxedExpression>();
        return SplitConjunctionsHelper(be, result);
      }
      private static List<BoxedExpression> SplitConjunctionsHelper(BoxedExpression be, List<BoxedExpression> result)
      {
        Contract.Requires(be != null);
        Contract.Requires(result != null);
        Contract.Ensures(Contract.Result<List<BoxedExpression>>() != null);

        if (be.IsBinary && be.BinaryOp == BinaryOperator.LogicalAnd)
        {
          result = SplitConjunctionsHelper(be.BinaryRight, SplitConjunctionsHelper(be.BinaryLeft, result));
        }
        else
        {
          result.Add(be);
        }

        return result;
      }
    }

    public static IEnumerable<ExpressionInPreState> ExpressionsInPreState<Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Expression, Attribute, Assembly>(
      BoxedExpression condition,
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, SymbolicValue> context,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      APC conditionPC,
      ExpressionInPreStateKind allowedKinds
    )
      where Type : IEquatable<Type>
    {
      Contract.Ensures(Contract.Result<IEnumerable<ExpressionInPreState>>() == null || Contract.ForAll(Contract.Result<IEnumerable<ExpressionInPreState>>(), e => e != null));

      foreach (var subCondition in BoxedExpressionsUtils.SplitConjunctions(condition))
      {
        if (subCondition == null)
          continue;
        var result = ExpressionInPreState(subCondition, context, mdDecoder, conditionPC, allowedKinds);
        if (result == null)
          continue;
        yield return result;
      }
    }

    private static BoxedExpression ApplyExistential(BoxedExpression result)
    {
      Contract.Requires(result != null);
      Contract.Ensures(Contract.Result<BoxedExpression>() != null);

      foreach (var triple in continuations)
      {
        Contract.Assume(triple.Item1 != null);
        Contract.Assume(triple.Item2 != null);
        Contract.Assume(triple.Item3 != null);
        result = new ExistsIndexedExpression(null, triple.Item1, triple.Item2, triple.Item3, result);
      }

      return result;
    }

    [ThreadStatic]
    private static List<Tuple<BoxedExpression, BoxedExpression, BoxedExpression>> continuations;

    [ContractVerification(false)]
    private static BoxedExpression ExpressionInPreStateInternal<Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Expression, Attribute, Assembly>(
      BoxedExpression condition,
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, SymbolicValue> context,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      out ExpressionInPreStateInfo info,
      APC conditionPC,
      bool allowObjectInvariant, // only false if current method is a constructor!
      Set<BoxedExpression> boundVariables
    )
      where Type : IEquatable<Type>
    {
      Contract.Requires(boundVariables != null);

      if (boundVariables.Contains(condition))
      {
        Contract.Assume(condition != null);
        info = new ExpressionInPreStateInfo(ExpressionInPreStateKind.Any);
        return condition;
      }

      if (condition.IsVariable)
      {
        // Special case as we may have some slack variable
        SymbolicValue var;
        if (!condition.TryGetFrameworkVariable(out var))
        {
          info = null;
          return null;
        }

        // First check if we've got a constant
        Type type;
        object value;
        if (context.ValueContext.IsConstant(conditionPC, var, out type, out value))
        {
          info = new ExpressionInPreStateInfo(ExpressionInPreStateKind.Any);
          return BoxedExpression.Const(value, type, mdDecoder);
        }

        var isConstructor = mdDecoder.IsConstructor(context.MethodContext.CurrentMethod);

        FList<PathElement> accessPath;
        #region Try to generate a precondition
        accessPath = context.ValueContext.VisibleAccessPathListFromPre(conditionPC, var); // ConditionPC okay here, because this also checks that the path is unmodified
        var headIsThis = accessPath != null ? accessPath.Head.ToString() == "this" : false;

        if (accessPath != null
          && (!isConstructor || !headIsThis))
        {
          var resultKind = headIsThis ? ExpressionInPreStateKind.Any : ExpressionInPreStateKind.MethodPrecondition;
          var hasOnlyImmutableVariables = accessPath.Length() == 1 || condition.IsArrayLength(conditionPC, context, mdDecoder);

          info = new ExpressionInPreStateInfo(accessPath.Length() > 1, true, hasOnlyImmutableVariables, resultKind);
          return BoxedExpression.Var(var, accessPath);
        }
        #endregion

        #region Try to generate an Assume or an Object Invariant

        accessPath = context.ValueContext.AccessPathList(conditionPC, var, false, false);
        if (accessPath != null && !conditionPC.Equals(context.MethodContext.CFG.EntryAfterRequires))
        {
          // check that path is unmodified since entry
          if (!context.ValueContext.PathUnmodifiedSinceEntry(conditionPC, accessPath))
          {
            accessPath = null; // can't use it
          }
        }
        if (accessPath != null
          && accessPath.Head.IsParameter
          && accessPath.Head.ToString() == "this"
          && context.ValueContext.PathSuitableInRequires(conditionPC, accessPath))
        {
          var isReadOnly = mdDecoder.IsReadOnly(accessPath);

          if (!isConstructor && isReadOnly && allowObjectInvariant)
          {
            var kind = ExpressionInPreStateKind.ObjectInvariant;

            info = new ExpressionInPreStateInfo(accessPath.Length() > 1, true, false, kind);
            return BoxedExpression.Var(var, accessPath);
          }
        }

        #endregion

        #region Try to generate any kind of entry assumption
        //accessPath = context.ValueContext.AccessPathList(conditionPC, var, false, false);

        if (accessPath != null
          && (!isConstructor || accessPath.Head.ToString() != "this"))
        {

          var hasOnlyImmutableVariables = accessPath.Length() == 1 || condition.IsArrayLength(conditionPC, context, mdDecoder);

          info = new ExpressionInPreStateInfo(accessPath.Length() > 1, true, hasOnlyImmutableVariables, ExpressionInPreStateKind.Assume);
          return BoxedExpression.Var(var, accessPath);
        }
        #endregion

        info = null;
        return null;
      }
      if (condition.IsConstant || condition.IsNull)
      {
        // no nested variables
        Contract.Assume(!mdDecoder.Equal((Type)condition.ConstantType, mdDecoder.System_Int32) || condition.Constant is Int32);
        info = new ExpressionInPreStateInfo(ExpressionInPreStateKind.Any, true);
        return BoxedExpression.Const(condition.Constant, (Type)condition.ConstantType, mdDecoder);
      }
      if (condition.IsSizeOf)
      {
        int size;
        object type;
        condition.SizeOf(out type, out size);
        info = new ExpressionInPreStateInfo(ExpressionInPreStateKind.Any);
        return BoxedExpression.SizeOf((Type)type, size);
      }
      if (condition.IsUnary)
      {
        // Recurse
        var arg = ExpressionInPreStateInternal(condition.UnaryArgument, context, mdDecoder, out info, conditionPC, allowObjectInvariant, boundVariables);
        if (arg != null)
        {
          return BoxedExpression.Unary(condition.UnaryOp, arg);
        }
        return null;
      }
      if (condition.IsBinary)
      {
        ExpressionInPreStateInfo infoLeft, infoRight;
        // Recurse
        var left = ExpressionInPreStateInternal(condition.BinaryLeft, context, mdDecoder, out infoLeft, conditionPC, allowObjectInvariant, boundVariables);
        if (left == null) { info = null; return null; }

        var right = ExpressionInPreStateInternal(condition.BinaryRight, context, mdDecoder, out infoRight, conditionPC, allowObjectInvariant, boundVariables);
        if (right == null) { info = null; return null; }

        info = ExpressionInPreStateInfo.Combine(infoLeft, infoRight);
        return BoxedExpression.Binary(condition.BinaryOp, left, right);
      }

      BoxedExpression arrayExp, indexExp;
      object t;
      if (condition.IsArrayIndexExpression(out arrayExp, out indexExp, out t) && t is Type)
      {
        ExpressionInPreStateInfo infoArray, infoIndex;

        var newArrayExp = ExpressionInPreStateInternal(arrayExp, context, mdDecoder, out infoArray, conditionPC, allowObjectInvariant, boundVariables);
        if (newArrayExp == null) { info = null; return null; }

        var newIndexExp = ExpressionInPreStateInternal(indexExp, context, mdDecoder, out infoIndex, conditionPC, allowObjectInvariant, boundVariables);
        if (newIndexExp == null)
        {
          // try to introduce the exists

          SymbolicValue arrayVar, arrayLen;
          if (newArrayExp.TryGetFrameworkVariable(out arrayVar)
            && context.ValueContext.TryGetArrayLength(conditionPC, arrayVar, out arrayLen))
          {
            var type = context.ValueContext.GetType(conditionPC, arrayVar);

            var boundVar = BoxedExpression.Var(string.Format("__j{0}__", continuations.Count));
            var zero = BoxedExpression.Const(0, mdDecoder.System_Int32, mdDecoder);

            // TODO: the info kind computation does not seem general enough here [MAF 3/23/13]

            info = new ExpressionInPreStateInfo(true, infoArray.hasVariables, false, ExpressionInPreStateKind.MethodPrecondition);
            FList<PathElement> accessPath = null;
            if (infoArray.kind.IsIncludedIn(ExpressionInPreStateKind.MethodPrecondition))
            {
              accessPath = context.ValueContext.VisibleAccessPathListFromPre(conditionPC, arrayLen);
            }
            if (accessPath == null && infoArray.kind.IsIncludedIn(ExpressionInPreStateKind.ObjectInvariant))
            {
              accessPath = context.ValueContext.AccessPathList(context.MethodContext.CFG.EntryAfterRequires, arrayLen, false, false);
              info = new ExpressionInPreStateInfo(true, infoArray.hasVariables, false, ExpressionInPreStateKind.ObjectInvariant);
            }

            if (accessPath != null)
            {
              var arrayLength = BoxedExpression.Var(arrayLen, context.ValueContext.VisibleAccessPathListFromPre(conditionPC, arrayLen));

              // Add the continuation
              continuations.Add(
                new Tuple<BoxedExpression, BoxedExpression, BoxedExpression>(boundVar, zero, arrayLength));

              return new BoxedExpression.ArrayIndexExpression<Type>(newArrayExp, boundVar, type.IsNormal ? type.Value : mdDecoder.System_Object);
            }
          }

          info = null;
          return null;
        }

        info = ExpressionInPreStateInfo.Combine(infoArray, infoIndex);
        return new BoxedExpression.ArrayIndexExpression<Type>(newArrayExp, newIndexExp, (Type)t);
      }
      bool isForAll;
      BoxedExpression boundExp, lowerBound, upperBound, body;
      if (condition.IsQuantifiedExpression(out isForAll, out boundExp, out lowerBound, out upperBound, out body))
      {
        ExpressionInPreStateInfo infoLowerBound, infoUpperBound, infoBody;

        boundVariables.Add(boundExp);

        var newLowerBound = ExpressionInPreStateInternal(lowerBound, context, mdDecoder, out infoLowerBound, conditionPC, allowObjectInvariant, boundVariables);
        if (newLowerBound == null) { info = null; return null; }

        var newUpperBound = ExpressionInPreStateInternal(upperBound, context, mdDecoder, out infoUpperBound, conditionPC, allowObjectInvariant, boundVariables);
        if (newUpperBound == null) { info = null; return null; }

        var newBody = ExpressionInPreStateInternal(body, context, mdDecoder, out infoBody, conditionPC, allowObjectInvariant, boundVariables);
        if (newBody == null) { info = null; return null; }

        info = ExpressionInPreStateInfo.Combine(infoLowerBound, infoUpperBound, infoBody);
        if (isForAll)
        {
          return new ForAllIndexedExpression(null, boundExp, newLowerBound, newUpperBound, newBody);
        }
        else
        {
          return new ExistsIndexedExpression(null, boundExp, newLowerBound, newUpperBound, newBody);
        }
      }
      object testtype;
      BoxedExpression test;
      if (condition.IsIsInstExpression(out test, out testtype))
      {
        var rec = ExpressionInPreStateInternal(test, context, mdDecoder, out info, conditionPC, allowObjectInvariant, boundVariables);
        if (rec == null)
        {
          info = null;
          return null;
        }
        return ClousotExpression<Type>.MakeIsInst((Type)testtype, rec);
      }

      info = null;
      return null;
    }


    public static string/*?*/ ConditionAsPrecondition<Label, Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Expression, Attribute, Assembly>(
      Expression condition,
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, SymbolicValue> context,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      out bool hasVariables,
      SourceLanguage slang
    )
      where Type : IEquatable<Type>
    {
      Contract.Requires(context != null);
      Contract.Requires(mdDecoder != null);

      return ConditionAsStringAtPC(context.MethodContext.CFG.EntryAfterRequires, condition, context, mdDecoder, out hasVariables, slang);
    }

    public static string/*?*/ ConditionAsStringAtPC<Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Expression, Attribute, Assembly>(
      APC atPC,
      Expression condition,
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, SymbolicValue> context,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      out bool hasVariables,
      SourceLanguage slang
    )
      where Type : IEquatable<Type>
    {
      Contract.Requires(context != null);
      Contract.Requires(mdDecoder != null);

      var decoder =
        new ExpressionDecoder<Local, Parameter, Method, Field, Property, Event, Expression, Type, SymbolicValue, Attribute, Assembly>(atPC, context, mdDecoder);

      string result = context.ExpressionContext.Decode<InvertComparison, string, ExpressionDecoder<Local, Parameter, Method, Field, Property, Event, Expression, Type, SymbolicValue, Attribute, Assembly>>(condition, decoder, InvertComparison.NoInversion);

      hasVariables = decoder.HasVariables;
      return result;
    }
  }
}
