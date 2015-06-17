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
using ClousotTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
  public class Options
  {
    private const string RelativeRoot = @"..\..\..\";
    private const string TestHarnessDirectory = @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug";
    private static readonly string RootDirectory;

    static Options()
    {
      RootDirectory = Path.GetFullPath(RelativeRoot);
    }

    private readonly string OutDirectory;
    public readonly string SourceFile;
    private readonly string compilerCode;
    private readonly string compilerOptions;
    public string ClousotOptions;
    public readonly List<string> LibPaths;
    public readonly List<string> References;
    public readonly bool UseContractReferenceAssemblies = true;
    public string BuildFramework = "v3.5";
    public string ContractFramework = "v3.5";
    public bool UseBinDir = false;
    public bool UseExe = false;
    public readonly string TestGroupName;
    public bool SkipForCCI2;
    public bool SkipSlicing;
    public bool GenerateUniqueOutputName = false;
    public bool Fast = false;

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
    bool IsV4Contracts { get { return this.ContractFramework.Contains("v4"); } }
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

    public string ContractMoniker
    {
      get
      {
        if (IsSilverlight)
        {
          if (IsV4Contracts)
          {
            return "SILVERLIGHT_4_0_CONTRACTS";
          }
          else
          {
            return "SILVERLIGHT_3_0_CONTRACTS";
          }
        }
        else
        {
          if (IsV4Contracts)
          {
            return "NETFRAMEWORK_4_0_CONTRACTS";
          }
          else
          {
            return "NETFRAMEWORK_3_5_CONTRACTS";
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
            return String.Format("/noconfig /nostdlib /define:\"DEBUG=-1,{0},CONTRACTS_FULL\",_MyType=\\\"Console\\\" " +
                                 "/imports:Microsoft.VisualBasic,System,System.Collections,System.Collections.Generic,System.Data,System.Diagnostics,System.Linq,System.Xml.Linq " +
                                 "/optioncompare:Binary /optionexplicit+ /optionstrict:custom /optioninfer+ {1}",
                                 Moniker,
                                 MakeAbsolute(@"Foxtrot\Contracts\ContractExtensions.vb")
                                 );
          default:
            if (IsV4 && !UseContractReferenceAssemblies)
            {
              // work around a bug in mscorlib.dll which has warnings when we extract contracts from it
              return String.Format("/noconfig /nostdlib /d:CONTRACTS_FULL;DEBUG;{0};{1} {2} {3}", Moniker, ContractMoniker,
                MakeAbsolute(@"Microsoft.Research\RegressionTest\ClousotTests\NoWarn.cs"),
                MakeAbsolute(@"Foxtrot\Contracts\ContractExtensions.cs")
                );
            }
            else
            {
              return String.Format("/noconfig /nostdlib /d:CONTRACTS_FULL;DEBUG;{0};{1} {2}", Moniker, ContractMoniker,
                MakeAbsolute(@"Foxtrot\Contracts\ContractExtensions.cs")
                );
            }
        }
      }
    }

    public string CompilerOptions(List<string> resolvedRefs)
    {
      if (compilerCode == "VB")
      {
        string mscorlib = null;
        foreach (var p in resolvedRefs)
        {
          if (p.EndsWith("mscorlib.dll")) { mscorlib = Path.GetDirectoryName(p); break; }
        }
        if (mscorlib != null)
        {
          return String.Format("/sdkpath:\"{0}\" ", mscorlib) + DefaultCompilerOptions + " " + compilerOptions;
        }
      }
      return DefaultCompilerOptions + " " + compilerOptions;
    }

    private static Dictionary<string, GroupInfo> groupInfo = new Dictionary<string, GroupInfo>();
    private int instance;
    public int Instance { get { return this.instance; } }
    public readonly GroupInfo Group;

    public Options(string testGroupName, TestContext context)
    {
      var dataRow = context.DataRow;
      this.OutDirectory = context.TestDeploymentDir;
      this.TestGroupName = testGroupName;
      this.Group = GetTestGroup(testGroupName, RootDirectory, out this.instance);
      this.SourceFile = LoadString(dataRow, "Name");
      this.ClousotOptions = LoadString(dataRow, "Options");
      this.UseContractReferenceAssemblies = LoadBool(dataRow, "ContractReferenceAssemblies", false);
      this.UseExe = LoadBool(dataRow, "Exe", false);
      this.compilerOptions = LoadString(dataRow, "CompilerOptions");
      this.References = LoadList(dataRow, "References", "mscorlib.dll", "System.dll", "ClousotTestHarness.dll");
      this.LibPaths = LoadList(dataRow, "LibPaths", MakeAbsolute(TestHarnessDirectory));
      this.compilerCode = LoadString(dataRow, "Compiler", "CS");
      this.SkipForCCI2 = LoadBool(dataRow, "SkipCCI2", false);
      this.SkipSlicing = LoadBool(dataRow, "SkipSlicing", false);
    }

    private GroupInfo GetTestGroup(string testGroupName, string rootDir, out int instance)
    {
      if (testGroupName == null)
      {
        instance = 0;
        return new GroupInfo(null, rootDir);
      }
      GroupInfo result;
      if (groupInfo.TryGetValue(testGroupName, out result))
      {
        result.Increment(out instance);
        return result;
      }
      instance = 0;
      result = new GroupInfo(testGroupName, rootDir);
      groupInfo.Add(testGroupName, result);
      return result;
    }

    private static string LoadString(System.Data.DataRow dataRow, string name, string defaultValue = "")
    {
      if (!ColumnExists(dataRow, name))
        return defaultValue;
      var result = dataRow[name] as string;
      if (String.IsNullOrEmpty(result))
        return defaultValue;
      return result;
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

    private static bool LoadBool(System.Data.DataRow dataRow, string name, bool defaultValue)
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

    /// <summary>
    /// Not only makes the exe absolute but also tries to find it in the deployment dir to make code coverage work.
    /// </summary>
    public string GetFullExecutablePath(string relativePath)
    {
      var deployed = Path.Combine(this.OutDirectory, Path.GetFileName(relativePath));
      if (File.Exists(deployed))
      {
        return deployed;
      }
      return MakeAbsolute(relativePath);
    }

    public string MakeAbsolute(string relativeToRoot)
    {
      return Path.Combine(RootDirectory, relativeToRoot); // MB: do not need Path.GetFullPath because RootDirectory is already an absolute path
    }

    public string TestName
    {
      get
      {
        var instance = this.Instance;
        if (SourceFile != null) { return Path.GetFileNameWithoutExtension(SourceFile) + "_" + instance; }
        else return instance.ToString();
      }
    }

    public int TestInstance { get { return this.Instance; } }

    public bool Skip
    {
      get
      {
        if (!System.Diagnostics.Debugger.IsAttached) return false;
        // use only the previously failed file indices
        return !Group.Selected;
      }
    }

    public object Framework
    {
      get
      {
        if (this.BuildFramework.EndsWith("v3.5"))
        {
          return "v3.5";
        }
        if (this.BuildFramework.EndsWith("v4.0"))
        {
          return "v4.0";
        }
        if (this.BuildFramework.EndsWith("v4.5"))
        {
          return "v4.5";
        }
        else
        {
          return "none";
        }
      }
    }
  }
}
