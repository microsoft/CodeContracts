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
using System.Diagnostics.Contracts;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides basic functionality for the <see cref="T:System.Windows.Forms.ContextMenuStrip"/> control. Although <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"/> and <see cref="T:System.Windows.Forms.ToolStripDropDown"/> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu"/> control of previous versions, <see cref="T:System.Windows.Forms.Menu"/> is retained for both backward compatibility and future use if you choose.
    /// </summary>
    
    [Designer("System.Windows.Forms.Design.ToolStripDropDownDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class ToolStripDropDownMenu : ToolStripDropDown
    {
        
        /// <summary>
        /// Gets the internal spacing, in pixels, of the control.
        /// </summary>
        /// 
        /// <returns>
        /// A Padding object representing the spacing.
        /// </returns>
        // protected override Padding DefaultPadding {get;}
        
        /// <summary>
        /// Gets the rectangle that represents the display area of the <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Drawing.Rectangle"/> that represents the display area.
        /// </returns>
        // public override Rectangle DisplayRectangle {get;}
        // public override LayoutEngine LayoutEngine {get;}
        
        /// <summary>
        /// Gets or sets a value indicating how the items of <see cref="T:System.Windows.Forms.ContextMenuStrip"/> are displayed.
        /// </summary>
        /// 
        /// <returns>
        /// One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle"/> values. The default is <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Flow"/>.
        /// </returns>
        // [DefaultValue(ToolStripLayoutStyle.Flow)]
        // public new ToolStripLayoutStyle LayoutStyle {get; set;}
        
        /// <summary>
        /// Gets the maximum height and width, in pixels, of the <see cref="T:System.Windows.Forms.ContextMenuStrip"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A Size object representing the height and width of the control, in pixels.
        /// </returns>
        // protected internal override Size MaxItemSize {get;}
        
        /// <summary>
        /// Gets or sets a value indicating whether space for an image is shown on the left edge of the <see cref="T:System.Windows.Forms.ToolStripMenuItem"/>.
        /// </summary>
        /// 
        /// <returns>
        /// true if the image margin is shown; otherwise, false. The default is true.
        /// </returns>
        /// 
        // [DefaultValue(true)]
        // public bool ShowImageMargin {get; set;}
        
        /// <summary>
        /// Gets or sets a value indicating whether space for a check mark is shown on the left edge of the <see cref="T:System.Windows.Forms.ToolStripMenuItem"/>.
        /// </summary>
        /// 
        /// <returns>
        /// true if the check margin is shown; otherwise, false. The default is false.
        /// </returns>
        /// 
        // public bool ShowCheckMargin {get; set;}
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"/> class.
        /// </summary>
        // public ToolStripDropDownMenu()
        
        /// <summary>
        /// Creates a default <see cref="T:System.Windows.Forms.ToolStripMenuItem"/> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.ToolStripMenuItem"/>, or a <see cref="T:System.Windows.Forms.ToolStripSeparator"/> if the <paramref name="text"/> parameter is a hyphen (-).
        /// </returns>
        /// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripMenuItem"/>. If the <paramref name="text"/> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator"/>.</param><param name="image">The <see cref="T:System.Drawing.Image"/> to display on the <see cref="T:System.Windows.Forms.ToolStripMenuItem"/>.</param><param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click"/> event when the <see cref="T:System.Windows.Forms.ToolStripMenuItem"/> is clicked.</param>
        // protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Layout"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs"/> that contains the event data. </param>
        // protected override void OnLayout(LayoutEventArgs e)
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.FontChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnFontChanged(EventArgs e)
        
        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        // protected override void OnPaintBackground(PaintEventArgs e)
        
        /// <summary>
        /// Resets the collection of displayed and overflow items after a layout is done.
        /// </summary>
        // protected override void SetDisplayedItems()
    }
}
