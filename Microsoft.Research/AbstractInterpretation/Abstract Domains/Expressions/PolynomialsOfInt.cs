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

// Few classes for helping manipulating PolynomialOfInts and MonomialOfInts

using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains.Expressions
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// A PolynomialOfInt is a triple <code>(op, left, right)</code> where <code>op</code> is the binary operator (e.g. <code>==</code>), <code>left</code> the list of monomes on the left and
  /// <code>right</code> the list of constant monomes on the right.
  /// The list stands for the "addition"
  /// </summary>
#if SUBPOLY_ONLY
  internal
#else
  public
#endif
 class PolynomialOfInt<Variable, Expression>
  {
    #region Static fields

    // TODO: figure out the statistics
    //static readonly private FIFOCache<Expression, PolynomialOfInt<Variable, Expression>> cache_PolynomialOfIntForm = new FIFOCache<Expression,PolynomialOfInt<Variable, Expression>>();
    // static readonly private FIFOCache<PolynomialOfInt<Variable, Expression>, PolynomialOfInt<Variable, Expression>> cache_CanonicalForms = new FIFOCache<PolynomialOfInt<Variable, Expression>, PolynomialOfInt<Variable, Expression>>();
    #endregion

    #region Private fields
    private ExpressionOperator? relation;            // ^ invariant ExpressionOperatorHelper.IsBinaryExpression(relation) || relation == null;

    private List<MonomialOfInt<Variable>>/*!*/ left;
    private List<MonomialOfInt<Variable>>/*?*/ right;                     //^ invariant (this.relation == null) ==> right == null;

    // Cached values
    private bool? Cached_IsLinear = null;
    private int? Cached_Degree = null;
    private bool? Cached_IsTautology = null;

    #endregion

    #region Getters
    /// <summary>
    /// The relation of the PolynomialOfInt, or null if it is a spourious PolynomialOfInt
    /// </summary>
    public ExpressionOperator? Relation
    {
      get
      {
        Contract.Ensures(Contract.Result<ExpressionOperator?>().HasValue == this.relation.HasValue);

        return this.relation;
      }
    }

    /// <summary>
    /// The variables defined in this PolynomialOfInt
    /// </summary>
    public Set<Variable> Variables
    {
      get
      {
        Set<Variable> result = new Set<Variable>();

        foreach (MonomialOfInt<Variable> m in this.Left)
        {
          result.AddRange(m.Variables);
        }

        if (this.Relation != null)
        {
          foreach (MonomialOfInt<Variable> m in this.Right)
          {
            result.AddRange(m.Variables);
          }
        }
        return result;
      }
    }

    /// <summary>
    /// The list of MonomialOfInts on the Left of the relational operator
    /// </summary>
    public IList<MonomialOfInt<Variable>> Left
    {
      get
      {
#if DEBUG
        return this.left.AsReadOnly();
#else
        return this.left;
#endif
      }
    }

    /// <summary>
    /// The list of MonomialOfInts on the Right of the relational operator.
    /// Precondition: <code>this.Relation != null</code>
    /// </summary>
    public IList<MonomialOfInt<Variable>> Right
    {
      get
      {
#if DEBUG
        return this.right != null ? this.right.AsReadOnly() : null;
#else
        return this.right;
#endif
      }
    }

    /// <summary>
    /// The Left part of this PolynomialOfInt as an instance of <code>PolynomialOfInt</code>
    /// </summary>
    public PolynomialOfInt<Variable, Expression> LeftAsPolynomialOfInt
    {
      get
      {
        PolynomialOfInt<Variable, Expression> result;
        TryToPolynomialForm(this.left, out result); // We know that it never fails...

        return result;
      }
    }

    /// <summary>
    /// The Right part of this PolynomialOfInt as an instance of <code>PolynomialOfInt</code>
    /// </summary>
    public PolynomialOfInt<Variable, Expression> RightAsPolynomialOfInt
    {
      get
      {
        Contract.Requires(this.Relation.HasValue);

        PolynomialOfInt<Variable, Expression> result;
        TryToPolynomialForm(this.right, out result); // We know that it never fails...

        return result;
      }
    }


    /// <summary>
    /// Is a simple tautology?
    /// </summary>
    public bool IsTautology
    {
      get
      {
        if (this.Relation == null)
          return false;

        if (Cached_IsTautology.HasValue)
          return Cached_IsTautology.Value;

        if (this.Left.Count == 1 && this.Right.Count == 1)
        {
          if (this.Left[0].Variables.Count == 0 && this.Right[0].Variables.Count == 0)
          {
            bool result;

            switch (this.Relation)
            {
              case ExpressionOperator.Equal:
              case ExpressionOperator.Equal_Obj:
                result = this.Right[0].K == this.Left[0].K;
                break;

              case ExpressionOperator.GreaterEqualThan:
              case ExpressionOperator.GreaterEqualThan_Un:
                result = this.Left[0].K >= this.Right[0].K;
                break;

              case ExpressionOperator.GreaterThan:
              case ExpressionOperator.GreaterThan_Un:
                result = this.Left[0].K > this.Right[0].K;
                break;

              case ExpressionOperator.LessEqualThan:
              case ExpressionOperator.LessEqualThan_Un:
                result = this.Left[0].K <= this.Right[0].K;
                break;

              case ExpressionOperator.LessThan:
              case ExpressionOperator.LessThan_Un:
                result = this.Left[0].K < this.Right[0].K;
                break;

              case ExpressionOperator.NotEqual:
                result = this.Right[0].K != this.Left[0].K;
                break;

              default:
                result = false;
                break;
            }

            Cached_IsTautology = result;
            return result;
          }
        }

        Cached_IsTautology = false;
        return false;
      }
    }

    public bool IsConstant(out int value)
    {
      if(this.Relation.HasValue || this.left.Count != 1 || this.Degree != 0)
      {
        value = default(int);
        return false;
      }

      value = this.left[0].K;
      return true;
    }


    /// <summary>
    /// Is a simple inconsistence?
    /// </summary>
    public bool IsInconsistent
    {
      get
      {
        if (this.Relation == null)
          return false;

        if (this.Left.Count == 1 && this.Right.Count == 1)
        {
          if (this.Left[0].Variables.Count == 0 && this.Right[0].Variables.Count == 0)
          {
            switch (this.Relation)
            {
              case ExpressionOperator.Equal:
              case ExpressionOperator.Equal_Obj:
                return this.Right[0].K != this.Left[0].K;

              case ExpressionOperator.GreaterEqualThan:
              case ExpressionOperator.GreaterEqualThan_Un:
                return this.Left[0].K < this.Right[0].K;

              case ExpressionOperator.GreaterThan:
              case ExpressionOperator.GreaterThan_Un:
                return this.Left[0].K <= this.Right[0].K;

              case ExpressionOperator.LessEqualThan:
              case ExpressionOperator.LessEqualThan_Un:
                return this.Left[0].K > this.Right[0].K;

              case ExpressionOperator.LessThan:
              case ExpressionOperator.LessThan_Un:
                return this.Left[0].K >= this.Right[0].K;

              case ExpressionOperator.NotEqual:
                return this.Right[0].K == this.Left[0].K;

              default:
                return false;
            }
          }
        }

        return false;
      }
    }

    /// <summary>
    /// Is it in the form of <code>a*x + b *y \leq c</code>, with a, b \in  {+1, -1, 0}, and c is an int
    /// </summary>
    public bool IsOctagonForm
    {
      get
      {
        if (this.Relation == null)
        {
          return false;
        }
        if (!this.IsLinear)
        {
          return false;
        }
        if (!(this.Left.Count <= 2 && this.Right.Count == 1))   // Something in the form of m1 + m2 <= m3
        {
          return false;
        }
        if (!(this.Left[0].K == -1 || this.Left[0].K == 1 || this.Left[0].K == 0))     // m1.K must be -1, 0 or +1
        {
          return false;
        }
        if (this.Left.Count == 2)
        {
          if (!(this.Left[1].K == -1 || this.Left[1].K == 1 || this.Left[0].K == 0)) // m2.K must be -1, 0 or +1
          {
            return false;
          }
        }
        if (!this.Right[0].IsConstant)                          // m3 must be a constant
        {
          return false;
        }

        return true;                                            // If we reached this point, then it is an octagon
      }
    }



    /// <summary>
    /// Is it a linear MonomialOfInt?
    /// </summary>
    public bool IsLinear
    {
      get
      {
        if (Cached_IsLinear.HasValue)
        {
          return Cached_IsLinear.Value;
        }

        foreach (var m in this.Left)
        { // all the MonomialOfInt on the left must be linear
          if (!m.IsLinear)
          {
            Cached_IsLinear = false;
            return false;
          }
        }
        if (this.Relation != null)
        {
          foreach (var m in this.Right)
          { // all the MonomialOfInt on the right must be linear
            if (!m.IsLinear)
            {
              Cached_IsLinear = false;
              return false;
            }
          }
          Cached_IsLinear = true;
          return true;
        }
        else
        {
          Cached_IsLinear = true;
          return true;
        }
      }
    }

    public bool IsLinearOctagon
    {
      get
      {
        if (!this.IsLinear)
        {
          return false;
        }
        else if (this.IsOctagonForm)
        {
          return true;
        }
        else if (!this.Relation.HasValue)
        {
          if (this.Left.Count > 2)
          {
            return false;
          }
          else if (this.Left.Count == 1)
          {   // is the case that we have <k, 1> or <+/- 1, x> ?
            return (this.Left[0].IsConstant || this.Left[0].K == 1 || this.Left[0].K == -1);
          }
          else
          {
            Contract.Assume(this.Left.Count == 2, "Error cannot have an empty left on a PolynomialOfInt");

            return (this.Left[0].IsLinear && (this.Left[0].K == 1 || this.Left[0].K == -1) && this.Left[1].IsConstant);
          }
        }
        else
        {
          return false;
        }
      }
    }

    /// <summary>
    /// The degree of the PolynomialOfInt
    /// </summary>
    public int Degree
    {
      get
      {
        if (this.Cached_Degree.HasValue)
          return this.Cached_Degree.Value;

        int max = -1;
        foreach (var m in this.Left)
        {
          if (m.Degree > max)
            max = m.Degree;
        }
        if (this.Right != null)
        {
          foreach (var m in this.Right)
          {
            if (m.Degree > max)
              max = m.Degree;
          }
        }

        this.Cached_Degree = max;
        return max;
      }
    }

    /// <summary>
    /// Is in the form of k1 * x \leq k2 ?
    /// </summary>
    public bool IsIntervalForm
    {
      get
      {
        if (this.Relation != ExpressionOperator.LessEqualThan) // Must be "<="
        {
          return false;
        }
        if (!(this.Left.Count == 1 && this.Right.Count == 1))   // Must be "m1 <= m2"
        {
          return false;
        }
        if (!(this.Left[0].Degree == 1))                 // Must be "m1 == k1 * x"
        {
          return false;
        }
        if (!this.Right[0].IsConstant)                  // Must be "m2 == k2"
        {
          return false;
        }

        return true;    // It is "k1 * x <= k2 "
      }
    }

    private bool IsMonomialOfInt(out MonomialOfInt<Variable> m)
    {
      if (!this.relation.HasValue && this.left.Count == 1)
      {
        m = this.left[0];
        return true;
      }
      else
      {
        m = default(MonomialOfInt<Variable>);
        return false;
      }
    }

    /// <summary>
    /// Add a constant to the left of the PolynomialOfInt
    /// </summary>
    /// <param name="p"></param>
    public void PlusConstant(int p)
    {
      Contract.Requires((object)p != null);
      Contract.Requires(!this.Relation.HasValue);

      this.Left.Add(new MonomialOfInt<Variable>(p));
    }

    #endregion

    #region Constructors

    /// <summary>
    /// A PolynomialOfInt made by just one MonomialOfInt...
    /// </summary>
    private PolynomialOfInt(MonomialOfInt<Variable> monome)
    {
      this.relation = null;

      // To please the Spec# compiler, the lines below have been modified
      // this.left = new List<MonomialOfInt<Variable>>(1);
      // this.left.Add(monome);

      List<MonomialOfInt<Variable>> this_left = new List<MonomialOfInt<Variable>>(1);
      this_left.Add(monome);
      this.left = this_left;
      //^ base();

      this.right = null;
    }


    /// <summary>
    /// If <code>monomes == (k1, xs1) ... (kn, xsn)</code> then it constructs the polynome <code>k1*xs1 +  ... + kn* xsn</code>
    /// </summary>
    private PolynomialOfInt(List<MonomialOfInt<Variable>> monomes)
    {
      this.relation = null;
      this.left = monomes;
      this.right = null;
    }

    /// <summary>
    /// If <code>left == (k1, xs1) ... (kn, xsn)</code> and <code>right == (c1, ys1) ... (cm, ysm)</code>, then it constructs the polynome standing for 
    /// <code> k1*xs1 +  ... + kn* xsn op c1*cs1 + ...  + cn*csn</code>
    /// </summary>
    /// <param name="op">Must be a binary operator</param>
    private PolynomialOfInt(ExpressionOperator op, List<MonomialOfInt<Variable>> left, List<MonomialOfInt<Variable>> right)
    {
      Contract.Requires(op.IsBinary());

      this.relation = op;
      this.left = left;
      this.right = right;
    }

    /// <summary>
    /// If <code>left == (k1, xs1) ... (kn, xsn)</code> and <code>right == (c1, ys1)</code>, then it constructs the polynome standing for 
    /// <code> k1*xs1 +  ... + kn* xsn op c1*ys1</code>
    /// </summary>
    /// <param name="op">Must be a binary operator</param>
    private PolynomialOfInt(ExpressionOperator op, List<MonomialOfInt<Variable>> left, MonomialOfInt<Variable> right)
    {
      Contract.Requires(op.IsBinary());

      this.relation = op;
      this.left = left;
      this.right = new List<MonomialOfInt<Variable>>();
      this.right.Add(right);
    }

    /// <summary>
    /// If <code>left == (k1, xs1) ... (kn, xsn)</code> and <code>right == (c1, ys1) ... (cm, ysm)</code>, then it constructs the polynome standing for 
    /// <code> k1*xs1 +  ... + kn* xsn op c1*cs1 + ...  + cn*csn</code>
    /// </summary>
    /// <param name="op">Must be a binary operator</param>
    /// <param name="left">shoukd be such that left.Relation == null</param>
    /// <param name="right">shoukd be such that right.Relation == null</param>
    private PolynomialOfInt(ExpressionOperator op, PolynomialOfInt<Variable, Expression> left, PolynomialOfInt<Variable, Expression> right)
    {
      Contract.Requires(op.IsBinary());
      Contract.Requires(!left.Relation.HasValue);
      Contract.Requires(!right.Relation.HasValue);

      this.relation = op;
      this.left = left.left;
      this.right = right.left;
    }

    private PolynomialOfInt(PolynomialOfInt<Variable, Expression> other)
    {
      this.relation = other.relation;
      this.left = new List<MonomialOfInt<Variable>>(other.left);
      this.right = other.right != null ? new List<MonomialOfInt<Variable>>(other.right) : null;
    }

    #endregion

    #region Manipulation of the PolynomialOfInt: Creates a PolynomialOfInt from a PureExpression and puts it into a canonical form

    /// <summary>
    /// Convert the pure expression <code>exp</code> into a PolynomialOfInt, that is somehow simpler to handle.
    /// Essentially, it gets rid of all the parentheses, it performs the(simple) multiplications, additions, etc.
    /// </summary>
    /// <param name="exp">The expression that will be decoded</param>
    /// <param name="decoder">The decoder for the expression</param>
    /// <param name="result">If the result is true, then it returns a PolynomialOfInt into a canonical form</param>
    static public bool TryToPolynomialForm(Expression/*!*/ exp, IExpressionDecoder<Variable, Expression>/*!*/ decoder,
      out PolynomialOfInt<Variable, Expression> result)
    {
      PolynomialOfInt<Variable, Expression> polLeft, polRight;
      bool success;

      // We check if it is in the cache
      // We do not want to do it for variables, as there are some problems if we want to refine them or not
      //if (!decoder.IsVariable(exp) /*&& cache_PolynomialOfIntForm.TryGetValue(exp, out result)*/)
      //{
      //  return true;
      //}

      if (decoder == null)
      {
        result = null;
        return false;
      }

      switch (decoder.OperatorFor(exp))   // Depending on the operator of the expression...
      {
        #region All the cases to "flat" the expression e to a PolynomialOfInt (there is the exception for division of PolynomialOfInts : not handled yet)
        case ExpressionOperator.Constant:
          //Sometimes we do not know the type of the constant, we try to extract the value,             

          var value = new VisitorForEvalConstant(decoder).Visit(exp, Unit.Value);

          if (value.Two && value.One != Int32.MaxValue && value.One != Int32.MinValue)
          {
            var constantMonome = new MonomialOfInt<Variable>(value.One);

            success = true;
            result = new PolynomialOfInt<Variable, Expression>(constantMonome);
          }
          else
          {
            success = false;
            result = null;
          }
          break;

        case ExpressionOperator.ConvertToInt32:
        case ExpressionOperator.ConvertToInt8:
          // We forget about the conversion....
          success = TryToPolynomialForm(decoder.LeftExpressionFor(exp), decoder, out result);
          break;

        case ExpressionOperator.ConvertToUInt32:
          // We specialize this case
          var constValue = new IntervalEnvironment<Variable, Expression>.EvalConstantVisitor(decoder).Visit(decoder.LeftExpressionFor(exp), new IntervalEnvironment<Variable, Expression>(decoder, VoidLogger.Log));

          Rational v;
          if (constValue.TryGetSingletonValue(out v) && v.IsInteger && v < 0 )
          {
            success = false;
            result = null;
          }
          else
          {
            success = TryToPolynomialForm(decoder.LeftExpressionFor(exp), decoder, out result);
          }
          break;

        case ExpressionOperator.ConvertToFloat64:
        case ExpressionOperator.ConvertToFloat32:
        case ExpressionOperator.ConvertToUInt8:
        case ExpressionOperator.ConvertToUInt16:
        case ExpressionOperator.ConvertToInt64:
          // Approximation ...
          success = false;
          result = null;
          break;

        case ExpressionOperator.Variable:               // return (1, e)
          var variableMonome = new MonomialOfInt<Variable>(decoder.UnderlyingVariable(exp));

          success = true;
          result = new PolynomialOfInt<Variable, Expression>(variableMonome);
          break;

        case ExpressionOperator.And:
        case ExpressionOperator.Or:
        case ExpressionOperator.Xor:
        case ExpressionOperator.LogicalAnd:
        case ExpressionOperator.LogicalNot:
        case ExpressionOperator.LogicalOr:
          success = false;
          result = null;
          break;

        // The cases below are ok
        case ExpressionOperator.Equal:
        case ExpressionOperator.Equal_Obj:
        case ExpressionOperator.NotEqual:
        case ExpressionOperator.LessThan:
        case ExpressionOperator.LessThan_Un:
        case ExpressionOperator.LessEqualThan:
        case ExpressionOperator.LessEqualThan_Un:
        case ExpressionOperator.GreaterThan:
        case ExpressionOperator.GreaterThan_Un:
        case ExpressionOperator.GreaterEqualThan:
        case ExpressionOperator.GreaterEqualThan_Un:
          if (TryToPolynomialForm(decoder.LeftExpressionFor(exp), decoder, out polLeft)
            && TryToPolynomialForm(decoder.RightExpressionFor(exp), decoder, out polRight)
            && !polLeft.Relation.HasValue
            && !polRight.Relation.HasValue)
          {
            success = true;
            result = new PolynomialOfInt<Variable, Expression>(decoder.OperatorFor(exp), polLeft.left, polRight.left);
          }
          else
          {
            success = false;
            result = null;
          }
          break;

        case ExpressionOperator.Addition:   // (m1 + ... + mt) + (n1 + ... + nk) = (m1 + ... + mn + n1  + ... nk)
          if (TryToPolynomialForm(decoder.LeftExpressionFor(exp), decoder, out  polLeft)
            && TryToPolynomialForm(decoder.RightExpressionFor(exp), decoder, out polRight))
          {
            success = true;
            result = Concatenate(polLeft, polRight);
          }
          else
          {
            success = false;
            result = null;
          }
          break;

        case ExpressionOperator.Subtraction: // (m1 + ... + mt) - (n1 + ... + nk) = (m1 + ... + mn - n1  - ... - nk)
          if (TryToPolynomialForm(decoder.LeftExpressionFor(exp), decoder, out polLeft)
            && TryToPolynomialForm(decoder.RightExpressionFor(exp), decoder, out polRight))
          {
            success = TryToPolynomialFormHelperForSubtraction(polLeft, polRight, out result);
          }
          else
          {
            success = false;
            result = null;
          }
          break;

        case ExpressionOperator.Modulus: // We give up for it
          success = false;
          result = null;
          break;

        case ExpressionOperator.Multiplication: // (m1 + ... + mt) * (n1 + ... + nk) = (m1*n1 + m1*n2 + ... + mt*n(k-1)+ mt*nk)
          var isPolLeft = TryToPolynomialForm(decoder.LeftExpressionFor(exp), decoder, out polLeft);
          var isPolRight = TryToPolynomialForm(decoder.RightExpressionFor(exp), decoder, out polRight);

          if (isPolLeft && isPolRight)
          {
            success = TryToPolynomialFormHelperForMultiplication(polLeft, polRight, out result);
          }
          // Handle the case exp * m
          else if (isPolLeft || isPolRight)
          {
            Expression notPolynomialOfInt;

            // Swap so that right is always the non-PolynomialOfInt
            if (isPolRight)
            {
              polLeft = polRight;
              isPolLeft = true;

              polRight = default(PolynomialOfInt<Variable, Expression>);
              isPolRight = false;

              notPolynomialOfInt = decoder.LeftExpressionFor(exp);
            }
            else
            {
              notPolynomialOfInt = decoder.RightExpressionFor(exp);
            }

            MonomialOfInt<Variable> m;
            if (polLeft.IsMonomialOfInt(out m) && TryMultiplyExpressionByMonomialOfInt(notPolynomialOfInt, m, decoder, out result))
            {
              success = true;
            }
            else
            {
              success = false;
              result = null;
            }
          }
          else
          {
            success = false;
            result = null;
          }
          break;

        case ExpressionOperator.ShiftLeft: // (m1 + ... + mt) << (n1 + ... + nk) : we give up...
          success = false;
          result = null;
          break;

        case ExpressionOperator.ShiftRight: // (m1 + ... + mt) >> (n1 + ... + nk) : we give up...
          {
            int k;
            if (decoder.IsConstantInt(decoder.RightExpressionFor(exp), out k) && k >= 0)
            {
              if (TryToPolynomialForm(decoder.LeftExpressionFor(exp), decoder, out polLeft))
              {
                int coeff;

                try
                {
                    coeff = 1 << k;
                }
                catch (ArithmeticExceptionRational)
                { // If we've got an exception, it means that we cannot convert this PolynomialOfInt 
                  result = null;
                  success = false;

                  return success;
                }

                var mList = new List<MonomialOfInt<Variable>>();
                foreach (MonomialOfInt<Variable> m in polLeft.Left)
                {
                  int newCoeff;

                  try
                  {
                    newCoeff = m.K / coeff;
                  }
                  catch (ArithmeticExceptionRational)
                  {
                    success = false;
                    result = null;

                    return success;
                  }

                  var tmp = new MonomialOfInt<Variable>(newCoeff, m.Variables);
                  mList.Add(tmp);
                }
                success = true; // Already true here, but it is to make the code more readable
                result = new PolynomialOfInt<Variable, Expression>(mList);
              }
              else
              {
                success = false;
                result = null;
              }
            }
            else
            {
              success = false;
              result = null;
            }
          }
          break;

        case ExpressionOperator.SizeOf: // We try to extract the sizeof of the expression, if we fail we give up
          int size;
          if (decoder.TrySizeOf(exp, out size))
          {
            MonomialOfInt<Variable> constantMonome = new MonomialOfInt<Variable>(size);
            result = new PolynomialOfInt<Variable, Expression>(constantMonome);
            success = true;
          }
          else
          {
            success = false;
            result = null;
          }
          break;

        case ExpressionOperator.Division: // (m1 + ... + mt) / (n1 + ... + nk) = ? We do just simple cases, and we did not made it a complete PolynomialOfInt division

          if (TryToPolynomialForm(decoder.LeftExpressionFor(exp), decoder, out polLeft)
            && TryToPolynomialForm(decoder.RightExpressionFor(exp), decoder, out polRight))
          {
            success = TryToPolynomialFormHelperForDivision(polLeft, polRight, out result);
          }
          else
          {
            success = false;
            result = null;
          }
          break;

        case ExpressionOperator.Not:
        case ExpressionOperator.UnaryMinus: // -(m1 + ... + mt) = -m1 - ... -mt

          if (TryToPolynomialForm(decoder.LeftExpressionFor(exp), decoder, out polLeft))
          {
            success = TryMinus(polLeft, out result);
          }
          else
          {
            success = false;
            result = null;
          }
          break;

        // If we do not know it, we treat it as just a symbol
        case ExpressionOperator.Unknown:
          result = new PolynomialOfInt<Variable, Expression>(new MonomialOfInt<Variable>(decoder.UnderlyingVariable(exp)));
          success = true;
          break;

        case ExpressionOperator.WritableBytes:
          result = new PolynomialOfInt<Variable, Expression>(ExtractWritableBytesExpression(decoder.LeftExpressionFor(exp), decoder, out success));
          break;


        default:
          Contract.Assert(false, "Unknown case ... ");

          success = false;

          throw new AbstractInterpretationException("Error, unknown case : " + exp);
        #endregion
      }

      if (success)
      {
        Contract.Assert(result != null); // Invariant success => result != null

        try
        {
          result = result.ToCanonicalForm();

          //if (!decoder.IsVariable(exp))
          //{
          //  cache_PolynomialOfIntForm.Add(exp, result);
          //}
        }
        catch (ArithmeticExceptionRational)
        {
          result = null;
          success = false;
        }
      }

      return success;
    }

    private static bool TryMultiplyExpressionByMonomialOfInt(Expression exp, MonomialOfInt<Variable> m, IExpressionDecoder<Variable, Expression> decoder, out PolynomialOfInt<Variable, Expression> result)
    {
      PolynomialOfInt<Variable, Expression> polLeft, polRight;

      switch (decoder.OperatorFor(exp))
      {
        case ExpressionOperator.Addition:
          // (e1 + e2) * m = (e1 * m) + (e2 * m)
          if (TryMultiplyExpressionByMonomialOfInt(decoder.LeftExpressionFor(exp), m, decoder, out polLeft) &&
            TryMultiplyExpressionByMonomialOfInt(decoder.RightExpressionFor(exp), m, decoder, out polRight))
          {
            result = Concatenate(polLeft, polRight).ToCanonicalForm();
            return true;
          }
          /* else*/
          goto default;

        case ExpressionOperator.Multiplication:
          // (e1 * e2) * m = (e1 * m) * (e2 * m)
          if (TryMultiplyExpressionByMonomialOfInt(decoder.LeftExpressionFor(exp), m, decoder, out polLeft) &&
            TryMultiplyExpressionByMonomialOfInt(decoder.RightExpressionFor(exp), m, decoder, out polRight))
          {
            return TryToPolynomialFormHelperForMultiplication(polLeft, polRight, out result);
          }
          /* else*/
          goto default;

        case ExpressionOperator.Subtraction:
          // (e1 - e2) * m = (e1 * m) - (e2 * m)
          if (TryMultiplyExpressionByMonomialOfInt(decoder.LeftExpressionFor(exp), m, decoder, out polLeft) &&
            TryMultiplyExpressionByMonomialOfInt(decoder.RightExpressionFor(exp), m, decoder, out polRight))
          {
            return TryToPolynomialFormHelperForSubtraction(polLeft, polRight, out result);
          }
          /* else*/
          goto default;


        case ExpressionOperator.Constant:
          // k * m = km
          var value = new VisitorForEvalConstant(decoder).Visit(exp, Unit.Value);

          if (value.Two && value.One != Int32.MinValue && value.One != Int32.MaxValue)
          {
            var k = value.One * m.K;

            result = new PolynomialOfInt<Variable, Expression>(new MonomialOfInt<Variable>(value.One * m.K, m.Variables));

            return true;
          }
          /* else */
          goto default;

        case ExpressionOperator.Division:
          // Just the case (e1 / e2) * e2 == e1
          if (TryToPolynomialForm(decoder.RightExpressionFor(exp), decoder, out polRight))
          {
            MonomialOfInt<Variable> tmp;
            if (polRight.IsMonomialOfInt(out tmp) && m.IsEquivalentTo(tmp))
            {
              return TryToPolynomialForm(decoder.LeftExpressionFor(exp), decoder, out result);
            }
          }
          /* else */
          goto default;

        case ExpressionOperator.UnaryMinus:
          // -(e1) * m =  -(e1 * m)
          if (TryToPolynomialForm(decoder.LeftExpressionFor(exp), decoder, out polLeft))
          {
            return TryMinus(polLeft, out result);
          }
          /* else */
          goto default;

        case ExpressionOperator.Variable:
        case ExpressionOperator.Unknown:
          // x * (k * xs) = k * [x; xs]
          var newvars = new List<Variable>(m.Variables);
          newvars.Add(decoder.UnderlyingVariable(exp));

          result = new PolynomialOfInt<Variable, Expression>(new MonomialOfInt<Variable>(m.K, newvars));
          return true;

        case ExpressionOperator.WritableBytes:
        case ExpressionOperator.ConvertToFloat32:
        case ExpressionOperator.ConvertToFloat64:
        case ExpressionOperator.ConvertToInt32:
        case ExpressionOperator.ConvertToUInt16:
        case ExpressionOperator.ConvertToUInt32:
        case ExpressionOperator.ConvertToUInt8:
        case ExpressionOperator.And:
        case ExpressionOperator.Equal:
        case ExpressionOperator.Equal_Obj:
        case ExpressionOperator.GreaterEqualThan:
        case ExpressionOperator.GreaterEqualThan_Un:
        case ExpressionOperator.GreaterThan:
        case ExpressionOperator.GreaterThan_Un:
        case ExpressionOperator.LessEqualThan:
        case ExpressionOperator.LessEqualThan_Un:
        case ExpressionOperator.LessThan:
        case ExpressionOperator.LessThan_Un:
        case ExpressionOperator.Not:
        case ExpressionOperator.NotEqual:
        case ExpressionOperator.Or:
        case ExpressionOperator.Xor:
        case ExpressionOperator.ShiftLeft:
        case ExpressionOperator.ShiftRight:
        case ExpressionOperator.SizeOf:
        case ExpressionOperator.Modulus:
        default:
          result = default(PolynomialOfInt<Variable, Expression>);
          return false;
      }
    }

    /// <summary>
    /// Construct the PolynomialOfInt "left == value".
    /// It handles left in a monolitic fashion
    /// </summary>
    static public bool TryToPolynomialForm_Eq(Expression left, int value, 
      out PolynomialOfInt<Variable, Expression> pol, 
      IExpressionDecoder<Variable, Expression> decoder)
    {
      Contract.Requires((object)value != null);

      pol = new PolynomialOfInt<Variable, Expression>(ExpressionOperator.Equal,
        new List<MonomialOfInt<Variable>>(1) { new MonomialOfInt<Variable>(decoder.UnderlyingVariable(left)) },
        new List<MonomialOfInt<Variable>>(1) { new MonomialOfInt<Variable>(value) });

      return true;
    }

    /// <summary>
    /// Convert the pure expression <code>left op right</code> into a PolynomialOfInt, that is somehow simpler to handle.
    /// Essentially, it gets rid of all the parenthesis, it performs the(simple) multiplications, additions, etc.
    /// </summary>
    /// <param name="result">if the result is true, it is a PolynomialOfInt in canonical form</param>
    static public bool TryToPolynomialForm(ExpressionOperator op, Expression/*!*/ left, Expression/*!*/ right, IExpressionDecoder<Variable, Expression>/*!*/ decoder, out PolynomialOfInt<Variable, Expression> result)
    {
      Contract.Requires(op.IsBinary());

      if (decoder.OperatorFor(left).IsRelationalOperator()
        || decoder.OperatorFor(right).IsRelationalOperator())
      {
        result = null;
        return false;
      }
      else
      {
        PolynomialOfInt<Variable, Expression> leftAsPol, rightAsPol;

        if (PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(left, decoder, out leftAsPol)
          && PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(right, decoder, out rightAsPol))
        {
          try
          {
            Contract.Assume(!leftAsPol.Relation.HasValue);
            Contract.Assume(!rightAsPol.Relation.HasValue);

            var tmp = new PolynomialOfInt<Variable, Expression>(op, leftAsPol, rightAsPol);
            result = tmp.ToCanonicalForm();
          }
          catch (ArithmeticExceptionRational)
          {
            result = null;
            return false;
          }

          return true;
        }
        else
        {
          result = null;

          return false;
        }
      }
    }

    static public bool TryToPolynomialFormNoDiv(ExpressionOperator op, Expression/*!*/ left, Expression/*!*/ right, IExpressionDecoder<Variable, Expression>/*!*/ decoder, out PolynomialOfInt<Variable, Expression> result)
    {
      Contract.Requires(op.IsBinary());

      if (decoder.OperatorFor(left).IsRelationalOperator()
        || decoder.OperatorFor(right).IsRelationalOperator())
      {
        result = null;
        return false;
      }


      {
        PolynomialOfInt<Variable, Expression> leftAsPol, rightAsPol;

        if (PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(left, decoder, out leftAsPol)
          && PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(right, decoder, out rightAsPol))
        {
          if (!leftAsPol.IsLinearOctagon || !rightAsPol.IsLinearOctagon)
          {
            result = null;
            return false;
          }

          try
          {
            Contract.Assume(!leftAsPol.Relation.HasValue);
            Contract.Assume(!rightAsPol.Relation.HasValue);

            var tmp = new PolynomialOfInt<Variable, Expression>(op, leftAsPol, rightAsPol);
            result = tmp.ToCanonicalForm();
          }
          catch (ArithmeticExceptionRational)
          {
            result = null;
            return false;
          }

          return true;
        }
        else
        {
          result = null;

          return false;
        }
      }
    }


    /// <param name="result">If the result is true, it is a PolynomialOfInt in canonical form</param>
    static public bool TryToPolynomialForm(ExpressionOperator op, PolynomialOfInt<Variable, Expression>/*!*/ left, PolynomialOfInt<Variable, Expression>/*!*/ right, out PolynomialOfInt<Variable, Expression> result)
    {
      if (!op.IsBinary() || left.Relation != null || right.Relation != null)
      {
        result = null;
        return false;
      }
      else
      {
        try
        {
          Contract.Assert(right.Relation == null);

          // here we return a new PolynomialOfInt. An alternative is to return a PolynomialOfInt in canonical form
          result = new PolynomialOfInt<Variable, Expression>(op, left, right).ToCanonicalForm();
          return true;
        }
        catch (ArithmeticExceptionRational)
        {
          // If there is an exception in the ints, then we simply fail
          result = null;
          return false;
        }
      }
    }

    /// <param name="result">The result: a PolynomialOfInt in canonical form</param>
    /// <returns>Always true</returns>
    static public bool TryToPolynomialForm(ExpressionOperator expressionOperator, List<MonomialOfInt<Variable>> left, List<MonomialOfInt<Variable>> right, out PolynomialOfInt<Variable, Expression> result)
    {
      Contract.Requires(expressionOperator.IsBinary());

      result = new PolynomialOfInt<Variable, Expression>(expressionOperator, left, right).ToCanonicalForm();

      return true;
    }

    static public bool TryToPolynomialForm(bool canonicalByConstruction, ExpressionOperator expressionOperator, List<MonomialOfInt<Variable>> left, List<MonomialOfInt<Variable>> right, out PolynomialOfInt<Variable, Expression> result)
    {
      Contract.Requires(expressionOperator.IsBinary());

      if (canonicalByConstruction)
      {
        result = new PolynomialOfInt<Variable, Expression>(expressionOperator, left, right);

        return true;
      }
      else
      {
        return TryToPolynomialForm(expressionOperator, left, right, out result);
      }
    }


    /// <summary>
    /// Convert the list of MonomialOfInts <code>MonomialOfInts</code> into a PolynomialOfInt in canonical form
    /// </summary>
    /// <param name="pol">The PolynomialOfInt in canonical form</param>
    /// <returns>Always true</returns>
    static public bool TryToPolynomialForm(List<MonomialOfInt<Variable>> MonomialOfInts, out PolynomialOfInt<Variable, Expression> pol)
    {
      if (MonomialOfInts.Count > 1)
      {
        pol = new PolynomialOfInt<Variable, Expression>(MonomialOfInts).ToCanonicalForm();
      }
      else
      {
        pol = new PolynomialOfInt<Variable, Expression>(MonomialOfInts);
      }

      return true;
    }

    static public bool TryToPolynomialForm(bool canonicalByConstruction, List<MonomialOfInt<Variable>> MonomialOfInts, out PolynomialOfInt<Variable, Expression> pol)
    {
      if (canonicalByConstruction)
      {
        pol = new PolynomialOfInt<Variable, Expression>(MonomialOfInts);
        return true;
      }
      else
      {
        return TryToPolynomialForm(MonomialOfInts, out pol);
      }
    }

    #endregion

    #region Manipulation of the PolynomialOfInt : Add a monome
    public PolynomialOfInt<Variable, Expression> AddMonomialOfIntToTheLeft(MonomialOfInt<Variable> m)
    {
      PolynomialOfInt<Variable, Expression> thisCloned = new PolynomialOfInt<Variable, Expression>(this);
      thisCloned.left.Add(m);

      return thisCloned.ToCanonicalForm();
    }

    public PolynomialOfInt<Variable, Expression> AddMonomialOfIntToTheLeft(List<MonomialOfInt<Variable>> m)
    {
      PolynomialOfInt<Variable, Expression> thisCloned = new PolynomialOfInt<Variable, Expression>(this);
      thisCloned.left.AddRange(m);

      return thisCloned.ToCanonicalForm();
    }

    #endregion

    #region ToCanonicalForm, ToPureExpression, Renaming
    /// <summary>
    /// Put this PolynomialOfInt into a canonical form, meaning that all the MonomialOfInt involving variables are on the left, all the constants on the right, and all the easy arithmetic operations are performed
    /// </summary>
    /// <returns>The canonical form of this PolynomialOfInt</returns>
    private PolynomialOfInt<Variable, Expression>/*!*/ ToCanonicalForm()
    {
      PolynomialOfInt<Variable, Expression> canonical;

      //if (cache_CanonicalForms.TryGetValue(this, out canonical))
      //{
      //  return canonical;
      //}

      // Two main cases: this PolynomialOfInt is binded by a relation, or it is just a sequence of MonomialOfInts
      if (this.Relation.HasValue)
      {
        switch (this.Relation.Value)
        {
          case ExpressionOperator.GreaterEqualThan:
            SwapOperands(ExpressionOperator.LessEqualThan);
            break;

          case ExpressionOperator.GreaterEqualThan_Un:
            SwapOperands(ExpressionOperator.LessEqualThan_Un);
            break;

          case ExpressionOperator.GreaterThan:
            SwapOperands(ExpressionOperator.LessThan);
            break;

          case ExpressionOperator.GreaterThan_Un:
            SwapOperands(ExpressionOperator.LessThan_Un);
            break;
        }

        var moved = this.MoveConstantsAndMonomes();
        var simplifiedLeft = Simplify(moved.left);
        var simplifiedRight = Simplify(moved.right);

        Contract.Assume(simplifiedRight.Count == 1, "At this point we expected just a constant on the right side...");

        canonical = new PolynomialOfInt<Variable, Expression>((ExpressionOperator)this.Relation, simplifiedLeft, simplifiedRight);
      }
      else
      {
        List<MonomialOfInt<Variable>> simplified = Simplify(this.left);
        canonical = new PolynomialOfInt<Variable, Expression>(simplified);
      }

      //cache_CanonicalForms.Add(this, canonical);

      return canonical;
    }

    /// <summary>
    /// Swap in place
    /// Works with side effects!!!
    /// </summary>
    private void SwapOperands(ExpressionOperator newOperator)
    {
      Contract.Ensures(this.relation.HasValue);
      Contract.Ensures(this.left == Contract.OldValue(this.right));
      Contract.Ensures(this.right == Contract.OldValue(this.left));

      this.relation = newOperator;

      var tmp = this.left;
      this.left = this.right;
      this.right = tmp;
    }

    /// <summary>
    /// Convert this PolynomialOfInt into a pure expression
    /// </summary>
    public Expression/*!*/ ToPureExpression(IExpressionEncoder<Variable, Expression>/*!*/ encoder)
    {
      Contract.Requires(encoder != null);

      return ToPureExpressionForm(this, encoder);
    }

    /// <summary>
    /// Convert a PolynomialOfInt into a pure expression
    /// </summary>
    /// <param name="encoder">The encoder for constructing an expression</param>
    static public Expression/*!*/ ToPureExpressionForm(PolynomialOfInt<Variable, Expression>/*!*/ p, IExpressionEncoder<Variable, Expression>/*!*/ encoder)
    {
      Contract.Requires(p != null);
      Contract.Requires(encoder != null);

      Expression eLeft = ConvertToPureExpression(p.left, encoder);
      if (p.Relation.HasValue)
      {
        Expression eRight = ConvertToPureExpression(p.right, encoder);
        return encoder.CompoundExpressionFor(ExpressionType.Bool, p.Relation.Value, eLeft, eRight);
      }
      else
      {
        return eLeft;
      }
    }

    /// <summary>
    /// Renames all the occurrences of the variable <code>x</code> to <code>xNew</code>
    /// </summary>
    public PolynomialOfInt<Variable, Expression>/*!*/ Rename(Variable/*!*/ x, Variable/*!*/ xNew)
    {
      return Rename(true, x, xNew);
    }

    /// <summary>
    /// Renames all the occurrences of the variable <code>x</code> to <code>xNew</code>
    /// </summary>
    public PolynomialOfInt<Variable, Expression>/*!*/ Rename(bool canonical, Variable/*!*/ x, Variable/*!*/ xNew)
    {
      // Improve: Use sharing when the renamed PolynomialOfInt is the same

      var leftResult = new List<MonomialOfInt<Variable>>();
      var rightResult = new List<MonomialOfInt<Variable>>();

      foreach (var m in this.Left)
      {
        leftResult.Add(m.Rename(x, xNew));
      }
      if (this.Relation.HasValue)
      {
        foreach (var m in this.Right)
        {
          rightResult.Add(m.Rename(x, xNew));
        }
      }

      PolynomialOfInt<Variable, Expression> result = this.Relation.HasValue
        ? new PolynomialOfInt<Variable, Expression>(this.Relation.Value, leftResult, rightResult)
        : new PolynomialOfInt<Variable, Expression>(leftResult);

      return canonical ? result.ToCanonicalForm() : result;
    }

    /// <summary>
    /// Rename the current PolynomialOfInt according to the translation map.
    /// </summary>
    /// <param name="trMap"></param>
    /// <returns></returns>
    public PolynomialOfInt<Variable, Expression>/*!*/ Rename(IDictionary<Variable, PolynomialOfInt<Variable, Expression>> trMap)
    {
      ///// It is very limited up to now !!!!!!!!!!!!! //////
      //TODO5: do it more generic (if we really need it...)

      Contract.Requires(this.IsLinear);

      var resultLeft = new List<MonomialOfInt<Variable>>();

      foreach (var m in this.Left)
      {
        // we know is linear...
        if (!m.IsConstant && trMap.ContainsKey(m.Variables[0]))
        {
          foreach (var m1 in trMap[m.Variables[0]].Left)
          {
            var newMonomialOfInt = new MonomialOfInt<Variable>(m1.K * m.K, m1.Variables);
            resultLeft.Add(newMonomialOfInt);
          }
        }
        else
        {
          resultLeft.Add(m);
        }
      }

      if (this.relation != null)
      {
        throw new AbstractInterpretationException("Not done yet!!!!!");
      }

      return new PolynomialOfInt<Variable, Expression>(resultLeft).ToCanonicalForm();
    }

    #endregion

    #region Equivalence of PolynomialOfInts

    public bool IsEquivalentTo(PolynomialOfInt<Variable, Expression>/*!*/ other)
    {
      var left = this.ToCanonicalForm();
      var right = other.ToCanonicalForm();

      if (!SimpleEquivalence(left, right))
      {
        return false;
      }
      else
      { // At this point we know they have the same cardinality

        if (!UniformCoefficents(left, right))
        {
          return false;
        }

        foreach (var m in left.Left)
        {
          if (!ContainsMonomialOfIntEquivalentTo(right.left, m))
            return false;
        }

        if (left.Relation.HasValue)
        {
          Contract.Assert(right.Relation.HasValue);

          foreach (var m in left.Right)
          {
            if (!ContainsMonomialOfIntEquivalentTo(right.right, m))
              return false;
          }
        }

        // right contains all the MonomialOfInts in left, and right has the same cardinality of left, so they are equivalent
        return true;
      }
    }

    private static bool SimpleEquivalence(PolynomialOfInt<Variable, Expression> left, PolynomialOfInt<Variable, Expression> right)
    {
      if (left.Degree != right.Degree)
        return false;
      if (left.Left.Count != right.Left.Count)
        return false;
      if (left.Relation != right.Relation)
        return false;
      if ((!left.Relation.HasValue && !right.Relation.HasValue) && (left.Right.Count != right.Right.Count))
        return false;

      return true;
    }

    private static bool UniformCoefficents(PolynomialOfInt<Variable, Expression> left, PolynomialOfInt<Variable, Expression> right)
    {
      var listLeft = left.left;
      var listRight = right.left;

      if (listLeft.Count == 0)
      {
        if (listRight.Count == 0)
        {
          return true;
        }
        else
        {
          listLeft = left.right;
          listRight = right.right;
        }
      }

      var mLeft = listLeft[0];
      MonomialOfInt<Variable> mRight = null;

      foreach (var m in listRight)
      {
        if (mLeft.Variables.Count != m.Variables.Count)
          continue;

        foreach (var v in mLeft.Variables)
        {
          if (!m.Variables.Contains(v))
            goto nextMonomialOfInt;
        }

        mRight = m;   // We found a MonomialOfInt with the same variables
        break;

      nextMonomialOfInt:
        ;
      }

      if (mRight != null)
      {
        int kLeft = mLeft.K;
        int kRight = mRight.K;

        if (kLeft != kRight)
        {
          if (kRight == 0)
            return true;

          int k = kLeft / kRight;

          foreach (var m in right.Left)
          {
            m.K = m.K * k;
          }

          if (right.Relation.HasValue)
          {
            foreach (var m in right.Right)
            {
              m.K = m.K * k;
            }
          }
        }

        return true;
      }
      else
      {
        return false;
      }
    }

    private static bool ContainsMonomialOfIntEquivalentTo(List<MonomialOfInt<Variable>> l, MonomialOfInt<Variable> toFind)
    {
      foreach (MonomialOfInt<Variable> m in l)
      {
        if (m.K != toFind.K)
          continue;

        if (m.Variables.Count != toFind.Variables.Count)
          continue;

        foreach (var v in m.Variables)
        {
          if (!toFind.Variables.Contains(v))
            goto nextMonomialOfInt;
        }

        // At this point, they have the same coefficent, and the same variables, so they are the same
        return true;

      nextMonomialOfInt:
        ;
      }

      return false;
    }

    #endregion

    #region Private methods
    /// <summary>
    /// Move the constants to the right of the relation operator, and the MonomialOfInt to the left.
    /// It makes sense just if this.Relation is LessEqualThan, LessThan, Equal or NotEqual
    /// </summary>
    private PolynomialOfInt<Variable, Expression> MoveConstantsAndMonomes()
    {
      Contract.Requires(this.Relation.HasValue);   // It can be applied just to PolynomialOfInts with a relation operator
      Contract.Requires(this.right != null);

      var newLeft = new List<MonomialOfInt<Variable>>();
      var newRight = new List<MonomialOfInt<Variable>>();

      // Move all the constants to the right, and leave the non-constants on the left
      foreach (var m in this.Left)
      {
        if (!m.IsConstant)    // k * xs, k != 0
        {
          newLeft.Add(m);
        }
        else                                // k * 1
        {
          MonomialOfInt<Variable> mToTheRight = new MonomialOfInt<Variable>(-m.K, m.Variables);
          newRight.Add(mToTheRight);
        }
      }

      // Move all the non-constant MonomialOfInts to the left, and leave the constants on the right
      foreach (MonomialOfInt<Variable> m in this.right)
      {
        if (!m.IsConstant)              // k * xs, k != 0
        {
          var mToTheLeft = new MonomialOfInt<Variable>(-m.K, m.Variables);
          newLeft.Add(mToTheLeft);
        }
        else
        {
          newRight.Add(m);
        }
      }

      // If there is nothing on the left or on the right, we add a zero
      if (newLeft.Count == 0)
      {
        newLeft.Add(new MonomialOfInt<Variable>(0));
      }
      if (newRight.Count == 0)
      {
        newRight.Add(new MonomialOfInt<Variable>(0));
      }

      return new PolynomialOfInt<Variable, Expression>((ExpressionOperator)this.Relation, newLeft, newRight);
    }

    /// <summary>
    /// Simplify the polinomial by performing basic arithmetic operations
    /// </summary>
    static private List<MonomialOfInt<Variable>> Simplify(List<MonomialOfInt<Variable>> monomes)
    {
      // Shortcuts for common and simple cases
      if (monomes.Count <= 1)
        return monomes;

      // keep a cache between the variables of the MonomialOfInts and the actual value. We use this data structure for performances
      var cache = new Dictionary<SetEqual<Variable>, MonomialOfInt<Variable>>();
      var cacheMap = new Dictionary<SetEqual<Variable>, Expression>();

      foreach (MonomialOfInt<Variable> m in monomes)
      {
        var vars = new SetEqual<Variable>(m.Variables);

        // We assume that m.Variables is ordered
        if (cache.ContainsKey(vars))
        {
          // if the string is in the dictionary, we update the value of the coefficent
          MonomialOfInt<Variable> oldMonomialOfInt = cache[vars];
          int newK = oldMonomialOfInt.K + m.K;     // Add the coefficients

          MonomialOfInt<Variable> newMonomialOfInt = new MonomialOfInt<Variable>(newK, oldMonomialOfInt.Variables);
          cache[vars] = newMonomialOfInt;  // Update the MonomialOfInt
        }
        else
        {   // it is a new MonomialOfInt, so we add it to the cache
          cache.Add(vars, m);
        }
      }

      var result = new List<MonomialOfInt<Variable>>(cache.Count);

      var positive = new List<MonomialOfInt<Variable>>(cache.Count);
      var negative = new List<MonomialOfInt<Variable>>(cache.Count);

      var constant = (MonomialOfInt<Variable>) null;

      foreach (var s in cache.Keys)
      {
        var m = cache[s];

        if (m.K != 0)
        {
          if (m.IsConstant)
          {
            constant = m;
          }
          else if (m.K > 0)
          {
            positive.Add(m);
          }
          else
          {
            negative.Add(m);
          }
        }
      }

      result.InsertRange(0, negative);
      result.InsertRange(0, positive);

      if (constant != null)
      {
        result.Insert(result.Count, constant);
      }

      // If everything turned out to be zero, we want to keep just one
      if (result.Count == 0)
      {
        result.Add(new MonomialOfInt<Variable>(0));
      }

      return result;
    }

    public class ListEqual<Element>
    {
      List<Element> list;
      public ListEqual(List<Element> a)
      {
        this.list = a;
      }

      public override int GetHashCode()
      {
        if (list.Count == 0)
          return 0;
        else
          return list[0].GetHashCode();
      }

      public override bool Equals(object obj)
      {
        if (!(obj is ListEqual<Element>))
        {
          return false;
        }

        List<Element> other = ((ListEqual<Element>)obj).list;

        if (list.Count != other.Count)
        {
          return false;
        }

        foreach (Element el in list)
        {
          if (!other.Contains(el))
          {
            return false;
          }
        }
        return true;
      }
    }

    public class SetEqual<Element>
    {
      Set<Element> elements;

      public SetEqual(Set<Element> a)
      {
        this.elements = a;
      }

      public SetEqual(List<Element> l)
      {
        this.elements = new Set<Element>(l);
      }

      public override int GetHashCode()
      {
        return this.elements.GetHashCode();
      }

      public override bool Equals(object obj)
      {
        SetEqual<Element> asSet = obj as SetEqual<Element>;

        if (asSet == null)
          return false;

        if (this.elements.Count != asSet.elements.Count)
          return false;

        return this.elements.IsSubset(asSet.elements);

      }
    }
    #endregion

    #region Overridden
    //^ [Confined]
    public override string/*!*/ ToString()
    {
      if (this.relation == null)
      {
        return listToString(this.left);
      }
      else
      {
        string leftStr = listToString(this.left);
        string rightStr = listToString(this.right);
        string retVal = leftStr + " " + this.relation + " " + rightStr;

        return retVal;
      }
    }

    static private string listToString(List<MonomialOfInt<Variable>> l)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      StringBuilder s = new StringBuilder();
      if (l.Count == 0)
        return "()";
      else
      {
        s.Append("(");
        foreach (MonomialOfInt<Variable> m in l)
        {
          s.Append(m.ToString() + ", ");
        }
        s.Append(")");

        string stmp = s.ToString();
        if (stmp.Length > 4)
        {
          stmp = stmp.Remove(stmp.Length - 4, 3);
        }
        return stmp;
      }
    }
    #endregion

    #region (almost) Private helper methods

    /// <summary>
    /// Meaning: let <code>polLeft = (m1 + ... + mt)</code> and <code>polRight = (n1 + ... + nk)</code>, 
    /// it returns the polynome <code>(m1 + ... + mt + n1 + ... + nk)</code>
    /// </summary>
    private static PolynomialOfInt<Variable, Expression> Concatenate(PolynomialOfInt<Variable, Expression> polLeft, PolynomialOfInt<Variable, Expression> polRight)
    {
      var concat = new List<MonomialOfInt<Variable>>(polLeft.Left.Count + polRight.Left.Count);
      concat.AddRange(polLeft.Left);          // Add all the monomes on the left
      concat.AddRange(polRight.Left);         // Add all the monomes on the right

      return new PolynomialOfInt<Variable, Expression>(concat);
    }

    private static bool TryToPolynomialFormHelperForSubtraction(PolynomialOfInt<Variable, Expression> polLeft, PolynomialOfInt<Variable, Expression> polRight, out PolynomialOfInt<Variable, Expression> result)
    {
      PolynomialOfInt<Variable, Expression> minusPolRight;
      if (TryMinus(polRight, out minusPolRight))
      {
        result = Concatenate(polLeft, minusPolRight);
        return true;
      }
      else
      {
        result = null;
        return false;
      }
    }

    private static bool TryToPolynomialFormHelperForMultiplication(PolynomialOfInt<Variable, Expression> polLeft, PolynomialOfInt<Variable, Expression> polRight, out PolynomialOfInt<Variable, Expression> result)
    {
      var left = polLeft.left;
      var right = polRight.left;
      var multiplicationResult = new List<MonomialOfInt<Variable>>(left.Count * right.Count);

      foreach (var l in left)         // l = (k, xs)
        foreach (var r in right)   // r  = (c, ys)
        {
          int resultCoeff;
          // result = (k*c, xs... ys)
          try
          {
            resultCoeff = l.K * r.K;
          }
          catch (ArithmeticExceptionRational)
          { // If we get an overflow in the multiplication, we get throw away the multiplication
            result = null;
            return false;
          }

          var concat = new List<Variable>(l.Variables.Count + r.Variables.Count);
          concat.AddRange(l.Variables);
          concat.AddRange(r.Variables);

          var resultMonomialOfInt = new MonomialOfInt<Variable>(resultCoeff, concat);

          multiplicationResult.Add(resultMonomialOfInt);
        }

      result = new PolynomialOfInt<Variable, Expression>(multiplicationResult);
      return true;
    }

    // We consider just the easy cases, when polLeft or polRight are constants
    private static bool TryToPolynomialFormHelperForDivision(PolynomialOfInt<Variable, Expression> polLeft, PolynomialOfInt<Variable, Expression> polRight, out PolynomialOfInt<Variable, Expression> result)
    {
      int leftVal, rightVal;

      if(polLeft.IsConstant(out leftVal) && polRight.IsConstant(out rightVal))
      {
        result = new PolynomialOfInt<Variable, Expression>(new MonomialOfInt<Variable>(leftVal / rightVal));
        return true;
      }

      result = null;
      return false;
    }

    private static bool TryMinus(PolynomialOfInt<Variable, Expression> p, out PolynomialOfInt<Variable, Expression> result)
    {
      var minusP = new List<MonomialOfInt<Variable>>(p.Left.Count);
      foreach (var m in p.Left)
      {
        int v = -m.K;
        if (v != Int32.MaxValue && v != Int32.MinValue)
        {
          var minusM = new MonomialOfInt<Variable>(v, m.Variables);
          minusP.Add(minusM);
        }
        else  // PolynomialOfInt with an infinity component does not make sense, so we fail
        {
          result = null;
          return false;
        }
      }

      result = new PolynomialOfInt<Variable, Expression>(minusP);
      return true;
    }

    private static Expression/*!*/ ConvertToPureExpression(List<MonomialOfInt<Variable>> p, IExpressionEncoder<Variable, Expression>/*!*/ encoder)
    {
      Contract.Requires(encoder != null);

      var result = default(Expression);
      if (p.Count == 0)
      {
        return encoder.ConstantFor(0); //return new Constant<int>(0);
      }
      else
      {
        foreach (MonomialOfInt<Variable> m in p)
        {
          bool neg;
          Expression newMonomialOfInt = ConvertToPureExpression(m, encoder, out neg);

          if (result == null)
          {
            if (neg)
              result = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.UnaryMinus, newMonomialOfInt);
            else
              result = newMonomialOfInt;
          }
          else if (neg)
            result = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Subtraction, result, newMonomialOfInt);  // result = result - m
          else
            result = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, result, newMonomialOfInt);  // result = result + m

        }
        return result;
      }
    }

    private static Expression ConvertToPureExpression(MonomialOfInt<Variable> m, IExpressionEncoder<Variable, Expression>/*!*/ encoder, out bool neg)
    {
      Contract.Requires(encoder != null);

      if (m.IsConstant)
      {
        neg = m.K < 0;
        return encoder.ConstantFor(Math.Abs(m.K));
      }
      else
      {
        var result = default(Expression);
        bool first = true;

        neg = false;

        if (m.K == -1)
        {
          neg = true;
        }
        else if (m.K != 1)
        {
          result = encoder.ConstantFor(m.K);
          first = false;
        }

        foreach (var v in m.Variables)
        {
          var vExp = encoder.VariableFor(v);
          if (first)
          {
            result = vExp;
            first = false;
          }
          else
          {
            result = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Multiplication, result, vExp); // result = result * v      
          }
        }

        return result;
      }

    }

    private static List<MonomialOfInt<Variable>> ExtractWritableBytesExpression(Expression e, IExpressionDecoder<Variable, Expression>/*!*/ decoder, out bool success)
    {
      List<MonomialOfInt<Variable>> polLeft, polRight;
      List<MonomialOfInt<Variable>> result;
      bool b1, b2;
      Expression lng;

      switch (decoder.OperatorFor(e))
      {
        case ExpressionOperator.Addition:
          polLeft = ExtractWritableBytesExpression(decoder.LeftExpressionFor(e), decoder, out b1);
          polRight = ExtractWritableBytesExpression(decoder.RightExpressionFor(e), decoder, out b2);
          success = b1 || b2;

          if (polRight != null && polLeft != null)
          {
            foreach (MonomialOfInt<Variable> mon in polRight)
            {
              mon.K = -mon.K;
            }

            result = new List<MonomialOfInt<Variable>>(polLeft);
            result.AddRange(polRight);
          }
          else
          {
            result = default(List<MonomialOfInt<Variable>>);
            success = false;
          }
          break;

        //We don't expect other than k*x
        case ExpressionOperator.Multiplication:
          PolynomialOfInt<Variable, Expression> temp;
          success = TryToPolynomialForm(e, decoder, out temp);
          result = temp.left;
          break;

        case ExpressionOperator.ConvertToInt32:
        case ExpressionOperator.ConvertToUInt16:
        case ExpressionOperator.ConvertToUInt32:
        case ExpressionOperator.ConvertToUInt8:
        case ExpressionOperator.ConvertToFloat32:
        case ExpressionOperator.ConvertToFloat64:
          result = ExtractWritableBytesExpression(decoder.LeftExpressionFor(e), decoder, out success);
          break;

        case ExpressionOperator.Variable:
#if !SUBPOLY_ONLY
          if (decoder.TryGetAssociatedExpression(e, AssociatedInfo.WritableBytes, out lng))
          {
            MonomialOfInt<Variable> lngMonome = new MonomialOfInt<Variable>(decoder.UnderlyingVariable(lng));
            success = true;
            result = new List<MonomialOfInt<Variable>>();
            result.Add(lngMonome);
          }
          else
#endif
          {
            success = false;
            result = new List<MonomialOfInt<Variable>>();
            result.Add(new MonomialOfInt<Variable>(decoder.UnderlyingVariable(e)));
          }
          break;

        default:

          PolynomialOfInt<Variable, Expression> poly;
          success = TryToPolynomialForm(e, decoder, out poly);

          if (success && poly.Relation == null)
            result = poly.left;
          else
            result = null;

          break;
      }
      return result;
    }

    #endregion


    /// <summary>
    /// Evaluate the constant to a int
    /// </summary>
    private class VisitorForEvalConstant
      : GenericTypeExpressionVisitor<Variable, Expression, Unit, Pair<int, bool>>
    {
      readonly private static Pair<int, bool> NotSuccessfull = new Pair<int, bool>(Int32.MaxValue, false);

      public VisitorForEvalConstant(IExpressionDecoder<Variable, Expression>/*!*/ decoder)
        : base(decoder)
      {
      }

      public override Pair<int, bool> VisitBool(Expression exp, Unit input)
      {
        return NotSuccessfull;
      }

      public override Pair<int, bool> VisitInt8(Expression exp, Unit input)
      {
        SByte value;
        if (Decoder.TryValueOf<SByte>(exp, ExpressionType.Int8, out value))
        {
          return Success(value);
        }
        else
        {
          return NotSuccessfull;
        }
      }

      public override Pair<int, bool> VisitInt16(Expression exp, Unit input)
      {
        Int16 value;
        if (Decoder.TryValueOf<Int16>(exp, ExpressionType.Int16, out value))
          return Success(value);
        else
          return NotSuccessfull;
      }

      public override Pair<int, bool> VisitInt32(Expression exp, Unit input)
      {
        Int32 value;
        if (Decoder.TryValueOf<Int32>(exp, ExpressionType.Int32, out value))
          return Success(value);
        else
          return NotSuccessfull;
      }

      public override Pair<int, bool> VisitInt64(Expression exp, Unit input)
      {
        Int64 value;
        if (Decoder.TryValueOf<Int64>(exp, ExpressionType.Int64, out value))
        {
          if (CanRepresentExactly(value))
            return Success((Int32)value);
        }
        return NotSuccessfull;
      }

      private bool CanRepresentExactly(long value)
      {
        return Int32.MinValue < value && value < Int32.MaxValue;
      }

      private bool CanRepresentExactly(UInt32 value)
      {
        return value < Int32.MaxValue;
      }

      public override Pair<int, bool> VisitString(Expression exp, Unit input)
      {
        return NotSuccessfull;
      }

      public override Pair<int, bool> VisitUInt8(Expression exp, Unit input)
      {
        Byte value;
        if (Decoder.TryValueOf<Byte>(exp, ExpressionType.UInt8, out value))
          return Success(value);
        else
          return NotSuccessfull;
      }

      public override Pair<int, bool> VisitUInt16(Expression exp, Unit input)
      {
        UInt16 value;
        if (Decoder.TryValueOf<UInt16>(exp, ExpressionType.UInt16, out value))
          return Success(value);
        else
          return NotSuccessfull;
      }

      public override Pair<int, bool> VisitUInt32(Expression exp, Unit input)
      {
        UInt32 value;
        if (Decoder.TryValueOf<UInt32>(exp, ExpressionType.UInt32, out value) && CanRepresentExactly(value))
          return Success((Int32) value);
        
          return NotSuccessfull;
      }

      public override Pair<int, bool> Default(Expression exp)
      {
        return NotSuccessfull;
      }

      private Pair<int, bool> Success(int value)
      {
        return new Pair<int, bool>(value, true);
      }

    }
  }

  /// <summary>
  /// A MonomialOfInt is a pair <code>(k, x1...xn)</code> where <code>k</code> is a int number and <code>xi</code> is a variable
  /// </summary>
#if SUBPOLY_ONLY
  internal
#else
  public
#endif
 class MonomialOfInt<Variable>
  {
    #region Cached values
    static private IComparer<Variable> varCompaper = new ExpressionComparer<Variable>();
    #endregion

    #region Private fields
    private int k;
    readonly private List<Variable> variables;     // The sorted list of the variables in the MonomialOfInt
    
    #endregion

    #region Getters
    public int K
    {
      get
      {
        return this.k;
      }
      set
      {
        Contract.Requires((object)value != null);

        this.k = value;
      }
    }

    public List<Variable> Variables
    {
      get
      {
        // Here it may be better to return a copy, but we trust the context it is not going to change it?
        return this.variables;
      }
    }

    public bool IsLinear
    {
      get
      {
        return this.Degree <= 1;
      }
    }


    public bool IsConstant
    {
      get
      {
        return this.variables.Count == 0;
      }
    }

    public bool IsVariable(out Variable var)
    {
      if (this.Degree == 1)
      {
        var = this.variables[0];

        return true;
      }
      else
      {
        var = default(Variable);

        return false;
      }
    }

    public int Degree
    {
      get
      {
        return this.variables.Count;
      }
    }



    #endregion

    #region Constructors

    ///<summary>
    /// Constructs a constant form a <code>int</code>
    ///</summary>
    public MonomialOfInt(int r)
    {

      this.k = r;
      this.variables = new List<Variable>(0);
    }

    ///<summary>
    ///Constructs a MonomialOfInt made by only a variable
    ///</summary>
    public MonomialOfInt(Variable x)
    {
      this.k = 1;
      this.variables = new List<Variable>(1);
      this.variables.Add(x);
    }

    /// <summary>
    /// Constructs a MonomialOfInt with just one variable
    /// </summary>
    public MonomialOfInt(int k, Variable x)
    {
      this.k = k;
      this.variables = new List<Variable>() { x };
    }


    /// <summary>
    /// Constructs a monome with several variables (i.e. of a degree > 1)
    /// </summary>
    //^ [NotDelayed]
    public MonomialOfInt(int k, IEnumerable<Variable> xs)
    {
      this.k = k;
      this.variables = new List<Variable>(xs);
      //^ base();
      this.variables.Sort(varCompaper);
    }

    #endregion

    /// <summary>
    /// Return a MonomialOfInt where the variable <code>x</code> is replaced with <code>xNew</code>
    /// </summary>
    public MonomialOfInt<Variable>/*!*/ Rename(Variable/*!*/ x, Variable/*!*/ xNew)
    {
      if (this.Variables.Contains(x))
      {
        var newVars = new Set<Variable>(this.Variables);
        newVars.Remove(x);
        newVars.Add(xNew);

        return new MonomialOfInt<Variable>(this.K, newVars);
      }
      else
      {
        return this;
      }
    }

    #region Overridden
    //^ [Confined]
    public override string/*!*/ ToString()
    {
      return this.k + "*" + VarsToString(this.variables) + " ";
    }

    static private string VarsToString(List<Variable> vars)
    {
      StringBuilder s = new StringBuilder();
      if (vars.Count == 0)
        s.Append("1");
      else
      {
        foreach (var v in vars)
        {
          if (v != null)
            s.Append(v.ToString() + "* ");
          else
            s.Append("null");
        }
      }
      string/*!*/ stmp = s.ToString();

      if (stmp.Length > 2)         // Get rid of the last "* ", if any
        stmp = stmp.Remove(stmp.Length - 2);

      return stmp;
    }
    #endregion

    public bool IsEquivalentTo(MonomialOfInt<Variable> tmp)
    {
      if (this.K != tmp.K)
        return false;

      foreach (var x in this.Variables)
      {
        if (!tmp.Variables.Contains(x))
          return false;
      }

      foreach (var x in tmp.variables)
      {
        if (!tmp.Variables.Contains(x))
          return false;
      }

      return true;
    }
  }

  public static class PolynomialOfIntHelper
  {
    //private PolynomialHelper()
    //{
    //}

    public static bool TryMatch_k1XPlusk2YEqualk3<Variable, Expression>(this PolynomialOfInt<Variable, Expression> expAsPolCanonical, out int k1, out Variable x, out int k2, out Variable y, out int k3)
    {
      if (expAsPolCanonical.Degree == 1 && expAsPolCanonical.Relation.HasValue && expAsPolCanonical.Relation.Value == ExpressionOperator.Equal
        && expAsPolCanonical.Left.Count == 2 && expAsPolCanonical.Right.Count == 1)
      {
        var m1 = expAsPolCanonical.Left[0];
        var m2 = expAsPolCanonical.Left[1];

        var m3 = expAsPolCanonical.Right[0];

        if (m3.Variables.Count == 0)
        {
          k1 = m1.K;
          x = m1.Variables[0];

          k2 = m2.K;
          y = m2.Variables[0];

          k3 = m3.K;

          return true;
        }
      }

      k1 = k2 = k3 = default(int);
      x = y = default(Variable);

      return false;
    }

    public static bool TryMatch_XMinusYEqualk3<Variable, Expression>(this PolynomialOfInt<Variable, Expression> expAsPolCanonical, out Variable x, out Variable y, out int k)
    {
      int k1, k2, k3;
      if (TryMatch_k1XPlusk2YEqualk3(expAsPolCanonical, out k1, out x, out k2, out y, out k3) && k1 == 1 && k2 == -1)
      {
        k = k3;

        return true;
      }

      k = default(int);

      return false;
    }



    /// <summary>
    ///  Matches the polynomial <code>expAsPolCanonical</code> with the expression <code>y - k</code>
    /// </summary>
    /// <param name="expAsPolCanonical">A polynomial what is <b>assumed</b> to be already into a canonical form</param>
    /// <returns>true iff the matching works</returns>
    public static bool Match_YMinusK<Variable, Expression>(PolynomialOfInt<Variable, Expression> expAsPolCanonical, out Variable y, out int k)
    {
      Contract.Ensures(!Contract.Result<bool>() || (object)Contract.ValueAtReturn(out k) != null);

      bool result;

      if (expAsPolCanonical.Relation == null && expAsPolCanonical.Degree == 1)
      {
        var monomials = expAsPolCanonical.Left;
        if (monomials.Count == 2 && monomials[0].IsLinear && monomials[1].IsConstant)
        {
          k = monomials[1].K;

          if (k < 0)
          {
            y = monomials[0].Variables[0];
            result = true;
          }
          else
          {
            result = HelperForFalse(out y, out k);
          }
        }
        else
        {
          result = HelperForFalse(out y, out k);
        }
      }
      else
      {
        result = HelperForFalse(out y, out k);
      }

      return result;
    }

    /// <summary>
    ///  Matches the polynomial <code>expAsPolCanonical</code> with the expression <code>y + k</code>
    /// </summary>
    /// <param name="expAsPolCanonical">A polynomial what is <b>assumed</b> to be already into a canonical form</param>
    /// <returns>true iff the matching works</returns>
    public static bool Match_YPlusK<Variable, Expression>(PolynomialOfInt<Variable, Expression> expAsPolCanonical, out Variable y, out int k)
    {
      Contract.Ensures(Contract.Result<bool>() == false || (object)Contract.ValueAtReturn(out k) != null);

      bool result;

      if (expAsPolCanonical.Relation == null && expAsPolCanonical.Degree == 1)
      {
        var monomials = expAsPolCanonical.Left;
        if (monomials.Count == 2 && monomials[0].IsLinear && monomials[1].IsConstant)
        {
          k = monomials[1].K;

          if (k > 0)
          {
            y = monomials[0].Variables[0];
            result = true;
          }
          else
          {
            result = HelperForFalse(out y, out k);
          }
        }
        else
        {
          result = HelperForFalse(out y, out k);
        }
      }
      else
      {
        result = HelperForFalse(out y, out k);
      }

      return result;
    }

    /// <summary>
    ///  Matches the polynomial <code>pol</code> with the expression <code>y</code>
    /// </summary>
    /// <param name="pol">A polynomial what is <b>assumed</b> to be already into a canonical form</param>
    /// <returns>true iff the matching works</returns>
    public static bool Match_Y<Variable, Expression>(PolynomialOfInt<Variable, Expression> pol, out Variable y)
    {
      if (!(pol.Degree == 1 && pol.Relation == null && pol.Left.Count == 1))
      {
        return HelperForFalse(out y);
      }
      else
      {
        y = pol.Variables.PickAnElement();
        return true;
      }
    }

    /// <summary>
    /// Matches the polynomial with the expression <code>k1 * x1 + k2 * x2 \leq k</code>
    /// </summary>
    /// <returns>true iff it is the case</returns>
    public static bool Match_k1XPlusk2YLessEqualThanK<Variable, Expression>(PolynomialOfInt<Variable, Expression> e1LTe2, out int k1, out Variable x1, out int k2, out Variable x2, out int k)
    {
      // ????
      // Contract.Requires(e1LTe2.Relation.HasValue);

      if (e1LTe2.Left.Count == 2 && e1LTe2.IsOctagonForm && e1LTe2.Relation.Value.IsLessEqualThan())
      {
        k1 = e1LTe2.Left[0].K;
        x1 = e1LTe2.Left[0].Variables[0];

        k2 = e1LTe2.Left[1].K;
        x2 = e1LTe2.Left[1].Variables[0];

        k = e1LTe2.Right[0].K;

        return true;
      }

      k1 = k2 = k = default(int);
      x1 = x2 = default(Variable);
      return false;
    }

    /// <summary>
    /// Matches the polynomial with the expression <code>k1 * x1 + k2 * x2 \lt k</code>
    /// </summary>
    /// <returns>true iff it is the case</returns>
    public static bool Match_k1XPlusk2YLessThanK<Variable, Expression>(PolynomialOfInt<Variable, Expression> e1LTe2, out int k1, out Variable x1, out int k2, out Variable x2, out int k)
    {
      if (e1LTe2.Left.Count == 2 && e1LTe2.IsOctagonForm)
      {
        k1 = e1LTe2.Left[0].K;
        x1 = e1LTe2.Left[0].Variables[0];

        k2 = e1LTe2.Left[1].K;
        x2 = e1LTe2.Left[1].Variables[0];
        if (e1LTe2.Relation.Value.IsLessThan())
        {
          k = e1LTe2.Right[0].K;

          return true;
        }
        else if (e1LTe2.Relation.Value.IsLessEqualThan()) // == ExpressionOperator.LessEqualThan)
        {
          k = e1LTe2.Right[0].K + 1;

          return true;
        }
      }

      k1 = k2 = k = default(int);
      x1 = x2 = default(Variable);
      return false;
    }



    /// <summary>
    /// Matches the polynomial with the expression <code>x1 + x2 \lt x3 + k</code>
    /// </summary>
    /// <returns>true iff it is the case</returns>
    public static bool Match_XPlusYLess_Equal_ThanZPlusK<Variable, Expression>(PolynomialOfInt<Variable, Expression> e1LTe2, out Variable x1, out Variable x2, out Variable x3, out int k)
    {
      x1 = x2 = x3 = default(Variable);
      k = default(int);

      var x = new Variable[3];
      if (e1LTe2.Left.Count == 3)
      {
        bool found = false;
        for (int i = 0; i < 3; i++)
        {
          if (!e1LTe2.Left[i].IsLinear)
            return false;
          switch ((int)e1LTe2.Left[i].K)
          {
            case 1:
              x[i - (found ? 1 : 0)] = e1LTe2.Left[i].Variables[0];
              break;
            case -1:
              if (found)
                return false;
              found = true;
              x[2] = e1LTe2.Left[i].Variables[0];
              break;
            default:
              return false;
          }
        }
        if (!found)
          return false;
        if (e1LTe2.Right.Count == 1 && e1LTe2.Right[0].IsConstant)
        {
          k = e1LTe2.Right[0].K;
          x1 = x[0];
          x2 = x[1];
          x3 = x[2];
          return true;
        }
      }
      return false;
    }



    /// <summary>
    /// Matches the polynomial with the expression <code>x1 \lt x2 + x3 + k</code>
    /// </summary>
    /// <returns>true iff it is the case</returns>
    public static bool Match_XLess_Equal_ThanYPlusZPlusK<Variable, Expression>(PolynomialOfInt<Variable, Expression> e1LTe2, out Variable x1, out Variable x2, out Variable x3, out int k)
    {
      x1 = x2 = x3 = default(Variable);
      k = default(int);
      var x = new Variable[3];

      if (e1LTe2.Left.Count == 3)
      {
        bool found = false;
        for (int i = 0; i < 3; i++)
        {
          if (!e1LTe2.Left[i].IsLinear)
            return false;
          switch ((int)e1LTe2.Left[i].K)
          {
            case -1:
              x[i - (found ? 1 : 0)] = e1LTe2.Left[i].Variables[0];
              break;
            case 1:
              if (found)
                return false;
              found = true;
              x[2] = e1LTe2.Left[i].Variables[0];
              break;
            default:
              return false;
          }
        }
        if (!found)
          return false;
        if (e1LTe2.Right.Count == 1 && e1LTe2.Right[0].IsConstant)
        {
          k = e1LTe2.Right[0].K;
          x1 = x[2];
          x2 = x[0];
          x3 = x[1];
          return true;
        }
      }
      return false;
    }



    /// <summary>
    /// Matches the polynomial <code>p</code> with <code>x - y + k</code>, where <code>k</code> can be positive, negative or zero
    /// </summary>
    /// <returns></returns>
    public static bool Match_XMinusYPlusK<Variable, Expression>(PolynomialOfInt<Variable, Expression> pol, out Variable x, out Variable y, out int k)
    {
      bool result;
      if (!pol.Relation.HasValue && pol.IsLinear)
      {
        if (pol.Left.Count == 3)
        {
          if (pol.Left[0].K == 1 && pol.Left[1].K == -1 && pol.Left[2].Variables.Count == 0)
          {
            x = pol.Left[0].Variables[0];
            y = pol.Left[1].Variables[0];
            k = pol.Left[2].K;

            result = true;
          }
          else if (pol.Left[0].K == -1 && pol.Left[1].K == 1 && pol.Left[2].Variables.Count == 0)
          {
            y = pol.Left[0].Variables[0];
            x = pol.Left[1].Variables[0];
            k = pol.Left[2].K;

            result = true;
          }
          else
          {
            result = HelperForFalse(out x, out y, out k);
          }
        }
        else if (pol.Left.Count == 2 && pol.Left[0].Variables.Count == 1 && pol.Left[1].Variables.Count == 1)
        {
          if (pol.Left[0].K == 1 && pol.Left[1].K == -1)
          {
            x = pol.Left[0].Variables[0];
            y = pol.Left[1].Variables[0];
            k = 0;
            result = true;
          }
          else if (pol.Left[0].K == -1 && pol.Left[1].K == 1)
          {
            y = pol.Left[0].Variables[0];
            x = pol.Left[1].Variables[0];
            k = 0;
            result = true;
          }
          else
          {
            result = HelperForFalse(out x, out y, out k);
          }
          // to be refined....
        }
        else
        {
          result = HelperForFalse(out x, out y, out k);
        }
      }
      else
      {
        result = HelperForFalse(out x, out y, out k);
      }

      return result;
    }

    /// <summary>
    /// Matches the polynomial with 
    /// </summary>
    public static bool Match_k1XLessThank2<Variable, Expression>(PolynomialOfInt<Variable, Expression> p, out int k1, out Variable x, out int k2)
    {
      if (p.Relation.Value.IsLessThan())  // (p.Relation == ExpressionOperator.LessThan)
      {
        return HelperForMatch_k1Xopk2(p, out k1, out x, out k2);
      }
      else
      {
        return HelperForFalse(out x, out k1, out k2);
      }
    }

    public static bool Match_k1XLessEqualThank2<Variable, Expression>(PolynomialOfInt<Variable, Expression> p, out int k1, out Variable x, out int k2)
    {
      if (p.Relation.Value.IsLessEqualThan())
      {
        return  HelperForMatch_k1Xopk2(p, out k1, out x, out k2);
      }
      else
      {
        return HelperForFalse(out x, out k1, out k2);
      }
    }

    public static bool Match_kY<Variable, Expression>(PolynomialOfInt<Variable, Expression> p, out Variable y, out int k)
    {
      Contract.Ensures(!Contract.Result<bool>() || (object)Contract.ValueAtReturn(out k) != null);

      if (p.Relation == null && p.Left.Count == 1 && p.Left[0].Degree == 1)
      {
        y = p.Left[0].Variables[0];
        k = p.Left[0].K;
        return true;
      }
      else
      {
        return HelperForFalse(out y, out k);
      }
    }

    private static bool HelperForMatch_k1Xopk2<Variable, Expression>(PolynomialOfInt<Variable, Expression> p, out int k1, out Variable x, out int k2)
    {
      bool result;
      if (p.IsLinear && p.Degree != 0 && p.Left.Count == 1 && p.Right.Count == 1)
      {
        k1 = p.Left[0].K;
        x = p.Left[0].Variables[0];
        k2 = p.Right[0].K;

        Contract.Assert(p.Right[0].Variables.Count == 0);  // no var

        result = true;
      }
      else
      {
        result = HelperForFalse(out x, out k1, out k2);
      }

      return result;
    }

    private static bool HelperForFalse<Variable>(out Variable x, out int k1, out int k2)
    {
      Contract.Ensures(Contract.Result<bool>() == false);

      x = default(Variable);
      k1 = default(int);
      k2 = default(int);
      return false;
    }

    private static bool HelperForFalse<Variable>(out Variable x, out Variable y, out int k)
    {
      Contract.Ensures(Contract.Result<bool>() == false);

      x = y =default(Variable);
      k = default(int);

      return false;
    }

    private static bool HelperForFalse<Variable>(out Variable y, out int k)
    {
      Contract.Ensures(Contract.Result<bool>() == false);

      y = default(Variable);
      k = default(int);

      return false;
    }

    private static bool HelperForFalse<Variable>(out Variable y)
    {
      Contract.Ensures(Contract.Result<bool>() == false);

      y = default(Variable);

      return false;
    }
  }

}
