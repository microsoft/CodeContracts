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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml.Linq;

namespace Tests
{
  [TestClass]
  public partial class Test
  {
    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

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

    // [TestMethod] no longer needed as we don't use the 2008 solution anymore.
    public void CheckOOBCProjectSources()
    {
      var v9Sources = new[]{
        @"Microsoft.Research\Contracts\Microsoft.VisualBasic.Compatibility\Microsoft.VisualBasic.Compatibility.csproj",
        @"Microsoft.Research\Contracts\Microsoft.VisualBasic.Contracts\Microsoft.VisualBasic.Contracts.csproj",
        @"Microsoft.Research\Contracts\mscorlib\mscorlib.Contracts.csproj",
        @"Microsoft.Research\Contracts\System\System.Contracts.csproj",
        @"Microsoft.Research\Contracts\System.Configuration\System.Configuration.csproj",
        @"Microsoft.Research\Contracts\System.Configuration.Install\System.Configuration.Install.csproj",
        @"Microsoft.Research\Contracts\System.Core.Contracts\System.Core.Contracts.csproj",
        @"Microsoft.Research\Contracts\System.Data\System.Data\System.Data.csproj",
        @"Microsoft.Research\Contracts\System.Drawing\System.Drawing.csproj",
        @"Microsoft.Research\Contracts\System.Security\System.Security.csproj",
        @"Microsoft.Research\Contracts\System.Web\System.Web.csproj",
        @"Microsoft.Research\Contracts\System.Windows\System.Windows.csproj",
        @"Microsoft.Research\Contracts\System.Windows.Browser\System.Windows.Browser.csproj",
        @"Microsoft.Research\Contracts\System.Windows.Forms\Windows.Forms\System.Windows.Forms.csproj",
        @"Microsoft.Research\Contracts\System.Xml\System.Xml.csproj",
        @"Microsoft.Research\Contracts\System.Xml.Linq\System.Xml.Linq.csproj",
        @"Microsoft.Research\Contracts\WindowsBase\WindowsBase.csproj",
      };
      var v10Sources = new[]{
        @"Microsoft.Research\Contracts\Microsoft.VisualBasic.Compatibility\Microsoft.VisualBasic.Compatibility10.csproj",
        @"Microsoft.Research\Contracts\Microsoft.VisualBasic.Contracts\Microsoft.VisualBasic.Contracts10.csproj",
        @"Microsoft.Research\Contracts\mscorlib\mscorlib.Contracts10.csproj",
        @"Microsoft.Research\Contracts\System\System.Contracts10.csproj",
        @"Microsoft.Research\Contracts\System.Configuration\System.Configuration10.csproj",
        @"Microsoft.Research\Contracts\System.Configuration.Install\System.Configuration.Install10.csproj",
        @"Microsoft.Research\Contracts\System.Core.Contracts\System.Core.Contracts10.csproj",
        @"Microsoft.Research\Contracts\System.Data\System.Data\System.Data10.csproj",
        @"Microsoft.Research\Contracts\System.Drawing\System.Drawing10.csproj",
        @"Microsoft.Research\Contracts\System.Security\System.Security10.csproj",
        @"Microsoft.Research\Contracts\System.Web\System.Web10.csproj",
        @"Microsoft.Research\Contracts\System.Windows\System.Windows10.csproj",
        @"Microsoft.Research\Contracts\System.Windows.Browser\System.Windows.Browser10.csproj",
        @"Microsoft.Research\Contracts\System.Windows.Forms\Windows.Forms\System.Windows.Forms10.csproj",
        @"Microsoft.Research\Contracts\System.Xml\System.Xml10.csproj",
        @"Microsoft.Research\Contracts\System.Xml.Linq\System.Xml.Linq10.csproj",
        @"Microsoft.Research\Contracts\WindowsBase\WindowsBase10.csproj",
        @"Microsoft.Research\Contracts\System.Numerics\System.Numerics.csproj"
      };

      for (int i = 0; i < v9Sources.Length; i++)
      {
        var opt = new Options(TestContext);
        CheckSourceFiles(opt.MakeAbsolute(v9Sources[i]), opt.MakeAbsolute(v10Sources[i]));
      }
    }

    private void CheckSourceFiles(string v9proj, string v10proj)
    {
      XElement v9 = XElement.Load(v9proj);
      XElement v10 = XElement.Load(v10proj);

      IEnumerable<string> v9Sources =
        from elem in v9.Descendants()
        where elem.Name.LocalName == "Compile"
        select (string)elem.Attribute("Include");
      IEnumerable<string> v10Sources =
        from elem in v10.Descendants()
        where elem.Name.LocalName == "Compile"
        select (string)elem.Attribute("Include");

      var onlyInV9 = v9Sources.Where(source => !v10Sources.Contains(source));
      var onlyInV10 = v10Sources.Where(source => !v9Sources.Contains(source));

      Console.WriteLine("Checking project sources {0} vs. {1}", v9proj, v10proj);
      var v9errors = onlyInV9.Count();
      foreach (var source in onlyInV9)
      {
        Console.WriteLine("File {0} only in {1}", source, v9proj);
      }

      var v10errors = onlyInV10.Count();
      foreach (var source in onlyInV10)
      {
        Console.WriteLine("File {0} only in {1}", source, v10proj);
      }

      Assert.AreEqual(0, v9errors, "Found {0} errors in project file {1}", v9errors, v9proj);
      Assert.AreEqual(0, v10errors, "Found {0} errors in project file {1}", v10errors, v10proj);
    }

    [TestMethod]
    [TestCategory("OOB"), TestCategory("CoreTest"), TestCategory("Short")]
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
      var cras40 = new[] {
        "System.Numerics"
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

      var windowsPhone = new[]{ 
        "mscorlib",
        "System",
        "System.Core",
        "System.Windows",
        "System.Xml",
        "System.Xml.Linq",
      };

      VerifyFramework(cras35, @"v3.5");
      VerifyFramework(cras35, @".NETFramework\v4.0");
      VerifyFramework(cras40, @".NETFramework\v4.0");
      VerifyFramework(silverlight, @"Silverlight\v3.0");
      VerifyFramework(silverlight, @"Silverlight\v4.0");
      VerifyFramework(windowsPhone, @"Silverlight\v4.0\Profile\WindowsPhone");
    }

    private static void VerifyFramework(string[] cras, string basePath)
    {
      foreach (var cra in cras)
      {
        Verify(cra, basePath);
      }
    }
  }
}
