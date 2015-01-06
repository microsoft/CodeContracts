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
  public static class EvaluateArithmeticWithOverflow
  {
    static public bool TryBinary(ExpressionOperator op, Int64 e1, Int64 e2, out Int32 res)
    {
      return new EvalInt32().VisitBinary(op, e1, e2, out res);
    }

    static public bool TryBinary(ExpressionOperator op, Int64 e1, Int64 e2, out UInt32 res)
    {
      return new EvalUInt32().VisitBinary(op, e1, e2, out res);
    }

    static public bool TryBinary<In>(ExpressionType targetType, ExpressionOperator op, In e1, In e2, out object res)
      where In: struct
    {
      res = default(object);
      
      switch (targetType)
      {
        case ExpressionType.Int32:
          {
            var unboxed1 = e1.ForceInt64();
            var unboxed2 = e2.ForceInt64();
            Int32 cheat;
            if (unboxed1.HasValue && unboxed2.HasValue &&
              TryBinary(op, unboxed1.Value, unboxed2.Value, out cheat))
            {
              res = cheat;
              return true;
            }
            return false;
          }

        case ExpressionType.UInt32:
          {
            var unboxed1 = e1.ForceInt64();
            var unboxed2 = e2.ForceInt64();
            UInt32 cheat;
            if (unboxed1.HasValue && unboxed2.HasValue &&
              TryBinary(op, unboxed1.Value, unboxed2.Value, out cheat))
            {
              res = cheat;
              return true;
            }
            return false;
          }


        case ExpressionType.Int8:
        case ExpressionType.Int16:
        case ExpressionType.Int64:

        case ExpressionType.UInt8:
        case ExpressionType.UInt16:
        case ExpressionType.UInt64:

        case ExpressionType.String:
        case ExpressionType.Bool:
        case ExpressionType.Float32:
        case ExpressionType.Float64:
        case ExpressionType.Unknown:
          {
            return false;
          }
      }

      Contract.Assert(false);  // Should be unreachable
      return false;
    }

 
    private class EvalInt32 : GenericConstEvalVisitor<Int64, Int32>
    {
      protected override bool VisitAddition(long left, long right, out int r)
      {
        r = (Int32)left + (Int32)right;
        return true;
      }

      protected override bool VisitAnd(long left, long right, out int r)
      {
        r = (Int32)left & (Int32)right;
        return true;
      }

      protected override bool VisitConvertToInt32(long left, out int r)
      {
          r = (Int32)left;
          return true;
      }

      protected override bool VisitDivision(long left, long right, out int r)
      {
        if (right == 0 || (((int)left) == Int32.MinValue && ((int)right == -1)))
        {
          r = default(int);
          return false;
        }
        r = (Int32)left / (Int32)right;
        return true;
      }

      protected override bool VisitEqual(long left, long right, out int r)
      {
        r = (Int32)left == (Int32)right ? 1 : 0;
        return true;
      }

      protected override bool VisitGreaterEqualThan(long left, long right, out int r)
      {
        r = (Int32)left >= (Int32)right ? 1 : 0;
        return true;
      }

      protected override bool VisitGreaterEqualThan_Un(long left, long right, out int r)
      {
        r = (UInt32)left >= (UInt32)right ? 1 : 0;
        return true;
      }

      protected override bool VisitGreaterThan(long left, long right, out int r)
      {
        r = (Int32)left > (Int32)right ? 1 : 0;
        return true;
      }

      protected override bool VisitLessEqualThan(long left, long right, out int r)
      {
        r = (Int32)left <= (Int32)right ? 1 : 0;
        return true;
      }

      protected override bool VisitGreaterThan_Un(long left, long right, out int r)
      {
        r = (UInt32)left > (UInt32)right ? 1 : 0;
        return true;
      }

      protected override bool VisitLessEqualThan_Un(long left, long right, out int r)
      {
        r = (UInt32)left <= (UInt32)right ? 1 : 0;
        return true;
      }

      protected override bool VisitLessThan(long left, long right, out int r)
      {
        r = (Int32)left < (Int32)right ? 1 : 0;
        return true;
      }

      protected override bool VisitLessThan_Un(long left, long right, out int r)
      {
        r = (UInt32)left < (UInt32)right ? 1 : 0;
        return true;
      }

      protected override bool VisitModulus(long left, long right, out int r)
      {
        if (right == 0)
        {
          r = default(int);
          return false;
        }
        r = (Int32)left % (Int32)right;
        return true;      
      }

      protected override bool VisitShiftLeft(long left, long right, out int r)
      {
        r = (Int32)left << (Int32)right;
        return true;
      }

      protected override bool VisitShiftRight(long left, long right, out int r)
      {
        r = (Int32)left >> (Int32)right;
        return true;
      }

      protected override bool VisitMultiplication(long left, long right, out int r)
      {
        r = (Int32)left * (Int32)right;
        return true;
      }

      protected override bool VisitNot(long left, out int r)
      {
        r = ~((Int32)left);
        return true;
      }

      protected override bool VisitNotEqual(long left, long right, out int r)
      {
        r = (Int32)left != (Int32)right ? 1 : 0;
        return true;
      }

      protected override bool VisitSubtraction(long left, long right, out int r)
      {
        r = (Int32)(left -right);
        return true;
      }

      protected override bool VisitUnaryMinus(long left, out int r)
      {
        var asInt = (Int32)left;

        if (asInt == Int32.MinValue)
        {
          r = default(int);
          return false;
        }
        
        r = -asInt;
        return true;
      }

      protected override bool VisitXor(long left, long right, out int r)
      {
        r = (Int32)left ^ (Int32)right;
        return true;
      }
    }

    private class EvalUInt32 : GenericConstEvalVisitor<Int64, UInt32>
    {
      protected override bool VisitAddition(long left, long right, out uint r)
      {
        r = (UInt32)left + (UInt32)right;
        return true;
      }

      protected override bool VisitAnd(long left, long right, out uint r)
      {
        r = (UInt32)left & (UInt32)right;
        return true;
      }

      protected override bool VisitConvertToUInt32(long left, out uint r)
      {
        r = (UInt32)left;
        return true;
      }

      protected override bool VisitDivision(long left, long right, out uint r)
      {
        if (right == 0)
        {
          r = default(int);
          return false;
        }
        r = (UInt32)left / (UInt32)right;
        return true;
      }

      protected override bool VisitEqual(long left, long right, out uint r)
      {
        r = (UInt32)left == (UInt32)right ? 1u : 0u;
        return true;
      }

      protected override bool VisitGreaterEqualThan(long left, long right, out uint r)
      {
        r = (UInt32)left >= (UInt32)right ? 1u : 0u;
        return true;
      }

      protected override bool VisitGreaterEqualThan_Un(long left, long right, out uint r)
      {
        r = (UInt32)left >= (UInt32)right ? 1u : 0u;
        return true;
      }

      protected override bool VisitGreaterThan(long left, long right, out uint r)
      {
        r = (UInt32)left > (UInt32)right ? 1u : 0u;
        return true;
      }

      protected override bool VisitLessEqualThan(long left, long right, out uint r)
      {
        r = (UInt32)left <= (UInt32)right ? 1u : 0u;
        return true;
      }

      protected override bool VisitGreaterThan_Un(long left, long right, out uint r)
      {
        r = (UInt32)left > (UInt32)right ? 1u : 0u;
        return true;
      }

      protected override bool VisitLessEqualThan_Un(long left, long right, out uint r)
      {
        r = (UInt32)left <= (UInt32)right ? 1u : 0u;
        return true;
      }

      protected override bool VisitLessThan(long left, long right, out uint r)
      {
        r = (UInt32)left < (UInt32)right ? 1u : 0u;
        return true;
      }

      protected override bool VisitLessThan_Un(long left, long right, out uint r)
      {
        r = (UInt32)left < (UInt32)right ? 1u : 0u;
        return true;
      }

      protected override bool VisitModulus(long left, long right, out uint r)
      {
        if (right == 0)
        {
          r = default(int);
          return false;
        }
        r = (UInt32)left % (UInt32)right;
        return true;
      }

      protected override bool VisitShiftLeft(long left, long right, out uint r)
      {
        r = (UInt32)left << (Int32)right;
        return true;
      }

      protected override bool VisitShiftRight(long left, long right, out uint r)
      {
        r = (UInt32)left >> (Int32)right;
        return true;
      }

      protected override bool VisitMultiplication(long left, long right, out uint r)
      {
        r = (UInt32)left * (UInt32)right;
        return true;
      }

      protected override bool VisitNot(long left, out uint r)
      {
        r = ~((UInt32)left);
        return true;
      }

      protected override bool VisitNotEqual(long left, long right, out uint r)
      {
        r = (UInt32)left != (UInt32)right ? 1u : 0u;
        return true;
      }

      protected override bool VisitSubtraction(long left, long right, out uint r)
      {
        r = (UInt32)left - (UInt32)right;
        return true;
      }

      protected override bool VisitUnaryMinus(long left, out uint r)
      {
        if (left == Int64.MinValue)
        {
          r = default(uint);
          return false;
        }
        r = (UInt32)(-left);
        return true;
      }

      protected override bool VisitXor(long left, long right, out uint r)
      {
        r = (UInt32)left ^ (UInt32)right;
        return true;
      }
    }
  }

  [ContractVerification(true)]
  abstract public class GenericConstEvalVisitor<In, Out>
  {
    protected bool Default(out Out r)
    {
      r = default(Out);
      return false;
    }

    public bool VisitBinary(ExpressionOperator op, In left, In right, out Out r)
    {
      try
      {
        switch (op)
        {
          #region All the cases
          case ExpressionOperator.Addition:
            return VisitAddition(left, right, out r);

          case ExpressionOperator.And:
            return VisitAnd(left, right, out r);

          case ExpressionOperator.Division:
            return VisitDivision(left, right, out r);

          case ExpressionOperator.Equal:
          case ExpressionOperator.Equal_Obj:
            return VisitEqual(left, right, out r);

          case ExpressionOperator.GreaterEqualThan:
            return VisitGreaterEqualThan(left, right, out r);

          case ExpressionOperator.GreaterEqualThan_Un:
            return VisitGreaterEqualThan_Un(left, right, out r);

          case ExpressionOperator.GreaterThan:
            return VisitGreaterThan(left, right, out r);

          case ExpressionOperator.GreaterThan_Un:
            return VisitGreaterThan_Un(left, right, out r);

          case ExpressionOperator.LessEqualThan:
            return VisitLessEqualThan(left, right, out r);

          case ExpressionOperator.LessEqualThan_Un:
            return VisitLessEqualThan_Un(left, right, out r);

          case ExpressionOperator.LessThan:
            return VisitLessThan(left, right, out r);

          case ExpressionOperator.LessThan_Un:
            return VisitLessThan_Un(left, right, out r);

          case ExpressionOperator.LogicalAnd:
            return VisitLogicalAnd(left, right, out r);

          case ExpressionOperator.LogicalOr:
            return VisitLogicalOr(left, right, out r);

          case ExpressionOperator.Modulus:
            return VisitModulus(left, right, out r);

          case ExpressionOperator.Multiplication:
            return VisitMultiplication(left, right, out r);

          case ExpressionOperator.NotEqual:
            return VisitNotEqual(left, right, out r);

          case ExpressionOperator.Or:
            return VisitOr(left, right, out r);

          case ExpressionOperator.ShiftLeft:
            return VisitShiftLeft(left, right, out r);

          case ExpressionOperator.ShiftRight:
            return VisitShiftRight(left, right, out r);

          case ExpressionOperator.Subtraction:
            return VisitSubtraction(left, right, out r);

          case ExpressionOperator.Unknown:
            return VisitUnknown(left, out r);

          case ExpressionOperator.WritableBytes:
            return VisitWritableBytes(left, right, out r);

          case ExpressionOperator.Xor:
            return VisitXor(left, right, out r);

          default:
            // We do not care for the other cases
            return Default(out r);
          #endregion
        }
      }
      catch (ArithmeticException)
      {
        r = default(Out);
        return false;
      }
    }

    public bool VisitUnary(ExpressionOperator op, In left, out Out r)
    {
      try
      {
        switch (op)
        {
          case ExpressionOperator.Constant:
            return VisitConstant(left, out r);

          case ExpressionOperator.ConvertToInt32:
            return VisitConvertToInt32(left, out r);

          case ExpressionOperator.ConvertToUInt8:
            return VisitConvertToUInt8(left, out r);

          case ExpressionOperator.ConvertToUInt16:
            return VisitConvertToUInt16(left, out r);

          case ExpressionOperator.ConvertToFloat32:
            return VisitConvertToFloat32(left, out r);

          case ExpressionOperator.ConvertToFloat64:
            return VisitConvertToFloat64(left, out r);

          case ExpressionOperator.ConvertToUInt32:
            return VisitConvertToUInt32(left, out r);

          case ExpressionOperator.LogicalNot:
            return VisitLogicalNot(left, out r);

          case ExpressionOperator.Not:
            return VisitNot(left, out r);

          case ExpressionOperator.SizeOf:
            return VisitSizeOf(left, out r);

          case ExpressionOperator.UnaryMinus:
            return VisitUnaryMinus(left, out r);

          default:
            return Default(out r);
        }
      }
      catch (ArithmeticException)
      {
        return Default(out r);
      }
    }

    #region All the cases
    virtual protected bool VisitUnaryMinus(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitSizeOf(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitNot(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitLogicalNot(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitConvertToUInt32(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitConvertToFloat64(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitConvertToFloat32(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitConvertToUInt16(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitConvertToUInt8(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitConvertToInt32(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitConstant(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitXor(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitWritableBytes(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitUnknown(In left, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitSubtraction(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitShiftRight(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitShiftLeft(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitOr(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitNotEqual(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitMultiplication(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitModulus(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitLogicalOr(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitLogicalAnd(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitLessThan_Un(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitLessThan(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitLessEqualThan_Un(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitLessEqualThan(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitGreaterThan_Un(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitGreaterThan(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitGreaterEqualThan_Un(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitGreaterEqualThan(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitEqual(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitDivision(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitAnd(In left, In right, out Out r)
    {
      return Default(out r);
    }

    virtual protected bool VisitAddition(In left, In right, out Out r)
    {
      return Default(out r);
    }
    #endregion
  }

}
