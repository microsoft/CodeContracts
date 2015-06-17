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
using System.Text.RegularExpressions;

namespace NighttestAnalyzer
{
  class Program
  {
    static int Main(string[] args)
    {
      if (args.Length != 1)
      {
        Usage();
        return 1;
      }

      DirectoryInfo dir = new DirectoryInfo(args[0]);

      if (!dir.Exists)
      {
        Usage();
        Console.WriteLine("Directory {0} does not exist", dir);
        return 1;
      }

      DLLResults.WriteHeadersTo(Console.Out);

      foreach (var dllRes in ProcessDirectory(dir))
        dllRes.WriteTo(Console.Out);

      return 0;
    }

    private static void Usage()
    {
      Console.WriteLine("Usage: nighttestanalyzer <dir>");
      Console.WriteLine();
    }

    private static IEnumerable<DLLResults> ProcessDirectory(DirectoryInfo dir)
    {
      foreach (var file in dir.EnumerateFiles())
      {
        var dllRes = ProcessFileName(file.FullName);
        if (dllRes != null)
          yield return dllRes;
      }
    }

    private static DLLResults ProcessFileName(string fullname)
    {
      var ext = Path.GetExtension(fullname);

      if (String.IsNullOrEmpty(ext))
        return null;

      if (ext.ToLower() == ".dll")
        return ProcessDLL(fullname);

      return ProcessFileName(Path.ChangeExtension(fullname, null));
    }

    private static HashSet<string> processedDLLs = new HashSet<string>();

    private static DLLResults ProcessDLL(string fullname)
    {
      if (processedDLLs.Contains(fullname))
        return null;

      processedDLLs.Add(fullname);

      var res1 = ProcessRun(fullname + ".1");
      var res2 = ProcessRun(fullname + ".2");
      var resDb = ProcessDB(fullname);

      return new DLLResults(Path.GetFileName(fullname), res1, res2, resDb);
    }

    private static DBResults ProcessDB(string fullname)
    {
      var res = new DBResults();
      res.Add(fullname + ".sdf");
      res.Add(fullname + ".mdf");
      res.Add(fullname + "_log.ldf");
      return res;
    }

    private static RunResults ProcessRun(string fullname)
    {
      var ferr = new FileInfo(fullname + ".err");

      if (ferr.Exists && ferr.Length > 0)
        return RunResults.Err;

      var fout = new FileInfo(fullname + ".out");

      if (!fout.Exists || fout.Length == 0)
        return RunResults.Err;

      return RunResults.FromOutLines(File.ReadLines(fout.FullName));
    }

    private class DBResults
    {
      public long Size { get; private set; }

      public DBResults()
      { }

      public void Add(string fullname)
      {
        var f = new FileInfo(fullname);
        if (f.Exists)
          this.Size += f.Length;
      }
    }

    private class RunResults
    {
      public static RunResults Err = new RunResults(IsErr: true);

      public bool IsErr { get; private set; }
      public int MaxClassDriversCount { get; private set; }
      public int TotalMethodsAnalyzed { get; private set; }
      public int ReadFromTheCache { get; private set; }
      public double TotalTimeSec { get; private set; }

      public RunResults()
      { }

      public RunResults(bool IsErr)
      {
        this.IsErr = IsErr;
      }
      
      internal static RunResults FromOutLines(IEnumerable<string> outLines)
      {
        var res = new RunResults();
        foreach (var line in outLines)
          res.MatchLine(line);
        return res;
      }

      private static Regex regexMaxClassDriversCount = new Regex(@"\A-- ADD CLASS DRIVER -- Max Class Drivers Count is: (\d+)");
      private static Regex regexTotalMethodsAnalyzed = new Regex(@"\ATotal methods analyzed (\d+)");
      private static Regex regexReadFromTheCache = new Regex(@"\ATotal method analysis read from the cache (\d+)");
      private static Regex regexTotalTimeMS = new Regex(@"\ATotal time (\d+)ms");
      private static Regex regexTotalTimeSec = new Regex(@"\ATotal time (\d*\.\d*)sec");
      private static Regex regexTotalTimeMin = new Regex(@"\ATotal time (\d+):(\d+)min");

      private object MatchLine(string line)
      {
        var match = regexMaxClassDriversCount.Match(line);
        if (match.Success)
          return this.MaxClassDriversCount = Int32.Parse(match.Groups[1].Value);

        match = regexTotalMethodsAnalyzed.Match(line);
        if (match.Success)
          return this.TotalMethodsAnalyzed = Int32.Parse(match.Groups[1].Value);

        match = regexReadFromTheCache.Match(line);
        if (match.Success)
          return this.ReadFromTheCache = Int32.Parse(match.Groups[1].Value);

        match = regexTotalTimeMS.Match(line);
        if (match.Success)
          return this.TotalTimeSec = Int32.Parse(match.Groups[1].Value) * .001;

        match = regexTotalTimeSec.Match(line);
        if (match.Success)
          return this.TotalTimeSec = Double.Parse(match.Groups[1].Value);

        match = regexTotalTimeMin.Match(line);
        if (match.Success)
          return this.TotalTimeSec = Int32.Parse(match.Groups[1].Value) * 60 + Int32.Parse(match.Groups[2].Value);

        return null;
      }
    }

    class DLLResults
    {
      private readonly string filename;
      private readonly RunResults res1;
      private readonly RunResults res2;
      private readonly DBResults resDb;

      public DLLResults(string filename, RunResults res1, RunResults res2, DBResults resDb)
      {
        this.filename = filename;
        this.res1 = res1;
        this.res2 = res2;
        this.resDb = resDb;
      }

      internal static void WriteHeadersTo(TextWriter tw)
      {
        tw.WriteLine("Filename,Size,Time1,Time2,MaxClassDriversCount");
      }

      internal void WriteTo(TextWriter tw)
      {
        if (res1.IsErr || res2.IsErr)
          return;

        if (res1.TotalMethodsAnalyzed == 0)
          return;

        tw.WriteLine("{0},{1},{2},{3},{4}", filename, resDb.Size, res1.TotalTimeSec, res2.TotalTimeSec, Math.Max(res1.MaxClassDriversCount, res2.MaxClassDriversCount));
      }
    }
  }
}
