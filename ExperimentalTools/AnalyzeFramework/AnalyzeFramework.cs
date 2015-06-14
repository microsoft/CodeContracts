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
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace AnalyzeFramework
{
  class AnalyzeFramework
  {
    static List<string> binaries;

    static bool HasErrors = false;
    static void Main(string[] args)
    {
      string errorMsg;
      if (!CheckOptionsAndInitializeVars(args, out errorMsg))
      {
        Console.WriteLine(errorMsg);
        Environment.ExitCode = -1;
        return;
      }

      Contract.Assert(binaries != null);
      Contract.Assert(binaries.Count > 0);

      RunAnalysisInParallel(args);

      if (HasErrors)
      {
        Environment.ExitCode = -2;
        Console.WriteLine(ErrorMessages.SomeAnalysisFailed);
        return;
      }
    }

    private static void RunAnalysisInParallel(string[] args)
    {
      var exe = args[0];
      var cmd = new string[args.Length - 1];
      for (var i = 1; i < args.Length; i++)
      {
        cmd[i - 1] = args[i];
      }

      Parallel.ForEach(binaries, binary => RunAnalysis(null, exe, cmd, binary));
    }


    private static void RunAnalysis(object threadContext, string exe, string[] args, string binary)
    {
      var start = DateTime.Now;

      var processStartInfo = new ProcessStartInfo();
      processStartInfo.FileName = exe;
      processStartInfo.Arguments = String.Join(" ", String.Join(" ", args), binary);
      processStartInfo.UseShellExecute = false;
      processStartInfo.RedirectStandardOutput = true;
      processStartInfo.RedirectStandardError = true;

      Console.WriteLine("Starting the analysis of {0}, Command line : {1}", binary, processStartInfo.Arguments);

      using (var process = Process.Start(processStartInfo))
      {
        using (var stdio = process.StandardOutput)
        {
          var result = stdio.ReadToEnd();
          Console.WriteLine(result);
        }
        using (var stderr = process.StandardError)
        {
          var result = stderr.ReadToEnd();
          if (!String.IsNullOrEmpty(result))
          {
            Console.WriteLine("*** Error in the analysis of " + binary);
            Console.WriteLine(result);
            HasErrors = true;
          }
        }
      }

      Console.WriteLine("Done analysis of {0}. Elapsed {1}", binary, DateTime.Now - start);
    }

    private static List<string> GatherBinaries()
    {
      return Directory.EnumerateFiles(".", "*.dll").ToList();
    }

    private static bool CheckOptionsAndInitializeVars(string[] args, out string errroMsg)
    {
      if (args.Length == 0)
      {
        errroMsg = ErrorMessages.Usage;
        return false;
      }

      if (!File.Exists(args[0]))
      {
        errroMsg = ErrorMessages.MissingClousot;
        return false;
      }

      binaries = GatherBinaries();

      if (binaries.Count == 0)
      {
        errroMsg = ErrorMessages.NoBinariesToAnalyze;
        return false;
      }

      errroMsg = null;
      return true;
    }

    static class ErrorMessages
    {
      public const string Usage = @"AnalyzeFramework <clousotexe> <params>";
      public const string MissingClousot = @"Can't find Clousot executable";
      public const string NoBinariesToAnalyze = @"No binary to analyze in the current directory";
      public const string SomeAnalysisFailed = @"Some analysis failed";
    }
  }
}
