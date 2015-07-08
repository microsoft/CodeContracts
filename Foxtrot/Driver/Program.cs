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
using System.Collections; // for IDictionary;
using System.Compiler;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Contracts.Foxtrot.Driver
{
    /// <summary>
    /// Main program for Foxtrot.
    /// </summary>
    public sealed class Program
    {
        // Private fields

        private static TypeNode userSpecifiedContractType;
        private static ContractNodes DefaultContractLibrary;
        private static ContractNodes BackupContractLibrary;

        private static string originalAssemblyName = null;
        private static FoxtrotOptions options;

        private const int LeaderBoard_RewriterId = 0x1;
        private const int LeaderBoardFeatureMask_Rewriter = LeaderBoard_RewriterId << 12;

        [Flags]
        private enum LeaderBoardFeatureId
        {
            StandardMode = 0x10,
            ThrowOnFailure = 0x20,
        }

        private static int LeaderBoardFeature(FoxtrotOptions options)
        {
            Contract.Requires(options != null);

            var result = options.level | LeaderBoardFeatureMask_Rewriter;
            if (options.assemblyMode == FoxtrotOptions.AssemblyMode.standard)
            {
                result |= (int) LeaderBoardFeatureId.StandardMode;
            }

            if (options.throwOnFailure)
            {
                result |= (int) LeaderBoardFeatureId.ThrowOnFailure;
            }

            return result;
        }

        /// <summary>
        /// Parses command line arguments and extracts, verifies, and/or rewrites a given assembly.
        /// </summary>
        /// <param name="args">Command line arguments, or <c>null</c> for help to be written to the console.</param>
        public static int Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                var r = InternalMain(args);
                return r;
            }
            finally
            {
                var delta = stopWatch.Elapsed;
                Console.WriteLine("elapsed time: {0}ms", delta.TotalMilliseconds);
            }
        }

        private static int InternalMain(string[] args)
        {
            options = new FoxtrotOptions();
            options.Parse(args);

            if (!options.nologo)
            {
                var version = typeof (FoxtrotOptions).Assembly.GetName().Version;

                Console.WriteLine("Microsoft (R) .NET Contract Rewriter Version {0}", version);
                Console.WriteLine("Copyright (C) Microsoft Corporation. All rights reserved.");
                Console.WriteLine("");
            }

            if (options.HasErrors)
            {
                options.PrintErrorsAndExit(Console.Out);
                return -1;
            }
            if (options.HelpRequested)
            {
                options.PrintOptions("", Console.Out);
                return 0;
            }

            if (options.breakIntoDebugger)
            {
                Debugger.Launch();
            }

#if DEBUG
            if (options.nobox)
            {
                Debug.Listeners.Clear();

                // listen for failed assertions
                Debug.Listeners.Add(new ExitTraceListener());
            }
#else
      Debug.Listeners.Clear();
#endif

            if (options.repro)
            {
                WriteReproFile(args);
            }

            var resolver = new AssemblyResolver(
                options.resolvedPaths, options.libpaths, options.debug,
                options.shortBranches, options.verbose > 2,
                PostLoadExtractionHook);

            GlobalAssemblyCache.probeGAC = options.useGAC;

            // Connect to LeaderBoard

            SendLeaderBoardRewriterFeature(options);

            int errorReturnValue = -1;

            IDictionary assemblyCache = new Hashtable();

            // Trigger static initializer of SystemTypes
            var savedGACFlag = GlobalAssemblyCache.probeGAC;
            
            GlobalAssemblyCache.probeGAC = false;
            
            TypeNode dummy = SystemTypes.Object;

            TargetPlatform.Clear();
            TargetPlatform.AssemblyReferenceFor = null;
            GlobalAssemblyCache.probeGAC = savedGACFlag;

            try
            {
                // Validate the command-line arguments.

                if (options.output != "same")
                {
                    if (!Path.IsPathRooted(options.output))
                    {
                        string s = Directory.GetCurrentDirectory();
                        options.output = Path.Combine(s, options.output);
                    }
                }

                if (options.assembly == null && options.GeneralArguments.Count == 1)
                {
                    options.assembly = options.GeneralArguments[0];
                }

                if (!File.Exists(options.assembly))
                {
                    throw new FileNotFoundException(String.Format("The given assembly '{0}' does not exist.", options.assembly));
                }

                InitializePlatform(resolver, assemblyCache);

                if (options.passthrough)
                {
                    options.rewrite = false;
                }
                if (!(options.rewrite || options.passthrough))
                {
                    Console.WriteLine("Error: Need to specify at least one of: /rewrite, /pass");
                    options.PrintOptions("", Console.Out);

                    return errorReturnValue;
                }

                if (options.extractSourceText && !options.debug)
                {
                    Console.WriteLine("Error: Cannot specify /sourceText without also specifying /debug");
                    options.PrintOptions("", Console.Out);

                    return errorReturnValue;
                }

                if (!(0 <= options.level && options.level <= 4))
                {
                    Console.WriteLine("Error: incorrect /level: {0}. /level must be between 0 and 4 (inclusive)", options.level);
                    return errorReturnValue;
                }

                if (options.automaticallyLookForOOBs && options.contracts != null && 0 < options.contracts.Count)
                {
                    Console.WriteLine("Error: Out of band contracts are being automatically applied, all files specified using the contracts option are ignored.");
                    return errorReturnValue;
                }

                // Sanity check: just make sure that all files specified for out-of-band contracts actually exist
                bool atLeastOneOobNotFound = false;
                if (options.contracts != null)
                {
                    foreach (string oob in options.contracts)
                    {
                        bool found = false;
                        if (File.Exists(oob)) found = true;

                        if (!found)
                        {
                            if (options.libpaths != null)
                            {
                                foreach (string dir in options.libpaths)
                                {
                                    if (File.Exists(Path.Combine(dir, oob)))
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                            }

                            if (!found)
                            {
                                Console.WriteLine("Error: Contract file '" + oob + "' could not be found");
                                atLeastOneOobNotFound = true;
                            }
                        }
                    }
                }

                if (atLeastOneOobNotFound)
                {
                    return errorReturnValue;
                }

                // Load the assembly to be rewritten

                originalAssemblyName = Path.GetFileNameWithoutExtension(options.assembly);
                AssemblyNode assemblyNode = AssemblyNode.GetAssembly(options.assembly,
                    TargetPlatform.StaticAssemblyCache, true, options.debug, true, options.shortBranches
                    , delegate(AssemblyNode a)
                    {
                        //Console.WriteLine("Loaded '" + a.Name + "' from '" + a.Location.ToString() + "'");
                        PossiblyLoadOOB(resolver, a, originalAssemblyName);
                    });

                if (assemblyNode == null)
                    throw new FileLoadException("The given assembly could not be loaded.", options.assembly);

                // Check to see if any metadata errors were reported

                if (assemblyNode.MetadataImportErrors != null && assemblyNode.MetadataImportErrors.Count > 0)
                {
                    string msg = "\tThere were errors reported in " + assemblyNode.Name + "'s metadata.\n";
                    foreach (Exception e in assemblyNode.MetadataImportErrors)
                    {
                        msg += "\t" + e.Message;
                    }

                    Console.WriteLine(msg);

                    throw new InvalidOperationException("Foxtrot: " + msg);
                }
                else
                {
                    //Console.WriteLine("\tThere were no errors reported in {0}'s metadata.", assemblyNode.Name);
                }

                // Load the rewriter assembly if any

                AssemblyNode rewriterMethodAssembly = null;
                if (options.rewriterMethods != null && 0 < options.rewriterMethods.Length)
                {
                    string[] pieces = options.rewriterMethods.Split(',');
                    if (!(pieces.Length == 2 || pieces.Length == 3))
                    {
                        Console.WriteLine("Error: Need to provide two or three comma separated arguments to /rewriterMethods");
                        options.PrintOptions("", Console.Out);

                        return errorReturnValue;
                    }

                    string assemName = pieces[0];
                    rewriterMethodAssembly = resolver.ProbeForAssembly(assemName, null, resolver.AllExt);
                    if (rewriterMethodAssembly == null)
                    {
                        Console.WriteLine("Error: Could not open assembly '" + assemName + "'");
                        return errorReturnValue;
                    }

                    string nameSpaceName = null;
                    string bareClassName = null;

                    if (pieces.Length == 2)
                    {
                        // interpret A.B.C as namespace A.B and class C
                        // no nested classes allowed.
                        string namespaceAndClassName = pieces[1];

                        int lastDot = namespaceAndClassName.LastIndexOf('.');

                        nameSpaceName = lastDot == -1 ? "" : namespaceAndClassName.Substring(0, lastDot);
                        bareClassName = namespaceAndClassName.Substring(lastDot + 1);

                        userSpecifiedContractType = rewriterMethodAssembly.GetType(Identifier.For(nameSpaceName),
                            Identifier.For(bareClassName));
                    }
                    else
                    {
                        // pieces.Length == 3
                        // namespace can be A.B and class can be C.D
                        nameSpaceName = pieces[1];
                        bareClassName = pieces[2];
                        userSpecifiedContractType = GetPossiblyNestedType(rewriterMethodAssembly, nameSpaceName, bareClassName);
                    }

                    if (userSpecifiedContractType == null)
                    {
                        Console.WriteLine("Error: Could not find type '" + bareClassName + "' in the namespace '" +
                                          nameSpaceName + "' in the assembly '" + assemName + "'");

                        return errorReturnValue;
                    }
                }

                // Load the ASTs for all of the contract methods

                AssemblyNode contractAssembly = null;
                if (!options.passthrough)
                {
                    // The contract assembly should be determined in the following order:
                    // 0. if the option contractLibrary was specified, use that.
                    // 1. the assembly being rewritten
                    // 2. the system assembly
                    // 3. the microsoft.contracts library

                    if (options.contractLibrary != null)
                    {
                        contractAssembly = resolver.ProbeForAssembly(options.contractLibrary,
                            Path.GetDirectoryName(options.assembly), resolver.EmptyAndDllExt);

                        if (contractAssembly != null)
                            DefaultContractLibrary = ContractNodes.GetContractNodes(contractAssembly, options.EmitError);

                        if (contractAssembly == null || DefaultContractLibrary == null)
                        {
                            Console.WriteLine("Error: could not load Contracts API from assembly '{0}'", options.contractLibrary);
                            return -1;
                        }
                    }
                    else
                    {
                        if (DefaultContractLibrary == null)
                        {
                            // See if contracts are in the assembly we're rewriting
                            DefaultContractLibrary = ContractNodes.GetContractNodes(assemblyNode, options.EmitError);
                        }

                        if (DefaultContractLibrary == null)
                        {
                            // See if contracts are in Mscorlib
                            DefaultContractLibrary = ContractNodes.GetContractNodes(SystemTypes.SystemAssembly,
                                options.EmitError);
                        }

                        // try to load Microsoft.Contracts.dll
                        var microsoftContractsLibrary = resolver.ProbeForAssembly("Microsoft.Contracts",
                            Path.GetDirectoryName(options.assembly), resolver.DllExt);

                        if (microsoftContractsLibrary != null)
                        {
                            BackupContractLibrary = ContractNodes.GetContractNodes(microsoftContractsLibrary, options.EmitError);
                        }

                        if (DefaultContractLibrary == null && BackupContractLibrary != null)
                        {
                            DefaultContractLibrary = BackupContractLibrary;
                        }
                    }
                }

                if (DefaultContractLibrary == null)
                {
                    if (options.output == "same")
                    {
                        Console.WriteLine(
                            "Warning: Runtime Contract Checking requested, but contract class could not be found. Did you forget to reference the contracts assembly?");

                        return 0; // Returning success so that it doesn't break any build processes.
                    }
                    else
                    {
                        // Then an output file was specified (assume it is not the same as the input assembly, but that could be checked here).
                        // In that case, consider it an error to not have found a contract class.
                        // No prinicpled reason, but this is a common pitfall that several users have run into and the rewriter just 
                        // didn't do anything without giving any reason.
                        Console.WriteLine(
                            "Error: Runtime Contract Checking requested, but contract class could not be found. Did you forget to reference the contracts assembly?");

                        return errorReturnValue;
                    }
                }

                if (0 < options.verbose)
                {
                    Console.WriteLine("Trace: Using '" + DefaultContractLibrary.ContractClass.DeclaringModule.Location +
                                      "' for the definition of the contract class");
                }

                // Make sure we extract contracts from the system assembly and the system.dll assemblies.
                // As they are already loaded, they will never trigger the post assembly load event.
                // But even if we are rewriting one of these assemblies (and so *not* trying to extract
                // the contracts at this point), still need to hook up our resolver to them. If this
                // isn't done, then any references chased down from them might not get resolved.

                bool isPreloadedAssembly = false;

                CheckIfPreloaded(resolver, assemblyNode, SystemTypes.SystemAssembly, ref isPreloadedAssembly);
                CheckIfPreloaded(resolver, assemblyNode, SystemTypes.SystemDllAssembly, ref isPreloadedAssembly);

                //CheckIfPreloaded(resolver, assemblyNode, SystemTypes.SystemRuntimeWindowsRuntimeAssembly, ref isPreloadedAssembly);
                //CheckIfPreloaded(resolver, assemblyNode, SystemTypes.SystemRuntimeWindowsRuntimeUIXamlAssembly, ref isPreloadedAssembly);

                if (!isPreloadedAssembly)
                {
                    assemblyNode.AssemblyReferenceResolution += resolver.ResolveAssemblyReference;
                }

                MikesArchitecture(resolver, assemblyNode, DefaultContractLibrary, BackupContractLibrary);

                return options.GetErrorCount();
            }
            catch (Exception exception)
            {
                SendLeaderBoardFailure();

                // Redirect the exception message to the console and quit.
                Console.Error.WriteLine(new System.CodeDom.Compiler.CompilerError(exception.Source, 0, 0, null, exception.Message));
                return errorReturnValue;
            }
            finally
            {
                // Reset statics

                userSpecifiedContractType = null;
                DefaultContractLibrary = null;
                BackupContractLibrary = null;
                originalAssemblyName = null;
                options = null;

                // eagerly close all assemblies due to pdb file locking issues
                DisposeAssemblies(assemblyCache.Values);

                // copy needed since Dispose actually removes things from the StaticAssemblyCache
                object[] assemblies = new object[TargetPlatform.StaticAssemblyCache.Values.Count];

                TargetPlatform.StaticAssemblyCache.Values.CopyTo(assemblies, 0);
                DisposeAssemblies(assemblies);
            }
        }

        private static void CheckIfPreloaded(AssemblyResolver resolver, AssemblyNode assemblyNode,
            AssemblyNode preloaded, ref bool isPreloadedAssembly)
        {
            Contract.Requires(preloaded == null || resolver != null);

            if (preloaded == null) return;

            resolver.PostLoadHook(preloaded);

            if (assemblyNode == preloaded)
            {
                isPreloadedAssembly = true;
            }
        }

        private static void InitializePlatform(AssemblyResolver resolver, System.Collections.IDictionary assemblyCache)
        {
            // Initialize CCI's platform if necessary

            if (options.targetplatform != null && options.targetplatform != "")
            {
                string platformDir = Path.GetDirectoryName(options.targetplatform);
                if (platformDir == "")
                {
                    platformDir = ".";
                }

                if (!Directory.Exists(platformDir))
                    throw new ArgumentException("Directory '" + platformDir + "' doesn't exist.");

                if (!File.Exists(options.targetplatform))
                    throw new ArgumentException("Cannot find the file '" + options.targetplatform +
                                                       "' to use as the system assembly.");

                TargetPlatform.GetDebugInfo = options.debug;
                switch (options.framework)
                {
                    case "v4.5":
                        TargetPlatform.SetToV4_5(platformDir);
                        break;

                    case "v4.0":
                        TargetPlatform.SetToV4(platformDir);
                        break;

                    case "v3.5":
                    case "v3.0":
                    case "v2.0":
                        TargetPlatform.SetToV2(platformDir);
                        break;

                    default:
                        if (platformDir.Contains("v4.0"))
                        {
                            TargetPlatform.SetToV4(platformDir);
                        }
                        else if (platformDir.Contains("v4.5"))
                        {
                            TargetPlatform.SetToV4_5(platformDir);
                        }
                        else if (platformDir.Contains("v3.5"))
                        {
                            TargetPlatform.SetToV2(platformDir);
                        }
                        else
                        {
                            TargetPlatform.SetToPostV2(platformDir);
                        }
                        break;
                }

                SystemAssemblyLocation.Location = options.targetplatform;

                // When rewriting the framework assemblies, it might not be the case that the input assembly (e.g., System.dll)
                // is in the same directory as the target platform that the rewriter has been asked to run on.
                SystemRuntimeWindowsRuntimeAssemblyLocation.Location =
                    SetPlatformAssemblyLocation("System.Runtime.WindowsRuntime.dll");

                SystemRuntimeWindowsRuntimeUIXamlAssemblyLocation.Location =
                    SetPlatformAssemblyLocation("System.Runtime.WindowsRuntime.UI.Xaml.dll");

                SystemDllAssemblyLocation.Location = SetPlatformAssemblyLocation("System.dll");

                SystemAssemblyLocation.SystemAssemblyCache = assemblyCache;
            }
            else
            {
                if (options.resolvedPaths != null)
                {
                    foreach (var path in options.resolvedPaths)
                    {
                        if (path == null) continue;

                        if (string.IsNullOrEmpty(path.Trim())) continue;

                        var candidate = Path.GetFullPath(path);
                        if (candidate.EndsWith(@"\mscorlib.dll", StringComparison.OrdinalIgnoreCase) &&
                            File.Exists(candidate))
                        {
                            // found our platform
                            var dir = Path.Combine(Path.GetPathRoot(candidate), Path.GetDirectoryName(candidate));

                            SelectPlatform(assemblyCache, dir, candidate);

                            goto doneWithLibPaths;
                        }
                    }
                }

                if (options.libpaths != null)
                {
                    // try to infer platform from libpaths
                    foreach (var path in options.libpaths)
                    {
                        if (path == "") continue;

                        var corlib = Path.Combine(path, "mscorlib.dll");

                        if (File.Exists(corlib))
                        {
                            // this should be our platform mscorlib.dll
                            SelectPlatform(assemblyCache, path, corlib);
                            goto doneWithLibPaths;
                        }
                    }
                }
            }
            
        doneWithLibPaths:
            ;

            if (string.IsNullOrEmpty(options.targetplatform))
            {
                SystemTypes.Initialize(false, true);

                // try to use mscorlib of assembly to rewrite
                AssemblyNode target = AssemblyNode.GetAssembly(options.assembly, TargetPlatform.StaticAssemblyCache,
                    true, false, true);

                if (target != null)
                {
                    var majorTargetVersion = target.MetadataFormatMajorVersion;
                    var corlib = SystemTypes.SystemAssembly.Location;
                    var path = Path.GetDirectoryName(corlib);

                    if (SystemTypes.SystemAssembly.Version.Major != majorTargetVersion)
                    {
                        var versionDir = String.Format(@"..\{0}", target.TargetRuntimeVersion);
                        path = Path.Combine(path, versionDir);
                        corlib = Path.Combine(path, "mscorlib.dll");
                        if (!File.Exists(corlib))
                        {
                            throw new ArgumentException(
                                "Cannot determine target runtime version. Please specify /resolvedPaths, /libpaths or /targetplatform");
                        }
                    }

                    if (majorTargetVersion == 2)
                    {
                        TargetPlatform.SetToV2(path);
                    }
                    else
                    {
                        TargetPlatform.SetToV4(path);
                    }

                    SystemAssemblyLocation.Location = corlib;

                    Contract.Assume(path != null);

                    SystemDllAssemblyLocation.Location = Path.Combine(path, "System.dll");
                    SystemRuntimeWindowsRuntimeAssemblyLocation.Location = Path.Combine(path,
                        "System.Runtime.WindowsRuntime.dll");
                    
                    SystemRuntimeWindowsRuntimeUIXamlAssemblyLocation.Location = Path.Combine(path,
                        "System.Runtime.WindowsRuntime.UI.Xaml.dll");
                    SystemAssemblyLocation.SystemAssemblyCache = assemblyCache;

                    options.targetplatform = corlib;
                }
            }

            // always make sure we have the platform assemblies loaded
            if (TargetPlatform.PlatformAssembliesLocation == null)
            {
                throw new ArgumentException("Could not find the platform assemblies.");
            }

            if (!string.IsNullOrEmpty(options.targetplatform))
            {
                TargetPlatform.AssemblyReferenceFor = null;

                assemblyCache.Clear();
                SystemTypes.Initialize(false, true, resolver.PostLoadHook);
            }

            // Force SystemTypes.Initialize() to be run before attaching any out-of-band contracts
            // Otherwise, if there is an out-of-band contract for mscorlib, then types in SystemTypes
            // get loaded twice.
            AssemblyNode corlibAssembly = SystemTypes.SystemAssembly;

            // Add the system assembly to the cache so if we rewrite it itself we won't load it twice
            TargetPlatform.UseGenerics = true;
            TargetPlatform.StaticAssemblyCache.Add(corlibAssembly.Name, corlibAssembly);
        }

        private static string SetPlatformAssemblyLocation(string platformAssemblyName)
        {
            Contract.Requires(platformAssemblyName != null);

            var result = Path.Combine(Path.GetDirectoryName(options.targetplatform), platformAssemblyName);
            ;
            var assemblyFileName = Path.GetFileName(options.assembly);
            if (assemblyFileName.Equals(platformAssemblyName, StringComparison.OrdinalIgnoreCase) &&
                File.Exists(options.assembly))
            {
                result = options.assembly;
            }

            return result;
        }

        private static void SelectPlatform(IDictionary assemblyCache, string path, string corlib)
        {
            Contract.Requires(corlib != null);
            Contract.Requires(path != null);

            Debug.Assert(corlib.StartsWith(path));
            SystemTypes.Clear();
            
            TargetPlatform.GetDebugInfo = options.debug;
            TargetPlatform.Clear();
            
            if (path.Contains("v4.0") || path.Contains("v4.5"))
            {
                TargetPlatform.SetToV4(path);
            }
            else
            {
                TargetPlatform.SetToV2(path);
            }

            SystemAssemblyLocation.Location = corlib;
            
            SystemRuntimeWindowsRuntimeAssemblyLocation.Location = Path.Combine(path,
                "System.Runtime.WindowsRuntime.dll");
            SystemRuntimeWindowsRuntimeUIXamlAssemblyLocation.Location = Path.Combine(path,
                "System.Runtime.WindowsRuntime.UI.Xaml.dll");
            
            SystemDllAssemblyLocation.Location = Path.Combine(path, "System.dll");
            SystemAssemblyLocation.SystemAssemblyCache = assemblyCache;

            options.targetplatform = corlib;
        }

        private static void SendLeaderBoardFailure()
        {
#if LeaderBoard
      var version = typeof(FoxtrotOptions).Assembly.GetName().Version;

      LeaderBoard.LeaderBoardAPI.SendLeaderBoardFailure(LeaderBoard_RewriterId, version);
#endif
        }

        private static void SendLeaderBoardRewriterFeature(FoxtrotOptions options)
        {
#if LeaderBoard
        LeaderBoard.LeaderBoardAPI.SendLeaderBoardFeatureUse(LeaderBoardFeature(options));
#endif
        }

        private static void WriteReproFile(string[] args)
        {
            try
            {
                var file = new StreamWriter("repro.bat");
#if false
      file.Write(@"c:\users\maf\cci1\foxtrot\driver\bin\debug\foxtrot.exe ");
      foreach (var arg in args)
      {
        file.Write("\"{0}\" ", arg);
      }
#else
                file.Write(Environment.CommandLine.Replace("-repro", ""));
#endif
                file.WriteLine(" %1 %2 %3 %4 %5");
                file.Close();
            }
            catch
            {
            }
        }

        private static TypeNode GetPossiblyNestedType(AssemblyNode assem, string namespaceName, string className)
        {
            Contract.Requires(assem != null);
            Contract.Requires(className != null);

            var ns = Identifier.For(namespaceName);

            string[] pieces = className.Split('.');

            // Get outermost type
            string outerMost = pieces[0];
            TypeNode t = assem.GetType(ns, Identifier.For(outerMost));

            if (t == null) return null;

            for (int i = 1; i < pieces.Length; i++)
            {
                var piece = pieces[i];
                t = t.GetNestedType(Identifier.For(piece));
                
                if (t == null) return null;
            }

            return t;
        }

        private static void DisposeAssemblies(IEnumerable assemblies)
        {
            try
            {
                Contract.Assume(assemblies != null);

                foreach (Module assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        assembly.Dispose();
                    }
                }
            }
            catch
            {
            }
        }

#if false
    /// <summary>
    /// Resolves assembly references based on the library paths specified.
    /// Tries to resolve to ".dll" or ".exe". First found wins.
    /// </summary>
    /// <param name="assemblyReference">Reference to resolve.</param>
    /// <param name="referencingModule">Referencing module.</param>
    /// <returns>The resolved assembly node (null if not found).</returns>
    private static AssemblyNode AssemblyReferenceResolution (AssemblyReference assemblyReference, Module referencingModule) {
      AssemblyNode a = ProbeForAssembly(assemblyReference.Name, referencingModule.Directory, DllAndExeExt);
      return a;
    }

    private static AssemblyNode ProbeForAssemblyWithExtension(string directory, string assemblyName, string[] exts)
    {
        do
        {
            var result = ProbeForAssemblyWithExtensionAtomic(directory, assemblyName, exts);
            if (result != null) return result;

            if (directory.EndsWith("CodeContracts")) return null;

            // if directory is a profile directory, we need to look in parent directory too
            if (directory.Contains("Profile") || directory.Contains("profile"))
            {
                try
                {
                    var parent = Directory.GetParent(directory);
                    directory = parent.FullName;
                }
                catch
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        while (!String.IsNullOrWhiteSpace(directory));
        return null;
    }

    private static AssemblyNode ProbeForAssemblyWithExtensionAtomic(string directory, string assemblyName, string[] exts)
    {
      foreach (string ext in exts)
      {
        bool tempDebugInfo = options.debug;
        string fullName = Path.Combine(directory, assemblyName + ext);
        LoadTracing("Attempting load from:");
        LoadTracing(fullName);
        if (File.Exists(fullName))
        {
          if (tempDebugInfo)
          {
            // Don't pass the debugInfo flag to GetAssembly unless the PDB file exists.
            string pdbFullName;
            if (ext == "")
            {
              pdbFullName = Path.Combine(directory, Path.GetFileNameWithoutExtension(assemblyName) + ".pdb");
            }
            else
            {
              pdbFullName = Path.Combine(directory, assemblyName + ".pdb");
            }
            if (!File.Exists(pdbFullName))
            {
              Trace.WriteLine(String.Format("Can not find PDB file. Debug info will not be available for assembly '{0}'.", assemblyName));
              tempDebugInfo = false;
            }
          }
          Trace.WriteLine(string.Format("Resolved assembly reference '{0}' to '{1}'. (Using directory {2})", assemblyName, fullName, directory));
          var result = AssemblyNode.GetAssembly(
              fullName, // path to assembly
              TargetPlatform.StaticAssemblyCache,  // global cache to use for assemblies
              true, // doNotLockFile
              tempDebugInfo, // getDebugInfo
              true, // useGlobalCache
              options.shortBranches,  // preserveShortBranches
              PostLoadExtractionHook
              );
          return result;
        }
      }
      return null;
    }

    private static void LoadTracing(string msg)
    {
      if (options.verbose > 2)
      {
        Console.WriteLine("Trace: {0}", msg);
      }
      Trace.WriteLine(msg);
    }
    static string[] EmptyExt = new[] { "" };
    static string[] DllExt = new[] { ".dll", ".winmd" };
    static string[] DllAndExeExt = new[] { ".dll", ".winmd", ".exe" };
    static string[] EmptyAndDllExt = new[] { "", ".dll", ".winmd" };
    static string[] AllExt = new[] { "", ".dll", ".winmd", ".exe" };

    private static AssemblyNode ProbeForAssembly (string assemblyName, string referencingModuleDirectory, string[] exts) {
      AssemblyNode a = null;
      try {
        LoadTracing("Attempting to load");
        LoadTracing(assemblyName);
        // Location Priority (in decreasing order):
        // 0. list of candidate full paths specified by client
        // 1. list of directories specified by client
        // 2. referencing Module's directory
        // 3. directory original assembly was in
        //
        // Extension Priority (in decreasing order):
        // dll, exe, (any others?)
        // Check user-supplied candidate paths
        LoadTracing("AssemblyResolver: Attempting user-supplied candidates.");
        if (options.resolvedPaths != null)
        {
          foreach (string candidate in options.resolvedPaths)
          {
            var candidateAssemblyName = Path.GetFileNameWithoutExtension(candidate);
            if (String.Compare(candidateAssemblyName, assemblyName, StringComparison.OrdinalIgnoreCase) != 0) continue;

            if (File.Exists(candidate))
            {
              a = ProbeForAssemblyWithExtension("", candidate, EmptyExt);
              break;
            }
          }
        }
        else
        {
          Trace.WriteLine("\tAssemblyResolver: No user-supplied resolvedPaths.");
        }
        if (a == null)
        {
          if (options.resolvedPaths != null)
          {
            Trace.WriteLine("\tAssemblyResolver: Did not find assembly in user-supplied resolvedPaths candidates.");
          }
        }
        else
        {
          goto End;
        } 

        // Check user-supplied search directories
        LoadTracing("AssemblyResolver: Attempting user-supplied directories.");
        if (options.libpaths != null)
        {
          foreach (string dir in options.libpaths)
          {
            a = ProbeForAssemblyWithExtension(dir, assemblyName, exts);
            if (a != null)
              break;
          }
        }
        else
        {
          Trace.WriteLine("\tAssemblyResolver: No user-supplied directories.");
        }
        if (a == null)
        {
          if (options.libpaths != null)
          {
            Trace.WriteLine("\tAssemblyResolver: Did not find assembly in user-supplied directories.");
          }
        }
        else
        {
          goto End;
        }
        
        // Check referencing module's directory
        Trace.WriteLine("\tAssemblyResolver: Attempting referencing assembly's directory.");
        if (referencingModuleDirectory != null) {
          a = ProbeForAssemblyWithExtension(referencingModuleDirectory, assemblyName, exts);
        }
        else
        {
          Trace.WriteLine("\t\tAssemblyResolver: Referencing assembly's directory is null.");
        }
        if (a == null) {
          if (referencingModuleDirectory != null) {
            Trace.WriteLine("\tAssemblyResolver: Did not find assembly in referencing assembly's directory.");
          }
        } else {
          goto End;
        }
        
        // Check input directory
        Trace.WriteLine("\tAssemblyResolver: Attempting input directory.");
        a = ProbeForAssemblyWithExtension("", assemblyName, exts);
        if (a == null) {
          Trace.WriteLine("\tAssemblyResolver: Did not find assembly in input directory.");
        } else {
          goto End;
        }
        
      End:
        if (a == null)
        {
          Trace.WriteLine("AssemblyResolver: Unable to resolve reference. (It still might be found, e.g., in the GAC.)");
        }
      } catch (Exception e) {
        Trace.WriteLine("AssemblyResolver: Exception occurred. Unable to resolve reference.");
        Trace.WriteLine("Inner exception: " + e.ToString());
      }
      return a;
    }
#endif

        /// <summary>
        /// Moves external resource files referenced in an assembly node to the specified path.
        /// </summary>
        /// <param name="assemblyNode">Assembly node containing the resource references.</param>
        /// <param name="path">Directory to move the external files to.</param>
        private static void MoveModuleResources(AssemblyNode assemblyNode, string directory)
        {
            Contract.Requires(assemblyNode != null);

            for (int i = 0, n = assemblyNode.Resources == null ? 0 : assemblyNode.Resources.Count; i < n; i++)
            {
                Resource r = assemblyNode.Resources[i];
                if (r.Data != null) continue; // not an external resource

                if (r.DefiningModule == null) continue; // error? should have a defining module that represents the file

                string newPath = Path.Combine(directory, r.Name);

                if (!directory.Equals(r.DefiningModule.Directory, StringComparison.OrdinalIgnoreCase))
                {
                    if (File.Exists(newPath))
                    {
                        Console.WriteLine("The file '" + newPath +
                                          "' already exists, so is not being copied. Make sure this is okay!");
                    }
                    else
                    {
                        File.Copy(r.DefiningModule.Location, newPath);
                    }
                }

                r.DefiningModule.Location = newPath;
            }
        }

#if KAEL
      private static void KaelsArchitecture(AssemblyNode assemblyNode) {

    // Finish decompiling expressions where CCI left off.
        new Abnormalizer().Visit(assemblyNode);
        

    // Check and extract all inline foxtrot contracts and place them in the object model.
        Checker checker = new Checker(new ContractNodes(assemblyNode));
        bool errorFound = false;
        checker.ErrorFound += delegate(System.CodeDom.Compiler.CompilerError error) {
          if (!error.IsWarning || warningLevel > 0) {
            Console.WriteLine(error.ToString());
          }
          errorFound |= !error.IsWarning;
        };
        checker.Visit(assemblyNode);
        if (errorFound)
          return;
        

        if (verify) {
          throw new NotImplementedException("Static verification is not yet implemented.");
        }

    // Write out the assembly, possibly injecting the runtime checks
        if (rewrite || passthrough) {
          // Reload the assembly to flush out the abnormalized contracts since the rewriter can't handle them yet.
          assemblyNode = LoadAssembly();

          if (!passthrough) {
            // Rewrite the assembly in memory.
            Rewriter rewriter = new Rewriter(new ContractNodes(assemblyNode));
            rewriter.InlinePreconditions = false;
            rewriter.InlinePostconditions = false;
            rewriter.InlineInvariant = false;
            rewriter.Verbose = verbose;
            rewriter.Decompile = decompile;
            rewriter.Visit(assemblyNode);
          }

          if (output == "same") {
            // Save the rewritten assembly to a temporary location.
            output = Path.Combine(Path.GetTempPath(), Path.GetFileName(assembly));

            // Re-attach external file resources to the new output assembly.
            MoveModuleResources(assemblyNode, output);

            // Save the rewritten assembly.
            assemblyNode.WriteModule(output, debug);

            // Make a copy of the original assembly and PDB.
            File.Delete(assembly + ".original");
            File.Delete(Path.ChangeExtension(assembly, ".pdb") + ".original");
            File.Move(assembly, assembly + ".original");
            File.Move(Path.ChangeExtension(assembly, ".pdb"), Path.ChangeExtension(assembly, ".pdb") + ".original");

            // Move the rewritten assembly and PDB to the original location.
            File.Move(output, assembly);
            File.Move(Path.ChangeExtension(output, ".pdb"), Path.ChangeExtension(assembly, ".pdb"));
          } else {
            // Re-attach external file resources to the new output assembly.
            MoveModuleResources(assemblyNode, output);

            // Save the rewritten assembly.
            assemblyNode.WriteModule(output, debug);
          }
        }
      }
#endif

        private static void LogFileLoads(AssemblyNode assemblyNode)
        {
            if (options.verbose > 0 && assemblyNode != null)
            {
                Console.WriteLine("Trace: Assembly '{0}' loaded from '{1}'", assemblyNode.Name, assemblyNode.Location);
            }
        }

        private static void PossiblyLoadOOB(AssemblyResolver resolver, AssemblyNode assemblyNode, string originalAssemblyName)
        {
            Contract.Requires(assemblyNode != null);
            Contract.Requires(originalAssemblyName != null);

            // Do NOT automatically attach an out-of-band contract to the main assembly being worked on,
            // but only for the assemblies it references.
            // That is because there might be a contract assembly X.Contracts for an assembly X that contains
            // contracts and we don't want to be confused or get duplicate contracts if that contract assembly
            // is found the next time X is rewritten.
            // So attach the contract only if it is explicitly listed in the options
            if (assemblyNode.Name.ToLower() == originalAssemblyName.ToLower())
            {
                assemblyNode.AssemblyReferenceResolution +=
                    new Module.AssemblyReferenceResolver(resolver.ResolveAssemblyReference);

                LogFileLoads(assemblyNode);
                return;
            }

            resolver.PostLoadHook(assemblyNode);
        }

        private static void PostLoadExtractionHook(AssemblyResolver resolver, AssemblyNode assemblyNode)
        {
            Contract.Requires(assemblyNode != null);

            LogFileLoads(assemblyNode);

            // If ends in ".Contracts", no need to do anything. Just return
            if (assemblyNode.Name.EndsWith(".Contracts")) return;

            // Possibly load OOB contract assembly for assemblyNode. If found, run extractor on the pair.
            if (options.automaticallyLookForOOBs)
            {
                string contractFileName = Path.GetFileNameWithoutExtension(assemblyNode.Location) + ".Contracts";

                // if (contractFileName != null)
                {
                    AssemblyNode contractAssembly = resolver.ProbeForAssembly(contractFileName, assemblyNode.Directory, resolver.DllExt);
                    if (contractAssembly != null)
                    {
                        ContractNodes usedContractNodes;
                        Extractor.ExtractContracts(assemblyNode, contractAssembly, DefaultContractLibrary,
                            BackupContractLibrary, DefaultContractLibrary, out usedContractNodes, null, false);
                    }
                }
            }
            else
            {
                // use only if specified

                if (options.contracts != null)
                {
                    foreach (var contractAssemblyName in options.contracts)
                    {
                        string contractFileName = Path.GetFileNameWithoutExtension(contractAssemblyName);
                        var assemblyName = assemblyNode.Name + ".Contracts";

                        if (contractFileName == assemblyName)
                        {
                            AssemblyNode contractAssembly = resolver.ProbeForAssembly(assemblyName, assemblyNode.Directory, resolver.DllExt);
                            if (contractAssembly != null)
                            {
                                ContractNodes usedContractNodes;
                                Extractor.ExtractContracts(assemblyNode, contractAssembly, DefaultContractLibrary,
                                    BackupContractLibrary, DefaultContractLibrary, out usedContractNodes, null, false);
                            }

                            break; // only do the one
                        }
                    }
                }
            }
        }

        private static int PEVerify(string assemblyFile)
        {
            var path = Path.GetDirectoryName(assemblyFile);
            var file = Path.GetFileName(assemblyFile);
            if (file == "mscorlib.dll") return -1; // peverify returns 0 for mscorlib without verifying.
            var oldCWD = Environment.CurrentDirectory;
            if (string.IsNullOrEmpty(path)) path = oldCWD;

            try
            {
                Environment.CurrentDirectory = path;
                object winsdkfolder =
                    Registry.GetValue(
                        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A\WinSDK-NetFx40Tools",
                        "InstallationFolder", null);
                if (winsdkfolder == null)
                {
                    winsdkfolder = Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SDKs\Windows",
                        "CurrentInstallFolder", null);
                }

                string peVerifyPath = null;
                if (winsdkfolder != null)
                {
                    peVerifyPath = (string) winsdkfolder + @"\peverify.exe";
                    if (!File.Exists(peVerifyPath))
                    {
                        peVerifyPath = (string) winsdkfolder + @"\bin\peverify.exe";
                    }

                    if (!File.Exists(peVerifyPath))
                    {
                        peVerifyPath = null;
                    }
                }

                if (peVerifyPath == null)
                {
                    peVerifyPath =
                        Environment.ExpandEnvironmentVariables(
                            @"%ProgramFiles%\Microsoft Visual Studio 8\Common7\IDE\PEVerify.exe");
                }

                if (String.IsNullOrEmpty(peVerifyPath) || !File.Exists(peVerifyPath))
                {
                    return -2;
                }

                ProcessStartInfo i = new ProcessStartInfo(peVerifyPath, "/unique \"" + file + "\"");

                i.RedirectStandardOutput = true;
                i.UseShellExecute = false;
                i.ErrorDialog = false;
                i.CreateNoWindow = true;

                using (Process p = Process.Start(i))
                {
                    if (!(p.WaitForExit(10000))) return -1;
#if false
          if (p.ExitCode != 0)
          {
            var s = p.StandardOutput.ReadToEnd();
            Console.WriteLine("{0}", s);
          }
#endif
                    return p.ExitCode;
                }
            }
            catch
            {
                return -1;
            }
            finally
            {
                Environment.CurrentDirectory = oldCWD;
            }
        }

        private static void MikesArchitecture(AssemblyResolver resolver, AssemblyNode assemblyNode,
            ContractNodes contractNodes, ContractNodes backupContracts)
        {
#if false
      var originalsourceDir = Path.GetDirectoryName(assemblyNode.Location);
      int oldPeVerifyCode = options.verify ? PEVerify(assemblyNode.Location, originalsourceDir) : -1;
#endif

            // Check to see if the assembly has already been rewritten

            if (!options.passthrough)
            {
                if (ContractNodes.IsAlreadyRewritten(assemblyNode))
                {
                    if (!options.allowRewritten)
                    {
                        Console.WriteLine("Assembly '" + assemblyNode.Name +
                                          "' has already been rewritten. I, your poor humble servant, cannot rewrite it. Instead I must give up without rewriting it. Help!");
                    }

                    return;
                }
            }

            // Extract the contracts from the code (includes checking the contracts)

            if (!options.passthrough)
            {
                string contractFileName = Path.GetFileNameWithoutExtension(assemblyNode.Location) + ".Contracts";

                if (options.contracts == null || options.contracts.Count <= 0) contractFileName = null;

                if (options.contracts != null &&
                    !options.contracts.Exists(
                        name => name.Equals(assemblyNode.Name + ".Contracts.dll", StringComparison.OrdinalIgnoreCase)))
                    contractFileName = null;

                AssemblyNode contractAssembly = null;
                if (contractFileName != null)
                {
                    contractAssembly = resolver.ProbeForAssembly(contractFileName, assemblyNode.Directory,
                        resolver.DllExt);
                }

                ContractNodes usedContractNodes;

                Extractor.ExtractContracts(assemblyNode, contractAssembly, contractNodes, backupContracts, contractNodes,
                    out usedContractNodes, options.EmitError, false);

                // important to extract source before we perform any more traversals due to contract instantiation. Otherwise,
                // we might get copies of contracts due to instantiation that have no source text yet.

                // Extract the text from the sources (optional)

                if (usedContractNodes != null && options.extractSourceText)
                {
                    GenerateDocumentationFromPDB gd = new GenerateDocumentationFromPDB(contractNodes);
                    gd.VisitForDoc(assemblyNode);
                }

                // After all contracts have been extracted in assembly, do some post-extractor checks

                // we run these even if no contracts were extracted due to checks having to do with overrides
                var contractNodesForChecks = usedContractNodes != null ? usedContractNodes : contractNodes;
                if (contractNodesForChecks != null)
                {
                    PostExtractorChecker pec = new PostExtractorChecker(contractNodesForChecks, options.EmitError, false,
                        options.fSharp, options.IsLegacyModeAssembly, options.addInterfaceWrappersWhenNeeded,
                        options.level);

                    if (contractAssembly != null)
                    {
                        pec.VisitForPostCheck(contractAssembly);
                    }
                    else
                    {
                        pec.VisitForPostCheck(assemblyNode);
                    }
                }

                // don't really need to test, since if they are the same, the assignment doesn't change that
                // but this is to emphasize that the nodes used in the AST by the extractor are different
                // than what we thought we were using.

                if (options.GetErrorCount() > 0)
                {
                    // we are done.
                    // But first, report any metadata errors so they are not masked by the errors
                    CheckForMetaDataErrors(assemblyNode);
                    return;
                }
            }

            // If we have metadata errors, cop out

            {
#if false
          for (int i = 0; i < assemblyNode.ModuleReferences.Count; i++)
          {
            Module m = assemblyNode.ModuleReferences[i].Module;
            Console.WriteLine("Location for referenced module '{0}' is '{1}'", m.Name, m.Location);
          }
#endif
                if (CheckForMetaDataErrors(assemblyNode))
                {
                    throw new Exception("Rewrite aborted due to metadata errors. Check output window");
                }
                for (int i = 0; i < assemblyNode.AssemblyReferences.Count; i++)
                {
                    AssemblyNode aref = assemblyNode.AssemblyReferences[i].Assembly;
                    if (CheckForMetaDataErrors(aref))
                    {
                        throw new Exception("Rewrite aborted due to metadata errors. Check output window");
                    }
                }
            }

            // Inject the contracts into the code (optional)

            if (options.rewrite && !options.passthrough)
            {
                // Rewrite the assembly in memory.
                ContractNodes cnForRuntime = null;

                // make sure to use the correct contract nodes for runtime code generation. We may have Contractnodes pointing to microsoft.Contracts even though
                // the code relies on mscorlib to provide the contracts. So make sure the code references the contract nodes first.
                if (contractNodes != null && contractNodes.ContractClass != null &&
                    contractNodes.ContractClass.DeclaringModule != SystemTypes.SystemAssembly)
                {
                    string assemblyNameContainingContracts = contractNodes.ContractClass.DeclaringModule.Name;
                    for (int i = 0; i < assemblyNode.AssemblyReferences.Count; i++)
                    {
                        if (assemblyNode.AssemblyReferences[i].Name == assemblyNameContainingContracts)
                        {
                            cnForRuntime = contractNodes;
                            break; // runtime actually references the contract library
                        }
                    }
                }

                if (cnForRuntime == null)
                {
                    // try to grab the system assembly contracts
                    cnForRuntime = ContractNodes.GetContractNodes(SystemTypes.SystemAssembly, null);
                }

                if (cnForRuntime == null)
                {
                    // Can happen if the assembly does not use contracts directly, but inherits them from some other place
                    // Use the normal contractNodes in this case (actually we should generate whatever we grab from ContractNodes)
                    cnForRuntime = contractNodes;
                }

                RuntimeContractMethods runtimeContracts =
                    new RuntimeContractMethods(userSpecifiedContractType,
                        cnForRuntime, assemblyNode, options.throwOnFailure, options.level, options.publicSurfaceOnly,
                        options.callSiteRequires,
                        options.recursionGuard, options.hideFromDebugger, options.IsLegacyModeAssembly);

                Rewriter rewriter = new Rewriter(assemblyNode, runtimeContracts, options.EmitError,
                    options.inheritInvariants, options.skipQuantifiers);

                rewriter.Verbose = 0 < options.verbose;
                rewriter.Visit(assemblyNode);
            }

            //Console.WriteLine(">>>Finished Rewriting<<<");

            // Set metadata version for target the same as for the source

            TargetPlatform.TargetRuntimeVersion = assemblyNode.TargetRuntimeVersion;

            // Write out the assembly (optional)

            if (options.rewrite || options.passthrough)
            {
                bool updateInPlace = options.output == "same";

                string pdbFile = Path.ChangeExtension(options.assembly, ".pdb");
                bool pdbExists = File.Exists(pdbFile);

                string backupAssembly = options.assembly + ".original";
                string backupPDB = pdbFile + ".original";


                if (updateInPlace)
                {
                    // Write the rewritten assembly in a temporary location.
                    options.output = options.assembly;

                    MoveAssemblyFileAndPDB(options.output, pdbFile, pdbExists, backupAssembly, backupPDB);
                }

                // Write the assembly.
                // Don't pass the debugInfo flag to WriteModule unless the PDB file exists.
                assemblyNode.WriteModule(options.output, options.debug && pdbExists && options.writePDBFile);

                string outputDir = updateInPlace
                    ? Path.GetDirectoryName(options.assembly)
                    : Path.GetDirectoryName(options.output);

                // Re-attach external file resources to the new output assembly.
                MoveModuleResources(assemblyNode, outputDir);

#if false
        if (oldPeVerifyCode == 0)
        {
          var newPeVerifyCode = PEVerify(assemblyNode.Location, originalsourceDir);
          if (newPeVerifyCode > 0)
          {
            if (updateInPlace)
            {
              // move original back in place
              MoveAssemblyFileAndPDB(backupAssembly, backupPDB, pdbExists, options.output, pdbFile);
            }
            throw new Exception("Rewrite failed to produce verifiable assembly");
          }
          else if (newPeVerifyCode == 0)
          {
            Console.WriteLine("rewriter output verified");
          }
        }
#endif
                if (updateInPlace)
                {
                    if (!options.keepOriginalFiles)
                    {
                        try
                        {
                            File.Delete(backupAssembly);
                        }
                        catch
                        {
                            // there are situations where the exe is still in use
                        }
                        if (options.debug && pdbExists && options.writePDBFile)
                        {
                            try
                            {
                                File.Delete(backupPDB);
                            }
                            catch
                            {
                                // I know this is stupid, but somehow on some machines we get an AccessError trying to delete the pdb.
                                // so we leave it in place.
                            }
                        }
                    }
                }
            }
        }

        private static void MoveAssemblyFileAndPDB(string sourceAssembly, string sourcePDB, bool pdbExists,
            string targetAssembly, string targetPDB)
        {
            // delete targets
            try
            {
                Contract.Assume(!String.IsNullOrEmpty(sourceAssembly));
                Contract.Assume(!String.IsNullOrEmpty(targetAssembly));

                File.Delete(targetAssembly);

                if (options.debug && pdbExists && options.writePDBFile)
                {
                    File.Delete(targetPDB);
                }
            }
            catch
            {
            }

            // move things to target
            try
            {
                File.Move(sourceAssembly, targetAssembly);
                if (options.debug && pdbExists && options.writePDBFile)
                {
                    Contract.Assume(!String.IsNullOrEmpty(sourcePDB));
                    File.Move(sourcePDB, targetPDB);
                }
            }
            catch
            {
            }
        }

        private static bool CheckForMetaDataErrors(AssemblyNode aref)
        {
            Contract.Requires(aref != null);

            bool result = false;
            if (aref.MetadataImportErrors != null && aref.MetadataImportErrors.Count > 0)
            {
                Console.WriteLine("Reading assembly '{0}' from '{1}' resulted in errors.", aref.Name, aref.Location);
                foreach (Exception ex in aref.MetadataImportErrors)
                {
                    Console.WriteLine("\t" + ex.Message);
                    result = true;
                }
            }

            if (options.ignoreMetadataErrors)
            {
                return false;
            }

            return result;
        }
    }
}
