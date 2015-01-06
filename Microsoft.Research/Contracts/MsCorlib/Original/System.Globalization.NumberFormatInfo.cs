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

        public string! PerMilleSymbol
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string! CurrencySymbol
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string! NaNSymbol
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string PercentDecimalSeparator
        {
          get;
          set;
        }

        public Int32[] PercentGroupSizes
        {
          get;
          set;
        }

        public int PercentPositivePattern
        {
          get;
          set
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 2);
        }

        public int NumberNegativePattern
        {
          get;
          set
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 4);
        }

        public static NumberFormatInfo CurrentInfo
        {
          get;
        }

        public int CurrencyDecimalDigits
        {
          get;
          set
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 99);
        }

        public int NumberDecimalDigits
        {
          get;
          set
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 99);
        }

        public int PercentNegativePattern
        {
          get;
          set
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 2);
        }

        public Int32[] CurrencyGroupSizes
        {
          get;
          set;
        }

        public string! PercentSymbol
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string! PositiveInfinitySymbol
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string! PositiveSign
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string! NegativeInfinitySymbol
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public int PercentDecimalDigits
        {
          get;
          set
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 99);
        }

        public string CurrencyGroupSeparator
        {
          get;
          set;
        }

        public Int32[] NumberGroupSizes
        {
          get;
          set;
        }

        public string NumberDecimalSeparator
        {
          get;
          set;
        }

        public int CurrencyPositivePattern
        {
          get;
          set
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 3);
        }

        public string NumberGroupSeparator
        {
          get;
          set;
        }

        public string PercentGroupSeparator
        {
          get;
          set;
        }

        public int CurrencyNegativePattern
        {
          get;
          set
            CodeContract.Requires(value >= 0);
            CodeContract.Requires(value <= 15);
        }

        public string! NegativeSign
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public bool IsReadOnly
        {
          get;
        }

        public static NumberFormatInfo InvariantInfo
        {
          get;
        }

        public string CurrencyDecimalSeparator
        {
          get;
          set;
        }

        public static NumberFormatInfo ReadOnly (NumberFormatInfo! nfi) {
            CodeContract.Requires(nfi != null);

          return default(NumberFormatInfo);
        }
        public object GetFormat (Type formatType) {

          return default(object);
        }
        public object Clone () {

          return default(object);
        }
        public static NumberFormatInfo GetInstance (IFormatProvider formatProvider) {

          return default(NumberFormatInfo);
        }
        public NumberFormatInfo () {
          return default(NumberFormatInfo);
        }
    }
}
