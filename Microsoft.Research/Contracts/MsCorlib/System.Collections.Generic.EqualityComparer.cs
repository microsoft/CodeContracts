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
  //     Provides a base class for implementations of the System.Collections.Generic.IEqualityComparer<T>
  //     generic interface.
  //
  // Type parameters:
  //   T:
  //     The type of objects to compare.
  public abstract class EqualityComparer<T>
  {
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.EqualityComparer<T>
    //     class.
    extern protected EqualityComparer();

    // Summary:
    //     Returns a default equality comparer for the type specified by the generic
    //     argument.
    //
    // Returns:
    //     The default instance of the System.Collections.Generic.EqualityComparer<T>
    //     class for type T.
    public static EqualityComparer<T> Default
    {
      get
      {
        Contract.Ensures(Contract.Result<EqualityComparer<T>>() != null);
        return default(EqualityComparer<T>);
      }
    }

    // Summary:
    //     When overridden in a derived class, determines whether two objects of type
    //     T are equal.
    //
    // Parameters:
    //   x:
    //     The first object to compare.
    //
    //   y:
    //     The second object to compare.
    //
    // Returns:
    //     true if the specified objects are equal; otherwise, false.
    [Pure]
    public abstract bool Equals(T x, T y);
    //
    // Summary:
    //     When overridden in a derived class, serves as a hash function for the specified
    //     object for hashing algorithms and data structures, such as a hash table.
    //
    // Parameters:
    //   obj:
    //     The object for which to get a hash code.
    //
    // Returns:
    //     A hash code for the specified object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The type of obj is a reference type and obj is null.
    [Pure]
    public abstract int GetHashCode(T obj);
  }
}
