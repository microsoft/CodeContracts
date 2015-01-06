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
using System.Linq;
using System.Xml.Linq;

namespace CCVersions
{
  #region ISourceManagerVersionSpec

  [ContractClass(typeof(ISourceManagerVersionSpecContract))]
  interface ISourceManagerVersionSpec
  {
    string ToString();

    long Id { get; }
  }

  #endregion

  #region Contracts for ISourceMAnagerVersionSpec

  [ContractClassFor(typeof(ISourceManagerVersionSpec))]
  internal abstract class ISourceManagerVersionSpecContract : ISourceManagerVersionSpec
  {
    [Pure]
    string ISourceManagerVersionSpec.ToString()
    {
      Contract.Ensures(Contract.Result<string>().Length >= 1);

      return default(string);
    }
    long ISourceManagerVersionSpec.Id
    {
      [Pure]
      get
      {
        return default(long);
      }
    }
  }

  #endregion

  #region ISourceManager

  /// <summary>
  /// Abstraction over the repository
  /// </summary>
  [ContractClass(typeof(ISourceManagerContract))]
  interface ISourceManager
  {
    string Id { get; }

    /// <summary>
    /// Drop location.
    /// At the moment, it is hardcoded to be in D:\ somewhere
    /// </summary>
    string WorkingDirectory { get; set; }

    /// <summary>
    /// Maps string to version.
    /// </summary>
    ISourceManagerVersionSpec ParseVersionSpec(string value);

    /// <summary>
    /// When First (Last) is null, the meaning is that it is the First (Last) version in the depot
    /// </summary>
    IEnumerable<ISourceManagerVersionSpec> VersionRange(ISourceManagerVersionSpec First, ISourceManagerVersionSpec Last, string filter = null);

    /// <summary>
    /// When First (Last) is null, the meaning is that it is the First (Last) version in the depot
    /// </summary>
    IEnumerable<ISourceManagerVersionSpec> VersionRange(int countPrior, ISourceManagerVersionSpec Last);

    /// <summary>
    /// When First (Last) is null, the meaning is that it is the First (Last) version in the depot
    /// </summary>
    IEnumerable<ISourceManagerVersionSpec> VersionRange(ISourceManagerVersionSpec first, int countAfter);


    /// <summary>
    /// Do the work
    /// </summary>
    bool GetSources(ISourceManagerVersionSpec Version);
  }

  #endregion

  #region Contracts for ISourceManager

  [ContractClassFor(typeof(ISourceManager))]
  internal abstract class ISourceManagerContract : ISourceManager
  {
    [Pure]
    string ISourceManager.Id
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    public string WorkingDirectory
    {
      [Pure] get { return default(string); }
      set {
        Contract.Requires(value != null);
      }
    }
    [Pure]
    ISourceManagerVersionSpec ISourceManager.ParseVersionSpec(string value)
    {
      Contract.Ensures(Contract.Result<ISourceManagerVersionSpec>() != null);
      return default(ISourceManagerVersionSpec);
    }
    [Pure]
    IEnumerable<ISourceManagerVersionSpec> ISourceManager.VersionRange(ISourceManagerVersionSpec First, ISourceManagerVersionSpec Last, string filter)
    {
      Contract.Requires(First != null);
      Contract.Requires(Last != null);
      Contract.Ensures(Contract.Result<IEnumerable<ISourceManagerVersionSpec>>() != null);
      return default(IEnumerable<ISourceManagerVersionSpec>);
    }
    bool ISourceManager.GetSources(ISourceManagerVersionSpec Version)
    {
      Contract.Requires(this.WorkingDirectory != null);
      Contract.Requires(Version != null);
      return default(bool);
    }


    public IEnumerable<ISourceManagerVersionSpec> VersionRange(int countPrior, ISourceManagerVersionSpec Last)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<ISourceManagerVersionSpec> VersionRange(ISourceManagerVersionSpec first, int countAfter)
    {
      throw new NotImplementedException();
    }
  }

  #endregion
  
  #region DummyVersionSpec

  class DummyVersionSpec : ISourceManagerVersionSpec
  {
    public static ISourceManagerVersionSpec Instance = new DummyVersionSpec();

    public override string ToString() { return " "; }

    public long Id { get { return 0; } }
  }

  #endregion

  #region IDBuilder

  /// <summary>
  /// Most of the depots work with URLs. Sanitize the name.
  /// Essentially transforms a string into a valid directory name
  /// </summary>
  static class IDBuilder
  {
    static readonly char[] InvalidChars = System.IO.Path.GetInvalidFileNameChars();

    public static string FromUri(string prefix, string uri)
    {
      Contract.Requires(uri != null);

      return prefix + new String(uri.Select(c => InvalidChars.Any(ic => ic == c) ? '_' : c).ToArray());
    }
  }

  #endregion

  #region SourceManagerFactories

  /// <summary>
  /// The factory for the known depositories
  /// </summary>
  static class SourceManagerFactories
  {
    public delegate ISourceManager SourceManagerFactory(XElement XConfig);

    private static Dictionary<string, SourceManagerFactory> dic = new Dictionary<string, SourceManagerFactory>();

    public static SourceManagerFactory Find(string id)
    {
      SourceManagerFactory res;
      dic.TryGetValue(id, out res);
      return res;
    }

    /// <summary>
    /// New repository should be added here
    /// </summary>
    static SourceManagerFactories()
    {
      dic.Add("TFS", TFSourceManager.Factory);  // TFS
      dic.Add("SD", SDSourceManager.Factory);   // Source depot - not tested 
      dic.Add("HG", HGSourceManager.Factory);   // Mercurial
      dic.Add("OO", OOSourceManager.Factory);   // OutputOnly - It only reads a cache file
    }
  }
  #endregion
}
