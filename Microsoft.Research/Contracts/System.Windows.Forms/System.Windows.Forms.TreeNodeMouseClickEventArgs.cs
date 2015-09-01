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

namespace System.Windows.Forms {
  // Summary:
  //     Provides data for the System.Windows.Forms.TreeView.NodeMouseClick and System.Windows.Forms.TreeView.NodeMouseDoubleClick
  //     events.
  public class TreeNodeMouseClickEventArgs : MouseEventArgs {
      /// Summary:
      ///     Initializes a new instance of the System.Windows.Forms.TreeNodeMouseClickEventArgs
      ///     class.
      ///
      /// Parameters:
      ///   node:
      ///     The node that was clicked.
      ///
      ///   button:
      ///     One of the System.Windows.Forms.MouseButtons members.
      ///
      ///   clicks:
      ///     The number of clicks that occurred.
      ///
      ///   x:
      ///     The x-coordinate where the click occurred.
      ///
      ///   y:
      ///     The y-coordinate where the click occurred.
      public TreeNodeMouseClickEventArgs(TreeNode node, MouseButtons button, int clicks, int x, int y)
          : base(button, clicks, x, y, 0)
      {
          
      }
    // Summary:
    //     Gets the node that was clicked.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that was clicked.
    //public TreeNode Node { get; }
  }
}
