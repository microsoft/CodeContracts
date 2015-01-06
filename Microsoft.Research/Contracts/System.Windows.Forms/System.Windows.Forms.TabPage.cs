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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents a single tab page in a System.Windows.Forms.TabControl. 
  public class TabPage
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.TabPage class.
    public TabPage() { }
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.TabPage class and
    //     specifies the text for the tab.
    //
    // Parameters:
    //   text:
    //     The text for the tab.
    public TabPage(string text) { }

    //
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     The default is true.
    // public bool Enabled { get; set; }
    //
    // Summary:
    //     Gets or sets the index to the image displayed on this tab.
    //
    // Returns:
    //     The zero-based index to the image in the System.Windows.Forms.TabControl.ImageList
    //     that appears on the tab. The default is -1, which signifies no image.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Windows.Forms.TabPage.ImageIndex value is less than -1.
    public int ImageIndex
    {
      get
      {
        return default(int);
      }
      set
      {
        Contract.Requires(value >= -1);
      }
    }
    //
    // Summary:
    //     Gets or sets the key accessor for the image in the System.Windows.Forms.TabControl.ImageList
    //     of the associated System.Windows.Forms.TabControl.
    //
    // Returns:
    //     A string representing the key of the image.

    //public string ImageKey { get; set; }
    //
    // Summary:
    //     This property is not meaningful for this control.
    //
    // Returns:
    //     A System.Drawing.Point.

    //public System.Drawing.Point Location { get; set; }
    //
    // Summary:
    //     This property is not meaningful for this control.
    //
    // Returns:
    //     A System.Drawing.Size.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public override System.Drawing.Size MaximumSize { get; set; }
    ////
    //// Summary:
    ////     This property is not meaningful for this control.
    ////
    //// Returns:
    ////     A System.Drawing.Size.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public override System.Drawing.Size MinimumSize { get; set; }
    //
    // Summary:
    //     This property is not meaningful for this control.
    //
    // Returns:
    //     A System.Drawing.Size.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public System.Drawing.Size PreferredSize { get; }
    //
    // Summary:
    //     This property is not meaningful for this control.
    //
    // Returns:
    //     An System.Int32.
    //public int TabIndex { get; set; }
    //
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     The default is true.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public bool TabStop { get; set; }
    //
    // Summary:
    //     Gets or sets the text to display on the tab.
    //
    // Returns:
    //     The text to display on the tab.
    //[Browsable(true)]
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //[Localizable(true)]
    //public override string Text { get; set; }
    //
    // Summary:
    //     Gets or sets the ToolTip text for this tab.
    //
    // Returns:
    //     The ToolTip text for this tab.
    //[Localizable(true)]
    //[DefaultValue("")]
    //public string ToolTipText { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.TabPage
    //     background renders using the current visual style when visual styles are
    //     enabled.
    //
    // Returns:
    //     true to render the background using the current visual style; otherwise,
    //     false. The default is false.
    //[DefaultValue(false)]
    //public bool UseVisualStyleBackColor { get; set; }
    //
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     The default is true.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public bool Visible { get; set; }

    // Summary:
    //     Occurs when the value of the System.Windows.Forms.TabPage.AutoSize property
    //     changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public event EventHandler AutoSizeChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.TabPage.Dock property changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public event EventHandler DockChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.TabPage.Enabled property
    ////     changes.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public event EventHandler EnabledChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.TabPage.Location property
    ////     changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public event EventHandler LocationChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.TabPage.TabIndex property
    ////     changes.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public event EventHandler TabIndexChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.TabPage.TabStop property
    ////     changes.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public event EventHandler TabStopChanged;
    ////
    //[Browsable(true)]
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //public event EventHandler TextChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.TabPage.Visible property
    ////     changes.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public event EventHandler VisibleChanged;

    //
    // Returns:
    //     A new instance of System.Windows.Forms.Control.ControlCollection assigned
    //     to the control.
    //protected override Control.ControlCollection CreateControlsInstance();
    //
    // Summary:
    //     Retrieves the tab page that contains the specified object.
    //
    // Parameters:
    //   comp:
    //     The object to look for.
    //
    // Returns:
    //     The System.Windows.Forms.TabPage that contains the specified object, or null
    //     if the object cannot be found.
    //public static TabPage GetTabPageOfComponent(object comp);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Enter event of the System.Windows.Forms.TabPage.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnEnter(EventArgs e);
    ////
    //// Summary:
    ////     Raises the System.Windows.Forms.Control.Leave event of the System.Windows.Forms.TabPage.
    ////
    //// Parameters:
    ////   e:
    ////     An System.EventArgs that contains the event data.
    //protected override void OnLeave(EventArgs e);
    ////
    //// Summary:
    ////     Paints the background of the System.Windows.Forms.TabPage.
    ////
    //// Parameters:
    ////   e:
    ////     A System.Windows.Forms.PaintEventArgs that contains data useful for painting
    ////     the background.
    //protected override void OnPaintBackground(PaintEventArgs e);
    ////
    //// Summary:
    ////     This member overrides System.Windows.Forms.Control.SetBoundsCore(System.Int32,System.Int32,System.Int32,System.Int32,System.Windows.Forms.BoundsSpecified).
    ////
    //// Parameters:
    ////   x:
    ////     The new System.Windows.Forms.Control.Left property value of the control.
    ////
    ////   y:
    ////     The new System.Windows.Forms.Control.Top property value of the control.
    ////
    ////   width:
    ////     The new System.Windows.Forms.Control.Width property value of the control.
    ////
    ////   height:
    ////     The new System.Windows.Forms.Control.Height property value of the control.
    ////
    ////   specified:
    ////     A bitwise combination of System.Windows.Forms.BoundsSpecified values.
    //protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified);
    ////
    //// Summary:
    ////     Returns a string containing the value of the System.Windows.Forms.TabPage.Text
    ////     property.
    ////
    //// Returns:
    ////     A string containing the value of the System.Windows.Forms.TabPage.Text property.
    //public override string ToString();

    // Summary:
    //     Contains the collection of controls that the System.Windows.Forms.TabPage
    //     uses.
  }
}