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

// File System.Web.HttpUtility.cs
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


namespace System.Web
{
  sealed public partial class HttpUtility
  {
    #region Methods and constructors
    public static void HtmlAttributeEncode(string s, TextWriter output)
    {
    }

    public static string HtmlAttributeEncode(string s)
    {
      return default(string);
    }

    public static void HtmlDecode(string s, TextWriter output)
    {
    }

    public static string HtmlDecode(string s)
    {
      return default(string);
    }

    public static string HtmlEncode(Object value)
    {
      return default(string);
    }

    public static void HtmlEncode(string s, TextWriter output)
    {
    }

    public static string HtmlEncode(string s)
    {
      return default(string);
    }

    public HttpUtility()
    {
    }

    public static string JavaScriptStringEncode(string value, bool addDoubleQuotes)
    {
      return default(string);
    }

    public static string JavaScriptStringEncode(string value)
    {
      return default(string);
    }

    public static System.Collections.Specialized.NameValueCollection ParseQueryString(string query)
    {
      return default(System.Collections.Specialized.NameValueCollection);
    }

    public static System.Collections.Specialized.NameValueCollection ParseQueryString(string query, Encoding encoding)
    {
      return default(System.Collections.Specialized.NameValueCollection);
    }

    public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
    {
      return default(string);
    }

    public static string UrlDecode(string str)
    {
      return default(string);
    }

    public static string UrlDecode(byte[] bytes, Encoding e)
    {
      return default(string);
    }

    public static string UrlDecode(string str, Encoding e)
    {
      return default(string);
    }

    public static byte[] UrlDecodeToBytes(string str, Encoding e)
    {
      return default(byte[]);
    }

    public static byte[] UrlDecodeToBytes(string str)
    {
      return default(byte[]);
    }

    public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
    {
      return default(byte[]);
    }

    public static byte[] UrlDecodeToBytes(byte[] bytes)
    {
      return default(byte[]);
    }

    public static string UrlEncode(string str, Encoding e)
    {
      return default(string);
    }

    public static string UrlEncode(string str)
    {
      return default(string);
    }

    public static string UrlEncode(byte[] bytes, int offset, int count)
    {
      return default(string);
    }

    public static string UrlEncode(byte[] bytes)
    {
      return default(string);
    }

    public static byte[] UrlEncodeToBytes(byte[] bytes)
    {
      return default(byte[]);
    }

    public static byte[] UrlEncodeToBytes(string str, Encoding e)
    {
      return default(byte[]);
    }

    public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
    {
      return default(byte[]);
    }

    public static byte[] UrlEncodeToBytes(string str)
    {
      return default(byte[]);
    }

    public static string UrlEncodeUnicode(string str)
    {
      return default(string);
    }

    public static byte[] UrlEncodeUnicodeToBytes(string str)
    {
      return default(byte[]);
    }

    public static string UrlPathEncode(string str)
    {
      return default(string);
    }
    #endregion
  }
}
