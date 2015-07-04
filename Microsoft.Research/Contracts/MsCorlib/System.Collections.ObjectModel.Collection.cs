using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
    // Summary:
    //     Provides the base class for a generic collection.
    //
    // Type parameters:
    //   T:
    //     The type of elements in the collection.
    public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
    {
        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.Collection<T>
        //     class that is empty.
        public Collection()
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.Collection<T>
        //     class as a wrapper for the specified list.
        //
        // Parameters:
        //   list:
        //     The list that is wrapped by the new collection.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     list is null.
        public Collection(IList<T> list)
        {
            Contract.Requires(list != null);
        }

        // Summary:
        //     Gets the number of elements actually contained in the System.Collections.ObjectModel.Collection<T>.
        //
        // Returns:
        //     The number of elements actually contained in the System.Collections.ObjectModel.Collection<T>.
        public int Count
        {
            get
            {
                return 0;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        bool IList.IsFixedSize
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool IList.IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        object IList.this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        bool ICollection<T>.IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        //
        // Summary:
        //     Gets a System.Collections.Generic.IList<T> wrapper around the System.Collections.ObjectModel.Collection<T>.
        //
        // Returns:
        //     A System.Collections.Generic.IList<T> wrapper around the System.Collections.ObjectModel.Collection<T>.
        protected IList<T> Items
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<T>>() != null);
                return null;
            }
        }

        // Summary:
        //     Gets or sets the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to get or set.
        //
        // Returns:
        //     The element at the specified index.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.-or-index is equal to or greater than System.Collections.ObjectModel.Collection<T>.Count.
        public T this[int index] {
            get
            {
                return default(T);
            }
            set
            {
            } 
        }

        // Summary:
        //     Adds an object to the end of the System.Collections.ObjectModel.Collection<T>.
        //
        // Parameters:
        //   item:
        //     The object to be added to the end of the System.Collections.ObjectModel.Collection<T>.
        //     The value can be null for reference types.
        public void Add(T item)
        {
            
        }
        //
        // Summary:
        //     Removes all elements from the System.Collections.ObjectModel.Collection<T>.

        int IList.Add(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            
        }

        bool IList.Contains(object value)
        {
            throw new NotImplementedException();
        }

        int IList.IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        void IList.Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        void IList.Remove(object value)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Removes all elements from the System.Collections.ObjectModel.Collection<T>.
        protected virtual void ClearItems()
        {
            
        }
        //
        // Summary:
        //     Determines whether an element is in the System.Collections.ObjectModel.Collection<T>.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.ObjectModel.Collection<T>.
        //     The value can be null for reference types.
        //
        // Returns:
        //     true if item is found in the System.Collections.ObjectModel.Collection<T>;
        //     otherwise, false.
        public bool Contains(T item)
        {
            return false;
        }
        //
        // Summary:
        //     Copies the entire System.Collections.ObjectModel.Collection<T> to a compatible
        //     one-dimensional System.Array, starting at the specified index of the target
        //     array.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements
        //     copied from System.Collections.ObjectModel.Collection<T>. The System.Array
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
        //     The number of elements in the source System.Collections.ObjectModel.Collection<T>
        //     is greater than the available space from index to the end of the destination
        //     array.
        public void CopyTo(T[] array, int index)
        {
           
        }
        //
        // Summary:
        //     Returns an enumerator that iterates through the System.Collections.ObjectModel.Collection<T>.
        //
        // Returns:
        //     An System.Collections.Generic.IEnumerator<T> for the System.Collections.ObjectModel.Collection<T>.
        public IEnumerator<T> GetEnumerator()
        {
            return null;
        }

        [ContractModel]
        public object[] Model
        {
            [ContractRuntimeIgnored]
            get
            {
                throw new NotImplementedException();
            }
        }

        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the
        //     first occurrence within the entire System.Collections.ObjectModel.Collection<T>.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List<T>. The value
        //     can be null for reference types.
        //
        // Returns:
        //     The zero-based index of the first occurrence of item within the entire System.Collections.ObjectModel.Collection<T>,
        //     if found; otherwise, -1.
        public int IndexOf(T item)
        {
            return 0;
        }
        //
        // Summary:
        //     Inserts an element into the System.Collections.ObjectModel.Collection<T>
        //     at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index at which item should be inserted.
        //
        //   item:
        //     The object to insert. The value can be null for reference types.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.-or-index is greater than System.Collections.ObjectModel.Collection<T>.Count.
        public void Insert(int index, T item)
        {
            
        }
        //
        // Summary:
        //     Inserts an element into the System.Collections.ObjectModel.Collection<T>
        //     at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index at which item should be inserted.
        //
        //   item:
        //     The object to insert. The value can be null for reference types.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.-or-index is greater than System.Collections.ObjectModel.Collection<T>.Count.
        protected virtual void InsertItem(int index, T item)
        {
            Contract.Requires((index >= 0) && (index <= Count));
        }
        //
        // Summary:
        //     Removes the first occurrence of a specific object from the System.Collections.ObjectModel.Collection<T>.
        //
        // Parameters:
        //   item:
        //     The object to remove from the System.Collections.ObjectModel.Collection<T>.
        //     The value can be null for reference types.
        //
        // Returns:
        //     true if item is successfully removed; otherwise, false. This method also
        //     returns false if item was not found in the original System.Collections.ObjectModel.Collection<T>.
        public bool Remove(T item)
        {
            return false;
        }
        //
        // Summary:
        //     Removes the element at the specified index of the System.Collections.ObjectModel.Collection<T>.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to remove.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.-or-index is equal to or greater than System.Collections.ObjectModel.Collection<T>.Count.
        public void RemoveAt(int index) { }
        //
        // Summary:
        //     Removes the element at the specified index of the System.Collections.ObjectModel.Collection<T>.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to remove.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.-or-index is equal to or greater than System.Collections.ObjectModel.Collection<T>.Count.
        protected virtual void RemoveItem(int index)
        {
            Contract.Requires((index >= 0) && (index < Count));
        }
        //
        // Summary:
        //     Replaces the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to replace.
        //
        //   item:
        //     The new value for the element at the specified index. The value can be null
        //     for reference types.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.-or-index is greater than System.Collections.ObjectModel.Collection<T>.Count.
        protected virtual void SetItem(int index, T item)
        {
            // Comment is wrong, index must be < Count!
            Contract.Requires((index >= 0) && (index < Count));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
