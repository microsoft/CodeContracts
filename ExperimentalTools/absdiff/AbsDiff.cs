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

#define SAMEWINDOW

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Microsoft.Research.AbsDiff
{
  class AbsDiff
  {
    public static string[] ClousotExes = new string[] { "clousot.exe", "cccheck.exe", "clousot2.exe" };
    public const string Platform = @"\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll";
    public const string ClousotDefaultArguments = "-bounds -nonnull -infer requires -infer methodensures -infer objectinvariants -suggest requires -suggest methodensures -suggest objectinvariants -suggest assumes -show progress -cache -cachefilename AbsDiffCache";
 
    public static string ClousotExe;

    public const string PerlScript = @"diffClousotContracts.pl";
    public const string PerlExe = "perl.exe";

    static void Main(string[] args)
    {
      // Parse the options
      var options = new Options(args);
      if (options.Error != null)
      {
        Console.WriteLine(options.Error);
        Environment.Exit(-1);
      }

      // Check the existence of the files
      string error;
      string[] outputFileNames;
      if (!CheckFiles(options, out outputFileNames, out error))
      {
        Console.WriteLine(error);
        Environment.Exit(-1);
      }

      Contract.Assert(outputFileNames.Length == 2);

      // Launch the two analyses
      var success= new bool[2];
      Parallel.For(0, 2,  i => RunProcess(options, outputFileNames, i, success));

      // Now run the diff, if success
      if (success[0] && success[1])
      {
        AbsDiffMain(options, outputFileNames);
      }
    }

    private static void AbsDiffMain(Options options, string[] outputFileNames)
    {
      Contract.Requires(outputFileNames.Length == 2);

      if (options.DiffPre)
      {
        RunAbsDiff(outputFileNames[0], outputFileNames[1], "-pre");
      }
      if (options.DiffPost)
      {
        RunAbsDiff(outputFileNames[0], outputFileNames[1], "-post");
      }
      if (options.DiffInv)
      {
        RunAbsDiff(outputFileNames[0], outputFileNames[1], "-inv");
      }
      if (options.DiffAssume)
      {
        RunAbsDiff(outputFileNames[0], outputFileNames[1], "-assume");
      }
    }

    private static void RunAbsDiff(string firstFile, string secondFile, string cmdSwitch)
    {
      Contract.Requires(!string.IsNullOrEmpty(cmdSwitch));

      var processStartInfo = new ProcessStartInfo();
      processStartInfo.FileName = PerlExe;
      processStartInfo.Arguments = String.Join(" ", PerlScript, firstFile, secondFile, cmdSwitch);
      processStartInfo.UseShellExecute = false;
      processStartInfo.RedirectStandardOutput = true;
      processStartInfo.RedirectStandardError = true;

      Console.WriteLine("Comparing {0}", SwithchToEnglish(cmdSwitch));

      using (var process = Process.Start(processStartInfo))
      {
        using (var stdio = process.StandardOutput)
        {
          var result = stdio.ReadToEnd();
          if (String.IsNullOrWhiteSpace(result))
          {
            Console.WriteLine("no (abs) diff");
          }
          else
          {
            Console.WriteLine(result);
          }
        }
      }
    }

    private static string SwithchToEnglish(string cmdSwitch)
    {
      Contract.Requires(!String.IsNullOrEmpty(cmdSwitch));

      switch (cmdSwitch)
      {
        case "-pre":
          return "preconditions";

        case "-post":
          return "postconditions";

        case "-inv":
          return "invariants";

        case "-assume":
          return "assumptions";

        default:
          return "unknown: " + cmdSwitch;
      }
    }

    private static void RunProcess(Options options, string[] outputFileNames, int i, bool[] success)
    {
      Contract.Requires(options != null);

      var start = DateTime.Now;

      var processStartInfo = new ProcessStartInfo();
      processStartInfo.FileName = ClousotExe;
      processStartInfo.Arguments = CreateClousotCMD(options, i);
#if SAMEWINDOW
      processStartInfo.UseShellExecute = false;
      processStartInfo.RedirectStandardOutput = true;
      processStartInfo.RedirectStandardError = true;
#else
      processStartInfo.Arguments += " > " + outputFileNames[i];
      processStartInfo.UseShellExecute = true;
      processStartInfo.RedirectStandardOutput = false;
      processStartInfo.RedirectStandardError = false;

#endif
      Console.WriteLine("#{0}: Starting the analysis of {1}", i, options.File[i]);
      if (options.Trace)
      {
        Console.WriteLine("     Command line : {0}", processStartInfo.Arguments);
      }
      using (var process = Process.Start(processStartInfo))
      {
#if SAMEWINDOW
        //process.BeginOutputReadLine();
        using (var stdio = process.StandardOutput)
        {
          var result = stdio.ReadToEnd();
          File.AppendAllText(outputFileNames[i], result);
          //Console.WriteLine(result);
          success[i] = true;
        }
        using (var stderr = process.StandardError)
        {
          var result = stderr.ReadToEnd();
          if (!String.IsNullOrEmpty(result))
          {
            Console.WriteLine("*** Error in the analysis of " + options.File[i]);
            Console.WriteLine(result);
            success[i] = false;
          }
        }
#endif
        success[i] = process.ExitCode == 0;
      }
     
      Console.WriteLine("#{0}: Done analysis of {1} (success? {2})", i, options.File[i], success[i]);
      Console.WriteLine("      Result wrote in  {0}. Elapsed {1}", outputFileNames[i], DateTime.Now - start);
    }

    private static string CreateClousotCMD(Options options, int i)
    {
      var platform = Environment.GetEnvironmentVariable("SystemRoot") + Platform;
      var customOptions = String.Join(" ", options.ClousotOptions);
      return String.Join(" ", "-platform", platform, ClousotDefaultArguments, customOptions, options.File[i]);
    }

    static bool CheckFiles(Options options, out string[] OutputFileName, out string errorMsg)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out OutputFileName).Length == 2);
      OutputFileName = new string[2];

      for (var i = 0; i < 2; i++)
      {
        if (!File.Exists(options.File[i]))
        {
          errorMsg = options.File[i] + ErrorMessages.FileNotExists;
          OutputFileName = null;
          return false;
        }
        else
        {
          OutputFileName[i] = options.File[i] + PrettyPrint(File.GetLastWriteTime(options.File[i])) + ".txt";
        }
      }

      if (!ExistsExeInThePath(PerlExe))
      {
        errorMsg = ErrorMessages.MissingPerlEXE;
        return false;
      }

      if (!File.Exists(PerlScript))
      {
        errorMsg = ErrorMessages.MissingPerlScript;
        return false;
      }

      foreach (var exe in ClousotExes)
      {
        var path = options.CCCheckDir + exe;
        if (File.Exists(path))
        {
          ClousotExe = path;
          errorMsg = null;
          return true;
        }
      }
      errorMsg = ErrorMessages.CCCheckNotFound;
      return false;
    }

    public static bool ExistsExeInThePath(string fileName)
    {
      Contract.Requires(fileName != null);

      if (File.Exists(fileName))
        return true;

      var values = Environment.GetEnvironmentVariable("PATH");
      foreach (var path in values.Split(';'))
      {
        Contract.Assume(path != null); // It would be cool if we can propose this assumption
        try
        {
          var fullPath = Path.Combine(path, fileName);
          if (File.Exists(fullPath))
          {
            return true;
          }
        }
        catch (Exception)
        {
         // Path.Combine may fail if the path contain some ill-formed string
          continue;
        }

      }
      return false;
    }

    private static string PrettyPrint(DateTime dateTime)
    {
      return dateTime.Year.ToString() + dateTime.Month.ToString() + dateTime.Day.ToString() + "-" + dateTime.Hour.ToString() + dateTime.Minute.ToString();
    }
  }

  class Options
  {
    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.ClousotOptions != null);
    }


    public readonly string[] File;
    public readonly bool DiffPre, DiffPost, DiffInv, DiffAssume;
    public readonly string Error;
    public string[] ClousotOptions { get { return this.clousotOptions; } }
    private readonly string[] clousotOptions;

    public readonly string CCCheckDir;
    public readonly bool Trace;

    public Options(string[] args)
    {
      Contract.Requires(args != null);

      if (args.Length < 2)
      {
        Error = ErrorMessages.Usage;
        return;
      }

      this.File = new string[2];
      this.File[0] = args[0];
      this.File[1] = args[1];

      if (File[0] == File[1])
      {
        this.Error = ErrorMessages.AreTheSameFile;
        return;
      }

      var clousotOptions = new List<string>();

      for (var i = 2; i < args.Length; i++)
      {
        var option = args[i];
        switch (option.ToLower())
        {
          case "-pre":
          case "/pre":
            this.DiffPre = true; break;
          case "-post":
          case "/post":
            this.DiffPost = true; break;
          case "-inv":
          case "/inv":
            this.DiffInv = true; break;
          case "-assume":
          case "/assume":
            this.DiffAssume = true; break;            
          case "-cccheckdir":
          case "/cccheckdir":
            if (i == args.Length - 1)
            {
              this.Error = ErrorMessages.Usage;
              return;
            }
            else
            {
              this.CCCheckDir = args[++i]; 
            }
            break;
          case "-break":
          case "/break":
            Debugger.Break();
            break;
          case "-trace":
          case "/trace":
            this.Trace = true;
            break;
          default:
            clousotOptions.Add(option);
            break;
        }
      }

      if (!(this.DiffPre || this.DiffPost || this.DiffInv || this.DiffAssume))
      {
        this.Error = ErrorMessages.Usage;
        return;
      }

      this.clousotOptions = clousotOptions.ToArray();
    }
  }

  class ErrorMessages
  {
    public const string Usage = "absdiff prevFile newFile [-pre | -post | -inv | -assume ]* [-cccheckdir <dir>] [cccheckOptions]";
    public const string FileNotExists = " does not exists";
    public const string CCCheckNotFound = " cccheck or clousot.exe should be in the same directory";
    public const string AreTheSameFile = "Nothing to do: The files to absdiff are the same file";
    public const string MissingPerlScript = "Missing Perl script for absdiff. Please put it in the same dir as absdiff.exe";
    public const string MissingPerlEXE = "Cannot find perl.exe. Please install it and/or have it in the %PATH%";
  }
}
