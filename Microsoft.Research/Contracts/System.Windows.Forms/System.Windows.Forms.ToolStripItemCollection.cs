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

using System.Diagnostics.Contracts;
using System.Drawing;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents a collection of System.Windows.Forms.ToolStripItem objects.
  //[ListBindable(false)]
  public class ToolStripItemCollection 
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolStripItemCollection
    //     class with the specified container System.Windows.Forms.ToolStrip and the
    //     specified array of System.Windows.Forms.ToolStripItem controls.
    //
    // Parameters:
    //   owner:
    //     The System.Windows.Forms.ToolStrip to which this System.Windows.Forms.ToolStripItemCollection
    //     belongs.
    //
    //   value:
    //     An array of type System.Windows.Forms.ToolStripItem containing the initial
    //     controls for this System.Windows.Forms.ToolStripItemCollection.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The owner parameter is null.
    public ToolStripItemCollection(ToolStrip owner, ToolStripItem[] value)
    {
      Contract.Requires(owner != null);
      Contract.Requires(value != null);
    }

    // Summary:
    //     Gets a value indicating whether the System.Windows.Forms.ToolStripItemCollection
    //     is read-only.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolStripItemCollection is read-only; otherwise,
    //     false.
    // public override bool IsReadOnly { get; }

    // Summary:
    //     Gets the item at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the item to get.
    //
    // Returns:
    //     The System.Windows.Forms.ToolStripItem located at the specified position
    //     in the System.Windows.Forms.ToolStripItemCollection.
    // public virtual ToolStripItem this//[int index] { get; }
    //
    // Summary:
    //     Gets the item with the specified name.
    //
    // Parameters:
    //   key:
    //     The name of the item to get.
    //
    // Returns:
    //     The System.Windows.Forms.ToolStripItem with the specified name.
    // public virtual ToolStripItem this[string key] { get; }

    // Summary:
    //     Adds a System.Windows.Forms.ToolStripItem that displays the specified text
    //     to the collection.
    //
    // Parameters:
    //   text:
    //     The text to be displayed on the System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     The new System.Windows.Forms.ToolStripItem.
    public ToolStripItem Add(string text)
    {
      Contract.Ensures(Contract.Result<ToolStripItem>() != null);

      return default(ToolStripItem);
    }
    //
    // Summary:
    //     Adds a System.Windows.Forms.ToolStripItem that displays the specified image
    //     to the collection.
    //
    // Parameters:
    //   image:
    //     The System.Drawing.Image to be displayed on the System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     The new System.Windows.Forms.ToolStripItem.
    public ToolStripItem Add(Image image)
    {
      Contract.Ensures(Contract.Result<ToolStripItem>() != null);

      return default(ToolStripItem);
    }
    
    //
    // Summary:
    //     Adds the specified item to the end of the collection.
    //
    // Parameters:
    //   value:
    //     The System.Windows.Forms.ToolStripItem to add to the end of the collection.
    //
    // Returns:
    //     An System.Int32 representing the zero-based index of the new item in the
    //     collection.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value parameter is null.
    // public int Add(ToolStripItem value);
    //
    // Summary:
    //     Adds a System.Windows.Forms.ToolStripItem that displays the specified image
    //     and text to the collection.
    //
    // Parameters:
    //   text:
    //     The text to be displayed on the System.Windows.Forms.ToolStripItem.
    //
    //   image:
    //     The System.Drawing.Image to be displayed on the System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     The new System.Windows.Forms.ToolStripItem.
    public ToolStripItem Add(string text, System.Drawing.Image image)
    {
      Contract.Ensures(Contract.Result<ToolStripItem>() != null);

      return default(ToolStripItem);
    }

    //
    // Summary:
    //     Adds a System.Windows.Forms.ToolStripItem that displays the specified image
    //     and text to the collection and that raises the System.Windows.Forms.ToolStripItem.Click
    //     event.
    //
    // Parameters:
    //   text:
    //     The text to be displayed on the System.Windows.Forms.ToolStripItem.
    //
    //   image:
    //     The System.Drawing.Image to be displayed on the System.Windows.Forms.ToolStripItem.
    //
    //   onClick:
    //     Raises the System.Windows.Forms.ToolStripItem.Click event.
    //
    // Returns:
    //     The new System.Windows.Forms.ToolStripItem.
    public ToolStripItem Add(string text, System.Drawing.Image image, EventHandler onClick)
    {
      Contract.Ensures(Contract.Result<ToolStripItem>() != null);

      return default(ToolStripItem);
    }
  }
}