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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    /// <summary>
    /// Unit tests for ExtractorTests
    /// </summary>
    [TestClass]
    public class ExtractorTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        [DeploymentItem("Foxtrot\\Tests\\TestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestInputs.xml", "ExtractorTest", DataAccessMethod.Sequential)]
        [TestMethod]
        [TestCategory("Runtime"), TestCategory("CoreTest"), TestCategory("V4.0"), TestCategory("Short")]
        public void ExtractorFailures()
        {
            var options = new Options(this.TestContext);
            options.ContractFramework = @".NetFramework\v4.0";
            options.BuildFramework = @".NetFramework\v4.0";
            options.FoxtrotOptions = options.FoxtrotOptions + " /nologo /verbose:4 /iw:-";
            TestDriver.BuildExtractExpectFailureOrWarnings(options);
        }
    }
}