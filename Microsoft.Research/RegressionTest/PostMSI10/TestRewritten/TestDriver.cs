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
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Tests
{
  public class TestDriver
  {
    const string ReferenceDirRoot = @"Microsoft.Research\Imported\ReferenceAssemblies\";
    const string ContractReferenceDirRoot = @"Microsoft.Research\Contracts\bin\Debug\";
    const string FoxtrotExe = @"Foxtrot\Driver\bin\debug\foxtrot.exe";
    const string ToolsRoot = @"Microsoft.Research\Imported\Tools\";

 
    internal static string Rewrite(string absoluteSourceDir, string absoluteBinary, Options options)
    {
      var referencedir = options.MakeAbsolute(Path.Combine(ReferenceDirRoot, options.BuildFramework));
      var contractreferencedir = options.MakeAbsolute(Path.Combine(ContractReferenceDirRoot, options.ContractFramework));
      var absoluteSourcedir = Path.GetDirectoryName(absoluteBinary);
      var absoluteSource = absoluteBinary;
      var libPathsString = FormLibPaths(absoluteSourceDir, contractreferencedir, options);

      var targetName = options.TestName + ".rw" + Path.GetExtension(absoluteBinary);
      var binDir = options.UseBinDir ? Path.Combine(Path.Combine(absoluteSourcedir, "bin"), options.BuildFramework) : absoluteSourcedir;
      var targetfile = Path.Combine(binDir, targetName);
      Console.WriteLine("Rewriting '{0}' to '{1}'", absoluteBinary, targetfile);
      if (!Directory.Exists(binDir))
      {
        Directory.CreateDirectory(binDir);
      }
      var exitCode = RunProcess(binDir, options.MakeAbsolute(FoxtrotExe),
        String.Format("/out:{0} -nobox -libpaths:{3} -libpaths:{4} {5} {1} {2}", targetfile, options.FoxtrotOptions, absoluteSource, referencedir, contractreferencedir, libPathsString),
        options.TestName);
      if (exitCode != 0)
      {
        return null;
      }
      else
      {
        if (options.UseBinDir)
        {
          File.Copy(targetfile, Path.Combine(absoluteSourcedir, targetName), true);
          return Path.Combine(absoluteSourcedir, targetName);
        }
      }
      return targetfile;
    }

    internal static int PEVerify(string assemblyFile, Options options)
    {
      var peVerifyPath = options.MakeAbsolute(Path.Combine(ToolsRoot, String.Format(@"{0}\peverify.exe", options.BuildFramework)));
      assemblyFile = options.MakeAbsolute(assemblyFile);
      var path = Path.GetDirectoryName(assemblyFile);
      var file = Path.GetFileName(assemblyFile);
      if (file == "mscorlib.dll") return -1; // peverify returns 0 for mscorlib without verifying.

      var exitCode = RunProcess(path, peVerifyPath, "/unique \"" + file + "\"", "repro");
      return exitCode;
    }

    internal static string RewriteAndVerify(string sourceDir, string binary, Options options)
    {
      if (!Path.IsPathRooted(sourceDir)) { sourceDir = options.MakeAbsolute(sourceDir); }
      if (!Path.IsPathRooted(binary)) { binary = options.MakeAbsolute(binary); }
      string target = Rewrite(sourceDir, binary, options);
      if (target != null)
      {
        PEVerify(target, options);
      }
      return target;
    }

    private static int RunProcess(string cwd, string tool, string arguments, string writeBatchFile = null)
    {
      ProcessStartInfo i = new ProcessStartInfo(tool, arguments);
      Console.WriteLine("Running '{0}'", i.FileName);
      Console.WriteLine("         {0}", i.Arguments);
      i.RedirectStandardOutput = true;
      i.RedirectStandardError = true;
      i.UseShellExecute = false;
      i.CreateNoWindow = true;
      i.WorkingDirectory = cwd;
      i.ErrorDialog = false;
      if (writeBatchFile != null)
      {
        var file = new StreamWriter(Path.Combine(cwd, writeBatchFile + ".bat"));
        file.WriteLine("{0} {1} %1 %2 %3 %4 %5", i.FileName, i.Arguments);
        file.Close();
      }
      using (Process p = Process.Start(i))
      {
        p.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceived);
        p.ErrorDataReceived += new DataReceivedEventHandler(ErrorDataReceived);
        p.BeginOutputReadLine();
        p.BeginErrorReadLine();

        Assert.IsTrue(p.WaitForExit(200000), String.Format("{0} timed out", i.FileName));
        if (p.ExitCode != 0)
        {
          Assert.AreEqual(0, p.ExitCode, "{0} returned an errorcode of {1}.", i.FileName, p.ExitCode);
        }
        return p.ExitCode;
      }
    }

    public static int Run(string targetExe)
    {
      return RunProcess(Environment.CurrentDirectory, targetExe, "");
    }

    static string FormLibPaths(string absoluteSourceDir, string contractReferenceDir, Options options)
    {
      var oldcwd = Environment.CurrentDirectory;
      try
      {
        Environment.CurrentDirectory = absoluteSourceDir;

        StringBuilder sb = null;
        if (options.UseContractReferenceAssemblies)
        {
          sb = new StringBuilder("/libpaths:").Append(contractReferenceDir);
        }
        if (options.LibPaths == null) return "";    
        foreach (var path in options.LibPaths)
        {
          if (sb == null)
          {
            sb = new StringBuilder("/libpaths:");
          }
          else
          {
            sb.Append(';');
          }
          sb.Append(options.MakeAbsolute(Path.Combine(path, options.ContractFramework)));
        }
        if (sb == null) return "";
        return sb.ToString();
      }
      finally
      {
        Environment.CurrentDirectory = oldcwd;
      }
    }


    internal static string Build(Options options, out string absoluteSourceDir)
    {
      var sourceFile = options.MakeAbsolute(options.SourceFile);
      var compilerpath = options.MakeAbsolute(Path.Combine(ToolsRoot, String.Format(@"{0}\{1}", options.BuildFramework, options.Compiler)));
      var contractreferencedir = options.MakeAbsolute(Path.Combine(ContractReferenceDirRoot, options.BuildFramework));
      var sourcedir = absoluteSourceDir = Path.GetDirectoryName(sourceFile);
      var outputdir = Path.Combine(Path.Combine(sourcedir, "bin"), options.BuildFramework);
      var extension = options.UseExe ? ".exe" : ".dll";
      var targetKind = options.UseExe ? "exe" : "library";
      var targetfile = Path.Combine(outputdir, Path.GetFileNameWithoutExtension(sourceFile) + extension);
      // add Microsoft.Contracts reference if needed
      if (!options.BuildFramework.Contains("v4."))
      {
        options.References.Add("Microsoft.Contracts.dll");
      }
      var oldCwd = Environment.CurrentDirectory;
      try
      {
        Environment.CurrentDirectory = sourcedir;

        var resolvedReferences = ResolveReferences(options);
        var referenceString = ReferenceOptions(resolvedReferences);
        if (!Directory.Exists(outputdir))
        {
          Directory.CreateDirectory(outputdir);
        }
        var exitCode = RunProcess(sourcedir, compilerpath, String.Format("/debug /t:{4} /out:{0} {3} {2} {1}", targetfile, sourceFile, referenceString, options.CompilerOptions, targetKind));
        if (exitCode != 0)
        {
          return null;
        }
        CopyReferenceAssemblies(resolvedReferences, outputdir);
        return targetfile;
      }
      finally
      {
        Environment.CurrentDirectory = oldCwd;
      }
    }
    static void ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
      Console.WriteLine("{0}", e.Data);
    }

    static void OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
      Console.WriteLine("{0}", e.Data);
    }

    private static void CopyReferenceAssemblies(List<string> resolvedReferences, string outputdir)
    {
      foreach (var r in resolvedReferences)
      {
        try
        {
          File.Copy(r, Path.Combine(outputdir, Path.GetFileName(r)), true);
        }
        catch { }
      }
    }
    private static List<string> ResolveReferences(Options options)
    {
      var result = new List<string>();
      foreach (var r in options.References)
      {
        foreach (var root in options.LibPaths)
        {
          var dir = options.MakeAbsolute(Path.Combine(root, options.BuildFramework));

          var path = Path.Combine(dir, r);
          if (File.Exists(path))
          {
            result.Add(path);
            break;
          }
        }
        foreach (var root in new[] { ReferenceDirRoot, ContractReferenceDirRoot })
        {
          var dir = options.MakeAbsolute(Path.Combine(root, options.BuildFramework));

          var path = Path.Combine(dir, r);
          if (File.Exists(path))
          {
            result.Add(path);
            break;
          }
        }
      }
      return result;        
    }

    private static string ReferenceOptions(List<string> references) 
    {
      var sb = new StringBuilder();
      foreach (var r in references)
      {
        sb.Append(String.Format(@"/r:{0} ", r));
      }
      return sb.ToString();
    }

    internal static void BuildRewriteRun(Options options)
    {
      string absoluteSourceDir;
      var target = Build(options, out absoluteSourceDir);
      if (target != null)
      {
        var rwtarget = RewriteAndVerify(absoluteSourceDir, target, options);
        if (rwtarget != null)
        {
          Run(rwtarget);
        }
      }
    }


  }

  class Options
  {
    const string RelativeRoot = @"..\..\..\..\..\..";
    const string TestHarnessDirectory = @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug";

    public readonly string RootDirectory;
    public readonly string SourceFile;
    private readonly string compilerCode;
    private readonly string compilerOptions;
    public string FoxtrotOptions;
    private List<string> libPaths;
    public readonly List<string> References;
    public readonly bool UseContractReferenceAssemblies = true;
    public string BuildFramework = @".NETFramework\v4.0";
    public string ContractFramework = @".NETFramework\v4.0";
    public bool UseBinDir = true;
    public bool UseExe = true;
    private int instance;

    public List<string> LibPaths
    {
      get
      {
        if (libPaths == null) { libPaths = new List<string>(); }
        return libPaths;
      }
    }
    public string TestName
    {
      get
      {
        if (SourceFile != null) { return Path.GetFileNameWithoutExtension(SourceFile) + instance; }
        else return instance.ToString();
      }
    }

    public string Compiler
    {
      get
      {
        switch (compilerCode)
        {
          case "VB": return "vbc.exe";
          default: return "csc.exe";
        }
      }
    }

    bool IsV4 { get { return this.BuildFramework.Contains("v4"); } }
    bool IsSilverlight { get { return this.BuildFramework.Contains("Silverlight"); } }
    string Moniker
    {
      get
      {
        if (IsSilverlight)
        {
          if (IsV4)
          {
            return "SILVERLIGHT_4_0";
          }
          else
          {
            return "SILVERLIGHT_3_0";
          }
        }
        else
        {
          if (IsV4)
          {
            return "NETFRAMEWORK_4_0";
          }
          else
          {
            return "NETFRAMEWORK_3_5";
          }
        }
      }
    }
    private string DefaultCompilerOptions
    {
      get
      {
        switch (compilerCode)
        {
          case "VB":
            return String.Format("/define:\"DEBUG=-1,{0},CONTRACTS_FULL\",_MyType=\\\"Console\\\" " +
                                 "/imports:Microsoft.VisualBasic,System,System.Collections,System.Collections.Generic,System.Data,System.Diagnostics,System.Linq,System.Xml.Linq " +
                                 "/optioncompare:Binary /optionexplicit+ /optionstrict+ /optioninfer+ ",
                                 Moniker);
          default:
            return String.Format("/d:CONTRACTS_FULL;DEBUG;{0} {1}", Moniker, MakeAbsolute(@"Foxtrot\Tests\Sources\TestHarness.cs"));
        }
      }
    }

    public string CompilerOptions
    {
      get
      {
        return DefaultCompilerOptions + " " + compilerOptions;
      }
    }

    private Options(int instance)
    {
      RootDirectory = Path.GetFullPath(RelativeRoot);
      this.instance = instance;
    }

    public Options(string testName, int instance, string foxtrotOptions)
      : this(instance)
    {
      this.SourceFile = testName;
      FoxtrotOptions = foxtrotOptions;
    }

    public Options(int instance, System.Data.DataRow dataRow)
      : this(instance)
    {
      SourceFile = LoadString(dataRow, "Name");
      FoxtrotOptions = LoadString(dataRow, "FoxtrotOptions");
      UseContractReferenceAssemblies = LoadBool("ContractReferenceAssemblies", dataRow, false);
      compilerOptions = LoadString(dataRow, "CompilerOptions");
      References = LoadList(dataRow, "References", "System.dll", "ClousotTestHarness.dll");
      libPaths = LoadList(dataRow, "LibPaths", MakeAbsolute(TestHarnessDirectory));
      compilerCode = LoadString("Compiler", dataRow, "CS");
      UseBinDir = LoadBool("BinDir", dataRow, false);
    }

    private static string LoadString(System.Data.DataRow dataRow, string name)
    {
      if (!ColumnExists(dataRow, name)) return "";
      return (string)dataRow[name];
    }

    private static List<string> LoadList(System.Data.DataRow dataRow, string name, params string[] initial)
    {
      if (!ColumnExists(dataRow, name)) return new List<string>();
      string listdata = dataRow[name] as string;
      var result = new List<string>(initial);
      if (!string.IsNullOrEmpty(listdata))
      {
        result.AddRange(listdata.Split(';'));
      }
      return result;
    }

    private static bool ColumnExists(System.Data.DataRow dataRow, string name)
    {
      return dataRow.Table.Columns.IndexOf(name) >= 0;
    }

    private bool LoadBool(string name, System.Data.DataRow dataRow, bool defaultValue)
    {
      if (!ColumnExists(dataRow, name)) return defaultValue;
      var booloption = dataRow[name] as string;
      if (!string.IsNullOrEmpty(booloption))
      {
        bool result;
        if (bool.TryParse(booloption, out result))
        {
          return result;
        }
      }
      return defaultValue;
    }

    private string LoadString(string name, System.Data.DataRow dataRow, string defaultValue)
    {
      if (!ColumnExists(dataRow, name)) return defaultValue;
      var option = dataRow[name] as string;
      if (!string.IsNullOrEmpty(option))
      {
        return option;
      }
      return defaultValue;
    }

    public string MakeAbsolute(string relativeToRoot)
    {
      return Path.GetFullPath(Path.Combine(this.RootDirectory, relativeToRoot));
    }

    internal void Delete(string fileName)
    {
      var absolute = MakeAbsolute(fileName);
      if (File.Exists(absolute))
      {
        File.Delete(absolute);
      }
    }
  }

}
