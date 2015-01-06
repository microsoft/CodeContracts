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

#define COMPILED_QUERIES
using System;
using System.Data;
using System.Data.Objects; // for compiled queries
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public abstract class ObjectContextEntityCacheDataAccessor<MM, CM, AI, AB, CacheEntities> : EntityCacheDataAccessor<MM, CM, AI, AB, CacheEntities>
    where MM : class, IMethodModel
    where CM : class, ICachingMetadata
    where AI : class, IAssemblyInfo
    where AB : class, IAssemblyBinding
    where CacheEntities : ObjectContext, IClousotCacheEntities<MM, CM, AI, AB>
  {
#if COMPILED_QUERIES
    private readonly Func<CacheEntities, int> compiledNbToDelete;
    private readonly Func<CacheEntities, string, MM> compiledGetMethodByName;
#endif

    protected ObjectContextEntityCacheDataAccessor(int maxCacheSize, CacheVersionParameters cacheVersionParameters)
      : base(maxCacheSize, cacheVersionParameters)
    {
#if COMPILED_QUERIES
      this.compiledNbToDelete = CompiledQuery.Compile<CacheEntities, int>(
        context => context.MethodModels.Count() - this.MaxCacheSize);
      this.compiledGetMethodByName = CompiledQuery.Compile<CacheEntities, string, MM>(
        (context, name) => context.MethodModels.FirstOrDefault(m => m.Name == name));
#endif

    }

#if COMPILED_QUERIES
    protected override MM GetMethodByName(string name) { return this.compiledGetMethodByName(this.entities, name); }
    protected override int NbToDelete() { return this.compiledNbToDelete(this.entities); }
#endif

    private bool EnsuresClousotCacheEntities(bool silent = false)
    {
      if (this.entities != null)
        return true;
      this.entities = this.CreateClousotCacheEntities(silent);
      return this.entities != null;
    }

    private int nbWaitingChanges = 0;
    const int MaxWaitingChanges = 500;

    public override bool TrySaveChanges(bool now = false)
    {
      if (!now && ++this.nbWaitingChanges <= MaxWaitingChanges)
      {
        return true;
      }
      try
      {
        // The connection should already be opened
        this.entities.SaveChanges();
        this.nbWaitingChanges = 0;

        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine("[cache] Encountered exception while trying to save changes: {0}", e.Message);

        if (!(e is SqlCeException || e is SqlException || e is IOException || e is DataException))
        {
          Console.WriteLine("[cache] -- the exception is not an SQL-related exception");
        }
        else if (e.InnerException != null)
        {
          Console.WriteLine("[cache] Inner exception: {0}", e.InnerException.Message);
        }

        //throw;
        return false;
      }
    }
  }
}
