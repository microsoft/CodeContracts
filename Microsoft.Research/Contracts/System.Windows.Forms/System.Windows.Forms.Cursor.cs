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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents the image used to paint the mouse pointer.
    /// </summary>
    public sealed class Cursor // : IDisposable, ISerializable
    {
        // <summary>
        // Gets or sets the bounds that represent the clipping rectangle for the cursor.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Drawing.Rectangle"/> that represents the clipping rectangle for the <see cref="T:System.Windows.Forms.Cursor"/>, in screen coordinates.
        // </returns>
        // public static Rectangle Clip { get; set; }
        
        // <summary>
        // Gets or sets a cursor object that represents the mouse cursor.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.Cursor"/> that represents the mouse cursor. The default is null if the mouse cursor is not visible.
        // </returns>
        // 
        // public static Cursor Current {get; set;}
        
        // <summary>
        // Gets the handle of the cursor.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.IntPtr"/> that represents the cursor's handle.
        // </returns>
        // <exception cref="T:System.Exception">The handle value is <see cref="F:System.IntPtr.Zero"/>. </exception>
        // public IntPtr Handle {get;}
        
        // <summary>
        // Gets the cursor hot spot.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Drawing.Point"/> representing the cursor hot spot.
        // </returns>
        // public Point HotSpot { get; }
        
        // <summary>
        // Gets or sets the cursor's position.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Drawing.Point"/> that represents the cursor's position in screen coordinates.
        // </returns>
        // public static Point Position {get; set;}
        
        // <summary>
        // Gets the size of the cursor object.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that represents the width and height of the <see cref="T:System.Windows.Forms.Cursor"/>.
        // </returns>
        // public Size Size {get;}
        
        // <summary>
        // Gets or sets the object that contains data about the <see cref="T:System.Windows.Forms.Cursor"/>.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Object"/> that contains data about the <see cref="T:System.Windows.Forms.Cursor"/>. The default is null.
        // </returns>
        // public object Tag {get; set;}
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor"/> class from the specified Windows handle.
        /// </summary>
        /// <param name="handle">An <see cref="T:System.IntPtr"/> that represents the Windows handle of the cursor to create. </param><exception cref="T:System.ArgumentException"><paramref name="handle"/> is <see cref="F:System.IntPtr.Zero"/>. </exception>
        public Cursor(IntPtr handle)
        {
            Contract.Requires(handle != IntPtr.Zero);
        }

        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor"/> class from the specified file.
        // </summary>
        // <param name="fileName">The cursor file to load. </param>
        // public Cursor(string fileName)
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor"/> class from the specified resource with the specified resource type.
        // </summary>
        // <param name="type">The resource <see cref="T:System.Type"/>. </param><param name="resource">The name of the resource. </param>
        // public Cursor(System.Type type, string resource)
        //  : this(type.Module.Assembly.GetManifestResourceStream(type, resource))
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor"/> class from the specified data stream.
        /// </summary>
        /// <param name="stream">The data stream to load the <see cref="T:System.Windows.Forms.Cursor"/> from. </param>
        public Cursor(Stream stream)
        {
            Contract.Requires(stream != null);
        }
        
        // <summary>
        // Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.Cursor"/> class are equal.
        // </summary>
        // 
        // <returns>
        // true if two instances of the <see cref="T:System.Windows.Forms.Cursor"/> class are equal; otherwise, false.
        // </returns>
        // <param name="left">A <see cref="T:System.Windows.Forms.Cursor"/> to compare. </param><param name="right">A <see cref="T:System.Windows.Forms.Cursor"/> to compare. </param><filterpriority>3</filterpriority>
        // public static bool operator ==(Cursor left, Cursor right)
        
        // <summary>
        // Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.Cursor"/> class are not equal.
        // </summary>
        // 
        // <returns>
        // true if two instances of the <see cref="T:System.Windows.Forms.Cursor"/> class are not equal; otherwise, false.
        // </returns>
        // <param name="left">A <see cref="T:System.Windows.Forms.Cursor"/> to compare. </param><param name="right">A <see cref="T:System.Windows.Forms.Cursor"/> to compare. </param><filterpriority>3</filterpriority>
        // public static bool operator !=(Cursor left, Cursor right)
        
        // <summary>
        // Copies the handle of this <see cref="T:System.Windows.Forms.Cursor"/>.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.IntPtr"/> that represents the cursor's handle.
        // </returns>
        // public IntPtr CopyHandle()
        
        
        // <summary>
        // Releases all resources used by the <see cref="T:System.Windows.Forms.Cursor"/>.
        // </summary>
        // public void Dispose()
        
        /// <summary>
        /// Draws the cursor on the specified surface, within the specified bounds.
        /// </summary>
        /// <param name="g">The <see cref="T:System.Drawing.Graphics"/> surface on which to draw the <see cref="T:System.Windows.Forms.Cursor"/>. </param><param name="targetRect">The <see cref="T:System.Drawing.Rectangle"/> that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor"/>. </param>
        public void Draw(Graphics g, Rectangle targetRect)
        {
            Contract.Requires(g != null);
        }

        /// <summary>
        /// Draws the cursor in a stretched format on the specified surface, within the specified bounds.
        /// </summary>
        /// <param name="g">The <see cref="T:System.Drawing.Graphics"/> surface on which to draw the <see cref="T:System.Windows.Forms.Cursor"/>. </param><param name="targetRect">The <see cref="T:System.Drawing.Rectangle"/> that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor"/>. </param>
        public void DrawStretched(Graphics g, Rectangle targetRect)
        {
            Contract.Requires(g != null);
        }
        
        // <summary>
        // Hides the cursor.
        // </summary>
        // 
        // public static void Hide()
        
        // <summary>
        // Displays the cursor.
        // </summary>
        // 
        // public static void Show()
       
        // <summary>
        // Retrieves a human readable string representing this <see cref="T:System.Windows.Forms.Cursor"/>.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.String"/> that represents this <see cref="T:System.Windows.Forms.Cursor"/>.
        // </returns>
        // 
        // public override string ToString()
        
        // <summary>
        // Retrieves the hash code for the current <see cref="T:System.Windows.Forms.Cursor"/>.
        // </summary>
        // 
        // <returns>
        // A hash code for the current <see cref="T:System.Windows.Forms.Cursor"/>.
        // </returns>
        // 
        // public override int GetHashCode()
        
        // <summary>
        // Returns a value indicating whether this cursor is equal to the specified <see cref="T:System.Windows.Forms.Cursor"/>.
        // </summary>
        // 
        // <returns>
        // true if this cursor is equal to the specified <see cref="T:System.Windows.Forms.Cursor"/>; otherwise, false.
        // </returns>
        // <param name="obj">The <see cref="T:System.Windows.Forms.Cursor"/> to compare. </param>
        // public override bool Equals(object obj)
    }
}
