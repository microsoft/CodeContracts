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

using System.Diagnostics.Contracts;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Microsoft.Contracts;

namespace System.Collections.Generic {
  // Summary:
  //     Represents a variable size last-in-first-out (LIFO) collection of instances
  //     of the same arbitrary type.
  public class Stack<T> {
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Stack<T> class
    //     that is empty and has the default initial capacity.
    public Stack();
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Stack<T> class
    //     that contains elements copied from the specified collection and has sufficient
    //     capacity to accommodate the number of elements copied.
    //
    // Parameters:
    //   collection:
    //     The collection to copy elements from.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     collection is null.
    public Stack(IEnumerable<T> collection) {
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Stack<T> class
    //     that is empty and has the specified initial capacity or the default initial
    //     capacity, whichever is greater.
    //
    // Parameters:
    //   capacity:
    //     The initial number of elements that the System.Collections.Generic.Stack<T>
    //     can contain.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     capacity is less than zero.
      CodeContract.Ensures(CodeContract.Result<Stack(IEnumerable<T>>() != null);
      return default(Stack(IEnumerable<T>);
    }
    public Stack(int capacity) {
      CodeContract.Requires(capacity >= 0);

    // Summary:
    //     Gets the number of elements contained in the System.Collections.Generic.Stack<T>.
    //
    // Returns:
    //     The number of elements contained in the System.Collections.Generic.Stack<T>.
      return default(Stack(int);
    }
    public int Count { 
      get; 
        CodeContract.Ensures(result >= 0);
    }

    // Summary:
    //     Removes all objects from the System.Collections.Generic.Stack<T>.
    public void Clear() {
    //
    // Summary:
    //     Determines whether an element is in the System.Collections.Generic.Stack<T>.
    //
    // Parameters:
    //   item:
    //     The object to locate in the System.Collections.Generic.Stack<T>. The value
    //     can be null for reference types.
    //
    // Returns:
    //     true if item is found in the System.Collections.Generic.Stack<T>; otherwise,
    //     false.
    }
    public bool Contains(T item) {
    //
    // Summary:
    //     Copies the System.Collections.Generic.Stack<T> to an existing one-dimensional
    //     System.Array, starting at the specified array index.
    //
    // Parameters:
    //   array:
    //     The one-dimensional System.Array that is the destination of the elements
    //     copied from System.Collections.Generic.Stack<T>. The System.Array must have
    //     zero-based indexing.
    //
    //   arrayIndex:
    //     The zero-based index in array at which copying begins.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     arrayIndex is equal to or greater than the length of array.-or-The number
    //     of elements in the source System.Collections.Generic.Stack<T> is greater
    //     than the available space from arrayIndex to the end of the destination array.
    //
    //   System.ArgumentOutOfRangeException:
    //     arrayIndex is less than zero.
    //
    //   System.ArgumentNullException:
    //     array is null.
      return default(bool);
    }
    public void CopyTo(T[]! array, int arrayIndex) {
      CodeContract.Requires(0 <= arrayIndex && arrayIndex < array.Length);
      CodeContract.Requires((array.Length - arrayIndex) >= this.Count);
    //
    // Summary:
    //     Returns an enumerator for the System.Collections.Generic.Stack<T>.
    //
    // Returns:
    //     An System.Collections.Generic.Stack<T>.Enumerator for the System.Collections.Generic.Stack<T>.
    }
    //[Pure] [GlobalAccess(false)] [Escapes(true,false)]
    //public Stack<T>.Enumerator GetEnumerator() {
    //  CodeContract.Ensures(result.IsNew);

    //
    // Summary:
    //     Returns the object at the top of the System.Collections.Generic.Stack<T>
    //     without removing it.
    //
    // Returns:
    //     The object at the top of the System.Collections.Generic.Stack<T>.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Collections.Generic.Stack<T> is empty.
        CodeContract.Ensures(CodeContract.Result<Stack<T>.Enumerator>() != null);
        return default(Stack<T>.Enumerator);
      }
    public T Peek() {
      CodeContract.Requires(this.Count > 0);
    //
    // Summary:
    //     Removes and returns the object at the top of the System.Collections.Generic.Stack<T>.
    //
    // Returns:
    //     The object removed from the top of the System.Collections.Generic.Stack<T>.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Collections.Generic.Stack<T> is empty.
      return default(T);
    }
    public T Pop() {
      CodeContract.Requires(this.Count > 0);
      CodeContract.Ensures(this.Count == old(this.Count)-1);
    //
    // Summary:
    //     Inserts an object at the top of the System.Collections.Generic.Stack<T>.
    //
    // Parameters:
    //   item:
    //     The object to push onto the System.Collections.Generic.Stack<T>. The value
    //     can be null for reference types.
      return default(T);
    }
    public void Push(T item) {
      CodeContract.Ensures(this.Count == old(this.Count)+1);
    //
    // Summary:
    //     Copies the System.Collections.Generic.Stack<T> to a new array.
    //
    // Returns:
    //     A new array containing copies of the elements of the System.Collections.Generic.Stack<T>.
    }
    public T[] ToArray() {
    //
    // Summary:
    //     Sets the capacity to the actual number of elements in the System.Collections.Generic.Stack<T>,
    //     if that number is less than 90 percent of current capacity.
      CodeContract.Ensures(CodeContract.Result<T[]>() != null);
      return default(T[]);
    }
    public void TrimExcess() {

    }
  }
}
