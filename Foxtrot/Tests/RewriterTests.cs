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

namespace Tests
{
  /// <summary>
  /// Summary description for RewriterTests
  /// </summary>
  [TestClass]
  public class RewriterTests
  {
    public RewriterTests()
    {
      //
      // TODO: Add constructor logic here
      //
    }

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

    #region Additional test attributes
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    //Use TestCleanup to run code after each test has run
    [TestCleanup()]
    public void MyTestCleanup() {
      if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed && TestContext.DataRow != null)
      {
        try
        {
          string sourceFile = (string)TestContext.DataRow["Name"];
          string options = (string)TestContext.DataRow["Options"];
          TestContext.WriteLine("FAILED: Name={0} Options={1}", sourceFile, options);
        }
        catch { }
      }
    }
    #endregion

    const string FrameworkBinariesToRewritePath = @"Foxtrot\Tests\RewriteExistingBinaries\";

    [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "RewriteExisting", DataAccessMethod.Sequential)]
    [TestMethod]
    [TestCategory("Runtime"), TestCategory("Framework"), TestCategory("V4.0"), TestCategory("CoreTest")]
    public void RewriteFrameworkDlls40()
    {
      var options = new Options(this.TestContext);
      var framework = @".NetFramework\v4.0";
      options.ContractFramework = framework;
      var dllpath = FrameworkBinariesToRewritePath + framework;
      options.BuildFramework = options.MakeAbsolute(dllpath);
      var extrareferencedir = options.MakeAbsolute(TestDriver.ReferenceDirRoot);
      options.LibPaths.Add(extrareferencedir);
      options.FoxtrotOptions = options.FoxtrotOptions + " /verbose:4";
      options.UseContractReferenceAssemblies = false;
      if (File.Exists(options.MakeAbsolute(Path.Combine(dllpath, options.SourceFile))))
      {
        TestDriver.RewriteBinary(options, dllpath);
      }
    }

    [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "RewriteExisting", DataAccessMethod.Sequential)]
    [TestMethod]
    [TestCategory("Runtime"), TestCategory("Framework"), TestCategory("V4.5"), TestCategory("CoreTest")]
    public void RewriteFrameworkDlls45()
    {
      var options = new Options(this.TestContext);
      var framework = @".NetFramework\v4.5";
      options.ContractFramework = framework;
      var dllpath = FrameworkBinariesToRewritePath + framework;
      var extrareferencedir = options.MakeAbsolute(TestDriver.ReferenceDirRoot);
      options.LibPaths.Add(extrareferencedir);
      options.BuildFramework = options.MakeAbsolute(dllpath);
      options.FoxtrotOptions = options.FoxtrotOptions + " /verbose:4";
      options.UseContractReferenceAssemblies = false;
      if (File.Exists(options.MakeAbsolute(Path.Combine(dllpath, options.SourceFile))))
      {
        TestDriver.RewriteBinary(options, dllpath);
      }
    }

    [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "TestConfiguration", DataAccessMethod.Sequential)]
    [TestMethod]
    [TestCategory("Runtime"), TestCategory("ThirdParty"), TestCategory("CoreTest")]
    public void RewriteQuickGraph()
    {
      var options = new Options("QuickGraph", (string)TestContext.DataRow["FoxtrotOptions"], TestContext);
      TestDriver.RewriteAndVerify("", @"Foxtrot\Tests\Quickgraph\QuickGraphBinaries\Quickgraph.dll", options);
    }

    [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "TestConfiguration", DataAccessMethod.Sequential)]
    [TestMethod]
    [TestCategory("Runtime"), TestCategory("ThirdParty"), TestCategory("CoreTest")]
    public void RewriteQuickGraphData()
    {
      var options = new Options("QuickGraphData", (string)TestContext.DataRow["FoxtrotOptions"], TestContext);
      options.FoxtrotOptions = options.FoxtrotOptions + " /verbose:4";
      options.Delete(@"Foxtrot\Tests\QuickGraph\QuickGraphBinaries\QuickGraph.Contracts.dll");
      TestDriver.RewriteAndVerify("", @"Foxtrot\Tests\Quickgraph\QuickGraphBinaries\Quickgraph.Data.dll", options);
    }

    [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "TestConfiguration", DataAccessMethod.Sequential)]
    [TestMethod]
    [TestCategory("Runtime"), TestCategory("ThirdParty"), TestCategory("CoreTest")]
    public void RewriteQuickGraphDataOOB()
    {
      var options = new Options("QuickGraphDataOOB", (string)TestContext.DataRow["FoxtrotOptions"], TestContext);
      options.LibPaths.Add(@"Foxtrot\Tests\QuickGraph\QuickGraphBinaries\Contracts"); // subdirectory containing contracts.
      options.FoxtrotOptions = options.FoxtrotOptions + " /verbose:4";
      TestDriver.RewriteAndVerify("", @"Foxtrot\Tests\Quickgraph\QuickGraphBinaries\Quickgraph.Data.dll", options);
    }

    [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "TestFile", DataAccessMethod.Sequential)]
    [TestMethod]
    [TestCategory("Runtime"), TestCategory("CoreTest"), TestCategory("V3.5")]
    public void BuildRewriteRunFromSourcesV35()
    {
      var options = new Options(this.TestContext);
      options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
      options.BuildFramework = "v3.5";
      options.ContractFramework = "v3.5";
      options.UseTestHarness = true;
      TestDriver.BuildRewriteRun(options);
    }

    [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "TestFile", DataAccessMethod.Sequential)]
    [TestMethod]
    [TestCategory("Runtime"), TestCategory("CoreTest"), TestCategory("V4.0")]
    public void BuildRewriteRunFromSourcesV40()
    {
      var options = new Options(this.TestContext);
      options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
      options.BuildFramework = @".NETFramework\v4.0";
      options.ContractFramework = @".NETFramework\v4.0";
      options.UseTestHarness = true;
      TestDriver.BuildRewriteRun(options);
    }

    [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "TestFile", DataAccessMethod.Sequential)]
    [TestMethod]
    [TestCategory("Runtime"), TestCategory("CoreTest"), TestCategory("V4.5")]
    public void BuildRewriteRunFromSourcesV45()
    {
      var options = new Options(this.TestContext);
      options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
      options.BuildFramework = @".NETFramework\v4.5";
      options.ContractFramework = @".NETFramework\v4.0";
      options.UseTestHarness = true;
      TestDriver.BuildRewriteRun(options);
    }

    [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "TestFile", DataAccessMethod.Sequential)]
    [TestMethod]
    [TestCategory("Runtime"), TestCategory("CoreTest"), TestCategory("Roslyn"), TestCategory("V4.5")]
    [Ignore()] // Old Roslyn bits are not compatible with CCRewrite. Test (and old binaries) should be removed in the next iteration.
    public void BuildRewriteRunFromSourcesRoslynV45()
    {
      var options = new Options(this.TestContext);
      options.IsLegacyRoslyn = true;
      options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
      options.BuildFramework = @"Roslyn\v4.5";
      options.ReferencesFramework = @".NetFramework\v4.5";
      options.ContractFramework = @".NETFramework\v4.0";
      options.UseTestHarness = true;
      TestDriver.BuildRewriteRun(options);
    }
      
    [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "TestFile", DataAccessMethod.Sequential)]
    [TestMethod]
    [TestCategory("Runtime"), TestCategory("CoreTest"), TestCategory("Roslyn"), TestCategory("VS14")]
    public void BuildRewriteRunFromSourcesRoslynVS14RC()
    {
      var options = new Options(this.TestContext);
      // For VS14RC+ version compiler name is the same Csc.exe, and behavior from async/iterator perspective is similar
      // to old compiler as well. That's why IsLegacyRoslyn should be false in this test case.
      options.IsLegacyRoslyn = false;
      options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
      options.BuildFramework = @"Roslyn\VS14RC";
      options.ReferencesFramework = @".NetFramework\v4.5";
      options.ContractFramework = @".NETFramework\v4.0";
      options.UseTestHarness = true;
      TestDriver.BuildRewriteRun(options);
    }


    [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "TestFile", DataAccessMethod.Sequential)]
    [TestMethod]
    [TestCategory("Runtime"), TestCategory("CoreTest"), TestCategory("V4.0"), TestCategory("V3.5")]
    public void BuildRewriteRunFromSourcesV40AgainstV35Contracts()
    {
      var options = new Options(this.TestContext);
      options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
      options.BuildFramework = @".NETFramework\v4.0";
      options.ContractFramework = @"v3.5";
      options.UseTestHarness = true;
      TestDriver.BuildRewriteRun(options);
    }

    private void GrabTestOptions(out string sourceFile, out string options, out string cscoptions, out List<string> refs, out List<string> libs)
    {
      sourceFile = (string)TestContext.DataRow["Name"];
      options = (string)TestContext.DataRow["Options"];
      cscoptions = TestContext.DataRow["CSCOptions"] as string;
      string references = TestContext.DataRow["References"] as string;
      refs = new List<string>(new[] { "System.dll" });
      if (references != null)
      {
        refs.AddRange(references.Split(';'));
      }
      libs = new List<string>();
      string libPaths = TestContext.DataRow["Libpaths"] as string;
      if (libPaths != null)
      {
        libs.AddRange(libPaths.Split(';'));
      }

    }


  }
}
