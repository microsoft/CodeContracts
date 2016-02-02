// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public static class TestDriver
    {
        private const string ReferenceDirRoot = @"Microsoft.Research\Imported\ReferenceAssemblies\";
        private const string ContractReferenceDirRoot = @"Microsoft.Research\Contracts\bin\<Configuration>";
        private const string ClousotExe = @"Microsoft.Research\Clousot\bin\debug\clousot.exe";
        private const string ToolsRoot = @"Microsoft.Research\Imported\Tools\";

        private static readonly Random randGenerator = new Random();
        
        internal static void Clousot(string absoluteSourceDir, string absoluteBinary, Options options, string testName, int testindex, Output output)
        {
            var referencedir = options.MakeAbsolute(Path.Combine(ReferenceDirRoot, options.BuildFramework));
            var contractreferencedir = options.MakeAbsolute(Path.Combine(Options.ReplaceConfiguration(ContractReferenceDirRoot), options.ContractFramework));
            var absoluteBinaryDir = Path.GetDirectoryName(absoluteBinary);
            var absoluteSource = absoluteBinary;
            var libPathsString = FormLibPaths(contractreferencedir, options);
            var args = string.Format("{0} /regression /define:cci1only;clousot1 -framework:{4} -libpaths:{2} {3} {1}", options.ClousotOptions, absoluteSource, referencedir, libPathsString, options.Framework);

            WriteRSPFile(absoluteBinaryDir, testName, args);
            
            if (options.Fast || Debugger.IsAttached)
            {
                output.WriteLine("Calling CCI1Driver.Main with: {0}", args);
                // Use output to avoid Clousot from closing the Console
                Assert.Equal(0, Microsoft.Research.CodeAnalysis.CCI1Driver.Main(args.Split(' '), output));
            }
            else
            {
                RunProcess(absoluteBinaryDir, options.GetFullExecutablePath(ClousotExe), args, output, testName);
            }
        }

        private static void WriteRSPFile(string dir, string testName, string args)
        {
            using (var file = new StreamWriter(Path.Combine(dir, testName + ".rsp")))
            {
                file.WriteLine(args);
                file.Close();
            }
        }

        private static int RunProcess(string cwd, string tool, string arguments, Output output, string writeBatchFile = null)
        {
            ProcessStartInfo i = new ProcessStartInfo(tool, arguments);
            output.WriteLine("Running '{0}'", i.FileName);
            output.WriteLine("         {0}", i.Arguments);
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

            using (Process p = Process.Start(i))
            {
                p.OutputDataReceived += output.OutputDataReceivedEventHandler;
                p.ErrorDataReceived += output.ErrDataReceivedEventHandler;
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();

                Assert.True(p.WaitForExit(200000), string.Format("{0} timed out", i.FileName));
                if (p.ExitCode != 0)
                {
                    Assert.Equal(0, p.ExitCode);
                }
                return p.ExitCode;
            }
        }

        private static string FormLibPaths(string contractReferenceDir, Options options)
        {
            // MB: do not change CurrentDirectory because it makes parallel tests fail

            if (options.LibPaths == null)
                return "";

            StringBuilder sb = null;
            if (options.UseContractReferenceAssemblies)
                sb = new StringBuilder("/libpaths:").Append(contractReferenceDir);

            foreach (var path in options.LibPaths)
            {
                if (sb == null)
                    sb = new StringBuilder("/libpaths:");
                else
                    sb.Append(';');

                sb.Append(options.MakeAbsolute(Path.Combine(path, options.ContractFramework)));
            }
            if (sb == null)
                return "";
            return sb.ToString();
        }


        internal static string Build(Options options, int testIndex, string extraCompilerOptions, Output output, out string absoluteSourceDir)
        {
            var sourceFile = options.MakeAbsolute(options.SourceFile);
            var compilerpath = options.MakeAbsolute(Path.Combine(ToolsRoot, options.BuildFramework, options.Compiler));
            var contractreferencedir = options.MakeAbsolute(Path.Combine(Options.ReplaceConfiguration(ContractReferenceDirRoot), options.BuildFramework));
            var sourcedir = absoluteSourceDir = Path.GetDirectoryName(sourceFile);
            var outputdir = Path.Combine(sourcedir, "bin", options.BuildFramework);
            var extension = options.UseExe ? ".exe" : ".dll";
            var targetKind = options.UseExe ? "exe" : "library";
            var suffix = "_" + testIndex;
            if (options.GenerateUniqueOutputName)
                suffix += "." + randGenerator.Next(0x10000).ToString("X4"); // enables concurrent tests on the same source file
            var targetfile = Path.Combine(outputdir, Path.GetFileNameWithoutExtension(sourceFile) + suffix + extension);
            // add Microsoft.Contracts reference if needed
            if (!options.BuildFramework.Contains("v4."))
            {
                options.References.Add("Microsoft.Contracts.dll");
            }

            // MB: do not modify the CurrentDirectory, that could cause parallel tests to fail

            var resolvedReferences = ResolveReferences(options);
            var referenceString = ReferenceOptions(resolvedReferences);
            if (!Directory.Exists(outputdir))
            {
                Directory.CreateDirectory(outputdir);
            }
            var args = String.Format("/debug /t:{4} /out:{0} {5} {3} {2} {1}", targetfile, sourceFile, referenceString, options.CompilerOptions(resolvedReferences), targetKind, extraCompilerOptions);
            var exitCode = RunProcess(sourcedir, compilerpath, args, output);
            if (exitCode != 0)
            {
                return null;
            }
            //CopyReferenceAssemblies(resolvedReferences, outputdir);

            return targetfile;
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
                foreach (var root in options.LibPaths)
                {
                    var dir = options.MakeAbsolute(Path.Combine(root, options.BuildFramework));

                    var path = Path.Combine(dir, r);
                    if (File.Exists(path))
                    {
                        result.Add(path);
                        break;
                    }
                }
                foreach (var root in new[] { ReferenceDirRoot, Options.ReplaceConfiguration(ContractReferenceDirRoot) })
                {
                    var dir = options.MakeAbsolute(Path.Combine(root, options.BuildFramework));

                    var path = Path.Combine(dir, r);
                    if (File.Exists(path))
                    {
                        result.Add(path);
                        break;
                    }
                }
            }
            return result;
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

        public static void BuildAndAnalyze(ITestOutputHelper testOutputHelper, Options options, string testName, int testIndex)
        {
            var output = Output.ConsoleOutputFor(testOutputHelper, testName);

            try
            {
                string absoluteSourceDir;
                var target = Build(options, testIndex, "/d:CLOUSOT1", output.Item1, out absoluteSourceDir);
                if (target != null)
                {
                    Clousot(absoluteSourceDir, target, options, testName, testIndex, output.Item1);
                }
            }
            finally
            {
                testOutputHelper.WriteLine(output.Item2.ToString());
            }
        }
    }
}
