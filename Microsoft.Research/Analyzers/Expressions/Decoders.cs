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

// The decoder (implemented as visitors for the IL)

using System;
using Generics = System.Collections.Generic;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Collections.Generic;

namespace Microsoft.Research.CodeAnalysis
{
  /// <summary>
  /// The wrapper for the analysis, so to share some code (in particular the decoder)
  /// </summary>
  public static partial class AnalysisWrapper
  {
    // This class is just to provide bindings to type parameters
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type:IEquatable<Type>
    {
      #region Expression decoder, implementation and helper visitors

      /// <summary>
      /// The decoder for the expressions
      /// </summary>
      public class ValueExpDecoder
        : ExternalExpressionDecoder.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Decoder, IExpressionDecoder<Variable, Expression>, IFullExpressionDecoder<Type, Variable, Expression>
      {
        #region Private environment

        private VisitorForOperatorFor visitorForOperatorFor;
        private VisitorForSkepticTypeOf visitorForSkepticTypeOf;
        private VisitorForNameOf visitorForNameOf;

        new IExpressionContextData<Local, Parameter, Method, Field, Type, Expression, Variable> context { get { return base.context.ExpressionContext; } }

        #endregion

        public ValueExpDecoder(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoderForMetaData)
          :base(context, decoderForMetaData)
        {
          Contract.Requires(context != null);

          this.visitorForOperatorFor = new VisitorForOperatorFor(context);
          this.visitorForSkepticTypeOf = new VisitorForSkepticTypeOf();
          this.visitorForNameOf = new VisitorForNameOf(context);
        }

        #region IExpressionDecoder<Variable, Expression> Members

        public ExpressionOperator OperatorFor(Expression exp)
        {
          return this.context.Decode<Expression, ExpressionOperator, VisitorForOperatorFor>(exp, this.visitorForOperatorFor, exp);
        }

        public Expression LeftExpressionFor(Expression exp)
        {
          UnaryOperator uop;
          BinaryOperator op;
          Expression left, right;
          if (base.IsUnaryOperator(exp, out uop, out left))
          {
            return left;
          }
          if (base.IsBinaryOperator(exp, out op, out left, out right))
          {
            return left;
          }
          throw new InvalidOperationException();
        }

        public Expression RightExpressionFor(Expression exp)
        {
          BinaryOperator op;
          Expression left, right;
          if (base.IsBinaryOperator(exp, out op, out left, out right))
          {
            return right;
          }
          throw new InvalidOperationException();
        }

        public Expression Stripped(Expression exp)
        {
          if (this.OperatorFor(exp).IsConversionOperator())
            return this.LeftExpressionFor(exp);
          else
            return exp;
        }

        public ExpressionType TypeOf(Expression/*!*/ exp)
        {
          if (this.IsConstant(exp))
          {
            return this.context.Decode<Unit, ExpressionType, VisitorForSkepticTypeOf>(exp, this.visitorForSkepticTypeOf, Unit.Value); 
          }

          var aType = this.context.GetType(exp);

          var result = ExpressionType.Unknown;

          if (aType.IsNormal)
          {
            Type t = aType.Value;
            if (this.decoderForMetaData.IsPrimitive(t))
            {
              if(this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_Single))
              {
                result = ExpressionType.Float32;
              }
              if(this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_Double))
              {
                result = ExpressionType.Float64;
              }
              if (this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_Int8))
              {
                result = ExpressionType.Int8;
              }
              else if (this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_Int16))
              {
                result = ExpressionType.Int16;
              }
              else if (this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_Int32) /*|| Object.Equals(t, this.decoderForMetaData.System_Int64)*/)
              {
                result = ExpressionType.Int32;
              }
              else if (this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_Int64))
              {
                result = ExpressionType.Int64;
              }
              else if (this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_Boolean))
              {
                result = ExpressionType.Bool;
              }
              else if (this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_String))
              {
                result = ExpressionType.String;
              }
              else if (this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_UInt8))
              {
                result = ExpressionType.UInt8;
              }
              else if (this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_UInt16))
              {
                result = ExpressionType.UInt16;
              }
              else if (this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_UInt32))
              {
                result = ExpressionType.UInt32;
              }
              else if (this.decoderForMetaData.Equal(t, this.decoderForMetaData.System_UInt64))
              {
                result = ExpressionType.UInt32;
              }
            }
          }

          return result;

        }

        public bool IsVariable(Expression exp)
        {
          object var;
          return base.IsVariable(exp, out var);
        }

        public bool IsSlackVariable(Variable v)
        {
          return false;
        }

        public bool IsFrameworkVariable(Variable v)
        {
          return true;
        }

        public bool IsSlackOrFrameworkVariable(Variable v)
        {
          return this.IsFrameworkVariable(v) || this.IsSlackVariable(v);
        }

        public bool IsConstant(Expression exp)
        {
          return this.OperatorFor(exp) == ExpressionOperator.Constant;
        }

        public bool IsConstantInt(Expression exp, out int value)
        {
          object obj;
          Type type;
          if (base.IsConstant(exp, out obj, out type) && obj is Int32)
          {
            value = (Int32)obj;
            return true;
          }

          value = default(Int32);
          return false;
        }

        public bool IsNaN(Expression exp)
        {
          Type type;
          object value;
          if (base.IsConstant(exp, out value, out type))
          {
            if (value is float)
            {
              return ((float)value).Equals(float.NaN);
            }
            if (value is double)
            {
              return ((double)value).Equals(double.NaN);
            }
          }

          return false;
        }

        public object Constant(Expression exp)
        {
          Type type;
          object value;
          base.IsConstant(exp, out value, out type);
          Contract.Assume(!this.decoderForMetaData.Equal(type, this.decoderForMetaData.System_Int32) || value is Int32);
          return value;
        }

        public bool IsWritableBytes(Expression exp)
        {
          return this.OperatorFor(exp) == ExpressionOperator.WritableBytes;
        }

        public bool IsUnaryExpression(Expression exp)
        {
          UnaryOperator oper;
          Expression arg;
          return base.IsUnaryOperator(exp, out oper, out arg);
        }

        public bool IsBinaryExpression(Expression exp)
        {
          BinaryOperator oper;
          Expression left, right;
          return base.IsBinaryOperator(exp, out oper, out left, out right);
        }

        public IEnumerable<Expression> Disjunctions(Expression exp)
        {
          var result = new List<Expression>();
          DisjunctionsInternal(exp, result);

          return result;
        }

        private void DisjunctionsInternal(Expression exp, List<Expression> result)
        {
          Contract.Requires(result != null);

          if (this.IsBinaryExpression(exp) && this.OperatorFor(exp) == ExpressionOperator.LogicalAnd)
          {
            var l = this.LeftExpressionFor(exp);
            result.Add(l);
            DisjunctionsInternal(l, result);
            var r = this.RightExpressionFor(exp);
            result.Add(r);
            DisjunctionsInternal(r, result);
          }
        }

        public Set<Variable> VariablesIn(Expression exp)
        {
          var exps = new Set<Expression>();

          base.AddFreeVariables(exp, exps);

          var result = new Set<Variable>();

          // Annoying to do it, I think there is some more refactoring underneath
          foreach (var e in exps)
          {
            object candidate;
            if (this.IsVariable(e, out candidate))
            {
              if (candidate is Variable)
              {
                result.Add((Variable) candidate);
              }
            }
          }

          return result;
        }

        /// <summary>
        /// Tries to get the value of <code>exp</code>, which must be a constant
        /// </summary>
        public bool TryValueOf<T>(Expression exp, ExpressionType aiType, out T/*?*/ value)
        {
          object obj;
          Type type;

          if (base.IsConstant(exp, out obj, out type))
          {
            if (obj is T)
            {
              value = (T)obj;
              return true;
            }
            else if (obj is int && aiType == ExpressionType.Bool)
            {
              int v = (int)obj;
              value = (T)((object)(v != 0));
              return true;
            }
          }
          // If we reach this point, it means that we were unable to convert it...

          value = default(T);
          return false;
        }

        public string/*?*/ NameOf(Variable var)
        {
          return var.ToString();
        }

        public bool IsSizeOf(Expression exp)
        {
          Type type;
          return base.IsSizeOf(exp, out type);
        }

        #endregion

        #region The visitors

        internal class VisitorForOperatorFor : IVisitValueExprIL<Expression, Type, Expression, Variable, Expression, ExpressionOperator>
        {
          #region Private state
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context;
          #endregion

          public VisitorForOperatorFor(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable>/*!*/ context)
          {
            this.context = context;
          }

          #region IVisitValueExprIL<Label,Type,Expression,Variable,Expression,ExpressionOperator> Members

          public ExpressionOperator SymbolicConstant(Expression pc, Variable symbol, Expression data)
          {
            return ExpressionOperator.Variable;
          }

          #endregion

          #region IVisitExprIL<Expression,Type,Expression,Variable,Expression,ExpressionOperator> Members

          public ExpressionOperator Binary(Expression pc, BinaryOperator op, Variable dest, Expression s1, Expression s2, Expression data)
          {
            switch (op)
            {
              #region All the cases for the binary
              // TODO2: Add the support for unsigned cases
              case BinaryOperator.Add:
              case BinaryOperator.Add_Ovf:
              case BinaryOperator.Add_Ovf_Un:
                return ExpressionOperator.Addition;

              case BinaryOperator.And:
                return ExpressionOperator.And;

              case BinaryOperator.Ceq:
                return ExpressionOperator.Equal;

              case BinaryOperator.Cobjeq:
                return ExpressionOperator.Equal_Obj;

              case BinaryOperator.Cne_Un:
                return ExpressionOperator.NotEqual;

              case BinaryOperator.Cge:
                return ExpressionOperator.GreaterEqualThan;

              case BinaryOperator.Cge_Un:
                return ExpressionOperator.GreaterEqualThan_Un;

              case BinaryOperator.Cgt:
                return ExpressionOperator.GreaterThan;

              case BinaryOperator.Cgt_Un:
                return ExpressionOperator.GreaterThan_Un;

              case BinaryOperator.Cle:
                return ExpressionOperator.LessEqualThan;

              case BinaryOperator.Cle_Un:
                return ExpressionOperator.LessEqualThan_Un;

              case BinaryOperator.Clt:
                return ExpressionOperator.LessThan;

              case BinaryOperator.Clt_Un:
                return ExpressionOperator.LessThan_Un;

              case BinaryOperator.Div:
              case BinaryOperator.Div_Un:
                return ExpressionOperator.Division;

              case BinaryOperator.Mul:
              case BinaryOperator.Mul_Ovf:
              case BinaryOperator.Mul_Ovf_Un:
                return ExpressionOperator.Multiplication;

              case BinaryOperator.Or:
                return ExpressionOperator.Or;

              case BinaryOperator.Rem:
              case BinaryOperator.Rem_Un:
                return ExpressionOperator.Modulus;

              case BinaryOperator.Shl:
                return ExpressionOperator.ShiftLeft;

              case BinaryOperator.Shr:
                return ExpressionOperator.ShiftRight;

              case BinaryOperator.Shr_Un: // TODO
                return ExpressionOperator.Unknown;

              case BinaryOperator.Sub:
              case BinaryOperator.Sub_Ovf:
              case BinaryOperator.Sub_Ovf_Un:
                return ExpressionOperator.Subtraction;

              case BinaryOperator.Xor:  
                return ExpressionOperator.Xor;

              default:
                Contract.Assert(false, "Unknown case when visiting an expression: " + op);
                throw new AbstractInterpretationException("Unknown case : " + op);
              #endregion
            }
          }

          public ExpressionOperator Isinst(Expression pc, Type type, Variable dest, Expression obj, Expression data)
          {
            return ExpressionOperator.Unknown;
          }

          public ExpressionOperator Ldconst(Expression pc, object constant, Type type, Variable dest, Expression data)
          {
            return ExpressionOperator.Constant;
          }

          public ExpressionOperator Ldnull(Expression pc, Variable dest, Expression data)
          {
            return ExpressionOperator.Constant;
          }

          public ExpressionOperator Sizeof(Expression pc, Type type, Variable dest, Expression data)
          {
            return ExpressionOperator.SizeOf;
          }

          public ExpressionOperator Unary(Expression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, Expression data)
          {
            switch (op)
            {
              case UnaryOperator.Conv_u:
                return ExpressionOperator.ConvertToUInt32;

              case UnaryOperator.Conv_u1:
                return ExpressionOperator.ConvertToUInt8;

              case UnaryOperator.Conv_u2:
                return ExpressionOperator.ConvertToUInt16;

              case UnaryOperator.Conv_u4:
                return ExpressionOperator.ConvertToUInt32;

              case UnaryOperator.Conv_i:
              case UnaryOperator.Conv_i4:
              case UnaryOperator.Conv_i1:
              case UnaryOperator.Conv_i2:
              case UnaryOperator.Conv_i8:
                //if (!overflow)
                return ExpressionOperator.ConvertToInt32;
              //else
              //  return ExpressionOperator.Unknown;

              case UnaryOperator.Conv_u8:
                return ExpressionOperator.ConvertToInt32; //Approximation

              case UnaryOperator.Conv_r4:
                return ExpressionOperator.ConvertToFloat32;

              case UnaryOperator.Conv_r8:
                return ExpressionOperator.ConvertToFloat64;

              case UnaryOperator.Conv_r_un:
                return ExpressionOperator.Unknown;

              case UnaryOperator.Neg:
              case UnaryOperator.Not:
                return ExpressionOperator.Not;

              case UnaryOperator.WritableBytes:
                return ExpressionOperator.WritableBytes;

              default:
                Contract.Assert(false, "I do not know this case for unary expressions : " + op);
                throw new AbstractInterpretationException("Unknown case" + op);
            }
          }

          public ExpressionOperator Box(Expression pc, Type type, Variable dest, Expression source, Expression data)
          {
              return ExpressionOperator.Unknown; // TODO support Box
          }

          #endregion
        }

        /// <summary>
        /// This class is a (temporary!!!) Hack.
        /// At the moment, we do not trust the context when it tells us the type of a constant, so we visit it and we check by ourselves which is the real type!
        /// </summary>
        internal class VisitorForSkepticTypeOf : IVisitValueExprIL<Expression, Type, Expression, Variable, Unit, ExpressionType>
        {

          #region IVisitValueExprIL<Expression,Type,Expression,Variable,Unit,ExpressionType> Members

          public ExpressionType SymbolicConstant(Expression pc, Variable symbol, Unit data)
          {
            return ExpressionType.Unknown;
          }

          #endregion

          #region IVisitExprIL<Expression,Type,Expression,Variable,Unit,ExpressionType> Members

          public ExpressionType Binary(Expression pc, BinaryOperator op, Variable dest, Expression s1, Expression s2, Unit data)
          {
            return ExpressionType.Unknown;
          }

          public ExpressionType Isinst(Expression pc, Type type, Variable dest, Expression obj, Unit data)
          {
            return ExpressionType.Unknown;
          }

          public ExpressionType Ldconst(Expression pc, object constant, Type type, Variable dest, Unit data)
          {
            // We are very skeptics, and we ask the runtime type of constant
            if (constant is Int32)
            {
              return ExpressionType.Int32;
            }
            else if (constant is Single)
            {
              return ExpressionType.Float32;
            }
            else if (constant is Double)
            {
              return ExpressionType.Float64;
            }
            else if (constant is Char)
            {
              return ExpressionType.Int8;
            }
            else if (constant is Int16)
            {
              return ExpressionType.Int16;
            }
            else if (constant is Int64)
            {
              return ExpressionType.UInt32;
            }
            else if (constant is Byte)
            {
              return ExpressionType.UInt8;
            }
            else if (constant is UInt16)
            {
              return ExpressionType.UInt16;
            }
            else if (constant is UInt32)
            {
              return ExpressionType.UInt32;
            }
            else if (constant is UInt64)
            {
              return ExpressionType.UInt32;
            }
            else if (constant is Boolean)
            {
              return ExpressionType.Bool;
            }
            else if (constant is String)
            {
              return ExpressionType.String;
            }
            else
            {
              return ExpressionType.Unknown;
            }
          }

          public ExpressionType Ldnull(Expression pc, Variable dest, Unit data)
          {
            return ExpressionType.Unknown;
          }

          public ExpressionType Sizeof(Expression pc, Type type, Variable dest, Unit data)
          {
            return ExpressionType.Int32;
          }

          public ExpressionType Unary(Expression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, Unit data)
          {
            return ExpressionType.Unknown;
          }

          public ExpressionType Box(Expression pc, Type type, Variable dest, Expression source, Unit data)
          {
            return ExpressionType.Unknown;
          }

          #endregion
        }

        internal class VisitorForNameOf : IVisitValueExprIL<Expression, Type, Expression, Variable, Unit, string>
        {
          #region Private state
          private IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context;
          #endregion

          #region Constructor
          public VisitorForNameOf(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context)
          {
            this.context = context;
          }
          #endregion

          #region IVisitValueExprIL<Expression,Type,Expression,Variable,Unit,string> Members

          public string SymbolicConstant(Expression aPC, Variable symbol, Unit data)
          {
            return symbol.ToString();
          }

          #endregion

          #region IVisitExprIL<Expression,Type,Expression,Variable,Unit,string> Members

          public string Binary(Expression pc, BinaryOperator op, Variable dest, Expression s1, Expression s2, Unit data)
          {
            return null;
          }

          public string Isinst(Expression pc, Type type, Variable dest, Expression obj, Unit data)
          {
            return null;
          }

          public string Ldconst(Expression pc, object constant, Type type, Variable dest, Unit data)
          {
            return "Constant(" + constant.ToString() + ")";
          }

          public string Ldnull(Expression pc, Variable dest, Unit data)
          {
            return null;
          }

          public string Sizeof(Expression pc, Type type, Variable dest, Unit data)
          {
            return null;
          }

          public string Unary(Expression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, Unit data)
          {
            return null;
          }

          public string Box(Expression pc, Type type, Variable dest, Expression source, Unit data)
          {
            return null;
          }

          #endregion
        }

        #endregion

      }

      #endregion
    }
  }
}