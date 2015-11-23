// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ClousotTests;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    /// <summary>
    /// Summary description for RewriterTests
    /// </summary>
    [Collection("Clousot")]
    public class ClousotTests : IDisposable
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly ClousotFixture _clousotFixture;

        public ClousotTests(ITestOutputHelper testOutputHelper, ClousotFixture clousotFixture)
        {
            this._testOutputHelper = testOutputHelper;
            this._clousotFixture = clousotFixture;
        }

        public void Dispose()
        {
            //if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed && CurrentGroupInfo != null && !System.Diagnostics.Debugger.IsAttached)
            //{
            //    // record failing case
            //    CurrentGroupInfo.WriteFailure();
            //}
        }

        #region Test data

        private static IEnumerable<Options> TestRunData
        {
            get
            {
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Decimal.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show unreached -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.Linq.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\AssumeInvariant.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show unreached -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.Linq.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Herman.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.Linq.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: true,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\Domino.cs",
                    clousotOptions: @"-infer autopropertiesensures -show unreached -arrays -NonNull -Bounds -bounds:type=subpolyhedra,reduction=simplex,noObl -show progress -arithmetic:obl=intOverflow -adaptive -enum -check assumptions -suggest asserttocontracts -check conditionsvalidity -missingPublicRequiresAreErrors -suggest necessaryensures -suggest readonlyfields  -infer requires -infer methodensures -infer objectinvariants -premode:combined -warnifsuggest requires -suggest codefixes -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.Linq.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: true,
                    skipSlicing: false,
                    skipForNet35: true);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\MultidimArrays.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull:noobl -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\NonNullTests\NonNull1.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -define:regular -show:validations;unreached -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\NonNullTests\NonNullNoWP.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -wp:false -define:regular -show:validations;unreached -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new[] { @"System.ComponentModel.Composition.dll", @"Microsoft.CSharp.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\NonNull.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.Linq.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestIntervals\IntervalsTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:Intervals -arithmetic:type:Pentagons,obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestKarr\KarrTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -define karronly -bounds:type:Karr,diseq=false -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestKarr\KarrTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -define karrothers -bounds:type:PentagonsKarrLeq  -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\ExpressionSimplification\SimplifyExpression.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestBinarySearch\TestBinarySearch.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -Bounds:type=PentagonsKarrLeqOctagons",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\BrianScenario\BrianScenario.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds  -nonnull -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\BrianScenario\Protocols.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds  -nonnull -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\IfaceImplicitlyImplementedBug.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show validations -regression -show progress -nonnull",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll", @"System.Core.dll" },
                    libPaths: new[] { @"Foxtrot\Tests\AssemblyWithContracts\bin\debug" },
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\DoubleZero.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show validations -regression -show progress -nonnull",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll", @"System.Core.dll" },
                    libPaths: new[] { @"Foxtrot\Tests\AssemblyWithContracts\bin\debug" },
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\OperatorOverloading.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show validations  -show progress -nonnull -bounds",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll", @"System.Core.dll" },
                    libPaths: new[] { @"Foxtrot\Tests\AssemblyWithContracts\bin\debug" },
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\EnumerableAll.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ArrayForAll.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TypeSpecializations.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull:noobl -bounds:noobl -show progress -show validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\HeapCrash.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /r:System.Configuration.dll",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ForAll.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Mulder.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\IOperations.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -show:validations;unreached",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\NonNullTests\BrianStrelioff.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -define:regular -show:validations;unreached",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\NonNullTests\Inference.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -show validations;unreached -define infer -infer:requires;propertyensures;methodensures -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\SimpleArrayAccesses\SimpleArrayAccesses.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -Bounds:type=PentagonsKarrLeqOctagons -wp=false -timeout 100",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestUnsafe\BasicTests.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false -show progress -timeout 60 -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestUnsafe\ExamplesFromMscorlib.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestUnsafe\ExamplesFromRedhawk.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestUnsafe\SafeWrapper.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestUnsafe\Unsafe.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false -timeout 60 -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestCollections\List.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull:noObl -bounds:type=subpolyhedra",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestCollections\Stack.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull:noObl -bounds:type=subpolyhedra,reduction=simplex -timeout 200",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\FrameworkTests\BitArray.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -define bitarray -nonnull -bounds -bounds:type=subpolyhedra,diseq=false",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestContractsWithClousot\TestContractsWithClousot\Contracts.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show progress;validations -bounds",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestContractsWithClousot\TestContractsWithClousot\ExamplesFromPapers.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show progress;validations -bounds",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestNumericalDisequalities\SimpleBranching.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -arithmetic:obl=intOverflow -wp=false -bounds:noobl -missingPublicRequiresAreErrors=false",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\WeakestPreconditionTests\Paths.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\WeakestPreconditionTests\Disjuncts.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\WeakestPreconditionTests\RichardCook.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\WeakestPreconditionTests\ADomainsInterface.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\AssertOverloads.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\ContractVerificationAttribute.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\CompilerGeneratedAttribute.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -nonnull -show progress -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\Disjunctions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\EnsuresOnOutByRef.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\Expressions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\GenericTests.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\HeapAnalysis.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\InterfaceContracts.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\Inference.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -nonnull:noObl -define inference -infer requires -infer propertyensures -infer methodensures",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\Invariants.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\Iterators.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -arrays -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\LegacyRequires.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\OldTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress -assemblyMode=standard",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: true,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\ParameterInEnsures.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\PureFunction.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\ReceiverHavoc.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\RecursiveSubroutines.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\StructTests.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestSubpolyhedra\TestSubPolyhedra.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,diseq:false,ch,infOct,reduction=simplex -define simplex-convexhull  -wp=false -enforcefairJoin=true -joinsbeforewiden 1 -timeout 100 -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /define:SIMPLEXCONVEX",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestSubpolyhedra\TestSubPolyhedra.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,reduction=fast,diseq:false,ch,infOct -define fast -wp=false  -enforcefairJoin=true -joinsbeforewiden 1 -timeout 100 -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestSubpolyhedra\TestSubPolyhedra.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,diseq:false,infOct,reduction=simplex -define simplex -wp=false -enforcefairJoin=true -joinsbeforewiden 1 -timeout 100 -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestSubpolyhedra\TestSubPolyhedra.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,reduction=fast,diseq:false -define fastnooptions -define fastnooptions_cci1 -wp=false -joinsbeforewiden=1 -enforcefairjoin -timeout 100 -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ChunkerTest\ChunkerTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -bounds -steps=2",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ChunkerTest\ChunkerTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -bounds:type=subpolyhedra,reduction=complete",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\PostconditionInferenceNonNull.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -nonnull -infer methodensures -infer requires -define nonnull -prefrompost=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\PostconditionInferenceNonNull.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -nonnull -infer nonnullreturn -define nonnullreturn -prefrompost=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\PostconditionInferenceSymbolicReturn.cs",
                    clousotOptions: @"-infer autopropertiesensures -includesuggestionsinregression -suggest requires  -show validations -nonnull:noobl -bounds:noobl -suggest methodensures -infer symbolicreturn -prefrompost=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\PostconditionInference.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -check falsepostconditions  -show progress -bounds -infer methodensures -infer requires -prefrompost=true -show validations -nonnull:noobl",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new[] { @"System.core.dll", @"Microsoft.Csharp.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\Filtering.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show progress -bounds -nonnull -suggest methodensures -show validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\PostOnly.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -define postonly -bounds -infer methodensures -show validations -nonnull:noobl",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestArithmetics\TestArithmetic.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -bounds:noobl -arithmetic:obl=divOverflow,obl=intOverflow,obl=floatOverflow  -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestPreconditionInference\PreconditionInference.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -infer= -infer requires -infer propertyensures -infer methodensures",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestIterativeDomainApplication\IterativeDomainApplication.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:intervals -bounds:type:pentagons -bounds",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestOctagons\OctagonsTests.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type=Octagons -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestWarningMasking.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show validations -bounds -nonnull -wp=false -suggest assumes -suggest requiresbase -show unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScore.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel low -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:LOW /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScore.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel mediumlow -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:MEDIUMLOW /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScore.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel medium -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:MEDIUM /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScore.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel full -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:FULL /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScoreMissingPre.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel low -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:LOW /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScoreMissingPre.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel mediumlow -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:MEDIUMLOW /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScoreMissingPre.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel medium -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:MEDIUM /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScoreMissingPre.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel full -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:FULL /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFindCommonRoot.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -nonnull -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\IteratorAnalysis\Iterators.cs",
                    clousotOptions: @"-infer autopropertiesensures -show progress -analyze:movenext -nonnull -show validations -bounds:noobl,type=subpolyhedra,reduction=simplex,diseq=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: true,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\IteratorAnalysis\IteratorSimpleContract\IteratorSimpleContract.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -analyze:movenext  -nonnull:noobl -show validations -bounds:noobl,type=subpolyhedra,reduction=simplex,diseq=false -arrays",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: true,
                    skipSlicing: true);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\BasicContainersTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -bounds:noObl,diseq=false -show progress  -show validations;unreached -wp=false -define Intervals",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\ArrayWithNonNullAnalysis.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false -define NonNull",
                    useContractReferenceAssemblies: true,
                    useExe: true,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\SymbolicElementsTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds -show progress -show validations;unreached -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\Enumerables.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: true,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\UserRepros.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=true",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\Ernst.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: true);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\vstte.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -suggest methodensures -suggest requires -nonnull -bounds -arrays:arraypurity -check exists  -show unreached -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\BuiltInFunctions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds -show progress  -show unreached -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestBooleanConnectives\TestDisjunctions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -bounds -bounds:type=subpolyhedra,reduction=simplex -nonnull -wp=false -show progress -sortwarns=false  -show validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\Array.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\Environment.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\General.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\JoelBaranick.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\Math.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\Purity.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\Strings.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\UserFeedback.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Web.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\ReferenceToAllOOBC.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=simplex,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Windows.Forms.dll", @"System.Web.dll", @"WindowsBase.dll", @"System.Xml.Linq.dll", @"System.Web.dll", @"System.Security.dll", @"System.Drawing.dll", @"System.Configuration.dll", @"System.Configuration.Install.dll", @"System.Data.dll", @"Microsoft.VisualBasic.dll", @"Microsoft.VisualBasic.Compatibility.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\General.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow -define infer -infer requires",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\OutOfBand\Client\Client.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show progress -bounds:type=subpolyhedra,reduction=fast,noObl",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll", @"System.Core.dll" },
                    libPaths: new[] { @"Foxtrot\Tests\AssemblyWithContracts\bin\debug" },
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\Allen.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress  -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Data.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\XML.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress  -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Data.dll", @"System.Xml.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\PeterGolde.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Data.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\Misc.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress  -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Data.dll", @"System.Xml.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\Strilan.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/rootnamespace:VisualBasicTests /optimize",
                    references: new[] { @"System.Data.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB",
                    skipForCCI2: true,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\Wurz.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -bounds -nonnull  -show validations -show progress -regression -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: true,
                    compilerOptions: @"/rootnamespace:VisualBasicTests /optimize ExtraWurz.vb",
                    references: new[] { @"System.Deployment.dll", @"System.Configuration.dll", @"System.Drawing.dll", @"System.Windows.Forms.dll", @"System.Data.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ClassWithProtocolFinal.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -assemblyMode=standard -show:validations -bounds -nonnull:noobl -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Abbreviators.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull:noobl -bounds:noobl -show:validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\NoUpHavocMethods.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull:noobl -bounds:noobl -show:validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\JPGrk.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arithmetic:obl=intOverflow -show:validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\Herman.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\EmileVanGerwen.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -bounds:type=octagons,diseq=false -show:validations;unreached -timeout 300",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: true,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\VictorDerks.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -bounds:type=octagons,diseq=false -show:validations;unreached -timeout 3",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\Dluk.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\Strilanc.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\AlexeyR.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\JonathanTapicer.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\JamesAlbert.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show:unreached -check conditionsvalidity",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new[] { @"System.Core.dll", @"System.Net.Http", @"System.Web.http", @"System.XML.dll", @"System.Xml.Linq.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWebExamples\PexForFun.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds -bounds:type=subpolyhedra,reduction=simplex -arrays -arithmetic:obl=intOverflow -show:validations;unreached -timeout 300 -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestWebExamples\BagOfNonNegative.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds -bounds:type=subpolyhedra,reduction=simplex -arrays -check exists -arithmetic:obl=intOverflow -show:unreached -timeout 300 -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestMemoryConsumption\CausingOutOfMemory.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires   -bounds -define outofmem -show validations -timeout 3600 -show progress -adaptive",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Enum\EnumTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays  -nonnull:noobl -arrays -enum -bounds -show validations -show unreached -timeout 3600 -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Enum\Switch.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull:noobl -arrays -enum -bounds -premode:combined -show validations -show unreached -timeout 3600 -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TimeOut\TimeOut.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:crashWithTimeOut -show validations -show unreached -show progress ",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TimeOut\SymbolicTimeout.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show validations -bounds:type:SubPolyhedra,diseq:false,ch,infOct,reduction=simplex -define simplex-convexhull  -wp=false -enforcefairJoin=true -joinsbeforewiden 1 -symbolictimeout 1337 -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /define:SIMPLEXCONVEX",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Collections.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest requires -suggest arrayrequires -suggest arraypurity -nonnull -bounds -arrays -show validations -show unreached -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Purity.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest requires -suggest arrayrequires -suggest arraypurity -infer arraypurity -nonnull -bounds -arrays -show validations -show unreached -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Preconditions-AllPaths.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode allPaths -suggest requires -nonnull -bounds -show validations -show unreached -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Preconditions-Backwards.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -throwArgExceptionAsAssert -premode backwards -suggest requires -infer requires -nonnull -bounds -arithmetic:obl=intOverflow -show=!! -show progress -suggest assumes -suggest codefixes -maskverifiedrepairs=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Preconditions-BlogExample.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -throwArgExceptionAsAssert -premode backwards -suggest requires -infer requires -nonnull -bounds -show=!! -show progress -show errors -suggest assumes -suggest codefixes -maskverifiedrepairs=false  -missingPublicRequiresAreErrors  -suggest calleeassumes -suggest assumes -suggest requires -suggest necessaryensures -suggest readonlyfields  -infer requires -infer methodensures",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\SimplePreconditionPropagation.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -throwArgExceptionAsAssert -premode combined -suggest requires -infer requires -nonnull -bounds -show validations -show unreached -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\RedundantAssumptions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode allPaths -suggest requires -nonnull -bounds -show validations -show unreached -show progress -check assumptions -infer methodensures",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\AssertToContracts.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode combined -suggest requires -nonnull -bounds -show unreached -show progress -check assumptions -infer methodensures -suggest asserttocontracts",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\AssertToContracts.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode combined -suggest requires -nonnull -bounds -show unreached -show progress -check assumptions -infer methodensures -suggest asserttocontracts -inferencemode=aggressive",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /define:AGGRESSIVEINFERENCE",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Exist.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds:noobl -arrays -suggest arrayrequires -suggest methodensures -infer methodensures -prefrompost -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\ForAll.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds:noobl -nonnull:noobl -arrays:arraypurity  -suggest methodensures  -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\ObjectInvariants.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -infer objectinvariants -inferencemode aggressive -suggest assumes -disableForwardObjectInvariantInference",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: true);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\PodelskiEtAl.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -show progress -missingPublicRequiresAreErrors -infer objectinvariants -infer requires",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: true);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\ObjectInvariants_FromConstructor.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -infer objectinvariants -inferencemode aggressive -suggest assumes -suggest necessaryensures -suggest readonlyfields",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: true);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\ObjectInvariants_Forward.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -infer objectinvariants -suggest assumes -suggest necessaryensures -suggest readonlyfields",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: true);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Assume.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -suggest requiresbase -suggest assumes -suggest codefixes -maskverifiedrepairs=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Assume.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -suggest requiresbase -suggest assumes -premode backwards -suggest codefixes -maskverifiedrepairs=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /define:CODEFIXES",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\CalleeAssume.cs",
                    clousotOptions: @" -includesuggestionsinregression -bounds -nonnull -suggest calleeassumes -show progress  -premode:combined",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\NecessaryEnsures.cs",
                    clousotOptions: @" -includesuggestionsinregression -bounds -nonnull -suggest necessaryensures -show progress  -premode:combined",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\Existential.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -nonnull:noobl -bounds:noobl -arrays:arraypurity -check exists -show validations -show unreached -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: true);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\Markers.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -extractmethodmode=true -includesuggestionsinregression -suggest arrayrequires -nonnull:noobl -bounds:noobl -arrays:arraypurity -show unreached -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\RefinePrecondition.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -show unreached -show progress -suggest methodensures -extractmethodmoderefine TestArray",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\RefinePrecondition.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -show unreached -show progress -suggest methodensures -extractmethodmoderefine TestIf",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\RefinePrecondition.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -arithmetic:obl=intOverflow -show unreached -show progress -suggest methodensures -extractmethodmoderefine TestLoop",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\RefinePrecondition.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -arithmetic:obl=intOverflow -show unreached -show progress -suggest methodensures -extractmethodmoderefine ExtractFromLinearSearch",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\RefinePrecondition.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -arithmetic:obl=intOverflow -show unreached -show progress -suggest methodensures -extractmethodmoderefine ExtractFromMax",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\CodeFixes\CodeFixes.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arithmetic -suggest codefixes -maskverifiedrepairs=false -suggest methodensures -show progress -suggest objectinvariants -infer objectinvariants -suggest assumes -premode backwards -check falsepostconditions",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\CodeFixes\CodeFixes.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arithmetic -suggest codefixesshort -maskverifiedrepairs=false -suggest methodensures -show progress -suggest objectinvariants -infer objectinvariants -suggest assumes -premode backwards -check falsepostconditions",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize  /define:SHORTCODEFIXES",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\CountFiltering.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show progress -bounds -nonnull -suggest methodensures -infer methodensures -infer requires -prefrompost=true -show unreached  -check falsepostconditions",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\CheckFalsePostconditions.cs",
                    clousotOptions: @"-infer autopropertiesensures -includesuggestionsinregression -show progress -bounds -nonnull  -check falsepostconditions",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\vstte.demo.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -suggest methodensures -suggest requires -suggest codefixes -maskverifiedrepairs=false -premode combined -nonnull -bounds -arrays:arraypurity -check exists  -show unreached -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Strings\SimpleStrings.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -suggest methodensures -suggest requires -suggest codefixes -maskverifiedrepairs=false -premode combined -nonnull -bounds -arrays:arraypurity -check exists  -show unreached -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\GroupActions\GroupActions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest requires -suggest assumes -suggest codefixes -maskverifiedrepairs=false  -bounds -nonnull -sortwarns=false -show progress -groupactions=true -premode combined   ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\TestPreconditionInference\InferenceTrace.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show validations -bounds -nonnull -arrays -infer= -infer requires -infer propertyensures -infer methodensures -infer objectinvariants -show inferencetrace -repairs -premode combined",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\CallInvariants.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -bounds -nonnull -arrays -infer= -suggest callinvariants",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Cloudot\Basics.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -bounds -nonnull -arrays -infer methodensures",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Inference\MissingPublicPreconditionsAsWarnings.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest=!! -bounds -nonnull -infer requires -missingPublicRequiresAreErrors",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\Basic\SuggestionsAsWarnings\SuggestionsAsWarns.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest=!! -bounds -nonnull -infer requires -suggest readonlyfields -check assumptions -warnIfSuggest readonlyfields -warnifSuggest redundantassume",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipForCCI2: false,
                    skipSlicing: false);
                //yield return new Options(
                //    sourceFile: @"Microsoft.Research\RegressionTest\TestMemoryConsumption\CausingOutOfMemory.cs",
                //    clousotOptions: @"-suggest requires  -bounds -define outofmem -show validations -timeout 3600 -show progress",
                //    useContractReferenceAssemblies: false,
                //    useExe: false,
                //    compilerOptions: @"/optimize",
                //    references: new[] { @"System.Core.dll" },
                //    libPaths: new string[0],
                //    compilerCode: "CS",
                //    skipForCCI2: false,
                //    skipSlicing: false);
                //// F: commenting it for now, because there is some mismatch il the iloffsets
                //yield return new Options(
                //    sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\List.cs",
                //    clousotOptions: @"-suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false",
                //    useContractReferenceAssemblies: true,
                //    useExe: false,
                //    compilerOptions: @"/optimize",
                //    references: new[] { @"System.Core.dll" },
                //    libPaths: new string[0],
                //    compilerCode: "CS",
                //    skipForCCI2: false,
                //    skipSlicing: false);
                //yield return new Options(
                //    sourceFile: @"Microsoft.Research\RegressionTest\Inference\PreconditionNecessityCheck.cs",
                //    clousotOptions: @"-suggest requires -includesuggestionsinregression -premode backwards -suggest requires -infer requires -check inferredrequires -nonnull -bounds -show validations -show unreached -show progress ",
                //    useContractReferenceAssemblies: true,
                //    useExe: false,
                //    compilerOptions: @"/optimize",
                //    references: new[] { @"System.Core.dll" },
                //    libPaths: new string[0],
                //    compilerCode: "CS",
                //    skipForCCI2: false,
                //    skipSlicing: false);
            }
        }

        public static IEnumerable<object> TestRun
        {
            get
            {
                return Enumerable.Range(0, TestRunData.Count()).Select(i => new object[] { i });
            }
        }

        #endregion Test data

        #region Regular tests

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        public void Analyze1Z3FromSourcesV35(int testIndex)
        {
            var options = GrabTestOptions("Analyze1Z3FromSourcesV35", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @"v3.5";
            options.ContractFramework = @"v3.5";
            options.ClousotOptions += " -useZ3";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze(_testOutputHelper, options);
        }

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot1")]
        public void Analyze1FromSourcesV35(int testIndex)
        {
            var options = GrabTestOptions("Analyze1FromSourcesV35", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @"v3.5";
            options.ContractFramework = @"v3.5";
            if (!options.Skip && !options.SkipForNet35)
                TestDriver.BuildAndAnalyze(_testOutputHelper, options);
        }

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2")]
        public void Analyze2FromSourcesV35(int testIndex)
        {
            var options = GrabTestOptions("Analyze2FromSourcesV35", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @"v3.5";
            options.ContractFramework = @"v3.5";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2(_testOutputHelper, options);
        }

        [MemberData("TestRun")]
        [Theory]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot1")]
        public void Analyze1FromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Analyze1FromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze(_testOutputHelper, options);
        }

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot1")]
        public void Analyze1FromSourcesV40AgainstV35Contracts(int testIndex)
        {
            var options = GrabTestOptions("Analyze1FromSourcesV40AgainstV35Contracts", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @"v3.5";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze(_testOutputHelper, options);
        }

        #endregion

        #region Fast Tests

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Parallel")]
        public void Analyze2FastBeginParallelFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Analyze2FastBeginParallelFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.GenerateUniqueOutputName = true;
            options.Fast = true;
            if (!options.Skip)
                TestDriver.AsyncFast2(_testOutputHelper).BeginTest(options);
        }

        [MemberData("TestRun")]
        [Theory]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Parallel")]
        public void Analyze2FastEndParallelFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Analyze2FastEndParallelFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.GenerateUniqueOutputName = true;
            options.Fast = true;
            if (!options.Skip)
                TestDriver.AsyncFast2(_testOutputHelper).EndTest(options);
        }

        #endregion

        #region Service tests

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Service")]
        public void Analyze2ServiceSequentialFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Analyze2ServiceSequentialFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2S(_testOutputHelper, options);
        }

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Service"), Trait("Category", "Parallel")]
        public void Analyze2ServiceBeginParallelFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Analyze2ServiceBeginParallelFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.GenerateUniqueOutputName = true;
            if (!options.Skip)
                TestDriver.Async2S(_testOutputHelper).BeginTest(options);
        }

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Service"), Trait("Category", "Parallel")]
        public void Analyze2ServiceEndParallelFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Analyze2ServiceEndParallelFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.GenerateUniqueOutputName = true;
            if (!options.Skip)
                TestDriver.Async2S(_testOutputHelper).EndTest(options);
        }

        #endregion

        #region Slicing tests

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Slicer")]
        public void Slice2SequentialFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Slice2SequentialFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -workers:0";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(_testOutputHelper, options);
        }

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Slicer")]
        public void Slice2FastSequentialFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Slice2FastSequentialFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -workers:0";
            options.Fast = true;
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(_testOutputHelper, options);
        }

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot1"), Trait("Category", "Slicer")]
        public void Slice2Analyze1SequentialFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Slice2Analyze1SequentialFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst -workers:1";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze1Slicing(_testOutputHelper, options);
        }

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Slicer")]
        public void Slice2Analyze2SequentialFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Slice2Analyze2SequentialFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst -workers:1";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(_testOutputHelper, options);
        }

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Slicer")]
        public void Slice2Analyze2SequentialFromSourcesV40WithDiskCache(int testIndex)
        {
            var options = GrabTestOptions("Slice2Analyze2SequentialFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst -workers:1 -clearcache -cache";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(_testOutputHelper, options);
        }


        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Slicer")]
        public void Slice2Analyze2FastSequentialFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Slice2Analyze2FastSequentialFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst -workers:1";
            options.Fast = true;
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(_testOutputHelper, options);
        }

        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Slicer")]
        public void Slice2Analyze2FastSequentialFromSourcesV40WithDiskCache(int testIndex)
        {
            var options = GrabTestOptions("Slice2Analyze2FastSequentialFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst -workers:1 -clearcache -cache";
            options.Fast = true;
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(_testOutputHelper, options);
        }


        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot1"), Trait("Category", "Slicer"), Trait("Category", "Parallel")]
        public void Slice2Analyze1ParallelFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Slice2Analyze1ParallelFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze1Slicing(_testOutputHelper, options);
        }

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Slicer"), Trait("Category", "Parallel")]
        public void Slice2Analyze2ParallelFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Slice2Analyze2ParallelFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst";
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(_testOutputHelper, options);
        }

        //[MemberData("TestRun")]
        [Theory(Skip = "This test fails")]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot2"), Trait("Category", "Slicer"), Trait("Category", "Parallel")]
        public void Slice2Analyze2FastParallelFromSourcesV40(int testIndex)
        {
            var options = GrabTestOptions("Slice2Analyze2FastParallelFromSourcesV40", TestRunData.ElementAt(testIndex));
            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            options.ClousotOptions += " -sliceFirst";
            options.Fast = true;
            if (!options.Skip)
                TestDriver.BuildAndAnalyze2Slicing(_testOutputHelper, options);
        }

        #endregion

        public sealed class ClousotFixture : IDisposable
        {
            public void Dispose()
            {
                TestDriver.Cleanup();
            }
        }

        [CollectionDefinition("Clousot")]
        public class ClousotCollectionDefinition : ICollectionFixture<ClousotFixture>
        {
        }

        private Options GrabTestOptions(string testGroupName, Options originalOptions)
        {
            originalOptions.TestGroupName = testGroupName;
            CurrentGroupInfo = originalOptions.Group;
            return originalOptions;
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
