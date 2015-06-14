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


using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace System
{
  public struct ArraySegment<T>  //: IList<T>, ICollection<T>, /*IReadOnlyList<T>, IReadOnlyCollection<T>, */ IEnumerable<T>, IEnumerable
  {
    public ArraySegment(T[] array)
    {
      Contract.Requires(array != null);
    }
    public ArraySegment(T[] array, int offset, int count)
    {
      Contract.Requires(array != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(array.Length-offset >= count);
    }

    // public static bool operator !=(ArraySegment<T> a, ArraySegment<T> b);
    //public static bool operator ==(ArraySegment<T> a, ArraySegment<T> b);

    public T[] Array
    {
      get
      {
        Contract.Ensures(Contract.Result<T[]>() != null);
        return default(T[]);
      }
    }

    //
    // Summary:
    //     Gets the position of the first element in the range delimited by the array
    //     segment, relative to the start of the original array.
    //
    // Returns:
    //     The position of the first element in the range delimited by the System.ArraySegment<T>,
    //     relative to the start of the original array.
    public int Offset {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return 0;
      }
    }

    // ignored
    public bool Equals(ArraySegment<T> obj) { return false; }
  }
}
