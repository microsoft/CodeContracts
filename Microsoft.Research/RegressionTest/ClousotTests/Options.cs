// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Tests
{
    [Flags]
    public enum TestRuns
    {
        None = 0,
        V40AgainstV35Contracts = 1,
        V40 = 2
    }

    public class Options
    {
        private const string RelativeRoot = @"..\..\..\..\..\";
        internal const string TestHarnessDirectory = @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\{Configuration}";
        protected static readonly string RootDirectory = Path.GetFullPath(RelativeRoot);

        private readonly string _outDirectory;
        private readonly string _compilerCode;
        private readonly string _compilerOptions;

        public readonly string SourceFile;
        public readonly string ClousotOptions;
        public readonly List<string> LibPaths;
        public readonly List<string> References;
        public readonly bool UseContractReferenceAssemblies = true;
        
        public readonly bool UseExe;
        public readonly TestRuns SkipFor;
        public readonly bool GenerateUniqueOutputName = false;
        public readonly bool Fast = false;

        public string BuildFramework = "v3.5";
        public string ContractFramework = "v3.5";

        public string Compiler
        {
            get
            {
                switch (_compilerCode)
                {
                    case "VB": return "vbc.exe";
                    default: return "csc.exe";
                }
            }
        }

        private bool IsV4 { get { return this.BuildFramework.Contains("v4"); } }
        private bool IsV4Contracts { get { return this.ContractFramework.Contains("v4"); } }
        private bool IsSilverlight { get { return this.BuildFramework.Contains("Silverlight"); } }
        private string Moniker
        {
            get
            {
                if (IsSilverlight)
                {
                    if (IsV4)
                    {
                        return "SILVERLIGHT_4_0";
                    }
                    else
                    {
                        return "SILVERLIGHT_3_0";
                    }
                }
                else
                {
                    if (IsV4)
                    {
                        return "NETFRAMEWORK_4_0";
                    }
                    else
                    {
                        return "NETFRAMEWORK_3_5";
                    }
                }
            }
        }

        public string ContractMoniker
        {
            get
            {
                if (IsSilverlight)
                {
                    if (IsV4Contracts)
                    {
                        return "SILVERLIGHT_4_0_CONTRACTS";
                    }
                    else
                    {
                        return "SILVERLIGHT_3_0_CONTRACTS";
                    }
                }
                else
                {
                    if (IsV4Contracts)
                    {
                        return "NETFRAMEWORK_4_0_CONTRACTS";
                    }
                    else
                    {
                        return "NETFRAMEWORK_3_5_CONTRACTS";
                    }
                }
            }
        }

        private string DefaultCompilerOptions
        {
            get
            {
                switch (_compilerCode)
                {
                    case "VB":
                        return String.Format("/noconfig /nostdlib /define:\"DEBUG=-1,{0},CONTRACTS_FULL\",_MyType=\\\"Console\\\" " +
                                             "/imports:Microsoft.VisualBasic,System,System.Collections,System.Collections.Generic,System.Data,System.Diagnostics,System.Linq,System.Xml.Linq " +
                                             "/optioncompare:Binary /optionexplicit+ /optionstrict:custom /optioninfer+ {1}",
                                             Moniker,
                                             MakeAbsolute(@"Foxtrot\Contracts\ContractExtensions.vb")
                                             );
                    default:
                        if (IsV4 && !UseContractReferenceAssemblies)
                        {
                            // work around a bug in mscorlib.dll which has warnings when we extract contracts from it
                            return String.Format("/noconfig /nostdlib /d:CONTRACTS_FULL;DEBUG;{0};{1} {2} {3}", Moniker, ContractMoniker,
                              MakeAbsolute(@"Microsoft.Research\RegressionTest\ClousotTests\NoWarn.cs"),
                              MakeAbsolute(@"Foxtrot\Contracts\ContractExtensions.cs")
                              );
                        }
                        else
                        {
                            return String.Format("/noconfig /nostdlib /d:CONTRACTS_FULL;DEBUG;{0};{1} {2}", Moniker, ContractMoniker,
                              MakeAbsolute(@"Foxtrot\Contracts\ContractExtensions.cs")
                              );
                        }
                }
            }
        }

        public string CompilerOptions(List<string> resolvedRefs)
        {
            if (_compilerCode == "VB")
            {
                string mscorlib = null;
                foreach (var p in resolvedRefs)
                {
                    if (p.EndsWith("mscorlib.dll")) { mscorlib = Path.GetDirectoryName(p); break; }
                }
                if (mscorlib != null)
                {
                    return String.Format("/sdkpath:\"{0}\" ", mscorlib) + DefaultCompilerOptions + " " + _compilerOptions;
                }
            }
            return DefaultCompilerOptions + " " + _compilerOptions;
        }

        public Options(
            string sourceFile,
            string clousotOptions,
            bool useContractReferenceAssemblies,
            bool useExe,
            string compilerOptions,
            string[] references,
            string[] libPaths,
            string compilerCode,
            TestRuns skipFor = TestRuns.None)
        {
            _outDirectory = Environment.CurrentDirectory;
            this.SourceFile = sourceFile;
            this.ClousotOptions = clousotOptions;
            this.UseContractReferenceAssemblies = useContractReferenceAssemblies;
            this.UseExe = useExe;
            this._compilerOptions = compilerOptions;
            this.References = new List<string> { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" };
            this.References.AddRange(references);
            this.LibPaths = new List<string> { MakeAbsolute(ReplaceConfiguration(TestHarnessDirectory)) };
            this.LibPaths.AddRange(libPaths);
            this._compilerCode = compilerCode;
            this.SkipFor = skipFor;
        }
        public static string ReplaceConfiguration(string path)
        {
            return path.Replace("{Configuration}", GetConfigurationName());
        }

        public static string GetConfigurationName()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string directoryName = Path.GetDirectoryName(path);
            return Path.GetFileName(directoryName);
        }

        /// <summary>
        /// Not only makes the exe absolute but also tries to find it in the deployment dir to make code coverage work.
        /// </summary>
        public string GetFullExecutablePath(string relativePath)
        {
            var deployed = Path.Combine(_outDirectory, Path.GetFileName(relativePath));
            if (File.Exists(deployed))
            {
                return deployed;
            }
            return MakeAbsolute(relativePath);
        }

        public string MakeAbsolute(string relativeToRoot)
        {
            return Path.Combine(RootDirectory, relativeToRoot); // MB: do not need Path.GetFullPath because RootDirectory is already an absolute path
        }

        public object Framework
        {
            get
            {
                if (this.BuildFramework.EndsWith("v3.5"))
                {
                    return "v3.5";
                }
                if (this.BuildFramework.EndsWith("v4.0"))
                {
                    return "v4.0";
                }
                if (this.BuildFramework.EndsWith("v4.5"))
                {
                    return "v4.5";
                }
                else
                {
                    return "none";
                }
            }
        }
    }
}
