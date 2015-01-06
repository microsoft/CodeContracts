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

using System.Collections.Generic;
using System.Data.Objects;

namespace Microsoft.Research.CodeAnalysis
{
  // TODO
#if false
  public class MyClousotCacheEntities : ObjectContext, IClousotCacheEntities
  {
    public ObjectSet<MethodModel> MethodModels { get; private set; }
    public ObjectSet<AssemblyBinding> AssemblyBindings { get; private set;  }
    public ObjectSet<CachingMetadata> CachingMetadatas { get; private set;  }
    public ObjectSet<AssemblyInfo> AssemblyInfoes { get; private set; }

    public MyClousotCacheEntities()
      : base("name=MyClousotCacheEntities", "MyClousotCacheEntities")
    {
      this.MethodModels = this.CreateObjectSet<MethodModel>();
      this.AssemblyBindings = this.CreateObjectSet<AssemblyBinding>();
      this.CachingMetadatas = this.CreateObjectSet<CachingMetadata>();
      this.AssemblyInfoes = this.CreateObjectSet<AssemblyInfo>();
    }
  }

  public class MemoryCacheDataAccessor : EntityCacheDataAccessor<ClousotCacheEntities>
  {
    private readonly Dictionary<string, byte[]> metadataIfCreation;

    public MemoryCacheDataAccessor(Dictionary<string, byte[]> metadataIfCreation, int maxCacheSize, CacheVersionParameters cacheVersionParameters)
      : base(maxCacheSize, cacheVersionParameters)
    {
      this.metadataIfCreation = metadataIfCreation;
    }

    public override bool IsValid { get { return true; } }

    protected override ClousotCacheEntities CreateClousotCacheEntities(bool silent)
    {
      var context = new ClousotCacheEntities();

      context.MetadataWorkspace.LoadFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

      return context;
    }

    public override void Clear()
    {
      this.Close();
    }
  }
#endif
}
