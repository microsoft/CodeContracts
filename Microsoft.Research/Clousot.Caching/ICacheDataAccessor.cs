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
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public interface ICacheDataAccessor : IDisposable
  {
    bool IsValid { get; }
    void Clear();
    ICachingMetadata GetMetadataOrNull(string key, bool silent = false);
    bool TryGetMethodModelForHash(ByteArray hash, out IMethodModel result);
    bool TryGetMethodModelForName(string name, out IMethodModel result);
    void AddAssemblyBinding(Guid assemblyGuid, IMethodModel methodModel);
    void AddAssemblyInfo(string name, Guid guid);
    bool TryAddMethodModel(IMethodModel methodModel, ByteArray methodId);
    bool TrySaveChanges(bool now = false);
    ByteArray GetHashForDate(ByteArray methodId, DateTime datetime, bool afterDateTime);

    // Create a new IMethodModel object, but do not attach it yet (use TryAddMethodModel to attach it)
    IMethodModel NewMethodModel();
  }

  public class CacheVersionParameters
  {
    public long Version { get; set; }
    public bool SetBaseLine { get; set; }
  }

  public interface ICacheDataAccessorFactory
  {
    ICacheDataAccessor Create(string name, Dictionary<string, byte[]> metadataIfCreation);
  }

  public class UndisposableCacheDataAccessor : ICacheDataAccessor
  {
    private readonly ICacheDataAccessor underlying;

    public UndisposableCacheDataAccessor(ICacheDataAccessor underlying)
    {
      this.underlying = underlying;
    }

    public bool IsValid { get { return this.underlying.IsValid; } }
    public void Clear() { this.underlying.Clear(); }
    public ICachingMetadata GetMetadataOrNull(string key, bool silent = false) { return this.underlying.GetMetadataOrNull(key, silent); }
    public bool TryGetMethodModelForHash(ByteArray hash, out IMethodModel result) { return this.underlying.TryGetMethodModelForHash(hash, out result); }
    public bool TryGetMethodModelForName(string name, out IMethodModel result) { return this.underlying.TryGetMethodModelForName(name, out result); }
    public void AddAssemblyBinding(Guid assemblyGuid, IMethodModel methodModel) { this.underlying.AddAssemblyBinding(assemblyGuid, methodModel); }
    public void AddAssemblyInfo(string name, Guid guid) { this.underlying.AddAssemblyInfo(name, guid); }
    public bool TryAddMethodModel(IMethodModel methodModel, ByteArray methodId) { return this.underlying.TryAddMethodModel(methodModel, methodId); }
    public bool TrySaveChanges(bool now = false) { return this.underlying.TrySaveChanges(now); }
    public ByteArray GetHashForDate(ByteArray methodId, DateTime datetime, bool afterT) { return this.underlying.GetHashForDate(methodId, datetime, afterT); }

    public IMethodModel NewMethodModel() { return this.underlying.NewMethodModel(); }

    void IDisposable.Dispose() { }
  }
}
