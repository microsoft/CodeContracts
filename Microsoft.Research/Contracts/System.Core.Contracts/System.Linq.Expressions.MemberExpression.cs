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
  //     Represents accessing a field or property.
  public sealed class MemberExpression : Expression
  {
    private MemberExpression() { }
    // Summary:
    //     Gets the containing object of the field or property.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression that represents the containing object
    //     of the field or property.
    public Expression Expression
    {
      get
      {
        // can be null for static fields
        return default(Expression);
      }
    }
    //
    // Summary:
    //     Gets the field or property to be accessed.
    //
    // Returns:
    //     The System.Reflection.MemberInfo that represents the field or property to
    //     be accessed.
    public MemberInfo Member
    {
      get
      {
        Contract.Ensures(Contract.Result<MemberInfo>() != null);
        return default(MemberInfo);
      }
    }

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    //
    // Summary:
    //     Creates a new expression that is like this one, but using the supplied children.
    //     If all of the children are the same, it will return this expression.
    //
    // Parameters:
    //   expression:
    //     The System.Linq.Expressions.MemberExpression.Expression property of the result.
    //
    // Returns:
    //     This expression if no children are changed or an expression with the updated
    //     children.
    [Pure]
    public MemberExpression Update(Expression expression)
    {
      Contract.Ensures(Contract.Result<MemberExpression>() != null);
      return default(MemberExpression);
    }
#endif
  }
}
