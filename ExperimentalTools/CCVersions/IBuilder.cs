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
using System.Diagnostics.Contracts;

namespace CCVersions
{
  /// <summary>
  /// Abstraction over the builder.
  /// At the moment, the only implementation is MSBuild
  /// </summary>
  [ContractClass(typeof(IBuilderContract))]
  interface IBuilder
  {
    /// <summary>
    /// Where are the sources?
    /// </summary>
    string SourceDirectory { get; }

    /// <summary>
    /// Do the the work
    /// </summary>
    /// <param name="ProjectsPath">can be .sln, .csproj, etc. Everything the builder understands</param>
    /// <param name="VersionId">version to be forwarded to cccheck so it knows which version has been compiled</param>
    /// <param name="OutputDirectory">Output</param>
    /// <returns></returns>
    bool Build(IEnumerable<string> ProjectsPath, long VersionId, string OutputDirectory);
  }

  [ContractClassFor(typeof(IBuilder))]
  internal abstract class IBuilderContract : IBuilder
  {
    [Pure]
    public string SourceDirectory { get { return default(string); } }

    public bool Build(IEnumerable<string> ProjectsPath, long VersionId, string OutputDirectory)
    {
      Contract.Requires(this.SourceDirectory != null);
      Contract.Requires(ProjectsPath != null);
      Contract.Requires(Contract.ForAll<string>(ProjectsPath, p => p != null));
      Contract.Requires(OutputDirectory != null);
      return default(bool);
    }
  }

  static class BuilderFactories
  {
    public delegate IBuilder BuilderFactory(string SourceDirectory);

    private static Dictionary<string, BuilderFactory> dic = new Dictionary<string, BuilderFactory>();

    public static readonly BuilderFactory Default;

    public static BuilderFactory Find(string id)
    {
      BuilderFactory res;
      dic.TryGetValue(id, out res);
      return res;
    }

    static BuilderFactories()
    {
      Default = MSBuild.Factory;

      dic.Add("MSBuild", MSBuild.Factory);
    }
  }
}
