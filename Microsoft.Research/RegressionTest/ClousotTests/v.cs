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
using ClousotTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

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

    private static readonly Options[] TestOptions =
    {
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Decimal.cs",
            compilerOptions: "",
            libPaths: new string[0],
            references: new[] { "System.Xml.Linq.dll", "System.Xml.dll", "System.Core.dll" },
            options: "-infer autopropertiesensures -suggest requires -show unreached -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
            contractReferenceAssemblies: true,
            exe: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\AssumeInvariant.cs",
            compilerOptions: "",
            libPaths: new string[0],
            references: new[] { "System.Xml.Linq.dll", "System.Xml.dll", "System.Core.dll" },
            options: "-infer autopropertiesensures -suggest requires  -show unreached -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
            contractReferenceAssemblies: true,
            exe: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Herman.cs",
            compilerOptions: "",
            libPaths: new string[0],
            references: new[] { "System.Xml.Linq.dll", "System.Xml.dll", "System.Core.dll" },
            options: "-infer autopropertiesensures -suggest requires  -show unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
            contractReferenceAssemblies: true,
            exe: false,
            skipCCI2: true),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\Domino.cs",
            compilerOptions: "",
            libPaths: new string[0],
            references: new[] { "System.Xml.Linq.dll", "System.Xml.dll", "System.Core.dll" },
            options: "-infer autopropertiesensures -show unreached -arrays -NonNull -Bounds -bounds:type=subpolyhedra,reduction=simplex,noObl -show progress -arithmetic:obl=intOverflow -adaptive -enum -check assumptions -suggest asserttocontracts -check conditionsvalidity -missingPublicRequiresAreErrors -suggest necessaryensures -suggest readonlyfields  -infer requires -infer methodensures -infer objectinvariants -premode:combined -warnifsuggest requires -suggest codefixes -show progress ",
            contractReferenceAssemblies: true,
            exe: false,
            skipCCI2: true),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\MultidimArrays.cs",
            compilerOptions: "",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires -nonnull:noobl -show:validations;unreached",
            contractReferenceAssemblies: true,
            exe: false,
            compiler: "CS"),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\NonNullTests\NonNull1.cs",
            compilerOptions: "/unsafe",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires -nonnull -define:regular -show:validations;unreached -missingPublicRequiresAreErrors=true",
            contractReferenceAssemblies: false,
            exe: false,
            compiler: "CS"),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\NonNullTests\NonNullNoWP.cs",
            compilerOptions: "/unsafe",
            libPaths: new string[0],
            references: new[] { "System.ComponentModel.Composition.dll", "Microsoft.CSharp.dll", "System.Core.dll" },
            options: "-infer autopropertiesensures -suggest requires -nonnull -wp:false -define:regular -show:validations;unreached -missingPublicRequiresAreErrors=true",
            contractReferenceAssemblies: true,
            exe: false,
            compiler: "CS"),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\NonNull.cs",
            compilerOptions: "",
            libPaths: new string[0],
            references: new[] { "System.Xml.Linq.dll", "System.Xml.dll", "System.Core.dll" },
            options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow",
            contractReferenceAssemblies: true,
            exe: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestIntervals\IntervalsTest.cs",
            compilerOptions: "/optimize /unsafe",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires  -show validations -bounds:type:Intervals -arithmetic:type:Pentagons,obl=intOverflow",
            contractReferenceAssemblies: true),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestKarr\KarrTest.cs",
            compilerOptions: "/optimize /unsafe",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires  -show validations -define karronly -bounds:type:Karr,diseq=false -missingPublicRequiresAreErrors=true",
            contractReferenceAssemblies: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestKarr\KarrTest.cs",
            compilerOptions: "/optimize /unsafe",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires  -show validations -define karrothers -bounds:type:PentagonsKarrLeq  -missingPublicRequiresAreErrors=true",
            contractReferenceAssemblies: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\Basic\ExpressionSimplification\SimplifyExpression.cs",
            compilerOptions: "/optimize /unsafe",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull",
            contractReferenceAssemblies: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestBinarySearch\TestBinarySearch.cs",
            compilerOptions: "/optimize /unsafe",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires  -show validations -Bounds:type=PentagonsKarrLeqOctagons",
            contractReferenceAssemblies: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\Basic\BrianScenario\BrianScenario.cs",
            compilerOptions: "/optimize /unsafe",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires  -show validations -bounds  -nonnull -show progress",
            contractReferenceAssemblies: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\Basic\BrianScenario\Protocols.cs",
            compilerOptions: "/optimize /unsafe",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires  -show validations -bounds  -nonnull -show progress",
            contractReferenceAssemblies: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\IfaceImplicitlyImplementedBug.cs",
            compilerOptions: "",
            libPaths: new[] { @"Foxtrot\Tests\AssemblyWithContracts\bin\debug" },
            references: new[] { "AssemblyWithContracts.dll", "System.Core.dll" },
            options: "-infer autopropertiesensures -suggest requires -show validations -regression -show progress -nonnull",
            contractReferenceAssemblies: true,
            exe: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\DoubleZero.cs",
            compilerOptions: "",
            libPaths: new[] { @"Foxtrot\Tests\AssemblyWithContracts\bin\debug" },
            references: new[] { "AssemblyWithContracts.dll;System.Core.dll" },
            options: "-infer autopropertiesensures -suggest requires -show validations -regression -show progress -nonnull",
            contractReferenceAssemblies: true,
            exe: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\OperatorOverloading.cs",
            compilerOptions: "",
            libPaths: new[] { @"Foxtrot\Tests\AssemblyWithContracts\bin\debug" },
            references: new[] { "AssemblyWithContracts.dll", "System.Core.dll" },
            options: "-infer autopropertiesensures -suggest requires -show validations  -show progress -nonnull -bounds",
            contractReferenceAssemblies: true,
            exe: false),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\EnumerableAll.cs",
            compilerOptions: "/optimize",
            libPaths: new string[0],
            references: new[] { "System.Core.dll" },
            options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
            contractReferenceAssemblies: true,
            exe: false,
            compiler: "CS"),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ArrayForAll.cs",
            compilerOptions: "/optimize",
            libPaths: new string[0],
            references: new[] { "System.Core.dll" },
            options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
            contractReferenceAssemblies: true,
            exe: false,
            compiler: "CS"),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\TypeSpecializations.cs",
            compilerOptions: "/optimize",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires -nonnull:noobl -bounds:noobl -show progress -show validations",
            contractReferenceAssemblies: true,
            exe: false,
            compiler: "CS"),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\HeapCrash.cs",
            compilerOptions: "/optimize /r:System.Configuration.dll",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
            contractReferenceAssemblies: true,
            exe: false,
            compiler: "CS"),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ForAll.cs",
            compilerOptions: "/optimize",
            libPaths: new string[0],
            references: new[] { "System.Core.dll" },
            options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show progress",
            contractReferenceAssemblies: true,
            exe: false,
            compiler: "CS"),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Mulder.cs",
            compilerOptions: "/optimize",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -show progress",
            contractReferenceAssemblies: true,
            exe: false,
            compiler: "CS"),
        new Options(
            sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\IOperations.cs",
            compilerOptions: "",
            libPaths: new string[0],
            references: new[] { "" },
            options: "-infer autopropertiesensures -suggest requires -bounds -show:validations;unreached",
            contractReferenceAssemblies: false,
            exe: false,
            compiler: "CS"),
#if false
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\NonNullTests\BrianStrelioff.cs"
     compilerOptions: "/unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -nonnull -define:regular -show:validations;unreached"
     contractReferenceAssemblies: false"
     exe: false"
     compiler: "CS"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\NonNullTests\Inference.cs"
     compilerOptions: "/unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -nonnull -show validations;unreached -define infer -infer:requires;propertyensures;methodensures -missingPublicRequiresAreErrors=true"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\SimpleArrayAccesses\SimpleArrayAccesses.cs"
     compilerOptions: "/optimize /unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -Bounds:type=PentagonsKarrLeqOctagons -wp=false -timeout 100"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestUnsafe\BasicTests.cs"
     compilerOptions: "/optimize /unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false -show progress -timeout 60 -missingPublicRequiresAreErrors=true"
     contractReferenceAssemblies: false"
     exe: false"
     compiler: "CS"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestUnsafe\ExamplesFromMscorlib.cs"
     compilerOptions: "/optimize /unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestUnsafe\ExamplesFromRedhawk.cs"
     compilerOptions: "/optimize /unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestUnsafe\SafeWrapper.cs"
     compilerOptions: "/optimize /unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestUnsafe\Unsafe.cs"
     compilerOptions: "/optimize /unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -buffers:type=subpolyhedra,fastcheck=false  -show validations -wp=false -timeout 60 -missingPublicRequiresAreErrors=true"
     contractReferenceAssemblies: false"),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestCollections\List.cs"
    compilerOptions: ""
    libPaths: new string[0],
    references: new[] { ""
    options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull:noObl -bounds:type=subpolyhedra"
    contractReferenceAssemblies: true"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestCollections\Stack.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull:noObl -bounds:type=subpolyhedra,reduction=simplex -timeout 200"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\FrameworkTests\BitArray.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -define bitarray -nonnull -bounds -bounds:type=subpolyhedra,diseq=false"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestContractsWithClousot\TestContractsWithClousot\Contracts.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show progress;validations -bounds"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestContractsWithClousot\TestContractsWithClousot\ExamplesFromPapers.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show progress;validations -bounds"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestNumericalDisequalities\SimpleBranching.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show validations -arithmetic:obl=intOverflow -wp=false -bounds:noobl -missingPublicRequiresAreErrors=false"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\WeakestPreconditionTests\Paths.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\WeakestPreconditionTests\Disjuncts.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds"
     contractReferenceAssemblies: false"),
  new Options(
      sourceFile: @"Microsoft.Research\RegressionTest\WeakestPreconditionTests\RichardCook.cs"
      compilerOptions: ""
      libPaths: new string[0],
      references: new[] { ""
      options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds"
      contractReferenceAssemblies: true"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\WeakestPreconditionTests\ADomainsInterface.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -bounds"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\AssertOverloads.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\ContractVerificationAttribute.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\CompilerGeneratedAttribute.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -nonnull -show progress -missingPublicRequiresAreErrors=true"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\Disjunctions.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\EnsuresOnOutByRef.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\Expressions.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\GenericTests.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\HeapAnalysis.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\InterfaceContracts.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\Inference.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -nonnull:noObl -define inference -infer requires -infer propertyensures -infer methodensures"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\Invariants.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\Iterators.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -arrays -define regular -show progress"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\LegacyRequires.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\OldTest.cs"
     compilerOptions: "/unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress -assemblyMode=standard"
     contractReferenceAssemblies: false"
     skipCCI2: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\ParameterInEnsures.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\PureFunction.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\ReceiverHavoc.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\RecursiveSubroutines.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\BasicInfrastructure\StructTests.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -nonnull -define regular -show progress"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestSubpolyhedra\TestSubPolyhedra.cs"
     compilerOptions: "/optimize /define:SIMPLEXCONVEX"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,diseq:false,ch,infOct,reduction=simplex -define simplex-convexhull  -wp=false -enforcefairJoin=true -joinsbeforewiden 1 -timeout 100 -show progress"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestSubpolyhedra\TestSubPolyhedra.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,reduction=fast,diseq:false,ch,infOct -define fast -wp=false  -enforcefairJoin=true -joinsbeforewiden 1 -timeout 100 -show progress"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestSubpolyhedra\TestSubPolyhedra.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,diseq:false,infOct,reduction=simplex -define simplex -wp=false -enforcefairJoin=true -joinsbeforewiden 1 -timeout 100 -show progress"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestSubpolyhedra\TestSubPolyhedra.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds:type:SubPolyhedra,reduction=fast,diseq:false -define fastnooptions -define fastnooptions_cci1 -wp=false -joinsbeforewiden=1 -enforcefairjoin -timeout 100 -show progress"
     contractReferenceAssemblies: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\ChunkerTest\ChunkerTest.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -bounds -steps=2"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\ChunkerTest\ChunkerTest.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -bounds:type=subpolyhedra,reduction=complete"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\PostconditionInferenceNonNull.cs"
     compilerOptions: "/optimize /unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -nonnull -infer methodensures -infer requires -define nonnull -prefrompost=false"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\PostconditionInferenceNonNull.cs"
     compilerOptions: "/optimize /unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -nonnull -infer nonnullreturn -define nonnullreturn -prefrompost=false"
     contractReferenceAssemblies: true"
  ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\PostconditionInferenceSymbolicReturn.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -includesuggestionsinregression -suggest requires  -show validations -nonnull:noobl -bounds:noobl -suggest methodensures -infer symbolicreturn -prefrompost=false"
     contractReferenceAssemblies: true"
  ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\PostconditionInference.cs"
     compilerOptions: "/optimize /unsafe"
     libPaths: new string[0],
     references: new[] { "System.core.dll;Microsoft.Csharp.dll"
     options: "-infer autopropertiesensures -suggest requires -check falsepostconditions  -show progress -bounds -infer methodensures -infer requires -prefrompost=true -show validations -nonnull:noobl"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\Filtering.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show progress -bounds -nonnull -suggest methodensures -show validations"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\PostOnly.cs"
     compilerOptions: "/optimize /unsafe"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -define postonly -bounds -infer methodensures -show validations -nonnull:noobl"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestArithmetics\TestArithmetic.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -bounds:noobl -arithmetic:obl=divOverflow,obl=intOverflow,obl=floatOverflow  -assemblyMode=standard"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestPreconditionInference\PreconditionInference.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds -infer= -infer requires -infer propertyensures -infer methodensures"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestIterativeDomainApplication\IterativeDomainApplication.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds:type:intervals -bounds:type:pentagons -bounds"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Basic\TestOctagons\OctagonsTests.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -bounds:type=Octagons -wp=false"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestWarningMasking.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show validations -bounds -nonnull -wp=false -suggest assumes -suggest requiresbase -show unreached"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScore.cs"
     compilerOptions: "/define:LOW /optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel low -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScore.cs"
     compilerOptions: "/define:MEDIUMLOW /optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel mediumlow -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScore.cs"
     compilerOptions: "/define:MEDIUM /optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel medium -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScore.cs"
     compilerOptions: "/define:FULL /optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel full -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes "
     contractReferenceAssemblies: true"
     ),
  new Options(
   sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScoreMissingPre.cs"
   compilerOptions: "/define:LOW /optimize"
   libPaths: new string[0],
   references: new[] { ""
   options: "-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel low -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors"
   contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScoreMissingPre.cs"
     compilerOptions: "/define:MEDIUMLOW /optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel mediumlow -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScoreMissingPre.cs"
     compilerOptions: "/define:MEDIUM /optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel medium -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFilteringWithScoreMissingPre.cs"
     compilerOptions: "/define:FULL /optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -show progress -bounds -nonnull -wp=false -warninglevel full -premode combined -suggest codefixes -maskverifiedrepairs=false -suggest assumes -missingPublicRequiresAreErrors"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestWarningMasking\TestFindCommonRoot.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -show validations -nonnull -wp=false"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\IteratorAnalysis\Iterators.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -show progress -analyze:movenext -nonnull -show validations -bounds:noobl,type=subpolyhedra,reduction=simplex,diseq=false"
     contractReferenceAssemblies: true"
     skipCCI2: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\IteratorAnalysis\IteratorSimpleContract\IteratorSimpleContract.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -analyze:movenext  -nonnull:noobl -show validations -bounds:noobl,type=subpolyhedra,reduction=simplex,diseq=false -arrays"
     contractReferenceAssemblies: true"
     SkipSlicing="true"
     skipCCI2: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\BasicContainersTest.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -arrays -bounds:noObl,diseq=false -show progress  -show validations;unreached -wp=false -define Intervals"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\ArrayWithNonNullAnalysis.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false -define NonNull"
     contractReferenceAssemblies: true"
     exe: true"
     ),
  new Options(
       sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\SymbolicElementsTest.cs"
       compilerOptions: "/optimize"
       libPaths: new string[0],
       references: new[] { "System.Core.dll"
       options: "-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds -show progress -show validations;unreached -wp=false"
       contractReferenceAssemblies: true"
       exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\Enumerables.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false"
     contractReferenceAssemblies: true"
     exe: false"
     skipCCI2: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\UserRepros.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=true"
     contractReferenceAssemblies: true"
     exe: false"
    ),
  new Options(
   sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\Ernst.cs"
   compilerOptions: "/optimize"
   libPaths: new string[0],
   references: new[] { "System.Core.dll"
   options: "-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false"
   contractReferenceAssemblies: true"
   exe: false"
   SkipSlicing="true"
     ),
  new Options(
 sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\vstte.cs"
 compilerOptions: "/optimize"
 libPaths: new string[0],
 references: new[] { "System.Core.dll"
 options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -suggest methodensures -suggest requires -nonnull -bounds -arrays:arraypurity -check exists  -show unreached -show progress"
 contractReferenceAssemblies: true"
 exe: false"
     ),
  new Options(
   sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\BuiltInFunctions.cs"
   compilerOptions: "/optimize"
   libPaths: new string[0],
   references: new[] { "System.Core.dll"
   options: "-infer autopropertiesensures -suggest requires -arrays -nonnull -bounds -show progress  -show unreached -wp=false"
   contractReferenceAssemblies: true"
   exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestBooleanConnectives\TestDisjunctions.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -arrays -bounds -bounds:type=subpolyhedra,reduction=simplex -nonnull -wp=false -show progress -sortwarns=false  -show validations"
     contractReferenceAssemblies: true"
     exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\Array.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow"
     contractReferenceAssemblies: true"
     exe: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\Environment.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow"
     contractReferenceAssemblies: true"
     exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\General.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow"
     contractReferenceAssemblies: true"
     exe: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\JoelBaranick.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Xml.dll;System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow"
     contractReferenceAssemblies: true"
     exe: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\Math.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow"
     contractReferenceAssemblies: true"
     exe: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\Purity.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow"
     contractReferenceAssemblies: true"
     exe: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\Strings.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow"
     contractReferenceAssemblies: true"
     exe: false"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\UserFeedback.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Web.dll;System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl  -show progress -arithmetic:obl=intOverflow -assemblyMode=standard"
     contractReferenceAssemblies: true"
     exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\ReferenceToAllOOBC.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Windows.Forms.dll;System.Web.dll;WindowsBase.dll;System.Xml.Linq.dll;System.Web.dll;System.Security.dll;System.Drawing.dll;System.Configuration.dll;System.Configuration.Install.dll;System.Data.dll;Microsoft.VisualBasic.dll;Microsoft.VisualBasic.Compatibility.dll;System.Xml.dll;System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=simplex,noObl  -show progress -arithmetic:obl=intOverflow"
     contractReferenceAssemblies: true"
     exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestFrameworkOOB\General.cs"
     compilerOptions: ""
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show validations;unreached -NonNull -Bounds -bounds:type=subpolyhedra,reduction=fast,noObl -show progress -arithmetic:obl=intOverflow -define infer -infer requires"
     contractReferenceAssemblies: true"
     exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\OutOfBand\Client\Client.cs"
     compilerOptions: ""
     LibPaths="Foxtrot\Tests\AssemblyWithContracts\bin\debug"
     references: new[] { "AssemblyWithContracts.dll;System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires  -show progress -bounds:type=subpolyhedra,reduction=fast,noObl"
     contractReferenceAssemblies: true"
     exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\Allen.vb"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Data.dll;System.Xml.Linq.dll;System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress  -assemblyMode=standard"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "VB"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\XML.vb"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Data.dll;System.Xml.dll;System.Xml.Linq.dll;System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress  -assemblyMode=standard"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "VB"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\PeterGolde.vb"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Data.dll;System.Xml.Linq.dll;System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress "
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "VB"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\Misc.vb"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Data.dll;System.Xml.dll;System.Xml.Linq.dll;System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress  -assemblyMode=standard"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "VB"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\Strilan.vb"
     compilerOptions: "/rootnamespace:VisualBasicTests /optimize"
     libPaths: new string[0],
     references: new[] { "System.Data.dll;System.Xml.Linq.dll;System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -bounds -nonnull  -show validations -show progress "
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "VB"
     skipCCI2: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\VisualBasicTests\Wurz.vb"
     compilerOptions: "/rootnamespace:VisualBasicTests /optimize ExtraWurz.vb"
     libPaths: new string[0],
     references: new[] { "System.Deployment.dll;System.Configuration.dll;System.Drawing.dll;System.Windows.Forms.dll;System.Data.dll;System.Xml.Linq.dll;System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -arrays -bounds -nonnull  -show validations -show progress -regression -assemblyMode=standard"
     contractReferenceAssemblies: true"
     exe: true"
     compiler: "VB"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\ClassWithProtocolFinal.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires  -assemblyMode=standard -show:validations -bounds -nonnull:noobl -show progress"
     contractReferenceAssemblies: true"),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\Abbreviators.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -nonnull:noobl -bounds:noobl -show:validations"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "CS"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\ClousotTests\Sources\NoUpHavocMethods.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -nonnull:noobl -bounds:noobl -show:validations"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "CS"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\JPGrk.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arithmetic:obl=intOverflow -show:validations"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "CS"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\Herman.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show:validations;unreached"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "CS"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\EmileVanGerwen.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -bounds:type=octagons,diseq=false -show:validations;unreached -timeout 300"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "CS"
     skipCCI2: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\VictorDerks.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -bounds:type=octagons,diseq=false -show:validations;unreached -timeout 3"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "CS"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\Dluk.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "CS"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\Strilanc.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "CS"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\AlexeyR.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "CS"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\JonathanTapicer.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -bounds:type=subpolyhedra,reduction=simplex -show:validations;unreached"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "CS"
     ),
   new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\UserRepros\JamesAlbert.cs"
     compilerOptions: "/optimize /unsafe"
     libPaths: new string[0],
     references: new[] { "System.Core.dll;System.Net.Http;System.Web.http;System.XML.dll;System.Xml.Linq.dll"
     options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:noobl -arrays -show:unreached -check conditionsvalidity"
     contractReferenceAssemblies: true"
     exe: false"
     compiler: "CS"
     ),

  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\TestWebExamples\PexForFun.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -nonnull -bounds -bounds:type=subpolyhedra,reduction=simplex -arrays -arithmetic:obl=intOverflow -show:validations;unreached -timeout 300 -show progress"
    contractReferenceAssemblies: true"
    exe: false"
    compiler: "CS"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\TestWebExamples\BagOfNonNegative.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -nonnull -bounds -bounds:type=subpolyhedra,reduction=simplex -arrays -check exists -arithmetic:obl=intOverflow -show:unreached -timeout 300 -show progress"
    contractReferenceAssemblies: true"
    exe: false"
    compiler: "CS"
    ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestMemoryConsumption\CausingOutOfMemory.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires   -bounds -define outofmem -show validations -timeout 3600 -show progress -adaptive"
     contractReferenceAssemblies: false"
     exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Enum\EnumTest.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -arrays  -nonnull:noobl -arrays -enum -bounds -show validations -show unreached -timeout 3600 -show progress "
     contractReferenceAssemblies: true"
     exe: false"
     ),
  new Options(
       sourceFile: @"Microsoft.Research\RegressionTest\Enum\Switch.cs"
       compilerOptions: "/optimize"
       libPaths: new string[0],
       references: new[] { "System.Core.dll"
       options: "-infer autopropertiesensures -suggest requires -arrays -nonnull:noobl -arrays -enum -bounds -premode:combined -show validations -show unreached -timeout 3600 -show progress "
       contractReferenceAssemblies: true"
       exe: false"
     ),
  new Options(
   sourceFile: @"Microsoft.Research\RegressionTest\TimeOut\TimeOut.cs"
   compilerOptions: "/optimize"
   libPaths: new string[0],
   references: new[] { "System.Core.dll"
   options: "-infer autopropertiesensures -suggest requires -nonnull -bounds:crashWithTimeOut -show validations -show unreached -show progress "
   contractReferenceAssemblies: false"
   exe: false"
     ),
  new Options(
   sourceFile: @"Microsoft.Research\RegressionTest\Inference\Collections.cs"
   compilerOptions: "/optimize"
   libPaths: new string[0],
   references: new[] { "System.Core.dll"
   options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest requires -suggest arrayrequires -suggest arraypurity -nonnull -bounds -arrays -show validations -show unreached -show progress "
   contractReferenceAssemblies: true"
   exe: false"
   ),
  new Options(
   sourceFile: @"Microsoft.Research\RegressionTest\Inference\Purity.cs"
  compilerOptions: "/optimize"
  libPaths: new string[0],
  references: new[] { "System.Core.dll"
  options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest requires -suggest arrayrequires -suggest arraypurity -infer arraypurity -nonnull -bounds -arrays -show validations -show unreached -show progress "
  contractReferenceAssemblies: true"
  exe: false"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Preconditions-AllPaths.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode allPaths -suggest requires -nonnull -bounds -show validations -show unreached -show progress "
    contractReferenceAssemblies: true"
    exe: false"
     ),
  new Options(
  sourceFile: @"Microsoft.Research\RegressionTest\Inference\Preconditions-Backwards.cs"
  compilerOptions: "/optimize"
  libPaths: new string[0],
  references: new[] { "System.Core.dll"
  options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -throwArgExceptionAsAssert -premode backwards -suggest requires -infer requires -nonnull -bounds -arithmetic:obl=intOverflow -show=!! -show progress -suggest assumes -suggest codefixes -maskverifiedrepairs=false"
  contractReferenceAssemblies: true"
  exe: false"
     ),
  new Options(
  sourceFile: @"Microsoft.Research\RegressionTest\Inference\Preconditions-BlogExample.cs"
  compilerOptions: "/optimize"
  libPaths: new string[0],
  references: new[] { "System.Core.dll"
  options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -throwArgExceptionAsAssert -premode backwards -suggest requires -infer requires -nonnull -bounds -show=!! -show progress -show errors -suggest assumes -suggest codefixes -maskverifiedrepairs=false  -missingPublicRequiresAreErrors  -suggest calleeassumes -suggest assumes -suggest requires -suggest necessaryensures -suggest readonlyfields  -infer requires -infer methodensures"
  contractReferenceAssemblies: true"
  exe: false"
     ),

  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\SimplePreconditionPropagation.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -throwArgExceptionAsAssert -premode combined -suggest requires -infer requires -nonnull -bounds -show validations -show unreached -show progress "
    contractReferenceAssemblies: true"
    exe: false"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\RedundantAssumptions.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode allPaths -suggest requires -nonnull -bounds -show validations -show unreached -show progress -check assumptions -infer methodensures"
    contractReferenceAssemblies: true"
    exe: false"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\AssertToContracts.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode combined -suggest requires -nonnull -bounds -show unreached -show progress -check assumptions -infer methodensures -suggest asserttocontracts"
    contractReferenceAssemblies: true"
    exe: false"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\AssertToContracts.cs"
    compilerOptions: "/optimize /define:AGGRESSIVEINFERENCE"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -premode combined -suggest requires -nonnull -bounds -show unreached -show progress -check assumptions -infer methodensures -suggest asserttocontracts -inferencemode=aggressive"
    contractReferenceAssemblies: true"
    exe: false"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Exist.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds:noobl -arrays -suggest arrayrequires -suggest methodensures -infer methodensures -prefrompost -show progress"
    contractReferenceAssemblies: true"
    exe: false"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\ForAll.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds:noobl -nonnull:noobl -arrays:arraypurity  -suggest methodensures  -show progress"
    contractReferenceAssemblies: true"
    exe: false"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\ObjectInvariants.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -infer objectinvariants -inferencemode aggressive -suggest assumes -disableForwardObjectInvariantInference"
    contractReferenceAssemblies: true"
    exe: false"
    SkipSlicing="true"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\PodelskiEtAl.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -show progress -missingPublicRequiresAreErrors -infer objectinvariants -infer requires"
    contractReferenceAssemblies: true"
    exe: false"
    SkipSlicing="true"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\ObjectInvariants_FromConstructor.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -infer objectinvariants -inferencemode aggressive -suggest assumes -suggest necessaryensures -suggest readonlyfields"
    contractReferenceAssemblies: true"
    exe: false"
    SkipSlicing="true"
   ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\ObjectInvariants_Forward.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -infer objectinvariants -suggest assumes -suggest necessaryensures -suggest readonlyfields"
    contractReferenceAssemblies: true"
    exe: false"
    SkipSlicing="true"
   ),

  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Assume.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -suggest requiresbase -suggest assumes -suggest codefixes -maskverifiedrepairs=false"
    contractReferenceAssemblies: true"
    exe: false"
  ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\Assume.cs"
    compilerOptions: "/optimize /define:CODEFIXES"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arrays:arraypurity  -suggest methodensures  -show progress -suggest objectinvariants -suggest requiresbase -suggest assumes -premode backwards -suggest codefixes -maskverifiedrepairs=false"
    contractReferenceAssemblies: true"
    exe: false"
  ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\CalleeAssume.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: " -includesuggestionsinregression -bounds -nonnull -suggest calleeassumes -show progress  -premode:combined"
    contractReferenceAssemblies: true"
    exe: false"
  ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\Inference\NecessaryEnsures.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: " -includesuggestionsinregression -bounds -nonnull -suggest necessaryensures -show progress  -premode:combined"
    contractReferenceAssemblies: true"
    exe: false"
  ),
  new Options(
   sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\Existential.cs"
   compilerOptions: "/optimize"
   libPaths: new string[0],
   references: new[] { "System.Core.dll"
   options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -nonnull:noobl -bounds:noobl -arrays:arraypurity -check exists -show validations -show unreached -show progress"
   contractReferenceAssemblies: true"
   exe: false"
   SkipSlicing="true"
     ),
  new Options(
   sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\Markers.cs"
  compilerOptions: "/optimize"
  libPaths: new string[0],
  references: new[] { "System.Core.dll"
  options: "-infer autopropertiesensures -suggest requires -extractmethodmode=true -includesuggestionsinregression -suggest arrayrequires -nonnull:noobl -bounds:noobl -arrays:arraypurity -show unreached -show progress"
  contractReferenceAssemblies: true"
  exe: false"
     ),
  new Options(
   sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\RefinePrecondition.cs"
  compilerOptions: "/optimize"
  libPaths: new string[0],
  references: new[] { "System.Core.dll"
  options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -show unreached -show progress -suggest methodensures -extractmethodmoderefine TestArray"
  contractReferenceAssemblies: true"
  exe: false"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\RefinePrecondition.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -show unreached -show progress -suggest methodensures -extractmethodmoderefine TestIf"
    contractReferenceAssemblies: true"
    exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\RefinePrecondition.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -arithmetic:obl=intOverflow -show unreached -show progress -suggest methodensures -extractmethodmoderefine TestLoop"
    contractReferenceAssemblies: true"
    exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\RefinePrecondition.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -arithmetic:obl=intOverflow -show unreached -show progress -suggest methodensures -extractmethodmoderefine ExtractFromLinearSearch"
    contractReferenceAssemblies: true"
    exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\ExtractMethod\RefinePrecondition.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -nonnull -bounds -arrays:arraypurity -arithmetic:obl=intOverflow -show unreached -show progress -suggest methodensures -extractmethodmoderefine ExtractFromMax"
    contractReferenceAssemblies: true"
    exe: false"
     ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\CodeFixes\CodeFixes.cs"
    compilerOptions: "/optimize"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arithmetic -suggest codefixes -maskverifiedrepairs=false -suggest methodensures -show progress -suggest objectinvariants -infer objectinvariants -suggest assumes -premode backwards -check falsepostconditions"
    contractReferenceAssemblies: true"
    exe: false"
   ),
  new Options(
    sourceFile: @"Microsoft.Research\RegressionTest\CodeFixes\CodeFixes.cs"
    compilerOptions: "/optimize  /define:SHORTCODEFIXES"
    libPaths: new string[0],
    references: new[] { "System.Core.dll"
    options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression  -bounds -nonnull -arithmetic -suggest codefixesshort -maskverifiedrepairs=false -suggest methodensures -show progress -suggest objectinvariants -infer objectinvariants -suggest assumes -premode backwards -check falsepostconditions"
    contractReferenceAssemblies: true"
    exe: false"
   ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\CountFiltering.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show progress -bounds -nonnull -suggest methodensures -infer methodensures -infer requires -prefrompost=true -show unreached  -check falsepostconditions"
     contractReferenceAssemblies: true"
   ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestPostconditionInference\CheckFalsePostconditions.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -includesuggestionsinregression -show progress -bounds -nonnull  -check falsepostconditions"
     contractReferenceAssemblies: true"
   ),  
  new Options(
 sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\vstte.demo.cs"
 compilerOptions: "/optimize"
 libPaths: new string[0],
 references: new[] { "System.Core.dll"
 options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -suggest methodensures -suggest requires -suggest codefixes -maskverifiedrepairs=false -premode combined -nonnull -bounds -arrays:arraypurity -check exists  -show unreached -show progress"
 contractReferenceAssemblies: true"
 exe: false"
     ),
  new Options(
 sourceFile: @"Microsoft.Research\RegressionTest\Strings\SimpleStrings.cs"
 compilerOptions: "/optimize"
 libPaths: new string[0],
 references: new[] { "System.Core.dll"
 options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest arrayrequires -suggest methodensures -suggest requires -suggest codefixes -maskverifiedrepairs=false -premode combined -nonnull -bounds -arrays:arraypurity -check exists  -show unreached -show progress"
 contractReferenceAssemblies: true"
 exe: false"
     ),
  new Options(
 sourceFile: @"Microsoft.Research\RegressionTest\GroupActions\GroupActions.cs"
 compilerOptions: "/optimize"
 libPaths: new string[0],
 references: new[] { "System.Core.dll"
 options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest requires -suggest assumes -suggest codefixes -maskverifiedrepairs=false  -bounds -nonnull -sortwarns=false -show progress -groupactions=true -premode combined   "
 contractReferenceAssemblies: true"
 exe: false"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestPreconditionInference\InferenceTrace.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -show validations -bounds -nonnull -arrays -infer= -infer requires -infer propertyensures -infer methodensures -infer objectinvariants -show inferencetrace -repairs -premode combined"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Inference\CallInvariants.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -bounds -nonnull -arrays -infer= -suggest callinvariants"
     contractReferenceAssemblies: true"
     ),
  new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\Cloudot\Basics.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -bounds -nonnull -arrays -infer methodensures"
     contractReferenceAssemblies: true"
     ),
  new Options(
    Name ="Microsoft.Research\RegressionTest\Inference\MissingPublicPreconditionsAsWarnings.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { ""
     options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest=!! -bounds -nonnull -infer requires -missingPublicRequiresAreErrors"
     contractReferenceAssemblies: true"
    ),
  new Options(
    Name ="Microsoft.Research\RegressionTest\Basic\SuggestionsAsWarnings\SuggestionsAsWarns.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-infer autopropertiesensures -suggest requires -includesuggestionsinregression -suggest=!! -bounds -nonnull -infer requires -suggest readonlyfields -check assumptions -warnIfSuggest readonlyfields -warnifSuggest redundantassume"
     contractReferenceAssemblies: true"
    ),
  
  <!--new Options(
     sourceFile: @"Microsoft.Research\RegressionTest\TestMemoryConsumption\CausingOutOfMemory.cs"
     compilerOptions: "/optimize"
     libPaths: new string[0],
     references: new[] { "System.Core.dll"
     options: "-suggest requires  -bounds -define outofmem -show validations -timeout 3600 -show progress"
     contractReferenceAssemblies: false"
     exe: false"),-->
  <!-- F: commenting it for now, because there is some mismatch il the iloffsets
  new Options(
   sourceFile: @"Microsoft.Research\RegressionTest\Containers\TestContainers\List.cs"
   compilerOptions: "/optimize"
   libPaths: new string[0],
   references: new[] { "System.Core.dll"
   options: "-suggest requires -arrays -nonnull -bounds:noObl -show progress  -show validations;unreached -wp=false"
   contractReferenceAssemblies: true"
   exe: false"
     ),
  -->
  <!--new Options(
   sourceFile: @"Microsoft.Research\RegressionTest\Inference\PreconditionNecessityCheck.cs"
   compilerOptions: "/optimize"
   libPaths: new string[0],
   references: new[] { "System.Core.dll"
   options: "-suggest requires -includesuggestionsinregression -premode backwards -suggest requires -infer requires -check inferredrequires -nonnull -bounds -show validations -show unreached -show progress "
   contractReferenceAssemblies: true"
   exe: false"
     ),-->
#endif
    };

    public static IEnumerable<object[]> ClousotTestInputs
    {
        get
        {
            for (int i = 0; i < TestOptions.Length; i++)
            {
                yield return new object[] { i };
            }
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
    public void MyTestCleanup() {
      if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed && CurrentGroupInfo != null && !System.Diagnostics.Debugger.IsAttached)
      {
        // record failing case
        CurrentGroupInfo.WriteFailure();
      }
    }
    #endregion

    #region Regular tests

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

    [Theory]
    [MemberData("ClousotTestInputs")]
    public void XunitSlice2SequentialFromSourcesV40(int optionsIndex)
    {
      Options options = TestOptions[optionsIndex];
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

    static GroupInfo currentGroupInfo;

    static GroupInfo CurrentGroupInfo
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
