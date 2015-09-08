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

// File System.Web.HttpResponseBase.cs
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
  abstract public partial class HttpResponseBase
  {
    #region Methods and constructors
    public virtual new void AddCacheDependency(System.Web.Caching.CacheDependency[] dependencies)
    {
    }

    public virtual new void AddCacheItemDependencies(string[] cacheKeys)
    {
    }

    public virtual new void AddCacheItemDependencies(System.Collections.ArrayList cacheKeys)
    {
    }

    public virtual new void AddCacheItemDependency(string cacheKey)
    {
    }

    public virtual new void AddFileDependencies(string[] filenames)
    {
    }

    public virtual new void AddFileDependencies(System.Collections.ArrayList filenames)
    {
    }

    public virtual new void AddFileDependency(string filename)
    {
    }

    public virtual new void AddHeader(string name, string value)
    {
    }

    public virtual new void AppendCookie(HttpCookie cookie)
    {
    }

    public virtual new void AppendHeader(string name, string value)
    {
    }

    public virtual new void AppendToLog(string param)
    {
    }

    public virtual new string ApplyAppPathModifier(string virtualPath)
    {
      return default(string);
    }

    public virtual new void BinaryWrite(byte[] buffer)
    {
    }

    public virtual new void Clear()
    {
    }

    public virtual new void ClearContent()
    {
    }

    public virtual new void ClearHeaders()
    {
    }

    public virtual new void Close()
    {
    }

    public virtual new void DisableKernelCache()
    {
    }

    public virtual new void End()
    {
    }

    public virtual new void Flush()
    {
    }

    protected HttpResponseBase()
    {
    }

    public virtual new void Pics(string value)
    {
    }

    public virtual new void Redirect(string url, bool endResponse)
    {
    }

    public virtual new void Redirect(string url)
    {
    }

    public virtual new void RedirectPermanent(string url)
    {
    }

    public virtual new void RedirectPermanent(string url, bool endResponse)
    {
    }

    public virtual new void RedirectToRoute(System.Web.Routing.RouteValueDictionary routeValues)
    {
    }

    public virtual new void RedirectToRoute(string routeName, System.Web.Routing.RouteValueDictionary routeValues)
    {
    }

    public virtual new void RedirectToRoute(Object routeValues)
    {
    }

    public virtual new void RedirectToRoute(string routeName)
    {
    }

    public virtual new void RedirectToRoute(string routeName, Object routeValues)
    {
    }

    public virtual new void RedirectToRoutePermanent(string routeName, System.Web.Routing.RouteValueDictionary routeValues)
    {
    }

    public virtual new void RedirectToRoutePermanent(string routeName)
    {
    }

    public virtual new void RedirectToRoutePermanent(string routeName, Object routeValues)
    {
    }

    public virtual new void RedirectToRoutePermanent(Object routeValues)
    {
    }

    public virtual new void RedirectToRoutePermanent(System.Web.Routing.RouteValueDictionary routeValues)
    {
    }

    public virtual new void RemoveOutputCacheItem(string path)
    {
    }

    public virtual new void RemoveOutputCacheItem(string path, string providerName)
    {
    }

    public virtual new void SetCookie(HttpCookie cookie)
    {
    }

    public virtual new void TransmitFile(string filename)
    {
    }

    public virtual new void TransmitFile(string filename, long offset, long length)
    {
    }

    public virtual new void Write(Object obj)
    {
    }

    public virtual new void Write(string s)
    {
    }

    public virtual new void Write(char ch)
    {
    }

    public virtual new void Write(char[] buffer, int index, int count)
    {
    }

    public virtual new void WriteFile(string filename, long offset, long size)
    {
    }

    public virtual new void WriteFile(IntPtr fileHandle, long offset, long size)
    {
    }

    public virtual new void WriteFile(string filename)
    {
    }

    public virtual new void WriteFile(string filename, bool readIntoMemory)
    {
    }

    public virtual new void WriteSubstitution(HttpResponseSubstitutionCallback callback)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool Buffer
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool BufferOutput
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new HttpCachePolicyBase Cache
    {
      get
      {
        return default(HttpCachePolicyBase);
      }
    }

    public virtual new string CacheControl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string Charset
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new Encoding ContentEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public virtual new string ContentType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new HttpCookieCollection Cookies
    {
      get
      {
        return default(HttpCookieCollection);
      }
    }

    public virtual new int Expires
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new DateTime ExpiresAbsolute
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public virtual new Stream Filter
    {
      get
      {
        return default(Stream);
      }
      set
      {
      }
    }

    public virtual new Encoding HeaderEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public virtual new System.Collections.Specialized.NameValueCollection Headers
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public virtual new bool IsClientConnected
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsRequestBeingRedirected
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new TextWriter Output
    {
      get
      {
        return default(TextWriter);
      }
    }

    public virtual new Stream OutputStream
    {
      get
      {
        return default(Stream);
      }
    }

    public virtual new string RedirectLocation
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string Status
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new int StatusCode
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new string StatusDescription
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new int SubStatusCode
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new bool SuppressContent
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool TrySkipIisCustomErrors
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
