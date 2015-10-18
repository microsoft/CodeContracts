// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public static class TestDriver
    {
        private const string ReferenceDirRoot = @"Microsoft.Research\Imported\ReferenceAssemblies\";
        private const string ContractReferenceDirRoot = @"Microsoft.Research\Contracts\bin\Debug\";
        private const string ClousotExe = @"Microsoft.Research\Clousot\bin\debug\clousot.exe";
        private const string Clousot2Exe = @"Microsoft.Research\Clousot2\bin\debug\clousot2.exe";
        private const string Clousot2SExe = @"Microsoft.Research\Clousot2S\bin\debug\clousot2s.exe";
        private const string Clousot2SlicingExe = @"Microsoft.Research\Clousot2_Queue\bin\debug\Clousot2_Queue.exe";
        private const string ClousotServiceHostExe = @"Microsoft.Research\Clousot2_WCFServiceHost\bin\debug\Cloudot.exe";
        private const string ToolsRoot = @"Microsoft.Research\Imported\Tools\";

        private static readonly Random randGenerator = new Random();

        internal static void Clousot(string absoluteSourceDir, string absoluteBinary, Options options, Output output)
        {
            var referencedir = options.MakeAbsolute(Path.Combine(ReferenceDirRoot, options.BuildFramework));
            var contractreferencedir = options.MakeAbsolute(Path.Combine(ContractReferenceDirRoot, options.ContractFramework));
            var absoluteBinaryDir = Path.GetDirectoryName(absoluteBinary);
            var absoluteSource = absoluteBinary;
            var libPathsString = FormLibPaths(contractreferencedir, options);
            var args = String.Format("{0} /regression /define:cci1only;clousot1 -framework:{4} -libpaths:{2} {3} {1}", options.ClousotOptions, absoluteSource, referencedir, libPathsString, options.Framework);
            WriteRSPFile(absoluteBinaryDir, options, args);
            if (options.Fast || System.Diagnostics.Debugger.IsAttached)
            {
                output.WriteLine("Calling CCI1Driver.Main with: {0}", args);
                // Use output to avoid Clousot from closing the Console
                Assert.Equal(0, Microsoft.Research.CodeAnalysis.CCI1Driver.Main(args.Split(' '), output));
            }
            else
                RunProcess(absoluteBinaryDir, options.GetFullExecutablePath(ClousotExe), args, output, options.TestName);
        }
        internal static void Clousot2(string absoluteSourceDir, string absoluteBinary, Options options, Output output)
        {
            var referencedir = options.MakeAbsolute(Path.Combine(ReferenceDirRoot, options.BuildFramework));
            var contractreferencedir = options.MakeAbsolute(Path.Combine(ContractReferenceDirRoot, options.ContractFramework));
            var absoluteBinaryDir = Path.GetDirectoryName(absoluteBinary);
            var absoluteSource = absoluteBinary;
            var libPathsString = FormLibPaths(contractreferencedir, options) + " /libpaths:.";
            var args = String.Format("{0} /show progress  /regression /define:cci2only;clousot2 -libpaths:{2} {3} {1}", options.ClousotOptions, absoluteSource, referencedir, libPathsString);
            WriteRSPFile(absoluteBinaryDir, options, args);
            if (options.Fast || System.Diagnostics.Debugger.IsAttached)
            {
                output.WriteLine("Calling CCI2Driver.Main with: {0}", args);
                // Use output to avoid Clousot2 from closing the Console
                Assert.Equal(0, Microsoft.Research.CodeAnalysis.CCI2Driver.Main(args.Split(' '), output));
            }
            else
                RunProcess(absoluteBinaryDir, options.GetFullExecutablePath(Clousot2Exe), args, output);
        }
        private static void WriteRSPFile(string dir, Options options, string args)
        {
            using (var file = new StreamWriter(Path.Combine(dir, options.TestName + ".rsp")))
            {
                file.WriteLine(args);
                file.Close();
            }
        }

        internal static void Clousot1Slicing(string absoluteSourceDir, string absoluteBinary, Options options, Output output)
        {
            var referencedir = options.MakeAbsolute(Path.Combine(ReferenceDirRoot, options.BuildFramework));
            var contractreferencedir = options.MakeAbsolute(Path.Combine(ContractReferenceDirRoot, options.ContractFramework));
            var absoluteBinaryDir = Path.GetDirectoryName(absoluteBinary);
            var absoluteSource = absoluteBinary;
            var libPathsString = FormLibPaths(contractreferencedir, options) + " /libpaths:.";
            var args = String.Format("{0} -cci1 /regression /define:cci1only;clousot1 -framework:{4} -libpaths:{2} {3} {1}", options.ClousotOptions, absoluteSource, referencedir, libPathsString, options.Framework);
            if (options.Fast || System.Diagnostics.Debugger.IsAttached)
            {
                output.WriteLine("Calling NewCCI2Driver.Main with: {0}", args);
                // Use output to avoid Clousot from closing the Console
                Assert.Equal(0, Microsoft.Research.CodeAnalysis.NewCCI2Driver.Main(args.Split(' '), output));
            }
            else
                RunProcess(absoluteBinaryDir, options.GetFullExecutablePath(Clousot2SlicingExe), args, output);
        }
        internal static void Clousot2Slicing(string absoluteSourceDir, string absoluteBinary, Options options, Output output)
        {
            var referencedir = options.MakeAbsolute(Path.Combine(ReferenceDirRoot, options.BuildFramework));
            var contractreferencedir = options.MakeAbsolute(Path.Combine(ContractReferenceDirRoot, options.ContractFramework));
            var absoluteBinaryDir = Path.GetDirectoryName(absoluteBinary);
            var absoluteSource = absoluteBinary;
            var libPathsString = FormLibPaths(contractreferencedir, options) + " /libpaths:.";
            var args = String.Format("{0} /show progress  /regression /define:cci2only;clousot2 -libpaths:{2} {3} {1}", options.ClousotOptions, absoluteSource, referencedir, libPathsString);
            if (options.Fast || System.Diagnostics.Debugger.IsAttached)
            {
                output.WriteLine("Calling NewCCI2Driver.Main with: {0}", args);
                // Use output to avoid Clousot2 from closing the Console
                Assert.Equal(0, Microsoft.Research.CodeAnalysis.NewCCI2Driver.Main(args.Split(' '), output));
            }
            else
                RunProcess(absoluteBinaryDir, options.GetFullExecutablePath(Clousot2SlicingExe), args, output);
        }
        internal static void Clousot2S(ITestOutputHelper testOutputHelper, string absoluteSourceDir, string absoluteBinary, Options options, Output output)
        {
            EnsureService(testOutputHelper, options);
            var referencedir = options.MakeAbsolute(Path.Combine(ReferenceDirRoot, options.BuildFramework));
            var contractreferencedir = options.MakeAbsolute(Path.Combine(ContractReferenceDirRoot, options.ContractFramework));
            var absoluteBinaryDir = Path.GetDirectoryName(absoluteBinary);
            var absoluteSource = absoluteBinary;
            var libPathsString = FormLibPaths(contractreferencedir, options) + " /libpaths:.";
            var args = String.Format("{0} /show progress  /regression /define:cci2only;clousot2 -libpaths:{2} {3} {1}", options.ClousotOptions, absoluteSource, referencedir, libPathsString);
            if (options.Fast || System.Diagnostics.Debugger.IsAttached)
            {
                output.WriteLine("Calling SDriver.Main with: {0}", args);
                // Use output to avoid Clousot2S from closing the Console
                Assert.Equal(0, Microsoft.Research.CodeAnalysis.SDriver.Main(args.Split(' '), output));
            }
            else
                RunProcess(absoluteBinaryDir, options.GetFullExecutablePath(Clousot2SExe), args, output);
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


        internal static string Build(Options options, string extraCompilerOptions, Output output, out string absoluteSourceDir)
        {
            var sourceFile = options.MakeAbsolute(options.SourceFile);
            var compilerpath = options.MakeAbsolute(Path.Combine(ToolsRoot, options.BuildFramework, options.Compiler));
            var contractreferencedir = options.MakeAbsolute(Path.Combine(ContractReferenceDirRoot, options.BuildFramework));
            var sourcedir = absoluteSourceDir = Path.GetDirectoryName(sourceFile);
            var outputdir = Path.Combine(sourcedir, "bin", options.BuildFramework);
            var extension = options.UseExe ? ".exe" : ".dll";
            var targetKind = options.UseExe ? "exe" : "library";
            var suffix = "_" + options.TestInstance;
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
                foreach (var root in new[] { ReferenceDirRoot, ContractReferenceDirRoot })
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

        public static void BuildAndAnalyze(ITestOutputHelper testOutputHelper, Options options)
        {
            var output = Output.ConsoleOutputFor(testOutputHelper, options.TestName);

            try
            {
                string absoluteSourceDir;
                var target = Build(options, "/d:CLOUSOT1", output.Item1, out absoluteSourceDir);
                if (target != null)
                {
                    Clousot(absoluteSourceDir, target, options, output.Item1);
                }
            }
            finally
            {
                testOutputHelper.WriteLine(output.Item2.ToString());
            }
        }

        public static void BuildAndAnalyze2(ITestOutputHelper testOutputHelper, Options options)
        {
            if (options.SkipForCCI2)
                return;

            var output = Output.ConsoleOutputFor(testOutputHelper, options.TestName);
            try
            {
                BuildAndAnalyze2(testOutputHelper, options, output.Item1);
            }
            finally
            {
                testOutputHelper.WriteLine(output.Item2.ToString());
            }
        }

        private static void BuildAndAnalyze2(ITestOutputHelper testOutputHelper, Options options, Output output)
        {
            string absoluteSourceDir;
            var target = Build(options, "/d:CLOUSOT2", output, out absoluteSourceDir);

            if (target != null)
                Clousot2(absoluteSourceDir, target, options, output);
        }

        public static void BuildAndAnalyze2S(ITestOutputHelper testOutputHelper, Options options)
        {
            if (options.SkipForCCI2)
                return;

            var output = Output.ConsoleOutputFor(testOutputHelper, options.TestName);
            try
            {
                BuildAndAnalyze2S(testOutputHelper, options, output.Item1);
            }
            finally
            {
                testOutputHelper.WriteLine(output.Item2.ToString());
            }
        }

        private static void BuildAndAnalyze2S(ITestOutputHelper testOutputHelper, Options options, Output output)
        {
            string absoluteSourceDir;
            var target = Build(options, "/d:CLOUSOT2", output, out absoluteSourceDir);

            if (target != null)
                Clousot2S(testOutputHelper, absoluteSourceDir, target, options, output);
        }

        public static void BuildAndAnalyze1Slicing(ITestOutputHelper testOutputHelper, Options options)
        {
            var output = Output.ConsoleOutputFor(testOutputHelper, options.TestName);
            try
            {
                BuildAndAnalyze1Slicing(options, output.Item1);
            }
            finally
            {
                testOutputHelper.WriteLine(output.Item2.ToString());
            }
        }

        private static void BuildAndAnalyze1Slicing(Options options, Output output)
        {
            string absoluteSourceDir;
            var target = Build(options, "/d:CLOUSOT1", output, out absoluteSourceDir);

            if (target != null)
                Clousot1Slicing(absoluteSourceDir, target, options, output);
        }

        public static void BuildAndAnalyze2Slicing(ITestOutputHelper testOutputHelper, Options options)
        {
            if (options.SkipForCCI2)
                return;

            if (options.SkipSlicing)
                return;

            var output = Output.ConsoleOutputFor(testOutputHelper, options.TestName);
            try
            {
                BuildAndAnalyze2Slicing(options, output.Item1);
            }
            finally
            {
                testOutputHelper.WriteLine(output.Item2.ToString());
            }
        }

        private static void BuildAndAnalyze2Slicing(Options options, Output output)
        {
            string absoluteSourceDir;
            var target = Build(options, "/d:CLOUSOT2 /d:SLICING", output, out absoluteSourceDir);

            if (target != null)
                Clousot2Slicing(absoluteSourceDir, target, options, output);
        }

        #region Parallel tests

        private const string DefaultBeginMessage = "Build and analysis launched. Look at End results.";
        private static bool SkipForCCI2(Options options) { return options.SkipForCCI2; }

        private static AsyncTestDriver asyncFast2;
        private static AsyncTestDriver async2S;

        public static AsyncTestDriver AsyncFast2(ITestOutputHelper testOutputHelper)
        {
            if (asyncFast2 == null)
                asyncFast2 = new AsyncTestDriver(testOutputHelper, BuildAndAnalyze2, SkipForCCI2, AsyncTestDriver.MaxWaitHandles_AllButOne) { BeginMessage = DefaultBeginMessage };

            return asyncFast2;
        }

        public static AsyncTestDriver Async2S(ITestOutputHelper testOutputHelper)
        {
            if (async2S == null)
                async2S = new AsyncTestDriver(testOutputHelper, BuildAndAnalyze2S, SkipForCCI2) { BeginMessage = DefaultBeginMessage };

            return async2S;
        }

        #endregion

        #region Service actions

        private static Process serviceProcess;
        private static Object serviceProcessLock = new Object();

        private static void EnsureService(ITestOutputHelper testOutputHelper, Options options)
        {
            lock (serviceProcessLock) // prevent the service to be run twice at the same time
            {
                if (serviceProcess == null)
                    StartService(testOutputHelper, options);
                Assert.False(serviceProcess.HasExited, "Service needed but service process already exited");
            }
        }

        private static void StartService(ITestOutputHelper testOutputHelper, Options options)
        {
            if (serviceProcess != null)
                StopService();

            // First make sure another instance is not already running (because we don't know which version is running)
            foreach (var process in Process.GetProcessesByName(Path.GetFileNameWithoutExtension(ClousotServiceHostExe)))
            {
                process.CloseMainWindow();
                if (!process.WaitForExit(1000))
                    process.Kill();
            }

            var serviceHostDir = options.MakeAbsolute(Path.GetDirectoryName(ClousotServiceHostExe));

            // note: we do not want to use ClousotServiceHostExe from the deployment directory because the app.config will be missing
            serviceProcess = StartServiceProcess(serviceHostDir, options.MakeAbsolute(ClousotServiceHostExe), "", Output.Ignore(testOutputHelper));
        }

        public static void Cleanup()
        {
            KillRemainingClients();
            StopService();
        }

        private static void KillRemainingClients()
        {
            foreach (var process in Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Clousot2SExe)))
            {
                process.CloseMainWindow();
                if (!process.WaitForExit(1000))
                    process.Kill();
            }
        }

        private static void StopService()
        {
            lock (serviceProcessLock)
            {
                if (serviceProcess == null)
                    return;

                serviceProcess.StandardInput.WriteLine();
                if (!serviceProcess.WaitForExit(2000))
                {
                    serviceProcess.Close();
                    if (!serviceProcess.WaitForExit(2000))
                    {
                        serviceProcess.Kill();
                        Assert.True(serviceProcess.WaitForExit(2000), "{0} did not want to exit");
                    }
                }
                Assert.Equal(0, serviceProcess.ExitCode);
                serviceProcess.Dispose();
                serviceProcess = null;
            }
        }

        private static Process StartServiceProcess(string cwd, string tool, string arguments, Output output, string writeBatchFile = null)
        {
            ProcessStartInfo i = new ProcessStartInfo(tool, arguments);
            output.WriteLine("Running '{0}'", i.FileName);
            output.WriteLine("         {0}", i.Arguments);
            i.RedirectStandardInput = true;
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

            var p = Process.Start(i);

            p.OutputDataReceived += output.OutputDataReceivedEventHandler;
            p.ErrorDataReceived += output.ErrDataReceivedEventHandler;
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            Assert.False(p.WaitForExit(1000), string.Format("{0} exited too quickly", i.FileName));

            return p;
        }

        #endregion
    }
}
