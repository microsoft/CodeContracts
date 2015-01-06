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

#if !SILVERLIGHT_4_0_WP

using System;
using System.Linq.Expressions;

namespace System.Linq
{
  // Summary:
  //     Defines methods to create and execute queries that are described by an System.Linq.IQueryable
  //     object.
  public interface IQueryProvider
  {
    // Summary:
    //     Constructs an System.Linq.IQueryable object that can evaluate the query represented
    //     by a specified expression tree.
    //
    // Parameters:
    //   expression:
    //     An expression tree that represents a LINQ query.
    //
    // Returns:
    //     An System.Linq.IQueryable that can evaluate the query represented by the
    //     specified expression tree.
    IQueryable CreateQuery(Expression expression);
    //
    // Summary:
    //     Constructs an System.Linq.IQueryable<T> object that can evaluate the query
    //     represented by a specified expression tree.
    //
    // Parameters:
    //   expression:
    //     An expression tree that represents a LINQ query.
    //
    // Type parameters:
    //   TElement:
    //     The type of the elements of the System.Linq.IQueryable<T> that is returned.
    //
    // Returns:
    //     An System.Linq.IQueryable<T> that can evaluate the query represented by the
    //     specified expression tree.
    IQueryable<TElement> CreateQuery<TElement>(Expression expression);
    //
    // Summary:
    //     Executes the query represented by a specified expression tree.
    //
    // Parameters:
    //   expression:
    //     An expression tree that represents a LINQ query.
    //
    // Returns:
    //     The value that results from executing the specified query.
    object Execute(Expression expression);
    //
    // Summary:
    //     Executes the strongly-typed query represented by a specified expression tree.
    //
    // Parameters:
    //   expression:
    //     An expression tree that represents a LINQ query.
    //
    // Type parameters:
    //   TResult:
    //     The type of the value that results from executing the query.
    //
    // Returns:
    //     The value that results from executing the specified query.
    TResult Execute<TResult>(Expression expression);
  }
}

#endif