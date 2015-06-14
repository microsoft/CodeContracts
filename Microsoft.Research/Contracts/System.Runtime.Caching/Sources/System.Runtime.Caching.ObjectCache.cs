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

// File System.Runtime.Caching.ObjectCache.cs
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
  abstract public partial class ObjectCache : IEnumerable<KeyValuePair<string, Object>>, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public virtual new bool Add(string key, Object value, DateTimeOffset absoluteExpiration, string regionName)
    {
      return default(bool);
    }

    public virtual new bool Add(CacheItem item, CacheItemPolicy policy)
    {
      return default(bool);
    }

    public virtual new bool Add(string key, Object value, CacheItemPolicy policy, string regionName)
    {
      return default(bool);
    }

    public abstract Object AddOrGetExisting(string key, Object value, DateTimeOffset absoluteExpiration, string regionName);

    public abstract CacheItem AddOrGetExisting(CacheItem value, CacheItemPolicy policy);

    public abstract Object AddOrGetExisting(string key, Object value, CacheItemPolicy policy, string regionName);

    public abstract bool Contains(string key, string regionName);

    public abstract CacheEntryChangeMonitor CreateCacheEntryChangeMonitor(IEnumerable<string> keys, string regionName);

    public abstract Object Get(string key, string regionName);

    public abstract CacheItem GetCacheItem(string key, string regionName);

    public abstract long GetCount(string regionName);

    protected abstract IEnumerator<KeyValuePair<string, Object>> GetEnumerator();

    public virtual new IDictionary<string, Object> GetValues(string regionName, string[] keys)
    {
      return default(IDictionary<string, Object>);
    }

    public abstract IDictionary<string, Object> GetValues(IEnumerable<string> keys, string regionName);

    protected ObjectCache()
    {
    }

    public abstract Object Remove(string key, string regionName);

    public abstract void Set(string key, Object value, DateTimeOffset absoluteExpiration, string regionName);

    public abstract void Set(CacheItem item, CacheItemPolicy policy);

    public abstract void Set(string key, Object value, CacheItemPolicy policy, string regionName);

    IEnumerator<KeyValuePair<string, Object>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.GetEnumerator()
    {
      return default(IEnumerator<KeyValuePair<string, Object>>);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }
    #endregion

    #region Properties and indexers
    public abstract DefaultCacheCapabilities DefaultCacheCapabilities
    {
      get;
    }

    public static IServiceProvider Host
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IServiceProvider>() != null);

        return default(IServiceProvider);
      }
      set
      {
      }
    }

    public abstract Object this [string key]
    {
      get;
      set;
    }

    public abstract string Name
    {
      get;
    }
    #endregion

    #region Fields
    public readonly static DateTimeOffset InfiniteAbsoluteExpiration;
    public readonly static TimeSpan NoSlidingExpiration;
    #endregion
  }

  #region ObjectCache contract binding
  [ContractClassFor(typeof(ObjectCache))]
  abstract class ObjectCacheContract : ObjectCache
  {
      public override object AddOrGetExisting(string key, object value, DateTimeOffset absoluteExpiration, string regionName)
      {
          Contract.Requires(key != null);
          throw new NotImplementedException();
      }

      public override CacheItem AddOrGetExisting(CacheItem value, CacheItemPolicy policy)
      {
          Contract.Requires(value != null);
          Contract.Ensures(Contract.Result<CacheItem>() != null);
          throw new NotImplementedException();
      }

      public override object AddOrGetExisting(string key, object value, CacheItemPolicy policy, string regionName)
      {
          Contract.Requires(key != null);
          throw new NotImplementedException();
      }

      public override bool Contains(string key, string regionName)
      {
          Contract.Requires(key != null);
          throw new NotImplementedException();
      }

      public override CacheEntryChangeMonitor CreateCacheEntryChangeMonitor(IEnumerable<string> keys, string regionName)
      {
          Contract.Requires(keys != null);
          Contract.Requires(Contract.ForAll(keys, k => k != null));
          Contract.Ensures(Contract.Result<CacheEntryChangeMonitor>() != null);
          throw new NotImplementedException();
      }

      public override object Get(string key, string regionName)
      {
          Contract.Requires(key != null);
          throw new NotImplementedException();
      }

      public override CacheItem GetCacheItem(string key, string regionName)
      {
          Contract.Requires(key != null);
          throw new NotImplementedException();
      }

      public override long GetCount(string regionName)
      {
          Contract.Ensures(Contract.Result<long>() >= 0);      
          throw new NotImplementedException();
      }

      protected override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
      {
          throw new NotImplementedException();
      }

      public override IDictionary<string, object> GetValues(IEnumerable<string> keys, string regionName)
      {
          Contract.Requires(keys != null);
          Contract.Requires(Contract.ForAll(keys, k => k != null));
          Contract.Ensures(Contract.Result<IDictionary<string,object>>() != null);
          throw new NotImplementedException();
      }

      public override object Remove(string key, string regionName)
      {
          Contract.Requires(key != null);
          throw new NotImplementedException();
      }

      public override void Set(string key, object value, DateTimeOffset absoluteExpiration, string regionName)
      {
          Contract.Requires(key != null);
          throw new NotImplementedException();
      }

      public override void Set(CacheItem item, CacheItemPolicy policy)
      {
          Contract.Requires(item != null);
          throw new NotImplementedException();
      }

      public override void Set(string key, object value, CacheItemPolicy policy, string regionName)
      {
          Contract.Requires(key != null);
          throw new NotImplementedException();
      }

      public override DefaultCacheCapabilities DefaultCacheCapabilities
      {
          get { throw new NotImplementedException(); }
      }

      public override object this[string key]
      {
          get
          {
              Contract.Requires(key != null);
              throw new NotImplementedException();
          }
          set
          {
              Contract.Requires(key != null);
              throw new NotImplementedException();
          }
      }

      public override string Name
      {
          get { throw new NotImplementedException(); }
      }
  }
  #endregion

}
