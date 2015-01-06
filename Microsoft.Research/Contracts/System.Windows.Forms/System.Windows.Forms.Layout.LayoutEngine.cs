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
  using System.Windows.Forms;

namespace System.Windows.Forms.Layout
{
  // Summary:
  //     Provides the base class for implementing layout engines.
  public abstract class LayoutEngine
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.Layout.LayoutEngine
    //     class.
    //protected LayoutEngine();

    // Summary:
    //     Initializes the layout engine.
    //
    // Parameters:
    //   child:
    //     The container on which the layout engine will operate.
    //
    //   specified:
    //     The bounds defining the container's size and position.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     child is not a type on which System.Windows.Forms.Layout.LayoutEngine can
    //     perform layout.
    //public virtual void InitLayout(object child, BoundsSpecified specified);
    //
    // Summary:
    //     Requests that the layout engine perform a layout operation.
    //
    // Parameters:
    //   container:
    //     The container on which the layout engine will operate.
    //
    //   layoutEventArgs:
    //     An event argument from a System.Windows.Forms.Control.Layout event.
    //
    // Returns:
    //     true if layout should be performed again by the parent of container; otherwise,
    //     false.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     container is not a type on which System.Windows.Forms.Layout.LayoutEngine
    //     can perform layout.
    //public virtual bool Layout(object container, LayoutEventArgs layoutEventArgs);
  }
}