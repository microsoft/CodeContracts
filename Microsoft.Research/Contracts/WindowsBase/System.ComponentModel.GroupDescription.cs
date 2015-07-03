using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace System.ComponentModel
{
#if !SILVERLIGHT && !NETFRAMEWORK_3_5 // => types defined in System.Windows!
    // Summary:
    //     Provides an abstract base class for types that describe how to divide the
    //     items in a collection into groups.
    public abstract class GroupDescription : INotifyPropertyChanged
    {
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.GroupDescription
        //     class.
        // protected GroupDescription();

        // Summary:
        //     Gets the collection of names that are used to initialize a group with a set
        //     of subgroups with the given names.
        //
        // Returns:
        //     The collection of names that are used to initialize a group with a set of
        //     subgroups with the given names.
        public ObservableCollection<object> GroupNames
        {
            get
            {
                Contract.Ensures(Contract.Result<ObservableCollection<object>>() != null);
                return null;
            }
        }

        // Summary:
        //     Occurs when a property value changes.
        // protected virtual event PropertyChangedEventHandler PropertyChanged;

        // Summary:
        //     Returns the group name(s) for the given item.
        //
        // Parameters:
        //   item:
        //     The item to return group names for.
        //
        //   level:
        //     The level of grouping.
        //
        //   culture:
        //     The System.Globalization.CultureInfo to supply to the converter.
        //
        // Returns:
        //     The group name(s) for the given item.
        public abstract object GroupNameFromItem(object item, int level, CultureInfo culture);
        //
        // Summary:
        //     Returns a value that indicates whether the group name and the item name match
        //     such that the item belongs to the group.
        //
        // Parameters:
        //   groupName:
        //     The name of the group to check.
        //
        //   itemName:
        //     The name of the item to check.
        //
        // Returns:
        //     true if the names match and the item belongs to the group; otherwise, false.
        public virtual bool NamesMatch(object groupName, object itemName)
        {
            return false;
        }
        //
        // Summary:
        //     Raises the System.ComponentModel.GroupDescription.PropertyChanged event.
        //
        // Parameters:
        //   e:
        //     Arguments of the event being raised.
        //protected virtual void OnPropertyChanged(PropertyChangedEventArgs e);

        //
        // Summary:
        //     Returns whether serialization processes should serialize the effective value
        //     of the System.ComponentModel.GroupDescription.GroupNames property on instances
        //     of this class.
        //
        // Returns:
        //     Returns true if the System.ComponentModel.GroupDescription.GroupNames property
        //     value should be serialized; otherwise, false.
        // public bool ShouldSerializeGroupNames();

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }
#endif
}
