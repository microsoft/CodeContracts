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

#region Assembly System.Drawing.dll, v4.0.30319
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Drawing.dll
#endregion

using System;
using System.ComponentModel;
//using System.Drawing.Design;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;

namespace System.Drawing {
  // Summary:
  //     Represents a Windows icon, which is a small bitmap image that is used to
  //     represent an object. Icons can be thought of as transparent bitmaps, although
  //     their size is determined by the system.
  [Serializable]
  //[Editor("System.Drawing.Design.IconEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
  //[TypeConverter(typeof(IconConverter))]
  public sealed class Icon : MarshalByRefObject/*, ISerializable,*/ /*ICloneable,*/ /*IDisposable*/ {
    // Summary:
    //     Initializes a new instance of the System.Drawing.Icon class from the specified
    //     data stream.
    //
    // Parameters:
    //   stream:
    //     The data stream from which to load the System.Drawing.Icon.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The stream parameter is null.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public Icon(Stream stream);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Icon class from the specified
    //     file name.
    //
    // Parameters:
    //   fileName:
    //     The file to load the System.Drawing.Icon from.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public Icon(string fileName) { }
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Icon class and attempts
    //     to find a version of the icon that matches the requested size.
    //
    // Parameters:
    //   original:
    //     The System.Drawing.Icon from which to load the newly sized icon.
    //
    //   size:
    //     A System.Drawing.Size structure that specifies the height and width of the
    //     new System.Drawing.Icon.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The original parameter is null.
    //public Icon(Icon original, Size size);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Icon class of the specified
    //     size from the specified stream.
    //
    // Parameters:
    //   stream:
    //     The stream that contains the icon data.
    //
    //   size:
    //     The desired size of the icon.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The stream is null or does not contain image data.
    //public Icon(Stream stream, Size size);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Icon class of the specified
    //     size from the specified file.
    //
    // Parameters:
    //   fileName:
    //     The name and path to the file that contains the icon data.
    //
    //   size:
    //     The desired size of the icon.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The string is null or does not contain image data.
    //public Icon(string fileName, Size size);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Icon class from a resource
    //     in the specified assembly.
    //
    // Parameters:
    //   type:
    //     A System.Type that specifies the assembly in which to look for the resource.
    //
    //   resource:
    //     The resource name to load.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An icon specified by resource cannot be found in the assembly that contains
    //     the specified type.
    //public Icon(Type type, string resource);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Icon class and attempts
    //     to find a version of the icon that matches the requested size.
    //
    // Parameters:
    //   original:
    //     The icon to load the different size from.
    //
    //   width:
    //     The width of the new icon.
    //
    //   height:
    //     The height of the new icon.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The original parameter is null.
    //public Icon(Icon original, int width, int height);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Icon class from the specified
    //     data stream and with the specified width and height.
    //
    // Parameters:
    //   stream:
    //     The data stream from which to load the icon.
    //
    //   width:
    //     The width, in pixels, of the icon.
    //
    //   height:
    //     The height, in pixels, of the icon.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The stream parameter is null.
    //public Icon(Stream stream, int width, int height);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Icon class with the specified
    //     width and height from the specified file.
    //
    // Parameters:
    //   fileName:
    //     The name and path to the file that contains the System.Drawing.Icon data.
    //
    //   width:
    //     The desired width of the System.Drawing.Icon.
    //
    //   height:
    //     The desired height of the System.Drawing.Icon.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The string is null or does not contain image data.
    //public Icon(string fileName, int width, int height);

    // Summary:
    //     Gets the Windows handle for this System.Drawing.Icon. This is not a copy
    //     of the handle; do not free it.
    //
    // Returns:
    //     The Windows handle for the icon.
    //[Browsable(false)]
    //public IntPtr Handle { get; }
    //
    // Summary:
    //     Gets the height of this System.Drawing.Icon.
    //
    // Returns:
    //     The height of this System.Drawing.Icon.
    //[Browsable(false)]
    //public int Height { get; }
    //
    // Summary:
    //     Gets the size of this System.Drawing.Icon.
    //
    // Returns:
    //     A System.Drawing.Size structure that specifies the width and height of this
    //     System.Drawing.Icon.
    //public Size Size { get; }
    //
    // Summary:
    //     Gets the width of this System.Drawing.Icon.
    //
    // Returns:
    //     The width of this System.Drawing.Icon.
    //[Browsable(false)]
    //public int Width { get; }

    // Summary:
    //     Clones the System.Drawing.Icon, creating a duplicate image.
    //
    // Returns:
    //     An object that can be cast to an System.Drawing.Icon.
    //public object Clone();
    //
    // Summary:
    //     Releases all resources used by this System.Drawing.Icon.
    //public void Dispose();
    //
    // Summary:
    //     Returns an icon representation of an image that is contained in the specified
    //     file.
    //
    // Parameters:
    //   filePath:
    //     The path to the file that contains an image.
    //
    // Returns:
    //     The System.Drawing.Icon representation of the image that is contained in
    //     the specified file.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The filePath does not indicate a valid file.-or-The filePath indicates a
    //     Universal Naming Convention (UNC) path.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public static Icon ExtractAssociatedIcon(string filePath);
    //
    // Summary:
    //     Creates a GDI+ System.Drawing.Icon from the specified Windows handle to an
    //     icon (HICON).
    //
    // Parameters:
    //   handle:
    //     A Windows handle to an icon.
    //
    // Returns:
    //     The System.Drawing.Icon this method creates.
    //public static Icon FromHandle(IntPtr handle);
    //
    // Summary:
    //     Saves this System.Drawing.Icon to the specified output System.IO.Stream.
    //
    // Parameters:
    //   outputStream:
    //     The System.IO.Stream to save to.
    //public void Save(Stream outputStream);
    //
    // Summary:
    //     Converts this System.Drawing.Icon to a GDI+ System.Drawing.Bitmap.
    //
    // Returns:
    //     A System.Drawing.Bitmap that represents the converted System.Drawing.Icon.
    //public Bitmap ToBitmap();
    //
    // Summary:
    //     Gets a human-readable string that describes the System.Drawing.Icon.
    //
    // Returns:
    //     A string that describes the System.Drawing.Icon.
    //public override string ToString();
  }
}
