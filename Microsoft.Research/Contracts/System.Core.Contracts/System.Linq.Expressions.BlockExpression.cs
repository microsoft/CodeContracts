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

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
  // Summary:
  //     Represents a block that contains a sequence of expressions where variables
  //     can be defined.
  public class BlockExpression : Expression {

    internal BlockExpression(){}

    // Summary:
    //     Gets the expressions in this block.
    //
    // Returns:
    //     The read-only collection containing all the expressions in this block.
    public ReadOnlyCollection<Expression> Expressions
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyCollection<Expression>>() != null);
        return default(ReadOnlyCollection<Expression>);
      }
    }
    //
    // Summary:
    //     Returns the node type of this expression. Extension nodes should return System.Linq.Expressions.ExpressionType.Extension
    //     when overriding this method.
    //
    // Returns:
    //     The System.Linq.Expressions.ExpressionType of the expression.
    public override sealed ExpressionType NodeType
    {
      get
      {
        return default(ExpressionType);
        
      }
    }
    //
    // Summary:
    //     Gets the last expression in this block.
    //
    // Returns:
    //     The System.Linq.Expressions.Expression object representing the last expression
    //     in this block.
    public Expression Result
    {
      get
      {
        Contract.Ensures(Contract.Result<Expression>() != null);
        return default(Expression);
      }
    }
    //
    // Summary:
    //     Gets the static type of the expression that this System.Linq.Expressions.Expression
    //     represents.
    //
    // Returns:
    //     The System.Linq.Expressions.BlockExpression.Type that represents the static
    //     type of the expression.
    public override Type Type
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);
        return default(Type);
      }
    }
    //
    // Summary:
    //     Gets the variables defined in this block.
    //
    // Returns:
    //     The read-only collection containing all the variables defined in this block.
    public ReadOnlyCollection<ParameterExpression> Variables
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyCollection<ParameterExpression>>() != null);
        return default(ReadOnlyCollection<ParameterExpression>);
      }
    }

    // Summary:
    //     Dispatches to the specific visit method for this node type. For example,
    //     System.Linq.Expressions.MethodCallExpression calls the System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression).
    //
    // Parameters:
    //   visitor:
    //     The visitor to visit this node with.
    //
    // Returns:
    //     The result of visiting this node.
    protected internal override Expression Accept(ExpressionVisitor visitor)
    {
      Contract.Requires(visitor != null);
      Contract.Ensures(Contract.Result<Expression>() != null);
      return default(Expression);
    }
    //
    // Summary:
    //     Creates a new expression that is like this one, but using the supplied children.
    //     If all of the children are the same, it will return this expression.
    //
    // Parameters:
    //   variables:
    //     The System.Linq.Expressions.BlockExpression.Variables property of the result.
    //
    //   expressions:
    //     The System.Linq.Expressions.BlockExpression.Expressions property of the result.
    //
    // Returns:
    //     This expression if no children changed, or an expression with the updated
    //     children.
    [Pure]
    public BlockExpression Update(IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
    {
      Contract.Requires(variables != null);
      Contract.Requires(expressions != null);
      Contract.Ensures(Contract.Result<BlockExpression>() != null);
      return default(BlockExpression);
    }
  }
}
#endif
