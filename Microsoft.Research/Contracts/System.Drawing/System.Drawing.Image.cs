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
using System.IO;
using System.Diagnostics.Contracts;
using System.Drawing.Imaging;


namespace System.Drawing
{
  // Summary:
  //     An abstract base class that provides functionality for the System.Drawing.Bitmap
  //     and System.Drawing.Imaging.Metafile descended classes.
  //[Serializable]
  //[ImmutableObject(true)]
  //[ComVisible(true)]
  //[Editor("System.Drawing.Design.ImageEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
  //[TypeConverter(typeof(ImageConverter))]
  public /*abstract*/ class Image //: MarshalByRefObject, ISerializable, ICloneable, IDisposable
  {
    internal Image() { }

    // Summary:
    //     Gets attribute flags for the pixel data of this System.Drawing.Image.
    //
    // Returns:
    //     The integer representing a bitwise combination of System.Drawing.Imaging.ImageFlags
    //     for this System.Drawing.Image.
    //[Browsable(false)]
    //public int Flags { get; }
    //
    // Summary:
    //     Gets an array of GUIDs that represent the dimensions of frames within this
    //     System.Drawing.Image.
    //
    // Returns:
    //     An array of GUIDs that specify the dimensions of frames within this System.Drawing.Image
    //     from most significant to least significant.
    //[Browsable(false)]
    public Guid[] FrameDimensionsList 
    { 
      get
    {
        Contract.Ensures(Contract.Result<Guid[]>() != null);

        return default(Guid[]);
    }
    }
    //
    // Summary:
    //     Gets the height, in pixels, of this System.Drawing.Image.
    //
    // Returns:
    //     The height, in pixels, of this System.Drawing.Image.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[DefaultValue(false)]
    //[Browsable(false)]
    public int Height 
    {
      get
      {
        // F: It would make sense putting > 0 below, but I could not prove it
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }

    //
    // Summary:
    //     Gets the horizontal resolution, in pixels per inch, of this System.Drawing.Image.
    //
    // Returns:
    //     The horizontal resolution, in pixels per inch, of this System.Drawing.Image.
    public float HorizontalResolution 
    {
      get
      {
        // F: It would make sense putting > 0 below, but I could not prove it
        Contract.Ensures(Contract.Result<float>() >= 0.0);

        return default(float);
      }
    }


    //
    // Summary:
    //     Gets or sets the color palette used for this System.Drawing.Image.
    //
    // Returns:
    //     A System.Drawing.Imaging.ColorPalette that represents the color palette used
    //     for this System.Drawing.Image.
    //[Browsable(false)]
    public ColorPalette Palette 
    { get
    {
      Contract.Ensures(Contract.Result<ColorPalette>() != null);

      return default(ColorPalette);
    }
      //set; 
    }

    //
    // Summary:
    //     Gets the width and height of this image.
    //
    // Returns:
    //     A System.Drawing.SizeF structure that represents the width and height of
    //     this System.Drawing.Image.
    //public SizeF PhysicalDimension { get; }
    //
    // Summary:
    //     Gets the pixel format for this System.Drawing.Image.
    //
    // Returns:
    //     A System.Drawing.Imaging.PixelFormat that represents the pixel format for
    //     this System.Drawing.Image.
    public PixelFormat PixelFormat {
      get {
        return default(PixelFormat);
      }
    }
    //
    // Summary:
    //     Gets IDs of the property items stored in this System.Drawing.Image.
    //
    // Returns:
    //     An array of the property IDs, one for each property item stored in this image.
    //[Browsable(false)]
    public int[] PropertyIdList 
    {
      get
      {
        Contract.Ensures(Contract.Result<int[]>() != null);

        return default(int[]);
      }
    }
    //
    // Summary:
    //     Gets all the property items (pieces of metadata) stored in this System.Drawing.Image.
    //
    // Returns:
    //     An array of System.Drawing.Imaging.PropertyItem objects, one for each property
    //     item stored in the image.
    //[Browsable(false)]
    public PropertyItem[] PropertyItems
    {
      get
      {
        Contract.Ensures(Contract.Result<PropertyItem[]>() != null);

        return default(PropertyItem[]);
      }
    }
    //
    // Summary:
    //     Gets the file format of this System.Drawing.Image.
    //
    // Returns:
    //     The System.Drawing.Imaging.ImageFormat that represents the file format of
    //     this System.Drawing.Image.
    public ImageFormat RawFormat
    {
      get
      {
        Contract.Ensures(Contract.Result<ImageFormat>() != null);

        return default(ImageFormat);
      }
    }
    //
    // Summary:
    //     Gets the width and height, in pixels, of this image.
    //
    // Returns:
    //     A System.Drawing.Size structure that represents the width and height, in
    //     pixels, of this image.
    //public Size Size { get; }
    //
    // Summary:
    //     Gets or sets an object that provides additional data about the image.
    //
    // Returns:
    //     The System.Object that provides additional data about the image.
    //[DefaultValue("")]
    //[TypeConverter(typeof(StringConverter))]
    //[Localizable(false)]
    //[Bindable(true)]
    //public object Tag { get; set; }
    //
    // Summary:
    //     Gets the vertical resolution, in pixels per inch, of this System.Drawing.Image.
    //
    // Returns:
    //     The vertical resolution, in pixels per inch, of this System.Drawing.Image.
    public float VerticalResolution
    {
      get
      {
        // F: It would make sense putting > 0 below, but I could not prove it
        Contract.Ensures(Contract.Result<float>() >= 0.0);

        return default(float);
      }
    }
    //
    // Summary:
    //     Gets the width, in pixels, of this System.Drawing.Image.
    //
    // Returns:
    //     The width, in pixels, of this System.Drawing.Image.
    //[Browsable(false)]
    //[DefaultValue(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Width
    {
      get
      {
        // F: It would make sense putting > 0 below, but I could not prove it
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }

    // Summary:
    //     Creates an exact copy of this System.Drawing.Image.
    //
    // Returns:
    //     The System.Drawing.Image this method creates, cast as an object.
    //public object Clone();
    //
    // Summary:
    //     Releases all resources used by this System.Drawing.Image.
    //
    // Returns:
    //     This method does not return a value.
    //public void Dispose();
    //
    //protected virtual void Dispose(bool disposing);
    //
    // Summary:
    //     Creates an System.Drawing.Image from the specified file.
    //
    // Parameters:
    //   filename:
    //     A string that contains the name of the file from which to create the System.Drawing.Image.
    //
    // Returns:
    //     The System.Drawing.Image this method creates.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     The file does not have a valid image format.-or-GDI+ does not support the
    //     pixel format of the file.
    //
    //   System.IO.FileNotFoundException:
    //     The specified file does not exist.
    public static Image FromFile(string filename)
    {
      Contract.Requires(filename != null);

      Contract.Ensures(Contract.Result<Image>() != null);

      return default(Image);
    }

    //
    // Summary:
    //     Creates an System.Drawing.Image from the specified file using embedded color
    //     management information in that file.
    //
    // Parameters:
    //   filename:
    //     A string that contains the name of the file from which to create the System.Drawing.Image.
    //
    //   useEmbeddedColorManagement:
    //     Set to true to use color management information embedded in the image file;
    //     otherwise, false.
    //
    // Returns:
    //     The System.Drawing.Image this method creates.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     The file does not have a valid image format.-or-GDI+ does not support the
    //     pixel format of the file.
    //
    //   System.IO.FileNotFoundException:
    //     The specified file does not exist.
    public static Image FromFile(string filename, bool useEmbeddedColorManagement)
    {
      Contract.Requires(filename != null);

      Contract.Ensures(Contract.Result<Image>() != null);

      return default(Image);
    }
    //
    // Summary:
    //     Creates a System.Drawing.Bitmap from a handle to a GDI bitmap.
    //
    // Parameters:
    //   hbitmap:
    //     The GDI bitmap handle from which to create the System.Drawing.Bitmap.
    //
    // Returns:
    //     The System.Drawing.Bitmap this method creates.
    public static Bitmap FromHbitmap(IntPtr hbitmap)
    {
      Contract.Ensures(Contract.Result<Bitmap>() != null);

      return default(Bitmap);
    }
    //
    // Summary:
    //     Creates a System.Drawing.Bitmap from a handle to a GDI bitmap and a handle
    //     to a GDI palette.
    //
    // Parameters:
    //   hbitmap:
    //     The GDI bitmap handle from which to create the System.Drawing.Bitmap.
    //
    //   hpalette:
    //     A handle to a GDI palette used to define the bitmap colors if the bitmap
    //     specified in the hBitmap parameter is not a device-independent bitmap (DIB).
    //
    // Returns:
    //     The System.Drawing.Bitmap this method creates.
    public static Bitmap FromHbitmap(IntPtr hbitmap, IntPtr hpalette)
    {
      Contract.Ensures(Contract.Result<Bitmap>() != null);

      return default(Bitmap);
    }
    //
    // Summary:
    //     Creates an System.Drawing.Image from the specified data stream.
    //
    // Parameters:
    //   stream:
    //     A System.IO.Stream that contains the data for this System.Drawing.Image.
    //
    // Returns:
    //     The System.Drawing.Image this method creates.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The stream does not have a valid image format-or-stream is null.
    public static Image FromStream(Stream stream)
    {
      Contract.Requires(stream != null);

      Contract.Ensures(Contract.Result<Image>() != null);

      return default(Image);
    }
    //
    // Summary:
    //     Creates an System.Drawing.Image from the specified data stream, optionally
    //     using embedded color management information in that stream.
    //
    // Parameters:
    //   stream:
    //     A System.IO.Stream that contains the data for this System.Drawing.Image.
    //
    //   useEmbeddedColorManagement:
    //     true to use color management information embedded in the data stream; otherwise,
    //     false.
    //
    // Returns:
    //     The System.Drawing.Image this method creates.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The stream does not have a valid image format -or-stream is null.
    public static Image FromStream(Stream stream, bool useEmbeddedColorManagement)
    {
      Contract.Requires(stream != null);

      Contract.Ensures(Contract.Result<Image>() != null);

      return default(Image);
    }

    //
    // Summary:
    //     Creates an System.Drawing.Image from the specified data stream, optionally
    //     using embedded color management information and validating the image data.
    //
    // Parameters:
    //   stream:
    //     A System.IO.Stream that contains the data for this System.Drawing.Image.
    //
    //   useEmbeddedColorManagement:
    //     true to use color management information embedded in the data stream; otherwise,
    //     false.
    //
    //   validateImageData:
    //     true to validate the image data; otherwise, false.
    //
    // Returns:
    //     The System.Drawing.Image this method creates.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The stream does not have a valid image format.
    public static Image FromStream(Stream stream, bool useEmbeddedColorManagement, bool validateImageData)
    {
      Contract.Requires(stream != null);

      Contract.Ensures(Contract.Result<Image>() != null);

      return default(Image);
    }
    //
    // Summary:
    //     Gets the bounds of the image in the specified unit.
    //
    // Parameters:
    //   pageUnit:
    //     One of the System.Drawing.GraphicsUnit values indicating the unit of measure
    //     for the bounding rectangle.
    //
    // Returns:
    //     The System.Drawing.RectangleF that represents the bounds of the image, in
    //     the specified unit.
    //public RectangleF GetBounds(ref GraphicsUnit pageUnit);
    //
    // Summary:
    //     Returns information about the parameters supported by the specified image
    //     encoder.
    //
    // Parameters:
    //   encoder:
    //     A GUID that specifies the image encoder.
    //
    // Returns:
    //     An System.Drawing.Imaging.EncoderParameters that contains an array of System.Drawing.Imaging.EncoderParameter
    //     objects. Each System.Drawing.Imaging.EncoderParameter contains information
    //     about one of the parameters supported by the specified image encoder.
    //public EncoderParameters GetEncoderParameterList(Guid encoder);
    //
    // Summary:
    //     Returns the number of frames of the specified dimension.
    //
    // Parameters:
    //   dimension:
    //     A System.Drawing.Imaging.FrameDimension that specifies the identity of the
    //     dimension type.
    //
    // Returns:
    //     The number of frames in the specified dimension.
    //public int GetFrameCount(FrameDimension dimension);
    //
    // Summary:
    //     Returns the color depth, in number of bits per pixel, of the specified pixel
    //     format.
    //
    // Parameters:
    //   pixfmt:
    //     The System.Drawing.Imaging.PixelFormat member that specifies the format for
    //     which to find the size.
    //
    // Returns:
    //     The color depth of the specified pixel format.
    //public static int GetPixelFormatSize(PixelFormat pixfmt);
    //
    // Summary:
    //     Gets the specified property item from this System.Drawing.Image.
    //
    // Parameters:
    //   propid:
    //     The ID of the property item to get.
    //
    // Returns:
    //     The System.Drawing.Imaging.PropertyItem this method gets.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The image format of this image does not support property items.
    //public PropertyItem GetPropertyItem(int propid);
    //
    // Summary:
    //     Returns a thumbnail for this System.Drawing.Image.
    //
    // Parameters:
    //   thumbWidth:
    //     The width, in pixels, of the requested thumbnail image.
    //
    //   thumbHeight:
    //     The height, in pixels, of the requested thumbnail image.
    //
    //   callback:
    //     A System.Drawing.Image.GetThumbnailImageAbort delegate. In GDI+ version 1.0,
    //     the delegate is not used. Even so, you must create a delegate and pass a
    //     reference to that delegate in this parameter.
    //
    //   callbackData:
    //     Must be System.IntPtr.Zero.
    //
    // Returns:
    ////     An System.Drawing.Image that represents the thumbnail.
    //public Image GetThumbnailImage(int thumbWidth, int thumbHeight, Image.GetThumbnailImageAbort callback, IntPtr callbackData)
    //{
    //  Contract.Requires(callbackData == System.IntPtr.Zero);

    //  Contract.Ensures(Contract.Result<Image>() != null);

    //  return default(Image);
    //}
    //
    // Summary:
    //     Returns a value that indicates whether the pixel format for this System.Drawing.Image
    //     contains alpha information.
    //
    // Parameters:
    //   pixfmt:
    //     The System.Drawing.Imaging.PixelFormat to test.
    //
    // Returns:
    //     true if pixfmt contains alpha information; otherwise, false.
    [Pure]
    public static bool IsAlphaPixelFormat(PixelFormat pixfmt)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a value that indicates whether the pixel format is 32 bits per pixel.
    //
    // Parameters:
    //   pixfmt:
    //     The System.Drawing.Imaging.PixelFormat to test.
    //
    // Returns:
    //     true if pixfmt is canonical; otherwise, false.
    [Pure]
    public static bool IsCanonicalPixelFormat(PixelFormat pixfmt)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a value that indicates whether the pixel format is 64 bits per pixel.
    //
    // Parameters:
    //   pixfmt:
    //     The System.Drawing.Imaging.PixelFormat enumeration to test.
    //
    // Returns:
    //     true if pixfmt is extended; otherwise, false.
    [Pure]
    public static bool IsExtendedPixelFormat(PixelFormat pixfmt)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Removes the specified property item from this System.Drawing.Image.
    //
    // Parameters:
    //   propid:
    //     The ID of the property item to remove.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The image does not contain the requested property item.-or-The image format
    //     for this image does not support property items.
    //public void RemovePropertyItem(int propid);
    //
    // Summary:
    //     This method rotates, flips, or rotates and flips the System.Drawing.Image.
    //
    // Parameters:
    //   rotateFlipType:
    //     A System.Drawing.RotateFlipType member that specifies the type of rotation
    //     and flip to apply to the image.
    //public void RotateFlip(RotateFlipType rotateFlipType);
    //
    // Summary:
    //     Saves this System.Drawing.Image to the specified file or stream.
    //
    // Parameters:
    //   filename:
    //     A string that contains the name of the file to which to save this System.Drawing.Image.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     filename is null.
    //
    //   System.Runtime.InteropServices.ExternalException:
    //     The image was saved with the wrong image format.-or- The image was saved
    //     to the same file it was created from.
    public void Save(string filename)
    {
      Contract.Requires(filename != null);

    }
    //
    // Summary:
    //     Saves this image to the specified stream in the specified format.
    //
    // Parameters:
    //   stream:
    //     The System.IO.Stream where the image will be saved.
    //
    //   format:
    //     An System.Drawing.Imaging.ImageFormat that specifies the format of the saved
    //     image.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     stream or format is null.
    //
    //   System.Runtime.InteropServices.ExternalException:
    //     The image was saved with the wrong image format
    public void Save(Stream stream, ImageFormat format)
    {
      Contract.Requires(stream != null);
      Contract.Requires(format != null);

    }
    //
    // Summary:
    //     Saves this System.Drawing.Image to the specified file in the specified format.
    //
    // Parameters:
    //   filename:
    //     A string that contains the name of the file to which to save this System.Drawing.Image.
    //
    //   format:
    //     The System.Drawing.Imaging.ImageFormat for this System.Drawing.Image.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     filename or format is null.
    //
    //   System.Runtime.InteropServices.ExternalException:
    //     The image was saved with the wrong image format.-or- The image was saved
    //     to the same file it was created from.
    public void Save(string filename, ImageFormat format)
    {
      Contract.Requires(filename != null);
      Contract.Requires(format != null);
    }
    //
    // Summary:
    //     Saves this image to the specified stream, with the specified encoder and
    //     image encoder parameters.
    //
    // Parameters:
    //   stream:
    //     The System.IO.Stream where the image will be saved.
    //
    //   encoder:
    //     The System.Drawing.Imaging.ImageCodecInfo for this System.Drawing.Image.
    //
    //   encoderParams:
    //     An System.Drawing.Imaging.EncoderParameters that specifies parameters used
    //     by the image encoder.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     stream is null.
    //
    //   System.Runtime.InteropServices.ExternalException:
    //     The image was saved with the wrong image format.
    //public void Save(Stream stream, ImageCodecInfo encoder, EncoderParameters encoderParams);
   
    //
    // Summary:
    //     Saves this System.Drawing.Image to the specified file, with the specified
    //     encoder and image-encoder parameters.
    //
    // Parameters:
    //   filename:
    //     A string that contains the name of the file to which to save this System.Drawing.Image.
    //
    //   encoder:
    //     The System.Drawing.Imaging.ImageCodecInfo for this System.Drawing.Image.
    //
    //   encoderParams:
    //     An System.Drawing.Imaging.EncoderParameters to use for this System.Drawing.Image.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     filename or encoder is null.
    //
    //   System.Runtime.InteropServices.ExternalException:
    //     The image was saved with the wrong image format.-or- The image was saved
    //     to the same file it was created from.
    //public void Save(string filename, ImageCodecInfo encoder, EncoderParameters encoderParams);
    //
    // Summary:
    //     Adds a frame to the file or stream specified in a previous call to the Overload:System.Drawing.Image.Save
    //     method. Use this method to save selected frames from a multiple-frame image
    //     to another multiple-frame image.
    //
    // Parameters:
    //   encoderParams:
    //     An System.Drawing.Imaging.EncoderParameters that holds parameters required
    //     by the image encoder that is used by the save-add operation.
    //
    // Returns:
    //     This method does not return a value.
    //public void SaveAdd(EncoderParameters encoderParams);
    //
    // Summary:
    //     Adds a frame to the file or stream specified in a previous call to the Overload:System.Drawing.Image.Save
    //     method.
    //
    // Parameters:
    //   image:
    //     An System.Drawing.Image that contains the frame to add.
    //
    //   encoderParams:
    //     An System.Drawing.Imaging.EncoderParameters that holds parameters required
    //     by the image encoder that is used by the save-add operation.
    //
    // Returns:
    //     This method does not return a value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     image is null.
    //public void SaveAdd(Image image, EncoderParameters encoderParams);
    //
    // Summary:
    //     Selects the frame specified by the dimension and index.
    //
    // Parameters:
    //   dimension:
    //     A System.Drawing.Imaging.FrameDimension that specifies the identity of the
    //     dimension type.
    //
    //   frameIndex:
    //     The index of the active frame.
    //
    // Returns:
    //     Always returns 0.
    public int SelectActiveFrame(FrameDimension dimension, int frameIndex)
    {

      Contract.Ensures(Contract.Result<int>() == 0);

      return default(int);
    }
    //
    // Summary:
    //     Stores a property item (piece of metadata) in this System.Drawing.Image.
    //
    // Parameters:
    //   propitem:
    //     The System.Drawing.Imaging.PropertyItem to be stored.
    //
    // Returns:
    //     This method does not return a value.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The image format of this image does not support property items.
    //public void SetPropertyItem(PropertyItem propitem);

    // Summary:
    //     Provides a callback method for determining when the System.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,System.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)
    //     method should prematurely cancel execution.
    //
    // Returns:
    //     This method returns true if it decides that the System.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,System.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)
    //     method should prematurely stop execution; otherwise, it returns false.
    //public delegate bool GetThumbnailImageAbort();
  }
}
