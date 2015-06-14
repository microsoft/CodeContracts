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

// File System.Web.HttpServerUtilityBase.cs
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
  abstract public partial class HttpServerUtilityBase
  {
    #region Methods and constructors
    public virtual new void ClearError()
    {
    }

    public virtual new Object CreateObject(Type type)
    {
      return default(Object);
    }

    public virtual new Object CreateObject(string progID)
    {
      return default(Object);
    }

    public virtual new Object CreateObjectFromClsid(string clsid)
    {
      return default(Object);
    }

    public virtual new void Execute(string path, TextWriter writer, bool preserveForm)
    {
    }

    public virtual new void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
    {
    }

    public virtual new void Execute(string path, bool preserveForm)
    {
    }

    public virtual new void Execute(string path)
    {
    }

    public virtual new void Execute(string path, TextWriter writer)
    {
    }

    public virtual new Exception GetLastError()
    {
      return default(Exception);
    }

    public virtual new void HtmlDecode(string s, TextWriter output)
    {
    }

    public virtual new string HtmlDecode(string s)
    {
      return default(string);
    }

    public virtual new void HtmlEncode(string s, TextWriter output)
    {
    }

    public virtual new string HtmlEncode(string s)
    {
      return default(string);
    }

    protected HttpServerUtilityBase()
    {
    }

    public virtual new string MapPath(string path)
    {
      return default(string);
    }

    public virtual new void Transfer(string path, bool preserveForm)
    {
    }

    public virtual new void Transfer(string path)
    {
    }

    public virtual new void Transfer(IHttpHandler handler, bool preserveForm)
    {
    }

    public virtual new void TransferRequest(string path, bool preserveForm)
    {
    }

    public virtual new void TransferRequest(string path)
    {
    }

    public virtual new void TransferRequest(string path, bool preserveForm, string method, System.Collections.Specialized.NameValueCollection headers)
    {
    }

    public virtual new void UrlDecode(string s, TextWriter output)
    {
    }

    public virtual new string UrlDecode(string s)
    {
      return default(string);
    }

    public virtual new void UrlEncode(string s, TextWriter output)
    {
    }

    public virtual new string UrlEncode(string s)
    {
      return default(string);
    }

    public virtual new string UrlPathEncode(string s)
    {
      return default(string);
    }

    public virtual new byte[] UrlTokenDecode(string input)
    {
      return default(byte[]);
    }

    public virtual new string UrlTokenEncode(byte[] input)
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public virtual new string MachineName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new int ScriptTimeout
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
