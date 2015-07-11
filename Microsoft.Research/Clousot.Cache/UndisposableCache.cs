// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            Contract.Invariant(_underlying != null);
        }
        #endregion

        private readonly IClousotCache _underlying;

        public UndisposableCacheDataAccessor(IClousotCache underlying)
        {
            Contract.Requires(underlying != null);
            _underlying = underlying;
        }

        public bool IsValid { get { return _underlying.IsValid; } }

        void IDisposable.Dispose() { }

        public Metadata GetMetadataOrNull(string key, bool silent = false)
        {
            return _underlying.GetMetadataOrNull(key, silent);
        }

        public bool TryGetMethodModelForHash(DataStructures.ByteArray hash, out Method result)
        {
            return _underlying.TryGetMethodModelForHash(hash, out result);
        }

        public bool TryGetBaselineModel(string methodFullname, string baselineId, out Method result)
        {
            return _underlying.TryGetBaselineModel(methodFullname, baselineId, out result);
        }

        public AssemblyInfo AddAssemblyInfo(string name, Guid guid)
        {
            return _underlying.AddAssemblyInfo(name, guid);
        }

        public void AddOrUpdateMethod(Method methodModel, DataStructures.ByteArray methodId)
        {
            _underlying.AddOrUpdateMethod(methodModel, methodId);
        }

        public void AddOrUpdateBaseline(string fullname, string baselineId, Method baseline)
        {
            _underlying.AddOrUpdateBaseline(fullname, baselineId, baseline);
        }

        public void DeleteMethodModel(Method methodModel)
        {
            _underlying.DeleteMethodModel(methodModel);
        }

        public Outcome AddNewOutcome(Method method, string message, bool related)
        {
            return _underlying.AddNewOutcome(method, message, related);
        }

        public Suggestion AddNewSuggestion(Method method, string suggestion, string kind, ClousotSuggestion.Kind type, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
        {
            return _underlying.AddNewSuggestion(method, suggestion, kind, type, extraInfo);
        }

        public OutcomeContext AddNewOutcomeContext(Outcome outcome, WarningContext warningContext)
        {
            return _underlying.AddNewOutcomeContext(outcome, warningContext);
        }

        public ContextEdge AddNewContextEdge(OutcomeOrSuggestion item, int rank)
        {
            return _underlying.AddNewContextEdge(item, rank);
        }

        public void SaveChanges(bool now = false)
        {
            _underlying.SaveChanges(now);
        }

        public DataStructures.ByteArray GetHashForDate(DataStructures.ByteArray methodId, DateTime datetime, bool afterDateTime)
        {
            return _underlying.GetHashForDate(methodId, datetime, afterDateTime);
        }

        public Method NewMethodModel()
        {
            return _underlying.NewMethodModel();
        }

        public void AddAssemblyBinding(Method methodModel, AssemblyInfo assemblyInfo)
        {
            _underlying.AddAssemblyBinding(methodModel, assemblyInfo);
        }

        public bool TestCache() { return _underlying.TestCache(); }


        public string CacheName
        {
            get
            {
                return _underlying.CacheName + " (undisposable)";
            }
        }
    }

    public class UndisposableMemoryCacheFactory : IClousotCacheFactory
    {
        private IClousotCache _existing;

        IClousotCache IClousotCacheFactory.Create(IClousotCacheOptions options)
        {
            if (_existing == null)
            {
                var memCache = new MemClousotCacheFactory().Create(options);
                _existing = new UndisposableCacheDataAccessor(memCache);
            }
            return _existing;
        }
    }
}
