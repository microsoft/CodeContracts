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

using System.Diagnostics.Contracts;
namespace System.Windows.Forms
{
  // Summary:
  //     Represents a collection of System.Windows.Forms.Control objects.
  //[ComVisible(false)]
  //[ListBindable(false)]
  public class ControlCollection //: ArrangedElementCollection, IList, ICollection, IEnumerable, ICloneable
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.Control.ControlCollection
    //     class.
    //
    // Parameters:
    //   owner:
    //     A System.Windows.Forms.Control representing the control that owns the control
    //     collection.
    //public ControlCollection(Control owner);

    // Summary:
    //     Gets the control that owns this System.Windows.Forms.Control.ControlCollection.
    //
    // Returns:
    //     The System.Windows.Forms.Control that owns this System.Windows.Forms.Control.ControlCollection.
    //public Control Owner { get; }

    // Summary:
    //     Indicates the System.Windows.Forms.Control at the specified indexed location
    //     in the collection.
    //
    // Parameters:
    //   index:
    //     The index of the control to retrieve from the control collection.
    //
    // Returns:
    //     The System.Windows.Forms.Control located at the specified index location
    //     within the control collection.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index value is less than zero or is greater than or equal to the number
    //     of controls in the collection.
    public virtual Control this[int index]
    {
      get
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);

        return default(Control);
      }
    }

    public /*virtual*/ int Count
    {
      get
      {
        return default(int);
      }
    }

    //
    // Summary:
    //     Indicates a System.Windows.Forms.Control with the specified key in the collection.
    //
    // Parameters:
    //   key:
    //     The name of the control to retrieve from the control collection.
    //
    // Returns:
    //     The System.Windows.Forms.Control with the specified key within the System.Windows.Forms.Control.ControlCollection.
    //public virtual Control this[string key] { get; }

    // Summary:
    //     Adds the specified control to the control collection.
    //
    // Parameters:
    //   value:
    //     The System.Windows.Forms.Control to add to the control collection.
    //
    // Exceptions:
    //   System.Exception:
    //     The specified control is a top-level control, or a circular control reference
    //     would result if this control were added to the control collection.
    //
    //   System.ArgumentException:
    //     The object assigned to the value parameter is not a System.Windows.Forms.Control.
    //public virtual void Add(Control value);
    //
    // Summary:
    //     Adds an array of control objects to the collection.
    //
    // Parameters:
    //   controls:
    //     An array of System.Windows.Forms.Control objects to add to the collection.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual void AddRange(Control[] controls)
    {
      Contract.Requires(controls != null);
    }
    //
    // Summary:
    //     Removes all controls from the collection.
    //public virtual void Clear();
    //
    // Summary:
    //     Determines whether the specified control is a member of the collection.
    //
    // Parameters:
    //   control:
    //     The System.Windows.Forms.Control to locate in the collection.
    //
    // Returns:
    //     true if the System.Windows.Forms.Control is a member of the collection; otherwise,
    //     false.
    //public bool Contains(Control control);
    //
    // Summary:
    //     Determines whether the System.Windows.Forms.Control.ControlCollection contains
    //     an item with the specified key.
    //
    // Parameters:
    //   key:
    //     The key to locate in the System.Windows.Forms.Control.ControlCollection.
    //
    // Returns:
    //     true if the System.Windows.Forms.Control.ControlCollection contains an item
    //     with the specified key; otherwise, false.
    //public virtual bool ContainsKey(string key);
    //
    // Summary:
    //     Searches for controls by their System.Windows.Forms.Control.Name property
    //     and builds an array of all the controls that match.
    //
    // Parameters:
    //   key:
    //     The key to locate in the System.Windows.Forms.Control.ControlCollection.
    //
    //   searchAllChildren:
    //     true to search all child controls; otherwise, false.
    //
    // Returns:
    //     An array of type System.Windows.Forms.Control containing the matching controls.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The key parameter is null or the empty string ("").
    [Pure]
    public Control[] Find(string key, bool searchAllChildren)
    {
      Contract.Requires(!string.IsNullOrEmpty(key));

      Contract.Ensures(Contract.Result<Control[]>() != null);

      return default(Control[]);
    }
    //
    // Summary:
    //     Retrieves the index of the specified child control within the control collection.
    //
    // Parameters:
    //   child:
    //     The System.Windows.Forms.Control to search for in the control collection.
    //
    // Returns:
    //     A zero-based index value that represents the location of the specified child
    //     control within the control collection.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The childSystem.Windows.Forms.Control is not in the System.Windows.Forms.Control.ControlCollection.
    [Pure]
    public int GetChildIndex(Control child)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Retrieves the index of the specified child control within the control collection,
    //     and optionally raises an exception if the specified control is not within
    //     the control collection.
    //
    // Parameters:
    //   child:
    //     The System.Windows.Forms.Control to search for in the control collection.
    //
    //   throwException:
    //     true to throw an exception if the System.Windows.Forms.Control specified
    //     in the child parameter is not a control in the System.Windows.Forms.Control.ControlCollection;
    //     otherwise, false.
    //
    // Returns:
    //     A zero-based index value that represents the location of the specified child
    //     control within the control collection; otherwise -1 if the specified System.Windows.Forms.Control
    //     is not found in the System.Windows.Forms.Control.ControlCollection.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The childSystem.Windows.Forms.Control is not in the System.Windows.Forms.Control.ControlCollection,
    //     and the throwException parameter value is true.
    public virtual int GetChildIndex(Control child, bool throwException)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Retrieves a reference to an enumerator object that is used to iterate over
    //     a System.Windows.Forms.Control.ControlCollection.
    //
    // Returns:
    //     An System.Collections.IEnumerator.
    //public override IEnumerator GetEnumerator();
    //
    // Summary:
    //     Retrieves the index of the specified control in the control collection.
    //
    // Parameters:
    //   control:
    //     The System.Windows.Forms.Control to locate in the collection.
    //
    // Returns:
    //     A zero-based index value that represents the position of the specified System.Windows.Forms.Control
    //     in the System.Windows.Forms.Control.ControlCollection.
    [Pure]
    public int IndexOf(Control control)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }

    //
    // Summary:
    //     Retrieves the index of the first occurrence of the specified item within
    //     the collection.
    //
    // Parameters:
    //   key:
    //     The name of the control to search for.
    //
    // Returns:
    //     The zero-based index of the first occurrence of the control with the specified
    //     name in the collection.
    [Pure]
    public virtual int IndexOfKey(string key)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }

    //
    // Summary:
    //     Removes the specified control from the control collection.
    //
    // Parameters:
    //   value:
    //     The System.Windows.Forms.Control to remove from the System.Windows.Forms.Control.ControlCollection.
    //public virtual void Remove(Control value);
    //
    // Summary:
    //     Removes a control from the control collection at the specified indexed location.
    //
    // Parameters:
    //   index:
    //     The index value of the System.Windows.Forms.Control to remove.
    //public void RemoveAt(int index)

    //
    // Summary:
    //     Removes the child control with the specified key.
    //
    // Parameters:
    //   key:
    //     The name of the child control to remove.
    //public virtual void RemoveByKey(string key);
    //
    // Summary:
    //     Sets the index of the specified child control in the collection to the specified
    //     index value.
    //
    // Parameters:
    //   child:
    //     The childSystem.Windows.Forms.Control to search for.
    //
    //   newIndex:
    //     The new index value of the control.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The child control is not in the System.Windows.Forms.Control.ControlCollection.
    //public virtual void SetChildIndex(Control child, int newIndex);
  }
}