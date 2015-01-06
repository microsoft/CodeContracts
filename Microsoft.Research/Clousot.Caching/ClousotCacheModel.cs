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
using System.Data;
using System.Data.EntityClient;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public delegate long IdGenerator();

  #region interface bindings
  partial class ClousotCacheEntities : IClousotCacheEntities<MethodModel, CachingMetadata, AssemblyInfo, AssemblyBinding> { }
  partial class AssemblyBinding : IAssemblyBinding { }
  partial class AssemblyInfo : IAssemblyInfo { }
  partial class CachingMetadata : ICachingMetadata { }
  partial class MethodModel : IMethodModel { }
  partial class OutcomeContextEdgeModel : IOutcomeContextEdgeModel { }
  partial class OutcomeContextModel : IOutcomeContextModel { }
  partial class OutcomeModel : IOutcomeModel { }
  partial class SuggestionContextEdgeModel : ISuggestionContextEdgeModel { }
  partial class SuggestionModel : ISuggestionModel { }
  #endregion

  #region interface implementation
  partial class ClousotCacheEntities
  {
    private readonly IdGenerator idGenerator;

    public ClousotCacheEntities(EntityConnection connection, IdGenerator idGenerator)
      : this(connection)
    {
      this.idGenerator = idGenerator;
    }

    IEnumerable<MethodModel> IClousotCacheEntities<MethodModel, CachingMetadata, AssemblyInfo, AssemblyBinding>.MethodModels { get { return this.MethodModels; } }
    IEnumerable<CachingMetadata> IClousotCacheEntities<MethodModel, CachingMetadata, AssemblyInfo, AssemblyBinding>.CachingMetadatas { get { return this.CachingMetadatas; } }
    IEnumerable<AssemblyInfo> IClousotCacheEntities<MethodModel, CachingMetadata, AssemblyInfo, AssemblyBinding>.AssemblyInfoes { get { return this.AssemblyInfoes; } }
    IEnumerable<AssemblyBinding> IClousotCacheEntities<MethodModel, CachingMetadata, AssemblyInfo, AssemblyBinding>.AssemblyBindings { get { return this.AssemblyBindings; } }

    public void AddMethodModel(MethodModel methodModel) { this.MethodModels.AddObject(methodModel); }
    public void DeleteMethodModel(MethodModel methodModel) { this.MethodModels.DeleteObject(methodModel); }

    public MethodModel NewMethodModel() { return new MethodModel(this.idGenerator); }
    public AssemblyInfo AddNewAssemblyInfo() { return new AssemblyInfo(); }

    public void AddHashDateBindingForNow(ByteArray methodIdHash, MethodModel methodModel)
    {
      this.IdHashTimeToMethods.AddObject(new IdHashTimeToMethod{ MethodIdHash = methodIdHash.Bytes, Method = methodModel, Time = DateTime.Now });
    }
    public ByteArray GetHashForDate(ByteArray methodIdHash, DateTime t, bool afterT = false)
    {
      var methodIdHashBytes = methodIdHash.Bytes;
      var latest = this.IdHashTimeToMethods
//        .Where(b => b.MethodIdHash.Equals(methodIdHashBytes) && (afterT? b.Time >= t : b.Time <= t))
        .Where(b => b.MethodIdHash.Equals(methodIdHashBytes))        
        .OrderByDescending(b => b.Time)
        .FirstOrDefault();
      
      if (latest == null)
        return null;
      latest.MethodReference.Load(); // The Load() is necessary, probably something is missing in the .edmx 
      return latest.Method.Hash;
    }
  }

  partial class AssemblyBinding
  {
    IMethodModel IAssemblyBinding.Method { get { return this.Method; } }
  }

  partial class MethodModel
  {
    private readonly IdGenerator idGenerator;

    private MethodModel() { this.idGenerator = ReadOnlyIdGenerator; }
    private static long ReadOnlyIdGenerator() { throw new ReadOnlyException(); }

    public MethodModel(IdGenerator idGenerator)
    {
      this.idGenerator = idGenerator;
      this.Id = idGenerator();
    }

    ByteArray IMethodModel.Hash
    {
      get { return this.Hash; }
      set { this.Hash = value.Bytes; }
    }

    public IAssemblyBinding AddNewAssemblyBinding()
    {
      return new AssemblyBinding() { MethodId = this.Id };
    }

    public IOutcomeModel AddNewOutcomeModel()
    {
      return new OutcomeModel(idGenerator) { Method = this };
    }

    public ISuggestionModel AddNewSuggestionModel()
    {
      return new SuggestionModel(idGenerator) { Method = this };
    }

    ICollection<ISuggestionModel> IMethodModel.Suggestions { get { this.Suggestions.Load(); return this.Suggestions.CollectionCast<SuggestionModel, ISuggestionModel>(); } }
    ICollection<IOutcomeModel> IMethodModel.Outcomes { get { this.Outcomes.Load(); return this.Outcomes.CollectionCast<OutcomeModel, IOutcomeModel>(); } }

    public bool Equals(MethodModel mm) { return mm != null && this.Id == mm.Id; }
    public bool Equals(IMethodModel mm) { return this.Equals(mm as MethodModel); }
  }

  partial class OutcomeModel
  {
    private readonly IdGenerator idGenerator;

    private OutcomeModel() { this.idGenerator = ReadOnlyIdGenerator; }
    private static long ReadOnlyIdGenerator() { throw new ReadOnlyException(); }

    public OutcomeModel(IdGenerator idGenerator)
    {
      this.idGenerator = idGenerator;
      this.Id = idGenerator();
    }

    public IContextEdgeModel AddNewContextEdgeModel()
    {
      return new OutcomeContextEdgeModel() { Id = idGenerator(), Outcome = this };
    }

    public IOutcomeContextModel AddNewContextModel()
    {
      return new OutcomeContextModel() { Id = idGenerator(), Outcome = this };
    }

    ICollection<IContextEdgeModel> IOutcomeOrSuggestionModel.ContextEdges
    {
      get
      {
        if ((this.EntityState & EntityState.Detached) == 0)
          this.OutcomeContextEdges.Load();
        return this.OutcomeContextEdges.CollectionCast<OutcomeContextEdgeModel, IContextEdgeModel>();
      }
    }
    ICollection<IOutcomeContextModel> IOutcomeModel.Contexts
    {
      get
      {
        if ((this.EntityState & EntityState.Detached) == 0)
          this.OutcomeContexts.Load();
        return this.OutcomeContexts.CollectionCast<OutcomeContextModel, IOutcomeContextModel>();
      }
    }
  }

  partial class SuggestionModel
  {
    private readonly IdGenerator idGenerator;

    private SuggestionModel() { this.idGenerator = ReadOnlyIdGenerator; }
    private static long ReadOnlyIdGenerator() { throw new ReadOnlyException(); }

    public SuggestionModel(IdGenerator idGenerator)
    {
      this.idGenerator = idGenerator;
      this.Id = idGenerator();
    }

    public IContextEdgeModel AddNewContextEdgeModel()
    {
      return new SuggestionContextEdgeModel() { Id = idGenerator(), Suggestion = this };
    }

    ICollection<IContextEdgeModel> IOutcomeOrSuggestionModel.ContextEdges
    {
      get
      {
        if ((this.EntityState & EntityState.Detached) == 0)
          this.SuggestionContextEdges.Load();
        return this.SuggestionContextEdges.CollectionCast<SuggestionContextEdgeModel, IContextEdgeModel>();
      }
    }
  }
  #endregion


  partial class OutcomeContextEdgeModel
  {
  }

  partial class SuggestionContextEdgeModel
  {
  }

  partial class OutcomeModel
  {

    public ProofOutcome ProofOutcome { get { return (ProofOutcome)this.ProofOutcomeByte; } set { this.ProofOutcomeByte = (byte)value; } }

    public WarningType WarningType { get { return (WarningType)this.WarningTypeByte; } set { this.WarningTypeByte = (byte)value; } }

  }

  partial class OutcomeContextModel
  {
    public WarningContext WarningContext
    {
      get { return new WarningContext((WarningContext.ContextType)this.TypeByte, this.AssociatedInfo); }
      set { this.TypeByte = (byte)value.Type; this.AssociatedInfo = value.AssociatedInfo; }
    }
  }

  partial class SuggestionModel
  {
  }

  partial class MethodModel
  {
    public AnalysisStatistics Statistics { 
      get {
        var res = new AnalysisStatistics {
          Bottom = (uint)this.StatsBottom,
          Top = (uint)this.StatsTop,
          True = (uint)this.StatsTrue,
          False = (uint)this.StatsFalse,
          Total = (uint)(this.StatsBottom + this.StatsTop + this.StatsTrue + this.StatsFalse)
        };
        return res;
      }
      set {
        this.StatsBottom = (int)value.Bottom;
        this.StatsFalse = (int)value.False;
        this.StatsTrue = (int)value.True;
        this.StatsTop = (int)value.Top;
      }
    }

    public ContractDensity ContractDensity
    {
      get
      {
        return new ContractDensity(
          (ulong)this.MethodInstructions,
          (ulong)this.ContractInstructions,
          (ulong)this.Contracts);
      }
      set
      {
        this.MethodInstructions = (long)value.MethodInstructions;
        this.ContractInstructions = (long)value.ContractInstructions;
        this.Contracts = (long)value.Contracts;
      }
    }

    public SwallowedBuckets Swallowed
    {
      get
      {
        return new SwallowedBuckets(
          outcome =>
          {
            switch (outcome)
            {
              case ProofOutcome.Top:
                return this.SwallowedTop;
              case ProofOutcome.Bottom:
                return this.SwallowedBottom;
              case ProofOutcome.True:
                return this.SwallowedTrue;
              case ProofOutcome.False:
                return this.SwallowedFalse;
              default:
                throw new ArgumentException();
            }
          });
      }
      set
      {
        this.SwallowedTop = value.GetCounter(ProofOutcome.Top);
        this.SwallowedBottom = value.GetCounter(ProofOutcome.Bottom);
        this.SwallowedTrue = value.GetCounter(ProofOutcome.True);
        this.SwallowedFalse = value.GetCounter(ProofOutcome.False);
      }
    }
  }

  partial class VersionResult
  {
      public long AllWarnings { get; protected set; }
      public long StatsAll { get; protected set; }
      public long SwallowedWarnings { get; protected set; }
      public long DisplayedWarnings { get; protected set; }
      public double ContractsPerMethod { get; protected set; }
      public double ContractDensity { get; protected set; }

      public VersionResult()
      { }

      public VersionResult(long version, IEnumerable<MethodModel> M)
      {
          Contract.Requires(M != null);
          Contract.Requires(Contract.ForAll(M, m => m != null));

          foreach (var m in M) {
              Contract.Assume(m.Outcomes != null);
              Contract.Assume(m.Suggestions != null);
              this.Add(m);
          }

          this.Version = version;
          this.Methods = M.Count();
          this.Complete();
      }

      private void Add(MethodModel m)
      {
          Contract.Requires(m != null);
          Contract.Requires(m.Outcomes != null);
          Contract.Requires(m.Suggestions != null);

          this.ContractInstructions += m.ContractInstructions;
          this.Contracts += m.Contracts;
          this.MethodInstructions += m.MethodInstructions;
          this.Outcomes += m.Outcomes.Count();
          this.StatsBottom += m.StatsBottom;
          this.StatsFalse += m.StatsFalse;
          this.StatsTop += m.StatsTop;
          this.StatsTrue += m.StatsTrue;
          this.Suggestions += m.Suggestions.Count();
          this.SwallowedBottom += m.SwallowedBottom;
          this.SwallowedFalse += m.SwallowedFalse;
          this.SwallowedTop += m.SwallowedTop;
          this.SwallowedTrue += m.SwallowedTrue;
          this.Timeout += m.Timeout ? 1 : 0;

          this.HasWarnings += m.StatsBottom + m.StatsFalse + m.StatsTop > 0 ? 1 : 0;
          this.ZeroTop += m.StatsTop == 0 ? 1 : 0;
      }

      internal void Complete()
      {
          this.AllWarnings = this.StatsBottom + this.StatsFalse + this.StatsTop;
          this.StatsAll = this.AllWarnings + this.StatsTrue;
          this.SwallowedWarnings = this.SwallowedBottom + this.SwallowedFalse + this.SwallowedTop;
          this.DisplayedWarnings = this.AllWarnings - this.SwallowedWarnings;
          this.ContractsPerMethod = SafeDiv(this.Contracts, this.Methods);
          this.ContractDensity = SafeDiv(this.ContractInstructions, this.MethodInstructions);
      }

      private static double SafeDiv(long x, long y, double def = default(double))
      {
          return y == 0 ? def : (double)x / (double)y;
      }

      static public VersionResult Create(long version, IEnumerable<MethodModel> M)
      {
          Contract.Requires(M != null);
          Contract.Ensures(Contract.Result<VersionResult>() != null);

          return new VersionResult(version, M);
      }
  }

}