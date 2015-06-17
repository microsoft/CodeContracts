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
using System.Drawing;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents a display device or multiple display devices on a single system.
  public class Screen
  {

    private Screen() { }

    // Summary:
    //     Gets an array of all displays on the system.
    //
    // Returns:
    //     An array of type System.Windows.Forms.Screen, containing all displays on
    //     the system.
    public static Screen[] AllScreens
    {
      get
      {
        Contract.Ensures(Contract.Result<Screen[]>() != null);

        return default(Screen[]);
      }
    }
    //
    // Summary:
    //     Gets the number of bits of memory, associated with one pixel of data.
    //
    // Returns:
    //     The number of bits of memory, associated with one pixel of data
    public int BitsPerPixel 
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }
    //
    // Summary:
    //     Gets the bounds of the display.
    //
    // Returns:
    //     A System.Drawing.Rectangle, representing the bounds of the display.
    //public Rectangle Bounds { get; }
    //
    // Summary:
    //     Gets the device name associated with a display.
    //
    // Returns:
    //     The device name associated with a display.
    public string DeviceName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
    
    //
    // Summary:
    //     Gets a value indicating whether a particular display is the primary device.
    //
    // Returns:
    //     true if this display is primary; otherwise, false.
    //public bool Primary { get; }
    //
    // Summary:
    //     Gets the primary display.
    //
    // Returns:
    //     The primary display.
    // F: It seems that PrimaryScreen can be null in some configurations
    //public static Screen PrimaryScreen { get; }
    ////
    //// Summary:
    ////     Gets the working area of the display. The working area is the desktop area
    ////     of the display, excluding taskbars, docked windows, and docked tool bars.
    ////
    //// Returns:
    ////     A System.Drawing.Rectangle, representing the working area of the display.
    //public Rectangle WorkingArea { get; }

    //// Summary:
    ////     Gets or sets a value indicating whether the specified object is equal to
    ////     this Screen.
    ////
    //// Parameters:
    ////   obj:
    ////     The object to compare to this System.Windows.Forms.Screen.
    ////
    //// Returns:
    ////     true if the specified object is equal to this System.Windows.Forms.Screen;
    ////     otherwise, false.
    //public override bool Equals(object obj);
    ////
    //// Summary:
    ////     Retrieves a System.Windows.Forms.Screen for the display that contains the
    ////     largest portion of the specified control.
    ////
    //// Parameters:
    ////   control:
    ////     A System.Windows.Forms.Control for which to retrieve a System.Windows.Forms.Screen.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Screen for the display that contains the largest region
    ////     of the specified control. In multiple display environments where no display
    ////     contains the control, the display closest to the specified control is returned.
    public static Screen FromControl(Control control)
    {
      Contract.Requires(control != null);

      Contract.Ensures(Contract.Result<Screen>() != null);

      return default(Screen);
    }
    ////
    //// Summary:
    ////     Retrieves a System.Windows.Forms.Screen for the display that contains the
    ////     largest portion of the object referred to by the specified handle.
    ////
    //// Parameters:
    ////   hwnd:
    ////     The window handle for which to retrieve the System.Windows.Forms.Screen.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Screen for the display that contains the largest region
    ////     of the object. In multiple display environments where no display contains
    ////     any portion of the specified window, the display closest to the object is
    ////     returned.
    public static Screen FromHandle(IntPtr hwnd)
    {
      Contract.Ensures(Contract.Result<Screen>() != null);

      return default(Screen);
    }
    ////
    //// Summary:
    ////     Retrieves a System.Windows.Forms.Screen for the display that contains the
    ////     specified point.
    ////
    //// Parameters:
    ////   point:
    ////     A System.Drawing.Point that specifies the location for which to retrieve
    ////     a System.Windows.Forms.Screen.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Screen for the display that contains the point. In
    ////     multiple display environments where no display contains the point, the display
    ////     closest to the specified point is returned.
    public static Screen FromPoint(Point point)
    {
      Contract.Ensures(Contract.Result<Screen>() != null);

      return default(Screen);
    }
   
    ////
    //// Summary:
    ////     Retrieves a System.Windows.Forms.Screen for the display that contains the
    ////     largest portion of the rectangle.
    ////
    //// Parameters:
    ////   rect:
    ////     A System.Drawing.Rectangle that specifies the area for which to retrieve
    ////     the display.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Screen for the display that contains the largest region
    ////     of the specified rectangle. In multiple display environments where no display
    ////     contains the rectangle, the display closest to the rectangle is returned.
    public static Screen FromRectangle(Rectangle rect)
    {
      Contract.Ensures(Contract.Result<Screen>() != null);

      return default(Screen);
    }
   
    ////
    //// Summary:
    ////     Retrieves the bounds of the display that contains the largest portion of
    ////     the specified control.
    ////
    //// Parameters:
    ////   ctl:
    ////     The System.Windows.Forms.Control for which to retrieve the display bounds.
    ////
    //// Returns:
    ////     A System.Drawing.Rectangle that specifies the bounds of the display that
    ////     contains the specified control. In multiple display environments where no
    ////     display contains the specified control, the display closest to the control
    ////     is returned.
    [Pure]
    public static Rectangle GetBounds(Control ctl)
    {
      Contract.Requires(ctl != null);

      return default(Rectangle);
    }
    ////
    //// Summary:
    ////     Retrieves the bounds of the display that contains the specified point.
    ////
    //// Parameters:
    ////   pt:
    ////     A System.Drawing.Point that specifies the coordinates for which to retrieve
    ////     the display bounds.
    ////
    //// Returns:
    ////     A System.Drawing.Rectangle that specifies the bounds of the display that
    ////     contains the specified point. In multiple display environments where no display
    ////     contains the specified point, the display closest to the point is returned.
    //public static Rectangle GetBounds(Point pt);
    ////
    //// Summary:
    ////     Retrieves the bounds of the display that contains the largest portion of
    ////     the specified rectangle.
    ////
    //// Parameters:
    ////   rect:
    ////     A System.Drawing.Rectangle that specifies the area for which to retrieve
    ////     the display bounds.
    ////
    //// Returns:
    ////     A System.Drawing.Rectangle that specifies the bounds of the display that
    ////     contains the specified rectangle. In multiple display environments where
    ////     no monitor contains the specified rectangle, the monitor closest to the rectangle
    ////     is returned.
    //public static Rectangle GetBounds(Rectangle rect);
    ////
    //// Summary:
    ////     Computes and retrieves a hash code for an object.
    ////
    //// Returns:
    ////     A hash code for an object.
    //public override int GetHashCode();
    ////
    //// Summary:
    ////     Retrieves the working area for the display that contains the largest region
    ////     of the specified control. The working area is the desktop area of the display,
    ////     excluding taskbars, docked windows, and docked tool bars.
    ////
    //// Parameters:
    ////   ctl:
    ////     The System.Windows.Forms.Control for which to retrieve the working area.
    ////
    //// Returns:
    ////     A System.Drawing.Rectangle that specifies the working area. In multiple display
    ////     environments where no display contains the specified control, the display
    ////     closest to the control is returned.
    [Pure]
    public static Rectangle GetWorkingArea(Control ctl)
    {
      Contract.Requires(ctl != null);

      return default(Rectangle);
    }
    ////
    //// Summary:
    ////     Retrieves the working area closest to the specified point. The working area
    ////     is the desktop area of the display, excluding taskbars, docked windows, and
    ////     docked tool bars.
    ////
    //// Parameters:
    ////   pt:
    ////     A System.Drawing.Point that specifies the coordinates for which to retrieve
    ////     the working area.
    ////
    //// Returns:
    ////     A System.Drawing.Rectangle that specifies the working area. In multiple display
    ////     environments where no display contains the specified point, the display closest
    ////     to the point is returned.
    //public static Rectangle GetWorkingArea(Point pt);
    ////
    //// Summary:
    ////     Retrieves the working area for the display that contains the largest portion
    ////     of the specified rectangle. The working area is the desktop area of the display,
    ////     excluding taskbars, docked windows, and docked tool bars.
    ////
    //// Parameters:
    ////   rect:
    ////     The System.Drawing.Rectangle that specifies the area for which to retrieve
    ////     the working area.
    ////
    //// Returns:
    ////     A System.Drawing.Rectangle that specifies the working area. In multiple display
    ////     environments where no display contains the specified rectangle, the display
    ////     closest to the rectangle is returned.
    //public static Rectangle GetWorkingArea(Rectangle rect);
    ////
    //// Summary:
    ////     Retrieves a string representing this object.
    ////
    //// Returns:
    ////     A string representation of the object.
    //public override string ToString();
  }
}
