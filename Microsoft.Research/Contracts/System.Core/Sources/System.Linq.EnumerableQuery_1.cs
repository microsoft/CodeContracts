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

// File System.Linq.EnumerableQuery_1.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Linq
{
  public partial class EnumerableQuery<T> : EnumerableQuery, IOrderedQueryable<T>, IQueryable<T>, IOrderedQueryable, IQueryable, IQueryProvider, IEnumerable<T>, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public EnumerableQuery(System.Linq.Expressions.Expression expression)
    {
    }

    public EnumerableQuery(IEnumerable<T> enumerable)
    {
    }

    IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
    {
      return default(IEnumerator<T>);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    IQueryable<S> System.Linq.IQueryProvider.CreateQuery<S>(System.Linq.Expressions.Expression expression)
    {
      return default(IQueryable<S>);
    }

    IQueryable System.Linq.IQueryProvider.CreateQuery(System.Linq.Expressions.Expression expression)
    {
      return default(IQueryable);
    }

    Object System.Linq.IQueryProvider.Execute(System.Linq.Expressions.Expression expression)
    {
      return default(Object);
    }

    S System.Linq.IQueryProvider.Execute<S>(System.Linq.Expressions.Expression expression)
    {
      return default(S);
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    Type System.Linq.IQueryable.ElementType
    {
      get
      {
        return default(Type);
      }
    }

    System.Linq.Expressions.Expression System.Linq.IQueryable.Expression
    {
      get
      {
        return default(System.Linq.Expressions.Expression);
      }
    }

    IQueryProvider System.Linq.IQueryable.Provider
    {
      get
      {
        return default(IQueryProvider);
      }
    }
    #endregion
  }
}
