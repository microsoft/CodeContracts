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

// File System.Web.HttpServerUtilityWrapper.cs
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
  public partial class HttpServerUtilityWrapper : HttpServerUtilityBase
  {
    #region Methods and constructors
    public override void ClearError()
    {
    }

    public override Object CreateObject(Type type)
    {
      return default(Object);
    }

    public override Object CreateObject(string progID)
    {
      return default(Object);
    }

    public override Object CreateObjectFromClsid(string clsid)
    {
      return default(Object);
    }

    public override void Execute(string path, bool preserveForm)
    {
    }

    public override void Execute(string path, TextWriter writer, bool preserveForm)
    {
    }

    public override void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
    {
    }

    public override void Execute(string path)
    {
    }

    public override void Execute(string path, TextWriter writer)
    {
    }

    public override Exception GetLastError()
    {
      return default(Exception);
    }

    public override void HtmlDecode(string s, TextWriter output)
    {
    }

    public override string HtmlDecode(string s)
    {
      return default(string);
    }

    public override string HtmlEncode(string s)
    {
      return default(string);
    }

    public override void HtmlEncode(string s, TextWriter output)
    {
    }

    public HttpServerUtilityWrapper(HttpServerUtility httpServerUtility)
    {
    }

    public override string MapPath(string path)
    {
      return default(string);
    }

    public override void Transfer(string path, bool preserveForm)
    {
    }

    public override void Transfer(string path)
    {
    }

    public override void Transfer(IHttpHandler handler, bool preserveForm)
    {
    }

    public override void TransferRequest(string path)
    {
    }

    public override void TransferRequest(string path, bool preserveForm)
    {
    }

    public override void TransferRequest(string path, bool preserveForm, string method, System.Collections.Specialized.NameValueCollection headers)
    {
    }

    public override void UrlDecode(string s, TextWriter output)
    {
    }

    public override string UrlDecode(string s)
    {
      return default(string);
    }

    public override string UrlEncode(string s)
    {
      return default(string);
    }

    public override void UrlEncode(string s, TextWriter output)
    {
    }

    public override string UrlPathEncode(string s)
    {
      return default(string);
    }

    public override byte[] UrlTokenDecode(string input)
    {
      return default(byte[]);
    }

    public override string UrlTokenEncode(byte[] input)
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public override string MachineName
    {
      get
      {
        return default(string);
      }
    }

    public override int ScriptTimeout
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
