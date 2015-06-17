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
    /// abstraction of SVN source control repository
    /// </summary>
    class SVNRepository : Repository
    {
    #region SVNRepository

    #region constructors
    protected override string ExeName { get { return "svn.exe"; } }

    public SVNRepository(string workingDirectory, string url, string name)
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
            Console.WriteLine("Checked out version " + versionNum);
            //Directory.Move(Path.Combine(this.WorkingDirectory, "svn"), checkoutTarget);
            return checkoutTarget;
        }
        Console.WriteLine("CHECKOUT OF " + checkoutTarget + " FAILED");
        // checkout failed
        return null;
    }

    // returns a list of all version numbers for this repository. either reads them from versionNums.xml file or creates this file
    public override IEnumerable<string> GetVersionNumbers()
    {
        string VERSION_NUMS_FILE = Path.Combine(this.WorkingDirectory, "versionNums.xml");
        Console.WriteLine(VERSION_NUMS_FILE);
        // see if we've already cached version number info
        if (!File.Exists(VERSION_NUMS_FILE))
        {
            Console.WriteLine("version listing file does not exist, creating");
            var arguments = "log ";
            arguments += this.Url + " --xml";
            var timeout = longTimeout;
            // get version num XML
            var res = this.InvokeSourceManagerForOutput(arguments, timeout);
            File.WriteAllText(VERSION_NUMS_FILE, res);
        }
        try
        {
            var xmlDoc = XDocument.Load(VERSION_NUMS_FILE, LoadOptions.None);
            // parse version nums
            IEnumerable<string> versionNums =
              from entry in xmlDoc.Element("log").Elements("logentry") select entry.Attribute("revision").Value;
            return versionNums;
        }
        catch (Exception e)
        {
            Console.WriteLine("Malformed XML in  " + VERSION_NUMS_FILE + "; couldn't parse.");
            throw e;
        }
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
        var arguments = "checkout ";
        arguments += this.Url + " -r " + revision;
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
  #endregion SVNRepository

}
