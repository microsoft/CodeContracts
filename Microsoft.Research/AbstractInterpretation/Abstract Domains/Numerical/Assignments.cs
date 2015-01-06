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

// Helpers for inferring constraints from assignments

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  /// <summary>
  /// The interface for the assignment crawler
  /// </summary>
  [ContractClass(typeof(IConstraintsForAssignmentContracts<,,>))]
  public interface IConstraintsForAssignment<Variable, Expression, AbstractDomain>
    where AbstractDomain : INumericalAbstractDomainQuery<Variable, Expression>
  {
    /// <returns>
    /// A set of constraints corresponding to the assignment <code>x := exp</code>, using the abstract domain <code>adom</code>
    /// </returns>
    List<Expression> InferConstraints(Expression x, Expression exp, AbstractDomain adom);  
  }

  [ContractClassFor(typeof(IConstraintsForAssignment<,,>))]
  abstract class IConstraintsForAssignmentContracts<Variable, Expression, AbstractDomain>
    : IConstraintsForAssignment<Variable, Expression, AbstractDomain>
    where AbstractDomain : INumericalAbstractDomainQuery<Variable, Expression>
  {

    List<Expression> IConstraintsForAssignment<Variable, Expression, AbstractDomain>.InferConstraints(Expression x, Expression exp, AbstractDomain adom)
    {
      Contract.Requires(x != null);
      Contract.Requires(exp != null);
      Contract.Requires(adom != null);

      Contract.Ensures(Contract.Result<List<Expression>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<List<Expression>>(), e => e != null));
      
      throw new NotImplementedException();
    }
  }

  [ContractVerification(true)]
  internal class LessEqualThanConstraints<Variable, Expression>
    : IConstraintsForAssignment<Variable, Expression, INumericalAbstractDomainQuery<Variable, Expression>>
  {
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.expManager != null);
    }

    readonly private ExpressionManagerWithEncoder<Variable, Expression> expManager;

    public LessEqualThanConstraints(ExpressionManagerWithEncoder<Variable, Expression> expManager)
    {
      Contract.Requires(expManager != null);

      this.expManager = expManager;
    }

    #region IConstraintsForAssignment<Expression, AbstractDomain> Members

    //[SuppressMessage("Microsoft.Contracts", "Ensures-106-407")]
    public List<Expression> InferConstraints(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> adom)
    {
      Contract.Assert(adom != null);

      var xExp = new Pair<Expression, Expression>(x, exp);
      var result = new List<Expression>();

      Polynomial<Variable, Expression> expAsPol;

      var decoder = this.expManager.Decoder;
      var encoder = this.expManager.Encoder;

      if (Polynomial<Variable, Expression>.TryToPolynomialForm(exp, decoder, out expAsPol))
      {
        if (expAsPol.IsLinear)
        {
          #region The cases
          Expression leqExp;
          Variable y;
          Rational k;

          // If it is in the form y - k, with k constant, then we add the constraint " x <= y "
          if (expAsPol.TryMatch_YMinusK(out y, out k))
          {
            var yExp = encoder.VariableFor(y);

            leqExp = encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, x, yExp);

            Contract.Assert(leqExp != null);

            result.Add(leqExp);  // add the constraint x <= y
          }
          // If it is in the form y + k, with k constant, we add the constraint "y <= x" 
          else if (expAsPol.TryMatch_YPlusK(out y, out k))
          {
            var yExp = encoder.VariableFor(y);

            leqExp = encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, yExp, x);

            Contract.Assert(leqExp != null);

            result.Add(leqExp); // As we know x, this is a clever encoding for the constraint y <= x

            // x = y + 1 then if y < z => x <= z
            if (k == 1)
            {
              result.AddRange(UpperBoundsNonStrict(x, adom.UpperBoundsFor(y, true)));
            }
          }
          // If it is in the form k * y then it add the constaints "y <= x" or "x <= y"
          else if (expAsPol.TryMatch_kY(out y, out k))
          {
            var yExp = encoder.VariableFor(y);

            Contract.Assert(yExp != null);

            if (TryHelperFor_MatchkY(x, adom, yExp, k, out leqExp))
            {
              // F: this should be some bug, it is not taking into account the postcondition
              Contract.Assume(leqExp != null);

              result.Add(leqExp);
            }
          }
          #endregion
        }
      }

      var nonPolynomial = new TreatNonPolynomialCases(x, this.expManager).Visit(exp, adom);

      Contract.Assert(nonPolynomial != null);

      result.AddRange(nonPolynomial);

      Contract.Assume(Contract.ForAll(result, e => e != null));

      return result;
    }

    private IEnumerable<Expression> UpperBoundsNonStrict(Expression x, IEnumerable<Expression> iSet)
    {
      Contract.Requires(iSet != null);

      foreach (var upp in iSet)
      {
        yield return this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, x, upp);
      }
    }

    private bool TryHelperFor_MatchkY(Expression x, INumericalAbstractDomainQuery<Variable, Expression> adom, 
      Expression y, Rational k, out Expression leqExp)
    {
      Contract.Requires(adom != null);
      Contract.Requires(y != null);
      Contract.Requires(!object.ReferenceEquals(k, null));

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out leqExp) != null);

      var yPositive = adom.CheckIfGreaterEqualThanZero(y);

      if (k >= 1 && yPositive.IsNormal())
      {
        if (yPositive.BoxedElement)
        {
          leqExp = this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, y, x);
          return true;
        }
        else
        {
          leqExp = this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, x, y);
          return true;
        }
      }

      leqExp = default(Expression);
      return false;
    }

    private class TreatNonPolynomialCases
     : GenericExpressionVisitor<INumericalAbstractDomainQuery<Variable, Expression>, Set<Expression>, Variable, Expression>
    {
      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(this.expManager != null);
      }

      readonly ExpressionManagerWithEncoder<Variable, Expression> expManager;

      private Expression x;

      public TreatNonPolynomialCases(Expression x, ExpressionManagerWithEncoder<Variable, Expression> expManager)
        : base(expManager.Decoder)
      {
        Contract.Requires(expManager != null);

        this.x = x;
        this.expManager = expManager;
      }

      public override Set<Expression> VisitModulus(Expression left, Expression right, Expression original, INumericalAbstractDomainQuery<Variable, Expression> data)           
      {
        var result = new Set<Expression>();

        if (data == null)
          return result;

        if (data.BoundsFor(right).LowerBound >= 0)
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, x, right));     // Then assume x < e2
         }

        return result;
      }

      public override Set<Expression> VisitSubtraction(Expression left, Expression right, Expression original, INumericalAbstractDomainQuery<Variable, Expression> data)
      {
        var result = new Set<Expression>();

        if (data == null)
          return result;

        if (data.BoundsFor(right).LowerBound >= 0)
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, x, left));     // Then assume x < left
        }

        return result;
      }

      public override Set<Expression> VisitAddition(Expression left, Expression right, Expression original, INumericalAbstractDomainQuery<Variable, Expression> data)
      {
        var result = new Set<Expression>();

        if (data == null)
          return result;

        // x := left + right
        if (data.BoundsFor(right).LowerBound >= 0)
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, left, x));     // Then assume left <= x
        }

        if (data.BoundsFor(left).LowerBound >= 0)
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, right, x));     // Then assume right <= x
        }

        return result; 
      }

      public override Set<Expression> VisitOr(Expression left, Expression right, Expression original, INumericalAbstractDomainQuery<Variable, Expression> data)
      {
        var result = new Set<Expression>();

        if (data == null || left == null || right == null)
          return result;

        if (data.CheckIfGreaterEqualThanZero(left).IsTrue() && data.CheckIfGreaterEqualThanZero(right).IsTrue())
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, left, x));
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, right, x));
        }

        return result;
      }

      protected override Set<Expression> Default(INumericalAbstractDomainQuery<Variable, Expression> data)
      {
        return new Set<Expression>();
      }
    }


    #endregion
  }

  internal class LessThanConstraints<Variable, Expression>
    : IConstraintsForAssignment<Variable, Expression, INumericalAbstractDomainQuery<Variable, Expression>>
  {
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(expManager != null);
    }

    readonly private ExpressionManagerWithEncoder<Variable, Expression> expManager;
    readonly private FIFOCache<Pair<Expression, Expression>, Cache_Entry<Expression>> cache;

    public LessThanConstraints(ExpressionManagerWithEncoder<Variable, Expression> expManager)
    {
      Contract.Requires(expManager != null);

      this.expManager = expManager;
      this.cache = new FIFOCache<Pair<Expression, Expression>, Cache_Entry<Expression>>(16);
    }

    #region IConstraintsForAssignment<Expression,AbstractDomain> Members

    public List<Expression> InferConstraints(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> oracleDomain)
    {
      Polynomial<Variable, Expression> expAsPol;
      Expression ltExp;

      var result = new List<Expression>();
      var xExp = new Pair<Expression, Expression>(x, exp);

      #region 0. Fetch the cache
      Cache_Entry<Expression> cached;
      if (cache.TryGetValue(xExp, out cached))
      {
        switch (cached.Match_case)
        {
          case Cache_Entry.MatchCase.YMinusK:
            result.Add(cached.ResultExpression);
            result.AddRange(UpperBounds(cached.X, oracleDomain.UpperBoundsFor(cached.Y, false)));
            break;

          case Cache_Entry.MatchCase.YPlusK:
            result.Add(cached.ResultExpression);
            result.AddRange(UpperBounds(cached.Y, oracleDomain.UpperBoundsFor(cached.X, false)));
            break;

          case Cache_Entry.MatchCase.kY:
            Helper_Match_kY(cached.X, oracleDomain, result, cached.Y);
            break;

          case Cache_Entry.MatchCase.NotLinear:
            // do nothing: this case essentially avoids the computation in the other branchs that we know to be unfruitfull
            break;

          default:
            // error?
            break;
        }
      }
      #endregion

      #region 1. If failed, Try to put the r-exp in a polynomial (with simplifications, etc.)
      else if (Polynomial<Variable, Expression>.TryToPolynomialForm(exp, this.expManager.Decoder, out expAsPol))
      {
        if (expAsPol.IsLinear)
        {
          // If it is in the form y - k, with k constant, then we add the constraint
          Variable y;
          Rational k;
          if (expAsPol.TryMatch_YMinusK(out y, out k) && !x.Equals(y))
          {
            var yExp = this.expManager.Encoder.VariableFor(y);

            ltExp = this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, x, yExp);
            result.Add(ltExp);

            result.AddRange(UpperBounds(x, oracleDomain.UpperBoundsFor(y, false)));

            // update the cache 
            cache.Add(xExp, Cache_Entry.For(Cache_Entry.MatchCase.YMinusK, x, yExp, k, ltExp));
          }
          // If it is in the form y + k, with k constant, we add the constraint "y < x" to the back constraits
          else if (expAsPol.TryMatch_YPlusK(out y, out k) && !x.Equals(y))
          {
            var yExp = this.expManager.Encoder.VariableFor(y);

            ltExp = this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, yExp, x);

            result.Add(ltExp);
            result.AddRange(UpperBounds(yExp, oracleDomain.UpperBoundsFor(x, false)));

            // update the cache
            cache.Add(xExp, Cache_Entry.For(Cache_Entry.MatchCase.YPlusK, x, yExp, k, ltExp));
          }
          // If it is in the form k*y, with k constant, if k > 1 we add the constraint "x > y"
          else if (expAsPol.TryMatch_kY(out y, out k) && !x.Equals(y))
          {
            var yExp = this.expManager.Encoder.VariableFor(y);
            if (k > 1)
            {
              Helper_Match_kY(x, oracleDomain, result, yExp);
              cache.Add(xExp, Cache_Entry.For(Cache_Entry.MatchCase.kY, x, yExp, k, default(Expression)));
            }
            else
            {
              cache.Add(xExp, Cache_Entry.ForNonLinear<Expression>());
            }
          }
        }
      }
      #endregion

      #region 2. Special treatment for reminder and non polynomial expressions

      result.AddRange(new TreatNonPolynomialCases(x, this.expManager).Visit(exp, oracleDomain));

      #endregion

      return result;
    }

    private void Helper_Match_kY(
      Expression x, INumericalAbstractDomainQuery<Variable, Expression> oracleDomain, List<Expression> result, Expression y)
    {
      Contract.Requires(result != null);

      Expression ltExp;
      if (oracleDomain.BoundsFor(y).LowerBound >= 1)
      {
        ltExp = this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, y, x);
        result.Add(ltExp);

        result.AddRange(UpperBounds(y, oracleDomain.UpperBoundsFor(x, false)));
      }
    }

    private IEnumerable<Expression> UpperBounds(Expression x, IEnumerable<Expression> iSet)
    {
      foreach (Expression upp in iSet)
      {
        yield return this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, x, upp);
      }
    }

    #endregion

    private class TreatNonPolynomialCases
      : GenericExpressionVisitor<INumericalAbstractDomainQuery<Variable, Expression>, Set<Expression>, Variable, Expression>
    {

      readonly private ExpressionManagerWithEncoder<Variable, Expression> expManager;

      private Expression x;

      public TreatNonPolynomialCases(Expression x, ExpressionManagerWithEncoder<Variable, Expression> expManager)
        : base(expManager.Decoder)
      {
        Contract.Requires(expManager != null);

        this.x = x;
        this.expManager = expManager;
      }

      public override Set<Expression> VisitModulus(Expression left, Expression right, Expression original, INumericalAbstractDomainQuery<Variable, Expression> data)
      {
        var result = new Set<Expression>();

        if (this.AreInfinities(left, right))
        {
          return result;
        }

        if (data.BoundsFor(right).LowerBound >= 0)
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, x, right));     // Then assume x < right
        }

        return result;
      }

      public override Set<Expression> VisitShiftRight(Expression left, Expression right, Expression original, INumericalAbstractDomainQuery<Variable, Expression> data)
      {
        if (this.AreInfinities(left, right))
        {
          return new Set<Expression>();
        }

        var bounds = data.BoundsFor(right).AsInterval;
        int val;
        Polynomial<Variable, Expression> pol;
        if (bounds.IsFiniteAndInt32Singleton(out val) && val > 0 &&
          Polynomial<Variable, Expression>.TryToPolynomialForm(left, this.Decoder, out pol) 
          && pol.IsLinear && !pol.Relation.HasValue && pol.Left.Length <= (1 << val))
        {
          var ub = null as IEnumerable<Expression>;

          foreach (var m in pol.Left)
          {
            Variable v;
            if (m.IsVariable(out v) && data.BoundsFor(v).LowerBound >= 0)
            {
              var upperbounds = data.UpperBoundsFor(v, true);
              ub = ub == null ? upperbounds : ub.Intersect(upperbounds);

              if (ub.Any())
              {
                continue;
              }
            }
            return new Set<Expression>();
          }

          // If we reach this point, we have meaningful constraints

          return new Set<Expression>(ub.ApplyToAll(exp => this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, x, exp)));
        }

        return new Set<Expression>();
      }

      public override Set<Expression> VisitSubtraction(Expression left, Expression right, Expression original, INumericalAbstractDomainQuery<Variable, Expression> data)
      {
        var result = new Set<Expression>();

        if (this.AreInfinities(left, right))
        {
          return result;
        }

        // It is x = left - right, 
        if (data.BoundsFor(right).LowerBound > 0)
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, x, left));     // Then assume x < left
        }
        // It is x = left - (- Abs(right))
        else if (data.BoundsFor(right).UpperBound < 0)
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, left, x));   // Then assume left < x
        }

        return result;
      }

      public override Set<Expression> VisitAddition(Expression left, Expression right, Expression original, INumericalAbstractDomainQuery<Variable, Expression> data)
      {
        var result = new Set<Expression>();

        if (this.AreInfinities(left, right))
        {
          return result;
        }

        // It is x = left + right, 
        if (data.BoundsFor(right).LowerBound > 0)
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, left, x));     // Then assume left < x 
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, this.Decoder.Stripped(left), x));
        }
        // It is x = left + (- Abs(right))
        else if (data.BoundsFor(right).UpperBound < 0)
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, x, left));   // The assume x < left
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, x, this.Decoder.Stripped(left)));
        }

        if (data.BoundsFor(left).LowerBound > 0)
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, right, x));     // Then assume right < x 
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, this.Decoder.Stripped(right), x));
        }
        // It is x = -(Abs(left)) + right
        else if (data.BoundsFor(left).UpperBound < 0)
        {
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, x, right));   // The assume x < right
          result.Add(this.expManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, x, this.Decoder.Stripped(right)));
        }

        return result;
      }

      protected override Set<Expression> Default(INumericalAbstractDomainQuery<Variable, Expression> data)
      {
        return new Set<Expression>();
      }

      private bool AreInfinities(Expression e1, Expression e2)
      {
        return this.Decoder.IsInfinity(e1) || this.Decoder.IsInfinity(e2);
      }
    }
  }

  internal class GreaterEqualThanZeroConstraints<Variable, Expression>
    : IConstraintsForAssignment<Variable, Expression, INumericalAbstractDomainQuery<Variable, Expression>>
  {
    private readonly ExpressionManagerWithEncoder<Variable, Expression> ExpressionManager;

    public GreaterEqualThanZeroConstraints(ExpressionManagerWithEncoder<Variable, Expression> expressionManager)
    {
      Contract.Requires(expressionManager != null);

      this.ExpressionManager = expressionManager;
    }

    #region IConstraintsForAssignment<Expression,Rational,AbstractDomain> Members

    public List<Expression> InferConstraints(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> adom)
    {
      Contract.Ensures(Contract.Result<List<Expression>>() != null);      

      var result = new List<Expression>();

      if (adom == null)
      {
        return result;
      }

      // This may appear straightforward and repeatitive, but sometimes the CheckIfxxx are more precise than what the abstract domain can track 
      // (because of the refinement of the expressions, and the fact that they are "goal directed")
      // As a consequence, we add explicitely this information to the abstract domain
      var isGeqZero = adom.CheckIfGreaterEqualThanZero(exp);

      if (isGeqZero.IsNormal())
      {
        var zero = this.ExpressionManager.Encoder.ConstantFor(0);
        if (isGeqZero.IsTrue())
        {
          // Discovered the disequality : 0 <= x 

          var newConstraint = this.ExpressionManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.GreaterEqualThan, x, zero);
          result.Add(newConstraint);
        }
        else
        {
          // Discovered the disequality : x < 0" 

          var newConstraint = this.ExpressionManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, x, zero);
          result.Add(newConstraint);
        }
      }

      return result;

    }
    #endregion

  }

  internal abstract class Cache_Entry
  {
    public enum MatchCase { NotLinear, YMinusK, YPlusK, kY }

    static public Cache_Entry<Expression> For<Expression>(MatchCase match_case, Expression x, Expression y, Rational k, Expression result)
    {
      Contract.Requires((object)k != null);
      Contract.Requires(match_case != MatchCase.NotLinear);

      return new Cache_Entry<Expression>(match_case, x, y, k, result);
    }

    static public Cache_Entry<Expression> ForNonLinear<Expression>()
    {
      return new Cache_Entry<Expression>(MatchCase.NotLinear, default(Expression), default(Expression), default(Rational), default(Expression));
    }
  }

  internal class Cache_Entry<Expression> : Cache_Entry
  {
    public readonly MatchCase Match_case;
    public readonly Expression X;
    public readonly Expression Y;
    public readonly Rational k;

    public readonly Expression ResultExpression;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(Match_case == MatchCase.NotLinear || !object.ReferenceEquals(k, null));
    }

    public Rational K
    {
      get
      {
        Contract.Requires(Match_case != MatchCase.NotLinear);
        Contract.Ensures((object)Contract.Result<Rational>() != null);

        return this.k;
      }
    }

    internal Cache_Entry(MatchCase match_case, Expression x, Expression y, Rational k, Expression resultExpression)
    {
      Contract.Requires(match_case == MatchCase.NotLinear || !object.ReferenceEquals(k, null));

      this.Match_case = match_case;
      this.X = x;
      this.Y = y;
      this.k = k;
      this.ResultExpression = resultExpression;
    }
  }
}