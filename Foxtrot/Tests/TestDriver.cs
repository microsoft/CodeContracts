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
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using Microsoft.Contracts.Foxtrot;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class TestDriver
    {
        public const string ReferenceDirRoot = @"Microsoft.Research\Imported\ReferenceAssemblies\";
        public const string ContractReferenceDirRoot = @"Microsoft.Research\Contracts\bin\Debug\";
        public const string FoxtrotExe = @"Foxtrot\Driver\bin\debug\foxtrot.exe";
        public const string ToolsRoot = @"Microsoft.Research\Imported\Tools\";


        internal static object Rewrite(ITestOutputHelper testOutputHelper, string absoluteSourceDir, string absoluteBinary, Options options, bool alwaysCapture = false)
        {
            string referencedir;

            if (Path.IsPathRooted(options.ReferencesFramework))
            {
                referencedir = options.ReferencesFramework;
            }
            else
            {
                referencedir = options.MakeAbsolute(Path.Combine(ReferenceDirRoot, options.ReferencesFramework));
            }

            var contractreferencedir = options.MakeAbsolute(Path.Combine(ContractReferenceDirRoot, options.ContractFramework));
            var absoluteSourcedir = Path.GetDirectoryName(absoluteBinary);
            var absoluteSource = absoluteBinary;
            var libPathsString = FormLibPaths(absoluteSourceDir, contractreferencedir, options);

            var targetName = options.TestName + ".rw" + Path.GetExtension(absoluteBinary);
            var binDir = options.UseBinDir ? Path.Combine(Path.Combine(absoluteSourcedir, "bin"), options.BuildFramework) : absoluteSourcedir;
            var targetfile = Path.Combine(binDir, targetName);

            testOutputHelper.WriteLine("Rewriting '{0}' to '{1}'", absoluteBinary, targetfile);

            if (!Directory.Exists(binDir))
            {
                Directory.CreateDirectory(binDir);
            }
            var optionString = String.Format("/out:{0} -nobox -libpaths:{3} {4} {1} {2}", targetfile, options.FoxtrotOptions, absoluteSource, referencedir, libPathsString);
            if (absoluteBinary.EndsWith("mscorlib.dll"))
            {
                optionString = String.Format("/platform:{0} {1}", absoluteBinary, optionString);
            }

            var capture = RunProcessAndCapture(testOutputHelper, binDir, options.GetFullExecutablePath(FoxtrotExe), optionString, options.TestName);
            if (capture.ExitCode != 0)
            {
                if (options.MustSucceed)
                {
                    if (capture.ExitCode != 0)
                    {
                        testOutputHelper.WriteLine("");
                    }

                    Assert.Equal(0, capture.ExitCode);
                }
                return capture;
            }
            else
            {
                if (alwaysCapture) return capture;

                if (options.UseBinDir)
                {
                    var fileName = Path.Combine(absoluteSourcedir, targetName);
                    if (File.Exists(fileName))
                    {
                        try
                        {
                            File.SetAttributes(fileName, FileAttributes.Normal);
                        }
                        catch { }
                    }
                    
                    File.Copy(targetfile, fileName, true);
                    return fileName;
                }
            }
            return targetfile;
        }

        private static int PEVerify(ITestOutputHelper testOutputHelper, string assemblyFile, Options options)
        {
            var peVerifyPath = options.GetPEVerifyFullPath(ToolsRoot);
            Assert.True(File.Exists(peVerifyPath), string.Format("Can't find peverify.exe at '{0}'", peVerifyPath));

            var path = Path.GetDirectoryName(assemblyFile);
            var file = Path.GetFileName(assemblyFile);
            if (file == "mscorlib.dll") return -1; // peverify returns 0 for mscorlib without verifying.

            var exitCode = RunProcess(testOutputHelper, path, peVerifyPath, "/unique \"" + file + "\"", true);
            return exitCode;
        }

        /// <summary>
        /// Attempts to extract contracts from the specified assembly file. Used to verify that
        /// Foxtrot can still extract contracts from a rewritten assembly.
        /// </summary>
        private static void ExtractContracts(string assemblyFile, Options options)
        {
            // use assembly resolver from Foxtrot.Extractor
            var resolver = new AssemblyResolver(
                resolvedPaths: new string[0],
                libpaths: options.LibPaths,
                usePDB: false,
                preserveShortBranches: true,
                trace: false,
                postLoad: (r, assemblyNode) => {
                    ContractNodes contractNodes = null;
                    Extractor.ExtractContracts(
                        assemblyNode, null, null, null, null, out contractNodes,
                        e => Assert.True(false, e.ToString()), false);
                });

            var assembly = resolver.ProbeForAssembly(
                assemblyName: Path.GetFileNameWithoutExtension(assemblyFile),
                referencingModuleDirectory: Path.GetDirectoryName(assemblyFile),
                exts: new string[] { Path.GetExtension(assemblyFile) });

            // the assembly must be resolved and have no metadata import errors
            Assert.NotNull(assembly);
            Assert.True(assembly.MetadataImportErrors == null || assembly.MetadataImportErrors.Count == 0,
                          "Parsing back the rewritten assembly produced metadata import errors");
        }

        internal static string RewriteAndVerify(ITestOutputHelper testOutputHelper, string sourceDir, string binary, Options options)
        {
            if (!Path.IsPathRooted(sourceDir)) { sourceDir = options.MakeAbsolute(sourceDir); }
            if (!Path.IsPathRooted(binary)) { binary = options.MakeAbsolute(binary); }
            string target = Rewrite(testOutputHelper, sourceDir, binary, options) as string;
            if (target != null)
            {
                PEVerify(testOutputHelper, target, options);

                if (options.DeepVerify)
                    ExtractContracts(target, options);
            }
            return target;
        }

        private static int RunProcess(ITestOutputHelper testOutputHelper, string cwd, string tool, string arguments, bool mustSucceed, string writeBatchFile = null)
        {
            var capture = RunProcessAndCapture(testOutputHelper, cwd, tool, arguments, writeBatchFile);
            if (mustSucceed && capture.ExitCode != 0)
            {
                Assert.Equal(0, capture.ExitCode);
            }
            
            return capture.ExitCode;
        }

        private static OutputCapture RunProcessAndCapture(ITestOutputHelper testOutputHelper, string cwd, string tool, string arguments, string writeBatchFile = null)
        {
            ProcessStartInfo i = new ProcessStartInfo(tool, arguments);
            testOutputHelper.WriteLine("Running '{0}'", i.FileName);
            testOutputHelper.WriteLine("         {0}", i.Arguments);
            i.RedirectStandardOutput = true;
            i.RedirectStandardError = true;
            i.UseShellExecute = false;
            i.CreateNoWindow = true;
            i.WorkingDirectory = cwd;
            i.ErrorDialog = false;

            if (writeBatchFile != null)
            {
                var file = new StreamWriter(Path.Combine(cwd, writeBatchFile + ".bat"));
                file.WriteLine("\"{0}\" {1} %1 %2 %3 %4 %5", i.FileName, i.Arguments);
                file.Close();
            }

            var capture = new OutputCapture(testOutputHelper);

            using (Process p = Process.Start(i))
            {
                p.OutputDataReceived += capture.OutputDataReceived;
                p.ErrorDataReceived += capture.ErrorDataReceived;
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();

                Assert.True(p.WaitForExit(200000), String.Format("{0} timed out", i.FileName));
                capture.ExitCode = p.ExitCode;
                return capture;
            }
        }

        public static int Run(ITestOutputHelper testOutputHelper, string targetExe)
        {
            return RunProcess(testOutputHelper, Environment.CurrentDirectory, targetExe, "", true);
        }

        static string FormLibPaths(string absoluteSourceDir, string contractReferenceDir, Options options)
        {
            var oldcwd = Environment.CurrentDirectory;
            try
            {
                Environment.CurrentDirectory = absoluteSourceDir;

                StringBuilder sb = null;
                if (options.UseContractReferenceAssemblies)
                {
                    sb = new StringBuilder("/libpaths:").Append(contractReferenceDir);
                }
                if (options.LibPaths == null) return "";
                foreach (var path in options.LibPaths)
                {
                    if (sb == null)
                    {
                        sb = new StringBuilder("/libpaths:");
                    }
                    else
                    {
                        sb.Append(';');
                    }
                    sb.Append(options.MakeAbsolute(Path.Combine(path, options.ContractFramework)));
                }
                if (sb == null) return "";
                return sb.ToString();
            }
            finally
            {
                Environment.CurrentDirectory = oldcwd;
            }
        }

        internal static string Build(ITestOutputHelper testOutputHelper, Options options, out string absoluteSourceDir)
        {
            var sourceFile = options.MakeAbsolute(options.SourceFile);

            var compilerPath = options.GetCompilerAbsolutePath(ToolsRoot);
            Assert.True(File.Exists(compilerPath), string.Format("Can't find compiler at '{0}'", compilerPath));

            var contractreferencedir = options.MakeAbsolute(Path.Combine(ContractReferenceDirRoot, options.ReferencesFramework));
            var sourcedir = absoluteSourceDir = Path.GetDirectoryName(sourceFile);

            var outputdir = Path.Combine(Path.Combine(sourcedir, "bin"), options.CompilerPath);
            var extension = options.UseExe ? ".exe" : ".dll";
            var targetKind = options.UseExe ? "exe" : "library";

            var targetfile = Path.Combine(outputdir, options.TestName + extension);

            // add Microsoft.Contracts reference if needed
            if (options.IsV35)
            {
                options.References.Add("Microsoft.Contracts.dll");
            }

            var oldCwd = Environment.CurrentDirectory;

            try
            {
                Environment.CurrentDirectory = sourcedir;

                var resolvedReferences = ResolveReferences(options);
                var referenceString = ReferenceOptions(resolvedReferences);

                if (!Directory.Exists(outputdir))
                {
                    Directory.CreateDirectory(outputdir);
                }

                string arguments = String.Format("/t:{4} /out:{0} {3} {2} {1}", targetfile, sourceFile, referenceString, options.FinalCompilerOptions, targetKind);

                if (!options.ReleaseMode)
                {
                    arguments = "/debug " + arguments;
                }

                var exitCode = RunProcess(testOutputHelper, sourcedir, compilerPath, arguments, true);

                if (exitCode != 0)
                {
                    return null;
                }

                // add reference to Microsoft.Contracts.dll if not already done because of transitive closure issues
                if (!options.References.Contains("Microsoft.Contracts.dll"))
                {
                    options.References.Add("Microsoft.Contracts.dll");

                    // recompute resolvedReferences
                    resolvedReferences = ResolveReferences(options);
                }

                CopyReferenceAssemblies(resolvedReferences, outputdir);

                return targetfile;
            }
            finally
            {
                Environment.CurrentDirectory = oldCwd;
            }
        }

        public class OutputCapture
        {
            private readonly ITestOutputHelper _testOutputHelper;

            public OutputCapture(ITestOutputHelper testOutputHelper)
            {
                _testOutputHelper = testOutputHelper;
            }

            public int ExitCode { get; set; }

            public readonly List<string> errOut = new List<string>();
            public readonly List<string> stdOut = new List<string>();

            internal void ErrorDataReceived(object sender, DataReceivedEventArgs e)
            {
                if (e.Data == null) return;
                this.errOut.Add(e.Data);
                _testOutputHelper.WriteLine("{0}", e.Data);
            }

            internal void OutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                if (e.Data == null) return;
                this.stdOut.Add(e.Data);
                _testOutputHelper.WriteLine("{0}", e.Data);
            }
        }

        private static void CopyReferenceAssemblies(List<string> resolvedReferences, string outputdir)
        {
            foreach (var r in resolvedReferences)
            {
                try
                {
                    var fileName = Path.Combine(outputdir, Path.GetFileName(r));
                    if (File.Exists(fileName))
                    {
                        try
                        {
                            File.SetAttributes(fileName, FileAttributes.Normal);
                        }
                        catch { }
                    }
                    File.Copy(r, fileName, true);
                }
                catch { }
            }
        }
        private static List<string> ResolveReferences(Options options)
        {
            var result = new List<string>();
            foreach (var r in options.References)
            {
                var found = FindReference(options, result, r, options.ReferencesFramework);
                if (!found && options.ReferencesFramework != options.ContractFramework)
                {
                    // try to find it in the contract framework
                    FindReference(options, result, r, options.ContractFramework);
                }
            }
            return result;
        }

        private static bool FindReference(Options options, List<string> result, string filename, string framework)
        {
            foreach (var root in System.Linq.Enumerable.Concat(options.LibPaths, new[] { ReferenceDirRoot, ContractReferenceDirRoot }))
            {
                var dir = options.MakeAbsolute(Path.Combine(root, framework));

                var path = Path.Combine(dir, filename);
                if (File.Exists(path))
                {
                    result.Add(path);
                    return true;
                }
            }
            return false;
        }

        private static string ReferenceOptions(List<string> references)
        {
            var sb = new StringBuilder();
            foreach (var r in references)
            {
                sb.Append(String.Format(@"/r:{0} ", r));
            }
            return sb.ToString();
        }

        internal static void BuildRewriteRun(ITestOutputHelper testOutputHelper, Options options)
        {
            string absoluteSourceDir;
            var target = Build(testOutputHelper, options, out absoluteSourceDir);
            if (target != null)
            {
                var rwtarget = RewriteAndVerify(testOutputHelper, absoluteSourceDir, target, options);
                if (rwtarget != null)
                {
                    Run(testOutputHelper, rwtarget);
                }
            }
        }

        internal static void BuildExtractExpectFailureOrWarnings(ITestOutputHelper testOutputHelper, Options options)
        {
            string absoluteSourceDir;
            var target = Build(testOutputHelper, options, out absoluteSourceDir);
            if (target != null)
            {
                RewriteAndExpectFailureOrWarnings(testOutputHelper, absoluteSourceDir, target, options);
            }
        }

        internal static void RewriteAndExpectFailureOrWarnings(ITestOutputHelper testOutputHelper, string sourceDir, string binary, Options options)
        {
            var capture = Rewrite(testOutputHelper, sourceDir, binary, options, true) as OutputCapture;

            CompareExpectedOutput(options, capture);
        }

        private static void CompareExpectedOutput(Options options, OutputCapture capture)
        {
            var expected = File.ReadAllLines(options.MakeAbsolute(@"Foxtrot\Tests\CheckerTests\" + Path.GetFileNameWithoutExtension(options.SourceFile) + ".Expected"));
            var expectedIndex = 0;
            var absoluteFile = options.MakeAbsolute(options.SourceFile);
            for (int i = 0; i < capture.stdOut.Count; i++)
            {
                var actual = capture.stdOut[i];

                if (actual.StartsWith("Trace:")) continue;
                if (actual.StartsWith("elapsed time:")) continue;
                if (expectedIndex >= expected.Length)
                {
                    Assert.Null(actual);
                }
                var expectedLine = expected[expectedIndex++];

                if (actual.StartsWith(absoluteFile, StringComparison.InvariantCultureIgnoreCase))
                {
                    actual = TrimFilePath(actual, options.SourceFile);
                    expectedLine = TrimFilePath(expectedLine, options.SourceFile);
                }
                Assert.Equal(expectedLine, actual);
            }
            if (expectedIndex < expected.Length)
            {
                Assert.Equal(expected[expectedIndex], null);
            }
        }

        private static string TrimFilePath(string actual, string sourceFile)
        {
            var i = actual.IndexOf(sourceFile, StringComparison.InvariantCultureIgnoreCase);
            if (i > 0) { return actual.Substring(i); }
            return actual;
        }

        internal static void RewriteBinary(ITestOutputHelper testOutputHelper, Options options, string sourceDir)
        {
            var absoluteDir = options.MakeAbsolute(sourceDir);
            var binary = Path.Combine(absoluteDir, options.SourceFile);
            Rewrite(testOutputHelper, absoluteDir, binary, options);
        }
    }
}
