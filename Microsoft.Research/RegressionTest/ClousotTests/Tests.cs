// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ClousotTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    /// <summary>
    /// Summary description for RewriterTests
    /// </summary>
    [TestClass]
    public class ClousotTests
    {
        public ClousotTests()
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

        #region Regular tests

        //[DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        //[TestMethod]
        public void Analyze1Z3FromSourcesV35()
        {
            var options = GrabTestOptions("Analyze1Z3FromSourcesV35");
            options.BuildFramework = @"v3.5";
            options.ContractFramework = @"v3.5";
            options.ClousotOptions += " -useZ3";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze(options);
        }

        [Timeout(3600000), DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot1")]
        public void Analyze1FromSourcesV35()
        {
            var options = GrabTestOptions("Analyze1FromSourcesV35");
            options.BuildFramework = @"v3.5";
            options.ContractFramework = @"v3.5";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze(options);
        }

        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2")]
        public void Analyze2FromSourcesV35()
        {
            var options = GrabTestOptions("Analyze2FromSourcesV35");
            options.BuildFramework = @"v3.5";
            options.ContractFramework = @"v3.5";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2(options);
        }

        [Timeout(3600000), DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot1")]
        public void Analyze1FromSourcesV40()
        {
            var options = GrabTestOptions("Analyze1FromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze(options);
        }

        [Timeout(3600000), DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot1")]
        public void Analyze1FromSourcesV40AgainstV35Contracts()
        {
            var options = GrabTestOptions("Analyze1FromSourcesV40AgainstV35Contracts");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @"v3.5";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze(options);
        }

        #endregion

        #region Fast Tests

        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Parallel")]
        public void Analyze2FastBeginParallelFromSourcesV40()
        {
            var options = GrabTestOptions("Analyze2FastBeginParallelFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.GenerateUniqueOutputName = true;
            options.Fast = true;
            if (!options.Skip)
                TestDriver.AsyncFast2.BeginTest(options);
        }

        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Parallel")]
        public void Analyze2FastEndParallelFromSourcesV40()
        {
            var options = GrabTestOptions("Analyze2FastEndParallelFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.GenerateUniqueOutputName = true;
            options.Fast = true;
            if (!options.Skip)
                TestDriver.AsyncFast2.EndTest(options);
        }

        #endregion

        #region Service tests

        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Service")]
        public void Analyze2ServiceSequentialFromSourcesV40()
        {
            var options = GrabTestOptions("Analyze2ServiceSequentialFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2S(options);
        }

        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Service"), TestCategory("Parallel")]
        public void Analyze2ServiceBeginParallelFromSourcesV40()
        {
            var options = GrabTestOptions("Analyze2ServiceBeginParallelFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.GenerateUniqueOutputName = true;
            if (!options.Skip)
                TestDriver.Async2S.BeginTest(options);
        }

        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Service"), TestCategory("Parallel")]
        public void Analyze2ServiceEndParallelFromSourcesV40()
        {
            var options = GrabTestOptions("Analyze2ServiceEndParallelFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.GenerateUniqueOutputName = true;
            if (!options.Skip)
                TestDriver.Async2S.EndTest(options);
        }

        #endregion

        #region Slicing tests

        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Slicer")]
        public void Slice2SequentialFromSourcesV40()
        {
            var options = GrabTestOptions("Slice2SequentialFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -workers:0";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(options);
        }

        [Timeout(2700000), DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Slicer")]
        public void Slice2FastSequentialFromSourcesV40()
        {
            var options = GrabTestOptions("Slice2FastSequentialFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -workers:0";
            options.Fast = true;
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(options);
        }

        [Timeout(4500000), DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot1"), TestCategory("Slicer")]
        public void Slice2Analyze1SequentialFromSourcesV40()
        {
            var options = GrabTestOptions("Slice2Analyze1SequentialFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst -workers:1";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze1Slicing(options);
        }

        [Timeout(2700000), DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Slicer")]
        public void Slice2Analyze2SequentialFromSourcesV40()
        {
            var options = GrabTestOptions("Slice2Analyze2SequentialFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst -workers:1";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(options);
        }

        [Timeout(4500000), DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Slicer")]
        public void Slice2Analyze2SequentialFromSourcesV40WithDiskCache()
        {
            var options = GrabTestOptions("Slice2Analyze2SequentialFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst -workers:1 -clearcache -cache";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(options);
        }


        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Slicer")]
        public void Slice2Analyze2FastSequentialFromSourcesV40()
        {
            var options = GrabTestOptions("Slice2Analyze2FastSequentialFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst -workers:1";
            options.Fast = true;
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(options);
        }

        [Timeout(3600000), DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Slicer")]
        public void Slice2Analyze2FastSequentialFromSourcesV40WithDiskCache()
        {
            var options = GrabTestOptions("Slice2Analyze2FastSequentialFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst -workers:1 -clearcache -cache";
            options.Fast = true;
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(options);
        }


        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot1"), TestCategory("Slicer"), TestCategory("Parallel")]
        public void Slice2Analyze1ParallelFromSourcesV40()
        {
            var options = GrabTestOptions("Slice2Analyze1ParallelFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze1Slicing(options);
        }

        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Slicer"), TestCategory("Parallel")]
        public void Slice2Analyze2ParallelFromSourcesV40()
        {
            var options = GrabTestOptions("Slice2Analyze2ParallelFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(options);
        }

        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotTests\ClousotTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("StaticChecker"), TestCategory("Clousot2"), TestCategory("Slicer"), TestCategory("Parallel")]
        public void Slice2Analyze2FastParallelFromSourcesV40()
        {
            var options = GrabTestOptions("Slice2Analyze2FastParallelFromSourcesV40");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst";
            options.Fast = true;
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(options);
        }

        #endregion

        [AssemblyCleanup] // Automatically called at the end of ClousotTests
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

        private static GroupInfo currentGroupInfo;

        private static GroupInfo CurrentGroupInfo
        {
            get
            {
                return currentGroupInfo;
            }
            set
            {
                // see if the group has changed and if so, delete the failure file
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    if (currentGroupInfo == null || currentGroupInfo.TestGroupName != value.TestGroupName)
                    {
                        // new group, delete the old file
                        value.DeleteFailureFile();
                    }
                }
                currentGroupInfo = value;
            }
        }
    }
}
