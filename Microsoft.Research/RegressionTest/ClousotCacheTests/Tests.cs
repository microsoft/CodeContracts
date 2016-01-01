// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ITestOutputHelper = Xunit.Abstractions.ITestOutputHelper;

namespace Tests
{
    using System.Collections.Generic;
    using System.IO;

    using global::ClousotCacheTests;

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
        //[TestMethod] -- fails
        public void Analyze1FromSourcesV35Cache()
        {
            var options = GrabTestOptions("Analyze1FromSourcesV35Cache");
            options.BuildFramework = @"v3.5";
            options.ContractFramework = @"v3.5";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze(ConsoleTestOutputHelper.Instance, options, options.TestName, options.TestInstance);
        }

        [TestCategory("StaticChecker"), TestCategory("Clousot1"), TestCategory("Cache")]
        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotCacheTests\ClousotCacheTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotCacheTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        //[TestMethod] -- fails
        public void Analyze1FromSourcesV40Cache()
        {
            var options = GrabTestOptions("Analyze1FromSourcesV40Cache");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze(ConsoleTestOutputHelper.Instance, options, options.TestName, options.TestInstance);
        }

        [TestCategory("StaticChecker"), TestCategory("Clousot1"), TestCategory("Cache")]
        [DeploymentItem(@"Microsoft.Research\RegressionTest\ClousotCacheTests\ClousotCacheTestInputs.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\ClousotCacheTestInputs.xml", "TestRun", DataAccessMethod.Sequential)]
        //[TestMethod] -- fails
        public void Analyze1FromSourcesV40AgainstV35ContractsCache()
        {
            var options = GrabTestOptions("Analyze1FromSourcesV40AgainstV35ContractsCache");
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @"v3.5";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze(ConsoleTestOutputHelper.Instance, options, options.TestName, options.TestInstance);
        }

        [AssemblyCleanup] // Automatically called at the end of ClousotCacheTests
        public static void AssemblyCleanup()
        {
        }


        class LegacyOptions : Options
        {
            private static Dictionary<string, GroupInfo> groupInfo = new Dictionary<string, GroupInfo>();
            private int instance;
            public int Instance { get { return instance; } }
            private string testGroupName;

            public GroupInfo Group;

            public LegacyOptions(
                string sourceFile,
                string clousotOptions,
                bool useContractReferenceAssemblies,
                bool useExe,
                string compilerOptions,
                string[] references,
                string[] libPaths,
                string compilerCode,
                TestRuns skipFor = TestRuns.None)
                : base(sourceFile,
                    clousotOptions,
                    useContractReferenceAssemblies,
                    useExe,
                    compilerOptions,
                    references,
                    libPaths,
                    compilerCode,
                    skipFor)
            {
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

            public static string LoadString(System.Data.DataRow dataRow, string name, string defaultValue = "")
            {
                if (!ColumnExists(dataRow, name))
                    return defaultValue;
                var result = dataRow[name] as string;
                if (String.IsNullOrEmpty(result))
                    return defaultValue;
                return result;
            }

            public static List<string> LoadList(System.Data.DataRow dataRow, string name, params string[] initial)
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

            public static bool LoadBool(System.Data.DataRow dataRow, string name, bool defaultValue)
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

            public string TestName
            {
                get
                {
                    if (SourceFile != null)
                        return Path.GetFileNameWithoutExtension(SourceFile) + "_" + Instance;

                    return Instance.ToString();
                }
            }

            public string TestGroupName
            {
                get
                {
                    return this.testGroupName;
                }

                set
                {
                    this.testGroupName = value;
                    this.Group = GetTestGroup(testGroupName, RootDirectory, out instance);
                }
            }

            public int TestInstance { get { return this.Instance; } }

            public bool Skip
            {
                get
                {
                    if (Group == null)
                        return false;

                    if (!System.Diagnostics.Debugger.IsAttached)
                        return false;

                    // use only the previously failed file indices
                    return !Group.Selected;
                }
            }
        }

        private LegacyOptions GrabTestOptions(string testGroupName)
        {
            var dataRow = TestContext.DataRow;
            var sourceFile = LegacyOptions.LoadString(dataRow, "Name");
            var clousotOptions = LegacyOptions.LoadString(dataRow, "Options");
            var useContractReferenceAssemblies = LegacyOptions.LoadBool(dataRow, "ContractReferenceAssemblies", false);
            var useExe = LegacyOptions.LoadBool(dataRow, "Exe", false);
            var compilerOptions = LegacyOptions.LoadString(dataRow, "CompilerOptions");
            var references = LegacyOptions.LoadList(dataRow, "References");
            var libPaths = LegacyOptions.LoadList(dataRow, "LibPaths");
            var compilerCode = LegacyOptions.LoadString(dataRow, "Compiler", "CS");

            var options = new LegacyOptions(
                sourceFile: sourceFile,
                clousotOptions: clousotOptions,
                useContractReferenceAssemblies: useContractReferenceAssemblies,
                useExe: useExe,
                compilerOptions: compilerOptions,
                references: references.ToArray(),
                libPaths: libPaths.ToArray(),
                compilerCode: compilerCode);

            options.TestGroupName = testGroupName;
            CurrentGroupInfo = options.Group;
            return options;
        }

        private static GroupInfo CurrentGroupInfo;

        private sealed class ConsoleTestOutputHelper : ITestOutputHelper
        {
            public static readonly ConsoleTestOutputHelper Instance = new ConsoleTestOutputHelper();

            public void WriteLine(string format, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void WriteLine(string message)
            {
                Console.WriteLine(message);
            }
        }
    }
}
