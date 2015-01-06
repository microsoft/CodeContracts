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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;
using System.Diagnostics.Contracts;

namespace System.Linq
{
  // Summary:
  //     Provides functionality to evaluate queries against a specific data source
  //     wherein the type of the data is known.
  //
  // Type parameters:
  //   T:
  //     The type of the data in the data source.
  public interface IQueryable<T> 
  {
  }

  // Summary:
  //     Provides functionality to evaluate queries against a specific data source
  //     wherein the type of the data is not specified.
  [ContractClass(typeof(CQueryable))]
  public interface IQueryable : IEnumerable
  {
    // Summary:
    //     Gets the type of the element(s) that are returned when the expression tree
    //     associated with this instance of System.Linq.IQueryable is executed.
    //
    // Returns:
    //     A System.Type that represents the type of the element(s) that are returned
    //     when the expression tree associated with this object is executed.
    Type ElementType { get; }
    //
    // Summary:
    //     Gets the expression tree that is associated with the instance of System.Linq.IQueryable.
    //
    // Returns:
    //     The System.Linq.Expressions.Expression that is associated with this instance
    //     of System.Linq.IQueryable.
    Expression Expression { get; }
    //
    // Summary:
    //     Gets the query provider that is associated with this data source.
    //
    // Returns:
    //     The System.Linq.IQueryProvider that is associated with this data source.
    IQueryProvider Provider { get; }
  }

  [ContractClassFor(typeof(IQueryable))]
  abstract class CQueryable : IQueryable
  {
    #region IQueryable Members

    Type IQueryable.ElementType
    {
      get {
        Contract.Ensures(Contract.Result<Type>() != null);

        throw new NotImplementedException();
      }
    }

    Expression IQueryable.Expression
    {
      get {
        Contract.Ensures(Contract.Result<Expression>() != null);

        throw new NotImplementedException();
      }
    }

    IQueryProvider IQueryable.Provider
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryProvider>() != null);
        throw new NotImplementedException();
      }
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion
  }

}

#endif