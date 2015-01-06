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

// The interfaces for the pure expressions used internally by the abstract domains
// When instantiating the framework for an analysis, these are the interfaces that must implemented

using System.Collections.Generic;
using Microsoft.Research.CodeAnalysis;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains.Expressions
{
  #region The expressions and the types that are understood by the abstract domains

  /// <summary>
  /// The expressions that are recognized by the abstract domains.
  /// 
  /// Those will be replaced by BinaryOp and UnaryOp
  /// </summary>
  public enum ExpressionOperator
  {
    Constant, Variable,                                                         // Basic
    Not, And, Or, Xor,                                                          // Bitwise
    LogicalAnd, LogicalOr, LogicalNot,                                          // Booleans
    Equal, NotEqual,
    Equal_Obj, 
    LessThan, LessThan_Un,                                                      // Comparison
    LessEqualThan, LessEqualThan_Un,
    GreaterThan, GreaterThan_Un, 
    GreaterEqualThan, GreaterEqualThan_Un,
    Addition, Subtraction, Multiplication, Division, Modulus, UnaryMinus,       // Arithmetic
    Addition_Overflow, Subtraction_Overflow, Multiplication_Overflow,
    ShiftLeft, ShiftRight,                                                      // Shifting operations
    ConvertToInt8, ConvertToInt16, ConvertToInt32, ConvertToInt64,              // Conversions to int
    ConvertToUInt8, ConvertToUInt16, ConvertToUInt32, ConvertToUInt64,          // Conversions to Uint
    ConvertToFloat32, ConvertToFloat64,
    SizeOf,                                                                     // SizeOf
    WritableBytes,                                                              // The writable bytes of a pointer
    Unknown                                                                     // All the other cases  
    // TODO: Add type expressions as "cast", "is", "as"
  }

  public enum ExpressionType
  {
    Unknown, Int8, Int16, Int32, Int64, UInt8, UInt16, UInt32, UInt64, Bool, String, Float32, Float64
  }
  #endregion

  #region Interfaces for Expression decoders and encoders

  /// <summary>
  /// The decoder for instructions, used to let communicate the "syntax" with the abstract domains
  /// </summary>
  /// <typeparam name="Expression">The type of expressions that will be decoded </typeparam>
  [ContractClass(typeof(IExpressionDecoderContracts<,>))]
  public interface IExpressionDecoder<Variable, Expression> 
  {
    /// <returns>
    /// The operator for the expression <code>exp</code>
    /// </returns>
    [Pure]
    ExpressionOperator OperatorFor(Expression exp);

    /// <summary>
    /// The expression on the left for the <code>exp</code> object
    /// </summary>
    /// <param name="exp">The expression</param>
    /// <returns>The left expression, that must be not-null</returns>
    [Pure]
    Expression LeftExpressionFor(Expression exp);

    /// <summary>
    /// The expression on the right for the <code>exp</code> object.
    /// If there is no expression on the right, then an exception of type <code>AbstractInterpretationException</code> is thrown
    /// </summary>
    /// <param name="exp">The expression</param>
    /// <returns>The right expression, if any. Otherwise an exception is thrown</returns>
    [Pure]
    Expression RightExpressionFor(Expression exp);

    /// <summary>
    /// If <code>exp</code> is in the form "(conv_op)e1" then it returns e1.
    /// Otherwise it returns exp;
    /// </summary>
    [Pure]
    Expression Stripped(Expression exp);

    [Pure]
    IEnumerable<Expression> Disjunctions(Expression exp);

    /// <returns>
    /// <code>true</code> iff <code>exp</code> is null
    /// </returns>
    [Pure]
    bool IsNull(Expression exp);

    /// <summary>
    /// The type of the expression <code>exp</code> (e.g. Int32, Int16, UInt32, String...)
    /// </summary>
    [Pure]
    ExpressionType TypeOf(Expression exp);

    /// <summary>
    /// If the expression has the associated info, it is returned.
    /// </summary>
    /// <returns>true if success, false, if no information</returns>
    [Pure]
    bool TryGetAssociatedExpression(Expression exp, AssociatedInfo infoKind, out Expression info);

    /// <summary>
    /// If the expression has the associated info, it is returned.
    /// </summary>
    /// <param name="pc">Try to find information associated at given pc (e.g., for variables)</param>
    /// <returns>true if success, false, if no information</returns>
    [Pure]
    bool TryGetAssociatedExpression(APC pc, Expression exp, AssociatedInfo infoKind, out Expression info);

    [Pure]
    bool IsVariable(Expression exp);
    
    /// <summary>
    /// Slack Variables are the ones generated by some abstract domains like SubPolyhedra, also called beta or fresh variables
    /// </summary>
    /// <returns></returns>
    [Pure]
    bool IsSlackVariable(Variable v);

    /// <summary>
    /// A Variable managed from the underlying framework
    /// </summary>
    [Pure]
    bool IsFrameworkVariable(Variable v);

    /// <summary>
    /// Just an helper, as it is used so many times
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    [Pure]
    bool IsSlackOrFrameworkVariable(Variable v);

    [Pure]
    bool IsConstant(Expression exp);

    [Pure]
    bool IsConstantInt(Expression exp, out int val);

    [Pure]
    bool IsNaN(Expression exp);

    [Pure]
    bool IsWritableBytes(Expression exp);

    [Pure]
    bool IsUnaryExpression(Expression exp);

    [Pure]
    bool IsBinaryExpression(Expression exp);

    [Pure]
    bool IsSizeOf(Expression exp);

    [Pure]
    Variable UnderlyingVariable(Expression exp);

    /// <summary>
    /// If expression is a constant, this gets the underlying representation of the constant
    /// </summary>
    [Pure]
    object Constant(Expression exp);

    [Pure]
    bool TrySizeOf(Expression type, out int value);

    /// <summary>
    /// The set of variables defined in the expression <code>exp</code>
    /// </summary>
    [Pure]
    Set<Variable> VariablesIn(Expression exp);

    /// <summary>
    /// The value of the expression. 
    /// It makes sense only if <code>exp</code> is a constant
    /// </summary>
    /// <param name="aiType">The type that is expected, in the abstract interpretation world</param>
    /// <typeparam name="T">The value of the constant depends on its type. So before of invoking <code>ValueOf</code> it is important to determine its type</typeparam>
    [Pure]
    bool TryValueOf<T>(Expression exp, ExpressionType aiType, out T value);     

    /// <summary>
    /// The name of the variable.
    /// It makes sense only if <code>exp</code> is a variable
    /// </summary>
    [Pure]
    string NameOf(Variable exp);

  }

  /// <summary>
  /// The encoder for instructions: given an abstract interpretation expression (whatever it means) it builds an Expression
  /// </summary>
  /// <typeparam name="TargetExpression">The target type for expressions </typeparam>
  [ContractClass(typeof(IExpressionEncoderContracts<,>))]
  public interface IExpressionEncoder<Variable, TargetExpression>
  {
    // F: This is for debugging only 
    void ResetFreshVariableCounter();
    
    /// <summary>
    /// Obtain a fresh variable of C# type <code>T</code>
    /// </summary>
    [Pure]
    Variable FreshVariable<T>();

    [Pure]
    TargetExpression VariableFor(Variable v);

    [Pure]
    TargetExpression ConstantFor(object value);

    /// <returns>
    /// The expression in <code>TargetExpression</code> that corresponds to <code>left</code>
    /// </returns>
    [Pure]
    TargetExpression CompoundExpressionFor(ExpressionType type, ExpressionOperator op, TargetExpression left);

    /// <returns>
    /// The expression in <code>TargerExpression</code> that corresponds to <code>op(left, right)</code>
    /// </returns>
    [Pure]
    TargetExpression CompoundExpressionFor(ExpressionType type, ExpressionOperator op, TargetExpression left, TargetExpression right);

    /// <summary>
    /// Substitute all the occurrences of <code>x</code> in <code>source</code> with the expression <code>exp</code>.
    /// <code>x</code> must be a variable
    /// </summary>
    [Pure]
    TargetExpression Substitute(TargetExpression source, TargetExpression x, TargetExpression exp);
    }

  #endregion

  #region Contracts
  [ContractClassFor(typeof(IExpressionDecoder<,>))]
  abstract class IExpressionDecoderContracts<Variable, Expression>
    : IExpressionDecoder<Variable, Expression>
  {
    #region IExpressionDecoder<Variable,Expression> Members

    ExpressionOperator IExpressionDecoder<Variable, Expression>.OperatorFor(Expression exp)
    {
      //Contract.Requires(exp != null);

      throw new System.NotImplementedException();
    }

    Expression IExpressionDecoder<Variable, Expression>.LeftExpressionFor(Expression exp)
    {
      //Contract.Requires(exp != null);

      Contract.Ensures(Contract.Result<Expression>() != null);
      

      throw new System.NotImplementedException();
    }

    Expression IExpressionDecoder<Variable, Expression>.RightExpressionFor(Expression exp)
    {
      //Contract.Requires(exp != null);

      Contract.Ensures(Contract.Result<Expression>() != null);

      throw new System.NotImplementedException();
    }

    Expression IExpressionDecoder<Variable, Expression>.Stripped(Expression exp)
    {
      //Contract.Requires(exp != null);

      Contract.Ensures(Contract.Result<Expression>() != null);

      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsNull(Expression exp)
    {
      //Contract.Requires(exp != null);

      throw new System.NotImplementedException();
    }

    ExpressionType IExpressionDecoder<Variable, Expression>.TypeOf(Expression exp)
    {
      //Contract.Requires(exp != null);

      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.TryGetAssociatedExpression(Expression exp, AssociatedInfo infoKind, out Expression info)
    {
      //Contract.Requires(exp != null);

      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.TryGetAssociatedExpression(APC pc, Expression exp, AssociatedInfo infoKind, out Expression info)
    {
      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsVariable(Expression exp)
    {
      //Contract.Requires(exp != null);

      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsSlackVariable(Variable v)
    {
      //Contract.Requires(v != null);

      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsFrameworkVariable(Variable v)
    {
      //Contract.Requires(v != null);

      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsSlackOrFrameworkVariable(Variable v)
    {
      //Contract.Requires(v != null);

      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsConstant(Expression exp)
    {
      //Contract.Requires(exp != null);

      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsConstantInt(Expression exp, out int val)
    {
      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsNaN(Expression exp)
    {
      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsWritableBytes(Expression exp)
    {
      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsUnaryExpression(Expression exp)
    {
      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsBinaryExpression(Expression exp)
    {
      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.IsSizeOf(Expression exp)
    {
      throw new System.NotImplementedException();
    }

    Variable IExpressionDecoder<Variable, Expression>.UnderlyingVariable(Expression exp)
    {
      throw new System.NotImplementedException();
    }

    object IExpressionDecoder<Variable, Expression>.Constant(Expression exp)
    {
      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.TrySizeOf(Expression type, out int value)
    {
      throw new System.NotImplementedException();
    }

    Set<Variable> IExpressionDecoder<Variable, Expression>.VariablesIn(Expression exp)
    {
      Contract.Ensures(Contract.Result<Set<Variable>>() != null);

      throw new System.NotImplementedException();
    }

    bool IExpressionDecoder<Variable, Expression>.TryValueOf<T>(Expression exp, ExpressionType aiType, out T value)
    {
      throw new System.NotImplementedException();
    }

    string IExpressionDecoder<Variable, Expression>.NameOf(Variable exp)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      
      throw new System.NotImplementedException();
    }

    #endregion


    public IEnumerable<Expression> Disjunctions(Expression exp)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Expression>>() != null);
      throw new System.NotImplementedException();
    }
  }

  [ContractClassFor(typeof(IExpressionEncoder<,>))]
  abstract class IExpressionEncoderContracts<Variable, Expression>
    : IExpressionEncoder<Variable, Expression>
  {
    #region IExpressionEncoder<Variable,Expression> Members

    void IExpressionEncoder<Variable, Expression>.ResetFreshVariableCounter()
    {
      throw new System.NotImplementedException();
    }

    Variable IExpressionEncoder<Variable, Expression>.FreshVariable<T>()
    {
      Contract.Ensures(Contract.Result<Variable>() != null);
      

      throw new System.NotImplementedException();
    }

    Expression IExpressionEncoder<Variable, Expression>.VariableFor(Variable v)
    {
      Contract.Ensures(Contract.Result<Expression>() != null);      

      throw new System.NotImplementedException();
    }

    Expression IExpressionEncoder<Variable, Expression>.ConstantFor(object value)
    {
      Contract.Requires(value != null);
      Contract.Ensures(Contract.Result<Expression>() != null);      

      throw new System.NotImplementedException();
    }

    Expression IExpressionEncoder<Variable, Expression>.CompoundExpressionFor(ExpressionType type, ExpressionOperator op, Expression left)
    {
      Contract.Ensures(Contract.Result<Expression>() != null);
      

      throw new System.NotImplementedException();
    }

    Expression IExpressionEncoder<Variable, Expression>.CompoundExpressionFor(ExpressionType type, ExpressionOperator op, Expression left, Expression right)
    {
      Contract.Ensures(Contract.Result<Expression>() != null);      

      throw new System.NotImplementedException();
    }

    Expression IExpressionEncoder<Variable, Expression>.Substitute(Expression source, Expression x, Expression exp)
    {
      Contract.Ensures(Contract.Result<Expression>() != null);      

      throw new System.NotImplementedException();
    }

    #endregion
  }
  #endregion

  #region ExpressionManager
  
  [ContractVerification(true)]
  public class ExpressionManager<Variable, Expression>
  {
    #region Object Invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.decoder != null);
      Contract.Invariant(this.TimeOut != null);
    }

    #endregion

    #region State

    public TimeOutChecker TimeOut { private set; get; }
    
    readonly private IExpressionDecoder<Variable, Expression> decoder;
    readonly protected IExpressionEncoder<Variable, Expression> encoder; // can be null

    public IExpressionDecoder<Variable, Expression> Decoder { get { Contract.Ensures(Contract.Result<IExpressionDecoder<Variable, Expression>>() != null); return this.decoder; } }

    public Logger Log;

    #endregion

    #region Constructor

    public ExpressionManager(TimeOutChecker timeout, IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder = null, Logger log = null)
    {
      Contract.Requires(decoder != null);

      Contract.Ensures(this.decoder == decoder);
      Contract.Ensures(this.encoder == encoder);
      Contract.Ensures(this.TimeOut != null);

      this.TimeOut = timeout?? DFARoot.TimeOut;

      Contract.Assume(this.TimeOut != null, "weakeness with ?? ?");

      this.decoder = decoder;
      this.encoder = encoder;
      this.Log = log == null? VoidLogger.Log : log;
    }

    #endregion

    #region Try

    public bool TryGetEncoder(out IExpressionEncoder<Variable, Expression> encoder)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out encoder) != null);

      encoder = this.encoder;

      return encoder != null;
    }
    
    #endregion
  }

  /// <summary>
  /// Instance of this class are sure that the Encoder != null
  /// </summary>
  [ContractVerification(true)]
  public class ExpressionManagerWithEncoder<Variable, Expression>
    : ExpressionManager<Variable, Expression>
  {

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.encoder != null);
    }

    public ExpressionManagerWithEncoder(TimeOutChecker timeout, IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder, Logger log = null)
       : base(timeout, decoder, encoder)
    {
      Contract.Requires(timeout != null);
      Contract.Requires(decoder != null);
      Contract.Requires(encoder != null);
    }

    public IExpressionEncoder<Variable, Expression> Encoder { get { Contract.Ensures(Contract.Result<IExpressionEncoder<Variable, Expression>>() != null); return this.encoder; } }

  }

  #endregion
}
