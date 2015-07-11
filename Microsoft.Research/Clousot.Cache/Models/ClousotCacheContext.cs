// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

        private class StartPolicy : DropCreateDatabaseIfModelChanges<ClousotCacheContext>
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
