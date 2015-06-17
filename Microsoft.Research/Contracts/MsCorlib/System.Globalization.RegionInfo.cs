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

namespace System.Globalization
{

  public class RegionInfo
  {

    extern public bool IsMetric
    {
      get;
    }

    extern public string TwoLetterISORegionName
    {
      get;
    }

    extern public string EnglishName
    {
      get;
    }

    extern public string ThreeLetterWindowsRegionName
    {
      get;
    }

    extern public static RegionInfo CurrentRegion
    {
      get;
    }

    extern public string Name
    {
      get;
    }

    extern public string ThreeLetterISORegionName
    {
      get;
    }

    extern public string CurrencySymbol
    {
      get;
    }

    extern public string ISOCurrencySymbol
    {
      get;
    }

    extern public string DisplayName
    {
      get;
    }

    public RegionInfo(int culture)
    {

      return default(RegionInfo);
    }
    public RegionInfo(string name)
    {
      Contract.Requires(name != null);
      return default(RegionInfo);
    }
  }
}
