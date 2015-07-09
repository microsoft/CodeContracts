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
using System.Reflection;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Linq.Expressions
{
  // Summary:
  //     Describes the node types for the nodes of an expression tree.
  public enum ExpressionType
  {
    // Summary:
    //     A node that represents arithmetic addition without overflow checking.
    Add = 0,
    //
    // Summary:
    //     A node that represents arithmetic addition with overflow checking.
    AddChecked = 1,
    //
    // Summary:
    //     A node that represents a bitwise AND operation.
    And = 2,
    //
    // Summary:
    //     A node that represents a short-circuiting conditional AND operation.
    AndAlso = 3,
    //
    // Summary:
    //     A node that represents getting the length of a one-dimensional array.
    ArrayLength = 4,
    //
    // Summary:
    //     A node that represents indexing into a one-dimensional array.
    ArrayIndex = 5,
    //
    // Summary:
    //     A node that represents a method call.
    Call = 6,
    //
    // Summary:
    //     A node that represents a null coalescing operation.
    Coalesce = 7,
    //
    // Summary:
    //     A node that represents a conditional operation.
    Conditional = 8,
    //
    // Summary:
    //     A node that represents an expression that has a constant value.
    Constant = 9,
    //
    // Summary:
    //     A node that represents a cast or conversion operation. If the operation is
    //     a numeric conversion, it overflows silently if the converted value does not
    //     fit the target type.
    Convert = 10,
    //
    // Summary:
    //     A node that represents a cast or conversion operation. If the operation is
    //     a numeric conversion, an exception is thrown if the converted value does
    //     not fit the target type.
    ConvertChecked = 11,
    //
    // Summary:
    //     A node that represents arithmetic division.
    Divide = 12,
    //
    // Summary:
    //     A node that represents an equality comparison.
    Equal = 13,
    //
    // Summary:
    //     A node that represents a bitwise XOR operation.
    ExclusiveOr = 14,
    //
    // Summary:
    //     A node that represents a "greater than" numeric comparison.
    GreaterThan = 15,
    //
    // Summary:
    //     A node that represents a "greater than or equal" numeric comparison.
    GreaterThanOrEqual = 16,
    //
    // Summary:
    //     A node that represents applying a delegate or lambda expression to a list
    //     of argument expressions.
    Invoke = 17,
    //
    // Summary:
    //     A node that represents a lambda expression.
    Lambda = 18,
    //
    // Summary:
    //     A node that represents a bitwise left-shift operation.
    LeftShift = 19,
    //
    // Summary:
    //     A node that represents a "less than" numeric comparison.
    LessThan = 20,
    //
    // Summary:
    //     A node that represents a "less than or equal" numeric comparison.
    LessThanOrEqual = 21,
    //
    // Summary:
    //     A node that represents creating a new System.Collections.IEnumerable object
    //     and initializing it from a list of elements.
    ListInit = 22,
    //
    // Summary:
    //     A node that represents reading from a field or property.
    MemberAccess = 23,
    //
    // Summary:
    //     A node that represents creating a new object and initializing one or more
    //     of its members.
    MemberInit = 24,
    //
    // Summary:
    //     A node that represents an arithmetic remainder operation.
    Modulo = 25,
    //
    // Summary:
    //     A node that represents arithmetic multiplication without overflow checking.
    Multiply = 26,
    //
    // Summary:
    //     A node that represents arithmetic multiplication with overflow checking.
    MultiplyChecked = 27,
    //
    // Summary:
    //     A node that represents an arithmetic negation operation.
    Negate = 28,
    //
    // Summary:
    //     A node that represents a unary plus operation. The result of a predefined
    //     unary plus operation is simply the value of the operand, but user-defined
    //     implementations may have non-trivial results.
    UnaryPlus = 29,
    //
    // Summary:
    //     A node that represents an arithmetic negation operation that has overflow
    //     checking.
    NegateChecked = 30,
    //
    // Summary:
    //     A node that represents calling a constructor to create a new object.
    New = 31,
    //
    // Summary:
    //     A node that represents creating a new one-dimensional array and initializing
    //     it from a list of elements.
    NewArrayInit = 32,
    //
    // Summary:
    //     A node that represents creating a new array where the bounds for each dimension
    //     are specified.
    NewArrayBounds = 33,
    //
    // Summary:
    //     A node that represents a bitwise complement operation.
    Not = 34,
    //
    // Summary:
    //     A node that represents an inequality comparison.
    NotEqual = 35,
    //
    // Summary:
    //     A node that represents a bitwise OR operation.
    Or = 36,
    //
    // Summary:
    //     A node that represents a short-circuiting conditional OR operation.
    OrElse = 37,
    //
    // Summary:
    //     A node that represents a reference to a parameter defined in the context
    //     of the expression.
    Parameter = 38,
    //
    // Summary:
    //     A node that represents raising a number to a power.
    Power = 39,
    //
    // Summary:
    //     A node that represents an expression that has a constant value of type System.Linq.Expressions.Expression.
    //     A System.Linq.Expressions.ExpressionType.Quote node can contain references
    //     to parameters defined in the context of the expression it represents.
    Quote = 40,
    //
    // Summary:
    //     A node that represents a bitwise right-shift operation.
    RightShift = 41,
    //
    // Summary:
    //     A node that represents arithmetic subtraction without overflow checking.
    Subtract = 42,
    //
    // Summary:
    //     A node that represents arithmetic subtraction with overflow checking.
    SubtractChecked = 43,
    //
    // Summary:
    //     A node that represents an explicit reference or boxing conversion where null
    //     is supplied if the conversion fails.
    TypeAs = 44,
    //
    // Summary:
    //     A node that represents a type test.
    TypeIs = 45,
  }

  public abstract class Expression
  {
    protected Expression() { }

    // Summary:
    //     Gets the node type of this System.Linq.Expressions.Expression.
    //
    // Returns:
    //     One of the System.Linq.Expressions.ExpressionType values.
#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    virtual
#endif
    extern public ExpressionType NodeType { get; }

    //
    // Summary:
    //     Gets the static type of the expression that this System.Linq.Expressions.Expression
    //     represents.
    //
    // Returns:
    //     The System.Type that represents the static type of the expression.
#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    virtual
#endif
    public Type Type
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);
        return default(Type);
      }
    }

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    protected internal virtual new System.Linq.Expressions.Expression Accept(ExpressionVisitor visitor)
    {
      return default(System.Linq.Expressions.Expression);
    }
#endif


    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     addition operation that does not have overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Add and the System.Linq.Expressions.BinaryExpression.Left
    //     and System.Linq.Expressions.BinaryExpression.Right properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The addition operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression Add(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }

    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     addition operation that does not have overflow checking. The implementing
    //     method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Add and the System.Linq.Expressions.BinaryExpression.Left,
    //     System.Linq.Expressions.BinaryExpression.Right and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the addition operator is not defined for left.Type and
    //     right.Type.
    [Pure]
    public static BinaryExpression Add(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }

#if !SILVERLIGHT_3_0 && !NETFRAMEWORK_3_5
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an addition
    //     assignment operation that does not have overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.AddAssign and the
    //     System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    public static BinaryExpression AddAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }

    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an addition
    //     assignment operation that does not have overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.AddAssign and the
    //     System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    public static BinaryExpression AddAssign(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an addition
    //     assignment operation that does not have overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    //   conversion:
    //     A System.Linq.Expressions.LambdaExpression to set the System.Linq.Expressions.BinaryExpression.Conversion
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.AddAssign and the
    //     System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     System.Linq.Expressions.BinaryExpression.Method, and System.Linq.Expressions.BinaryExpression.Conversion
    //     properties set to the specified values.
    public static BinaryExpression AddAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an addition
    //     assignment operation that has overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.AddAssignChecked
    //     and the System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    public static BinaryExpression AddAssignChecked(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an addition
    //     assignment operation that has overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.AddAssignChecked
    //     and the System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    public static BinaryExpression AddAssignChecked(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }

    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an addition
    //     assignment operation that has overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    //   conversion:
    //     A System.Linq.Expressions.LambdaExpression to set the System.Linq.Expressions.BinaryExpression.Conversion
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.AddAssignChecked
    //     and the System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     System.Linq.Expressions.BinaryExpression.Method, and System.Linq.Expressions.BinaryExpression.Conversion
    //     properties set to the specified values.
    public static BinaryExpression AddAssignChecked(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
#endif

    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     addition operation that has overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.AddChecked and the
    //     System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The addition operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression AddChecked(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     addition operation that has overflow checking. The implementing method can
    //     be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.AddChecked and the
    //     System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the addition operator is not defined for left.Type and
    //     right.Type.
    [Pure]
    public static BinaryExpression AddChecked(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }

    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a bitwise
    //     AND operation.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.And and the System.Linq.Expressions.BinaryExpression.Left
    //     and System.Linq.Expressions.BinaryExpression.Right properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The bitwise AND operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression And(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a bitwise
    //     AND operation. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.And and the System.Linq.Expressions.BinaryExpression.Left,
    //     System.Linq.Expressions.BinaryExpression.Right, and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the bitwise AND operator is not defined for left.Type
    //     and right.Type.
    [Pure]
    public static BinaryExpression And(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a conditional
    //     AND operation that evaluates the second operand only if it has to.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.AndAlso and the
    //     System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The bitwise AND operator is not defined for left.Type and right.Type.  -or-
    //     left.Type and right.Type are not the same Boolean type.
    [Pure]
    public static BinaryExpression AndAlso(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a conditional
    //     AND operation that evaluates the second operand only if it has to. The implementing
    //     method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.AndAlso and the
    //     System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the bitwise AND operator is not defined for left.Type
    //     and right.Type.  -or- method is null and left.Type and right.Type are not
    //     the same Boolean type.
    [Pure]
    public static BinaryExpression AndAlso(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents applying
    //     an array index operator to an array of rank one.
    //
    // Parameters:
    //   array:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   index:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ArrayIndex and the
    //     System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array or index is null.
    //
    //   System.ArgumentException:
    //     array.Type does not represent an array type.  -or- array.Type represents
    //     an array type whose rank is not 1.  -or- index.Type does not represent the
    //     System.Int32 type.
    [Pure]
    public static BinaryExpression ArrayIndex(Expression array, Expression index)
    {
      Contract.Requires(array != null);
      Contract.Requires(index != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MethodCallExpression that represents applying
    //     an array index operator to an array of rank more than one.
    //
    // Parameters:
    //   array:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.MethodCallExpression.Object
    //     property equal to.
    //
    //   indexes:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.Expression
    //     objects to use to populate the System.Linq.Expressions.MethodCallExpression.Arguments
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MethodCallExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Call and the System.Linq.Expressions.MethodCallExpression.Object
    //     and System.Linq.Expressions.MethodCallExpression.Arguments properties set
    //     to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array or indexes is null.
    //
    //   System.ArgumentException:
    //     array.Type does not represent an array type.  -or- The rank of array.Type
    //     does not match the number of elements in indexes.  -or- The System.Linq.Expressions.Expression.Type
    //     property of one or more elements of indexes does not represent the System.Int32
    //     type.
    [Pure]
    public static MethodCallExpression ArrayIndex(Expression array, IEnumerable<Expression> indexes)
    {
      Contract.Requires(array != null);
      Contract.Requires(indexes != null);
      Contract.Ensures(Contract.Result<MethodCallExpression>() != null);
      return default(MethodCallExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MethodCallExpression that represents applying
    //     an array index operator to an array of rank more than one.
    //
    // Parameters:
    //   array:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.MethodCallExpression.Object
    //     property equal to.
    //
    //   indexes:
    //     An array of System.Linq.Expressions.Expression objects to use to populate
    //     the System.Linq.Expressions.MethodCallExpression.Arguments collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MethodCallExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Call and the System.Linq.Expressions.MethodCallExpression.Object
    //     and System.Linq.Expressions.MethodCallExpression.Arguments properties set
    //     to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array or indexes is null.
    //
    //   System.ArgumentException:
    //     array.Type does not represent an array type.  -or- The rank of array.Type
    //     does not match the number of elements in indexes.  -or- The System.Linq.Expressions.Expression.Type
    //     property of one or more elements of indexes does not represent the System.Int32
    //     type.
    [Pure]
    public static MethodCallExpression ArrayIndex(Expression array, params Expression[] indexes)
    {
      Contract.Requires(array != null);
      Contract.Requires(indexes != null);
      Contract.Ensures(Contract.Result<MethodCallExpression>() != null);
      return default(MethodCallExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents getting
    //     the length of a one-dimensional array.
    //
    // Parameters:
    //   array:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ArrayLength and
    //     the System.Linq.Expressions.UnaryExpression.Operand property equal to array.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.
    //
    //   System.ArgumentException:
    //     array.Type does not represent an array type.
    [Pure]
    public static UnaryExpression ArrayLength(Expression array)
    {
      Contract.Requires(array != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberAssignment that represents the initialization
    //     of a field or property.
    //
    // Parameters:
    //   member:
    //     A System.Reflection.MemberInfo to set the System.Linq.Expressions.MemberBinding.Member
    //     property equal to.
    //
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.MemberAssignment.Expression
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberAssignment that has System.Linq.Expressions.MemberBinding.BindingType
    //     equal to System.Linq.Expressions.MemberBindingType.Assignment and the System.Linq.Expressions.MemberBinding.Member
    //     and System.Linq.Expressions.MemberAssignment.Expression properties set to
    //     the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     member or expression is null.
    //
    //   System.ArgumentException:
    //     member does not represent a field or property.  -or- The property represented
    //     by member does not have a set accessor.  -or- expression.Type is not assignable
    //     to the type of the field or property that member represents.
    [Pure]
    public static MemberAssignment Bind(MemberInfo member, Expression expression)
    {
      Contract.Requires(member != null);
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<MemberAssignment>() != null);
      return default(MemberAssignment);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberAssignment that represents the initialization
    //     of a member by using a property accessor method.
    //
    // Parameters:
    //   propertyAccessor:
    //     A System.Reflection.MethodInfo that represents a property accessor method.
    //
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.MemberAssignment.Expression
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberAssignment that has the System.Linq.Expressions.MemberBinding.BindingType
    //     property equal to System.Linq.Expressions.MemberBindingType.Assignment, the
    //     System.Linq.Expressions.MemberBinding.Member property set to the System.Reflection.PropertyInfo
    //     that represents the property accessed in propertyAccessor, and the System.Linq.Expressions.MemberAssignment.Expression
    //     property set to expression.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     propertyAccessor or expression is null.
    //
    //   System.ArgumentException:
    //     propertyAccessor does not represent a property accessor method.  -or- The
    //     property accessed by propertyAccessor does not have a set accessor.  -or-
    //     expression.Type is not assignable to the type of the field or property that
    //     member represents.
    [Pure]
    public static MemberAssignment Bind(MethodInfo propertyAccessor, Expression expression)
    {
      Contract.Requires(propertyAccessor != null);
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<MemberAssignment>() != null);
      return default(MemberAssignment);
    }
#if  NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains the given
    //     expressions and has no variables.
    //
    // Parameters:
    //   expressions:
    //     The expressions in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(IEnumerable<Expression> expressions)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains the given
    //     expressions and has no variables.
    //
    // Parameters:
    //   expressions:
    //     The expressions in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(params Expression[] expressions)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains two expressions
    //     and has no variables.
    //
    // Parameters:
    //   arg0:
    //     The first expression in the block.
    //
    //   arg1:
    //     The second expression in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(Expression arg0, Expression arg1)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains the given
    //     variables and expressions.
    //
    // Parameters:
    //   variables:
    //     The variables in the block.
    //
    //   expressions:
    //     The expressions in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains the given
    //     variables and expressions.
    //
    // Parameters:
    //   variables:
    //     The variables in the block.
    //
    //   expressions:
    //     The expressions in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(IEnumerable<ParameterExpression> variables, params Expression[] expressions)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains the given
    //     expressions, has no variables and has specific result type.
    //
    // Parameters:
    //   type:
    //     The result type of the block.
    //
    //   expressions:
    //     The expressions in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(Type type, IEnumerable<Expression> expressions)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains the given
    //     expressions, has no variables and has specific result type.
    //
    // Parameters:
    //   type:
    //     The result type of the block.
    //
    //   expressions:
    //     The expressions in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(Type type, params Expression[] expressions)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains three expressions
    //     and has no variables.
    //
    // Parameters:
    //   arg0:
    //     The first expression in the block.
    //
    //   arg1:
    //     The second expression in the block.
    //
    //   arg2:
    //     The third expression in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains the given
    //     variables and expressions.
    //
    // Parameters:
    //   type:
    //     The result type of the block.
    //
    //   variables:
    //     The variables in the block.
    //
    //   expressions:
    //     The expressions in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(Type type, IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains the given
    //     variables and expressions.
    //
    // Parameters:
    //   type:
    //     The result type of the block.
    //
    //   variables:
    //     The variables in the block.
    //
    //   expressions:
    //     The expressions in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(Type type, IEnumerable<ParameterExpression> variables, params Expression[] expressions)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains four expressions
    //     and has no variables.
    //
    // Parameters:
    //   arg0:
    //     The first expression in the block.
    //
    //   arg1:
    //     The second expression in the block.
    //
    //   arg2:
    //     The third expression in the block.
    //
    //   arg3:
    //     The fourth expression in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2, Expression arg3)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BlockExpression that contains five expressions
    //     and has no variables.
    //
    // Parameters:
    //   arg0:
    //     The first expression in the block.
    //
    //   arg1:
    //     The second expression in the block.
    //
    //   arg2:
    //     The third expression in the block.
    //
    //   arg3:
    //     The fourth expression in the block.
    //
    //   arg4:
    //     The fifth expression in the block.
    //
    // Returns:
    //     The created System.Linq.Expressions.BlockExpression.
    public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
    {
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
#endif
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MethodCallExpression that represents a
    //     call to a method that takes no arguments.
    //
    // Parameters:
    //   instance:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.MethodCallExpression.Object
    //     property equal to (pass null for a static (Shared in Visual Basic) method).
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.MethodCallExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.MethodCallExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Call and the System.Linq.Expressions.MethodCallExpression.Object
    //     and System.Linq.Expressions.MethodCallExpression.Method properties set to
    //     the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     method is null.  -or- instance is null and method represents an instance
    //     method.
    //
    //   System.ArgumentException:
    //     instance.Type is not assignable to the declaring type of the method represented
    //     by method.
    [Pure]
    public static MethodCallExpression Call(Expression instance, MethodInfo method)
    {
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<MethodCallExpression>() != null);
      return default(MethodCallExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MethodCallExpression that represents a
    //     call to a static (Shared in Visual Basic) method.
    //
    // Parameters:
    //   method:
    //     A System.Reflection.MethodInfo that represents a static (Shared in Visual
    //     Basic) method to set the System.Linq.Expressions.MethodCallExpression.Method
    //     property equal to.
    //
    //   arguments:
    //     An array of System.Linq.Expressions.Expression objects to use to populate
    //     the System.Linq.Expressions.MethodCallExpression.Arguments collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MethodCallExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Call and the System.Linq.Expressions.MethodCallExpression.Method
    //     and System.Linq.Expressions.MethodCallExpression.Arguments properties set
    //     to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     method is null.
    //
    //   System.ArgumentException:
    //     The number of elements in arguments does not equal the number of parameters
    //     for the method represented by method.  -or- One or more of the elements of
    //     arguments is not assignable to the corresponding parameter for the method
    //     represented by method.
    [Pure]
    public static MethodCallExpression Call(MethodInfo method, params Expression[] arguments)
    {
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<MethodCallExpression>() != null);
      return default(MethodCallExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MethodCallExpression that represents a
    //     call to a method that takes arguments.
    //
    // Parameters:
    //   instance:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.MethodCallExpression.Object
    //     property equal to (pass null for a static (Shared in Visual Basic) method).
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.MethodCallExpression.Method
    //     property equal to.
    //
    //   arguments:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.Expression
    //     objects to use to populate the System.Linq.Expressions.MethodCallExpression.Arguments
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MethodCallExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Call and the System.Linq.Expressions.MethodCallExpression.Object,
    //     System.Linq.Expressions.MethodCallExpression.Method, and System.Linq.Expressions.MethodCallExpression.Arguments
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     method is null.  -or- instance is null and method represents an instance
    //     method.
    //
    //   System.ArgumentException:
    //     instance.Type is not assignable to the declaring type of the method represented
    //     by method.  -or- The number of elements in arguments does not equal the number
    //     of parameters for the method represented by method.  -or- One or more of
    //     the elements of arguments is not assignable to the corresponding parameter
    //     for the method represented by method.
    [Pure]
    public static MethodCallExpression Call(Expression instance, MethodInfo method, IEnumerable<Expression> arguments)
    {
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<MethodCallExpression>() != null);
      return default(MethodCallExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MethodCallExpression that represents a
    //     call to a method that takes arguments.
    //
    // Parameters:
    //   instance:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.MethodCallExpression.Object
    //     property equal to (pass null for a static (Shared in Visual Basic) method).
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.MethodCallExpression.Method
    //     property equal to.
    //
    //   arguments:
    //     An array of System.Linq.Expressions.Expression objects to use to populate
    //     the System.Linq.Expressions.MethodCallExpression.Arguments collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MethodCallExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Call and the System.Linq.Expressions.MethodCallExpression.Object,
    //     System.Linq.Expressions.MethodCallExpression.Method, and System.Linq.Expressions.MethodCallExpression.Arguments
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     method is null.  -or- instance is null and method represents an instance
    //     method.  -or- arguments is not null and one or more of its elements is null.
    //
    //   System.ArgumentException:
    //     instance.Type is not assignable to the declaring type of the method represented
    //     by method.  -or- The number of elements in arguments does not equal the number
    //     of parameters for the method represented by method.  -or- One or more of
    //     the elements of arguments is not assignable to the corresponding parameter
    //     for the method represented by method.
    [Pure]
    public static MethodCallExpression Call(Expression instance, MethodInfo method, params Expression[] arguments)
    {
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<MethodCallExpression>() != null);
      return default(MethodCallExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MethodCallExpression that represents a
    //     call to an instance method by calling the appropriate factory method.
    //
    // Parameters:
    //   instance:
    //     An System.Linq.Expressions.Expression whose System.Linq.Expressions.Expression.Type
    //     property value will be searched for a specific method.
    //
    //   methodName:
    //     The name of the method.
    //
    //   typeArguments:
    //     An array of System.Type objects that specify the type parameters of the method.
    //
    //   arguments:
    //     An array of System.Linq.Expressions.Expression objects that represents the
    //     arguments to the method.
    //
    // Returns:
    //     A System.Linq.Expressions.MethodCallExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Call, the System.Linq.Expressions.MethodCallExpression.Object
    //     property equal to instance, System.Linq.Expressions.MethodCallExpression.Method
    //     set to the System.Reflection.MethodInfo that represents the specified instance
    //     method, and System.Linq.Expressions.MethodCallExpression.Arguments set to
    //     the specified arguments.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     instance or methodName is null.
    //
    //   System.InvalidOperationException:
    //     No method whose name is methodName, whose type parameters match typeArguments,
    //     and whose parameter types match arguments is found in instance.Type or its
    //     base types.  -or- More than one method whose name is methodName, whose type
    //     parameters match typeArguments, and whose parameter types match arguments
    //     is found in instance.Type or its base types.
    [Pure]
    public static MethodCallExpression Call(Expression instance, string methodName, Type[] typeArguments, params Expression[] arguments)
    {
      Contract.Requires(methodName != null);
      Contract.Requires(instance != null);
      Contract.Ensures(Contract.Result<MethodCallExpression>() != null);
      return default(MethodCallExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MethodCallExpression that represents a
    //     call to a static (Shared in Visual Basic) method by calling the appropriate
    //     factory method.
    //
    // Parameters:
    //   type:
    //     The System.Type that specifies the type that contains the specified static
    //     (Shared in Visual Basic) method.
    //
    //   methodName:
    //     The name of the method.
    //
    //   typeArguments:
    //     An array of System.Type objects that specify the type parameters of the method.
    //
    //   arguments:
    //     An array of System.Linq.Expressions.Expression objects that represent the
    //     arguments to the method.
    //
    // Returns:
    //     A System.Linq.Expressions.MethodCallExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Call, the System.Linq.Expressions.MethodCallExpression.Method
    //     property set to the System.Reflection.MethodInfo that represents the specified
    //     static (Shared in Visual Basic) method, and the System.Linq.Expressions.MethodCallExpression.Arguments
    //     property set to the specified arguments.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type or methodName is null.
    //
    //   System.InvalidOperationException:
    //     No method whose name is methodName, whose type parameters match typeArguments,
    //     and whose parameter types match arguments is found in type or its base types.
    //      -or- More than one method whose name is methodName, whose type parameters
    //     match typeArguments, and whose parameter types match arguments is found in
    //     type or its base types.
    [Pure]
    public static MethodCallExpression Call(Type type, string methodName, Type[] typeArguments, params Expression[] arguments)
    {
      Contract.Requires(methodName != null);
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<MethodCallExpression>() != null);
      return default(MethodCallExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a coalescing
    //     operation.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Coalesce and the
    //     System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The System.Linq.Expressions.Expression.Type property of left does not represent
    //     a reference type or a nullable value type.
    //
    //   System.ArgumentException:
    //     left.Type and right.Type are not convertible to each other.
    [Pure]
    public static BinaryExpression Coalesce(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a coalescing
    //     operation, given a conversion function.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   conversion:
    //     A System.Linq.Expressions.LambdaExpression to set the System.Linq.Expressions.BinaryExpression.Conversion
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Coalesce and the
    //     System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right
    //     and System.Linq.Expressions.BinaryExpression.Conversion properties set to
    //     the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     left.Type and right.Type are not convertible to each other.  -or- conversion
    //     is not null and conversion.Type is a delegate type that does not take exactly
    //     one argument.
    //
    //   System.InvalidOperationException:
    //     The System.Linq.Expressions.Expression.Type property of left does not represent
    //     a reference type or a nullable value type.  -or- The System.Linq.Expressions.Expression.Type
    //     property of left represents a type that is not assignable to the parameter
    //     type of the delegate type conversion.Type.  -or- The System.Linq.Expressions.Expression.Type
    //     property of right is not equal to the return type of the delegate type conversion.Type.
    [Pure]
    public static BinaryExpression Coalesce(Expression left, Expression right, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }

    //
    // Summary:
    //     Creates a System.Linq.Expressions.ConditionalExpression.
    //
    // Parameters:
    //   test:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.ConditionalExpression.Test
    //     property equal to.
    //
    //   ifTrue:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.ConditionalExpression.IfTrue
    //     property equal to.
    //
    //   ifFalse:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.ConditionalExpression.IfFalse
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.ConditionalExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Conditional and
    //     the System.Linq.Expressions.ConditionalExpression.Test, System.Linq.Expressions.ConditionalExpression.IfTrue,
    //     and System.Linq.Expressions.ConditionalExpression.IfFalse properties set
    //     to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     test or ifTrue or ifFalse is null.
    //
    //   System.ArgumentException:
    //     test.Type is not System.Boolean.  -or- ifTrue.Type is not equal to ifFalse.Type.
    [Pure]
    public static ConditionalExpression Condition(Expression test, Expression ifTrue, Expression ifFalse)
    {
      Contract.Requires(test != null);
      Contract.Requires(ifTrue != null);
      Contract.Requires(ifFalse != null);
      Contract.Ensures(Contract.Result<ConditionalExpression>() != null);
      return default(ConditionalExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.ConstantExpression that has the System.Linq.Expressions.ConstantExpression.Value
    //     property set to the specified value.
    //
    // Parameters:
    //   value:
    //     An System.Object to set the System.Linq.Expressions.ConstantExpression.Value
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.ConstantExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Constant and the
    //     System.Linq.Expressions.ConstantExpression.Value property set to the specified
    //     value.
    [Pure]
    public static ConstantExpression Constant(object value)
    {
      Contract.Ensures(Contract.Result<ConstantExpression>() != null);
      return default(ConstantExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.ConstantExpression that has the System.Linq.Expressions.ConstantExpression.Value
    //     and System.Linq.Expressions.Expression.Type properties set to the specified
    //     values.
    //
    // Parameters:
    //   value:
    //     An System.Object to set the System.Linq.Expressions.ConstantExpression.Value
    //     property equal to.
    //
    //   type:
    //     A System.Type to set the System.Linq.Expressions.Expression.Type property
    //     equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.ConstantExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Constant and the
    //     System.Linq.Expressions.ConstantExpression.Value and System.Linq.Expressions.Expression.Type
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type is null.
    //
    //   System.ArgumentException:
    //     value is not null and type is not assignable from the dynamic type of value.
    [Pure]
    public static ConstantExpression Constant(object value, Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<ConstantExpression>() != null);
      return default(ConstantExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents a conversion
    //     operation.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    //   type:
    //     A System.Type to set the System.Linq.Expressions.Expression.Type property
    //     equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Convert and the
    //     System.Linq.Expressions.UnaryExpression.Operand and System.Linq.Expressions.Expression.Type
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression or type is null.
    //
    //   System.InvalidOperationException:
    //     No conversion operator is defined between expression.Type and type.
    [Pure]
    public static UnaryExpression Convert(Expression expression, Type type)
    {
      Contract.Requires(expression != null);
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents a conversion
    //     operation for which the implementing method is specified.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    //   type:
    //     A System.Type to set the System.Linq.Expressions.Expression.Type property
    //     equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.UnaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Convert and the
    //     System.Linq.Expressions.UnaryExpression.Operand, System.Linq.Expressions.Expression.Type,
    //     and System.Linq.Expressions.UnaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression or type is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly one argument.
    //
    //   System.Reflection.AmbiguousMatchException:
    //     More than one method that matches the method description was found.
    //
    //   System.InvalidOperationException:
    //     No conversion operator is defined between expression.Type and type.  -or-
    //     expression.Type is not assignable to the argument type of the method represented
    //     by method.  -or- The return type of the method represented by method is not
    //     assignable to type.  -or- expression.Type or type is a nullable value type
    //     and the corresponding non-nullable value type does not equal the argument
    //     type or the return type, respectively, of the method represented by method.
    [Pure]
    public static UnaryExpression Convert(Expression expression, Type type, MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents a conversion
    //     operation that throws an exception if the target type is overflowed.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    //   type:
    //     A System.Type to set the System.Linq.Expressions.Expression.Type property
    //     equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ConvertChecked and
    //     the System.Linq.Expressions.UnaryExpression.Operand and System.Linq.Expressions.Expression.Type
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression or type is null.
    //
    //   System.InvalidOperationException:
    //     No conversion operator is defined between expression.Type and type.
    [Pure]
    public static UnaryExpression ConvertChecked(Expression expression, Type type)
    {
      Contract.Requires(expression != null);
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents a conversion
    //     operation that throws an exception if the target type is overflowed and for
    //     which the implementing method is specified.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    //   type:
    //     A System.Type to set the System.Linq.Expressions.Expression.Type property
    //     equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.UnaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ConvertChecked and
    //     the System.Linq.Expressions.UnaryExpression.Operand, System.Linq.Expressions.Expression.Type,
    //     and System.Linq.Expressions.UnaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression or type is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly one argument.
    //
    //   System.Reflection.AmbiguousMatchException:
    //     More than one method that matches the method description was found.
    //
    //   System.InvalidOperationException:
    //     No conversion operator is defined between expression.Type and type.  -or-
    //     expression.Type is not assignable to the argument type of the method represented
    //     by method.  -or- The return type of the method represented by method is not
    //     assignable to type.  -or- expression.Type or type is a nullable value type
    //     and the corresponding non-nullable value type does not equal the argument
    //     type or the return type, respectively, of the method represented by method.
    [Pure]
    public static UnaryExpression ConvertChecked(Expression expression, Type type, MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     division operation.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Divide and the System.Linq.Expressions.BinaryExpression.Left
    //     and System.Linq.Expressions.BinaryExpression.Right properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The division operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression Divide(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     division operation. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Divide and the System.Linq.Expressions.BinaryExpression.Left,
    //     System.Linq.Expressions.BinaryExpression.Right, and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the division operator is not defined for left.Type and
    //     right.Type.
    [Pure]
    public static BinaryExpression Divide(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates an System.Linq.Expressions.ElementInit, given an System.Collections.Generic.IEnumerable<T>
    //     as the second argument.
    //
    // Parameters:
    //   addMethod:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.ElementInit.AddMethod
    //     property equal to.
    //
    //   arguments:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.Expression
    //     objects to set the System.Linq.Expressions.ElementInit.Arguments property
    //     equal to.
    //
    // Returns:
    //     An System.Linq.Expressions.ElementInit that has the System.Linq.Expressions.ElementInit.AddMethod
    //     and System.Linq.Expressions.ElementInit.Arguments properties set to the specified
    //     values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     addMethod or arguments is null.
    //
    //   System.ArgumentException:
    //     The method that addMethod represents is not named "Add" (case insensitive).
    //      -or- The method that addMethod represents is not an instance method.  -or-
    //     arguments does not contain the same number of elements as the number of parameters
    //     for the method that addMethod represents.  -or- The System.Linq.Expressions.Expression.Type
    //     property of one or more elements of arguments is not assignable to the type
    //     of the corresponding parameter of the method that addMethod represents.
    [Pure]
    public static ElementInit ElementInit(MethodInfo addMethod, IEnumerable<Expression> arguments)
    {
      Contract.Requires(addMethod != null);
      Contract.Requires(arguments != null);
      Contract.Ensures(Contract.Result<ElementInit>() != null);
      return default(ElementInit);
    }
    //
    // Summary:
    //     Creates an System.Linq.Expressions.ElementInit, given an array of values
    //     as the second argument.
    //
    // Parameters:
    //   addMethod:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.ElementInit.AddMethod
    //     property equal to.
    //
    //   arguments:
    //     An array of System.Linq.Expressions.Expression objects to set the System.Linq.Expressions.ElementInit.Arguments
    //     property equal to.
    //
    // Returns:
    //     An System.Linq.Expressions.ElementInit that has the System.Linq.Expressions.ElementInit.AddMethod
    //     and System.Linq.Expressions.ElementInit.Arguments properties set to the specified
    //     values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     addMethod or arguments is null.
    //
    //   System.ArgumentException:
    //     The method that addMethod represents is not named "Add" (case insensitive).
    //      -or- The method that addMethod represents is not an instance method.  -or-
    //     arguments does not contain the same number of elements as the number of parameters
    //     for the method that addMethod represents.  -or- The System.Linq.Expressions.Expression.Type
    //     property of one or more elements of arguments is not assignable to the type
    //     of the corresponding parameter of the method that addMethod represents.
    [Pure]
    public static ElementInit ElementInit(MethodInfo addMethod, params Expression[] arguments)
    {
      Contract.Requires(addMethod != null);
      Contract.Requires(arguments != null);
      Contract.Ensures(Contract.Result<ElementInit>() != null);
      return default(ElementInit);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an equality
    //     comparison.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Equal and the System.Linq.Expressions.BinaryExpression.Left
    //     and System.Linq.Expressions.BinaryExpression.Right properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The equality operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression Equal(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an equality
    //     comparison. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   liftToNull:
    //     true to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to true;
    //     false to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to false.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Equal and the System.Linq.Expressions.BinaryExpression.Left,
    //     System.Linq.Expressions.BinaryExpression.Right, System.Linq.Expressions.BinaryExpression.IsLiftedToNull,
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the equality operator is not defined for left.Type and
    //     right.Type.
    [Pure]
    public static BinaryExpression Equal(Expression left, Expression right, bool liftToNull, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a bitwise
    //     XOR operation.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ExclusiveOr and
    //     the System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The XOR operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression ExclusiveOr(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a bitwise
    //     XOR operation. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ExclusiveOr and
    //     the System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the XOR operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression ExclusiveOr(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberExpression that represents accessing
    //     a field.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.MemberExpression.Expression
    //     property equal to.
    //
    //   field:
    //     The System.Reflection.FieldInfo to set the System.Linq.Expressions.MemberExpression.Member
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.MemberAccess and
    //     the System.Linq.Expressions.MemberExpression.Expression and System.Linq.Expressions.MemberExpression.Member
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     field is null.  -or- The field represented by field is not static (Shared
    //     in Visual Basic) and expression is null.
    //
    //   System.ArgumentException:
    //     expression.Type is not assignable to the declaring type of the field represented
    //     by field.
    [Pure]
    public static MemberExpression Field(Expression expression, FieldInfo field)
    {
      Contract.Requires(field != null);
      Contract.Ensures(Contract.Result<MemberExpression>() != null);
      return default(MemberExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberExpression that represents accessing
    //     a field given the name of the field.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression whose System.Linq.Expressions.Expression.Type
    //     contains a field named fieldName.
    //
    //   fieldName:
    //     The name of a field.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.MemberAccess, the
    //     System.Linq.Expressions.MemberExpression.Expression property set to expression,
    //     and the System.Linq.Expressions.MemberExpression.Member property set to the
    //     System.Reflection.FieldInfo that represents the field denoted by fieldName.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression or fieldName is null.
    //
    //   System.ArgumentException:
    //     No field named fieldName is defined in expression.Type or its base types.
    [Pure]
    public static MemberExpression Field(Expression expression, string fieldName)
    {
      Contract.Requires(fieldName != null);
      Contract.Requires(expression != null);

      Contract.Ensures(Contract.Result<MemberExpression>() != null);
      return default(MemberExpression);
    }
    //
    // Summary:
    //     Creates a System.Type object that represents a generic System.Action delegate
    //     type that has specific type arguments.
    //
    // Parameters:
    //   typeArgs:
    //     An array of zero to four System.Type objects that specify the type arguments
    //     for the System.Action delegate type.
    //
    // Returns:
    //     The type of a System.Action delegate that has the specified type arguments.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     typeArgs contains more than four elements.
    //
    //   System.ArgumentNullException:
    //     typeArgs is null.
    [Pure]
    public static Type GetActionType(params Type[] typeArgs)
    {
      Contract.Requires(typeArgs != null);
      Contract.Requires(typeArgs.Length <= 4);
      Contract.Ensures(Contract.Result<Type>() != null);
      return default(Type);
    }
    //
    // Summary:
    //     Creates a System.Type object that represents a generic System.Func delegate
    //     type that has specific type arguments.
    //
    // Parameters:
    //   typeArgs:
    //     An array of one to five System.Type objects that specify the type arguments
    //     for the System.Func delegate type.
    //
    // Returns:
    //     The type of a System.Func delegate that has the specified type arguments.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     typeArgs contains less than one or more than five elements.
    //
    //   System.ArgumentNullException:
    //     typeArgs is null.
    [Pure]
    public static Type GetFuncType(params Type[] typeArgs)
    {
      Contract.Requires(typeArgs != null);
      Contract.Requires(typeArgs.Length >= 1);
      Contract.Requires(typeArgs.Length <= 5);
      Contract.Ensures(Contract.Result<Type>() != null);
      return default(Type);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a "greater
    //     than" numeric comparison.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.GreaterThan and
    //     the System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The "greater than" operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression GreaterThan(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a "greater
    //     than" numeric comparison. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   liftToNull:
    //     true to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to true;
    //     false to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to false.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.GreaterThan and
    //     the System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     System.Linq.Expressions.BinaryExpression.IsLiftedToNull, and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the "greater than" operator is not defined for left.Type
    //     and right.Type.
    [Pure]
    public static BinaryExpression GreaterThan(Expression left, Expression right, bool liftToNull, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a "greater
    //     than or equal" numeric comparison.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.GreaterThanOrEqual
    //     and the System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The "greater than or equal" operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a "greater
    //     than or equal" numeric comparison. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   liftToNull:
    //     true to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to true;
    //     false to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to false.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.GreaterThanOrEqual
    //     and the System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     System.Linq.Expressions.BinaryExpression.IsLiftedToNull, and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the "greater than or equal" operator is not defined for
    //     left.Type and right.Type.
    [Pure]
    public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates an System.Linq.Expressions.InvocationExpression.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.InvocationExpression.Expression
    //     property equal to.
    //
    //   arguments:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.Expression
    //     objects to use to populate the System.Linq.Expressions.InvocationExpression.Arguments
    //     collection.
    //
    // Returns:
    //     An System.Linq.Expressions.InvocationExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Invoke and the System.Linq.Expressions.InvocationExpression.Expression
    //     and System.Linq.Expressions.InvocationExpression.Arguments properties set
    //     to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression is null.
    //
    //   System.ArgumentException:
    //     expression.Type does not represent a delegate type or an System.Linq.Expressions.Expression<TDelegate>.
    //      -or- The System.Linq.Expressions.Expression.Type property of an element
    //     of arguments is not assignable to the type of the corresponding parameter
    //     of the delegate represented by expression.
    //
    //   System.InvalidOperationException:
    //     arguments does not contain the same number of elements as the list of parameters
    //     for the delegate represented by expression.
    [Pure]
    public static InvocationExpression Invoke(Expression expression, IEnumerable<Expression> arguments)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<InvocationExpression>() != null);
      return default(InvocationExpression);
    }
    //
    // Summary:
    //     Creates an System.Linq.Expressions.InvocationExpression.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.InvocationExpression.Expression
    //     property equal to.
    //
    //   arguments:
    //     An array of System.Linq.Expressions.Expression objects to use to populate
    //     the System.Linq.Expressions.InvocationExpression.Arguments collection.
    //
    // Returns:
    //     An System.Linq.Expressions.InvocationExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Invoke and the System.Linq.Expressions.InvocationExpression.Expression
    //     and System.Linq.Expressions.InvocationExpression.Arguments properties set
    //     to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression is null.
    //
    //   System.ArgumentException:
    //     expression.Type does not represent a delegate type or an System.Linq.Expressions.Expression<TDelegate>.
    //      -or- The System.Linq.Expressions.Expression.Type property of an element
    //     of arguments is not assignable to the type of the corresponding parameter
    //     of the delegate represented by expression.
    //
    //   System.InvalidOperationException:
    //     arguments does not contain the same number of elements as the list of parameters
    //     for the delegate represented by expression.
    [Pure]
    public static InvocationExpression Invoke(Expression expression, params Expression[] arguments)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<InvocationExpression>() != null);
      return default(InvocationExpression);
    }
    //
    // Summary:
    //     Creates an System.Linq.Expressions.Expression<TDelegate> where the delegate
    //     type is known at compile time.
    //
    // Parameters:
    //   body:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.LambdaExpression.Body
    //     property equal to.
    //
    //   parameters:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.ParameterExpression
    //     objects to use to populate the System.Linq.Expressions.LambdaExpression.Parameters
    //     collection.
    //
    // Type parameters:
    //   TDelegate:
    //     A delegate type.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression<TDelegate> that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Lambda and the System.Linq.Expressions.LambdaExpression.Body
    //     and System.Linq.Expressions.LambdaExpression.Parameters properties set to
    //     the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     body is null.  -or- One or more elements in parameters are null.
    //
    //   System.ArgumentException:
    //     TDelegate is not a delegate type.  -or- body.Type represents a type that
    //     is not assignable to the return type of TDelegate.  -or- parameters does
    //     not contain the same number of elements as the list of parameters for TDelegate.
    //      -or- The System.Linq.Expressions.Expression.Type property of an element
    //     of parameters is not assignable from the type of the corresponding parameter
    //     type of TDelegate.
    [Pure]
    public static Expression<TDelegate> Lambda<TDelegate>(Expression body, IEnumerable<ParameterExpression> parameters)
    {
      Contract.Requires(body != null);
      Contract.Ensures(Contract.Result<Expression<TDelegate>>() != null);
      return default(Expression<TDelegate>);
    }
    //
    // Summary:
    //     Creates an System.Linq.Expressions.Expression<TDelegate> where the delegate
    //     type is known at compile time.
    //
    // Parameters:
    //   body:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.LambdaExpression.Body
    //     property equal to.
    //
    //   parameters:
    //     An array of System.Linq.Expressions.ParameterExpression objects to use to
    //     populate the System.Linq.Expressions.LambdaExpression.Parameters collection.
    //
    // Type parameters:
    //   TDelegate:
    //     A delegate type.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression<TDelegate> that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Lambda and the System.Linq.Expressions.LambdaExpression.Body
    //     and System.Linq.Expressions.LambdaExpression.Parameters properties set to
    //     the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     body is null.  -or- One or more elements in parameters are null.
    //
    //   System.ArgumentException:
    //     TDelegate is not a delegate type.  -or- body.Type represents a type that
    //     is not assignable to the return type of TDelegate.  -or- parameters does
    //     not contain the same number of elements as the list of parameters for TDelegate.
    //      -or- The System.Linq.Expressions.Expression.Type property of an element
    //     of parameters is not assignable from the type of the corresponding parameter
    //     type of TDelegate.
    [Pure]
    public static Expression<TDelegate> Lambda<TDelegate>(Expression body, params ParameterExpression[] parameters)
    {
      Contract.Requires(body != null);
      Contract.Ensures(Contract.Result<Expression<TDelegate>>() != null);
      return default(Expression<TDelegate>);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.LambdaExpression by first constructing
    //     a delegate type.
    //
    // Parameters:
    //   body:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.LambdaExpression.Body
    //     property equal to.
    //
    //   parameters:
    //     An array of System.Linq.Expressions.ParameterExpression objects to use to
    //     populate the System.Linq.Expressions.LambdaExpression.Parameters collection.
    //
    // Returns:
    //     A System.Linq.Expressions.LambdaExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Lambda and the System.Linq.Expressions.LambdaExpression.Body
    //     and System.Linq.Expressions.LambdaExpression.Parameters properties set to
    //     the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     body is null.  -or- One or more elements of parameters are null.
    //
    //   System.ArgumentException:
    //     parameters contains more than four elements.
    [Pure]
    public static LambdaExpression Lambda(Expression body, params ParameterExpression[] parameters)
    {
      Contract.Requires(body != null);
      Contract.Ensures(Contract.Result<LambdaExpression>() != null);
      return default(LambdaExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.LambdaExpression and can be used when the
    //     delegate type is not known at compile time.
    //
    // Parameters:
    //   delegateType:
    //     A System.Type that represents a delegate type.
    //
    //   body:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.LambdaExpression.Body
    //     property equal to.
    //
    //   parameters:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.ParameterExpression
    //     objects to use to populate the System.Linq.Expressions.LambdaExpression.Parameters
    //     collection.
    //
    // Returns:
    //     An object that represents a lambda expression which has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Lambda and the System.Linq.Expressions.LambdaExpression.Body
    //     and System.Linq.Expressions.LambdaExpression.Parameters properties set to
    //     the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     delegateType or body is null.  -or- One or more elements in parameters are
    //     null.
    //
    //   System.ArgumentException:
    //     delegateType does not represent a delegate type.  -or- body.Type represents
    //     a type that is not assignable to the return type of the delegate type represented
    //     by delegateType.  -or- parameters does not contain the same number of elements
    //     as the list of parameters for the delegate type represented by delegateType.
    //      -or- The System.Linq.Expressions.Expression.Type property of an element
    //     of parameters is not assignable from the type of the corresponding parameter
    //     type of the delegate type represented by delegateType.
    [Pure]
    public static LambdaExpression Lambda(Type delegateType, Expression body, IEnumerable<ParameterExpression> parameters)
    {
      Contract.Requires(delegateType != null);
      Contract.Requires(body != null);
      Contract.Ensures(Contract.Result<LambdaExpression>() != null);
      return default(LambdaExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.LambdaExpression and can be used when the
    //     delegate type is not known at compile time.
    //
    // Parameters:
    //   delegateType:
    //     A System.Type that represents a delegate type.
    //
    //   body:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.LambdaExpression.Body
    //     property equal to.
    //
    //   parameters:
    //     An array of System.Linq.Expressions.ParameterExpression objects to use to
    //     populate the System.Linq.Expressions.LambdaExpression.Parameters collection.
    //
    // Returns:
    //     An object that represents a lambda expression which has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Lambda and the System.Linq.Expressions.LambdaExpression.Body
    //     and System.Linq.Expressions.LambdaExpression.Parameters properties set to
    //     the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     delegateType or body is null.  -or- One or more elements in parameters are
    //     null.
    //
    //   System.ArgumentException:
    //     delegateType does not represent a delegate type.  -or- body.Type represents
    //     a type that is not assignable to the return type of the delegate type represented
    //     by delegateType.  -or- parameters does not contain the same number of elements
    //     as the list of parameters for the delegate type represented by delegateType.
    //      -or- The System.Linq.Expressions.Expression.Type property of an element
    //     of parameters is not assignable from the type of the corresponding parameter
    //     type of the delegate type represented by delegateType.
    [Pure]
    public static LambdaExpression Lambda(Type delegateType, Expression body, params ParameterExpression[] parameters)
    {
      Contract.Requires(delegateType != null);
      Contract.Requires(body != null);
      Contract.Ensures(Contract.Result<LambdaExpression>() != null);
      return default(LambdaExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a bitwise
    //     left-shift operation.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.LeftShift and the
    //     System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The left-shift operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression LeftShift(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a bitwise
    //     left-shift operation. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.LeftShift and the
    //     System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the left-shift operator is not defined for left.Type and
    //     right.Type.
    [Pure]
    public static BinaryExpression LeftShift(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a "less
    //     than" numeric comparison.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.LessThan and the
    //     System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The "less than" operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression LessThan(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a "less
    //     than" numeric comparison. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   liftToNull:
    //     true to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to true;
    //     false to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to false.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.LessThan and the
    //     System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     System.Linq.Expressions.BinaryExpression.IsLiftedToNull, and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the "less than" operator is not defined for left.Type
    //     and right.Type.
    [Pure]
    public static BinaryExpression LessThan(Expression left, Expression right, bool liftToNull, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a " less
    //     than or equal" numeric comparison.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.LessThanOrEqual
    //     and the System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The "less than or equal" operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression LessThanOrEqual(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a " less
    //     than or equal" numeric comparison. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   liftToNull:
    //     true to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to true;
    //     false to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to false.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.LessThanOrEqual
    //     and the System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     System.Linq.Expressions.BinaryExpression.IsLiftedToNull, and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the "less than or equal" operator is not defined for left.Type
    //     and right.Type.
    [Pure]
    public static BinaryExpression LessThanOrEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberListBinding where the member is a
    //     field or property.
    //
    // Parameters:
    //   member:
    //     A System.Reflection.MemberInfo that represents a field or property to set
    //     the System.Linq.Expressions.MemberBinding.Member property equal to.
    //
    //   initializers:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.ElementInit
    //     objects to use to populate the System.Linq.Expressions.MemberListBinding.Initializers
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberListBinding that has the System.Linq.Expressions.MemberBinding.BindingType
    //     property equal to System.Linq.Expressions.MemberBindingType.ListBinding and
    //     the System.Linq.Expressions.MemberBinding.Member and System.Linq.Expressions.MemberListBinding.Initializers
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     member is null. -or- One or more elements of initializers is null.
    //
    //   System.ArgumentException:
    //     member does not represent a field or property.  -or- The System.Reflection.FieldInfo.FieldType
    //     or System.Reflection.PropertyInfo.PropertyType of the field or property that
    //     member represents does not implement System.Collections.IEnumerable.
    [Pure]
    public static MemberListBinding ListBind(MemberInfo member, IEnumerable<ElementInit> initializers)
    {
      Contract.Requires(member != null);
      Contract.Ensures(Contract.Result<MemberListBinding>() != null);
      return default(MemberListBinding);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberListBinding where the member is a
    //     field or property.
    //
    // Parameters:
    //   member:
    //     A System.Reflection.MemberInfo that represents a field or property to set
    //     the System.Linq.Expressions.MemberBinding.Member property equal to.
    //
    //   initializers:
    //     An array of System.Linq.Expressions.ElementInit objects to use to populate
    //     the System.Linq.Expressions.MemberListBinding.Initializers collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberListBinding that has the System.Linq.Expressions.MemberBinding.BindingType
    //     property equal to System.Linq.Expressions.MemberBindingType.ListBinding and
    //     the System.Linq.Expressions.MemberBinding.Member and System.Linq.Expressions.MemberListBinding.Initializers
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     member is null. -or- One or more elements of initializers is null.
    //
    //   System.ArgumentException:
    //     member does not represent a field or property.  -or- The System.Reflection.FieldInfo.FieldType
    //     or System.Reflection.PropertyInfo.PropertyType of the field or property that
    //     member represents does not implement System.Collections.IEnumerable.
    [Pure]
    public static MemberListBinding ListBind(MemberInfo member, params ElementInit[] initializers)
    {
      Contract.Requires(member != null);
      Contract.Ensures(Contract.Result<MemberListBinding>() != null);
      return default(MemberListBinding);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberListBinding based on a specified
    //     property accessor method.
    //
    // Parameters:
    //   propertyAccessor:
    //     A System.Reflection.MethodInfo that represents a property accessor method.
    //
    //   initializers:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.ElementInit
    //     objects to use to populate the System.Linq.Expressions.MemberListBinding.Initializers
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberListBinding that has the System.Linq.Expressions.MemberBinding.BindingType
    //     property equal to System.Linq.Expressions.MemberBindingType.ListBinding,
    //     the System.Linq.Expressions.MemberBinding.Member property set to the System.Reflection.MemberInfo
    //     that represents the property accessed in propertyAccessor, and System.Linq.Expressions.MemberListBinding.Initializers
    //     populated with the elements of initializers.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     propertyAccessor is null. -or- One or more elements of initializers are null.
    //
    //   System.ArgumentException:
    //     propertyAccessor does not represent a property accessor method.  -or- The
    //     System.Reflection.PropertyInfo.PropertyType of the property that the method
    //     represented by propertyAccessor accesses does not implement System.Collections.IEnumerable.
    [Pure]
    public static MemberListBinding ListBind(MethodInfo propertyAccessor, IEnumerable<ElementInit> initializers)
    {
      Contract.Requires(propertyAccessor != null);
      Contract.Ensures(Contract.Result<MemberListBinding>() != null);
      return default(MemberListBinding);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberListBinding object based on a specified
    //     property accessor method.
    //
    // Parameters:
    //   propertyAccessor:
    //     A System.Reflection.MethodInfo that represents a property accessor method.
    //
    //   initializers:
    //     An array of System.Linq.Expressions.ElementInit objects to use to populate
    //     the System.Linq.Expressions.MemberListBinding.Initializers collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberListBinding that has the System.Linq.Expressions.MemberBinding.BindingType
    //     property equal to System.Linq.Expressions.MemberBindingType.ListBinding,
    //     the System.Linq.Expressions.MemberBinding.Member property set to the System.Reflection.MemberInfo
    //     that represents the property accessed in propertyAccessor, and System.Linq.Expressions.MemberListBinding.Initializers
    //     populated with the elements of initializers.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     propertyAccessor is null. -or- One or more elements of initializers is null.
    //
    //   System.ArgumentException:
    //     propertyAccessor does not represent a property accessor method.  -or- The
    //     System.Reflection.PropertyInfo.PropertyType of the property that the method
    //     represented by propertyAccessor accesses does not implement System.Collections.IEnumerable.
    [Pure]
    public static MemberListBinding ListBind(MethodInfo propertyAccessor, params ElementInit[] initializers)
    {
      Contract.Requires(propertyAccessor != null);
      Contract.Ensures(Contract.Result<MemberListBinding>() != null);
      return default(MemberListBinding);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.ListInitExpression that uses specified
    //     System.Linq.Expressions.ElementInit objects to initialize a collection.
    //
    // Parameters:
    //   newExpression:
    //     A System.Linq.Expressions.NewExpression to set the System.Linq.Expressions.ListInitExpression.NewExpression
    //     property equal to.
    //
    //   initializers:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.ElementInit
    //     objects to use to populate the System.Linq.Expressions.ListInitExpression.Initializers
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.ListInitExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ListInit and the
    //     System.Linq.Expressions.ListInitExpression.NewExpression and System.Linq.Expressions.ListInitExpression.Initializers
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     newExpression or initializers is null.  -or- One or more elements of initializers
    //     is null.
    //
    //   System.ArgumentException:
    //     newExpression.Type does not implement System.Collections.IEnumerable.
    [Pure]
    public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<ElementInit> initializers)
    {
      Contract.Requires(newExpression != null);
      Contract.Requires(initializers != null);
      Contract.Ensures(Contract.Result<ListInitExpression>() != null);
      return default(ListInitExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.ListInitExpression that uses a method named
    //     "Add" to add elements to a collection.
    //
    // Parameters:
    //   newExpression:
    //     A System.Linq.Expressions.NewExpression to set the System.Linq.Expressions.ListInitExpression.NewExpression
    //     property equal to.
    //
    //   initializers:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.Expression
    //     objects to use to populate the System.Linq.Expressions.ListInitExpression.Initializers
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.ListInitExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ListInit and the
    //     System.Linq.Expressions.ListInitExpression.NewExpression property set to
    //     the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     newExpression or initializers is null.  -or- One or more elements of initializers
    //     is null.
    //
    //   System.ArgumentException:
    //     newExpression.Type does not implement System.Collections.IEnumerable.
    //
    //   System.InvalidOperationException:
    //     There is no instance method named "Add" (case insensitive) declared in newExpression.Type
    //     or its base type.  -or- The add method on newExpression.Type or its base
    //     type does not take exactly one argument.  -or- The type represented by the
    //     System.Linq.Expressions.Expression.Type property of the first element of
    //     initializers is not assignable to the argument type of the add method on
    //     newExpression.Type or its base type.  -or- More than one argument-compatible
    //     method named "Add" (case-insensitive) exists on newExpression.Type and/or
    //     its base type.
    [Pure]
    public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<Expression> initializers)
    {
      Contract.Requires(newExpression != null);
      Contract.Requires(initializers != null);
      Contract.Ensures(Contract.Result<ListInitExpression>() != null);
      return default(ListInitExpression);      
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.ListInitExpression that uses specified
    //     System.Linq.Expressions.ElementInit objects to initialize a collection.
    //
    // Parameters:
    //   newExpression:
    //     A System.Linq.Expressions.NewExpression to set the System.Linq.Expressions.ListInitExpression.NewExpression
    //     property equal to.
    //
    //   initializers:
    //     An array of System.Linq.Expressions.ElementInit objects to use to populate
    //     the System.Linq.Expressions.ListInitExpression.Initializers collection.
    //
    // Returns:
    //     A System.Linq.Expressions.ListInitExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ListInit and the
    //     System.Linq.Expressions.ListInitExpression.NewExpression and System.Linq.Expressions.ListInitExpression.Initializers
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     newExpression or initializers is null.  -or- One or more elements of initializers
    //     is null.
    //
    //   System.ArgumentException:
    //     newExpression.Type does not implement System.Collections.IEnumerable.
    [Pure]
    public static ListInitExpression ListInit(NewExpression newExpression, params ElementInit[] initializers)
    {
      Contract.Requires(newExpression != null);
      Contract.Requires(initializers != null);
      Contract.Ensures(Contract.Result<ListInitExpression>() != null);
      return default(ListInitExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.ListInitExpression that uses a method named
    //     "Add" to add elements to a collection.
    //
    // Parameters:
    //   newExpression:
    //     A System.Linq.Expressions.NewExpression to set the System.Linq.Expressions.ListInitExpression.NewExpression
    //     property equal to.
    //
    //   initializers:
    //     An array of System.Linq.Expressions.Expression objects to use to populate
    //     the System.Linq.Expressions.ListInitExpression.Initializers collection.
    //
    // Returns:
    //     A System.Linq.Expressions.ListInitExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ListInit and the
    //     System.Linq.Expressions.ListInitExpression.NewExpression property set to
    //     the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     newExpression or initializers is null.  -or- One or more elements of initializers
    //     is null.
    //
    //   System.ArgumentException:
    //     newExpression.Type does not implement System.Collections.IEnumerable.
    //
    //   System.InvalidOperationException:
    //     There is no instance method named "Add" (case insensitive) declared in newExpression.Type
    //     or its base type.  -or- The add method on newExpression.Type or its base
    //     type does not take exactly one argument.  -or- The type represented by the
    //     System.Linq.Expressions.Expression.Type property of the first element of
    //     initializers is not assignable to the argument type of the add method on
    //     newExpression.Type or its base type.  -or- More than one argument-compatible
    //     method named "Add" (case-insensitive) exists on newExpression.Type and/or
    //     its base type.
    [Pure]
    public static ListInitExpression ListInit(NewExpression newExpression, params Expression[] initializers)
    {
      Contract.Requires(newExpression != null);
      Contract.Requires(initializers != null);
      Contract.Ensures(Contract.Result<ListInitExpression>() != null);
      return default(ListInitExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.ListInitExpression that uses a specified
    //     method to add elements to a collection.
    //
    // Parameters:
    //   newExpression:
    //     A System.Linq.Expressions.NewExpression to set the System.Linq.Expressions.ListInitExpression.NewExpression
    //     property equal to.
    //
    //   addMethod:
    //     A System.Reflection.MethodInfo that represents an instance method named "Add"
    //     (case insensitive), that adds an element to a collection.
    //
    //   initializers:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.Expression
    //     objects to use to populate the System.Linq.Expressions.ListInitExpression.Initializers
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.ListInitExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ListInit and the
    //     System.Linq.Expressions.ListInitExpression.NewExpression property set to
    //     the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     newExpression or initializers is null.  -or- One or more elements of initializers
    //     is null.
    //
    //   System.ArgumentException:
    //     newExpression.Type does not implement System.Collections.IEnumerable.  -or-
    //     addMethod is not null and it does not represent an instance method named
    //     "Add" (case insensitive) that takes exactly one argument.  -or- addMethod
    //     is not null and the type represented by the System.Linq.Expressions.Expression.Type
    //     property of one or more elements of initializers is not assignable to the
    //     argument type of the method that addMethod represents.
    //
    //   System.InvalidOperationException:
    //     addMethod is null and no instance method named "Add" that takes one type-compatible
    //     argument exists on newExpression.Type or its base type.
    [Pure]
    public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, IEnumerable<Expression> initializers)
    {
      Contract.Requires(newExpression != null);
      Contract.Requires(initializers != null);
      Contract.Ensures(Contract.Result<ListInitExpression>() != null);
      return default(ListInitExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.ListInitExpression that uses a specified
    //     method to add elements to a collection.
    //
    // Parameters:
    //   newExpression:
    //     A System.Linq.Expressions.NewExpression to set the System.Linq.Expressions.ListInitExpression.NewExpression
    //     property equal to.
    //
    //   addMethod:
    //     A System.Reflection.MethodInfo that represents an instance method that takes
    //     one argument, that adds an element to a collection.
    //
    //   initializers:
    //     An array of System.Linq.Expressions.Expression objects to use to populate
    //     the System.Linq.Expressions.ListInitExpression.Initializers collection.
    //
    // Returns:
    //     A System.Linq.Expressions.ListInitExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.ListInit and the
    //     System.Linq.Expressions.ListInitExpression.NewExpression property set to
    //     the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     newExpression or initializers is null.  -or- One or more elements of initializers
    //     is null.
    //
    //   System.ArgumentException:
    //     newExpression.Type does not implement System.Collections.IEnumerable.  -or-
    //     addMethod is not null and it does not represent an instance method named
    //     "Add" (case insensitive) that takes exactly one argument.  -or- addMethod
    //     is not null and the type represented by the System.Linq.Expressions.Expression.Type
    //     property of one or more elements of initializers is not assignable to the
    //     argument type of the method that addMethod represents.
    //
    //   System.InvalidOperationException:
    //     addMethod is null and no instance method named "Add" that takes one type-compatible
    //     argument exists on newExpression.Type or its base type.
    [Pure]
    public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, params Expression[] initializers)
    {
      Contract.Requires(newExpression != null);
      Contract.Requires(initializers != null);
      Contract.Ensures(Contract.Result<ListInitExpression>() != null);
      return default(ListInitExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression, given the left and right
    //     operands, by calling an appropriate factory method.
    //
    // Parameters:
    //   binaryType:
    //     The System.Linq.Expressions.ExpressionType that specifies the type of binary
    //     operation.
    //
    //   left:
    //     An System.Linq.Expressions.Expression that represents the left operand.
    //
    //   right:
    //     An System.Linq.Expressions.Expression that represents the right operand.
    //
    // Returns:
    //     The System.Linq.Expressions.BinaryExpression that results from calling the
    //     appropriate factory method.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     binaryType does not correspond to a binary expression node.
    //
    //   System.ArgumentNullException:
    //     left or right is null.
    [Pure]
    public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression, given the left operand,
    //     right operand and implementing method, by calling the appropriate factory
    //     method.
    //
    // Parameters:
    //   binaryType:
    //     The System.Linq.Expressions.ExpressionType that specifies the type of binary
    //     operation.
    //
    //   left:
    //     An System.Linq.Expressions.Expression that represents the left operand.
    //
    //   right:
    //     An System.Linq.Expressions.Expression that represents the right operand.
    //
    //   liftToNull:
    //     true to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to true;
    //     false to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to false.
    //
    //   method:
    //     A System.Reflection.MethodInfo that specifies the implementing method.
    //
    // Returns:
    //     The System.Linq.Expressions.BinaryExpression that results from calling the
    //     appropriate factory method.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     binaryType does not correspond to a binary expression node.
    //
    //   System.ArgumentNullException:
    //     left or right is null.
    [Pure]
    public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression, given the left operand,
    //     right operand, implementing method and type conversion function, by calling
    //     the appropriate factory method.
    //
    // Parameters:
    //   binaryType:
    //     The System.Linq.Expressions.ExpressionType that specifies the type of binary
    //     operation.
    //
    //   left:
    //     An System.Linq.Expressions.Expression that represents the left operand.
    //
    //   right:
    //     An System.Linq.Expressions.Expression that represents the right operand.
    //
    //   liftToNull:
    //     true to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to true;
    //     false to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to false.
    //
    //   method:
    //     A System.Reflection.MethodInfo that specifies the implementing method.
    //
    //   conversion:
    //     A System.Linq.Expressions.LambdaExpression that represents a type conversion
    //     function. This parameter is used only if binaryType is System.Linq.Expressions.ExpressionType.Coalesce.
    //
    // Returns:
    //     The System.Linq.Expressions.BinaryExpression that results from calling the
    //     appropriate factory method.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     binaryType does not correspond to a binary expression node.
    //
    //   System.ArgumentNullException:
    //     left or right is null.
    [Pure]
    public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberExpression that represents accessing
    //     either a field or a property.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression that represents the object that the
    //     member belongs to.
    //
    //   member:
    //     The System.Reflection.MemberInfo that describes the field or property to
    //     be accessed.
    //
    // Returns:
    //     The System.Linq.Expressions.MemberExpression that results from calling the
    //     appropriate factory method.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression or member is null.
    //
    //   System.ArgumentException:
    //     member does not represent a field or property.
    [Pure]
    public static MemberExpression MakeMemberAccess(Expression expression, MemberInfo member)
    {
      Contract.Requires(expression != null);
      Contract.Requires(member != null);

      Contract.Ensures(Contract.Result<MemberExpression>() != null);
      return default(MemberExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression, given an operand, by calling
    //     the appropriate factory method.
    //
    // Parameters:
    //   unaryType:
    //     The System.Linq.Expressions.ExpressionType that specifies the type of unary
    //     operation.
    //
    //   operand:
    //     An System.Linq.Expressions.Expression that represents the operand.
    //
    //   type:
    //     The System.Type that specifies the type to be converted to (pass null if
    //     not applicable).
    //
    // Returns:
    //     The System.Linq.Expressions.UnaryExpression that results from calling the
    //     appropriate factory method.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     unaryType does not correspond to a unary expression node.
    //
    //   System.ArgumentNullException:
    //     operand is null.
    [Pure]
    public static UnaryExpression MakeUnary(ExpressionType unaryType, Expression operand, Type type)
    {
      Contract.Requires(operand != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression, given an operand and implementing
    //     method, by calling the appropriate factory method.
    //
    // Parameters:
    //   unaryType:
    //     The System.Linq.Expressions.ExpressionType that specifies the type of unary
    //     operation.
    //
    //   operand:
    //     An System.Linq.Expressions.Expression that represents the operand.
    //
    //   type:
    //     The System.Type that specifies the type to be converted to (pass null if
    //     not applicable).
    //
    //   method:
    //     The System.Reflection.MethodInfo that represents the implementing method.
    //
    // Returns:
    //     The System.Linq.Expressions.UnaryExpression that results from calling the
    //     appropriate factory method.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     unaryType does not correspond to a unary expression node.
    //
    //   System.ArgumentNullException:
    //     operand is null.
    [Pure]
    public static UnaryExpression MakeUnary(ExpressionType unaryType, Expression operand, Type type, MethodInfo method)
    {
      Contract.Requires(operand != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberMemberBinding that represents the
    //     recursive initialization of members of a field or property.
    //
    // Parameters:
    //   member:
    //     The System.Reflection.MemberInfo to set the System.Linq.Expressions.MemberBinding.Member
    //     property equal to.
    //
    //   bindings:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.MemberBinding
    //     objects to use to populate the System.Linq.Expressions.MemberMemberBinding.Bindings
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberMemberBinding that has the System.Linq.Expressions.MemberBinding.BindingType
    //     property equal to System.Linq.Expressions.MemberBindingType.MemberBinding
    //     and the System.Linq.Expressions.MemberBinding.Member and System.Linq.Expressions.MemberMemberBinding.Bindings
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     member or bindings is null.
    //
    //   System.ArgumentException:
    //     member does not represent a field or property.  -or- The System.Linq.Expressions.MemberBinding.Member
    //     property of an element of bindings does not represent a member of the type
    //     of the field or property that member represents.
    [Pure]
    public static MemberMemberBinding MemberBind(MemberInfo member, IEnumerable<MemberBinding> bindings)
    {
      Contract.Requires(member != null);
      Contract.Requires(bindings != null);
      Contract.Ensures(Contract.Result<MemberMemberBinding>() != null);
      return default(MemberMemberBinding);
    }

    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberMemberBinding that represents the
    //     recursive initialization of members of a field or property.
    //
    // Parameters:
    //   member:
    //     The System.Reflection.MemberInfo to set the System.Linq.Expressions.MemberBinding.Member
    //     property equal to.
    //
    //   bindings:
    //     An array of System.Linq.Expressions.MemberBinding objects to use to populate
    //     the System.Linq.Expressions.MemberMemberBinding.Bindings collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberMemberBinding that has the System.Linq.Expressions.MemberBinding.BindingType
    //     property equal to System.Linq.Expressions.MemberBindingType.MemberBinding
    //     and the System.Linq.Expressions.MemberBinding.Member and System.Linq.Expressions.MemberMemberBinding.Bindings
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     member or bindings is null.
    //
    //   System.ArgumentException:
    //     member does not represent a field or property.  -or- The System.Linq.Expressions.MemberBinding.Member
    //     property of an element of bindings does not represent a member of the type
    //     of the field or property that member represents.
    [Pure]
    public static MemberMemberBinding MemberBind(MemberInfo member, params MemberBinding[] bindings)
    {
      Contract.Requires(member != null);
      Contract.Requires(bindings != null);
      Contract.Ensures(Contract.Result<MemberMemberBinding>() != null);
      return default(MemberMemberBinding);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberMemberBinding that represents the
    //     recursive initialization of members of a member that is accessed by using
    //     a property accessor method.
    //
    // Parameters:
    //   propertyAccessor:
    //     The System.Reflection.MethodInfo that represents a property accessor method.
    //
    //   bindings:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.MemberBinding
    //     objects to use to populate the System.Linq.Expressions.MemberMemberBinding.Bindings
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberMemberBinding that has the System.Linq.Expressions.MemberBinding.BindingType
    //     property equal to System.Linq.Expressions.MemberBindingType.MemberBinding,
    //     the System.Linq.Expressions.MemberBinding.Member property set to the System.Reflection.PropertyInfo
    //     that represents the property accessed in propertyAccessor, and System.Linq.Expressions.MemberMemberBinding.Bindings
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     propertyAccessor or bindings is null.
    //
    //   System.ArgumentException:
    //     propertyAccessor does not represent a property accessor method.  -or- The
    //     System.Linq.Expressions.MemberBinding.Member property of an element of bindings
    //     does not represent a member of the type of the property accessed by the method
    //     that propertyAccessor represents.
    [Pure]
    public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, IEnumerable<MemberBinding> bindings)
    {
      Contract.Requires(propertyAccessor != null);
      Contract.Requires(bindings != null);
      Contract.Ensures(Contract.Result<MemberMemberBinding>() != null);
      return default(MemberMemberBinding);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberMemberBinding that represents the
    //     recursive initialization of members of a member that is accessed by using
    //     a property accessor method.
    //
    // Parameters:
    //   propertyAccessor:
    //     The System.Reflection.MethodInfo that represents a property accessor method.
    //
    //   bindings:
    //     An array of System.Linq.Expressions.MemberBinding objects to use to populate
    //     the System.Linq.Expressions.MemberMemberBinding.Bindings collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberMemberBinding that has the System.Linq.Expressions.MemberBinding.BindingType
    //     property equal to System.Linq.Expressions.MemberBindingType.MemberBinding,
    //     the System.Linq.Expressions.MemberBinding.Member property set to the System.Reflection.PropertyInfo
    //     that represents the property accessed in propertyAccessor, and System.Linq.Expressions.MemberMemberBinding.Bindings
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     propertyAccessor or bindings is null.
    //
    //   System.ArgumentException:
    //     propertyAccessor does not represent a property accessor method.  -or- The
    //     System.Linq.Expressions.MemberBinding.Member property of an element of bindings
    //     does not represent a member of the type of the property accessed by the method
    //     that propertyAccessor represents.
    [Pure]
    public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, params MemberBinding[] bindings)
    {
      Contract.Requires(propertyAccessor != null);
      Contract.Requires(bindings != null);
      Contract.Ensures(Contract.Result<MemberMemberBinding>() != null);
      return default(MemberMemberBinding);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberInitExpression.
    //
    // Parameters:
    //   newExpression:
    //     A System.Linq.Expressions.NewExpression to set the System.Linq.Expressions.MemberInitExpression.NewExpression
    //     property equal to.
    //
    //   bindings:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.MemberBinding
    //     objects to use to populate the System.Linq.Expressions.MemberInitExpression.Bindings
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberInitExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.MemberInit and the
    //     System.Linq.Expressions.MemberInitExpression.NewExpression and System.Linq.Expressions.MemberInitExpression.Bindings
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     newExpression or bindings is null.
    //
    //   System.ArgumentException:
    //     The System.Linq.Expressions.MemberBinding.Member property of an element of
    //     bindings does not represent a member of the type that newExpression.Type
    //     represents.
    [Pure]
    public static MemberInitExpression MemberInit(NewExpression newExpression, IEnumerable<MemberBinding> bindings)
    {
      Contract.Requires(newExpression != null);
      Contract.Requires(bindings != null);
      Contract.Ensures(Contract.Result<MemberInitExpression>() != null);
      return default(MemberInitExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberInitExpression.
    //
    // Parameters:
    //   newExpression:
    //     A System.Linq.Expressions.NewExpression to set the System.Linq.Expressions.MemberInitExpression.NewExpression
    //     property equal to.
    //
    //   bindings:
    //     An array of System.Linq.Expressions.MemberBinding objects to use to populate
    //     the System.Linq.Expressions.MemberInitExpression.Bindings collection.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberInitExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.MemberInit and the
    //     System.Linq.Expressions.MemberInitExpression.NewExpression and System.Linq.Expressions.MemberInitExpression.Bindings
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     newExpression or bindings is null.
    //
    //   System.ArgumentException:
    //     The System.Linq.Expressions.MemberBinding.Member property of an element of
    //     bindings does not represent a member of the type that newExpression.Type
    //     represents.
    [Pure]
    public static MemberInitExpression MemberInit(NewExpression newExpression, params MemberBinding[] bindings)
    {
      Contract.Requires(newExpression != null);
      Contract.Requires(bindings != null);
      Contract.Ensures(Contract.Result<MemberInitExpression>() != null);
      return default(MemberInitExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     remainder operation.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Modulo and the System.Linq.Expressions.BinaryExpression.Left
    //     and System.Linq.Expressions.BinaryExpression.Right properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The modulus operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression Modulo(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     remainder operation. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Modulo and the System.Linq.Expressions.BinaryExpression.Left,
    //     System.Linq.Expressions.BinaryExpression.Right, and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the modulus operator is not defined for left.Type and
    //     right.Type.
    [Pure]
    public static BinaryExpression Modulo(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     multiplication operation that does not have overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Multiply and the
    //     System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The multiplication operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression Multiply(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     multiplication operation that does not have overflow checking and for which
    //     the implementing method is specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Multiply and the
    //     System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the multiplication operator is not defined for left.Type
    //     and right.Type.
    [Pure]
    public static BinaryExpression Multiply(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     multiplication operation that has overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.MultiplyChecked
    //     and the System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The multiplication operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression MultiplyChecked(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     multiplication operation that has overflow checking. The implementing method
    //     can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.MultiplyChecked
    //     and the System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the multiplication operator is not defined for left.Type
    //     and right.Type.
    [Pure]
    public static BinaryExpression MultiplyChecked(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents an arithmetic
    //     negation operation.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Negate and the System.Linq.Expressions.UnaryExpression.Operand
    //     property set to the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression is null.
    //
    //   System.InvalidOperationException:
    //     The unary minus operator is not defined for expression.Type.
    [Pure]
    public static UnaryExpression Negate(Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents an arithmetic
    //     negation operation. The implementing method can be specified.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.UnaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Negate and the System.Linq.Expressions.UnaryExpression.Operand
    //     and System.Linq.Expressions.UnaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly one argument.
    //
    //   System.InvalidOperationException:
    //     method is null and the unary minus operator is not defined for expression.Type.
    //      -or- expression.Type (or its corresponding non-nullable type if it is a
    //     nullable value type) is not assignable to the argument type of the method
    //     represented by method.
    [Pure]
    public static UnaryExpression Negate(Expression expression, MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents an arithmetic
    //     negation operation that has overflow checking.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.NegateChecked and
    //     the System.Linq.Expressions.UnaryExpression.Operand property set to the specified
    //     value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression is null.
    //
    //   System.InvalidOperationException:
    //     The unary minus operator is not defined for expression.Type.
    [Pure]
    public static UnaryExpression NegateChecked(Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents an arithmetic
    //     negation operation that has overflow checking. The implementing method can
    //     be specified.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.UnaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.NegateChecked and
    //     the System.Linq.Expressions.UnaryExpression.Operand and System.Linq.Expressions.UnaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly one argument.
    //
    //   System.InvalidOperationException:
    //     method is null and the unary minus operator is not defined for expression.Type.
    //      -or- expression.Type (or its corresponding non-nullable type if it is a
    //     nullable value type) is not assignable to the argument type of the method
    //     represented by method.
    [Pure]
    public static UnaryExpression NegateChecked(Expression expression, MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.NewExpression that represents calling the
    //     specified constructor that takes no arguments.
    //
    // Parameters:
    //   constructor:
    //     The System.Reflection.ConstructorInfo to set the System.Linq.Expressions.NewExpression.Constructor
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.NewExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.New and the System.Linq.Expressions.NewExpression.Constructor
    //     property set to the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     constructor is null.
    //
    //   System.ArgumentException:
    //     The constructor that constructor represents has at least one parameter.
    [Pure]
    public static NewExpression New(ConstructorInfo constructor)
    {
      Contract.Requires(constructor != null);
      Contract.Ensures(Contract.Result<NewExpression>() != null);
      return default(NewExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.NewExpression that represents calling the
    //     parameterless constructor of the specified type.
    //
    // Parameters:
    //   type:
    //     A System.Type that has a constructor that takes no arguments.
    //
    // Returns:
    //     A System.Linq.Expressions.NewExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.New and the System.Linq.Expressions.NewExpression.Constructor
    //     property set to the System.Reflection.ConstructorInfo that represents the
    //     parameterless constructor of the specified type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type is null.
    //
    //   System.ArgumentException:
    //     The type that type represents does not have a parameterless constructor.
    [Pure]
    public static NewExpression New(Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<NewExpression>() != null);
      return default(NewExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.NewExpression that represents calling the
    //     specified constructor with the specified arguments.
    //
    // Parameters:
    //   constructor:
    //     The System.Reflection.ConstructorInfo to set the System.Linq.Expressions.NewExpression.Constructor
    //     property equal to.
    //
    //   arguments:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.Expression
    //     objects to use to populate the System.Linq.Expressions.NewExpression.Arguments
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.NewExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.New and the System.Linq.Expressions.NewExpression.Constructor
    //     and System.Linq.Expressions.NewExpression.Arguments properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     constructor is null.  -or- An element of arguments is null.
    //
    //   System.ArgumentException:
    //     The arguments parameter does not contain the same number of elements as the
    //     number of parameters for the constructor that constructor represents.  -or-
    //     The System.Linq.Expressions.Expression.Type property of an element of arguments
    //     is not assignable to the type of the corresponding parameter of the constructor
    //     that constructor represents.
    [Pure]
    public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments)
    {
      Contract.Requires(constructor != null);
      Contract.Ensures(Contract.Result<NewExpression>() != null);
      return default(NewExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.NewExpression that represents calling the
    //     specified constructor with the specified arguments.
    //
    // Parameters:
    //   constructor:
    //     The System.Reflection.ConstructorInfo to set the System.Linq.Expressions.NewExpression.Constructor
    //     property equal to.
    //
    //   arguments:
    //     An array of System.Linq.Expressions.Expression objects to use to populate
    //     the System.Linq.Expressions.NewExpression.Arguments collection.
    //
    // Returns:
    //     A System.Linq.Expressions.NewExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.New and the System.Linq.Expressions.NewExpression.Constructor
    //     and System.Linq.Expressions.NewExpression.Arguments properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     constructor is null.  -or- An element of arguments is null.
    //
    //   System.ArgumentException:
    //     The length of arguments does match the number of parameters for the constructor
    //     that constructor represents.  -or- The System.Linq.Expressions.Expression.Type
    //     property of an element of arguments is not assignable to the type of the
    //     corresponding parameter of the constructor that constructor represents.
    [Pure]
    public static NewExpression New(ConstructorInfo constructor, params Expression[] arguments)
    {
      Contract.Requires(constructor != null);
      Contract.Ensures(Contract.Result<NewExpression>() != null);
      return default(NewExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.NewExpression that represents calling the
    //     specified constructor with the specified arguments. The members that access
    //     the constructor initialized fields are specified.
    //
    // Parameters:
    //   constructor:
    //     The System.Reflection.ConstructorInfo to set the System.Linq.Expressions.NewExpression.Constructor
    //     property equal to.
    //
    //   arguments:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.Expression
    //     objects to use to populate the System.Linq.Expressions.NewExpression.Arguments
    //     collection.
    //
    //   members:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Reflection.MemberInfo
    //     objects to use to populate the System.Linq.Expressions.NewExpression.Members
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.NewExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.New and the System.Linq.Expressions.NewExpression.Constructor,
    //     System.Linq.Expressions.NewExpression.Arguments and System.Linq.Expressions.NewExpression.Members
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     constructor is null.  -or- An element of arguments is null.  -or- An element
    //     of members is null.
    //
    //   System.ArgumentException:
    //     The arguments parameter does not contain the same number of elements as the
    //     number of parameters for the constructor that constructor represents.  -or-
    //     The System.Linq.Expressions.Expression.Type property of an element of arguments
    //     is not assignable to the type of the corresponding parameter of the constructor
    //     that constructor represents.  -or- The members parameter does not have the
    //     same number of elements as arguments.  -or- An element of arguments has a
    //     System.Linq.Expressions.Expression.Type property that represents a type that
    //     is not assignable to the type of the member that is represented by the corresponding
    //     element of members.  -or- An element of members represents a property that
    //     does not have a get accessor.
    [Pure]
    public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, IEnumerable<MemberInfo> members)
    {
      Contract.Requires(constructor != null);
      Contract.Ensures(Contract.Result<NewExpression>() != null);
      return default(NewExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.NewExpression that represents calling the
    //     specified constructor with the specified arguments. The members that access
    //     the constructor initialized fields are specified as an array.
    //
    // Parameters:
    //   constructor:
    //     The System.Reflection.ConstructorInfo to set the System.Linq.Expressions.NewExpression.Constructor
    //     property equal to.
    //
    //   arguments:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.Expression
    //     objects to use to populate the System.Linq.Expressions.NewExpression.Arguments
    //     collection.
    //
    //   members:
    //     An array of System.Reflection.MemberInfo objects to use to populate the System.Linq.Expressions.NewExpression.Members
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.NewExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.New and the System.Linq.Expressions.NewExpression.Constructor,
    //     System.Linq.Expressions.NewExpression.Arguments and System.Linq.Expressions.NewExpression.Members
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     constructor is null.  -or- An element of arguments is null.  -or- An element
    //     of members is null.
    //
    //   System.ArgumentException:
    //     The arguments parameter does not contain the same number of elements as the
    //     number of parameters for the constructor that constructor represents.  -or-
    //     The System.Linq.Expressions.Expression.Type property of an element of arguments
    //     is not assignable to the type of the corresponding parameter of the constructor
    //     that constructor represents.  -or- The members parameter does not have the
    //     same number of elements as arguments.  -or- An element of arguments has a
    //     System.Linq.Expressions.Expression.Type property that represents a type that
    //     is not assignable to the type of the member that is represented by the corresponding
    //     element of members.  -or- An element of members represents a property that
    //     does not have a get accessor.
    [Pure]
    public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, params MemberInfo[] members)
    {
      Contract.Requires(constructor != null);
      Contract.Ensures(Contract.Result<NewExpression>() != null);
      return default(NewExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.NewArrayExpression that represents creating
    //     an array that has a specified rank.
    //
    // Parameters:
    //   type:
    //     A System.Type that represents the element type of the array.
    //
    //   bounds:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.Expression
    //     objects to use to populate the System.Linq.Expressions.NewArrayExpression.Expressions
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.NewArrayExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.NewArrayBounds and
    //     the System.Linq.Expressions.NewArrayExpression.Expressions property set to
    //     the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type or bounds is null.  -or- An element of bounds is null.
    //
    //   System.ArgumentException:
    //     The System.Linq.Expressions.Expression.Type property of an element of bounds
    //     does not represent an integral type.
    [Pure]
    public static NewArrayExpression NewArrayBounds(Type type, IEnumerable<Expression> bounds)
    {
      Contract.Requires(type != null);
      Contract.Requires(bounds != null);
      Contract.Ensures(Contract.Result<NewArrayExpression>() != null);
      return default(NewArrayExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.NewArrayExpression that represents creating
    //     an array that has a specified rank.
    //
    // Parameters:
    //   type:
    //     A System.Type that represents the element type of the array.
    //
    //   bounds:
    //     An array of System.Linq.Expressions.Expression objects to use to populate
    //     the System.Linq.Expressions.NewArrayExpression.Expressions collection.
    //
    // Returns:
    //     A System.Linq.Expressions.NewArrayExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.NewArrayBounds and
    //     the System.Linq.Expressions.NewArrayExpression.Expressions property set to
    //     the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type or bounds is null.  -or- An element of bounds is null.
    //
    //   System.ArgumentException:
    //     The System.Linq.Expressions.Expression.Type property of an element of bounds
    //     does not represent an integral type.
    [Pure]
    public static NewArrayExpression NewArrayBounds(Type type, params Expression[] bounds)
    {
      Contract.Requires(type != null);
      Contract.Requires(bounds != null);
      Contract.Ensures(Contract.Result<NewArrayExpression>() != null);
      return default(NewArrayExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.NewArrayExpression that represents creating
    //     a one-dimensional array and initializing it from a list of elements.
    //
    // Parameters:
    //   type:
    //     A System.Type that represents the element type of the array.
    //
    //   initializers:
    //     An System.Collections.Generic.IEnumerable<T> that contains System.Linq.Expressions.Expression
    //     objects to use to populate the System.Linq.Expressions.NewArrayExpression.Expressions
    //     collection.
    //
    // Returns:
    //     A System.Linq.Expressions.NewArrayExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.NewArrayInit and
    //     the System.Linq.Expressions.NewArrayExpression.Expressions property set to
    //     the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type or initializers is null.  -or- An element of initializers is null.
    //
    //   System.InvalidOperationException:
    //     The System.Linq.Expressions.Expression.Type property of an element of initializers
    //     represents a type that is not assignable to the type that type represents.
    [Pure]
    public static NewArrayExpression NewArrayInit(Type type, IEnumerable<Expression> initializers)
    {
      Contract.Requires(type != null);
      Contract.Requires(initializers != null);
      Contract.Ensures(Contract.Result<NewArrayExpression>() != null);
      return default(NewArrayExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.NewArrayExpression that represents creating
    //     a one-dimensional array and initializing it from a list of elements.
    //
    // Parameters:
    //   type:
    //     A System.Type that represents the element type of the array.
    //
    //   initializers:
    //     An array of System.Linq.Expressions.Expression objects to use to populate
    //     the System.Linq.Expressions.NewArrayExpression.Expressions collection.
    //
    // Returns:
    //     A System.Linq.Expressions.NewArrayExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.NewArrayInit and
    //     the System.Linq.Expressions.NewArrayExpression.Expressions property set to
    //     the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type or initializers is null.  -or- An element of initializers is null.
    //
    //   System.InvalidOperationException:
    //     The System.Linq.Expressions.Expression.Type property of an element of initializers
    //     represents a type that is not assignable to the type type.
    [Pure]
    public static NewArrayExpression NewArrayInit(Type type, params Expression[] initializers)
    {
      Contract.Requires(type != null);
      Contract.Requires(initializers != null);
      Contract.Ensures(Contract.Result<NewArrayExpression>() != null);
      return default(NewArrayExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents a bitwise
    //     complement operation.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Not and the System.Linq.Expressions.UnaryExpression.Operand
    //     property set to the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression is null.
    //
    //   System.InvalidOperationException:
    //     The unary not operator is not defined for expression.Type.
    [Pure]
    public static UnaryExpression Not(Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents a bitwise
    //     complement operation. The implementing method can be specified.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.UnaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Not and the System.Linq.Expressions.UnaryExpression.Operand
    //     and System.Linq.Expressions.UnaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly one argument.
    //
    //   System.InvalidOperationException:
    //     method is null and the unary not operator is not defined for expression.Type.
    //      -or- expression.Type (or its corresponding non-nullable type if it is a
    //     nullable value type) is not assignable to the argument type of the method
    //     represented by method.
    [Pure]
    public static UnaryExpression Not(Expression expression, MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an inequality
    //     comparison.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.NotEqual and the
    //     System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The inequality operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression NotEqual(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an inequality
    //     comparison. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   liftToNull:
    //     true to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to true;
    //     false to set System.Linq.Expressions.BinaryExpression.IsLiftedToNull to false.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.NotEqual and the
    //     System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     System.Linq.Expressions.BinaryExpression.IsLiftedToNull, and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the inequality operator is not defined for left.Type and
    //     right.Type.
    [Pure]
    public static BinaryExpression NotEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a bitwise
    //     OR operation.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Or and the System.Linq.Expressions.BinaryExpression.Left
    //     and System.Linq.Expressions.BinaryExpression.Right properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The bitwise OR operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression Or(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a bitwise
    //     OR operation. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Or and the System.Linq.Expressions.BinaryExpression.Left,
    //     System.Linq.Expressions.BinaryExpression.Right, and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the bitwise OR operator is not defined for left.Type and
    //     right.Type.
    [Pure]
    public static BinaryExpression Or(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a conditional
    //     OR operation that evaluates the second operand only if it has to.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.OrElse and the System.Linq.Expressions.BinaryExpression.Left
    //     and System.Linq.Expressions.BinaryExpression.Right properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The bitwise OR operator is not defined for left.Type and right.Type.  -or-
    //     left.Type and right.Type are not the same Boolean type.
    [Pure]
    public static BinaryExpression OrElse(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a conditional
    //     OR operation that evaluates the second operand only if it has to. The implementing
    //     method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.OrElse and the System.Linq.Expressions.BinaryExpression.Left,
    //     System.Linq.Expressions.BinaryExpression.Right, and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the bitwise OR operator is not defined for left.Type and
    //     right.Type.  -or- method is null and left.Type and right.Type are not the
    //     same Boolean type.
    [Pure]
    public static BinaryExpression OrElse(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.ParameterExpression.
    //
    // Parameters:
    //   type:
    //     A System.Type to set the System.Linq.Expressions.Expression.Type property
    //     equal to.
    //
    //   name:
    //     The value to set the System.Linq.Expressions.ParameterExpression.Name property
    //     equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.ParameterExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Parameter and the
    //     System.Linq.Expressions.Expression.Type and System.Linq.Expressions.ParameterExpression.Name
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type is null.
    [Pure]
    public static ParameterExpression Parameter(Type type, string name)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<ParameterExpression>() != null);
      return default(ParameterExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents raising
    //     a number to a power.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Power and the System.Linq.Expressions.BinaryExpression.Left
    //     and System.Linq.Expressions.BinaryExpression.Right properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The exponentiation operator is not defined for left.Type and right.Type.
    //      -or- left.Type and/or right.Type are not System.Double.
    [Pure]
    public static BinaryExpression Power(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents raising
    //     a number to a power. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Power and the System.Linq.Expressions.BinaryExpression.Left,
    //     System.Linq.Expressions.BinaryExpression.Right, and System.Linq.Expressions.BinaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the exponentiation operator is not defined for left.Type
    //     and right.Type.  -or- method is null and left.Type and/or right.Type are
    //     not System.Double.
    [Pure]
    public static BinaryExpression Power(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberExpression that represents accessing
    //     a property by using a property accessor method.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.MemberExpression.Expression
    //     property equal to.
    //
    //   propertyAccessor:
    //     The System.Reflection.MethodInfo that represents a property accessor method.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.MemberAccess, the
    //     System.Linq.Expressions.MemberExpression.Expression property set to expression
    //     and the System.Linq.Expressions.MemberExpression.Member property set to the
    //     System.Reflection.PropertyInfo that represents the property accessed in propertyAccessor.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     propertyAccessor is null.  -or- The method that propertyAccessor represents
    //     is not static (Shared in Visual Basic) and expression is null.
    //
    //   System.ArgumentException:
    //     expression.Type is not assignable to the declaring type of the method represented
    //     by propertyAccessor.  -or- The method that propertyAccessor represents is
    //     not a property accessor method.
    [Pure]
    public static MemberExpression Property(Expression expression, MethodInfo propertyAccessor)
    {
      Contract.Requires(propertyAccessor != null);
      Contract.Ensures(Contract.Result<MemberExpression>() != null);
      return default(MemberExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberExpression that represents accessing
    //     a property.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.MemberExpression.Expression
    //     property equal to.
    //
    //   property:
    //     The System.Reflection.PropertyInfo to set the System.Linq.Expressions.MemberExpression.Member
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.MemberAccess and
    //     the System.Linq.Expressions.MemberExpression.Expression and System.Linq.Expressions.MemberExpression.Member
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     property is null.  -or- The property that property represents is not static
    //     (Shared in Visual Basic) and expression is null.
    //
    //   System.ArgumentException:
    //     expression.Type is not assignable to the declaring type of the property that
    //     property represents.
    [Pure]
    public static MemberExpression Property(Expression expression, PropertyInfo property)
    {
      Contract.Requires(property != null);
      Contract.Ensures(Contract.Result<MemberExpression>() != null);
      return default(MemberExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberExpression that represents accessing
    //     a property given the name of the property.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression whose System.Linq.Expressions.Expression.Type
    //     contains a property named propertyName.
    //
    //   propertyName:
    //     The name of a property.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.MemberAccess, the
    //     System.Linq.Expressions.MemberExpression.Expression property set to expression,
    //     and the System.Linq.Expressions.MemberExpression.Member property set to the
    //     System.Reflection.PropertyInfo that represents the property denoted by propertyName.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression or propertyName is null.
    //
    //   System.ArgumentException:
    //     No property named propertyName is defined in expression.Type or its base
    //     types.
    [Pure]
    public static MemberExpression Property(Expression expression, string propertyName)
    {
      Contract.Requires(propertyName != null);
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<MemberExpression>() != null);
      return default(MemberExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.MemberExpression that represents accessing
    //     a property or field given the name of the property or field.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression whose System.Linq.Expressions.Expression.Type
    //     contains a property or field named propertyOrFieldName.
    //
    //   propertyOrFieldName:
    //     The name of a property or field.
    //
    // Returns:
    //     A System.Linq.Expressions.MemberExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.MemberAccess, the
    //     System.Linq.Expressions.MemberExpression.Expression property set to expression,
    //     and the System.Linq.Expressions.MemberExpression.Member property set to the
    //     System.Reflection.PropertyInfo or System.Reflection.FieldInfo that represents
    //     the property or field denoted by propertyOrFieldName.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression or propertyOrFieldName is null.
    //
    //   System.ArgumentException:
    //     No property or field named propertyOrFieldName is defined in expression.Type
    //     or its base types.
    [Pure]
    public static MemberExpression PropertyOrField(Expression expression, string propertyOrFieldName)
    {
      Contract.Requires(expression != null);
      Contract.Requires(propertyOrFieldName != null);
      Contract.Ensures(Contract.Result<MemberExpression>() != null);
      return default(MemberExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents an expression
    //     that has a constant value of type System.Linq.Expressions.Expression.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Quote and the System.Linq.Expressions.UnaryExpression.Operand
    //     property set to the specified value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression is null.
    [Pure]
    public static UnaryExpression Quote(Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a bitwise
    //     right-shift operation.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.RightShift and the
    //     System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The right-shift operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression RightShift(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents a bitwise
    //     right-shift operation. The implementing method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.RightShift and the
    //     System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the right-shift operator is not defined for left.Type
    //     and right.Type.
    [Pure]
    public static BinaryExpression RightShift(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     subtraction operation that does not have overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Subtract and the
    //     System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The subtraction operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression Subtract(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     subtraction operation that does not have overflow checking. The implementing
    //     method can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.Subtract and the
    //     System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the subtraction operator is not defined for left.Type
    //     and right.Type.
    [Pure]
    public static BinaryExpression Subtract(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     subtraction operation that has overflow checking.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.SubtractChecked
    //     and the System.Linq.Expressions.BinaryExpression.Left and System.Linq.Expressions.BinaryExpression.Right
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.InvalidOperationException:
    //     The subtraction operator is not defined for left.Type and right.Type.
    [Pure]
    public static BinaryExpression SubtractChecked(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.BinaryExpression that represents an arithmetic
    //     subtraction operation that has overflow checking. The implementing method
    //     can be specified.
    //
    // Parameters:
    //   left:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Left
    //     property equal to.
    //
    //   right:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.BinaryExpression.Right
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.BinaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.BinaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.SubtractChecked
    //     and the System.Linq.Expressions.BinaryExpression.Left, System.Linq.Expressions.BinaryExpression.Right,
    //     and System.Linq.Expressions.BinaryExpression.Method properties set to the
    //     specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     left or right is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly two arguments.
    //
    //   System.InvalidOperationException:
    //     method is null and the subtraction operator is not defined for left.Type
    //     and right.Type.
    [Pure]
    public static BinaryExpression SubtractChecked(Expression left, Expression right, MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<BinaryExpression>() != null);
      return default(BinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents an explicit
    //     reference or boxing conversion where null is supplied if the conversion fails.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    //   type:
    //     A System.Type to set the System.Linq.Expressions.Expression.Type property
    //     equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.TypeAs and the System.Linq.Expressions.UnaryExpression.Operand
    //     and System.Linq.Expressions.Expression.Type properties set to the specified
    //     values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression or type is null.
    [Pure]
    public static UnaryExpression TypeAs(Expression expression, Type type)
    {
      Contract.Requires(expression != null);
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.TypeBinaryExpression.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.TypeBinaryExpression.Expression
    //     property equal to.
    //
    //   type:
    //     A System.Type to set the System.Linq.Expressions.TypeBinaryExpression.TypeOperand
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.TypeBinaryExpression for which the System.Linq.Expressions.Expression.NodeType
    //     property is equal to System.Linq.Expressions.ExpressionType.TypeIs and for
    //     which the System.Linq.Expressions.TypeBinaryExpression.Expression and System.Linq.Expressions.TypeBinaryExpression.TypeOperand
    //     properties are set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression or type is null.
    [Pure]
    public static TypeBinaryExpression TypeIs(Expression expression, Type type)
    {
      Contract.Requires(expression != null);
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<TypeBinaryExpression>() != null);
      return default(TypeBinaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents a unary
    //     plus operation.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.UnaryPlus and the
    //     System.Linq.Expressions.UnaryExpression.Operand property set to the specified
    //     value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression is null.
    //
    //   System.InvalidOperationException:
    //     The unary plus operator is not defined for expression.Type.
    [Pure]
    public static UnaryExpression UnaryPlus(Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.UnaryExpression that represents a unary
    //     plus operation. The implementing method can be specified.
    //
    // Parameters:
    //   expression:
    //     An System.Linq.Expressions.Expression to set the System.Linq.Expressions.UnaryExpression.Operand
    //     property equal to.
    //
    //   method:
    //     A System.Reflection.MethodInfo to set the System.Linq.Expressions.UnaryExpression.Method
    //     property equal to.
    //
    // Returns:
    //     A System.Linq.Expressions.UnaryExpression that has the System.Linq.Expressions.Expression.NodeType
    //     property equal to System.Linq.Expressions.ExpressionType.UnaryPlus and the
    //     System.Linq.Expressions.UnaryExpression.Operand and System.Linq.Expressions.UnaryExpression.Method
    //     properties set to the specified values.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     expression is null.
    //
    //   System.ArgumentException:
    //     method is not null and the method it represents returns void, is not static
    //     (Shared in Visual Basic), or does not take exactly one argument.
    //
    //   System.InvalidOperationException:
    //     method is null and the unary plus operator is not defined for expression.Type.
    //      -or- expression.Type (or its corresponding non-nullable type if it is a
    //     nullable value type) is not assignable to the argument type of the method
    //     represented by method.
    [Pure]
    public static UnaryExpression UnaryPlus(Expression expression, MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<UnaryExpression>() != null);
      return default(UnaryExpression);
    }
#if  NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    //
    // Summary:
    //     Creates a System.Linq.Expressions.ParameterExpression node that can be used
    //     to identify a parameter or a variable in an expression tree.
    //
    // Parameters:
    //   type:
    //     The type of the parameter or variable.
    //
    // Returns:
    //     A System.Linq.Expressions.ParameterExpression node with the specified name
    //     and type
    public static ParameterExpression Variable(Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<ParameterExpression>() != null);
      return default(ParameterExpression);
    }
    //
    // Summary:
    //     Creates a System.Linq.Expressions.ParameterExpression node that can be used
    //     to identify a parameter or a variable in an expression tree.
    //
    // Parameters:
    //   type:
    //     The type of the parameter or variable.
    //
    //   name:
    //     The name of the parameter or variable. This name is used for debugging or
    //     printing purpose only.
    //
    // Returns:
    //     A System.Linq.Expressions.ParameterExpression node with the specified name
    //     and type.
    public static ParameterExpression Variable(Type type, string name)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<ParameterExpression>() != null);
      return default(ParameterExpression);
    }
#endif
  }

  public sealed class Expression<TDelegate>
  {
    private Expression() { }

#if !SILVERLIGHT
    public TDelegate Compile()
    {
      Contract.Ensures(!ReferenceEquals(Contract.Result<TDelegate>(), null));
      return default(TDelegate);
    }
#endif

  }

}
