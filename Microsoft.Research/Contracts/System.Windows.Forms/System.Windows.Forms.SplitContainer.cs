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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents a control consisting of a movable bar that divides a container's
  //     display area into two resizable panels.
  // [Designer("System.Windows.Forms.Design.SplitContainerDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  // [ClassInterface(ClassInterfaceType.AutoDispatch)]
  // [DefaultEvent("SplitterMoved")]
  // [Docking(DockingBehavior.AutoDock)]
  // [ComVisible(true)]
  public class SplitContainer //: ContainerControl
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.SplitContainer class.
    //public SplitContainer();

    // Summary:
    //     When overridden in a derived class, gets or sets a value indicating whether
    //     scroll bars automatically appear if controls are placed outside the System.Windows.Forms.SplitContainer
    //     client area. This property is not relevant to this class.
    //
    // Returns:
    //     true if scroll bars to automatically appear when controls are placed outside
    //     the System.Windows.Forms.SplitContainer client area; otherwise, false. The
    //     default is false.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Localizable(true)]
    // [DefaultValue(false)]
    // [Browsable(false)]
    //public override bool AutoScroll { get; set; }
    //
    // Summary:
    //     Gets or sets the size of the auto-scroll margin. This property is not relevant
    //     to this class. This property is not relevant to this class.
    //
    // Returns:
    //     A System.Drawing.Size value that represents the height and width, in pixels,
    //     of the auto-scroll margin.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    //public System.Drawing.Size AutoScrollMargin { get; set; }
    //
    // Summary:
    //     Gets or sets the minimum size of the scroll bar. This property is not relevant
    //     to this class.
    //
    // Returns:
    //     A System.Drawing.Size that represents the minimum height and width of the
    //     scroll bar, in pixels.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    //public System.Drawing.Size AutoScrollMinSize { get; set; }
    //
    // Summary:
    //     This property is not relevant to this class.
    //
    // Returns:
    //     A System.Drawing.Point value.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    //public override System.Drawing.Point AutoScrollOffset { get; set; }
    //
    // Summary:
    //     This property is not relevant to this class.
    //
    // Returns:
    //     A System.Drawing.Point value.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public System.Drawing.Point AutoScrollPosition { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.SplitContainer
    //     is automatically resized to display its entire contents. This property is
    //     not relevant to this class.
    //
    // Returns:
    //     true if the System.Windows.Forms.SplitContainer is automatically resized;
    //     otherwise, false.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public override bool AutoSize { get; set; }
    //
    // Summary:
    //     Gets or sets the background image displayed in the control.
    //
    // Returns:
    //     An System.Drawing.Image that represents the image to display in the background
    //     of the control.
    // [Browsable(true)]
    // [EditorBrowsable(EditorBrowsableState.Always)]
    //public override System.Drawing.Image BackgroundImage { get; set; }
    //
    // Summary:
    //     This property is not relevant to this class.
    //
    // Returns:
    //     An System.Windows.Forms.ImageLayout value.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    //public override ImageLayout BackgroundImageLayout { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Windows.Forms.BindingContext for the System.Windows.Forms.SplitContainer.
    //
    // Returns:
    //     A System.Windows.Forms.BindingContext for the control.
    // [Browsable(false)]
    //public override BindingContext BindingContext { get; set; }
    //
    // Summary:
    //     Gets or sets the style of border for the System.Windows.Forms.SplitContainer.
    //
    // Returns:
    //     One of the System.Windows.Forms.BorderStyle values. The default is System.Windows.Forms.BorderStyle.Fixed3D.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value of the property is not one of the System.Windows.Forms.BorderStyle
    //     values.
    // [DispId(-504)]
    //public BorderStyle BorderStyle { get; set; }
    //
    // Summary:
    //     Gets a collection of child controls. This property is not relevant to this
    //     class.
    //
    // Returns:
    //     An object of type System.Windows.Forms.Control.ControlCollection that contains
    //     the child controls.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    //public Control.ControlCollection Controls { get; }
    //
    // Summary:
    //     Gets the default size of the System.Windows.Forms.SplitContainer.
    //
    // Returns:
    //     A System.Drawing.Size that represents the size of the System.Windows.Forms.SplitContainer.
    // protected override System.Drawing.Size DefaultSize { get; }
    //
    // Summary:
    //     Gets or sets which System.Windows.Forms.SplitContainer borders are attached
    //     to the edges of the container.
    //
    // Returns:
    //     One of the System.Windows.Forms.DockStyle values. The default value is None.
    //public DockStyle Dock { get; set; }
    //
    // Summary:
    //     Gets or sets which System.Windows.Forms.SplitContainer panel remains the
    //     same size when the container is resized.
    //
    // Returns:
    //     One of the values of System.Windows.Forms.FixedPanel. The default value is
    //     None.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The assigned value is not one of the System.Windows.Forms.FixedPanel values.
    //public FixedPanel FixedPanel { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the splitter is fixed or movable.
    //
    // Returns:
    //     true if the splitter is fixed; otherwise, false. The default is false.
    // [DefaultValue(false)]
    // [Localizable(true)]
    //public bool IsSplitterFixed { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating the horizontal or vertical orientation of
    //     the System.Windows.Forms.SplitContainer panels.
    //
    // Returns:
    //     One of the System.Windows.Forms.Orientation values. The default is Vertical.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The assigned value is not one of the System.Windows.Forms.Orientation values.
    // [Localizable(true)]
    //public Orientation Orientation { get; set; }
    //
    // Summary:
    //     Gets or sets the interior spacing, in pixels, between the edges of a System.Windows.Forms.SplitterPanel
    //     and its contents. This property is not relevant to this class.
    //
    // Returns:
    //     An object of type System.Windows.Forms.Padding representing the interior
    //     spacing.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    //public Padding Padding { get; set; }
    //
    // Summary:
    //     Gets the left or top panel of the System.Windows.Forms.SplitContainer, depending
    //     on System.Windows.Forms.SplitContainer.Orientation.
    //
    // Returns:
    //     If System.Windows.Forms.SplitContainer.Orientation is Vertical, the left
    //     panel of the System.Windows.Forms.SplitContainer. If System.Windows.Forms.SplitContainer.Orientation
    //     is Horizontal, the top panel of the System.Windows.Forms.SplitContainer.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    // [Localizable(false)]
    public SplitterPanel Panel1
    {
      get
      {
        Contract.Ensures(Contract.Result<SplitterPanel>() != null);
        return default(SplitterPanel);
      }
    }
    //
    // Summary:
    //     Gets or sets a value determining whether System.Windows.Forms.SplitContainer.Panel1
    //     is collapsed or expanded.
    //
    // Returns:
    //     true if System.Windows.Forms.SplitContainer.Panel1 is collapsed; otherwise,
    //     false. The default is false.
    // [DefaultValue(false)]
    //public bool Panel1Collapsed { get; set; }
    //
    // Summary:
    //     Gets or sets the minimum distance in pixels of the splitter from the left
    //     or top edge of System.Windows.Forms.SplitContainer.Panel1.
    //
    // Returns:
    //     An System.Int32 representing the minimum distance in pixels of the splitter
    //     from the left or top edge of System.Windows.Forms.SplitContainer.Panel1.
    //     The default value is 25 pixels, regardless of System.Windows.Forms.SplitContainer.Orientation.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The specified value is incompatible with the orientation.
    // [RefreshProperties(RefreshProperties.All)]
    // [DefaultValue(25)]
    // [Localizable(true)]
    //public int Panel1MinSize { get; set; }
    //
    // Summary:
    //     Gets the right or bottom panel of the System.Windows.Forms.SplitContainer,
    //     depending on System.Windows.Forms.SplitContainer.Orientation.
    //
    // Returns:
    //     If System.Windows.Forms.SplitContainer.Orientation is Vertical, the right
    //     panel of the System.Windows.Forms.SplitContainer. If System.Windows.Forms.SplitContainer.Orientation
    //     is Horizontal, the bottom panel of the System.Windows.Forms.SplitContainer.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    // [Localizable(false)]
    //public SplitterPanel Panel2 { get; }
    //
    // Summary:
    //     Gets or sets a value determining whether System.Windows.Forms.SplitContainer.Panel2
    //     is collapsed or expanded.
    //
    // Returns:
    //     true if System.Windows.Forms.SplitContainer.Panel2 is collapsed; otherwise,
    //     false. The default is false.
    // [DefaultValue(false)]
    //public bool Panel2Collapsed { get; set; }
    //
    // Summary:
    //     Gets or sets the minimum distance in pixels of the splitter from the right
    //     or bottom edge of System.Windows.Forms.SplitContainer.Panel2.
    //
    // Returns:
    //     An System.Int32 representing the minimum distance in pixels of the splitter
    //     from the right or bottom edge of System.Windows.Forms.SplitContainer.Panel2.
    //     The default value is 25 pixels, regardless of System.Windows.Forms.SplitContainer.Orientation.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The specified value is incompatible with the orientation.
    // [RefreshProperties(RefreshProperties.All)]
    // [DefaultValue(25)]
    // [Localizable(true)]
    //public int Panel2MinSize { get; set; }
    //
    // Summary:
    //     Gets or sets the location of the splitter, in pixels, from the left or top
    //     edge of the System.Windows.Forms.SplitContainer.
    //
    // Returns:
    //     An System.Int32 representing the location of the splitter, in pixels, from
    //     the left or top edge of the System.Windows.Forms.SplitContainer. The default
    //     value is 50 pixels.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value is less than zero.
    //
    //   System.InvalidOperationException:
    //     The value is incompatible with the orientation.
    // [DefaultValue(50)]
    // [Localizable(true)]
    // [SettingsBindable(true)]
    extern public int SplitterDistance { get; set; }
    //
    // Summary:
    //     Gets or sets a value representing the increment of splitter movement in pixels.
    //
    // Returns:
    //     An System.Int32 representing the increment of splitter movement in pixels.
    //     The default value is one pixel.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value is less than one.
    // [Localizable(true)]
    // [DefaultValue(1)]
    //public int SplitterIncrement { get; set; }
    //
    // Summary:
    //     Gets the size and location of the splitter relative to the System.Windows.Forms.SplitContainer.
    //
    // Returns:
    //     A System.Drawing.Rectangle that specifies the size and location of the splitter
    //     relative to the System.Windows.Forms.SplitContainer.
    // [Browsable(false)]
    //public System.Drawing.Rectangle SplitterRectangle { get; }
    //
    // Summary:
    //     Gets or sets the width of the splitter in pixels.
    //
    // Returns:
    //     An System.Int32 representing the width of the splitter, in pixels. The default
    //     is four pixels.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value is less than one or is incompatible with the orientation.
    // [Localizable(true)]
    // [DefaultValue(4)]
    //public int SplitterWidth { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the user can give the focus to the
    //     splitter using the TAB key.
    //
    // Returns:
    //     true if the user can give the focus to the splitter using the TAB key; otherwise,
    //     false. The default is true.
    // [DefaultValue(true)]
    // [DispId(-516)]
    //public bool TabStop { get; set; }
    //
    // Summary:
    //     This property is not relevant to this class.
    //
    // Returns:
    //     A string.
    // [Bindable(false)]
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    //public override string Text { get; set; }

    // Summary:
    //     Occurs when the value of the System.Windows.Forms.SplitContainer.AutoSize
    //     property changes. This property is not relevant to this class.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // public event EventHandler AutoSizeChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.SplitContainer.BackgroundImage property
    //     changes.
    // [EditorBrowsable(EditorBrowsableState.Always)]
    // [Browsable(true)]
    // public event EventHandler BackgroundImageChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.SplitContainer.BackgroundImageLayout
    //     property changes. This event is not relevant to this class.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // public event EventHandler BackgroundImageLayoutChanged;
    //
    // Summary:
    //     This event is not relevant to this class.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    // public event ControlEventHandler ControlAdded;
    //
    // Summary:
    //     This event is not relevant to this class.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    // public event ControlEventHandler ControlRemoved;
    //
    // Summary:
    //     This event is not relevant to this class.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // public event EventHandler PaddingChanged;
    //
    // Summary:
    //     Occurs when the splitter control is moved.
    // public event SplitterEventHandler SplitterMoved;
    //
    // Summary:
    //     Occurs when the splitter control is in the process of moving.
    // public event SplitterCancelEventHandler SplitterMoving;
    //
    // Summary:
    //     This event is not relevant to this class.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    // public event EventHandler TextChanged;

    // Summary:
    //     Creates a new instance of the control collection for the control.
    //
    // Returns:
    //     A new instance of System.Windows.Forms.Control.ControlCollection assigned
    //     to the control.
    // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override Control.ControlCollection CreateControlsInstance();
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.GotFocus event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnGotFocus(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.KeyDown event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.KeyEventArgs that contains the event data.
    // protected override void OnKeyDown(KeyEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.KeyUp event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.KeyEventArgs that contains the event data.
    // protected override void OnKeyUp(KeyEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Layout event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.LayoutEventArgs that contains the event data.
    // protected override void OnLayout(LayoutEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.LostFocus event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnLostFocus(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnMouseCaptureChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseDown event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    // protected override void OnMouseDown(MouseEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseLeave event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnMouseLeave(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseMove event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void OnMouseMove(MouseEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseUp event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    // protected override void OnMouseUp(MouseEventArgs e);
    // protected override void OnMove(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Paint event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.PaintEventArgs that contains the event data.
    // protected override void OnPaint(PaintEventArgs e);
    //
    // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void OnRightToLeftChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.SplitContainer.SplitterMoved event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.SplitterEventArgs that contains the event data.
    //public void OnSplitterMoved(SplitterEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.SplitContainer.SplitterMoving event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.SplitterEventArgs that contains the event data.
    //public void OnSplitterMoving(SplitterCancelEventArgs e);
    //
    // Summary:
    //     Processes a dialog box key.
    //
    // Parameters:
    //   keyData:
    //     One of the System.Windows.Forms.Keys values that represents the key to process.
    //
    // Returns:
    //     true if the key was processed by the control; otherwise, false.
    // protected override bool ProcessDialogKey(Keys keyData);
    //
    // Summary:
    //     Selects the next available control and makes it the active control.
    //
    // Parameters:
    //   forward:
    //     true to cycle forward through the controls in the System.Windows.Forms.ContainerControl;
    //     otherwise, false.
    //
    // Returns:
    //     true if a control is selected; otherwise, false.
    // protected override bool ProcessTabKey(bool forward);
    //
    // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void ScaleControl(System.Drawing.SizeF factor, BoundsSpecified specified);
    //
    // protected override void Select(bool directed, bool forward);
    //
    //
    // Parameters:
    //   x:
    //     The new System.Windows.Forms.Control.Left property value of the control.
    //
    //   y:
    //     The new System.Windows.Forms.Control.Top property value of the control.
    //
    //   width:
    //     The new System.Windows.Forms.Control.Width property value of the control.
    //
    //   height:
    //     The new System.Windows.Forms.Control.Height property value of the control.
    //
    //   specified:
    //     A bitwise combination of the System.Windows.Forms.BoundsSpecified values.
    // protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified);
    //
    // Summary:
    //     Processes Windows messages.
    //
    // Parameters:
    //   msg:
    //     The Windows System.Windows.Forms.Message to process.
    // protected override void WndProc(ref Message msg);
  }
}
