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
  //     Provides basic functionality for controls that display a System.Windows.Forms.ToolStripDropDown
  //     when a System.Windows.Forms.ToolStripDropDownButton, System.Windows.Forms.ToolStripMenuItem,
  //     or System.Windows.Forms.ToolStripSplitButton control is clicked.
  //[Designer("System.Windows.Forms.Design.ToolStripMenuItemDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  //[DefaultProperty("DropDownItems")]
  public abstract class ToolStripDropDownItem 
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolStripDropDownItem
    //     class.
    // protected ToolStripDropDownItem();
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolStripDropDownItem
    //     class with the specified display text, image, and action to take when the
    //     drop-down control is clicked.
    //
    // Parameters:
    //   text:
    //     The display text of the drop-down control.
    //
    //   image:
    //     The System.Drawing.Image to be displayed on the control.
    //
    //   onClick:
    //     The action to take when the drop-down control is clicked.
    // protected ToolStripDropDownItem(string text, System.Drawing.Image image, EventHandler onClick);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolStripDropDownItem
    //     class with the specified display text, image, and System.Windows.Forms.ToolStripItem
    //     collection that the drop-down control contains.
    //
    // Parameters:
    //   text:
    //     The display text of the drop-down control.
    //
    //   image:
    //     The System.Drawing.Image to be displayed on the control.
    //
    //   dropDownItems:
    //     A System.Windows.Forms.ToolStripItem collection that the drop-down control
    //     contains.
    // protected ToolStripDropDownItem(string text, System.Drawing.Image image, params ToolStripItem//[] dropDownItems);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolStripDropDownItem
    //     class with the specified display text, image, action to take when the drop-down
    //     control is clicked, and control name.
    //
    // Parameters:
    //   text:
    //     The display text of the drop-down control.
    //
    //   image:
    //     The System.Drawing.Image to be displayed on the control.
    //
    //   onClick:
    //     The action to take when the drop-down control is clicked.
    //
    //   name:
    //     The name of the control.
    // protected ToolStripDropDownItem(string text, System.Drawing.Image image, EventHandler onClick, string name);

    // Summary:
    //     Gets or sets the System.Windows.Forms.ToolStripDropDown that will be displayed
    //     when this System.Windows.Forms.ToolStripDropDownItem is clicked.
    //
    // Returns:
    //     A System.Windows.Forms.ToolStripDropDown that is associated with the System.Windows.Forms.ToolStripDropDownItem.
    //[TypeConverter(typeof(ReferenceConverter))]
    //public ToolStripDropDown DropDown { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating the direction in which the System.Windows.Forms.ToolStripDropDownItem
    //     emerges from its parent container.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripDropDownDirection values.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The property is set to a value that is not one of the System.Windows.Forms.ToolStripDropDownDirection
    //     values.
    //[Browsable(false)]
    //public ToolStripDropDownDirection DropDownDirection { get; set; }
    //
    // Summary:
    //     Gets the collection of items in the System.Windows.Forms.ToolStripDropDown
    //     that is associated with this System.Windows.Forms.ToolStripDropDownItem.
    //
    // Returns:
    //     A System.Windows.Forms.ToolStripItemCollection of controls.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToolStripItemCollection DropDownItems 
    {
      get
      {
        Contract.Ensures(Contract.Result<ToolStripItemCollection>() != null);

        return default(ToolStripItemCollection);
      }
    }
    //
    // Summary:
    //     Gets the screen coordinates, in pixels, of the upper-left corner of the System.Windows.Forms.ToolStripDropDownItem.
    //
    // Returns:
    //     A Point representing the x and y screen coordinates, in pixels.
    // protected internal virtual System.Drawing.Point DropDownLocation { get; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Windows.Forms.ToolStripDropDownItem
    //     has System.Windows.Forms.ToolStripDropDown controls associated with it.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolStripDropDownItem has System.Windows.Forms.ToolStripDropDown
    //     controls; otherwise, false.
    //[Browsable(false)]
    // public virtual bool HasDropDownItems { get; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Windows.Forms.ToolStripDropDownItem
    //     is in the pressed state.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolStripDropDownItem is in the pressed
    //     state; otherwise, false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    // public override bool Pressed { get; }

    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStripDropDown closes.
    // public event EventHandler DropDownClosed;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStripDropDown is clicked.
    // public event ToolStripItemClickedEventHandler DropDownItemClicked;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStripDropDown has opened.
    // public event EventHandler DropDownOpened;
    //
    // Summary:
    //     Occurs as the System.Windows.Forms.ToolStripDropDown is opening.
    // public event EventHandler DropDownOpening;

    // protected override AccessibleObject CreateAccessibilityInstance();
    //
    // Summary:
    //     Creates a generic System.Windows.Forms.ToolStripDropDown for which events
    //     can be defined.
    //
    // Returns:
    //     A System.Windows.Forms.ToolStripDropDown.
    // protected virtual ToolStripDropDown CreateDefaultDropDown();
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Windows.Forms.ToolStripDropDownItem
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    // protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Makes a visible System.Windows.Forms.ToolStripDropDown hidden.
    //public void HideDropDown();
    //
    // protected override void OnBoundsChanged();
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripDropDownItem.DropDownClosed event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected internal virtual void OnDropDownClosed(EventArgs e);
    //
    // Summary:
    //     Raised in response to the System.Windows.Forms.ToolStripDropDownItem.HideDropDown()
    //     method.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnDropDownHide(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripDropDownItem.DropDownItemClicked
    //     event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ToolStripItemClickedEventArgs that contains the event
    //     data.
    // protected internal virtual void OnDropDownItemClicked(ToolStripItemClickedEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripDropDownItem.DropDownOpened event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected internal virtual void OnDropDownOpened(EventArgs e);
    //
    // Summary:
    //     Raised in response to the System.Windows.Forms.ToolStripDropDownItem.ShowDropDown()
    //     method.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnDropDownShow(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripDropDown.FontChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnFontChanged(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnRightToLeftChanged(EventArgs e);
    //
    // protected internal override bool ProcessCmdKey(ref Message m, Keys keyData);
    //
    // protected internal override bool ProcessDialogKey(Keys keyData);
    //
    // Summary:
    //     Displays the System.Windows.Forms.ToolStripDropDownItem control associated
    //     with this System.Windows.Forms.ToolStripDropDownItem.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Windows.Forms.ToolStripDropDownItem is the same as the parent
    //     System.Windows.Forms.ToolStrip.
    //public void ShowDropDown();
  }
}
