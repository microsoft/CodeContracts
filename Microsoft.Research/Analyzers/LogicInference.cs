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
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains.Numerical;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.Research.CodeAnalysis
{

  public class FactQueryStatistics 
  {
    [ThreadStatic]
    static protected ulong cachehits = 0;
    [ThreadStatic]
    static protected ulong cachemisses = 0;

    public static string Statistics
    {
      get
      {
        var total = cachehits + cachemisses;
        var p = total != 0 ? cachehits / (double)total : 0;
        return string.Format("{0} questions, {1} found in cache ({2,6:P1}), {3} missed", total, cachehits, p, cachemisses); 
      }
    }
  }

  [ContractVerification(true)]
  public class FactQueryWithMemory<Variable> : FactQueryStatistics, IFactQuery<BoxedExpression, Variable>
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.inner != null);
      Contract.Invariant(this.nullCache != null);
      Contract.Invariant(this.nonNullCache != null);
      Contract.Invariant(this.trueCache != null);
      Contract.Invariant(this.geqZeroCache != null);
      Contract.Invariant(this.isLessThanCache != null);
      Contract.Invariant(this.isNonZero != null);
      Contract.Invariant(this.nullVarCache != null);
      Contract.Invariant(this.nonNullCache != null);
      Contract.Invariant(this.isNonZero != null);
      Contract.Invariant(this.unreachableCache != null);
      Contract.Invariant(this.nonNullVarCache != null);
    }
   
    private readonly IFactQuery<BoxedExpression, Variable> inner;

    private readonly ICache<Pair<APC, BoxedExpression>, ProofOutcome> nullCache;
    private readonly ICache<Pair<APC, BoxedExpression>, ProofOutcome> nonNullCache;
    private readonly ICache<Pair<APC, BoxedExpression>, ProofOutcome> trueCache;
    private readonly ICache<Pair<APC, BoxedExpression>, ProofOutcome> geqZeroCache;
    private readonly ICache<Tuple<APC, BoxedExpression, BoxedExpression>, ProofOutcome> isLessThanCache;
    private readonly ICache<Pair<APC, BoxedExpression>, ProofOutcome> isNonZero;

    private readonly ICache<Pair<APC, Variable>, ProofOutcome> nullVarCache;
    private readonly ICache<Pair<APC, Variable>, ProofOutcome> nonNullVarCache;
    private readonly ICache<APC, bool> unreachableCache;


    public FactQueryWithMemory(IFactQuery<BoxedExpression, Variable> inner)
    {
      Contract.Requires(inner != null);

      this.inner = inner;

      this.nullCache = new FIFOCache<Pair<APC, BoxedExpression>, ProofOutcome>();
      this.nonNullCache = new FIFOCache<Pair<APC, BoxedExpression>, ProofOutcome>();
      this.trueCache = new FIFOCache<Pair<APC, BoxedExpression>, ProofOutcome>();
      this.geqZeroCache = new FIFOCache<Pair<APC, BoxedExpression>, ProofOutcome>();
      this.isLessThanCache = new FIFOCache<Tuple<APC, BoxedExpression, BoxedExpression>, ProofOutcome>();
      this.isNonZero = new FIFOCache<Pair<APC, BoxedExpression>, ProofOutcome>();
      this.nullVarCache = new FIFOCache<Pair<APC, Variable>, ProofOutcome>();
      this.nonNullVarCache = new FIFOCache<Pair<APC, Variable>, ProofOutcome>();
      this.unreachableCache = new FIFOCache<APC, bool>();
    }

    public ProofOutcome IsNull(APC pc, BoxedExpression expr)
    {
      ProofOutcome outcome;
      var key = new Pair<APC, BoxedExpression>(pc, expr);
      if (!this.nullCache.TryGetValue(key, out outcome))
      {
        cachemisses++;
        outcome = this.inner.IsNull(pc, expr);
        this.nullCache.Add(key, outcome); 
      }

      return outcome;
    }

    public ProofOutcome IsNonNull(APC pc, BoxedExpression expr)
    {
      ProofOutcome outcome;
      var key = new Pair<APC, BoxedExpression>(pc, expr);
      if (!this.nonNullCache.TryGetValue(key, out outcome))
      {
        cachemisses++;

        outcome = this.inner.IsNonNull(pc, expr);
        this.nonNullCache.Add(key, outcome);
      }
      else
      {
        cachehits++;
      }

      return outcome;
    }

    public ProofOutcome IsTrue(APC pc, BoxedExpression condition)
    {
      ProofOutcome outcome;
      var key = new Pair<APC, BoxedExpression>(pc, condition);
      if (!this.trueCache.TryGetValue(key, out outcome))
      {
        cachemisses++;

        outcome = this.inner.IsTrue(pc, condition);
        this.trueCache.Add(key, outcome);
      }
      else
      {
        cachehits++;
      }
      return outcome;
    }

    public ProofOutcome IsTrue(APC pc, Question question)
    {
      return this.inner.IsTrue(pc, question);
    }

    public ProofOutcome IsTrueImply(APC pc, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal)
    {
      return this.inner.IsTrueImply(pc, posAssumptions, negAssumptions, goal);
    }

    public ProofOutcome IsGreaterEqualToZero(APC pc, BoxedExpression expr)
    {
      ProofOutcome outcome;
      var key = new Pair<APC, BoxedExpression>(pc, expr);
      if (!this.geqZeroCache.TryGetValue(key, out outcome))
      {
        cachemisses++;

        outcome = this.inner.IsGreaterEqualToZero(pc, expr);
        this.geqZeroCache.Add(key, outcome);
      }
      else
      {
        cachehits++;
      }
      return outcome;
    }

    public ProofOutcome IsLessThan(APC pc, BoxedExpression left, BoxedExpression right)
    {
      ProofOutcome outcome;
      var key = new Tuple<APC, BoxedExpression, BoxedExpression>(pc, left, right);
      if (!this.isLessThanCache.TryGetValue(key, out outcome))
      {
        cachemisses++;

        outcome = this.inner.IsLessThan(pc, left, right);
        this.isLessThanCache.Add(key, outcome);
      }
      else
      {
        cachehits++;
      }
      return outcome;
    }

    public ProofOutcome IsNonZero(APC pc, BoxedExpression condition)
    {
      ProofOutcome outcome;
      var key = new Pair<APC, BoxedExpression>(pc, condition);
      if (!this.isNonZero.TryGetValue(key, out outcome))
      {
        cachemisses++;

        outcome = this.inner.IsNonZero(pc, condition);
        this.isNonZero.Add(key, outcome);
      }
      else
      {
        cachehits++;
      }
      return outcome;
    }

    public ProofOutcome HaveSameFloatType(APC pc, BoxedExpression left, BoxedExpression right)
    {
      return this.inner.HaveSameFloatType(pc, left, right);
    }

    public bool TryGetFloatType(APC pc, BoxedExpression exp, out ConcreteFloat type)
    {
      return this.inner.TryGetFloatType(pc, exp, out type);
    }

    public IEnumerable<Variable> LowerBounds(APC pc, BoxedExpression exp, bool strict)
    {
      return this.inner.LowerBounds(pc, exp, strict); ;
    }

    public IEnumerable<Variable> UpperBounds(APC pc, BoxedExpression exp, bool strict)
    {
      return this.inner.UpperBounds(pc, exp, strict); ;
    }

    public IEnumerable<BoxedExpression> LowerBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
    {
      return this.inner.LowerBoundsAsExpressions(pc, exp, strict); ;
    }

    public IEnumerable<BoxedExpression> UpperBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
    {
      return this.inner.UpperBoundsAsExpressions(pc, exp, strict); ;
    }

    public Pair<long, long> BoundsFor(APC pc, BoxedExpression exp)
    {
      return this.inner.BoundsFor(pc, exp);
    }

    public ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object type)
    {
      return this.inner.IsVariableDefinedForType(pc, v, type);
    }

    public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filterVariables = null, bool replaceVarsWithAccessPaths = true)
    {
      return this.inner.InvariantAt(pc, filterVariables, replaceVarsWithAccessPaths);
    }

    public ProofOutcome IsNull(APC pc, Variable value)
    {
      ProofOutcome outcome;
      var key = new Pair<APC, Variable>(pc, value);
      if (!this.nullVarCache.TryGetValue(key, out outcome))
      {
        cachemisses++;

        outcome = this.inner.IsNull(pc, value);
        this.nullVarCache.Add(key, outcome);
      }
      else
      {
        cachehits++;
      }
      return outcome;
    }

    public ProofOutcome IsNonNull(APC pc, Variable value)
    {
      ProofOutcome outcome;
      var key = new Pair<APC, Variable>(pc, value);
      if (!this.nonNullVarCache.TryGetValue(key, out outcome))
      {
        cachemisses++;

        outcome = this.inner.IsNonNull(pc, value);
        this.nonNullVarCache.Add(key, outcome);
      }
      else
      {
        cachehits++;
      }
      return outcome;
    }

    public bool IsUnreachable(APC pc)
    {
      bool outcome;
      if (!this.unreachableCache.TryGetValue(pc, out outcome))
      {
        cachemisses++;

        outcome = this.inner.IsUnreachable(pc);
        this.unreachableCache.Add(pc, outcome);
      }
      else
      {
        cachehits++;
      }
      return outcome;
    }
  }

  public class BasicFacts<Local, Parameter, Method, Field, Type, Expression, Variable>
    : IFactQuery<BoxedExpression, Variable>
    where Type : IEquatable<Type>
  {
    protected IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context;
    protected IFactBase<Variable> factBase;
    protected Predicate<APC> isUnreachable;

    public BasicFacts(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context, IFactBase<Variable> factBase, Predicate<APC> isUnreachable)
    {
      Contract.Requires(context != null);
      Contract.Requires(factBase != null);
      Contract.Requires(isUnreachable != null);

      this.context = context;
      this.factBase = factBase;
      this.isUnreachable = isUnreachable;
    }

    #region IFactQuery<Expression> Members

    protected static bool AsVariable(BoxedExpression expr, out Variable v)
    {
      object variable = expr.UnderlyingVariable;
      if (variable is Variable)
      {
        v = (Variable)variable;
        return true;
      }
      v = default(Variable);
      return false;
    }

    public virtual ProofOutcome IsNull(APC pc, BoxedExpression expr)
    {
      ProofOutcome result;
      Variable v;
      if (AsVariable(expr, out v))
      {
        result = this.factBase.IsNull(pc, v);
        if (result != ProofOutcome.Top) return result;
      }
      return ProofOutcome.Top;
    }

    public virtual ProofOutcome IsNonNull(APC pc, BoxedExpression expr)
    {
      ProofOutcome result;
      Variable v;
      if (AsVariable(expr, out v))
      {
        result = this.factBase.IsNonNull(pc, v);
        if (result != ProofOutcome.Top) return result;
      }
      return ProofOutcome.Top;
    }

    
    #endregion

    public virtual ProofOutcome IsNull(APC pc, Variable value)
    {
      return this.factBase.IsNull(pc, value);
    }

    public virtual ProofOutcome IsNonNull(APC pc, Variable value)
    {
      return this.factBase.IsNonNull(pc, value);
    }

    #region IFactQuery<Label,Expression,Variable> Members


    public ProofOutcome IsGreaterEqualToZero(APC pc, BoxedExpression expr)
    {
      return ProofOutcome.Top;
    }

    public ProofOutcome IsLessThan(APC pc, BoxedExpression left, BoxedExpression right)
    {
      return ProofOutcome.Top;
    }

    public ProofOutcome IsNonZero(APC pc, BoxedExpression condition)
    {
      return this.IsNonNull(pc, condition);
    }

    public ProofOutcome IsTrue(APC pc, BoxedExpression condition)
    {
      return IsNonZero(pc, condition);
    }

    public ProofOutcome IsTrue(APC pc, Question question)
    {
      return ProofOutcome.Top;
    }

    public ProofOutcome IsTrueImply(APC pc, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal)
    {
      var goalOutcome = IsTrue(pc, goal);

      // This is because we may have false ==> false, which is true, but as we do not check for the antecedents here, we cannot just say false

      if (goalOutcome == ProofOutcome.True || goalOutcome == ProofOutcome.Bottom)
      {
        return goalOutcome;
      }

      return ProofOutcome.Top;
    }

    public ProofOutcome HaveSameFloatType(APC pc, BoxedExpression left, BoxedExpression right)
    {
      return ProofOutcome.Top;
    }

    public bool TryGetFloatType(APC pc, BoxedExpression exp, out ConcreteFloat type)
    {
      type = default(ConcreteFloat);
      return false;
    }

    public IEnumerable<Variable> LowerBounds(APC pc, BoxedExpression exp, bool strict)
    {
      yield break;
    }

    public IEnumerable<Variable> UpperBounds(APC pc, BoxedExpression exp, bool strict)
    {
      yield break;
    }

    public IEnumerable<BoxedExpression> LowerBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
    {
      yield break;
    }

    public IEnumerable<BoxedExpression> UpperBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
    {
      yield break;
    }

    public Pair<long, long> BoundsFor(APC pc, BoxedExpression exp)
    {
      return new Pair<long, long>(Int64.MinValue, Int64.MaxValue);
    }

    public ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object y)
    {
      return ProofOutcome.Top;
    }

    public bool IsUnreachable(APC pc)
    {
      if (this.isUnreachable != null && this.isUnreachable(pc))
      {
        return true;
      }
      return this.factBase.IsUnreachable(pc);
    }

    virtual public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filterVariables, bool replaceVarsWithAccessPaths=true)
    {
      return FList<BoxedExpression>.Empty;
    }

    #endregion

  }

  public class SimpleLogicInference<Local, Parameter, Method, Field, Type, Expression, Variable> 
    : BasicFacts<Local,Parameter,Method,Field,Type,Expression,Variable>
    , IFactQuery<BoxedExpression, Variable>
    where Type : IEquatable<Type>
  {
    readonly Func<BoxedExpression, BoxedExpression> MakeNotNull;
    readonly Func<BoxedExpression, BoxedExpression> MakeNull;
    readonly Predicate<Type> isReferenceType;

    public SimpleLogicInference(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context, 
                                IFactBase<Variable> factBase, 
                                Func<BoxedExpression, BoxedExpression> MakeNotNull, Func<BoxedExpression, BoxedExpression> MakeNull,
                                Predicate<APC> isUnreachable, Predicate<Type> isReferenceType
      )
      : base(context, factBase, isUnreachable)
    {
      this.MakeNotNull = MakeNotNull;
      this.MakeNull = MakeNull;
      this.isReferenceType = isReferenceType;
    }

    #region IFactQuery<Expression> Members

    public override ProofOutcome IsNull(APC pc, BoxedExpression expr)
    {
      ProofOutcome result;
      Variable v;
      if (AsVariable(expr, out v))
      {
        result = this.factBase.IsNull(pc, v);
        if (result != ProofOutcome.Top) return result;
      }
      if (expr.IsConstant)
      {
        object c = expr.Constant;
        if (c == null)
          return ProofOutcome.True;
        if (c is string) return ProofOutcome.False;
        IConvertible iic = c as IConvertible;
        try
        {
          if (iic != null)
          {
            Int64 i = iic.ToInt64(null);
            if (i == 0) return ProofOutcome.True;
            return ProofOutcome.False;
          }
        }
        catch
        {
          return ProofOutcome.Top;
        }
      }
      // simplify ceq cne
      BoxedExpression left, right;
      BinaryOperator op;
      if (expr.IsBinaryExpression(out op, out left, out right))
      {
        if (IsBooleanComparison(op, left, right))
        {
          return op == BinaryOperator.Ceq ?
            this.IsNull(pc, left) :
            this.IsNonNull(pc, left);
        }
        if ((op == BinaryOperator.Ceq || op == BinaryOperator.Cobjeq) && this.IsNull(pc, right) == ProofOutcome.True)
        {
          return this.IsNonNull(pc, left);
        }
        if (op == BinaryOperator.Cne_Un && this.IsNull(pc, right) == ProofOutcome.True)
        {
          return this.IsNull(pc, left);
        }
      }
      return ProofOutcome.Top;
    }

    public override ProofOutcome IsNonNull(APC pc, BoxedExpression expr)
    {
      ProofOutcome result;
      Variable v;
      if (AsVariable(expr, out v))
      {
        result = this.factBase.IsNonNull(pc, v);
        if (result != ProofOutcome.Top) return result;
      }
      if (expr.IsConstant)
      {
        object c = expr.Constant;
        if (c == null) return ProofOutcome.False;
        if (c is string)
        {
          return ProofOutcome.True;
        }
        IConvertible iic = c as IConvertible;
        if (iic != null)
        {
          try
          {
            int i = iic.ToInt32(null);
            if (i == 0) return ProofOutcome.False;
            return ProofOutcome.True;
          }
          catch (OverflowException)
          {
            // We where unable to convert it to an int, so let's just return Top
            return ProofOutcome.Top;
          }
        }
      }
      // simplify ceq cne
      BoxedExpression left, right;
      BinaryOperator op;
      if (expr.IsBinaryExpression(out op, out left, out right))
      {
        if (IsBooleanComparison(op, left, right))
        {
          return op == BinaryOperator.Ceq ?
            this.IsNonNull(pc, left) :
            this.IsNull(pc, left);
        }
        if ((op == BinaryOperator.Ceq || op == BinaryOperator.Cobjeq) )
        {
          if (this.IsNull(pc, right) == ProofOutcome.True)
          {
            return this.IsNull(pc, left);
          }
          else if (            // (a relOp b) == (c relOp d)
            (left.IsBinary && left.BinaryOp.IsComparisonBinaryOperator()) &&
            (right.IsBinary && right.BinaryOp.IsComparisonBinaryOperator()))
          {

            var leftResult = this.IsTrue(pc, left);
            if (leftResult.IsNormal())
            {
              var rightResult = this.IsTrue(pc, right);
              if (rightResult.IsNormal())
              {
                if (leftResult == rightResult)
                {
                  return ProofOutcome.True;
                }
                else
                {
                  return ProofOutcome.False;
                }
              }
            }
          }
        }
        if (
          (op == BinaryOperator.Cgt_Un || op == BinaryOperator.Cne_Un) 
          && this.IsNull(pc, right) == ProofOutcome.True)
        {
          return this.IsNonNull(pc, left);
        }
      }
      return ProofOutcome.Top;
    }

    public override FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> variables, bool replaceVarsWithAccessPaths = true)
    {
      var result = FList<BoxedExpression>.Empty;

      if (this.MakeNotNull != null && this.MakeNull != null)
      {
        if (variables != null)
        {
          foreach (var x in variables.GetEnumerable())
          {
            var t = this.context.ValueContext.GetType(pc, x);

            if (t.IsNormal && this.isReferenceType(t.Value))
            {
              var accessPath = this.context.ValueContext.AccessPathList(pc, x, true, true);
              if (accessPath != null || 
                !replaceVarsWithAccessPaths // We allow symbolic variables without a source name
                )
              {
                if (this.IsNonNull(pc, x) == ProofOutcome.True)
                {
                  result = result.Cons(this.MakeNotNull(BoxedExpression.Var(x, accessPath)));
                }
                if (this.IsNull(pc, x) == ProofOutcome.True)
                {
                  result = result.Cons(this.MakeNull(BoxedExpression.Var(x, accessPath)));
                }
              }
            }
              // This is the case when we have for instance foo(null)
            else if (!replaceVarsWithAccessPaths && this.IsNull(pc, x) == ProofOutcome.True)
            {
              result = result.Cons(this.MakeNull(BoxedExpression.Var(x)));
            }
          }
        }
        else
        {
          return this.factBase.InvariantAt(pc, variables, replaceVarsWithAccessPaths);
        }
      }

      return result;
    }

    #endregion

    #region Private helper methods
    
    [Pure]
    static private bool IsBooleanComparison(BinaryOperator bop, BoxedExpression left, BoxedExpression right)
    {
      int k;
      if ((bop == BinaryOperator.Ceq || bop == BinaryOperator.Cne_Un) && right.IsConstantInt(out k) && k != 0)
      {
        BoxedExpression dummy1, dummy2;
        return left.IsBinaryExpression(out bop, out dummy1, out dummy2) && bop.IsComparisonBinaryOperator();
      }

      return false;
    }

    #endregion
  }

  public class ConstantPropagationFactQuery<Variable> : IFactQuery<BoxedExpression, Variable>
  {
    #region IFactQuery<BoxedExpression,Variable> Members

    public ProofOutcome IsNull(APC pc, BoxedExpression expr)
    {
      int value;
      if (expr.IsConstantIntOrNull(out value))
      {
        return IsTrue(value == 0);
      }

      return ProofOutcome.Top;
    }

    public ProofOutcome IsNonNull(APC pc, BoxedExpression expr)
    {
      int value;
      if (expr.IsConstantIntOrNull(out value))
      {
        return IsTrue(value != 0);
      }

      return ProofOutcome.Top;
    }

    public ProofOutcome IsTrue(APC pc, BoxedExpression condition)
    {
      int value;
      if (condition.IsConstantIntOrNull(out value))
      {
        // We do not return false when value == 0 because we want to force some other analysis to prove that it is unreachable
        return value != 0 ? ProofOutcome.True : ProofOutcome.Top;
      }

      return ConstantFact(condition);
    }

    public ProofOutcome IsTrue(APC pc, Question question)
    {
      return ProofOutcome.Top;
    }

    public ProofOutcome IsTrueImply(APC pc, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal)
    {
      UnaryOperator op;
      BoxedExpression embedded;
      if (goal.IsUnaryExpression(out op, out embedded) && op.IsConversionOperator())
      {
        goal = embedded;
      }

      foreach (var assumption in posAssumptions.GetEnumerable<BoxedExpression>())
      {
        if (assumption.Equals(goal))
          return ProofOutcome.True;
      }

      return ProofOutcome.Top;
    }

    public ProofOutcome IsGreaterEqualToZero(APC pc, BoxedExpression expr)
    {
      int value;
      if (expr.IsConstantIntOrNull(out value))
      {
        return IsTrue(value >= 0);
      }

      return ProofOutcome.Top;
    }

    public ProofOutcome IsLessThan(APC pc, BoxedExpression left, BoxedExpression right)
    {
      int valueLeft, valueRight;
      if (left.IsConstantIntOrNull(out valueLeft) && right.IsConstantIntOrNull(out valueRight))
      {
        return IsTrue(valueLeft < valueRight);
      }

      return ProofOutcome.Top;
    }

    public ProofOutcome IsNonZero(APC pc, BoxedExpression condition)
    {
      int value;
      if (condition.IsConstantIntOrNull(out value))
      {
        return IsTrue(value != 0);
      }

      return ProofOutcome.Top;
    }

    public ProofOutcome HaveSameFloatType(APC pc, BoxedExpression left, BoxedExpression right)
    {
      return ProofOutcome.Top;
    }

    public bool TryGetFloatType(APC pc, BoxedExpression exp, out ConcreteFloat type)
    {
      type = default(ConcreteFloat);
      return false;
    }

    public IEnumerable<Variable> LowerBounds(APC pc, BoxedExpression exp, bool strict)
    {
      yield break;
    }

    public IEnumerable<Variable> UpperBounds(APC pc, BoxedExpression exp, bool strict)
    {
      yield break;
    }

    public IEnumerable<BoxedExpression> LowerBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
    {
      yield break;
    }

    public IEnumerable<BoxedExpression> UpperBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
    {
      yield break;
    }

    public Pair<long, long> BoundsFor(APC pc, BoxedExpression exp)
    {
      return new Pair<long, long>(Int64.MinValue, Int64.MaxValue);
    }

    public ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object y)
    {
      return ProofOutcome.Top;
    }

    public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filterVariables, bool replaceVarsWithAccessPaths = true)
    {
      return FList<BoxedExpression>.Empty;
    }

    #endregion

    #region IFactBase<Variable> Members

    public ProofOutcome IsNull(APC pc, Variable value)
    {
      return ProofOutcome.Top;
    }

    public ProofOutcome IsNonNull(APC pc, Variable value)
    {
      return ProofOutcome.Top;
    }

    public bool IsUnreachable(APC pc)
    {
      return false;
    }

    #endregion

    #region Private Helpers Method

    private ProofOutcome ConstantFact(BoxedExpression be)
    {
      BinaryOperator bop;
      BoxedExpression left, right;
      int leftValue, rightValue;
      string leftString, rightString;

      if (be.IsBinaryExpression(out bop, out left, out right))
      {
        var leftConstInt = left.IsConstantIntOrNull(out leftValue);
        var rightConstInt = right.IsConstantIntOrNull(out rightValue);

        if (leftConstInt || rightConstInt)
        {
          #region The cases
          // Both are constants
          if (leftConstInt && rightConstInt)
          {
            #region all the cases
            switch (bop)
            {
              case BinaryOperator.Add:
                return IsTrue(leftValue + rightValue != 0);

              case BinaryOperator.And:
                return IsTrue((leftValue & rightValue) != 0);

              case BinaryOperator.Ceq:
                return IsTrue(leftValue == rightValue);

              case BinaryOperator.Cge:
                return IsTrue(leftValue >= rightValue);

              case BinaryOperator.Cge_Un:
                return IsTrue(((uint)leftValue >= ((uint)rightValue)));

              case BinaryOperator.Cgt:
                return IsTrue(leftValue > rightValue);

              case BinaryOperator.Cgt_Un:
                return IsTrue(((uint)leftValue > ((uint)rightValue)));

              case BinaryOperator.Cle:
                return IsTrue(leftValue <= rightValue);

              case BinaryOperator.Cle_Un:
                return IsTrue(((uint)leftValue <= ((uint)rightValue)));

              case BinaryOperator.Clt:
                return IsTrue(leftValue < rightValue);

              case BinaryOperator.Clt_Un:
                return IsTrue(((uint)leftValue < ((uint)rightValue)));

              case BinaryOperator.Cne_Un:
                return IsTrue(((uint)leftValue != ((uint)rightValue)));

              case BinaryOperator.Cobjeq:
                return ProofOutcome.Top;

              case BinaryOperator.Div:
                return IsTrue(rightValue != 0 && leftValue / rightValue != 0);

              case BinaryOperator.LogicalAnd:
                return IsTrue(leftValue != 0 && rightValue != 0);

              case BinaryOperator.LogicalOr:
                return IsTrue(leftValue != 0 || rightValue != 0);

              case BinaryOperator.Mul:
                return IsTrue(leftValue * rightValue != 0);

              case BinaryOperator.Or:
                return IsTrue((leftValue | rightValue) != 0);

              case BinaryOperator.Rem:
                return IsTrue(rightValue != 0 && leftValue % rightValue != 0);

              case BinaryOperator.Shl:
                return IsTrue(leftValue << rightValue != 0);

              case BinaryOperator.Shr:
                return IsTrue(leftValue >> rightValue != 0);

              case BinaryOperator.Sub:
                return IsTrue(leftValue - rightValue != 0);

              case BinaryOperator.Xor:
                return IsTrue((leftValue ^ rightValue) != 0);
            }
            #endregion
          }

          // Look for negation
          if (bop == BinaryOperator.Ceq)
          {
            if (rightConstInt && rightValue == 0)
            {
              return ConstantFact(left).Negate();
            }
            if (leftConstInt && leftValue == 0)
            {
              return ConstantFact(right).Negate();
            }
          }
          #endregion
        }
          // We look for other constants
        else if(left.IsConstant && right.IsConstant)
        {          
          var leftConst = left.Constant;
          var rightConst = right.Constant;
          switch (bop)
          {
              // Note Ceq does not work, because we have to make sure that they are not floats
            case BinaryOperator.Cobjeq:
              return leftConst != null ? IsTrue(leftConst.Equals(rightConst)) : IsTrue(rightConst == null);

            case BinaryOperator.Cne_Un:
              return leftConst != null ? IsTrue(!leftConst.Equals(rightConst)) : IsTrue(rightConst != null);
          }
        }
          // Special case for strings!!!
        else if (left.IsConstantString(out leftString) && right.IsConstantString(out rightString))
        {
          switch (bop)
          {
            // Note Ceq does not work, because we have to make sure that they are not floats
            case BinaryOperator.Cobjeq:
              return leftString != null ? IsTrue(leftString.Equals(rightString)) : IsTrue(rightString == null);

            case BinaryOperator.Cne_Un:
              return rightString != null ? IsTrue(!leftString.Equals(rightString)) : IsTrue(rightString != null);
          }
        }
      }

      return ProofOutcome.Top;
    }

    /// <summary>
    /// Returns ProofOutcome.True iff b.
    /// We do not check for False, as we may want other analyses to refine it to bottom
    /// </summary>
    private ProofOutcome IsTrue(bool b)
    {
      return b ? ProofOutcome.True : ProofOutcome.Top;
    }
    #endregion
  }

  public class ComposedFactQuery<Variable> :  IFactQuery<BoxedExpression, Variable>,
    IEnumerable<IFactQuery<BoxedExpression, Variable>>
  {
    Predicate<APC> isUnreachable;
    List<IFactQuery<BoxedExpression, Variable>> elements = new List<IFactQuery<BoxedExpression, Variable>>();

    public ComposedFactQuery(Predicate<APC> isUnreachable)
    {
      this.isUnreachable = isUnreachable;
    }

    /// <param name="elem"> can be null to mean no fact query at all</param>
    public void Add(IFactQuery<BoxedExpression, Variable> elem)
    {
      if (elem != null)
      {
        this.elements.Add(elem);
      }
    }

    #region IFactQuery<BoxedExpression,Variable> Members

    public ProofOutcome IsNull(APC pc, BoxedExpression expr)
    {
      
      var r = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        var outcome = query.IsNull(pc, expr);

        if (outcome != ProofOutcome.Top)
          return outcome;
      }
      return r;
    }

    public ProofOutcome IsNonNull(APC pc, BoxedExpression expr)
    {
      var r = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        var outcome = query.IsNonNull(pc, expr);

        if (outcome != ProofOutcome.Top)
          return outcome;
      }
      return r;
    }

    public ProofOutcome IsTrue(APC pc, BoxedExpression condition)
    {
      if (condition.IsBinary && condition.BinaryOp == BinaryOperator.LogicalOr)
      {
        foreach (var disjunct in condition.SplitDisjunctions())
        {
          var disOutcome = this.IsTrue(pc, disjunct);
          switch (disOutcome)
          {
            case ProofOutcome.Bottom:
            case ProofOutcome.True:
              return disOutcome;

            case ProofOutcome.False:
            case ProofOutcome.Top:
            default:
              //do nothing;
              break;
          }
        }
      }

      var r = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        var outcome = query.IsTrue(pc, condition);

        switch (outcome)
        {
          case ProofOutcome.True:
          case ProofOutcome.Bottom:
            return outcome;

          case ProofOutcome.False:
            // If it is False, we continue asking to see if the assertion is unreachable
            r = outcome;
            break;

          case ProofOutcome.Top:
            // do nothing
            break;
        }

      }
      if (r != ProofOutcome.Top)
      {
        return r;
      }

      // simplify ceq cne
      BoxedExpression left, right;
      BinaryOperator op;
      if (condition.IsBinaryExpression(out op, out left, out right))
      {
        if ((op == BinaryOperator.Ceq || op == BinaryOperator.Cobjeq) && left.IsRelational() && this.IsNull(pc, right) == ProofOutcome.True)
        {
          ProofOutcome oc = this.IsTrue(pc, left);
          // negate definite outcomes
          if (oc == ProofOutcome.True) return ProofOutcome.False;
          if (oc == ProofOutcome.False) return ProofOutcome.True;
          return oc; // top and bottom are the same
        }
        else if (op == BinaryOperator.Ceq)
        {
          int lcint,rcint;
          if (left.IsConstantIntOrNull(out lcint) && right.IsConstantIntOrNull(out rcint))
          {
            if (lcint != rcint)
            {
              return ProofOutcome.False;
            }
            else
            {
              return ProofOutcome.True;
            }
          }
        }
      }
      if (condition.IsUnary && condition.UnaryOp == UnaryOperator.Not)
      {
        ProofOutcome oc = this.IsTrue(pc, condition.UnaryArgument);
        // negate definite outcomes
        if (oc == ProofOutcome.True) return ProofOutcome.False;
        if (oc == ProofOutcome.False) return ProofOutcome.True;
        return oc; // top and bottom are the same
      }

      return ProofOutcome.Top;
    }

    public ProofOutcome IsTrue(APC pc, Question question)
    {
      ProofOutcome result = ProofOutcome.Top;
      foreach (var el in this.elements)
      {
        result = el.IsTrue(pc, question).Meet(result);

        switch (result)
        {
          case ProofOutcome.True:
          case ProofOutcome.Bottom:
            {
              return result;
            }

          case ProofOutcome.False:
          case ProofOutcome.Top:
            {
              continue;
            }
        }
      }

      return result;
    }

    public ProofOutcome IsTrueImply(APC pc, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal)
    {
      var r = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        var outcome = query.IsTrueImply(pc, posAssumptions, negAssumptions, goal);

        if (outcome != ProofOutcome.Top)
          return outcome;
      }

      return r;
    }

    public ProofOutcome IsGreaterEqualToZero(APC pc, BoxedExpression expr)
    {
      var r = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        var outcome = query.IsGreaterEqualToZero(pc, expr);

        if (outcome != ProofOutcome.Top)
        {
          return outcome;
        }
      }
      return r;
    }

    public ProofOutcome IsLessThan(APC pc, BoxedExpression left, BoxedExpression right)
    {
      var r = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        var outcome = query.IsLessThan(pc, left, right);

        if (outcome != ProofOutcome.Top)
        {
          return outcome;
        }
      }
      return r;
    }

    public ProofOutcome IsNonZero(APC pc, BoxedExpression condition)
    {
      var r = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        var outcome = query.IsNonZero(pc, condition);

        if (outcome != ProofOutcome.Top)
        {
          return outcome;
        }
      }
      return r;
    }

    public ProofOutcome HaveSameFloatType(APC pc, BoxedExpression left, BoxedExpression right)
    {
      var r = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        var outcome = query.HaveSameFloatType(pc, left, right);

        if (outcome != ProofOutcome.Top)
        {
          return outcome;
        }
      }
      return r;
    }

    public bool TryGetFloatType(APC pc, BoxedExpression exp, out ConcreteFloat type)
    {
      type = default(ConcreteFloat);
      foreach (var query in this.elements)
      {
        if (query.TryGetFloatType(pc, exp, out type))
        {
          return true;
        }
      }

      return false;
    }


    public bool IsUnreachable(APC pc)
    {
      if (this.isUnreachable != null && this.isUnreachable(pc))
      {
        return true;
      }
      foreach (var query in elements)
      {
        if (query.IsUnreachable(pc))
        {
          return true;
        }
      }
      return false;
    }

    public IEnumerable<Variable> LowerBounds(APC pc, BoxedExpression exp, bool strict)
    {
      foreach (var query in elements)
      {
        foreach (var lb in query.LowerBounds(pc, exp, strict))
        {
          yield return lb;
        }
      }

    }

    public IEnumerable<Variable> UpperBounds(APC pc, BoxedExpression exp, bool strict)
    {
      foreach (var query in elements)
      {
        foreach (var ub in query.UpperBounds(pc, exp, strict))
        {
          yield return ub;
        }
      }
    }

    public IEnumerable<BoxedExpression> LowerBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
    {
      foreach (var query in elements)
      {
        foreach (var lb in query.LowerBoundsAsExpressions(pc, exp, strict))
        {
          yield return lb;
        }
      }

    }

    public IEnumerable<BoxedExpression> UpperBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
    {
      foreach (var query in elements)
      {
        foreach (var ub in query.UpperBoundsAsExpressions(pc, exp, strict))
        {
          yield return ub;
        }
      }
    }

    public Pair<long, long> BoundsFor(APC pc, BoxedExpression exp)
    {
      Pair<long, long>? result = null;
      foreach (var query in this.elements)
      {
        var p = query.BoundsFor(pc, exp);
        if (!result.HasValue)
        {
          result = p;
        }
        else
        {
          result = new Pair<long, long>(Math.Max(result.Value.One, p.One), Math.Min(result.Value.Two, p.Two));
        }
      }

      return result.HasValue? result.Value : new Pair<long, long>(Int64.MinValue, Int64.MaxValue);
    }

    public ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object y)
    {
      foreach (var query in this.elements)
      {
        var outcome = query.IsVariableDefinedForType(pc, v, y);
        if (outcome != ProofOutcome.Top)
        {
          return outcome;
        }
      }

      return ProofOutcome.Top;
    }

    public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filterVariables, bool replaceVarsWithAccessPaths = true)
    {
      var result = FList<BoxedExpression>.Empty;
      foreach (var query in this.elements)
      {
        result = result.Append(query.InvariantAt(pc, filterVariables, replaceVarsWithAccessPaths));
      }

      return result;
    }

    #endregion

    #region IFactBase<Variable> Members

    public ProofOutcome IsNull(APC pc, Variable value)
    {
      var r = ProofOutcome.Top;
      foreach (var f in this.elements)
      {
        var outcome = f.IsNull(pc, value);

        if (outcome != ProofOutcome.Top)
        {
          return outcome;
        }
      }
      return r;
    }

    public ProofOutcome IsNonNull(APC pc, Variable value)
    {
      var r = ProofOutcome.Top;
      foreach (var f in this.elements)
      {
        var outcome = f.IsNonNull(pc, value);

        if (outcome != ProofOutcome.Top)
        {
          return outcome;
        }
        //r = r.Meet(outcome);
        //if (r == ProofOutcome.Bottom) return r;
      }
      return r;
    }

    #endregion

    #region IEnumerable<IFactQuery<BoxedExpression,Variable>> Members

    public IEnumerator<IFactQuery<BoxedExpression, Variable>> GetEnumerator()
    {
      return this.elements.GetEnumerator();
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.elements.GetEnumerator();
    }

    #endregion
  }

  public class DisjunctionFactQuery<Variable> : IFactQuery<BoxedExpression, Variable>
  {
    Predicate<APC> isUnreachable;
    List<IFactQuery<BoxedExpression, Variable>> elements = new List<IFactQuery<BoxedExpression, Variable>>();

    public DisjunctionFactQuery(Predicate<APC> isUnreachable)
    {
      this.isUnreachable = isUnreachable;
    }

    /// <param name="elem"> can be null to mean no fact query at all</param>
    public void Add(IFactQuery<BoxedExpression, Variable> elem)
    {
      if (elem != null)
      {
        this.elements.Add(elem);
      }
    }

    #region IFactQuery<BoxedExpression,Variable> Members

    public ProofOutcome IsNull(APC pc, BoxedExpression expr)
    {
      var result = ProofOutcome.Bottom;
      foreach (var query in this.elements)
      {
        var outcome = query.IsNull(pc, expr);

        result = result.Join(outcome);

        if (result == ProofOutcome.Top)
          return result;
      }
      return result;
    }

    public ProofOutcome IsNonNull(APC pc, BoxedExpression expr)
    {
      var result = ProofOutcome.Bottom;
      foreach (var query in this.elements)
      {
        var outcome = query.IsNonNull(pc, expr);
        result = result.Join(outcome);
        if (result == ProofOutcome.Top)
          return result;
      }
      return result;
    }

    public ProofOutcome IsTrue(APC pc, BoxedExpression condition)
    {
      var result = ProofOutcome.Bottom;
      foreach (var query in this.elements)
      {
        var outcome = query.IsTrue(pc, condition);

        result = result.Join(outcome);
        if (result == ProofOutcome.Top) break;
      }
      if (result != ProofOutcome.Top)
        return result;

      return result;
// dead code?
#if false // Dead code??? 
      // simplify ceq cne
      BoxedExpression left, right;
      BinaryOperator op;
      if (condition.IsBinaryExpression(out op, out left, out right))
      {
        if ((op == BinaryOperator.Ceq || op == BinaryOperator.Cobjeq) && left.IsRelational() && this.IsNull(pc, right) == ProofOutcome.True)
        {
          ProofOutcome oc = this.IsTrue(pc, left);
          // negate definite outcomes
          if (oc == ProofOutcome.True) return ProofOutcome.False;
          if (oc == ProofOutcome.False) return ProofOutcome.True;
          return oc; // top and bottom are the same
        }
        else if (op == BinaryOperator.Ceq)
        {
          int lcint, rcint;
          if (left.IsConstantIntOrNull(out lcint) && right.IsConstantIntOrNull(out rcint))
          {
            if (lcint != rcint)
            {
              return ProofOutcome.False;
            }
            else
            {
              return ProofOutcome.True;
            }
          }
        }
      }
      if (condition.IsUnary && condition.UnaryOp == UnaryOperator.Not)
      {
        ProofOutcome oc = this.IsTrue(pc, condition.UnaryArgument);
        // negate definite outcomes
        if (oc == ProofOutcome.True) return ProofOutcome.False;
        if (oc == ProofOutcome.False) return ProofOutcome.True;
        return oc; // top and bottom are the same
      }

      return ProofOutcome.Top;
#endif
    }

    public ProofOutcome IsTrue(APC pc, Question question)
    {
      ProofOutcome result = ProofOutcome.Bottom;
      foreach (var el in this.elements)
      {
        result = el.IsTrue(pc, question).Join(result);

        if (result == ProofOutcome.Top) return result;
      }

      return result;
    }

    public ProofOutcome IsTrueImply(APC pc, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal)
    {
      var result = ProofOutcome.Bottom;
      foreach (var query in this.elements)
      {
        var outcome = query.IsTrueImply(pc, posAssumptions, negAssumptions, goal);

        result = result.Join(outcome);
        if (result == ProofOutcome.Top) return result;
      }
      return result;
    }

    public ProofOutcome IsGreaterEqualToZero(APC pc, BoxedExpression expr)
    {
      var result = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        var outcome = query.IsGreaterEqualToZero(pc, expr);

        result = result.Join(outcome);

        if (result == ProofOutcome.Top) return result;
      }
      return result;
    }

    public ProofOutcome IsLessThan(APC pc, BoxedExpression left, BoxedExpression right)
    {
      var result = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        var outcome = query.IsLessThan(pc, left, right);

        result = result.Join(outcome);
        if (result == ProofOutcome.Top) return result;
      }
      return result;
    }

    public ProofOutcome IsNonZero(APC pc, BoxedExpression condition)
    {
      var result = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        result = query.IsNonZero(pc, condition).Join(result);

        if (result == ProofOutcome.Top) return result;
      }
      return result;
    }

    public ProofOutcome HaveSameFloatType(APC pc, BoxedExpression left, BoxedExpression right)
    {
      var result = ProofOutcome.Top;
      foreach (var query in this.elements)
      {
        result = query.HaveSameFloatType(pc, left, right).Join(result);

        if (result == ProofOutcome.Top) return result;
      }
      return result;
    }

    public bool TryGetFloatType(APC pc, BoxedExpression exp, out ConcreteFloat type)
    {
      type = default(ConcreteFloat);
      var first = true;
      foreach (var query in this.elements)
      {
        ConcreteFloat newType;
        if (!query.TryGetFloatType(pc, exp, out newType))
        {
          return false;
        }

        if (first)
        {
          type = newType;
          first = false;
        }
        else
        {
          if (!newType.Equals(type))
          {
            return false;
          }
        }
      }
      return !first;
    }

    public bool IsUnreachable(APC pc)
    {
      if (this.isUnreachable != null && this.isUnreachable(pc))
      {
        return true;
      }
      foreach (var query in elements)
      {
        if (!query.IsUnreachable(pc)) return false;
      }
      return true;
    }

    /// <summary>
    /// This isn't right, but I would have to perform a set intersection, so I'm just punting on this now.
    /// </summary>
    public IEnumerable<Variable> LowerBounds(APC pc, BoxedExpression exp, bool strict)
    {
      foreach (var query in elements)
      {
        foreach (var lb in query.LowerBounds(pc, exp, strict))
        {
          yield return lb;
        }
      }

    }

    /// <summary>
    /// This isn't right, but I would have to perform a set intersection, so I'm just punting on this now.
    /// </summary>
    public IEnumerable<Variable> UpperBounds(APC pc, BoxedExpression exp, bool strict)
    {
      foreach (var query in elements)
      {
        foreach (var ub in query.UpperBounds(pc, exp, strict))
        {
          yield return ub;
        }
      }
    }

    /// <summary>
    /// This isn't right, but I would have to perform a set intersection, so I'm just punting on this now.
    /// </summary>
    public IEnumerable<BoxedExpression> LowerBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
    {
      foreach (var query in elements)
      {
        foreach (var lb in query.LowerBoundsAsExpressions(pc, exp, strict))
        {
          yield return lb;
        }
      }

    }

    /// <summary>
    /// This isn't right, but I would have to perform a set intersection, so I'm just punting on this now.
    /// </summary>
    public IEnumerable<BoxedExpression> UpperBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
    {
      foreach (var query in elements)
      {
        foreach (var ub in query.UpperBoundsAsExpressions(pc, exp, strict))
        {
          yield return ub;
        }
      }
    }

    public Pair<long, long> BoundsFor(APC pc, BoxedExpression exp)
    {
      return new Pair<long, long>(Int64.MinValue, Int64.MaxValue);
    }

    public ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object y)
    {
      foreach (var query in this.elements)
      {
        var outcome = query.IsVariableDefinedForType(pc, v, y);
        if (outcome != ProofOutcome.Top)
        {
          return outcome;
        }
      }

      return ProofOutcome.Top;
    }

    public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filterVariables, bool replaceVarsWithAccessPaths = true)
    {
      return FList<BoxedExpression>.Empty;
    }


    #endregion

    #region IFactBase<Variable> Members

    public ProofOutcome IsNull(APC pc, Variable value)
    {
      var result = ProofOutcome.Bottom;
      foreach (var f in this.elements)
      {
        result = f.IsNull(pc, value).Join(result);

        if (result == ProofOutcome.Top) return result;
      }
      return result;
    }

    public ProofOutcome IsNonNull(APC pc, Variable value)
    {
      var result = ProofOutcome.Bottom;
      foreach (var f in this.elements)
      {
        result = f.IsNonNull(pc, value).Join(result);

        if (result == ProofOutcome.Top) return result;

      }
      return result;
    }

    #endregion
  }

  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      public class AILogicInference<AbstractDomain> : IFactQuery<BoxedExpression, Variable>
        where AbstractDomain : INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>
      {
        #region Private state
        IFixpointInfo<APC, AbstractDomain> fixpointInfo;
        IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context;
        IValueAnalysisOptions options;
        IExpressionDecoder<BoxedVariable<Variable>, BoxedExpression> decoder;
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadatadecoder;
        readonly protected bool trace;
        #endregion

        public AILogicInference(
          IExpressionDecoder<BoxedVariable<Variable>, BoxedExpression> decoder,
          IValueAnalysisOptions options, 
          IFixpointInfo<APC, AbstractDomain> fixpointInfo,
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadatadecoder,
          bool trace
          )
        {
          Contract.Requires(fixpointInfo != null);

          this.decoder = decoder;
          this.options = options;
          this.fixpointInfo = fixpointInfo;
          this.context = context;
          this.metadatadecoder = metadatadecoder;
          this.trace = trace;
        }

        bool PreState(APC label, out AbstractDomain ifFound) { return this.fixpointInfo.PreState(label, out ifFound); }

        #region IFactQuery<Label,BoxedExpression,Variable> Members

        public ProofOutcome IsNull(APC pc, BoxedExpression expr)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsNonNull(APC pc, BoxedExpression expr)
        {
          return ProofOutcome.Top;
        }

        public virtual ProofOutcome IsGreaterEqualToZero(APC pc, BoxedExpression expr)
        { 
          // Easy checks: if the type is unsigned, then it is trivially greater equal than zero
          // (We do the check here to shortcut the evaluation)
          if(expr.IsVariable)
          {
            var typeForExpr = this.context.ValueContext.GetType(pc, (Variable) expr.UnderlyingVariable);
            if (typeForExpr.IsNormal && IsUnsignedType(typeForExpr.Value))
            {
              return ProofOutcome.True;
            }
          }

          AbstractDomain adomain;
          if (this.PreState(pc, out adomain))
          {
            return CheckGreaterEqualThanZero(expr, adomain);
          }

          return ProofOutcome.Bottom;
        }

        protected ProofOutcome CheckGreaterEqualThanZero(BoxedExpression expr, AbstractDomain adomain)
        {
          if (adomain.IsBottom)
          {
            return ProofOutcome.Bottom;
          }

          return adomain.CheckIfGreaterEqualThanZero(expr).ToProofOutcome();
        }

        public virtual ProofOutcome IsLessThan(APC pc, BoxedExpression left, BoxedExpression right)
        {
          AbstractDomain adomain;
          
          if (this.PreState(pc, out adomain))
          {
            return CheckIfLessThan(left, right, adomain);
          }

          return ProofOutcome.Bottom;
        }

        protected ProofOutcome CheckIfLessThan(BoxedExpression left, BoxedExpression right, AbstractDomain adomain)
        {
          if (adomain.IsBottom)
          {
            return ProofOutcome.Bottom;
          }

          return adomain.CheckIfLessThan(left, right).ToProofOutcome();
        }

        public virtual ProofOutcome IsNonZero(APC pc, BoxedExpression condition)
        {
          AbstractDomain adomain;
          if (this.PreState(pc, out adomain))
          {
            return CheckIfNonZero(condition, adomain);
          }

          return ProofOutcome.Top;
        }

        protected ProofOutcome CheckIfNonZero(BoxedExpression condition, AbstractDomain adomain)
        {
          if (adomain.IsBottom)
          {
            return ProofOutcome.Bottom;
          }

          return adomain.CheckIfNonZero(condition).ToProofOutcome();
        }

        public ProofOutcome HaveSameFloatType(APC pc, BoxedExpression left, BoxedExpression right)
        {
          AbstractDomain adomain;
          if(this.PreState(pc, out adomain))
          {
            return CheckHaveSameFloatType(pc, adomain, left, right);
          }

          return ProofOutcome.Top;
        }

        public bool TryGetFloatType(APC pc, BoxedExpression exp, out ConcreteFloat type)
        {
          AbstractDomain adomain;
            Variable expVar;
          if (!exp.IsNaN() && exp.TryGetFrameworkVariable(out expVar) && this.PreState(pc, out adomain))
          {
            var liftedType = adomain.GetFloatType(new BoxedVariable<Variable>(expVar));
            if (liftedType.IsNormal())
            {
              type = liftedType.BoxedElement;
              return true;
            }
          }

          type = default(ConcreteFloat);
          return false;
        }

        protected ProofOutcome CheckHaveSameFloatType(APC pc, AbstractDomain adomain, BoxedExpression left, BoxedExpression right)
        {
          if (adomain.IsBottom)
          {
            return ProofOutcome.Bottom;
          }

          if (left.IsNaN() || right.IsNaN())
          {
            return ProofOutcome.True;
          }

          var leftVar = this.decoder.UnderlyingVariable(left);
          var rightVar = this.decoder.UnderlyingVariable(right);

          var typeForLeft = adomain.GetFloatType(leftVar);
          var typeForRight = adomain.GetFloatType(rightVar);

          if (typeForLeft.IsNormal() && typeForRight.IsNormal())
          {
            if(typeForLeft.BoxedElement == typeForRight.BoxedElement && typeForLeft.BoxedElement != ConcreteFloat.Uncompatible)
            {
              return ProofOutcome.True;
            }
          }

          // Note: we never return ProofOutcome.False

          return ProofOutcome.Top;
        }

        public virtual ProofOutcome IsTrue(APC pc, BoxedExpression condition)
        {
          // simplify X ceq 0 if possible
          AbstractDomain adomain;
          if (this.PreState(pc, out adomain))
          {
            return CheckIfHolds(condition, adomain);
          }
          return ProofOutcome.Top;
        }

        public virtual ProofOutcome IsTrue(APC pc, Question q)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsTrueImply(APC pc, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal)
        {
          AbstractDomain adomain;
          if (this.PreState(pc, out adomain))
          {
            return CheckIfImplicationHolds(adomain, posAssumptions, negAssumptions, goal);
          }

          return ProofOutcome.Top;
        }

        protected ProofOutcome CheckIfImplicationHolds(AbstractDomain adomain, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal)
        {

          var dup = (AbstractDomain) adomain.Clone();

          for(int i = 0; i < 2; i++)
          {
            var copyPosAssumptions = posAssumptions;
            var copyNegAssumptions = negAssumptions;

            for (; copyPosAssumptions != null; copyPosAssumptions = copyPosAssumptions.Tail)
            {
              var assumption = copyPosAssumptions.Head.Simplify(this.metadatadecoder);
              if (assumption != null)
              {
                dup = (AbstractDomain)dup.TestTrue(assumption);
              }
            }

            for (; copyNegAssumptions != null; copyNegAssumptions = copyNegAssumptions.Tail)
            {
              dup = (AbstractDomain)dup.TestFalse(copyNegAssumptions.Head);
            }

          }

          if (this.trace)
          {
            Console.WriteLine("Checking {0} in the abstract domain:", goal);
            Console.WriteLine(dup.ToString());
          }

          var result = CheckIfHolds(goal, dup);

          return result == ProofOutcome.Bottom ? ProofOutcome.True /* bottom implies everything */ : result;
        }


        protected ProofOutcome CheckIfHolds(BoxedExpression condition, AbstractDomain adomain)
        {
          if (adomain.IsBottom)
          {
            return ProofOutcome.Bottom;
          }
          else
          {
            return adomain.CheckIfHolds(condition.Simplify(this.metadatadecoder)).ToProofOutcome();
          }
        }

        public IEnumerable<Variable> LowerBounds(APC pc, BoxedExpression expression, bool strict)
        {
          AbstractDomain adomain;
          if (this.PreState(pc, out adomain))
          {
            foreach (var lb in adomain.LowerBoundsFor(expression, strict))
            {
              if (lb.IsVariable) {
                object o = (object)lb.UnderlyingVariable;
                if (o is Variable)
                {
                  yield return (Variable)o;
                }
              }
            }
          }
        }

        public IEnumerable<Variable> UpperBounds(APC pc, BoxedExpression expression, bool strict)
        {
          AbstractDomain adomain;
          if (this.PreState(pc, out adomain))
          {
            foreach (var ub in adomain.UpperBoundsFor(expression, strict))
            {
              if (ub.IsVariable) {
                var o = (object)ub.UnderlyingVariable;
                if (o is Variable)
                {
                  yield return (Variable)o;
                }
              }
            }
          }
        }

        public IEnumerable<BoxedExpression> LowerBoundsAsExpressions(APC pc, BoxedExpression expression, bool strict)
        {
          AbstractDomain adomain;
          if (this.PreState(pc, out adomain))
          {
            foreach (BoxedExpression lb in adomain.LowerBoundsFor(expression, strict))
            {
              yield return lb;
            }
          }
        }

        public IEnumerable<BoxedExpression> UpperBoundsAsExpressions(APC pc, BoxedExpression expression, bool strict)
        {
          AbstractDomain adomain;
          if (this.PreState(pc, out adomain))
          {
            foreach (BoxedExpression ub in adomain.UpperBoundsFor(expression, strict))
            {
              yield return ub;
            }
          }
        }

        public Pair<long, long> BoundsFor(APC pc, BoxedExpression exp)
        {
           AbstractDomain adomain;
           if (this.PreState(pc, out adomain))
           {
             var range = adomain.BoundsFor(exp);
             var lowBound = range.LowerBound.IsInfinity ? Int64.MinValue : (Int64) range.LowerBound.PreviousInt32;
             var uppBound = range.UpperBound.IsInfinity ? Int64.MaxValue : (Int64)range.UpperBound.NextInt32;

             return new Pair<long, long>(lowBound, uppBound);
           }
           else
           {
             return new Pair<long, long>(Int64.MinValue, Int64.MaxValue);
           }
        }

        public ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object t)
        {
          if (!(t is Type))
          {
            return ProofOutcome.Top;
          }

          var type = (Type)t;
          List<int> typeValues;
          AbstractDomain adomain;

          if (this.metadatadecoder.IsEnumWithoutFlagAttribute(type)
            && this.metadatadecoder.TryGetEnumValues(type, out typeValues)
            && this.PreState(pc, out adomain))
          {
            var ranges = DisInterval.For(typeValues);
            var bounds = adomain.BoundsFor(new BoxedVariable<Variable>(v));

            if (bounds.LessEqual(ranges))
            {
              return ProofOutcome.True;
            }

            if (bounds.Meet(ranges).IsBottom)
            {
              return ProofOutcome.False;
            }
          }

          return ProofOutcome.Top;
        }

        public bool IsUnreachable(APC pc)
        {
          AbstractDomain adomain;
          if (!this.PreState(pc, out adomain))
          {
            return true;
          }
          return adomain.IsBottom;
        }

        public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filterVariables, bool replaceVarsWithAccessPaths=true)
        {
          AbstractDomain adomain;
          if (this.PreState(pc, out adomain))
          {
            // Should we filter some variable?
            if (filterVariables != null)
            {
              var asArray = filterVariables.Map(v => new BoxedVariable<Variable>(v)).ToArray();
              var cloned = (AbstractDomain)adomain.Clone();

              foreach (var x in adomain.Variables)
              {
                if (!asArray.Contains(x))
                {
                  cloned.ProjectVariable(x);
                }
              }

              adomain = cloned;
            }
            var exps = FList<BoxedExpression>.Empty;
            var mutator = new ReplaceSymbolicValueForAccessPath<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(this.context, this.metadatadecoder);

            foreach (var exp in adomain.To(new BoxedExpressionFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>(this.metadatadecoder)).SplitConjunctions())
            {
              bool condition;

              var tmp = mutator.ReadAt(pc, exp, replaceVarsWithAccessPaths);
              if (tmp != null && !tmp.IsTrivialCondition(out condition))
                exps = exps.Cons(tmp);
            }            

            return exps;
          }

          return FList<BoxedExpression>.Empty;
        }

        #endregion

        #region Private
        /// <summary>
        /// True if <code>type</code> is an unsigned type, ignoring surrounding optional/required modifiers
        /// </summary>
        private bool IsUnsignedType(Type/*!*/ type)
        {
          if (type.Equals(this.metadatadecoder.System_Char))
            return true;
          if (type.Equals(this.metadatadecoder.System_UInt16))
            return true;
          if (type.Equals(this.metadatadecoder.System_UInt32))
            return true;
          if (type.Equals(this.metadatadecoder.System_UInt64))
            return true;

          return false;
        }

        #endregion

        #region IFactBase<Label,Variable> Members

        public ProofOutcome IsNull(APC pc, Variable value)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsNonNull(APC pc, Variable value)
        {
          return ProofOutcome.Top;
        }

        #endregion

        #region Tracing
        [Conditional("DEBUG")]
        protected void Trace(string format, params string[] args)
        {
          if(this.trace)
          {
            try
            {
              var str = string.Format(format, args);
              Console.WriteLine(str);
            }
            catch (FormatException)
            {
              Console.WriteLine("Malformed tracing string");
            }
          }
        }

        [Conditional("DEBUG")]
        protected void Trace(string msg)
        {
          Trace(msg, "");
        }

        #endregion
      }

      public class AILogicInferenceWithRefinements<AbstractDomain> : AILogicInference<AbstractDomain>
        where AbstractDomain : INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>
      {
        #region Private state
        
        List<IFixpointInfo<APC, AbstractDomain>> fixpointInfos; 
        FixpointEnum<APC, AbstractDomain> fixpointenumerator;

        #endregion

        public AILogicInferenceWithRefinements(
          IExpressionDecoder<BoxedVariable<Variable>, BoxedExpression> decoder,
          IValueAnalysisOptions options,
          IFixpointInfo<APC, AbstractDomain> fixpointInfo,
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadatadecoder,
          FixpointEnum<APC, AbstractDomain> fixpointenumerator,
          bool trace
          )
          : base(decoder, options, fixpointInfo, context, metadatadecoder, trace)
        {
          this.fixpointenumerator = fixpointenumerator;
          this.fixpointInfos = new List<IFixpointInfo<APC, AbstractDomain>>();
        }

        static bool TryPreState(IFixpointInfo<APC, AbstractDomain> fixpointInfo, APC label, out AbstractDomain ifFound) 
        { 
          return fixpointInfo.PreState(label, out ifFound); 
        }

        #region IFactQuery<Label,BoxedExpression,Variable> Members

        override public ProofOutcome IsGreaterEqualToZero(APC pc, BoxedExpression expr)
        {
          ProofOutcome result = base.IsGreaterEqualToZero(pc, expr);

          if (result == ProofOutcome.Top)
          {
            foreach (var fixpoint in this.fixpointenumerator())
            {
              AbstractDomain adom;
              if (TryPreState(fixpoint, pc, out adom))
              {
                result = base.CheckGreaterEqualThanZero(expr, adom);
                if (result == ProofOutcome.True)
                {
                  return result;
                }
              }
              else
              {
                return ProofOutcome.Bottom;
              }
            }
          }

          return result;
        }

        override public ProofOutcome IsLessThan(APC pc, BoxedExpression left, BoxedExpression right)
        {
          ProofOutcome result = base.IsLessThan(pc, left, right);

          if (result == ProofOutcome.Top)
          {
            foreach (var fixpoint in this.fixpointenumerator())
            {
              AbstractDomain adom;
              if (TryPreState(fixpoint, pc, out adom))
              {
                result = base.CheckIfLessThan(left, right, adom);
                if (result == ProofOutcome.True)
                {
                  return result;
                }
              }
              else
              {
                return ProofOutcome.Bottom;
              }
            }
          }

          return result;
        }

        override public ProofOutcome IsNonZero(APC pc, BoxedExpression condition)
        {
          ProofOutcome result = base.IsNonZero(pc, condition);

          if (result == ProofOutcome.Top)
          {
            foreach (var fixpoint in this.fixpointenumerator())
            {
              AbstractDomain adom;
              if (TryPreState(fixpoint, pc, out adom))
              {
                result = base.CheckIfNonZero(condition, adom);
                if (result == ProofOutcome.True)
                {
                  return result;
                }
              }
              else
              {
                return ProofOutcome.Bottom;
              }
            }
          }

          return result;
        }

        override public ProofOutcome IsTrue(APC pc, BoxedExpression condition)
        {
          Trace("Trying to prove the condition: {0}", condition.ToString());

          var result = base.IsTrue(pc, condition);

          Trace("Direct attempt outcome : {0}", result.ToString());

          if (result == ProofOutcome.Top)
          {
            var step = 1;

            foreach (var fixpoint in this.fixpointenumerator())
            {
              Trace("Refinement step {0}", step.ToString());

              AbstractDomain adom;
              if (TryPreState(fixpoint, pc, out adom))
              {
                result = base.CheckIfHolds(condition, adom);

                Trace("Outcome : {0}", result.ToString());

                if (result == ProofOutcome.True)
                {
                  return result;
                }
              }
              else
              {
                Trace("Found a contraddiction (_|_)");

                return ProofOutcome.Bottom;
              }

              step++;
            }
          }

          return result;
        }

        #endregion
      }

      public abstract class QuantifierFactQuery<ArrayDomain> : IFactQuery<BoxedExpression, Variable>
        where ArrayDomain : ArrayState
      {
        #region Protected state

        readonly protected IFixpointInfo<APC, ArrayDomain> fixpointInfo;
        readonly protected IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context;
        readonly protected IValueAnalysisOptions options;
        readonly protected IExpressionDecoder<BoxedVariable<Variable>, BoxedExpression> decoder;
        readonly protected IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> encoder;
        readonly protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadatadecoder;

        #endregion

        #region Constructor
        
        protected QuantifierFactQuery(
          IExpressionDecoder<BoxedVariable<Variable>, BoxedExpression> decoder,
          IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> encoder,
          IValueAnalysisOptions options,
          IFixpointInfo<APC, ArrayDomain> fixpointInfo,
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadatadecoder
          )
        {
          this.decoder = decoder;
          this.encoder = encoder;
          this.options = options;
          this.fixpointInfo = fixpointInfo;
          this.context = context;
          this.metadatadecoder = metadatadecoder;
        }

        #endregion

        #region Subclass contracts

        abstract public ProofOutcome IsTrue(APC pc, BoxedExpression condition);

        #endregion

        #region IFactQuery<BoxedExpression,Variable> Members

        public ProofOutcome IsTrue(APC pc, Question question)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsNull(APC pc, BoxedExpression expr)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsNonNull(APC pc, BoxedExpression expr)
        {
          return ProofOutcome.Top;
        }


        public ProofOutcome IsTrueImply(APC pc, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsGreaterEqualToZero(APC pc, BoxedExpression expr)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsLessThan(APC pc, BoxedExpression left, BoxedExpression right)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsNonZero(APC pc, BoxedExpression condition)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome HaveSameFloatType(APC pc, BoxedExpression left, BoxedExpression right)
        {
          return ProofOutcome.Top;
        }

        public bool TryGetFloatType(APC pc, BoxedExpression exp, out ConcreteFloat type)
        {
          type = default(ConcreteFloat);
          return false;
        }

        public IEnumerable<Variable> LowerBounds(APC pc, BoxedExpression exp, bool strict)
        {
          yield break;
        }

        public IEnumerable<Variable> UpperBounds(APC pc, BoxedExpression exp, bool strict)
        {
          yield break;
        }

        public IEnumerable<BoxedExpression> LowerBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
        {
          yield break;
        }

        public IEnumerable<BoxedExpression> UpperBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
        {
          yield break;
        }

        public Pair<long, long> BoundsFor(APC pc, BoxedExpression exp)
        {
          return new Pair<long, long>(Int64.MinValue, Int64.MaxValue);
        }

        public ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object y)
        {
          return ProofOutcome.Top;
        }

        public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filterVariables, bool replaceVarsWithAccessPaths = true)
        {
          var result = FList<BoxedExpression>.Empty;

          ArrayDomain adom;
          if (this.fixpointInfo.PostState(pc, out adom) && adom.IsNormal())
          {
            Func<Variable, BoxedExpression> arrayLengthFetcher = (Variable arr) =>
              {
                Variable length;
                if (this.context.ValueContext.TryGetArrayLength(pc, arr, out length))
                {
                  return BoxedExpression.Var(length);
                }
                else
                {
                  return null;
                }
              };

            var expFactory = new BoxedExpressionFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>(BoxedExpression.Var("__i__"), this.metadatadecoder, arrayLengthFetcher);
            var clone = adom.Duplicate();
            var array = clone.Array;

            if (array.IsNormal())
            {
              foreach (var x in adom.Array.Variables)
              {
                Variable xVar;
                if (x.TryUnpackVariable(out xVar))
                {
                  if (!filterVariables.Contains(xVar))
                  {
                    array.RemoveVariable(x);
                  }
                }
              }
              var newInvariants = array.To(expFactory).SplitConjunctions().ToFList();
              result = result.Append(newInvariants);
            }

            for (var i = 0; i < clone.PluginsCount; i++)
            {
              var state = clone.PluginAbstractStateAt(i);

              if (state.IsNormal())
              {
                foreach (var x in state.Variables)
                {
                  Variable xVar;
                  if (x.TryUnpackVariable(out xVar))
                  {
                    if (!filterVariables.Contains(xVar))
                    {
                      state.RemoveVariable(x);
                    }
                  }
                }

                var newInvariants = state.To(expFactory).SplitConjunctions().ToFList();

                result = result.Append(newInvariants);
              }
            }
          }

          var convertedExp = FList<BoxedExpression>.Empty;
          var mutator = new ReplaceSymbolicValueForAccessPath<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(this.context, this.metadatadecoder);
          foreach (var exp in result.GetEnumerable())
          {
            bool dummy;
            var expM = mutator.ReadAt(pc, exp, replaceVarsWithAccessPaths);
            if (expM != null && !exp.IsTrivialCondition(out dummy))
            {
              convertedExp = FList<BoxedExpression>.Cons(expM, convertedExp);
            }
          }

          return convertedExp;
        }

        #endregion

        #region IFactBase<Variable> Members

        public ProofOutcome IsNull(APC pc, Variable value)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsNonNull(APC pc, Variable value)
        {
          return ProofOutcome.Top;
        }

        public bool IsUnreachable(APC pc)
        {
          return false;
        }

        #endregion
      }

      public class ComposedQuantifierFactQuery<ArrayDomain> : QuantifierFactQuery<ArrayDomain>
                where ArrayDomain : ArrayState
      {
        #region State

        readonly ForAllFactQuery<ArrayDomain> forAll;
        readonly ExistsFactQuery<ArrayDomain> exists;

        readonly Func<APC, Variable, ForAllIndexedExpression> forAllDecoder;
        readonly Func<APC, Variable, ExistsIndexedExpression> existsDecoder;
        #endregion

        #region Constructor

        public ComposedQuantifierFactQuery(
          ForAllFactQuery<ArrayDomain> forAll, ExistsFactQuery<ArrayDomain> exists,
          IExpressionDecoder<BoxedVariable<Variable>, BoxedExpression> decoder,
          IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> encoder,
          IValueAnalysisOptions options,
          IFixpointInfo<APC, ArrayDomain> fixpointInfo,
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadatadecoder,
          Func<APC, Variable, ForAllIndexedExpression> forAllDecoder,
          Func<APC, Variable, ExistsIndexedExpression> existsDecoder)
          : base(decoder, encoder, options, fixpointInfo, context, metadatadecoder)
        {
          Contract.Requires(forAll != null);

          this.forAll = forAll;
          this.exists = exists;

          this.forAllDecoder = forAllDecoder;
          this.existsDecoder = existsDecoder;
        }

        #endregion

        #region IsTrue

        public override ProofOutcome IsTrue(APC pc, BoxedExpression condition)
        {
          // Direct check
          var result = InternalIsTrue(pc, condition);

          if (result != ProofOutcome.Top)
            return result;

          // Negations
          BoxedExpression left, right;
          BinaryOperator bop;
          if (condition.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Ceq)
          {
            int k;
            var b = left.IsConstantInt(out k);
            if (!b && right.IsConstantInt(out k))
            {
              var tmp = left;
              left = right;
              right = tmp;

              b = true;
            }

            // invariant: left is definetely the constant, right some expression

            left = null;

            if (b)
            {
              Variable v;

              if (k != 0)
              {
                return InternalIsTrue(pc, right);
              }
              else if (right.TryGetFrameworkVariable(out v))
              {
                var asForAll = forAllDecoder(pc, v);
                if (asForAll != null)
                {
                  return exists.IsTrue(pc, asForAll.Negate() as ExistsIndexedExpression); // negation of ForAll should be exists

                }
                else
                {
                  var asExists = existsDecoder(pc, v);
                  if (asExists != null)
                  {
                    return forAll.IsTrue(pc, asExists.Negate() as ForAllIndexedExpression); // negation of Exists should be forall
                  }
                }
              }
            }
          }

          return ProofOutcome.Top;
        }

        private ProofOutcome InternalIsTrue(APC pc, BoxedExpression condition)
        {
          Contract.Requires(condition != null);

          var forAllOutcome = this.forAll.IsTrue(pc, condition);
          if (forAllOutcome != ProofOutcome.Top)
          {
            return forAllOutcome;
          }

          if (this.exists != null)
          {
            var existsOutcome = this.exists.IsTrue(pc, condition);
            if (existsOutcome != ProofOutcome.Top)
            {
              return existsOutcome;
            }
          }

          return ProofOutcome.Top;
        }

        #endregion

        #region ToString
        public override string ToString()
        {
          return "composed quantifiers";
        }
        #endregion
      }

      public class ForAllFactQuery<ArrayDomain> : QuantifierFactQuery<ArrayDomain>
        where ArrayDomain : ArrayState
      {
        #region State

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(forAllDecoder != null);
        }

        readonly protected Func<APC, Variable, ForAllIndexedExpression> forAllDecoder;

        #endregion

        #region Constructor

        public ForAllFactQuery(
          IExpressionDecoder<BoxedVariable<Variable>, BoxedExpression> decoder,
          IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> encoder,
          IValueAnalysisOptions options, 
          IFixpointInfo<APC, ArrayDomain> fixpointInfo,
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadatadecoder,
          Func<APC, Variable, ForAllIndexedExpression> forAllDecoder
          )
          : base(decoder, encoder, options, fixpointInfo, context, metadatadecoder)
        {
          Contract.Requires(fixpointInfo != null);
          Contract.Requires(forAllDecoder != null);

          this.forAllDecoder = forAllDecoder;
        }

        #endregion

        #region IsTrue

        override public ProofOutcome IsTrue(APC pc, BoxedExpression condition)
        {
          Variable conditionVar;
          if (condition.TryGetFrameworkVariable(out conditionVar))
          {
            return IsTrue(pc, forAllDecoder(pc, conditionVar));
          }
          else
          {
            return IsTrue(pc, condition as ForAllIndexedExpression);
          }

        }

        internal ProofOutcome IsTrue(APC pc, ForAllIndexedExpression forAllExp)
        {
          if (forAllExp == null)
          {
            return ProofOutcome.Top;
          }

          ArrayDomain adom;
          if (forAllExp != null && this.fixpointInfo.PreState(pc, out adom))
          {
            // Shortcut for ForAll(false => ..)
            if (forAllExp.LowerBound.Equals(forAllExp.UpperBound))
            {
              return ProofOutcome.True;
            }

            // Shortcut for ForAll(empty => ..)
            if (adom.Numerical.CheckIfNonZero(forAllExp.UpperBound).IsFalse() 
              || adom.Numerical.CheckIfLessEqualThan(forAllExp.LowerBound, forAllExp.UpperBound).IsFalse())
            {
              return ProofOutcome.True;
            }

            BoxedExpression.ArrayIndexExpression<Type> arrayExp;
            Variable arrFrameworkVar;

            // Look for the array inside the body
            if (forAllExp.Body.TryFindArrayExp(forAllExp.BoundVariable, out arrayExp)
              && arrayExp.Array.TryGetFrameworkVariable<Variable>(out arrFrameworkVar))
            {
              var slackVar = new BoxedVariable<Variable>(true);
              var slackExp = BoxedExpression.Var(slackVar);
              var renamedBody = forAllExp.Body.Substitute(arrayExp, slackExp);

              var arrVar = new BoxedVariable<Variable>(arrFrameworkVar);

              ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> arrSegment;

              // Look for the segmentation of arrVar
              if (adom.Array.TryGetValue(arrVar, out arrSegment))
              {
                var slackIndexVar = new BoxedVariable<Variable>(true);
                var slackIndexExp = BoxedExpression.Var(slackIndexVar);
                var slackIndexNorm = NormalizedExpression<BoxedVariable<Variable>>.For(slackIndexVar);

                // replace the bound variable (a simple string) with a temp slack index variable, to which we push some information
                renamedBody = renamedBody.Substitute(forAllExp.BoundVariable, slackIndexExp);

                // lowerBound <= slackIndexExp
                var numDom = adom.Numerical.TestTrueLessEqualThan(forAllExp.LowerBound, slackIndexExp);

                // slackIndexExp < upperBound
                numDom = numDom.TestTrueLessThan(slackIndexExp, forAllExp.UpperBound);

                NormalizedExpression<BoxedVariable<Variable>> neLeft, neRight;

                var expManager = new ExpressionManagerWithEncoder<BoxedVariable<Variable>, BoxedExpression>(DFARoot.TimeOut, this.decoder, this.encoder);

                IAbstractDomain v;
                if ((NormalizedExpression<BoxedVariable<Variable>>.TryConvertFrom(forAllExp.LowerBound, expManager, out neLeft) &&
                  NormalizedExpression<BoxedVariable<Variable>>.TryConvertFrom(forAllExp.UpperBound, expManager, out neRight) &&
                  arrSegment.TryGetAbstractValue(neLeft, neRight, numDom, out v)) ||
                  (arrSegment.TryGetAbstractValue(slackIndexNorm, numDom, out v) && !v.IsTop))
                {
                  // ForAll(false => q) is True 
                  if (v.IsBottom)
                  {
                    return ProofOutcome.True;
                  }

                  var nonRelationalAbstractValue = v as NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>;

                  Contract.Assume(nonRelationalAbstractValue != null);

                  if (nonRelationalAbstractValue != null && nonRelationalAbstractValue.IsNormal())
                  {
                    numDom = numDom.TestTrue(slackVar, nonRelationalAbstractValue, this.encoder);

                    var outcome = numDom.CheckIfHolds(renamedBody).ToProofOutcome();

                    // The semantic check did not work out. 
                    if (outcome == ProofOutcome.Top)
                    {
                      // We try slack != exp
                      if (nonRelationalAbstractValue.DisEqualities.IsNormal())
                      {
                        BoxedExpression exp1, exp2;
                        if (renamedBody.IsCheckExp1NotEqExp2(out exp1, out exp2))
                        {
                          Variable noteq;
                          if (exp1.UnderlyingVariable == slackVar && exp2.TryGetFrameworkVariable(out noteq) &&
                            nonRelationalAbstractValue.DisEqualities.Contains(new BoxedVariable<Variable>(noteq)))
                          {
                            return ProofOutcome.True;
                          }
                          else if (exp2.UnderlyingVariable == slackVar && exp1.TryGetFrameworkVariable(out noteq) &&
                            nonRelationalAbstractValue.DisEqualities.Contains(new BoxedVariable<Variable>(noteq)))
                          {
                            return ProofOutcome.True;
                          }
                        }
                      }

                      // We try now with the syntactic condition
                      if (nonRelationalAbstractValue.SymbolicConditions.IsNormal())
                      {
                        var renameBodyWithSlackVar = renamedBody.ReplaceVariable(slackVar, nonRelationalAbstractValue.SymbolicConditions.SlackVariable);

                        foreach (var exp in nonRelationalAbstractValue.SymbolicConditions.Conditions.Values)
                        {
                          if (object.Equals(exp, renameBodyWithSlackVar))
                          {
                            return ProofOutcome.True;
                          }
                        }
                      }

                    }

                    return outcome;
                  }
                }
              }
            }
          }
          return ProofOutcome.Top;
        }

        #endregion

        #region ToString
        public override string ToString()
        {
          return "forall";
        }
        #endregion
      }

      public class ExistsFactQuery<ArrayDomain> : QuantifierFactQuery<ArrayDomain>
        where ArrayDomain : ArrayState
      {
        #region Object invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.existsDecoder != null);
        }

        #endregion

        #region Private state

        readonly Func<APC, Variable, ExistsIndexedExpression> existsDecoder;
        
        #endregion

        #region Constructor
        public ExistsFactQuery(
          IExpressionDecoder<BoxedVariable<Variable>, BoxedExpression> decoder,
          IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> encoder,
          IValueAnalysisOptions options, 
          IFixpointInfo<APC, ArrayDomain> fixpointInfo,
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadatadecoder,
          Func<APC, Variable, ExistsIndexedExpression> existsDecoder
          )
          : base(decoder, encoder, options, fixpointInfo, context, metadatadecoder)
        {
          Contract.Requires(fixpointInfo != null);
          Contract.Requires(existsDecoder != null);

          this.existsDecoder = existsDecoder;
        }
        #endregion

        #region IsTrue

        public override ProofOutcome IsTrue(APC pc, BoxedExpression condition)
        {
          Variable conditionVar;
          if (condition.TryGetFrameworkVariable(out conditionVar))
          {
            return IsTrue(pc, existsDecoder(pc, conditionVar));           
          }
          return ProofOutcome.Top;
        }

        internal ProofOutcome IsTrue(APC pc, ExistsIndexedExpression existsExp)
        {
          ArrayDomain adom;

          if (existsExp != null && this.fixpointInfo.PreState(pc, out adom))
          {
            // Shourtcuts for Exists(empty)
            if (
              existsExp.LowerBound.Equals(existsExp.UpperBound)
              ||
              adom.Numerical.CheckIfNonZero(existsExp.UpperBound).IsFalse())
            {
              return ProofOutcome.False;
            }

            BoxedExpression.ArrayIndexExpression<Type> arrayExp;
            Variable arrFrameworkVariable;
            if (existsExp.Body.TryFindArrayExp(existsExp.BoundVariable, out arrayExp)
              && arrayExp.Array.TryGetFrameworkVariable(out arrFrameworkVariable))
            {
              // We only do matches a == b
              BoxedExpression left, right;
              Variable varLeft, varRight;
              if (existsExp.Body.IsCheckExp1EqExp2(out left, out right))
              {
                // Left or right may be an array indexed expression and as a consequence not having an associated Framework variable
                Variable sourceArray;
                // varleft == one[two]
                bool unmodifiedSinceEntry;

                if (adom.IsUnmodifiedArrayElementFromEntry(new BoxedVariable<Variable>(arrFrameworkVariable), arrayExp.Index)
                  &&
                  (left.TryGetFrameworkVariable(out varLeft) && adom.IsVariableValueFlowFromArray(varLeft, out sourceArray, out unmodifiedSinceEntry))
                  ||
                  (right.TryGetFrameworkVariable(out varRight) && adom.IsVariableValueFlowFromArray(varRight, out sourceArray, out unmodifiedSinceEntry))
                  )
                {
                  return sourceArray.Equals(arrFrameworkVariable) ? ProofOutcome.True : ProofOutcome.Top;
                }
              }
            }
          }
          return ProofOutcome.Top;
        }

        #endregion

        #region ToString
        public override string ToString()
        {
          return "exists";
        }
        #endregion
      }
    }
  }
}
