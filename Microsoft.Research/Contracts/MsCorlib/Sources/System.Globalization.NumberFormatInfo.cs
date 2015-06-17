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

// File System.Globalization.NumberFormatInfo.cs
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


namespace System.Globalization
{
  sealed public partial class NumberFormatInfo : ICloneable, IFormatProvider
  {
    #region Methods and constructors
    public Object Clone()
    {
      return default(Object);
    }

    public Object GetFormat(Type formatType)
    {
      return default(Object);
    }

    public static System.Globalization.NumberFormatInfo GetInstance(IFormatProvider formatProvider)
    {
      return default(System.Globalization.NumberFormatInfo);
    }

    public NumberFormatInfo()
    {
    }

    public static System.Globalization.NumberFormatInfo ReadOnly(System.Globalization.NumberFormatInfo nfi)
    {
      Contract.Ensures(Contract.Result<System.Globalization.NumberFormatInfo>() != null);

      return default(System.Globalization.NumberFormatInfo);
    }
    #endregion

    #region Properties and indexers
    public int CurrencyDecimalDigits
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string CurrencyDecimalSeparator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string CurrencyGroupSeparator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int[] CurrencyGroupSizes
    {
      get
      {
        return default(int[]);
      }
      set
      {
      }
    }

    public int CurrencyNegativePattern
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int CurrencyPositivePattern
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string CurrencySymbol
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public static System.Globalization.NumberFormatInfo CurrentInfo
    {
      get
      {
        return default(System.Globalization.NumberFormatInfo);
      }
    }

    public DigitShapes DigitSubstitution
    {
      get
      {
        return default(DigitShapes);
      }
      set
      {
      }
    }

    public static System.Globalization.NumberFormatInfo InvariantInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Globalization.NumberFormatInfo>() != null);

        return default(System.Globalization.NumberFormatInfo);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public string NaNSymbol
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string[] NativeDigits
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public string NegativeInfinitySymbol
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string NegativeSign
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int NumberDecimalDigits
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string NumberDecimalSeparator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string NumberGroupSeparator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int[] NumberGroupSizes
    {
      get
      {
        return default(int[]);
      }
      set
      {
      }
    }

    public int NumberNegativePattern
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int PercentDecimalDigits
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string PercentDecimalSeparator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string PercentGroupSeparator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int[] PercentGroupSizes
    {
      get
      {
        return default(int[]);
      }
      set
      {
      }
    }

    public int PercentNegativePattern
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int PercentPositivePattern
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string PercentSymbol
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string PerMilleSymbol
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string PositiveInfinitySymbol
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string PositiveSign
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion
  }
}
