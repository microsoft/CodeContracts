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

#define UseSymbolicValuesForAdditions

// Few classes for helping manipulating Polynomials and Monomials

using System;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains.Expressions
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Diagnostics.CodeAnalysis;

  /// <summary>
  /// A Polynomial is a triple <code>(op, left, right)</code> where <code>op</code> is the binary operator (e.g. <code>==</code>), <code>left</code> the list of monomes on the left and
  /// <code>right</code> the list of constant monomes on the right.
  /// The list stands for the "addition"
  /// </summary>
  [ContractVerification(false)]
  public struct Polynomial<Variable, Expression>
  {
    #region Static fields

    // TODO: figure out the statistics
#if false
    static readonly private FIFOCache<Expression, Polynomial<Variable, Expression>> cache_PolynomialForm = new FIFOCache<Expression, Polynomial<Variable, Expression>>();
    static readonly private FIFOCache<Polynomial<Variable, Expression>, Polynomial<Variable, Expression>> cache_CanonicalForms = new FIFOCache<Polynomial<Variable, Expression>, Polynomial<Variable, Expression>>();
#endif
    #endregion

    #region Private fields
    private ExpressionOperator? relation;

    private Monomial<Variable>[] left;
    private Monomial<Variable>[] right;

    // Cached values
    private bool? Cached_IsLinear;
    private int? Cached_Degree;
    private bool? Cached_IsTautology;
    private Set<Variable> Cached_Vars;

    #endregion

    #region Object invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.left != null);
      Contract.Invariant(this.relation.HasValue || right == null);
      Contract.Invariant(!this.relation.HasValue || this.relation.Value.IsBinary());
    }

    #endregion

    #region Getters
    /// <summary>
    /// The relation of the polynomial, or null if it is a spourious polynomial
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
    /// The variables defined in this polynomial
    /// </summary>
    public IReadonlySet<Variable> Variables
    {
      get
      {
        if (Cached_Vars == null)
        {
          var result = new Set<Variable>();

          foreach (var m in this.Left)
          {
            result.AddRange(m.Variables);
          }

          if (this.Relation != null)
          {
            foreach (var m in this.Right)
            {
              result.AddRange(m.Variables);
            }
          }

          Cached_Vars = result;
        }

        return Cached_Vars;
      }
    }

    /// <summary>
    /// The list of monomials on the Left of the relational operator
    /// </summary>
    public Monomial<Variable>[] Left
    {
      get
      {
        Contract.Ensures(Contract.Result<Monomial<Variable>[]>() != null);

        return this.left;
      }
    }

    /// <summary>
    /// The list of monomials on the Right of the relational operator.
    /// Precondition: <code>this.Relation != null</code>
    /// </summary>
    public Monomial<Variable>[] Right
    {
      [SuppressMessage("Microsoft.Contracts", "Ensures-20-41", Justification = "Algorithmical knowledge")]
      get
      {
        Contract.Ensures(Contract.Result<Monomial<Variable>[]>() == null || Contract.Result<Monomial<Variable>[]>().Length >= 0);

        return this.right;
      }
    }

    /// <summary>
    /// The Left part of this polynomial as an instance of <code>Polynomial</code>
    /// </summary>
    public Polynomial<Variable, Expression> LeftAsPolynomial
    {
      get
      {
        Polynomial<Variable, Expression> result;
        TryToPolynomialForm(this.left, out result); // We know that it never fails...

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

        if (this.Left.Length == 1 && this.Right.Length == 1)
        {
          if (this.Left[0].IsConstant && this.Right[0].IsConstant)
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


    /// <summary>
    /// Is a simple inconsistence?
    /// </summary>
    public bool IsInconsistent
    {
      get
      {
        if (this.Relation == null)
          return false;

        if (this.Left.Length == 1 && this.Right.Length == 1)
        {
          if (this.Left[0].IsConstant && this.Right[0].IsConstant)
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
    /// Is it in the form of <code>a*x + b *y \leq c</code>, with a, b \in  {+1, -1, 0}, and c \in Rational 
    /// </summary>
    public bool IsOctagonForm
    {
      get
      {
        Contract.Ensures(!Contract.Result<bool>() || this.Degree <= 1);
        Contract.Ensures(!Contract.Result<bool>() || this.Right != null);

        if (this.Relation == null)
        {
          return false;
        }
        if (this.Left.Length == 0)
        {
          return false;
        }
        if (!this.IsLinear)
        {
          return false;
        }
        if (!(this.Left.Length <= 2 && this.Right.Length == 1))   // Something in the form of m1 + m2 <= m3
        {
          return false;
        }
        var m0K = this.Left[0].K;

        if (!(m0K == -1 || m0K == 1 || m0K.IsZero))     // m1.K must be -1, 0 or +1
        {
          return false;
        }
        if (this.Left.Length == 2)
        {
          var m1K = this.Left[1].K;
          if (!(m1K == -1 || m1K == 1 || m1K.IsZero)) // m2.K must be -1, 0 or +1
          {
            return false;
          }
        }
        if (!this.Right[0].IsConstant)                          // m3 must be a constant
        {
          return false;
        }

        Contract.Assert(this.Right != null);

        return true;                                            // If we reached this point, then it is an octagon
      }
    }

    public bool IsLinear
    {
      get
      {
        if (Cached_IsLinear.HasValue)
        {
          return Cached_IsLinear.Value;
        }

        foreach (var m in this.Left)
        { // all the monomial on the left must be linear
          if (!m.IsLinear)
          {
            Cached_IsLinear = false;
            return false;
          }
        }
        if (this.Relation != null)
        {
          foreach (var m in this.Right)
          { // all the monomial on the right must be linear
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
          if (this.Left.Length > 2)
          {
            return false;
          }
          else if (this.Left.Length == 1)
          {   // is the case that we have <k, 1> or <+/- 1, x> ?
            return (this.Left[0].IsConstant || this.Left[0].K == 1 || this.Left[0].K == -1);
          }
          else
          {
            Contract.Assume(this.Left.Length == 2, "Error cannot have an empty left on a Polynomial");

            return (this.Left[0].IsLinear && (this.Left[0].K == 1 || this.Left[0].K == -1) && this.Left[1].IsConstant);
          }
        }
        else
        {
          return false;
        }
      }
    }

    private bool IsIntConstant(out long kLeft)
    {
      kLeft = default(long);

      if (this.Relation.HasValue)
        return false;

      return this.Left.Length == 1 && this.Left[0].IsIntConstant(out kLeft);
    }

    public int Degree
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        if (this.Cached_Degree.HasValue)
        {
          Contract.Assume(this.Cached_Degree.Value >= 0);
          return this.Cached_Degree.Value;
        }

        int max = 0;
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
        if (!(this.Left.Length == 1 && this.Right.Length == 1))   // Must be "m1 <= m2"
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

    private bool IsMonomial(out Monomial<Variable> m)
    {
      if (!this.relation.HasValue && this.left.Length == 1)
      {
        m = this.left[0];
        return true;
      }
      else
      {
        m = default(Monomial<Variable>);
        return false;
      }
    }

    #endregion

    #region Constructors

    [ContractVerification(false)]
    private Polynomial(Monomial<Variable> monome)
    {
      this.relation = null;

      var this_left = new Monomial<Variable>[] { monome };

      this.left = this_left;

      this.right = null;

      Cached_IsLinear = Cached_IsTautology = null;
      Cached_Degree = null;
      Cached_Vars = null;
    }

    /// <summary>
    /// If <code>monomes == (k1, xs1) ... (kn, xsn)</code> then it constructs the polynome <code>k1*xs1 +  ... + kn* xsn</code>
    /// </summary>
    [ContractVerification(false)]
    private Polynomial(Monomial<Variable>[] monomes)
    {
      Contract.Requires(monomes != null);

      this.relation = null;
      this.left = monomes;
      this.right = null;

      Cached_IsLinear = Cached_IsTautology = null;
      Cached_Degree = null;
      Cached_Vars = null;
    }

    /// <summary>
    /// If <code>left == (k1, xs1) ... (kn, xsn)</code> and <code>right == (c1, ys1) ... (cm, ysm)</code>, then it constructs the polynome standing for 
    /// <code> k1*xs1 +  ... + kn* xsn op c1*cs1 + ...  + cn*csn</code>
    /// </summary>
    /// <param name="op">Must be a binary operator</param>
    [ContractVerification(false)]
    private Polynomial(ExpressionOperator op, Monomial<Variable>[] left, Monomial<Variable>[] right)
    {
      Contract.Requires(op.IsBinary());
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      Contract.Ensures(Contract.ValueAtReturn(out this.left) == left);
      Contract.Ensures(Contract.ValueAtReturn(out this.right) == right);

      this.relation = op;
      this.left = left;
      this.right = right;
      
      Cached_IsLinear = Cached_IsTautology = null;
      Cached_Degree = null;
      Cached_Vars = null; 
    }

    /// <summary>
    /// If <code>left == (k1, xs1) ... (kn, xsn)</code> and <code>right == (c1, ys1)</code>, then it constructs the polynome standing for 
    /// <code> k1*xs1 +  ... + kn* xsn op c1*ys1</code>
    /// </summary>
    /// <param name="op">Must be a binary operator</param>
    [ContractVerification(false)]
    private Polynomial(ExpressionOperator op, Monomial<Variable>[] left, Monomial<Variable> right)
    {
      Contract.Requires(op.IsBinary());
      Contract.Requires(left != null);
      Contract.Requires(!object.ReferenceEquals(right, null));

      this.relation = op;
      this.left = left;
      this.right = new Monomial<Variable>[] { right };

      Cached_IsLinear = Cached_IsTautology = null;
      Cached_Degree = null;
      Cached_Vars = null;
    }

    /// <summary>
    /// If <code>left == (k1, xs1) ... (kn, xsn)</code> and <code>right == (c1, ys1) ... (cm, ysm)</code>, then it constructs the polynome standing for 
    /// <code> k1*xs1 +  ... + kn* xsn op c1*cs1 + ...  + cn*csn</code>
    /// </summary>
    /// <param name="op">Must be a binary operator</param>
    /// <param name="left">shoukd be such that left.Relation == null</param>
    /// <param name="right">shoukd be such that right.Relation == null</param>
    [ContractVerification(false)]
    private Polynomial(ExpressionOperator op, Polynomial<Variable, Expression> left, Polynomial<Variable, Expression> right)
    {
      Contract.Requires(op.IsBinary());
      Contract.Requires(!left.Relation.HasValue);
      Contract.Requires(!right.Relation.HasValue);

      Contract.Assume(left.left != null);
      Contract.Assume(right.left != null);

      this.relation = op;
      this.left = left.left;
      this.right = right.left;

      Cached_IsLinear = Cached_IsTautology = null;
      Cached_Degree = null;
      Cached_Vars = null;
    }

    [ContractVerification(false)]
    private Polynomial(Polynomial<Variable, Expression> other)
    {
      Contract.Requires(!object.ReferenceEquals(other, null));

      Contract.Assume(other.left != null);

      this.relation = other.relation;
      this.left = DuplicateFrom(other.left);
      this.right = other.right != null ? DuplicateFrom(other.right) : null;

      Cached_IsLinear = Cached_IsTautology = null;
      Cached_Degree = null;
      Cached_Vars = null;
    }

    #endregion


    #region Manipulation of the polynomial: Creates a Polynomial from a PureExpression and puts it into a canonical form

    /// <summary>
    /// Convert the pure expression <code>exp</code> into a polynomial, that is somehow simpler to handle.
    /// Essentially, it gets rid of all the parentheses, it performs the(simple) multiplications, additions, etc.
    /// </summary>
    /// <param name="exp">The expression that will be decoded</param>
    /// <param name="decoder">The decoder for the expression</param>
    /// <param name="result">If the result is true, then it returns a polynomial into a canonical form</param>
    static public bool TryToPolynomialForm(Expression exp, IExpressionDecoder<Variable, Expression> decoder,
      out Polynomial<Variable, Expression> result)
    {
      Contract.Requires(decoder != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result).Left != null);

      if (TryToPolynomialFormInternal(exp, decoder, out result))
      {
        try
        {
          if (result.TryToCanonicalForm(out result))
          {
            return true;
          }
        }
        catch (ArithmeticExceptionRational)
        {
        }
      }

      return false;
    }

    private static bool TryToPolynomialFormInternal(Expression exp,
      IExpressionDecoder<Variable, Expression> decoder, out Polynomial<Variable, Expression> result)
    {
      Contract.Requires(decoder != null);

      return new TryToPolynomialVisitor(decoder).Visit(exp, out result);
    }


    /// <summary>
    /// Convert the pure expression <code>left op right</code> into a polynomial, that is somehow simpler to handle.
    /// Essentially, it gets rid of all the parenthesis, it performs the(simple) multiplications, additions, etc.
    /// </summary>
    /// <param name="result">if the result is true, it is a polynomial in canonical form</param>
    static public bool TryToPolynomialForm(ExpressionOperator op, Expression left, Expression right,
      IExpressionDecoder<Variable, Expression> decoder, out Polynomial<Variable, Expression> result)
    {
      Contract.Requires(op.IsBinary());
      Contract.Requires(decoder != null);

      if (!decoder.OperatorFor(left).IsRelationalOperator()
        && !decoder.OperatorFor(right).IsRelationalOperator())
      {
        Polynomial<Variable, Expression> leftAsPol, rightAsPol;

        if (Polynomial<Variable, Expression>.TryToPolynomialForm(left, decoder, out leftAsPol)  // We can convert to polynomial
          && Polynomial<Variable, Expression>.TryToPolynomialForm(right, decoder, out rightAsPol)
          && !leftAsPol.Relation.HasValue && !rightAsPol.Relation.HasValue                      // the polynomial are polynomial forms indeed
          )
        {
          try
          {
            result = new Polynomial<Variable, Expression>(op, leftAsPol, rightAsPol);

            if (result.TryToCanonicalForm(out result))
            {
              return true;
            }
          }
          catch (ArithmeticExceptionRational)
          {
            // We swallow it, and contine by failing
          }
        }
      }

      result = default(Polynomial<Variable, Expression>);
      return false;
    }

    static public bool TryToPolynomialForm(ExpressionOperator op, Variable left, Expression right,
      IExpressionDecoder<Variable, Expression> decoder, out Polynomial<Variable, Expression> result)
    {
      Contract.Requires(op.IsBinary());
      Contract.Requires(decoder != null);

      if (decoder.OperatorFor(right).IsRelationalOperator())
      {
        result = default(Polynomial<Variable, Expression>);
        return false;
      }
      else
      {
        Polynomial<Variable, Expression> rightAsPol;

        if (Polynomial<Variable, Expression>.TryToPolynomialForm(right, decoder, out rightAsPol) && !rightAsPol.Relation.HasValue)
        {
          try
          {

            var leftAsPol = new Polynomial<Variable, Expression>(new Monomial<Variable>[] { new Monomial<Variable>(left) });

            var tmp = new Polynomial<Variable, Expression>(op, leftAsPol, rightAsPol);
            if (tmp.TryToCanonicalForm(out result))
            {
              return true;
            }
            else
            {
              return false;
            }
          }
          catch (ArithmeticExceptionRational)
          {
            result = default(Polynomial<Variable, Expression>);
            return false;
          }

        }
        else
        {
          result = default(Polynomial<Variable, Expression>);

          return false;
        }
      }
    }


    /// <param name="result">If the result is true, it is a polynomial in canonical form</param>
    static public bool TryToPolynomialForm(
      ExpressionOperator op,
      Polynomial<Variable, Expression> left,
      Polynomial<Variable, Expression> right,
      out Polynomial<Variable, Expression> result)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result).left != null);
      Contract.Ensures(!Contract.Result<bool>() || op != ExpressionOperator.LessEqualThan || Contract.ValueAtReturn(out result).Relation == ExpressionOperator.LessEqualThan);

      if (!op.IsBinary() || left.Relation != null || right.Relation != null)
      {
        result = default(Polynomial<Variable, Expression>);
        return false;
      }
      else
      {
        try
        {
          Contract.Assert(right.Relation == null);

          // here we return a new Polynomial. An alternative is to return a polynomial in canonical form
          result = new Polynomial<Variable, Expression>(op, left, right);
          if (result.TryToCanonicalForm(out result))
          {
            return true;
          }
        }
        catch (ArithmeticExceptionRational)
        {
          // If there is an exception in the rationals, then we simply fail
        }
      }
      result = default(Polynomial<Variable, Expression>);
      return false;
    }

    /// <param name="result">The result: a polynomial in canonical form</param>
    /// <returns>Always true</returns>
    static public bool TryToPolynomialForm(
      ExpressionOperator expressionOperator, Monomial<Variable>[] left, Monomial<Variable>[] right,
      out Polynomial<Variable, Expression> result)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Requires(expressionOperator.IsBinary());

      Contract.Ensures(!object.ReferenceEquals(Contract.ValueAtReturn(out result), null));

      result = new Polynomial<Variable, Expression>(expressionOperator, left, right);
      return result.TryToCanonicalForm(out result);
    }

    static public bool TryToPolynomialForm(
      bool canonicalByConstruction,
      ExpressionOperator expressionOperator, Monomial<Variable>[] left, Monomial<Variable>[] right,
      out Polynomial<Variable, Expression> result)
    {
      Contract.Requires(expressionOperator.IsBinary());
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      Contract.Ensures(Contract.Result<bool>() == true);
      Contract.Ensures(!object.ReferenceEquals(Contract.ValueAtReturn(out result), null));

      if (canonicalByConstruction)
      {
        result = new Polynomial<Variable, Expression>(expressionOperator, left, right);

        return true;
      }
      else
      {
        return TryToPolynomialForm(expressionOperator, left, right, out result);
      }
    }


    /// <summary>
    /// Convert the list of monomials <code>monomials</code> into a Polynomial in canonical form
    /// </summary>
    /// <param name="pol">The polynomial in canonical form</param>
    /// <returns>Always true</returns>
    static public bool TryToPolynomialForm(Monomial<Variable>[] monomials, out Polynomial<Variable, Expression> pol)
    {
      Contract.Requires(monomials != null);

      if (monomials.Length > 1)
      {
        pol = new Polynomial<Variable, Expression>(monomials);

        return pol.TryToCanonicalForm(out pol);
      }
      else
      {
        pol = new Polynomial<Variable, Expression>(monomials);

        return true;
      }
    }

    static public bool TryToPolynomialForm(bool canonicalByConstruction, Monomial<Variable>[] monomials, out Polynomial<Variable, Expression> pol)
    {
      Contract.Requires(monomials != null);

      if (canonicalByConstruction)
      {
        pol = new Polynomial<Variable, Expression>(monomials);
        return true;
      }
      else
      {
        return TryToPolynomialForm(monomials, out pol);
      }
    }

    #endregion

    #region Manipulation of the polynomial : Add a monome
    public Polynomial<Variable, Expression> AddMonomialToTheLeft(Monomial<Variable> m)
    {

      var tmpLeft = new Monomial<Variable>[this.left.Length + 1];

      Array.Copy(this.left, tmpLeft, this.left.Length);
      tmpLeft[this.left.Length] = m;

      var tmpright = this.right != null ? DuplicateFrom(this.right) : null;

      Polynomial<Variable, Expression> tmp, result;
      if (this.relation.HasValue)
      {
        tmp = new Polynomial<Variable, Expression>(this.relation.Value, tmpLeft, tmpright);
      }
      else
      {
        tmp = new Polynomial<Variable, Expression>(tmpLeft);
      }

      tmp.TryToCanonicalForm(out result);

      return result;
    }

    public Polynomial<Variable, Expression> AddMonomialToTheLeft(Monomial<Variable>[] m)
    {
      Contract.Requires(m != null);

      Contract.Ensures(!Object.ReferenceEquals(Contract.Result<Polynomial<Variable, Expression>>(), null));

      var tmpleft = new Monomial<Variable>[this.left.Length + m.Length];

      Array.Copy(this.left, tmpleft, this.left.Length);
      Array.Copy(m, 0, tmpleft, this.left.Length, m.Length);

      var tmpright = this.right != null ? DuplicateFrom(this.right) : null;

      var tmp = new Polynomial<Variable, Expression>(this.relation.Value, tmpleft, tmpright);

      Polynomial<Variable, Expression> result;

      tmp.TryToCanonicalForm(out result);

      return result;
    }

    #endregion

    #region ToCanonicalForm, ToPureExpression, Renaming
    /// <summary>
    /// Put this Polynomial into a canonical form, meaning that all the monomial involving variables are on the left, all the constants on the right, and all the easy arithmetic operations are performed
    /// </summary>
    /// <returns>The canonical form of this polynomial</returns>
    private bool TryToCanonicalForm(out Polynomial<Variable, Expression> result)
    {
      // Two main cases: this polynomial is binded by a relation, or it is just a sequence of monomials
      if (this.Relation.HasValue)
      {
        Contract.Assume(this.right != null);
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

        Polynomial<Variable, Expression> moved;
        if (this.TryMoveConstantsAndMonomes(out moved))
        {
          Contract.Assume(this.relation.HasValue);

          Monomial<Variable>[] simplifiedLeft, simplifiedRight;
          if (TrySimplifyMonomes(moved.left, out simplifiedLeft) && TrySimplifyMonomes(moved.right, out simplifiedRight))
          {
            Contract.Assume(simplifiedRight.Length == 1, "At this point we expected just a constant on the right side...");

            if (TryReduceCoefficents(simplifiedLeft, simplifiedRight))
            {
              Contract.Assert(this.relation.Value.IsBinary());

              result = new Polynomial<Variable, Expression>(this.relation.Value, simplifiedLeft, simplifiedRight);
              return true;
            }
          }
        }
        else
        {
          // Do nothing, we will return false;
        }
      }
      else
      {
        Monomial<Variable>[] simplified;
        if (TrySimplifyMonomes(this.left, out simplified))
        {
          result = new Polynomial<Variable, Expression>(simplified);
          return true;
        }
      }

      result = default(Polynomial<Variable, Expression>);
      return false;
    }

    /// <summary>
    /// Swap in place
    /// Works with side effects!!!
    /// </summary>
    private void SwapOperands(ExpressionOperator newOperator)
    {
      Contract.Requires(this.right != null);

      Contract.Ensures(this.relation.HasValue);
      Contract.Ensures(this.left == Contract.OldValue(this.right));
      Contract.Ensures(this.right == Contract.OldValue(this.left));

      this.relation = newOperator;

      var tmp = this.left;
      this.left = this.right;
      this.right = tmp;
    }

    /// <summary>
    /// Convert this polynomial into a pure expression
    /// </summary>
    public Expression ToPureExpression(IExpressionEncoder<Variable, Expression> encoder)
    {
      Contract.Requires(encoder != null);

      Contract.Ensures(Contract.Result<Expression>() != null);

      return ToPureExpressionForm(this, encoder);
    }

    /// <summary>
    /// Convert a polynomial into a pure expression
    /// </summary>
    /// <param name="encoder">The encoder for constructing an expression</param>
    static private Expression ToPureExpressionForm(Polynomial<Variable, Expression> p, IExpressionEncoder<Variable, Expression> encoder)
    {
      Contract.Requires(!object.ReferenceEquals(p, null));
      Contract.Requires(encoder != null);

      Contract.Ensures(Contract.Result<Expression>() != null);

      var eLeft = ConvertToPureExpression(p.left, encoder);
      if (p.Relation.HasValue)
      {
        var eRight = ConvertToPureExpression(p.right, encoder);
        return encoder.CompoundExpressionFor(ExpressionType.Bool, p.Relation.Value, eLeft, eRight);
      }
      else
      {
        return eLeft;
      }
    }


    /// <returns>true if the polynomial is linear, and in such a case a list representation</returns>
    public bool TryToList(out List<Pair<Rational, Variable>> val)
    {
      if (this.Relation.HasValue)
      {
        long k;
        if (this.Relation.Value == ExpressionOperator.Equal && this.right[0].IsIntConstant(out k) && k == 0)
        {
          // ok
        }
        else
        {
          val = default(List<Pair<Rational, Variable>>);
          return false;
        }
      }

      val = new List<Pair<Rational, Variable>>();

      foreach (var m in this.left)
      {
        if (m.IsLinear)
        {
          val.Add(new Pair<Rational, Variable>(m.K, m.VariableAt(0)));
        }
        else
        {
          val = default(List<Pair<Rational, Variable>>);
          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// Renames all the occurrences of the variable <code>x</code> to <code>xNew</code>
    /// </summary>
    public Polynomial<Variable, Expression> Rename(Variable x, Variable xNew)
    {
      return Rename(true, x, xNew);
    }

    /// <summary>
    /// Renames all the occurrences of the variable <code>x</code> to <code>xNew</code>
    /// </summary>
    public Polynomial<Variable, Expression> Rename(bool canonical, Variable x, Variable xNew)
    {
      // Improve: Use sharing when the renamed polynomial is the same

      var leftResult = new List<Monomial<Variable>>();
      var rightResult = new List<Monomial<Variable>>();

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

      Polynomial<Variable, Expression> result = this.Relation.HasValue
        ? new Polynomial<Variable, Expression>(this.Relation.Value, leftResult.ToArray(), rightResult.ToArray())
        : new Polynomial<Variable, Expression>(leftResult.ToArray());

      if (canonical)
      {
        result.TryToCanonicalForm(out result);
      }

      return result;
    }

    #endregion

    #region Equivalence of polynomials

    public bool IsEquivalentTo(Polynomial<Variable, Expression> other)
    {
      Polynomial<Variable, Expression> left, right;
      if (!this.TryToCanonicalForm(out left) || !other.TryToCanonicalForm(out right))
      {
        return false;
      }

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
          if (!ContainsMonomialEquivalentTo(right.left, m))
            return false;
        }

        if (left.Relation.HasValue)
        {
          Contract.Assume(right.Relation.HasValue);

          foreach (var m in left.Right)
          {
            if (!ContainsMonomialEquivalentTo(right.right, m))
              return false;
          }
        }

        // right contains all the monomials in left, and right has the same cardinality of left, so they are equivalent
        return true;
      }
    }

    private static bool SimpleEquivalence(Polynomial<Variable, Expression> left, Polynomial<Variable, Expression> right)
    {
      if (left.Degree != right.Degree)
        return false;
      if (left.Left.Length != right.Left.Length)
        return false;
      if (left.Relation != right.Relation)
        return false;
      if ((!left.Relation.HasValue && !right.Relation.HasValue) && (left.Right.Length != right.Right.Length))
        return false;

      return true;
    }

    private static bool UniformCoefficents(Polynomial<Variable, Expression> left, Polynomial<Variable, Expression> right)
    {
      Contract.Requires(left.left != null);
      Contract.Requires(right.left != null);

      var listLeft = left.left;
      var listRight = right.left;

      if (listLeft.Length == 0)
      {
        if (listRight.Length == 0)
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
      Monomial<Variable>? mRight = null;

      foreach (var m in listRight)
      {
        if (mLeft.Degree != m.Degree)
          continue;

        foreach (var v in mLeft.Variables)
        {
          if (!m.ContainsVariable(v))
            goto nextMonomial;
        }

        mRight = m;   // We found a monomial with the same variables
        break;

      nextMonomial:
        ;
      }

      if (mRight.HasValue)
      {
        var kLeft = mLeft.K;
        var kRight = mRight.Value.K;

        if (kLeft != kRight)
        {
          if (kRight == 0)
            return true;

          var k = kLeft / kRight;

          for (int i = 0; i < right.Left.Length; i++)
          {
            right.Left[i].K = right.Left[i].K * k;
          }

          if (right.Relation.HasValue)
          {
            for (int i = 0; i < right.Right.Length; i++)
            {
              right.Right[i].K = right.Right[i].K * k;
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

    private static bool ContainsMonomialEquivalentTo(Monomial<Variable>[] l, Monomial<Variable> toFind)
    {
      foreach (var m in l)
      {
        if (m.K != toFind.K)
          continue;

        if (m.Degree != toFind.Degree)
          continue;

        foreach (var v in m.Variables)
        {
          if (!toFind.ContainsVariable(v))
            goto nextMonomial;
        }

        // At this point, they have the same coefficent, and the same variables, so they are the same
        return true;

      nextMonomial:
        ;
      }

      return false;
    }

    #endregion

    #region Contains Dummy variables?

    public bool ContainsDummyVariables(IExpressionDecoder<Variable, Expression> decoder)
    {
      Contract.Requires(decoder != null);

      if (this.left == null)
        return false;

      foreach (var monomial in this.Left)
      {
        foreach (var x in monomial.Variables)
        {
          if (!decoder.IsSlackOrFrameworkVariable(x))
          {
            return true;
          }
        }
      }

      if (this.right != null)
      {
        foreach (var monomial in this.Right)
        {
          foreach (var x in monomial.Variables)
          {
            if (!decoder.IsSlackOrFrameworkVariable(x))
            {
              return true;
            }
          }
        }
      }

      return false;
    }

    #endregion

    #region Private methods
    [ContractVerification(true)]
    static private Monomial<Variable>[] DuplicateFrom(Monomial<Variable>[] original)
    {
      Contract.Requires(original != null);

      Contract.Ensures(Contract.Result<Monomial<Variable>[]>() != null);
      Contract.Ensures(Contract.Result<Monomial<Variable>[]>().Length == original.Length);

      var result = new Monomial<Variable>[original.Length];

      Array.Copy(original, result, original.Length);

      return result;
    }


    /// <summary>
    /// Move the constants to the right of the relation operator, and the monomial to the left.
    /// It makes sense just if this.Relation is LessEqualThan, LessThan, Equal or NotEqual
    /// </summary>
    private bool TryMoveConstantsAndMonomes(out Polynomial<Variable, Expression> result)
    {
      Contract.Ensures(Contract.Result<bool>() == false || Contract.ValueAtReturn(out result).left != null);
      Contract.Ensures(Contract.Result<bool>() == false || Contract.ValueAtReturn(out result).right != null);

      Contract.Assume(this.relation.HasValue);   // It can be applied just to polynomials with a relation operator
      Contract.Assume(this.right != null);

      Contract.Assert(this.relation.Value.IsBinary());

      var newLeft = new List<Monomial<Variable>>();
      var newRight = new List<Monomial<Variable>>();

      // Move all the constants to the right, and leave the non-constants on the left
      foreach (var m in this.Left)
      {
        if (!m.IsConstant)    // k * xs, k != 0
        {
          newLeft.Add(m);
        }
        else                                // k * 1
        {
          if (m.K.IsMinValue) // overflow
          {
            result = default(Polynomial<Variable, Expression>);
            return false;
          }

          newRight.Add(-m);
        }
      }

      // Move all the non-constant monomials to the left, and leave the constants on the right
      foreach (var m in this.right)
      {
        if (!m.IsConstant)              // k * xs, k != 0
        {
          newLeft.Add(-m);
        }
        else
        {
          newRight.Add(m);
        }
      }

      // If there is nothing on the left or on the right, we add a zero
      if (newLeft.Count == 0)
      {
        newLeft.Add(new Monomial<Variable>(0));
      }
      if (newRight.Count == 0)
      {
        newRight.Add(new Monomial<Variable>(0));
      }

      Contract.Assert(this.relation.Value.IsBinary());     // F: We can prove this assert thanks to the one above
      result= new Polynomial<Variable, Expression>(this.relation.Value, newLeft.ToArray(), newRight.ToArray());
      return true;
    }

    /// <summary>
    /// Simplify the polinomial by performing basic arithmetic operations.
    /// If one of the monomes is non-linear, then return false
    /// </summary>
    static private bool TrySimplifyMonomes(Monomial<Variable>[] monomes, out Monomial<Variable>[] result)
    {
      Contract.Requires(monomes != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);

      // Shortcuts for common and simple cases
      if (monomes.Length <= 1)
      {
        result = monomes;
        return true;
      }

      // keep a cache between the variables of the monomials and the actual value. We use this data structure for performances
      var cache = new Dictionary<ListEqual<Variable>, Monomial<Variable>>();

      foreach (var m in monomes)
      {
        var vars = new ListEqual<Variable>(m.Degree, m.Variables);

        // We assume that m.Variables is ordered
        Monomial<Variable> oldMonomial;
        if (cache.TryGetValue(vars, out oldMonomial))
        {
          // if the string is in the dictionary, we update the value of the coefficent
          Rational newK;
          if (Rational.TryAdd(oldMonomial.K, m.K, out newK))
          {
            var newMonomial = new Monomial<Variable>(newK, oldMonomial.Variables);
            cache[vars] = newMonomial;  // Update the monomial
          }
          else // We give up
          {
            result = monomes;
            return false;
          }
        }
        else
        {   // it is a new monomial, so we add it to the cache
          cache.Add(vars, m);
        }
      }

      var positive = new List<Monomial<Variable>>(cache.Count);
      var negative = new List<Monomial<Variable>>(cache.Count);

      Monomial<Variable>? constant = null;

      foreach (var pair in cache)
      {
        var m = pair.Value;

        if (m.K.IsNotZero)
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

      var resultList = new List<Monomial<Variable>>(cache.Count);

      resultList.InsertRange(0, negative);
      resultList.InsertRange(0, positive);

      if (constant.HasValue)
      {
        resultList.Insert(resultList.Count, constant.Value);
      }

      // If everything turned out to be zero, we want to keep just one
      if (resultList.Count == 0)
      {
        resultList.Add(new Monomial<Variable>(0));
      }

      result = resultList.ToArray();

      return true;
    }

    [ContractVerification(true)]
    public class ListEqual<Element>
    {
      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(this.elements != null);
      }

      private Element[] elements;
      private int? cachedHashCode;

      public ListEqual(int len, IEnumerable<Element> arr)
      {
        Contract.Requires(len >=  0);
        Contract.Requires(arr != null);

        this.elements = new Element[len];
        var i = 0;
        foreach (var x in arr)
        {
          if (i >= len)
            break;

          this.elements[i++] = x;
        }
      }

      public override int GetHashCode()
      {
        if (!this.cachedHashCode.HasValue)
        {
          var local = 0;
          foreach (var x in this.elements)
          {
            local += x.GetHashCode();
          }

          this.cachedHashCode = local;
        }
        return this.cachedHashCode.Value;
      }

      public override bool Equals(object obj)
      {
        var asListEqual = obj as ListEqual<Element>;

        if (asListEqual == null)
        {
          return false;
        }

        Contract.Assume(asListEqual.elements != null);

        if (this.elements.Length != asListEqual.elements.Length)
        {
          return false;
        }

        // Such a common case that we specialize it
        if (this.elements.Length == 1)
        {
          return this.elements[0].Equals(asListEqual.elements[0]);
        }

        // slow path
        return this.elements.Intersect(asListEqual.elements).Count() == this.elements.Length;
      }
    }

    #endregion

    #region Overridden

    public override string ToString()
    {
      return this.ToString(null);
    }

    public string ToString(Func<Variable, bool> except)
    {
      if (!this.relation.HasValue)
      {
        return ListToString(this.left, except);
      }
      else
      {
        var leftStr = ListToString(this.left, except);
        var rightStr = ListToString(this.right, except);
        var retVal = leftStr + " " + this.relation + " " + rightStr;

        return retVal;
      }

    }

    static private string ListToString(Monomial<Variable>[] l, Func<Variable, bool> except)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      if (l == null)
      {
        return "";
      }

      var s = new StringBuilder();
      if (l.Length == 0)
      {
        return "()";
      }
      else
      {
        var elems = new List<string>();

        foreach (var m in l)
        {
          if (except != null && m.Variables.Any(except))
          {
            continue;
          }

          elems.Add(m.ToString() + ", ");
        }

        // To simplify debugging
        elems.Sort();

        s.Append("(");

        foreach (var str in elems)
        {
          s.Append(str);
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

    #region Private helper methods
    /// <returns>false if an arithmetic error may occur</returns>
    [ContractVerification(true)]
    private static bool TryReduceCoefficents(Monomial<Variable>[] simplifiedLeft, Monomial<Variable>[] simplifiedRight)
    {
      Contract.Requires(simplifiedLeft != null);
      Contract.Requires(simplifiedRight != null);
      Contract.Requires(simplifiedRight.Length > 0);

      if (simplifiedRight[0].K.IsMinValue)
      {
        return false;
      }

      var r = Rational.Abs(simplifiedRight[0].K);

      if (r.IsNotZero)
      {
        // We check that all can be divided by r. A better implementation should consider the GCD, and merge the two cycles
        foreach (var m in simplifiedLeft)
        {
          if (!(m.K / r).IsInteger)
          {
            return true;
          }
        }

        simplifiedRight[0].K = simplifiedRight[0].K / r;

        for (int i = 0; i < simplifiedLeft.Length; i++)
        {
          simplifiedLeft[i].K = simplifiedLeft[i].K / r;
        }
      }

      return true;
    }

    /// <summary>
    /// Meaning: let <code>polLeft = (m1 + ... + mt)</code> and <code>polRight = (n1 + ... + nk)</code>, 
    /// it returns the polynome <code>(m1 + ... + mt + n1 + ... + nk)</code>
    /// </summary>
    private static Polynomial<Variable, Expression> Concatenate(Polynomial<Variable, Expression> polLeft, Polynomial<Variable, Expression> polRight)
    {
      var concat = new Monomial<Variable>[polLeft.Left.Length + polRight.Left.Length];

      Array.Copy(polLeft.Left, concat, polLeft.Left.Length);
      Array.Copy(polRight.Left, 0, concat, polLeft.Left.Length, polRight.Left.Length);

      return new Polynomial<Variable, Expression>(concat);
    }



    private static bool TryToPolynomialFormHelperForMultiplication(
      Polynomial<Variable, Expression> polLeft, Polynomial<Variable, Expression> polRight, out Polynomial<Variable, Expression> result)
    {
      Contract.Requires(polLeft.left != null);
      Contract.Requires(polRight.left != null);

      var left = polLeft.left;
      var right = polRight.left;
      var multiplicationResult = new List<Monomial<Variable>>(left.Length * right.Length);

      foreach (var l in left) // l = (k, xs)
      {
        foreach (var r in right)   // r  = (c, ys)
        {
          Rational resultCoeff; // result = (k*c, xs... ys)
          if (!Rational.TryMul(l.K, r.K, out resultCoeff))
          {
            result = default(Polynomial<Variable, Expression>);
            return false;
          }

          Contract.Assert(!object.Equals(resultCoeff, null));

          var concat = new List<Variable>(l.Degree + r.Degree);
          concat.AddRange(l.Variables);
          concat.AddRange(r.Variables);

          var resultMonomial = new Monomial<Variable>(resultCoeff, concat);

          multiplicationResult.Add(resultMonomial);
        }
      }

      result = new Polynomial<Variable, Expression>(multiplicationResult.ToArray());
      return true;
    }


    [Pure]
    private static bool TryMinus(Polynomial<Variable, Expression> p, out Polynomial<Variable, Expression> result)
    {
      if (p.Relation.HasValue)
      {
        result = default(Polynomial<Variable, Expression>);
        return false;
      }

      var minusP = new Monomial<Variable>[p.Left.Length];
      var counter = 0;
      foreach (var m in p.Left)
      {
        var v = -m.K;
        if (!v.IsInfinity)
        {
          var minusM = new Monomial<Variable>(v, m.Variables);
          minusP[counter++] = minusM;
        }
        else  // Polynomial with an infinity component does not make sense, so we fail
        {
          result = default(Polynomial<Variable, Expression>);
          return false;
        }
      }

      result = new Polynomial<Variable, Expression>(minusP);
      return true;
    }

    [Pure]
    private static Expression ConvertToPureExpression(Monomial<Variable>[] p, IExpressionEncoder<Variable, Expression> encoder)
    {
      Contract.Requires(p != null);
      Contract.Requires(encoder != null);

      var result = default(Expression);
      if (p.Length == 0)
      {
        return encoder.ConstantFor(0); //return new Constant<int>(0);
      }
      else
      {
        foreach (var m in p)
        {
          bool neg;
          Expression newmonomial = ConvertToPureExpression(m, encoder, out neg);

          if (result == null)
          {
            if (neg)
              result = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.UnaryMinus, newmonomial);
            else
              result = newmonomial;
          }
          else if (neg)
            result = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Subtraction, result, newmonomial);  // result = result - m
          else
            result = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, result, newmonomial);  // result = result + m

        }
        return result;
      }
    }

    private static Expression ConvertToPureExpression(Monomial<Variable> m, IExpressionEncoder<Variable, Expression>/*!*/ encoder, out bool neg)
    {
      Contract.Requires(encoder != null);

      if (m.IsConstant)
      {
        neg = m.K < 0;
        return Rational.Abs(m.K).ToExpression(encoder);
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
          result = m.K.ToExpression(encoder);
          first = false;
        }

        foreach (var v in m.Variables)
        {
          if (first)
          {
            result = encoder.VariableFor(v);
            first = false;
          }
          else
          {
            result = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Multiplication, result, encoder.VariableFor(v)); // result = result * v      
          }
        }

        return result;
      }

    }

    private static Monomial<Variable>[] ExtractWritableBytesExpression(
      Expression e, IExpressionDecoder<Variable, Expression> decoder, out bool success)
    {
      Contract.Requires(decoder != null);
      Contract.Ensures(!Contract.ValueAtReturn(out success) || Contract.Result<Monomial<Variable>[]>() != null);

      Monomial<Variable>[] polLeft, polRight;
      Monomial<Variable>[] result;
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
            for (var i = 0; i < polRight.Length; i++)
            {
              polRight[i].K = -polRight[i].K;
            }

            var tmpresult = new List<Monomial<Variable>>(polLeft);
            tmpresult.AddRange(polRight);

            result = tmpresult.ToArray();
          }
          else
          {
            result = default(Monomial<Variable>[]);
            success = false;
          }
          break;

        //We don't expect other than k*x
        case ExpressionOperator.Multiplication:
          Polynomial<Variable, Expression> temp;
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
            var lngVar = decoder.UnderlyingVariable(lng);
            var lngMonome = new Monomial<Variable>(lngVar);
            success = true;
            result = new Monomial<Variable>[] { lngMonome };
          }
          else
#endif
          {
            success = false;
            result = new Monomial<Variable>[] { new Monomial<Variable>(decoder.UnderlyingVariable(e)) };
          }
          break;

        default:

          Polynomial<Variable, Expression> poly;
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

    private class TryToPolynomialVisitor : GenericExpressionVisitor<Unit, bool, Variable, Expression>
    {
      Polynomial<Variable, Expression> result;

      public TryToPolynomialVisitor(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
      }

      public bool Visit(Expression exp, out Polynomial<Variable, Expression> res)
      {
        if (exp != null && this.Visit(exp, Unit.Value))
        {
          res = result;
          return true;
        }
        else
        {
          res = default(Polynomial<Variable, Expression>);
          return false;
        }
      }

      public override bool VisitConstant(Expression left, Unit data)
      {
        var value = new VisitorForEvalConstant(this.Decoder).Visit(left, Unit.Value);

        if (value.Two && !value.One.IsInfinity)
        {
          var constantMonome = new Monomial<Variable>(value.One);
          result = new Polynomial<Variable, Expression>(constantMonome);

          return true;
        }
        return false;
      }

      public override bool VisitAddition(Expression left, Expression right, Expression original, Unit data)
      {
        Polynomial<Variable, Expression> polLeft, polRight;

        var visitLeft = Visit(left, out  polLeft);
        var visitRight = Visit(right, out polRight);

        // Let's do the 4 cases 
        // We converted left and right
        if (visitLeft && visitRight)
        {
          Int64 kLeft, kRight;
          object vv;
          if (polLeft.IsIntConstant(out kLeft) && polRight.IsIntConstant(out kRight) &&
            EvaluateArithmeticWithOverflow.TryBinary<Int64>(Decoder.TypeOf(original), ExpressionOperator.Addition, kLeft, kRight, out vv))
          {
            var asLong = vv.ForceInt64();
            if (asLong.HasValue)
            {
              this.result = new Polynomial<Variable, Expression>(new Monomial<Variable>(Rational.For(asLong.Value)));
              return true;
            }
          }

          result = Concatenate(polLeft, polRight);
          return true;
        }

#if UseSymbolicValuesForAdditions

        // We converted left, but not right
        if (visitLeft && !visitRight)
        {
          var monomialForRight = new Monomial<Variable>(this.Decoder.UnderlyingVariable(right));

          Int64 kLeft;
          if (polLeft.IsIntConstant(out kLeft))
          {
            var monomials = new Monomial<Variable>[] { new Monomial<Variable>(Rational.For(kLeft)), monomialForRight };
            this.result = new Polynomial<Variable, Expression>(monomials);
          }
          else
          {
            this.result = Concatenate(polLeft, new Polynomial<Variable, Expression>(monomialForRight));
          }

          return true;
        }
        // We converted right, but not left
        if (!visitLeft && visitRight)
        {
          var monomialForLeft = new Monomial<Variable>(this.Decoder.UnderlyingVariable(left));
          Int64 kRight;
          if (polRight.IsIntConstant(out kRight))
          {
            var monomials = new Monomial<Variable>[] { monomialForLeft, new Monomial<Variable>(Rational.For(kRight)) };
            this.result = new Polynomial<Variable, Expression>(monomials);
          }
          else
          {
            this.result = Concatenate(new Polynomial<Variable, Expression>(monomialForLeft), polRight);
          }

          return true;
        }

        if (!visitRight && !visitLeft)
        {
          var monomials = new Monomial<Variable>[] { new Monomial<Variable>(this.Decoder.UnderlyingVariable(left)), new Monomial<Variable>(this.Decoder.UnderlyingVariable(right)) };
          this.result = new Polynomial<Variable, Expression>(monomials);

          return true;
        }

        Contract.Assert(false);
#endif        
        return false;
      }

      public override bool VisitAddition_Overflow(Expression left, Expression right, Expression original, Unit data)
      {
        Polynomial<Variable, Expression> polLeft, polRight;

        if (Visit(left, out  polLeft) && Visit(right, out polRight))
        {
          result = Concatenate(polLeft, polRight);
          return true;
        }

        return false;
      }

      public override bool VisitSubtraction(Expression left, Expression right, Expression original, Unit data)
      {
        Polynomial<Variable, Expression> polLeft, polRight;

        if (Visit(left, out polLeft) && Visit(right, out polRight))
        {
          Int64 kLeft, kRight;
          object vv;
          var type = Decoder.TypeOf(original);
          if (polLeft.IsIntConstant(out kLeft) && polRight.IsIntConstant(out kRight) 
            && 
            (type != ExpressionType.Int32 || kRight != Int32.MinValue) 
            && 
            (type != ExpressionType.Int64 || kLeft != Int64.MinValue)
            &&
            EvaluateArithmeticWithOverflow.TryBinary<Int64>(type, ExpressionOperator.Subtraction, kLeft, kRight, out vv))
          {
            var asLong = vv.ForceInt64();
            if (asLong.HasValue)
            {
              result = new Polynomial<Variable, Expression>(new Monomial<Variable>(Rational.For(asLong.Value)));
              return true;
            }
          }

          return TryToPolynomialFormHelperForSubtraction(polLeft, polRight, out result);
        }

        return false;
      }

      public override bool VisitSubtraction_Overflow(Expression left, Expression right, Expression original, Unit data)
      {
        Polynomial<Variable, Expression> polLeft, polRight;

        if (Visit(left, out polLeft) && Visit(right, out polRight))
        {
          return TryToPolynomialFormHelperForSubtraction(polLeft, polRight, out result);
        }
        return false;
      }

      public override bool VisitModulus(Expression left, Expression right, Expression original, Unit data)
      {
        result = new Polynomial<Variable, Expression>(new Monomial<Variable>(Decoder.UnderlyingVariable(original)));

        return true;
      }

      public override bool VisitMultiplication(Expression left, Expression right, Expression original, Unit data)
      {
        Polynomial<Variable, Expression> polLeft, polRight;

        var isPolLeft = Visit(left, out polLeft);
        var isPolRight = Visit(right, out polRight);

        if (isPolLeft && isPolRight)
        {
          Int64 kLeft, kRight;
          object vv;
          if (polLeft.IsIntConstant(out kLeft) && polRight.IsIntConstant(out kRight) &&
            EvaluateArithmeticWithOverflow.TryBinary<Int64>(Decoder.TypeOf(original), ExpressionOperator.Multiplication, kLeft, kRight, out vv))
          {
            var asLong = vv.ForceInt64();
            if (asLong.HasValue)
            {
              result = new Polynomial<Variable, Expression>(new Monomial<Variable>(Rational.For(asLong.Value)));
              return true;
            }
          }

          return TryToPolynomialFormHelperForMultiplication(polLeft, polRight, out result);
        }
        // Handle the case exp * m
        else if (isPolLeft || isPolRight)
        {
          Expression notPolynomial;

          // Swap so that right is always the non-polynomial
          if (isPolRight)
          {
            polLeft = polRight;
            isPolLeft = true;

            polRight = default(Polynomial<Variable, Expression>);
            isPolRight = false;

            notPolynomial = left;
          }
          else
          {
            notPolynomial = right;
          }

          Monomial<Variable> m;
          if (polLeft.IsMonomial(out m) && TryMultiplyExpressionByMonomial(notPolynomial, m, out result))
          {
            return true;
          }

        }
        return false;
      }

      public override bool VisitMultiplication_Overflow(Expression left, Expression right, Expression original, Unit data)
      {
        Polynomial<Variable, Expression> polLeft, polRight;

        var isPolLeft = Visit(left, out polLeft);
        var isPolRight = Visit(right, out polRight);

        if (isPolLeft && isPolRight)
        {
          return TryToPolynomialFormHelperForMultiplication(polLeft, polRight, out result);
        }
        // Handle the case exp * m
        else if (isPolLeft || isPolRight)
        {
          Expression notPolynomial;

          // Swap so that right is always the non-polynomial
          if (isPolRight)
          {
            polLeft = polRight;
            isPolLeft = true;

            polRight = default(Polynomial<Variable, Expression>);
            isPolRight = false;

            notPolynomial = left;
          }
          else
          {
            notPolynomial = right;
          }

          Monomial<Variable> m;
          if (polLeft.IsMonomial(out m) && TryMultiplyExpressionByMonomial(notPolynomial, m, out result))
          {
            return true;
          }

        }
        return false;
      }

      public override bool VisitShiftRight(Expression left, Expression right, Expression original, Unit data)
      {
        Polynomial<Variable, Expression> polLeft;

        int k;
        if (this.Decoder.IsConstantInt(right, out k) && k >= 0)
        {
          if (Visit(left, out polLeft))
          {
            Rational coeff;

            try
            {
              coeff = Rational.ToThePowerOfTwo(k);
            }
            catch (ArithmeticExceptionRational)
            { // If we've got an exception, it means that we cannot convert this polynomial 
              return false;
            }

            var mList = new List<Monomial<Variable>>();
            foreach (var m in polLeft.Left)
            {
              Rational newCoeff;
              if (Rational.TryDiv(m.K, coeff, out newCoeff) && newCoeff.IsInteger)
              {
                mList.Add(new Monomial<Variable>(newCoeff, m.Variables));
              }
              else
              {
                return false;
              }
            }
            result = new Polynomial<Variable, Expression>(mList.ToArray());
            return true;
          }
        }
        return false;

      }

      public override bool VisitSizeOf(Expression sizeofExp, Unit data)
      {
        int size;
        if (this.Decoder.TrySizeOf(sizeofExp, out size))
        {
          result = new Polynomial<Variable, Expression>(new Monomial<Variable>(size));
          return true;
        }

        return false;
      }

      public override bool VisitDivision(Expression left, Expression right, Expression original, Unit data)
      {
        Polynomial<Variable, Expression> polLeft, polRight;

        if (Visit(left, out polLeft) && Visit(right, out polRight))
        {
          return TryToPolynomialFormHelperForDivision(polLeft, polRight, out result);
        }

        return false;
      }

      public override bool VisitNot(Expression left, Unit data)
      {
        return false;
      }

      public override bool VisitUnaryMinus(Expression left, Expression original, Unit data)
      {
        Polynomial<Variable, Expression> polLeft;

        if (Visit(left, out polLeft))
        {
          return TryMinus(polLeft, out result);
        }

        return false;
      }

      public override bool VisitWritableBytes(Expression left, Expression wholeExpression, Unit data)
      {
        bool success;
        var monome = ExtractWritableBytesExpression(left, this.Decoder, out success);
        if (success)
        {
          result = new Polynomial<Variable, Expression>(monome);
          return true;
        }

        return false;
      }

      public override bool VisitConvertToInt8(Expression left, Expression original, Unit data)
      {
        if (this.Decoder.TypeOf(left) == ExpressionType.Int8)
        {
          return this.Visit(left, data);
        }
        else
        {
          var constValue = new IntervalEnvironment<Variable, Expression>.EvalConstantVisitor(Decoder).Visit(
            left, new IntervalEnvironment<Variable, Expression>(Decoder, VoidLogger.Log));

          Rational v;
          // Take care of polymorphism of constants
          if (constValue.TryGetSingletonValue(out v))
          {
            var value = (short)v;
            this.result = new Polynomial<Variable, Expression>(new Monomial<Variable>(value));
          }
          else
          {
            this.result = new Polynomial<Variable, Expression>(new Monomial<Variable>(this.Decoder.UnderlyingVariable(original)));
          }

          return true;
        }
      }

      public override bool VisitConvertToInt32(Expression left, Expression original, Unit data)
      {
        return this.Visit(left, data);
      }

      public override bool VisitConvertToUInt16(Expression left, Expression original, Unit data)
      {
        var constValue = new IntervalEnvironment<Variable, Expression>.EvalConstantVisitor(Decoder).Visit(
          left, new IntervalEnvironment<Variable, Expression>(Decoder, VoidLogger.Log));

        Rational v;
        // Take care of polymorphism of constants
        if (constValue.TryGetSingletonValue(out v) && v < 0)
        {
          var value = (UInt16)v;
          this.result = new Polynomial<Variable, Expression>(new Monomial<Variable>(value));

          return true;
        }
        else
        {
          return this.Visit(left, data);
        }
      }

      public override bool VisitConvertToUInt32(Expression left, Expression original, Unit data)
      {
        var constValue = new IntervalEnvironment<Variable, Expression>.EvalConstantVisitor(Decoder).Visit(
          left, new IntervalEnvironment<Variable, Expression>(Decoder, VoidLogger.Log));

        Rational v;
        // We assume polynomials containing Int32 coefficients. If the constant is too large, we give up
        if (constValue.TryGetSingletonValue(out v) && v < 0)
        {
          return false;
        }
        else
        {
          return Visit(left, out result);
        }
      }

      public override bool VisitVariable(Variable variable, Expression original, Unit data)
      {
        result = new Polynomial<Variable, Expression>(new Monomial<Variable>(Decoder.UnderlyingVariable(original)));

        return true;
      }

      public override bool VisitEqual(Expression left, Expression right, Expression original, Unit data)
      {
        return HelperForRelations(left, right, original);
      }

      public override bool VisitEqual_Obj(Expression left, Expression right, Expression original, Unit data)
      {
        return HelperForRelations(left, right, original);
      }

      public override bool VisitNotEqual(Expression left, Expression right, Expression original, Unit data)
      {
        return HelperForRelations(left, right, original); ;
      }

      public override bool VisitLessEqualThan(Expression left, Expression right, Expression original, Unit data)
      {
        return HelperForRelations(left, right, original);
      }
      public override bool VisitLessEqualThan_Un(Expression left, Expression right, Expression original, Unit data)
      {
        return HelperForRelations(left, right, original); ;
      }

      public override bool VisitLessThan(Expression left, Expression right, Expression original, Unit data)
      {
        return HelperForRelations(left, right, original); ;
      }

      public override bool VisitLessThan_Un(Expression left, Expression right, Expression original, Unit data)
      {
        return HelperForRelations(left, right, original); ;
      }

      public override bool VisitGreaterEqualThan(Expression left, Expression right, Expression original, Unit data)
      {
        return HelperForRelations(left, right, original); ;
      }

      public override bool VisitGreaterEqualThan_Un(Expression left, Expression right, Expression original, Unit data)
      {
        return HelperForRelations(left, right, original); ;
      }

      public override bool VisitGreaterThan(Expression left, Expression right, Expression original, Unit data)
      {
        return HelperForRelations(left, right, original); ;
      }

      public override bool VisitGreaterThan_Un(Expression left, Expression right, Expression original, Unit data)
      {
        return HelperForRelations(left, right, original); ;
      }

      private bool HelperForRelations(Expression left, Expression right, Expression original)
      {
        Polynomial<Variable, Expression> polLeft, polRight;

        if (Visit(left, out polLeft) && Visit(right, out polRight) && !polLeft.Relation.HasValue && !polRight.Relation.HasValue)
        {
          var op = this.Decoder.OperatorFor(original);  // Doing it instead of passing as parameter as I am lazy
          result = new Polynomial<Variable, Expression>(op, polLeft.left, polRight.left);
          return true;
        }
        return false;
      }

      protected override bool Default(Unit data)
      {
        result = default(Polynomial<Variable, Expression>);
        return false;
      }

      private static bool TryToPolynomialFormHelperForSubtraction(
        Polynomial<Variable, Expression> polLeft, Polynomial<Variable, Expression> polRight, out Polynomial<Variable, Expression> result)
      {
        Polynomial<Variable, Expression> minusPolRight;
        if (TryMinus(polRight, out minusPolRight))
        {
          result = Concatenate(polLeft, minusPolRight);
          return true;
        }
        else
        {
          result = default(Polynomial<Variable, Expression>);
          return false;
        }
      }

      // We consider just the easy cases, when polLeft or polRight are constants
      private bool TryToPolynomialFormHelperForDivision(Polynomial<Variable, Expression> polLeft, Polynomial<Variable, Expression> polRight,
        out Polynomial<Variable, Expression> result)
      {       
        long leftK, rightK;
        if (polLeft.IsIntConstant(out leftK) && polRight.IsIntConstant(out rightK))
        {
          if (rightK != 0)
          {
            var division = leftK / rightK;
            if (Int32.MinValue <= division && division <= Int32.MaxValue)
            {
              result = new Polynomial<Variable, Expression>(new Monomial<Variable>((Int32)division));
              return true;
            }
          }
          result = default(Polynomial<Variable, Expression>);
          return false;
        }

        if (polRight.Left.Length == 1) // (n1 + ... + nt) / m
        {
          var m = polRight.Left[0];
          if (m.IsConstant && m.K != 0)    // (n1 + .... + nt) / k
          {
            var divided = new Monomial<Variable>[polLeft.Left.Length];
            var counter = 0;
            foreach (var n in polLeft.Left)
            {
              Rational v;

              try
              {
                v = n.K / m.K;
              }
              catch (ArithmeticExceptionRational)
              { // Division caused an error
                result = default(Polynomial<Variable, Expression>);
                return false;
              }

              var dividedMonomial = new Monomial<Variable>(v, n.Variables);
              divided[counter++] = dividedMonomial;
            }

            result = new Polynomial<Variable, Expression>(divided);
            return true;
          }
        }

        result = default(Polynomial<Variable, Expression>);
        return false;
      }

      private bool TryMultiplyExpressionByMonomial(Expression exp, Monomial<Variable> m, out Polynomial<Variable, Expression> result)
      {
        Contract.Requires(exp != null);

        Polynomial<Variable, Expression> polLeft, polRight;

        switch (this.Decoder.OperatorFor(exp))
        {
          case ExpressionOperator.Addition:
            // (e1 + e2) * m = (e1 * m) + (e2 * m)
            if (TryMultiplyExpressionByMonomial(this.Decoder.LeftExpressionFor(exp), m, out polLeft) &&
              TryMultiplyExpressionByMonomial(this.Decoder.RightExpressionFor(exp), m, out polRight))
            {
              result = Concatenate(polLeft, polRight);
              return result.TryToCanonicalForm(out result);
            }
            /* else*/
            goto default;

          case ExpressionOperator.Multiplication:
            // (e1 * e2) * m = (e1 * m) * (e2 * m)
            if (TryMultiplyExpressionByMonomial(Decoder.LeftExpressionFor(exp), m, out polLeft) &&
              TryMultiplyExpressionByMonomial(Decoder.RightExpressionFor(exp), m, out polRight))
            {
              return TryToPolynomialFormHelperForMultiplication(polLeft, polRight, out result);
            }
            /* else*/
            goto default;

          case ExpressionOperator.Subtraction:
            // (e1 - e2) * m = (e1 * m) - (e2 * m)
            if (TryMultiplyExpressionByMonomial(Decoder.LeftExpressionFor(exp), m, out polLeft) &&
              TryMultiplyExpressionByMonomial(Decoder.RightExpressionFor(exp), m, out polRight))
            {
              return TryToPolynomialFormHelperForSubtraction(polLeft, polRight, out result);
            }
            /* else*/
            goto default;


          case ExpressionOperator.Constant:
            // k * m = km
            var value = new VisitorForEvalConstant(this.Decoder).Visit(exp, Unit.Value);

            Contract.Assume(!object.ReferenceEquals(value.One, null));

            if (value.Two && !value.One.IsInfinity)
            {
              var k = value.One * m.K;

              result = new Polynomial<Variable, Expression>(new Monomial<Variable>(value.One * m.K, m.Variables));

              return true;
            }
            /* else */
            goto default;

          case ExpressionOperator.Division:
            // Just the case (e1 / e2) * e2 == e1
            if (TryToPolynomialForm(this.Decoder.RightExpressionFor(exp), this.Decoder, out polRight))
            {
              Monomial<Variable> tmp;
              if (polRight.IsMonomial(out tmp) && m.IsEquivalentTo(tmp))
              {
                return TryToPolynomialForm(this.Decoder.LeftExpressionFor(exp), this.Decoder, out result);
              }
            }
            /* else */
            goto default;

          case ExpressionOperator.UnaryMinus:
            // -(e1) * m =  -(e1 * m)
            if (TryToPolynomialForm(this.Decoder.LeftExpressionFor(exp), this.Decoder, out polLeft))
            {
              return TryMinus(polLeft, out result);
            }
            /* else */
            goto default;

          case ExpressionOperator.Variable:
          case ExpressionOperator.Unknown:
            // x * (k * xs) = k * [x; xs]
            var newvars = new List<Variable>(m.Variables);
            newvars.Add(this.Decoder.UnderlyingVariable(exp));

            result = new Polynomial<Variable, Expression>(new Monomial<Variable>(m.K, newvars));
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
            result = default(Polynomial<Variable, Expression>);
            return false;
        }

      }
    }

    /// <summary>
    /// Evaluate the constant to a rational
    /// </summary>
    private class VisitorForEvalConstant
      : GenericTypeExpressionVisitor<Variable, Expression, Unit, Pair<Rational, bool>>
    {
      readonly private static Pair<Rational, bool> NotSuccessfull = new Pair<Rational, bool>(Rational.PlusInfinity, false);

      public VisitorForEvalConstant(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
        Contract.Requires(decoder != null);
      }

      public override Pair<Rational, bool> VisitBool(Expression exp, Unit input)
      {
        return NotSuccessfull;
      }

      public override Pair<Rational, bool> VisitInt8(Expression exp, Unit input)
      {
        SByte value;
        if (Decoder.TryValueOf<SByte>(exp, ExpressionType.Int8, out value))
        {
          return Success(Rational.For(value));
        }
        else
        {
          return NotSuccessfull;
        }
      }

      public override Pair<Rational, bool> VisitInt16(Expression exp, Unit input)
      {
        Int16 value;
        if (Decoder.TryValueOf<Int16>(exp, ExpressionType.Int16, out value))
          return Success(Rational.For(value));
        else
          return NotSuccessfull;
      }

      public override Pair<Rational, bool> VisitInt32(Expression exp, Unit input)
      {
        Int32 value;
        if (Decoder.TryValueOf<Int32>(exp, ExpressionType.Int32, out value))
          return Success(Rational.For(value));
        else
          return NotSuccessfull;
      }

      public override Pair<Rational, bool> VisitInt64(Expression exp, Unit input)
      {
        Int64 value;
        if (Decoder.TryValueOf<Int64>(exp, ExpressionType.Int64, out value))
        {
          if (Rational.CanRepresentExactly(value))
            return Success(Rational.For(value));
          else
            return value > 0 ? Success(Rational.PlusInfinity) : Success(Rational.MinusInfinity);
        }
        else
          return NotSuccessfull;
      }

      public override Pair<Rational, bool> VisitFloat32(Expression exp, Unit input)
      {
        Single value;
        if (Decoder.TryValueOf<Single>(exp, ExpressionType.Float32, out value))
        {
          var intv = value.ConvertFromDouble();
          if (intv.IsSingleton)
            return new Pair<Rational, bool>(intv.LowerBound, true);
        }

        return NotSuccessfull;
      }

      public override Pair<Rational, bool> VisitFloat64(Expression exp, Unit input)
      {
        Double value;
        if (Decoder.TryValueOf<Double>(exp, ExpressionType.Float32, out value))
        {
          var intv = value.ConvertFromDouble();
          if (intv.IsSingleton)
            return new Pair<Rational, bool>(intv.LowerBound, true);
        }

        return NotSuccessfull;
      }

      public override Pair<Rational, bool> VisitString(Expression exp, Unit input)
      {
        return NotSuccessfull;
      }

      public override Pair<Rational, bool> VisitUInt8(Expression exp, Unit input)
      {
        Byte value;
        if (Decoder.TryValueOf<Byte>(exp, ExpressionType.UInt8, out value))
          return Success(Rational.For(value));
        else
          return NotSuccessfull;
      }

      public override Pair<Rational, bool> VisitUInt16(Expression exp, Unit input)
      {
        UInt16 value;
        if (Decoder.TryValueOf<UInt16>(exp, ExpressionType.UInt16, out value))
          return Success(Rational.For(value));
        else
          return NotSuccessfull;
      }

      public override Pair<Rational, bool> VisitUInt32(Expression exp, Unit input)
      {
        UInt32 value;
        if (Decoder.TryValueOf<UInt32>(exp, ExpressionType.UInt32, out value))
          return Success(Rational.For(value));
        else
          return NotSuccessfull;
      }

      public override Pair<Rational, bool> Default(Expression exp)
      {
        if (this.Decoder.IsNull(exp))
          return Success(Rational.For(0));
        else
          return NotSuccessfull;
      }

      private Pair<Rational, bool> Success(Rational value)
      {
        return new Pair<Rational, bool>(value, true);
      }

    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;

      if (!(obj is Polynomial<Variable, Expression>))
        return false;

      var other = (Polynomial<Variable, Expression>)obj;

      return FastEqual(this, other);
    }

    public override int GetHashCode()
    {
      var hash = 0;
      foreach (var m in this.Left)
      {
        hash += m.GetHashCode();
      }

      if (this.Right != null)
      {
        foreach (var m in this.Right)
        {
          hash += m.GetHashCode();
        }
      }

      return hash;
    }

    public static bool FastEqual(Polynomial<Variable, Expression> p1, Polynomial<Variable, Expression> p2)
    {
      if (p1.Relation != p2.Relation)
        return false;
      if (!FastEqual(p1.Left, p2.Left))
        return false;
      if (!FastEqual(p1.Right, p2.Right))
        return false;

      return true;
    }

    [Pure]
    private static bool FastEqual(Monomial<Variable>[] l1, Monomial<Variable>[] l2)
    {
      if (l1 == null && l2 == null)
        return true;
      if (l1 == null || l2 == null)
        return false;

      if (l1.Length != l2.Length)
        return false;

      var b2 = new bool[l2.Length];

      for (int i = 0; i < l1.Length; i++)
      {
        for (int j = 0; j < l2.Length; j++)
        {
          if (b2[j])
            continue;

          if (l1[i].Equals(l2[j]))
          {
            b2[j] = true;

            break;
          }
          return false;
        }
      }

      return true;
    }

    public static IComparer<Polynomial<Variable, Expression>> Comparer
    {
      get
      {
        return new PolynomialComparison();
      }
    }

    private class PolynomialComparison : IComparer<Polynomial<Variable, Expression>>
    {

      #region IComparer<Polynomial<Variable,Expression>> Members

      // F: HACK HACK using strings to compare polynomials, very inefficient, should rewrite it
      public int Compare(Polynomial<Variable, Expression> x, Polynomial<Variable, Expression> y)
      {
        var xStr = x.ToString();
        var yStr = y.ToString();

        if (xStr == yStr)
          return 0;
        else
          return LexographicOrder(xStr, yStr);
      }

      static private int LexographicOrder(string x, string y)
      {
        Contract.Requires(x != null);
        Contract.Requires(y != null);

        for (var i = 0; i < Math.Min(x.Length, y.Length); i++)
        {
          if (x[i] < y[i])
            return -1;
          else if (x[i] > y[i])
            return 1;
        }

        return x.Length < y.Length ? -1 : 1;
      }

      #endregion
    }
  }

  /// <summary>
  /// A Monomial is a pair <code>(k, x1...xn)</code> where <code>k</code> is a rational number and <code>xi</code> is a variable
  /// </summary>
  [ContractVerification(true)]
  public struct Monomial<Variable>
  {
    #region Cached values
    static readonly private IComparer<Variable> varCompaper = new ExpressionComparer<Variable>();
    #endregion

    #region Object Invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(!object.ReferenceEquals(this.k, null));
      Contract.Invariant(this.variables != null);
    }
    #endregion

    #region Private fields
    private Rational k;
    readonly private Variable[] variables;     // The sorted list of the variables in the monomial
    #endregion

    #region Getters
    public Rational K
    {
      get
      {
        Contract.Ensures(!object.ReferenceEquals(Contract.Result<Rational>(), null));

        return this.k;
      }
      set
      {
        Contract.Requires(!object.ReferenceEquals(value, null));

        this.k = value;
      }
    }

    public IEnumerable<Variable> Variables
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<Variable>>() == this.variables);
        Contract.Ensures(Contract.Result<IEnumerable<Variable>>() != null);

        return this.variables;
      }
    }

    [Pure]
    public Variable VariableAt(int i)
    {
      Contract.Requires(i >= 0);
      Contract.Requires(i < this.Degree);

      return this.variables[i];
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
        Contract.Ensures(Contract.Result<bool>() || this.Degree > 0);

        return this.variables.Length == 0;
      }
    }

    [Pure]
    internal bool IsIntConstant(out long kLeft)
    {
      if (this.variables.Length == 0 && this.k.IsInteger)
      {
        kLeft = (Int64)this.k.NextInt64;
        return true;
      }

      kLeft = default(long);
      return false;
    }


    [Pure]
    public bool ContainsVariable(Variable var)
    {
      foreach (var x in this.variables)
      {
        if (x.Equals(var))
          return true;
      }

      return false;
    }

    [Pure]
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
        Contract.Ensures(Contract.Result<int>() == this.variables.Length);

        return this.variables.Length;
      }
    }

    #endregion

    #region Constructors
    /// <summary>
    /// Constructs a constant from an <code>Int32</code>
    /// </summary>
    public Monomial(int k)
      : this(Rational.For(k))
    {
    }

    ///<summary>
    /// Constructs a constant form a <code>Rational</code>
    ///</summary>
    public Monomial(Rational r)
    {
      Contract.Requires((object)r != null);

      this.k = r;
      this.variables = new Variable[0];
    }

    ///<summary>
    ///Constructs a monomial made by only a variable
    ///</summary>
    public Monomial(Variable x)
    {
      this.k = Rational.For(1);
      this.variables = new Variable[] { x };
    }

    /// <summary>
    /// Constructs a monomial with just one variable
    /// </summary>
    public Monomial(Rational k, Variable x)
    {
      Contract.Requires((object)k != null);

      this.k = k;
      this.variables = k.IsNotZero ? new Variable[] { x } : new Variable[0];
    }

    /// <summary>
    /// Constructs a monomial with just one variable
    /// </summary>
    /// <summary>
    /// Constructs a monomial with just one variable
    /// </summary>
    public Monomial(int k, Variable x)
      : this(Rational.For(k), x)
    {
    }

    /// <summary>
    /// Constructs a monome with several variables (i.e. of a degree > 1)
    /// </summary>
    public Monomial(Rational k, IEnumerable<Variable> xs)
    {
      Contract.Requires(!object.ReferenceEquals(k, null));

      this.k = k;

      var tobeSorted = new List<Variable>(xs);
      tobeSorted.Sort(varCompaper);

      this.variables = tobeSorted.ToArray();
    }

    /// <summary>
    /// Constructs a monome with several variables (i.e. of a degree > 1)
    /// </summary>
    public Monomial(int k, IEnumerable<Variable> xs)
      : this(Rational.For(k), xs)
    {
    }
    #endregion

    private Monomial(Rational k, Variable[] vars)
    {
      Contract.Requires(!object.Equals(k, null));
      Contract.Requires(vars != null);

      this.k = k;
      this.variables = vars;
    }

    public static Monomial<Variable> operator -(Monomial<Variable> m)
    {
      Contract.Assume(m.variables != null);

      var copy = new Variable[m.variables.Length];
      Array.Copy(m.variables, copy, m.variables.Length);
      return new Monomial<Variable>(-m.K, copy);
    }

    public Monomial<Variable> Rename(Variable x, Variable xNew)
    {
      if (this.ContainsVariable(x))
      {
        var newVars = new Set<Variable>(this.Variables);
        newVars.Remove(x);
        newVars.Add(xNew);

        return new Monomial<Variable>(this.K, newVars);
      }
      else
      {
        return this;
      }
    }

    #region Overridden
    public override string ToString()
    {
      return this.k + "*" + VarsToString(this.variables) + " ";
    }

    static private string VarsToString(Variable[] vars)
    {
      if (vars == null)
      {
        return "";
      }

      var s = new StringBuilder();

      if (vars.Length == 0)
      {
        s.Append("1");
      }
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

      var stmp = s.ToString();

      if (stmp.Length > 2)         // Get rid of the last "* ", if any
        stmp = stmp.Remove(stmp.Length - 2);

      return stmp;
    }
    #endregion

    public bool IsEquivalentTo(Monomial<Variable> other)
    {
      if (this.K != other.K)
      {
        return false;
      }

      if (this.Degree != other.Degree)
      {
        return false;
      }

      // This case is so common that we want to specialize it
      if (this.Degree == 1)
      {
        var vThis = this.variables[0];
        var vOther = other.variables[0];

        return vThis.Equals(vOther);
      }
      else
      {
        foreach (var x in this.Variables)
        {
          if (!other.ContainsVariable(x))
            return false;
        }

        return true;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;

      if (object.ReferenceEquals(this, obj))
        return true;

      var other = (Monomial<Variable>)obj;

      return this.IsEquivalentTo(other);
    }

    public override int GetHashCode()
    {
      var hash = 0;
      // if (this.variables != null)
      {
        foreach (var x in this.Variables)
        {
          hash += x.GetHashCode();
        }
      }

      hash += this.K.GetHashCode();

      return hash;
    }

  }
}