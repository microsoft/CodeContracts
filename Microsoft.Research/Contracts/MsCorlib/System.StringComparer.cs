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
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

namespace System
{
  public abstract class StringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
  {
    // Methods
    extern protected StringComparer();
    extern public int Compare(object x, object y);
    public abstract int Compare(string x, string y);
    public static StringComparer Create(CultureInfo culture, bool ignoreCase)
    {
      Contract.Requires(culture != null);
      Contract.Ensures(Contract.Result<StringComparer>() != null);
      return default(StringComparer);
    }
    new extern public bool Equals(object x, object y);
    public abstract bool Equals(string x, string y);
    extern public int GetHashCode(object obj);
    public abstract int GetHashCode(string obj);

    // Properties
    public static StringComparer CurrentCulture
    {
      get
      {
        Contract.Ensures(Contract.Result<StringComparer>() != null);
        return default(StringComparer);
      }
    }
    public static StringComparer CurrentCultureIgnoreCase
    {
      get
      {
        Contract.Ensures(Contract.Result<StringComparer>() != null);
        return default(StringComparer);
      }
    }
    public static StringComparer InvariantCulture
    {
      get
      {
        Contract.Ensures(Contract.Result<StringComparer>() != null);
        return default(StringComparer);
      }
    }
    public static StringComparer InvariantCultureIgnoreCase
    {
      get
      {
        Contract.Ensures(Contract.Result<StringComparer>() != null);
        return default(StringComparer);
      }
    }
    public static StringComparer Ordinal
    {
      get
      {
        Contract.Ensures(Contract.Result<StringComparer>() != null);
        return default(StringComparer);
      }
    }
    public static StringComparer OrdinalIgnoreCase
    {
      get
      {
        Contract.Ensures(Contract.Result<StringComparer>() != null);
        return default(StringComparer);
      }
    }
  }
}
