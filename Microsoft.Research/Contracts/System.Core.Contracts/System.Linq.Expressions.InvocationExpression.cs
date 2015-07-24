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
using System.Text;

namespace System.Linq.Expressions
{
  // Summary:
  //     Represents an expression that applies a delegate or lambda expression to
  //     a list of argument expressions.
  public sealed class InvocationExpression : Expression
  {
    private InvocationExpression() { }

    // Summary:
    //     Gets the arguments that the delegate is applied to.
    //
    // Returns:
    //     A System.Collections.ObjectModel.ReadOnlyCollection<T> of System.Linq.Expressions.Expression
    //     objects which represent the arguments that the delegate is applied to.
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
    //     Gets the delegate or lambda expression to be applied.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression that represents the delegate to be
    //     applied.
    public Expression Expression
    {
      get
      {
        Contract.Ensures(Contract.Result<Expression>() != null);
        return default(Expression);
      }
    }

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    [Pure]
    public InvocationExpression Update(Expression expression, IEnumerable<Expression> arguments)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<InvocationExpression>() != null);
      return default(InvocationExpression);
    }
#endif
  }
}
