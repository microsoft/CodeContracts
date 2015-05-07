using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Runtime;

namespace System.ComponentModel
{
#if !SILVERLIGHT && !NETFRAMEWORK_3_5 // => types defined in System.Windows!

    // Summary:
    //     Defines the direction and the property name to be used as the criteria for
    //     sorting a collection.
    public struct SortDescription
    {
        //
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.SortDescription structure.
        //
        // Parameters:
        //   propertyName:
        //     The name of the property to sort the list by.
        //
        //   direction:
        //     The sort order.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The propertyName parameter cannot be null.
        //
        //   System.ArgumentException:
        //     The propertyName parameter cannot be empty
        //
        //   System.ComponentModel.InvalidEnumArgumentException:
        //     The direction parameter does not specify a valid value.
        public SortDescription(string propertyName, ListSortDirection direction)
        {
            Contract.Requires(!String.IsNullOrEmpty(propertyName));
        }

        // Summary:
        //     Compares two System.ComponentModel.SortDescription objects for value inequality.
        //
        // Parameters:
        //   sd1:
        //     The first instance to compare.
        //
        //   sd2:
        //     The second instance to compare.
        //
        // Returns:
        //     true if the values are not equal; otherwise, false.
        //public static bool operator !=(SortDescription sd1, SortDescription sd2);
        //
        // Summary:
        //     Compares two System.ComponentModel.SortDescription objects for value equality.
        //
        // Parameters:
        //   sd1:
        //     The first instance to compare.
        //
        //   sd2:
        //     The second instance to compare.
        //
        // Returns:
        //     true if the two objects are equal; otherwise, false.
        //public static bool operator ==(SortDescription sd1, SortDescription sd2);

        // Summary:
        //     Gets or sets a value that indicates whether to sort in ascending or descending
        //     order.
        //
        // Returns:
        //     A System.ComponentModel.ListSortDirection value to indicate whether to sort
        //     in ascending or descending order.
        //public ListSortDirection Direction { get; set; }
        //
        // Summary:
        //     Gets a value that indicates whether this object is in an immutable state.
        //
        // Returns:
        //     true if this object is in use; otherwise, false.
        //public bool IsSealed { get; }

        //
        // Summary:
        //     Gets or sets the property name being used as the sorting criteria.
        //
        // Returns:
        //     The default value is null.
        //public string PropertyName { get; set; }

        // Summary:
        //     Compares the specified instance and the current instance of System.ComponentModel.SortDescription
        //     for value equality.
        //
        // Parameters:
        //   obj:
        //     The System.ComponentModel.SortDescription instance to compare.
        //
        // Returns:
        //     true if obj and this instance of System.ComponentModel.SortDescription have
        //     the same values.
        //public override bool Equals(object obj);
        //
        // Summary:
        //     Returns the hash code for this instance of System.ComponentModel.SortDescription.
        //
        // Returns:
        //     The hash code for this instance of System.ComponentModel.SortDescription.
        //public override int GetHashCode();
    }


    // Summary:
    //     Represents a collection of System.ComponentModel.SortDescription objects.
    public class SortDescriptionCollection : Collection<SortDescription>, INotifyCollectionChanged
    {
        // Summary:
        //     Gets an empty and non-modifiable instance of System.ComponentModel.SortDescriptionCollection.
        public static readonly SortDescriptionCollection Empty;

        // Summary:
        //     Initializes a new instance of the System.ComponentModel.SortDescriptionCollection
        //     class.
        public SortDescriptionCollection() { }

        // Summary:
        //     Occurs when an item is added or removed.
        // protected event NotifyCollectionChangedEventHandler CollectionChanged;

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        // Summary:
        //     Removes all items from the collection.
        //protected override void ClearItems();
        //
        // Summary:
        //     Inserts an item into the collection at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index where the item is inserted.
        //
        //   item:
        //     The object to insert.
        //protected override void InsertItem(int index, SortDescription item);
        //
        // Summary:
        //     Removes the item at the specified index in the collection.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to remove.
        //protected override void RemoveItem(int index);
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
        //protected override void SetItem(int index, SortDescription item);
    }
#endif
}
