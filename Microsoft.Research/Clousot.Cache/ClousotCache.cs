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
  public class ClousotCache : IClousotCache
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.options != null);
    }

    readonly private ICacheModel model;
    readonly private IClousotCacheOptions options;

    public ClousotCache(ICacheModel model, IClousotCacheOptions options)
    {
      Contract.Requires(model != null);
      Contract.Requires(options != null);

      this.options = options;
      this.model = model;

      try
      {
        // force db initialization
        var ignored = this.model.Metadatas.FirstOrDefault();

        if (model.IsFresh)
        {
          var md = model.NewMetadata();
          md.Key = "Version";
          md.Value = Encoding.UTF8.GetBytes(options.ClousotVersion.AssumeNotNull());
          model.AddOrUpdate(md);
          model.SaveChanges(now: true);
        }
      }
      catch (Exception e)
      {
        // failed
        this.model = null;
        if (options.Trace)
        {
          Console.WriteLine("[cache] Cannot open cache: {0}", e.Message);
          for (var inner = e.InnerException; inner != null; inner = inner.InnerException)
          {
            Console.WriteLine("[cache] Inner Exception: {0}", inner.Message);
          }
        }

      }
    }

    public bool IsValid
    {
      [ContractVerification(false)] // cccheck does not understand exceptional block
      get {
        Contract.Ensures(!Contract.Result<bool>() || this.model != null);
        
        return model != null && model.IsValid; 
      }
    }

    public bool TestCache()
    {
      var metaData = this.GetMetadataOrNull("Version");
      if (metaData == null || !metaData.Value.AssumeNotNull().SequenceEqual(Encoding.UTF8.GetBytes(options.ClousotVersion.AssumeNotNull())))
      {
        return false;
      }
      return true;
    }

    public Metadata GetMetadataOrNull(string key, bool silent = false)
    {
      if (!this.IsValid) return null;
      Contract.Assume(key != null, "Should it be a precondition?");
      return this.model.MetadataByKey(key);
    }

    public bool TryGetMethodModelForHash(ByteArray hash, out Method result)
    {
      if (!this.IsValid) { result = null; return false; }
      Contract.Assume(hash.Bytes != null);
      result = this.model.MethodByHash(hash.Bytes);
      return result != null;
    }

    public bool TryGetBaselineModel(string methodFullname, string baselineId, out Method result)
    {
      if (!this.IsValid) { result = null; return false; }
      result = this.model.BaselineByName(methodFullname.MD5Encode(), baselineId);
      return result != null;
    }

    public AssemblyInfo AddAssemblyInfo(string assemblyName, Guid assemblyGuid)
    {
      if (!this.IsValid) return null;

      var ainfo = this.model.GetOrCreateAssemblyInfo(assemblyGuid);
      ainfo.Created = DateTime.Now;
      ainfo.SourceControlInfo = this.options.SourceControlInfo;
      ainfo.IsBaseLine = this.options.SetCacheBaseLine;
      ainfo.Name = assemblyName;
      this.model.AddOrUpdate(ainfo);
      return ainfo;
    }

    public void AddOrUpdateMethod(Method methodModel, ByteArray methodId)
    {
      if (!this.IsValid) return;

      this.model.AddOrUpdate(methodModel);
      var idhash = model.NewHashDateBindingForNow(methodId, methodModel);
      model.AddOrUpdate(idhash);
    }

    public void DeleteMethodModel(Method methodModel)
    {
      if (!this.IsValid) return;
      this.model.DeleteMethodModel(methodModel);
    }

    public void AddOrUpdateBaseline(string fullname, string baselineId, Method baseline)
    {
      if (!this.IsValid) return;

      this.model.AddOrUpdate(new BaselineMethod { MethodFullNameHash = fullname.MD5Encode(), BaselineId = baselineId, Method = baseline });
    }

    public Outcome AddNewOutcome(Method method, string message, bool related)
    {
      if (!this.IsValid) return null;

      var res = this.model.NewOutcome(method);
      res.Message = message;
      res.Related = related;
      return res;
    }

    public Suggestion AddNewSuggestion(Method method, string message, string kind, ClousotSuggestion.Kind type, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      if (!this.IsValid) return null;

      var res = this.model.NewSuggestion(method);
      res.Message = message;
      res.Type= (byte)type;
      res.Kind = kind;
      res.ExtraInfo = extraInfo.Serialize();

      return res;
    }

    public OutcomeContext AddNewOutcomeContext(Outcome outcome, WarningContext context)
    {
      if (!this.IsValid) return null;

      var result = this.model.NewOutcomeContext(outcome);
      result.WarningContext = context;
      return result;
    }

    public ContextEdge AddNewContextEdge(OutcomeOrSuggestion item, int rank)
    {
      if (!this.IsValid) return null;

      var result = this.model.NewContextEdge(item);
      result.Rank = rank;
      return result;
    }

    public void SaveChanges(bool now = false)
    {
      if (!this.IsValid) return;

      this.model.SaveChanges(now);
    }

    public ByteArray GetHashForDate(ByteArray methodId, DateTime datetime, bool afterDateTime)
    {
      if (!this.IsValid) return null;

      return this.model.GetHashForDate(methodId, datetime, afterDateTime);
    }

    public Method NewMethodModel()
    {
      if (!this.IsValid) return null;

      return this.model.NewMethod();
    }

    public void Dispose()
    {
      if (!this.IsValid) return;

      this.model.Dispose();
    }


    [ContractVerification(false)] // cccheck does not understand exceptional block
    public void AddAssemblyBinding(Method methodModel, AssemblyInfo assemblyInfo)
    {
      Contract.Assume(this.model != null);
      this.model.AddOrUpdate(methodModel, assemblyInfo);
    }


    public string CacheName
    {
      [ContractVerification(false)] // cccheck does not understand exceptional block
      get
      {
        Contract.Assume(this.model != null);
        return this.model.CacheName;
      }
    }
  }
}
