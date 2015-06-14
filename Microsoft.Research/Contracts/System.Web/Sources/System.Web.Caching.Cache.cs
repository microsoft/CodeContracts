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

// File System.Web.Caching.Cache.cs
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
  sealed public partial class Cache : System.Collections.IEnumerable
  {
    #region Methods and constructors
    public Object Add(string key, Object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
    {
      return default(Object);
    }

    public Cache()
    {
    }

    public Object Get(string key)
    {
      return default(Object);
    }

    public System.Collections.IDictionaryEnumerator GetEnumerator()
    {
      return default(System.Collections.IDictionaryEnumerator);
    }

    public void Insert(string key, Object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
    {
    }

    public void Insert(string key, Object value)
    {
    }

    public void Insert(string key, Object value, CacheDependency dependencies)
    {
    }

    public void Insert(string key, Object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
    {
    }

    public void Insert(string key, Object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
    }

    public Object Remove(string key)
    {
      return default(Object);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }
    #endregion

    #region Properties and indexers
    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public long EffectivePercentagePhysicalMemoryLimit
    {
      get
      {
        return default(long);
      }
    }

    public long EffectivePrivateBytesLimit
    {
      get
      {
        return default(long);
      }
    }

    public Object this [string key]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static DateTime NoAbsoluteExpiration;
    public readonly static TimeSpan NoSlidingExpiration;
    #endregion
  }
}
