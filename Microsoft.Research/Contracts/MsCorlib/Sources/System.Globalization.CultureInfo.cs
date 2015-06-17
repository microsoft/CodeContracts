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

// File System.Globalization.CultureInfo.cs
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
  public partial class CultureInfo : ICloneable, IFormatProvider
  {
    #region Methods and constructors
    public void ClearCachedData()
    {
    }

    public virtual new Object Clone()
    {
      return default(Object);
    }

    public static System.Globalization.CultureInfo CreateSpecificCulture(string name)
    {
      Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() != null);

      return default(System.Globalization.CultureInfo);
    }

    public CultureInfo(string name)
    {
    }

    public CultureInfo(string name, bool useUserOverride)
    {
    }

    public CultureInfo(int culture)
    {
    }

    public CultureInfo(int culture, bool useUserOverride)
    {
    }

    public override bool Equals(Object value)
    {
      return default(bool);
    }

    public System.Globalization.CultureInfo GetConsoleFallbackUICulture()
    {
      Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() != null);

      return default(System.Globalization.CultureInfo);
    }

    public static System.Globalization.CultureInfo GetCultureInfo(string name)
    {
      Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() != null);

      return default(System.Globalization.CultureInfo);
    }

    public static System.Globalization.CultureInfo GetCultureInfo(string name, string altName)
    {
      Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() != null);

      return default(System.Globalization.CultureInfo);
    }

    public static System.Globalization.CultureInfo GetCultureInfo(int culture)
    {
      Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() != null);

      return default(System.Globalization.CultureInfo);
    }

    public static System.Globalization.CultureInfo GetCultureInfoByIetfLanguageTag(string name)
    {
      Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() != null);

      return default(System.Globalization.CultureInfo);
    }

    public static System.Globalization.CultureInfo[] GetCultures(CultureTypes types)
    {
      return default(System.Globalization.CultureInfo[]);
    }

    public virtual new Object GetFormat(Type formatType)
    {
      return default(Object);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public static System.Globalization.CultureInfo ReadOnly(System.Globalization.CultureInfo ci)
    {
      Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() != null);

      return default(System.Globalization.CultureInfo);
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public virtual new Calendar Calendar
    {
      get
      {
        return default(Calendar);
      }
    }

    public virtual new CompareInfo CompareInfo
    {
      get
      {
        return default(CompareInfo);
      }
    }

    public CultureTypes CultureTypes
    {
      get
      {
        Contract.Ensures(((System.Globalization.CultureTypes)(1)) <= Contract.Result<System.Globalization.CultureTypes>());
        Contract.Ensures(Contract.Result<System.Globalization.CultureTypes>() <= ((System.Globalization.CultureTypes)(94)));

        return default(CultureTypes);
      }
    }

    public static System.Globalization.CultureInfo CurrentCulture
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() == System.Threading.Thread.CurrentThread.CurrentCulture);

        return default(System.Globalization.CultureInfo);
      }
    }

    public static System.Globalization.CultureInfo CurrentUICulture
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() == System.Threading.Thread.CurrentThread.CurrentUICulture);

        return default(System.Globalization.CultureInfo);
      }
    }

    public virtual new DateTimeFormatInfo DateTimeFormat
    {
      get
      {
        return default(DateTimeFormatInfo);
      }
      set
      {
      }
    }

    public virtual new string DisplayName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string EnglishName
    {
      get
      {
        return default(string);
      }
    }

    public string IetfLanguageTag
    {
      get
      {
        return default(string);
      }
    }

    public static System.Globalization.CultureInfo InstalledUICulture
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() != null);

        return default(System.Globalization.CultureInfo);
      }
    }

    public static System.Globalization.CultureInfo InvariantCulture
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() != null);

        return default(System.Globalization.CultureInfo);
      }
    }

    public virtual new bool IsNeutralCulture
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new int KeyboardLayoutId
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int LCID
    {
      get
      {
        return default(int);
      }
    }

    public virtual new string Name
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string NativeName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new NumberFormatInfo NumberFormat
    {
      get
      {
        return default(NumberFormatInfo);
      }
      set
      {
      }
    }

    public virtual new System.Globalization.Calendar[] OptionalCalendars
    {
      get
      {
        return default(System.Globalization.Calendar[]);
      }
    }

    public virtual new System.Globalization.CultureInfo Parent
    {
      get
      {
        return default(System.Globalization.CultureInfo);
      }
    }

    public virtual new TextInfo TextInfo
    {
      get
      {
        return default(TextInfo);
      }
    }

    public virtual new string ThreeLetterISOLanguageName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string ThreeLetterWindowsLanguageName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string TwoLetterISOLanguageName
    {
      get
      {
        return default(string);
      }
    }

    public bool UseUserOverride
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
