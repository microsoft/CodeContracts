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

using Microsoft.Research.DataStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CCBaseline
{
  class Options : OptionParsing
  {
    [OptionDescription("Directory of source or wildcard", Required=true)]
    public string repoDir = null;

    [OptionDescription("Project alternatives to build in order of preference", Required = true)]
    public List<string> projects = null;

    [OptionDescription("Starting version to build")]
    public string version = "0";

    [OptionDescription("Whether to save the baseline or use it")]
    public bool save = false;

    protected override bool ParseUnknown(string option, string[] args, ref int index, string optionEqualsArgument)
    {
      if (option == "break")
      {
        System.Diagnostics.Debugger.Launch();
        return true;
      }
      else
      {
        return base.ParseUnknown(option, args, ref index, optionEqualsArgument);
      }
    }

    public void ___Dummy()
    {
      this.version = "0";
      this.repoDir = this.version = null;
      this.projects = null;
      this.save = false;
    }
  }

  class Program
  {
    static int Main(string[] args)
    {
      var options = new Options();
      options.Parse(args);

      if (options.HelpRequested)
      {
        options.PrintOptions("", Console.Out);
        return 0;
      }
      if (options.HasErrors)
      {
        options.PrintErrorsAndExit(Console.Out);
      }

      if (options.repoDir.EndsWith("*"))
      {
        return RunMany(options);
      }
      else
      {
        return RunOne(options);
      }
    }

    static int RunMany(Options options)
    {
      var root = options.repoDir.Substring(0, options.repoDir.Length-1);
      var projects = options.projects;
      var minversion = options.version;
      var dirs = Directory.EnumerateDirectories(root).OrderBy(s => s).ToArray();
      Contract.Assume(Contract.ForAll(dirs, d => d != null));
      for (int i = 0; i < dirs.Length; i++)
      {
        var dir = dirs[i];
        var match = Regex.Match(dir, @"[\\/](\d+)$");
        if (match.Success) {
          // a version number
          var version = match.Groups[1].Value;
          if (string.Compare(version, minversion) < 0) continue;

          Console.WriteLine("CCBaseline: Computing baseline of {0}", dir);
          if (RunOne(dir, projects, options.GeneralArguments, version, save:true) != 0) {
            Console.WriteLine("Computing baseline of {0} failed", dir);
            continue;
          }

          Console.WriteLine("CCHistory: Applying baseline of {0}", dir);
          if (RunOne(dir, projects, options.GeneralArguments, version, save:false) != 0)
          {
            Console.WriteLine("Using the baseline of {0} failed", dir);
            continue;
          }
          // run against prior baseline
          for (int j = 1; j < 3; j++)
          { // run for past 2 versions
            if (i - j < 0) break;
            var pastdir = dirs[i - j];
            var pastmatch = Regex.Match(pastdir, @"[\\/](\d+)$");
            if (pastmatch.Success)
            {
              // a version number
              var priorVersion = pastmatch.Groups[1].Value;
              if (string.Compare(priorVersion, minversion) < 0) break;
              Console.WriteLine("CCBaseline: Using prior baseline {0} for {1}", priorVersion, dir);
              if (RunOne(dir, projects, options.GeneralArguments, priorVersion, save:false) != 0)
              {
                Console.WriteLine("Using the prior baseline {0} for {1} failed", priorVersion, dir);
              }
            }
          }
        }
      }
      return 0;
    }

    static int RunOne(Options options)
    {
      return RunOne(options.repoDir, options.projects, options.GeneralArguments, options.version, options.save);
    }

    static int RunOne(string dir, List<string> projects, List<string> clousotOptions, string version, bool save)
    {
      Contract.Requires(version != null);

      if (!Directory.Exists(dir))
      {
        Console.WriteLine("Directory {0} does not exist", dir);
        return -1;
      }

      string project = null;
      foreach (var projectFile in projects)
      {
        project = Path.Combine(Path.GetFullPath(dir), projectFile);
        if (File.Exists(project)) break;
        project = null;
      }
      if (project == null)
      {
          Console.WriteLine("None of the projects {0} exist", String.Join(" ", projects));
          return -1;
      }
      var outputPrefix = Path.GetFileNameWithoutExtension(project);
      var build = MSBuild.Factory(dir);
      build.SaveSemanticBaseline = save;
      build.ExtraOptions = string.Join(" ", clousotOptions);
      var success = build.Build(new[] { project }, version, outputPrefix);
      if (!success)
      {
        Console.WriteLine("build of {0} failed", dir);
        return -1;
      }
      return 0;
    }
  }
}
