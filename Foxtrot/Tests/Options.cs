using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Tests
{
    [Serializable]
    internal sealed class Options
    {
        const string RelativeRoot = @"..\..\..\..\";

        const string TestHarnessDirectory = @"Microsoft.Research\RegressionTest\ClousotTestHarness\bin\debug";

        public readonly string RootDirectory;

        public readonly string SourceFile;

        private readonly string compilerCode;

        public string FoxtrotOptions;

        private List<string> libPaths;

        public readonly List<string> References;

        public bool UseContractReferenceAssemblies;

        public string BuildFramework = "v4.5";

        public string ReferencesFramework
        {
            get
            {
                return referencesFramework ?? BuildFramework;
            }
            set
            {
                referencesFramework = value;
            }
        }

        private string referencesFramework;

        public string ContractFramework = "v4.5";

        public bool UseBinDir = true;

        public bool UseExe = true;

        /// <summary>
        /// If set to true, verification will include reading contracts back from the rewritten
        /// assembly in addition to running peverify.
        /// </summary>
        public bool DeepVerify = false;

        public List<string> LibPaths
        {
            get
            {
                if (libPaths == null) { libPaths = new List<string>(); }
                return libPaths;
            }
        }

        public string TestName
        {
            get
            {
                if (SourceFile != null) { return Path.GetFileNameWithoutExtension(SourceFile); }
                else return "Unknown";
            }
        }

        /// <summary>
        /// Should be true, if old (pre-RC) Roslyn compiler needs to be used.
        /// </summary>
        public bool IsLegacyRoslyn { get; set; }

        public string Compiler
        {
            get
            {
                switch (compilerCode)
                {
                    case "VB":
                        return IsLegacyRoslyn ? "rvbc.exe" : "vbc.exe";
                    default:
                        return IsLegacyRoslyn ? "rcsc.exe" : "csc.exe";
                }
            }
        }

        public string CompilerPath
        {
            get { return compilerPath ?? BuildFramework; }
            set { compilerPath = value; }
        }

        private string compilerPath;

        public bool IsV35 { get { return this.BuildFramework.Contains("v3.5"); } }

        public bool IsV40 { get { return this.BuildFramework.Contains("v4.0"); } }

        public bool IsV45 { get { return this.BuildFramework.Contains("v4.5") || IsRoslynBasedCompiler; } }
        
        bool IsSilverlight { get { return this.BuildFramework.Contains("Silverlight"); } }

        public bool IsRoslynBasedCompiler { get { return CompilerPath.Contains("Roslyn"); } }

        public string GetCompilerAbsolutePath(string toolsRoot)
        {
            return MakeAbsolute(Path.Combine(toolsRoot, CompilerPath, Compiler));
        }

        public string GetPEVerifyFullPath(string toolsRoot)
        {
            return MakeAbsolute(Path.Combine(toolsRoot, CompilerPath, "peverify.exe"));
        }

        string Moniker
        {
            get
            {
                if (!IsLegacyRoslyn) { return FrameworkMoniker; }
                
                if (compilerCode == "VB")
                {
                    return FrameworkMoniker + ",ROSLYN";
                }

                return FrameworkMoniker + ";ROSLYN";
            }
        }

        string FrameworkMoniker
        {
            get
            {
                if (IsSilverlight)
                {
                    if (IsV40)
                    {
                        return "SILVERLIGHT_4_0";
                    }
                    if (IsV45)
                    {
                        return "SILVERLIGHT_4_5";
                    }
                    {
                        return "SILVERLIGHT_3_0";
                    }
                }
                else
                {
                    if (IsV40)
                    {
                        return "NETFRAMEWORK_4_0";
                    }
                    if (IsV45)
                    {
                        return "NETFRAMEWORK_4_5";
                    }
                    {
                        return "NETFRAMEWORK_3_5";
                    }
                }
            }
        }

        public bool UseTestHarness { get; set; }

        private string DefaultCompilerOptions
        {
            get
            {
                if (compilerCode == "VB")
                {
                    return
                        String.Format("/noconfig /nostdlib /define:\"DEBUG=-1,{0},CONTRACTS_FULL\",_MyType=\\\"Console\\\" " +
                                      "/imports:Microsoft.VisualBasic,System,System.Collections,System.Collections.Generic,System.Data,System.Diagnostics,System.Linq,System.Xml.Linq " +
                                      "/optioncompare:Binary /optionexplicit+ /optionstrict+ /optioninfer+ ",
                            Moniker);
                }
                
                if (IsRoslynBasedCompiler)
                {
                    if (UseTestHarness)
                    {
                        return String.Format("/d:CONTRACTS_FULL;ROSLYN;DEBUG;{0} /noconfig /nostdlib {1}", Moniker,
                            MakeAbsolute(@"Foxtrot\Tests\Sources\TestHarness.cs"));
                    }
                    else
                    {
                        return String.Format("/d:CONTRACTS_FULL;ROSLYN;DEBUG;{0} /noconfig /nostdlib", Moniker);
                    }
                }
                else
                {
                    if (UseTestHarness)
                    {
                        return String.Format("/d:CONTRACTS_FULL;DEBUG;{0} /noconfig /nostdlib {1}", Moniker,
                            MakeAbsolute(@"Foxtrot\Tests\Sources\TestHarness.cs"));
                    }
                    else
                    {
                        return String.Format("/d:CONTRACTS_FULL;DEBUG;{0} /noconfig /nostdlib", Moniker);
                    }
                }
            }
        }

        public string FinalCompilerOptions
        {
            get
            {
                return DefaultCompilerOptions + " " + CompilerOptions;
            }
        }

        public string CompilerOptions { get; set; }

        public Options(
            string sourceFile,
            string foxtrotOptions,
            bool useContractReferenceAssemblies,
            string compilerOptions,
            string[] references,
            string[] libPaths,
            string compilerCode,
            bool useBinDir,
            bool useExe,
            bool mustSucceed)
        {
            this.SourceFile = sourceFile;
            this.FoxtrotOptions = foxtrotOptions;
            this.UseContractReferenceAssemblies = useContractReferenceAssemblies;
            this.CompilerOptions = compilerOptions;
            this.References = new List<string>(new[] { "mscorlib.dll", "System.dll", "ClousotTestHarness.dll" });
            this.References.AddRange(references);
            this.libPaths = new List<string>(libPaths ?? Enumerable.Empty<string>());
            this.compilerCode = compilerCode;
            this.UseBinDir = useBinDir;
            this.UseExe = useExe;
            this.MustSucceed = mustSucceed;

            this.RootDirectory = Path.GetFullPath(RelativeRoot);
        }

        public bool ReleaseMode { get; set; }

        private static string LoadString(System.Data.DataRow dataRow, string name)
        {
            if (!ColumnExists(dataRow, name)) return "";
            return dataRow[name] as string;
        }

        private static List<string> LoadList(System.Data.DataRow dataRow, string name, params string[] initial)
        {
            var result = new List<string>(initial);

            if (!ColumnExists(dataRow, name)) return result;

            string listdata = dataRow[name] as string;

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

        private bool LoadBool(string name, System.Data.DataRow dataRow, bool defaultValue)
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

        private string LoadString(string name, System.Data.DataRow dataRow, string defaultValue)
        {
            if (!ColumnExists(dataRow, name)) return defaultValue;

            var option = dataRow[name] as string;

            if (!string.IsNullOrEmpty(option))
            {
                return option;
            }

            return defaultValue;
        }

        /// <summary>
        /// Not only makes the exe absolute but also tries to find it in the deployment dir to make code coverage work.
        /// </summary>
        public string GetFullExecutablePath(string relativePath)
        {
            return MakeAbsolute(relativePath);
        }

        public string MakeAbsolute(string relativeToRoot)
        {
            return Path.GetFullPath(Path.Combine(this.RootDirectory, relativeToRoot));
        }

        internal void Delete(string fileName)
        {
            var absolute = MakeAbsolute(fileName);
            if (File.Exists(absolute))
            {
                File.Delete(absolute);
            }
        }

        public bool MustSucceed { get; set; }
    }
}