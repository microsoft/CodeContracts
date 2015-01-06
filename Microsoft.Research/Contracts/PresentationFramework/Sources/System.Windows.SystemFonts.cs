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

// File System.Windows.SystemFonts.cs
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


namespace System.Windows
{
  static public partial class SystemFonts
  {
    #region Properties and indexers
    public static System.Windows.Media.FontFamily CaptionFontFamily
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Media.FontFamily>() != null);

        return default(System.Windows.Media.FontFamily);
      }
    }

    public static ResourceKey CaptionFontFamilyKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double CaptionFontSize
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey CaptionFontSizeKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontStyle CaptionFontStyle
    {
      get
      {
        return default(FontStyle);
      }
    }

    public static ResourceKey CaptionFontStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static TextDecorationCollection CaptionFontTextDecorations
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.TextDecorationCollection>() != null);

        return default(TextDecorationCollection);
      }
    }

    public static ResourceKey CaptionFontTextDecorationsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontWeight CaptionFontWeight
    {
      get
      {
        return default(FontWeight);
      }
    }

    public static ResourceKey CaptionFontWeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static System.Windows.Media.FontFamily IconFontFamily
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Media.FontFamily>() != null);

        return default(System.Windows.Media.FontFamily);
      }
    }

    public static ResourceKey IconFontFamilyKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double IconFontSize
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey IconFontSizeKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontStyle IconFontStyle
    {
      get
      {
        return default(FontStyle);
      }
    }

    public static ResourceKey IconFontStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static TextDecorationCollection IconFontTextDecorations
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.TextDecorationCollection>() != null);

        return default(TextDecorationCollection);
      }
    }

    public static ResourceKey IconFontTextDecorationsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontWeight IconFontWeight
    {
      get
      {
        return default(FontWeight);
      }
    }

    public static ResourceKey IconFontWeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static System.Windows.Media.FontFamily MenuFontFamily
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Media.FontFamily>() != null);

        return default(System.Windows.Media.FontFamily);
      }
    }

    public static ResourceKey MenuFontFamilyKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MenuFontSize
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MenuFontSizeKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontStyle MenuFontStyle
    {
      get
      {
        return default(FontStyle);
      }
    }

    public static ResourceKey MenuFontStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static TextDecorationCollection MenuFontTextDecorations
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.TextDecorationCollection>() != null);

        return default(TextDecorationCollection);
      }
    }

    public static ResourceKey MenuFontTextDecorationsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontWeight MenuFontWeight
    {
      get
      {
        return default(FontWeight);
      }
    }

    public static ResourceKey MenuFontWeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static System.Windows.Media.FontFamily MessageFontFamily
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Media.FontFamily>() != null);

        return default(System.Windows.Media.FontFamily);
      }
    }

    public static ResourceKey MessageFontFamilyKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MessageFontSize
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MessageFontSizeKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontStyle MessageFontStyle
    {
      get
      {
        return default(FontStyle);
      }
    }

    public static ResourceKey MessageFontStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static TextDecorationCollection MessageFontTextDecorations
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.TextDecorationCollection>() != null);

        return default(TextDecorationCollection);
      }
    }

    public static ResourceKey MessageFontTextDecorationsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontWeight MessageFontWeight
    {
      get
      {
        return default(FontWeight);
      }
    }

    public static ResourceKey MessageFontWeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static System.Windows.Media.FontFamily SmallCaptionFontFamily
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Media.FontFamily>() != null);

        return default(System.Windows.Media.FontFamily);
      }
    }

    public static ResourceKey SmallCaptionFontFamilyKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double SmallCaptionFontSize
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey SmallCaptionFontSizeKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontStyle SmallCaptionFontStyle
    {
      get
      {
        return default(FontStyle);
      }
    }

    public static ResourceKey SmallCaptionFontStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static TextDecorationCollection SmallCaptionFontTextDecorations
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.TextDecorationCollection>() != null);

        return default(TextDecorationCollection);
      }
    }

    public static ResourceKey SmallCaptionFontTextDecorationsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontWeight SmallCaptionFontWeight
    {
      get
      {
        return default(FontWeight);
      }
    }

    public static ResourceKey SmallCaptionFontWeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static System.Windows.Media.FontFamily StatusFontFamily
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Media.FontFamily>() != null);

        return default(System.Windows.Media.FontFamily);
      }
    }

    public static ResourceKey StatusFontFamilyKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double StatusFontSize
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey StatusFontSizeKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontStyle StatusFontStyle
    {
      get
      {
        return default(FontStyle);
      }
    }

    public static ResourceKey StatusFontStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static TextDecorationCollection StatusFontTextDecorations
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.TextDecorationCollection>() != null);

        return default(TextDecorationCollection);
      }
    }

    public static ResourceKey StatusFontTextDecorationsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static FontWeight StatusFontWeight
    {
      get
      {
        return default(FontWeight);
      }
    }

    public static ResourceKey StatusFontWeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }
    #endregion
  }
}
