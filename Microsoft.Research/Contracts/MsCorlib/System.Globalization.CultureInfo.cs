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
using System.Globalization;
using System.Diagnostics.Contracts;

namespace System.Globalization
{
#if !SILVERLIGHT
  public enum CultureTypes
  {
    NeutralCultures = 1,
    SpecificCultures = 2,
    InstalledWin32Cultures = 4,
    AllCultures = InstalledWin32Cultures | SpecificCultures | NeutralCultures,
    UserCustomCulture = 8,
    ReplacementCultures = 16,
    WindowsOnlyCultures = 32,
    FrameworkCultures = 64,
  }
#endif

  public abstract class CultureInfo
  {

    public static CultureInfo CurrentUICulture
    {
      get
      {
        Contract.Ensures(Contract.Result<CultureInfo>() != null);
        return default(CultureInfo);
      }
    }

    public virtual TextInfo TextInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<TextInfo>() != null);
        return default(TextInfo);
      }
    }
#if false
    public Calendar[] OptionalCalendars
    {
      get
      {
        Contract.Ensures(Contract.Result<Calendar[]>() != null);
      }
    }
#endif
    public virtual string DisplayName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
#if false
    public bool UseUserOverride
    {
      get;
    }

    public int LCID
    {
      get;
    }
#endif
    public virtual NumberFormatInfo NumberFormat
    {
      get
      {
        Contract.Ensures(Contract.Result<NumberFormatInfo>() != null);
        return default(NumberFormatInfo);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

#if !SILVERLIGHT
    public virtual string ThreeLetterWindowsLanguageName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
#endif

    public virtual CompareInfo CompareInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<CompareInfo>() != null);
        return default(CompareInfo);
      }
    }

#if !SILVERLIGHT
    public virtual string ThreeLetterISOLanguageName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
#endif

    public virtual string NativeName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public static CultureInfo CurrentCulture
    {
      get
      {
        Contract.Ensures(Contract.Result<CultureInfo>() != null);
        return default(CultureInfo);
      }
    }


    public virtual string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public virtual Calendar Calendar
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Globalization.Calendar>() != null);
        return default(Calendar);
      }
    }

    public virtual string TwoLetterISOLanguageName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

#if !SILVERLIGHT
    public static CultureInfo InstalledUICulture
    {
      get
      {
        Contract.Ensures(Contract.Result<CultureInfo>() != null);
        return default(CultureInfo);
      }
    }
#endif

    public virtual DateTimeFormatInfo DateTimeFormat
    {
      get
      {
        Contract.Ensures(Contract.Result<DateTimeFormatInfo>() != null);
        return null;
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public virtual CultureInfo Parent
    {
        get
        {
            Contract.Ensures(Contract.Result<CultureInfo>() != null);
            return null;
        }
    }

    public virtual string EnglishName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public static CultureInfo InvariantCulture
    {
      get
      {
        Contract.Ensures(Contract.Result<CultureInfo>() != null);
        return default(CultureInfo);
      }
    }

    extern public bool IsReadOnly
    {
      get;
    }

    public static CultureInfo ReadOnly(CultureInfo ci)
    {
      Contract.Requires(ci != null);

      return default(CultureInfo);
    }

#if !SILVERLIGHT
    extern public void ClearCachedData();
#endif

    public abstract object GetFormat(Type formatType);

#if !SILVERLIGHT

    public static CultureInfo[] GetCultures(CultureTypes types)
    {
      Contract.Ensures(Contract.Result<CultureInfo[]>() != null);
      return default(CultureInfo[]);
    }

    public static CultureInfo CreateSpecificCulture(string name)
    {
      Contract.Ensures(Contract.Result<CultureInfo>() != null);

      return default(CultureInfo);
    }

    public static CultureInfo GetCultureInfo(int culture)
    {
      Contract.Requires(culture >= 0);

      return default(CultureInfo);
    }

    public static CultureInfo GetCultureInfo(string name)
    {
      Contract.Requires(name != null);

      return default(CultureInfo);
    }

    public static CultureInfo GetCultureInfo(string name, string altName)
    {
      Contract.Requires(name != null);
      Contract.Requires(altName != null);

      return default(CultureInfo);
    }
#endif

  }
}
