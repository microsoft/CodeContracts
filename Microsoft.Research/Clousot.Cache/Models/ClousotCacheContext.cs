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

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.Research.CodeAnalysis.Caching.Models.Mapping;
using System.Data.SqlClient;

namespace Microsoft.Research.CodeAnalysis.Caching.Models
{
  public class ClousotCacheContext : DbContext
  {
    static ClousotCacheContext()
    {
      Database.SetInitializer<ClousotCacheContext>(new StartPolicy());
    }

    class StartPolicy : DropCreateDatabaseIfModelChanges<ClousotCacheContext>
    {
      protected override void Seed(ClousotCacheContext context)
      {
        base.Seed(context);
      }
    }

    public ClousotCacheContext(string connection)
      : base(connection)
    {
    }

    public DbSet<AssemblyInfo> AssemblyInfoes { get; set; }
    public DbSet<IdHashTimeToMethod> IdHashTimeToMethods { get; set; }
    public DbSet<Metadata> Metadatas { get; set; }
    public DbSet<Method> Methods { get; set; }
    public DbSet<OutcomeContextEdge> OutcomeContextEdges { get; set; }
    public DbSet<OutcomeContext> OutcomeContexts { get; set; }
    public DbSet<Outcome> Outcomes { get; set; }
    public DbSet<SuggestionContextEdge> SuggestionContextEdges { get; set; }
    public DbSet<Suggestion> Suggestions { get; set; }
    public DbSet<VersionResult> VersionResults { get; set; }
    public DbSet<BaselineMethod> BaselineMethods { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Configurations.Add(new AssemblyInfoMap());
      modelBuilder.Configurations.Add(new IdHashTimeToMethodMap());
      modelBuilder.Configurations.Add(new MetadataMap());
      modelBuilder.Configurations.Add(new MethodMap());
      modelBuilder.Configurations.Add(new OutcomeContextEdgeMap());
      modelBuilder.Configurations.Add(new OutcomeContextMap());
      modelBuilder.Configurations.Add(new OutcomeMap());
      modelBuilder.Configurations.Add(new SuggestionContextEdgeMap());
      modelBuilder.Configurations.Add(new SuggestionMap());
      modelBuilder.Configurations.Add(new VersionResultMap());
      modelBuilder.Configurations.Add(new BaselineMethodMap());
    }
  }
}
