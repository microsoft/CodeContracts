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

// File System.Web.Util.HttpEncoder.cs
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


namespace System.Web.Util
{
  public partial class HttpEncoder
  {
    #region Methods and constructors
    protected internal virtual new void HeaderNameValueEncode(string headerName, string headerValue, out string encodedHeaderName, out string encodedHeaderValue)
    {
      encodedHeaderName = default(string);
      encodedHeaderValue = default(string);
    }

    protected internal virtual new void HtmlAttributeEncode(string value, TextWriter output)
    {
    }

    protected internal virtual new void HtmlDecode(string value, TextWriter output)
    {
    }

    protected internal virtual new void HtmlEncode(string value, TextWriter output)
    {
    }

    public HttpEncoder()
    {
    }

    protected internal virtual new byte[] UrlEncode(byte[] bytes, int offset, int count)
    {
      return default(byte[]);
    }

    protected internal virtual new string UrlPathEncode(string value)
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public static System.Web.Util.HttpEncoder Current
    {
      get
      {
        return default(System.Web.Util.HttpEncoder);
      }
      set
      {
      }
    }

    public static System.Web.Util.HttpEncoder Default
    {
      get
      {
        return default(System.Web.Util.HttpEncoder);
      }
    }
    #endregion
  }
}
