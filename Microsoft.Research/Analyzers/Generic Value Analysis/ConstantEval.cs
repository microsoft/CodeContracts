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
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Expressions;
namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      [ContractVerification(true)]
      public class ConstantEvaluator
      {
        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(this.Context != null);
          Contract.Invariant(this.DecoderForMetaData != null);
        }

        private readonly IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context;
        private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> DecoderForMetaData;

        public ConstantEvaluator(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder)
        {
          Contract.Requires(context != null);
          Contract.Requires(decoder != null);

          this.Context = context;
          this.DecoderForMetaData = decoder;
        }

        #region TryEval

        public bool TryEvalToConstant(APC pc, Variable dest, BinaryOperator op, BoxedExpression leftExp, BoxedExpression rightExp, out long value)
        {
          Contract.Requires(leftExp != null);
          Contract.Requires(rightExp != null);

          var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.Post(pc), dest);

          long leftVal, rightVal;
          if (type.IsNormal && TryEvalToConstant(pc, leftExp, out leftVal) && TryEvalToConstant(pc, rightExp, out rightVal))
          {
            return TryEval(type.Value, op, leftVal, rightVal, out value);
          }

          value = -1;
          return false;
        }

        public bool TryEvalToConstant(APC pc, BoxedExpression exp, out long value)
        {
          Contract.Requires(exp != null);

          // Constants 
          int intVal;
          if (exp.IsConstantIntOrNull(out intVal))
          {
            value = intVal;
            return true;
          }

          long leftVal, rightVal;

          // Unary
          if (exp.IsUnary && TryEvalToConstant(pc, exp.UnaryArgument, out leftVal))
          {
            #region All the cases
            switch (exp.UnaryOp)
            {
              case UnaryOperator.Neg:
                value = -leftVal;
                return true;

              case UnaryOperator.Not:
                value = leftVal == 0 ? 1 : 0;
                return true;

              case UnaryOperator.Conv_i:
                value = (Int32)leftVal;
                return true;

              case UnaryOperator.Conv_i1:
                value = (SByte)leftVal;
                return true;

              case UnaryOperator.Conv_i2:
                value = (Int16)leftVal;
                return true;

              case UnaryOperator.Conv_i4:
                value = (Int32)leftVal;
                return true;

              case UnaryOperator.Conv_i8:
                value = (Int64)leftVal;
                return true;

              case UnaryOperator.Conv_u:
                value = (uint)leftVal;
                return true;

              case UnaryOperator.Conv_u1:
                value = (Byte)leftVal;
                return true;

              case UnaryOperator.Conv_u2:
                value = (UInt16)leftVal;
                return true;

              case UnaryOperator.Conv_u4:
                value = (UInt32)leftVal;
                return true;

              default:
                value = -1;
                return false;
            }
            #endregion
          }

          // Binary
          BinaryOperator bop;
          BoxedExpression leftExp, rightExp;
          Variable var;

          if (exp.TryGetFrameworkVariable(out var) &&
            exp.IsBinaryExpression(out bop, out leftExp, out rightExp) &&
            TryEvalToConstant(pc, leftExp, out leftVal) && TryEvalToConstant(pc, rightExp, out rightVal))
          {
            value = -1;
            var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.Post(pc), var);
            return type.IsNormal && TryEval(type.Value, bop, leftVal, rightVal, out value);
          }

          // IsInst
          object t;
          Variable v;
          FlatDomain<Type> subType;
          BoxedExpression be;
          if (exp.IsIsInstExpression(out be, out t)
            && be.TryGetFrameworkVariable(out v)
            && (subType = this.Context.ValueContext.GetType(pc, v)).IsNormal
            && t is Type)
          {
            var superType = (Type)t;
            Contract.Assert(superType != null);
            var derivesFrom = this.DecoderForMetaData.DerivesFrom(subType.Value, superType);

            if (derivesFrom)
            {
              value = 1; // true
              return true;
            }
            else if (this.DecoderForMetaData.IsSealed(subType.Value))
            {
              // It is not a subtype, and the type is sealed so there is no hope
              value = 0;
              return true;
            }

          }

          // Give up
          value = -1;
          return false;
        }

        public bool TryEval(Type t, BinaryOperator bop, long leftVal, long rightVal, out long value)
        {
          if (TryEvalTypeIndependent(bop, leftVal, rightVal, out value))
          {
            return true;
          }
          // Boolean are not interesting: should have been already considered by the previous case
          //if (this.DecoderForMetaData.System_Boolean.Equals(t))
          //{
          //} 
          if (this.DecoderForMetaData.System_Int8.Equals(t))
          {
          }
          if (this.DecoderForMetaData.System_Int16.Equals(t))
          {
          }
          if (this.DecoderForMetaData.System_Int32.Equals(t))
          {
            return TryEvalInt32(bop, (Int32)leftVal, (Int32)rightVal, out value);
          }
          if (this.DecoderForMetaData.System_Int64.Equals(t))
          {
          }
          if (this.DecoderForMetaData.System_UInt8.Equals(t))
          {
          }
          if (this.DecoderForMetaData.System_UInt16.Equals(t))
          {
          }
          if (this.DecoderForMetaData.System_UInt32.Equals(t))
          {
            UInt32 uvalue;
            if (TryEvalUInt32(bop, (UInt32)leftVal, (UInt32)rightVal, out uvalue))
            {
              value = (long)uvalue;
              return true;
            }
          }
          if (this.DecoderForMetaData.System_UInt64.Equals(t))
          {
          }

          value = -1;
          return false;
        }
       
        public bool TryEvalTypeIndependent(BinaryOperator bop, long leftVal, long rightVal, out long value)
        {
          switch (bop)
          {
            #region All the cases
            case BinaryOperator.And:
              value = leftVal & rightVal;
              return true;

            case BinaryOperator.Ceq:
              value = ToInt(leftVal == rightVal);
              return true;

            case BinaryOperator.Cge:
              value = ToInt(leftVal >= rightVal);
              return true;

            case BinaryOperator.Cgt:
              value = ToInt(leftVal > rightVal);
              return true;

            case BinaryOperator.Cle:
              value = ToInt(leftVal <= rightVal);
              return true;

            case BinaryOperator.Clt:
              value = ToInt(leftVal < rightVal);
              return true;

            case BinaryOperator.Cne_Un:
              value = ToInt(leftVal != rightVal);
              return true;

            case BinaryOperator.LogicalAnd:
              value = ToInt(leftVal != 0 && rightVal != 0);
              return true;

            case BinaryOperator.LogicalOr:
              value = ToInt(leftVal != 0 || rightVal != 0);
              return true;

            case BinaryOperator.Or:
              value = leftVal | rightVal;
              return true;

            case BinaryOperator.Xor:
              value = leftVal ^ rightVal;
              return true;
          }
          #endregion
          value = -1;
          return false;
        }

        private bool TryEvalInt32(BinaryOperator bop, Int32 leftVal, Int32 rightVal, out long value)
        {
          int result;
          if (EvaluateArithmeticWithOverflow.TryBinary(bop.ToExpressionOperator(), leftVal, rightVal, out result))
          {
            value = result;
            return true;
          }
          value = default(long);
          return false;
        }

        private bool TryEvalUInt32(BinaryOperator bop, long leftVal, long rightVal, out uint value)
        {
          return EvaluateArithmeticWithOverflow.TryBinary(bop.ToExpressionOperator(), leftVal, rightVal, out value);
        }

        private int ToInt(bool b)
        {
          return b ? 1 : 0;
        }

        #endregion
      }
    }
  }
}