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
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
    /// <summary>
    /// Defines a base class for controls that support auto-scrolling behavior.
    /// </summary>
    
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [Designer("System.Windows.Forms.Design.ScrollableControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [ComVisible(true)]
    public class ScrollableControl : Control // , IArrangedElement, IComponent, IDisposable
    {
        /// <summary>
        /// Gets or sets a value indicating whether the container enables the user to scroll to any controls placed outside of its visible boundaries.
        /// </summary>
        /// 
        /// <returns>
        /// true if the container enables auto-scrolling; otherwise, false. The default value is false.
        /// </returns>
        /// 
        
        // [DefaultValue(false)]
        // public virtual bool AutoScroll {get; set;}
        
        /// <summary>
        /// Gets or sets the size of the auto-scroll margin.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Drawing.Size"/> that represents the height and width of the auto-scroll margin in pixels.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Drawing.Size.Height"/> or <see cref="P:System.Drawing.Size.Width"/> value assigned is less than 0. </exception>
        public Size AutoScrollMargin
        {
            get { return default(Size); }
            set
            {
                Contract.Requires(value.Width >= 0 && value.Height >=0);
            }
        }

        /// <summary>
        /// Gets or sets the location of the auto-scroll position.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Drawing.Point"/> that represents the auto-scroll position in pixels.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public Point AutoScrollPosition {get; set;}
        
        /// <summary>
        /// Gets or sets the minimum size of the auto-scroll.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Drawing.Size"/> that determines the minimum size of the virtual area through which the user can scroll.
        /// </returns>
        /// 
        // public Size AutoScrollMinSize {get; set;}
        
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.CreateParams"/> that contains the required creation parameters when the handle to the control is created.
        /// </returns>
        // protected override CreateParams CreateParams {get;}
       
        /// <summary>
        /// Gets the rectangle that represents the virtual display area of the control.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Drawing.Rectangle"/> that represents the display area of the control.
        /// </returns>
        /// 
        // public override Rectangle DisplayRectangle { get; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the horizontal scroll bar is visible.
        /// </summary>
        /// 
        /// <returns>
        /// true if the horizontal scroll bar is visible; otherwise, false.
        /// </returns>
        // protected bool HScroll {get; set;}
        
        /// <summary>
        /// Gets the characteristics associated with the horizontal scroll bar.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.HScrollProperties"/> that contains information about the horizontal scroll bar.
        /// </returns>
        /// 
        // public HScrollProperties HorizontalScroll {get;}
        
        /// <summary>
        /// Gets or sets a value indicating whether the vertical scroll bar is visible.
        /// </summary>
        /// 
        /// <returns>
        /// true if the vertical scroll bar is visible; otherwise, false.
        /// </returns>
        // protected bool VScroll {get;}
        
        /// <summary>
        /// Gets the characteristics associated with the vertical scroll bar.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.VScrollProperties"/> that contains information about the vertical scroll bar.
        /// </returns>
        /// 
        // public VScrollProperties VerticalScroll {get;}
        
        /// <summary>
        /// Gets the dock padding settings for all edges of the control.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges"/> that represents the padding for all the edges of a docked control.
        /// </returns>
        /// 
        // public ScrollableControl.DockPaddingEdges DockPadding {get;}
       
        /// <summary>
        /// Occurs when the user or code scrolls through the client area.
        /// </summary>
        /// 
        // public event ScrollEventHandler Scroll
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollableControl"/> class.
        /// </summary>
        // public ScrollableControl()
        
        /// <summary>
        /// Adjusts the scroll bars on the container based on the current control positions and the control currently selected.
        /// </summary>
        /// <param name="displayScrollbars">true to show the scroll bars; otherwise, false. </param>
        // protected virtual void AdjustFormScrollbars(bool displayScrollbars)
        
        /// <summary>
        /// Determines whether the specified flag has been set.
        /// </summary>
        /// 
        /// <returns>
        /// true if the specified flag has been set; otherwise, false.
        /// </returns>
        /// <param name="bit">The flag to check.</param>
        // protected bool GetScrollState(int bit)
       
        /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs"/> that contains the event data. </param>
        protected void OnLayout(LayoutEventArgs levent)
        {
            Contract.Requires(levent != null);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected void OnMouseWheel(MouseEventArgs e)
        {
            Contract.Requires(e != null);
        }

        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        //protected override void OnRightToLeftChanged(EventArgs e)
        
        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        // protected override void OnPaintBackground(PaintEventArgs e)
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        // protected override void OnPaddingChanged(EventArgs e)
        
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnVisibleChanged(EventArgs e)
        
        /// <param name="dx">The horizontal scaling factor.</param><param name="dy">The vertical scaling factor.</param>
        // protected override void ScaleCore(float dx, float dy)
       
        /// <param name="factor">The factor by which the height and width of the control will be scaled.</param><param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified"/> value that specifies the bounds of the control to use when defining its size and position.</param>
        // protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        
        
        /// <summary>
        /// Positions the display window to the specified value.
        /// </summary>
        /// <param name="x">The horizontal offset at which to position the <see cref="T:System.Windows.Forms.ScrollableControl"/>.</param><param name="y">The vertical offset at which to position the <see cref="T:System.Windows.Forms.ScrollableControl"/>.</param>
        // protected void SetDisplayRectLocation(int x, int y)
        
        /// <summary>
        /// Scrolls the specified child control into view on an auto-scroll enabled control.
        /// </summary>
        /// <param name="activeControl">The child control to scroll into view. </param><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void ScrollControlIntoView(Control activeControl)
        
        /// <summary>
        /// Calculates the scroll offset to the specified child control.
        /// </summary>
        /// 
        /// <returns>
        /// The upper-left hand <see cref="T:System.Drawing.Point"/> of the display area relative to the client area required to scroll the control into view.
        /// </returns>
        /// <param name="activeControl">The child control to scroll into view. </param>
        protected virtual Point ScrollToControl(Control activeControl)
        {
            Contract.Requires(activeControl != null);
            return default(Point);
        }
    
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ScrollableControl.Scroll"/> event.
        /// </summary>
        /// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs"/> that contains the event data. </param>
        // protected virtual void OnScroll(ScrollEventArgs se)
        
        /// <summary>
        /// Sets the size of the auto-scroll margins.
        /// </summary>
        /// <param name="x">The <see cref="P:System.Drawing.Size.Width"/> value. </param><param name="y">The <see cref="P:System.Drawing.Size.Height"/> value. </param>
        // public void SetAutoScrollMargin(int x, int y)
        
        /// <summary>
        /// Sets the specified scroll state flag.
        /// </summary>
        /// <param name="bit">The scroll state flag to set. </param><param name="value">The value to set the flag. </param>
        // protected void SetScrollState(int bit, bool value)
        
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"/> to process. </param>
        // protected override void WndProc(ref Message m)
        
        /// <summary>
        /// Determines the border padding for docked controls.
        /// </summary>
        public class DockPaddingEdges // : ICloneable
        {
            /// <summary>
            /// Gets or sets the padding width for all edges of a docked control.
            /// </summary>
            /// 
            /// <returns>
            /// The padding width, in pixels.
            /// </returns>
            // public int All {get; set;}
            
            /// <summary>
            /// Gets or sets the padding width for the bottom edge of a docked control.
            /// </summary>
            /// 
            /// <returns>
            /// The padding width, in pixels.
            /// </returns>
            // public int Bottom {get; set;}
            
            /// <summary>
            /// Gets or sets the padding width for the left edge of a docked control.
            /// </summary>
            /// 
            /// <returns>
            /// The padding width, in pixels.
            /// </returns>
            // public int Left {get; set;}
            
            /// <summary>
            /// Gets or sets the padding width for the right edge of a docked control.
            /// </summary>
            /// 
            /// <returns>
            /// The padding width, in pixels.
            /// </returns>
            // public int Right { get; set; }
            
            /// <summary>
            /// Gets or sets the padding width for the top edge of a docked control.
            /// </summary>
            /// 
            /// <returns>
            /// The padding width, in pixels.
            /// </returns>
            //public int Top { get; set; }
            
            /// <returns>
            /// true if the specified object  is equal to the current object; otherwise, false.
            /// </returns>
            // public override bool Equals(object other)
            
            /// <returns>
            /// A hash code for the current object.
            /// </returns>
            // public override int GetHashCode()
            
            /// <summary>
            /// Returns an empty string.
            /// </summary>
            /// 
            /// <returns>
            /// An empty string.
            /// </returns>
            // public override string ToString()
        }

        /// <summary>
        /// A <see cref="T:System.ComponentModel.TypeConverter"/> for the <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges"/> class.
        /// </summary>
        public class DockPaddingEdgesConverter : TypeConverter
        {
            /// <summary>
            /// Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.
            /// </summary>
            /// 
            /// <returns>
            /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> with the properties that are exposed for the <see cref="T:System.Windows.Forms.ScrollableControl"/>.
            /// </returns>
            /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context. </param><param name="value">An object that specifies the type of array for which to get properties.</param><param name="attributes">An array of type attribute that is used as a filter.</param>
            // public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            
            /// <summary>
            /// Returns whether the current object supports properties, using the specified context.
            /// </summary>
            /// 
            /// <returns>
            /// true in all cases.
            /// </returns>
            /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context. </param>
            // public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        }
    }
}
