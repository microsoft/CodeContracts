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
  //     Provides data for the System.Windows.Forms.Control.Paint event.
  public class PaintEventArgs //: EventArgs, IDisposable
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.PaintEventArgs class
    //     with the specified graphics and clipping rectangle.
    //
    // Parameters:
    //   graphics:
    //     The System.Drawing.Graphics used to paint the item.
    //
    //   clipRect:
    //     The System.Drawing.Rectangle that represents the rectangle in which to paint.
    public PaintEventArgs(Graphics graphics, Rectangle clipRect)
    {
      Contract.Requires(graphics != null);
    }

    // Summary:
    //     Gets the rectangle in which to paint.
    //
    // Returns:
    //     The System.Drawing.Rectangle in which to paint.
    //public Rectangle ClipRectangle { get; }
    //
    // Summary:
    //     Gets the graphics used to paint.
    //
    // Returns:
    //     The System.Drawing.Graphics object used to paint. The System.Drawing.Graphics
    //     object provides methods for drawing objects on the display device.
    public Graphics Graphics 
    {
      get
      {
        Contract.Ensures(Contract.Result<Graphics>() != null);

        return default(Graphics);
      }
    }

    // Summary:
    //     Releases all resources used by the System.Windows.Forms.PaintEventArgs.
    //public void Dispose();
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Windows.Forms.PaintEventArgs
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    //protected virtual void Dispose(bool disposing);
  }
}
