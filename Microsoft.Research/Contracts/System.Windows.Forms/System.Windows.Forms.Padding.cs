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
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents padding or margin information associated with a user interface (UI) element.
    /// </summary>
    [Serializable]
    public struct Padding
    {
        // <summary>
        // Provides a <see cref="T:System.Windows.Forms.Padding"/> object with no padding.
        // </summary>
        // public static readonly Padding Empty = new Padding(0);

        // <summary>
        // Gets or sets the padding value for all the edges.
        // </summary>
        // <returns>
        // The padding, in pixels, for all edges if the same; otherwise, -1.
        // </returns>
        // public int All {get; set;}
       
        // <summary>
        // Gets or sets the padding value for the bottom edge.
        // </summary>
        // <returns>
        // The padding, in pixels, for the bottom edge.
        // </returns>
        // public int Bottom { get; set; }
        
        // <summary>
        // Gets or sets the padding value for the left edge.
        // </summary>
        // <returns>
        // The padding, in pixels, for the left edge.
        // </returns>
        // public int Left {get; set;}
       
        // <summary>
        // Gets or sets the padding value for the right edge.
        // </summary>
        // <returns>
        // The padding, in pixels, for the right edge.
        // </returns>
        // public int Right {get; set;}
        
        // <summary>
        // Gets or sets the padding value for the top edge.
        // </summary>
        // <returns>
        // The padding, in pixels, for the top edge.
        // </returns>
        // public int Top {get; set;}
        
        // <summary>
        // Gets the combined padding for the right and left edges.
        // </summary>
        // <returns>
        // Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Left"/> and <see cref="P:System.Windows.Forms.Padding.Right"/> padding values.
        // </returns>
        // public int Horizontal { get; }
       
        // <summary>
        // Gets the combined padding for the top and bottom edges.
        // </summary>
        // <returns>
        // Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Top"/> and <see cref="P:System.Windows.Forms.Padding.Bottom"/> padding values.
        // </returns>
        // public int Vertical {get;}
        
        // <summary>
        // Gets the padding information in the form of a <see cref="T:System.Drawing.Size"/>.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> containing the padding information.
        // </returns>
        // public Size Size {get;}
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding"/> class using the supplied padding size for all edges.
        // </summary>
        // <param name="all">The number of pixels to be used for padding for all edges.</param>
        // public Padding(int all)
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding"/> class using a separate padding size for each edge.
        // </summary>
        // <param name="left">The padding size, in pixels, for the left edge.</param><param name="top">The padding size, in pixels, for the top edge.</param><param name="right">The padding size, in pixels, for the right edge.</param><param name="bottom">The padding size, in pixels, for the bottom edge.</param>
        // public Padding(int left, int top, int right, int bottom)
        
        // <summary>
        // Performs vector addition on the two specified <see cref="T:System.Windows.Forms.Padding"/> objects, resulting in a new <see cref="T:System.Windows.Forms.Padding"/>.
        // </summary>
        // <returns>
        // A new <see cref="T:System.Windows.Forms.Padding"/> that results from adding <paramref name="p1"/> and <paramref name="p2"/>.
        // </returns>
        // <param name="p1">The first <see cref="T:System.Windows.Forms.Padding"/> to add.</param><param name="p2">The second <see cref="T:System.Windows.Forms.Padding"/> to add.</param><filterpriority>3</filterpriority>
        // public static Padding operator +(Padding p1, Padding p2)
        
        // <summary>
        // Performs vector subtraction on the two specified <see cref="T:System.Windows.Forms.Padding"/> objects, resulting in a new <see cref="T:System.Windows.Forms.Padding"/>.
        // </summary>
        // <returns>
        // The <see cref="T:System.Windows.Forms.Padding"/> result of subtracting <paramref name="p2"/> from <paramref name="p1"/>.
        // </returns>
        // <param name="p1">The <see cref="T:System.Windows.Forms.Padding"/> to subtract from (the minuend).</param><param name="p2">The <see cref="T:System.Windows.Forms.Padding"/> to subtract from (the subtrahend).</param><filterpriority>3</filterpriority>
        // public static Padding operator -(Padding p1, Padding p2)
        
        // <summary>
        // Tests whether two specified <see cref="T:System.Windows.Forms.Padding"/> objects are equivalent.
        // </summary>
        // <returns>
        // true if the two <see cref="T:System.Windows.Forms.Padding"/> objects are equal; otherwise, false.
        // </returns>
        // <param name="p1">A <see cref="T:System.Windows.Forms.Padding"/> to test.</param><param name="p2">A <see cref="T:System.Windows.Forms.Padding"/> to test.</param><filterpriority>3</filterpriority>
        // public static bool operator ==(Padding p1, Padding p2)
        
        // <summary>
        // Tests whether two specified <see cref="T:System.Windows.Forms.Padding"/> objects are not equivalent.
        // </summary>
        // <returns>
        // true if the two <see cref="T:System.Windows.Forms.Padding"/> objects are different; otherwise, false.
        // </returns>
        // <param name="p1">A <see cref="T:System.Windows.Forms.Padding"/> to test.</param><param name="p2">A <see cref="T:System.Windows.Forms.Padding"/> to test.</param><filterpriority>3</filterpriority>
        // public static bool operator !=(Padding p1, Padding p2)
        
        // <summary>
        // Computes the sum of the two specified <see cref="T:System.Windows.Forms.Padding"/> values.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.Padding"/> that contains the sum of the two specified <see cref="T:System.Windows.Forms.Padding"/> values.
        // </returns>
        // <param name="p1">A <see cref="T:System.Windows.Forms.Padding"/>.</param><param name="p2">A <see cref="T:System.Windows.Forms.Padding"/>.</param>
        // public static Padding Add(Padding p1, Padding p2)
        
        // <summary>
        // Subtracts one specified <see cref="T:System.Windows.Forms.Padding"/> value from another.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.Padding"/> that contains the result of the subtraction of one specified <see cref="T:System.Windows.Forms.Padding"/> value from another.
        // </returns>
        // <param name="p1">A <see cref="T:System.Windows.Forms.Padding"/>.</param><param name="p2">A <see cref="T:System.Windows.Forms.Padding"/>.</param>
        // public static Padding Subtract(Padding p1, Padding p2)
        
        // <summary>
        // Determines whether the value of the specified object is equivalent to the current <see cref="T:System.Windows.Forms.Padding"/>.
        // </summary>
        // <returns>
        // true if the <see cref="T:System.Windows.Forms.Padding"/> objects are equivalent; otherwise, false.
        // </returns>
        // <param name="other">The object to compare to the current <see cref="T:System.Windows.Forms.Padding"/>.</param>
        // public override bool Equals(object other)
        
        // <summary>
        // Generates a hash code for the current <see cref="T:System.Windows.Forms.Padding"/>.
        // </summary>
        // <returns>
        // A 32-bit signed integer hash code.
        // </returns>
        // public override int GetHashCode()
        
        // <summary>
        // Returns a string that represents the current <see cref="T:System.Windows.Forms.Padding"/>.
        // </summary>
        // <returns>
        // A <see cref="T:System.String"/> that represents the current <see cref="T:System.Windows.Forms.Padding"/>.
        // </returns>
        // public override string ToString()
    }
}
