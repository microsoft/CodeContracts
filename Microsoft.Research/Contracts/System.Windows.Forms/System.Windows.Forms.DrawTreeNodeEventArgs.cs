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

namespace System.Windows.Forms {
  // Summary:
  //     Provides data for the System.Windows.Forms.TreeView.DrawNode event.
  public class DrawTreeNodeEventArgs : EventArgs {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.DrawTreeNodeEventArgs
    //     class.
    //
    // Parameters:
    //   graphics:
    //     The System.Drawing.Graphics surface on which to draw.
    //
    //   node:
    //     The System.Windows.Forms.TreeNode to draw.
    //
    //   bounds:
    //     The System.Drawing.Rectangle within which to draw.
    //
    //   state:
    //     A bitwise combination of the System.Windows.Forms.TreeNodeStates values indicating
    //     the current state of the System.Windows.Forms.TreeNode to draw.
    //public DrawTreeNodeEventArgs(Graphics graphics, TreeNode node, Rectangle bounds, TreeNodeStates state);

    // Summary:
    //     Gets the size and location of the System.Windows.Forms.TreeNode to draw.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the bounds of the System.Windows.Forms.TreeNode
    //     to draw.
    //public Rectangle Bounds { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.TreeNode
    //     should be drawn by the operating system rather than being owner drawn.
    //
    // Returns:
    //     true if the node should be drawn by the operating system; false if the node
    //     will be drawn in the event handler. The default value is false.
    public bool DrawDefault { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets the System.Drawing.Graphics object used to draw the System.Windows.Forms.TreeNode.
    //
    // Returns:
    //     A System.Drawing.Graphics used to draw the System.Windows.Forms.TreeNode.
    //public Graphics Graphics { get; }
    //
    // Summary:
    //     Gets the System.Windows.Forms.TreeNode to draw.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode to draw.
    //public TreeNode Node { get; }
    //
    // Summary:
    //     Gets the current state of the System.Windows.Forms.TreeNode to draw.
    //
    // Returns:
    //     A bitwise combination of the System.Windows.Forms.TreeNodeStates values indicating
    //     the current state of the System.Windows.Forms.TreeNode.
    //public TreeNodeStates State { get; }
  }
}
