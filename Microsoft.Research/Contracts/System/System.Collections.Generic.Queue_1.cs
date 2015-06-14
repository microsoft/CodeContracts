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

//#if !SILVERLIGHT_4_0_WP && !SILVERLIGHT_3_0 && !SILVERLIGHT_4_0 && !SILVERLIGHT_5_0 && !NETFRAMEWORK_3_5
#if true
#region Assembly System.dll, v4.0.0.0
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.dll
#endregion

using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
  // Summary:
  //     Represents a first-in, first-out collection of objects.
  //
  // Type parameters:
  //   T:
  //     Specifies the type of elements in the queue.
  public class Queue<T> : IEnumerable<T>, ICollection, IEnumerable
  {
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Queue<T> class
    //     that is empty and has the default initial capacity.
    public Queue() {
      Contract.Ensures(this.Count == 0);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Queue<T> class
    //     that contains elements copied from the specified collection and has sufficient
    //     capacity to accommodate the number of elements copied.
    //
    // Parameters:
    //   collection:
    //     The collection whose elements are copied to the new System.Collections.Generic.Queue<T>.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     collection is null.
    public Queue(IEnumerable<T> collection)
    {
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.Queue<T> class
    //     that is empty and has the specified initial capacity.
    //
    // Parameters:
    //   capacity:
    //     The initial number of elements that the System.Collections.Generic.Queue<T>
    //     can contain.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     capacity is less than zero.
    public Queue(int capacity)
    {
      Contract.Ensures(this.Count == 0);
    }

    // Summary:
    //     Gets the number of elements contained in the System.Collections.Generic.Queue<T>.
    //
    // Returns:
    //     The number of elements contained in the System.Collections.Generic.Queue<T>.
    public int Count
    {
      get
      {
        return default(int);
      }
    }

    // Summary:
    //     Removes all objects from the System.Collections.Generic.Queue<T>.
    public void Clear() {
      Contract.Ensures(this.Count == 0);
    }
    //
    // Summary:
    //     Determines whether an element is in the System.Collections.Generic.Queue<T>.
    //
    // Parameters:
    //   item:
    //     The object to locate in the System.Collections.Generic.Queue<T>. The value
    //     can be null for reference types.
    //
    // Returns:
    //     true if item is found in the System.Collections.Generic.Queue<T>; otherwise,
    //     false.
    [Pure]
    public bool Contains(T item)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Copies the System.Collections.Generic.Queue<T> elements to an existing one-dimensional
    //     System.Array, starting at the specified array index.
    //
    // Parameters:
    //   array:
    //     The one-dimensional System.Array that is the destination of the elements
    //     copied from System.Collections.Generic.Queue<T>. The System.Array must have
    //     zero-based indexing.
    //
    //   arrayIndex:
    //     The zero-based index in array at which copying begins.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     arrayIndex is less than zero.
    //
    //   System.ArgumentException:
    //     The number of elements in the source System.Collections.Generic.Queue<T>
    //     is greater than the available space from arrayIndex to the end of the destination
    //     array.
    public void CopyTo(T[] array, int arrayIndex)
    {
      Contract.Requires(array != null);
      Contract.Requires(arrayIndex >= 0);
      Contract.Requires(arrayIndex + this.Count <= array.Length);
    }
    //
    // Summary:
    //     Removes and returns the object at the beginning of the System.Collections.Generic.Queue<T>.
    //
    // Returns:
    //     The object that is removed from the beginning of the System.Collections.Generic.Queue<T>.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Collections.Generic.Queue<T> is empty.
    public T Dequeue()
    {
      Contract.Requires(this.Count > 0);
      Contract.Ensures(this.Count == Contract.OldValue(this.Count) -1);

      return default(T);
    }
    //
    // Summary:
    //     Adds an object to the end of the System.Collections.Generic.Queue<T>.
    //
    // Parameters:
    //   item:
    //     The object to add to the System.Collections.Generic.Queue<T>. The value can
    //     be null for reference types.
    public void Enqueue(T item)
    {
      Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
    }

    //
    // Summary:
    //     Returns an enumerator that iterates through the System.Collections.Generic.Queue<T>.
    //
    // Returns:
    //     An System.Collections.Generic.Queue<T>.Enumerator for the System.Collections.Generic.Queue<T>.
    public Queue<T>.Enumerator GetEnumerator()
    {
      return default(Queue<T>.Enumerator);
    }
    //
    // Summary:
    //     Returns the object at the beginning of the System.Collections.Generic.Queue<T>
    //     without removing it.
    //
    // Returns:
    //     The object at the beginning of the System.Collections.Generic.Queue<T>.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Collections.Generic.Queue<T> is empty.
    public T Peek()
    {
      Contract.Requires(this.Count > 0);
      return default(T);
    }
    //
    // Summary:
    //     Copies the System.Collections.Generic.Queue<T> elements to a new array.
    //
    // Returns:
    //     A new array containing elements copied from the System.Collections.Generic.Queue<T>.
    public T[] ToArray()
    {
      Contract.Ensures(Contract.Result<T[]>() != null);

      return null;
    }
    //
    // Summary:
    //     Sets the capacity to the actual number of elements in the System.Collections.Generic.Queue<T>,
    //     if that number is less than 90 percent of current capacity.
    public void TrimExcess()
    {
    }

    // Summary:
    //     Enumerates the elements of a System.Collections.Generic.Queue<T>.
    public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      #region IEnumerator<T> Members

      public T Current
      {
        get { throw new NotImplementedException(); }
      }

      #endregion

      #region IDisposable Members

      public void Dispose()
      {
        throw new NotImplementedException();
      }

      #endregion

      #region IEnumerator Members

      object IEnumerator.Current
      {
        get { throw new NotImplementedException(); }
      }

      public bool MoveNext()
      {
        throw new NotImplementedException();
      }

      void IEnumerator.Reset()
      {
        throw new NotImplementedException();
      }

      #endregion

      #region IEnumerator Members


      #endregion

      #region IEnumerator Members


      #endregion
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    bool ICollection.IsSynchronized
    {
      get { throw new NotImplementedException(); }
    }

    object ICollection.SyncRoot
    {
      get { throw new NotImplementedException(); }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
}
#endif
