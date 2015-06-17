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

// File System.Windows.Media.Color.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Windows.Media
{
  public partial struct Color : IFormattable, IEquatable<Color>
  {
    #region Methods and constructors
    public static Color operator - (Color color1, Color color2)
    {
      return default(Color);
    }

    public static bool operator != (Color color1, Color color2)
    {
      return default(bool);
    }

    public static Color operator * (Color color, float coefficient)
    {
      return default(Color);
    }

    public static Color operator + (Color color1, Color color2)
    {
      return default(Color);
    }

    public static bool operator == (Color color1, Color color2)
    {
      return default(bool);
    }

    public static Color Add(Color color1, Color color2)
    {
      return default(Color);
    }

    public static bool AreClose(Color color1, Color color2)
    {
      return default(bool);
    }

    public void Clamp()
    {
    }

    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public static bool Equals(Color color1, Color color2)
    {
      return default(bool);
    }

    public bool Equals(Color color)
    {
      return default(bool);
    }

    public static Color FromArgb(byte a, byte r, byte g, byte b)
    {
      return default(Color);
    }

    public static Color FromAValues(float a, float[] values, Uri profileUri)
    {
      return default(Color);
    }

    public static Color FromRgb(byte r, byte g, byte b)
    {
      return default(Color);
    }

    public static Color FromScRgb(float a, float r, float g, float b)
    {
      return default(Color);
    }

    public static Color FromValues(float[] values, Uri profileUri)
    {
      return default(Color);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public float[] GetNativeColorValues()
    {
      return default(float[]);
    }

    public static Color Multiply(Color color, float coefficient)
    {
      return default(Color);
    }

    public static Color Subtract(Color color1, Color color2)
    {
      return default(Color);
    }

    string System.IFormattable.ToString(string format, IFormatProvider provider)
    {
      return default(string);
    }

    public string ToString(IFormatProvider provider)
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public byte A
    {
      get
      {
        return default(byte);
      }
      set
      {
      }
    }

    public byte B
    {
      get
      {
        return default(byte);
      }
      set
      {
      }
    }

    public ColorContext ColorContext
    {
      get
      {
        return default(ColorContext);
      }
    }

    public byte G
    {
      get
      {
        return default(byte);
      }
      set
      {
      }
    }

    public byte R
    {
      get
      {
        return default(byte);
      }
      set
      {
      }
    }

    public float ScA
    {
      get
      {
        return default(float);
      }
      set
      {
      }
    }

    public float ScB
    {
      get
      {
        return default(float);
      }
      set
      {
      }
    }

    public float ScG
    {
      get
      {
        return default(float);
      }
      set
      {
      }
    }

    public float ScR
    {
      get
      {
        return default(float);
      }
      set
      {
      }
    }
    #endregion
  }
}
