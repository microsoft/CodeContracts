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

using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    /// <summary>
    /// Unit tests for ExtractorTests
    /// </summary>
    public class ExtractorTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ExtractorTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private static Options[] ExtractorTestData =
        {
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1080.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: true),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1078.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1000.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1001.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1002.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { @"System.Core.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1003.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1004.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1005.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1006.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1008.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1009.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1010.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1011.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1012.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1013.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1014.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1015.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: @"/optimize",
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CC1016.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
            new Options(
                sourceFile: @"Foxtrot\Tests\CheckerTests\CheckerErrors.cs",
                foxtrotOptions: null,
                useContractReferenceAssemblies: true,
                compilerOptions: null,
                references: new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" },
                libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                compilerCode: "CS",
                useBinDir: false,
                useExe: false,
                mustSucceed: false),
        };

        public static IEnumerable<object> ExtractorTest
        {
            get
            {
                return Enumerable.Range(0, ExtractorTestData.Length).Select(i => new object[] { i });
            }
        }

        [Theory]
        [MemberData("ExtractorTest")]
        [Trait("Category", "Runtime")]
        [Trait("Category", "CoreTest")]
        [Trait("Category", "V4.0")]
        [Trait("Category", "Short")]
        public void ExtractorFailures(int testIndex)
        {
            Options options = ExtractorTestData[testIndex];
            options.ContractFramework = @".NetFramework\v4.0";
            options.BuildFramework = @".NetFramework\v4.0";
            options.FoxtrotOptions = options.FoxtrotOptions + " /nologo /verbose:4 /iw:-";
            TestDriver.BuildExtractExpectFailureOrWarnings(_testOutputHelper, options);
        }
    }
}
