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

#if !SILVERLIGHT

using System.Diagnostics.Contracts;
using System;


namespace System.Collections.Specialized
{
  // Summary:
  //     Represents a collection of strings.
  //[Serializable]
  public class StringCollection : IList, ICollection, IEnumerable
  {
    // Summary:
    //     Initializes a new instance of the System.Collections.Specialized.StringCollection
    //     class.
    //public StringCollection();

    // Summary:
    //     Gets the number of strings contained in the System.Collections.Specialized.StringCollection.
    //
    // Returns:
    //     The number of strings contained in the System.Collections.Specialized.StringCollection.
    extern public virtual int Count { get; }
    
    //
    // Summary:
    //     Gets a value indicating whether the System.Collections.Specialized.StringCollection
    //     is read-only.
    //
    // Returns:
    //     This property always returns false.
    extern public bool IsReadOnly { get; }
    //
    // Summary:
    //     Gets a value indicating whether access to the System.Collections.Specialized.StringCollection
    //     is synchronized (thread safe).
    //
    // Returns:
    //     This property always returns false.
    public bool IsSynchronized {
      get
      {
        Contract.Ensures(!Contract.Result<bool>());
        return false;
      }
    }
    //
    // Summary:
    //     Gets an object that can be used to synchronize access to the System.Collections.Specialized.StringCollection.
    //
    // Returns:
    //     An object that can be used to synchronize access to the System.Collections.Specialized.StringCollection.
    extern public object SyncRoot { get; }

    // Summary:
    //     Gets or sets the element at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the entry to get or set.
    //
    // Returns:
    //     The element at the specified index.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.  -or- index is equal to or greater than System.Collections.Specialized.StringCollection.Count.
    public string this[int index]
    {
      get
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
        return default(string);
      }
      set {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
      }
    }

    // Summary:
    //     Adds a string to the end of the System.Collections.Specialized.StringCollection.
    //
    // Parameters:
    //   value:
    //     The string to add to the end of the System.Collections.Specialized.StringCollection.
    //     The value can be null.
    //
    // Returns:
    //     The zero-based index at which the new element is inserted.
    public int Add(string value)
    {
      Contract.Ensures(Contract.Result<int>() == Contract.OldValue(Count));
      Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
      return default(int);
    }
    //
    // Summary:
    //     Copies the elements of a string array to the end of the System.Collections.Specialized.StringCollection.
    //
    // Parameters:
    //   value:
    //     An array of strings to add to the end of the System.Collections.Specialized.StringCollection.
    //     The array itself can not be null but it can contain elements that are null.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     value is null.
    public void AddRange(string[] value)
    {
      Contract.Requires(value != null);
    }
    //
    // Summary:
    //     Removes all the strings from the System.Collections.Specialized.StringCollection.
    extern public void Clear();
    //
    // Summary:
    //     Determines whether the specified string is in the System.Collections.Specialized.StringCollection.
    //
    // Parameters:
    //   value:
    //     The string to locate in the System.Collections.Specialized.StringCollection.
    //     The value can be null.
    //
    // Returns:
    //     true if value is found in the System.Collections.Specialized.StringCollection;
    //     otherwise, false.
    [Pure]
    public bool Contains(string value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Count > 0);
      return default(bool);
    }
    //
    // Summary:
    //     Copies the entire System.Collections.Specialized.StringCollection values
    //     to a one-dimensional array of strings, starting at the specified index of
    //     the target array.
    //
    // Parameters:
    //   array:
    //     The one-dimensional array of strings that is the destination of the elements
    //     copied from System.Collections.Specialized.StringCollection. The System.Array
    //     must have zero-based indexing.
    //
    //   index:
    //     The zero-based index in array at which copying begins.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.
    //
    //   System.ArgumentException:
    //     array is multidimensional.  -or- index is equal to or greater than the length
    //     of array.  -or- The number of elements in the source System.Collections.Specialized.StringCollection
    //     is greater than the available space from index to the end of the destination
    //     array.
    //
    //   System.InvalidCastException:
    //     The type of the source System.Collections.Specialized.StringCollection cannot
    //     be cast automatically to the type of the destination array.
    extern public void CopyTo(string[] array, int index);

    extern void System.Collections.ICollection.CopyTo(Array array, int index);
    extern IEnumerator IEnumerable.GetEnumerator();
    extern int IList.Add(object value);
    extern bool IList.Contains(object value);
    extern int IList.IndexOf(object value);
    extern void IList.Insert(int index, object value);
    extern void IList.Remove(object value);
    extern bool IList.IsFixedSize { get; }
    extern bool IList.IsReadOnly { get; }
    extern object IList.this[int index] { get; set; }

    //
    // Summary:
    //     Returns a System.Collections.Specialized.StringEnumerator that iterates through
    //     the System.Collections.Specialized.StringCollection.
    //
    // Returns:
    //     A System.Collections.Specialized.StringEnumerator for the System.Collections.Specialized.StringCollection.
    public StringEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<StringEnumerator>() != null);
      return default(StringEnumerator);
    }
    //
    // Summary:
    //     Searches for the specified string and returns the zero-based index of the
    //     first occurrence within the System.Collections.Specialized.StringCollection.
    //
    // Parameters:
    //   value:
    //     The string to locate. The value can be null.
    //
    // Returns:
    //     The zero-based index of the first occurrence of value in the System.Collections.Specialized.StringCollection,
    //     if found; otherwise, -1.
    public int IndexOf(string value)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < this.Count);

      return default(int);
    }
    //
    // Summary:
    //     Inserts a string into the System.Collections.Specialized.StringCollection
    //     at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index at which value is inserted.
    //
    //   value:
    //     The string to insert. The value can be null.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.  -or- index greater than System.Collections.Specialized.StringCollection.Count.
    public void Insert(int index, string value)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index <= Count);
      Contract.Ensures(Count == Contract.OldValue(Count) + 1);
    }
    //
    // Summary:
    //     Removes the first occurrence of a specific string from the System.Collections.Specialized.StringCollection.
    //
    // Parameters:
    //   value:
    //     The string to remove from the System.Collections.Specialized.StringCollection.
    //     The value can be null.
    extern public void Remove(string value);
    //
    // Summary:
    //     Removes the string at the specified index of the System.Collections.Specialized.StringCollection.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the string to remove.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.  -or- index is equal to or greater than System.Collections.Specialized.StringCollection.Count.
    extern public void RemoveAt(int index);
  }
}

#endif
