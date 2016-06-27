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
  //     Represents a constructor call.
  public sealed class NewExpression : Expression
  {
    private NewExpression() { }

    // Summary:
    //     Gets the arguments to the constructor.
    //
    // Returns:
    //     A collection of System.Linq.Expressions.Expression objects that represent
    //     the arguments to the constructor.
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
    //     Gets the called constructor.
    //
    // Returns:
    //     The System.Reflection.ConstructorInfo that represents the called constructor.
    public ConstructorInfo Constructor
    {
      get
      {
        Contract.Ensures(Contract.Result<ConstructorInfo>() != null);
        return default(ConstructorInfo);
      }
    }
    //
    // Summary:
    //     Gets the members that can retrieve the values of the fields that were initialized
    //     with constructor arguments.
    //
    // Returns:
    //     A collection of System.Reflection.MemberInfo objects that represent the members
    //     that can retrieve the values of the fields that were initialized with constructor
    //     arguments.
    public ReadOnlyCollection<MemberInfo> Members
    {
      get
      {
        return default(ReadOnlyCollection<MemberInfo>);
      }
    }

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    //
    // Summary:
    //     Creates a new expression that is like this one, but using the supplied children.
    //     If all of the children are the same, it will return this expression.
    //
    // Parameters:
    //   arguments:
    //     The System.Linq.Expressions.NewExpression.Arguments property of the result.
    //
    // Returns:
    //     This expression if no children are changed or an expression with the updated
    //     children.
    [Pure]
    public NewExpression Update(IEnumerable<Expression> arguments)
    {
      Contract.Ensures(Contract.Result<NewExpression>() != null);
      return default(NewExpression);
    }
#endif
  }
}
