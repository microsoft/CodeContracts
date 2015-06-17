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
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microsoft.Research.CodeAnalysis
{
  class SQLServerCacheDataAccessor : SQLCacheDataAccessor
  {
    protected override string TablesCreationQueries
    {
      get
      {
        using (var textReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Microsoft.Research.CodeAnalysis.Caching.CacheCreation.sql")))
          return textReader.ReadToEnd();
      }
    }

    private readonly string sqlServer; // for example @".\SQLEXPRESS"

    private readonly string VersionString = typeof(GeneralOptions).Assembly.GetName().Version.ToString();

    private string dbName;

#if false
    protected override string FileName
    {
      get { return base.FileName; }
      set
      {
        base.FileName = Path.ChangeExtension(value, "mdf");
        this.LogFileName = Path.ChangeExtension(this.FileName, null) + "_log.LDF";

        var filename = Path.GetFileNameWithoutExtension(value);
        var shortFilename = filename.Substring(0, Math.Min(40, filename.Length));
        using (var hw = new HashWriter())
        {
          hw.Write(value);
          this.fileAlias = hw.GetHash().Select(b => b.ToString("X2")).Aggregate(shortFilename, String.Concat);
          this.dbName = this.fileAlias + VersionString; // +Guid.NewGuid().ToString().Replace('-', '_');
        }
      }
    }
#endif

    private string MakeConnectionString(string dbName)
    {
      return new SqlConnectionStringBuilder
      {
        IntegratedSecurity = true,
        InitialCatalog = dbName,
        DataSource = this.sqlServer,
        UserInstance = false,
        //ConnectTimeout = 5,
      }.ToString();
    }

    protected override bool DatabaseExist(out DbConnection dbConnection)
    {
      var cstring = this.MakeConnectionString("master");
      var sqlConnection = new SqlConnection(cstring);
      sqlConnection.Open();

      try
      {
          sqlConnection.ChangeDatabase(this.dbName);
          dbConnection = sqlConnection;
          return true;
      }
      catch (SqlException)
      {
      }
      dbConnection = null;
      return false;
    }

    protected override DbConnection CreateDatabase()
    {
      var sqlConnection = new SqlConnection(this.MakeConnectionString("master"));
      sqlConnection.Open();

      // create it
      SqlCommand cmd = sqlConnection.CreateCommand();

      //cmd.CommandText = String.Format("CREATE DATABASE \"{0}\" ON (Name=\"{1}\", filename='{2}')", this.dbName, this.fileAlias, this.FileName); // SQL Injection?
      cmd.CommandText = String.Format("CREATE DATABASE \"{0}\"", this.dbName); // SQL Injection?
      cmd.ExecuteNonQuery();

      sqlConnection.ChangeDatabase(this.dbName);

      return sqlConnection;
    }

    protected override void CloseDatabase()
    {
      if (this.connection == null)
        return;
      try
      {
        this.connection.Close();
      }
      catch (SqlException)
      {
      }
      this.connection = null;
    }

    protected override void DeleteDatabase()
    {
        // don't delete anything on the server
        if (this.connection != null)
        {
            this.CloseConnection();
        }
#if false
        try
        {
          this.connection.StoreConnection.ChangeDatabase("master");
          DbCommand cmd = this.connection.StoreConnection.CreateCommand();
          cmd.CommandText = String.Format("DROP DATABASE \"{0}\"", this.dbName);
          cmd.ExecuteNonQuery();
        }
        catch
        {
          try
          {
            this.CloseDatabase();
          }
          catch { }
        }
        this.CloseConnection();
      }
      if (File.Exists(this.FileName))
        File.Delete(this.FileName);
      if (File.Exists(this.LogFileName))
        File.Delete(this.LogFileName);
#endif
    }

    public SQLServerCacheDataAccessor(string sqlServer, string dbName, Dictionary<string, byte[]> metadataIfCreation, int maxCacheSize, CacheVersionParameters cacheVersionParameters)
      : base(metadataIfCreation, maxCacheSize, cacheVersionParameters, new string[]{
        "res://*/Caching.ClousotCacheModelForSQLServer.csdl",
        "res://*/Caching.ClousotCacheModelForSQLServer.ssdl",
        "res://*/Caching.ClousotCacheModelForSQLServer.msl"
      })
    {
      this.sqlServer = sqlServer;
      this.dbName = dbName;
    }

  }
}
