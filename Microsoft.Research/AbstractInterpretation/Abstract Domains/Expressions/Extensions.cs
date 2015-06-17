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
using Microsoft.Research.AbstractDomains.Numerical;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.AbstractDomains.Expressions
{
  
  [ContractVerification(true)]
  public static class PolynomialExtensions
  {
    public static bool TryMatch_k1XPlusk2YEqualk3<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Rational k1, out Variable x, out Rational k2, out Variable y, out Rational k3)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k1), null));
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k2), null));
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k3), null));

      if (pol.Right == null)
      {
        k1 = k2 = k3 = default(Rational);
        x = y = default(Variable);

        return false;
      }

      if (pol.Degree == 1  && pol.Relation.HasValue
        && pol.Relation.Value == ExpressionOperator.Equal
        && pol.Left.Length == 2 && pol.Right.Length == 1)
      {
        var m3 = pol.Right[0];

        if (m3.IsConstant)
        {
          var m1 = pol.Left[0];
          var m2 = pol.Left[1];

          if (m1.IsVariable(out x) && m2.IsVariable(out y))
          {
            k1 = m1.K;
            k2 = m2.K;
            k3 = m3.K;

            return true;
          }
        }
      }
      
      k1 = k2 = k3 = default(Rational);
      x = y = default(Variable);

      return false;
    }

    public static bool TryMatch_XMinusYEqualk3<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Variable x, out Variable y, out Rational k)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k), null));

      Rational k1, k2, k3;
      if (pol.TryMatch_k1XPlusk2YEqualk3(out k1, out x, out k2, out y, out k3) && k1 == 1 && k2 == -1)
      {
        k = k3;

        return true;
      }

      k = default(Rational);

      return false;
    }

    public static bool TryMatch_YMinusK<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Variable y, out Rational k)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k),null));

      if (pol.Relation == null && pol.Degree == 1)
      {
        var monomials = pol.Left;
        if (monomials.Length == 2 && monomials[0].IsLinear && monomials[1].IsConstant)
        {
          k = monomials[1].K;

          if (k < 0)
          {
            if(monomials[0].IsVariable(out y))
              return true;
          }
        }
      }

      y = default(Variable);
      k = default(Rational);
      return false;
    }

    public static bool TryMatch_YPlusK<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Variable y, out Rational k)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k), null));

      if (pol.Relation == null && pol.Degree == 1)
      {
        var monomials = pol.Left;
        if (monomials.Length == 2 && monomials[0].IsLinear && monomials[0].K >= 0 && monomials[1].IsConstant)
        {
          k = monomials[1].K;

          if (k > 0)
          {
            if(monomials[0].IsVariable(out y))
              return true;
          }
        }
      }

      y = default(Variable);
      k = default(Rational);

      return false;
    }

    public static bool TryMatch_Y<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Variable y)
    {
      if (pol.Degree == 1 && pol.Relation == null && pol.Left.Length == 1)
      {
        if(pol.Left[0].IsVariable(out y))
          return true;
      }

      y = default(Variable);
      return false;
    }

    public static bool TryMatch_XPlusY<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Variable x, out Variable y)
    {
      x = default(Variable);
      y = default(Variable);
     
      if (!pol.Relation.HasValue && pol.IsLinear && pol.Left.Length == 2)
      {
        var m1 = pol.Left[0];
        var m2 = pol.Left[1];

        return m1.K == 1 && m2.K == 1 && m1.IsVariable(out x) && m2.IsVariable(out y);
      }

      return false;
    }

    public static bool TryMatch_XMinusY<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Variable x, out Variable y)
    {

      if (!pol.Relation.HasValue && pol.IsLinear && pol.Left.Length == 2)
      {
        var m1 = pol.Left[0];
        var m2 = pol.Left[1];

        if (!m1.IsConstant && !m2.IsConstant)
        {
          if (m1.K == 1 && m2.K == -1)
          {
            x = m1.VariableAt(0);
            y = m2.VariableAt(0);

            return true;
          }
          if (m1.K == -1 && m2.K == 1)
          {
            x = m2.VariableAt(0);
            y = m1.VariableAt(0);

            return true;
          }
        }
      }

      x = default(Variable);
      y = default(Variable);

      return false;
    }

    public static bool TryMatch_k1XPlusk2YLessEqualThanK<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Rational k1, out Variable x1, out Rational k2, out Variable x2, out Rational k)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k1), null));
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k2), null));
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k), null));

      if (pol.Left.Length == 2 && pol.IsOctagonForm && pol.Relation.HasValue && pol.Relation.Value.IsLessEqualThan() && pol.Right.Length > 0)
      {
        k1 = pol.Left[0].K;
        var b1 = pol.Left[0].IsVariable(out x1);

        k2 = pol.Left[1].K;
        var b2 = pol.Left[1].IsVariable(out x2);

        k = pol.Right[0].K;

        return b1 && b2;
      }

      k1 = k2 = k = default(Rational);
      x1 = x2 = default(Variable);
      return false;
    }

    public static bool TryMatch_k1XPlusk2YLessThanK<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Rational k1, out Variable x1, out Rational k2, out Variable x2, out Rational k)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k1), null));
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k2), null));
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k), null));

      if (pol.Left.Length == 2 && pol.IsOctagonForm && pol.Relation.HasValue && pol.Right.Length > 0)
      {
        k1 = pol.Left[0].K;
        var b1 = pol.Left[0].IsVariable(out x1);

        k2 = pol.Left[1].K;
        var b2 = pol.Left[1].IsVariable(out x2);
        if (pol.Relation.Value.IsLessThan())
        {
          k = pol.Right[0].K;

          return true;
        }
        else if (pol.Relation.Value.IsLessEqualThan()) // == ExpressionOperator.LessEqualThan)
        {
          k = pol.Right[0].K + 1;

          return true;
        }
      }

      k1 = k2 = k = default(Rational);
      x1 = x2 = default(Variable);
      return false;
    }

    public static bool TryMatch_XPlusYLess_Equal_ThanZPlusK<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Variable x1, out Variable x2, out Variable x3, out Rational k)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k), null));

      x1 = x2 = x3 = default(Variable);
      k = default(Rational);

      if (pol.Right == null)
      {
        return false;
      }

      var x = new Variable[3];
      if (pol.Left.Length == 3)
      {
        var found = false;
        for (int i = 0; i < 3; i++)
        {
          var mon = pol.Left[i];
          if (!mon.IsLinear)
          {
            return false;
          }
          switch ((int)mon.K)
          {
            case 1:
               mon.IsVariable(out x[i - (found ? 1 : 0)]);
              break;
            case -1:
              if (found)
                return false;
              found = true;
               mon.IsVariable(out x[2]);
              break;
            default:
              return false;
          }
        }
        if (!found)
          return false;
        if (pol.Right.Length == 1 && pol.Right[0].IsConstant)
        {
          k = pol.Right[0].K;
          x1 = x[0];
          x2 = x[1];
          x3 = x[2];
          return true;
        }
      }
      return false;
    }

    public static bool TryMatch_XLess_Equal_ThanYPlusZPlusK<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Variable x1, out Variable x2, out Variable x3, out Rational k)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k), null));

      x1 = x2 = x3 = default(Variable);
      k = default(Rational);

      if (pol.Right == null)
      {
        return false;
      }

      var x = new Variable[3];
      if (pol.Left.Length == 3)
      {
        var found = false;
        for (int i = 0; i < 3; i++)
        {
          var mon = pol.Left[i];
          if (!mon.IsLinear)
            return false;
          switch ((int)mon.K)
          {
            case -1:
               mon.IsVariable(out x[i - (found ? 1 : 0)]);
              break;
            case 1:
              if (found)
                return false;
              found = true;
               mon.IsVariable(out x[2]);
              break;
            default:
              return false;
          }
        }
        if (!found)
          return false;
        if (pol.Right.Length == 1 && pol.Right[0].IsConstant)
        {
          k = pol.Right[0].K;
          x1 = x[2];
          x2 = x[0];
          x3 = x[1];
          return true;
        }
      }
      return false;
    }

    public static bool TryMatch_XMinusYPlusK<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Variable x, out Variable y, out Rational k)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k), null));

      if (!pol.Relation.HasValue && pol.IsLinear)
      {
        if (pol.Left.Length == 3)
        {
          var left0 = pol.Left[0];

          if (left0.K == 1 && pol.Left[1].K == -1 && pol.Left[2].IsConstant)
          {
            left0.IsVariable(out x);
            var b = pol.Left[1].IsVariable(out y);
            k = pol.Left[2].K;

            return b;
          }
          else if (left0.K == -1 && pol.Left[1].K == 1 && pol.Left[2].IsConstant)
          {
            left0.IsVariable(out y);
            var b = pol.Left[1].IsVariable(out x);
            k = pol.Left[2].K;

            return b;
          }

        }
        else if (pol.Left.Length == 2 && pol.Left[0].Degree == 1 && pol.Left[1].Degree == 1)
        {
          if (pol.Left[0].K == 1 && pol.Left[1].K == -1)
          {
            x = pol.Left[0].VariableAt(0);
            y = pol.Left[1].VariableAt(0);
            k = Rational.For(0);

            return true;
          }
          else if (pol.Left[0].K == -1 && pol.Left[1].K == 1)
          {
            y = pol.Left[0].VariableAt(0);
            x = pol.Left[1].VariableAt(0);
            k = Rational.For(0);

            return true;
          }

        }

      }

      x = y = default(Variable);
      k = default(Rational);
      return false;
    }

    public static bool TryMatch_k1XLessThank2<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Rational k1, out Variable x, out Rational k2)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k1), null));
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k2), null));

      if (pol.Relation.HasValue && pol.Relation.Value.IsLessThan())
      {
        return pol.HelperForTryMatch_k1Xopk2(out k1, out x, out k2);
      }
      else
      {
        x = default(Variable);
        k1 = k2 = default(Rational);
        return false;
      }
    }

    public static bool TryMatch_k1XLessEqualThank2<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Rational k1, out Variable x, out Rational k2)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k1), null));
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k2), null));

      if (pol.Relation.HasValue && pol.Relation.Value.IsLessEqualThan())
      {
        return HelperForTryMatch_k1Xopk2(pol, out k1, out x, out k2);
      }
      else
      {
        x = default(Variable);
        k1 = k2 = default(Rational);
        return false;
      }
    }

    public static bool TryMatch_kY<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Variable y, out Rational k)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.ReferenceEquals(Contract.ValueAtReturn(out k), null));

      if (pol.Relation == null && pol.Left.Length == 1 && pol.Left[0].Degree == 1)
      {
        y = pol.Left[0].VariableAt(0);
        k = pol.Left[0].K;
        return true;
      }
     
      y = default(Variable);
      k = default(Rational);
      return false;
    }

    private static bool HelperForTryMatch_k1Xopk2<Variable, Expression>(
      this Polynomial<Variable, Expression> pol,
      out Rational k1, out Variable x, out Rational k2)
    {
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k1), null));
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out k2), null));

      if (pol.IsLinear && pol.Degree != 0 && pol.Left.Length == 1 && pol.Right != null && pol.Right.Length == 1)
      {
        k1 = pol.Left[0].K;
        var b = pol.Left[0].IsVariable(out x);
        k2 = pol.Right[0].K;

        Contract.Assume(pol.Right[0].IsConstant);  // no var

        return b;
      }

      x = default(Variable);
      k1 = k2 = default(Rational);
      return false;
    }
  }

  public static class ObjectExtensions
  {
    static public Int32? ForceInt32(this object value)
    {
      var ic = value as IConvertible;
      if (ic != null)
      {
        try
        {
          return ic.ToInt32(null);
        }
        catch
        {
        }
      }
      return null;
    }

    static public Int64? ForceInt64(this object value)
    {
      var ic = value as IConvertible;
      if (ic != null)
      {
        try
        {
          return ic.ToInt64(null);
        }
        catch
        {
        }
      }
      return null;
    }

  }

  [ContractVerification(true)]
  public static class ExpressionTypeExtensions
  {
    public static bool IsFloatingPointType(this ExpressionType type)
    {
      return type == ExpressionType.Float32 || type == ExpressionType.Float64;
    }

    public static bool IsUnsignedType(this ExpressionType type)
    {
      return type == ExpressionType.UInt8 || type == ExpressionType.UInt16 || type == ExpressionType.UInt32 || type == ExpressionType.UInt64;
    }
  } 

  [ContractVerification(true)]
  public static class ExpressionOperatorExtensions
  {
    public static bool IsRelationalOperator(this ExpressionOperator op)
    {
      switch (op)
      {
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
        case ExpressionOperator.NotEqual:
        case ExpressionOperator.Or:
        case ExpressionOperator.Xor:
          return true;

        default:
          return false;
      }
    }
  }
}
