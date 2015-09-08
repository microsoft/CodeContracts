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

// File System.Web.HttpServerUtility.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
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
  sealed public partial class HttpServerUtility
  {
    #region Methods and constructors
    public void ClearError ()
    {
    }

    public Object CreateObject (string progID)
    {
      Contract.Ensures (Contract.Result<System.Object>() != null);

      return default(Object);
    }

    public Object CreateObject (Type type)
    {
      Contract.Requires (type != null);
      Contract.Ensures (Contract.Result<System.Object>() != null);

      return default(Object);
    }

    public Object CreateObjectFromClsid (string clsid)
    {
      Contract.Ensures (Contract.Result<System.Object>() != null);

      return default(Object);
    }

    public void Execute (string path, bool preserveForm)
    {
    }

    public void Execute (IHttpHandler handler, TextWriter writer, bool preserveForm)
    {
    }

    public void Execute (string path, TextWriter writer, bool preserveForm)
    {
    }

    public void Execute (string path, TextWriter writer)
    {
    }

    public void Execute (string path)
    {
    }

    public Exception GetLastError ()
    {
      return default(Exception);
    }

    public void HtmlDecode (string s, TextWriter output)
    {
    }

    [Pure]
    public string HtmlDecode (string s)
    {
      Contract.Ensures(s == null || Contract.Result<string>() != null);

      return default(string);
    }

    [Pure]
    public string HtmlEncode (string s)
    {
      Contract.Ensures(s == null || Contract.Result<string>() != null);

      return default(string);
    }

    public void HtmlEncode (string s, TextWriter output)
    {
    }

    public string MapPath (string path)
    {
      Contract.Ensures(path == null || Contract.Result<string>() != null);

      return default(string);
    }

    public void Transfer (IHttpHandler handler, bool preserveForm)
    {
    }

    public void Transfer (string path)
    {
    }

    public void Transfer (string path, bool preserveForm)
    {
    }

    public void TransferRequest (string path)
    {
    }

    public void TransferRequest (string path, bool preserveForm, string method, System.Collections.Specialized.NameValueCollection headers)
    {
    }

    public void TransferRequest (string path, bool preserveForm)
    {
    }

    public string UrlDecode (string s)
    {
      return default(string);
    }

    public void UrlDecode (string s, TextWriter output)
    {
      Contract.Requires (output != null);
    }

    public string UrlEncode (string s)
    {
      return default(string);
    }

    public void UrlEncode (string s, TextWriter output)
    {
      Contract.Requires (output != null);
    }

    [Pure]
    public string UrlPathEncode (string s)
    {
      Contract.Ensures(Contract.Result<string>() != null || s == null);

      return default(string);
    }

    public static byte[] UrlTokenDecode (string input)
    {
      return default(byte[]);
    }

    public static string UrlTokenEncode (byte[] input)
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public string MachineName
    {
      get
      {
        return default(string);
      }
    }

    public int ScriptTimeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }
    #endregion
  }
}
