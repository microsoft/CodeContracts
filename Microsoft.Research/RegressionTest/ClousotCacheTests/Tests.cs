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
using ClousotTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
  /// <summary>
  /// Summary description for RewriterTests
  /// </summary>
  [TestClass]
  public class ClousotCacheTests
  {
    public ClousotCacheTests()
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
    public void MyTestCleanup()
    {
      if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed && CurrentGroupInfo != null && !System.Diagnostics.Debugger.IsAttached)
      {
        // record failing case
        CurrentGroupInfo.WriteFailure();
      }
    }
    #endregion

    [TestCategory("StaticChecker"), TestCategory("Clousot1")]
    [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotCacheTests\ClousotCacheTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotCacheTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
    [TestMethod]
    public void Analyze1FromSourcesV35Cache()
    {
      var options = GrabTestOptions("Analyze1FromSourcesV35Cache");
      options.BuildFramework = @"v3.5";
      options.ContractFramework = @"v3.5";
      if (!options.Skip)
        TestDriver.BuildAndAnalyze(options);
    }

    [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Cache")]
    [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotCacheTests\ClousotCacheTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotCacheTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
    [TestMethod]
    public void Analyze2FromSourcesV35Cache()
    {
      var options = GrabTestOptions("Analyze2FromSourcesV35Cache");
      options.BuildFramework = @"v3.5";
      options.ContractFramework = @"v3.5";
      if (!options.Skip)
        TestDriver.BuildAndAnalyze2(options);
    }

    [TestCategory("StaticChecker"), TestCategory("Clousot1"), TestCategory("Cache")]
    [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotCacheTests\ClousotCacheTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotCacheTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
    [TestMethod]
    public void Analyze1FromSourcesV40Cache()
    {
      var options = GrabTestOptions("Analyze1FromSourcesV40Cache");
      options.BuildFramework = @".NETFramework\v4.0";
      options.ContractFramework = @".NETFramework\v4.0";
      if (!options.Skip)
        TestDriver.BuildAndAnalyze(options);
    }

    [TestCategory("StaticChecker"), TestCategory("Clousot1"), TestCategory("Cache")]
    [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotCacheTests\ClousotCacheTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotCacheTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
    [TestMethod]
    public void Analyze1FromSourcesV40AgainstV35ContractsCache()
    {
      var options = GrabTestOptions("Analyze1FromSourcesV40AgainstV35ContractsCache");
      options.BuildFramework = @".NETFramework\v4.0";
      options.ContractFramework = @"v3.5";
      if (!options.Skip)
        TestDriver.BuildAndAnalyze(options);
    }

    [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Service"), TestCategory("Cache")]
    [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotCacheTests\ClousotCacheTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotCacheTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
    [TestMethod]
    public void Analyze2ServiceSequentialFromSourcesV40Cache()
    {
      var options = GrabTestOptions("Analyze2ServiceSequentialFromSourcesV40Cache");
      options.BuildFramework = @".NETFramework\v4.0";
      options.ContractFramework = @".NETFramework\v4.0";
      if (!options.Skip)
        TestDriver.BuildAndAnalyze2S(options);
    }

    [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Service"), TestCategory("Cache"), TestCategory("Short")]
    [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotCacheTests\ClousotCacheTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotCacheTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
    [TestMethod]
    public void Analyze2FastSequentialFromSourcesV40Cache()
    {
      var options = GrabTestOptions("Analyze2FastSequentialFromSourcesV40Cache");
      options.BuildFramework = @".NETFramework\v4.0";
      options.ContractFramework = @".NETFramework\v4.0";
      options.Fast = true;
      if (!options.Skip)
        TestDriver.BuildAndAnalyze2(options);
    }

    [AssemblyCleanup] // Automatically called at the end of ClousotCacheTests
    public static void AssemblyCleanup()
    {
      TestDriver.Cleanup();
    }

    private Options GrabTestOptions(string testGroupName)
    {
      var options = new Options(testGroupName, TestContext);
      CurrentGroupInfo = options.Group;
      return options;
    }

    static GroupInfo CurrentGroupInfo;

  }

}
