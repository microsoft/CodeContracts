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
using Microsoft.Research.CodeAnalysis.Caching.Models;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Microsoft.Research.CodeAnalysis.Caching
{
    // ICacheModel contract binding

    [ContractClass(typeof (ICacheModelContract))]
    public partial interface ICacheModel
    {
    }

    [ContractClassFor(typeof (ICacheModel))]
    internal abstract class ICacheModelContract : ICacheModel
    {
        public IEnumerable<Method> Methods
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Method>>() != null);
                throw new NotImplementedException();
            }
        }

        public IEnumerable<Metadata> Metadatas
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Metadata>>() != null);
                throw new NotImplementedException();
            }
        }

        public IEnumerable<AssemblyInfo> AssemblyInfoes
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<AssemblyInfo>>() != null);
                throw new NotImplementedException();
            }
        }

        public Metadata MetadataByKey(string key)
        {
            Contract.Requires(key != null);
            throw new NotImplementedException();
        }

        public Method MethodByHash(byte[] hash)
        {
            Contract.Requires(hash != null);
            throw new NotImplementedException();
        }

        public Method BaselineByName(byte[] methodNameHash, string baselineName)
        {
            Contract.Requires(methodNameHash != null);
            Contract.Requires(baselineName != null);

            throw new NotImplementedException();
        }

        public Method NewMethod()
        {
            Contract.Ensures(Contract.Result<Method>() != null);
            throw new NotImplementedException();
        }

        public AssemblyInfo GetOrCreateAssemblyInfo(Guid assemblyGuid)
        {
            Contract.Ensures(Contract.Result<AssemblyInfo>() != null);

            throw new NotImplementedException();
        }

        public IdHashTimeToMethod NewHashDateBindingForNow(ByteArray methodIdHash, Method methodModel)
        {
            Contract.Requires(methodIdHash != null);
            Contract.Requires(methodModel != null);
            Contract.Ensures(Contract.Result<IdHashTimeToMethod>() != null);

            throw new NotImplementedException();
        }

        public Outcome NewOutcome(Method method)
        {
            Contract.Ensures(Contract.Result<Outcome>() != null);

            throw new NotImplementedException();
        }

        public Suggestion NewSuggestion(Method method)
        {
            Contract.Ensures(Contract.Result<Suggestion>() != null);

            throw new NotImplementedException();
        }

        public OutcomeContext NewOutcomeContext(Outcome outcome)
        {
            Contract.Ensures(Contract.Result<OutcomeContext>() != null);

            throw new NotImplementedException();
        }

        public ContextEdge NewContextEdge(OutcomeOrSuggestion item)
        {
            Contract.Ensures(Contract.Result<ContextEdge>() != null);

            throw new NotImplementedException();
        }

        public Metadata NewMetadata()
        {
            Contract.Ensures(Contract.Result<Metadata>() != null);

            throw new NotImplementedException();
        }

        public void DeleteMethodModel(Method methodModel)
        {
            Contract.Requires(methodModel != null);

            throw new NotImplementedException();
        }

        public ByteArray GetHashForDate(ByteArray methodIdHash, DateTime t, bool afterT)
        {
            Contract.Requires(methodIdHash != null);
            // It can return null!

            throw new NotImplementedException();
        }

        public bool IsValid
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsFresh
        {
            get { throw new NotImplementedException(); }
        }

        public void SaveChanges(bool now = false)
        {
            throw new NotImplementedException();
        }

        public void AddOrUpdate(AssemblyInfo ainfo)
        {
            Contract.Requires(ainfo != null);
            throw new NotImplementedException();
        }

        public void AddOrUpdate(Method methodModel)
        {
            Contract.Requires(methodModel != null);
            throw new NotImplementedException();
        }

        public void AddOrUpdate(IdHashTimeToMethod idhash)
        {
            Contract.Requires(idhash != null);
            throw new NotImplementedException();
        }

        public void AddOrUpdate(Metadata metadata)
        {
            Contract.Requires(metadata != null);

            throw new NotImplementedException();
        }

        public void AddOrUpdate(BaselineMethod baseline)
        {
            Contract.Requires(baseline != null);
            throw new NotImplementedException();
        }

        public void AddOrUpdate(Method methodModel, AssemblyInfo assemblyInfo)
        {
            Contract.Requires(methodModel != null);
            Contract.Requires(assemblyInfo != null);

            throw new NotImplementedException();
        }

        public string CacheName
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);

                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public partial interface ICacheModel : IDisposable
    {
        IEnumerable<Method> Methods { get; }
        IEnumerable<Metadata> Metadatas { get; }
        IEnumerable<AssemblyInfo> AssemblyInfoes { get; }

        Metadata MetadataByKey(string key);
        Method MethodByHash(byte[] hash);
        Method BaselineByName(byte[] methodNameHash, string baselineName);
        Method NewMethod();
        
        AssemblyInfo GetOrCreateAssemblyInfo(Guid assemblyGuid);
        IdHashTimeToMethod NewHashDateBindingForNow(ByteArray methodIdHash, Method methodModel);

        Outcome NewOutcome(Method method);
        Suggestion NewSuggestion(Method method);
        OutcomeContext NewOutcomeContext(Outcome outcome);
        ContextEdge NewContextEdge(OutcomeOrSuggestion item);
        Metadata NewMetadata();

        void DeleteMethodModel(Method methodModel);

        ByteArray GetHashForDate(ByteArray methodIdHash, DateTime t, bool afterT);

        bool IsValid { get; }

        /// <summary>
        /// True if the database was freshly created.
        /// </summary>
        bool IsFresh { get; }

        void SaveChanges(bool now = false);

        void AddOrUpdate(AssemblyInfo ainfo);
        void AddOrUpdate(Method methodModel);
        void AddOrUpdate(IdHashTimeToMethod idhash);
        void AddOrUpdate(Metadata metadata);
        void AddOrUpdate(BaselineMethod baseline);
        void AddOrUpdate(Method methodModel, AssemblyInfo assemblyInfo);

        string CacheName { get; }
    }

    public interface IClousotCacheFactory
    {
        IClousotCache Create(IClousotCacheOptions options);
    }

    public static class ClousotCacheFactoryExtensions
    {
        /// <summary>
        /// Extension method that creates cache asynchronously.
        /// </summary>
        /// <remarks>
        /// Task's result would be null if <see cref="factory"/>'s Create method returns null or when
        /// TestCache will return false.
        /// </remarks>
        public static Task<IClousotCache> CreateAndCheckAsync(this IClousotCacheFactory factory, IClousotCacheOptions options)
        {
            Contract.Requires(factory != null);
            Contract.Requires(options != null);

            // This implementation violates common TPL practice that suggest not to wrap
            // sync functions into async. But this helps to fix particular issue with
            // cache selection.
            // This implementation could be refined in the future by switching to appropriate
            // asynchronous implementation.

            return Task.Factory.StartNew(() =>
            {
                var cache = factory.Create(options);
                
                if (cache == null || !cache.TestCache())
                {
                    return null;
                }

                return cache;
            });
        }
    }
}
