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
using System.Linq;
using System.Text;
using Microsoft.Research.CodeAnalysis.Caching.Models;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching
{
  #region IClousotCache contract binding
  [ContractClass(typeof(IClousotCacheContract))]
  public partial interface IClousotCache { }

  [ContractClassFor(typeof(IClousotCache))]
  abstract class IClousotCacheContract : IClousotCache
  {
    public bool IsValid
    {
      get { throw new NotImplementedException(); }
    }

    [Pure]
    public Metadata GetMetadataOrNull(string key, bool silent = false)
    {
      throw new NotImplementedException();
    }

    [Pure]
    public bool TryGetMethodModelForHash(ByteArray hash, out Method result)
    {
      Contract.Requires(hash != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);

      throw new NotImplementedException();
    }

    [Pure]
    public bool TryGetBaselineModel(string methodFullName, string baselineId, out Method result)
    {
      Contract.Requires(methodFullName != null);
      Contract.Requires(baselineId != null);

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);

      throw new NotImplementedException();
    }

    public AssemblyInfo AddAssemblyInfo(string name, Guid guid)
    {
      Contract.Requires(name != null);

      Contract.Ensures(Contract.Result<AssemblyInfo>() != null);

      throw new NotImplementedException();
    }

    public void AddOrUpdateMethod(Method methodModel, ByteArray methodId)
    {
      Contract.Requires(methodModel != null);
      Contract.Requires(methodId != null);

      throw new NotImplementedException();
    }

    public void AddOrUpdateBaseline(string fullname, string baselineId, Method baseline)
    {
      Contract.Requires(fullname != null);
      Contract.Requires(baselineId != null);
      Contract.Requires(baseline != null);

      throw new NotImplementedException();
    }

    public Outcome AddNewOutcome(Method method, string message, bool related)
    {
      Contract.Requires(method != null);
      Contract.Requires(message != null);
      Contract.Ensures(Contract.Result<Outcome>() != null);

      throw new NotImplementedException();
    }

    public Suggestion AddNewSuggestion(Method method, string suggestion, string kind, ClousotSuggestion.Kind type, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      Contract.Requires(method != null);
      Contract.Requires(suggestion != null);
      Contract.Ensures(Contract.Result<Suggestion>() != null);

      throw new NotImplementedException();
    }

    public OutcomeContext AddNewOutcomeContext(Outcome outcome, WarningContext warningContext)
    {
      Contract.Requires(outcome != null);
      Contract.Ensures(Contract.Result<OutcomeContext>() != null);

      throw new NotImplementedException();
    }

    public ContextEdge AddNewContextEdge(OutcomeOrSuggestion item, int rank)
    {
      Contract.Requires(item != null);
      Contract.Ensures(Contract.Result<ContextEdge>() != null);

      throw new NotImplementedException();
    }

    public void SaveChanges(bool now = false)
    {
      throw new NotImplementedException();
    }

    public ByteArray GetHashForDate(ByteArray methodId, DateTime datetime, bool afterDateTime)
    {
      Contract.Requires(methodId != null);
      // It may return null!!
      //Contract.Ensures(Contract.Result<ByteArray>() != null);

      throw new NotImplementedException();
    }

    public Method NewMethodModel()
    {
      Contract.Ensures(Contract.Result<Method>() != null);

      throw new NotImplementedException();
    }

    public bool TestCache()
    {
      throw new NotImplementedException();
    }

    public void AddAssemblyBinding(Method methodModel, AssemblyInfo assemblyInfo)
    {
      Contract.Requires(methodModel != null);
      Contract.Requires(assemblyInfo != null);

      throw new NotImplementedException();
    }

    public string CacheName
    {
      get {
        Contract.Ensures(Contract.Result<string>() != null); 
        throw new NotImplementedException();
      }
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }


    public void DeleteMethodModel(Method methodModel)
    {
      Contract.Requires(methodModel != null);
    }
  }
  #endregion

  public partial interface IClousotCache : IDisposable
  {
    bool IsValid { get; }
    Metadata GetMetadataOrNull(string key, bool silent = false);
    bool TryGetMethodModelForHash(ByteArray hash, out Method result);
    bool TryGetBaselineModel(string methodFullName, string baselineId, out Method result);
    AssemblyInfo AddAssemblyInfo(string name, Guid guid);
    void AddOrUpdateMethod(Method methodModel, ByteArray methodId);

    void DeleteMethodModel(Method methodModel);
    
    void AddOrUpdateBaseline(string fullname, string baselineId, Method baseline);
    Outcome AddNewOutcome(Method method, string message, bool related);
    Suggestion AddNewSuggestion(Method method, string suggestion, string kind, ClousotSuggestion.Kind type, ClousotSuggestion.ExtraSuggestionInfo extraInfo);
    OutcomeContext AddNewOutcomeContext(Outcome outcome, WarningContext warningContext);
    ContextEdge AddNewContextEdge(OutcomeOrSuggestion item, int rank);
    void SaveChanges(bool now = false);
    ByteArray GetHashForDate(ByteArray methodId, DateTime datetime, bool afterDateTime);
    // Create a new IMethodModel object, but do not attach it yet (use TryAddMethodModel to attach it)
    Method NewMethodModel();
    bool TestCache();
    void AddAssemblyBinding(Method methodModel, AssemblyInfo assemblyInfo);
    string CacheName { get; }
  }

  public interface IClousotCacheOptions
  {
    string ClousotVersion { get; }
    string GetCacheDBName();
    string CacheDirectory { get; }
    string CacheServer { get; }
    int CacheServerTimeout { get; }
    bool ClearCache { get; }
    bool SaveToCache { get; }
    string SourceControlInfo { get; }
    bool SetCacheBaseLine { get; }
    bool Trace { get; }
  }

}
