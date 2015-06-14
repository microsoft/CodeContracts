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
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public static class BoxedExpressionsUtils
  {
    [ContractVerification(true)]
    public static List<BoxedExpression> SplitConjunctions(this BoxedExpression be)
    {
      Contract.Requires(be != null);
      Contract.Ensures(Contract.Result<List<BoxedExpression>>() != null);

      var result = new List<BoxedExpression>();
      return SplitHelper(BinaryOperator.LogicalAnd, be, result);
    }

    [ContractVerification(true)]
    public static List<BoxedExpression> SplitDisjunctions(this BoxedExpression be)
    {
      Contract.Requires(be != null);
      Contract.Ensures(Contract.Result<List<BoxedExpression>>() != null);

      var result = new List<BoxedExpression>();
      return SplitHelper(BinaryOperator.LogicalOr, be, result);
    }

    public static bool IsConstantTrue(this BoxedExpression be)
    {
      if (!be.IsConstant)
        return false;

      if (be.Constant is bool)
      {
        return ((bool)be.Constant);
      }

      return false;
    }


    /// <summary>
    /// Perform some rewritings to the boxed expressions, so that is is simplified, and it is nicer for a human reader
    /// </summary>
    public static BoxedExpression MakeItPrettier<Variable>(this BoxedExpression be, IExpressionDecoder<BoxedVariable<Variable>, BoxedExpression> decoder, IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> encoder)
    {
      Contract.Requires(encoder != null);

      var result = be;

      // 0. Do not do any work on MinValue <= exp, as -exp <= -MinValue is wrong
      BinaryOperator bop;
      BoxedExpression left, right;
      int maybeMinInt; long maybeMinLong;
      if(be.IsBinaryExpression(out bop, out left, out right) && 
        (bop == BinaryOperator.Cle || bop == BinaryOperator.Clt || bop == BinaryOperator.Cle_Un || bop == BinaryOperator.Clt_Un) &&
        ((left.IsConstantInt(out maybeMinInt) && maybeMinInt == Int32.MinValue) || (left.IsConstantInt64(out maybeMinLong) && maybeMinLong== Int64.MinValue))
        )
      {
        return result;
      }

      // 1. If it is a polynomial, do some arithmetic simplification, puts all the variables on one side, etc. 
      Polynomial<BoxedVariable<Variable>, BoxedExpression> pol;
      if (Polynomial<BoxedVariable<Variable>, BoxedExpression>.TryToPolynomialForm(be, decoder, out pol))
      {
        if (pol.IsTautology)
        {
          return encoder.ConstantFor(true);
        }

        // m1 - m2 == 0 becomes m1 == m2
        BoxedVariable<Variable> x, y;
        Rational k;
        if (pol.TryMatch_XMinusYEqualk3(out x, out y, out k) && k.IsZero)
        {
          var xExp = encoder.VariableFor(x);
          var yExp = encoder.VariableFor(y);
          return encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, xExp, yExp);
        }

        result = pol.ToPureExpression(encoder);
      }

      // To do: add some more pretty printing there

      result = MakeItPrettier(result, x => encoder.ConstantFor(x));

      return result;
    }

    public static bool TryGetRange<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(this Variable var, FlatDomain<Type> t,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder, out IntervalStruct result)
      where Type: IEquatable<Type>
    {
      Contract.Requires(MetaDataDecoder != null);

      if (t.IsNormal)
      {
        var type = t.Value;
        if (MetaDataDecoder.System_Boolean.Equals(type) || MetaDataDecoder.System_Int32.Equals(type))
        {
          result = new IntervalStruct(Int32.MinValue, Int32.MaxValue, (Decimal d) => BoxedExpression.Const((int) d, MetaDataDecoder.System_Int32, MetaDataDecoder) );
          return true;
        }
        if (MetaDataDecoder.System_Char.Equals(type))
        {
          result = new IntervalStruct(Char.MinValue, Char.MaxValue, (Decimal d) => BoxedExpression.Const((char) d, MetaDataDecoder.System_Char, MetaDataDecoder));
          return true;
        }
        if (MetaDataDecoder.System_Int8.Equals(type))
        {
          result = new IntervalStruct(SByte.MinValue, SByte.MaxValue, (Decimal d) => BoxedExpression.Const((sbyte)d, MetaDataDecoder.System_Int8, MetaDataDecoder));
          return true;
        }
        if (MetaDataDecoder.System_Int16.Equals(type))
        {
          result = new IntervalStruct(short.MinValue, short.MaxValue, (Decimal d) => BoxedExpression.Const((short)d, MetaDataDecoder.System_Int16, MetaDataDecoder));
          return true;
        }
        if (MetaDataDecoder.System_Int64.Equals(type))
        {
          result = new IntervalStruct(long.MinValue, long.MaxValue, (Decimal d) => BoxedExpression.Const((long)d, MetaDataDecoder.System_Int64, MetaDataDecoder));
          return true;
        }
        if (MetaDataDecoder.System_UInt8.Equals(type))
        {
          result = new IntervalStruct(byte.MinValue, byte.MaxValue, (Decimal d) => BoxedExpression.Const((byte)d, MetaDataDecoder.System_UInt8, MetaDataDecoder));
          return true;
        }
        if (MetaDataDecoder.System_UInt16.Equals(type))
        {
          result = new IntervalStruct(UInt16.MinValue, UInt16.MaxValue, (Decimal d) => BoxedExpression.Const((UInt16)d, MetaDataDecoder.System_UInt16, MetaDataDecoder));
          return true;
        }
        if (MetaDataDecoder.System_UInt32.Equals(type))
        {
          result = new IntervalStruct(UInt32.MinValue, UInt32.MaxValue, (Decimal d) => BoxedExpression.Const((UInt32)d, MetaDataDecoder.System_UInt32, MetaDataDecoder));
          return true;
        }
        if (MetaDataDecoder.System_UInt64.Equals(type))
        {
          result = new IntervalStruct(UInt64.MinValue, UInt64.MaxValue, (Decimal d) => BoxedExpression.Const((UInt64)d, MetaDataDecoder.System_UInt64, MetaDataDecoder));
          return true;
        }

        // return false for Single, Double, 
      }

      result = default(IntervalStruct);
      return false;
    }
   

    #region privates

    [ContractVerification(true)]
    private static List<BoxedExpression> SplitHelper(BinaryOperator bop, BoxedExpression be, List<BoxedExpression> result)
    {
      Contract.Requires(be != null);
      Contract.Requires(result != null);
      Contract.Ensures(Contract.Result<List<BoxedExpression>>() != null);

      if (be.IsBinary && be.BinaryOp == bop)
      {
        result = SplitHelper(bop, be.BinaryRight, SplitHelper(bop, be.BinaryLeft, result));
      }
      else
      {
        result.Add(be);
      }

      return result;
    }


    delegate BoxedExpression ConstFor(Int32 k);

    private static BoxedExpression MakeItPrettier(BoxedExpression result, ConstFor GetConst)
    {
      BinaryOperator binop;
      BoxedExpression left, right, mLeft, mRight;
      if (result.IsBinaryExpression(out binop, out left, out right))
      {
        #region All the cases
        switch (binop)
        {
          case BinaryOperator.Add:
          case BinaryOperator.Add_Ovf:
          case BinaryOperator.Add_Ovf_Un:
            return result;

          case BinaryOperator.And:
            return BoxedExpression.Binary(BinaryOperator.And, MakeItPrettier(left, GetConst), MakeItPrettier(right, GetConst));

          case BinaryOperator.Ceq:

            // -left == -right -> left == right 
            if (BothWithNegativeSigns(left, right, out mLeft, out mRight, GetConst))
            {
              return BoxedExpression.Binary(BinaryOperator.Ceq, mLeft, mRight);
            }
            else
            {
              return result;
            }

          case BinaryOperator.Cge:
            // -left >= -right  -> left <= right
            if (BothWithNegativeSigns(left, right, out mLeft, out mRight, GetConst))
            {
              return BoxedExpression.Binary(BinaryOperator.Cle, mLeft, mRight);
            }
            else
            {
              return result;
            }

          case BinaryOperator.Cge_Un:
            // -left >= -right  -> left <= right
            if (BothWithNegativeSigns(left, right, out mLeft, out mRight, GetConst))
            {
              return BoxedExpression.Binary(BinaryOperator.Cle_Un, mLeft, mRight);
            }
            else
            {
              return result;
            }

          case BinaryOperator.Cgt:
            // -left > -right -> left < right
            if (BothWithNegativeSigns(left, right, out mLeft, out mRight, GetConst))
            {
              return BoxedExpression.Binary(BinaryOperator.Clt, mLeft, mRight);
            }
            else
            {
              return result;
            }

          case BinaryOperator.Cgt_Un:
            // -left > -right -> left < right
            if (BothWithNegativeSigns(left, right, out mLeft, out mRight, GetConst))
            {
              return BoxedExpression.Binary(BinaryOperator.Clt_Un, mLeft, mRight);
            }
            else
            {
              return result;
            }

          case BinaryOperator.Cle:
            // -left <= -right -> left >= right
            if (BothWithNegativeSigns(left, right, out mLeft, out mRight, GetConst))
            {
              return BoxedExpression.Binary(BinaryOperator.Cge, mLeft, mRight);
            }
            else
            {
              return result;
            }

          case BinaryOperator.Cle_Un:
            // -left <= -right -> left >= right
            if (BothWithNegativeSigns(left, right, out mLeft, out mRight, GetConst))
            {
              return BoxedExpression.Binary(BinaryOperator.Cge_Un, mLeft, mRight);
            }
            else
            {
              return result;
            }

          case BinaryOperator.Clt:
            // -left < -right -> left > right
            if (BothWithNegativeSigns(left, right, out mLeft, out mRight, GetConst))
            {
              return BoxedExpression.Binary(BinaryOperator.Cgt, mLeft, mRight);
            }
            else
            {
              return result;
            }

          case BinaryOperator.Clt_Un:
            // -left < -right -> left > right
            if (BothWithNegativeSigns(left, right, out mLeft, out mRight, GetConst))
            {
              return BoxedExpression.Binary(BinaryOperator.Cgt_Un, mLeft, mRight);
            }
            else
            {
              return result;
            }

          case BinaryOperator.Cne_Un:
            // -left != -right -> left != right
            if (BothWithNegativeSigns(left, right, out mLeft, out mRight, GetConst))
            {
              return BoxedExpression.Binary(BinaryOperator.Cne_Un, mLeft, mRight);
            }
            else
            {
              return result;
            }

          case BinaryOperator.Div:
          case BinaryOperator.Div_Un:
          case BinaryOperator.Mul:
          case BinaryOperator.Mul_Ovf:
          case BinaryOperator.Mul_Ovf_Un:
            return result;


          case BinaryOperator.Or:
            return BoxedExpression.Binary(BinaryOperator.Or, MakeItPrettier(left, GetConst), MakeItPrettier(right, GetConst));

          case BinaryOperator.Rem:
          case BinaryOperator.Rem_Un:
          case BinaryOperator.Shl:
          case BinaryOperator.Shr:
          case BinaryOperator.Shr_Un:
          case BinaryOperator.Sub:
          case BinaryOperator.Sub_Ovf:
          case BinaryOperator.Sub_Ovf_Un:
          case BinaryOperator.Xor:
            return result;

          // Should be unreachable
          default:
            return result;

        }
        #endregion
      }
      else
      {
        return result;
      }
    }

    private static bool BothWithNegativeSigns(BoxedExpression left, BoxedExpression right, out BoxedExpression minusLeft, out BoxedExpression minusRight, ConstFor GetConst)
    {
      if (left.IsUnary && left.UnaryOp == UnaryOperator.Neg)
      {
        // case -left, -right 
        if (right.IsUnary && right.UnaryOp == UnaryOperator.Neg)
        {
          minusLeft = left.UnaryArgument;
          minusRight = right.UnaryArgument;

          return true;
        }

        Int32 value;
        // case -left, constant
        if (right.IsConstant && TryInt32Constant(right.Constant, out value))
        {
          minusLeft = left.UnaryArgument;
          minusRight = GetConst(-value);

          return true;
        }

      }
      else if (right.IsUnary && right.UnaryOp == UnaryOperator.Neg)
      {
        Int32 value;
        // case constant, -right
        if (left.IsConstant && TryInt32Constant(left.Constant, out value))
        {
          minusLeft = GetConst(-value);
          minusRight = right.UnaryArgument;

          return true;
        }
      }

      minusLeft = minusRight = null;
      return false;
    }

    private static bool TryInt32Constant(object o, out Int32 value)
    {
      if (o is Int32)
      {
        value = (Int32)o;
        return true;
      }
      else if (o is Int64)
      {
        Int64 asInt64 = (Int64)o;
        if (asInt64 >= Int32.MinValue && asInt64 <= Int32.MaxValue)
        {
          value = (Int32)asInt64;
          return true;
        }
      }
      value = -2345;
      return false;
    }


    #endregion
  }
}
