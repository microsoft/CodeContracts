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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    /// <summary>
    /// Unit tests for ccrewriter a.k.a. Foxtrot
    /// </summary>
    [TestClass]
    public class RewriterTests
    {
        const string FrameworkBinariesToRewritePath = @"Foxtrot\Tests\RewriteExistingBinaries\";

        public TestContext TestContext { get; set; }

        [TestCleanup]
        public void MyTestCleanup()
        {
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

        #region Roslyn compiler unit tests
        [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "TestFile", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("Runtime"), TestCategory("CoreTest"), TestCategory("Roslyn"), TestCategory("VS14")]
        public void BuildRewriteRunFromSourcesRoslynVS14RC()
        {
            var options = CreateRoslynOptions("VS14RC3");

            TestDriver.BuildRewriteRun(options);
        }

        /// <summary>
        /// Unit test for #47 - "Could not resolve type reference" for some iterator methods in VS2015
        /// </summary>
        [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "IteratorWithComplexGeneric", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("Runtime"), TestCategory("CoreTest"), TestCategory("Roslyn"), TestCategory("VS14")]
        public void Roslyn_IteratorBlockWithComplexGeneric()
        {
            var options = CreateRoslynOptions("VS14RC3");
            TestDriver.BuildRewriteRun(options);
        }

        /// <summary>
        /// Creates <see cref="Options"/> instance with default values suitable for testing roslyn-based compiler.
        /// </summary>
        /// <param name="compilerVersion">Should be the same as a folder name in the /Imported/Roslyn folder.</param>
        private Options CreateRoslynOptions(string compilerVersion)
        {
            var options = new Options(this.TestContext);
            // For VS14RC+ version compiler name is the same Csc.exe, and behavior from async/iterator perspective is similar
            // to old compiler as well. That's why IsLegacyRoslyn should be false in this test case.
            options.IsLegacyRoslyn = false;
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.CompilerPath = string.Format(@"Roslyn\{0}", compilerVersion);
            options.BuildFramework = @".NetFramework\v4.5";
            options.ReferencesFramework = @".NetFramework\v4.5";
            options.ContractFramework = @".NETFramework\v4.5";
            options.UseTestHarness = true;

            return options;
        }

        #endregion Roslyn compiler unit tests

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
        [Ignore] // Old Roslyn bits are not compatible with CCRewrite. Test (and old binaries) should be removed in the next iteration.
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

        [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "PublicSurfaceOnly", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("Runtime"), TestCategory("CoreTest"), TestCategory("V4.5")]
        public void BuildRewriteFromSources45WithPublicSurfaceOnly()
        {
            var options = new Options(this.TestContext);
            // For testing purposes you can remove /publicsurface and see what happen. Part of the tests should fail!
            //options.FoxtrotOptions = options.FoxtrotOptions + String.Format("/throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /publicsurface /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.BuildFramework = @".NETFramework\v4.5";
            options.ContractFramework = @".NETFramework\v4.0";
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
