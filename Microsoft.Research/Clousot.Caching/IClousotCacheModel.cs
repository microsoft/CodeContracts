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
  public interface IClousotCacheEntities<MM, CM, AI, AB> : IDisposable
    where MM : class, IMethodModel
    where CM : class, ICachingMetadata
    where AI : class, IAssemblyInfo
    where AB : class, IAssemblyBinding
  {
    IEnumerable<MM> MethodModels { get; }
    IEnumerable<CM> CachingMetadatas { get; }
    IEnumerable<AI> AssemblyInfoes { get; }
    IEnumerable<AB> AssemblyBindings { get; }

    void AddMethodModel(MM methodModel);
    void DeleteMethodModel(MM methodModel);

    MM NewMethodModel();
    AI AddNewAssemblyInfo();

    void AddHashDateBindingForNow(ByteArray methodIdHash, MM methodModel);
    ByteArray GetHashForDate(ByteArray methodIdHash, DateTime t, bool afterT);
  }

  public interface IAssemblyBinding
  {
    Guid AssemblyId { get; set; }
    IMethodModel Method { get; }
  }

  public interface IAssemblyInfo
  {
    Guid AssemblyId { get; set; }
    DateTime Created { get; set; }
    bool? IsBaseLine { get; set; }
    long? Version { get; set; }
    string Name { get; set; }
  }

  public interface ICachingMetadata
  {
    string Key { get; set; }
    byte[] Value { get; set; }
  }

  public interface IMethodModel : IEquatable<IMethodModel>
  {
    ByteArray Hash { get; set; }
    string Name { get; set; }
    bool Timeout { get; set; }
    AnalysisStatistics Statistics { get; set; }
    SwallowedBuckets Swallowed { get; set; }
    ContractDensity ContractDensity { get; set; }
    long PureParametersMask { get; set; }
    byte[] InferredExpr { get; set; }
    byte[] InferredExprHash { get; set; }
    string InferredExprString { get; set; }
    string FullName { get; set; }
    // Auto-load
    ICollection<IOutcomeModel> Outcomes { get; }
    ICollection<ISuggestionModel> Suggestions { get; }

    // Create new values and attach them
    IAssemblyBinding AddNewAssemblyBinding();
    IOutcomeModel AddNewOutcomeModel();
    ISuggestionModel AddNewSuggestionModel();
  }

  public interface IOutcomeOrSuggestionModel
  {
    string Message { get; set; }
    int SubroutineLocalId { get; set; }
    int BlockIndex { get; set; }
    int ApcIndex { get; set; }
    // Auto-load
    ICollection<IContextEdgeModel> ContextEdges { get; }

    // Create a new IContextEdgeModel and attach it
    IContextEdgeModel AddNewContextEdgeModel();
  }

  public interface IContextEdgeModel
  {
    int Block1SubroutineLocalId { get; set; }
    int Block1Index { get; set; }
    int Block2SubroutineLocalId { get; set; }
    int Block2Index { get; set; }
    string Tag { get; set; }
    int Rank { get; set; }
  }

  public interface IOutcomeContextEdgeModel : IContextEdgeModel
  { }

  public interface IOutcomeContextModel
  {
    WarningContext WarningContext { get; set; }
  }

  public interface IOutcomeModel : IOutcomeOrSuggestionModel
  {
    bool Related { get; set; }
    ProofOutcome ProofOutcome { get; set; }
    WarningType WarningType { get; set; }
    // Auto-load
    ICollection<IOutcomeContextModel> Contexts { get; }

    // Create a new IOutcomeContextModel and attach it
    IOutcomeContextModel AddNewContextModel();
  }

  public interface ISuggestionContextEdgeModel :IContextEdgeModel
  { }

  public interface ISuggestionModel : IOutcomeOrSuggestionModel
  {
    string Kind { get; set; }
  }

  public static class ICacheModelExtensions
  {
    public static IAssemblyBinding AddNewAssemblyBinding(this IMethodModel @this, Guid assemblyGuid)
    {
      var res = @this.AddNewAssemblyBinding();
      res.AssemblyId = assemblyGuid;
      return res;
    }

    public static IOutcomeModel AddNewOutcomeModel(this IMethodModel @this, string message, bool related)
    {
      var res = @this.AddNewOutcomeModel();
      res.Message = message;
      res.Related = related;
      return res;
    }

    public static ISuggestionModel AddNewSuggestionModel(this IMethodModel @this, string message, string kind)
    {
      var res = @this.AddNewSuggestionModel();
      res.Message = message;
      res.Kind = kind;
      return res;
    }

    public static IContextEdgeModel AddNewContextEdgeModel(this IOutcomeOrSuggestionModel @this, int rank)
    {
      var res = @this.AddNewContextEdgeModel();
      res.Rank = rank;
      return res;
    }

    public static IOutcomeContextModel AddNewContextModel(this IOutcomeModel @this, WarningContext warningContext)
    {
      var res = @this.AddNewContextModel();
      res.WarningContext = warningContext;
      return res;
    }

    public static ByteArray GetResultHash(this IMethodModel methodModel)
    {
      // Put in this hash only what the analysis of other methods will rely on (i.e. time stats would not be a good idea)

      return new ByteArray(methodModel.InferredExprHash, methodModel.PureParametersMask, methodModel.Timeout);
    }
  }
}
