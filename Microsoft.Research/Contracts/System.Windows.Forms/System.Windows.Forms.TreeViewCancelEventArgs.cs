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
using System.ComponentModel;
using System.Runtime;

namespace System.Windows.Forms {
  // Summary:
  //     Provides data for the System.Windows.Forms.TreeView.BeforeCheck, System.Windows.Forms.TreeView.BeforeCollapse,
  //     System.Windows.Forms.TreeView.BeforeExpand, and System.Windows.Forms.TreeView.BeforeSelect
  //     events of a System.Windows.Forms.TreeView control.
  public class TreeViewCancelEventArgs : CancelEventArgs {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.TreeViewCancelEventArgs
    //     class with the specified tree node, a value specifying whether the event
    //     is to be canceled, and the type of tree view action that raised the event.
    //
    // Parameters:
    //   node:
    //     The System.Windows.Forms.TreeNode that the event is responding to.
    //
    //   cancel:
    //     true to cancel the event; otherwise, false.
    //
    //   action:
    //     One of the System.Windows.Forms.TreeViewAction values indicating the type
    //     of action that raised the event.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public TreeViewCancelEventArgs(TreeNode node, bool cancel, TreeViewAction action);

    // Summary:
    //     Gets the type of System.Windows.Forms.TreeView action that raised the event.
    //
    // Returns:
    //     One of the System.Windows.Forms.TreeViewAction values.
    //public TreeViewAction Action { get; }
    //
    // Summary:
    //     Gets the tree node to be checked, expanded, collapsed, or selected.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode to be checked, expanded, collapsed, or
    //     selected.
    //public TreeNode Node { get; }
  }
}
