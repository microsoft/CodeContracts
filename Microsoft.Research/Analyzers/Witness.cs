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
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  [ContractVerification(true)]
  public class Witness
  {
    #region Object Invariant
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.Context != null);
      Contract.Invariant(this.trace != null);
    }
    #endregion

    #region State

    readonly public APC PC;
    readonly public uint? SourceProofObligationID;
    readonly public ProofOutcome Outcome;
    readonly public WarningType Warning;
    readonly public ClousotSuggestion.Kind? TypeOfSuggestionTurnedIntoWarning;
    readonly public Set<WarningContext> Context;
    readonly private List<KeyValuePair<APC, string>> trace = new List<KeyValuePair<APC, string>>();

    public IEnumerable<KeyValuePair<APC, string>> Trace
    {
      get
      {
        return this.trace.Distinct(new TraceComparer());
      }
    }

    private double? score;
    private string justification;
    private List<string> justification_expanded;

    #endregion


    public Witness(uint? sourceProofObligation, WarningType warning, ProofOutcome outcome, APC PC, Set<WarningContext> context, ClousotSuggestion.Kind? type = null)
    {
      Contract.Requires(context != null);

      this.SourceProofObligationID = sourceProofObligation;
      this.PC = PC;
      this.Outcome = outcome;
      this.Warning = warning;
      this.Context = context;
      this.TypeOfSuggestionTurnedIntoWarning = type;
    }

    public Witness(uint? sourceProofObligation, WarningType warning, ProofOutcome outcome, APC PC, IEnumerable<WarningContext> context, ClousotSuggestion.Kind? type = null)
      : this(sourceProofObligation, warning, outcome, PC, new Set<WarningContext>(context), type)
    {
      Contract.Requires(context != null);
    }
    public Witness(uint? sourceProofObligation, WarningType warning, ProofOutcome outcome, APC PC, ClousotSuggestion.Kind? type = null)
      : this(sourceProofObligation, warning, outcome, PC, new Set<WarningContext>(), type)
    {
    }

    public void AddWarningContext(WarningContext context)
    {
      this.Context.Add(context);
    }

    public void AddTrace(APC pc, string msg)
    {
      this.trace.Add(new KeyValuePair<APC, string>(pc, msg));
    }

    /// <summary>
    /// Abstraction from WarningType to WarningKind
    /// </summary>
    [ContractVerification(true)]
    public WarningKind WarningKind
    {
      get
      {
        switch (this.Warning)
        {
          case WarningType.ArithmeticDivisionByZero:
            return WarningKind.DivByZero;

          case WarningType.ArithmeticDivisionOverflow:
            return WarningKind.ArithmeticOverflow;

          case WarningType.ArithmeticMinValueNegation:
            return WarningKind.MinValueNegation;

          case WarningType.ArithmeticOverflow:
            return WarningKind.ArithmeticOverflow;

          case WarningType.ArithmeticUnderflow:
            return WarningKind.ArithmeticOverflow;

          case WarningType.ArithmeticFloatEqualityPrecisionMismatch:
            return WarningKind.FloatEqualityPrecisionMismatch;

          case WarningType.ArrayCreation:
            return WarningKind.ArrayCreation;

          case WarningType.ArrayLowerBound:
            return WarningKind.ArrayLowerBound;

          case WarningType.ArrayUpperBound:
            return WarningKind.ArrayUpperBound;

          case WarningType.ArrayPurity:
            return WarningKind.Purity;

          case WarningType.ContractAssert:
            return WarningKind.Assert;

          case WarningType.ContractEnsures:
            return WarningKind.Ensures;

          case WarningType.ContractInvariant:
            return WarningKind.Invariant;

          case WarningType.ContractAssume:
          case WarningType.MissingPreconditionInvolvingReadonly:
            return WarningKind.Assume;

          case WarningType.ContractRequires:
            return WarningKind.Requires;

          case WarningType.EnumRange:
            return WarningKind.Enum;

          case WarningType.MissingPrecondition:
            return WarningKind.MissingPrecondition;

          case WarningType.Suggestion:
            return WarningKind.Suggestion;

          case WarningType.NonnullArray:
          case WarningType.NonnullCall:
          case WarningType.NonnullField:
          case WarningType.NonnullUnbox:
            return WarningKind.Nonnull;

          case WarningType.UnsafeCreation:
          case WarningType.UnsafeLowerBound:
          case WarningType.UnsafeUpperBound:
            return WarningKind.Unsafe;

          case WarningType.UnreachedCodeAfterPrecondition:
          case WarningType.TestAlwaysEvaluatingToAConstant:
          case WarningType.FalseEnsures:
          case WarningType.FalseRequires:
          case WarningType.ClousotCacheNotAvailable:
            return WarningKind.UnreachedCode;

          // Should be unreachable
          default:
            Contract.Assert(false);
            throw new InvalidOperationException();
        }
      }
    }

    #region Comparing traces

    class TraceComparer : IEqualityComparer<KeyValuePair<APC, string>>
    {
      public bool Equals(KeyValuePair<APC, string> x, KeyValuePair<APC, string> y)
      {
        return x.Key.Equals(y.Key);
      }

      public int GetHashCode(KeyValuePair<APC, string> obj)
      {
        return obj.GetHashCode();
      }
    }

    #endregion

    #region Scoring logic
    /// <summary>
    /// Compute a score for this witness
    /// </summary>
    [Pure] // not really...
    [ContractVerification(true)]
    public double GetScore(WarningScoresManager scoresManager)
    {
      Contract.Requires(scoresManager != null);

      if (!this.score.HasValue)
      {
        EnsureScoreAndJustificationAreComputed(scoresManager);
      }

      return this.score.Value;
    }

    [Pure] // not really...
    [ContractVerification(true)]
    public string GetJustificationString(WarningScoresManager scoreManager)
    {
      Contract.Requires(scoreManager != null);

      if (this.justification == null)
      {
        EnsureScoreAndJustificationAreComputed(scoreManager);
      }

      return this.justification;
    }

    [Pure] // not really...
    [ContractVerification(true)]
    public List<string> GetJustificationList(WarningScoresManager scoreManager)
    {
      Contract.Requires(scoreManager != null);

      if (this.justification == null)
      {
        EnsureScoreAndJustificationAreComputed(scoreManager);
      }

      return this.justification_expanded;
    }

    private void EnsureScoreAndJustificationAreComputed(WarningScoresManager scoresManager)
    {
      Contract.Ensures(this.score.HasValue);
      Contract.Ensures(this.justification != null);

      var pair = scoresManager.GetScore(this);

      this.score = pair.Item1;
      this.justification_expanded = pair.Item2;
      this.justification = String.Join(" ", this.justification_expanded);
    }

    public string GetWarningLevel(double score, WarningScoresManager scoresManager)
    {
      Contract.Requires(scoresManager != null);

      if (score > scoresManager.LOWSCORE)
        return "High";

      if (score > scoresManager.MEDIUMLOWSCORE)
        return "MediumHigh";

      if (score > scoresManager.MEDIUMSCORE)
        return "Medium";

      return "Low";
    }

    #endregion

  }         
  public static class WarningContextFetcher
  {
    static public IEnumerable<WarningContext> InferContext<Local, Parameter, Method, Field, Typ, ExternalExpression, Variable>(
      APC pc,
      BoxedExpression exp,
      IExpressionContext<Local, Parameter, Method, Field, Typ, ExternalExpression, Variable> context,
      Predicate<Typ> isBooleanType,
      Predicate<Field> isReadOnly = null
      )
      where Typ : IEquatable<Typ>
    {
      Contract.Ensures(Contract.Result<IEnumerable<WarningContext>>() != null);

      var collect = new List<WarningContext>();

      InferContextInternal(pc, exp, collect, context, true, isBooleanType, isReadOnly);

      return collect;
    }

    static private void InferContextInternal<Local, Parameter, Method, Field, Typ, ExternalExpression, Variable>(
      APC pc,
      BoxedExpression exp,
      System.Collections.Generic.List<WarningContext> result,
      IExpressionContext<Local, Parameter, Method, Field, Typ, ExternalExpression, Variable> context,
      bool isFirstCall,
      Predicate<Typ> isBooleanType,
      Predicate<Field> isReadOnly = null)
      where Typ : IEquatable<Typ>
    {
      // sanity check
      if (exp == null)
      {
        return;
      }

      if (exp is QuantifiedIndexedExpression) 
      {
        result.Add(new WarningContext(WarningContext.ContextType.MayContainForAllOrADisjunction));
        return;
      }

      if (exp.IsVariable)
      {
        if (exp.UnderlyingVariable is Variable)
        {
          var v = (Variable)exp.UnderlyingVariable;

          // We do a special treatement for the first call, because we want to see if it is a boolean.
          // In this case, we may have failed proving it for two main reasons:
          //  1. it is a ForAll not decompiled
          //  2. it is a disjunction  
          if (isFirstCall)
          {
            var type = context.ValueContext.GetType(pc, v);
            if ((type.IsNormal && isBooleanType(type.Value)) 
              || type.IsTop // Sometimes we get no type with inferred preconditions
              )
            {
              result.Add(new WarningContext(WarningContext.ContextType.MayContainForAllOrADisjunction));
            }
          }

          // special case to check if the value comes from the parameters
          // With the new precondition inference, it is no more the case that we catch it
          var accessPath = context.ValueContext.AccessPathList(pc, v, false, false);
          if (accessPath != null && context.ValueContext.PathUnmodifiedSinceEntry(pc, accessPath))
          {
            result.Add(new WarningContext(WarningContext.ContextType.ViaParameterUnmodified));
          }

          var flags = context.ValueContext.PathContexts(pc, v);


          if ((flags & PathContextFlags.ViaArray) == PathContextFlags.ViaArray)
          {
            result.Add(new WarningContext(WarningContext.ContextType.ViaArray, 1));
          }
          if ((flags & PathContextFlags.ViaField) == PathContextFlags.ViaField)
          {
            var ro = 0;
            // If it is a readonly field, then we remember it              
            foreach (var f in context.ValueContext.AccessPathList(pc, (Variable)exp.UnderlyingVariable, false, false).FieldsIn<Field>())
            {
              if (isReadOnly != null && isReadOnly(f))
              {
                ro++;
              }
            }
            if (ro > 0)
            {
              result.Add(new WarningContext(WarningContext.ContextType.ContainsReadOnly, ro));
            }

            result.Add(new WarningContext(WarningContext.ContextType.ViaField, 1));
          }
          if ((flags & PathContextFlags.ViaOldValue) == PathContextFlags.ViaOldValue)
          {
            result.Add(new WarningContext(WarningContext.ContextType.ViaOldValue, 1));
          }
          if ((flags & PathContextFlags.ViaMethodReturn) == PathContextFlags.ViaMethodReturn)
          {
            result.Add(new WarningContext(WarningContext.ContextType.ViaMethodCall, 1));
          }
          if ((flags & PathContextFlags.ViaCast) == PathContextFlags.ViaCast)
          {
            result.Add(new WarningContext(WarningContext.ContextType.ViaCast));
          }
          if ((flags & PathContextFlags.ViaOutParameter) == PathContextFlags.ViaOutParameter)
          {
            var name = exp.CanPrintAsSourceCode() ? exp.ToString() : null;
            result.Add(new WarningContext(WarningContext.ContextType.ViaOutParameter, name));
          }
          if((flags & PathContextFlags.ViaPureMethodReturn) == PathContextFlags.ViaPureMethodReturn)
          {
            result.Add(new WarningContext(WarningContext.ContextType.ViaPureMethodReturn));
          }
          if (flags.HasFlag(PathContextFlags.ViaCallThisHavoc))
          {
            result.Add(new WarningContext(WarningContext.ContextType.ViaCallThisHavoc));
          }
        }

        return;
      }
      if (exp.IsUnary)
      {
        InferContextInternal(pc, exp.UnaryArgument, result, context, false, isBooleanType, isReadOnly);

        return;
      }
      if (exp.IsBinary)
      {
        if (exp.BinaryOp == BinaryOperator.LogicalOr)
        {
          result.Add(new WarningContext(WarningContext.ContextType.MayContainForAllOrADisjunction));
        }

        InferContextInternal(pc, exp.BinaryLeft, result, context, false, isBooleanType, isReadOnly);
        InferContextInternal(pc, exp.BinaryRight, result, context, false, isBooleanType, isReadOnly);

        // Special case for x != null
        if(exp.BinaryOp == BinaryOperator.Cne_Un)
        {
          bool isMethodCall;
          if(IsVarNotNull(exp.BinaryLeft, exp.BinaryRight, out isMethodCall) || IsVarNotNull(exp.BinaryRight, exp.BinaryLeft, out isMethodCall))
          {
            if (isMethodCall)
            {
              result.Add(new WarningContext(WarningContext.ContextType.IsPureMethodCallNotEqNullCheck));
            }
            else
            {
              result.Add(new WarningContext(WarningContext.ContextType.IsVarNotEqNullCheck));
            }
          }
        }

        return;
      }
      object dummy;
      BoxedExpression inner;
      if (exp.IsIsInstExpression(out inner, out dummy))
      {
        result.Add(new WarningContext(WarningContext.ContextType.ContainsIsInst));

        InferContextInternal(pc, inner, result, context, false, isBooleanType, isReadOnly);

        return;
      }
    }

    private static bool IsVarNotNull(BoxedExpression left, BoxedExpression right, out bool IsMethodCall)
    {
      IsMethodCall = false;
      if(left == null || right == null)
      {
        return false;
      }

      if(right.IsNull && (left.IsVariable && left.AccessPath != null && left.AccessPath.Length == 2))
      {
        IsMethodCall = (left.AccessPath[0].IsMethodCall) || (left.AccessPath[1].IsMethodCall);

        return true;
      }

      return  false;
    }
  }
}
