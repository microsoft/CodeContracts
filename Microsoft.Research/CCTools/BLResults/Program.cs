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
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CCBaseline
{
  using System.Diagnostics.Contracts;
  using Results = Dictionary<string, List<Result>>;

  class Result
  {
    public Stats Original;
    public Stats BL0;
    public Stats BL1;
    public Stats BL2;

    internal void InsertBaseline(Stats stats)
    {
      if (stats.BaselineVersion == stats.AnalyzedVersion) {
        this.BL0 = stats;
        return;
      }
      // earlier
      if (BL1.BaselineVersion == null)
      {
        BL1 = stats;
        return;
      }
      if (string.Compare(stats.BaselineVersion, BL1.BaselineVersion) < 0)
      {
        BL2 = stats;
      }
      else
      {
        // swap
        BL2 = BL1;
        BL1 = stats;
      }
    }
  }
  class LocalStats
  {
    public string MethodsAnalyzed;
    public string MethodsWithBaseLine;
    public string MethodsWithoutBaseLine;
    public string MethodsWithIdenticalBaseLine;
    public string MethodsWith0Warnings;
    public string Checked;
    public string Correct;
    public string Unknown;
    public string Unreached;
    public string Warnings
    {
      get
      {
        if (this.Unknown != null) return this.Unknown;
        if (this.Checked != null) return "0";
        return null;
      }
    }

    public string NewMethods
    {
      get
      {
        if (MethodsAnalyzed == null || MethodsWithBaseLine == null || MethodsWithoutBaseLine == null || MethodsWithIdenticalBaseLine == null) return "";
        return (int.Parse(MethodsAnalyzed) - int.Parse(MethodsWithBaseLine)).ToString();
      }
    }

    public string MethodsWithDiffs
    {
      get
      {
        if (MethodsAnalyzed == null || MethodsWithBaseLine == null || MethodsWithoutBaseLine == null || MethodsWithIdenticalBaseLine == null) return "";
        return (int.Parse(MethodsWithBaseLine) - int.Parse(MethodsWithIdenticalBaseLine)).ToString();
      }
    }

  }
  struct Stats
  {
    public string ProjectName;
    public string MethodsAnalyzed;
    public string MethodsWith0Warnings;
    public string Checked;
    public string Correct;
    public string Unknown;
    public string Unreached;
    public string BaselineVersion;
//    public bool Original;
    public string AnalyzedVersion;


    public string Warnings
    {
      get
      {
        if (this.Unknown != null) return this.Unknown;
        if (this.Checked != null) return "0";
        return null;
      }
    }

    public int WarningsNum
    {
      get
      {
        if (Warnings != null) return Int32.Parse(Warnings);
        return 0;
      }
    }
  }

  class Program
  {
    static int Main(string[] args)
    {
      if (args.Length < 1)
      {
        Console.WriteLine("Usage: blresults <repoDir>");
        return -1;
      }
      var results = new Results();
      if (args[0].EndsWith("*"))
      {
        RunMany(args, results);
      }
      else if (args[0].EndsWith(".log"))
      {
        var lr = RunOneLog(args[0]);
        PrintStats(lr, args.Length > 1);
        return 0;
      }
      else
      {
        RunOne(args[0], results);
      }
      PrintStats(results);
      return 0;
    }

    static int RunMany(string[] args, Results results)
    {
      Contract.Requires(args != null);
      Contract.Requires(Contract.ForAll(args, a => a != null));

      var len = args[0].Length - 1;
      Contract.Assume(len >= 0);
      var root = args[0].Substring(0, len);
      var dirs = Directory.EnumerateDirectories(root).OrderBy(s => s).ToArray();
      Contract.Assume(Contract.ForAll(dirs, d => d != null));
      var minversion = "";
      for (int i = 0; i < dirs.Length; i++)
      {
        var dir = dirs[i];
        var match = Regex.Match(dir, @"[\\/](\d+)$");
        if (match.Success)
        {
          // a version number
          var version = match.Groups[1].Value;
          if (string.Compare(version, minversion) < 0) continue;
          RunOne(dir, results);
        }
      }
      return 0;
    }

    private static void PrintStats(Dictionary<string, LocalStats> results, bool latex)
    {
      Console.WriteLine(FormatLine(latex,
        "Assembly",
        "Methods",
        "Changed",
        "New",
        "Checks",
        "Correct",
        "Warnings",
        "Unreached",
        "Method w/0W"));
      var lines = GetStats(results, latex).ToArray();
      Array.Sort(lines);
      foreach (var line in lines)
      {
        Console.WriteLine(line);
      }
    }

    static string[] toSkip = { 
                               "DW", "S", "DGC", "DGCM", "DGL", "DGS", "GC", "GCM", "GL", "GPBS", "GPBSM", "GPTF", "GPU", "GS", "TFD" };

    private static IEnumerable<string> GetStats(Dictionary<string, LocalStats> results, bool latex)
    {
      foreach (var pair in results)
      {
        var result = pair.Value;
        var name = NormalizeName(pair.Key);

        if (latex && toSkip.Contains(name)) continue;

        yield return FormatLine(latex, name, result.MethodsAnalyzed, result.MethodsWithDiffs, result.NewMethods, result.Checked, result.Correct, result.Warnings, result.Unreached, result.MethodsWith0Warnings);
      }
    }

    private static string FormatLine(bool latex, params string[] args)
    {
      var joiner = latex ? " & " : ", ";
      var terminator = latex ? @" \\" : "";
      var padded = args.Take(1).Select(s => FixedWidth(s, true)).Concat(args.Skip(1).Select(s => FixedWidth(s)).ToArray());

      return string.Join(joiner, padded) + terminator;

    }

    private static string FixedWidth(string s, bool left = false)
    {
      if (left)
      {
        return String.Format("{0,-10}", s);
      }
      return String.Format("{0,10}", s);
    }
    /// <summary>
    /// Remove dots, keep only uppercase letters, except change Tf to TF
    /// </summary>
    private static string NormalizeName(string name)
    {
      name = name.Replace(".", "");
      name = name.Replace("Tf", "TF");
      name = name.Replace("Cm", "CM");
      var chars = name.Where(Char.IsUpper);
      return String.Concat(chars);
    }


    private static void PrintStats(Results results)
    {
      foreach (var pair in results)
      {
        Console.WriteLine("Project {0}:", pair.Key);
        Console.WriteLine("Version,Methods,Checks,Correct,Warnings,Methods w/0W,BL0 Warnings,BL0 Mw0W,BL1 Warnings,BL1 Mw0W,BL2 Warnings,BL2 Mw0W,New Warnings"); 

        foreach (var result in pair.Value)
        {
          Stats min = Minimum(result.BL1, result.BL2);
          var newWarnings = (min.WarningsNum >= result.BL0.WarningsNum) ? min.WarningsNum - result.BL0.WarningsNum : min.WarningsNum;
          Console.WriteLine(string.Join(",", result.Original.AnalyzedVersion, result.Original.MethodsAnalyzed, result.Original.Checked, result.Original.Correct, result.Original.Warnings, result.Original.MethodsWith0Warnings, result.BL0.Warnings, result.BL0.MethodsWith0Warnings, result.BL1.Warnings, result.BL1.MethodsWith0Warnings, result.BL2.Warnings, result.BL2.MethodsWith0Warnings, newWarnings));
        }
        Console.WriteLine();
      }
    }

    private static Stats Minimum(Stats stats1, Stats stats2)
    {
      if (stats1.Warnings == null || stats2.Warnings == null) return stats1;

      int w1, w2;
      if (!Int32.TryParse(stats1.Warnings, out w1) || !Int32.TryParse(stats2.Warnings, out w2)) return stats1;
      if (w1 < w2) return stats1;
      return stats2;
    }

    static void  RunOne(string dir, Results results)
    {
      if (!Directory.Exists(dir))
      {
        Console.WriteLine("Directory {0} does not exist", dir);
      }
      string analyzedVersion = "<dummy>";
      MatchOneValue(ref analyzedVersion, dir, @"(^|[\\/])(\d+)", groupNo: 2);

      Dictionary<string,Result> localResults = new Dictionary<string,Result>();
      foreach (var file in Directory.EnumerateFiles(dir, "*.txt"))
      {
        var original = file.Contains("save");
        var vmatch = Regex.Match(file, @"buildlog.(\d+)");
        if (!vmatch.Success) continue;
        var version = vmatch.Groups[1].Value;
        var stats = OneFileStats(file);
        foreach (var projPair in stats)
        {

          var current = GetProjectResult(localResults, projPair.Key);
          Contract.Assume(current != null);
          var stat = new Stats
            {
              ProjectName = projPair.Key,
              AnalyzedVersion = analyzedVersion,
              BaselineVersion = version,
              Checked = projPair.Value.Checked,
              Correct = projPair.Value.Correct,
              MethodsAnalyzed = projPair.Value.MethodsAnalyzed,
              MethodsWith0Warnings = projPair.Value.MethodsWith0Warnings,
              Unknown = projPair.Value.Unknown,
              Unreached = projPair.Value.Unreached
            };
          if (original)
          {
            current.Original = stat;
          }
          else
          {
            current.InsertBaseline(stat);
          }
        }
      }

      foreach (var pair in localResults)
      {
        var list = GetProjectResults(results, pair.Key);
        list.Add(pair.Value);
      }
    }

    static Dictionary<string, LocalStats> RunOneLog(string file)
    {
      var stats = OneFileStats(file);

      return stats;

    }

    private static LocalStats GetOrMaterialize(string project, Dictionary<string, LocalStats> dic)
    {
      Contract.Ensures(Contract.Result<LocalStats>() != null);

      LocalStats result;
      if (!dic.TryGetValue(project, out result))
      {
        result = new LocalStats();
        dic.Add(project, result);
      }
      else
      {
        Contract.Assume(result != null);
      }
      return result;
    }
    private static Dictionary<string,LocalStats> OneFileStats(string file)
    {
      var result = new Dictionary<string, LocalStats>();
      try
      {
        var lines = File.ReadAllLines(file);

        LocalStats last = null;
        for (int i = 0; i < lines.Length; i++)
        {
          var line = lines[i];
          string projectName = "<dummy>";
          if (MatchOneValue(ref projectName, line, @"^\s*(\d+(:\d+)?>)?CodeContracts:\s+([^:]+):", groupNo: 3)) {
            if (!projectName.StartsWith("Checked "))
            {
              last = GetOrMaterialize(projectName, result);
              MatchOneValue(ref last.MethodsAnalyzed, line, @"Total methods analyzed\s+(\d+)");
              MatchOneValue(ref last.MethodsWith0Warnings, line, @"Methods with 0 warnings\s+(\d+)");
              MatchOneValue(ref last.MethodsWithBaseLine, line, @"Methods with baseline:\s+(\d+)");
              MatchOneValue(ref last.MethodsWithoutBaseLine, line, @"Methods w/o  baseline:\s+(\d+)");
              MatchOneValue(ref last.MethodsWithIdenticalBaseLine, line, @"Methods with identical baseline:\s+(\d+)");
              MatchOneValue(ref last.Checked, line, @"Checked\s+(\d+)\s+assertion");
              MatchOneValue(ref last.Correct, line, @"(\d+)\s+correct");
              MatchOneValue(ref last.Unknown, line, @"(\d+)\s+unknown");
              MatchOneValue(ref last.Unreached, line, @"(\d+)\s+unreached");
            }
          }

        }
      }
      catch
      {
      }
      return result;
    }

    private static Result GetProjectResult(Dictionary<string, Result> local, string projectName)
    {
      Result result;
      if (!local.TryGetValue(projectName, out result))
      {
        result = new Result();
        local.Add(projectName, result);
      }
      return result;
    }
    private static List<Result> GetProjectResults(Results all, string projectName)
    {
      Contract.Ensures(Contract.Result<List<Result>>() != null);
      List<Result> list;
      if (!all.TryGetValue(projectName, out list))
      {
        list = new List<Result>();
        all.Add(projectName, list);
      }
      else
      {
        Contract.Assume(list != null);
      }
      return list;
    }

    private static bool MatchOneValue(ref string result, string line, string pattern, int groupNo = 1)
    {
      Contract.Requires(result != null);
      Contract.Ensures(result != null);

      var aMatch = Regex.Match(line, pattern);
      if (aMatch.Success)
      {
        result = aMatch.Groups[groupNo].Value;
        Contract.Assert(result != null);
      }
      return aMatch.Success;
    }
  }
}
