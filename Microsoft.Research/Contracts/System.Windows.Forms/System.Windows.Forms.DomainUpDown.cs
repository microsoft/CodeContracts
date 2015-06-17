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
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents a Windows spin box (also known as an up-down control) that displays
  //     string values.
  //// [DefaultProperty("Items")]
  //// [DefaultBindingProperty("SelectedItem")]
  //// [ComVisible(true)]
  //// [DefaultEvent("SelectedItemChanged")]
  //// [ClassInterface(ClassInterfaceType.AutoDispatch)]
  public class DomainUpDown : UpDownBase
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.DomainUpDown class.
    //public DomainUpDown();

    // Summary:
    //     A collection of objects assigned to the spin box (also known as an up-down
    //     control).
    //
    // Returns:
    //     A System.Windows.Forms.DomainUpDown.DomainUpDownItemCollection that contains
    //     an System.Object collection.
    // [Localizable(true)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public DomainUpDown.DomainUpDownItemCollection Items
    {
      get
      {
        Contract.Ensures(Contract.Result<DomainUpDown.DomainUpDownItemCollection>() != null);

        return default(DomainUpDown.DomainUpDownItemCollection);
      }
    }
    //
    // Summary:
    //     Gets or sets the spacing between the System.Windows.Forms.DomainUpDown control's
    //     contents and its edges.
    //
    // Returns:
    //     System.Windows.Forms.Padding.Empty in all cases.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public Padding Padding { get; set; }
    //
    // Summary:
    //     Gets or sets the index value of the selected item.
    //
    // Returns:
    //     The zero-based index value of the selected item. The default value is -1.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The assigned value is less than the default, -1.-or- The assigned value is
    //     greater than the System.Windows.Forms.DomainUpDown.Items count.
    // [Browsable(false)]
    // [DefaultValue(-1)]
    public int SelectedIndex 
    { 
      get
      {
        Contract.Ensures(Contract.Result<int>() >= -1);

        return default(int);
      }
       set
       {
         Contract.Requires(value >= 0);
       }
    }
    //
    // Summary:
    //     Gets or sets the selected item based on the index value of the selected item
    //     in the collection.
    //
    // Returns:
    //     The selected item based on the System.Windows.Forms.DomainUpDown.SelectedIndex
    //     value. The default value is null.
    // [Browsable(false)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public object SelectedItem { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the item collection is sorted.
    //
    // Returns:
    //     true if the item collection is sorted; otherwise, false. The default value
    //     is false.
    // [DefaultValue(false)]
    //public bool Sorted { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the collection of items continues
    //     to the first or last item if the user continues past the end of the list.
    //
    // Returns:
    //     true if the list starts again when the user reaches the beginning or end
    //     of the collection; otherwise, false. The default value is false.
    // [DefaultValue(false)]
    // [Localizable(true)]
    //public bool Wrap { get; set; }

    // Summary:
    //     Occurs when the value of the System.Windows.Forms.DomainUpDown.Padding property
    //     changes.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // public event EventHandler PaddingChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.DomainUpDown.SelectedItem property has
    //     been changed.
    // public event EventHandler SelectedItemChanged;

    // Summary:
    //     Creates a new accessibility object for the System.Windows.Forms.DomainUpDown
    //     control.
    //
    // Returns:
    //     A new System.Windows.Forms.DomainUpDown.DomainUpDownAccessibleObject for
    //     the control.
    // protected override AccessibleObject CreateAccessibilityInstance();
    //
    // Summary:
    //     Displays the next item in the object collection.
    //public override void DownButton();
    //
    // Summary:
    //     Raises the System.Windows.Forms.DomainUpDown.SelectedItemChanged event.
    //
    // Parameters:
    //   source:
    //     The source of the event.
    //
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnChanged(object source, EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.DomainUpDown.SelectedItemChanged event.
    //
    // Parameters:
    //   source:
    //     The source of the event.
    //
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected void OnSelectedItemChanged(object source, EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.KeyPress event.
    //
    // Parameters:
    //   source:
    //     The source of the event.
    //
    //   e:
    //     A System.Windows.Forms.KeyPressEventArgs that contains the event data.
    // protected override void OnTextBoxKeyPress(object source, KeyPressEventArgs e);
    //
    // Summary:
    //     Returns a string that represents the System.Windows.Forms.DomainUpDown control.
    //
    // Returns:
    //     A string that represents the current System.Windows.Forms.DomainUpDown.
    //public override string ToString();
    //
    // Summary:
    //     Displays the previous item in the collection.
    //public override void UpButton();
    //
    // Summary:
    //     Updates the text in the spin box (also known as an up-down control) to display
    //     the selected item.
    // protected override void UpdateEditText();

    // Summary:
    //     Provides information about the items in the System.Windows.Forms.DomainUpDown
    //     control to accessibility client applications.
    // [ComVisible(true)]
    //public class DomainItemAccessibleObject : AccessibleObject
    //{
    //  // Summary:
    //  //     Initializes a new instance of the System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject
    //  //     class.
    //  //
    //  // Parameters:
    //  //   name:
    //  //     The name of the System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject.
    //  //
    //  //   parent:
    //  //     The System.Windows.Forms.AccessibleObject that contains the items in the
    //  //     System.Windows.Forms.DomainUpDown control.
    //  public DomainItemAccessibleObject(string name, AccessibleObject parent);

    //  // Summary:
    //  //     Gets or sets the object name.
    //  //
    //  // Returns:
    //  //     The object name, or null if the property has not been set.
    //  public override string Name { get; set; }
    //  //
    //  // Summary:
    //  //     Gets the parent of an accessible object.
    //  //
    //  // Returns:
    //  //     An System.Windows.Forms.AccessibleObject that represents the parent of an
    //  //     accessible object, or null if there is no parent object.
    //  public override AccessibleObject Parent { get; }
    //  //
    //  // Summary:
    //  //     Gets the role of this accessible object.
    //  //
    //  // Returns:
    //  //     The System.Windows.Forms.AccessibleRole.ListItem value.
    //  public override AccessibleRole Role { get; }
    //  //
    //  // Summary:
    //  //     Gets the state of the System.Windows.Forms.RadioButton control.
    //  //
    //  // Returns:
    //  //     If the System.Windows.Forms.RadioButton.Checked property is set to true,
    //  //     returns System.Windows.Forms.AccessibleStates.Checked.
    //  public override AccessibleStates State { get; }
    //  //
    //  // Summary:
    //  //     Gets the value of an accessible object.
    //  //
    //  // Returns:
    //  //     The Name property of the System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject.
    //  public override string Value { get; }
    //}

    // Summary:
    //     Provides information about the System.Windows.Forms.DomainUpDown control
    //     to accessibility client applications.
    // [ComVisible(true)]
    //public class DomainUpDownAccessibleObject : Control.ControlAccessibleObject
    //{
    //  // Summary:
    //  //     Initializes a new instance of the System.Windows.Forms.DomainUpDown.DomainUpDownAccessibleObject
    //  //     class.
    //  public DomainUpDownAccessibleObject(Control owner);

    //  // Summary:
    //  //     Gets the role of this accessible object.
    //  //
    //  // Returns:
    //  //     The System.Windows.Forms.AccessibleRole.ComboBox value.
    //  public override AccessibleRole Role { get; }

    //  // Summary:
    //  //     Gets the accessible child corresponding to the specified index.
    //  //
    //  // Parameters:
    //  //   index:
    //  //     The zero-based index of the accessible child.
    //  //
    //  // Returns:
    //  //     An System.Windows.Forms.AccessibleObject that represents the accessible child
    //  //     corresponding to the specified index.
    //  public override AccessibleObject GetChild(int index);
    //  //
    //  // Summary:
    //  //     Retrieves the number of children belonging to an accessible object.
    //  //
    //  // Returns:
    //  //     Returns 3 in all cases.
    //  public override int GetChildCount();
    //}

    // Summary:
    //     Encapsulates a collection of objects for use by the System.Windows.Forms.DomainUpDown
    //     class.
    public class DomainUpDownItemCollection : ArrayList
    {
      private DomainUpDownItemCollection() { }

      // Summary:
      //     Gets or sets the item at the specified indexed location in the collection.
      //
      // Parameters:
      //   index:
      //     The indexed location of the item in the collection.
      //
      // Returns:
      //     An System.Object that represents the item at the specified indexed location.
      // [Browsable(false)]
      // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      //public override object this[int index] { get; set; }

      // Summary:
      //     Adds the specified object to the end of the collection.
      //
      // Parameters:
      //   item:
      //     The System.Object to be added to the end of the collection.
      //
      // Returns:
      //     The zero-based index value of the System.Object added to the collection.
      //public override int Add(object item);
      //
      // Summary:
      //     Inserts the specified object into the collection at the specified location.
      //
      // Parameters:
      //   index:
      //     The indexed location within the collection to insert the System.Object.
      //
      //   item:
      //     The System.Object to insert.
      //public override void Insert(int index, object item);
      //
      // Summary:
      //     Removes the specified item from the collection.
      //
      // Parameters:
      //   item:
      //     The System.Object to remove from the collection.
      //public override void Remove(object item);
      //
      // Summary:
      //     Removes the item from the specified location in the collection.
      //
      // Parameters:
      //   item:
      //     The indexed location of the System.Object in the collection.
      //public override void RemoveAt(int item);
    }
  }
}
