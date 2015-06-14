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

// The instantiation for the numerical analysis, based on the optimistic heap abstraction

using System;
using Generics = System.Collections.Generic;
using Microsoft.Research.AbstractDomains.Expressions;

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;

using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{

  /// <summary>
  /// The decoder for the BoxedExpression class stores the decoder of the InternalExpression and ExternalExpression classes 
  /// and apply them (for instance it uses the ExternalExpression decoder if the passed BoxedExpression contains an ExternalExpression, 
  /// and it boxes the result obtained)
  /// </summary>
  public class BoxedExpressionDecoder<Type, Variable, ExternalExpression> 
    : BoxedExpressionDecoder<Variable>
  {
    #region Private fields
    private IFullExpressionDecoder<Type, Variable, ExternalExpression> outdecoder;
    #endregion

    protected TypeSeeker typeseeker;

    public IFullExpressionDecoder<Type, Variable, ExternalExpression> Outdecoder
    {
      get
      {
        return this.outdecoder;
      }
    }

    #region Constructor
    /// <summary>
    /// In order to be instantiated it requires a decoder for the external expressions
    /// </summary>
    public BoxedExpressionDecoder(IFullExpressionDecoder<Type, Variable, ExternalExpression> outdecoder, TypeSeeker typeseeker)
    {
      this.outdecoder = outdecoder;
      this.typeseeker = typeseeker;
    }

    public BoxedExpressionDecoder(IFullExpressionDecoder<Type, Variable, ExternalExpression> outdecoder)
    {
      this.outdecoder = outdecoder;
      this.typeseeker = null;
    }
    #endregion

    #region Public methods implementing IExpressionDecoder
    //Usually the decoder just checks the type of the passed expression(s) and applies the InternalExpression decoder or the ExternalExpression one
    
    //[DebuggerStepThrough()]
    public override ExpressionOperator OperatorFor(BoxedExpression exp)
    {
      if (exp.IsVariable) return ExpressionOperator.Variable;
      if (exp.IsConstant) return ExpressionOperator.Constant;
      if (exp.IsSizeOf) return ExpressionOperator.SizeOf;
      if (exp.IsUnary)
      {
        #region All the cases
        switch (exp.UnaryOp)
        {
          case UnaryOperator.Conv_i2:
            return ExpressionOperator.ConvertToInt8;

          case UnaryOperator.Conv_i4:
          case UnaryOperator.Conv_i:
          case UnaryOperator.Conv_i8:
            return ExpressionOperator.ConvertToInt32;

          case UnaryOperator.Conv_u:
          case UnaryOperator.Conv_u4:
          case UnaryOperator.Conv_u8:
            return ExpressionOperator.ConvertToUInt32;

          case UnaryOperator.Conv_r4:
            return ExpressionOperator.ConvertToFloat32;

          case UnaryOperator.Conv_r8:
            return ExpressionOperator.ConvertToFloat64;

          case UnaryOperator.Conv_u2:
            return ExpressionOperator.ConvertToUInt16;

          case UnaryOperator.Conv_u1:
            return ExpressionOperator.ConvertToUInt8;

          case UnaryOperator.Neg:
            return ExpressionOperator.UnaryMinus;

          case UnaryOperator.Not:
            return ExpressionOperator.Not;

          case UnaryOperator.WritableBytes:
            return ExpressionOperator.WritableBytes;

          default:
            return ExpressionOperator.Unknown;
        }
        #endregion
      }
      if (exp.IsBinary)
      {
        #region all the cases
        switch (exp.BinaryOp)
        {
          case BinaryOperator.Add:
            return ExpressionOperator.Addition;

          case BinaryOperator.Add_Ovf:
          case BinaryOperator.Add_Ovf_Un:
            return ExpressionOperator.Addition_Overflow;

          case BinaryOperator.And:
            return ExpressionOperator.And;

          case BinaryOperator.Ceq:
            return ExpressionOperator.Equal;

          case BinaryOperator.Cobjeq:
            return ExpressionOperator.Equal_Obj;

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

          case BinaryOperator.Cne_Un:
            return ExpressionOperator.NotEqual;

          case BinaryOperator.Div:
          case BinaryOperator.Div_Un:
            return ExpressionOperator.Division;

          case BinaryOperator.LogicalAnd:
            return ExpressionOperator.LogicalAnd;

          case BinaryOperator.LogicalOr:
            return ExpressionOperator.LogicalOr;

          case BinaryOperator.Mul:
            return ExpressionOperator.Multiplication;
          
          case BinaryOperator.Mul_Ovf:
          case BinaryOperator.Mul_Ovf_Un:
            return ExpressionOperator.Multiplication_Overflow;
         
          case BinaryOperator.Or:
            return ExpressionOperator.Or;

          case BinaryOperator.Rem:
          case BinaryOperator.Rem_Un:
            return ExpressionOperator.Modulus;

          case BinaryOperator.Shl:
            return ExpressionOperator.ShiftLeft;

          case BinaryOperator.Shr:
          case BinaryOperator.Shr_Un:
            return ExpressionOperator.ShiftRight;

          case BinaryOperator.Sub:
            return ExpressionOperator.Subtraction;

          case BinaryOperator.Sub_Ovf:
          case BinaryOperator.Sub_Ovf_Un:
            return ExpressionOperator.Subtraction_Overflow;

          case BinaryOperator.Xor:
            return ExpressionOperator.Xor;

          default:
            return ExpressionOperator.Unknown;
        }
        #endregion
      }

      return ExpressionOperator.Unknown;
    }

    public override BoxedExpression LeftExpressionFor(BoxedExpression exp)
    {
      if (exp.IsBinary) { return exp.BinaryLeft; }
      if (exp.IsUnary) { return exp.UnaryArgument; }
      throw new InvalidOperationException();
    }

    public override BoxedExpression RightExpressionFor(BoxedExpression exp)
    {
      if (exp.IsBinary) { return exp.BinaryRight; }
      throw new InvalidOperationException();
    }

    public override ExpressionType TypeOf(BoxedExpression exp)
    {
      if (exp.IsConstant)
      {
        object value = exp.Constant;
        if (value == null) return ExpressionType.Unknown;
        IConvertible ic = value as IConvertible;
        if (ic != null)
        {
          switch (ic.GetTypeCode())
          {
            case TypeCode.Boolean: return ExpressionType.Bool;
            case TypeCode.Byte: return ExpressionType.UInt8;
            case TypeCode.Single: return ExpressionType.Float32;
            case TypeCode.Double: return ExpressionType.Float64;
            case TypeCode.Int16: return ExpressionType.Int16;
            case TypeCode.Int32: return ExpressionType.Int32;
            case TypeCode.Int64: return ExpressionType.Int64;
            case TypeCode.SByte: return ExpressionType.Int8;
            case TypeCode.String: return ExpressionType.String;
            case TypeCode.UInt16: return ExpressionType.UInt16;
            case TypeCode.UInt32: return ExpressionType.UInt32;
          }
        }
        return ExpressionType.Unknown;
      }
      if (exp.IsUnary)
      {
        switch (exp.UnaryOp) 
        { 
          case UnaryOperator.Not: 
            return ExpressionType.Bool; 
          
          case UnaryOperator.Conv_i1:
            return ExpressionType.Int8;

          case UnaryOperator.Conv_i2:
            return ExpressionType.Int16;

          case UnaryOperator.Conv_i4:
            return ExpressionType.Int32;

          case UnaryOperator.Conv_i8:
            return ExpressionType.Int64;

          case UnaryOperator.Conv_u1:
            return ExpressionType.UInt8;

          case UnaryOperator.Conv_u2:
            return ExpressionType.UInt16;

          case UnaryOperator.Conv_u4:
            return ExpressionType.UInt32;

          case UnaryOperator.Conv_u8:
            return ExpressionType.UInt64;

          case UnaryOperator.Conv_r4:
            return ExpressionType.Float32;

          case UnaryOperator.Conv_r8:
          case UnaryOperator.Conv_r_un:
            return ExpressionType.Float64;

          default: 
            return ExpressionType.Int32; 
        }
      }
      if (exp.IsBinary)
      {
        switch (exp.BinaryOp)
        {
          case BinaryOperator.Ceq:
          case BinaryOperator.Cobjeq:
          case BinaryOperator.Cge:
          case BinaryOperator.Cge_Un:
          case BinaryOperator.Cgt:
          case BinaryOperator.Cgt_Un:
          case BinaryOperator.Cle:
          case BinaryOperator.Cle_Un:
          case BinaryOperator.Clt:
          case BinaryOperator.Clt_Un:
          case BinaryOperator.Cne_Un:
            return ExpressionType.Bool;

          case BinaryOperator.Add:
          case BinaryOperator.Add_Ovf:
          case BinaryOperator.Add_Ovf_Un:
          case BinaryOperator.Div:
          case BinaryOperator.Div_Un:
          case BinaryOperator.Mul:
          case BinaryOperator.Mul_Ovf:
          case BinaryOperator.Mul_Ovf_Un:
          case BinaryOperator.Rem:
          case BinaryOperator.Rem_Un:
          case BinaryOperator.Sub:
          case BinaryOperator.Sub_Ovf:
          case BinaryOperator.Sub_Ovf_Un:
            return Join(TypeOf(exp.BinaryLeft), TypeOf(exp.BinaryRight)); 

          case BinaryOperator.Shl:
          case BinaryOperator.Shr:
          case BinaryOperator.Shr_Un:
          default: 
            return ExpressionType.Int32;
        }
      }
      if (exp.IsVariable && this.typeseeker != null)
      {
        return this.typeseeker(exp);
      }

      return ExpressionType.Unknown;
    }

    private ExpressionType Join(ExpressionType e1, ExpressionType e2)
    {
      if (e1 == e2)
      {
        return e1;
      }
      if (e1 == ExpressionType.Unknown)
      {
        return e2;
      }
      if (e2 == ExpressionType.Unknown)
      {
        return e1;
      }
      if (e1 == ExpressionType.Float64 || e2 == ExpressionType.Float64)
      {
        return ExpressionType.Float64;
      }
      if (e1 == ExpressionType.Float32 || e2 == ExpressionType.Float32)
      {
        return ExpressionType.Float32;
      }

      return e1;
    }

    public override bool IsVariable(BoxedExpression exp)
    {
      return exp.IsVariable;
    }

    public override bool IsSlackVariable(BoxedVariable<Variable> var)
    {
      return var.IsSlackVariable;
    }

    public override bool IsFrameworkVariable(BoxedVariable<Variable> var)
    {
      return var.IsFrameworkVariable;
    }

    public override BoxedVariable<Variable> UnderlyingVariable(BoxedExpression exp)
    {
      // F: HACK: should be changed 
      var embedded = exp.UnderlyingVariable;

      if (embedded is Variable)
      {
        return new BoxedVariable<Variable>((Variable) exp.UnderlyingVariable);
      }
      else if (exp.UnderlyingVariable is BoxedVariable<Variable>)
      {
        return (BoxedVariable<Variable>) embedded;
      }
      else
      {
        return new BoxedVariable<Variable>(false);
      }
    }

    public override bool IsConstant(BoxedExpression exp)
    {
      return exp != null && exp.IsConstant;
    }

    public override bool IsConstantInt(BoxedExpression exp, out int value)
    {
      return exp.IsConstantIntOrNull(out value);
    }

    public override bool IsNaN(BoxedExpression exp)
    {
      if (!exp.IsConstant)
        return false;

      object constant = exp.Constant;
      
      if (constant is float)
      {
        return ((float)constant).Equals(float.NaN);
      }

      if (constant is double)
      {
        return ((double)constant).Equals(double.NaN);
      }

      return false;
    }

    public override object Constant(BoxedExpression exp)
    {
      return exp.Constant;
    }

    public override bool IsWritableBytes(BoxedExpression exp)
    {
      return exp.IsUnary && exp.UnaryOp == UnaryOperator.WritableBytes;
    }

    public override bool IsNull(BoxedExpression exp)
    {
      return exp.IsConstant && exp.IsNull;
    }

    public override bool IsUnaryExpression(BoxedExpression exp)
    {
      return exp.IsUnary;
    }

    public override bool IsBinaryExpression(BoxedExpression exp)
    {
      return exp.IsBinary;
    }

    public override Set<BoxedVariable<Variable>> VariablesIn(BoxedExpression exp)
    {
      var retExp = new Set<BoxedExpression>();
      exp.AddFreeVariables(retExp);

      var result = new Set<BoxedVariable<Variable>>();

      foreach (var e in retExp)
      {
        BoxedVariable<Variable> bv;
        if (e.UnderlyingVariable is Variable)
        {
          bv = new BoxedVariable<Variable>((Variable)e.UnderlyingVariable);
        }
        else if (e.UnderlyingVariable is BoxedVariable<Variable>)
        {
          bv = (BoxedVariable<Variable>)e.UnderlyingVariable;
        }
        else
        {
          continue;
        }
        result.Add(bv);
      }

      return result;
    }

    public override bool TryValueOf<T>(BoxedExpression exp, ExpressionType aiType, out T value)
    {
      if (exp.IsConstant)
      {
        object tmpValue = exp.Constant;
        if (tmpValue != null)
        {
          if (tmpValue is T)
          {
            value = (T)tmpValue;
            return true;
          }
          if (tmpValue is String)
          {
            // early out to avoid format exceptions
            value = default(T);
            return false;
          }
          IConvertible ic = tmpValue as IConvertible;
          if (ic != null)
          {
            try
            {
              value = (T)ic.ToType(typeof(T), null);
              return true;
            }
            catch
            {
            }
          }
        }
      }
      value = default(T);
      return false;
    }

    public override string NameOf(BoxedVariable<Variable> var)
    {
      return var.ToString();
    }

    public override bool IsSizeOf(BoxedExpression exp)
    {
      return exp.IsSizeOf;
    }

    public override bool TrySizeOf(BoxedExpression exp, out int value)
    {
      return exp.SizeOf(out value);
    }

    public override bool TryGetAssociatedExpression(BoxedExpression e, AssociatedInfo infoKind, out BoxedExpression info)
    {
      return e.TryGetAssociatedInfo(infoKind, out info);
    }

    public override bool TryGetAssociatedExpression(APC atPC, BoxedExpression e, AssociatedInfo infoKind, out BoxedExpression info)
    {
      return e.TryGetAssociatedInfo(atPC, infoKind, out info);
    }

    public override BoxedExpression Stripped(BoxedExpression exp)
    {
      if (exp.IsUnary)
      {
        switch (exp.UnaryOp)
        {
          case UnaryOperator.Conv_i:
          case UnaryOperator.Conv_i1:
          case UnaryOperator.Conv_i2:
          case UnaryOperator.Conv_i4:
          case UnaryOperator.Conv_i8:
          case UnaryOperator.Conv_r_un:
          case UnaryOperator.Conv_r4:
          case UnaryOperator.Conv_r8:
          case UnaryOperator.Conv_u:
          case UnaryOperator.Conv_u1:
          case UnaryOperator.Conv_u2:
          case UnaryOperator.Conv_u4:
          case UnaryOperator.Conv_u8:
            return exp.UnaryArgument;

          default:
            break;
        }
      }
      return exp;
    }

    #endregion
  }

  public abstract class BoxedExpressionDecoder<Variable>
    : IExpressionDecoder<BoxedVariable<Variable>, BoxedExpression>
  {
    public delegate ExpressionType TypeSeeker(object o);

    public static BoxedExpressionDecoder<Type, Variable, Expression> Decoder<Type, Expression>(IFullExpressionDecoder<Type, Variable, Expression> decoder,
      TypeSeeker typeseeker)
      where Expression : IEquatable<Expression>
    {
      Contract.Ensures(Contract.Result<BoxedExpressionDecoder<Type, Variable, Expression>>() != null);

      return new BoxedExpressionDecoder<Type, Variable, Expression>(decoder, typeseeker);
    }

    public static BoxedExpressionDecoder<Type, Variable, Expression> Decoder<Type, Expression>(IFullExpressionDecoder<Type, Variable, Expression> decoder)
  where Expression : IEquatable<Expression>
    {
      return new BoxedExpressionDecoder<Type, Variable, Expression>(decoder);
    }

    #region IExpressionDecoder<BoxedVariable<Expression>, BoxedExpression> Members

    public abstract ExpressionOperator OperatorFor(BoxedExpression exp);

    public abstract BoxedExpression LeftExpressionFor(BoxedExpression exp);

    public abstract BoxedExpression RightExpressionFor(BoxedExpression exp);

    public abstract bool IsNull(BoxedExpression exp);

    public abstract ExpressionType TypeOf(BoxedExpression exp);

    public abstract bool TryGetAssociatedExpression(BoxedExpression exp, AssociatedInfo infoKind, out BoxedExpression info);

    public abstract bool TryGetAssociatedExpression(APC atPC, BoxedExpression exp, AssociatedInfo infoKind, out BoxedExpression info);

    public abstract bool IsVariable(BoxedExpression exp);

    public abstract bool IsSlackVariable(BoxedVariable<Variable> var);

    public abstract bool IsFrameworkVariable(BoxedVariable<Variable> var);

    public abstract BoxedVariable<Variable> UnderlyingVariable(BoxedExpression exp);

    public abstract bool IsConstant(BoxedExpression exp);

    public abstract bool IsConstantInt(BoxedExpression exp, out int value);

    public abstract bool IsNaN(BoxedExpression exp);

    public abstract object Constant(BoxedExpression exp);

    public abstract bool IsWritableBytes(BoxedExpression exp);

    public abstract bool IsUnaryExpression(BoxedExpression exp);

    public abstract bool IsBinaryExpression(BoxedExpression exp);

    public abstract bool IsSizeOf(BoxedExpression exp);

    public abstract bool TrySizeOf(BoxedExpression type, out int value);

    public abstract Set<BoxedVariable<Variable>> VariablesIn(BoxedExpression exp);

    public abstract bool TryValueOf<T>(BoxedExpression exp, ExpressionType aiType, out T value);

    public abstract string NameOf(BoxedVariable<Variable> var);

    public abstract BoxedExpression Stripped(BoxedExpression exp);

    #endregion

    public IEnumerable<BoxedExpression> Disjunctions(BoxedExpression exp)
    {
      return exp.SplitDisjunctions();
    }

    public bool IsSlackOrFrameworkVariable(BoxedVariable<Variable> v)
    {
      return this.IsFrameworkVariable(v) || this.IsSlackVariable(v);
    }
  }

  public abstract class BoxedExpressionEncoder<Variable>
    : IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression>
  {
    public static IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> Encoder<Local, Parameter, Method, Field, Property, Event, Type, Expression, /*Variable,*/ Attribute, Assembly>(
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context
      )
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      Contract.Ensures(Contract.Result<IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression>>() != null);

      return new BoxedExpressionEncoder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>(mdDecoder);
    }

    #region IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> Members

    // F: This is only for debugging
    public abstract void ResetFreshVariableCounter();

    public abstract BoxedVariable<Variable> FreshVariable<T>();

    public abstract BoxedExpression ConstantFor(object value);

    public abstract BoxedExpression VariableFor(BoxedVariable<Variable> v);

    public abstract BoxedExpression CompoundExpressionFor(ExpressionType type, ExpressionOperator op, BoxedExpression left);

    public abstract BoxedExpression CompoundExpressionFor(ExpressionType type, ExpressionOperator op, BoxedExpression left, BoxedExpression right);

    public abstract BoxedExpression Substitute(BoxedExpression source, BoxedExpression x, BoxedExpression exp);

    #endregion
  }

  /// <summary>
  /// The encoder for the BoxedExpression class that creates no external boxes
  /// </summary>
  public class BoxedExpressionEncoder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable> : BoxedExpressionEncoder<Variable>
  {
    #region Private fields
    readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;

    private class VariableRep : IEquatable<VariableRep>
    {
      private static int idgen;

      private readonly int id;

      public VariableRep()
      {
        this.id = ++idgen;
      }

      public override string ToString()
      {
        return "x" + this.id.ToString();
      }

      #region IEquatable<VariableRep> Members

      public bool Equals(VariableRep other)
      {
        return this.id == other.id;
      }

      #endregion
    }
    #endregion

    #region Constructor

    public BoxedExpressionEncoder(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      this.mdDecoder = mdDecoder;
    }
    #endregion

    #region Public methods implementing IExpressionEncoder

    public override void ResetFreshVariableCounter()
    {
      BoxedVariable<Variable>.ResetFreshVariableCounter();
    }

    public override BoxedVariable<Variable> FreshVariable<T>()
    {
      return new BoxedVariable<Variable>(true);
    }

    public override BoxedExpression ConstantFor(object value)
    {
      Contract.Assume(!(value is Rational));

      if (value is Int32)
      {
        return BoxedExpression.Const(value, mdDecoder.System_Int32, mdDecoder);
      }
      if (value is Int64)
      {
        long l = (long)value;
        if (l < Int32.MaxValue && l > Int32.MinValue)
        {
          int i = (int)l;
          return BoxedExpression.Const(i, mdDecoder.System_Int32, mdDecoder);
        }
        return BoxedExpression.Const(value, mdDecoder.System_Int64, mdDecoder);
      }
      if (value is Int16)
      {
        return BoxedExpression.Const(value, mdDecoder.System_Int16, mdDecoder);
      }
      if (value is SByte)
      {
        return BoxedExpression.Const(value, mdDecoder.System_Int8, mdDecoder);
      }
      if (value is bool)
      {
        return BoxedExpression.Const(value, mdDecoder.System_Boolean, mdDecoder);
      }
      // default: // Value and type must agree, otherwise we fail later.
      throw new NotImplementedException();
    }

    public override BoxedExpression VariableFor(BoxedVariable<Variable> v)
    {
      return BoxedExpression.Var(v);
    }

    public override BoxedExpression CompoundExpressionFor(ExpressionType type, ExpressionOperator op, BoxedExpression arg)
    {
      return BoxedExpression.Unary(ConvertToUnary(op), arg);
    }

    public override BoxedExpression CompoundExpressionFor(ExpressionType type, ExpressionOperator op, BoxedExpression left, BoxedExpression right)
    {
      return BoxedExpression.Binary(ConvertToBinary(op), left, right);
    }

    public override BoxedExpression Substitute(BoxedExpression source, BoxedExpression x, BoxedExpression exp)
    {
      return source.Substitute(x, exp);
    }

    #endregion

    #region Helpers
    internal static UnaryOperator ConvertToUnary(ExpressionOperator op)
    {
      switch (op)
      {
        case ExpressionOperator.ConvertToInt32: return UnaryOperator.Conv_i4;
        case ExpressionOperator.ConvertToUInt16: return UnaryOperator.Conv_u2;
        case ExpressionOperator.ConvertToUInt32: return UnaryOperator.Conv_u4;
        case ExpressionOperator.ConvertToUInt8: return UnaryOperator.Conv_u1;
        case ExpressionOperator.ConvertToFloat32: return UnaryOperator.Conv_r4;
        case ExpressionOperator.ConvertToFloat64: return UnaryOperator.Conv_r8;
        case ExpressionOperator.Not: return UnaryOperator.Not;
        case ExpressionOperator.UnaryMinus: return UnaryOperator.Neg;
        case ExpressionOperator.WritableBytes: return UnaryOperator.WritableBytes;
        default: throw new NotImplementedException();
      }

    }

    internal static BinaryOperator ConvertToBinary(ExpressionOperator op)
    {
      switch (op)
      {
        case ExpressionOperator.Addition: return BinaryOperator.Add;
        case ExpressionOperator.And: return BinaryOperator.And;
        case ExpressionOperator.Division: return BinaryOperator.Div;
        case ExpressionOperator.Equal: return BinaryOperator.Ceq;
        case ExpressionOperator.Equal_Obj: return BinaryOperator.Cobjeq;
        case ExpressionOperator.GreaterEqualThan: return BinaryOperator.Cge;
        case ExpressionOperator.GreaterEqualThan_Un: return BinaryOperator.Cge_Un;
        case ExpressionOperator.GreaterThan: return BinaryOperator.Cgt;
        case ExpressionOperator.GreaterThan_Un: return BinaryOperator.Cgt_Un;
        case ExpressionOperator.LessEqualThan: return BinaryOperator.Cle;
        case ExpressionOperator.LessEqualThan_Un: return BinaryOperator.Cle_Un;
        case ExpressionOperator.LessThan: return BinaryOperator.Clt;
        case ExpressionOperator.LessThan_Un: return BinaryOperator.Clt_Un;
        case ExpressionOperator.Modulus: return BinaryOperator.Rem;
        case ExpressionOperator.Multiplication: return BinaryOperator.Mul;
        case ExpressionOperator.Multiplication_Overflow: return BinaryOperator.Mul_Ovf;
        case ExpressionOperator.NotEqual: return BinaryOperator.Cne_Un;
        case ExpressionOperator.Or: return BinaryOperator.Or;
        case ExpressionOperator.ShiftLeft: return BinaryOperator.Shl;
        case ExpressionOperator.ShiftRight: return BinaryOperator.Shr;
        case ExpressionOperator.Subtraction: return BinaryOperator.Sub;
        case ExpressionOperator.Xor: return BinaryOperator.Xor;
        default: throw new NotImplementedException();
      }
    }
    #endregion

  }

}