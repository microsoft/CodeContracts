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

// File System.Web.UI.WebControls.FontUnit.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Web.UI.WebControls
{
  public partial struct FontUnit
  {
    #region Methods and constructors
    public static bool operator !=  (System.Web.UI.WebControls.FontUnit left, System.Web.UI.WebControls.FontUnit right)
    {
      return default(bool);
    }

    public static bool operator ==  (System.Web.UI.WebControls.FontUnit left, System.Web.UI.WebControls.FontUnit right)
    {
      return default(bool);
    }

    public override bool Equals (Object obj)
    {
      return default(bool);
    }

    public FontUnit (FontSize type)
    {
    }

    public FontUnit (int value)
    {
    }

    public FontUnit (Unit value)
    {
    }

    public FontUnit (string value)
    {
    }

    public FontUnit (double value)
    {
    }

    public FontUnit (double value, UnitType type)
    {
    }

    public FontUnit (string value, System.Globalization.CultureInfo culture)
    {
    }

    public override int GetHashCode ()
    {
      return default(int);
    }

    public static System.Web.UI.WebControls.FontUnit Parse (string s)
    {
      return default(System.Web.UI.WebControls.FontUnit);
    }

    public static System.Web.UI.WebControls.FontUnit Parse (string s, System.Globalization.CultureInfo culture)
    {
      return default(System.Web.UI.WebControls.FontUnit);
    }

    public static System.Web.UI.WebControls.FontUnit Point (int n)
    {
      return default(System.Web.UI.WebControls.FontUnit);
    }

    public static implicit operator System.Web.UI.WebControls.FontUnit (int n)
    {
      return default(System.Web.UI.WebControls.FontUnit);
    }

    public string ToString (System.Globalization.CultureInfo culture)
    {
      return default(string);
    }

    public string ToString (IFormatProvider formatProvider)
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public bool IsEmpty
    {
      get
      {
        return default(bool);
      }
    }

    public FontSize Type
    {
      get
      {
        return default(FontSize);
      }
    }

    public Unit Unit
    {
      get
      {
        return default(Unit);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Web.UI.WebControls.FontUnit Empty;
    public readonly static System.Web.UI.WebControls.FontUnit Large;
    public readonly static System.Web.UI.WebControls.FontUnit Larger;
    public readonly static System.Web.UI.WebControls.FontUnit Medium;
    public readonly static System.Web.UI.WebControls.FontUnit Small;
    public readonly static System.Web.UI.WebControls.FontUnit Smaller;
    public readonly static System.Web.UI.WebControls.FontUnit XLarge;
    public readonly static System.Web.UI.WebControls.FontUnit XSmall;
    public readonly static System.Web.UI.WebControls.FontUnit XXLarge;
    public readonly static System.Web.UI.WebControls.FontUnit XXSmall;
    #endregion
  }
}
