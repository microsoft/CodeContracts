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
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Baseline
{

  class Baseliner
  {

    #region contracts
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.Repo != null);
    }
    #endregion

    // repository to perform baselining on
    Repository Repo;

    public Baseliner(Repository repo)
    {
      this.Repo = repo;
    }

    /// <summary>
    /// perform baselining on Repo with specified strategy, rebuilding each version
    /// </summary>
    /// <param name="baselineStrategy">ilBased, typeBased, mixed, or semantic</param>
    public void BaselineWithRebuild(string baselineStrategy, int? startAtVersion = -1)
    {
      BaselineImpl(true, baselineStrategy, startAtVersion);
    }

    /// <summary>
    /// perform baselining on Repo with specified strategy, rebuilding each version only if it hasn't already been built
    /// </summary>
    /// <param name="baselineStrategy">ilBased, typeBased, mixed, or semantic</param>
    public void Baseline(string baselineStrategy)
    {
      BaselineImpl(false, baselineStrategy);
    }

    // do the actual work of baselining
    private void BaselineImpl(bool rebuild, string baselineStrategy, int? startAtVersion = -1)
    {
      List<string> reversed = new List<string>();
      // version numbers are most recent first by default; reverse this ordering
      foreach (string vnum in Repo.GetVersionNumbers()) reversed.Add(vnum);
      reversed.Reverse();
      List<string> baselines = new List<string>(); // holds baseline files of previous revision
      List<string> nextBaselines = new List<string>(); // holds baseline files of current revision
      // set up logging
      string logFileName = Path.Combine(Repo.WorkingDirectory, baselineStrategy + "_log.txt");
      var logFile = File.AppendText(logFileName);
      logFile.AutoFlush = true;
      // for each version of the program from first to last
      foreach (string version in reversed)
      {
        if (Convert.ToInt32(version) < startAtVersion) continue;
        Console.WriteLine("Baselining " + version);
        WarningInfo log = new WarningInfo(version);
        if (!Repo.CheckedOut(version)) Contract.Assert(Repo.Checkout(version) != null); // must check out repo in order to baseline it!
        if (rebuild || !Repo.Built(version))
        {
          Console.WriteLine("version " + version + " not built; building");
          if (!Repo.Build(version))
          {
            Console.WriteLine("Build of version " + version + " failed!");
            continue;
          }
          //Contract.Assert(Build(version)); // must successfully build repo in order to baseline it!
        }
        string versionDir = Repo.MakeRepoPath(version);
        // get repro.bat file for all built projects
        List<string> repros = Util.GetRepros(versionDir);

        // else, run each repro.bat with the baseline flag pointing at the baseline file for the previous version, if such a file exists
        foreach (string repro in repros)
        {
          bool newProj = false;
          string baselineFileName = repro.Replace("repro.bat", "base.xml");
          nextBaselines.Add(baselineFileName);
          // cleanup old baseline files to avoid accidentally suppressing all errors or other weirdness
          if (File.Exists(baselineFileName))
          {
            File.Delete(baselineFileName);
          }
          if (File.Exists(baselineFileName + ".new"))
          {
            File.Delete(baselineFileName + ".new");
          }
          string stem = Path.Combine(Repo.WorkingDirectory, Repo.Name);
          var match = Regex.Match(repro.Replace("repro.bat", ""), "[0-9]+(.*)");
          Contract.Assert(match.Success);
          string repoStem = match.Groups[1].Value;
          repoStem = repoStem.Substring(0, repoStem.IndexOf("obj")); // try to make stem robust to silly changes in hierarchy of binary directory
          // try to be robust to changes like changing from '\Theme\' to '\Source\Theme'
          string altRepoStem = repoStem.Substring(repoStem.IndexOf('\\', 1));

          //Console.WriteLine("repoStem is " + repoStem);
          //Console.WriteLine("altRepoStem is " + altRepoStem);

          string baselineMatch = null;
          // pick appropriate baseline for this repro file
          foreach (string baseline in baselines)
          {
            Console.WriteLine("baseline is " + baseline);
            if (baseline.Contains(repoStem))
            {
              // only one baseline should match the stem; otherwise it's ambiguous which one we should use
              Contract.Assert(baselineMatch == null, "ambiguous which baseline file to use " + baselineMatch + " or " + baseline);
              baselineMatch = baseline;
            }
          }

          // try alternate baseline matching if needed
          if (baselineMatch == null && altRepoStem.Length > 2)
          {
            foreach (string baseline in baselines)
            {
              Console.WriteLine("baseline is " + baseline);
              if (baseline.Contains(altRepoStem))
              {
                baselineMatch = baseline;
              }
            }
          }

          if (baselineMatch == null)
          {
            Console.WriteLine("Found new project " + repro + "; using empty baseline");
            // we have no previous baseline file for this project; it must be new
            baselineMatch = baselineFileName;
            newProj = true;
          }
          Contract.Assert(baselineMatch != null, "No baseline file matches stem " + repoStem);
          Console.WriteLine("Using baseline file " + baselineMatch);

          Console.WriteLine("Running analysis...");
          string analysisResults = RunAnalysisWithBaseLine(repro, baselineMatch, baselineStrategy, newProj);
          ParseAndLogResults(analysisResults, log);
          Console.WriteLine("Done.");
        }

        // update baseline files
        baselines = nextBaselines;
        nextBaselines = new List<string>();
        // write log data
        logFile.WriteLine(log.DumpToCSV());
      }
    }

    // ugly regexp matching of clousot output to get our results
    public void ParseAndLogResults(string analysisResults, WarningInfo log)
    {
      Contract.Requires(analysisResults != null);
      Contract.Requires(log != null);
      var hashNameDiscrepancies = Regex.Matches(analysisResults, "HashName Discrepancy:(.*)");
      List<string> interestingMethods = new List<string>();
      foreach (Match match in hashNameDiscrepancies)
      {
        interestingMethods.Add(match.Groups[1].Value.ToString());
      }
      var methodMatchFailures = Regex.Match(analysisResults, "Failed to match ([0-9]+) methods");
      int matchFailures = 0;
      if (methodMatchFailures.Success) matchFailures = Convert.ToInt32(methodMatchFailures.Groups[1].Value);
      log.LogMethodMatchFailures(matchFailures);
      int assertionsChecked = 0, assertionsCorrect = 0, assertionsUnknown = 0, assertionsFalse = 0, assertionsMasked = 0, assertionsUnreached = 0;
      int index = analysisResults.IndexOf("OutputSuggestion,,Checked ");
      if (index != -1)
      {
        string resultsLine = analysisResults.Substring(index);
        Console.WriteLine(resultsLine);
        var assertionsCheckedMatch = Regex.Match(resultsLine, "Checked ([0-9]+) assertion");
        var assertionsCorrectMatch = Regex.Match(resultsLine, "([0-9]+) correct");
        var assertionsUnknownMatch = Regex.Match(resultsLine, "([0-9]+) unknown");
        var assertionsFalseMatch = Regex.Match(resultsLine, "([0-9]+) false");
        var assertionsMaskedMatch = Regex.Match(resultsLine, "([0-9]+) masked");
        var assertionsUnreachedMatch = Regex.Match(resultsLine, "([0-9]+) unreached");
        if (assertionsCheckedMatch.Success) assertionsChecked = Convert.ToInt32(assertionsCheckedMatch.Groups[1].Value);
        if (assertionsCorrectMatch.Success) assertionsCorrect = Convert.ToInt32(assertionsCorrectMatch.Groups[1].Value);
        if (assertionsUnknownMatch.Success) assertionsUnknown = Convert.ToInt32(assertionsUnknownMatch.Groups[1].Value);
        if (assertionsFalseMatch.Success) assertionsFalse = Convert.ToInt32(assertionsFalseMatch.Groups[1].Value);
        if (assertionsMaskedMatch.Success) assertionsMasked = Convert.ToInt32(assertionsMaskedMatch.Groups[1].Value);
        if (assertionsUnreachedMatch.Success) assertionsUnreached = Convert.ToInt32(assertionsUnreachedMatch.Groups[1].Value);
      }
      log.LogWarningInfo(assertionsChecked, assertionsCorrect, assertionsUnknown, assertionsFalse, assertionsMasked, assertionsUnreached, interestingMethods);
    }

    // use repro.bat file to re-run original analysis using given file as baseline
    [ContractVerification(false)]
    private string RunAnalysisWithBaseLine(string reproName, string baselineFile, string baselineStrategy, bool newProject)
    {
      Contract.Requires(reproName != null);
      Contract.Requires(baselineFile != null);
      Contract.Requires(baselineStrategy != null);
      Contract.Requires(newProject || File.Exists(baselineFile));
      string newBaseLine = Path.Combine(Directory.GetCurrentDirectory(), reproName.Replace("repro.bat", "base.xml"));
      Contract.Requires(!File.Exists(newBaseLine));
      Contract.Ensures(File.Exists(newBaseLine));
      string invokeDir = reproName.Replace("repro.bat", "");

      // remove max warning bound from rsp file
      List<string> rsp = Util.GetFilesMatching(invokeDir, "*.rsp");
      Contract.Assert(rsp.Count == 1, "only expecting 1 rsp file, found " + rsp.Count + " in " + invokeDir);
      string rspContents = File.ReadAllText(rsp[0]);
      File.WriteAllText(rsp[0], rspContents.Replace(" -maxwarnings 50 ", ""));

      string baselineFullName = Path.Combine(Directory.GetCurrentDirectory(), baselineFile);
      // create new repro file that uses the baseline we are given
      // modify repro to run clousot.exe instead of cccheck, add appropriate baselining flag
      string contents = File.ReadAllText(reproName);
      // this is a hack, but it doesn't matter since we won't have to modify the repro file anymore once my changes are in the main branch of Clousot
      contents = contents.Replace(@"C:\Program Files (x86)\Microsoft\Contracts\Bin\cccheck.exe""", "clousot\" -nobox -baseLine \"" +
                                  Path.Combine(Directory.GetCurrentDirectory(), baselineFile) + "\" -baseLineStrategy " + baselineStrategy + " -cache");
      string newRepro = reproName.Replace("repro.bat", "myrepro.bat");
      File.Delete(newRepro);
      File.WriteAllText(newRepro, contents);
      string oldRepro = reproName.Replace("repro.bat", "oldRepro.bat");
      File.Delete(oldRepro);
      File.Move(reproName, oldRepro);
      Console.WriteLine("running " + newRepro + " at ");
      Console.WriteLine("{0:G}", DateTime.Now);
      string result = Util.Invoke(newRepro, invokeDir, "");
      Console.WriteLine("done with repro.");
      //Console.WriteLine("Result " + result);
      // HACK to get a new baseline.xml file; just run the analysis again. this is bad. the right fix is to combine base.xml and base.xml.new into a single baseline file
      contents = File.ReadAllText(oldRepro);
      contents = contents.Replace(@"C:\Program Files (x86)\Microsoft\Contracts\Bin\cccheck.exe""", "clousot\" -baseLine \"" +
                                  Path.Combine(Directory.GetCurrentDirectory(), reproName.Replace("repro.bat", "base.xml")) + "\" -baseLineStrategy " + baselineStrategy + " -cache");
      string reproNewBase = reproName.Replace("repro.bat", "reproNewBase.bat");
      File.WriteAllText(reproNewBase, contents);
      Util.Invoke(reproNewBase, invokeDir, "");
      return result;
    }
  }

}
