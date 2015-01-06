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

// File System.Runtime.Caching.MemoryCache.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;
using System.Linq;

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


namespace System.Runtime.Caching
{
  public partial class MemoryCache : ObjectCache, System.Collections.IEnumerable, IDisposable
  {
    #region Methods and constructors
      public override Object AddOrGetExisting(string key, Object value, CacheItemPolicy policy, string regionName)
      {
          return default(Object);
      }

      public override CacheItem AddOrGetExisting(CacheItem item, CacheItemPolicy policy)
      {
          return default(CacheItem);
      }

      public override Object AddOrGetExisting(string key, Object value, DateTimeOffset absoluteExpiration, string regionName)
      {
          return default(Object);
      }

      public override bool Contains(string key, string regionName)
      {
          return default(bool);
      }

      public override CacheEntryChangeMonitor CreateCacheEntryChangeMonitor(IEnumerable<string> keys, string regionName)
      {
          return default(CacheEntryChangeMonitor);
      }

      public void Dispose()
      {
      }

      public override Object Get(string key, string regionName)
      {
          return default(Object);
      }

      public override CacheItem GetCacheItem(string key, string regionName)
      {
          return default(CacheItem);
      }

      public override long GetCount(string regionName)
      {
          return default(long);
      }

      protected override IEnumerator<KeyValuePair<string, Object>> GetEnumerator()
      {
          return default(IEnumerator<KeyValuePair<string, Object>>);
      }

      public override IDictionary<string, Object> GetValues(IEnumerable<string> keys, string regionName)
      {
          return default(IDictionary<string, Object>);
      }

      public MemoryCache(string name, System.Collections.Specialized.NameValueCollection config)
      {
          Contract.Requires(!String.IsNullOrEmpty(name));
          Contract.Requires(!String.Equals(name, "default", StringComparison.OrdinalIgnoreCase));
      }

      public override Object Remove(string key, string regionName)
      {
          return default(Object);
      }

      public override void Set(string key, Object value, CacheItemPolicy policy, string regionName)
      {
      }

      public override void Set(string key, Object value, DateTimeOffset absoluteExpiration, string regionName)
      {
      }

      public override void Set(CacheItem item, CacheItemPolicy policy)
      {
      }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
          return default(System.Collections.IEnumerator);
      }

      public long Trim(int percent)
      {
          return default(long);
      }
    #endregion

    #region Properties and indexers
    public long CacheMemoryLimit
    {
      get
      {
        return default(long);
      }
    }

    public static System.Runtime.Caching.MemoryCache Default
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Runtime.Caching.MemoryCache>() != null);
        return default(System.Runtime.Caching.MemoryCache);
      }
    }

    public override DefaultCacheCapabilities DefaultCacheCapabilities
    {
      get
      {
        return default(DefaultCacheCapabilities);
      }
    }

    public override Object this [string key]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public override string Name
    {
      get
      {
        return default(string);
      }
    }

    public long PhysicalMemoryLimit
    {
      get
      {
        return default(long);
      }
    }

    public TimeSpan PollingInterval
    {
      get
      {
        return default(TimeSpan);
      }
    }
    #endregion
  }
}
