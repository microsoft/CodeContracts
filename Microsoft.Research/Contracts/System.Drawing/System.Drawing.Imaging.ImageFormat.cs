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
using System.Drawing;
using System.Diagnostics.Contracts;

namespace System.Drawing.Imaging
{
  // Summary:
  //     Specifies the file format of the image. Not inheritable.
  //[TypeConverter(typeof(ImageFormatConverter))]
  public sealed class ImageFormat
  {
    // Summary:
    //     Initializes a new instance of the System.Drawing.Imaging.ImageFormat class
    //     by using the specified System.Guid structure.
    //
    // Parameters:
    //   guid:
    //     The System.Guid structure that specifies a particular image format.
    //public ImageFormat(Guid guid);

    // Summary:
    //     Gets the bitmap (BMP) image format.
    //
    // Returns:
    //     An System.Drawing.Imaging.ImageFormat object that indicates the bitmap image
    //     format.
    public static ImageFormat Bmp 
    {
      get
      {
        Contract.Ensures(Contract.Result<ImageFormat>() != null);

        return default(ImageFormat);
      }
    }
    
    //
    // Summary:
    //     Gets the enhanced metafile (EMF) image format.
    //
    // Returns:
    //     An System.Drawing.Imaging.ImageFormat object that indicates the enhanced
    //     metafile image format.
    public static ImageFormat Emf
    {
      get
      {
        Contract.Ensures(Contract.Result<ImageFormat>() != null);

        return default(ImageFormat);
      }
    }
    //
    // Summary:
    //     Gets the Exchangeable Image File (Exif) format.
    //
    // Returns:
    //     An System.Drawing.Imaging.ImageFormat object that indicates the Exif format.
    public static ImageFormat Exif
    {
      get
      {
        Contract.Ensures(Contract.Result<ImageFormat>() != null);

        return default(ImageFormat);
      }
    }

    // Summary:
    //     Gets the Graphics Interchange Format (GIF) image format.
    //
    // Returns:
    //     An System.Drawing.Imaging.ImageFormat object that indicates the GIF image
    //     format.
    public static ImageFormat Gif
    {
      get
      {
        Contract.Ensures(Contract.Result<ImageFormat>() != null);

        return default(ImageFormat);
      }
    }
    //
    // Summary:
    //     Gets a System.Guid structure that represents this System.Drawing.Imaging.ImageFormat
    //     object.
    //
    // Returns:
    //     A System.Guid structure that represents this System.Drawing.Imaging.ImageFormat
    //     object.
    //public Guid Guid { get; }
    //
    // Summary:
    //     Gets the Windows icon image format.
    //
    // Returns:
    //     An System.Drawing.Imaging.ImageFormat object that indicates the Windows icon
    //     image format.
    public static ImageFormat Icon
    {
      get
      {
        Contract.Ensures(Contract.Result<ImageFormat>() != null);

        return default(ImageFormat);
      }
    }
    //
    // Summary:
    //     Gets the Joint Photographic Experts Group (JPEG) image format.
    //
    // Returns:
    //     An System.Drawing.Imaging.ImageFormat object that indicates the JPEG image
    //     format.
    public static ImageFormat Jpeg
    {
      get
      {
        Contract.Ensures(Contract.Result<ImageFormat>() != null);

        return default(ImageFormat);
      }
    }

    // Summary:
    //     Gets a memory bitmap image format.
    //
    // Returns:
    //     An System.Drawing.Imaging.ImageFormat object that indicates the memory bitmap
    //     image format.
    public static ImageFormat MemoryBmp
    {
      get
      {
        Contract.Ensures(Contract.Result<ImageFormat>() != null);

        return default(ImageFormat);
      }
    }
    //
    // Summary:
    //     Gets the W3C Portable Network Graphics (PNG) image format.
    //
    // Returns:
    //     An System.Drawing.Imaging.ImageFormat object that indicates the PNG image
    //     format.
    public static ImageFormat Png
    {
      get
      {
        Contract.Ensures(Contract.Result<ImageFormat>() != null);

        return default(ImageFormat);
      }
    }
    //
    // Summary:
    //     Gets the Tagged Image File Format (TIFF) image format.
    //
    // Returns:
    //     An System.Drawing.Imaging.ImageFormat object that indicates the TIFF image
    //     format.
    public static ImageFormat Tiff
    {
      get
      {
        Contract.Ensures(Contract.Result<ImageFormat>() != null);

        return default(ImageFormat);
      }
    }
    // Summary:
    //     Gets the Windows metafile (WMF) image format.
    //
    // Returns:
    //     An System.Drawing.Imaging.ImageFormat object that indicates the Windows metafile
    //     image format.
    public static ImageFormat Wmf
    {
      get
      {
        Contract.Ensures(Contract.Result<ImageFormat>() != null);

        return default(ImageFormat);
      }
    }

    // Summary:
    //     Returns a value that indicates whether the specified object is an System.Drawing.Imaging.ImageFormat
    //     object that is equivalent to this System.Drawing.Imaging.ImageFormat object.
    //
    // Parameters:
    //   o:
    //     The object to test.
    //
    // Returns:
    //     true if o is an System.Drawing.Imaging.ImageFormat object that is equivalent
    //     to this System.Drawing.Imaging.ImageFormat object; otherwise, false.
    //public override bool Equals(object o);
    //
    // Summary:
    //     Returns a hash code value that represents this object.
    //
    // Returns:
    //     A hash code that represents this object.
    //public override int GetHashCode();
    //
    // Summary:
    //     Converts this System.Drawing.Imaging.ImageFormat object to a human-readable
    //     string.
    //
    // Returns:
    //     A string that represents this System.Drawing.Imaging.ImageFormat object.
    //public override string ToString();
  }
}
