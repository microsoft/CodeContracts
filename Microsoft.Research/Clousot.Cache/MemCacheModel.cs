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

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.CodeAnalysis.Caching.Models;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis.Caching
{
  struct Table<T>
  {
    object lockValue;
    List<T> list;

    public Table(int dummy)
    {
      this.lockValue = new Object();
      this.list = new List<T>();
    }

    public object Lock { get { return this.lockValue; } }

    public T Find(Predicate<T> p)
    {
      Contract.Requires(p != null);

      lock (this.lockValue)
      {
        Contract.Assume(this.list != null);
        return this.list.Find(p);
      }
    }

    public void Remove(Predicate<T> p)
    {
      Contract.Requires(p != null);

      lock (this.lockValue)
      {
        Contract.Assume(this.list != null);
        var i = this.list.FindIndex(p);
        if (i >= 0)
        {
          this.list.RemoveAt(i);
        }
      }
    }

    public void AddOrUpdate(Predicate<T> p, T value)
    {
      Contract.Requires(p != null);

      lock (this.lockValue)
      {
        Contract.Assume(this.list != null);
        var i = list.FindIndex(p);
        if (i >= 0)
        {
          list[i] = value;
        }
        else
        {
          list.Add(value);
        }
      }
    }

    public ICollection<T> Entries { get { return this.list; } }
  }

  [ContractVerification(false)]
  public class MemCacheModel : ICacheModel
  {
    Table<Method> methodTable = new Table<Method>(0);
    Table<Metadata> metadataTable = new Table<Metadata>(0);
    Table<AssemblyInfo> assemblyInfoTable = new Table<AssemblyInfo>(0);
    Table<BaselineMethod> baselineTable = new Table<BaselineMethod>(0);
    readonly object idHashLock = new object();
    public readonly Dictionary<ByteArray, SortedList<DateTime, ByteArray>> methodHashFromId = new Dictionary<ByteArray, SortedList<DateTime, ByteArray>>();

    public IEnumerable<Method> Methods
    {
      get { return this.methodTable.Entries.AssumeNotNull(); }
    }

    public IEnumerable<Metadata> Metadatas
    {
      get { return this.metadataTable.Entries.AssumeNotNull(); }
    }

    public IEnumerable<AssemblyInfo> AssemblyInfoes
    {
      get { return this.assemblyInfoTable.Entries.AssumeNotNull(); }
    }

    public Metadata MetadataByKey(string key)
    {
      return this.metadataTable.Find(m => m.Key == key);
    }

    public Method MethodByHash(byte[] hash)
    {
      return this.methodTable.Find(m => m.Hash.ContentEquals(hash));
    }

    public Method BaselineByName(byte[] methodFullNameHash, string baselineId)
    {
      var candidate = this.baselineTable.Find(m => m.MethodFullNameHash.ContentEquals(methodFullNameHash) && m.BaselineId == baselineId);
      if (candidate != null) return candidate.Method;
      return null;
    }

    public Metadata NewMetadata() { return new Metadata(); }

    public Method NewMethod()
    {
      return new Method();
    }

    public AssemblyInfo GetOrCreateAssemblyInfo(Guid guid)
    {
      return new AssemblyInfo { AssemblyId = guid };
    }

    public IdHashTimeToMethod NewHashDateBindingForNow(DataStructures.ByteArray methodIdHash, Method methodModel)
    {
      var result = new IdHashTimeToMethod { Method = methodModel, MethodIdHash = methodIdHash.Bytes, Time = DateTime.Now };
      return result;
    }

    public Outcome NewOutcome(Method method)
    {
      var result = new Outcome { Method = method };
      method.Outcomes.Add(result);
      return result;
    }

    public Suggestion NewSuggestion(Method method)
    {
      var result = new Suggestion { Method = method };
      method.Suggestions.Add(result);
      return result;
    }

    public OutcomeContext NewOutcomeContext(Outcome outcome)
    {
      var result = new OutcomeContext { Outcome = outcome };
      outcome.OutcomeContexts.Add(result);
      return result;
    }

    public ContextEdge NewContextEdge(OutcomeOrSuggestion item)
    {
      Outcome outcome = item as Outcome;
      if (outcome != null)
      {
        var result = new OutcomeContextEdge { Outcome = outcome };
        outcome.OutcomeContextEdges.Add(result);
        return result;
      }
      Suggestion sugg = item as Suggestion;
      if (sugg != null)
      {
        var result = new SuggestionContextEdge { Suggestion = sugg };
        sugg.SuggestionContextEdges.Add(result);
        return result;
      }
      return null;
    }


    public void DeleteMethodModel(Method methodModel)
    {
      this.methodTable.Remove(m => m.Hash.ContentEquals(methodModel.Hash));
    }

    public ByteArray GetHashForDate(ByteArray methodIdHash, DateTime t, bool afterT)
    {
      lock (this.idHashLock)
      {
        SortedList<DateTime, ByteArray> hashes;
        if (!this.methodHashFromId.TryGetValue(methodIdHash, out hashes))
          return null;
        Contract.Assume(hashes != null);
        ByteArray hash;
        if (afterT)
        {
          // hash = hashes.Where(entry => entry.Key >= t).FirstOrDefault().Value;

          hash = hashes.OrderBy(entry => entry.Key).FirstOrDefault().Value;
          return hash;
        }
        else
        {
          if (hashes.TryUpperBound(t, out hash))
            return hash;
        }
        return null;
      }
    }

    public bool IsValid
    {
      get { return true; }
    }

    public bool IsFresh
    {
      get { return true; }
    }

    public void SaveChanges(bool now)
    {
    }


    public void AddOrUpdate(AssemblyInfo ainfo)
    {
      this.assemblyInfoTable.AddOrUpdate(ai => ai.AssemblyId == ainfo.AssemblyId, ainfo);
    }

    public void AddOrUpdate(Metadata value)
    {
      this.metadataTable.AddOrUpdate(md => md.Key == value.Key, value);
    }

    public void AddOrUpdate(Method methodModel)
    {
      this.methodTable.AddOrUpdate(m => m.Hash.ContentEquals(methodModel.Hash), methodModel);
    }

    public void AddOrUpdate(BaselineMethod baseline)
    {
      this.baselineTable.AddOrUpdate(m => m.MethodFullNameHash.ContentEquals(baseline.MethodFullNameHash) && m.BaselineId == baseline.BaselineId, baseline);
    }

    public void AddOrUpdate(IdHashTimeToMethod idhash)
    {
      lock (this.idHashLock)
      {
        var datetime = idhash.Time;
        SortedList<DateTime, ByteArray> hashes;
        if (!this.methodHashFromId.TryGetValue(idhash.MethodIdHash, out hashes))
        {
          this.methodHashFromId.Add(idhash.MethodIdHash, hashes = new SortedList<DateTime, ByteArray>());
        }
        else
        {
          Contract.Assume(hashes != null, "we do not know the invariant on methodHashFromId");
        }
        Console.WriteLine("[cache] Adding an entry in the method cache model at time: {0}", datetime);

        hashes[datetime] = idhash.Method.Hash;
      }
    }

    public void AddOrUpdate(Method method, AssemblyInfo assemblyInfo)
    {
        lock (this.assemblyInfoTable.Lock)
        {
            if (!method.Assemblies.Where(a => a.AssemblyId == assemblyInfo.AssemblyId).Any())
            {
                method.Assemblies.Add(assemblyInfo);
            }
        }
    }

    public void Dispose()
    {
    }


    public string CacheName
    {
      get { return "Memory cache"; }
    }
  }

  public class MemClousotCacheFactory : IClousotCacheFactory
  {
    public MemClousotCacheFactory()
    {
    }

    public IClousotCache Create(IClousotCacheOptions options)
    {
      Contract.Ensures(Contract.Result<IClousotCache>() != null);

      var model = new MemCacheModel();
      return new ClousotCache(model, options);
    }
  }

}
