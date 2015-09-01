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
using System.Windows.Forms;

namespace CCVersions
{
  #region HGVersionSpec

  class HGVersionSpec : ExeVersionSpec
  {
    public HGVersionSpec(string value)
      : base(value)
    { }

    public HGVersionSpec(int revNum)
      : base(revNum)
    { }
  }

  #endregion HGVersionSpec

  #region HGRepository
  class HGRepository : ExeRepository
  {
    protected override string ExeName { get { return "hg.exe"; } }

    public HGRepository(string workingDirectory)
      : base(workingDirectory)
    { }

    public bool Clone(string source, string branch = null, int? timeoutMilliseconds = null)
    {
      var arguments = "clone";
      if (branch != null)
      {
        arguments += " -b \"" + branch + "\"";
        Contract.Assert(!String.IsNullOrWhiteSpace(arguments)); 
      }
      var timeout = timeoutMilliseconds ?? longTimeout;
      var res = this.Invoke(arguments, timeout);
      return res.ExitCode == 0;
    }

    public HGVersionSpec Identify(string revision = null)
    {
      Contract.Ensures(Contract.Result<HGVersionSpec>() != null);

      var arguments = "identify -n";
      if (revision != null)
      {
        arguments += " -r \"" + revision + "\"";
        Contract.Assert(!String.IsNullOrWhiteSpace(arguments));
      }
      var res = this.Invoke(arguments, shortTimeout);
      if (res.ExitCode != 0)
      {
        throw new ExeInvokeException(String.Format("ExitCode = {0}", res.ExitCode));
      }
      return new HGVersionSpec(res.StdOut);
    }

    public IEnumerable<HGVersionSpec> Log(HGVersionSpec first, HGVersionSpec last)
    {
      Contract.Requires(first != null);
      Contract.Requires(last != null);
      var arguments = "log --template \"{rev}\\n\"";
      arguments += " -r " + first.Id + ":" + last.Id;
      Contract.Assume(!String.IsNullOrWhiteSpace(arguments));
      var res = this.Invoke(arguments, mediumTimeout);
      if (res.ExitCode != 0)
      {
        return null;
      }
      return res.StdOut.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(s => new HGVersionSpec(s));
    }

    public bool Pull(int? timeoutMilliseconds = null)
    {
      var arguments = "pull";
      var timeout = timeoutMilliseconds ?? longTimeout;
      var res = this.Invoke(arguments, timeout);
      return res.ExitCode == 0;
    }

    public bool Update(HGVersionSpec version)
    {
      Contract.Requires(version != null);

      var arguments = "update -C";
      arguments += " -r " + version.Id;
      Contract.Assume(!String.IsNullOrWhiteSpace(arguments));
      var res = this.Invoke(arguments, longTimeout);
      return res.ExitCode == 0;
    }

    protected override IEnumerable<Tuple<string, string>> EnvironmentVariables
    {
      get { yield break; }
    }
  }
  #endregion

  class HGSourceManager : ISourceManager
  {
    readonly string branch;
    readonly string id;
    readonly string uri;
    string workingDirectory;
    HGRepository repository;
    bool needClone = false;
    bool needPull = false;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.branch != null);
      Contract.Invariant(this.id != null);
      Contract.Invariant(this.uri != null);
      Contract.Invariant(this.repository != null || this.workingDirectory == null); // WorkindDirectory != null implies repository != null
    }

    public static HGSourceManager Factory(XElement xConfig)
    {
      Contract.Requires(xConfig != null);

      return new HGSourceManager(xConfig);
    }

    private HGSourceManager(XElement xConfig)
    {
      Contract.Requires(xConfig != null);

      var xProtocol = xConfig.Element("protocol");
      var protocol = xProtocol == null ? "http" : xProtocol.Value;
      var xServer = xConfig.Element("server");
      var server = xServer == null ? "localhost" : xServer.Value;
      var xPort = xConfig.Element("port");
      var port = xPort == null ? null : xPort.Value;
      var xPath = xConfig.Element("path");
      var path = xPath == null ? "" : xPath.Value;

      var xBranch = xConfig.Element("branch");
      this.branch = xBranch == null ? "default" : xBranch.Value;

      this.uri = String.Format("{0}://{1}{2}/{3}", protocol, server, port == null ? "" : ":" + port, path);

      this.id = IDBuilder.FromUri("HG_", this.uri);
    }

    public string Id { get { return this.id; } }

    private bool CheckRepository()
    {
      if (this.needClone)
      {
        var res = MessageBox.Show("Will now clone the whole repository. This operation can take a long time. Continue?", "CCVersions", MessageBoxButtons.YesNo);
        if (res == DialogResult.Yes)
        {
          this.repository.Clone(this.uri);
          this.needClone = false;
          this.needPull = false;
        }
      }
      if (this.needPull)
      {
        try
        {
          var res = MessageBox.Show("Check for new versions?", "CCVersions", MessageBoxButtons.YesNo);
          if (res == DialogResult.Yes)
          {
            this.repository.Pull();
          }
          this.needPull = false;
        }
        catch { }
      }
      return !this.needClone && !this.needPull;
    }

    public string WorkingDirectory
    {
      get { return this.workingDirectory; }
      set
      {
        Contract.Ensures(this.WorkingDirectory != null);

        this.workingDirectory = value;
        this.repository = new HGRepository(this.WorkingDirectory);
        try
        {
          this.repository.Identify();
          this.needPull = true;
        }
        catch (ExeRepository.ExeInvokeException)
        {
          this.needClone = true;
          this.CheckRepository();
        }
      }
    }

    public ISourceManagerVersionSpec ParseVersionSpec(string value)
    {
      if (this.repository != null && !String.IsNullOrWhiteSpace(value))
      {
        return this.repository.Identify(value);
      }
      else
      {
        return new HGVersionSpec(value);
      }
    }

    [Pure]
    public IEnumerable<HGVersionSpec> VersionRange(HGVersionSpec First, HGVersionSpec Last)
    {
      Contract.Requires(First != null);
      Contract.Requires(Last != null);
      Contract.Ensures(Contract.Result<IEnumerable<HGVersionSpec>>() != null);

      if (!this.CheckRepository())
      {
        return new HGVersionSpec[0];
      }

      var first = First.IsNull ? this.repository.Identify("null") : First;
      var last = Last.IsNull ? this.repository.Identify("tip") : Last;

      var log = repository.Log(first, last);

      return log ?? new HGVersionSpec[0];
    }
    public IEnumerable<ISourceManagerVersionSpec> VersionRange(ISourceManagerVersionSpec First, ISourceManagerVersionSpec Last, string filter = null)
    {
      Contract.Ensures(Contract.Result<IEnumerable<ISourceManagerVersionSpec>>() != null);

      return VersionRange((HGVersionSpec)First, (HGVersionSpec)Last);
    }

    public bool GetSources(HGVersionSpec Version)
    {
      Contract.Requires(Version != null);
      Contract.Requires(this.WorkingDirectory != null);

      if (!this.CheckRepository())
      {
        return false;
      }

      repository.Update(Version);

      return true;
    }
    public bool GetSources(ISourceManagerVersionSpec Version)
    {
      return GetSources((HGVersionSpec)Version);
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
}
