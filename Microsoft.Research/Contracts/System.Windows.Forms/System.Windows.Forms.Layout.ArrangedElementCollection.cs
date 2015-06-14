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
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms.Layout
{
  // Summary:
  //     Represents a collection of objects.
  public class ArrangedElementCollection : ICollection
  {
    internal ArrangedElementCollection() { }

    //
    // Summary:
    //     Gets a value indicating whether the collection is read-only.
    //
    // Returns:
    //     true if the collection is read-only; otherwise, false. The default is false.
    //public virtual bool IsReadOnly { get; }

    // Summary:
    //     Copies the entire contents of this collection to a compatible one-dimensional
    //     System.Array, starting at the specified index of the target array.
    //
    // Parameters:
    //   array:
    //     The one-dimensional System.Array that is the destination of the elements
    //     copied from the current collection. The array must have zero-based indexing.
    //
    //   index:
    //     The zero-based index in array at which copying begins.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than 0.
    //
    //   System.ArgumentException:
    //     One of the following conditions has occurred:array is multidimensional.index
    //     is equal to or greater than the length of array.The number of elements in
    //     the source collection is greater than the available space from index to the
    //     end of array.
    //
    //   System.InvalidCastException:
    //     The type of the source element cannot be cast automatically to the type of
    //     array.
    //
    // Summary:
    //     Determines whether two System.Windows.Forms.Layout.ArrangedElementCollection
    //     instances are equal.
    //
    // Parameters:
    //   obj:
    //     The System.Windows.Forms.Layout.ArrangedElementCollection to compare with
    //     the current System.Windows.Forms.Layout.ArrangedElementCollection.
    //
    // Returns:
    //     true if the specified System.Windows.Forms.Layout.ArrangedElementCollection
    //     is equal to the current System.Windows.Forms.Layout.ArrangedElementCollection;
    //     otherwise, false.
    //public override bool Equals(object obj);
    //
    // Summary:
    //     Returns an enumerator for the entire collection.
    //
    // Returns:
    //     An System.Collections.IEnumerator for the entire collection.
    //
    // Summary:
    //     Returns the hash code for this instance.
    //
    // Returns:
    //     A hash code for the current System.Windows.Forms.Layout.ArrangedElementCollection.
    //public override int GetHashCode();

    #region ICollection Members


    bool ICollection.IsSynchronized
    {
      get { throw new NotImplementedException(); }
    }

    object ICollection.SyncRoot
    {
      get { throw new NotImplementedException(); }
    }

    public void CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get { throw new NotImplementedException(); }
    }

    #endregion

    #region IEnumerable Members

    public IEnumerator GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
