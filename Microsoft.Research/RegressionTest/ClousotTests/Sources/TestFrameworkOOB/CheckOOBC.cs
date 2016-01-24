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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFrameworkOOB
{
  [TestClass]
  public class TestOOBCWellFormedness
  {
    private static void Verify(string assemblyName, string frameworkPath)
    {
      var path = Path.Combine(@"..\..\..\Microsoft.Research\Contracts\bin\debug", frameworkPath);

      string originalPath = Path.Combine(Path.Combine(@"..\..\..\Microsoft.Research\Imported\ReferenceAssemblies", frameworkPath), assemblyName + ".dll");
      if (originalPath == null)
      {
        Assert.Fail("Can't find original assembly {0}", assemblyName);
        return;
      }
      var checker = new CRASanitizer.Checker(Console.Out);

      var contractAssemblyName = assemblyName + ".Contracts.dll";
      Console.WriteLine("Checking {1} {0} for errors...", assemblyName, frameworkPath);


      var errors = checker.CheckSurface(Path.Combine(path, contractAssemblyName), originalPath);

      Assert.AreEqual(0, errors, "Found {0} errors in contract reference assembly {1}", errors, contractAssemblyName);
      Console.WriteLine("... done.");
    }

    private static string FindOriginalAssembly(string assemblyName)
    {
      // probe 2.0 framework, reference assembly directory for v3.0 and v3.5 and Silverlight 2.0
      var frameworkPath = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\Microsoft.Net\Framework\v2.0.50727");
      var refassemPath = GetRefAssemPath();
      if (refassemPath == null)
      {
        refassemPath = "";
      }
      var v30refpath = Path.Combine(refassemPath, "v3.0");
      var v35refpath = Path.Combine(refassemPath, "v3.5");
      var silverlight30 = Path.Combine(refassemPath, @"Silverlight\v3.0");
      var backuppath = @"..\..\..\Foxtrot\Contracts\Silverlight\v3.0\ImportedBinaries";

      return ProbeFor(assemblyName, frameworkPath, v35refpath, v30refpath, silverlight30, backuppath);
    }

    private static string ProbeFor(string assemblyName, params string[] paths)
    {
      foreach (var path in paths)
      {
        var candidate = Path.Combine(path, assemblyName + ".dll");
        if (File.Exists(candidate)) return candidate;
      }
      return null;
    }

    private static string GetRefAssemPath()
    {
      var refPath = Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\Reference Assemblies\Microsoft\Framework");
      if (Directory.Exists(refPath)) return refPath;
      refPath = Environment.ExpandEnvironmentVariables(@"%ProgramFiles(x86)%\Reference Assemblies\Microsoft\Framework");
      if (Directory.Exists(refPath)) return refPath;
      return null;
    }

    [TestMethod]
    public void CheckOOBCAssemblies()
    {
      var cras35 = new[]{ 
        "Microsoft.VisualBasic", 
        "Microsoft.VisualBasic.Compatibility",
        "mscorlib", 
        "System",
        "System.Configuration", 
        "System.Configuration.Install", 
        "System.Core",
        "System.Data",
        "System.Drawing",
        "System.Security",
        "System.Web",
        "System.Windows.Forms",
        "System.Xml",
        "System.Xml.Linq",
        "WindowsBase",
      };
      var silverlight = new[]{ 
        "Microsoft.VisualBasic", 
        "mscorlib",
        "System",
        "System.Core",
        "System.Windows",
        "System.Windows.Browser",
        "System.Xml",
        "System.Xml.Linq",
      };

      VerifyFramework(cras35, @"v3.5");
      VerifyFramework(silverlight, @"Silverlight\v3.0");
      VerifyFramework(silverlight, @"Silverlight\v4.0");
    }

    private static void VerifyFramework(string[] cras, string basePath)
    {
      foreach (var cra in cras)
      {
        Verify(cra, basePath);
      }
    }

    /// <summary>
    /// If something goes wrong with AsmMeta, then it might add a bogus external reference
    /// to the reference assembly. That happened once (without saying whose fault it was
    /// exactly...) and took a long time to track down. So if this test fails, check
    /// AsmMeta to make sure it is doing the right thing.
    /// </summary>
    [TestMethod]
    public void CheckMscorlibReferenceAssemblyForExternalReferences()
    {
      var checker = new CRASanitizer.Checker(Console.Out);

      Assert.AreEqual((uint)0, checker.NumberOfExternalReferences(
        @"..\..\..\Microsoft.Research\Contracts\bin\Debug\v3.5\Mscorlib.Contracts.dll"));
      Console.WriteLine("Checked to make sure mscorlib.Contracts.dll doesn't have any external references");
    }

  }

}
