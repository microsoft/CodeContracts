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
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Text;

namespace System.Linq.Expressions
{
  // Summary:
  //     Represents calling a method.
  public sealed class MethodCallExpression : Expression
  {
    private MethodCallExpression() { }

    // Summary:
    //     Gets the arguments to the called method.
    //
    // Returns:
    //     A System.Collections.ObjectModel.ReadOnlyCollection<T> of System.Linq.Expressions.Expression
    //     objects which represent the arguments to the called method.
    public ReadOnlyCollection<Expression> Arguments
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyCollection<Expression>>() != null);
        return default(ReadOnlyCollection<Expression>);
      }
    }

    //
    // Summary:
    //     Gets the called method.
    //
    // Returns:
    //     The System.Reflection.MethodInfo that represents the called method.
    public MethodInfo Method
    {
      get
      {
        Contract.Ensures(Contract.Result<MethodInfo>() != null);
        Contract.Ensures(!Contract.Result<MethodInfo>().IsGenericMethodDefinition);
        Contract.Ensures(!Contract.Result<MethodInfo>().ContainsGenericParameters);
        return default(MethodInfo);
      }
    }
    //
    // Summary:
    //     Gets the receiving object of the method.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression that represents the receiving object
    //     of the method.
    public Expression Object
    {
      get
      {
        Contract.Ensures(Method.IsStatic || Contract.Result<Expression>() != null);
        Contract.Ensures(!Method.IsStatic || Contract.Result<Expression>() == null);
        return default(Expression);
      }
    }

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    //
    // Summary:
    //     Creates a new expression that is like this one, but using the supplied children.
    //     If all of the children are the same, it will return this expression.
    //
    // Parameters:
    //   object:
    //     The System.Linq.Expressions.MethodCallExpression.Object property of the result.
    //
    //   arguments:
    //     The System.Linq.Expressions.MethodCallExpression.Arguments property of the result.
    //
    // Returns:
    //     This expression if no children are changed or an expression with the updated
    //     children.
    [Pure]
    public MethodCallExpression Update(Expression @object, IEnumerable<Expression> arguments)
    {
      Contract.Requires(Method.IsStatic || @object != null);
      Contract.Requires(!Method.IsStatic || @object == null);
      Contract.Ensures(Contract.Result<MethodCallExpression>() != null);
      return default(MethodCallExpression);
    }
#endif
  }
}