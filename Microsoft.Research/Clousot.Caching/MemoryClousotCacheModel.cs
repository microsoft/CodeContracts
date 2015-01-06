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
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public class MemoryClousotCacheEntities : IClousotCacheEntities<MemMethodModel, MemCachingMetadata, MemAssemblyInfo, MemAssemblyBinding>
  {
    public readonly HashSet<MemMethodModel> methodModels = new HashSet<MemMethodModel>();
    public readonly List<MemCachingMetadata> cachingMetadatas = new List<MemCachingMetadata>();
    public readonly List<MemAssemblyInfo> assemblyInfoes = new List<MemAssemblyInfo>();
    public readonly Dictionary<ByteArray, SortedList<DateTime, ByteArray>> methodHashFromId = new Dictionary<ByteArray, SortedList<DateTime, ByteArray>>();

    IEnumerable<MemMethodModel> IClousotCacheEntities<MemMethodModel, MemCachingMetadata, MemAssemblyInfo, MemAssemblyBinding>.MethodModels { get { return this.methodModels; } }
    IEnumerable<MemCachingMetadata> IClousotCacheEntities<MemMethodModel, MemCachingMetadata, MemAssemblyInfo, MemAssemblyBinding>.CachingMetadatas { get { return this.cachingMetadatas; } }
    IEnumerable<MemAssemblyInfo> IClousotCacheEntities<MemMethodModel, MemCachingMetadata, MemAssemblyInfo, MemAssemblyBinding>.AssemblyInfoes { get { return this.assemblyInfoes; } }
    IEnumerable<MemAssemblyBinding> IClousotCacheEntities<MemMethodModel, MemCachingMetadata, MemAssemblyInfo, MemAssemblyBinding>.AssemblyBindings { get { return this.methodModels.SelectMany(m => m.AssemblyBindings); } }

    public void AddMethodModel(MemMethodModel methodModel) { this.methodModels.Add(methodModel); }
    public void DeleteMethodModel(MemMethodModel methodModel) { this.methodModels.Remove(methodModel); }

    public MemMethodModel NewMethodModel() { return new MemMethodModel(); }
    public MemAssemblyInfo AddNewAssemblyInfo() { return this.assemblyInfoes.AddReturn(new MemAssemblyInfo()); }

    public void AddHashDateBindingForNow(ByteArray methodIdHash, MemMethodModel methodModel)
    {
      var datetime = DateTime.Now;
      SortedList<DateTime, ByteArray> hashes;
      if (!this.methodHashFromId.TryGetValue(methodIdHash, out hashes))
        this.methodHashFromId.Add(methodIdHash, hashes = new SortedList<DateTime, ByteArray>());

      Console.WriteLine("[cache] Adding an entry in the method cache model at time: {0}", datetime);
      
      hashes.Add(datetime, methodModel.Hash);
    }
    public ByteArray GetHashForDate(ByteArray methodIdHash, DateTime t, bool afterT)
    {
      SortedList<DateTime, ByteArray> hashes;
      if (!this.methodHashFromId.TryGetValue(methodIdHash, out hashes))
        return null;
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

    public void Dispose() { }
  }


  public class MemAssemblyBinding : IAssemblyBinding
  {
    private readonly MemMethodModel method;

    internal MemAssemblyBinding(MemMethodModel method)
    {
      this.method = method;
    }

    public Guid AssemblyId { get; set; }
    public IMethodModel Method { get { return this.method; } }
  }

  public class MemAssemblyInfo : IAssemblyInfo
  {
    public Guid AssemblyId { get; set; }
    public DateTime Created { get; set; }
    public bool? IsBaseLine { get; set; }
    public long? Version { get; set; }
    public string Name { get; set; }
  }

  public class MemCachingMetadata : ICachingMetadata
  {
    public string Key { get; set; }
    public byte[] Value { get; set; }
  }

  public class MemMethodModel : IMethodModel
  {
    private readonly List<MemAssemblyBinding> assemblyBindings = new List<MemAssemblyBinding>();
    private readonly List<IOutcomeModel> outcomes = new List<IOutcomeModel>();
    private readonly List<ISuggestionModel> suggestions = new List<ISuggestionModel>();

    internal MemMethodModel() { }

    public ByteArray Hash { get; set; }
    public string Name { get; set; }
    public bool Timeout { get; set; }
    public AnalysisStatistics Statistics { get; set; }
    public SwallowedBuckets Swallowed { get; set; }
    public ContractDensity ContractDensity { get; set; }
    public long PureParametersMask { get; set; }
    public byte[] InferredExpr { get; set; }
    public byte[] InferredExprHash { get; set; }
    public string InferredExprString { get; set; }
    public string FullName { get; set; }

    public ICollection<IOutcomeModel> Outcomes { get { return this.outcomes; } }
    public ICollection<ISuggestionModel> Suggestions { get { return this.suggestions; } }

    public IEnumerable<MemAssemblyBinding> AssemblyBindings { get { return this.assemblyBindings; } }

    public IAssemblyBinding AddNewAssemblyBinding() { return this.assemblyBindings.AddReturn(new MemAssemblyBinding(this)); }
    public IOutcomeModel AddNewOutcomeModel() { return this.outcomes.AddReturn(new MemOutcomeModel(this)); }
    public ISuggestionModel AddNewSuggestionModel() { return this.suggestions.AddReturn(new MemSuggestionModel(this)); }

    public override int GetHashCode() { return this.Hash.GetHashCode(); }
    public bool Equals(IMethodModel other) { return other != null && this.Hash.Equals(other.Hash); }
    public override bool Equals(object other) { return this.Equals(other as IMethodModel); }
  }

  public abstract class MemOutcomeOrSuggestionModel : IOutcomeOrSuggestionModel
  {
    protected readonly List<IContextEdgeModel> contextEdges = new List<IContextEdgeModel>();

    internal MemOutcomeOrSuggestionModel(MemMethodModel method) { }

    public string Message { get; set; }
    public int SubroutineLocalId { get; set; }
    public int BlockIndex { get; set; }
    public int ApcIndex { get; set; }

    public ICollection<IContextEdgeModel> ContextEdges { get { return this.contextEdges; } }

    public abstract IContextEdgeModel AddNewContextEdgeModel();
  }

  public class MemOutcomeModel : MemOutcomeOrSuggestionModel, IOutcomeModel
  {
    private readonly List<IOutcomeContextModel> contexts = new List<IOutcomeContextModel>();

    internal MemOutcomeModel(MemMethodModel method) : base(method) { }

    public bool Related { get; set; }
    public ProofOutcome ProofOutcome { get; set; }
    public WarningType WarningType { get; set; }

    public ICollection<IOutcomeContextModel> Contexts { get { return this.contexts; } }

    public override IContextEdgeModel AddNewContextEdgeModel() { var tmp = new MemOutcomeContextEdgeModel(this); contextEdges.Add(tmp); return tmp; }
    public IOutcomeContextModel AddNewContextModel() { var tmp = new MemOutcomeContextModel(this); contexts.Add(tmp); return tmp; }
  }

  public class MemSuggestionModel : MemOutcomeOrSuggestionModel, ISuggestionModel
  {
    internal MemSuggestionModel(MemMethodModel method) : base(method) { }

    public string Kind { get; set; }

    public override IContextEdgeModel AddNewContextEdgeModel() { var tmp = new MemSuggestionContextEdgeModel(this); contextEdges.Add(tmp); return tmp; }
  }

  public class MemOutcomeContextModel : IOutcomeContextModel
  {
    internal MemOutcomeContextModel(MemOutcomeModel outcome) { }

    public WarningContext WarningContext { get; set; }
  }

  public abstract class MemContextEdgeModel : IContextEdgeModel
  {
    internal MemContextEdgeModel(MemOutcomeOrSuggestionModel outcomeOrSuggestion) { }

    public int Block1SubroutineLocalId { get; set; }
    public int Block1Index { get; set; }
    public int Block2SubroutineLocalId { get; set; }
    public int Block2Index { get; set; }
    public string Tag { get; set; }
    public int Rank { get; set; }
  }

  public class MemOutcomeContextEdgeModel : MemContextEdgeModel, IOutcomeContextEdgeModel
  {
    internal MemOutcomeContextEdgeModel(MemOutcomeModel outcome) : base(outcome) { }
  }

  public class MemSuggestionContextEdgeModel : MemContextEdgeModel, ISuggestionContextEdgeModel
  {
    internal MemSuggestionContextEdgeModel(MemSuggestionModel suggestion) : base(suggestion) { }
  }


  static class CollectionExtensions
  {
    public static T AddReturn<T>(this ICollection<T> source, T item)
    {
      source.Add(item);
      return item;
    }
  }
}
