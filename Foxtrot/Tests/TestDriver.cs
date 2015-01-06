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
    public const string ReferenceDirRoot = @"Microsoft.Research\Imported\ReferenceAssemblies\";
    public const string ContractReferenceDirRoot = @"Microsoft.Research\Contracts\bin\Debug\";
    public const string FoxtrotExe = @"Foxtrot\Driver\bin\debug\foxtrot.exe";
    public const string ToolsRoot = @"Microsoft.Research\Imported\Tools\";


    internal static object Rewrite(string absoluteSourceDir, string absoluteBinary, Options options, bool alwaysCapture = false)
    {
      string referencedir;
      if (Path.IsPathRooted(options.ReferencesFramework))
      {
        referencedir = options.ReferencesFramework;
      }
      else
      {
        referencedir = options.MakeAbsolute(Path.Combine(ReferenceDirRoot, options.ReferencesFramework));
      }
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
      var optionString = String.Format("/out:{0} -nobox -libpaths:{3} {4} {1} {2}", targetfile, options.FoxtrotOptions, absoluteSource, referencedir, libPathsString);
      if (absoluteBinary.EndsWith("mscorlib.dll"))
        optionString = String.Format("/platform:{0} {1}", absoluteBinary, optionString);
      var capture = RunProcessAndCapture(binDir, options.GetFullExecutablePath(FoxtrotExe), optionString, options.TestName);
      if (capture.ExitCode != 0)
      {
        if (options.MustSucceed)
        {
          Assert.AreEqual(0, capture.ExitCode, "{0} returned an errorcode of {1}.", FoxtrotExe, capture.ExitCode);
        }
        return capture;
      }
      else
      {
        if (alwaysCapture) return capture;

        if (options.UseBinDir)
        {
          var fileName = Path.Combine(absoluteSourcedir, targetName);
          if (File.Exists(fileName))
          {
            try
            {
              File.SetAttributes(fileName, FileAttributes.Normal);
            }
            catch { }
          }
          File.Copy(targetfile, fileName, true);
          return fileName;
        }
      }
      return targetfile;
    }

    private static int PEVerify(string assemblyFile, Options options)
    {
      var peVerifyPath = options.MakeAbsolute(Path.Combine(ToolsRoot, String.Format(@"{0}\peverify.exe", options.BuildFramework)));
      var path = Path.GetDirectoryName(assemblyFile);
      var file = Path.GetFileName(assemblyFile);
      if (file == "mscorlib.dll") return -1; // peverify returns 0 for mscorlib without verifying.

      var exitCode = RunProcess(path, peVerifyPath, "/unique \"" + file + "\"", true);
      return exitCode;
    }

    internal static string RewriteAndVerify(string sourceDir, string binary, Options options)
    {
      if (!Path.IsPathRooted(sourceDir)) { sourceDir = options.MakeAbsolute(sourceDir); }
      if (!Path.IsPathRooted(binary)) { binary = options.MakeAbsolute(binary); }
      string target = Rewrite(sourceDir, binary, options) as string;
      if (target != null)
      {
        PEVerify(target, options);
      }
      return target;
    }

    private static int RunProcess(string cwd, string tool, string arguments, bool mustSucceed, string writeBatchFile = null)
    {
      var capture = RunProcessAndCapture(cwd, tool, arguments, writeBatchFile);
      if (mustSucceed && capture.ExitCode != 0)
      {
        Assert.AreEqual(0, capture.ExitCode, "{0} returned an errorcode of {1}.", tool, capture.ExitCode);
      }
      return capture.ExitCode;

    }

    private static OutputCapture RunProcessAndCapture(string cwd, string tool, string arguments, string writeBatchFile = null)
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
        file.WriteLine("\"{0}\" {1} %1 %2 %3 %4 %5", i.FileName, i.Arguments);
        file.Close();
      }
      var capture = new OutputCapture();
      using (Process p = Process.Start(i))
      {
        p.OutputDataReceived += new DataReceivedEventHandler(capture.OutputDataReceived);
        p.ErrorDataReceived += new DataReceivedEventHandler(capture.ErrorDataReceived);
        p.BeginOutputReadLine();
        p.BeginErrorReadLine();

        Assert.IsTrue(p.WaitForExit(200000), String.Format("{0} timed out", i.FileName));
        capture.ExitCode = p.ExitCode;
        return capture;
      }
    }

    public static int Run(string targetExe)
    {
      return RunProcess(Environment.CurrentDirectory, targetExe, "", true);
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
      var contractreferencedir = options.MakeAbsolute(Path.Combine(ContractReferenceDirRoot, options.ReferencesFramework));
      var sourcedir = absoluteSourceDir = Path.GetDirectoryName(sourceFile);
      var outputdir = Path.Combine(Path.Combine(sourcedir, "bin"), options.BuildFramework);
      var extension = options.UseExe ? ".exe" : ".dll";
      var targetKind = options.UseExe ? "exe" : "library";
      var targetfile = Path.Combine(outputdir, options.TestName + extension);
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
        var exitCode = RunProcess(sourcedir, compilerpath, String.Format("/debug /t:{4} /out:{0} {3} {2} {1}", targetfile, sourceFile, referenceString, options.CompilerOptions, targetKind), true);
        if (exitCode != 0)
        {
          return null;
        }
        // add reference to Microsoft.Contracts.dll if not already done because of transitive closure issues
        if (!options.References.Contains("Microsoft.Contracts.dll"))
        {
            options.References.Add("Microsoft.Contracts.dll");
            // recompute resolvedReferences
            resolvedReferences = ResolveReferences(options);
        }
        
        CopyReferenceAssemblies(resolvedReferences, outputdir);
        return targetfile;
      }
      finally
      {
        Environment.CurrentDirectory = oldCwd;
      }
    }

    public class OutputCapture
    {
      public int ExitCode { get; set; }

      public readonly List<string> errOut = new List<string>();
      public readonly List<string> stdOut = new List<string>();

      internal void ErrorDataReceived(object sender, DataReceivedEventArgs e)
      {
        if (e.Data == null) return;
        this.errOut.Add(e.Data);
        Console.WriteLine("{0}", e.Data);
      }

      internal void OutputDataReceived(object sender, DataReceivedEventArgs e)
      {
        if (e.Data == null) return;
        this.stdOut.Add(e.Data);
        Console.WriteLine("{0}", e.Data);
      }
    }

    private static void CopyReferenceAssemblies(List<string> resolvedReferences, string outputdir)
    {
      foreach (var r in resolvedReferences)
      {
        try
        {
          var fileName = Path.Combine(outputdir, Path.GetFileName(r));
          if (File.Exists(fileName))
          {
            try
            {
              File.SetAttributes(fileName, FileAttributes.Normal);
            }
            catch { }
          }
          File.Copy(r, fileName, true);
        }
        catch { }
      }
    }
    private static List<string> ResolveReferences(Options options)
    {
      var result = new List<string>();
      foreach (var r in options.References)
      {
        var found = FindReference(options, result, r, options.ReferencesFramework);
        if (!found && options.ReferencesFramework != options.ContractFramework)
        {
          // try to find it in the contract framework
          FindReference(options, result, r, options.ContractFramework);
        }
      }
      return result;
    }

    private static bool FindReference(Options options, List<string> result, string filename, string framework)
    {
      foreach (var root in System.Linq.Enumerable.Concat(options.LibPaths, new[] { ReferenceDirRoot, ContractReferenceDirRoot }))
      {
        var dir = options.MakeAbsolute(Path.Combine(root, framework));

        var path = Path.Combine(dir, filename);
        if (File.Exists(path))
        {
          result.Add(path);
          return true;
        }
      }
      return false;
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

    internal static void BuildExtractExpectFailureOrWarnings(Options options)
    {
      string absoluteSourceDir;
      var target = Build(options, out absoluteSourceDir);
      if (target != null)
      {
        RewriteAndExpectFailureOrWarnings(absoluteSourceDir, target, options);
      }
    }

    internal static void RewriteAndExpectFailureOrWarnings(string sourceDir, string binary, Options options)
    {
      var capture = Rewrite(sourceDir, binary, options, true) as OutputCapture;

      CompareExpectedOutput(options, capture);
    }

    private static void CompareExpectedOutput(Options options, OutputCapture capture)
    {
      var expected = File.ReadAllLines(options.MakeAbsolute(@"Foxtrot\Tests\CheckerTests\" + Path.GetFileNameWithoutExtension(options.SourceFile)+ ".Expected"));
      var expectedIndex = 0;
      var absoluteFile = options.MakeAbsolute(options.SourceFile);
      for (int i = 0; i < capture.stdOut.Count; i++)
      {
        var actual = capture.stdOut[i];

        if (actual.StartsWith("Trace:")) continue;
        if (actual.StartsWith("elapsed time:")) continue;
        if (expectedIndex >= expected.Length)
        {
          Assert.AreEqual(null, actual);
        }
        var expectedLine = expected[expectedIndex++];

        if (actual.StartsWith(absoluteFile, StringComparison.InvariantCultureIgnoreCase))
        {
          actual = TrimFilePath(actual, options.SourceFile);
          expectedLine = TrimFilePath(expectedLine, options.SourceFile);
        }
        Assert.AreEqual(expectedLine, actual);
      }
      if (expectedIndex < expected.Length)
      {
        Assert.AreEqual(expected[expectedIndex], null);
      }
    }

    private static string TrimFilePath(string actual, string sourceFile)
    {
      var i = actual.IndexOf(sourceFile, StringComparison.InvariantCultureIgnoreCase);
      if (i > 0) { return actual.Substring(i); }
      return actual;
    }

    internal static void RewriteBinary(Options options, string sourceDir)
    {
      var absoluteDir = options.MakeAbsolute(sourceDir);
      var binary = Path.Combine(absoluteDir, options.SourceFile);
      Rewrite(absoluteDir, binary, options);
    }
  }

  class Options
  {
    const string RelativeRoot = @"..\..\..\";
    const string TestHarnessDirectory = @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug";

    public readonly string RootDirectory;
    public readonly string SourceFile;
    private readonly string compilerCode;
    private readonly string compilerOptions;
    public string FoxtrotOptions;
    private List<string> libPaths;
    public readonly List<string> References;
    public bool UseContractReferenceAssemblies;
    public string BuildFramework = "v3.5";
    public string ReferencesFramework {
      get {
        if (referencesFramework == null) { return BuildFramework; }
        return referencesFramework;
      }
      set {
        this.referencesFramework = value;
      }
    }
    private string referencesFramework;
    public string ContractFramework = "v3.5";
    public bool UseBinDir = true;
    public bool UseExe = true;
    private int instance;
    private TestContext TestContext;

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
        if (SourceFile != null) { return Path.GetFileNameWithoutExtension(SourceFile) + "_" + instance; }
        else return instance.ToString();
      }
    }

    public bool IsRoslyn { get; set; }

    public string Compiler
    {
      get
      {
        switch (compilerCode)
        {
          case "VB":
            if (IsRoslyn)
            {
              return "rvbc.exe";
            }
            else
            {
              return "vbc.exe";
            }
          default:
            if (IsRoslyn)
            {
              return "rcsc.exe";
            }
            else
            {
              return "csc.exe";
            }
        }
      }
    }

    bool IsV40 { get { return this.BuildFramework.Contains("v4.0"); } }
    bool IsV45 { get { return this.BuildFramework.Contains("v4.5"); } }
    bool IsSilverlight { get { return this.BuildFramework.Contains("Silverlight"); } }

    string Moniker
    {
      get
      {
        if (!IsRoslyn) { return FrameworkMoniker; }
        if (compilerCode == "VB")
        {
          return FrameworkMoniker + ",ROSLYN";
        }
        return FrameworkMoniker + ";ROSLYN";
      }
    }
    string FrameworkMoniker
    {
      get
      {
        if (IsSilverlight)
        {
          if (IsV40)
          {
            return "SILVERLIGHT_4_0";
          }
          if (IsV45)
          {
            return "SILVERLIGHT_4_5";
          }
          {
            return "SILVERLIGHT_3_0";
          }
        }
        else
        {
          if (IsV40)
          {
            return "NETFRAMEWORK_4_0";
          }
          if (IsV45)
          {
            return "NETFRAMEWORK_4_5";
          }
          {
            return "NETFRAMEWORK_3_5";
          }
        }
      }
    }

    public bool UseTestHarness { get; set; }

    private string DefaultCompilerOptions
    {
      get
      {
        switch (compilerCode)
        {
          case "VB":
            return String.Format("/noconfig /nostdlib /define:\"DEBUG=-1,{0},CONTRACTS_FULL\",_MyType=\\\"Console\\\" " +
                                 "/imports:Microsoft.VisualBasic,System,System.Collections,System.Collections.Generic,System.Data,System.Diagnostics,System.Linq,System.Xml.Linq " +
                                 "/optioncompare:Binary /optionexplicit+ /optionstrict+ /optioninfer+ ",
                                 Moniker);
          default:
            if (this.UseTestHarness)
            {
              return String.Format("/d:CONTRACTS_FULL;DEBUG;{0} /noconfig /nostdlib {1}", Moniker, MakeAbsolute(@"Foxtrot\Tests\Sources\TestHarness.cs"));
            }
            else
            {
              return String.Format("/d:CONTRACTS_FULL;DEBUG;{0} /noconfig /nostdlib", Moniker);
            }
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


    private class TestGroup
    {
      int instance;

      private TestGroup() { }
      public int Instance { get { return this.instance; } }
      void Increment() { this.instance++; }
      private static readonly Dictionary<string, TestGroup> testGroups = new Dictionary<string, TestGroup>();

      public static TestGroup Get(string testGroupName)
      {
        TestGroup result;
        if (!testGroups.TryGetValue(testGroupName, out result))
        {
          result = new TestGroup();
          testGroups.Add(testGroupName, result);
        }
        else
        {
          result.Increment();
        }
        return result;
      }
    }


    private readonly TestGroup Group;

    public Options(string testName, string foxtrotOptions, TestContext context)
      : this(context)
    {
      this.SourceFile = testName;
      FoxtrotOptions = foxtrotOptions;
    }

    public Options(TestContext context)
    {
      this.TestContext = context;
      this.Group = TestGroup.Get(context.TestName);

      RootDirectory = Path.GetFullPath(RelativeRoot);
      this.instance = this.Group.Instance;

      var dataRow = context.DataRow;
      if (dataRow != null)
      {
        SourceFile = LoadString(dataRow, "Name");
        FoxtrotOptions = LoadString(dataRow, "FoxtrotOptions");
        UseContractReferenceAssemblies = LoadBool("ContractReferenceAssemblies", dataRow, true);
        compilerOptions = LoadString(dataRow, "CompilerOptions");
        References = LoadList(dataRow, "References", "mscorlib.dll", "System.dll", "ClousotTestHarness.dll");
        libPaths = LoadList(dataRow, "LibPaths", MakeAbsolute(TestHarnessDirectory));
        compilerCode = LoadString("Compiler", dataRow, "CS");
        UseBinDir = LoadBool("BinDir", dataRow, false);
        UseExe = LoadBool("UseExe", dataRow, true);
        MustSucceed = LoadBool("MustSucceed", dataRow, true);
      }
    }


    private static string LoadString(System.Data.DataRow dataRow, string name)
    {
      if (!ColumnExists(dataRow, name)) return "";
      return dataRow[name] as string;
    }

    private static List<string> LoadList(System.Data.DataRow dataRow, string name, params string[] initial)
    {
      var result = new List<string>(initial);
      if (!ColumnExists(dataRow, name)) return result;
      string listdata = dataRow[name] as string;
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

    /// <summary>
    /// Not only makes the exe absolute but also tries to find it in the deployment dir to make code coverage work.
    /// </summary>
    public string GetFullExecutablePath(string relativePath)
    {
      if (this.TestContext != null)
      {
        var deployed = Path.Combine(this.TestContext.DeploymentDirectory, Path.GetFileName(relativePath));
        if (File.Exists(deployed))
        {
          // sanity check
          if (File.Exists(Path.Combine(this.TestContext.DeploymentDirectory, "Foxtrot.Extractor.dll")) &&
              File.Exists(Path.Combine(this.TestContext.DeploymentDirectory, "Microsoft.Contracts.dll")))
          {
            return deployed;
          }
        }
      }
      return MakeAbsolute(relativePath);
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

    public bool MustSucceed { get; set; }
  }

}
