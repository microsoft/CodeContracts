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
using System.IO;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents a collection of System.Windows.Forms.Form objects.
    public class Cursor
    {
        // <summary>
        // Gets or sets the bounds that represent the clipping rectangle for the cursor.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Drawing.Rectangle"/> that represents the clipping rectangle for the <see cref="T:System.Windows.Forms.Cursor"/>, in screen coordinates.
        // </returns>
        // <filterpriority>1</filterpriority>
        //public static Rectangle Clip { get; set; }

        // <summary>
        // Gets or sets a cursor object that represents the mouse cursor.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.Cursor"/> that represents the mouse cursor. The default is null if the mouse cursor is not visible.
        // </returns>
        // <filterpriority>1</filterpriority>
        //public static Cursor Current {get; set;}

        // <summary>
        // Gets the handle of the cursor.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.IntPtr"/> that represents the cursor's handle.
        // </returns>
        // <exception cref="T:System.Exception">The handle value is <see cref="F:System.IntPtr.Zero"/>. </exception><filterpriority>1</filterpriority>
        // public IntPtr Handle {get; set; }

        // <summary>
        // Gets the cursor hot spot.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Drawing.Point"/> representing the cursor hot spot.
        // </returns>
        // public Point HotSpot {get;}

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
        // <filterpriority>1</filterpriority>
        // public Size Size {get;}

        // <summary>
        // Gets or sets the object that contains data about the <see cref="T:System.Windows.Forms.Cursor"/>.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Object"/> that contains data about the <see cref="T:System.Windows.Forms.Cursor"/>. The default is null.
        // </returns>
        // <filterpriority>1</filterpriority>
        // public object Tag {get; set;}


        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor"/> class from the specified Windows handle.
        // </summary>
        // <param name="handle">An <see cref="T:System.IntPtr"/> that represents the Windows handle of the cursor to create. </param><exception cref="T:System.ArgumentException"><paramref name="handle"/> is <see cref="F:System.IntPtr.Zero"/>. </exception>
        //public Cursor(IntPtr handle)

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor"/> class from the specified file.
        /// </summary>
        /// <param name="fileName">The cursor file to load. </param>
        public Cursor(string fileName)
        {
            // This opens a stream, which will thrown an ArgumentNullException if FileName is null or empty.
            Contract.Requires(!String.IsNullOrEmpty(fileName));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor"/> class from the specified resource with the specified resource type.
        /// </summary>
        /// <param name="type">The resource <see cref="T:System.Type"/>. </param><param name="resource">The name of the resource. </param>
        public Cursor(System.Type type, string resource)
        {
            Contract.Requires(type != null || (type == null && resource != null));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor"/> class from the specified data stream.
        /// </summary>
        /// <param name="stream">The data stream to load the <see cref="T:System.Windows.Forms.Cursor"/> from. </param>
        public Cursor(Stream stream)
        {
            Contract.Requires(stream != null);
        }

        /// <summary>
        /// Draws the cursor on the specified surface, within the specified bounds.
        /// </summary>
        /// <param name="g">The <see cref="T:System.Drawing.Graphics"/> surface on which to draw the <see cref="T:System.Windows.Forms.Cursor"/>. </param><param name="targetRect">The <see cref="T:System.Drawing.Rectangle"/> that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor"/>. </param><filterpriority>1</filterpriority>
        public void Draw(Graphics g, Rectangle targetRect)
        {
            Contract.Requires(g != null);
        }

        /// <summary>
        /// Draws the cursor in a stretched format on the specified surface, within the specified bounds.
        /// </summary>
        /// <param name="g">The <see cref="T:System.Drawing.Graphics"/> surface on which to draw the <see cref="T:System.Windows.Forms.Cursor"/>. </param><param name="targetRect">The <see cref="T:System.Drawing.Rectangle"/> that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor"/>. </param><filterpriority>1</filterpriority>
        public void DrawStretched(Graphics g, Rectangle targetRect)
        {
            Contract.Requires(g != null);
        }

        // Other public methods elided for now.
    }
}