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

// File System.Web.UI.ControlCachePolicy.cs
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


namespace System.Web.UI
{
  sealed public partial class ControlCachePolicy
  {
    #region Methods and constructors
    internal ControlCachePolicy() { }

    public void SetExpires (DateTime expirationTime)
    {
    }

    public void SetSlidingExpiration (bool useSlidingExpiration)
    {
    }

    public void SetVaryByCustom (string varyByCustom)
    {
    }
    #endregion

    #region Properties and indexers
    public bool Cached
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Web.Caching.CacheDependency Dependency
    {
      get
      {
        return default(System.Web.Caching.CacheDependency);
      }
      set
      {
      }
    }

    public TimeSpan Duration
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

 #if NETFRAMEWORK_4_0
    public string ProviderName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
#endif

    public bool SupportsCaching
    {
      get
      {
        return default(bool);
      }
    }

    public string VaryByControl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.HttpCacheVaryByParams VaryByParams
    {
      get
      {
        return default(System.Web.HttpCacheVaryByParams);
      }
    }
    #endregion
  }
}
