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
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace CCVersions
{
  class TFVersionSpec : ISourceManagerVersionSpec
  {
    readonly VersionSpec vs;

    public TFVersionSpec(string value)
    {
      if (String.IsNullOrWhiteSpace(value))
      {
        this.vs = null;
      }
      else
      {
        this.vs = VersionSpec.ParseSingleSpec(value, null);
      }
    }
    public TFVersionSpec(int changeset)
    {
      this.vs = new ChangesetVersionSpec(changeset);
    }

    public override string ToString()
    {
      if (this.vs == null)
        return "NULL";
      var res = this.vs.DisplayString;
      Contract.Assume(res != null);
      Contract.Assume(res.Length >= 1);
      return res;
    }

    public long Id
    {
      get
      {
        if (this.vs == null)
          return -1;
        if (this.vs is ChangesetVersionSpec)
          return (this.vs as ChangesetVersionSpec).ChangesetId;
        long res;
        if (Int64.TryParse(this.ToString().Substring(1), out res)) // assume ToString gives C1234
          return res;
        return -2;
      }
    }

    public VersionSpec VersionSpec { get { return vs; } }
  }

  class TFSourceManager : ISourceManager
  {
    readonly string id;
    readonly string uri;
    string workingDirectory;
    readonly string subPath;
    readonly TfsTeamProjectCollection tpc;
    readonly VersionControlServer vcServer;
    Workspace workspace;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.id != null);
      Contract.Invariant(this.subPath != null);
      Contract.Invariant(this.uri != null);
      Contract.Invariant(this.vcServer != null);
      Contract.Invariant(this.workspace != null || this.workingDirectory == null); // WorkingDirectory != null implies workspace != null
    }

    public static TFSourceManager Factory(XElement xConfig)
    {
      Contract.Requires(xConfig != null);

      return new TFSourceManager(xConfig);
    }

    private TFSourceManager(XElement xConfig)
    {
      Contract.Requires(xConfig != null);


      // Read the config.xml file
      var xProtocol = xConfig.Element("protocol");
      var protocol = xProtocol == null ? "http" : xProtocol.Value;
      var xServer = xConfig.Element("server");
      var server = xServer == null ? "localhost" : xServer.Value;
      var xPort = xConfig.Element("port");
      var port = xPort == null ? "8080" : xPort.Value;
      var xPath = xConfig.Element("path");
      var path = xPath == null ? "tfs" : xPath.Value;
      var xSubPath = xConfig.Element("subpath");
      this.subPath = xSubPath == null ? "" : xSubPath.Value;

      // build the URL
      this.uri = String.Format("{0}://{1}:{2}/{3}", protocol, server, port, path);

      // Build the TFS objects
      // The TFS needs a window were to show some dialogs, in the case of
      this.tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(this.uri), new UICredentialsProvider(Program.TheMainForm));
      this.vcServer = tpc.GetService<VersionControlServer>();
      Contract.Assume(this.vcServer != null);

      // build the ID
      this.id = IDBuilder.FromUri("TF_", this.uri + '/' + this.subPath);
    }

    public string Id { get { return this.id; } }

    public string WorkingDirectory
    {
      get { return this.workingDirectory; }
      set {
        Contract.Ensures(this.WorkingDirectory != null);

        this.workingDirectory = value;
        this.workspace = this.vcServer.TryGetWorkspace(this.WorkingDirectory);
        if (this.workspace == null)
        {
          var name = String.Format("{0}-{1}", System.Environment.MachineName, System.Guid.NewGuid().ToString().Substring(0, 8));
          this.workspace = this.vcServer.CreateWorkspace(name);
          Contract.Assume(this.workspace != null);
          this.workspace.Map("$/" + this.subPath, this.WorkingDirectory);
        }
      }
    }

    public ISourceManagerVersionSpec ParseVersionSpec(string value)
    {
      return new TFVersionSpec(value);
    }

    [Pure]
    public IEnumerable<TFVersionSpec> VersionRange(TFVersionSpec First, TFVersionSpec Last)
    {
      Contract.Requires(First != null);
      Contract.Requires(Last != null);
      Contract.Ensures(Contract.Result<IEnumerable<TFVersionSpec>>() != null);

      var history = this.vcServer.QueryHistory("$/" + this.subPath, VersionSpec.Latest, 0, RecursionType.Full, null, First.VersionSpec, Last.VersionSpec, Int32.MaxValue, false, false);
      Contract.Assume(history != null);
      return history.Cast<Changeset>().Select(changeSet => new TFVersionSpec(changeSet.ChangesetId)).Reverse();
    }

    public IEnumerable<ISourceManagerVersionSpec> VersionRange(ISourceManagerVersionSpec First, ISourceManagerVersionSpec Last, string filter = null)
    {
      Contract.Ensures(Contract.Result<IEnumerable<ISourceManagerVersionSpec>>() != null);

      return VersionRange((TFVersionSpec)First, (TFVersionSpec)Last);
    }

    public IEnumerable<ISourceManagerVersionSpec> VersionRange(ISourceManagerVersionSpec First, int countAfter)
    {
      Contract.Ensures(Contract.Result<IEnumerable<ISourceManagerVersionSpec>>() != null);

      return VersionRange((TFVersionSpec)First, new TFVersionSpec(null));
    }

    public IEnumerable<ISourceManagerVersionSpec> VersionRange(int countPrio, ISourceManagerVersionSpec Last)
    {
      Contract.Ensures(Contract.Result<IEnumerable<ISourceManagerVersionSpec>>() != null);

      return VersionRange(new TFVersionSpec(null), (TFVersionSpec)Last);
    }


    public bool GetSources(TFVersionSpec Version)
    {
      Contract.Requires(Version != null);
      Contract.Requires(this.WorkingDirectory != null);

      var getStatus = this.workspace.Get(Version.VersionSpec, GetOptions.GetAll | GetOptions.Overwrite);
      Contract.Assume(getStatus != null);
      return getStatus.NumFailures == 0;
    }
    public bool GetSources(ISourceManagerVersionSpec Version)
    {
      return GetSources((TFVersionSpec)Version);
    }
  }
}
