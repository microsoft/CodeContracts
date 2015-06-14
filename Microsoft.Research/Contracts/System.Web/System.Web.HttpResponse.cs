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

// File System.Web.HttpResponse.cs
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
  sealed public partial class HttpResponse
  {
    #region Methods and constructors
    public void AddCacheDependency (System.Web.Caching.CacheDependency[] dependencies)
    {
    }

    public void AddCacheItemDependencies (string[] cacheKeys)
    {
    }

    public void AddCacheItemDependencies (System.Collections.ArrayList cacheKeys)
    {
    }

    public void AddCacheItemDependency (string cacheKey)
    {
    }

    public void AddFileDependencies (System.Collections.ArrayList filenames)
    {
    }

    public void AddFileDependencies (string[] filenames)
    {
    }

    public void AddFileDependency (string filename)
    {
    }

    public void AddHeader (string name, string value)
    {
    }

    public void AppendCookie (HttpCookie cookie)
    {
    }

    public void AppendHeader (string name, string value)
    {
    }

    public void AppendToLog (string param)
    {
    }

    public string ApplyAppPathModifier (string virtualPath)
    {
      return default(string);
    }

    public void BinaryWrite (byte[] buffer)
    {
      Contract.Requires (buffer != null);
    }

    public void Clear ()
    {
    }

    public void ClearContent ()
    {
    }

    public void ClearHeaders ()
    {
    }

    public void Close ()
    {
    }

    public void DisableKernelCache ()
    {
    }

    public void End ()
    {
    }

    public void Flush ()
    {
    }

    public HttpResponse (TextWriter writer)
    {
    }

    public void Pics (string value)
    {
    }

    public void Redirect (string url)
    {
    }

#if NETFRAMEWORK_4_0
    public void Redirect (string url, bool endResponse)
    {
    }

    public void RedirectPermanent (string url)
    {
    }

    public void RedirectPermanent (string url, bool endResponse)
    {
    }

    public void RedirectToRoute (Object routeValues)
    {
    }

    public void RedirectToRoute (string routeName, Object routeValues)
    {
    }

    public void RedirectToRoute (string routeName)
    {
    }

    public void RedirectToRoute (System.Web.Routing.RouteValueDictionary routeValues)
    {
    }

    public void RedirectToRoute (string routeName, System.Web.Routing.RouteValueDictionary routeValues)
    {
    }

    public void RedirectToRoutePermanent (string routeName, Object routeValues)
    {
    }

    public void RedirectToRoutePermanent (string routeName, System.Web.Routing.RouteValueDictionary routeValues)
    {
    }

    public void RedirectToRoutePermanent (string routeName)
    {
    }

    public void RedirectToRoutePermanent (System.Web.Routing.RouteValueDictionary routeValues)
    {
    }

    public void RedirectToRoutePermanent (Object routeValues)
    {
    }

    public static void RemoveOutputCacheItem (string path, string providerName)
    {
    }
#endif

    public static void RemoveOutputCacheItem (string path)
    {
    }

    public void SetCookie (HttpCookie cookie)
    {
    }

    public void TransmitFile (string filename, long offset, long length)
    {
    }

    public void TransmitFile (string filename)
    {
    }

    public void Write (char ch)
    {
    }

    public void Write (char[] buffer, int index, int count)
    {
    }

    public void Write (Object obj)
    {
    }

    public void Write (string s)
    {
    }

    public void WriteFile (IntPtr fileHandle, long offset, long size)
    {
    }

    public void WriteFile (string filename, bool readIntoMemory)
    {
    }

    public void WriteFile (string filename, long offset, long size)
    {
    }

    public void WriteFile (string filename)
    {
    }

    public void WriteSubstitution (HttpResponseSubstitutionCallback callback)
    {
      Contract.Requires (callback != null);
    }
    #endregion

    #region Properties and indexers
    public bool Buffer
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool BufferOutput
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public HttpCachePolicy Cache
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.HttpCachePolicy>() != null);

        return default(HttpCachePolicy);
      }
    }

    public string CacheControl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Charset
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Encoding ContentEncoding
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Text.Encoding>() != null);

        return default(Encoding);
      }
      set
      {
      }
    }

    public string ContentType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public HttpCookieCollection Cookies
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.HttpCookieCollection>() != null);

        return default(HttpCookieCollection);
      }
    }

    public int Expires
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public DateTime ExpiresAbsolute
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public Stream Filter
    {
      get
      {
        return default(Stream);
      }
      set
      {
      }
    }

    public Encoding HeaderEncoding
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Text.Encoding>() != null);

        return default(Encoding);
      }
      set
      {
      }
    }

    public System.Collections.Specialized.NameValueCollection Headers
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Collections.Specialized.NameValueCollection>() != null);

        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public bool IsClientConnected
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsRequestBeingRedirected
    {
      get
      {
        return default(bool);
      }
    }

    public TextWriter Output
    {
      get
      {
        return default(TextWriter);
      }
    }

    public Stream OutputStream
    {
      get
      {
        return default(Stream);
      }
    }

    public string RedirectLocation
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Status
    {
      get
      {
        return default(string);
      }
      set
      {
        Contract.Requires (value != null);
      }
    }

    public int StatusCode
    {
      get
      {
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
        return default(string);
      }
      set
      {
      }
    }

    public int SubStatusCode
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public bool SuppressContent
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool TrySkipIisCustomErrors
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
