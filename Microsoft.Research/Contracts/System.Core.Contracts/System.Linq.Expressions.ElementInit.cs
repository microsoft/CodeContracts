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
  //     Represents an initializer for a single element of an System.Collections.IEnumerable
  //     collection.
  public sealed class ElementInit
  {
    private ElementInit() { }

    // Summary:
    //     Gets the instance method that is used to add an element to an System.Collections.IEnumerable
    //     collection.
    //
    // Returns:
    //     A System.Reflection.MethodInfo that represents an instance method that adds
    //     an element to a collection.
    public MethodInfo AddMethod
    {
      get
      {
        Contract.Ensures(Contract.Result<MethodInfo>() != null);
        return default(MethodInfo);
      }
    }
    //
    // Summary:
    //     Gets the collection of arguments that are passed to a method that adds an
    //     element to an System.Collections.IEnumerable collection.
    //
    // Returns:
    //     A System.Collections.ObjectModel.ReadOnlyCollection<T> of System.Linq.Expressions.Expression
    //     objects that represent the arguments for a method that adds an element to
    //     a collection.
    public ReadOnlyCollection<Expression> Arguments
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyCollection<Expression>>() != null);
        return default(ReadOnlyCollection<Expression>);
      }
    }

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    [Pure]
    public ElementInit Update(IEnumerable<Expression> arguments)
    {
      Contract.Requires(arguments != null);
      Contract.Ensures(Contract.Result<ElementInit>() != null);
      return default(ElementInit);
    }
#endif
  }
}