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

// The implementation of the environment of intervals

using System;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  /// <summary>
  /// An interval environment is a map from Variables to intervals
  /// </summary>

  public sealed class IntervalEnvironment<Variable, Expression> :
      IntervalEnvironment_Base<IntervalEnvironment<Variable, Expression>, Variable, Expression, Interval, Rational>,
    INumericalAbstractDomain<Variable, Expression>
  {
    #region Constructors

    internal IntervalEnvironment(IExpressionDecoder<Variable, Expression> Decoder, Logger Log)
      : this(new ExpressionManager<Variable, Expression>(null, Decoder, null), true)
    {
    }

    public IntervalEnvironment(ExpressionManager<Variable, Expression> expManager)
      : this(expManager, false)
    {
      Contract.Requires(expManager != null);
    }

    private IntervalEnvironment(IntervalEnvironment<Variable, Expression> original)
      : base(original)
    {
    }

    private IntervalEnvironment(ExpressionManager<Variable, Expression> expManager, bool internalInvocation)
      : base(expManager)
    {
    }

    #endregion
    
    #region IPureExpressionAssignmentsWithBackward<Expression> Members

    /// <summary>
    /// The backward assignment for the intervals.
    /// It modifies the state "in loco", hence with side effects
    /// </summary>
    /// <param name="x">The variable to be backward assigned</param>
    /// <param name="exp">The expression</param>
    public void AssignBackward(Variable/*!*/ x, Expression/*!*/ exp)
    {
      //var postState = this.DuplicateMe();

      //// For all the variables "y" in the expression "exp", we try to revert the assignment, so to find the previous value for "y"
      //foreach (var y in this.ExpressionManager.Decoder.UnderlyingVariablesIn(exp))
      //{
      //  Debug.Assert(y != null);
      //  Expression backexpression;

      //  // If it can invert the expression
      //  if (ExpressionInverter<Expression>.TryInvertAssignment(x, exp, y, this.ExpressionManager.Decoder, this.Encoder, out backexpression))
      //  {
      //    Polynomial<Variable, Expression> tmpPoly;
      //    if (!Polynomial<Variable, Expression>.TryToPolynomialForm(backexpression, this.ExpressionManager.Decoder, out tmpPoly))
      //    {
      //      break;
      //    }

      //    Expression normalizedBackExpression = tmpPoly.ToPureExpression(this.Encoder);

      //    var previousValueForY = postState.Eval(normalizedBackExpression);

      //    if (y.Equals(x))
      //    {
      //      this[y] = previousValueForY;            // We srongly update the value of "x", as its previous values is meaningless
      //    }
      //    else
      //    {
      //      this[y] = previousValueForY.Meet(this[y]);             // the previous value for y is the one in the poststate intersect the one "inverted"              
      //    }
      //  }
      //  else
      //  {   // It cannot invert the expression, so we skip it
      //    this.ProjectVariable(y);
      //  }
      //}

      //if (!this.ExpressionManager.Decoder.UnderlyingVariablesIn(exp).Contains(x))
      //{   // The previous value for x is lost
      //  this.ProjectVariable(x);
      //}
    }

    #endregion

    #region Code for handling the guards

    /// <summary>
    /// Assume exp \geq 0
    /// </summary>
    override public IntervalEnvironment<Variable, Expression> TestTrueGeqZero(Expression exp)
    {
      var newConstraints = IntervalInference.InferConstraints_GeqZero(exp, this.ExpressionManager.Decoder, this);

      foreach (var pair in newConstraints)
      {
        this[pair.One] = pair.Two;
      }

      return this;
    }

    public override IntervalEnvironment<Variable, Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2)
    {
      var isSignedComparison = true;
      var decoder = this.ExpressionManager.Decoder;

      var type1 = decoder.TypeOf(exp1); 
      var type2 = decoder.TypeOf(exp2);
      if ((type1 != ExpressionType.Unknown && type2 != ExpressionType.Unknown) && 
        (type1.IsUnsignedType() || type2.IsUnsignedType()))
      {
        isSignedComparison = false;
      }

      return HelperForTestTrueLessEqualThanSignedOrUnsigned(isSignedComparison, exp1, exp2);
    }

    public override IntervalEnvironment<Variable, Expression> TestTrueLessEqualThan_Un(Expression exp1, Expression exp2)
    {
      return HelperForTestTrueLessEqualThanSignedOrUnsigned(false, exp1, exp2);
    }

    /// <summary>
    /// We handle just expressions (that after being normalized) become 
    ///     "a * x \leq b ", or "a * x + b* y \leq c" 
    /// where {a, b, c} are constants and {x, y} are variables
    /// </summary>
    private IntervalEnvironment<Variable, Expression> HelperForTestTrueLessEqualThanSignedOrUnsigned(bool isSigned, Expression left, Expression right)
    {
      bool isBottom;
      var result = IntervalInference.InferConstraints_Leq(isSigned, left, right, this.ExpressionManager.Decoder, this, out isBottom);

      if (isBottom)
      {
        return this.Bottom;
      }

      foreach (var pair in result)
      {
        this.RefineWith(pair.One, pair.Two);
      }

      return this;
    }

    public override IntervalEnvironment<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
    {
      return HelperForTestTrueLessThanSignedOrUnsigned(true, exp1, exp2);
    }

    public override IntervalEnvironment<Variable, Expression> TestTrueLessThan_Un(Expression exp1, Expression exp2)
    {
      return HelperForTestTrueLessThanSignedOrUnsigned(false, exp1, exp2);
    }

    /// <summary>
    /// We handle just expressions (that after being normalized) become "  a * x \lt b ", where {a, b} are constants and "x" is a variable
    /// </summary>
    private IntervalEnvironment<Variable, Expression> HelperForTestTrueLessThanSignedOrUnsigned(bool isSigned, Expression left, Expression right)
    {
      bool isBottom;
      var constraints = IntervalInference.InferConstraints_LT(isSigned, left, right, this.ExpressionManager.Decoder, this, out isBottom);

      if (isBottom)
      {
        return this.Bottom;
      }

      foreach (var pair in constraints)
      {
        this.RefineWith(pair.One, pair.Two);
      }
      return this;
    }

    public override IntervalEnvironment<Variable, Expression> TestNotEqual(Expression e1, Expression e2)
    {
      bool isBottomLT, isBottomGT;
      List<Pair<Variable, Interval>> constraintsLT, constraintsGT;

      IntervalInference.InferConstraints_NotEq(e1, e2, this.ExpressionManager.Decoder, this, 
        out constraintsLT, out constraintsGT, out isBottomLT, out isBottomGT);
      
      if (isBottomLT)
      {
        // Bottom join Bottom = Bottom
        if (isBottomGT)
        {
          return this.Bottom;
        }
        this.TestTrueListOfFacts(constraintsGT);
      }
      else if (isBottomGT)
      {
        this.TestTrueListOfFacts(constraintsLT);
      }
      else
      {
        var join = JoinConstraints(constraintsLT, constraintsGT);

        this.TestTrueListOfFacts(join);
      }

      return this;
    }


    /// <summary>
    /// Special case for <code>guard != 0</code>
    /// Note: It works with side-effects on <code>this</code>!!!
    /// </summary>
    override protected IntervalEnvironment<Variable, Expression> TestNotEqualToZero(Expression guard)
    {
      var v = Eval(guard);
      Interval newConstraint;

      if (v.LowerBound.IsZero)
      {
        newConstraint = Interval.For(1, v.UpperBound);
      }
      else if (v.UpperBound.IsZero)
      {
        newConstraint = Interval.For(v.LowerBound, -1);
      }
      else
      {
        newConstraint = Interval.UnknownInterval;
      }

      var tmp = v.Meet(newConstraint);
      var guardVar = this.ExpressionManager.Decoder.UnderlyingVariable(guard);

      if (tmp.IsTop)
      {  
        this.RemoveElement(guardVar);
      }
      else
      {
        this[guardVar] = tmp;
      }

      return this;
    }

    protected override IntervalEnvironment<Variable, Expression> TestNotEqualToZero(Variable v)
    {
      // cannot capture v != 0
      return this;
    }

    protected override IntervalEnvironment<Variable, Expression> TestEqualToZero(Variable v)
    {
      var intv = Interval.For(0);
      Interval prevVal;
      if (this.TryGetValue(v, out prevVal))
      {
        intv = prevVal.Meet(intv);
      }

      this[v] = intv;

      return this;
    }

    /// <summary>
    /// Assume that <code>k \lt right</code>
    /// </summary>
    protected override void AssumeKLessThanRight(Interval k, Variable right)
    {
      Interval refined;
      if(IntervalInference.TryRefine_KLessThanRight(true, k, right, Rational.For(1), this, out refined))
      {
        this[right] = refined;
      }
    }

    /// <summary>
    /// Assume that <code>left \t k</code>
    /// </summary>
    protected override void AssumeLeftLessThanK(Variable left, Interval k)
    {
      Interval refined;
      if (IntervalInference.TryRefine_LeftLessThanK(true, left, k, this, out refined))
      {
        this[left] = refined;
      }
    }

    #endregion

    #region Private, Helper Methods, and Uninteresting code

    protected override IntervalEnvironment<Variable, Expression> Factory()
    {
      return new IntervalEnvironment<Variable, Expression>(this.ExpressionManager);
    }

    protected override IntervalEnvironment<Variable, Expression> NewInstance(ExpressionManager<Variable, Expression> expManager)
    {
      return new IntervalEnvironment<Variable, Expression>(expManager);
    }

    override protected IntervalEnvironment<Variable, Expression> DuplicateMe()
    {
      return new IntervalEnvironment<Variable, Expression>(this);
    }

    #endregion

    #region Internal methods for SubPolyhedra

    internal Interval EvalArray(SparseRationalArray array, Dictionary<int, Variable> dimsToVar, int keyForConstant)
    {
      Interval i;
      int j, k;
      return EvalArray(array, dimsToVar, keyForConstant, out i, out j, out k);
    }

    /// <summary>
    /// Evaluating a row from Karr with the map <paramref name="varsToDimensions"/>. Given a row a1*x1 + ... + an*xn = b, it evaluates a1*x1 + ... + an*xn - b
    /// </summary>
    /// <param name="array">The row to evaluate</param>
    /// <param name="varsToDimensions">The bijective map from variables to positions</param>
    /// <param name="keyForConstant">The last index of the row</param>
    internal Interval EvalArray(SparseRationalArray array, Dictionary<int, Variable> dimsToVar,
      int keyForConstant, out Interval finiteBounds, out int infLower, out int infUpper)
    {
      infLower = 0;
      infUpper = 0;
      finiteBounds = Interval.For(0);
      var tmpValue = Interval.For(0);

      foreach (var pair in array.GetElements())
      {
        Interval tmpInt;
        if (pair.Key == keyForConstant)
        {
          tmpInt = Interval.For(-pair.Value); // take the opposite as we want to evaluate f(x) = 0
        }
        else if (!this.TryGetValue(dimsToVar[pair.Key], out tmpInt))
        {
          tmpInt = Interval.UnknownInterval;
        }
        else
        {
          tmpInt = pair.Value * tmpInt;
        }
        tmpValue = tmpValue + tmpInt;

        var newLower = finiteBounds.LowerBound;
        var newUpper = finiteBounds.UpperBound;

        if (tmpInt.UpperBound.IsPlusInfinity)
        {
          infUpper++;
        }
        else
        {
          if (!Rational.TryAdd(newUpper, tmpInt.UpperBound, out newUpper))
          {
            newUpper = Rational.PlusInfinity;
          }
        }
        if (tmpInt.LowerBound.IsMinusInfinity)
        {
          infLower++;
        }
        else
        {
          if (!Rational.TryAdd(newLower, tmpInt.LowerBound, out newLower))
          {
            newLower = Rational.MinusInfinity;
          }
        }
        finiteBounds = Interval.For(newLower, newUpper);
      }

      return tmpValue;
    }


    /// <summary>
    /// This method returns a set of polynomials which contains constraints in the convex hull of the projections of the two elements on two-dimensional planes
    /// </summary>
    /// <param name="other">The other element with which we want to join</param>
    /// <param name="slack">A slack variable to insert in the polynomials</param>
    [ContractVerification(false)] // The analysis of this method takes forever. Should consider refactoring
    internal Set<Polynomial<Variable, Expression>> ConvexHullHelper(
      IntervalEnvironment<Variable, Expression> other, Variable slack, SubPolyhedra.JoinConstraintInference inference)
    {
      Contract.Requires(other != null);

      var result = new Set<Polynomial<Variable, Expression>>();

      var commonVariables = this.VariablesNonSlack.SetIntersection(other.VariablesNonSlack);

      var oct = false;

      if (commonVariables.Count <= SubPolyhedra.MaxVariablesInOctagonsConstraintInference)
      {
        oct = true;
      }
      else if (inference == SubPolyhedra.JoinConstraintInference.Standard)
      {
        return result;
      }

      bool CH = 
        inference == SubPolyhedra.JoinConstraintInference.CHOct 
        || inference == SubPolyhedra.JoinConstraintInference.ConvexHull2D;

      oct = 
        oct 
        || inference == SubPolyhedra.JoinConstraintInference.CHOct 
        || inference == SubPolyhedra.JoinConstraintInference.Octagons;
      
      int i = 0, j = 0;

      var this_Bounds = new Dictionary<Variable, Interval>();
      var other_Bounds = new Dictionary<Variable, Interval>();

      foreach (var e1 in commonVariables)
      {
        i++;

        Interval e1Left, e1Right;

        if(
          !TryEvalWithCache(this, e1, this_Bounds, out e1Left) 
          || !TryEvalWithCache(other, e1, other_Bounds, out e1Right))
        {
          continue;
        }

        j = 0;
        foreach (var e2 in commonVariables)
        {
          j++;

          // either e1 == e2 or we already have relations between e1 and e2
          if (e1.Equals(e2) || j <= i)
          {
            continue;
          }

          Interval e2Left, e2Right;
          if (
            !TryEvalWithCache(this, e2, this_Bounds, out e2Left) 
            || !TryEvalWithCache(other, e2, other_Bounds, out e2Right))
          {
            continue;
          }

          if (oct)
          {
            #region Adding octogonal constraints to get at least as much precision as octagons

            var monomials = new Monomial<Variable>[] { new Monomial<Variable>(e1), new Monomial<Variable>(-1, e2), new Monomial<Variable>(slack) };

            Polynomial<Variable, Expression> pol;
            if (!Polynomial<Variable, Expression>.TryToPolynomialForm(true, monomials, out pol))
            {
              throw new AbstractInterpretationException("Impossible case");
            }

            result.Add(pol);

            #endregion
          }


          if (CH)
          {
            #region Convex Hull

            // adaptation of the Monotone Chain algorithm

            var vertices = new PairNonNull<Rational, Rational>[8] 
          { new PairNonNull<Rational, Rational>(e1Left.LowerBound, e2Left.LowerBound),
            new PairNonNull<Rational, Rational>(e1Left.LowerBound, e2Left.UpperBound),
            new PairNonNull<Rational, Rational>(e1Left.UpperBound, e2Left.LowerBound),
            new PairNonNull<Rational, Rational>(e1Left.UpperBound, e2Left.UpperBound),
            new PairNonNull<Rational, Rational>(e1Right.LowerBound, e2Right.LowerBound),
            new PairNonNull<Rational, Rational>(e1Right.LowerBound, e2Right.UpperBound),
            new PairNonNull<Rational, Rational>(e1Right.UpperBound, e2Right.LowerBound),
            new PairNonNull<Rational, Rational>(e1Right.UpperBound, e2Right.UpperBound)
          };

            try
            {
              Array.Sort(vertices,
                delegate(PairNonNull<Rational, Rational> x, PairNonNull<Rational, Rational> y)
                {
                  if ((x.One - y.One).Sign == 0)
                    return (x.Two - y.Two).Sign;
                  else
                    return (x.One - y.One).Sign;
                });

            }
            catch (InvalidOperationException)
            {
              return result;
            }

            var IsInHull = new bool[8];
            try
            {
              Polynomial<Variable, Expression> pol;
              #region Computation of the Lower Hull
              IsInHull[0] = IsFinite(vertices[0]);
              IsInHull[2] = IsFinite(vertices[2]);
              IsInHull[4] = IsFinite(vertices[4]);

              if (IsInHull[0] && IsInHull[2] && IsInHull[4]
                && (vertices[2].One - vertices[0].One) * (vertices[4].Two - vertices[0].Two) <= (vertices[4].One - vertices[0].One) * (vertices[2].Two - vertices[0].Two))
              {
                IsInHull[2] = false;
              }

              IsInHull[6] = IsFinite(vertices[6]);

              if (IsInHull[2] && IsInHull[4] && IsInHull[6]
                && (vertices[4].One - vertices[2].One) * (vertices[6].Two - vertices[2].Two) <= (vertices[6].One - vertices[2].One) * (vertices[4].Two - vertices[2].Two))
              {
                IsInHull[4] = false;
              }

              if (IsInHull[0] && IsInHull[4] && IsInHull[6]
                && (vertices[4].One - vertices[0].One) * (vertices[6].Two - vertices[0].Two) <= (vertices[6].One - vertices[0].One) * (vertices[4].Two - vertices[0].Two))
              {
                IsInHull[4] = false;
              }

              if (IsInHull[0] && IsInHull[2] && IsInHull[6]
                && (vertices[2].One - vertices[0].One) * (vertices[6].Two - vertices[0].Two) <= (vertices[6].One - vertices[0].One) * (vertices[2].Two - vertices[0].Two))
              {
                IsInHull[2] = false;
              }
              #endregion

              #region Computation of the Upper Hull
              IsInHull[1] = IsFinite(vertices[1]);

              IsInHull[3] = IsFinite(vertices[3]);

              IsInHull[5] = IsFinite(vertices[5]);

              if (IsInHull[1] && IsInHull[3] && IsInHull[5]
                && (vertices[3].One - vertices[1].One) * (vertices[5].Two - vertices[1].Two) >= (vertices[5].One - vertices[1].One) * (vertices[3].Two - vertices[1].Two))
              {
                IsInHull[3] = false;
              }

              IsInHull[7] = IsFinite(vertices[7]);

              if (IsInHull[3] && IsInHull[5] && IsInHull[7]
                && (vertices[5].One - vertices[3].One) * (vertices[7].Two - vertices[3].Two) >= (vertices[7].One - vertices[3].One) * (vertices[5].Two - vertices[3].Two))
              {
                IsInHull[5] = false;
              }

              if (IsInHull[1] && IsInHull[5] && IsInHull[7]
                && (vertices[5].One - vertices[1].One) * (vertices[7].Two - vertices[1].Two) >= (vertices[7].One - vertices[1].One) * (vertices[5].Two - vertices[1].Two))
              {
                IsInHull[5] = false;
              }

              if (IsInHull[1] && IsInHull[3] && IsInHull[7]
                && (vertices[3].One - vertices[1].One) * (vertices[7].Two - vertices[1].Two) >= (vertices[7].One - vertices[1].One) * (vertices[3].Two - vertices[1].Two))
              {
                IsInHull[3] = false;
              }
              #endregion

              #region Removing points that are in the hull of the subset of finite points but not in the hull due to infinite extreme points
              int index = 0;
              var value = Rational.For(0);

              if (vertices[0].One.IsInfinity)
              {
                index = 2;
                value = vertices[2].Two;
                for (int n = 4; n < 8; n += 2)
                {
                  if (vertices[n].Two <= value)
                  {
                    for (int m = index; m < n; m += 2)
                    {
                      IsInHull[m] = false;
                    }
                    index = n;
                    value = vertices[n].Two;
                  }
                }
              }

              if (vertices[6].One.IsInfinity)
              {
                index = 4;
                value = vertices[4].Two;
                for (int n = 2; n >= 0; n -= 2)
                {
                  if (vertices[n].Two <= value)
                  {
                    for (int m = index; m > n; m -= 2)
                    {
                      IsInHull[m] = false;
                    }
                    index = n;
                    value = vertices[n].Two;
                  }
                }
              }

              if (vertices[1].One.IsInfinity)
              {
                index = 3;
                value = vertices[3].Two;
                for (int n = 5; n < 8; n += 2)
                {
                  if (vertices[n].Two >= value)
                  {
                    for (int m = index; m < n; m += 2)
                    {
                      IsInHull[m] = false;
                    }
                    index = n;
                    value = vertices[n].Two;
                  }
                }
              }

              if (vertices[7].One.IsInfinity)
              {
                index = 5;
                value = vertices[5].Two;
                for (int n = 3; n >= 0; n -= 2)
                {
                  if (vertices[n].Two >= value)
                  {
                    for (int m = index; m > n; m -= 2)
                    {
                      IsInHull[m] = false;
                    }
                    index = n;
                    value = vertices[n].Two;
                  }
                }
              }
              #endregion

              #region Adding to the result the Polynomials for the edges that are in the hull and neither horizontal nor vertical
              var point = new PairNonNull<Rational, Rational>();
              Rational c;
              int numberOfPointsInHull = 0;

              for (int k = 0; k < 8; k += 2)
              {
                if (IsInHull[k])
                {
                  if (numberOfPointsInHull > 0 && point.One != vertices[k].One && point.Two != vertices[k].Two)
                  {
                    try
                    {
                      c = (vertices[k].One - point.One) / (point.Two - vertices[k].Two);
                    }
                    catch (ArithmeticExceptionRational)
                    {
                      continue;
                    }

                    var list = new Monomial<Variable>[] { new Monomial<Variable>(e1), new Monomial<Variable>(c, e2), new Monomial<Variable>(slack) };

                    if (!Polynomial<Variable, Expression>.TryToPolynomialForm(true, list, out pol))
                    {
                      throw new AbstractInterpretationException("Impossible case");
                    }
                    result.Add(pol);
                  }
                  point = vertices[k];
                  numberOfPointsInHull++;
                }
              }
              for (int k = 1; k < 8; k += 2)
              {
                if (IsInHull[k])
                {
                  if (numberOfPointsInHull > 0 && point.One != vertices[k].One && point.Two != vertices[k].Two)
                  {
                    try
                    {
                      c = (vertices[k].One - point.One) / (point.Two - vertices[k].Two);
                    }
                    catch (ArithmeticExceptionRational)
                    {
                      continue;
                    }

                    var list = new Monomial<Variable>[] { new Monomial<Variable>(e1), new Monomial<Variable>(c, e2), new Monomial<Variable>(slack) };

                    if (!Polynomial<Variable, Expression>.TryToPolynomialForm(true, list, out pol))
                    {
                      throw new AbstractInterpretationException("Impossible case");
                    }

                    result.Add(pol);
                  }
                  point = vertices[k];
                  numberOfPointsInHull++;
                }
              }
              #endregion
            }
            catch (ArithmeticExceptionRational)
            {
              // Ignore the constraint
            }
#endregion
          }
        }
      }
      return result;
    }

    public static bool TryEvalWithCache(
      IntervalEnvironment<Variable, Expression> env, Variable exp, Dictionary<Variable, Interval> cache, out Interval valueIfNormal)
    {
      Contract.Requires(env != null);
      Contract.Requires(cache != null);

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out valueIfNormal) != null);

      if (!cache.TryGetValue(exp, out valueIfNormal))
      {
        valueIfNormal = env.Eval(exp);
        cache[exp] = valueIfNormal;
      }
      else
      {
        Contract.Assume(valueIfNormal != null);
      }

      return valueIfNormal.IsNormal;
    }

    private static bool IsFinite(PairNonNull<Rational, Rational> point)
    {
      return !point.One.IsInfinity && !point.Two.IsInfinity;
    }

    #endregion

    #region Redefinition for the stand-alone version of subpolyhedra
#if SUBPOLY_ONLY
    new Interval this[Expression index]
    {
      get
      {
        return base[index];
      }
      set
      {
        if (this.ExpressionManager.Decoder.IsVariable(index))
        {
          base[index] = value;
        }
      }
    }
#endif
    #endregion

    #region Abstraction over Intervals
    /// <summary>
    /// Tighten the interval <code>val</code> using the range of values
    /// </summary>
    protected override Interval ApplyConversion(ExpressionOperator conversionType, Interval val)
    {
      return Interval.ApplyConversion(conversionType, val);
    }

    protected override Interval ConvertInterval(Interval intv)
    {
      return intv;
    }

    public override DisInterval BoundsFor(Expression exp)
    {
      return this.Eval(exp).AsDisInterval;
    }

    public override DisInterval BoundsFor(Variable var)
    {
      Interval result;
      if (this.TryGetValue(var, out result))
      {
        return result.AsDisInterval;
      }

      return DisInterval.UnknownInterval;
    }

    override public List<Pair<Variable, Int32>> IntConstants
    {
      get
      {
        var result = new List<Pair<Variable, Int32>>();

        foreach (var pair in this.Elements)
        {
          Rational r;
          if (pair.Value.TryGetSingletonValue(out r) && r.IsInteger)
          {
            var intValue = (Int32) r; // Cast is safe, as r is an Int32
            result.Add(pair.Key, intValue);
          }
        }

        return result;
      }
    }

    public override bool IsGreaterEqualThanZero(Rational val) { return val >= 0; }
    public override bool IsGreaterThanZero(Rational val) { return val > 0; }
    public override bool IsLessThanZero(Rational val) { return val < 0; }
    public override bool IsLessEqualThanZero(Rational val) { return val <= 0; }
    public override bool IsLessThan(Rational val1, Rational val2) { return val1 < val2; }
    public override bool IsLessEqualThan(Rational val1, Rational val2) { return val1 <= val2; }
    public override bool IsZero(Rational val) { return val.IsZero; }
    public override bool IsNotZero(Rational val) { return val.IsNotZero; }
    public override Interval IntervalUnknown { get { return Interval.UnknownInterval; } }
    public override Interval IntervalZero { get { return Interval.For(0); } }
    public override Interval IntervalOne
    {
      get { return Interval.For(1); }
    }

    public override Interval IntervalGreaterEqualThanMinusOne
    {
      get { return Interval.For(-1, Rational.PlusInfinity); }
    }

    public override Interval Interval_Positive
    {
      get { return Interval.PositiveInterval; }
    }

    public override Interval Interval_StrictlyPositive
    {
      get { return this.IntervalRightOpen(Rational.For(1)); }
    }

    public override Interval IntervalSingleton(Rational val)
    {
      return Interval.For(val);
    }

    public override Interval IntervalLeftOpen(Rational sup) 
    { 
      return Interval.For(Rational.MinusInfinity, sup); 
    }

    public sealed override Interval IntervalRightOpen(Rational inf)
    {
      return Interval.For(inf, Rational.PlusInfinity);
    }
    
    public override Interval For(long value)
    {
      if (Rational.CanRepresentExactly(value))
        return Interval.For(Rational.For(value));
      else if (value >= 0) // of course it will never be zero, it is just to shut up the compiler
        return Interval.For(1, Rational.PlusInfinity);
      else
        return Interval.For(Rational.MinusInfinity, -1);
    }

    public override Interval For(byte v)
    {
      return Interval.For(v);
    }

    public override Interval For(double d)
    {
      return Interval.For(d, d, true);
    }

    public override Interval For(int v)
    {
      return Interval.For((int)v);
    }

    public override Interval For(sbyte s)
    {
      return Interval.For(s);
    }

    public override Interval For(short v)
    {
      return Interval.For(v);
    }

    public override Interval For(uint u)
    {
      return Interval.For(u);
    }

    public override Interval For(ushort u)
    {
      return Interval.For(u);
    }

    public override Interval For(Rational r)
    {
      return Interval.For(r);
    }

    public override Interval For(Rational inf, Rational sup)
    {
      return Interval.For(inf, sup);
    }

    public override bool AreEqual(Interval left, Interval right)
    {
      return left.IsNormal() && right.IsNormal() && left.LessEqual(right) && right.LessEqual(left);
    }

    public override Interval Interval_Add(Interval left, Interval right)
    {
      return left + right;
    }

    public override Interval Interval_BitwiseAnd(Interval left, Interval right)
    {
      return left & right;
    }

    public override Interval Interval_BitwiseOr(Interval left, Interval right)
    {
      return left | right;
    }

    public override Interval Interval_BitwiseXor(Interval left, Interval right)
    {
      return left ^ right;
    }

    public override Interval Interval_Div(Interval left, Interval right)
    {
      return left / right;
    }

    public override Interval Interval_Mul(Interval left, Interval right)
    {
      return left * right;
    }

    public override Interval Interval_Rem(Interval left, Interval right)
    {
      return left % right;
    }

    public override Interval Interval_ShiftLeft(Interval left, Interval right)
    {
      return Interval.ShiftLeft(left, right);
    }

    public override Interval Interval_ShiftRight(Interval left, Interval right)
    {
      return Interval.ShiftRight(left, right);
    }

    public override Interval Interval_Sub(Interval left, Interval right)
    {
      return left - right;
    }

    public override Interval Interval_UnaryMinus(Interval left)
    {
      return -left;
    }

    public override Interval Interval_Not(Interval left)
    {
      if (!left.IsNormal)
        return left;

      int val;
      if (left.IsFiniteAndInt32Singleton(out val))
      {
        return val == 0 ? Interval.For(1) : Interval.For(0);
      }

      return Interval.UnknownInterval;
    }

    public override bool IsMaxInt32(Interval intv)
    {
      return intv.IsSingleton && intv.LowerBound.IsInteger && ((Int32)(intv.LowerBound.NextInt32)) == Int32.MaxValue;
    }

    public override bool IsMinInt32(Interval intv)
    {
      return intv.IsSingleton && intv.LowerBound.IsInteger && ((Int32)(intv.LowerBound.NextInt32)) == Int32.MinValue;
    }

    public override bool IsMinusInfinity(Rational val)
    {
      return val.IsMinusInfinity;
    }

    public override bool IsPlusInfinity(Rational val)
    {
      return val.IsPlusInfinity;
    }

    public override Rational MinusInfinty
    {
      get { return Rational.MinusInfinity;  }
    }

    public override Rational PlusInfinity
    {
      get { return Rational.PlusInfinity; }
    }

    public override bool TryAdd(Rational left, Rational right, out Rational result)
    {
      if (Rational.TryAdd(left, right, out result))
      {
        return true;
      }
      else
      {
        result = default(Rational);
        return true;
      }
    }

    protected override void AssumeInDisInterval_Internal(Variable x, DisInterval value)
    {
      AssumeInInterval_Internal(x, value.AsInterval);
    }

    void AssumeInInterval_Internal(Variable x, Interval value)
    {
      Contract.Requires(value != null);

      if (value.IsTop)
      {
        return;
      }

      Interval prev;
      if (this.TryGetValue(x, out prev))
      {
        value = prev.Meet(value);
      }

      if (value.IsBottom)
      {
        this.State = AbstractState.Bottom;
      }
      else
      {
        this[x] = value;
      }
    }

    protected override T To<T>(Rational n, IFactory<T> factory)
    {
      return factory.Constant(n);
    }
    #endregion

    public IntervalEnvironment<Variable, Expression> AbstractsAwayTooLargeBounds(long min, long max)
    {
      Contract.Requires(min <= max);
      Contract.Ensures(Contract.Result<IntervalEnvironment<Variable, Expression>>() != null);

      if(this.IsBottom || this.IsTop)
      {
        return this;
      }

      var buff = new IntervalEnvironment<Variable, Expression>(this.ExpressionManager);

      foreach(var pair in this.Elements)
      {
        // Is lower bound small enough?
        if (min <= pair.Value.LowerBound)
        {
          // Is upper bound small enough?
          if (pair.Value.UpperBound <= max)
          {
            buff[pair.Key] = pair.Value;
          }
          else
          { // Abstract upper bound
            buff[pair.Key] = Interval.For(pair.Value.LowerBound, Rational.PlusInfinity);
          }
        }
        else
        { // We know lower bound is too large
          if (pair.Value.UpperBound <= max)
          {
            buff[pair.Key] = Interval.For(Rational.MinusInfinity, pair.Value.UpperBound);
          }
          else
          { 
            // Interval too large, let's forget it!
          }
        }
      }

      return buff;
    }

    #region Statistics

    new public string Statistics()
    {
      switch(this.State)
      {
        case AbstractState.Normal:
          return string.Format("Vars: {0}", this.Count);
        case AbstractState.Bottom:
          return "bottom";
        case AbstractState.Top:
          return "top";
        default:
          {
            Contract.Assume(false);
          return "";
          }
      }
    }
    
    #endregion
  }
}