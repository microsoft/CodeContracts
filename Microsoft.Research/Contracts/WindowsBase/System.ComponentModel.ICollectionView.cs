using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace System.ComponentModel
{
#if !SILVERLIGHT && !NETFRAMEWORK_3_5 // => types defined in System.Windows!

    // Summary:
    //     Provides information for the System.ComponentModel.ICollectionView.CurrentChanging
    //     event.
    public class CurrentChangingEventArgs : EventArgs
    {
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.CurrentChangingEventArgs
        //     class.
        public CurrentChangingEventArgs() { }
        //
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.CurrentChangingEventArgs
        //     class with the specified isCancelable value.
        //
        // Parameters:
        //   isCancelable:
        //     A value that indicates whether the event is cancelable.
        public CurrentChangingEventArgs(bool isCancelable) { }

        // Summary:
        //     Gets or sets a value that indicates whether to cancel the event.
        //
        // Returns:
        //     true if the event is to be canceled; otherwise, false. The default value
        //     is false.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     If the value of System.ComponentModel.CurrentChangingEventArgs.IsCancelable
        //     is false.
        public bool Cancel
        {
            get
            {
                return false;
            }
            set
            {

            }
        }
        //
        // Summary:
        //     Gets a value that indicates whether the event is cancelable.
        //
        // Returns:
        //     true if the event is cancelable, otherwise, false. The default value is true.
        public bool IsCancelable
        {
            get
            {
                return false;
            }
        }
    }

    // Summary:
    //     Represents the method that handles the System.Windows.Data.CollectionView.CurrentChanging
    //     event.
    //
    // Parameters:
    //   sender:
    //     The object that raised the event.
    //
    //   e:
    //     Information about the event.
    public delegate void CurrentChangingEventHandler(object sender, CurrentChangingEventArgs e);

    // Summary:
    //     Enables collections to have the functionalities of current record management,
    //     custom sorting, filtering, and grouping.
    [ContractClass(typeof (ICollectionViewContract))]
    public interface ICollectionView : IEnumerable, INotifyCollectionChanged
    {
        // Summary:
        //     Gets a value that indicates whether this view supports filtering via the
        //     System.ComponentModel.ICollectionView.Filter property.
        //
        // Returns:
        //     true if this view support filtering; otherwise, false.
        bool CanFilter { get; }
        //
        // Summary:
        //     Gets a value that indicates whether this view supports grouping via the System.ComponentModel.ICollectionView.GroupDescriptions
        //     property.
        //
        // Returns:
        //     true if this view supports grouping; otherwise, false.
        bool CanGroup { get; }
        //
        // Summary:
        //     Gets a value that indicates whether this view supports sorting via the System.ComponentModel.ICollectionView.SortDescriptions
        //     property.
        //
        // Returns:
        //     true if this view supports sorting; otherwise, false.
        bool CanSort { get; }
        //
        // Summary:
        //     Gets or sets the cultural info for any operations of the view that may differ
        //     by culture, such as sorting.
        //
        // Returns:
        //     The culture to use during sorting.
        CultureInfo Culture { get; set; }
        //
        // Summary:
        //     Gets the current item in the view.
        //
        // Returns:
        //     The current item of the view or null if there is no current item.
        object CurrentItem { get; }
        //
        // Summary:
        //     Gets the ordinal position of the System.ComponentModel.ICollectionView.CurrentItem
        //     within the view.
        //
        // Returns:
        //     The ordinal position of the System.ComponentModel.ICollectionView.CurrentItem
        //     within the view.
        int CurrentPosition { get; }
        //
        // Summary:
        //     Gets or sets a callback used to determine if an item is suitable for inclusion
        //     in the view.
        //
        // Returns:
        //     A method used to determine if an item is suitable for inclusion in the view.
        Predicate<object> Filter { get; set; }
        //
        // Summary:
        //     Gets a collection of System.ComponentModel.GroupDescription objects that
        //     describe how the items in the collection are grouped in the view.
        //
        // Returns:
        //     A collection of System.ComponentModel.GroupDescription objects that describe
        //     how the items in the collection are grouped in the view.
        ObservableCollection<GroupDescription> GroupDescriptions { get; }
        //
        // Summary:
        //     Gets the top-level groups.
        //
        // Returns:
        //     A read-only collection of the top-level groups or null if there are no groups.
        ReadOnlyObservableCollection<object> Groups { get; }
        //
        // Summary:
        //     Gets a value that indicates whether the System.ComponentModel.ICollectionView.CurrentItem
        //     of the view is beyond the end of the collection.
        //
        // Returns:
        //     Returns true if the System.ComponentModel.ICollectionView.CurrentItem of
        //     the view is beyond the end of the collection; otherwise, false.
        bool IsCurrentAfterLast { get; }
        //
        // Summary:
        //     Gets a value that indicates whether the System.ComponentModel.ICollectionView.CurrentItem
        //     of the view is beyond the beginning of the collection.
        //
        // Returns:
        //     Returns true if the System.ComponentModel.ICollectionView.CurrentItem of
        //     the view is beyond the beginning of the collection; otherwise, false.
        bool IsCurrentBeforeFirst { get; }
        //
        // Summary:
        //     Returns a value that indicates whether the resulting view is empty.
        //
        // Returns:
        //     true if the resulting view is empty; otherwise, false.
        bool IsEmpty { get; }
        //
        // Summary:
        //     Gets a collection of System.ComponentModel.SortDescription objects that describe
        //     how the items in the collection are sorted in the view.
        //
        // Returns:
        //     A collection of System.ComponentModel.SortDescription objects that describe
        //     how the items in the collection are sorted in the view.
        SortDescriptionCollection SortDescriptions { get; }
        //
        // Summary:
        //     Returns the underlying collection.
        //
        // Returns:
        //     An System.Collections.IEnumerable object that is the underlying collection.
        IEnumerable SourceCollection { get; }

        // Summary:
        //     When implementing this interface, raise this event after the current item
        //     has been changed.
        event EventHandler CurrentChanged;
        //
        // Summary:
        //     When implementing this interface, raise this event before changing the current
        //     item. Event handler can cancel this event.
        event CurrentChangingEventHandler CurrentChanging;

        // Summary:
        //     Returns a value that indicates whether a given item belongs to this collection
        //     view.
        //
        // Parameters:
        //   item:
        //     The object to check.
        //
        // Returns:
        //     true if the item belongs to this collection view; otherwise, false.
        bool Contains(object item);
        //
        // Summary:
        //     Enters a defer cycle that you can use to merge changes to the view and delay
        //     automatic refresh.
        //
        // Returns:
        //     An System.IDisposable object that you can use to dispose of the calling object.
        IDisposable DeferRefresh();
        //
        // Summary:
        //     Sets the specified item to be the System.ComponentModel.ICollectionView.CurrentItem
        //     in the view.
        //
        // Parameters:
        //   item:
        //     The item to set as the System.ComponentModel.ICollectionView.CurrentItem.
        //
        // Returns:
        //     true if the resulting System.ComponentModel.ICollectionView.CurrentItem is
        //     within the view; otherwise, false.
        bool MoveCurrentTo(object item);
        //
        // Summary:
        //     Sets the first item in the view as the System.ComponentModel.ICollectionView.CurrentItem.
        //
        // Returns:
        //     true if the resulting System.ComponentModel.ICollectionView.CurrentItem is
        //     an item within the view; otherwise, false.
        bool MoveCurrentToFirst();
        //
        // Summary:
        //     Sets the last item in the view as the System.ComponentModel.ICollectionView.CurrentItem.
        //
        // Returns:
        //     true if the resulting System.ComponentModel.ICollectionView.CurrentItem is
        //     an item within the view; otherwise, false.
        bool MoveCurrentToLast();
        //
        // Summary:
        //     Sets the item after the System.ComponentModel.ICollectionView.CurrentItem
        //     in the view as the System.ComponentModel.ICollectionView.CurrentItem.
        //
        // Returns:
        //     true if the resulting System.ComponentModel.ICollectionView.CurrentItem is
        //     an item within the view; otherwise, false.
        bool MoveCurrentToNext();
        //
        // Summary:
        //     Sets the item at the specified index to be the System.ComponentModel.ICollectionView.CurrentItem
        //     in the view.
        //
        // Parameters:
        //   position:
        //     The index to set the System.ComponentModel.ICollectionView.CurrentItem to.
        //
        // Returns:
        //     true if the resulting System.ComponentModel.ICollectionView.CurrentItem is
        //     an item within the view; otherwise, false.
        bool MoveCurrentToPosition(int position);
        //
        // Summary:
        //     Sets the item before the System.ComponentModel.ICollectionView.CurrentItem
        //     in the view as the System.ComponentModel.ICollectionView.CurrentItem.
        //
        // Returns:
        //     true if the resulting System.ComponentModel.ICollectionView.CurrentItem is
        //     an item within the view; otherwise, false.
        bool MoveCurrentToPrevious();
        //
        // Summary:
        //     Recreates the view.
        void Refresh();
    }

    [ContractClassFor(typeof (ICollectionView))]
    abstract class ICollectionViewContract : ICollectionView
    {
        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        bool ICollectionView.CanFilter
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ICollectionView.CanGroup
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ICollectionView.CanSort
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        CultureInfo ICollectionView.Culture
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

        object ICollectionView.CurrentItem
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int ICollectionView.CurrentPosition
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        Predicate<object> ICollectionView.Filter
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

        ObservableCollection<GroupDescription> ICollectionView.GroupDescriptions
        {
            get
            {
                Contract.Ensures(Contract.Result<ObservableCollection<GroupDescription>>() != null);
                throw new NotImplementedException();
            }
        }

        ReadOnlyObservableCollection<object> ICollectionView.Groups
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ICollectionView.IsCurrentAfterLast
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ICollectionView.IsCurrentBeforeFirst
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ICollectionView.IsEmpty
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        SortDescriptionCollection ICollectionView.SortDescriptions
        {
            get
            {
                Contract.Ensures(Contract.Result<SortDescriptionCollection>() != null);
                throw new NotImplementedException();
            }
        }

        IEnumerable ICollectionView.SourceCollection
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        event EventHandler ICollectionView.CurrentChanged
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        event CurrentChangingEventHandler ICollectionView.CurrentChanging
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        bool ICollectionView.Contains(object item)
        {
            throw new NotImplementedException();
        }

        IDisposable ICollectionView.DeferRefresh()
        {
            throw new NotImplementedException();
        }

        bool ICollectionView.MoveCurrentTo(object item)
        {
            throw new NotImplementedException();
        }

        bool ICollectionView.MoveCurrentToFirst()
        {
            throw new NotImplementedException();
        }

        bool ICollectionView.MoveCurrentToLast()
        {
            throw new NotImplementedException();
        }

        bool ICollectionView.MoveCurrentToNext()
        {
            throw new NotImplementedException();
        }

        bool ICollectionView.MoveCurrentToPosition(int position)
        {
            throw new NotImplementedException();
        }

        bool ICollectionView.MoveCurrentToPrevious()
        {
            throw new NotImplementedException();
        }

        void ICollectionView.Refresh()
        {
            throw new NotImplementedException();
        }
    }
#endif
}
