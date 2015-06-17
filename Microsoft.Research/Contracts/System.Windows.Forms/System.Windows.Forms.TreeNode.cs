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
using System.Runtime.Serialization;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents a node of a System.Windows.Forms.TreeView.
  //[Serializable]
  //[DefaultProperty("Text")]
  //[TypeConverter(typeof(TreeNodeConverter))]
  public class TreeNode //: MarshalByRefObject, ICloneable, ISerializable
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.TreeNode class.
    //public TreeNode();
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.TreeNode class with
    //     the specified label text.
    //
    // Parameters:
    //   text:
    //     The label System.Windows.Forms.TreeNode.Text of the new tree node.
    //public TreeNode(string text);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.TreeNode class using
    //     the specified serialization information and context.
    //
    // Parameters:
    //   serializationInfo:
    //     A System.Runtime.Serialization.SerializationInfo containing the data to deserialize
    //     the class.
    //
    //   context:
    //     The System.Runtime.Serialization.StreamingContext containing the source and
    //     destination of the serialized stream.
    //protected TreeNode(SerializationInfo serializationInfo, StreamingContext context);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.TreeNode class with
    //     the specified label text and child tree nodes.
    //
    // Parameters:
    //   text:
    //     The label System.Windows.Forms.TreeNode.Text of the new tree node.
    //
    //   children:
    //     An array of child System.Windows.Forms.TreeNode objects.
    //public TreeNode(string text, TreeNode[] children);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.TreeNode class with
    //     the specified label text and images to display when the tree node is in a
    //     selected and unselected state.
    //
    // Parameters:
    //   text:
    //     The label System.Windows.Forms.TreeNode.Text of the new tree node.
    //
    //   imageIndex:
    //     The index value of System.Drawing.Image to display when the tree node is
    //     unselected.
    //
    //   selectedImageIndex:
    //     The index value of System.Drawing.Image to display when the tree node is
    //     selected.
    //public TreeNode(string text, int imageIndex, int selectedImageIndex);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.TreeNode class with
    //     the specified label text, child tree nodes, and images to display when the
    //     tree node is in a selected and unselected state.
    //
    // Parameters:
    //   text:
    //     The label System.Windows.Forms.TreeNode.Text of the new tree node.
    //
    //   imageIndex:
    //     The index value of System.Drawing.Image to display when the tree node is
    //     unselected.
    //
    //   selectedImageIndex:
    //     The index value of System.Drawing.Image to display when the tree node is
    //     selected.
    //
    //   children:
    //     An array of child System.Windows.Forms.TreeNode objects.
    //public TreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode//[] children);

    // Summary:
    //     Gets or sets the background color of the tree node.
    //
    // Returns:
    //     The background System.Drawing.Color of the tree node. The default is System.Drawing.Color.Empty.
    //public System.Drawing.Color BackColor { get; set; }
    //
    // Summary:
    //     Gets the bounds of the tree node.
    //
    // Returns:
    //     The System.Drawing.Rectangle that represents the bounds of the tree node.
    //[Browsable(false)]
    //public System.Drawing.Rectangle Bounds { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the tree node is in a checked state.
    //
    // Returns:
    //     true if the tree node is in a checked state; otherwise, false.
    //[DefaultValue(false)]
    //public bool Checked { get; set; }
    //
    // Summary:
    //     Gets the shortcut menu associated with this tree node.
    //
    // Returns:
    //     The System.Windows.Forms.ContextMenu associated with the tree node.
    //[DefaultValue("")]
    //public virtual ContextMenu ContextMenu { get; set; }
    //
    // Summary:
    //     Gets or sets the shortcut menu associated with this tree node.
    //
    // Returns:
    //     The System.Windows.Forms.ContextMenuStrip associated with the tree node.
    //[DefaultValue("")]
    //public virtual ContextMenuStrip ContextMenuStrip { get; set; }
    //
    // Summary:
    //     Gets the first child tree node in the tree node collection.
    //
    // Returns:
    //     The first child System.Windows.Forms.TreeNode in the System.Windows.Forms.TreeNode.Nodes
    //     collection.
    //[Browsable(false)]
    //public TreeNode FirstNode { get; }
    //
    // Summary:
    //     Gets or sets the foreground color of the tree node.
    //
    // Returns:
    //     The foreground System.Drawing.Color of the tree node.
    //public System.Drawing.Color ForeColor { get; set; }
    //
    // Summary:
    //     Gets the path from the root tree node to the current tree node.
    //
    // Returns:
    //     The path from the root tree node to the current tree node.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The node is not contained in a System.Windows.Forms.TreeView.
    //[Browsable(false)]
    //public string FullPath { get; }
    //
    // Summary:
    //     Gets the handle of the tree node.
    //
    // Returns:
    //     The tree node handle.
    //[Browsable(false)]
    //public IntPtr Handle { get; }
    //
    // Summary:
    //     Gets or sets the image list index value of the image displayed when the tree
    //     node is in the unselected state.
    //
    // Returns:
    //     A zero-based index value that represents the image position in the assigned
    //     System.Windows.Forms.ImageList.
    //[TypeConverter(typeof(TreeViewImageIndexConverter))]
    //[Localizable(true)]
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[DefaultValue(-1)]
    //[RelatedImageList("TreeView.ImageList")]
    //public int ImageIndex { get; set; }
    //
    // Summary:
    //     Gets or sets the key for the image associated with this tree node when the
    //     node is in an unselected state.
    //
    // Returns:
    //     The key for the image associated with this tree node when the node is in
    //     an unselected state.
    //[TypeConverter(typeof(TreeViewImageKeyConverter))]
    //[Localizable(true)]
    //[DefaultValue("")]
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[RelatedImageList("TreeView.ImageList")]
    //public string ImageKey { get; set; }
    //
    // Summary:
    //     Gets the position of the tree node in the tree node collection.
    //
    // Returns:
    //     A zero-based index value that represents the position of the tree node in
    //     the System.Windows.Forms.TreeNode.Nodes collection.
    //public int Index { get; }
    //
    // Summary:
    //     Gets a value indicating whether the tree node is in an editable state.
    //
    // Returns:
    //     true if the tree node is in editable state; otherwise, false.
    //[Browsable(false)]
    //public bool IsEditing { get; }
    //
    // Summary:
    //     Gets a value indicating whether the tree node is in the expanded state.
    //
    // Returns:
    //     true if the tree node is in the expanded state; otherwise, false.
    //[Browsable(false)]
    //public bool IsExpanded { get; }
    //
    // Summary:
    //     Gets a value indicating whether the tree node is in the selected state.
    //
    // Returns:
    //     true if the tree node is in the selected state; otherwise, false.
    //[Browsable(false)]
    //public bool IsSelected { get; }
    //
    // Summary:
    //     Gets a value indicating whether the tree node is visible or partially visible.
    //
    // Returns:
    //     true if the tree node is visible or partially visible; otherwise, false.
    //[Browsable(false)]
    //public bool IsVisible { get; }
    //
    // Summary:
    //     Gets the last child tree node.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNode that represents the last child tree node.
    //[Browsable(false)]
    //public TreeNode LastNode { get; }
    //
    // Summary:
    //     Gets the zero-based depth of the tree node in the System.Windows.Forms.TreeView
    //     control.
    //
    // Returns:
    //     The zero-based depth of the tree node in the System.Windows.Forms.TreeView
    //     control.
    //[Browsable(false)]
    public int Level
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }
    //
    // Summary:
    //     Gets or sets the name of the tree node.
    //
    // Returns:
    //     A System.String that represents the name of the tree node.
    public string Name
    {
      get
      {
        // **F : From reflector
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
       set
       {
         // ** F: can set to null
       }
    }

    //
    // Summary:
    //     Gets the next sibling tree node.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNode that represents the next sibling tree node.
    //[Browsable(false)]
    // ** F: Can return null
    //public TreeNode NextNode { get; }
    //
    // Summary:
    //     Gets the next visible tree node.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNode that represents the next visible tree node.
    //[Browsable(false)]
    //public TreeNode NextVisibleNode { get; }
    //
    // Summary:
    //     Gets or sets the font used to display the text on the tree node's label.
    //
    // Returns:
    //     The System.Drawing.Font used to display the text on the tree node's label.
    //[Localizable(true)]
    //[DefaultValue("")]
    //public System.Drawing.Font NodeFont { get; set; }
    //
    // Summary:
    //     Gets the collection of System.Windows.Forms.TreeNode objects assigned to
    //     the current tree node.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNodeCollection that represents the tree nodes
    //     assigned to the current tree node.
    //[ListBindable(false)]
    //[Browsable(false)]
    public TreeNodeCollection Nodes 
    {
      get
      {
        Contract.Ensures(Contract.Result<TreeNodeCollection>() != null);

        return default(TreeNodeCollection);
      }
    }
        
    //
    // Summary:
    //     Gets the parent tree node of the current tree node.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNode that represents the parent of the current
    //     tree node.
    //[Browsable(false)]
    //public TreeNode Parent { get; }
    //
    // Summary:
    //     Gets the previous sibling tree node.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNode that represents the previous sibling tree
    //     node.
    //[Browsable(false)]
    //public TreeNode PrevNode { get; }
    //
    // Summary:
    //     Gets the previous visible tree node.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNode that represents the previous visible tree
    //     node.
    //[Browsable(false)]
    //public TreeNode PrevVisibleNode { get; }
    //
    // Summary:
    //     Gets or sets the image list index value of the image that is displayed when
    //     the tree node is in the selected state.
    //
    // Returns:
    //     A zero-based index value that represents the image position in an System.Windows.Forms.ImageList.
    //[TypeConverter(typeof(TreeViewImageIndexConverter))]
    //[Localizable(true)]
    //[DefaultValue(-1)]
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[RelatedImageList("TreeView.ImageList")]

    // *** F: not putting here ensures result >= 0, as from Reflector it seems that one can set it to a negative number, and no check is performed ...
    //public int SelectedImageIndex { get; set; }

    //
    // Summary:
    //     Gets or sets the key of the image displayed in the tree node when it is in
    //     a selected state.
    //
    // Returns:
    //     The key of the image displayed when the tree node is in a selected state.
    //[TypeConverter(typeof(TreeViewImageKeyConverter))]
    //[DefaultValue("")]
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[RelatedImageList("TreeView.ImageList")]
    //[Localizable(true)]
    // *** F: not putting here ensures result >= 0, as from Reflector it seems that one can set it to a negative number, and no check is performed ...
    //public string SelectedImageKey { get; set; }
    //
    // Summary:
    //     Gets or sets the index of the image used to indicate the state of the System.Windows.Forms.TreeNode
    //     when the parent System.Windows.Forms.TreeView has its System.Windows.Forms.TreeView.CheckBoxes
    //     property set to false.
    //
    // Returns:
    //     The index of the image used to indicate the state of the System.Windows.Forms.TreeNode.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The specified index is less than -1 or greater than 14.
    //[Localizable(true)]
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[RelatedImageList("TreeView.StateImageList")]
    //[DefaultValue(-1)]
    public int StateImageIndex {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= -1);
        Contract.Ensures(Contract.Result<int>() <= 14);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= -1);
        Contract.Requires(value <= 14);

      }
    }


    //
    // Summary:
    //     Gets or sets the key of the image used to indicate the state of the System.Windows.Forms.TreeNode
    //     when the parent System.Windows.Forms.TreeView has its System.Windows.Forms.TreeView.CheckBoxes
    //     property set to false.
    //
    // Returns:
    //     The key of the image used to indicate the state of the System.Windows.Forms.TreeNode.
    //[DefaultValue("")]
    //[TypeConverter(typeof(ImageKeyConverter))]
    //[Localizable(true)]
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[RelatedImageList("TreeView.StateImageList")]
    //public string StateImageKey { get; set; }
    //
    // Summary:
    //     Gets or sets the object that contains data about the tree node.
    //
    // Returns:
    //     An System.Object that contains data about the tree node. The default is null.
    //[Localizable(false)]
    //[Bindable(true)]
    //[DefaultValue("")]
    //[TypeConverter(typeof(StringConverter))]
    //public object Tag { get; set; }
    //
    // Summary:
    //     Gets or sets the text displayed in the label of the tree node.
    //
    // Returns:
    //     The text displayed in the label of the tree node.
    //[Localizable(true)]
    public string Text
    {
      get
      {
        // **F : From reflector
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
        // ** F: can set to null
      }
    }
    //
    // Summary:
    //     Gets or sets the text that appears when the mouse pointer hovers over a System.Windows.Forms.TreeNode.
    //
    // Returns:
    //     Gets the text that appears when the mouse pointer hovers over a System.Windows.Forms.TreeNode.
    //[DefaultValue("")]
    //[Localizable(false)]
    //public string ToolTipText { get; set; }
    //
    // Summary:
    //     Gets the parent tree view that the tree node is assigned to.
    //
    // Returns:
    //     A System.Windows.Forms.TreeView that represents the parent tree view that
    //     the tree node is assigned to, or null if the node has not been assigned to
    //     a tree view.
    //[Browsable(false)]
    //public TreeView TreeView { get; }

    // Summary:
    //     Initiates the editing of the tree node label.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     System.Windows.Forms.TreeView.LabelEdit is set to false.
    //public void BeginEdit();
    //
    // Summary:
    //     Copies the tree node and the entire subtree rooted at this tree node.
    //
    // Returns:
    //     The System.Object that represents the cloned System.Windows.Forms.TreeNode.
    //public virtual object Clone();
    //
    // Summary:
    //     Collapses the tree node.
    //public void Collapse();
    //
    // Summary:
    //     Collapses the System.Windows.Forms.TreeNode and optionally collapses its
    //     children.
    //
    // Parameters:
    //   ignoreChildren:
    //     true to leave the child nodes in their current state; false to collapse the
    //     child nodes.
    //public void Collapse(bool ignoreChildren);
    //
    // Summary:
    //     Loads the state of the System.Windows.Forms.TreeNode from the specified System.Runtime.Serialization.SerializationInfo.
    //
    // Parameters:
    //   serializationInfo:
    //     The System.Runtime.Serialization.SerializationInfo that describes the System.Windows.Forms.TreeNode.
    //
    //   context:
    //     The System.Runtime.Serialization.StreamingContext that indicates the state
    //     of the stream during deserialization.
    //protected virtual void Deserialize(SerializationInfo serializationInfo, StreamingContext context);
    //
    // Summary:
    //     Ends the editing of the tree node label.
    //
    // Parameters:
    //   cancel:
    //     true if the editing of the tree node label text was canceled without being
    //     saved; otherwise, false.
    //public void EndEdit(bool cancel);
    //
    // Summary:
    //     Ensures that the tree node is visible, expanding tree nodes and scrolling
    //     the tree view control as necessary.
    //public void EnsureVisible();
    //
    // Summary:
    //     Expands the tree node.
    //public void Expand();
    //
    // Summary:
    //     Expands all the child tree nodes.
    //public void ExpandAll();
    //
    // Summary:
    //     Returns the tree node with the specified handle and assigned to the specified
    //     tree view control.
    //
    // Parameters:
    //   tree:
    //     The System.Windows.Forms.TreeView that contains the tree node.
    //
    //   handle:
    //     The handle of the tree node.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNode that represents the tree node assigned to
    //     the specified System.Windows.Forms.TreeView control with the specified handle.
    //public static TreeNode FromHandle(TreeView tree, IntPtr handle);
    //
    // Summary:
    //     Returns the number of child tree nodes.
    //
    // Parameters:
    //   includeSubTrees:
    //     true if the resulting count includes all tree nodes indirectly rooted at
    //     this tree node; otherwise, false.
    //
    // Returns:
    //     The number of child tree nodes assigned to the System.Windows.Forms.TreeNode.Nodes
    //     collection.
    public int GetNodeCount(bool includeSubTrees)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Removes the current tree node from the tree view control.
    //public void Remove();
    //
    // Summary:
    //     Saves the state of the System.Windows.Forms.TreeNode to the specified System.Runtime.Serialization.SerializationInfo.
    //
    // Parameters:
    //   si:
    //     The System.Runtime.Serialization.SerializationInfo that describes the System.Windows.Forms.TreeNode.
    //
    //   context:
    //     The System.Runtime.Serialization.StreamingContext that indicates the state
    //     of the stream during serialization
    //protected virtual void Serialize(SerializationInfo si, StreamingContext context);
    //
    // Summary:
    //     Toggles the tree node to either the expanded or collapsed state.
    //public void Toggle();
    //
    //
    // Returns:
    //     A System.String that represents the current System.Object.
    //public override string ToString();
  }
}
