// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class ClousotTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ClousotTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        #region Test data

        private static IEnumerable<Options> TestRunDataSource
        {
            get
            {
                #region MaxJoins            
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxJoins\MaxJoins.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -define:regular -show:validations;unreached -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxJoins\MaxJoins0.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -define:regular -show:validations;unreached -missingPublicRequiresAreErrors=true -maxJoins 0",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxJoins\MaxJoins1.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -define:regular -show:validations;unreached -missingPublicRequiresAreErrors=true -maxJoins 1",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxJoins\MaxJoins2.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -define:regular -show:validations;unreached -missingPublicRequiresAreErrors=true -maxJoins 2",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                #endregion

                #region MaxWidenings
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxWidenings\MaxWidenings0.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show validations -bounds:type:SubPolyhedra,diseq:false,ch,infOct,reduction=simplex -define simplex-convexhull -wp=false -enforcefairJoin=true -joinsbeforewiden 1 -show progress -maxWidenings 2",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /define:SIMPLEXCONVEX",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxWidenings\MaxWidenings1.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show validations -bounds:type:SubPolyhedra,diseq:false,ch,infOct,reduction=simplex -define simplex-convexhull -wp=false -enforcefairJoin=true -joinsbeforewiden 1 -show progress -maxWidenings 3",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /define:SIMPLEXCONVEX",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxWidenings\MaxWidenings2.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:Intervals -arithmetic:type:Pentagons,obl=intOverflow -maxWidenings 0",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxWidenings\MaxWidenings3.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:Intervals -arithmetic:type:Pentagons,obl=intOverflow -maxWidenings 4",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxWidenings\MaxWidenings4.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:Intervals -arithmetic:type:Pentagons,obl=intOverflow -maxWidenings 10",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxWidenings\MaxWidenings5.cs",
                    clousotOptions: @"-nobox -nologo -nopex -stats=!! -stats controller -suggest=!! -infer=!! -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds:type:Intervals -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow -trace dfa -trace numerical -trace suspended -joinsbeforewiden 0 -maxWidenings 0",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxWidenings\MaxWidenings6.cs",
                    clousotOptions: @"-nobox -nologo -nopex -stats=!! -stats controller -suggest=!! -infer=!! -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds:type:Intervals -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow -trace dfa -trace numerical -trace suspended -joinsbeforewiden 1 -maxWidenings 0",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxWidenings\MaxWidenings7.cs",
                    clousotOptions: @"-nobox -nologo -nopex -infer autopropertiesensures -infer methodensures -infer nonnullreturn -infer propertyensures -suggest=!! -stats=!! -stats controller -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds -bounds:type=subpolyhedra,reduction=simplex,diseq=false -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow -trace dfa -trace numerical -trace suspended -joinsbeforewiden 1 -maxWidenings 0",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxWidenings\MaxWidenings8.cs",
                    clousotOptions: @"-nobox -nologo -nopex -infer autopropertiesensures -infer methodensures -infer nonnullreturn -infer propertyensures -suggest=!! -stats=!! -stats controller -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds:mpw=false -bounds:type=subpolyhedra,reduction=simplex,diseq=false,mpw=false -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow,mpw=false -trace dfa -trace numerical -trace suspended -joinsbeforewiden 1 -maxWidenings 1",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                #endregion

                #region MaxSteps
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxSteps\MaxSteps0.cs",
                    clousotOptions: @"-nobox -nologo -nopex -stats=!! -stats controller -suggest=!! -infer=!! -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds:type:Intervals -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow -trace suspended -maxSteps 0",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxSteps\MaxSteps1.cs",
                    clousotOptions: @"-nobox -nologo -nopex -stats=!! -stats controller -suggest=!! -infer=!! -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds:type:Intervals -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow -trace suspended -maxSteps 50",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxSteps\MaxSteps2.cs",
                    clousotOptions: @"-nobox -nologo -nopex -stats=!! -stats controller -suggest=!! -infer=!! -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds:type:Intervals -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow -trace suspended -maxSteps 200",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                #endregion

                #region MaxCalls
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxCalls\MaxCalls.cs",
                    clousotOptions: @"-nobox -nologo -nopex -stats=!! -stats controller -suggest=!! -infer=!! -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds:type:Intervals -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow -trace dfa -trace numerical -trace suspended -maxCalls 10",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxCalls\MaxCalls0.cs",
                    clousotOptions: @"-nobox -nologo -nopex -stats=!! -stats controller -suggest=!! -infer=!! -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds:type:Intervals -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow -trace dfa -trace numerical -trace suspended -maxCalls 0",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxCalls\MaxCalls1.cs",
                    clousotOptions: @"-nobox -nologo -nopex -stats=!! -stats controller -suggest=!! -infer=!! -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds:type:Intervals -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow -trace dfa -trace numerical -trace suspended -maxCalls 1",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                #endregion

                #region MaxFieldReads
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxFieldReads\MaxFieldReads0.cs",
                    clousotOptions: @"-nobox -nologo -nopex -stats=!! -stats controller -suggest=!! -infer=!! -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds:type:Intervals -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow -trace dfa -trace numerical -trace suspended -maxFieldReads 10",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\MaxFieldReads\MaxFieldReads1.cs",
                    clousotOptions: @"-nobox -nologo -nopex -stats=!! -stats controller -suggest=!! -infer=!! -warninglevel full -assemblyMode=standard -wp=true -premode combined -adaptive -show validations -nonnull -bounds:type:Intervals -arithmetic:type:Intervals,obl=div0,obl=negMin,obl=floatEq,obl=divOverflow,obl=intOverflow -trace dfa -trace numerical -trace suspended -maxFieldReads 0",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                #endregion

                yield return new Options( // #0
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Decimal.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show unreached -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.Linq.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #1
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\AssumeInvariant.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show unreached -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.Linq.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #2
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Herman.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.Linq.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #3
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\UserRepros\Domino.cs",
                    clousotOptions: @"-infer autopropertiesensures -show unreached -arrays -NonNull -Bounds -bounds:type=subpolyhedra,reduction=simplex,noObl -show progress -arithmetic:obl=intOverflow -adaptive -enum -check assumptions -suggest asserttocontracts -check conditionsvalidity -missingPublicRequiresAreErrors -suggest necessaryensures -suggest readonlyfields  -infer requires -infer methodensures -infer objectinvariants -premode:combined -warnifsuggest requires -suggest codefixes -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.Linq.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipFor: TestRuns.V40AgainstV35Contracts);
                yield return new Options( // #4
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\MultidimArrays.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull:noobl -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #5
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\NonNullTests\NonNull1.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -define:regular -show:validations;unreached -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #6
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\NonNullTests\NonNullNoWP.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -wp:false -define:regular -show:validations;unreached -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new[] { @"System.ComponentModel.Composition.dll", @"Microsoft.CSharp.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipFor: TestRuns.V40AgainstV35Contracts);
                yield return new Options( // #7
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestFrameworkOOB\NonNull.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.Linq.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestIntervals\IntervalsTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:Intervals -arithmetic:type:Pentagons,obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestKarr\KarrTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -define karronly -bounds:type:Karr,diseq=false -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #10
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestKarr\KarrTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -define karrothers -bounds:type:PentagonsKarrLeq  -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ExpressionSimplification\SimplifyExpression.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestBinarySearch\TestBinarySearch.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -Bounds:type=PentagonsKarrLeqOctagons",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BrianScenario\BrianScenario.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds  -nonnull -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BrianScenario\Protocols.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds  -nonnull -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\IfaceImplicitlyImplementedBug.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show validations -regression -show progress -nonnull",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll", @"System.Core.dll" },
                    libPaths: new[] { @"Foxtrot\Tests\AssemblyWithContracts\bin\debug" },
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\DoubleZero.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show validations -regression -show progress -nonnull",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll", @"System.Core.dll" },
                    libPaths: new[] { @"Foxtrot\Tests\AssemblyWithContracts\bin\debug" },
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\OperatorOverloading.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show validations  -show progress -nonnull -bounds",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll", @"System.Core.dll" },
                    libPaths: new[] { @"Foxtrot\Tests\AssemblyWithContracts\bin\debug" },
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\EnumerableAll.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ArrayForAll.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #20
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TypeSpecializations.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull:noobl -bounds:noobl -show progress -show validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\HeapCrash.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /r:System.Configuration.dll",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ForAll.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Mulder.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\IOperations.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -show:validations;unreached",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #25
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\NonNullTests\BrianStrelioff.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -define:regular -show:validations;unreached",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\NonNullTests\Inference.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -show validations;unreached -define infer -infer:requires;propertyensures;methodensures -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\SimpleArrayAccesses\SimpleArrayAccesses.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -Bounds:type=PentagonsKarrLeqOctagons -wp=false -timeout 100",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestUnsafe\BasicTests.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false -show progress -timeout 60 -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestUnsafe\ExamplesFromMscorlib.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestUnsafe\ExamplesFromRedhawk.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestUnsafe\SafeWrapper.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestUnsafe\Unsafe.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false -timeout 60 -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestCollections\List.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull:noObl -bounds:type=subpolyhedra",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestCollections\Stack.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull:noObl -bounds:type=subpolyhedra,reduction=simplex -timeout 200",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\FrameworkTests\BitArray.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -define bitarray -nonnull -bounds -bounds:type=subpolyhedra,diseq=false",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #36
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContractsWithClousot\Contracts.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show progress;validations -bounds",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #37
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContractsWithClousot\ExamplesFromPapers.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show progress;validations -bounds",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestNumericalDisequalities\SimpleBranching.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -arithmetic:obl=intOverflow -wp=false -bounds:noobl -missingPublicRequiresAreErrors=false",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\WeakestPreconditionTests\Paths.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\WeakestPreconditionTests\Disjuncts.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\WeakestPreconditionTests\RichardCook.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\WeakestPreconditionTests\ADomainsInterface.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\AssertOverloads.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\ContractVerificationAttribute.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\CompilerGeneratedAttribute.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -nonnull -show progress -missingPublicRequiresAreErrors=true",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\Disjunctions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\EnsuresOnOutByRef.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\Expressions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\GenericTests.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\HeapAnalysis.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\InterfaceContracts.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\Inference.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -nonnull:noObl -define inference -infer requires -infer propertyensures -infer methodensures",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\Invariants.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\Iterators.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -arrays -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\LegacyRequires.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\OldTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress -assemblyMode=standard",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\ParameterInEnsures.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\PureFunction.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\ReceiverHavoc.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\RecursiveSubroutines.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\BasicInfrastructure\StructTests.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestSubpolyhedra\TestSubPolyhedra.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,diseq:false,ch,infOct,reduction=simplex -define simplex-convexhull  -wp=false -enforcefairJoin=true -joinsbeforewiden 1 -timeout 100 -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize /define:SIMPLEXCONVEX",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestSubpolyhedra\TestSubPolyhedra.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,reduction=fast,diseq:false,ch,infOct -define fast -wp=false  -enforcefairJoin=true -joinsbeforewiden 1 -timeout 100 -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestSubpolyhedra\TestSubPolyhedra.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,diseq:false,infOct,reduction=simplex -define simplex -wp=false -enforcefairJoin=true -joinsbeforewiden 1 -timeout 100 -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestSubpolyhedra\TestSubPolyhedra.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,reduction=fast,diseq:false -define fastnooptions -define fastnooptions_cci1 -wp=false -joinsbeforewiden=1 -enforcefairjoin -timeout 100 -show progress",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #66
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ChunkerTest\ChunkerTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -bounds -steps=2",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #67
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ChunkerTest\ChunkerTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -bounds:type=subpolyhedra,reduction=complete",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestPostconditionInference\PostconditionInferenceNonNull.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -nonnull -infer methodensures -infer requires -define nonnull -prefrompost=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestPostconditionInference\PostconditionInferenceNonNull.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -nonnull -infer nonnullreturn -define nonnullreturn -prefrompost=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestPostconditionInference\PostconditionInferenceSymbolicReturn.cs",
                    clousotOptions: @"-infer autopropertiesensures -includesuggestionsinregression -suggest requires  -show validations -nonnull:noobl -bounds:noobl -suggest methodensures -infer symbolicreturn -prefrompost=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestPostconditionInference\PostconditionInference.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -check falsepostconditions  -show progress -bounds -infer methodensures -infer requires -prefrompost=true -show validations -nonnull:noobl",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new[] { @"System.core.dll", @"Microsoft.Csharp.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestPostconditionInference\Filtering.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show progress -bounds -nonnull -suggest methodensures -show validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestPostconditionInference\PostOnly.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -define postonly -bounds -infer methodensures -show validations -nonnull:noobl",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestArithmetics\TestArithmetic.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -bounds:noobl -arithmetic:obl=divOverflow,obl=intOverflow,obl=floatOverflow  -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestPreconditionInference\PreconditionInference.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds -infer= -infer requires -infer propertyensures -infer methodensures",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestIterativeDomainApplication\IterativeDomainApplication.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type:intervals -bounds:type:pentagons -bounds",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestOctagons\OctagonsTests.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -bounds:type=Octagons -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWarningMasking\TestWarningMasking.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show validations -bounds -nonnull -wp=false -suggest assumes -suggest requiresbase -show unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWarningMasking\TestFilteringWithScore.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel low -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:LOW /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWarningMasking\TestFilteringWithScore.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel mediumlow -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:MEDIUMLOW /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWarningMasking\TestFilteringWithScore.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel medium -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:MEDIUM /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWarningMasking\TestFilteringWithScore.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel full -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:FULL /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWarningMasking\TestFilteringWithScoreMissingPre.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel low -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:LOW /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWarningMasking\TestFilteringWithScoreMissingPre.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel mediumlow -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:MEDIUMLOW /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWarningMasking\TestFilteringWithScoreMissingPre.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel medium -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:MEDIUM /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #86
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWarningMasking\TestFilteringWithScoreMissingPre.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel full -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/define:FULL /optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS",
                    skipFor: TestRuns.V40AgainstV35Contracts);
                yield return new Options( // #87
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWarningMasking\TestFindCommonRoot.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations -nonnull -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\IteratorAnalysis\Iterators.cs",
                    clousotOptions: @"-infer autopropertiesensures -show progress -analyze:movenext -nonnull -show validations -bounds:noobl,type=subpolyhedra,reduction=simplex,diseq=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #89
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\IteratorSimpleContract\IteratorSimpleContract.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -analyze:movenext  -nonnull:noobl -show validations -bounds:noobl,type=subpolyhedra,reduction=simplex,diseq=false -arrays",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContainers\BasicContainersTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -bounds:noObl,diseq=false -show progress  -show validations;unreached -wp=false -define Intervals",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContainers\ArrayWithNonNullAnalysis.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false -define NonNull",
                    useContractReferenceAssemblies: true,
                    useExe: true,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContainers\SymbolicElementsTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds -show progress -show validations;unreached -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContainers\Enumerables.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContainers\UserRepros.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=true",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContainers\Ernst.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContainers\vstte.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -suggest methodensures -suggest requires -nonnull -bounds -arrays:arraypurity -check exists  -show unreached -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContainers\BuiltInFunctions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds -show progress  -show unreached -wp=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestBooleanConnectives\TestDisjunctions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -bounds -bounds:type=subpolyhedra,reduction=simplex -nonnull -wp=false -show progress -sortwarns=false  -show validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestFrameworkOOB\Array.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestFrameworkOOB\Environment.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestFrameworkOOB\General.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestFrameworkOOB\JoelBaranick.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestFrameworkOOB\Math.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestFrameworkOOB\Purity.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestFrameworkOOB\Strings.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #106
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestFrameworkOOB\UserFeedback.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Web.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestFrameworkOOB\ReferenceToAllOOBC.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=simplex,noObl  -show progress -arithmetic:obl=intOverflow",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Windows.Forms.dll", @"System.Web.dll", @"WindowsBase.dll", @"System.Xml.Linq.dll", @"System.Web.dll", @"System.Security.dll", @"System.Drawing.dll", @"System.Configuration.dll", @"System.Configuration.Install.dll", @"System.Data.dll", @"Microsoft.VisualBasic.dll", @"Microsoft.VisualBasic.Compatibility.dll", @"System.Xml.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestFrameworkOOB\General.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow -define infer -infer requires",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #109
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\OutOfBand\Client.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -show progress -bounds:type=subpolyhedra,reduction=fast,noObl",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"",
                    references: new[] { @"AssemblyWithContracts.dll", @"System.Core.dll" },
                    libPaths: new[] { @"Foxtrot\Tests\AssemblyWithContracts\bin\debug" },
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\VisualBasicTests\Allen.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress  -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Data.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\VisualBasicTests\XML.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress  -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Data.dll", @"System.Xml.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\VisualBasicTests\PeterGolde.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Data.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB");
                yield return new Options( // #113
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\VisualBasicTests\Misc.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress  -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Data.dll", @"System.Xml.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\VisualBasicTests\Strilan.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/rootnamespace:VisualBasicTests /optimize",
                    references: new[] { @"System.Data.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\VisualBasicTests\Wurz.vb",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -bounds -nonnull  -show validations -show progress -regression -assemblyMode=standard",
                    useContractReferenceAssemblies: true,
                    useExe: true,
                    compilerOptions: @"/rootnamespace:VisualBasicTests /optimize ExtraWurz.vb",
                    references: new[] { @"System.Deployment.dll", @"System.Configuration.dll", @"System.Drawing.dll", @"System.Windows.Forms.dll", @"System.Data.dll", @"System.Xml.Linq.dll", @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "VB");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ClassWithProtocolFinal.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires  -assemblyMode=standard -show:validations -bounds -nonnull:noobl -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Abbreviators.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull:noobl -bounds:noobl -show:validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\NoUpHavocMethods.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull:noobl -bounds:noobl -show:validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\UserRepros\JPGrk.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arithmetic:obl=intOverflow -show:validations",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\UserRepros\Herman.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\UserRepros\EmileVanGerwen.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -bounds:type=octagons,diseq=false -show:validations;unreached -timeout 300",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\UserRepros\VictorDerks.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -bounds:type=octagons,diseq=false -show:validations;unreached -timeout 3",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\UserRepros\Dluk.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\UserRepros\Strilanc.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\UserRepros\AlexeyR.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\UserRepros\JonathanTapicer.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\UserRepros\JamesAlbert.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show:unreached -check conditionsvalidity",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /unsafe",
                    references: new[] { @"System.Core.dll", @"System.Net.Http", @"System.Web.http", @"System.XML.dll", @"System.Xml.Linq.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWebExamples\PexForFun.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds -bounds:type=subpolyhedra,reduction=simplex -arrays -arithmetic:obl=intOverflow -show:validations;unreached -timeout 300 -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestWebExamples\BagOfNonNegative.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds -bounds:type=subpolyhedra,reduction=simplex -arrays -check exists -arithmetic:obl=intOverflow -show:unreached -timeout 300 -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestMemoryConsumption\CausingOutOfMemory.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires   -bounds -define outofmem -show validations -timeout 3600 -show progress -adaptive",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Enum\EnumTest.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays  -nonnull:noobl -arrays -enum -bounds -show validations -show unreached -timeout 3600 -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Enum\Switch.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -arrays -nonnull:noobl -arrays -enum -bounds -premode:combined -show validations -show unreached -timeout 3600 -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TimeOut\TimeOut.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -nonnull -bounds:crashWithTimeOut -show validations -show unreached -show progress ",
                    useContractReferenceAssemblies: false,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\Collections.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest requires -suggest arrayrequires -suggest arraypurity -nonnull -bounds -arrays -show validations -show unreached -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\Purity.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest requires -suggest arrayrequires -suggest arraypurity -infer arraypurity -nonnull -bounds -arrays -show validations -show unreached -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\Preconditions-AllPaths.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode allPaths -suggest requires -nonnull -bounds -show validations -show unreached -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\Preconditions-Backwards.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -throwArgExceptionAsAssert -premode backwards -suggest requires -infer requires -nonnull -bounds -arithmetic:obl=intOverflow -show=!! -show progress -suggest assumes -suggest codefixes -maskverifiedrepairs=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\Preconditions-BlogExample.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -throwArgExceptionAsAssert -premode backwards -suggest requires -infer requires -nonnull -bounds -show=!! -show progress -show errors -suggest assumes -suggest codefixes -maskverifiedrepairs=false  -missingPublicRequiresAreErrors  -suggest calleeassumes -suggest assumes -suggest requires -suggest necessaryensures -suggest readonlyfields  -infer requires -infer methodensures",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\SimplePreconditionPropagation.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -throwArgExceptionAsAssert -premode combined -suggest requires -infer requires -nonnull -bounds -show validations -show unreached -show progress ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\RedundantAssumptions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode allPaths -suggest requires -nonnull -bounds -show validations -show unreached -show progress -check assumptions -infer methodensures",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\AssertToContracts.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode combined -suggest requires -nonnull -bounds -show unreached -show progress -check assumptions -infer methodensures -suggest asserttocontracts",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\AssertToContracts.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode combined -suggest requires -nonnull -bounds -show unreached -show progress -check assumptions -infer methodensures -suggest asserttocontracts -inferencemode=aggressive",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /define:AGGRESSIVEINFERENCE",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\Exist.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds:noobl -arrays -suggest arrayrequires -suggest methodensures -infer methodensures -prefrompost -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\ForAll.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds:noobl -nonnull:noobl -arrays:arraypurity  -suggest methodensures  -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\ObjectInvariants.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -infer objectinvariants -inferencemode aggressive -suggest assumes -disableForwardObjectInvariantInference",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\PodelskiEtAl.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -show progress -missingPublicRequiresAreErrors -infer objectinvariants -infer requires",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\ObjectInvariants_FromConstructor.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -infer objectinvariants -inferencemode aggressive -suggest assumes -suggest necessaryensures -suggest readonlyfields",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\ObjectInvariants_Forward.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -infer objectinvariants -suggest assumes -suggest necessaryensures -suggest readonlyfields",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\Assume.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -suggest requiresbase -suggest assumes -suggest codefixes -maskverifiedrepairs=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\Assume.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -suggest requiresbase -suggest assumes -premode backwards -suggest codefixes -maskverifiedrepairs=false",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize /define:CODEFIXES",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\CalleeAssume.cs",
                    clousotOptions: @" -includesuggestionsinregression -bounds -nonnull -suggest calleeassumes -show progress  -premode:combined",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\NecessaryEnsures.cs",
                    clousotOptions: @" -includesuggestionsinregression -bounds -nonnull -suggest necessaryensures -show progress  -premode:combined",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContainers\Existential.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -nonnull:noobl -bounds:noobl -arrays:arraypurity -check exists -show validations -show unreached -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ExtractMethod\Markers.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -extractmethodmode=true -includesuggestionsinregression -suggest arrayrequires -nonnull:noobl -bounds:noobl -arrays:arraypurity -show unreached -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ExtractMethod\RefinePrecondition.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -show unreached -show progress -suggest methodensures -extractmethodmoderefine TestArray",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ExtractMethod\RefinePrecondition.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -show unreached -show progress -suggest methodensures -extractmethodmoderefine TestIf",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ExtractMethod\RefinePrecondition.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -arithmetic:obl=intOverflow -show unreached -show progress -suggest methodensures -extractmethodmoderefine TestLoop",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ExtractMethod\RefinePrecondition.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -arithmetic:obl=intOverflow -show unreached -show progress -suggest methodensures -extractmethodmoderefine ExtractFromLinearSearch",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ExtractMethod\RefinePrecondition.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -arithmetic:obl=intOverflow -show unreached -show progress -suggest methodensures -extractmethodmoderefine ExtractFromMax",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #160
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\CodeFixes\CodeFixes.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arithmetic -suggest codefixes -maskverifiedrepairs=false -suggest methodensures -show progress -suggest objectinvariants -infer objectinvariants -suggest assumes -premode backwards -check falsepostconditions",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\CodeFixes\CodeFixes.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arithmetic -suggest codefixesshort -maskverifiedrepairs=false -suggest methodensures -show progress -suggest objectinvariants -infer objectinvariants -suggest assumes -premode backwards -check falsepostconditions",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize  /define:SHORTCODEFIXES",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestPostconditionInference\CountFiltering.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show progress -bounds -nonnull -suggest methodensures -infer methodensures -infer requires -prefrompost=true -show unreached  -check falsepostconditions",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestPostconditionInference\CheckFalsePostconditions.cs",
                    clousotOptions: @"-infer autopropertiesensures -includesuggestionsinregression -show progress -bounds -nonnull  -check falsepostconditions",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContainers\vstte.demo.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -suggest methodensures -suggest requires -suggest codefixes -maskverifiedrepairs=false -premode combined -nonnull -bounds -arrays:arraypurity -check exists  -show unreached -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Strings\SimpleStrings.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -suggest methodensures -suggest requires -suggest codefixes -maskverifiedrepairs=false -premode combined -nonnull -bounds -arrays:arraypurity -check exists  -show unreached -show progress",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\GroupActions\GroupActions.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest requires -suggest assumes -suggest codefixes -maskverifiedrepairs=false  -bounds -nonnull -sortwarns=false -show progress -groupactions=true -premode combined   ",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestPreconditionInference\InferenceTrace.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show validations -bounds -nonnull -arrays -infer= -infer requires -infer propertyensures -infer methodensures -infer objectinvariants -show inferencetrace -repairs -premode combined",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\CallInvariants.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -bounds -nonnull -arrays -infer= -suggest callinvariants",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Cloudot\Basics.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -bounds -nonnull -arrays -infer methodensures",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options(
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\MissingPublicPreconditionsAsWarnings.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest=!! -bounds -nonnull -infer requires -missingPublicRequiresAreErrors",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new string[0],
                    libPaths: new string[0],
                    compilerCode: "CS");
                yield return new Options( // #171
                    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\SuggestionsAsWarnings\SuggestionsAsWarns.cs",
                    clousotOptions: @"-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest=!! -bounds -nonnull -infer requires -suggest readonlyfields -check assumptions -warnIfSuggest readonlyfields -warnifSuggest redundantassume",
                    useContractReferenceAssemblies: true,
                    useExe: false,
                    compilerOptions: @"/optimize",
                    references: new[] { @"System.Core.dll" },
                    libPaths: new string[0],
                    compilerCode: "CS");
                //yield return new Options( // #172
                //    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestMemoryConsumption\CausingOutOfMemory.cs",
                //    clousotOptions: @"-suggest requires  -bounds -define outofmem -show validations -timeout 3600 -show progress",
                //    useContractReferenceAssemblies: false,
                //    useExe: false,
                //    compilerOptions: @"/optimize",
                //    references: new[] { @"System.Core.dll" },
                //    libPaths: new string[0],
                //    compilerCode: "CS");
                //yield return new Options(
                //    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TestContainers\List.cs",
                //    clousotOptions: @"-suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false",
                //    useContractReferenceAssemblies: true,
                //    useExe: false,
                //    compilerOptions: @"/optimize",
                //    references: new[] { @"System.Core.dll" },
                //    libPaths: new string[0],
                //    compilerCode: "CS");
                //yield return new Options(
                //    sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Inference\PreconditionNecessityCheck.cs",
                //    clousotOptions: @"-suggest requires -includesuggestionsinregression -premode backwards -suggest requires -infer requires -check inferredrequires -nonnull -bounds -show validations -show unreached -show progress ",
                //    useContractReferenceAssemblies: true,
                //    useExe: false,
                //    compilerOptions: @"/optimize",
                //    references: new[] { @"System.Core.dll" },
                //    libPaths: new string[0],
                //    compilerCode: "CS");
            }
        }

        private static readonly Options[] TestRunData = TestRunDataSource.ToArray();

        public static IEnumerable<object> TestRun
        {
            get
            {
                return TestRunData.Select((options, index) => new object[] { index.ToString("00000"), index, Path.GetFileNameWithoutExtension(options.SourceFile) + "_" + index});
            }
        }

        #endregion Test data

        #region Tests

        [MemberData("TestRun")]
        [Theory]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot1"), Trait("Category", "Maintenance")]
        public void SourceFileExists(string sortKey, int testIndex, string testName)
        {
            var options = TestRunData[testIndex];

            var sourceFile = options.MakeAbsolute(options.SourceFile);

            Assert.True(File.Exists(sourceFile));
        }

        [MemberData("TestRun")]
        [Theory]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot1")]
        public void V40(string sortKey, int testIndex, string testName)
        {
            var options = TestRunData[testIndex];

            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @".NETFramework\v4.0";
            
            if (!options.SkipFor.HasFlag(TestRuns.V40))
                TestDriver.BuildAndAnalyze(_testOutputHelper, options, testName, testIndex);
        }

        [MemberData("TestRun")]
        [Theory]
        [Trait("Category", "StaticChecker"), Trait("Category", "Clousot1")]
        public void V40AgainstV35Contracts(string sortKey, int testIndex, string testName)
        {
            var options = TestRunData[testIndex];

            options.BuildFramework = @".NETFramework\v4.0";
            options.ContractFramework = @"v3.5";
            
            if (!options.SkipFor.HasFlag(TestRuns.V40AgainstV35Contracts))
                TestDriver.BuildAndAnalyze(_testOutputHelper, options, testName, testIndex);
        }

        #endregion
    }
}
