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
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public abstract class SQLCacheDataAccessor : ObjectContextEntityCacheDataAccessor<MethodModel, CachingMetadata, AssemblyInfo, AssemblyBinding, ClousotCacheEntities>
  {
    private EntityConnection connection; // We keep the connection open so that we do not reload all the SQLce engine

    private readonly Dictionary<string, byte[]> metadataIfCreation;
    private readonly string[] EntityConnectionMetadataPaths;

    static private readonly Assembly[] EntityConnectionMetadataAssembliesToConsider = new Assembly[] { Assembly.GetExecutingAssembly() }; // Thread-safe

#if COMPILED_QUERIES
    private readonly Func<ClousotCacheEntities, byte[], MethodModel> compiledGetMethodByHash;
    private readonly Func<ClousotCacheEntities, string, MethodModel> compiledGetMethodByName;
    private readonly Func<ClousotCacheEntities, int> compiledNbToDelete;
    private readonly Func<ClousotCacheEntities, long, Guid, bool> compiledGetBinding;
#endif

    protected SQLCacheDataAccessor(Dictionary<string, byte[]> metadataIfCreation, int maxCacheSize, CacheVersionParameters cacheVersionParameters, string[] EntityConnectionMetadataPaths)
      : base(maxCacheSize, cacheVersionParameters)
    {
      this.metadataIfCreation = metadataIfCreation;
      this.EntityConnectionMetadataPaths = EntityConnectionMetadataPaths;

#if COMPILED_QUERIES
      // we need to recompile them here because MethodModels & co are now IQueryable
      this.compiledNbToDelete = CompiledQuery.Compile<ClousotCacheEntities, int>(
        context => context.MethodModels.Count() - this.MaxCacheSize);
      this.compiledGetMethodByName = CompiledQuery.Compile<ClousotCacheEntities, string, MethodModel>(
        (context, name) => context.MethodModels.FirstOrDefault(m => m.Name == name));
      this.compiledGetBinding = CompiledQuery.Compile<ClousotCacheEntities, long, Guid, bool>(
        (context, methodid, guid) => context.AssemblyBindings.Any(b => b.MethodId == methodid && b.AssemblyId == guid));
      this.compiledGetMethodByHash = CompiledQuery.Compile<ClousotCacheEntities, byte[], MethodModel>(
        (context, hash) => context.MethodModels.FirstOrDefault(m => m.Hash == hash));
#endif
    }

#if COMPILED_QUERIES
    protected override MethodModel GetMethodByHash(ByteArray hash) { return this.compiledGetMethodByHash(this.entities, hash.Bytes); }
    protected override MethodModel GetMethodByName(string name) { return this.compiledGetMethodByName(this.entities, name); }
    protected override int NbToDelete() { return this.compiledNbToDelete(this.entities); }
    protected override bool GetBinding(MethodModel method, Guid guid) { return this.compiledGetBinding(this.entities, method.Id, guid); }
#else
    // we need to redefine them here because MethodModels & co are now IQueryable
    protected override MethodModel GetMethodByHash(ByteArray hash) { return this.entities.MethodModels.FirstOrDefault(m => m.Hash == hash.Bytes); }
    protected override MethodModel GetMethodByName(string name) { return this.entities.MethodModels.FirstOrDefault(m => m.Name == name); }
    protected override int NbToDelete() { return this.entities.MethodModels.Count() - this.MaxCacheSize; }
    protected override bool GetBinding(MethodModel method, Guid guid) { return this.entities.AssemblyBindings.Any(b => b.MethodId == method.Id && b.AssemblyId == guid);}
#endif

    protected virtual long GetNewId() { return 0; }

    public override bool IsValid { get { return this.connection != null; } }

    abstract protected bool DatabaseExist(out DbConnection dbConnection);
    abstract protected DbConnection CreateDatabase();
    abstract protected string TablesCreationQueries { get; }

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

    protected override ClousotCacheEntities CreateClousotCacheEntities(bool silent)
    {
      if (this.connection != null && this.connection.State == ConnectionState.Open)
        throw new InvalidOperationException("Connection already open");
      try
      {
        DbConnection dbConnection;
        if (this.DatabaseExist(out dbConnection))
        {
          this.connection = this.OpenEntityConnection(dbConnection);
          return new ClousotCacheEntities(this.connection, this.GetNewId);
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

        var objectContext = new ClousotCacheEntities(this.connection, this.GetNewId);
        foreach (var m in this.metadataIfCreation)
          objectContext.AddToCachingMetadatas(new CachingMetadata { Key = m.Key, Value = m.Value });
        objectContext.SaveChanges();
        return objectContext;
      }
      catch (Exception e)
      {
        if (!(e is SqlCeException || e is SqlException || e is IOException || e is DataException))
          throw;
        if (!silent)
          Console.WriteLine("Error: unable to open the cache file: " + e.Message);
        this.Close();
      }
      return null;
    }

    public override IMethodModel NewMethodModel() { return new MethodModel(this.GetNewId); }

    protected virtual void CloseDatabase()
    {
      if (this.connection == null)
        return;
      try
      {
        this.connection.Close();
        this.connection.Dispose();
      }
      catch (SqlException)
      { }
      catch (System.Data.EntityException)
      { }
      this.connection = null;
    }

    protected override void Close()
    {
      this.CloseDatabase();
      base.Close();
    }

    protected abstract void DeleteDatabase();

    public override void Clear()
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
        this.Close();
      }
    }
  }
}
