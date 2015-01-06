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
  //     Creates a panel that is associated with a System.Windows.Forms.SplitContainer.
  // [Designer("System.Windows.Forms.Design.SplitterPanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  // [ClassInterface(ClassInterfaceType.AutoDispatch)]
  // [ComVisible(true)]
  // [ToolboxItem(false)]
  // [Docking(DockingBehavior.Never)]
  public sealed class SplitterPanel //: Panel
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.SplitterPanel class
    //     with its specified System.Windows.Forms.SplitContainer.
    //
    // Parameters:
    //   owner:
    //     The System.Windows.Forms.SplitContainer that contains the System.Windows.Forms.SplitterPanel.
    //public SplitterPanel(SplitContainer owner);

    // Summary:
    //     Gets or sets how a System.Windows.Forms.SplitterPanel attaches to the edges
    //     of the System.Windows.Forms.SplitContainer. This property is not relevant
    //     to this class.
    //
    // Returns:
    //     One of the System.Windows.Forms.AnchorStyles values.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public AnchorStyles Anchor { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.SplitterPanel
    //     is automatically resized to display its entire contents. This property is
    //     not relevant to this class.
    //
    // Returns:
    //     true if the System.Windows.Forms.SplitterPanel is automatically resized;
    //     otherwise, false.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool AutoSize { get; set; }
    //
    // Summary:
    //     Enables the System.Windows.Forms.SplitterPanel to shrink when System.Windows.Forms.SplitterPanel.AutoSize
    //     is true. This property is not relevant to this class.
    //
    // Returns:
    //     One of the System.Windows.Forms.AutoSizeMode values. The default is System.Windows.Forms.AutoSizeMode.GrowOnly.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    // [Localizable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    //public override AutoSizeMode AutoSizeMode { get; set; }
    //
    // Summary:
    //     Gets or sets the border style for the System.Windows.Forms.SplitterPanel.
    //     This property is not relevant to this class.
    //
    // Returns:
    //     One of the System.Windows.Forms.BorderStyle values.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    //public BorderStyle BorderStyle { get; set; }
    // protected override Padding DefaultMargin { get; }
    //
    // Summary:
    //     Gets or sets which edge of the System.Windows.Forms.SplitContainer that the
    //     System.Windows.Forms.SplitterPanel is docked to. This property is not relevant
    //     to this class.
    //
    // Returns:
    //     One of the System.Windows.Forms.DockStyle values.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    //public DockStyle Dock { get; set; }
    //
    // Summary:
    //     Gets the internal spacing between the System.Windows.Forms.SplitterPanel
    //     and its edges. This property is not relevant to this class.
    //
    // Returns:
    //     A System.Windows.Forms.ScrollableControl.DockPaddingEdges that represents
    //     the padding for all the edges of a docked control.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public ScrollableControl.DockPaddingEdges DockPadding { get; }
    //
    // Summary:
    //     Gets or sets the height of the System.Windows.Forms.SplitterPanel.
    //
    // Returns:
    //     The height of the System.Windows.Forms.SplitterPanel, in pixels.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The height cannot be set.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [EditorBrowsable(EditorBrowsableState.Always)]
    // [Browsable(false)]
    public int Height
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
      set
      {
        // Cannot be set!!!
        Contract.Requires(false); 
      }
    }
    //
    // Summary:
    //     Gets or sets the coordinates of the upper-left corner of the System.Windows.Forms.SplitterPanel
    //     relative to the upper-left corner of its System.Windows.Forms.SplitContainer.
    //     This property is not relevant to this class.
    //
    // Returns:
    //     The System.Drawing.Point that represents the upper-left corner of the System.Windows.Forms.SplitterPanel
    //     relative to the upper-left corner of its System.Windows.Forms.SplitContainer.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public System.Drawing.Point Location { get; set; }
    //
    // Summary:
    //     Gets or sets the size that is the upper limit that System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)
    //     can specify. This property is not relevant to this class.
    //
    // Returns:
    //     An ordered pair of type System.Drawing.Size representing the width and height
    //     of a rectangle.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    //public System.Drawing.Size MaximumSize { get; set; }
    //
    // Summary:
    //     Gets or sets the size that is the lower limit that System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)
    //     can specify. This property is not relevant to this class.
    //
    // Returns:
    //     An ordered pair of type System.Drawing.Size representing the width and height
    //     of a rectangle.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The width cannot be set.
    // [Browsable(false)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    //public System.Drawing.Size MinimumSize { get; set; }
    //
    // Summary:
    //     The name of this System.Windows.Forms.SplitterPanel. This property is not
    //     relevant to this class.
    //
    // Returns:
    //     A System.String representing the name of this System.Windows.Forms.SplitterPanel.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public string Name { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Windows.Forms.SplitContainer that contains this System.Windows.Forms.SplitterPanel.
    //     This property is not relevant to this class.
    //
    // Returns:
    //     A System.Windows.Forms.Control representing the System.Windows.Forms.SplitContainer
    //     that contains this System.Windows.Forms.SplitterPanel.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    //public Control Parent { get; set; }
    //
    // Summary:
    //     Gets or sets the height and width of the System.Windows.Forms.SplitterPanel.
    //     This property is not relevant to this class.
    //
    // Returns:
    //     The System.Drawing.Size that represents the height and width of the System.Windows.Forms.SplitterPanel
    //     in pixels.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    //public System.Drawing.Size Size { get; set; }
    //
    // Summary:
    //     Gets or sets the tab order of the System.Windows.Forms.SplitterPanel within
    //     its System.Windows.Forms.SplitContainer. This property is not relevant to
    //     this class.
    //
    // Returns:
    //     The index value of the System.Windows.Forms.SplitterPanel within the set
    //     of other System.Windows.Forms.SplitterPanel objects within its System.Windows.Forms.SplitContainer
    //     that are included in the tab order.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    //public int TabIndex { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the user can give the focus to this
    //     System.Windows.Forms.SplitterPanel using the TAB key. This property is not
    //     relevant to this class.
    //
    // Returns:
    //     true if the user can give the focus to this System.Windows.Forms.SplitterPanel
    //     using the TAB key; otherwise, false.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    //// [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool TabStop { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.SplitterPanel
    //     is displayed. This property is not relevant to this class.
    //
    // Returns:
    //     true if the System.Windows.Forms.SplitterPanel is displayed; otherwise, false.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    //public bool Visible { get; set; }
    //
    // Summary:
    //     Gets or sets the width of the System.Windows.Forms.SplitterPanel.
    //
    // Returns:
    //     The width of the System.Windows.Forms.SplitterPanel in pixels.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Always)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public int Width { get; set; }

    // Summary:
    //     This event is not relevant to this class.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // public event EventHandler AutoSizeChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.SplitterPanel.Dock property
    //     changes. This event is not relevant to this class.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // public event EventHandler DockChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.SplitterPanel.Location
    //     property changes. This event is not relevant to this class.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    // public event EventHandler LocationChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.SplitterPanel.TabIndex
    //     property changes. This event is not relevant to this class.
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [Browsable(false)]
    // public event EventHandler TabIndexChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.SplitterPanel.TabStop property
    //     changes. This event is not relevant to this class.
    // [Browsable(false)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // public event EventHandler TabStopChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.SplitterPanel.Visible property
    //     changes. This event is not relevant to this class.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // [EditorBrowsable(EditorBrowsableState.Never)]
    // [Browsable(false)]
    // public event EventHandler VisibleChanged;
  }
}