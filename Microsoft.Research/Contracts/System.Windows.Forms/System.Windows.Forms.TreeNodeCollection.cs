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
  // Summary:
  //     Represents a collection of System.Windows.Forms.TreeNode objects.
  public class TreeNodeCollection //: IList, ICollection, IEnumerable
  {
    private TreeNodeCollection() { }

    // Summary:
    //     Gets the total number of System.Windows.Forms.TreeNode objects in the collection.
    //
    // Returns:
    //     The total number of System.Windows.Forms.TreeNode objects in the collection.
    //[Browsable(false)]
    //public int Count { get; }
    //
    // Summary:
    //     Gets a value indicating whether the collection is read-only.
    //
    // Returns:
    //     true if the collection is read-only; otherwise, false. The default is false.
    //public bool IsReadOnly { get; }

    // Summary:
    //     Gets or sets the System.Windows.Forms.TreeNode at the specified indexed location
    //     in the collection.
    //
    // Parameters:
    //   index:
    //     The indexed location of the System.Windows.Forms.TreeNode in the collection.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode at the specified indexed location in the
    //     collection.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index value is less than 0 or is greater than the number of tree nodes
    //     in the collection.
    //public virtual TreeNode this[int index] { get; set; }
    //
    // Summary:
    //     Gets the tree node with the specified key from the collection.
    //
    // Parameters:
    //   key:
    //     The name of the System.Windows.Forms.TreeNode to retrieve from the collection.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode with the specified key.
    //public virtual TreeNode this[string key] { get; }

    // Summary:
    //     Adds a new tree node with the specified label text to the end of the current
    //     tree node collection.
    //
    // Parameters:
    //   text:
    //     The label text displayed by the System.Windows.Forms.TreeNode.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNode that represents the tree node being added
    //     to the collection.
    public virtual TreeNode Add(string text)
    {
      Contract.Ensures(Contract.Result<TreeNode>() != null);

      return default(TreeNode);
    }
    
    //
    // Summary:
    //     Adds a previously created tree node to the end of the tree node collection.
    //
    // Parameters:
    //   node:
    //     The System.Windows.Forms.TreeNode to add to the collection.
    //
    // Returns:
    //     The zero-based index value of the System.Windows.Forms.TreeNode added to
    //     the tree node collection.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The node is currently assigned to another System.Windows.Forms.TreeView.
    //public virtual int Add(TreeNode node);
    //
    // Summary:
    //     Creates a new tree node with the specified key and text, and adds it to the
    //     collection.
    //
    // Parameters:
    //   key:
    //     The name of the tree node.
    //
    //   text:
    //     The text to display in the tree node.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that was added to the collection.
    public virtual TreeNode Add(string key, string text)
    {
      Contract.Ensures(Contract.Result<TreeNode>() != null);

      return default(TreeNode);
    }


    // Summary:
    //     Creates a tree node with the specified key, text, and image, and adds it
    //     to the collection.
    //
    // Parameters:
    //   key:
    //     The name of the tree node.
    //
    //   text:
    //     The text to display in the tree node.
    //
    //   imageIndex:
    //     The index of the image to display in the tree node.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that was added to the collection.
    public virtual TreeNode Add(string key, string text, int imageIndex)
    {
      Contract.Ensures(Contract.Result<TreeNode>() != null);

      return default(TreeNode);
    }
    

    // Summary:
    //     Creates a tree node with the specified key, text, and image, and adds it
    //     to the collection.
    //
    // Parameters:
    //   key:
    //     The name of the tree node.
    //
    //   text:
    //     The text to display in the tree node.
    //
    //   imageKey:
    //     The image to display in the tree node.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that was added to the collection.
    public virtual TreeNode Add(string key, string text, string imageKey)
    {
      Contract.Ensures(Contract.Result<TreeNode>() != null);

      return default(TreeNode);
    }

    //
    // Summary:
    //     Creates a tree node with the specified key, text, and images, and adds it
    //     to the collection.
    //
    // Parameters:
    //   key:
    //     The name of the tree node.
    //
    //   text:
    //     The text to display in the tree node.
    //
    //   imageIndex:
    //     The index of the image to display in the tree node.
    //
    //   selectedImageIndex:
    //     The index of the image to be displayed in the tree node when it is in a selected
    //     state.
    //
    // Returns:
    //     The tree node that was added to the collection.
    public virtual TreeNode Add(string key, string text, int imageIndex, int selectedImageIndex)
    {
      Contract.Ensures(Contract.Result<TreeNode>() != null);

      return default(TreeNode);
    }
    
    // Summary:
    //     Creates a tree node with the specified key, text, and images, and adds it
    //     to the collection.
    //
    // Parameters:
    //   key:
    //     The name of the tree node.
    //
    //   text:
    //     The text to display in the tree node.
    //
    //   imageKey:
    //     The key of the image to display in the tree node.
    //
    //   selectedImageKey:
    //     The key of the image to display when the node is in a selected state.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that was added to the collection.
    public virtual TreeNode Add(string key, string text, string imageKey, string selectedImageKey)
    {
      Contract.Ensures(Contract.Result<TreeNode>() != null);

      return default(TreeNode);
    }

    //
    // Summary:
    //     Adds an array of previously created tree nodes to the collection.
    //
    // Parameters:
    //   nodes:
    //     An array of System.Windows.Forms.TreeNode objects representing the tree nodes
    //     to add to the collection.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     nodes is null.
    //
    //   System.ArgumentException:
    //     nodes is the child of another System.Windows.Forms.TreeView.
    //public virtual void AddRange(TreeNode[] nodes);
    //

    //
    // Summary:
    //     Determines whether the specified tree node is a member of the collection.
    //
    // Parameters:
    //   node:
    //     The System.Windows.Forms.TreeNode to locate in the collection.
    //
    // Returns:
    //     true if the System.Windows.Forms.TreeNode is a member of the collection;
    //     otherwise, false.
    //public bool Contains(TreeNode node);
    //
    // Summary:
    //     Determines whether the collection contains a tree node with the specified
    //     key.
    //
    // Parameters:
    //   key:
    //     The name of the System.Windows.Forms.TreeNode to search for.
    //
    // Returns:
    //     true to indicate the collection contains a System.Windows.Forms.TreeNode
    //     with the specified key; otherwise, false.
    //public virtual bool ContainsKey(string key);
    //
    // Summary:
    //     Copies the entire collection into an existing array at a specified location
    //     within the array.
    //
    // Parameters:
    //   dest:
    //     The destination array.
    //
    //   index:
    //     The index in the destination array at which storing begins.
    //public void CopyTo(Array dest, int index);
    //
    // Summary:
    //     Finds the tree nodes with specified key, optionally searching subnodes.
    //
    // Parameters:
    //   key:
    //     The name of the tree node to search for.
    //
    //   searchAllChildren:
    //     true to search child nodes of tree nodes; otherwise, false.
    //
    // Returns:
    //     An array of System.Windows.Forms.TreeNode objects whose System.Windows.Forms.TreeNode.Name
    //     property matches the specified key.
    public TreeNode[] Find(string key, bool searchAllChildren)
    {
      Contract.Ensures(Contract.Result<TreeNode[]>() != null);

      return default(TreeNode[]);
    }

    //
    // Summary:
    //     Returns the index of the first occurrence of a tree node with the specified
    //     key.
    //
    // Parameters:
    //   key:
    //     The name of the tree node to search for.
    //
    // Returns:
    //     The zero-based index of the first occurrence of a tree node with the specified
    //     key, if found; otherwise, -1.
    public virtual int IndexOfKey(string key)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);
      
      return default(int);
    }
    //
    // Summary:
    //     Creates a tree node with the specified text and inserts it at the specified
    //     index.
    //
    // Parameters:
    //   index:
    //     The location within the collection to insert the node.
    //
    //   text:
    //     The text to display in the tree node.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that was inserted in the collection.
    //public virtual TreeNode Insert(int index, string text);
    //
    // Summary:
    //     Inserts an existing tree node into the tree node collection at the specified
    //     location.
    //
    // Parameters:
    //   index:
    //     The indexed location within the collection to insert the tree node.
    //
    //   node:
    //     The System.Windows.Forms.TreeNode to insert into the collection.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The node is currently assigned to another System.Windows.Forms.TreeView.
    //public virtual void Insert(int index, TreeNode node);
    //
    // Summary:
    //     Creates a tree node with the specified text and key, and inserts it into
    //     the collection.
    //
    // Parameters:
    //   index:
    //     The location within the collection to insert the node.
    //
    //   key:
    //     The name of the tree node.
    //
    //   text:
    //     The text to display in the tree node.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that was inserted in the collection.
    //public virtual TreeNode Insert(int index, string key, string text);
    //
    // Summary:
    //     Creates a tree node with the specified key, text, and image, and inserts
    //     it into the collection at the specified index.
    //
    // Parameters:
    //   index:
    //     The location within the collection to insert the node.
    //
    //   key:
    //     The name of the tree node.
    //
    //   text:
    //     The text to display in the tree node.
    //
    //   imageIndex:
    //     The index of the image to display in the tree node.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that was inserted in the collection.
    //public virtual TreeNode Insert(int index, string key, string text, int imageIndex);
    //
    // Summary:
    //     Creates a tree node with the specified key, text, and image, and inserts
    //     it into the collection at the specified index.
    //
    // Parameters:
    //   index:
    //     The location within the collection to insert the node.
    //
    //   key:
    //     The name of the tree node.
    //
    //   text:
    //     The text to display in the tree node.
    //
    //   imageKey:
    //     The key of the image to display in the tree node.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that was inserted in the collection.
    //public virtual TreeNode Insert(int index, string key, string text, string imageKey);
    //
    // Summary:
    //     Creates a tree node with the specified key, text, and images, and inserts
    //     it into the collection at the specified index.
    //
    // Parameters:
    //   index:
    //     The location within the collection to insert the node.
    //
    //   key:
    //     The name of the tree node.
    //
    //   text:
    //     The text to display in the tree node.
    //
    //   imageIndex:
    //     The index of the image to display in the tree node.
    //
    //   selectedImageIndex:
    //     The index of the image to display in the tree node when it is in a selected
    //     state.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that was inserted in the collection.
    //public virtual TreeNode Insert(int index, string key, string text, int imageIndex, int selectedImageIndex);
    //
    // Summary:
    //     Creates a tree node with the specified key, text, and images, and inserts
    //     it into the collection at the specified index.
    //
    // Parameters:
    //   index:
    //     The location within the collection to insert the node.
    //
    //   key:
    //     The name of the tree node.
    //
    //   text:
    //     The text to display in the tree node.
    //
    //   imageKey:
    //     The key of the image to display in the tree node.
    //
    //   selectedImageKey:
    //     The key of the image to display in the tree node when it is in a selected
    //     state.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that was inserted in the collection.
    //public virtual TreeNode Insert(int index, string key, string text, string imageKey, string selectedImageKey);
    //
    // Summary:
    //     Removes the specified tree node from the tree node collection.
    //
    // Parameters:
    //   node:
    //     The System.Windows.Forms.TreeNode to remove.
    //public void Remove(TreeNode node);
    //
    // Summary:
    //     Removes a tree node from the tree node collection at a specified index.
    //
    // Parameters:
    //   index:
    //     The index of the System.Windows.Forms.TreeNode to remove.
    //public virtual void RemoveAt(int index);
    //
    // Summary:
    //     Removes the tree node with the specified key from the collection.
    //
    // Parameters:
    //   key:
    //     The name of the tree node to remove from the collection.
    //public virtual void RemoveByKey(string key);
  }
}
