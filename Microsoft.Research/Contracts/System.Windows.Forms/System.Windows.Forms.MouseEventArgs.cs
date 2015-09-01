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

#region Assembly System.Windows.Forms.dll, v4.0.30319
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Windows.Forms.dll
#endregion

using System;
using System.Drawing;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.Windows.Forms {
  // Summary:
  //     Provides data for the System.Windows.Forms.Control.MouseUp, System.Windows.Forms.Control.MouseDown,
  //     and System.Windows.Forms.Control.MouseMove events.
  // [ComVisible(true)]
  public class MouseEventArgs : EventArgs {
    /// Summary:
    ///     Initializes a new instance of the System.Windows.Forms.MouseEventArgs class.
    ///
    /// Parameters:
    ///   button:
    ///     One of the System.Windows.Forms.MouseButtons values indicating which mouse
    ///     button was pressed.
    ///
    ///   clicks:
    ///     The number of times a mouse button was pressed.
    ///
    ///   x:
    ///     The x-coordinate of a mouse click, in pixels.
    ///
    ///   y:
    ///     The y-coordinate of a mouse click, in pixels.
    ///
    ///   delta:
    ///     A signed count of the number of detents the wheel has rotated.
    public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
        { }

    /// Summary:
    ///     Gets which mouse button was pressed.
    ///
    /// Returns:
    ///     One of the System.Windows.Forms.MouseButtons values.
    public MouseButtons Button { get; }
    
    ///
    /// Summary:
    ///     Gets the number of times the mouse button was pressed and released.
    ///
    /// Returns:
    ///     An System.Int32 containing the number of times the mouse button was pressed
    ///     and released.
    public int Clicks { get; }
   
    
    /// Summary:
    ///     Gets a signed count of the number of detents the mouse wheel has rotated.
    ///     A detent is one notch of the mouse wheel.
    ///
    /// Returns:
    ///     A signed count of the number of detents the mouse wheel has rotated.
    public int Delta { get; }
    
    //
    // Summary:
    //     Gets the location of the mouse during the generating mouse event.
    //
    // Returns:
    //     A System.Drawing.Point containing the x- and y- coordinate of the mouse,
    //     in pixels, relative to the top-left corner of the form.
    //public Point Location { get; }
    //
    
    /// Summary:
    ///     Gets the x-coordinate of the mouse during the generating mouse event.
    ///
    /// Returns:
    ///     The x-coordinate of the mouse, in pixels.
    public int X { get; }
    
    /// Summary:
    ///     Gets the y-coordinate of the mouse during the generating mouse event.
    ///
    /// Returns:
    ///     The y-coordinate of the mouse, in pixels.
    public int Y { get; }
  }
}
