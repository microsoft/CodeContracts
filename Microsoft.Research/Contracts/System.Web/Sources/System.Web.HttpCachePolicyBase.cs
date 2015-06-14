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

// File System.Web.HttpCachePolicyBase.cs
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
  abstract public partial class HttpCachePolicyBase
  {
    #region Methods and constructors
    public virtual new void AddValidationCallback(HttpCacheValidateHandler handler, Object data)
    {
    }

    public virtual new void AppendCacheExtension(string extension)
    {
    }

    protected HttpCachePolicyBase()
    {
    }

    public virtual new void SetAllowResponseInBrowserHistory(bool allow)
    {
    }

    public virtual new void SetCacheability(HttpCacheability cacheability, string field)
    {
    }

    public virtual new void SetCacheability(HttpCacheability cacheability)
    {
    }

    public virtual new void SetETag(string etag)
    {
    }

    public virtual new void SetETagFromFileDependencies()
    {
    }

    public virtual new void SetExpires(DateTime date)
    {
    }

    public virtual new void SetLastModified(DateTime date)
    {
    }

    public virtual new void SetLastModifiedFromFileDependencies()
    {
    }

    public virtual new void SetMaxAge(TimeSpan delta)
    {
    }

    public virtual new void SetNoServerCaching()
    {
    }

    public virtual new void SetNoStore()
    {
    }

    public virtual new void SetNoTransforms()
    {
    }

    public virtual new void SetOmitVaryStar(bool omit)
    {
    }

    public virtual new void SetProxyMaxAge(TimeSpan delta)
    {
    }

    public virtual new void SetRevalidation(HttpCacheRevalidation revalidation)
    {
    }

    public virtual new void SetSlidingExpiration(bool slide)
    {
    }

    public virtual new void SetValidUntilExpires(bool validUntilExpires)
    {
    }

    public virtual new void SetVaryByCustom(string custom)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new HttpCacheVaryByContentEncodings VaryByContentEncodings
    {
      get
      {
        return default(HttpCacheVaryByContentEncodings);
      }
    }

    public virtual new HttpCacheVaryByHeaders VaryByHeaders
    {
      get
      {
        return default(HttpCacheVaryByHeaders);
      }
    }

    public virtual new HttpCacheVaryByParams VaryByParams
    {
      get
      {
        return default(HttpCacheVaryByParams);
      }
    }
    #endregion
  }
}
