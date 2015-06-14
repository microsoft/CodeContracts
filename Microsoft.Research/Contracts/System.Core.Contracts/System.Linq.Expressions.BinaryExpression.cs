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
using System.Reflection;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Linq.Expressions
{
  // Summary:
  //     Represents an expression that has a binary operator.
  public sealed class BinaryExpression : Expression
  {
    private BinaryExpression() { }

    // Summary:
    //     Gets the type conversion function that is used by a coalescing operation.
    //
    // Returns:
    //     A System.Linq.Expressions.LambdaExpression that represents a type conversion
    //     function.
    public LambdaExpression Conversion
    {
      get
      {
        Contract.Ensures(Contract.Result<LambdaExpression>() != null);
        return default(LambdaExpression);
      }
    }
    //
    // Summary:
    //     Gets a value that indicates whether the expression tree node represents a
    //     lifted call to an operator.
    //
    // Returns:
    //     true if the node represents a lifted call; otherwise, false.
    extern public bool IsLifted { get; }
    //
    // Summary:
    //     Gets a value that indicates whether the expression tree node represents a
    //     lifted call to an operator whose return type is lifted to a nullable type.
    //
    // Returns:
    //     true if the operator's return type is lifted to a nullable type; otherwise,
    //     false.
    extern public bool IsLiftedToNull { get; }
    //
    // Summary:
    //     Gets the left operand of the binary operation.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression that represents the left operand of
    //     the binary operation.
    public Expression Left
    {
      get
      {
        Contract.Ensures(Contract.Result<Expression>() != null);
        return default(Expression);
      }
    }
    //
    // Summary:
    //     Gets the implementing method for the binary operation.
    //
    // Returns:
    //     The System.Reflection.MethodInfo that represents the implementing method.
    public MethodInfo Method
    {
      get
      {
        return default(MethodInfo);
      }
    }
    //
    // Summary:
    //     Gets the right operand of the binary operation.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression that represents the right operand of
    //     the binary operation.
    public Expression Right
    {
      get
      {
        Contract.Ensures(Contract.Result<Expression>() != null);
        return default(Expression);
      }
    }
  }
}
