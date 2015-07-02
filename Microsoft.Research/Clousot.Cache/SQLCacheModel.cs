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

#define EF5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.CodeAnalysis.Caching.Models;
using System.Data.Entity;
using Microsoft.Research.DataStructures;
using System.Data.SqlClient;
using Microsoft.Research.CodeAnalysis;

//temp
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;
// Entityframework v5:
#if EF5 
using System.Data.Objects;
#else
// Entity Framework v6
using System.Data.Entity.Core.Objects;
#endif

using System.Linq.Expressions;
using System.Diagnostics.Contracts;
using System.IO;

namespace Microsoft.Research.CodeAnalysis.Caching
{
  static class IndexExtension
  {
    public static void CreateUniqueIndex<TModel>(this DbContext context, Expression<Func<TModel, object>> expression)
    {
      Contract.Requires(context != null);
      Contract.Requires(expression != null);
      // Assumes singular table name matching the name of the Model type

      var tableName = typeof(TModel).Name + "s"; // hack. No way to read the configuration back? IF we use non-standard table names, this won't work
      var columnName = GetLambdaExpressionName(expression.Body);
      var indexName = string.Format("IX_{0}_{1}", tableName, columnName);

      var createIndexSql = string.Format("CREATE INDEX {0} ON {1} ({2})", indexName, tableName, columnName);

      try
      {
        Contract.Assume(context.Database != null);
        context.Database.ExecuteSqlCommand(createIndexSql);
      }
      catch
      { }
    }

    public static string GetLambdaExpressionName(Expression expression)
    {
      MemberExpression memberExp = expression as MemberExpression;

      if (memberExp == null)
      {
        // Check if it is an UnaryExpression and unwrap it
        var unaryExp = expression as UnaryExpression;
        if (unaryExp != null)
          memberExp = unaryExp.Operand as MemberExpression;
      }

      if (memberExp == null)
        throw new ArgumentException("Cannot get name from expression", "expression");

      return memberExp.Member.Name;
    }

  }

  public abstract class SQLCacheModel : ClousotCacheContext, ICacheModel
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(cachename != null);
    }

    
    private bool isFresh;
    private int nbWaitingChanges = 0;
    const int MaxWaitingChanges = 500;
    private DateTime lastSave;
    const int MaxWaitTime = 30; // seconds
    readonly private bool trace;
    readonly protected string cachename;

    public SQLCacheModel(string connection, string cachename, bool trace = false)
      : base(connection)
    {
      Contract.Requires(cachename != null);

      this.trace = trace;
      Contract.Assume(this.Configuration != null);
      this.Configuration.AutoDetectChangesEnabled = false;
      this.Configuration.ValidateOnSaveEnabled = false;
      lastSave = DateTime.Now;
      this.cachename = cachename;
    }

    /// <summary>
    /// Called when created for first time
    /// </summary>
    protected void Initialize()
    {
      this.isFresh = true;
      this.CreateUniqueIndex<Method>(x => x.Hash);
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }

    public new IEnumerable<Method> Methods
    {
      get
      {
        return base.Methods;
      }
    }

    public new IEnumerable<Metadata> Metadatas
    {
      get
      {
        return base.Metadatas;
      }
    }

    public new IEnumerable<AssemblyInfo> AssemblyInfoes
    {
      get
      {
        return base.AssemblyInfoes;
      }
    }

    public Metadata MetadataByKey(string key)
    {
      try
      {
        return base.Metadatas.Find(key);
      }
      catch (Exception e)
      {
        if (this.trace)
        {
          Console.WriteLine("[cache] SqlCacheModel: MetadataByKey failed: {0}", e.Message);
        }
        return default(Metadata);
      }
    }

    public Method MethodByHash(byte[] hash)
    {
      try
      {
        var query = base.Methods.Where(m => m.Hash.Equals(hash));
        
        return query.FirstOrDefault();
      }
      catch (Exception e)
      {
        if (this.trace)
        {
          Console.WriteLine("[cache] SqlCacheModel: MethodByHash failed: {0}", e.Message);
        }
        return null;
      }
    }

    public Method BaselineByName(byte[] methodNameHash, string baselineName)
    {
      try
      {
        var candidate = base.BaselineMethods.Find(methodNameHash, baselineName);
        //var candidate = base.BaselineMethods.Where(m => m.MethodFullName == methodName && m.BaselineId == baselineName).FirstOrDefault();
        if (candidate != null) return candidate.Method;
        return null;
      }
      catch (Exception e)
      {
        if (this.trace)
        {
          Console.WriteLine("[cache] SqlCacheModel: MethodByName failed: {0}", e.Message);
        }
        return null;
      }
    }

    public Metadata NewMetadata()
    {
      return new Metadata();
    }

    public Method NewMethod()
    {
      return new Method();
    }

    public Outcome NewOutcome(Method method)
    {
      var result = new Outcome() { Method = method };
      if (ValidMethodModel(method))
      {
        method.Outcomes.AssumeNotNull().Add(result);
      }
      return result;
    }

    public Suggestion NewSuggestion(Method method)
    {
      var result = new Suggestion() { Method = method };
      if (ValidMethodModel(method))
      {
        method.Suggestions.AssumeNotNull().Add(result);
      }
      return result;
    }

    public OutcomeContext NewOutcomeContext(Outcome outcome)
    {
      var result = new OutcomeContext();
      if (ValidMethodModel(outcome.Method))
      {
        outcome.OutcomeContexts.Add(result);
      }
      return result;
    }

    public ContextEdge NewContextEdge(OutcomeOrSuggestion item)
    {
      // TODO: this might now work if the underlying DfContext generates proxies.

      Outcome outcome = item as Outcome;
      if (outcome != null)
      {
        var result = new OutcomeContextEdge();
        Contract.Assume(outcome.Method != null);
        if (ValidMethodModel(outcome.Method))
        {
          outcome.OutcomeContextEdges.Add(result);
        }
        return result;
      }
      Suggestion sugg = item as Suggestion;
      if (sugg != null)
      {
        Contract.Assume(sugg.Method != null);
        var result = new SuggestionContextEdge();
        if (ValidMethodModel(sugg.Method))
        {
          sugg.SuggestionContextEdges.Add(result);
        }
        return result;
      }
      return null;
    }

    public void DeleteMethodModel(Method methodModel)
    {
      try
      {
        base.Methods.Remove(methodModel);
      }
      catch (Exception e)
      {
        if (this.trace)
        {
          Console.WriteLine("[cache] SqlCacheModel: DeleteMethodModel failed: {0}", e.Message);
        }
      }
    }

    public AssemblyInfo GetOrCreateAssemblyInfo(Guid guid)
    {
      var result = base.AssemblyInfoes.Find(guid);
      if (result == null)
      {
        result = base.AssemblyInfoes.Create();
        Contract.Assume(result != null);
        result.AssemblyId = guid;
        base.AssemblyInfoes.Add(result);
      }

      return result;
    }

    public IdHashTimeToMethod NewHashDateBindingForNow(ByteArray methodIdHash, Method methodModel)
    {
      var result = new IdHashTimeToMethod { Method = methodModel, MethodIdHash = methodIdHash.Bytes, Time = DateTime.Now };
      return result;
    }

    public ByteArray GetHashForDate(ByteArray methodIdHash, DateTime t, bool afterT)
    {
      try
      {
        var methodIdHashBytes = methodIdHash.Bytes;
        var latest = base.IdHashTimeToMethods
          //        .Where(b => b.MethodIdHash.Equals(methodIdHashBytes) && (afterT? b.Time >= t : b.Time <= t))
          .Where(b => b.MethodIdHash.Equals(methodIdHashBytes))
          .OrderByDescending(b => b.Time)
          .FirstOrDefault();

        if (latest == null)
          return null;
        return latest.Method.AssumeNotNull().Hash;
      }
      catch (Exception e)
      {
        if (this.trace)
        {
          Console.WriteLine("[cache] SqlCacheModel: GetHashForDate failed: {0}", e.Message);
        }
        return null;
      }
    }

    public bool IsValid
    {
      get
      {
        try {
          return base.Database != null
            && base.Database.Connection != null
            && base.Database.Connection.State != System.Data.ConnectionState.Broken;
        }
        catch (Exception e)
        {
          if (this.trace)
          {
            Console.WriteLine("[cache] SqlCacheModel: IsValid failed: {0}", e.Message);
          }
          return false;
        }
      }
    }

    public bool IsFresh { get { return this.isFresh; } }

    public void SaveChanges(bool now)
    {
      if (!now && ++this.nbWaitingChanges <= MaxWaitingChanges && (DateTime.Now - this.lastSave).Seconds < MaxWaitTime)
        return;

      this.lastSave = DateTime.Now;
      for (var i = 0; i < 3; i++)
      {
        try
        {
          if (this.trace)
          {
            Console.WriteLine("[cache] SqlCacheModel: SaveChanges saving...");
          }
          Contract.Assume(this.Configuration != null);
          this.Configuration.AutoDetectChangesEnabled = true;
          base.SaveChanges();
          Contract.Assume(this.Configuration != null);
          this.Configuration.AutoDetectChangesEnabled = false;
          this.nbWaitingChanges = 0;
        }
        catch (Exception e)
        {
          if (this.trace || i == 2)
          {
            Console.WriteLine("[cache] SqlCacheModel: SaveChanges failed: {0}", e.Message);

            foreach (var result in this.GetValidationErrors())
            {
              foreach (var error in result.ValidationErrors)
              {
                Console.WriteLine("validation error: {0}", error.ErrorMessage);
              }
            }

            for (var inner = e.InnerException; inner != null; inner = inner.InnerException)
            {
              Console.WriteLine("Innner exception: {0}", inner.Message);
            }
          }
          FixupPendingChanges();
          continue;
        }
        break;
      }
    }

    private void FixupPendingChanges()
    {
      var objContext = (this as IObjectContextAdapter).ObjectContext;
      Contract.Assume(objContext != null);
      RemoveDuplicateAdds<AssemblyInfo>(objContext, ai => base.AssemblyInfoes.Find(ai.AssemblyId));
      RemoveDuplicateAdds<Method>(objContext, method => this.MethodByHash(method.Hash));
    }

    private void RemoveDuplicateAdds<T>(ObjectContext objContext, Func<T,T> getStored) where T:class
    {
      Contract.Requires(objContext != null);
      Contract.Requires(getStored != null);
      Contract.Assume(this.ChangeTracker != null);
      Contract.Assume(this.ChangeTracker.Entries<T>() != null);
#if EF5
      var pendingAssemblyInfo = this.ChangeTracker.Entries<T>().AssumeNotNull().Where(a => a.State == System.Data.EntityState.Added);
#else
      var pendingAssemblyInfo = this.ChangeTracker.Entries<T>().AssumeNotNull().Where(a => a.State == System.Data.Entity.EntityState.Added);
#endif
      foreach (var p in pendingAssemblyInfo)
      {
        Contract.Assume(p != null);
        if (getStored(p.Entity) != null) {
          objContext.Detach(p.Entity);
        }
      }
    }

    public void AddOrUpdate(Metadata value)
    {
      try
      {
        base.Metadatas.AddOrUpdate(value);
      }
      catch (Exception e)
      {
        if (this.trace)
        {
          Console.WriteLine("[cache] SqlCacheModel: AddOrUpdate Metadata failed: {0}", e.Message);
        }
      }
    }

    public void AddOrUpdate(AssemblyInfo ainfo)
    {
      return; // nothing to do if we go through our construction.
      //try
      //{
      //  base.AssemblyInfoes.AddOrUpdate(ainfo);
      //}
      //catch (Exception e)
      //{
      //  if (this.trace)
      //  {
      //    Console.WriteLine("[cache] SqlCacheModel: AddOrUpdate AssemblyInfo failed: {0}", e.Message);
      //  }
      //}

    }

    private bool ValidMethodModel(Method methodModel, bool warn = false)
    {
      if (methodModel.FullName == null) return true; // not set yet?
      if (methodModel.FullName.Length > CacheUtils.MaxMethodLength)
      {
        Console.WriteLine("[cache] Won't cache Method {0}. FullName is too long: {1} > {2}", methodModel.FullName, methodModel.FullName.Length, CacheUtils.MaxMethodLength);
        return false;
      }
      return true;
    }

    public void AddOrUpdate(Method methodModel)
    {
      if (!ValidMethodModel(methodModel, warn:true)) return;
      try
      {
        // don't use Migrations.AddOrUpdate method. It is really slow. Assume we can update (no cache hit)
        base.Methods.Add(methodModel);
      }
      catch (Exception e)
      {
        if (this.trace)
        {
          Console.WriteLine("[cache] SqlCacheModel: AddOrUpdate MethodModel failed: {0}", e.Message);
        }
      }
    }

    public void AddOrUpdate(IdHashTimeToMethod idhash)
    {
      if (!ValidMethodModel(idhash.Method)) return;
      try
      {
        // assume no duplicates
        base.IdHashTimeToMethods.Add(idhash);
      }
      catch (Exception e)
      {
        if (this.trace)
        {
          Console.WriteLine("[cache] SqlCacheModel: AddOrUpdate IdHashTimeToMethod failed: {0}", e.Message);
        }
      }
    }

    public void AddOrUpdate(Method method, AssemblyInfo assemblyInfo)
    {
        if (!ValidMethodModel(method)) return;
        try
        {
            if (!method.Assemblies.AssumeNotNull()
              .Where(a => a.AssemblyId == assemblyInfo.AssemblyId).AssumeNotNull()
              .Any())
            {
                method.Assemblies.Add(assemblyInfo);
            }
        }
        catch (Exception e)
        {
            if (this.trace)
            {
                Console.WriteLine("[cache] SqlCacheModel: AddOrUpdate method assembly binding failed: {0}", e.Message);
            }
        }
    }

    public void AddOrUpdate(BaselineMethod baseline)
    {
      try
      {
        var candidate = base.BaselineMethods.Find(baseline.MethodFullNameHash, baseline.BaselineId);
        if (candidate != null)
        {
          candidate.Method = baseline.Method;
        }
        else
        {
          base.BaselineMethods.Add(baseline);
        }
      }
      catch (Exception e)
      {
        if (this.trace)
        {
          Console.WriteLine("[cache] SqlCacheModel: AddOrUpdate baseline binding failed: {0}", e.Message);
        }
      }
      
    }


    public string CacheName
    {
      get { return this.cachename; }
    }
  }

  public class SqlCacheModelNoCreate : SQLCacheModel
  {
    static SqlCacheModelNoCreate()
    {
      Database.SetInitializer<SqlCacheModelNoCreate>(null);
    }

    public SqlCacheModelNoCreate(string connection, string cacheName, bool trace) : base(connection, cacheName, trace) {
      Contract.Requires(cacheName != null);    
    }
  }

  public class SqlCacheModelUseExisting : SQLCacheModel
  {
    class Policy : CreateDatabaseIfNotExists<SqlCacheModelUseExisting>
    {
      protected override void Seed(SqlCacheModelUseExisting context)
      {
        context.Initialize();
        base.Seed(context);
      }
    }

    static SqlCacheModelUseExisting() {
      Database.SetInitializer<SqlCacheModelUseExisting>(new Policy());
    }

    public SqlCacheModelUseExisting(string connection, string cachename, bool trace)
      : base(connection, cachename, trace)
    {
      Contract.Requires(cachename != null);
    }
  }

  public class SqlCacheModelClearExisting : SQLCacheModel
  {
    class Policy : DropCreateDatabaseAlways<SqlCacheModelClearExisting>
    {
      protected override void Seed(SqlCacheModelClearExisting context)
      {
        context.Initialize();
        base.Seed(context);
      }
    }

    static SqlCacheModelClearExisting()
    {
      Database.SetInitializer<SqlCacheModelClearExisting>(new Policy());
    }

    public SqlCacheModelClearExisting(string connection, string cachename, bool trace)
      : base(connection, cachename, trace)
    {
      Contract.Requires(cachename != null);
    }
  }

  public class SqlCacheModelDropOnModelChange : SQLCacheModel
  {
    class Policy : DropCreateDatabaseIfModelChanges<SqlCacheModelDropOnModelChange>
    {
      protected override void Seed(SqlCacheModelDropOnModelChange context)
      {
        context.Initialize();
        base.Seed(context);
      }
    }

    static SqlCacheModelDropOnModelChange()
    {
      Database.SetInitializer<SqlCacheModelDropOnModelChange>(new Policy());
    }

    public SqlCacheModelDropOnModelChange(string connection, string cachename, bool trace)
      : base(connection, cachename, trace)
    {
      Contract.Requires(cachename != null);
    }
  }

  public class SQLClousotCacheFactory : IClousotCacheFactory
  {
    readonly protected string DbName;
    readonly protected bool deleteOnModelChange;

    public SQLClousotCacheFactory(string DbName, bool deleteOnModelChange = false)
    {
      this.deleteOnModelChange = deleteOnModelChange;
      this.DbName = DbName;
    }

    public virtual IClousotCache Create(IClousotCacheOptions options)
    {
      if (String.IsNullOrWhiteSpace(DbName) || options == null) { return null; }
      var connection = BuildConnectionString(options).ToString();

      Contract.Assert(DbName != null, "Helping cccheck");

      SQLCacheModel model;
      if (options.ClearCache)
      {
        model = new SqlCacheModelClearExisting(connection, DbName, options.Trace);
      }
      else if (options.SaveToCache)
      {
        if (this.deleteOnModelChange)
        {
          model = new SqlCacheModelDropOnModelChange(connection, DbName, options.Trace);
        }
        else
        {
          model = new SqlCacheModelUseExisting(connection, DbName, options.Trace);
        }
      }
      else
      {
        model = new SqlCacheModelNoCreate(connection, DbName, options.Trace);
      }
      return new ClousotCache(model, options);
    }

    [Pure]
    protected virtual SqlConnectionStringBuilder BuildConnectionString(IClousotCacheOptions options)
    {
      Contract.Requires(options != null);
      Contract.Ensures(Contract.Result<SqlConnectionStringBuilder>() != null);

      return new SqlConnectionStringBuilder
      {
        IntegratedSecurity = true,
        InitialCatalog = options.GetCacheDBName(),
        DataSource = this.DbName,
        UserInstance = false,
        MultipleActiveResultSets = true,
        ConnectTimeout = options.CacheServerTimeout,
      };
    }
  }

  public class LocalDbClousotCacheFactory : SQLClousotCacheFactory
  {
    public LocalDbClousotCacheFactory(string dbName)
      : base(dbName, true)
    {}

    protected override SqlConnectionStringBuilder BuildConnectionString(IClousotCacheOptions options)
    {
      if (!string.IsNullOrWhiteSpace(options.CacheDirectory))
      {
        var name = options.GetCacheDBName();
        return new SqlConnectionStringBuilder
        {
          IntegratedSecurity = true,
          InitialCatalog = name,
          DataSource = this.DbName,
          UserInstance = false,
          MultipleActiveResultSets = true,
          ConnectTimeout = options.CacheServerTimeout,
          AttachDBFilename = Path.Combine(options.CacheDirectory.AssumeNotNull(), name + ".mdf")
        };
      }

      return base.BuildConnectionString(options);
    }
  }
}
