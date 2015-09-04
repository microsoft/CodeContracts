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

namespace System.Windows.Forms
{
    /// <summary>
    /// Allows a custom control to prevent the <see cref="E:System.Windows.Forms.Control.MouseWheel"/> event from being sent to its parent container.
    /// </summary>
    public class HandledMouseEventArgs : MouseEventArgs
    {
        /// <summary>
        /// Gets or sets whether this event should be forwarded to the control's parent container.
        /// </summary>
        /// <returns>
        /// true if the mouse event should go to the parent control; otherwise, false.
        /// </returns>
        public bool Handled {get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.HandledMouseEventArgs"/> class with the specified mouse button, number of mouse button clicks, horizontal and vertical screen coordinates, and the change of mouse pointer position.
        /// </summary>
        /// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons"/> values indicating which mouse button was pressed. </param><param name="clicks">The number of times a mouse button was pressed. </param><param name="x">The x-coordinate of a mouse click, in pixels. </param><param name="y">The y-coordinate of a mouse click, in pixels. </param><param name="delta">A signed count of the number of detents the wheel has rotated. </param>
        public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
            : this(button, clicks, x, y, delta, false)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.HandledMouseEventArgs"/> class with the specified mouse button, number of mouse button clicks, horizontal and vertical screen coordinates, the change of mouse pointer position, and the value indicating whether the event is handled.
        /// </summary>
        /// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons"/> values indicating which mouse button was pressed. </param><param name="clicks">The number of times a mouse button was pressed. </param><param name="x">The x-coordinate of a mouse click, in pixels. </param><param name="y">The y-coordinate of a mouse click, in pixels. </param><param name="delta">A signed count of the number of detents the wheel has rotated. </param><param name="defaultHandledValue">true if the event is handled; otherwise, false. </param>
        public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta, bool defaultHandledValue)
            : base(button, clicks, x, y, delta)
        {
            
        }

    }
}
