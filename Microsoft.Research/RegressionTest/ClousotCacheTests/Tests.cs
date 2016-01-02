// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ClousotCacheTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Tests;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    public class ChacheTestOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases;
        }
    }

    /// <summary>
    /// Cache tests for 
    /// </summary>
    /// <remarks>
    /// For XUnit there is no way to guarantee the test order of [MemberData] driven tests - however for these tests the order is important.
    /// Workaround: use [Fact] instead of [Theory]+[MemberData] and execute all cases within one test.
    /// </remarks>
    public class ClousotCacheTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ClousotCacheTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        #region Test data

        private static IEnumerable<Options> TestRunDataSource
        {
            get
            {
                // These 4 TestRun must be run together in that order !
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\Test.cs",
                    compilerOptions: @"/optimize /d:FIRST",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-nonnull -bounds -show progress -cache -emitErrorOnCacheLookup -clearCache -cacheServer="""" -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\Test2.cs",
                    compilerOptions: @"/optimize /d:FIRST",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-nonnull -bounds -show progress -cache -emitErrorOnCacheLookup -cacheServer="""" -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\Test.cs",
                    compilerOptions: @"/optimize",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-nonnull -bounds -show progress -cache -emitErrorOnCacheLookup -cacheServer="""" -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\Test2.cs",
                    compilerOptions: @"/optimize",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-nonnull -bounds -show progress -cache -emitErrorOnCacheLookup -cacheServer="""" -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                // The remaining TestRun's are paired. Each pair must be run in that order
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\TestQuantifiers.cs",
                    compilerOptions: @"/optimize /d:FIRST",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-nonnull -bounds -arrays -show progress -cache -emitErrorOnCacheLookup -clearCache -cacheServer=""""  -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\TestQuantifiers.cs",
                    compilerOptions: @"/optimize",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-nonnull -bounds -arrays -show progress -cache -emitErrorOnCacheLookup -cacheServer="""" -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\DB.cs",
                    compilerOptions: @"/optimize /d:FIRST /debug-",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-nonnull -bounds -show progress -cache -emitErrorOnCacheLookup -clearCache -cacheServer="""" -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\DB.cs",
                    compilerOptions: @"/optimize /debug-",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-nonnull -bounds -show progress -cache -emitErrorOnCacheLookup -cacheServer="""" -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\Serialization.cs",
                    compilerOptions: @"/optimize /d:FIRST /debug-",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-nonnull -bounds -show progress -cache -emitErrorOnCacheLookup -clearCache -cacheServer="""" -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\Serialization.cs",
                    compilerOptions: @"/optimize /debug-",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-nonnull -bounds -show progress -cache -emitErrorOnCacheLookup -cacheServer="""" -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\SerializationInferred.cs",
                    compilerOptions: @"/optimize /d:FIRST /debug-",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-includesuggestionsinregression -suggest requires -suggest methodensures -infer methodensures -infer requires -nonnull -bounds -show progress -cache -emitErrorOnCacheLookup -clearCache -cacheServer="""" -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\SerializationInferred.cs",
                    compilerOptions: @"/optimize /debug-",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-includesuggestionsinregression -suggest requires -suggest methodensures  -infer methodensures -infer requires -nonnull -bounds -show progress -cache -emitErrorOnCacheLookup -cacheServer="""" -cacheName:Test",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\SemanticBaseliningTests.cs",
                    compilerOptions: @"/optimize /d:FIRST",
                    libPaths: new string[0],
                    references: new string[0],
                    clousotOptions: @"-show progress,validations -emitErrorOnCacheLookup -clearCache -cache -cacheServer="""" -cacheName:Test -infer methodensures -suggest methodensures -suggest assumes -suggest requires -nonnull -bounds -repairs -premode combined -saveSemanticBaseline:semanticTest",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerCode: "CS"
                    );
                yield return new Options(
                   sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\SemanticBaseliningTests.cs",
                   compilerOptions: @"/optimize",
                   libPaths: new string[0],
                   references: new string[0],
                   clousotOptions: @"-show progress,validations -emitErrorOnCacheLookup -cache -cacheServer="""" -cacheName:Test -infer methodensures -suggest methodensures -suggest assumes -suggest requires -nonnull -bounds -repairs -premode combined -useSemanticBaseline:semanticTest -skipIdenticalMethods=false",
                   useContractReferenceAssemblies: true,
                   useExe: false,
                   compilerCode: "CS"
                   );
                /*
                yield return new Options(
                        sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\TestPurity.cs",
                        compilerOptions: @"/optimize /d:FIRST",
                        libPaths: new string[0],
                        references: new string[0],
                        clousotOptions: @"-nonnull:noobl -bounds:noobl -arrays:arraypurity -infer arraypurity -suggest arraypurity -show progress -cache -emitErrorOnCacheLookup -cacheName:Test"
                        useContractReferenceAssemblies: true,
                        useExe: false,
                        compilerCode: "CS"
                        SkipCCI2="true"
                    );

                yield return new Options(
                        sourceFile: @"Microsoft.Research\RegressionTest\ClousotCacheTests\Sources\TestPurity.cs",
                        compilerOptions: @"/optimize",
                        libPaths: new string[0],
                        references: new string[0],
                        clousotOptions: @"-nonnull:noobl -bounds:noobl -arrays:arraypurity -infer arraypurity -suggest arraypurity -show progress -cache -emitErrorOnCacheLookup -cacheName:Test"
                        useContractReferenceAssemblies: true,
                        useExe: false,
                        compilerCode: "CS"
                        SkipCCI2="true"
                    );
                */
            }
        }

        private static readonly Options[] TestRunData = TestRunDataSource.ToArray();

        #endregion

        [Fact]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot1"), Trait("Category", "Cache")]
        public void V35Cache()
        {
            Execute(@"v3.5", @"v3.5");
        }

        [Fact]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot1"), Trait("Category", "Cache")]
        public void V40Cache()
        {
            Execute(@".NETFramework\v4.0", @".NETFramework\v4.0");
        }

        [Fact]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot1"), Trait("Category", "Cache")]
        public void V40AgainstV35ContractsCache()
        {
            Execute( @".NETFramework\v4.0", @"v3.5");
        }

        private void Execute(string buildFramework, string contractFramework)
        {
            foreach (var testIndex in Enumerable.Range(0, TestRunData.Length))
            {
                var options = TestRunData[testIndex];
                var testName = Path.GetFileNameWithoutExtension(options.SourceFile) + "_" + testIndex;

                Execute(testIndex, testName, buildFramework, contractFramework);
            }
        }

        private void Execute(int testIndex, string testName, string buildFramework, string contractFramework)
        {
            var options = TestRunData[testIndex];

            options.BuildFramework = buildFramework;
            options.ContractFramework = contractFramework;

            TestDriver.BuildAndAnalyze(_testOutputHelper, options, testName, testIndex);
        }
    }
}
