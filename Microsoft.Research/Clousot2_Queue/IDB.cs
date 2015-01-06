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
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.CodeAnalysis.Caching;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  interface IDB : Caching.IClousotCacheFactory
  {
    /// <summary>
    /// Add to the database a new SliceDefinition
    /// </summary>
    ISliceId RegisterSlice(SliceDefinition sliceDef);

    /// <summary>
    /// Given a slice ID and a point in time, returns the corresponding SliceHash
    /// </summary>
    ISliceHash ComputeSliceHash(ISliceId sliceId, DateTime t);

    /// <summary>
    /// Have already seen this slice?
    /// </summary>
    bool AlreadyComputed(ISliceId sliceId, ISliceHash sliceHash);

    /// <summary>
    /// Notify we are done computing the slice
    /// </summary>
    void MarkAsComputed(ISliceId sliceId, ISliceHash sliceHash);

    /// <summary>
    /// Given a slice Id, returns its dependences as slices. Dependencies are callers and callees
    /// </summary>
    IEnumerable<ISliceId> Dependences(ISliceId sliceId);

    /// <summary>
    /// Given a slice Id, returns all the slices in the same class
    /// </summary>
    IEnumerable<ISliceId> SlicesForMethodsInTheSameType(ISliceId sliceId);
  }

  class MemorySingletonDB : SliceDataAccessor
  {
    public MemorySingletonDB() : base(new IClousotCacheFactory[] { new MemClousotCacheFactory() }) { }
  }

  class StdDB : SliceDataAccessor
  {
    public StdDB(IClousotCacheOptions options)
      : base(new IClousotCacheFactory[] { 
          new SQLClousotCacheFactory(options.CacheServer) ,
          //new SQLClousotCacheFactory(@"localhost\sqlexpress", true),
          new LocalDbClousotCacheFactory(),
      })
    {
      Contract.Requires(options != null);
    }
  }

  static class IDBExtensions
  {
    public static IEnumerable<IDB> AsEnumerable(this IDB db) { return new IDB[] { db }; }
  }
}
