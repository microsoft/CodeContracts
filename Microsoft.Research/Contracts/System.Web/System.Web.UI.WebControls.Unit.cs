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

// File System.Web.UI.WebControls.Unit.cs
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
  public partial struct Unit
  {
    #region Methods and constructors
    public static bool operator !=  (System.Web.UI.WebControls.Unit left, System.Web.UI.WebControls.Unit right)
    {
      return default(bool);
    }

    public static bool operator ==  (System.Web.UI.WebControls.Unit left, System.Web.UI.WebControls.Unit right)
    {
      return default(bool);
    }

    public override bool Equals (Object obj)
    {
      return default(bool);
    }

    public override int GetHashCode ()
    {
      return default(int);
    }

    public static System.Web.UI.WebControls.Unit Parse (string s)
    {
      return default(System.Web.UI.WebControls.Unit);
    }

    public static System.Web.UI.WebControls.Unit Parse (string s, System.Globalization.CultureInfo culture)
    {
      return default(System.Web.UI.WebControls.Unit);
    }

    public static System.Web.UI.WebControls.Unit Percentage (double n)
    {
      return default(System.Web.UI.WebControls.Unit);
    }

    public static System.Web.UI.WebControls.Unit Pixel (int n)
    {
      return default(System.Web.UI.WebControls.Unit);
    }

    public static System.Web.UI.WebControls.Unit Point (int n)
    {
      return default(System.Web.UI.WebControls.Unit);
    }

    public static implicit operator System.Web.UI.WebControls.Unit (int n)
    {
      return default(System.Web.UI.WebControls.Unit);
    }

    public string ToString (System.Globalization.CultureInfo culture)
    {
      return default(string);
    }

    public string ToString (IFormatProvider formatProvider)
    {
      return default(string);
    }

    public Unit (double value)
    {
    }

    public Unit (int value)
    {
    }

    public Unit (string value)
    {
    }

    public Unit (double value, UnitType type)
    {
    }

    public Unit (string value, System.Globalization.CultureInfo culture)
    {
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

    public UnitType Type
    {
      get
      {
        return default(UnitType);
      }
    }

    public double Value
    {
      get
      {
        return default(double);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Web.UI.WebControls.Unit Empty;
    #endregion
  }
}
