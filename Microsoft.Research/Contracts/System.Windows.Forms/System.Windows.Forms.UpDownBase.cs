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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
  // Summary:
  //     Implements the basic functionality required by a spin box (also known as
  //     an up-down control).
  ////[Designer("System.Windows.Forms.Design.UpDownBaseDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  ////[ComVisible(true)]
  ////[ClassInterface(ClassInterfaceType.AutoDispatch)]
  public abstract class UpDownBase : ContainerControl
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.UpDownBase class.
    public UpDownBase() { }

    // Summary:
    //     Gets a value indicating whether the container will allow the user to scroll
    //     to any controls placed outside of its visible boundaries.
    //
    // Returns:
    //     false in all cases.
    ////[Browsable(false)]
    ////[EditorBrowsable(EditorBrowsableState.Never)]
    ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public override bool AutoScroll { get; set; }
    //
    // Summary:
    //     Gets or sets the size of the auto-scroll margin.
    //
    // Returns:
    //     A System.Drawing.Size that represents the height and width, in pixels, of
    //     the auto-scroll margin.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Drawing.Size.Height or System.Drawing.Size.Width is less than
    //     0.
    //////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //////[EditorBrowsable(EditorBrowsableState.Never)]
    //////[Browsable(false)]
    public System.Drawing.Size AutoScrollMargin 
    {
      get
      {
        return default(System.Drawing.Size);
      }
      set
      {
        Contract.Requires(value.Height >= 0);
        Contract.Requires(value.Width >= 0);
      }
    }
    //
    // Summary:
    //     Gets or sets the minimum size of the auto-scroll area.
    //
    // Returns:
    //     A System.Drawing.Size that represents the minimum height and width, in pixels,
    //     of the scroll bars.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public System.Drawing.Size AutoScrollMinSize { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the control should automatically
    //     resize based on its contents.
    //
    // Returns:
    //     true to indicate the control should automatically resize based on its contents;
    //     otherwise, false.
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //[Browsable(true)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    //public override bool AutoSize { get; set; }
    //
    // Summary:
    //     Gets or sets the background color for the text box portion of the spin box
    //     (also known as an up-down control).
    //
    // Returns:
    //     A System.Drawing.Color that represents the background color of the text box
    //     portion of the spin box.
    //public override System.Drawing.Color BackColor { get; set; }
    //
    // Summary:
    //     Gets or sets the background image for the System.Windows.Forms.UpDownBase.
    //
    // Returns:
    //     The background image for the System.Windows.Forms.UpDownBase.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public override System.Drawing.Image BackgroundImage { get; set; }
    //
    // Summary:
    //     Gets or sets the layout of the System.Windows.Forms.UpDownBase.BackgroundImage
    //     of the System.Windows.Forms.UpDownBase.
    //
    // Returns:
    //     One of the System.Windows.Forms.ImageLayout values.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public override ImageLayout BackgroundImageLayout { get; set; }
    //
    // Summary:
    //     Gets or sets the border style for the spin box (also known as an up-down
    //     control).
    //
    // Returns:
    //     One of the System.Windows.Forms.BorderStyle values. The default value is
    //     System.Windows.Forms.BorderStyle.Fixed3D.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value assigned is not one of the System.Windows.Forms.BorderStyle values.
    //[DispId(-504)]
    //public BorderStyle BorderStyle { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the text property is being changed
    //     internally by its parent class.
    //
    // Returns:
    //     true if the System.Windows.Forms.UpDownBase.Text property is being changed
    //     internally by the System.Windows.Forms.UpDownBase class; otherwise, false.
    //protected bool ChangingText { get; set; }
    //
    // Summary:
    //     Gets or sets the shortcut menu associated with the spin box (also known as
    //     an up-down control).
    //
    // Returns:
    //     The System.Windows.Forms.ContextMenu associated with the spin box.
    //public override ContextMenu ContextMenu { get; set; }
    //
    // Summary:
    //     Gets or sets the shortcut menu for the spin box (also known as an up-down
    //     control).
    //
    // Returns:
    //     The System.Windows.Forms.ContextMenuStrip associated with the control.
    //public override ContextMenuStrip ContextMenuStrip { get; set; }
    //
    // Summary:
    //     Overrides the System.Windows.Forms.Control.CreateParams property.
    ////protected override CreateParams CreateParams { get; }
    //
    //
    // Returns:
    //     The default System.Drawing.Size of the control.
    ////protected override System.Drawing.Size DefaultSize { get; }
    //
    // Summary:
    //     Gets the dock padding settings for all edges of the System.Windows.Forms.UpDownBase
    //     control.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public ScrollableControl.DockPaddingEdges DockPadding { get; }
    //
    // Summary:
    //     Returns true if this control has focus.
    //
    // Returns:
    //     true if the control has focus; otherwise, false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public override bool Focused { get; }
    //
    // Summary:
    //     Gets or sets the foreground color of the spin box (also known as an up-down
    //     control).
    //
    // Returns:
    //     The foreground System.Drawing.Color of the spin box.
    //public override System.Drawing.Color ForeColor { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the user can use the UP ARROW and
    //     DOWN ARROW keys to select values.
    //
    // Returns:
    //     true if the control allows the use of the UP ARROW and DOWN ARROW keys to
    //     select values; otherwise, false. The default value is true.
    //[DefaultValue(true)]
    //public bool InterceptArrowKeys { get; set; }
    //
    // Summary:
    //     Gets or sets the maximum size of the spin box (also known as an up-down control).
    //
    // Returns:
    //     The System.Drawing.Size, which is the maximum size of the spin box.
    //public override System.Drawing.Size MaximumSize { get; set; }
    //
    // Summary:
    //     Gets or sets the minimum size of the spin box (also known as an up-down control).
    //
    // Returns:
    //     The System.Drawing.Size, which is the minimum size of the spin box.
    //public override System.Drawing.Size MinimumSize { get; set; }
    //
    // Summary:
    //     Gets the height of the spin box (also known as an up-down control).
    //
    // Returns:
    //     The height, in pixels, of the spin box.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public int PreferredHeight { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the text can be changed by the use
    //     of the up or down buttons only.
    //
    // Returns:
    //     true if the text can be changed by the use of the up or down buttons only;
    //     otherwise, false. The default value is false.
    //[DefaultValue(false)]
    //public bool ReadOnly { get; set; }
    //
    // Summary:
    //     Gets or sets the text displayed in the spin box (also known as an up-down
    //     control).
    //
    // Returns:
    //     The string value displayed in the spin box.
    //[Localizable(true)]
    //public override string Text { get; set; }
    //
    // Summary:
    //     Gets or sets the alignment of the text in the spin box (also known as an
    //     up-down control).
    //
    // Returns:
    //     One of the System.Windows.Forms.HorizontalAlignment values. The default value
    //     is System.Windows.Forms.HorizontalAlignment.Left.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value assigned is not one of the System.Windows.Forms.HorizontalAlignment
    //     values.
    //[Localizable(true)]
    //public HorizontalAlignment TextAlign { get; set; }
    //
    // Summary:
    //     Gets or sets the alignment of the up and down buttons on the spin box (also
    //     known as an up-down control).
    //
    // Returns:
    //     One of the System.Windows.Forms.LeftRightAlignment values. The default value
    //     is System.Windows.Forms.LeftRightAlignment.Right.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value assigned is not one of the System.Windows.Forms.LeftRightAlignment
    //     values.
    //[Localizable(true)]
    //public LeftRightAlignment UpDownAlign { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether a value has been entered by the user.
    //
    // Returns:
    //     true if the user has changed the System.Windows.Forms.UpDownBase.Text property;
    //     otherwise, false.
    //protected bool UserEdit { get; set; }

    // Summary:
    //     Occurs when the value of the System.Windows.Forms.UpDownBase.AutoSize property
    //     changes.
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //[Browsable(true)]
    //public event EventHandler AutoSizeChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.UpDownBase.BackgroundImage
    //     property changes.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public event EventHandler BackgroundImageChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.UpDownBase.BackgroundImageLayout
    //     property changes.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public event EventHandler BackgroundImageLayoutChanged;
    //
    // Summary:
    //     Occurs when the mouse pointer enters the System.Windows.Forms.UpDownBase
    //     control.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public event EventHandler MouseEnter;
    //
    // Summary:
    //     Occurs when the mouse pointer rests on the System.Windows.Forms.UpDownBase
    //     control.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public event EventHandler MouseHover;
    //
    // Summary:
    //     Occurs when the mouse pointer leaves the System.Windows.Forms.UpDownBase
    //     control.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public event EventHandler MouseLeave;
    //
    // Summary:
    //     Occurs when the user moves the mouse pointer over the System.Windows.Forms.UpDownBase
    //     control.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public event MouseEventHandler MouseMove;

    // Summary:
    //     When overridden in a derived class, handles the clicking of the down button
    //     on the spin box (also known as an up-down control).
    //public abstract void DownButton();
    //
    // Summary:
    //     When overridden in a derived class, raises the Changed event.
    //
    // Parameters:
    //   source:
    //     The source of the event.
    //
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected virtual void OnChanged(object source, EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.FontChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnFontChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.HandleCreated event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnHandleCreated(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.HandleDestroyed event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnHandleDestroyed(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Layout event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.LayoutEventArgs that contains the event data.
    //protected override void OnLayout(LayoutEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseDown event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    //protected override void OnMouseDown(MouseEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseUp event.
    //
    // Parameters:
    //   mevent:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    //protected override void OnMouseUp(MouseEventArgs mevent);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseWheel event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    //protected override void OnMouseWheel(MouseEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Paint event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.PaintEventArgs that contains the event data.
    //protected override void OnPaint(PaintEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.KeyDown event.
    //
    // Parameters:
    //   source:
    //     The source of the event.
    //
    //   e:
    //     A System.Windows.Forms.KeyEventArgs that contains the event data.
    //protected virtual void OnTextBoxKeyDown(object source, KeyEventArgs e);
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
    //protected virtual void OnTextBoxKeyPress(object source, KeyPressEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.LostFocus event.
    //
    // Parameters:
    //   source:
    //     The source of the event.
    //
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected virtual void OnTextBoxLostFocus(object source, EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Resize event.
    //
    // Parameters:
    //   source:
    //     The source of the event.
    //
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected virtual void OnTextBoxResize(object source, EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.TextChanged event.
    //
    // Parameters:
    //   source:
    //     The source of the event.
    //
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected virtual void OnTextBoxTextChanged(object source, EventArgs e);
    //
    // Summary:
    //     Selects a range of text in the spin box (also known as an up-down control)
    //     specifying the starting position and number of characters to select.
    //
    // Parameters:
    //   start:
    //     The position of the first character to be selected.
    //
    //   length:
    //     The total number of characters to be selected.
    public void Select(int start, int length)
    {
      Contract.Requires(start >= 0);
      // F: in the code it seems there is no check in length
    }
    //
    // Summary:
    //     When overridden in a derived class, handles the clicking of the up button
    //     on the spin box (also known as an up-down control).
    //public abstract void UpButton();
    //
    // Summary:
    //     When overridden in a derived class, updates the text displayed in the spin
    //     box (also known as an up-down control).
    //protected abstract void UpdateEditText();
    //
    // Summary:
    //     When overridden in a derived class, validates the text displayed in the spin
    //     box (also known as an up-down control).
    //protected virtual void ValidateEditText();
    //
    // Summary:
    //     Overrides the System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)
    //     method.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //protected override void WndProc(ref Message m);
  }
}
