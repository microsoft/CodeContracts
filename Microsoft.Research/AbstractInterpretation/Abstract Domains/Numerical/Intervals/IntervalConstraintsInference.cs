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
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  public static class IntervalInference
  {
    /// <summary>
    /// left \lt k
    /// </summary>
    static public bool TryRefine_LeftLessThanK<Variable, Expression, IType>(
      bool isSigned, Variable left, IType k,
      ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery, out IType refinedIntv)
      where IType : IntervalBase<IType, Rational>
    {
      var result = new List<Pair<Variable, IType>>();

      if (k.IsNormal)
      {
        var upperBound = k.UpperBound.IsInteger ? k.UpperBound - 1 : k.UpperBound;
        var LessEquanThanK = iquery.IntervalLeftOpen(upperBound);

        IType oldValueForLeft;
        if (iquery.TryGetValue(left, isSigned, out oldValueForLeft))
        {
          refinedIntv = oldValueForLeft.Meet(LessEquanThanK);
        }
        else
        {
          refinedIntv = LessEquanThanK;
        }

        return true;
      }

      refinedIntv = default(IType);
      return false;
    }

    static public bool TryRefine_KLessThanRight<Variable, Expression, IType>(
      bool isSigned, IType k, Variable right,
      Rational succ,
      ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery, out IType refinedIntv)
      where IType : IntervalBase<IType, Rational>
    {
      if (k.IsNormal)
      {
        var lowerBound = k.LowerBound.IsInteger ? k.LowerBound.NextInt32 + succ : k.LowerBound;
        var GreaterThanK = iquery.IntervalRightOpen(lowerBound);

        IType oldValueForRight;
        if (iquery.TryGetValue(right, isSigned, out oldValueForRight))
        {
          refinedIntv = oldValueForRight.Meet(GreaterThanK);
        }
        else
        {
          refinedIntv = GreaterThanK;
        }

        return true;
      }

      refinedIntv = default(IType);
      return false;
    }

    static public bool TryRefine_KLessEqualThanRight<Variable, Expression, NType, IType>(
      bool isSigned, IType k, Variable right,
      ISetOfNumbersAbstraction<Variable, Expression, NType, IType> iquery, out IType refinedIntv)
      where IType : IntervalBase<IType, NType>
    {
      if (k.IsNormal)
      {
        var GreaterThanK = iquery.IntervalRightOpen(k.LowerBound);

        IType oldValueForRight;
        if (iquery.TryGetValue(right, isSigned, out oldValueForRight))
        {
          refinedIntv = oldValueForRight.Meet(GreaterThanK);
        }
        else
        {
          refinedIntv = GreaterThanK;
        }

        return true;
      }

      refinedIntv = default(IType);
      return false;
    }

    static public bool TryRefine_LeftLessEqualThanK<Variable, Expression, NType, IType>(
      bool isSigned, Variable left, IType k,
      ISetOfNumbersAbstraction<Variable, Expression, NType, IType> iquery, out IType refinedIntv)
      where IType : IntervalBase<IType, NType>
    {
      if (k.IsNormal)
      {
        var LessEquanThanK = iquery.IntervalLeftOpen(k.UpperBound);

        IType oldValueForLeft;
        if (iquery.TryGetValue(left, isSigned, out oldValueForLeft))
        {
          refinedIntv = oldValueForLeft.Meet(LessEquanThanK);
        }
        else
        {
          refinedIntv = LessEquanThanK;
        }

        return true;
      }

      refinedIntv = default(IType);
      return false;
    }

    /// <summary>
    /// Infer exp \in [0, +oo], and if exp is a compound expression also some other constraints
    /// </summary>
    static public List<Pair<Variable, IType>> InferConstraints_GeqZero<Variable, Expression, IType>(
      Expression exp, IExpressionDecoder<Variable, Expression> decoder,
      ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery)
      where IType : IntervalBase<IType, Rational>
    {
      Contract.Requires(decoder != null);
      Contract.Requires(iquery != null);

      Contract.Ensures(Contract.Result<List<Pair<Variable, IType>>>() != null);

      var result = new List<Pair<Variable, IType>>();

      var expVar = decoder.UnderlyingVariable(exp);

      result.Add(expVar, iquery.Eval(exp).Meet(iquery.Interval_Positive));

      if (!decoder.IsVariable(exp))
      {
        Polynomial<Variable, Expression> zero; // = 0

        if (!Polynomial<Variable, Expression>.TryToPolynomialForm(new Monomial<Variable>[] { new Monomial<Variable>(0) }, out zero))
        {
          throw new AbstractInterpretationException("It can never be the case that the conversion of a list of monomials into a polynomial fails");
        }

        Polynomial<Variable, Expression> expAsPolynomial, newPolynomial;

        if (Polynomial<Variable, Expression>.TryToPolynomialForm(exp, decoder, out expAsPolynomial)
          && Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessEqualThan, zero, expAsPolynomial, out newPolynomial)) // 0 <= exp
        {
          if (newPolynomial.IsIntervalForm)
          {  // if it is in the form of k1 * x <= k2 
            var k1 = newPolynomial.Left[0].K;
            var x = newPolynomial.Left[0].VariableAt(0);

            var k2 = newPolynomial.Right[0].K;

            Rational bound;
            if (Rational.TryDiv(k2, k1, out bound)) //var bound = k2 / k1;
            {
              if (k1 > 0)
              {
                var intv = iquery.Eval(x).Meet(iquery.IntervalLeftOpen(bound));
                result.Add(x, intv);
              }
              else if (k1 < 0)
              {
                var intv = iquery.Eval(x).Meet(iquery.IntervalRightOpen(bound));
                result.Add(x, intv);
              }
              else
              {
                throw new AbstractInterpretationException("Impossible case");
              }
            }
          }

        }
      }

      return result;
    }

    static public List<Pair<Variable, IType>> InferConstraints_Leq<Variable, Expression, IType>(
      bool isSignedComparison,
      Expression left, Expression right,
       IExpressionDecoder<Variable, Expression> decoder,
       ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery,
      out bool isBottom)
    where IType : IntervalBase<IType, Rational>
    {
      Contract.Requires(iquery != null);
      Contract.Ensures(Contract.Result<List<Pair<Variable, IType>>>() != null);

      isBottom = false;   // false, unless someine proves the contrary

      var result = new List<Pair<Variable, IType>>();

      if (IsFloat(left, decoder) || IsFloat(right, decoder))
      {
        return result;
      }

      var kLeft = iquery.Eval(left);
      var kRight = iquery.Eval(right);

      // We have to take into account the polymorphism of constants
      if (!isSignedComparison)
      {
        kLeft = kLeft.ToUnsigned();
        kRight = kRight.ToUnsigned();
      }

      //AssumeKLessEqualThanRight(kLeft, this.Decoder.UnderlyingVariable(right))

      IType refinedIntv;
      var rightVar = decoder.UnderlyingVariable(right);
      if (IntervalInference.TryRefine_KLessEqualThanRight(isSignedComparison, kLeft, rightVar, iquery, out refinedIntv))
      {
        // If it is an unsigned comparison, and it is a constant, then we should avoid generating the constraint
        // Example: left <={un} right, with right == -1 then we do not want to generate the constraint right == 2^{32}-1 which is wrong!
        
        // unsigned ==> right is not a constant
        if (isSignedComparison || !kRight.IsSingleton)
        {
          result.Add(rightVar, refinedIntv);
        }
      }

      //AssumeLeftLessEqualThanK(this.Decoder.UnderlyingVariable(left), kRight);
      var leftVar = decoder.UnderlyingVariable(left);
      if (IntervalInference.TryRefine_LeftLessEqualThanK(isSignedComparison, leftVar, kRight, iquery, out refinedIntv))
      {
        // unsigned ==> left is not a constant
        if (isSignedComparison || !kLeft.IsSingleton)
        {
          result.Add(leftVar, refinedIntv);
        }
      }

      if (isSignedComparison)
      {
        Polynomial<Variable, Expression> guardInCanonicalForm;

        if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessEqualThan, left, right, decoder, out guardInCanonicalForm)
          && guardInCanonicalForm.IsLinear)
        {
          // We consider only the case when there is at MOST one variable on the left, i.e. a * x \leq b, or TWO, i.e. a*x +b*y <= c        
          if (guardInCanonicalForm.Left.Length == 1)
          {
            return HelperFortestTrueLessEqualThan_AxLeqK(guardInCanonicalForm, iquery, result, out isBottom);
          }
          else if (guardInCanonicalForm.Left.Length == 2)
          {
            return HelperFortestTrueLessEqualThan_AxByLtK(guardInCanonicalForm, iquery, result, out isBottom);
          }
        }
      }

      return result;
    }

    static public List<Pair<Variable, IType>> InferConstraints_LT<Variable, Expression, IType>(
      bool isSignedComparison,
      Expression left, Expression right,
      IExpressionDecoder<Variable, Expression> decoder,
      ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery,
      out bool isBottom)
      where IType : IntervalBase<IType, Rational>
    {
      Contract.Ensures(Contract.Result<List<Pair<Variable, IType>>>() != null);

      isBottom = false; // False untils someone proves the contrary

      var result = new List<Pair<Variable, IType>>();

      var kLeft = iquery.Eval(left);
      var kRight = iquery.Eval(right);

      if (!isSignedComparison && !IsFloat(left, decoder) && !IsFloat(right, decoder))
      {
        kLeft = kLeft.ToUnsigned();
        kRight = kRight.ToUnsigned();
      }

      IType refinedIntv;
      var rightVar = decoder.UnderlyingVariable(right);

      var succ = IsFloat(left, decoder) || IsFloat(right, decoder) ? Rational.For(0) : Rational.For(1);

      if (TryRefine_KLessThanRight(isSignedComparison, kLeft, rightVar, succ, iquery, out refinedIntv))
      {
        // If it is an unsigned comparison, and it is a constant, then we should avoid generating the constraint
        // Example: left <{un} right, with right == -1 then we do not want to generate the constraint right == 2^{32}-1 which is wrong!

        // unsigned ==> right is not a constant
        if (isSignedComparison || !kRight.IsSingleton)
        {
          result.Add(rightVar, refinedIntv);
        }
      }

      if (IsFloat(left, decoder) || IsFloat(right, decoder))
      {
        return result;
      }

      var leftVar = decoder.UnderlyingVariable(left);
      if (TryRefine_LeftLessThanK(isSignedComparison, leftVar, kRight, iquery, out refinedIntv))
      {
        // As above
        // unsigned ==> right is not a constant
        if (isSignedComparison || !kLeft.IsSingleton)
        {

          result.Add(leftVar, refinedIntv);
        }
      }

      if (isSignedComparison)
      {
        // Try to infer some more fact
        Polynomial<Variable, Expression> guardInCanonicalForm;
        if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessThan, left, right, decoder, out guardInCanonicalForm)
          && guardInCanonicalForm.IsLinear)
        {
          // First, we consider only the case when there is at MOST one variable on the left, i.e. a * x < b
          {
            if (guardInCanonicalForm.Left.Length == 1)
            {
              result = HelperFortestTrueLessThan_AxLtK(guardInCanonicalForm, iquery, result, out isBottom);
            }
            // Then, we consider the case when it is in the form a*x + b*y < k 
            else if (guardInCanonicalForm.Left.Length == 2)
            {
              return HelperFortestTrueLessThan_AxByLtK(guardInCanonicalForm, iquery, result, out isBottom);
            }
          }
        }
        else
        {
          #region Try to infer something else...
          switch (decoder.OperatorFor(left))
          {
            case ExpressionOperator.And:  // case "(leftLeft && leftRight) < right
              var leftRight = decoder.RightExpressionFor(left);

              Int32 valueLeft;
              if (decoder.IsConstantInt(leftRight, out valueLeft))
              {
                if (IsPowerOfTwoMinusOne(valueLeft))
                {   // add the constraint " 0 <= right < valueLeft "
                  var oldVal = iquery.Eval(right);
                  var evalVal = iquery.Eval(left);
                  var newVal = iquery.For(Rational.For(0), Rational.For(valueLeft - 1));   //  [0, valueLeft-1], "-1" as we know it is an integer

                  result.Add(rightVar, oldVal.Meet(newVal).Meet(evalVal));
                }

              }
              break;

            default:
              // do nothing...
              break;
          }

          #endregion
        }
      }
      return result;
    }

    static public void InferConstraints_NotEq<Variable, Expression, IType>(
    Expression left, Expression right,
    IExpressionDecoder<Variable, Expression> decoder,
    ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery,
      out List<Pair<Variable, IType>> resultLeft, 
      out List<Pair<Variable, IType>> resultRight,
      out bool isBottomLeft,
      out bool isBottomRight)
    where IType : IntervalBase<IType, Rational>
    {
      isBottomLeft = false; // False untils someone proves the contrary
      isBottomRight = false;

      resultLeft = new List<Pair<Variable, IType>>();
      resultRight = new List<Pair<Variable, IType>>();

      var kLeft = iquery.Eval(left);
      var kRight = iquery.Eval(right);

      var rightVar = decoder.UnderlyingVariable(right);
      var leftVar = decoder.UnderlyingVariable(left);

      var succ = IsFloat(left, decoder) || IsFloat(right, decoder) ? Rational.For(0) : Rational.For(1);

      NewMethod<Variable, Expression, IType>(succ, iquery, kLeft, kRight, rightVar, leftVar, resultLeft);
      NewMethod<Variable, Expression, IType>(succ, iquery, kRight, kLeft, leftVar, rightVar, resultRight);

      // Try to infer some more fact
      Polynomial<Variable, Expression> tmpLeftPoly, tmpRightPoly;

      if (Polynomial<Variable, Expression>.TryToPolynomialForm(left, decoder, out tmpLeftPoly)
        && Polynomial<Variable, Expression>.TryToPolynomialForm(right, decoder, out tmpRightPoly))
      {
        NewMethod2<Variable, Expression, IType>(tmpLeftPoly, tmpRightPoly, iquery, ref isBottomLeft, resultLeft);
        NewMethod2<Variable, Expression, IType>(tmpRightPoly, tmpLeftPoly, iquery, ref isBottomRight, resultRight);
      }

    }

    private static void NewMethod2<Variable, Expression, IType>(
      Polynomial<Variable, Expression> tmpLeftPoly, Polynomial<Variable, Expression> tmpRightPoly, 
      ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery, 
      ref bool isBottom, 
      List<Pair<Variable, IType>> resultLeft) 
      where IType : IntervalBase<IType, Rational>
    {
      Polynomial<Variable, Expression> guardInCanonicalForm;

      if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessThan, tmpLeftPoly, tmpRightPoly, out guardInCanonicalForm))
      {
        // First, we consider only the case when there is at MOST one variable on the left, i.e. a * x < b
        if (guardInCanonicalForm.IsLinear)
        {
          if (guardInCanonicalForm.Left.Length == 1)
          {
            resultLeft = HelperFortestTrueLessThan_AxLtK(guardInCanonicalForm, iquery, resultLeft, out isBottom);
          }
          // Then, we consider the case when it is in the form a*x + b*y < k 
          else if (guardInCanonicalForm.Left.Length == 2)
          {
            resultLeft = HelperFortestTrueLessThan_AxByLtK(guardInCanonicalForm, iquery, resultLeft, out isBottom);
          }
        }
      }
    }

    private static void NewMethod<Variable, Expression, IType>(
      Rational succ, 
      ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery, 
       IType kLeft, IType kRight, Variable rightVar, Variable leftVar, 
      List<Pair<Variable, IType>> result) 
      where IType : IntervalBase<IType, Rational>
    {
      IType refinedIntv;
      if (TryRefine_KLessThanRight(true, kLeft, rightVar, succ, iquery, out refinedIntv))
      {
        result.Add(rightVar, refinedIntv);
      }

      if (TryRefine_LeftLessThanK(true, leftVar, kRight, iquery, out refinedIntv))
      {
        result.Add(leftVar, refinedIntv);
      }
    }

   
    #region Private helpers

    static private List<Pair<Variable, IType>> HelperFortestTrueLessEqualThan_AxLeqK<Variable, Expression, IType>(
      Polynomial<Variable, Expression> guardInCanonicalForm, ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery,
      List<Pair<Variable, IType>> result,
      out bool isBottom)
      where IType : IntervalBase<IType, Rational>
    {
      Contract.Requires(!object.ReferenceEquals(guardInCanonicalForm, null));
      Contract.Requires(result != null);
      Contract.Requires(guardInCanonicalForm.Right.Length == 1);

      Contract.Ensures(Contract.Result<List<Pair<Variable, IType>>>() != null);

      var MonomialForX = guardInCanonicalForm.Left[0];
      var MonomilaForB = guardInCanonicalForm.Right[0];

      Contract.Assert(MonomilaForB.IsConstant);

      isBottom = false;

      // 1. We have a case a <= b
      if (MonomialForX.IsConstant)
      {
        var a = MonomialForX.K;
        var b = MonomilaForB.K;

        if (a > b)
        {
          isBottom = true;
        }
      }
      else // 2. We have the case a * x \leq b
      {
        var x = MonomialForX.VariableAt(0);

        Rational k;
        if ( 
          MonomialForX.K.IsInteger &&
          Rational.TryDiv(MonomilaForB.K, MonomialForX.K, out k))
        {
          var oldValueForX = iquery.Eval(x);

          IType newValueForX;

          if (MonomialForX.K.Sign == 1)
          {   // The constraint is x \leq k
            newValueForX = oldValueForX.Meet(iquery.IntervalLeftOpen(k));
          }
          else
          {   // The constraint is x \geq k
            newValueForX = oldValueForX.Meet(iquery.IntervalRightOpen(k));
          }

          if (newValueForX.IsBottom)
          {
            isBottom = true;
          }
          else
          {
            Contract.Assert(isBottom == false);

            result.Add(x, newValueForX);
          }
        }
      }

      return result;
    }

    /// <summary>
    /// Handles the case when the guard is in the form of "A*x + B*y &lt;= k"
    /// </summary>
    static private List<Pair<Variable, IType>> HelperFortestTrueLessEqualThan_AxByLtK<Variable, Expression, IType>(
      Polynomial<Variable, Expression> guardInCanonicalForm, ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery,
      List<Pair<Variable, IType>> result,
      out bool isBottom)
      where IType : IntervalBase<IType, Rational>
    {
      Contract.Requires(!object.ReferenceEquals(guardInCanonicalForm, null));
      Contract.Requires(iquery != null);
      Contract.Requires(result != null);

      Contract.Requires(guardInCanonicalForm.Right.Length == 1);
      Contract.Requires(guardInCanonicalForm.Left.Length == 2);

      Contract.Ensures(Contract.Result<List<Pair<Variable, IType>>>() != null);

      isBottom = false;   // false, unless someone proves not

      var monomialForX = guardInCanonicalForm.Left[0];
      var monomialForY = guardInCanonicalForm.Left[1];

      var X = monomialForX.VariableAt(0);
      var Y = monomialForY.VariableAt(0);

      var K = guardInCanonicalForm.Right[0].K;

      var A = monomialForX.K;
      var B = monomialForY.K;

      var intervalForA = iquery.For(A);
      var intervalForB = iquery.For(B);
      var intervalForK = iquery.For(K);

      var intervalForX = iquery.Eval(X);
      var intervalForY = iquery.Eval(Y);

      // 1. Handle the case for x

      // evalForX =(k - b * y) / a
      var evalForX = iquery.Interval_Div(iquery.Interval_Sub(intervalForK, (iquery.Interval_Mul(intervalForB, intervalForY))), intervalForA);

      if (A > 0)
      { // x <= (k - b * y) / a
        var upperBound = evalForX.UpperBound;
        var geq = iquery.IntervalLeftOpen(upperBound);

        var newIntv = intervalForX.Meet(geq);

        if (newIntv.IsBottom)
        {
          isBottom = true;

          return result;
        }

        result.Add(X, newIntv);
      }
      else if (A < 0)
      { // x >= (k - b * y) / a
        var lowerBound = evalForX.LowerBound;
        var leq = iquery.IntervalRightOpen(lowerBound);

        var newIntv = intervalForX.Meet(leq);

        if(newIntv.IsBottom)
        {
          isBottom = true;

          return result;
        }

        result.Add(X, newIntv);
      }
      else
      {
        Contract.Assert(false, "Cannot have a == 0");
      }

      // 2. Handle the case for y

      // evalForY =(k - a * x) / b
      //var evalForY = (intervalForK - (intervalForA * intervalForX)) / (intervalForB);

      var evalForY = iquery.Interval_Div(iquery.Interval_Sub(intervalForK, (iquery.Interval_Mul(intervalForA, intervalForX))), intervalForB);

      if (B > 0)
      { // y <= (k - a * x) / b
        var upperBound = evalForY.UpperBound;
        var geq = iquery.IntervalLeftOpen(upperBound);

        var newIntv = intervalForY.Meet(geq);

        if(newIntv.IsBottom)
        {
          isBottom = true;
          return result;
        }

        result.Add(Y, newIntv);
      }
      else if (B < 0)
      { // x >= (k - a * x) / b
        var lowerBound = evalForY.LowerBound;
        var leq = iquery.IntervalRightOpen(lowerBound);

        var newIntv = intervalForY.Meet(leq);

        if (newIntv.IsBottom)
        {
          isBottom = true;
          return result;
        }

        result.Add(Y, newIntv);
      }
      else
      {
        Contract.Assert(false, "Cannot have b == 0");
      }

      return result;
    }


    /// <summary>
    /// Handles the case when the guard is in the form of "A*x &lt; k"
    /// </summary>
    /// <param name="guardInCanonicalForm">A polynomial that must have been already put into canonical form</param>
    static private List<Pair<Variable, IType>> HelperFortestTrueLessThan_AxLtK<Variable, Expression, IType>(
      Polynomial<Variable, Expression> guardInCanonicalForm, ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery,
      List<Pair<Variable, IType>> result,
      out bool isBottom)
      where IType : IntervalBase<IType, Rational>
    {
      Contract.Requires(!object.ReferenceEquals(guardInCanonicalForm, null));
      Contract.Requires(guardInCanonicalForm.Right.Length == 1);
      Contract.Requires(iquery  != null);
      Contract.Requires(result != null);

      Contract.Ensures(Contract.Result<List<Pair<Variable, IType>> >() != null);

      isBottom = false;

      var MonomialForX = guardInCanonicalForm.Left[0];
      var MonomilaForB = guardInCanonicalForm.Right[0];

      Contract.Assert(MonomilaForB.IsConstant);

      // 1. We have a case a < b
      if (MonomialForX.IsConstant)
      {
        var a = MonomialForX.K;
        var b = MonomilaForB.K;

        if (a >=  b)
        {
          isBottom = true;
        }
      }
      else // 2. We have the case a * x < b
      {
        var x = MonomialForX.VariableAt(0);

        Rational k;
        if (!Rational.TryDiv(MonomilaForB.K, MonomialForX.K, out k)) //var k = MonomilaForB.K / MonomialForX.K;
        {
          return result;
        }

        var oldValueForX = iquery.Eval(x);
        
        IType newConstraint;
        if (MonomialForX.K.Sign == 1)
        {   // The constraint is x < k, but if k is a rational, we approximate it with x \leq k
          if (k.IsInteger)
          {
            newConstraint = iquery.IntervalLeftOpen(k - 1);
          }
          else
          {
            newConstraint = iquery.IntervalLeftOpen(k.PreviousInt32);
          }
        }
        else
        {   // The constraint is x > k, but if k is a rational, we approximate it with k \leq x
          if (k.IsInteger)
          {
            newConstraint = iquery.IntervalRightOpen(k + 1);
          }
          else
          {
            newConstraint = iquery.IntervalRightOpen(k.NextInt32);
          }
        }

        var newValueForX = oldValueForX.Meet(newConstraint);

        if (newValueForX.IsBottom)
        {
          isBottom = true;
        }
        else
        {
          result.Add(x, newValueForX);          
        }
      }

      return result;
    }

    /// <summary>
    /// Handles the case when the guard is in the form of "A*x + B*y &lt; k"
    /// </summary>
    /// <param name="guardInCanonicalForm">A polynomial that must have been already put into canonical form</param>
    static private List<Pair<Variable, IType>> HelperFortestTrueLessThan_AxByLtK<Variable, Expression, IType>(
      Polynomial<Variable, Expression> guardInCanonicalForm, ISetOfNumbersAbstraction<Variable, Expression, Rational, IType> iquery,
      List<Pair<Variable, IType>> result,
      out bool isBottom)
      where IType : IntervalBase<IType, Rational>
    {
      Contract.Requires(!object.ReferenceEquals(guardInCanonicalForm, null));
      Contract.Requires(iquery != null);
      Contract.Requires(result != null);
      Contract.Requires(guardInCanonicalForm.Left.Length == 2);
      Contract.Requires(guardInCanonicalForm.Right.Length == 1);

      Contract.Ensures(Contract.Result<List<Pair<Variable, IType>>>() != null);

      isBottom = false; // False, unless another proof

      var monomialForX = guardInCanonicalForm.Left[0];
      var monomialForY = guardInCanonicalForm.Left[1];

      var X = monomialForX.VariableAt(0);
      var Y = monomialForY.VariableAt(0);

      var K = guardInCanonicalForm.Right[0].K;

      var A = monomialForX.K;
      var B = monomialForY.K;

      var intervalForA = iquery.For(A);
      var intervalForB = iquery.For(B);
      var intervalForK = iquery.For(K);

      var intervalForX = iquery.Eval(X);
      var intervalForY = iquery.Eval(Y);

      // 1. Handle the case for x

      // evalForX =(k - b * y) / a
      var evalForX = iquery.Interval_Div((iquery.Interval_Sub(intervalForK,(iquery.Interval_Mul(intervalForB, intervalForY)))), intervalForA);

      if (A > 0)
      { // x < (k - b * y) / a
        var upperBound = evalForX.UpperBound;
        IType gt;
        if (upperBound.IsInteger)
        {
          gt = iquery.IntervalLeftOpen(upperBound - 1);
        }
        else
        {
          gt = iquery.IntervalLeftOpen(upperBound.PreviousInt32);
        }

        var intv = intervalForX.Meet(gt);

        if(intv.IsBottom)
        {
          isBottom = true;
          return result;
        }
        result.Add(X, intv);
      }
      else if (A < 0)
      { // x > (k - b * y) / a
        var lowerBound = evalForX.LowerBound;
        
        IType lt;

        if (lowerBound.IsInteger)
        {
          lt = iquery.IntervalRightOpen(lowerBound + 1);
        }
        else
        {
          lt = iquery.IntervalRightOpen(lowerBound.NextInt32);
        }

        var intv= intervalForX.Meet(lt);

        if(intv.IsBottom)
        {
          isBottom = true;
          return result;
        }
        result.Add(X, intv);
      }
      else
      {
        Contract.Assert(false, "Cannot have a == 0");
      }

      // 2. Handle the case for y

      // evalForY =(k - a * x) / b
      var evalForY = iquery.Interval_Div((iquery.Interval_Sub(intervalForK, (iquery.Interval_Mul(intervalForA, intervalForX)))), intervalForB);

      if (B > 0)
      { // y < (k - a * x) / b
        var upperBound = evalForY.UpperBound;
        IType gt;

        if (upperBound.IsInteger)
        {
          gt = iquery.IntervalLeftOpen(upperBound - 1);
        }
        else
        {
          gt = iquery.IntervalLeftOpen(upperBound.PreviousInt32);
        }

        var intv = intervalForY.Meet(gt);
        if(intv.IsBottom)
        {
          isBottom = true;
          return result;
        }

        result.Add(Y, intv);
      }
      else if (B < 0)
      { // x > (k - a * x) / b
        var lowerBound = evalForY.LowerBound;
        IType lt;

        if (lowerBound.IsInteger)
        {
          lt = iquery.IntervalRightOpen(lowerBound + 1);
        }
        else
        {
          lt = iquery.IntervalRightOpen(lowerBound.NextInt32);
        }

        var intv = intervalForY.Meet(lt);

        if(intv.IsBottom)
        {
          isBottom = true;
          return result;
        }

        result.Add(Y, intv);      }
      else
      {
        Contract.Assert(false, "Cannot have b == 0");
      }

      return result;
    }

    private static bool IsPowerOfTwoMinusOne(int r)
    {
      return r == 15 || r == 31 || r == 63 || r == 127 || r == 255 || r == 511 || r == 1023 || r == 2047 || r == 4095 || r == 8191 || r == 16385 || r == 32767
      || r == 65535 || r == 131071 || r == 262143 || r == 524287 || r == 1048575 || r == 2097151 || r == 4194303 || r == 8388607 || r == 16777215;
    }

    private static bool IsFloat<Variable, Expression>(Expression exp, IExpressionDecoder<Variable, Expression> decoder)
    {
      if (decoder == null)
      {
        return false;
      }

      var type = decoder.TypeOf(exp);

      return type == ExpressionType.Float32 || type == ExpressionType.Float64;
    }
    #endregion
  }
}