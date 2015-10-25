using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    internal sealed class AsyncTestsDataSource
    {
        private static readonly Options OptionsTemplate = new Options(
                    sourceFile: @"dummy_file",
                    foxtrotOptions: @"",
                    useContractReferenceAssemblies: true,
                    compilerOptions: null,
                    references: new[] { @"System.dll", @"System.Core.dll", @"System.Threading.Tasks.dll" },
                    libPaths: new[] { @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug" },
                    compilerCode: "CS",
                    useBinDir: false,
                    useExe: true,
                    mustSucceed: true);

        public static string[] SourceFiles = new[]
        {
            @"Foxtrot\Tests\AsyncTests\AsyncWithoutAwait.cs",
            @"Foxtrot\Tests\AsyncTests\AsyncInGenericClass.cs",
            @"Foxtrot\Tests\AsyncTests\AsyncInGenericClass.cs",
            @"Foxtrot\Tests\AsyncTests\AsyncEnsuresWithInterface.cs",
        };

        public static IEnumerable<Options> BaseTestCases
        {
            get
            {
                var sourceFiles = new[]
                {
                    @"Foxtrot\Tests\AsyncTests\AsyncWithoutAwait.cs",
                    @"Foxtrot\Tests\AsyncTests\AsyncInGenericClass.cs",
                    @"Foxtrot\Tests\AsyncTests\AsyncInGenericClass.cs",
                    @"Foxtrot\Tests\AsyncTests\AsyncEnsuresWithInterface.cs",
                };

                return sourceFiles.Select(f => OptionsTemplate.WithSourceFile(f));
            }
        }

        public static IEnumerable<Options> AsyncPostconditionsTestCases
        {
            get
            {
                var sourceFiles = new[]
                {
                    @"Foxtrot\Tests\AsyncPostconditions\AsyncPostconditionInGenericClass.cs",
                    @"Foxtrot\Tests\AsyncPostconditions\AsyncPostconditionInGenericClass2.cs",

                    @"Foxtrot\Tests\AsyncPostconditions\AsyncPostconditionInGenericMethod.cs",
                    @"Foxtrot\Tests\AsyncPostconditions\AsyncPostconditionInGenericMethod2.cs",
                    @"Foxtrot\Tests\AsyncPostconditions\AsyncPostconditionInGenericMethod3.cs",

                    @"Foxtrot\Tests\AsyncPostconditions\AsyncPostconditionInGenericMethodInStruct.cs",

                    @"Foxtrot\Tests\AsyncPostconditions\CancelledAsyncPostcondition.cs",
                    @"Foxtrot\Tests\AsyncPostconditions\AsyncPostconditionsInInterface.cs",
                    @"Foxtrot\Tests\AsyncPostconditions\MixedAsyncPostcondition.cs",
                    @"Foxtrot\Tests\AsyncPostconditions\NormalAsyncPostcondition.cs",
                    
                    @"Foxtrot\Tests\AsyncPostconditions\ExceptionalAsyncPostcondition.cs",
                    @"Foxtrot\Tests\AsyncPostconditions\ExceptionalAsyncPostconditionInGenericMethod.cs",
                    @"Foxtrot\Tests\AsyncPostconditions\ExceptionalAsyncPostconditionInGenericMethod2.cs",
                };

                return sourceFiles.Select(f => OptionsTemplate.WithSourceFile(f));
            }
        }
    }
}