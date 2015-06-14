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
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Baseline
{

  /// <summary>
  /// abstraction of Git source control repository
  /// </summary>
  class GitRepository : Repository
  {
    #region GitRepository

    #region constructors
    protected override string ExeName { get { return "git.exe"; } }

    public GitRepository(string workingDirectory, string url, string name)
      : base(workingDirectory, url, name)
    { }


    #endregion constructors

    #region implementation of abstract methods
    [Pure]
    public override bool Built(string versionNum)
    {
      string versionPath = MakeRepoPath(versionNum);
      if (Util.GetFilesMatching(versionPath, "*.csproj").Count == 0) return true; // nothing to build; trivially built
      string versionDir = MakeRepoPath(versionNum);
      List<string> repros = Util.GetRepros(versionDir); // if we've already run the analysis and created a repro file, the project has been built and analyzed
      return repros.Count != 0;
    }

    [Pure]
    public override bool CheckedOut(string versionNum)
    {
      return Directory.Exists(MakeRepoPath(versionNum));
    }

    // returns path to directory containing checked-out repo on success, null otherwise
    public override string Checkout(string versionNum)
    {
      string checkoutTarget = MakeRepoPath(versionNum);
      if (CheckedOut(versionNum))
      {
        return checkoutTarget; // already have this version of the repo
      }

      Directory.CreateDirectory(checkoutTarget);

      Console.WriteLine("Checking out version " + versionNum);
      bool success = DoCheckout(versionNum, checkoutTarget);
      if (success)
      {
        // TODO: need to cd into directory and do git reset --hard <version_num>
        if (this.InvokeSourceManager(" reset --hard ", checkoutTarget))
        {
          Console.WriteLine("Checked out version " + versionNum);
          return checkoutTarget;
        }
        Console.WriteLine("Reverting to version " + versionNum + " FAILED");
      }
      Console.WriteLine("CHECKOUT OF " + checkoutTarget + " FAILED");
      // checkout failed
      return null;
    }

    // returns a list of all version numbers for this repository. either reads them from gitVersionNums.txt file or creates this file
    public override IEnumerable<string> GetVersionNumbers()
    {
      bool success = DoCheckout("", this.Name);
      if (!success)
      {
        Console.WriteLine("checkout failed.");
        return new string[0];
      }

      string FILENAME = "gitVersionNums.txt";
      string VERSION_NUMS_FILE = Path.Combine(this.WorkingDirectory, FILENAME);
      Console.WriteLine(VERSION_NUMS_FILE);
      // see if we've already cached version number info
      if (!File.Exists(VERSION_NUMS_FILE))
      {
        Console.WriteLine("version listing file does not exist, creating");
        var arguments = "log";
        var timeout = longTimeout;
        // get version num text from git
        var res = this.InvokeSourceManagerForOutput(arguments, timeout);
      }

      string COMMIT = "commit ";
      string[] fileText = File.ReadAllLines(VERSION_NUMS_FILE);
      List<string> commits = new List<string>();
      foreach (string line in fileText)
      {
        if (line.StartsWith(COMMIT))
        {
          // TODO: will line end with a '\n'? if so, we need to strip it off
          string commitNum = line.Substring(COMMIT.Length, line.Length - COMMIT.Length);
          commits.Add(commitNum);
        }
      }
      return commits;
    }

    public override bool Build(string version)
    {
      string repoPath = MakeRepoPath(version);
      bool success = true;
      List<string> solutions = Util.GetSolutionFiles(repoPath); // find buildable entities
      Console.WriteLine(solutions.Count + " slns");
      foreach (string sln in solutions) // build 'em
      {
        bool result = BuildSolution(sln, version) && success;
        success = success && result;
      }
      if (!Built(version)) return false;
      return success;
    }
    #endregion implementation of abstract methods

    #region utility methods
    private bool DoCheckout(string revision, string workingDir, int? timeoutMilliseconds = null)
    {
      Contract.Requires(revision != null);
      var arguments = "clone ";
      // purposely ignore revision number; must check out whole repository and revert
      arguments += this.Url;
      var timeout = timeoutMilliseconds ?? longTimeout;
      return this.InvokeSourceManager(arguments, workingDir, timeout);
    }

    private bool BuildSolution(string solution, string version)
    {
      Contract.Requires(solution != null);
      Contract.Requires(version != null);
      Console.WriteLine("Building " + solution);
      var builder = MSBuild.Factory(".");
      var projectPath = new string[] { solution };
      return builder.Build(projectPath, -1, MakeRepoPath(version));
    }
    #endregion

  }
    #endregion GitRepository

}
