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
using System.Collections.Specialized;


namespace System.Windows.Forms
{
  // Summary:
  //     Specifies the number of colors used to display an image in an System.Windows.Forms.ImageList
  //     control.
  public enum ColorDepth
  {
    // Summary:
    //     A 4-bit image.
    Depth4Bit = 4,
    //
    // Summary:
    //     An 8-bit image.
    Depth8Bit = 8,
    //
    // Summary:
    //     A 16-bit image.
    Depth16Bit = 16,
    //
    // Summary:
    //     A 24-bit image.
    Depth24Bit = 24,
    //
    // Summary:
    //     A 32-bit image.
    Depth32Bit = 32,
  }

  // Summary:
  //     Provides methods to manage a collection of System.Drawing.Image objects.
  //     This class cannot be inherited.
  public sealed class ImageList
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ImageList class with
    //     default values for System.Windows.Forms.ImageList.ColorDepth, System.Windows.Forms.ImageList.ImageSize,
    //     and System.Windows.Forms.ImageList.TransparentColor.
    public ImageList()
    { }

    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ImageList class, associating
    //     it with a container.
    //
    // Parameters:
    //   container:
    //     An object implementing System.ComponentModel.IContainer to associate with
    //     this instance of System.Windows.Forms.ImageList.
    // public ImageList(IContainer container);
    //{ }

    // Summary:
    //     Gets the color depth of the image list.
    //
    // Returns:
    //     The number of available colors for the image. In the .NET Framework version
    //     1.0, the default is System.Windows.Forms.ColorDepth.Depth4Bit. In the .NET
    //     Framework version 1.1 or later, the default is System.Windows.Forms.ColorDepth.Depth8Bit.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The color depth is not a valid System.Windows.Forms.ColorDepth enumeration
    //     value.
    public ColorDepth ColorDepth
    {
      get
      {
        return default(ColorDepth);
      }
      set
      {
      }
    }

    //
    // Summary:
    //     Gets the handle of the image list object.
    //
    // Returns:
    //     The handle for the image list. The default is null.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     Creating the handle for the System.Windows.Forms.ImageList failed.
    public IntPtr Handle { get { return default(IntPtr); } }

    // Summary:
    //     Gets a value indicating whether the underlying Win32 handle has been created.
    //
    // Returns:
    //     true if the System.Windows.Forms.ImageList.Handle has been created; otherwise,
    //     false. The default is false.
    public bool HandleCreated { get { return default(bool); } }

    //
    // Summary:
    //     Gets the System.Windows.Forms.ImageList.ImageCollection for this image list.
    //
    // Returns:
    //     The collection of images.
    public ImageList.ImageCollection Images
	{
		get
		{
				Contract.Ensures(Contract.Result<ImageCollection>() != null);
				return default(ImageCollection);
		}
	}


    //
    // Summary:
    //     Gets or sets the size of the images in the image list.
    //
    // Returns:
    //     The System.Drawing.Size that defines the height and width, in pixels, of
    //     the images in the list. The default size is 16 by 16. The maximum size is
    //     256 by 256.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value assigned is equal to System.Drawing.Size.IsEmpty.-or- The value
    //     of the height or width is less than or equal to 0.-or- The value of the height
    //     or width is greater than 256.
    //
    //   System.ArgumentOutOfRangeException:
    //     The new size has a dimension less than 0 or greater than 256.
    // public System.Drawing.Size ImageSize { get; set; }
    //
    // Summary:
    //     Gets the System.Windows.Forms.ImageListStreamer associated with this image
    //     list.
    //
    // Returns:
    //     null if the image list is empty; otherwise, a System.Windows.Forms.ImageListStreamer
    //     for this System.Windows.Forms.ImageList.
    //public ImageListStreamer ImageStream { get; set; }
    //
    // Summary:
    //     Gets or sets an object that contains additional data about the System.Windows.Forms.ImageList.
    //
    // Returns:
    //     An System.Object that contains additional data about the System.Windows.Forms.ImageList.

    //public object Tag { get; set; }
    //
    // Summary:
    //     Gets or sets the color to treat as transparent.
    //
    // Returns:
    //     One of the System.Drawing.Color values. The default is Transparent.
    //public System.Drawing.Color TransparentColor { get; set; }

    // Summary:
    //     Occurs when the System.Windows.Forms.ImageList.Handle is recreated.

    //public event EventHandler RecreateHandle;


    //
    // Summary:
    //     Draws the image indicated by the specified index on the specified System.Drawing.Graphics
    //     at the given location.
    //
    // Parameters:
    //   g:
    //     The System.Drawing.Graphics to draw on.
    //
    //   pt:
    //     The location defined by a System.Drawing.Point at which to draw the image.
    //
    //   index:
    //     The index of the image in the System.Windows.Forms.ImageList to draw.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index is less than 0.-or- The index is greater than or equal to the count
    //     of images in the image list.
    //public void Draw(System.Drawing.Graphics g, System.Drawing.Point pt, int index)
    //{
    //  Contract.Requires(index >= 0);
    //}


    //
    // Summary:
    //     Draws the image indicated by the given index on the specified System.Drawing.Graphics
    //     at the specified location.
    //
    // Parameters:
    //   g:
    //     The System.Drawing.Graphics to draw on.
    //
    //   x:
    //     The horizontal position at which to draw the image.
    //
    //   y:
    //     The vertical position at which to draw the image.
    //
    //   index:
    //     The index of the image in the System.Windows.Forms.ImageList to draw.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index is less than 0.-or- The index is greater than or equal to the count
    //     of images in the image list.
    //public void Draw(System.Drawing.Graphics g, int x, int y, int index);

    //
    // Summary:
    //     Draws the image indicated by the given index on the specified System.Drawing.Graphics
    //     using the specified location and size.
    //
    // Parameters:
    //   g:
    //     The System.Drawing.Graphics to draw on.
    //
    //   x:
    //     The horizontal position at which to draw the image.
    //
    //   y:
    //     The vertical position at which to draw the image.
    //
    //   width:
    //     The width, in pixels, of the destination image.
    //
    //   height:
    //     The height, in pixels, of the destination image.
    //
    //   index:
    //     The index of the image in the System.Windows.Forms.ImageList to draw.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index is less than 0.-or- The index is greater than or equal to the count
    //     of images in the image list.
    //public void Draw(System.Drawing.Graphics g, int x, int y, int width, int height, int index);
    //
    // Summary:
    //     Returns a string that represents the current System.Windows.Forms.ImageList.
    //
    // Returns:
    //     A string that represents the current System.Windows.Forms.ImageList.
    //public override string ToString();


    // Summary:
    //     Encapsulates the collection of System.Drawing.Image objects in an System.Windows.Forms.ImageList.
    public sealed class ImageCollection //: IList, ICollection, IEnumerable
    {
      private ImageCollection() { }

      // Summary:
      //     Gets the number of images currently in the list.
      //
      // Returns:
      //     The number of images in the list. The default is 0.
     // [Browsable(false)]
      //public int Count { get; }

      //
      // Summary:
      //     Gets a value indicating whether the System.Windows.Forms.ImageList has any
      //     images.
      //
      // Returns:
      //     true if there are no images in the list; otherwise, false. The default is
      //     false.
      //public bool Empty { get; }
      //
      // Summary:
      //     Gets a value indicating whether the list is read-only.
      //
      // Returns:
      //     Always false.
      //public bool IsReadOnly { get; }
      //
      // Summary:
      //     Gets the collection of keys associated with the images in the System.Windows.Forms.ImageList.ImageCollection.
      //
      // Returns:
      //     A System.Collections.Specialized.StringCollection containing the names of
      //     the images in the System.Windows.Forms.ImageList.ImageCollection.
      public StringCollection Keys
      {
        get
        {
          Contract.Ensures(Contract.Result<StringCollection>() != null); 
          return default(StringCollection);
        }
      }

      // Summary:
      //     Gets or sets an System.Drawing.Image at the specified index within the collection.
      //
      // Parameters:
      //   index:
      //     The index of the image to get or set.
      //
      // Returns:
      //     The image in the list specified by index.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index is less than 0 or greater than or equal to System.Windows.Forms.ImageList.ImageCollection.Count.
      //
      //   System.ArgumentException:
      //     image is not a System.Drawing.Bitmap.
      //
      //   System.ArgumentNullException:
      //     The image to be assigned is null or not a System.Drawing.Bitmap.
      //
      //   System.InvalidOperationException:
      //     The image cannot be added to the list.
     // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
     // [Browsable(false)]
      //public System.Drawing.Image this[int index] { get; set; }
      //
      // Summary:
      //     Gets an System.Drawing.Image with the specified key from the collection.
      //
      // Parameters:
      //   key:
      //     The name of the image to retrieve from the collection.
      //
      // Returns:
      //     The System.Drawing.Image with the specified key.
      //public System.Drawing.Image this[string key] { get; }

      // Summary:
      //     Adds the specified icon to the System.Windows.Forms.ImageList.
      //
      // Parameters:
      //   value:
      //     An System.Drawing.Icon to add to the list.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     value is null -or- value is not an System.Drawing.Icon.
      //public void Add(System.Drawing.Icon value);
      //
      // Summary:
      //     Adds the specified image to the System.Windows.Forms.ImageList.
      //
      // Parameters:
      //   value:
      //     A System.Drawing.Bitmap of the image to add to the list.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     The image being added is null.
      //
      //   System.ArgumentException:
      //     The image being added is not a System.Drawing.Bitmap.
      //public void Add(System.Drawing.Image value);
      //
      // Summary:
      //     Adds an icon with the specified key to the end of the collection.
      //
      // Parameters:
      //   key:
      //     The name of the icon.
      //
      //   icon:
      //     The System.Drawing.Icon to add to the collection.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     icon is null.
      //public void Add(string key, System.Drawing.Icon icon);
      //
      // Summary:
      //     Adds an image with the specified key to the end of the collection.
      //
      // Parameters:
      //   key:
      //     The name of the image.
      //
      //   image:
      //     The System.Drawing.Image to add to the collection.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     image is null.
      //public void Add(string key, System.Drawing.Image image);
      //
      // Summary:
      //     Adds the specified image to the System.Windows.Forms.ImageList, using the
      //     specified color to generate the mask.
      //
      // Parameters:
      //   value:
      //     A System.Drawing.Bitmap of the image to add to the list.
      //
      //   transparentColor:
      //     The System.Drawing.Color to mask this image.
      //
      // Returns:
      //     The index of the newly added image, or -1 if the image cannot be added.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     The image being added is null.
      //
      //   System.ArgumentException:
      //     The image being added is not a System.Drawing.Bitmap.
      //public int Add(System.Drawing.Image value, System.Drawing.Color transparentColor);
      //
      // Summary:
      //     Adds an array of images to the collection.
      //
      // Parameters:
      //   images:
      //     The array of System.Drawing.Image objects to add to the collection.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     images is null.
      //public void AddRange(System.Drawing.Image[] images);
      //
      // Summary:
      //     Adds an image strip for the specified image to the System.Windows.Forms.ImageList.
      //
      // Parameters:
      //   value:
      //     A System.Drawing.Bitmap with the images to add.
      //
      // Returns:
      //     The index of the newly added image, or -1 if the image cannot be added.
      //
      // Exceptions:
      //   System.ArgumentException:
      //     The image being added is null.  -or- The image being added is not a System.Drawing.Bitmap.
      //
      //   System.InvalidOperationException:
      //     The image cannot be added.  -or- The width of image strip being added is
      //     0, or the width is not equal to the existing image width.  -or- The image
      //     strip height is not equal to existing image height.
      public int AddStrip(System.Drawing.Image value)
      {
        Contract.Requires(value != null);

        Contract.Ensures(Contract.Result<int>() >= -1);

        return default(int);
      }
      //
      // Summary:
      //     Removes all the images and masks from the System.Windows.Forms.ImageList.
      //public void Clear();
      //
      // Summary:
      //     Not supported. The System.Collections.IList.Contains(System.Object) method
      //     indicates whether a specified object is contained in the list.
      //
      // Parameters:
      //   image:
      //     The System.Drawing.Image to find in the list.
      //
      // Returns:
      //     true if the image is found in the list; otherwise, false.
      //
      // Exceptions:
      //   System.NotSupportedException:
      //     This method is not supported.
     // [EditorBrowsable(EditorBrowsableState.Never)]
      //public bool Contains(System.Drawing.Image image);
      //
      // Summary:
      //     Determines if the collection contains an image with the specified key.
      //
      // Parameters:
      //   key:
      //     The key of the image to search for.
      //
      // Returns:
      //     true to indicate an image with the specified key is contained in the collection;
      //     otherwise, false.
      //public bool ContainsKey(string key);
      //
      // Summary:
      //     Returns an enumerator that can be used to iterate through the item collection.
      //
      // Returns:
      //     An System.Collections.IEnumerator that represents the item collection.
      //public IEnumerator GetEnumerator();
      //
      // Summary:
      //     Not supported. The System.Collections.IList.IndexOf(System.Object) method
      //     returns the index of a specified object in the list.
      //
      // Parameters:
      //   image:
      //     The System.Drawing.Image to find in the list.
      //
      // Returns:
      //     The index of the image in the list.
      //
      // Exceptions:
      //   System.NotSupportedException:
      //     This method is not supported.
     // [EditorBrowsable(EditorBrowsableState.Never)]
      //public int IndexOf(System.Drawing.Image image);
      //
      // Summary:
      //     Determines the index of the first occurrence of an image with the specified
      //     key in the collection.
      //
      // Parameters:
      //   key:
      //     The key of the image to retrieve the index for.
      //
      // Returns:
      //     The zero-based index of the first occurrence of an image with the specified
      //     key in the collection, if found; otherwise, -1.
      //public int IndexOfKey(string key);
      //
      // Summary:
      //     Not supported. The System.Collections.IList.Remove(System.Object) method
      //     removes a specified object from the list.
      //
      // Parameters:
      //   image:
      //     The System.Drawing.Image to remove from the list.
      //
      // Exceptions:
      //   System.NotSupportedException:
      //     This method is not supported.
     // [EditorBrowsable(EditorBrowsableState.Never)]
      //public void Remove(System.Drawing.Image image);
      //
      // Summary:
      //     Removes an image from the list.
      //
      // Parameters:
      //   index:
      //     The index of the image to remove.
      //
      // Exceptions:
      //   System.InvalidOperationException:
      //     The image cannot be removed.
      //
      //   System.ArgumentOutOfRangeException:
      //     The index value was less than 0.  -or- The index value is greater than or
      //     equal to the System.Windows.Forms.ImageList.ImageCollection.Count of images.
      //public void RemoveAt(int index);
      //
      // Summary:
      //     Removes the image with the specified key from the collection.
      //
      // Parameters:
      //   key:
      //     The key of the image to remove from the collection.
      //public void RemoveByKey(string key);
      //
      // Summary:
      //     Sets the key for an image in the collection.
      //
      // Parameters:
      //   index:
      //     The zero-based index of an image in the collection.
      //
      //   name:
      //     The name of the image to be set as the image key.
      //
      // Exceptions:
      //   System.IndexOutOfRangeException:
      //     The specified index is less than 0 or greater than or equal to System.Windows.Forms.ImageList.ImageCollection.Count.
      public void SetKeyName(int index, string name)
      {
        Contract.Requires(index >= 0);

      }
    }
  }
}