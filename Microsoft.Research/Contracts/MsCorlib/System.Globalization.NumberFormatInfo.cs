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

using System.Diagnostics.Contracts;
using System;

namespace System.Globalization
{

  public class NumberFormatInfo
  {

    public string PerMilleSymbol
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string CurrencySymbol
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string NaNSymbol
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string PercentDecimalSeparator
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public Int32[] PercentGroupSizes
    {
      get
      {
        Contract.Ensures(Contract.Result<Int32[]>() != null);
        return default(Int32[]);
      }
      set
      {
        Contract.Requires(value != null);

      }
    }

    public int PercentPositivePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 2);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value <= 2);
      }
    }

    public int NumberNegativePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 4);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value <= 4);
      }
    }

    public static NumberFormatInfo CurrentInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<NumberFormatInfo>() != null);
        return default(NumberFormatInfo);
      }
    }

    public int CurrencyDecimalDigits
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 99);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value <= 99);
      }
    }

    public int NumberDecimalDigits
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 99);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value <= 99);
      }
    }

    public int PercentNegativePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 2);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value <= 2);
      }
    }

    public Int32[] CurrencyGroupSizes
    {
      get
      {
        Contract.Ensures(Contract.Result<Int32[]>() != null);
        return default(Int32[]);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string PercentSymbol
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string PositiveInfinitySymbol
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string PositiveSign
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string NegativeInfinitySymbol
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public int PercentDecimalDigits
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 99);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value <= 99);
      }
    }

    public string CurrencyGroupSeparator
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public Int32[] NumberGroupSizes
    {
      get
      {
        Contract.Ensures(Contract.Result<Int32[]>() != null);
        return default(Int32[]);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string NumberDecimalSeparator
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public int CurrencyPositivePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 3);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value <= 3);
      }
    }

    public string NumberGroupSeparator
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string PercentGroupSeparator
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public int CurrencyNegativePattern
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 3);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value <= 15);
      }
    }

    public string NegativeSign
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    extern public bool IsReadOnly
    {
      get;
    }

    public static NumberFormatInfo InvariantInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<NumberFormatInfo>() != null);
        return default(NumberFormatInfo);
      }
    }

    public string CurrencyDecimalSeparator
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);

      }
    }

    [Pure]
    public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
    {
      Contract.Requires(nfi != null);
      Contract.Ensures(Contract.Result<NumberFormatInfo>() != null);
      Contract.Ensures(Contract.Result<NumberFormatInfo>().IsReadOnly);

      return default(NumberFormatInfo);
    }

    [Pure]
    public virtual object GetFormat(Type formatType)
    {
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }

    [Pure]
    public static NumberFormatInfo GetInstance(IFormatProvider formatProvider)
    {
      Contract.Ensures(Contract.Result<NumberFormatInfo>() != null);

      return default(NumberFormatInfo);
    }

  }
}
