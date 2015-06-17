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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.VisualBasic.CompilerServices
{
  public static class Conversions
  {
    [Pure]
    public static string ToString(bool b)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string ToString(byte b)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string ToString(char c)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string ToString(decimal d)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string ToString(double d)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string ToString(Int16 i16)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string ToString(Int32 i32)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string ToString(Int64 i64)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string ToString(object obj)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string ToString(Single s)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string ToString(UInt32 ui32)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string ToString(UInt64 ui64)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
  }
}
