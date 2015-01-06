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
using System.Data;
using System.Data.Objects; // for compiled queries
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public abstract class EntityCacheDataAccessor<MM, CM, AI, AB, CacheEntities> : ICacheDataAccessor
    where MM : class, IMethodModel
    where CM : class, ICachingMetadata
    where AI : class, IAssemblyInfo
    where AB : class, IAssemblyBinding
    where CacheEntities : IClousotCacheEntities<MM, CM, AI, AB>
  {
    protected CacheEntities entities;

    protected readonly int MaxCacheSize; // the maximum number of method for which warnings are stored.
    private readonly CacheVersionParameters cacheVersion;

    protected EntityCacheDataAccessor(int maxCacheSize, CacheVersionParameters cacheVersionParameters)
    {
      this.MaxCacheSize = maxCacheSize;

      this.cacheVersion = cacheVersionParameters;
    }

    protected abstract CacheEntities CreateClousotCacheEntities(bool silent);

    protected virtual MM GetMethodByHash(ByteArray hash) { return this.entities.MethodModels.FirstOrDefault(m => hash.Equals(m.Hash)); }
    protected virtual MM GetMethodByName(string name) { return this.entities.MethodModels.FirstOrDefault(m => m.Name == name); }
    protected virtual bool GetBinding(MM method, Guid guid) { return this.entities.AssemblyBindings.Any(b => b.AssemblyId == guid && b.Method.Hash.Equals(method.Hash)); }
    protected virtual int NbToDelete() { return this.entities.MethodModels.Count() - this.MaxCacheSize; }
    
    private bool EnsuresClousotCacheEntities(bool silent = false)
    {
      if (this.entities != null)
        return true;
      this.entities = this.CreateClousotCacheEntities(silent);
      return this.entities != null;
    }

    public abstract bool IsValid { get; }

    public abstract void Clear();

    protected virtual void Close()
    {
      if (this.entities != null)
      {
        this.entities.Dispose();
        this.entities = default(CacheEntities);
      }
    }

    public virtual ByteArray GetHashForDate(ByteArray methodIdHash, DateTime t, bool afterT)
    {
      this.EnsuresClousotCacheEntities();
      return this.entities.GetHashForDate(methodIdHash, t, afterT);
    }

    public virtual IMethodModel NewMethodModel()
    {
      this.EnsuresClousotCacheEntities();
      return this.entities.NewMethodModel();
    }

    public virtual ICachingMetadata GetMetadataOrNull(string key, bool silent = false)
    {
      try
      {
        if (!this.EnsuresClousotCacheEntities(silent)) { return null; }
        return this.entities.CachingMetadatas.FirstOrDefault(m => m.Key == key);
      }
      catch (Exception e)
      {
        if (!(e is SqlCeException || e is SqlException || e is IOException || e is DataException))
          throw;
        if (!silent)
          Console.WriteLine("Error: unable to read the metadata in the cache file: " + e.Message);
        return null;
      }
    }

    public virtual bool TryGetMethodModelForHash(ByteArray hash, out IMethodModel result)
    {
      try
      {
        if (!this.EnsuresClousotCacheEntities())
        {
          result = default(MM);
          return false;
        }
        result = this.GetMethodByHash(hash);
        return result != null;
      }
      catch (Exception e)
      {
        if (!(e is SqlCeException || e is SqlException || e is IOException || e is DataException))
          throw;
        Console.WriteLine("Error: unable to read the cache: " + e.Message);
        result = default(MM);
        return false;
      }
    }

    public virtual bool TryGetMethodModelForName(string name, out IMethodModel result)
    {
      try
      {
        if (!this.EnsuresClousotCacheEntities())
        {
          result = default(MM);
          return false;
        }
        result = this.GetMethodByName(name);
        return result != null;
      }
      catch (Exception e)
      {
        if (!(e is SqlCeException || e is SqlException || e is IOException || e is DataException))
          throw;
        Console.WriteLine("Error: unable to read the cache while getting method by name: " + e.Message);
        result = default(MM);
        return false;
      }
    }

    public virtual void AddAssemblyBinding(Guid assemblyGuid, IMethodModel methodModel)
    {
      if (this.entities == null) return;
      if (this.GetBinding((MM)methodModel, assemblyGuid))
        return;
      methodModel.AddNewAssemblyBinding(assemblyGuid);
    }

    public virtual void AddAssemblyInfo(string assemblyName, Guid assemblyGuid)
    {
      if (!this.EnsuresClousotCacheEntities()) return;
      var ainfo = entities.AssemblyInfoes.FirstOrDefault(a => a.AssemblyId == assemblyGuid);
      if (ainfo != null)
      {
        if (this.cacheVersion.Version >= 0 || this.cacheVersion.SetBaseLine)
        {
          ainfo.Version = this.cacheVersion.Version;
          if (this.cacheVersion.SetBaseLine)
          {
            // only ever set it. Don't clear it.
            ainfo.IsBaseLine = this.cacheVersion.SetBaseLine;
          }
          this.TrySaveChanges(now: true); // why now?
        }
        return;
      }
      ainfo = this.entities.AddNewAssemblyInfo();
      ainfo.AssemblyId = assemblyGuid;
      ainfo.Created = DateTime.Now;
      ainfo.Version = this.cacheVersion.Version;
      ainfo.IsBaseLine = this.cacheVersion.SetBaseLine;
      ainfo.Name = assemblyName;
      this.TrySaveChanges(now: true); // why now?
    }

    public virtual bool TryAddMethodModel(IMethodModel _methodModel, ByteArray methodId)
    {
      var methodModel = (MM)_methodModel;

      try
      {
        if (!this.EnsuresClousotCacheEntities())
          return false;
        var oldEntry = this.GetMethodByHash(methodModel.Hash);
        if (oldEntry != null)
        {
          this.entities.DeleteMethodModel(oldEntry);
          // We force the removing
          this.TrySaveChanges(now: true);
        }
        this.entities.AddMethodModel(methodModel);
        this.entities.AddHashDateBindingForNow(methodId, methodModel);

        // If it fails, it throws an exception that we capture in the catch block
        return this.TrySaveChanges(); 

        // FIFO algorithm
#if false // ignore FIFO for now
        var nbToDelete = this.compiledNbToDelete(this.objectContext);
        if (nbToDelete > 0)
        {
          var toDelete = this.compiledMethodsToDelete(this.objectContext, nbToDelete);
          foreach (var m in toDelete)
            this.objectContext.DeleteObject(m);
          this.objectContext.SaveChanges();
        }
        return true;
#endif

      }
      catch (Exception e)
      {
        if (!(e is SqlCeException || e is SqlException || e is IOException || e is DataException))
        {
          throw;
        }
         
        Console.WriteLine("[cache] Encountered an error while saving the method {0} (hash {1})", methodModel.Name, methodModel.Hash, methodModel);

        Console.WriteLine("[cache] Removing the method model from the entities");
        this.entities.DeleteMethodModel(methodModel);
        // TODO: delete hashDate binding 

        if(e.InnerException != null)
        {
          Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
        }

        return false;
      }
    }

    public abstract bool TrySaveChanges(bool now = false);

    #region IDisposable Members

    protected void Dispose(bool disposing)
    {
      this.Close();
    }

    public void Dispose()
    {
      this.Dispose(true);
    }

    #endregion

    ~EntityCacheDataAccessor()
    {
      this.Dispose(false);
    }
  }
}

