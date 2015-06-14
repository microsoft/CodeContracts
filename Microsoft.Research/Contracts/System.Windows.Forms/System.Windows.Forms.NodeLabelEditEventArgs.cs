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
using System.Runtime;

namespace System.Windows.Forms {
  // Summary:
  //     Provides data for the System.Windows.Forms.TreeView.BeforeLabelEdit and System.Windows.Forms.TreeView.AfterLabelEdit
  //     events.
  public class NodeLabelEditEventArgs : EventArgs {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.NodeLabelEditEventArgs
    //     class for the specified System.Windows.Forms.TreeNode.
    //
    // Parameters:
    //   node:
    //     The tree node containing the text to edit.
    //public NodeLabelEditEventArgs(TreeNode node);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.NodeLabelEditEventArgs
    //     class for the specified System.Windows.Forms.TreeNode and the specified text
    //     with which to update the tree node label.
    //
    // Parameters:
    //   node:
    //     The tree node containing the text to edit.
    //
    //   label:
    //     The new text to associate with the tree node.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public NodeLabelEditEventArgs(TreeNode node, string label);

    // Summary:
    //     Gets or sets a value indicating whether the edit has been canceled.
    //
    // Returns:
    //     true if the edit has been canceled; otherwise, false.
    public bool CancelEdit { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets the new text to associate with the tree node.
    //
    // Returns:
    //     The string value that represents the new System.Windows.Forms.TreeNode label
    //     or null if the user cancels the edit.
    //public string Label { get; }
    //
    // Summary:
    //     Gets the tree node containing the text to edit.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNode that represents the tree node containing
    //     the text to edit.
    //public TreeNode Node { get; }
  }
}
