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

// File System.Web.HttpResponseWrapper.cs
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
  public partial class HttpResponseWrapper : HttpResponseBase
  {
    #region Methods and constructors
    public override void AddCacheDependency(System.Web.Caching.CacheDependency[] dependencies)
    {
    }

    public override void AddCacheItemDependencies(string[] cacheKeys)
    {
    }

    public override void AddCacheItemDependencies(System.Collections.ArrayList cacheKeys)
    {
    }

    public override void AddCacheItemDependency(string cacheKey)
    {
    }

    public override void AddFileDependencies(string[] filenames)
    {
    }

    public override void AddFileDependencies(System.Collections.ArrayList filenames)
    {
    }

    public override void AddFileDependency(string filename)
    {
    }

    public override void AddHeader(string name, string value)
    {
    }

    public override void AppendCookie(HttpCookie cookie)
    {
    }

    public override void AppendHeader(string name, string value)
    {
    }

    public override void AppendToLog(string param)
    {
    }

    public override string ApplyAppPathModifier(string virtualPath)
    {
      return default(string);
    }

    public override void BinaryWrite(byte[] buffer)
    {
    }

    public override void Clear()
    {
    }

    public override void ClearContent()
    {
    }

    public override void ClearHeaders()
    {
    }

    public override void Close()
    {
    }

    public override void DisableKernelCache()
    {
    }

    public override void End()
    {
    }

    public override void Flush()
    {
    }

    public HttpResponseWrapper(HttpResponse httpResponse)
    {
    }

    public override void Pics(string value)
    {
    }

    public override void Redirect(string url)
    {
    }

    public override void Redirect(string url, bool endResponse)
    {
    }

    public override void RedirectPermanent(string url, bool endResponse)
    {
    }

    public override void RedirectPermanent(string url)
    {
    }

    public override void RemoveOutputCacheItem(string path, string providerName)
    {
    }

    public override void RemoveOutputCacheItem(string path)
    {
    }

    public override void SetCookie(HttpCookie cookie)
    {
    }

    public override void TransmitFile(string filename, long offset, long length)
    {
    }

    public override void TransmitFile(string filename)
    {
    }

    public override void Write(char[] buffer, int index, int count)
    {
    }

    public override void Write(Object obj)
    {
    }

    public override void Write(string s)
    {
    }

    public override void Write(char ch)
    {
    }

    public override void WriteFile(string filename, long offset, long size)
    {
    }

    public override void WriteFile(IntPtr fileHandle, long offset, long size)
    {
    }

    public override void WriteFile(string filename)
    {
    }

    public override void WriteFile(string filename, bool readIntoMemory)
    {
    }

    public override void WriteSubstitution(HttpResponseSubstitutionCallback callback)
    {
    }
    #endregion

    #region Properties and indexers
    public override bool Buffer
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override bool BufferOutput
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override HttpCachePolicyBase Cache
    {
      get
      {
        return default(HttpCachePolicyBase);
      }
    }

    public override string CacheControl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string Charset
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override Encoding ContentEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public override string ContentType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override HttpCookieCollection Cookies
    {
      get
      {
        return default(HttpCookieCollection);
      }
    }

    public override int Expires
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public override DateTime ExpiresAbsolute
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public override Stream Filter
    {
      get
      {
        return default(Stream);
      }
      set
      {
      }
    }

    public override Encoding HeaderEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public override System.Collections.Specialized.NameValueCollection Headers
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public override bool IsClientConnected
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsRequestBeingRedirected
    {
      get
      {
        return default(bool);
      }
    }

    public override TextWriter Output
    {
      get
      {
        return default(TextWriter);
      }
    }

    public override Stream OutputStream
    {
      get
      {
        return default(Stream);
      }
    }

    public override string RedirectLocation
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string Status
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override int StatusCode
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public override string StatusDescription
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override int SubStatusCode
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public override bool SuppressContent
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override bool TrySkipIisCustomErrors
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}
