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

namespace ParseStats
{
  using System.Diagnostics.Contracts;
  using Results = Dictionary<string, LocalStats>;

  class LocalStats
  {
    public string MethodsAnalyzed;
    public string MethodsWithBaseLine;
    public string MethodsWithoutBaseLine;
    public string MethodsWithIdenticalBaseLine;
    public string MethodsWith0Warnings;
    public string MethodsReadFromCache;
    public string Checked;
    public string Correct;
    public string Unknown;
    public string Unreached;
    public string TotalTime;
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


    internal void Add(LocalStats value)
    {
      AddInts(ref MethodsAnalyzed, value.MethodsAnalyzed);
      AddInts(ref MethodsWithBaseLine, value.MethodsWithBaseLine);
      AddInts(ref MethodsWithoutBaseLine, value.MethodsWithoutBaseLine);
      AddInts(ref MethodsWithIdenticalBaseLine, value.MethodsWithIdenticalBaseLine);
      AddInts(ref MethodsWith0Warnings, value.MethodsWith0Warnings);
      AddInts(ref MethodsReadFromCache, value.MethodsReadFromCache);
      AddInts(ref Checked, value.Checked);
      AddInts(ref Correct, value.Correct);
      AddInts(ref Unknown, value.Unknown);
      AddInts(ref Unreached, value.Unreached);
      AddFloat(ref TotalTime, value.TotalTime);
    }

    private void AddFloat(ref string aloc, string value)
    {
      var a = ParseFloat(aloc);
      var b = ParseFloat(value);
      aloc = (a + b).ToString();
    }

    private double ParseFloat(string aloc)
    {
      double result;
      if (double.TryParse(aloc, out result))
      {
        return result;
      }
      return 0;
    }

    private int ParseInt(string aloc)
    {
      int result;
      if (Int32.TryParse(aloc, out result))
      {
        return result;
      }
      return 0;
    }

    private void AddInts(ref string aloc, string value)
    {
      var a = ParseInt(aloc);
      var b = ParseInt(value);
      aloc = (a + b).ToString();
    }

    public string AvgTimePerMethod
    {
      get
      {
        var totalTime = ParseFloat(this.TotalTime);
        var methods = ParseInt(this.MethodsAnalyzed);
        if (methods > 0)
        {
          return (totalTime / methods).ToString();
        }
        else
        {
          return "";
        }
      }
    }
  }

  class Program
  {
    static int Main(string[] args)
    {
      if (args.Length > 2) { System.Diagnostics.Debugger.Launch(); }
      if (args.Length < 1)
      {
        Console.WriteLine("Usage: parseStats logfile");
        return -1;
      }
      if (args[0].StartsWith(@"*\"))
      {
        var components = args[0].Split('\\');
        var results = new Results();
        // first dir is key
        Contract.Assume(components.Count() > 0, "Follows from the fact that Split return a non-empty array");
        foreach (var file in Directory.EnumerateFiles(".", components.Last(), SearchOption.AllDirectories)) {
          var fileComps = file.Split('\\');
          if (Matches(components, fileComps))
          {
            var lr = RunOneLog(file);
            var stats = Sum(lr);
            results.Add(fileComps[1], stats);
          }
        }
        PrintStats(results, args.Length > 1);
      }
      else
      {
        var lr = RunOneLog(args[0]);
        PrintStats(lr, args.Length > 1);
      }
      return 0;
    }

    private static LocalStats Sum(Results lr)
    {
      var result = new LocalStats();
      foreach (var value in lr.Values)
      {
        result.Add(value);
      }
      return result;
    }

    private static bool Matches(string[] components, string[] fileComps)
    {
      Contract.Ensures(!Contract.Result<bool>() || fileComps.Length > 1);
      for (int i = 0; i < components.Length - 1; i++)
      {
        var toMatch = components[components.Length - 1 - i];
        var filePath = fileComps[fileComps.Length - 1 - i];
        if (String.Compare(filePath, toMatch, true) != 0)
        {
          return false;
        }
      }
      return true;
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
        "Method w/0W",
        "Methods read from Cache",
        "AvgTimePerMethod",
        "Total Time"
        ));
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

        yield return FormatLine(latex, name,
          result.MethodsAnalyzed, 
          result.MethodsWithDiffs,
          result.NewMethods,
          result.Checked, 
          result.Correct,
          result.Warnings,
          result.Unreached,
          result.MethodsWith0Warnings,
          result.MethodsReadFromCache,
          result.AvgTimePerMethod,
          result.TotalTime
          );
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
      return name;
      /*
      name = name.Replace(".", "");
      name = name.Replace("Tf", "TF");
      name = name.Replace("Cm", "CM");
      var chars = name.Where(Char.IsUpper);
      return String.Concat(chars);
       */ 
    }



    static Results RunOneLog(string file)
    {
      var stats = OneFileStats(file);

      return stats;

    }

    private static LocalStats GetOrMaterialize(string project, Dictionary<string, LocalStats> dic)
    {
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
          string projectName = null;
          if (MatchOneValue(ref projectName, line, @"^\s*(\d+(:\d+)?>)?\s*CodeContracts:\s+([^:]+):", groupNo: 3) /*&& projectName != null*/) {
            if (!projectName.StartsWith("Checked ") && !projectName.StartsWith("Suggested requires"))
            {
              last = GetOrMaterialize(projectName, result);
              MatchOneValue(ref last.MethodsAnalyzed, line, @"Total methods analyzed\s+(\d+)");
              MatchOneValue(ref last.MethodsWith0Warnings, line, @"Methods with 0 warnings\s+(\d+)");
              MatchOneValue(ref last.MethodsWithBaseLine, line, @"Methods with baseline:\s+(\d+)");
              MatchOneValue(ref last.MethodsWithoutBaseLine, line, @"Methods w/o  baseline:\s+(\d+)");
              MatchOneValue(ref last.MethodsReadFromCache, line, @"Total method analysis read from the cache\s+(\d+)");
              MatchOneValue(ref last.MethodsWithIdenticalBaseLine, line, @"Methods with identical baseline:\s+(\d+)");
              MatchOneValue(ref last.Checked, line, @"Checked\s+(\d+)\s+assertion");
              MatchOneValue(ref last.Correct, line, @"(\d+)\s+correct");
              MatchOneValue(ref last.Unknown, line, @"(\d+)\s+unknown");
              MatchOneValue(ref last.Unreached, line, @"(\d+)\s+unreached");
              MatchOneValue(ref last.TotalTime, line, @"Total time\s+(\d+.\d+)sec.");
              if (MatchOneValue(ref last.TotalTime, line, @"Total time\s+(\d+:\d+)min."))
              {
                var minsec = last.TotalTime.Split(':');
                Contract.Assume(minsec.Length >= 2);
                var min = Int32.Parse(minsec[0]);
                var sec = Int32.Parse(minsec[1]);
                last.TotalTime = (min * 60 + sec).ToString();
              }
              if (MatchOneValue(ref last.TotalTime, line, @"Total time\s+(\d+)ms."))
              {
                var sec = Int32.Parse(last.TotalTime);
                last.TotalTime = (sec/1000.0).ToString();
              }
            }
          }

        }
      }
      catch
      {
      }
      return result;
    }


    private static bool MatchOneValue(ref string result, string line, string pattern, int groupNo = 1)
    {
      Contract.Ensures(!Contract.Result<bool>() || result != null);

      var aMatch = Regex.Match(line, pattern);
      if (aMatch.Success)
      {
        result = aMatch.Groups[groupNo].Value;
      }
      return aMatch.Success;
    }
  }
}
