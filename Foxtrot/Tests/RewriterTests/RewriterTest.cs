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
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    /// <summary>
    /// Unit tests for ccrewriter a.k.a. Foxtrot
    /// </summary>
    public class RewriterTest
    {
        const string FrameworkBinariesToRewritePath = @"Foxtrot\Tests\RewriteExistingBinaries\";

        private readonly ITestOutputHelper _testOutputHelper;

        public RewriterTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        #region Test Data

        public static IEnumerable<object> AsyncTests
        {
            get
            {
                return Enumerable.Range(0, AsyncTestsDataSource.BaseTestCases.Count()).Select(i => new object[] { i });
            }
        }

        public static IEnumerable<object> AsyncPostconditions
        {
            get
            {
                return Enumerable.Range(0, AsyncTestsDataSource.AsyncPostconditionsTestCases.Count()).Select(i => new object[] { i });
            }
        }


        private static IEnumerable<Options> TestFileData
        {
            get
            {
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\RequiresForAll.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\ComplexGeneric.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\PostAmbleDeletion.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\StaticOldValue.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\AbbrevGenLit.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\AbbrevGenClosure.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\ClosureOnlyBody.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\ClosureCtor.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\GenericInterfaceMethods.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\GenericConstraints.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\DupTest.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\ContractClassRemnants.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritClosureMatchingExistingClosure.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InvariantOnCtorThrow.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritRequirePublicSurface.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\ClosureAfterRequires.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\TestClosureDeletion.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\ClosureDeletionWithAbbrev.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async1.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async1.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async2.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async2.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async3.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async3.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async4.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async4.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async5.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async5.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async6.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async6.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async6b.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async6b.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async7.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async7.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async8.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async8.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async9.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async9.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async10.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async10.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async11.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async11.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async12.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async12.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async_EnsuresWithCapturedForAll.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Iterator1.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Iterator1.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Iterator2.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Iterator2.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Iterator3.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Iterator3.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritHiddenEnsures.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\BaseInvariantDelayed.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\GenericInstanceClosure.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritGenericTypeProtected.cs",
                    foxtrotOptions: @"/csr",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/d:LOCALBASE",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritGenericProtected.cs",
                    foxtrotOptions: @"/csr",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/d:LOCALBASE",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritOOBProtected.cs",
                    foxtrotOptions: @"/csr",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/d:LOCALBASE",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritOOBClosure.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll" },
                    libPaths: @"Foxtrot\Tests\AssemblyWithContracts\bin\Debug".Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries),
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\IteratorWithLegacyPrecondition.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\DefaultParameter.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\DefaultExpression.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritGenericAbstractClass.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritGenericAbstractClass.cs",
                    foxtrotOptions: @"/csr",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritGenericClosurePre.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritGenericClosurePre.cs",
                    foxtrotOptions: @"/csr",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritExists.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll" },
                    libPaths: @"Foxtrot\Tests\AssemblyWithContracts\bin\Debug".Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries),
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritForall.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll" },
                    libPaths: @"Foxtrot\Tests\AssemblyWithContracts\bin\Debug".Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries),
                    compilerCode: "",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritGenericExists.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll" },
                    libPaths: @"Foxtrot\Tests\AssemblyWithContracts\bin\Debug".Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries),
                    compilerCode: "",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritGenericForall.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll" },
                    libPaths: @"Foxtrot\Tests\AssemblyWithContracts\bin\Debug".Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries),
                    compilerCode: "",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritPost.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritPre.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritPreEx.cs",
                    foxtrotOptions: @"-assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritPreLegacy.cs",
                    foxtrotOptions: @"-assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritPreLegacyManual.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InterleavedValidations.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InterleavedValidationsInherit.cs",
                    foxtrotOptions: @"-assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\OOBinterfacePost.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\OOBInterfacePre.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\OOBInterfacePreGeneric.cs",
                    foxtrotOptions: @"/callsiterequires",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new[] { @"AssemblyWithContracts.dll", @"System.Core.dll" },
                    libPaths: @"Foxtrot\Tests\AssemblyWithContracts\bin\Debug".Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries),
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\OOBPost.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\OOBPre.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritPreInterface.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritClosureWithStelem.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritClosureWithStelem.cs",
                    foxtrotOptions: @"/callsiterequires",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
            }
        }

        public static IEnumerable<object> TestFile
        {
            get
            {
                return Enumerable.Range(0, TestFileData.Count()).Select(i => new object[] { i });
            }
        }


        private static IEnumerable<Options> RoslynCompatibilityData
        {
            get
            {
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\ExpressionsInRequires.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll", @"System.Linq.Expressions.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\AsyncWithLegacyRequires.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\AsyncRequiresWithMultipleAwaits.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\InheritOOBClosure.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll" },
                    libPaths: @"Foxtrot\Tests\AssemblyWithContracts\bin\Debug".Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries),
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\Sources\Async12.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\AbbrevGenClosure.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\CapturingContractResult.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\RequiresForAll.cs",
                    foxtrotOptions: @"-assemblyMode standard",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\InheritNonGenericAbstractClass.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\AsyncRequiresWithForAll.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: null,
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\AsyncWithoutContract.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: null,
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\InheritGenericAbstractClass.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: null,
                    references: new[] { @"System.Core.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\CapturingAsyncLambda.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: null,
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
            }
        }

        public static IEnumerable<object> RoslynCompatibility
        {
            get
            {
                return Enumerable.Range(0, RoslynCompatibilityData.Count()).Select(i => new object[] { i });
            }
        }


        private static IEnumerable<Options> IteratorWithComplexGenericData
        {
            get
            {
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\RoslynCompatibility\IteratorWithTuple.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
            }
        }

        public static IEnumerable<object> IteratorWithComplexGeneric
        {
            get
            {
                return Enumerable.Range(0, IteratorWithComplexGenericData.Count()).Select(i => new object[] { i });
            }
        }


        private static IEnumerable<Options> RewriteExistingData
        {
            get
            {
                yield return new Options(
                    sourceFile: @"mscorlib.dll",
                    foxtrotOptions: @"/throwonfailure=false",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.AddIn.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.Core.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.Design.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.Xml.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"Microsoft.VisualStudio.Utilities.v11.0.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.ComponentModel.Composition.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.Drawing.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.IO.Compression.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.IO.Compression.FileSystem.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.Net.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.Net.Http.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.Net.Http.WebRequestdll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.Numerics.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.Reflection.Context.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.Runtime.WindowsRuntime.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"System.Runtime.WindowsRuntime.UI.Xaml.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"VerifyWin8P.dll",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
            }
        }

        public static IEnumerable<object> RewriteExisting
        {
            get
            {
                return Enumerable.Range(0, RewriteExistingData.Count()).Select(i => new object[] { i });
            }
        }


        private static IEnumerable<Options> TestConfigurationData
        {
            get
            {
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:0 /throwonfailure=false",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:1 /throwonfailure=false",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:2 /throwonfailure=false",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:3 /throwonfailure=false",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:4 /throwonfailure=false",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:0 /throwonfailure=true",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:1 /throwonfailure=true",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:2 /throwonfailure=true",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:3 /throwonfailure=true",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:4 /throwonfailure=true",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:0 /throwonfailure=true /callsiterequires",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:1 /throwonfailure=true /callsiterequires",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:2 /throwonfailure=true /callsiterequires",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:3 /throwonfailure=true /callsiterequires",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
                yield return new Options(
                    sourceFile: @"",
                    foxtrotOptions: @"/level:4 /throwonfailure=true /callsiterequires",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);
            }
        }

        public static IEnumerable<object> TestConfiguration
        {
            get
            {
                return Enumerable.Range(0, TestConfigurationData.Count()).Select(i => new object[] { i });
            }
        }


        private static IEnumerable<Options> PublicSurfaceOnlyData
        {
            get
            {
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\PublicSurfaceOnly\IteratorWithComplexPrecondition.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: false);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\PublicSurfaceOnly\AsyncMethodWithComplexPrecondition.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new[] { @"AsyncCtpLibrary.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: false);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\PublicSurfaceOnly\CallPrivateRequiresShouldNotFail.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: false);
                yield return new Options(
                    sourceFile: @"Foxtrot\Tests\PublicSurfaceOnly\CallPrivateEnsuresShouldNotFail.cs",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: false);
            }
        }

        public static IEnumerable<object> PublicSurfaceOnly
        {
            get
            {
                return Enumerable.Range(0, PublicSurfaceOnlyData.Count()).Select(i => new object[] { i });
            }
        }
        #endregion

        #region Async tests
        [Theory]
        [MemberData("AsyncTests")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "Roslyn"), Trait("Category", "VS14")]
        public void AsyncTestsWithRoslynCompiler(int testIndex)
        {
            Options options = AsyncTestsDataSource.BaseTestCases.ElementAt(testIndex);
            CreateRoslynOptions(options, "VS14RC3");
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        [Theory]
        [MemberData("AsyncTests")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "V4.5")]
        public void AsyncTestsWithDotNet45(int testIndex)
        {
            Options options = AsyncTestsDataSource.BaseTestCases.ElementAt(testIndex);
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.BuildFramework = @".NETFramework\v4.5";
            options.ContractFramework = @".NETFramework\v4.0";
            options.UseTestHarness = true;
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        #endregion
        [Theory]
        [MemberData("AsyncPostconditions")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "Roslyn"), Trait("Category", "VS14")]
        public void TestAsyncPostconditionsWithRoslyn(int testIndex)
        {
            Options options = AsyncTestsDataSource.AsyncPostconditionsTestCases.ElementAt(testIndex);
            CreateRoslynOptions(options, "VS14RC3");
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        // TODO ST: it seems that the Trait("Category", "Roslyn" is invalid for this test! And VS14 as well!
        [Theory]
        [MemberData("AsyncPostconditions")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "Roslyn"), Trait("Category", "VS14")]
        public void TestAsyncPostconditionsV45(int testIndex)
        {
            Options options = AsyncTestsDataSource.AsyncPostconditionsTestCases.ElementAt(testIndex);
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.BuildFramework = @".NETFramework\v4.5";
            options.ContractFramework = @".NETFramework\v4.0";
            options.UseTestHarness = true;
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        #region Roslyn compiler unit tests
        [Theory]
        [MemberData("TestFile")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "Roslyn"), Trait("Category", "VS14")]
        public void Roslyn_BuildRewriteRunFromSources(int testIndex)
        {
            Options options = TestFileData.ElementAt(testIndex);
            CreateRoslynOptions(options, "VS14RC3");
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        [Theory]
        [MemberData("RoslynCompatibility")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "Roslyn"), Trait("Category", "VS14")]
        public void Roslyn_CompatibilityCheck(int testIndex)
        {
            Options options = RoslynCompatibilityData.ElementAt(testIndex);
            CreateRoslynOptions(options, "VS14RC3");

            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        [Theory]
        [MemberData("RoslynCompatibility")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "V4.5")]
        public void TestTheRoslynCompatibilityCasesWithVS2013Compiler(int testIndex)
        {
            Options options = RoslynCompatibilityData.ElementAt(testIndex);
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.BuildFramework = @".NETFramework\v4.5";
            options.ContractFramework = @".NETFramework\v4.0";
            options.UseTestHarness = true;
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        [Theory]
        [MemberData("RoslynCompatibility")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "Roslyn"), Trait("Category", "VS14")]
        public void Roslyn_CompatibilityCheckInReleaseMode(int testIndex)
        {
            Options options = RoslynCompatibilityData.ElementAt(testIndex);
            CreateRoslynOptions(options, "VS14RC3");

            options.CompilerOptions = "/optimize";
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        /// <summary>
        /// Unit test for #47 - "Could not resolve type reference" for some iterator methods in VS2015 and
        /// #186 - ccrewrite produces an incorrect type name in IteratorStateMachineAttribute with some generic types
        /// </summary>
        [Theory]
        [MemberData("IteratorWithComplexGeneric")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "Roslyn"), Trait("Category", "VS14")]
        public void Roslyn_IteratorBlockWithComplexGeneric(int testIndex)
        {
            Options options = IteratorWithComplexGenericData.ElementAt(testIndex);
            CreateRoslynOptions(options, "VS14RC3");
            // Bug with metadata reader could be reproduced only with a new mscorlib and new System.dll.
            options.BuildFramework = @".NetFramework\v4.6";
            options.ReferencesFramework = @".NetFramework\v4.6";
            options.DeepVerify = true;
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        /// <summary>
        /// Creates <see cref="Options"/> instance with default values suitable for testing roslyn-based compiler.
        /// </summary>
        /// <param name="compilerVersion">Should be the same as a folder name in the /Imported/Roslyn folder.</param>
        private void CreateRoslynOptions(Options options, string compilerVersion)
        {
            // For VS14RC+ version compiler name is the same Csc.exe, and behavior from async/iterator perspective is similar
            // to old compiler as well. That's why IsLegacyRoslyn should be false in this test case.
            options.IsLegacyRoslyn = false;
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.CompilerPath = string.Format(@"Roslyn\{0}", compilerVersion);
            options.BuildFramework = @".NetFramework\v4.5";
            options.ReferencesFramework = @".NetFramework\v4.5";
            options.ContractFramework = @".NETFramework\v4.5";
            options.UseTestHarness = true;
        }

        #endregion Roslyn compiler unit tests

        [Theory]
        [MemberData("RewriteExisting")]
        [Trait("Category", "Runtime"), Trait("Category", "Framework"), Trait("Category", "V4.0"), Trait("Category", "CoreTest")]
        public void RewriteFrameworkDlls40(int testIndex)
        {
            Options options = RewriteExistingData.ElementAt(testIndex);
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
                TestDriver.RewriteBinary(_testOutputHelper, options, dllpath);
            }
        }

        [Theory]
        [MemberData("RewriteExisting")]
        [Trait("Category", "Runtime"), Trait("Category", "Framework"), Trait("Category", "V4.5"), Trait("Category", "CoreTest")]
        public void RewriteFrameworkDlls45(int testIndex)
        {
            Options options = RewriteExistingData.ElementAt(testIndex);
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
                TestDriver.RewriteBinary(_testOutputHelper, options, dllpath);
            }
        }

        [Theory]
        [MemberData("TestConfiguration")]
        [Trait("Category", "Runtime"), Trait("Category", "ThirdParty"), Trait("Category", "CoreTest")]
        public void RewriteQuickGraph(int testIndex)
        {
            Options options = TestConfigurationData.ElementAt(testIndex);
            //var options = new Options("QuickGraph", (string)TestContext.DataRow["FoxtrotOptions"], TestContext);
            options.BuildFramework = @".NetFramework\v4.0";
            TestDriver.RewriteAndVerify(_testOutputHelper, "", @"Foxtrot\Tests\Quickgraph\QuickGraphBinaries\Quickgraph.dll", options);
        }

        [Theory]
        [MemberData("TestConfiguration")]
        [Trait("Category", "Runtime"), Trait("Category", "ThirdParty"), Trait("Category", "CoreTest")]
        public void RewriteQuickGraphData(int testIndex)
        {
            Options options = TestConfigurationData.ElementAt(testIndex);
            //var options = new Options("QuickGraphData", (string)TestContext.DataRow["FoxtrotOptions"], TestContext);
            options.BuildFramework = @".NetFramework\v4.0";
            options.FoxtrotOptions = options.FoxtrotOptions + " /verbose:4";
            options.Delete(@"Foxtrot\Tests\QuickGraph\QuickGraphBinaries\QuickGraph.Contracts.dll");
            TestDriver.RewriteAndVerify(_testOutputHelper, "", @"Foxtrot\Tests\Quickgraph\QuickGraphBinaries\Quickgraph.Data.dll", options);
        }

        [Theory]
        [MemberData("TestConfiguration")]
        [Trait("Category", "Runtime"), Trait("Category", "ThirdParty"), Trait("Category", "CoreTest")]
        public void RewriteQuickGraphDataOOB(int testIndex)
        {
            Options options = TestConfigurationData.ElementAt(testIndex);
            //var options = new Options("QuickGraphDataOOB", (string)TestContext.DataRow["FoxtrotOptions"], TestContext);
            options.BuildFramework = @".NetFramework\v4.0";
            options.LibPaths.Add(@"Foxtrot\Tests\QuickGraph\QuickGraphBinaries\Contracts"); // subdirectory containing contracts.
            options.FoxtrotOptions = options.FoxtrotOptions + " /verbose:4";
            TestDriver.RewriteAndVerify(_testOutputHelper, "", @"Foxtrot\Tests\Quickgraph\QuickGraphBinaries\Quickgraph.Data.dll", options);
        }

        [Theory]
        [MemberData("TestFile")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "V3.5")]
        public void BuildRewriteRunFromSourcesV35(int testIndex)
        {
            Options options = TestFileData.ElementAt(testIndex);
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.BuildFramework = "v3.5";
            options.ContractFramework = "v3.5";
            options.UseTestHarness = true;
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        [Theory]
        [MemberData("TestFile")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "V4.0")]
        public void BuildRewriteRunFromSourcesV40(int testIndex)
        {
            Options options = TestFileData.ElementAt(testIndex);
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.UseTestHarness = true;
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        [Theory]
        [MemberData("TestFile")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "V4.5")]
        public void BuildRewriteRunFromSourcesV45(int testIndex)
        {
            Options options = TestFileData.ElementAt(testIndex);
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.BuildFramework = @".NETFramework\v4.5";
            options.ContractFramework = @".NETFramework\v4.0";
            options.UseTestHarness = true;
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        [Theory]
        [MemberData("RoslynCompatibility")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "V4.5")]
        public void BuildRewriteRunRoslynTestCasesWithV45(int testIndex)
        {
            Options options = RoslynCompatibilityData.ElementAt(testIndex);
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.BuildFramework = @".NETFramework\v4.5";
            options.ContractFramework = @".NETFramework\v4.0";
            options.UseTestHarness = true;
            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        [Theory(Skip = "Old Roslyn bits are not compatible with CCRewrite. Test (and old binaries) should be removed in the next iteration.")]
        [MemberData("TestFile")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "Roslyn"), Trait("Category", "V4.5")]
        public void BuildRewriteRunFromSourcesRoslynV45(int testIndex)
        {
            Options options = TestFileData.ElementAt(testIndex);
            options.IsLegacyRoslyn = true;
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.BuildFramework = @"Roslyn\v4.5";
            options.ReferencesFramework = @".NetFramework\v4.5";
            options.ContractFramework = @".NETFramework\v4.0";
            options.UseTestHarness = true;

            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        [Theory]
        [MemberData("TestFile")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "V4.0"), Trait("Category", "V3.5")]
        public void BuildRewriteRunFromSourcesV40AgainstV35Contracts(int testIndex)
        {
            Options options = TestFileData.ElementAt(testIndex);
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @"v3.5";
            options.UseTestHarness = true;

            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        [Theory]
        [MemberData("PublicSurfaceOnly")]
        [Trait("Category", "Runtime"), Trait("Category", "CoreTest"), Trait("Category", "V4.5")]
        public void BuildRewriteFromSources45WithPublicSurfaceOnly(int testIndex)
        {
            Options options = PublicSurfaceOnlyData.ElementAt(testIndex);
            // For testing purposes you can remove /publicsurface and see what happen. Part of the tests should fail!
            //options.FoxtrotOptions = options.FoxtrotOptions + String.Format("/throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.FoxtrotOptions = options.FoxtrotOptions + String.Format(" /publicsurface /throwonfailure /rw:{0}.exe,TestInfrastructure.RewriterMethods", Path.GetFileNameWithoutExtension(options.TestName));
            options.BuildFramework = @".NETFramework\v4.5";
            options.ContractFramework = @".NETFramework\v4.0";
            options.UseTestHarness = true;

            TestDriver.BuildRewriteRun(_testOutputHelper, options);
        }

        //private void GrabTestOptions(out string sourceFile, out string options, out string cscoptions, out List<string> refs, out List<string> libs)
        //{
        //    sourceFile = (string)TestContext.DataRow["Name"];
        //    options = (string)TestContext.DataRow["Options"];
        //    cscoptions = TestContext.DataRow["CSCOptions"] as string;
        //    string references = TestContext.DataRow["References"] as string;
        //    refs = new List<string>(new[] { "System.dll" });
        //    if (references != null)
        //    {
        //        refs.AddRange(references.Split(';'));
        //    }
        //    libs = new List<string>();
        //    string libPaths = TestContext.DataRow["Libpaths"] as string;
        //    if (libPaths != null)
        //    {
        //        libs.AddRange(libPaths.Split(';'));
        //    }
        //}
    }
}
