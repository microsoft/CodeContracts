// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            Contract.Invariant(_options != null);
        }

        readonly private ICacheModel _model;
        readonly private IClousotCacheOptions _options;

        public ClousotCache(ICacheModel model, IClousotCacheOptions options)
        {
            Contract.Requires(model != null);
            Contract.Requires(options != null);

            _options = options;
            _model = model;

            try
            {
                // force db initialization
                var ignored = _model.Metadatas.FirstOrDefault();

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
                _model = null;
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
            get
            {
                Contract.Ensures(!Contract.Result<bool>() || _model != null);

                return _model != null && _model.IsValid;
            }
        }

        public bool TestCache()
        {
            var metaData = this.GetMetadataOrNull("Version");
            if (metaData == null || !metaData.Value.AssumeNotNull().SequenceEqual(Encoding.UTF8.GetBytes(_options.ClousotVersion.AssumeNotNull())))
            {
                return false;
            }
            return true;
        }

        public Metadata GetMetadataOrNull(string key, bool silent = false)
        {
            if (!this.IsValid) return null;
            Contract.Assume(key != null, "Should it be a precondition?");
            return _model.MetadataByKey(key);
        }

        public bool TryGetMethodModelForHash(ByteArray hash, out Method result)
        {
            if (!this.IsValid) { result = null; return false; }
            Contract.Assume(hash.Bytes != null);
            result = _model.MethodByHash(hash.Bytes);
            return result != null;
        }

        public bool TryGetBaselineModel(string methodFullname, string baselineId, out Method result)
        {
            if (!this.IsValid) { result = null; return false; }
            result = _model.BaselineByName(methodFullname.MD5Encode(), baselineId);
            return result != null;
        }

        public AssemblyInfo AddAssemblyInfo(string assemblyName, Guid assemblyGuid)
        {
            if (!this.IsValid) return null;

            var ainfo = _model.GetOrCreateAssemblyInfo(assemblyGuid);
            ainfo.Created = DateTime.Now;
            ainfo.SourceControlInfo = _options.SourceControlInfo;
            ainfo.IsBaseLine = _options.SetCacheBaseLine;
            ainfo.Name = assemblyName;
            _model.AddOrUpdate(ainfo);
            return ainfo;
        }

        public void AddOrUpdateMethod(Method methodModel, ByteArray methodId)
        {
            if (!this.IsValid) return;

            _model.AddOrUpdate(methodModel);
            var idhash = _model.NewHashDateBindingForNow(methodId, methodModel);
            _model.AddOrUpdate(idhash);
        }

        public void DeleteMethodModel(Method methodModel)
        {
            if (!this.IsValid) return;
            _model.DeleteMethodModel(methodModel);
        }

        public void AddOrUpdateBaseline(string fullname, string baselineId, Method baseline)
        {
            if (!this.IsValid) return;

            _model.AddOrUpdate(new BaselineMethod { MethodFullNameHash = fullname.MD5Encode(), BaselineId = baselineId, Method = baseline });
        }

        public Outcome AddNewOutcome(Method method, string message, bool related)
        {
            if (!this.IsValid) return null;

            var res = _model.NewOutcome(method);
            res.Message = message;
            res.Related = related;
            return res;
        }

        public Suggestion AddNewSuggestion(Method method, string message, string kind, ClousotSuggestion.Kind type, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
        {
            if (!this.IsValid) return null;

            var res = _model.NewSuggestion(method);
            res.Message = message;
            res.Type = (byte)type;
            res.Kind = kind;
            res.ExtraInfo = extraInfo.Serialize();

            return res;
        }

        public OutcomeContext AddNewOutcomeContext(Outcome outcome, WarningContext context)
        {
            if (!this.IsValid) return null;

            var result = _model.NewOutcomeContext(outcome);
            result.WarningContext = context;
            return result;
        }

        public ContextEdge AddNewContextEdge(OutcomeOrSuggestion item, int rank)
        {
            if (!this.IsValid) return null;

            var result = _model.NewContextEdge(item);
            result.Rank = rank;
            return result;
        }

        public void SaveChanges(bool now = false)
        {
            if (!this.IsValid) return;

            _model.SaveChanges(now);
        }

        public ByteArray GetHashForDate(ByteArray methodId, DateTime datetime, bool afterDateTime)
        {
            if (!this.IsValid) return null;

            return _model.GetHashForDate(methodId, datetime, afterDateTime);
        }

        public Method NewMethodModel()
        {
            if (!this.IsValid) return null;

            return _model.NewMethod();
        }

        public void Dispose()
        {
            if (!this.IsValid) return;

            _model.Dispose();
        }


        [ContractVerification(false)] // cccheck does not understand exceptional block
        public void AddAssemblyBinding(Method methodModel, AssemblyInfo assemblyInfo)
        {
            Contract.Assume(_model != null);
            _model.AddOrUpdate(methodModel, assemblyInfo);
        }


        public string CacheName
        {
            [ContractVerification(false)] // cccheck does not understand exceptional block
            get
            {
                Contract.Assume(_model != null);
                return _model.CacheName;
            }
        }
    }
}
