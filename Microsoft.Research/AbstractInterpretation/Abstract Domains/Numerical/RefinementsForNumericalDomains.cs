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

// Contains refinements for Pentagons, namely:
// + A generic abstract domain, parameterized by a numerical abstract domain, which keeps track of (linear) equalities
// + A generic abstract domain, on the top of the previous, to also keep track of LessEqualThan relations

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;
using DataStructures = Microsoft.Research.DataStructures;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  /// <summary>
  /// The reduced product Karr x NumericalDomain
  /// </summary>
  public class NumericalDomainWithKarr<Variable, Expression>
    : ReducedNumericalDomains<LinearEqualitiesEnvironment<Variable, Expression>, INumericalAbstractDomain<Variable, Expression>, Variable, Expression>
  {

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(trueTestVisitor != null);
      Contract.Invariant(falseTestVisitor != null);
      Contract.Invariant(checkIfHoldsVisitor != null);   
    }

    private const int MinStepsToEnableLessEqualThanClosure = -1; 
    private const int MinStepsToEnableLessThanClosure = 0;

    private int closureSteps;
    [ThreadStatic]
    private static int defaultClosureSteps;
    public static int DefaultClosureSteps
    {
      get
      {
        return defaultClosureSteps;
      }
      set
      {
        defaultClosureSteps = value;
      }
    }

    [ThreadStatic]
    private static int maxPairwiseEqualitiesInClosure;
    public static int MaxPairWiseEqualitiesInClosure
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        Contract.Assume(maxPairwiseEqualitiesInClosure >= 0);

        return maxPairwiseEqualitiesInClosure;
      }
      set
      {
        maxPairwiseEqualitiesInClosure = Math.Max(value, 0);
      }
    }

    private bool inRecursion = false;


    private RefinedTestTrueVisitor trueTestVisitor;
    private RefinedTestFalseVisitor falseTestVisitor;
    private RefinedCheckIfHoldsVisitor checkIfHoldsVisitor;

    #region Constructors

    public NumericalDomainWithKarr(
      LinearEqualitiesEnvironment<Variable, Expression> left, INumericalAbstractDomain<Variable, Expression> right,
      ExpressionManagerWithEncoder<Variable, Expression> expManager)
      : base(left, right, expManager)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      Contract.Requires(expManager != null);
      
      closureSteps = defaultClosureSteps;

      this.trueTestVisitor = new RefinedTestTrueVisitor(expManager);
      this.falseTestVisitor = new RefinedTestFalseVisitor(expManager.Decoder);
      this.checkIfHoldsVisitor = new RefinedCheckIfHoldsVisitor(expManager.Decoder);
      this.trueTestVisitor.FalseVisitor = this.falseTestVisitor;
      this.falseTestVisitor.TrueVisitor = this.trueTestVisitor;
    }

    #endregion

    #region Implementations of the abstract domain operations

    public override IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
    {
      return this.Right.LowerBoundsFor(v, strict);
    }

    public override IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
    {
      return this.Right.UpperBoundsFor(v, strict);
    }

    public override IAbstractDomain Join(IAbstractDomain a)
    {
      var other = a as NumericalDomainWithKarr<Variable, Expression>;

      Contract.Assume(other != null);
      return this.Join(other);
    }

    public IAbstractDomain Join(NumericalDomainWithKarr<Variable, Expression> other)
    {
      Contract.Requires(other != null);
      Contract.Ensures(Contract.Result<IAbstractDomain>() != null);

      NumericalDomainWithKarr<Variable, Expression> adom;
      if (AbstractDomainsHelper.TryTrivialJoin(this, other, out adom))
      {
        return adom;
      }

      // Propagate the equalities that we have in Karr
      var thisClosedRight = this.CloseRight();
      var otherClosedRight = other.CloseRight();

      var resultLeft = this.Left.Join(other.Left);
      var resultRight = thisClosedRight.Join(otherClosedRight) as INumericalAbstractDomain<Variable, Expression>;

      Contract.Assume(resultRight != null);

      return this.Factory(resultLeft, resultRight);
    }

    public override IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
    {
      var aDomain = base.TestTrue(guard) as NumericalDomainWithKarr<Variable, Expression>;
      return this.trueTestVisitor.Visit(guard, aDomain);
    }

    public override IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      var aDomain = base.TestFalse(guard) as NumericalDomainWithKarr<Variable, Expression>;
      return this.falseTestVisitor.Visit(guard, aDomain);
    }

    public override FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
    {
      var results = new FlatAbstractDomain<bool>[this.arr.Length];

      for (var i = 0; i < arr.Length; i++)
      {
        results[i] = this.arr[i].CheckIfGreaterEqualThanZero(exp);
      }

      return results[0].Meet(results[1]);
    }

    public override FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      return this.checkIfHoldsVisitor.Visit(exp, this);
    }

    public override FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
    {
      var results = new FlatAbstractDomain<bool>[this.arr.Length];

      for(var i = 0; i < arr.Length; i++)
      {
        results[i] = this.arr[i].CheckIfLessThan(e1, e2);
      }

      var result = results[0].Meet(results[1]);

      if (!result.IsTop)
      {
        return result;
      }

      return CheckIfLessWithClosure(e1, e2, result, ref inRecursion);
    }

    public override FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2)
    {
      // Skip Karr
      return this.arr[1].CheckIfLessThanIncomplete(e1, e2);

      /*
      var results = new FlatAbstractDomain<bool>[this.arr.Length];

      for (var i = 0; i < arr.Length; i++)
      {
        results[i] = this.arr[i].CheckIfLessThan(e1, e2);
      }

      return results[0].Meet(results[1]);
       */
    }

    public override FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
    {
      return Left.CheckIfLessThan_Un(e1, e2).Meet(Right.CheckIfLessThan_Un(e1, e2));
    }

    public override FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      var results = new FlatAbstractDomain<bool>[this.arr.Length];

      for(var i = 0; i < arr.Length; i++)
      {
        results[i] = this.arr[i].CheckIfLessEqualThan(e1, e2);
      }

      var result = results[0].Meet(results[1]);

      if (!result.IsTop)
      {
        return result;
      }

      return CheckIfLessEqualThanWithClosure(e1, e2);
    }

    public override FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
    {
      return Left.CheckIfLessEqualThan_Un(e1, e2).Meet(Right.CheckIfLessEqualThan_Un(e1, e2));
    }

    public override ReducedCartesianAbstractDomain<LinearEqualitiesEnvironment<Variable, Expression>, INumericalAbstractDomain<Variable, Expression>> Reduce(LinearEqualitiesEnvironment<Variable, Expression> left, INumericalAbstractDomain<Variable, Expression> right)
    {
      return this.Factory(left, right);
    }

    protected override ReducedCartesianAbstractDomain<LinearEqualitiesEnvironment<Variable, Expression>, INumericalAbstractDomain<Variable, Expression>> Factory(LinearEqualitiesEnvironment<Variable, Expression> left, INumericalAbstractDomain<Variable, Expression> right)
    {
      return new NumericalDomainWithKarr<Variable, Expression>(left, right, this.ExpressionManager);
    } 

    #endregion

    #region Private methods 

    private INumericalAbstractDomain<Variable, Expression> CloseRight()
    {
      var closedRight = this.Right;

      var count = 0;

      foreach (var eq in this.Left.PairWiseEqualities(MaxPairWiseEqualitiesInClosure))
      {
        Contract.Assume(eq.One != null);
        Contract.Assume(eq.Two != null);

        closedRight = closedRight.TestTrueEqual(eq.One, eq.Two);

        count++;
      }

      return closedRight;
    }


    /// <summary>
    /// Heuristic to determine if an expression is complex enough.
    /// At the moment, returns true iff e is in the form Bop(e, k) or Bop(k, e) and k > 1
    /// </summary>
    private bool IsComplexEnoughExpression(Expression e)
    {
      var decoder = this.ExpressionManager.Decoder;

      int k;
      return decoder.IsBinaryExpression(e) &&
        ((decoder.IsConstantInt(decoder.LeftExpressionFor(e), out k) && k > 1)
        ||
        (decoder.IsConstantInt(decoder.RightExpressionFor(e), out k) && k > 1))
        ;
    }


    private FlatAbstractDomain<bool> CheckIfLessWithClosure(Expression e1, Expression e2, FlatAbstractDomain<bool> result, ref bool inRecursion)
    {
      var decoder = this.ExpressionManager.Decoder;
      var encoder = this.ExpressionManager.Encoder;

      #region Equalities
      if (closureSteps >= 0)
      {
        // If we got no answer, we refine the expressions if we can

        // Get all the variables defined in the two expressions
        var vars = decoder.VariablesIn(e1);
        vars.AddRange(decoder.VariablesIn(e2));

        foreach (var equality in this.Left.PairWiseEqualities(Int32.MaxValue))
        {
          Expression e1Prime, e2Prime;

          if (vars.Contains(decoder.UnderlyingVariable(equality.One)))
          {
            e1Prime = encoder.Substitute(e1, equality.One, equality.Two);
            e2Prime = encoder.Substitute(e2, equality.One, equality.Two);
          }
          else if (vars.Contains(this.ExpressionManager.Decoder.UnderlyingVariable(equality.Two)))
          {
            e1Prime = encoder.Substitute(e1, equality.Two, equality.One);
            e2Prime = encoder.Substitute(e2, equality.Two, equality.One);
          }
          else
          {
            continue; // we skip it 
          }
          closureSteps--;
          var pair = Simplify(e1Prime, e2Prime, ExpressionOperator.LessThan);
          result = this.CheckIfLessThan(pair.One, pair.Two);
          closureSteps++;

          if (!result.IsTop)
          {
            return result;  // We found some answer
          }
        }
      }
      #endregion

      #region Transitive closure
      // This code below is very expensive, a main slowdown in Clousot.
      // By default we try not to execute it, and we force the execution either if it is indicated from the command line or if we have reason to believe that the inequality can be decided only using it
      //
      if (closureSteps > MinStepsToEnableLessThanClosure || (!inRecursion && (IsComplexEnoughExpression(e1) || IsComplexEnoughExpression(e2))))
      {
        // try to check against a lower bound for e2

        Polynomial<Variable, Expression> poly;
        var leftExp = e1;
        var rightExp = e2;

        if (Polynomial<Variable, Expression>.TryToPolynomialForm(
          ExpressionOperator.LessEqualThan, e1, e2, this.ExpressionManager.Decoder, out poly))
        {
          Variable x, y, z;
          Rational k;
          if (poly.TryMatch_XPlusYLess_Equal_ThanZPlusK(out x, out y, out z, out k))
          {
            var xExp = encoder.VariableFor(x);
            var yExp = encoder.VariableFor(y);
            var zExp = encoder.VariableFor(z);

            leftExp = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, xExp, yExp);
            if (k.IsZero)
            {
              rightExp = zExp;
            }
            else
            {
              rightExp = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, zExp, k.ToExpression<Variable, Expression>(encoder));
            }
          }

          if (poly.TryMatch_XLess_Equal_ThanYPlusZPlusK(out x, out y, out z, out k))
          {
            var xExp = encoder.VariableFor(x);
            var yExp = encoder.VariableFor(y);
            var zExp = encoder.VariableFor(z);

            leftExp = xExp;
            if (k.IsZero)
            {
              rightExp = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, yExp, zExp);
            }
            else
            {
              rightExp = encoder.CompoundExpressionFor(ExpressionType.Int32,
                ExpressionOperator.Addition, yExp,
                encoder.CompoundExpressionFor(ExpressionType.Int32,
                ExpressionOperator.Addition, zExp, k.ToExpression<Variable, Expression>(encoder)));
            }
          }
        }

        inRecursion = true;
        closureSteps--;

        foreach (var e in this.LowerBoundsFor(rightExp, true))
        {
          if (e.Equals(rightExp) || (this.LowerBoundsFor(e, true).Contains(rightExp)))
          {
            inRecursion = false;

            closureSteps++;
            return result.Top;
          }
          result = CheckIfLessEqualThan(leftExp, e);
          if (result.IsTrue())
          {
            inRecursion = false;
            closureSteps++;
            return result;
          }
        }

        foreach (var e in this.LowerBoundsFor(rightExp, false))
        {
          result = CheckIfLessThan(leftExp, e);
          if (result.IsTrue())
          {
            inRecursion = false;

            closureSteps++;
            return result;
          }
        }

        foreach (var e in this.UpperBoundsFor(leftExp, true))
        {
          if (e.Equals(leftExp) || (this.UpperBoundsFor(e, true).Contains(leftExp)))
          {
            inRecursion = false;

            closureSteps++;
            return result.Top;
          }
          result = CheckIfLessThan(e, rightExp);
          if (result.IsTrue())
          {
            inRecursion = false;

            closureSteps++;
            return result;
          }
        }

        foreach (var e in this.UpperBoundsFor(leftExp, false))
        {
          result = CheckIfLessThan(e, rightExp);
          if (result.IsTrue())
          {
            closureSteps++;
            return result;
          }
        }

        inRecursion = false;

        closureSteps++;
      }
      #endregion

      #region Special combinations

      switch (decoder.OperatorFor(e1))
      {
        case ExpressionOperator.Division:
          {
            var up = decoder.LeftExpressionFor(e1);
            var down = decoder.RightExpressionFor(e1);
            int k;
            // (a + b) / k, k > 1
            if (decoder.IsConstantInt(down, out k) && k > 1 && decoder.OperatorFor(up) == ExpressionOperator.Addition)
            {
              var a = decoder.LeftExpressionFor(up);
              var b = decoder.RightExpressionFor(up);

              if (this.CheckIfGreaterEqualThanZero(a).IsTrue() && this.CheckIfGreaterEqualThanZero(b).IsTrue())
              {
                // if a <= e2 and b < a then a + b / k < e2
                if ((this.CheckIfLessEqualThan(a, e2).IsTrue() && this.CheckIfLessThan(b, a).IsTrue()) ||
                  (this.CheckIfLessEqualThan(b, e2).IsTrue()) && this.CheckIfLessThan(a, b).IsTrue())
                {
                  return CheckOutcome.True;
                }
              }
            }
          }
          break;

        default:
          {
            break;
          }
      }

      #endregion

      return CheckOutcome.Top;
    }

    private FlatAbstractDomain<bool> CheckIfLessEqualThanWithClosure(Expression e1, Expression e2)
    {
      FlatAbstractDomain<bool> result;

      var decoder = this.ExpressionManager.Decoder;
      var encoder = this.ExpressionManager.Encoder;

      #region Domain Combination
      switch (decoder.OperatorFor(e2))
      {
        case ExpressionOperator.Multiplication:
          {
            // try to match e1 <= e21 * k, k > 0
            var e2Right = decoder.RightExpressionFor(e2);
            int k;
            if (decoder.IsConstantInt(e2Right, out k) && k > 0)
            {
              if (this.CheckIfGreaterEqualThanZero(e2Right).IsTrue())
              {
                var e2Left = decoder.LeftExpressionFor(e2);
                if (this.CheckIfLessEqualThan(e1, e2Left).IsTrue())
                {
                  return CheckOutcome.True;
                }
              }
            }
          }
          break;
      }
      #endregion

      #region Equalities
      if (closureSteps > MinStepsToEnableLessEqualThanClosure)
      { // If we got no answer, we refine the expressions if we can

        // Get all the variables defined in the two expressions
        var vars = decoder.VariablesIn(e1);
        vars.AddRange(decoder.VariablesIn(e2));

        foreach (var equality in this.Left.PairWiseEqualities(Int32.MaxValue))
        {
          Expression e1Prime, e2Prime;

          if (vars.Contains(decoder.UnderlyingVariable(equality.One)))
          {
            e1Prime = encoder.Substitute(e1, equality.One, equality.Two);
            e2Prime = encoder.Substitute(e2, equality.One, equality.Two);
          }
          else if (vars.Contains(decoder.UnderlyingVariable(equality.Two)))
          {
            e1Prime = encoder.Substitute(e1, equality.Two, equality.One);
            e2Prime = encoder.Substitute(e2, equality.Two, equality.One);
          }
          else
          {
            continue; // we skip it 
          }
          closureSteps--;

          var pair = Simplify(e1Prime, e2Prime, ExpressionOperator.LessEqualThan);
          result = this.CheckIfLessEqualThan(pair.One, pair.Two);

          closureSteps++;

          if (!result.IsTop)
          {
            return result;  // We found some answer
          }
        }
      }
      #endregion

      #region Closure
      if (closureSteps > MinStepsToEnableLessEqualThanClosure)
      { // put it into tractable form then try to check against a lower bound for e2 or an upper bound for e1

        Polynomial<Variable, Expression> poly;
        var leftExp = e1;
        var rightExp = e2;

        if (Polynomial<Variable, Expression>.TryToPolynomialForm(
          ExpressionOperator.LessEqualThan, e1, e2, decoder, out poly))
        {
          Variable x, y, z;
          Rational k;
          if (poly.TryMatch_XPlusYLess_Equal_ThanZPlusK(out x, out y, out z, out k))
          {
            var xExp = encoder.VariableFor(x);
            var yExp = encoder.VariableFor(y);
            var zExp = encoder.VariableFor(z);

            leftExp = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, xExp, yExp);
            if (k.IsZero)
            {
              rightExp = zExp;
            }
            else
            {
              rightExp = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, zExp, k.ToExpression<Variable, Expression>(encoder));
            }
          }
          if (poly.TryMatch_XLess_Equal_ThanYPlusZPlusK(out x, out y, out z, out k))
          {
            var xExp = encoder.VariableFor(x);
            var yExp = encoder.VariableFor(y);
            var zExp = encoder.VariableFor(z);

            leftExp = xExp;
            if (k.IsZero)
            {
              rightExp = encoder.CompoundExpressionFor(ExpressionType.Int32,
                ExpressionOperator.Addition, yExp, zExp);
            }
            else
            {
              rightExp = encoder.CompoundExpressionFor(ExpressionType.Int32,
                ExpressionOperator.Addition,
                yExp,
               encoder.CompoundExpressionFor(ExpressionType.Int32,
                ExpressionOperator.Addition, zExp, k.ToExpression<Variable, Expression>(encoder)));
            }
          }
        }

        closureSteps--;

        var lowerBounds = this.LowerBoundsFor(rightExp, false);

        foreach (var e in lowerBounds)
        {
          result = CheckIfLessEqualThan(leftExp, e);
          if (result.IsTrue())
          {
            closureSteps++;
            return result;
          }
        }

        var upperBounds = this.UpperBoundsFor(leftExp, false);

        foreach (var e in upperBounds)
        {
          result = CheckIfLessEqualThan(e, rightExp);
          if (result.IsTrue())
          {
            closureSteps++;
            return result;
          }
        }
        result = CheckOutcome.Top;
        closureSteps++;
      }
      #endregion

      return CheckOutcome.Top;
    }

    private Pair<Expression, Expression> Simplify(Expression e1, Expression e2, ExpressionOperator op)
    {
      Contract.Requires(op.IsBinary());

      var decoder = this.ExpressionManager.Decoder;
      var encoder = this.ExpressionManager.Encoder;

      var vars1 = decoder.VariablesIn(e1);
      var vars2 = decoder.VariablesIn(e2);
      
      var intersection = vars1.Intersection(vars2);

      if (intersection.IsEmpty)
      {
        return new Pair<Expression, Expression>(e1, e2);
      }

      Polynomial<Variable, Expression> pol;

      if (Polynomial<Variable, Expression>.TryToPolynomialForm(op, e1, e2, decoder, out pol))
      {
        Variable x, y;
        Rational k1, k2, k;
        if (op == ExpressionOperator.LessEqualThan && pol.TryMatch_k1XLessEqualThank2(out k1, out x, out k2))
        {
          var xExp = encoder.VariableFor(x);

          if (k1 == -1 && k2.IsInteger)
          {
            return new Pair<Expression, Expression>(encoder.ConstantFor((int)-k2), xExp);
          }
          if (k1 == 1 && k2.IsInteger)
          {
            return new Pair<Expression, Expression>(xExp, encoder.ConstantFor((int)k2));
          }
        }
        else if (op.IsLessThan() && pol.TryMatch_k1XLessThank2(out k1, out x, out k2))
        {
          var xExp = encoder.VariableFor(x);

          if (k1 == -1 && k2.IsInteger)
          {
            return new Pair<Expression, Expression>(encoder.ConstantFor((int)-k2), xExp);
          }
          if (k1 == 1 && k2.IsInteger)
          {
            return new Pair<Expression, Expression>(xExp, encoder.ConstantFor((int)k2));
          }
        }
        else if (op.IsLessEqualThan() && pol.TryMatch_k1XPlusk2YLessEqualThanK(out k1, out x, out k2, out y, out k) && k.IsZero)
        {
          var xExp = encoder.VariableFor(x);
          var yExp = encoder.VariableFor(y);

          if (k1 == -1 && k2 == 1)
          {
            return new Pair<Expression, Expression>(yExp, xExp);
          }
          if (k1 == 1 && k2 == -1)
          {
            return new Pair<Expression, Expression>(xExp, yExp);
          }
        }
        else if (op.IsLessThan() && pol.TryMatch_k1XPlusk2YLessThanK(out k1, out x, out k2, out y, out k) && k.IsZero)
        {
          var xExp = encoder.VariableFor(x);
          var yExp = encoder.VariableFor(y);
          
          if (k1 == -1 && k2 == 1)
          {
            return new Pair<Expression, Expression>(yExp, xExp);
          }
          if (k1 == 1 && k2 == -1)
          {
            return new Pair<Expression, Expression>(xExp, yExp);
          }
        }
      }
      return new Pair<Expression, Expression>(e1, e2);
    }

    #endregion

    #region Visitors for the expressions

    private class RefinedCheckIfHoldsVisitor 
      : CheckIfHoldsVisitor<NumericalDomainWithKarr<Variable, Expression>, Variable, Expression>
    {
      public RefinedCheckIfHoldsVisitor(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
      }

      public override FlatAbstractDomain<bool> VisitEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        var direct = base.VisitEqual(left, right, original, data);

        if (direct.IsNormal())
        {
          return direct;
        }

        var resultLeft = Domain.CheckIfLessEqualThan(left, right);

        // left <= right
        if (resultLeft.IsTrue())
        {
          var resultRight = Domain.CheckIfLessEqualThan(right, left);

          // left >= right
          if (resultLeft.IsTrue())
          {
            return resultRight;
          }
        }

        var notEq = this.VisitNotEqual(left, right, original, data);
        if (notEq.IsNormal() && notEq.IsTrue())
        {
          return CheckOutcome.False;
        }

        return resultLeft.Top;
      }

      public override FlatAbstractDomain<bool> VisitEqual_Obj(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        return VisitEqual(left, right, original, data);
      }

      public override FlatAbstractDomain<bool> VisitLessEqualThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        return Domain.CheckIfLessEqualThan(left, right);
      }

      public override FlatAbstractDomain<bool> VisitLessEqualThan_Un(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        return Domain.CheckIfLessEqualThan_Un(left, right);
      }

      public override FlatAbstractDomain<bool> VisitLessThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        return Domain.CheckIfLessThan(left, right);
      }

      public override FlatAbstractDomain<bool> VisitLessThan_Un(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        return Domain.CheckIfLessThan_Un(left, right);
      }

      public override FlatAbstractDomain<bool> VisitNotEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        int value;
        if (this.Decoder.IsNull(right) || (this.Decoder.TryValueOf<Int32>(right, ExpressionType.Int32, out value) && value == 0))
        { // "left != 0" means that left should hold
          var outcome = Domain.CheckIfHolds(left);

          if (!outcome.IsTop)
          {
            return outcome;
          }
        }

        var exp = this.Domain.ExpressionManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.NotEqual, left, right);

        // Shortcut: as the checking in Karr may be quite expensive, we first try to validate it in cheaper domains, and only if we cannot conclude, we try Karr
        var tryRight = Domain.Right.CheckIfHolds(exp);
        if (tryRight.IsTop)
        {
          return Domain.Left.CheckIfHolds(exp);
        }
        else
        {
          return tryRight;
        }
      }

      public override FlatAbstractDomain<bool> VisitVariable(Variable var, Expression original, FlatAbstractDomain<bool> data)
      {
        // Is there a domain out there that can prove that original != null?
        var directCheck = this.Domain.CheckIfNonZero(original);

        if (directCheck.IsNormal())
        {
          return directCheck;
        }

        // If not, let's collect a collective bound for the whole domain and see if it is != 0
        var valueForVar = this.Domain.BoundsFor(original);

        if (valueForVar.IsNormal())
        {
          if (valueForVar.LowerBound > 0 || valueForVar.UpperBound < 0)
          { // It is true...
            return CheckOutcome.True;
          }
          else if (valueForVar.IsSingleton && valueForVar.LowerBound.IsZero)
          { // It is false
            return CheckOutcome.False;
          }
        }
        return Default(data);
      }
    }

    private class RefinedTestTrueVisitor
      : TestTrueVisitor<NumericalDomainWithKarr<Variable, Expression>, Variable, Expression>
    {
      #region invariant

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.expManager != null);
      }
      
      #endregion

      #region Private state

      private readonly ExpressionManagerWithEncoder<Variable, Expression> expManager;

      #endregion

      public RefinedTestTrueVisitor(ExpressionManagerWithEncoder<Variable, Expression> expManager)
        : base(expManager.Decoder)
      {
        Contract.Requires(expManager != null);

        this.expManager = expManager;
      }

      public override NumericalDomainWithKarr<Variable, Expression> VisitEqual(Expression left, Expression right, Expression original, NumericalDomainWithKarr<Variable, Expression> data)
      {
        return data.TestTrueEqual(left, right) as NumericalDomainWithKarr<Variable, Expression>;
      }

      public override NumericalDomainWithKarr<Variable, Expression> VisitLessEqualThan(Expression left, Expression right, Expression original, NumericalDomainWithKarr<Variable, Expression> data)
      {
        Polynomial<Variable, Expression>  pol;

        if (!Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessEqualThan, left, right, this.expManager.Decoder, out pol))
        {
          return data;
        }


        var encoder = this.expManager.Encoder;

        var eq = encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, left, right);
        var isEq = data.CheckIfHolds(eq);
        if (isEq.IsNormal() && isEq.BoxedElement)
        {
          // We know Karr works with side effects
          /*data.Left = */ data.Left.TestTrue(eq);
        }

        Variable v1, v2, v3;
        Rational k;
        if (pol.TryMatch_XPlusYLess_Equal_ThanZPlusK(out v1, out v2, out v3, out k))
        {
          var freshExpression = encoder.FreshVariable<Expression>();
          var freshExpressionExp = encoder.VariableFor(freshExpression);

          var v1Exp = encoder.VariableFor(v1);
          var v2Exp = encoder.VariableFor(v2);
          var v3Exp = encoder.VariableFor(v3);

          var leftEnv =
            data.Left.TestTrueEqual(
              encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Subtraction,
              encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, v1Exp, v2Exp),
              encoder.ConstantFor((Int32)k)), freshExpressionExp);
          
          var rightEnv = data.Right.TestTrueLessEqualThan(freshExpressionExp, v3Exp); 
                      
          // it may be necessary to update the betaDep map

          return new NumericalDomainWithKarr<Variable, Expression>(leftEnv, rightEnv, data.ExpressionManager);
        }
        if (pol.TryMatch_XLess_Equal_ThanYPlusZPlusK(out v1, out v2, out v3, out k))
        {

          var freshExpression = encoder.FreshVariable<Expression>();
          var freshExpressionExp = encoder.VariableFor(freshExpression);

          var v1Exp = encoder.VariableFor(v1);
          var v2Exp = encoder.VariableFor(v2);
          var v3Exp = encoder.VariableFor(v3);

          var leftEnv = data.Left.TestTrueEqual(encoder.CompoundExpressionFor(ExpressionType.Int32,
            ExpressionOperator.Addition,
              encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, v2Exp, v3Exp),
              encoder.ConstantFor((Int32)k)), freshExpressionExp);
         
          var rightEnv = data.Right.TestTrueLessEqualThan(v1Exp, freshExpressionExp);

          // it may be necessary to update the betaDep map
          return new NumericalDomainWithKarr<Variable, Expression>(leftEnv, rightEnv, data.ExpressionManager);
        }
        return data;
      }

      public override NumericalDomainWithKarr<Variable, Expression> VisitLessThan(Expression left, Expression right, Expression original, 
        NumericalDomainWithKarr<Variable, Expression> data)
      {
        Polynomial<Variable, Expression> pol;

        if (!Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessThan, left, right, this.expManager.Decoder, out pol))
        {
          return data;
        }

        var encoder = this.expManager.Encoder;

        Variable v1, v2, v3;
        Rational k;
        if (pol.TryMatch_XPlusYLess_Equal_ThanZPlusK(out v1, out v2, out v3, out k))
        {
          var freshExpression = encoder.FreshVariable<Expression>();
          var freshExpressionExp = encoder.VariableFor(freshExpression);

          var v1Exp = encoder.VariableFor(v1);
          var v2Exp = encoder.VariableFor(v2);
          var v3Exp = encoder.VariableFor(v3);

          var leftEnv = data.Left.TestTrueEqual(
              encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Subtraction,
                  encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, 
                  v1Exp, v2Exp), 
                  encoder.ConstantFor((Int32)k)), 
                freshExpressionExp);

          var rightEnv = data.Right.TestTrueLessThan(freshExpressionExp, v3Exp);

          // it may be necessary to update the betaDep map

          return new NumericalDomainWithKarr<Variable, Expression>(leftEnv, rightEnv, data.ExpressionManager);
        }

        if (pol.TryMatch_XLess_Equal_ThanYPlusZPlusK(out v1, out v2, out v3, out k))
        {
          var freshExpression = encoder.FreshVariable<Expression>();
          var freshExpressionExp = encoder.VariableFor(freshExpression);

          var v1Exp = encoder.VariableFor(v1);
          var v2Exp = encoder.VariableFor(v2);
          var v3Exp = encoder.VariableFor(v3);

          var leftEnv =
            data.Left.TestTrueEqual(encoder.CompoundExpressionFor(ExpressionType.Int32,
              ExpressionOperator.Addition, encoder.CompoundExpressionFor(ExpressionType.Int32,
              ExpressionOperator.Addition, v2Exp, v3Exp), encoder.ConstantFor((Int32)k)), freshExpressionExp);

          var rightEnv = data.Right.TestTrueLessThan(v1Exp, freshExpressionExp);

          // it may be necessary to update the betaDep map
          return new NumericalDomainWithKarr<Variable, Expression>(leftEnv, rightEnv, data.ExpressionManager);
        }
        return data;
      }

      public override NumericalDomainWithKarr<Variable, Expression> VisitNotEqual(Expression left, Expression right, Expression original, NumericalDomainWithKarr<Variable, Expression> data)
      {
        return data;
      }

      public override NumericalDomainWithKarr<Variable, Expression> VisitVariable(Variable var, Expression original,  NumericalDomainWithKarr<Variable, Expression> data)
      {
        return data;
      }
    }

    private class RefinedTestFalseVisitor : TestFalseVisitor< NumericalDomainWithKarr<Variable, Expression>, Variable, Expression>
    {
      public RefinedTestFalseVisitor(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
        Contract.Requires(decoder != null);
      }

      public override NumericalDomainWithKarr<Variable, Expression> VisitVariable(Variable var, Expression original, NumericalDomainWithKarr<Variable, Expression> data)
      {
        return data;
      }
    }

    #endregion
  }

  public class RefinedWithOctagons<RightDomain, Variable, Expression>
    : ReducedNumericalDomains<OctagonEnvironment<Variable, Expression>, RightDomain, Variable, Expression>
    where RightDomain : class, INumericalAbstractDomain<Variable, Expression>
  {
    public RefinedWithOctagons(OctagonEnvironment<Variable, Expression> octagon, RightDomain otherDomain,
    ExpressionManagerWithEncoder<Variable, Expression> expManager)
      : base(octagon, otherDomain, expManager)
    {
    }

    public override IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
    {
      return this.Right.LowerBoundsFor(v, strict); // not implemented for octagons
    }

    public override IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
    {
      return this.Right.UpperBoundsFor(v, strict); // not implemented for octagons
    }

    public override FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
    {
      var tryOctagons = this.Left.CheckIfGreaterEqualThanZero(exp);

      if (tryOctagons.IsTop)
      {
        return this.Right.CheckIfGreaterEqualThanZero(exp);
      }
      else
      {
        return tryOctagons;
      }
    }

    public override FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
    {
      var tryOctagons = this.Left.CheckIfLessThan(e1, e2);

      if (tryOctagons.IsTop)
      {
        return this.Right.CheckIfLessThan(e1, e2);
      }
      else
      {
        return tryOctagons;
      }
    }
    public override FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
    {
      return this.Left.CheckIfLessThan_Un(e1, e2).Meet(this.Right.CheckIfLessThan_Un(e1, e2));
    }   

    public override FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      var tryOctagons = this.Left.CheckIfLessEqualThan(e1, e2);

      if (tryOctagons.IsTop)
      {
        return this.Right.CheckIfLessEqualThan(e1, e2);
      }
      else
      {
        return tryOctagons;
      }
    }

    public override FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
    {
      return this.Left.CheckIfLessEqualThan_Un(e1, e2).Meet(this.Right.CheckIfLessEqualThan_Un(e1, e2));
    }

    public override ReducedCartesianAbstractDomain<OctagonEnvironment<Variable, Expression>, RightDomain> Reduce(OctagonEnvironment<Variable, Expression> left, RightDomain right)
    {
      return this.Factory(left, right);
    }

    protected override ReducedCartesianAbstractDomain<OctagonEnvironment<Variable, Expression>, RightDomain> Factory(OctagonEnvironment<Variable, Expression> left, RightDomain right)
    {
      return new RefinedWithOctagons<RightDomain, Variable, Expression>(left, right, this.ExpressionManager);
    }
  }

  public class RefinedGeneric<LeftDomain, RightDomain, Variable, Expression>
    : ReducedNumericalDomains<LeftDomain, RightDomain, Variable, Expression>
    where LeftDomain : class, INumericalAbstractDomain<Variable, Expression>
    where RightDomain : class, INumericalAbstractDomain<Variable, Expression>
  {
    #region Constructor

    public RefinedGeneric(LeftDomain left, RightDomain right,
      ExpressionManagerWithEncoder<Variable, Expression> expManager)
      : base(left, right, expManager)
    { }
    #endregion

    #region Implementation of AssignInterval

    // In the base we did not had any type for the Left argument (N1), so we could not propagate the assignment. 
    // But here we know its type, so we can propagate the information
    public override void AssumeInDisInterval(Variable x, DisInterval value)
    {
      Left.AssumeInDisInterval(x, value);
      Right.AssumeInDisInterval(x, value);
    }
    #endregion

    #region Override for To<T>

    public override T To<T>(IFactory<T> factory)
    {
      var leftRefined = (LeftDomain)this.Left.RemoveRedundanciesWith(this.Right);

      var left = leftRefined.To(factory);
      var right = this.Right.To(factory);

      return factory.And(left, right);

    }
    #endregion

    #region Implementation of abstract methods

    public override FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
    {
      return this.Left.CheckIfGreaterEqualThanZero(exp).Meet(this.Right.CheckIfGreaterEqualThanZero(exp));
    }

    public override FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
    {
      return this.Left.CheckIfLessThan(e1, e2).Meet(this.Right.CheckIfLessThan(e1, e2));
    }

    public override FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      return this.Left.CheckIfLessEqualThan(e1, e2).Meet(this.Right.CheckIfLessEqualThan(e1, e2));
    }

    public override ReducedCartesianAbstractDomain<LeftDomain, RightDomain> Reduce(LeftDomain left, RightDomain right)
    {
      return new RefinedGeneric<LeftDomain, RightDomain, Variable, Expression>(left, right, this.ExpressionManager);
    }

    protected override ReducedCartesianAbstractDomain<LeftDomain, RightDomain> Factory(LeftDomain left, RightDomain right)
    {
      return new RefinedGeneric<LeftDomain, RightDomain, Variable, Expression>(left, right, this.ExpressionManager);
    }

    #endregion
  }

  public class RefinedWithIntervalsIEEE<RightDomain, Variable, Expression>
    : ReducedNumericalDomains<IntervalEnvironment_IEEE754<Variable, Expression>, RightDomain, Variable, Expression>
    where RightDomain : class, INumericalAbstractDomain<Variable, Expression>
  {

    public RefinedWithIntervalsIEEE(IntervalEnvironment_IEEE754<Variable, Expression> left, RightDomain right,
      ExpressionManagerWithEncoder<Variable, Expression> expManager)
      : base(left, right, expManager)
    {
    }

    public override void AssumeInDisInterval(Variable x, DisInterval value)
    {
      this.Left.AssumeInDisInterval(x, value);
      this.Right.AssumeInDisInterval(x, value);
    }

    public override FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
    {
      if (this.Left.CheckIfGreaterEqualThanZero(exp).IsTrue())
        return CheckOutcome.True;

      return this.Right.CheckIfGreaterEqualThanZero(exp);
    }

    public override FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
    {
      if (this.Left.CheckIfLessThan(e1, e2).IsTrue())
        return CheckOutcome.True;

      return this.Right.CheckIfLessThan(e1, e2);
    }

    public override FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      if (this.Left.CheckIfLessEqualThan(e1, e2).IsTrue())
        return CheckOutcome.True;

      return this.Right.CheckIfLessEqualThan(e1, e2);
    }

    public override ReducedCartesianAbstractDomain<IntervalEnvironment_IEEE754<Variable, Expression>, RightDomain> 
      Reduce(IntervalEnvironment_IEEE754<Variable, Expression> left, RightDomain right)
    {
      return new RefinedWithIntervalsIEEE<RightDomain, Variable, Expression>(left, right, this.ExpressionManager);
    }

    protected override ReducedCartesianAbstractDomain<IntervalEnvironment_IEEE754<Variable, Expression>, RightDomain> Factory(IntervalEnvironment_IEEE754<Variable, Expression> left, RightDomain right)
    {
      return new RefinedWithIntervalsIEEE<RightDomain, Variable, Expression>(left, right, this.ExpressionManager);
    }
  }

  public class RefinedWithFloatTypes<RightDomain, Variable, Expression>
    : ReducedNumericalDomains<FloatTypes<Variable, Expression>, RightDomain, Variable, Expression>
    where RightDomain : class, INumericalAbstractDomain<Variable, Expression>
  {

    public RefinedWithFloatTypes(FloatTypes<Variable, Expression> left, RightDomain right, 
      ExpressionManagerWithEncoder<Variable, Expression> expManager)
      : base(left, right, expManager)
    {
      Contract.Requires(expManager != null);
    }

    public override FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
    {
      return this.Left.CheckIfGreaterEqualThanZero(exp).Meet(this.Right.CheckIfGreaterEqualThanZero(exp));
    }

    public override FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
    {
      return this.Left.CheckIfLessThan(e1, e2).Meet(this.Right.CheckIfLessThan(e1, e2));
    }

    public override FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      return this.Left.CheckIfLessEqualThan(e1, e2).Meet(this.Right.CheckIfLessEqualThan(e1, e2));
    }

    public override ReducedCartesianAbstractDomain<FloatTypes<Variable, Expression>, RightDomain> Reduce(FloatTypes<Variable, Expression> left, RightDomain right)
    {
      return this.Factory(left, right);
    }

    protected override ReducedCartesianAbstractDomain<FloatTypes<Variable, Expression>, RightDomain> Factory(FloatTypes<Variable, Expression> left, RightDomain right)
    {
      return new RefinedWithFloatTypes<RightDomain, Variable, Expression>(left, right, this.ExpressionManager);
    }
  }
}
