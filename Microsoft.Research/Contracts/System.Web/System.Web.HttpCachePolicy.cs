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

// File System.Web.HttpCachePolicy.cs
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
  sealed public partial class HttpCachePolicy
  {
    #region Methods and constructors

    internal HttpCachePolicy() { }

    public void AddValidationCallback (HttpCacheValidateHandler handler, Object data)
    {
    }

    public void AppendCacheExtension (string extension)
    {
    }

    public void SetAllowResponseInBrowserHistory (bool allow)
    {
    }

    public void SetCacheability (HttpCacheability cacheability, string field)
    {
    }

    public void SetCacheability (HttpCacheability cacheability)
    {
    }

    public void SetETag (string etag)
    {
    }

    public void SetETagFromFileDependencies ()
    {
    }

    public void SetExpires (DateTime date)
    {
    }

    public void SetLastModified (DateTime date)
    {
    }

    public void SetLastModifiedFromFileDependencies ()
    {
    }

    public void SetMaxAge (TimeSpan delta)
    {
    }

    public void SetNoServerCaching ()
    {
    }

    public void SetNoStore ()
    {
    }

    public void SetNoTransforms ()
    {
    }

    public void SetOmitVaryStar (bool omit)
    {
    }

    public void SetProxyMaxAge (TimeSpan delta)
    {
    }

    public void SetRevalidation (HttpCacheRevalidation revalidation)
    {
    }

    public void SetSlidingExpiration (bool slide)
    {
    }

    public void SetValidUntilExpires (bool validUntilExpires)
    {
    }

    public void SetVaryByCustom (string custom)
    {
    }
    #endregion

    #region Properties and indexers
    public HttpCacheVaryByContentEncodings VaryByContentEncodings
    {
      get
      {
        return default(HttpCacheVaryByContentEncodings);
      }
    }

    public HttpCacheVaryByHeaders VaryByHeaders
    {
      get
      {
        return default(HttpCacheVaryByHeaders);
      }
    }

    public HttpCacheVaryByParams VaryByParams
    {
      get
      {
        return default(HttpCacheVaryByParams);
      }
    }
    #endregion
  }
}
