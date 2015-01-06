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
using System.Collections.ObjectModel;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Linq.Expressions
{
  // Summary:
  //     Describes a lambda expression.
  public class LambdaExpression : Expression
  {
    private LambdaExpression() { }

    // Summary:
    //     Gets the body of the lambda expression.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression that represents the body of the lambda
    //     expression.
    public Expression Body
    {
      get
      {
        Contract.Ensures(Contract.Result<Expression>() != null);
        return default(Expression);
      }
    }
    //
    // Summary:
    //     Gets the parameters of the lambda expression.
    //
    // Returns:
    //     A System.Collections.ObjectModel.ReadOnlyCollection<T> of System.Linq.Expressions.ParameterExpression
    //     objects that represent the parameters of the lambda expression.
    public ReadOnlyCollection<ParameterExpression> Parameters
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyCollection<ParameterExpression>>() != null);
        return default(ReadOnlyCollection<ParameterExpression>);
      }
    }

#if !SILVERLIGHT_4_0_WP
    // Summary:
    //     Produces a delegate that represents the lambda expression.
    //
    // Returns:
    //     A System.Delegate that, when it is executed, has the behavior described by
    //     the semantics of the System.Linq.Expressions.LambdaExpression.
    public Delegate Compile()
    {
      Contract.Ensures(Contract.Result<Delegate>() != null);
      return default(Delegate);
    }
#endif
  }
}
