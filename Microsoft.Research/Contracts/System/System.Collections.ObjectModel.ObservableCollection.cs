using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace System.Collections.ObjectModel
{
#if !SILVERLIGHT && !NETFRAMEWORK_3_5 // => types defined in WindowsBase!
    // Summary:
    //     Represents a dynamic data collection that provides notifications when items
    //     get added, removed, or when the whole list is refreshed.
    //
    // Type parameters:
    //   T:
    //     The type of elements in the collection.
    public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection<T>
        //     class.
        public ObservableCollection() { }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection<T>
        //     class that contains elements copied from the specified collection.
        //
        // Parameters:
        //   collection:
        //     The collection from which the elements are copied.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The collection parameter cannot be null.
        public ObservableCollection(IEnumerable<T> collection)
        {
            Contract.Requires(collection != null);
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection<T>
        //     class that contains elements copied from the specified list.
        //
        // Parameters:
        //   list:
        //     The list from which the elements are copied.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The list parameter cannot be null.
        public ObservableCollection(List<T> list)
        {
            Contract.Requires(list != null);
        }

        public virtual event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        //
        // Summary:
        //     Occurs when a property value changes.
        //protected virtual event PropertyChangedEventHandler PropertyChanged;

        // Summary:
        //     Disallows reentrant attempts to change this collection.
        //
        // Returns:
        //     An System.IDisposable object that can be used to dispose of the object.
        //protected IDisposable BlockReentrancy();
        //
        // Summary:
        //     Checks for reentrant attempts to change this collection.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     If there was a call to System.Collections.ObjectModel.ObservableCollection<T>.BlockReentrancy()
        //     of which the System.IDisposable return value has not yet been disposed of.
        //     Typically, this means when there are additional attempts to change this collection
        //     during a System.Collections.ObjectModel.ObservableCollection<T>.CollectionChanged
        //     event. However, it depends on when derived classes choose to call System.Collections.ObjectModel.ObservableCollection<T>.BlockReentrancy().
        //protected void CheckReentrancy();
        //
        // Summary:
        //     Removes all items from the collection.
        protected override void ClearItems()
        {
        }

        //
        // Summary:
        //     Inserts an item into the collection at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index at which item should be inserted.
        //
        //   item:
        //     The object to insert.
        protected override void InsertItem(int index, T item)
        {
        }
        //
        // Summary:
        //     Moves the item at the specified index to a new location in the collection.
        //
        // Parameters:
        //   oldIndex:
        //     The zero-based index specifying the location of the item to be moved.
        //
        //   newIndex:
        //     The zero-based index specifying the new location of the item.
        public void Move(int oldIndex, int newIndex)
        {
            Contract.Requires(oldIndex < Count);
            Contract.Requires(newIndex < Count);
        }
        //
        // Summary:
        //     Moves the item at the specified index to a new location in the collection.
        //
        // Parameters:
        //   oldIndex:
        //     The zero-based index specifying the location of the item to be moved.
        //
        //   newIndex:
        //     The zero-based index specifying the new location of the item.
        protected virtual void MoveItem(int oldIndex, int newIndex)
        {
            Contract.Requires(oldIndex < Count);
            Contract.Requires(newIndex < Count);
        }
        //
        // Summary:
        //     Raises the System.Collections.ObjectModel.ObservableCollection<T>.CollectionChanged
        //     event with the provided arguments.
        //
        // Parameters:
        //   e:
        //     Arguments of the event being raised.
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            Contract.Requires(e != null);
        }
        //
        // Summary:
        //     Raises the System.Collections.ObjectModel.ObservableCollection<T>.PropertyChanged
        //     event with the provided arguments.
        //
        // Parameters:
        //   e:
        //     Arguments of the event being raised.
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            Contract.Requires(e != null);
        }
        //
        // Summary:
        //     Removes the item at the specified index of the collection.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to remove.
        protected override void RemoveItem(int index) { }
        //
        // Summary:
        //     Replaces the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to replace.
        //
        //   item:
        //     The new value for the element at the specified index.
        protected override void SetItem(int index, T item) { }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }
#endif
}
