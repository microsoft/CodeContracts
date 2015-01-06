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
using System.Drawing.Imaging;
using System.Diagnostics.Contracts;


namespace System.Drawing
{
  // Summary:
  //     Encapsulates a GDI+ bitmap, which consists of the pixel data for a graphics
  //     image and its attributes. A System.Drawing.Bitmap is an object used to work
  //     with images defined by pixel data.
  //[Serializable]
  //[ComVisible(true)]
  //[Editor("System.Drawing.Design.BitmapEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
  public sealed class Bitmap //: Image
  {
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class from the specified
    //     existing image.
    //
    // Parameters:
    //   original:
    //     The System.Drawing.Image from which to create the new System.Drawing.Bitmap.
    public Bitmap(Image original)
    {
      Contract.Requires(original != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class from the specified
    //     data stream.
    //
    // Parameters:
    //   stream:
    //     The data stream used to load the image.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     stream does not contain image data or is null.-or-stream contains a PNG image
    //     file with a single dimension greater than 65,535 pixels.
    public Bitmap(Stream stream)
    {
      Contract.Requires(stream != null);

    }
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class from the specified
    //     file.
    //
    // Parameters:
    //   filename:
    //     The name of the bitmap file.
    //public Bitmap(string filename);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class from the specified
    //     existing image, scaled to the specified size.
    //
    // Parameters:
    //   original:
    //     The System.Drawing.Image from which to create the new System.Drawing.Bitmap.
    //
    //   newSize:
    //     The System.Drawing.Size structure that represent the size of the new System.Drawing.Bitmap.
    //
    // Exceptions:
    //   System.Exception:
    //     The operation failed.
    public Bitmap(Image original, Size newSize)
    {
      Contract.Requires(original != null);

    }

    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class with the specified
    //     size.
    //
    // Parameters:
    //   width:
    //     The width, in pixels, of the new System.Drawing.Bitmap.
    //
    //   height:
    //     The height, in pixels, of the new System.Drawing.Bitmap.
    //
    // Exceptions:
    //   System.Exception:
    //     The operation failed.
    //public Bitmap(int width, int height);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class from the specified
    //     data stream.
    //
    // Parameters:
    //   stream:
    //     The data stream used to load the image.
    //
    //   useIcm:
    //     true to use color correction for this System.Drawing.Bitmap; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     stream does not contain image data or is null.-or-stream contains a PNG image
    //     file with a single dimension greater than 65,535 pixels.
    //public Bitmap(Stream stream, bool useIcm);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class from the specified
    //     file.
    //
    // Parameters:
    //   filename:
    //     The name of the bitmap file.
    //
    //   useIcm:
    //     true to use color correction for this System.Drawing.Bitmap; otherwise, false.
    //public Bitmap(string filename, bool useIcm);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class from a specified
    //     resource.
    //
    // Parameters:
    //   type:
    //     The class used to extract the resource.
    //
    //   resource:
    //     The name of the resource.
    //public Bitmap(Type type, string resource);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class from the specified
    //     existing image, scaled to the specified size.
    //
    // Parameters:
    //   original:
    //     The System.Drawing.Image from which to create the new System.Drawing.Bitmap.
    //
    //   width:
    //     The width, in pixels, of the new System.Drawing.Bitmap.
    //
    //   height:
    //     The height, in pixels, of the new System.Drawing.Bitmap.
    //
    // Exceptions:
    //   System.Exception:
    //     The operation failed.
    //public Bitmap(Image original, int width, int height);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class with the specified
    //     size and with the resolution of the specified System.Drawing.Graphics object.
    //
    // Parameters:
    //   width:
    //     The width, in pixels, of the new System.Drawing.Bitmap.
    //
    //   height:
    //     The height, in pixels, of the new System.Drawing.Bitmap.
    //
    //   g:
    //     The System.Drawing.Graphics object that specifies the resolution for the
    //     new System.Drawing.Bitmap.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     g is null.
    public Bitmap(int width, int height, Graphics g)
    {
      Contract.Requires(g != null);

    }
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class with the specified
    //     size and format.
    //
    // Parameters:
    //   width:
    //     The width, in pixels, of the new System.Drawing.Bitmap.
    //
    //   height:
    //     The height, in pixels, of the new System.Drawing.Bitmap.
    //
    //   format:
    //     The System.Drawing.Imaging.PixelFormat enumeration for the new System.Drawing.Bitmap.
    //public Bitmap(int width, int height, PixelFormat format);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Bitmap class with the specified
    //     size, pixel format, and pixel data.
    //
    // Parameters:
    //   width:
    //     The width, in pixels, of the new System.Drawing.Bitmap.
    //
    //   height:
    //     The height, in pixels, of the new System.Drawing.Bitmap.
    //
    //   stride:
    //     Integer that specifies the byte offset between the beginning of one scan
    //     line and the next. This is usually (but not necessarily) the number of bytes
    //     in the pixel format (for example, 2 for 16 bits per pixel) multiplied by
    //     the width of the bitmap. The value passed to this parameter must be a multiple
    //     of four..
    //
    //   format:
    //     The System.Drawing.Imaging.PixelFormat enumeration for the new System.Drawing.Bitmap.
    //
    //   scan0:
    //     Pointer to an array of bytes that contains the pixel data.
    //public Bitmap(int width, int height, int stride, PixelFormat format, IntPtr scan0);

    // Summary:
    //     Creates a copy of the section of this System.Drawing.Bitmap defined by System.Drawing.Rectangle
    //     structure and with a specified System.Drawing.Imaging.PixelFormat enumeration.
    //
    // Parameters:
    //   rect:
    //     Defines the portion of this System.Drawing.Bitmap to copy. Coordinates are
    //     relative to this System.Drawing.Bitmap.
    //
    //   format:
    //     Specifies the System.Drawing.Imaging.PixelFormat enumeration for the destination
    //     System.Drawing.Bitmap.
    //
    // Returns:
    //     The new System.Drawing.Bitmap that this method creates.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     rect is outside of the source bitmap bounds.
    //
    //   System.ArgumentException:
    //     The height or width of rect is 0.
    //public Bitmap Clone(Rectangle rect, PixelFormat format)
    //{
    //  Contract.Ensures(Contract.Result<Bitmap>() != null);

    //  return default(Bitmap);
    //}
    //
    // Summary:
    //     Creates a copy of the section of this System.Drawing.Bitmap defined with
    //     a specified System.Drawing.Imaging.PixelFormat enumeration.
    //
    // Parameters:
    //   rect:
    //     Defines the portion of this System.Drawing.Bitmap to copy.
    //
    //   format:
    //     Specifies the System.Drawing.Imaging.PixelFormat enumeration for the destination
    //     System.Drawing.Bitmap.
    //
    // Returns:
    //     The System.Drawing.Bitmap that this method creates.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     rect is outside of the source bitmap bounds.
    //
    //   System.ArgumentException:
    //     The height or width of rect is 0.
    //public Bitmap Clone(RectangleF rect, PixelFormat format);
    //
    // Summary:
    //     Creates a System.Drawing.Bitmap from a Windows handle to an icon.
    //
    // Parameters:
    //   hicon:
    //     A handle to an icon.
    //
    // Returns:
    //     The System.Drawing.Bitmap that this method creates.
    public static Bitmap FromHicon(IntPtr hicon)
    {
      Contract.Ensures(Contract.Result<Bitmap>() != null);

      return default(Bitmap);
    }
    //
    // Summary:
    //     Creates a System.Drawing.Bitmap from the specified Windows resource.
    //
    // Parameters:
    //   hinstance:
    //     A handle to an instance of the executable file that contains the resource.
    //
    //   bitmapName:
    //     A string containing the name of the resource bitmap.
    //
    // Returns:
    //     The System.Drawing.Bitmap that this method creates.
    public static Bitmap FromResource(IntPtr hinstance, string bitmapName)
    {
      Contract.Ensures(Contract.Result<Bitmap>() != null);

      return default(Bitmap);
    }
    //
    // Summary:
    //     Creates a GDI bitmap object from this System.Drawing.Bitmap.
    //
    // Returns:
    //     A handle to the GDI bitmap object that this method creates.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The height or width of the bitmap is greater than System.Int16.MaxValue.
    //
    //   System.Exception:
    //     The operation failed.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public IntPtr GetHbitmap();
    //
    // Summary:
    //     Creates a GDI bitmap object from this System.Drawing.Bitmap.
    //
    // Parameters:
    //   background:
    //     A System.Drawing.Color structure that specifies the background color. This
    //     parameter is ignored if the bitmap is totally opaque.
    //
    // Returns:
    //     A handle to the GDI bitmap object that this method creates.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The height or width of the bitmap is greater than System.Int16.MaxValue.
    //
    //   System.Exception:
    //     The operation failed.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public IntPtr GetHbitmap(Color background);
    //
    // Summary:
    //     Returns the handle to an icon.
    //
    // Returns:
    //     A Windows handle to an icon with the same image as the System.Drawing.Bitmap.
    //
    // Exceptions:
    //   System.Exception:
    //     The operation failed.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public IntPtr GetHicon();
    //
    // Summary:
    //     Gets the color of the specified pixel in this System.Drawing.Bitmap.
    //
    // Parameters:
    //   x:
    //     The x-coordinate of the pixel to retrieve.
    //
    //   y:
    //     The y-coordinate of the pixel to retrieve.
    //
    // Returns:
    //     A System.Drawing.Color structure that represents the color of the specified
    //     pixel.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     x is less than 0, or greater than or equal to System.Drawing.Image.Width.
    //     ory is less than 0, or greater than or equal to System.Drawing.Image.Height
    //
    //   System.Exception:
    //     The operation failed.
    public Color GetPixel(int x, int y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y>= 0);

      return default(Color);

    }
    //
    // Summary:
    //     Locks a System.Drawing.Bitmap into system memory.
    //
    // Parameters:
    //   rect:
    //     A System.Drawing.Rectangle structure specifying the portion of the System.Drawing.Bitmap
    //     to lock.
    //
    //   flags:
    //     An System.Drawing.Imaging.ImageLockMode enumeration specifying the access
    //     level (read/write) for the System.Drawing.Bitmap.
    //
    //   format:
    //     A System.Drawing.Imaging.PixelFormat enumeration specifying the data format
    //     of this System.Drawing.Bitmap.
    //
    // Returns:
    //     A System.Drawing.Imaging.BitmapData containing information about this lock
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Drawing.Imaging.PixelFormat is not a specific bits-per-pixel value.-or-The
    //     incorrect System.Drawing.Imaging.PixelFormat is passed in for a bitmap.
    //
    //   System.Exception:
    //     The operation failed.
    public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
    {
      Contract.Ensures(Contract.Result<BitmapData>() != null);
      return default(BitmapData);
    }
    //
    // Summary:
    //     Locks a System.Drawing.Bitmap into system memory
    //
    // Parameters:
    //   rect:
    //     A rectangle structure specifying the portion of the System.Drawing.Bitmap
    //     to lock.
    //
    //   flags:
    //     One of the System.Drawing.Imaging.ImageLockMode values specifying the access
    //     level (read/write) for the System.Drawing.Bitmap.
    //
    //   format:
    //     One of the System.Drawing.Imaging.PixelFormat values indicating the data
    //     format of the System.Drawing.Bitmap.
    //
    //   bitmapData:
    //     A System.Drawing.Imaging.BitmapData containing information about the lock
    //     operation.
    //
    // Returns:
    //     A System.Drawing.Imaging.BitmapData containing information about the lock
    //     operation.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     System.Drawing.Imaging.PixelFormat value is not a specific bits-per-pixel
    //     value.-or-The incorrect System.Drawing.Imaging.PixelFormat is passed in for
    //     a bitmap.
    //
    //   System.Exception:
    //     The operation failed.
    public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format, BitmapData bitmapData) {
      Contract.Ensures(Contract.Result<BitmapData>() != null);
      return default(BitmapData);
    }

    //
    // Summary:
    //     Makes the default transparent color transparent for this System.Drawing.Bitmap.
    //
    // Returns:
    //     This method does not return a value.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The image format of the System.Drawing.Bitmap is an icon format.
    //
    //   System.Exception:
    //     The operation failed.
    //public void MakeTransparent();
    //
    // Summary:
    //     Makes the specified color transparent for this System.Drawing.Bitmap.
    //
    // Parameters:
    //   transparentColor:
    //     The System.Drawing.Color structure that represents the color to make transparent.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The image format of the System.Drawing.Bitmap is an icon format.
    //
    //   System.Exception:
    //     The operation failed.
    //public void MakeTransparent(Color transparentColor);
    //
    // Summary:
    //     Sets the color of the specified pixel in this System.Drawing.Bitmap.
    //
    // Parameters:
    //   x:
    //     The x-coordinate of the pixel to set.
    //
    //   y:
    //     The y-coordinate of the pixel to set.
    //
    //   color:
    //     A System.Drawing.Color structure that represents the color to assign to the
    //     specified pixel.
    //
    // Returns:
    //     This method does not return a value.
    //
    // Exceptions:
    //   System.Exception:
    //     The operation failed.
    //public void SetPixel(int x, int y, Color color);
    //
    // Summary:
    //     Sets the resolution for this System.Drawing.Bitmap.
    //
    // Parameters:
    //   xDpi:
    //     The horizontal resolution, in dots per inch, of the System.Drawing.Bitmap.
    //
    //   yDpi:
    //     The vertical resolution, in dots per inch, of the System.Drawing.Bitmap.
    //
    // Exceptions:
    //   System.Exception:
    //     The operation failed.
    //public void SetResolution(float xDpi, float yDpi);
    //
    // Summary:
    //     Unlocks this System.Drawing.Bitmap from system memory.
    //
    // Parameters:
    //   bitmapdata:
    //     A System.Drawing.Imaging.BitmapData specifying information about the lock
    //     operation.
    //
    // Exceptions:
    //   System.Exception:
    //     The operation failed.
    //public void UnlockBits(BitmapData bitmapdata);
  }
}
