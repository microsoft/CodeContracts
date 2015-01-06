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
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CCVersions
{
  #region SDVersionSpec

  class SDVersionSpec : ExeVersionSpec
  {
    public SDVersionSpec(string value)
      : base(value)
    { }

    public SDVersionSpec(int revNum)
      : base(revNum)
    { }
  }

  #endregion SDVersionSpec

  #region SDRepository
  class SDRepository : ExeRepository
  {
    readonly private string sdport;
    readonly private string sdclient;
    protected override string FallbackLocateExe()
    {
      const string mehdi = @"d:\rdm3\rd_cse\tools\amd64\sd.exe";
      if (File.Exists(mehdi))
        return mehdi;
      return null;
    }

    protected override string ExeName { get { return "sd.exe"; } }

    public SDRepository(string workingDirectory, string sdport, string sdclient)
      : base(workingDirectory)
    {
      this.sdport = sdport;
      this.sdclient = sdclient;
    }

    public bool Exists()
    {
 	    return this.Invoke("", shortTimeout).ExitCode == 0;
    }

    public bool Init()
    {
      var arguments = "init";
      var res = this.Invoke(arguments, shortTimeout);
      if (res.ExitCode != 0)
        throw new ExeInvokeException(String.Format("ExitCode = {0}", res.ExitCode));
      return true;
    }

    private void SetClientRoot()
    {
      var arguments = String.Format("client -o");
      var res = this.Invoke(arguments, mediumTimeout);
      var clientSpec = Regex.Replace(res.StdOut, @"^Root:.+$", "Root: " + this.CurrentInstanceDirectory, RegexOptions.Multiline);
      var res2 = this.Invoke("client -i", stdinput: clientSpec);
    }

    readonly char[] newline = new[] { '\n' };
    private string currentVersion;
    public IEnumerable<SDVersionSpec> Changes(long skip, long count, string filter)
    {
      SetClientRoot();
      var arguments = String.Format("changes -r -m {0},{1} {2}", skip, count, filter);
      var res = this.Invoke(arguments, mediumTimeout);
      foreach (var line in res.StdOut.Split(newline))
      {
        var match = Regex.Match(line, @"Change\s+(\d+)");
        if (match.Success)
        {
          yield return new SDVersionSpec(match.Groups[1].Value);
        }
      }
    }

    public IEnumerable<SDVersionSpec> Changes(long count)
    {
      return Changes(0, count, null);
    }

    public void Sync(SDVersionSpec Version)
    {
      this.currentVersion = Version.Id.ToString(); // for specific directory

      SetClientRoot();
      var arguments = String.Format("sync -w -f @{0}", Version.Id);
      var res = this.Invoke(arguments, longTimeout);
      if (res.ExitCode != 0)
        throw new ExeInvokeException(String.Format("ExitCode = {0}", res.ExitCode));
    }

    protected override IEnumerable<Tuple<string, string>> EnvironmentVariables
    {
      get {
        yield return new Tuple<string, string>("SDPORT", this.sdport);
        yield return new Tuple<string, string>("SDCLIENT", this.sdclient);
        yield return new Tuple<string,string>("SDROOT", this.CurrentInstanceDirectory);
      }
    }

    public override string CurrentInstanceDirectory
    {
      get
      {
        if (this.currentVersion != null)
        {
          return Path.Combine(this.WorkingDirectory, this.currentVersion);
        }
        return this.WorkingDirectory;
      }
    }
  }
  #endregion

  class SDSourceManager : ISourceManager
  {
    readonly string id;
    readonly string sdport, sdclient;
    string workingDirectory;
    SDRepository repository;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.id != null);
      Contract.Invariant(this.sdclient != null);
      Contract.Invariant(this.sdport != null);
      Contract.Invariant(this.repository != null || this.workingDirectory == null); // WorkindDirectory != null implies repository != null
    }

    public static SDSourceManager Factory(XElement xConfig)
    {
      Contract.Requires(xConfig != null);

      return new SDSourceManager(xConfig);
    }

    private SDSourceManager(XElement xConfig)
    {
      Contract.Requires(xConfig != null);

      var xSdPort = xConfig.Element("sdport");
      if (xSdPort == null)
        throw new ArgumentNullException("sdport");
      this.sdport = xSdPort.Value;
      var xSdClient = xConfig.Element("sdclient");
      if (xSdClient == null)
        throw new ArgumentNullException("sdclient");
      this.sdclient = xSdClient.Value;

      // todo? views

      this.id = IDBuilder.FromUri("SD_", this.sdport + '/' + this.sdclient);
    }

    public string Id { get { return this.id; } }

    public string WorkingDirectory
    {
      get { return this.workingDirectory; }
      set
      {
        Contract.Ensures(this.WorkingDirectory != null);

        this.workingDirectory = value;
        this.repository = new SDRepository(value, sdclient: this.sdclient, sdport: this.sdport);
        if (!this.repository.Exists())
        {
          var sdiniFilename = Path.Combine(this.WorkingDirectory, "sd.ini");
          using (var stream = new FileStream(sdiniFilename, FileMode.Create))
          {
            using (var tw = new StreamWriter(stream))
            {
              tw.WriteLine("SDPORT={0}", this.sdport);
              tw.WriteLine("sdclient={0}", this.sdclient);
            }
          }
          this.repository.Init();
        }
      }
    }

    public ISourceManagerVersionSpec ParseVersionSpec(string value)
    {
      return new SDVersionSpec(value);
    }

    [Pure]
    public IEnumerable<SDVersionSpec> VersionRange(SDVersionSpec First, SDVersionSpec Last, string filter = null)
    {
      Contract.Requires(First != null);
      Contract.Requires(Last != null);
      Contract.Ensures(Contract.Result<IEnumerable<SDVersionSpec>>() != null);

      var first = First.IsNull ? new SDVersionSpec(1) : First;
      var tip = this.repository.Changes(1).FirstOrDefault();
      var last = Last.IsNull ? tip : Last;

      var skip = tip.Id - last.Id;
      var count = last.Id - first.Id + 1;
      if (count <= 0)
        return new SDVersionSpec[0];

      return repository.Changes(skip, count, filter);
    }
    public IEnumerable<ISourceManagerVersionSpec> VersionRange(ISourceManagerVersionSpec First, ISourceManagerVersionSpec Last, string filter = null)
    {
      Contract.Ensures(Contract.Result<IEnumerable<ISourceManagerVersionSpec>>() != null);

      return VersionRange((SDVersionSpec)First, (SDVersionSpec)Last, filter);
    }

    public bool GetSources(SDVersionSpec Version)
    {
      Contract.Requires(Version != null);
      Contract.Requires(this.WorkingDirectory != null);

      Directory.CreateDirectory(Path.Combine(this.WorkingDirectory, Version.Id.ToString()));
      repository.Sync(Version);

      return true;
    }
    public bool GetSources(ISourceManagerVersionSpec Version)
    {
      return GetSources((SDVersionSpec)Version);
    }


    public IEnumerable<ISourceManagerVersionSpec> VersionRange(int countPrior, ISourceManagerVersionSpec Last)
    {
      var last = (SDVersionSpec)Last;
      var tip = this.repository.Changes(1).FirstOrDefault();
      last = last.IsNull ? tip : last;

      var skip = tip.Id - last.Id;
      var count = countPrior;
      if (count <= 0)
        return new SDVersionSpec[0];

      return repository.Changes(skip, count, null);
    }

    public IEnumerable<ISourceManagerVersionSpec> VersionRange(ISourceManagerVersionSpec First, int countAfter)
    {
      var first = (SDVersionSpec)First;
      first = first.IsNull ? new SDVersionSpec(1) : first;
      var tip = this.repository.Changes(1).FirstOrDefault();

      var count = countAfter;
      var skip = tip.Id - first.Id - count;
      if (count <= 0)
        return new SDVersionSpec[0];

      return repository.Changes(skip, count, null);
    }
  }
}
