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
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Linq.Expressions
{
  // Summary:
  //     Represents an expression that has a conditional operator.
  public sealed class ConditionalExpression : Expression
  {
    private ConditionalExpression() { }

    // Summary:
    //     Gets the expression to execute if the test evaluates to false.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression that represents the expression to execute
    //     if the test is false.
    public Expression IfFalse
    {
      get
      {
        Contract.Ensures(Contract.Result<Expression>() != null);
        return default(Expression);
      }
    }
    //
    // Summary:
    //     Gets the expression to execute if the test evaluates to true.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression that represents the expression to execute
    //     if the test is true.
    public Expression IfTrue
    {
      get
      {
        Contract.Ensures(Contract.Result<Expression>() != null);
        return default(Expression);
      }
    }
    //
    // Summary:
    //     Gets the test of the conditional operation.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression that represents the test of the conditional
    //     operation.
    public Expression Test
    {
      get
      {
        Contract.Ensures(Contract.Result<Expression>() != null);
        return default(Expression);
      }
    }

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    //
    // Summary:
    //     Creates a new expression that is like this one, but using the supplied children.
    //     If all of the children are the same, it will return this expression
    //
    // Parameters:
    //   test:
    //     The System.Linq.Expressions.ConditionalExpression.Test property of the result.
    //
    //   ifTrue:
    //     The System.Linq.Expressions.ConditionalExpression.IfTrue property of the result.
    //
    //   ifFalse:
    //     The System.Linq.Expressions.ConditionalExpression.IfFalse property of the result.
    //
    // Returns:
    //     This expression if no children changed, or an expression with the updated children.
    [Pure]
    public ConditionalExpression Update(Expression test, Expression ifTrue, Expression ifFalse)
    {
        Contract.Requires(test != null);
        Contract.Requires(ifTrue != null);
        Contract.Requires(ifFalse != null);
        Contract.Ensures(Contract.Result<ConditionalExpression>() != null);
        return default(ConditionalExpression);
    }
#endif
  }
}
