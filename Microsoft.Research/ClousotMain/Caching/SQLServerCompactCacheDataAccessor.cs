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
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Reflection;

namespace Microsoft.Research.CodeAnalysis
{
  class SQLServerCompactCacheDataAccessor : SQLCacheDataAccessor
  {
    private string FileName;

    private string ConnectionString
    {
      get
      {
        return "data source='" + this.FileName + "'; flush interval=1; Max Database Size=2048;"; // max size 2GB
      }
    }

    protected override string TablesCreationQueries
    {
      get
      {
        using (var textReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Microsoft.Research.CodeAnalysis.Caching.CacheCreation.sqlce")))
          return textReader.ReadToEnd();
      }
    }

    protected override bool DatabaseExist(out DbConnection dbConnection)
    {
      if (File.Exists(this.FileName))
      {
        /*try // TODO : when should we compact the DB ?
        {
          using (var sqlCeEngine = new SqlCeEngine(ConnectionString))
            sqlCeEngine.Compact(null);
        }
        catch (Exception e)
        {
          Console.WriteLine("Warning : unable to compact the cache file : " + e.ToString());
        }*/
        dbConnection = new SqlCeConnection(this.ConnectionString);
        return true;
      }
      dbConnection = null;
      return false;
    }

    protected override DbConnection CreateDatabase()
    {
      using (var sqlCeEngine = new SqlCeEngine(this.ConnectionString))
      {
        sqlCeEngine.CreateDatabase();
      }
      return new SqlCeConnection(this.ConnectionString);
    }
    
    protected override void DeleteDatabase()
    {
      this.CloseConnection();

      if (File.Exists(this.FileName))
        File.Delete(this.FileName);
    }

    // This is because SQLCE does not support auto generated Id when it is used with the entity framework (?!)
    // TODO: it seems that it has been fixed in SQLCE 4.0: http://connect.microsoft.com/VisualStudio/feedback/details/475454/entity-framework-sql-server-ce-and-identity-columns
    private long nextReservedId = 0;
    private long nextFreeId = 0;
    public override long GetNewId()
    {
      if (nextFreeId == nextReservedId)
      {
        var entry = this.GetMetadataOrNull("NextFreeId");
        this.nextReservedId = BitConverter.ToInt64(entry.Value, 0);
        this.nextFreeId = this.nextReservedId + 100;
        entry.Value = BitConverter.GetBytes(this.nextFreeId);
        this.TrySaveChanges();
      }

      return nextReservedId++;
    }

    static private Dictionary<string, byte[]> addExtraMetadataIfCreation(Dictionary<string, byte[]> metadataIfCreation)
    {
      metadataIfCreation.Add("NextFreeId", BitConverter.GetBytes((long)0));
      return metadataIfCreation;
    }

    public SQLServerCompactCacheDataAccessor(string directoryName, string dbName, Dictionary<string, byte[]> metadataIfCreation, int maxCacheSize, CacheVersionParameters cacheVersionParameters)
      : base(addExtraMetadataIfCreation(metadataIfCreation), maxCacheSize, cacheVersionParameters, new string[]{
        "res://*/Caching.ClousotCacheModel.csdl",
        "res://*/Caching.ClousotCacheModel.ssdl",
        "res://*/Caching.ClousotCacheModel.msl"
      })
    {
        var filename = dbName;
        if (!filename.EndsWith(".sdf")) {
            filename += ".sdf";
        }
        if (directoryName != null)
        {
            filename = Path.Combine(directoryName, filename);
        }
        this.FileName = filename;
    }

  }
}
