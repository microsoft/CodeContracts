// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.CodeAnalysis.Caching.Models;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis.Caching
{
    internal struct Table<T>
    {
        private object _lockValue;
        private List<T> _list;

        public Table(int dummy)
        {
            _lockValue = new Object();
            _list = new List<T>();
        }

        public object Lock { get { return _lockValue; } }

        public T Find(Predicate<T> p)
        {
            Contract.Requires(p != null);

            lock (_lockValue)
            {
                Contract.Assume(_list != null);
                return _list.Find(p);
            }
        }

        public void Remove(Predicate<T> p)
        {
            Contract.Requires(p != null);

            lock (_lockValue)
            {
                Contract.Assume(_list != null);
                var i = _list.FindIndex(p);
                if (i >= 0)
                {
                    _list.RemoveAt(i);
                }
            }
        }

        public void AddOrUpdate(Predicate<T> p, T value)
        {
            Contract.Requires(p != null);

            lock (_lockValue)
            {
                Contract.Assume(_list != null);
                var i = _list.FindIndex(p);
                if (i >= 0)
                {
                    _list[i] = value;
                }
                else
                {
                    _list.Add(value);
                }
            }
        }

        public ICollection<T> Entries { get { return _list; } }
    }

    [ContractVerification(false)]
    public class MemCacheModel : ICacheModel
    {
        private Table<Method> _methodTable = new Table<Method>(0);
        private Table<Metadata> _metadataTable = new Table<Metadata>(0);
        private Table<AssemblyInfo> _assemblyInfoTable = new Table<AssemblyInfo>(0);
        private Table<BaselineMethod> _baselineTable = new Table<BaselineMethod>(0);
        private readonly object _idHashLock = new object();
        public readonly Dictionary<ByteArray, SortedList<DateTime, ByteArray>> methodHashFromId = new Dictionary<ByteArray, SortedList<DateTime, ByteArray>>();

        public IEnumerable<Method> Methods
        {
            get { return _methodTable.Entries.AssumeNotNull(); }
        }

        public IEnumerable<Metadata> Metadatas
        {
            get { return _metadataTable.Entries.AssumeNotNull(); }
        }

        public IEnumerable<AssemblyInfo> AssemblyInfoes
        {
            get { return _assemblyInfoTable.Entries.AssumeNotNull(); }
        }

        public Metadata MetadataByKey(string key)
        {
            return _metadataTable.Find(m => m.Key == key);
        }

        public Method MethodByHash(byte[] hash)
        {
            return _methodTable.Find(m => m.Hash.ContentEquals(hash));
        }

        public Method BaselineByName(byte[] methodFullNameHash, string baselineId)
        {
            var candidate = _baselineTable.Find(m => m.MethodFullNameHash.ContentEquals(methodFullNameHash) && m.BaselineId == baselineId);
            if (candidate != null) return candidate.Method;
            return null;
        }

        public Metadata NewMetadata() { return new Metadata(); }

        public Method NewMethod()
        {
            return new Method();
        }

        public AssemblyInfo GetOrCreateAssemblyInfo(Guid guid)
        {
            return new AssemblyInfo { AssemblyId = guid };
        }

        public IdHashTimeToMethod NewHashDateBindingForNow(DataStructures.ByteArray methodIdHash, Method methodModel)
        {
            var result = new IdHashTimeToMethod { Method = methodModel, MethodIdHash = methodIdHash.Bytes, Time = DateTime.Now };
            return result;
        }

        public Outcome NewOutcome(Method method)
        {
            var result = new Outcome { Method = method };
            method.Outcomes.Add(result);
            return result;
        }

        public Suggestion NewSuggestion(Method method)
        {
            var result = new Suggestion { Method = method };
            method.Suggestions.Add(result);
            return result;
        }

        public OutcomeContext NewOutcomeContext(Outcome outcome)
        {
            var result = new OutcomeContext { Outcome = outcome };
            outcome.OutcomeContexts.Add(result);
            return result;
        }

        public ContextEdge NewContextEdge(OutcomeOrSuggestion item)
        {
            Outcome outcome = item as Outcome;
            if (outcome != null)
            {
                var result = new OutcomeContextEdge { Outcome = outcome };
                outcome.OutcomeContextEdges.Add(result);
                return result;
            }
            Suggestion sugg = item as Suggestion;
            if (sugg != null)
            {
                var result = new SuggestionContextEdge { Suggestion = sugg };
                sugg.SuggestionContextEdges.Add(result);
                return result;
            }
            return null;
        }


        public void DeleteMethodModel(Method methodModel)
        {
            _methodTable.Remove(m => m.Hash.ContentEquals(methodModel.Hash));
        }

        public ByteArray GetHashForDate(ByteArray methodIdHash, DateTime t, bool afterT)
        {
            lock (_idHashLock)
            {
                SortedList<DateTime, ByteArray> hashes;
                if (!this.methodHashFromId.TryGetValue(methodIdHash, out hashes))
                    return null;
                Contract.Assume(hashes != null);
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
        }

        public bool IsValid
        {
            get { return true; }
        }

        public bool IsFresh
        {
            get { return true; }
        }

        public void SaveChanges(bool now)
        {
        }


        public void AddOrUpdate(AssemblyInfo ainfo)
        {
            _assemblyInfoTable.AddOrUpdate(ai => ai.AssemblyId == ainfo.AssemblyId, ainfo);
        }

        public void AddOrUpdate(Metadata value)
        {
            _metadataTable.AddOrUpdate(md => md.Key == value.Key, value);
        }

        public void AddOrUpdate(Method methodModel)
        {
            _methodTable.AddOrUpdate(m => m.Hash.ContentEquals(methodModel.Hash), methodModel);
        }

        public void AddOrUpdate(BaselineMethod baseline)
        {
            _baselineTable.AddOrUpdate(m => m.MethodFullNameHash.ContentEquals(baseline.MethodFullNameHash) && m.BaselineId == baseline.BaselineId, baseline);
        }

        public void AddOrUpdate(IdHashTimeToMethod idhash)
        {
            lock (_idHashLock)
            {
                var datetime = idhash.Time;
                SortedList<DateTime, ByteArray> hashes;
                if (!this.methodHashFromId.TryGetValue(idhash.MethodIdHash, out hashes))
                {
                    this.methodHashFromId.Add(idhash.MethodIdHash, hashes = new SortedList<DateTime, ByteArray>());
                }
                else
                {
                    Contract.Assume(hashes != null, "we do not know the invariant on methodHashFromId");
                }
                Console.WriteLine("[cache] Adding an entry in the method cache model at time: {0}", datetime);

                hashes[datetime] = idhash.Method.Hash;
            }
        }

        public void AddOrUpdate(Method method, AssemblyInfo assemblyInfo)
        {
            lock (_assemblyInfoTable.Lock)
            {
                if (!method.Assemblies.Where(a => a.AssemblyId == assemblyInfo.AssemblyId).Any())
                {
                    method.Assemblies.Add(assemblyInfo);
                }
            }
        }

        public void Dispose()
        {
        }


        public string CacheName
        {
            get { return "Memory cache"; }
        }
    }

    public class MemClousotCacheFactory : IClousotCacheFactory
    {
        public MemClousotCacheFactory()
        {
        }

        public IClousotCache Create(IClousotCacheOptions options)
        {
            Contract.Ensures(Contract.Result<IClousotCache>() != null);

            var model = new MemCacheModel();
            return new ClousotCache(model, options);
        }
    }
}
