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

// File System.Net.HttpListenerResponse.cs
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


namespace System.Net
{
  sealed public partial class HttpListenerResponse : IDisposable
  {
    #region Methods and constructors
    public void Abort()
    {
    }

    public void AddHeader(string name, string value)
    {
      Contract.Requires(this.Headers != null);
    }

    public void AppendCookie(Cookie cookie)
    {
    }

    public void AppendHeader(string name, string value)
    {
      Contract.Requires(this.Headers != null);
    }

    public void Close(byte[] responseEntity, bool willBlock)
    {
    }

    public void Close()
    {
    }

    public void CopyFrom(HttpListenerResponse templateResponse)
    {
      Contract.Requires(templateResponse != null);
    }

    internal HttpListenerResponse()
    {
    }

    public void Redirect(string url)
    {
      Contract.Requires(this.Headers != null);
      Contract.Ensures(this.StatusCode == 302);
    }

    public void SetCookie(Cookie cookie)
    {
    }

    void System.IDisposable.Dispose()
    {
    }
    #endregion

    #region Properties and indexers
    public Encoding ContentEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public long ContentLength64
    {
      get
      {
        return default(long);
      }
      set
      {
      }
    }

    public string ContentType
    {
      get
      {
        Contract.Requires(this.Headers != null);

        return default(string);
      }
      set
      {
      }
    }

    public CookieCollection Cookies
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.CookieCollection>() != null);

        return default(CookieCollection);
      }
      set
      {
      }
    }

    public WebHeaderCollection Headers
    {
      get
      {
        return default(WebHeaderCollection);
      }
      set
      {
        Contract.Requires(value != null);
        Contract.Requires(value.AllKeys != null);
        Contract.Ensures(0 <= value.AllKeys.Length);
      }
    }

    public bool KeepAlive
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Stream OutputStream
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

        return default(Stream);
      }
    }

    public Version ProtocolVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Version>() != null);

        return default(Version);
      }
      set
      {
        Contract.Ensures((value.Minor - value.Major) <= 0);
        Contract.Ensures(0 <= value.Minor);
        Contract.Ensures(value.Major == 1);
      }
    }

    public string RedirectLocation
    {
      get
      {
        Contract.Requires(this.Headers != null);

        return default(string);
      }
      set
      {
      }
    }

    public bool SendChunked
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int StatusCode
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 65535);

        return default(int);
      }
      set
      {
      }
    }

    public string StatusDescription
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
      }
    }
    #endregion
  }
}
