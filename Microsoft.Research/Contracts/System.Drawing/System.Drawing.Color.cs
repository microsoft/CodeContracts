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


namespace System.Drawing
{
  // Summary:
  //     Represents an ARGB (alpha, red, green, blue) color.
  //[Serializable]
  //[TypeConverter(typeof(ColorConverter))]
  //[Editor("System.Drawing.Design.ColorEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
  //[DebuggerDisplay("{NameAndARGBValue}")]
  public struct Color
  {
    // Summary:
    //     Represents a color that is null.
    //public static readonly Color Empty;

    // Summary:
    //     Tests whether two specified System.Drawing.Color structures are different.
    //
    // Parameters:
    //   left:
    //     The System.Drawing.Color that is to the left of the inequality operator.
    //
    //   right:
    //     The System.Drawing.Color that is to the right of the inequality operator.
    //
    // Returns:
    //     true if the two System.Drawing.Color structures are different; otherwise,
    //     false.
    //public static bool operator !=(Color left, Color right);
    //
    // Summary:
    //     Tests whether two specified System.Drawing.Color structures are equivalent.
    //
    // Parameters:
    //   left:
    //     The System.Drawing.Color that is to the left of the equality operator.
    //
    //   right:
    //     The System.Drawing.Color that is to the right of the equality operator.
    //
    // Returns:
    //     true if the two System.Drawing.Color structures are equal; otherwise, false.
    //public static bool operator ==(Color left, Color right);

    // Summary:
    //     Gets the alpha component value of this System.Drawing.Color structure.
    //
    // Returns:
    //     The alpha component value of this System.Drawing.Color.
    //public byte A { get; }
    //
    // Summary:
    //     Gets a system-defined color.
    //
    // Returns:
    //     A System.Drawing.Color representing a system-defined color.
    //public static Color AliceBlue { get; }
    //
    // Summary:
    //     Gets a system-defined color.
    //
    // Returns:
    //     A System.Drawing.Color representing a system-defined color.
    //public static Color AntiqueWhite { get; }
    //
    // Summary:
    //     Gets a system-defined color.
    //
    // Returns:
    //     A System.Drawing.Color representing a system-defined color.
    //public static Color Aqua { get; }
    //
    // Summary:
    //     Gets a system-defined color.
    //
    // Returns:
    //     A System.Drawing.Color representing a system-defined color.
    //public static Color Aquamarine { get; }
    //
    // Summary:
    //     Gets a system-defined color.
    //
    // Returns:
    //     A System.Drawing.Color representing a system-defined color.
    //public static Color Azure { get; }
    //
    // Summary:
    //     Gets the blue component value of this System.Drawing.Color structure.
    //
    // Returns:
    //     The blue component value of this System.Drawing.Color.
    //public byte B { get; }
    //
    // Summary:
    //     Gets a system-defined color.
    //
    // Returns:
    //     A System.Drawing.Color representing a system-defined color.
    //public static Color Beige { get; }
    //
    // Summary:
    //     Gets a system-defined color.
    //
    // Returns:
    //     A System.Drawing.Color representing a system-defined color.
    //public static Color Bisque { get; }
    //
    // Summary:
    //     Gets a system-defined color.
    //
    // Returns:
    //     A System.Drawing.Color representing a system-defined color.
    //public static Color Black { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color BlanchedAlmond { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Blue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color BlueViolet { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Brown { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color BurlyWood { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color CadetBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Chartreuse { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Chocolate { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Coral { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color CornflowerBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Cornsilk { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Crimson { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Cyan { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkCyan { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkGoldenrod { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkGray { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkKhaki { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkMagenta { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkOliveGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkOrange { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkOrchid { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkRed { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkSalmon { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkSeaGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkSlateBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkSlateGray { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkTurquoise { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DarkViolet { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DeepPink { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DeepSkyBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DimGray { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color DodgerBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Firebrick { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color FloralWhite { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color ForestGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Fuchsia { get; }
    ////
    //// Summary:
    ////     Gets the green component value of this System.Drawing.Color structure.
    ////
    //// Returns:
    ////     The green component value of this System.Drawing.Color.
    //public byte G { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Gainsboro { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color GhostWhite { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Gold { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Goldenrod { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color strcture representing a system-defined color.
    //public static Color Gray { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Green { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color GreenYellow { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Honeydew { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color HotPink { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color IndianRed { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Indigo { get; }
    ////
    //// Summary:
    ////     Specifies whether this System.Drawing.Color structure is uninitialized.
    ////
    //// Returns:
    ////     This property returns true if this color is uninitialized; otherwise, false.
    //public bool IsEmpty { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether this System.Drawing.Color structure is a
    ////     predefined color. Predefined colors are represented by the elements of the
    ////     System.Drawing.KnownColor enumeration.
    ////
    //// Returns:
    ////     true if this System.Drawing.Color was created from a predefined color by
    ////     using either the System.Drawing.Color.FromName(System.String) method or the
    ////     System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor) method; otherwise,
    ////     false.
    //public bool IsKnownColor { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether this System.Drawing.Color structure is a
    ////     named color or a member of the System.Drawing.KnownColor enumeration.
    ////
    //// Returns:
    ////     true if this System.Drawing.Color was created by using either the System.Drawing.Color.FromName(System.String)
    ////     method or the System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)
    ////     method; otherwise, false.
    //public bool IsNamedColor { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether this System.Drawing.Color structure is a
    ////     system color. A system color is a color that is used in a Windows display
    ////     element. System colors are represented by elements of the System.Drawing.KnownColor
    ////     enumeration.
    ////
    //// Returns:
    ////     true if this System.Drawing.Color was created from a system color by using
    ////     either the System.Drawing.Color.FromName(System.String) method or the System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)
    ////     method; otherwise, false.
    //public bool IsSystemColor { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Ivory { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Khaki { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Lavender { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LavenderBlush { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LawnGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LemonChiffon { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightCoral { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightCyan { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightGoldenrodYellow { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightGray { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightPink { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightSalmon { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightSeaGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightSkyBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightSlateGray { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightSteelBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LightYellow { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Lime { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color LimeGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Linen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Magenta { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Maroon { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MediumAquamarine { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MediumBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MediumOrchid { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MediumPurple { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MediumSeaGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MediumSlateBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MediumSpringGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MediumTurquoise { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MediumVioletRed { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MidnightBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MintCream { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color MistyRose { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Moccasin { get; }
    ////
    //// Summary:
    ////     Gets the name of this System.Drawing.Color.
    ////
    //// Returns:
    ////     The name of this System.Drawing.Color.
    //public string Name { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color NavajoWhite { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Navy { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color OldLace { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Olive { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color OliveDrab { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Orange { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color OrangeRed { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Orchid { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color PaleGoldenrod { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color PaleGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color PaleTurquoise { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color PaleVioletRed { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color PapayaWhip { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color PeachPuff { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Peru { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Pink { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Plum { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color PowderBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Purple { get; }
    ////
    //// Summary:
    ////     Gets the red component value of this System.Drawing.Color structure.
    ////
    //// Returns:
    ////     The red component value of this System.Drawing.Color.
    //public byte R { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Red { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color RosyBrown { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color RoyalBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color SaddleBrown { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Salmon { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color SandyBrown { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color SeaGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color SeaShell { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Sienna { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Silver { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color SkyBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color SlateBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color SlateGray { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Snow { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color SpringGreen { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color SteelBlue { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Tan { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Teal { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Thistle { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Tomato { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Transparent { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Turquoise { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Violet { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Wheat { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color White { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color WhiteSmoke { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color Yellow { get; }
    ////
    //// Summary:
    ////     Gets a system-defined color.
    ////
    //// Returns:
    ////     A System.Drawing.Color representing a system-defined color.
    //public static Color YellowGreen { get; }

    // Summary:
    //     Tests whether the specified object is a System.Drawing.Color structure and
    //     is equivalent to this System.Drawing.Color structure.
    //
    // Parameters:
    //   obj:
    //     The object to test.
    //
    // Returns:
    //     true if obj is a System.Drawing.Color structure equivalent to this System.Drawing.Color
    //     structure; otherwise, false.
    //public override bool Equals(object obj);
    //
    // Summary:
    //     Creates a System.Drawing.Color structure from a 32-bit ARGB value.
    //
    // Parameters:
    //   argb:
    //     A value specifying the 32-bit ARGB value.
    //
    // Returns:
    //     The System.Drawing.Color structure that this method creates.
    //public static Color FromArgb(int argb);
    //
    // Summary:
    //     Creates a System.Drawing.Color structure from the specified System.Drawing.Color
    //     structure, but with the new specified alpha value. Although this method allows
    //     a 32-bit value to be passed for the alpha value, the value is limited to
    //     8 bits.
    //
    // Parameters:
    //   alpha:
    //     The alpha value for the new System.Drawing.Color. Valid values are 0 through
    //     255.
    //
    //   baseColor:
    //     The System.Drawing.Color from which to create the new System.Drawing.Color.
    //
    // Returns:
    //     The System.Drawing.Color that this method creates.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     alpha is less than 0 or greater than 255.
    public static Color FromArgb(int alpha, Color baseColor)
    {
      Contract.Requires(alpha >= 0);
      Contract.Requires(alpha <= 255);

      return default(Color);
    }
    //
    // Summary:
    //     Creates a System.Drawing.Color structure from the specified 8-bit color values
    //     (red, green, and blue). The alpha value is implicitly 255 (fully opaque).
    //     Although this method allows a 32-bit value to be passed for each color component,
    //     the value of each component is limited to 8 bits.
    //
    // Parameters:
    //   red:
    //     The red component value for the new System.Drawing.Color. Valid values are
    //     0 through 255.
    //
    //   green:
    //     The green component value for the new System.Drawing.Color. Valid values
    //     are 0 through 255.
    //
    //   blue:
    //     The blue component value for the new System.Drawing.Color. Valid values are
    //     0 through 255.
    //
    // Returns:
    //     The System.Drawing.Color that this method creates.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     red, green, or blue is less than 0 or greater than 255.
    public static Color FromArgb(int red, int green, int blue)
    {
      Contract.Requires(red >= 0);
      Contract.Requires(red <= 255);

      Contract.Requires(green >= 0);
      Contract.Requires(green <= 255);

      Contract.Requires(blue >= 0);
      Contract.Requires(blue <= 255);

      return default(Color);
    }
    //
    // Summary:
    //     Creates a System.Drawing.Color structure from the four ARGB component (alpha,
    //     red, green, and blue) values. Although this method allows a 32-bit value
    //     to be passed for each component, the value of each component is limited to
    //     8 bits.
    //
    // Parameters:
    //   alpha:
    //     The alpha component. Valid values are 0 through 255.
    //
    //   red:
    //     The red component. Valid values are 0 through 255.
    //
    //   green:
    //     The green component. Valid values are 0 through 255.
    //
    //   blue:
    //     The blue component. Valid values are 0 through 255.
    //
    // Returns:
    //     The System.Drawing.Color that this method creates.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     alpha, red, green, or blue is less than 0 or greater than 255.
    public static Color FromArgb(int alpha, int red, int green, int blue)
    {
      Contract.Requires(alpha >= 0);
      Contract.Requires(alpha <= 255);

      Contract.Requires(red >= 0);
      Contract.Requires(red <= 255);

      Contract.Requires(green >= 0);
      Contract.Requires(green <= 255);

      Contract.Requires(blue >= 0);
      Contract.Requires(blue <= 255);

      return default(Color);
    }

    //
    // Summary:
    //     Creates a System.Drawing.Color structure from the specified predefined color.
    //
    // Parameters:
    //   color:
    //     An element of the System.Drawing.KnownColor enumeration.
    //
    // Returns:
    //     The System.Drawing.Color that this method creates.
    //public static Color FromKnownColor(KnownColor color);
    //
    // Summary:
    //     Creates a System.Drawing.Color structure from the specified name of a predefined
    //     color.
    //
    // Parameters:
    //   name:
    //     A string that is the name of a predefined color. Valid names are the same
    //     as the names of the elements of the System.Drawing.KnownColor enumeration.
    //
    // Returns:
    //     The System.Drawing.Color that this method creates.
    //public static Color FromName(string name);
    //
    // Summary:
    //     Gets the hue-saturation-brightness (HSB) brightness value for this System.Drawing.Color
    //     structure.
    //
    // Returns:
    //     The brightness of this System.Drawing.Color. The brightness ranges from 0.0
    //     through 1.0, where 0.0 represents black and 1.0 represents white.
    public float GetBrightness()
    {
      Contract.Ensures(Contract.Result<float>()>= 0.0 );
      Contract.Ensures(Contract.Result<float>() <= 1.0);

      return default(float);
    }


    //
    // Summary:
    //     Returns a hash code for this System.Drawing.Color structure.
    //
    // Returns:
    //     An integer value that specifies the hash code for this System.Drawing.Color.
    //public override int GetHashCode();
    //
    // Summary:
    //     Gets the hue-saturation-brightness (HSB) hue value, in degrees, for this
    //     System.Drawing.Color structure.
    //
    // Returns:
    //     The hue, in degrees, of this System.Drawing.Color. The hue is measured in
    //     degrees, ranging from 0.0 through 360.0, in HSB color space.
    public float GetHue()
    {
      Contract.Ensures(Contract.Result<float>() >= 0.0);
      Contract.Ensures(Contract.Result<float>() <= 360.0);

      return default(float);
    }


    //
    // Summary:
    //     Gets the hue-saturation-brightness (HSB) saturation value for this System.Drawing.Color
    //     structure.
    //
    // Returns:
    //     The saturation of this System.Drawing.Color. The saturation ranges from 0.0
    //     through 1.0, where 0.0 is grayscale and 1.0 is the most saturated.
    public float GetSaturation()
    {
      Contract.Ensures(Contract.Result<float>() >= 0.0);
      Contract.Ensures(Contract.Result<float>() <= 1.0);

      return default(float);
    }
    //
    // Summary:
    //     Gets the 32-bit ARGB value of this System.Drawing.Color structure.
    //
    // Returns:
    //     The 32-bit ARGB value of this System.Drawing.Color.
    //public int ToArgb();
    //
    // Summary:
    //     Gets the System.Drawing.KnownColor value of this System.Drawing.Color structure.
    //
    // Returns:
    //     An element of the System.Drawing.KnownColor enumeration, if the System.Drawing.Color
    //     is created from a predefined color by using either the System.Drawing.Color.FromName(System.String)
    //     method or the System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)
    //     method; otherwise, 0.
    //public KnownColor ToKnownColor();
    //
    // Summary:
    //     Converts this System.Drawing.Color structure to a human-readable string.
    //
    // Returns:
    //     A string that is the name of this System.Drawing.Color, if the System.Drawing.Color
    //     is created from a predefined color by using either the System.Drawing.Color.FromName(System.String)
    //     method or the System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)
    //     method; otherwise, a string that consists of the ARGB component names and
    //     their values.
    //public override string ToString();
  }
}
