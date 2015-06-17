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

using System;
using System.Collections;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.Collections.Specialized
{
  // Summary:
  //     Implements a hash table with the key and the value strongly typed to be strings
  //     rather than objects.
  public class StringDictionary
  {
    // Summary:
    //     Gets the number of key/value pairs in the System.Collections.Specialized.StringDictionary.
    //
    // Returns:
    //     The number of key/value pairs in the System.Collections.Specialized.StringDictionary.
    //      Retrieving the value of this property is an O(1) operation.
    public virtual int Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }
    //
    // Summary:
    //     Gets a value indicating whether access to the System.Collections.Specialized.StringDictionary
    //     is synchronized (thread safe).
    //
    // Returns:
    //     true if access to the System.Collections.Specialized.StringDictionary is
    //     synchronized (thread safe); otherwise, false.
#if false
    public virtual bool IsSynchronized { get; }
#endif
    //
    // Summary:
    //     Gets a collection of keys in the System.Collections.Specialized.StringDictionary.
    //
    // Returns:
    //     An System.Collections.ICollection that provides the keys in the System.Collections.Specialized.StringDictionary.
    public virtual ICollection Keys
    {
      get
      {
        Contract.Ensures(Contract.Result<ICollection>() != null);
        return default(ICollection);
      }
    }
    //
    // Summary:
    //     Gets an object that can be used to synchronize access to the System.Collections.Specialized.StringDictionary.
    //
    // Returns:
    //     An System.Object that can be used to synchronize access to the System.Collections.Specialized.StringDictionary.
#if false
    public virtual object SyncRoot { get; }
#endif
    //
    // Summary:
    //     Gets a collection of values in the System.Collections.Specialized.StringDictionary.
    //
    // Returns:
    //     An System.Collections.ICollection that provides the values in the System.Collections.Specialized.StringDictionary.
    public virtual ICollection Values
    {
      get
      {
        Contract.Ensures(Contract.Result<ICollection>() != null);
        return default(ICollection);
      }
    }

    // Summary:
    //     Gets or sets the value associated with the specified key.
    //
    // Parameters:
    //   key:
    //     The key whose value to get or set.
    //
    // Returns:
    //     The value associated with the specified key. If the specified key is not
    //     found, Get returns null, and Set creates a new entry with the specified key.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is null.
#if false
    public virtual string this[string key] { get; set; }
#endif
    // Summary:
    //     Adds an entry with the specified key and value into the System.Collections.Specialized.StringDictionary.
    //
    // Parameters:
    //   key:
    //     The key of the entry to add.
    //
    //   value:
    //     The value of the entry to add. The value can be null.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is null.
    //
    //   System.ArgumentException:
    //     An entry with the same key already exists in the System.Collections.Specialized.StringDictionary.
    //
    //   System.NotSupportedException:
    //     The System.Collections.Specialized.StringDictionary is read-only.
#if false
    public virtual void Add(string key, string value);
#endif
    //
    // Summary:
    //     Removes all entries from the System.Collections.Specialized.StringDictionary.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.Specialized.StringDictionary is read-only.
    public virtual void Clear()
    {
      Contract.Ensures(this.Count == 0);
    }
    //
    // Summary:
    //     Determines if the System.Collections.Specialized.StringDictionary contains
    //     a specific key.
    //
    // Parameters:
    //   key:
    //     The key to locate in the System.Collections.Specialized.StringDictionary.
    //
    // Returns:
    //     true if the System.Collections.Specialized.StringDictionary contains an entry
    //     with the specified key; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The key is null.
    [Pure]
    public virtual bool ContainsKey(string key)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Determines if the System.Collections.Specialized.StringDictionary contains
    //     a specific value.
    //
    // Parameters:
    //   value:
    //     The value to locate in the System.Collections.Specialized.StringDictionary.
    //     The value can be null.
    //
    // Returns:
    //     true if the System.Collections.Specialized.StringDictionary contains an element
    //     with the specified value; otherwise, false.
    [Pure]
    public virtual bool ContainsValue(string value)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Copies the string dictionary values to a one-dimensional System.Array instance
    //     at the specified index.
    //
    // Parameters:
    //   array:
    //     The one-dimensional System.Array that is the destination of the values copied
    //     from the System.Collections.Specialized.StringDictionary.
    //
    //   index:
    //     The index in the array where copying begins.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     array is multidimensional.  -or- The number of elements in the System.Collections.Specialized.StringDictionary
    //     is greater than the available space from index to the end of array.
    //
    //   System.ArgumentNullException:
    //     array is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than the lower bound of array.
    public virtual void CopyTo(Array array, int index)
    {
    }
    //
    // Summary:
    //     Returns an enumerator that iterates through the string dictionary.
    //
    // Returns:
    //     An System.Collections.IEnumerator that iterates through the string dictionary.
#if false
    public virtual IEnumerator GetEnumerator();
#endif
    //
    // Summary:
    //     Removes the entry with the specified key from the string dictionary.
    //
    // Parameters:
    //   key:
    //     The key of the entry to remove.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The key is null.
    //
    //   System.NotSupportedException:
    //     The System.Collections.Specialized.StringDictionary is read-only.
#if false
    public virtual void Remove(string key);
#endif
  }
}
#endif