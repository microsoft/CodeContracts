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
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Objects; // for compiled queries
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microsoft.Research.CodeAnalysis
{
  abstract class SQLCacheDataAccessor : ICacheDataAccessor
  {
    private readonly int MaxCacheSize; // the maximum number of method for which warnings are stored.

    protected EntityConnection connection; // We keep the connection open so that we do not reload all the SQLce engine
    protected ClousotCacheEntities objectContext;

    private Dictionary<string, byte[]> metadataIfCreation;

    private readonly Func<ClousotCacheEntities, byte[], MethodModel> compiledGetMethod;
    private readonly Func<ClousotCacheEntities, long, Guid, bool> compiledGetBinding;
    private readonly Func<ClousotCacheEntities, int> compiledNbToDelete;

    protected SQLCacheDataAccessor(Dictionary<string, byte[]> metadataIfCreation, int maxCacheSize, CacheVersionParameters cacheVersionParameters, string[] EntityConnectionMetadataPaths)
    {
      this.metadataIfCreation = metadataIfCreation;
      this.MaxCacheSize = maxCacheSize;
      this.EntityConnectionMetadataPaths = EntityConnectionMetadataPaths;

      this.cacheVersion = cacheVersionParameters;

#if COMPILED_QUERIES
      this.compiledGetMethod = CompiledQuery.Compile<ClousotCacheEntities, byte[], MethodModel>(
        (context, hash) => context.MethodModels.FirstOrDefault(m => m.Hash == hash));
      this.compiledGetBinding = CompiledQuery.Compile<ClousotCacheEntities, long, Guid, bool>(
        (context, methodid, guid) => context.AssemblyBindings.Any(b => b.MethodId == methodid && b.AssemblyId == guid));
      this.compiledNbToDelete = CompiledQuery.Compile<ClousotCacheEntities, int>(
        context => context.MethodModels.Count() - this.MaxCacheSize);
#else
      this.compiledGetMethod = (context, hash) => context.MethodModels.FirstOrDefault(m => m.Hash == hash);
      this.compiledGetBinding = (context, methodid, guid) => context.AssemblyBindings.Any(b => b.MethodId == methodid && b.AssemblyId == guid);
      this.compiledNbToDelete = (context) => context.MethodModels.Count() - this.MaxCacheSize;
#endif

    }

    private readonly CacheVersionParameters cacheVersion;
    protected string currentAssembly;
    protected Guid currentAssemblyGuid;

    public bool IsValid { get { return this.connection != null; } }

    public virtual long GetNewId()
    {
      return default(long);
    }

    abstract protected bool DatabaseExist(out DbConnection dbConnection);
    abstract protected DbConnection CreateDatabase();
    abstract protected string TablesCreationQueries { get; }

    protected readonly string[] EntityConnectionMetadataPaths;
    static private readonly Assembly[] EntityConnectionMetadataAssembliesToConsider = new Assembly[]{Assembly.GetExecutingAssembly()};

    private EntityConnection OpenEntityConnection(DbConnection dbConnection)
    {
      var dbName = dbConnection.Database;
      if (dbConnection.State != ConnectionState.Closed)
        dbConnection.Close();
      var connection = new EntityConnection(new System.Data.Metadata.Edm.MetadataWorkspace(
        EntityConnectionMetadataPaths, EntityConnectionMetadataAssembliesToConsider), dbConnection);
      connection.Open();
      if (!String.IsNullOrEmpty(dbName))
        connection.StoreConnection.ChangeDatabase(dbName);
      return connection;
    }

    private bool OpenConnection(bool silent = false)
    {
      if (this.connection != null && this.connection.State == ConnectionState.Open)
        return true;
      try
      {
        DbConnection dbConnection;
        if (this.DatabaseExist(out dbConnection))
        {
          this.connection = this.OpenEntityConnection(dbConnection);
          this.objectContext = new ClousotCacheEntities(this.connection);
          return true;
        }

        dbConnection = this.CreateDatabase();
        this.connection = this.OpenEntityConnection(dbConnection);

        using (var trans = this.connection.StoreConnection.BeginTransaction())
        {
          using (var cmd = this.connection.StoreConnection.CreateCommand())
          {
            cmd.Transaction = trans;
            foreach (var query in this.TablesCreationQueries.Split(';'))
            {
              if (String.IsNullOrWhiteSpace(query))
                continue;
              cmd.CommandText = query;
              cmd.ExecuteNonQuery();
            }
          }
          trans.Commit();
        }

        this.objectContext = new ClousotCacheEntities(this.connection);
        foreach (var m in this.metadataIfCreation)
          this.objectContext.AddToCachingMetadatas(new CachingMetadata { Key = m.Key, Value = m.Value });
        this.TrySaveChanges();
        return true;
      }
      catch (Exception e)
      {
        if (!(e is SqlCeException || e is SqlException || e is IOException || e is DataException))
          throw;
        if (!silent)
          Console.WriteLine("Error: unable to open the cache file: " + e.Message);
        this.CloseConnection();
      }
      return false;
    }

    virtual protected void CloseDatabase()
    { }

    protected void CloseConnection()
    {
      if (this.connection != null)
      {
        this.connection.Close();
        this.connection.Dispose();
        this.connection = null;
      }
      if (this.objectContext != null)
      {
        this.objectContext.Dispose();
        this.objectContext = null;
      }
    }

    #region ICacheDataAccessor Members

    public CachingMetadata GetMetadataOrNull(string key, bool silent = false)
    {
      try
      {
        if (!this.OpenConnection(silent)) { return null; }
        return this.objectContext.CachingMetadatas.FirstOrDefault(m => m.Key == key);
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

    public bool TryGetMethodModelForHash(byte[] hash, out MethodModel result)
    {
      try
      {
        if (!this.OpenConnection())
        {
          result = null;
          return false;
        }
        result = this.compiledGetMethod(this.objectContext, hash);
        return result != null;
      }
      catch (Exception e)
      {
        if (!(e is SqlCeException || e is SqlException || e is IOException || e is DataException))
          throw;
        Console.WriteLine("Error: unable to read the cache: " + e.Message);
        result = null;
        return false;
      }
    }

    public void AddAssemblyBinding(MethodModel methodModel)
    {
        if (this.objectContext == null) return;
        if (this.compiledGetBinding(this.objectContext, methodModel.Id, this.currentAssemblyGuid))
            return;
        methodModel.AssemblyBindings.Add(new AssemblyBinding { AssemblyId = this.currentAssemblyGuid });
    }

    public void AddAssemblyInfo()
    {
        if (!this.OpenConnection()) return;
        var ainfo = objectContext.AssemblyInfoes.FirstOrDefault(a => a.AssemblyId == this.currentAssemblyGuid);
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
                this.TrySaveChanges(now: true);
            }
            return;
        }
        ainfo = new AssemblyInfo { 
            AssemblyId = this.currentAssemblyGuid,
            Created = DateTime.Now,
            Version = this.cacheVersion.Version,
            IsBaseLine = this.cacheVersion.SetBaseLine,
            Name = this.currentAssembly,
        };
        objectContext.AssemblyInfoes.AddObject(ainfo);
        this.TrySaveChanges(now: true);
    }


    public bool TryAddMethodModel(MethodModel methodModel)
    {
      try
      {
        if (!this.OpenConnection())
          return false;
        var oldEntry = this.compiledGetMethod(this.objectContext, methodModel.Hash);
        if (oldEntry != null)
        {
          this.objectContext.DeleteObject(oldEntry);
          // We force the removing
          this.objectContext.SaveChanges();
        }
        this.objectContext.AddToMethodModels(methodModel);
        this.objectContext.SaveChanges();
        AddAssemblyBinding(methodModel);
        this.objectContext.SaveChanges();

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
#endif
        return true;
      }
      catch (Exception e)
      {
        if (!(e is SqlCeException || e is SqlException || e is IOException || e is DataException))
          throw;
        Console.WriteLine("Error: unable to add the method in the cache: " + e.Message);
        return false;
      }
    }

    private int nbWaitingChanges = 0;
    const int MaxWaitingChanges = 500;

    public bool TrySaveChanges(bool now = true)
    {
      if (!now && ++this.nbWaitingChanges <= MaxWaitingChanges)
        return false;
      try
      {
        // The connection should already be opened
        this.objectContext.SaveChanges();
        this.nbWaitingChanges = 0;
        return true;
      }
      catch (Exception e)
      {
        if (!(e is SqlCeException || e is SqlException || e is IOException || e is DataException))
          throw;
        Console.WriteLine("Error: unable to save the changes: " + e.Message);
        return false;
      }
    }

    protected abstract void DeleteDatabase();

    public void Clear()
    {
      try
      {
        this.DeleteDatabase();
      }
      catch (Exception e)
      {
        if (!(e is SqlCeException || e is SqlException || e is IOException || e is DataException))
          throw;
        Console.WriteLine("Error: unable to delete the cache file: " + e.ToString());
        this.CloseConnection();
      }
    }

    public virtual void StartAssembly(string name, Guid guid)
    {
        this.currentAssembly = name;
        this.currentAssemblyGuid = guid;

        this.AddAssemblyInfo();
    }

    public virtual void EndAssembly()
    {
        this.currentAssembly = null;
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
      this.Dispose(true);
    }

    protected void Dispose(bool disposing)
    {
      this.CloseDatabase();
      this.CloseConnection();
    }

    #endregion

    ~SQLCacheDataAccessor()
    {
      this.Dispose(false);
    }
  }
}
