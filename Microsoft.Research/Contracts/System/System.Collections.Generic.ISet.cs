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


#if NETFRAMEWORK_4_0

using System;
using System.Collections;
using System.Diagnostics.Contracts;

namespace System.Collections.Generic
{
  // Summary:
  //     Provides the base interface for the abstraction of sets.
  //
  // Type parameters:
  //   T:
  //     The type of elements in the set.
  
  [ContractClass(typeof(ISetContracts<>))]
  public interface ISet<T>  : ICollection<T>, IEnumerable<T>, IEnumerable
  {
    // Summary:
    //     Adds an element to the current set and returns a value to indicate if the
    //     element was successfully added.
    //
    // Parameters:
    //   item:
    //     The element to add to the set.
    //
    // Returns:
    //     true if the element is added to the set; false if the element is already
    //     in the set.
    new bool Add(T item);
    //
    // Summary:
    //     Removes all elements in the specified collection from the current set.
    //
    // Parameters:
    //   other:
    //     The collection of items to remove from the set.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    void ExceptWith(IEnumerable<T> other);
    //
    // Summary:
    //     Modifies the current set so that it contains only elements that are also
    //     in a specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current set.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    void IntersectWith(IEnumerable<T> other);
    //
    // Summary:
    //     Determines whether the current set is a property (strict) subset of a specified
    //     collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current set.
    //
    // Returns:
    //     true if the current set is a correct subset of other; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    bool IsProperSubsetOf(IEnumerable<T> other);
    //
    // Summary:
    //     Determines whether the current set is a correct superset of a specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current set.
    //
    // Returns:
    //     true if the System.Collections.Generic.ISet<T> object is a correct superset
    //     of other; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    bool IsProperSupersetOf(IEnumerable<T> other);
    //
    // Summary:
    //     Determines whether a set is a subset of a specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current set.
    //
    // Returns:
    //     true if the current set is a subset of other; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    bool IsSubsetOf(IEnumerable<T> other);
    //
    // Summary:
    //     Determines whether the current set is a superset of a specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current set.
    //
    // Returns:
    //     true if the current set is a superset of other; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    bool IsSupersetOf(IEnumerable<T> other);
    //
    // Summary:
    //     Determines whether the current set overlaps with the specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current set.
    //
    // Returns:
    //     true if the current set and other share at least one common element; otherwise,
    //     false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    bool Overlaps(IEnumerable<T> other);
    //
    // Summary:
    //     Determines whether the current set and the specified collection contain the
    //     same elements.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current set.
    //
    // Returns:
    //     true if the current set is equal to other; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    bool SetEquals(IEnumerable<T> other);
    //
    // Summary:
    //     Modifies the current set so that it contains only elements that are present
    //     either in the current set or in the specified collection, but not both.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current set.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    void SymmetricExceptWith(IEnumerable<T> other);
    //
    // Summary:
    //     Modifies the current set so that it contains all elements that are present
    //     in both the current set and in the specified collection.
    //
    // Parameters:
    //   other:
    //     The collection to compare to the current set.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     other is null.
    void UnionWith(IEnumerable<T> other);
  }


  [ContractClassFor(typeof(ISet<>))]
  abstract class ISetContracts<T> : ISet<T>
  {
    #region ISet<T> Members

    public bool Add(T item)
    {
      Contract.Ensures(!Contract.Result<bool>() || this.Count == Contract.OldValue(this.Count) + 1);
      Contract.Ensures(Contract.Result<bool>() || this.Count == Contract.OldValue(this.Count));

      throw new NotImplementedException();
    }

    public void ExceptWith(IEnumerable<T> other)
    {
      Contract.Requires(other != null);
    }

    public void IntersectWith(IEnumerable<T> other)
    {
      Contract.Requires(other != null);
    }

    [Pure]
    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
      Contract.Requires(other != null);

      return default(bool);
    }

    [Pure]
    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
      Contract.Requires(other != null);

      return default(bool);
    }

    [Pure]
    public bool IsSubsetOf(IEnumerable<T> other)
    {
      Contract.Requires(other != null);

      return default(bool);
    }

    [Pure]
    public bool IsSupersetOf(IEnumerable<T> other)
    {
      Contract.Requires(other != null);

      return default(bool);
    }

    [Pure]
    public bool Overlaps(IEnumerable<T> other)
    {
      Contract.Requires(other != null);

      return default(bool);
    }

    [Pure]
    public bool SetEquals(IEnumerable<T> other)
    {
      Contract.Requires(other != null);

      return default(bool);
    }

    public void SymmetricExceptWith(IEnumerable<T> other)
    {
      Contract.Requires(other != null);
    }

    public void UnionWith(IEnumerable<T> other)
    {
      Contract.Requires(other != null);
    }

    #endregion

    #region ICollection<T> Members

    void ICollection<T>.Add(T item)
    {
      throw new NotImplementedException();
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public bool Contains(T item)
    {
      throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get { throw new NotImplementedException(); }
    }

    public bool IsReadOnly
    {
      get { throw new NotImplementedException(); }
    }

    public bool Remove(T item)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator()
    {
      throw new NotImplementedException();
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