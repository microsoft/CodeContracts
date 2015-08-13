// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

            MetaDataDecoder = mdDecoder;
        }

        protected override BoxedExpression Unary(BoxedExpression original, UnaryOperator unaryOperator, BoxedExpression argument)
        {
            int value;
            if (unaryOperator == UnaryOperator.Neg && argument.IsConstantInt(out value))
            {
                return BoxedExpression.Const(-value, MetaDataDecoder.System_Int32, MetaDataDecoder);
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
            return BoxedExpression.Const(k, MetaDataDecoder.System_Int32, MetaDataDecoder);
        }
    }
}
