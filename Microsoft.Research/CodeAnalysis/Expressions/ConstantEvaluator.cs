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
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public class ConstantEvaluator<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : 
    BoxedExpressionTransformer<Void>
  {

    private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder;

    public ConstantEvaluator(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      Contract.Requires(mdDecoder != null);

      this.MetaDataDecoder = mdDecoder;
    }

    protected override BoxedExpression Unary(BoxedExpression original, UnaryOperator unaryOperator, BoxedExpression argument)
    {
      int value;
      if (unaryOperator == UnaryOperator.Neg && argument.IsConstantInt(out value))
      {
        return BoxedExpression.Const(-value, MetaDataDecoder.System_Int32, this.MetaDataDecoder);
      }
      else
      {
        return base.Unary(original, unaryOperator, argument);
      }
    }

    protected override BoxedExpression Binary(BoxedExpression original, BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right)
    {
      int a, b;
      if (left.IsConstantInt(out a) && right.IsConstantInt(out b))
      {
        switch (binaryOperator)
        {
          case BinaryOperator.Add:
          case BinaryOperator.Add_Ovf:
            return this.Const(a + b);

          case BinaryOperator.Add_Ovf_Un:
            return this.Const((uint)a + (uint)b);

          case BinaryOperator.Div:
            return b != 0 ? this.Const(a / b) : original;

          case BinaryOperator.Div_Un:
            return b != 0 ? this.Const((uint)a / (uint)b) : original;

          case BinaryOperator.Mul:
          case BinaryOperator.Mul_Ovf:
            return this.Const(a * b);

          case BinaryOperator.Mul_Ovf_Un:
            return this.Const((uint)a * (uint)b);

          case BinaryOperator.Rem:
            return b != 0 ? this.Const(a % b) : original;

          case BinaryOperator.Rem_Un:
            return b != 0 ? this.Const((uint)a % (uint)b) : original;

          case BinaryOperator.Sub:
          case BinaryOperator.Sub_Ovf:
            return this.Const(a - b);

          case BinaryOperator.Sub_Ovf_Un:
            return this.Const((uint)a - (uint)b);

          case BinaryOperator.Shl:
          case BinaryOperator.Shr:
          case BinaryOperator.Shr_Un:
          default:

            return original;
        }
      }

      var leftResult = this.Visit(left);
      Contract.Assume(leftResult != null);

      var rightResult = this.Visit(right);
      Contract.Assume(rightResult != null);

      return BoxedExpression.Binary(binaryOperator, leftResult, rightResult, original.UnderlyingVariable);
    }

    private BoxedExpression Const(object k)
    {
      return BoxedExpression.Const(k, this.MetaDataDecoder.System_Int32, this.MetaDataDecoder);
    }

  }  

}
