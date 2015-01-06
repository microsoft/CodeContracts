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
using System.Collections;
using System.Diagnostics.Contracts;

namespace System.Collections.Generic
{
  // Summary:
  //     Provides a base class for implementations of the System.Collections.Generic.IComparer<T>
  //     generic interface.
  //
  // Type parameters:
  //   T:
  //     The type of objects to compare.
  public abstract class Comparer<T> 
  {
    // Summary:
    //     Returns a default sort order comparer for the type specified by the generic
    //     argument.
    //
    // Returns:
    //     An object that inherits System.Collections.Generic.Comparer<T> and serves
    //     as a sort order comparer for type T.
    public static Comparer<T> Default
    {
      get
      {
        Contract.Ensures(Contract.Result<Comparer<T>>() != null);
        return default(Comparer<T>);
      }
    }

    // Summary:
    //     When overridden in a derived class, performs a comparison of two objects
    //     of the same type and returns a value indicating whether one object is less
    //     than, equal to, or greater than the other.
    //
    // Parameters:
    //   x:
    //     The first object to compare.
    //
    //   y:
    //     The second object to compare.
    //
    // Returns:
    //     Value Condition Less than zero x is less than y.Zero x equals y.Greater than
    //     zero x is greater than y.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     Type T does not implement either the System.IComparable<T> generic interface
    //     or the System.IComparable interface.
    [Pure]
    public abstract int Compare(T x, T y);
  }
}
