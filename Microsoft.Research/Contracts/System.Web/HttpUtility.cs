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
using System.Collections.Specialized;
using System.IO;
using System.Diagnostics.Contracts;

namespace System.Web
{
  public sealed class HttpUtility
  {
    [Pure]
    public static string HtmlAttributeEncode(string s)
    {
      Contract.Ensures(s == null || Contract.Result<string>() != null);
      return default(string);
    }

    public static void HtmlAttributeEncode(string s, TextWriter output)
    {
      Contract.Requires(output != null);
    }

    [Pure]
    public static string HtmlDecode(string s)
    {
      Contract.Ensures(s == null || Contract.Result<string>() != null);
      return default(string);
    }

    public static void HtmlDecode(string s, TextWriter output)
    {
      Contract.Requires(output != null);
    }

    [Pure]
    public static string HtmlEncode(string s)
    {
      Contract.Ensures(s == null || Contract.Result<string>() != null);
      return default(string);
    }

    public static void HtmlEncode(string s, TextWriter output)
    {
      Contract.Requires(output != null);
    }

    [Pure]
    public static NameValueCollection ParseQueryString(string query)
    {
      Contract.Requires(query != null);
      Contract.Ensures(Contract.Result<NameValueCollection>() != null);
      return default(NameValueCollection);
    }

    [Pure]
    public static NameValueCollection ParseQueryString(string query, Encoding encoding)
    {
      Contract.Requires(query != null);
      Contract.Requires(encoding != null);
      Contract.Ensures(Contract.Result<NameValueCollection>() != null);
      return default(NameValueCollection);
    }

    [Pure]
    public static string UrlDecode(string str)
    {
      Contract.Ensures(str == null || Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string UrlDecode(byte[] bytes, Encoding e)
    {
      Contract.Ensures(bytes == null || Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string UrlDecode(string str, Encoding e)
    {
      Contract.Ensures(str == null || Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
    {
      Contract.Requires(bytes == null || offset + count <= bytes.Length);
      Contract.Ensures(Contract.Result<string>() != null || bytes == null && count == 0);
      return default(string);
    }

    [Pure]
    public static byte[] UrlDecodeToBytes(byte[] bytes)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null || bytes == null);
      return default(byte[]);
    }

    [Pure]
    public static byte[] UrlDecodeToBytes(string str)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null || str == null);
      return default(byte[]);
    }

    [Pure]
    public static byte[] UrlDecodeToBytes(string str, Encoding e)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null || str == null);
      return default(byte[]);
    }

    [Pure]
    public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
    {
      Contract.Requires(bytes == null || offset + count <= bytes.Length);
      Contract.Ensures(Contract.Result<byte[]>() != null || bytes == null && count == 0);
      return default(byte[]);
    }

    [Pure]
    public static string UrlEncode(byte[] bytes)
    {
      Contract.Ensures(Contract.Result<string>() != null || bytes == null);
      return default(string);
    }

    [Pure]
    public static string UrlEncode(string str)
    {
      Contract.Ensures(Contract.Result<string>() != null || str == null);
      return default(string);
    }

    [Pure]
    public static string UrlEncode(string str, Encoding e)
    {
      Contract.Ensures(Contract.Result<string>() != null || str == null);
      return default(string);
    }

    [Pure]
    public static string UrlEncode(byte[] bytes, int offset, int count)
    {
      Contract.Requires(bytes == null || offset + count <= bytes.Length);
      Contract.Ensures(Contract.Result<string>() != null || bytes == null);
      return default(string);
    }

    [Pure]
    public static byte[] UrlEncodeToBytes(string str)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null || str == null);
      return default(byte[]);
    }

    [Pure]
    public static byte[] UrlEncodeToBytes(byte[] bytes)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null || bytes == null);
      return default(byte[]);
    }

    [Pure]
    public static byte[] UrlEncodeToBytes(string str, Encoding e)
    {
      Contract.Requires(e != null);
      Contract.Ensures(Contract.Result<byte[]>() != null || str == null);
      return default(byte[]);
    }

    [Pure]
    public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
    {
      Contract.Requires(bytes == null || offset + count <= bytes.Length);
      Contract.Ensures(Contract.Result<byte[]>() != null || bytes == null);
      return default(byte[]);
    }

    [Pure]
    public static string UrlEncodeUnicode(string str)
    {
      Contract.Ensures(Contract.Result<string>() != null || str == null);
      return default(string);
    }

    [Pure]
    public static byte[] UrlEncodeUnicodeToBytes(string str)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null || str == null);
      return default(byte[]);
    }

    [Pure]
    public static string UrlPathEncode(string str)
    {
      Contract.Ensures(Contract.Result<string>() != null || str == null);
      return default(string);
    }

  }

}
