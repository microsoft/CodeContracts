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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.CodeAnalysis.Caching.Models;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching
{
#if false
  class MemoryCacheDataAccessor : EntityCacheDataAccessor<MemMethodModel, MemCachingMetadata, MemAssemblyInfo, MemAssemblyBinding, MemoryClousotCacheEntities>
  {
    private readonly MemoryClousotCacheEntities persistedClousotCache;
    private readonly object LockAssemblyInfoes = new object();
    private readonly object LockMethods = new object();

    public MemoryCacheDataAccessor(Dictionary<string, byte[]> metadataForCreation)
     : base(Int32.MaxValue, new CacheVersionParameters { SetBaseLine = false, Version = 0 })
    {
      this.persistedClousotCache = new MemoryClousotCacheEntities();
      this.persistedClousotCache.cachingMetadatas.AddRange(metadataForCreation.Select(md => new MemCachingMetadata() { Key = md.Key, Value = md.Value }));
    }

    protected override MemoryClousotCacheEntities CreateClousotCacheEntities(bool silent) { return this.persistedClousotCache; }

    public override bool IsValid { get { return true; } }

    public override void Clear()
    {
      lock (this.LockMethods)
        this.persistedClousotCache.methodModels.Clear();
      lock (this.LockAssemblyInfoes)
        this.persistedClousotCache.assemblyInfoes.Clear();
      this.persistedClousotCache.cachingMetadatas.Clear();
    }

    public override bool TryGetMethodModelForHash(ByteArray hash, out IMethodModel result)
    {
      lock (this.LockMethods)
        return base.TryGetMethodModelForHash(hash, out result);
    }

    public override bool TryGetMethodModelForName(string name, out IMethodModel result)
    {
      lock (this.LockMethods)
        return base.TryGetMethodModelForName(name, out result);
    }

    public override void AddAssemblyBinding(Guid assemblyGuid, IMethodModel methodModel)
    {
      lock (this.LockMethods)
        base.AddAssemblyBinding(assemblyGuid, methodModel);
    }

    public override void AddAssemblyInfo(string assemblyName, Guid assemblyGuid)
    {
      lock (this.LockAssemblyInfoes)
        base.AddAssemblyInfo(assemblyName, assemblyGuid);
    }

    public override void TryAddMethodModel(IMethodModel methodModel, ByteArray methodId)
    {
      lock (this.LockMethods)
        base.TryAddMethodModel(methodModel, methodId);
    }

    public override void TrySaveChanges(bool now = false) {; }
  }

  class UnclearableMemoryCacheDataAccessor : MemoryCacheDataAccessor
  {
    public UnclearableMemoryCacheDataAccessor(Dictionary<string, byte[]> metadata)
      : base(metadata)
    { }

    public override void Clear() { }
  }
#endif

  class SliceId : ISliceId
  {
    private static readonly object Lock = new object();
    private static int idgen;

    private readonly int id;
    private readonly string dll;

    public SliceId(string dll)
    {
      lock (Lock)
        this.id = ++idgen;
      this.dll = dll;
    }

    public int Id { get { return this.id; } }
    public string Dll { get { return this.dll; } }

    public override int GetHashCode() { return this.id; }

    public override string ToString() { return String.Format("{0}:{1}", this.id, this.dll); }

    public override bool Equals(object obj)
    {
      return this.Equals(obj as SliceId);
    }

    public bool Equals(ISliceId other)
    {
      return other != null && this.id == other.Id;
    }
  }

  class SliceHash : ByteArray, ISliceHash
  {
    public SliceHash(byte[] hash)
      : base(hash)
    { }

    public ByteArray Hash { get { return this; } }

    public bool Equals(ISliceHash other) { return other != null && this.Hash.Equals(other.Hash); }
  }

  // TODO: make the SliceDataAccessor into a DB to save memory
  class SliceDataAccessor : IDB
  {
    // TODO: do no act like everything is already know, because the DB can be accessed while the slices are still being built

    private static readonly ISliceId[] EmptySliceIdArray = new ISliceId[0];

    // Locks for the parallel cases
    private readonly object LockCacheAccessor = new Object();
    private readonly object LockDependencesCache = new Object();
    private readonly object LockComputed = new Object();

    private readonly IClousotCacheFactory[] cacheDataAccessorFactories; // Our standard DB
    private IClousotCache cacheAccessor;
    
    private readonly Dictionary<ISliceId, SliceDefinition> slices = new Dictionary<ISliceId, SliceDefinition>();
    private readonly Dictionary<MethodId, ISliceId> sliceOfMethod = new Dictionary<MethodId, ISliceId>();
    private readonly Dictionary<ISliceId, IEnumerable<ISliceId>> dependencesCache = new Dictionary<ISliceId, IEnumerable<ISliceId>>();
    private readonly Dictionary<ISliceId, IEnumerable<ISliceId>> callersCache = new Dictionary<ISliceId, IEnumerable<ISliceId>>();
    private readonly Set<Pair<ISliceId, ISliceHash>> computed = new Set<Pair<ISliceId, ISliceHash>>();

    public SliceDataAccessor(IClousotCacheFactory[] cacheDataAccessorFactories)
    {
      this.cacheDataAccessorFactories = cacheDataAccessorFactories;
    }

    public ISliceHash ComputeSliceHash(ISliceId sliceId, DateTime t)
    {
      // We do not consider the methods in the same type at the moment

      SliceDefinition sliceDef;

      // If we do not know the slice, we just return null
      if (!this.slices.TryGetValue(sliceId, out sliceDef))
        return null;

      // TODO: wire the hashing option
      using (var tw = new HashWriter(false))
      {
        foreach (var methodId in sliceDef.Dependencies)
        {
          tw.Write(methodId);
          tw.Write(':');

          var methodHash = this.GetHashForDate(methodId, t, false);
          Method methodModel;

          if (methodHash == null || !this.TryGetMethodModelForHash(methodHash, out methodModel))
            tw.WriteLine("<null>");
          else
            tw.WriteLine(this.GetResultHash(methodModel));
        }

        return new SliceHash(tw.GetHash());
      }
    }

    public ByteArray GetResultHash(Method methodModel)
    {
      Contract.Requires(methodModel != null);

      // Put in this hash only what the analysis of other methods will rely on (i.e. time stats would not be a good idea)

      return new ByteArray(methodModel.InferredExprHash, methodModel.PureParametersMask, methodModel.Timeout);
    }

    // Get the latest just before or after the time t 
    private ByteArray GetHashForDate(MethodId methodId, DateTime t, bool afterT)
    {
      if (this.cacheAccessor == null)
        return null;
      return this.cacheAccessor.GetHashForDate(methodId, t, afterT);
    }

    private bool TryGetMethodModelForHash(ByteArray methodHash, out Method methodModel)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out methodModel) != null);

      if (this.cacheAccessor == null)
      {
        methodModel = default(Method);
        return false;
      }
      return this.cacheAccessor.TryGetMethodModelForHash(methodHash, out methodModel);
    }

    public bool AlreadyComputed(ISliceId sliceId, ISliceHash sliceHash)
    {
      lock (this.LockComputed)
        return this.computed.Contains(Pair.For(sliceId, sliceHash));
    }

    public void MarkAsComputed(ISliceId sliceId, ISliceHash sliceHash)
    {
      lock (this.LockComputed)
        this.computed.Add(Pair.For(sliceId, sliceHash));
    }

    public ISliceId RegisterSlice(SliceDefinition sliceDef)
    {
      var sliceId = new SliceId(sliceDef.Dll);
      this.slices.Add(sliceId, sliceDef);
      foreach (var h in sliceDef.Methods)
        this.sliceOfMethod.Add(h, sliceId);
      lock (this.LockDependencesCache)
        this.dependencesCache.Clear(); // invalidate dependences because it can be based on outdated sliceOfMethod values
      return sliceId;
    }

    public IEnumerable<ISliceId> Dependences(ISliceId sliceId)
    {
      IEnumerable<ISliceId> res;
      lock (this.LockDependencesCache)
      {
        if (this.dependencesCache.TryGetValue(sliceId, out res))
        {
          return res;
        }
      }

      SliceDefinition sliceDef;
      if (!this.slices.TryGetValue(sliceId, out sliceDef))
      {
        return EmptySliceIdArray; // should not happen
      }

      var result = new Set<ISliceId>();
      ISliceId depSliceId;
      foreach (var m in sliceDef.Dependencies)
      {
        if (this.sliceOfMethod.TryGetValue(m, out depSliceId))
        {
          result.Add(depSliceId);
        }
      }
      lock (this.LockDependencesCache)
      {
        var callers = ComputeCallersDependencies(sliceId);
        result.AddRange(callers);

        this.dependencesCache.Add(sliceId, result);
      }
      return result;
    }

    private IEnumerable<ISliceId> ComputeCallersDependencies(ISliceId sliceId)
    {
      Contract.Requires(sliceId != null);

      lock (this.LockDependencesCache)
      {
        IEnumerable<ISliceId> cachedCallers;
        if (this.callersCache.TryGetValue(sliceId, out cachedCallers))
        {
          return cachedCallers;
        }

        var result = new Set<ISliceId>();

        // TODO: generalize to a set of methods?
        var methodIdForSliceId = this.sliceOfMethod.Where(pair => pair.Value.Equals(sliceId)).Select(pair => pair.Key).FirstOrDefault();

        if (methodIdForSliceId != null)
        {
          foreach (var slice in this.slices)
          {
            if (slice.Value.Dependencies.Contains(methodIdForSliceId))
            {
              // the slice depends on the method 
              result.Add(slice.Key);
            }
          }
        }

        this.callersCache.Add(sliceId, result);

        return result;
      }
    }

    public IEnumerable<ISliceId> SlicesForMethodsInTheSameType(ISliceId sliceId)
    {
      SliceDefinition sliceDefinition;
      if(this.slices.TryGetValue(sliceId, out sliceDefinition))
      {
        foreach (var m in sliceDefinition.MethodsInTheSameType)
        {
          foreach (var slice in this.slices)
          {
            if (slice.Value.Methods.Contains(m))
            {
              Console.WriteLine("Scheduling slice: {0}", slice.Key);

              yield return slice.Key;
              break; // done with the methodId m;
            }
          }
        }
      }
    }

    public virtual IClousotCache Create(IClousotCacheOptions options)
    {
      if (this.cacheAccessor == null) // avoids locking if not necessary
      {
        lock (this.LockCacheAccessor)
          if (this.cacheAccessor == null)
          {
            foreach (var factory in this.cacheDataAccessorFactories)
            {
              this.cacheAccessor = factory.Create(options);
              //test the db connection
              if (this.cacheAccessor != null && this.cacheAccessor.TestCache())
                break;
              this.cacheAccessor = null;
            }
          }
      }
      return new UndisposableCacheDataAccessor(this.cacheAccessor);
    }

    public void Dispose()
    {
      if (this.cacheAccessor != null)
      {
        this.cacheAccessor.Dispose();
        this.cacheAccessor = null;
      }
    }
  }
}
