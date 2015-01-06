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

// File System.Web.Caching.CacheDependency.cs
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


namespace System.Web.Caching
{
  public partial class CacheDependency : IDisposable
  {
    #region Methods and constructors
    public CacheDependency(string[] filenames, string[] cachekeys, DateTime start)
    {
    }

    public CacheDependency(string[] filenames, string[] cachekeys)
    {
    }

    public CacheDependency(string[] filenames, string[] cachekeys, System.Web.Caching.CacheDependency dependency, DateTime start)
    {
    }

    public CacheDependency(string[] filenames, string[] cachekeys, System.Web.Caching.CacheDependency dependency)
    {
    }

    public CacheDependency(string[] filenames, DateTime start)
    {
    }

    public CacheDependency(string filename)
    {
    }

    protected CacheDependency()
    {
    }

    public CacheDependency(string[] filenames)
    {
    }

    public CacheDependency(string filename, DateTime start)
    {
    }

    protected virtual new void DependencyDispose()
    {
    }

    public void Dispose()
    {
    }

    protected internal void FinishInit()
    {
    }

    public virtual new string GetUniqueID()
    {
      return default(string);
    }

    protected void NotifyDependencyChanged(Object sender, EventArgs e)
    {
    }

    protected void SetUtcLastModified(DateTime utcLastModified)
    {
    }
    #endregion

    #region Properties and indexers
    public bool HasChanged
    {
      get
      {
        return default(bool);
      }
    }

    public DateTime UtcLastModified
    {
      get
      {
        return default(DateTime);
      }
    }
    #endregion
  }
}
