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

namespace Microsoft.Research.CodeAnalysis.Caching
{
  public class UndisposableCacheDataAccessor : IClousotCache
  {
    #region Object invariant
    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.underlying != null);
    }
    #endregion

    private readonly IClousotCache underlying;

    public UndisposableCacheDataAccessor(IClousotCache underlying)
    {
      Contract.Requires(underlying != null);
      this.underlying = underlying;
    }

    public bool IsValid { get { return this.underlying.IsValid; } }

    void IDisposable.Dispose() { }

    public Metadata GetMetadataOrNull(string key, bool silent = false)
    {
      return underlying.GetMetadataOrNull(key, silent);
    }

    public bool TryGetMethodModelForHash(DataStructures.ByteArray hash, out Method result)
    {
      return underlying.TryGetMethodModelForHash(hash, out result);
    }

    public bool TryGetBaselineModel(string methodFullname, string baselineId, out Method result)
    {
      return underlying.TryGetBaselineModel(methodFullname, baselineId, out result);
    }

    public AssemblyInfo AddAssemblyInfo(string name, Guid guid)
    {
      return underlying.AddAssemblyInfo(name, guid);
    }

    public void AddOrUpdateMethod(Method methodModel, DataStructures.ByteArray methodId)
    {
      underlying.AddOrUpdateMethod(methodModel, methodId);
    }

    public void AddOrUpdateBaseline(string fullname, string baselineId, Method baseline)
    {
      this.underlying.AddOrUpdateBaseline(fullname, baselineId, baseline);
    }

    public void DeleteMethodModel(Method methodModel)
    {
      this.underlying.DeleteMethodModel(methodModel);
    }

    public Outcome AddNewOutcome(Method method, string message, bool related)
    {
      return underlying.AddNewOutcome(method, message, related);
    }

    public Suggestion AddNewSuggestion(Method method, string suggestion, string kind, ClousotSuggestion.Kind type, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      return underlying.AddNewSuggestion(method, suggestion, kind, type, extraInfo);
    }

    public OutcomeContext AddNewOutcomeContext(Outcome outcome, WarningContext warningContext)
    {
      return underlying.AddNewOutcomeContext(outcome, warningContext);
    }

    public ContextEdge AddNewContextEdge(OutcomeOrSuggestion item, int rank)
    {
      return underlying.AddNewContextEdge(item, rank);
    }

    public void SaveChanges(bool now = false)
    {
      underlying.SaveChanges(now);
    }

    public DataStructures.ByteArray GetHashForDate(DataStructures.ByteArray methodId, DateTime datetime, bool afterDateTime)
    {
      return underlying.GetHashForDate(methodId, datetime, afterDateTime);
    }

    public Method NewMethodModel()
    {
      return underlying.NewMethodModel();
    }

    public void AddAssemblyBinding(Method methodModel, AssemblyInfo assemblyInfo)
    {
        underlying.AddAssemblyBinding(methodModel, assemblyInfo);
    }

    public bool TestCache() { return this.underlying.TestCache(); }


    public string CacheName
    {
      get
      {
        return underlying.CacheName + " (undisposable)";
      }
    }



  }

  public class UndisposableMemoryCacheFactory : IClousotCacheFactory
  {
    IClousotCache existing;

    IClousotCache IClousotCacheFactory.Create(IClousotCacheOptions options)
    {
      if (existing == null)
      {
        var memCache = new MemClousotCacheFactory().Create(options);
        this.existing = new UndisposableCacheDataAccessor(memCache);
      }
      return this.existing;
    }
  }
}
