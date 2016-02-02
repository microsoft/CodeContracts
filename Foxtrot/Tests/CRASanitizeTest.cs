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
using System.IO;
using System.Xml.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
  public partial class Test
  {
    private readonly ITestOutputHelper _testOutputHelper;

    public Test(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private static void Verify(ITestOutputHelper testOutputHelper, string assemblyName, string frameworkPath)
    {
      var path = Path.Combine(Options.ReplaceConfiguration(@"..\..\..\..\Microsoft.Research\Contracts\bin\<Configuration>"), frameworkPath);
      string originalPath = Path.Combine(@"..\..\..\..\Microsoft.Research\Imported\ReferenceAssemblies", frameworkPath, assemblyName + ".dll");

      StringWriter stringWriter = new StringWriter();
      var checker = new CRASanitizer.Checker(stringWriter);

      var contractAssemblyName = assemblyName + ".Contracts.dll";
      testOutputHelper.WriteLine("Checking {1} {0} for errors...", assemblyName, frameworkPath);

      testOutputHelper.WriteLine("  Contract Path: {0}", Path.GetFullPath(Path.Combine(path, contractAssemblyName)));
      testOutputHelper.WriteLine("  Original Path: {0}", Path.GetFullPath(originalPath));

      var errors = checker.CheckSurface(Path.Combine(path, contractAssemblyName), originalPath);

      testOutputHelper.WriteLine(stringWriter.ToString());
      Assert.Equal(0, errors);
      testOutputHelper.WriteLine("... done.");
    }

    public static IEnumerable<object[]> OOBCAssemblies
    {
        get
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

          foreach (var assembly in cras35)
          {
              yield return new[] { assembly, @"v3.5" };
          }

          foreach (var assembly in cras35)
          {
              yield return new[] { assembly, @".NETFramework\v4.0" };
          }

          foreach (var assembly in cras40)
          {
              yield return new[] { assembly, @".NETFramework\v4.0" };
          }

          foreach (var assembly in silverlight)
          {
              yield return new[] { assembly, @"Silverlight\v3.0" };
          }

          foreach (var assembly in silverlight)
          {
              yield return new[] { assembly, @"Silverlight\v4.0" };
          }

          foreach (var assembly in windowsPhone)
          {
              yield return new[] { assembly, @"Silverlight\v4.0\Profile\WindowsPhone" };
          }
        }
    }

    [Theory]
    [MemberData("OOBCAssemblies")]
    [Trait("Category", "OOB")]
    [Trait("Category", "CoreTest")]
    [Trait("Category", "Short")]
    public void CheckOOBCAssemblies(string cra, string basePath)
    {
      Verify(_testOutputHelper, cra, basePath);
    }
  }
}
