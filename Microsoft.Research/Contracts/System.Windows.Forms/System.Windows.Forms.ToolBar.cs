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
  //     Represents a Windows toolbar. Although System.Windows.Forms.ToolStrip replaces
  //     and adds functionality to the System.Windows.Forms.ToolBar control of previous
  //     versions, System.Windows.Forms.ToolBar is retained for both backward compatibility
  //     and future use if you choose.
  public class ToolBar
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolBar class.
    public ToolBar() { }

    // Summary:
    //     Gets or set the value that determines the appearance of a toolbar control
    //     and its buttons.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolBarAppearance values. The default is
    //     ToolBarAppearance.Normal.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The assigned value is not one of the System.Windows.Forms.ToolBarAppearance
    //     values.
    //[Localizable(true)]
    //public ToolBarAppearance Appearance { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the toolbar adjusts its size automatically,
    //     based on the size of the buttons and the dock style.
    //
    // Returns:
    //     true if the toolbar adjusts its size automatically, based on the size of
    //     the buttons and dock style; otherwise, false. The default is true.
    ////[Localizable(true)]
    ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //[Browsable(true)]
    //[DefaultValue(true)]
    // // public override bool AutoSize { get; set; }
    //
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     A System.Drawing.Color.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    // public override System.Drawing.Color BackColor { get; set; }
    //
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     An System.Drawing.Image.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    // public override System.Drawing.Image BackgroundImage { get; set; }
    //
    // Summary:
    //     This property is not meaningful for this control.
    //
    // Returns:
    //     An System.Windows.Forms.ImageLayout value.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public override ImageLayout BackgroundImageLayout { get; set; }
    //
    // Summary:
    //     Gets or sets the border style of the toolbar control.
    //
    // Returns:
    //     One of the System.Windows.Forms.BorderStyle values. The default is BorderStyle.None.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The assigned value is not one of the System.Windows.Forms.BorderStyle values.
    //[DispId(-504)]
    //public BorderStyle BorderStyle { get; set; }
    //
    // Summary:
    //     Gets the collection of System.Windows.Forms.ToolBarButton controls assigned
    //     to the toolbar control.
    //
    // Returns:
    //     A System.Windows.Forms.ToolBar.ToolBarButtonCollection that contains a collection
    //     of System.Windows.Forms.ToolBarButton controls.
    //[Localizable(true)]
    //[MergableProperty(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToolBar.ToolBarButtonCollection Buttons 
    {
      get
      {
        // To add: \forall c in result. c != null
        Contract.Ensures(Contract.Result<ToolBar.ToolBarButtonCollection>() != null);

        return default(ToolBar.ToolBarButtonCollection);
      }
    }
    //
    // Summary:
    //     Gets or sets the size of the buttons on the toolbar control.
    //
    // Returns:
    //     A System.Drawing.Size object that represents the size of the System.Windows.Forms.ToolBarButton
    //     controls on the toolbar. The default size has a width of 24 pixels and a
    //     height of 22 pixels, or large enough to accommodate the System.Drawing.Image
    //     and text, whichever is greater.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Drawing.Size.Width or System.Drawing.Size.Height property of the
    //     System.Drawing.Size object is less than 0.
    //[RefreshProperties(RefreshProperties.All)]
    //[Localizable(true)]
    //public System.Drawing.Size ButtonSize { get; set; }

    //
    // Returns:
    //     A System.Windows.Forms.CreateParams that contains the required creation parameters
    //     when the handle to the control is created.
    // // protected override CreateParams CreateParams { get; }
    //
    // Summary:
    //     Gets the default Input Method Editor (IME) mode supported by this control.
    //
    // Returns:
    //     One of the System.Windows.Forms.ImeMode values.
    // protected override ImeMode DefaultImeMode { get; }
    //
    //
    // Returns:
    //     The default System.Drawing.Size of the control.
    // protected override System.Drawing.Size DefaultSize { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the toolbar displays a divider.
    //
    // Returns:
    //     true if the toolbar displays a divider; otherwise, false. The default is
    //     true.
    //[DefaultValue(true)]
    //public bool Divider { get; set; }
    //
    //
    // Returns:
    //     One of the System.Windows.Forms.DockStyle values. The default is System.Windows.Forms.DockStyle.None.
    //[Localizable(true)]
    // public override DockStyle Dock { get; set; }
    //
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     A System.Boolean value.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // protected override bool DoubleBuffered { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether drop-down buttons on a toolbar display
    //     down arrows.
    //
    // Returns:
    //     true if drop-down toolbar buttons display down arrows; otherwise, false.
    //     The default is false.
    //[Localizable(true)]
    //[DefaultValue(false)]
    //public bool DropDownArrows { get; set; }
    //
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     A System.Drawing.Color.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public override System.Drawing.Color ForeColor { get; set; }
    //
    // Summary:
    //     Gets or sets the collection of images available to the toolbar button controls.
    //
    // Returns:
    //     An System.Windows.Forms.ImageList that contains images available to the System.Windows.Forms.ToolBarButton
    //     controls. The default is null.
    //[DefaultValue("")]
    //public ImageList ImageList { get; set; }
    ////
    //// Summary:
    ////     Gets the size of the images in the image list assigned to the toolbar.
    ////
    //// Returns:
    ////     A System.Drawing.Size that represents the size of the images (in the System.Windows.Forms.ImageList)
    ////     assigned to the System.Windows.Forms.ToolBar.
    ////[EditorBrowsable(EditorBrowsableState.Advanced)]
    ////[Browsable(false)]
    ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public System.Drawing.Size ImageSize { get; }
    ////
    //// Summary:
    ////     This member is not meaningful for this control.
    ////
    //// Returns:
    ////     One of the System.Windows.Forms.ImeMode values.
    ////[EditorBrowsable(EditorBrowsableState.Never)]
    ////[Browsable(false)]
    //public ImeMode ImeMode { get; set; }
    ////
    //// Summary:
    ////     This member is not meaningful for this control.
    ////
    //// Returns:
    ////     A System.Windows.Forms.RightToLeft value.
    ////[EditorBrowsable(EditorBrowsableState.Never)]
    ////[Browsable(false)]
    //// public override RightToLeft RightToLeft { get; set; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether the toolbar displays a ToolTip for
    ////     each button.
    ////
    //// Returns:
    ////     true if the toolbar display a ToolTip for each button; otherwise, false.
    ////     The default is false.
    ////[Localizable(true)]
    ////[DefaultValue(false)]
    //public bool ShowToolTips { get; set; }
    ////
    ////[DefaultValue(false)]
    //public bool TabStop { get; set; }
    ////
    //// Summary:
    ////     This member is not meaningful for this control.
    ////
    //// Returns:
    ////     A System.String.
    ////[EditorBrowsable(EditorBrowsableState.Never)]
    ////[Browsable(false)]
    ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    ////[Bindable(false)]
    //// public override string Text { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the alignment of text in relation to each image displayed on
    ////     the toolbar button controls.
    ////
    //// Returns:
    ////     One of the System.Windows.Forms.ToolBarTextAlign values. The default is ToolBarTextAlign.Underneath.
    ////
    //// Exceptions:
    ////   System.ComponentModel.InvalidEnumArgumentException:
    ////     The assigned value is not one of the System.Windows.Forms.ToolBarTextAlign
    ////     values.
    ////[Localizable(true)]
    //public ToolBarTextAlign TextAlign { get; set; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether the toolbar buttons wrap to the next
    ////     line if the toolbar becomes too small to display all the buttons on the same
    ////     line.
    ////
    //// Returns:
    ////     true if the toolbar buttons wrap to another line if the toolbar becomes too
    ////     small to display all the buttons on the same line; otherwise, false. The
    ////     default value is true.
    ////[Localizable(true)]
    ////[DefaultValue(true)]
    //public bool Wrappable { get; set; }

    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ToolBar.AutoSize property
    //     has changed.
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //[Browsable(true)]
    // public event EventHandler AutoSizeChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolBar.BackColor property changes.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    // public event EventHandler BackColorChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolBar.BackgroundImage property changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public event EventHandler BackgroundImageChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolBar.BackgroundImageLayout property
    //     changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public event EventHandler BackgroundImageLayoutChanged;
    //
    // Summary:
    //     Occurs when a System.Windows.Forms.ToolBarButton on the System.Windows.Forms.ToolBar
    //     is clicked.
    // public event ToolBarButtonClickEventHandler ButtonClick;
    //
    // Summary:
    //     Occurs when a drop-down style System.Windows.Forms.ToolBarButton or its down
    //     arrow is clicked.
    // public event ToolBarButtonClickEventHandler ButtonDropDown;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolBar.ForeColor property changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public event EventHandler ForeColorChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolBar.ImeMode property changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public event EventHandler ImeModeChanged;
    //
    // Summary:
    //     This member is not meaningful for this control.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public event PaintEventHandler Paint;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolBar.RightToLeft property changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public event EventHandler RightToLeftChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolBar.Text property changes.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    // public event EventHandler TextChanged;

    // protected override void CreateHandle();
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Windows.Forms.ToolBar
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    // protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolBar.ButtonClick event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ToolBarButtonClickEventArgs that contains the event
    //     data.
    // protected virtual void OnButtonClick(ToolBarButtonClickEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolBar.ButtonDropDown event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ToolBarButtonClickEventArgs that contains the event
    //     data.
    // protected virtual void OnButtonDropDown(ToolBarButtonClickEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.FontChanged event.
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
    //
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnHandleCreated(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnResize(EventArgs e);
    //
    //
    // Parameters:
    //   factor:
    //     The factor by which the height and width of the control will be scaled.
    //
    //   specified:
    //     A System.Windows.Forms.BoundsSpecified value that specifies the bounds of
    //     the control to use when defining its size and position.
    // protected override void ScaleControl(System.Drawing.SizeF factor, BoundsSpecified specified);
    //
    //
    // Parameters:
    //   dx:
    //     The horizontal scaling factor.
    //
    //   dy:
    //     The vertical scaling factor.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // protected override void ScaleCore(float dx, float dy);
    //
    // Summary:
    //     Sets the specified bounds of the System.Windows.Forms.ToolBar control.
    //
    // Parameters:
    //   x:
    //     The new Left property value of the control.
    //
    //   y:
    //     The new Top property value of the control.
    //
    //   width:
    //     The new Width property value of the control.
    //
    //   height:
    //     Not used.
    //
    //   specified:
    //     A bitwise combination of the System.Windows.Forms.BoundsSpecified values.
    // protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified);
    //
    // Summary:
    //     Returns a string that represents the System.Windows.Forms.ToolBar control.
    //
    // Returns:
    //     A String that represents the current System.Windows.Forms.ToolBar.
    // public override string ToString();
    //
    //
    // Parameters:
    //   m:
    //     The Windows System.Windows.Forms.Message to process.
    // protected override void WndProc(ref Message m);

    public class ToolBarButtonCollection 
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ToolBar.ToolBarButtonCollection
      //     class and assigns it to the specified toolbar.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ToolBar that is the parent of the collection of
      //     System.Windows.Forms.ToolBarButton controls.
      public ToolBarButtonCollection(ToolBar owner) { }



      // Summary:
      //     Gets or sets the toolbar button at the specified indexed location in the
      //     toolbar button collection.
      //
      // Parameters:
      //   index:
      //     The indexed location of the System.Windows.Forms.ToolBarButton in the collection.
      //
      // Returns:
      //     A System.Windows.Forms.ToolBarButton that represents the toolbar button at
      //     the specified indexed location.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     The index value is null.
      //
      //   System.ArgumentOutOfRangeException:
      //     The index value is less than zero.-or- The index value is greater than the
      //     number of buttons in the collection, and the collection of buttons is not
      //     null.
      //public virtual ToolBarButton this[int index] { get; set; }
      ////
      //// Summary:
      ////     Gets a System.Windows.Forms.ToolBarButton with the specified key from the
      ////     collection.
      ////
      //// Parameters:
      ////   key:
      ////     The name of the System.Windows.Forms.ToolBarButton to retrieve.
      ////
      //// Returns:
      ////     The System.Windows.Forms.ToolBarButton whose System.Windows.Forms.ToolBarButton.Name
      ////     property matches the specified key.
      //public virtual ToolBarButton this[string key] { get; }
    }
  }
}
