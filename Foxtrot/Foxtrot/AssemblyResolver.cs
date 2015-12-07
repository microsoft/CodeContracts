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
using System.Collections.Generic;
using System.Compiler;
using System.IO;
using System.Diagnostics;

namespace Microsoft.Contracts.Foxtrot
{
    public class AssemblyResolver
    {
        public string[] EmptyExt = new[] {""};
        public string[] DllExt = new[] {".dll", ".winmd"};
        public string[] DllAndExeExt = new[] {".dll", ".winmd", ".exe"};
        public string[] EmptyAndDllExt = new[] {"", ".dll", ".winmd"};
        public string[] AllExt = new[] {"", ".dll", ".winmd", ".exe"};

        private IEnumerable<string> resolvedPaths;
        private IEnumerable<string> libpaths;
        
        private bool trace;
        private bool usePDB;
        private bool preserveShortBranches;
        private Action<AssemblyResolver, AssemblyNode> postLoad;

        public AssemblyResolver(IEnumerable<string> resolvedPaths, IEnumerable<string> libpaths, bool usePDB,
            bool preserveShortBranches, bool trace, Action<AssemblyResolver, AssemblyNode> postLoad)
        {
            this.resolvedPaths = resolvedPaths;
            this.libpaths = libpaths;
            this.trace = trace;
            this.usePDB = usePDB;
            this.preserveShortBranches = preserveShortBranches;
            this.postLoad = postLoad;
        }

        /// <summary>
        /// Resolves assembly references based on the library paths specified.
        /// Tries to resolve to ".dll" or ".exe". First found wins.
        /// </summary>
        /// <param name="assemblyReference">Reference to resolve.</param>
        /// <param name="referencingModule">Referencing module.</param>
        /// <returns>The resolved assembly node (null if not found).</returns>
        public AssemblyNode ResolveAssemblyReference(AssemblyReference assemblyReference, Module referencingModule)
        {
            AssemblyNode a = ProbeForAssembly(assemblyReference.Name, referencingModule.Directory, DllAndExeExt);

            return a;
        }

        public AssemblyNode ProbeForAssembly(string assemblyName, string referencingModuleDirectory, string[] exts)
        {
            AssemblyNode a = null;
            try
            {
                if (trace)
                {
                    LoadTracing(string.Format("Attempting to load: {0}", assemblyName));
                }

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
                if (this.resolvedPaths != null)
                {
                    foreach (string candidate in this.resolvedPaths)
                    {
                        var candidateAssemblyName = Path.GetFileNameWithoutExtension(candidate);
                        if (String.Compare(candidateAssemblyName, assemblyName, StringComparison.OrdinalIgnoreCase) != 0)
                            continue;

                        if (File.Exists(candidate))
                        {
                            a = ProbeForAssemblyWithExtension("", candidate, EmptyExt);
                            break;
                        }
                    }
                }

                if (a == null)
                {
                    if (this.resolvedPaths != null)
                    {
                    }
                }
                else
                {
                    goto End;
                }

                // Check user-supplied search directories

                LoadTracing("AssemblyResolver: Attempting user-supplied directories.");
                if (this.libpaths != null)
                {
                    foreach (string dir in this.libpaths)
                    {
                        a = ProbeForAssemblyWithExtension(dir, assemblyName, exts);
                        if (a != null)
                            break;
                    }
                }

                if (a == null)
                {
                    if (this.libpaths != null)
                    {
                    }
                }
                else
                {
                    goto End;
                }

                // Check referencing module's directory

                if (referencingModuleDirectory != null)
                {
                    a = ProbeForAssemblyWithExtension(referencingModuleDirectory, assemblyName, exts);
                }
                
                if (a == null)
                {
                    if (referencingModuleDirectory != null)
                    {
                    }
                }
                else
                {
                    goto End;
                }

                // Check input directory

                a = ProbeForAssemblyWithExtension("", assemblyName, exts);
                if (a == null)
                {
                }
                else
                {
                    goto End;
                }

            End:
                if (a == null)
                {
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("AssemblyResolver: Exception occurred. Unable to resolve reference.");
                Trace.WriteLine("Inner exception: " + e);
            }

            return a;
        }

        private AssemblyNode ProbeForAssemblyWithExtension(string directory, string assemblyName, string[] exts)
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
            } while (!string.IsNullOrWhiteSpace(directory));
            
            return null;
        }

        private AssemblyNode ProbeForAssemblyWithExtensionAtomic(string directory, string assemblyName, string[] exts)
        {
            foreach (string ext in exts)
            {
                bool tempDebugInfo = this.usePDB;
                string fullName = Path.Combine(directory, assemblyName + ext);
                
                if (this.trace)
                {
                    LoadTracing(String.Format("Attempting load from {0}", fullName));
                }

                if (File.Exists(fullName))
                {
                    if (tempDebugInfo)
                    {
                        // Don't pass the debugInfo flag to GetAssembly unless the PDB file exists.
                        string pdbFullName;
                        if (String.IsNullOrEmpty(ext))
                        {
                            pdbFullName = Path.ChangeExtension(fullName, ".pdb");
                        }
                        else
                        {
                            pdbFullName = Path.Combine(directory, assemblyName + ".pdb");
                        }

                        if (!File.Exists(pdbFullName))
                        {
                            tempDebugInfo = false;
                        }
                    }

                    if (this.trace)
                    {
                        LoadTracing(string.Format("Resolved assembly reference '{0}' to '{1}'. (Using directory {2})",
                            assemblyName, fullName, directory));
                    }

                    var result = AssemblyNode.GetAssembly(
                        fullName, // path to assembly
                        TargetPlatform.StaticAssemblyCache, // global cache to use for assemblies
                        true, // doNotLockFile
                        tempDebugInfo, // getDebugInfo
                        true, // useGlobalCache
                        this.preserveShortBranches, // preserveShortBranches
                        this.PostLoadHook
                        );

                    return result;
                }
            }

            return null;
        }

        public void PostLoadHook(AssemblyNode assemblyNode)
        {
            assemblyNode.AssemblyReferenceResolution += this.ResolveAssemblyReference;

            if (!SystemTypes.IsInitialized) return;

            if (this.postLoad != null) this.postLoad(this, assemblyNode);
        }

        private void LoadTracing(string msg)
        {
            if (this.trace)
            {
                Console.WriteLine("Trace: {0}", msg);
            }
        }

        public static string GetTargetPlatform(string platformOption, IEnumerable<string> resolvedPaths, IEnumerable<string> libPaths)
        {
            var stdlib = platformOption;
            if (stdlib == null)
            {
                stdlib = FindMscorlibCandidate(resolvedPaths);
            }

            if (stdlib == null)
            {
                stdlib = FindMscorlib(libPaths);
            }

            return stdlib;
        }

        private static string FindMscorlib(IEnumerable<string> p)
        {
            foreach (var path in p)
            {
                if (string.IsNullOrEmpty(path)) continue;

                var corlib = Path.Combine(path, "mscorlib.dll");

                if (File.Exists(corlib))
                {
                    return corlib;
                }
            }

            return null;
        }

        private static string FindMscorlibCandidate(IEnumerable<string> p)
        {
            foreach (var path in p)
            {
                try
                {
                    if (string.IsNullOrEmpty(path.Trim())) continue;

                    var candidate = Path.GetFullPath(path);
                    
                    if (candidate.EndsWith(@"\mscorlib.dll") && File.Exists(candidate))
                    {
                        return candidate;
                    }
                }
                catch
                {
                    // ignore errors from GetFullPath
                }
            }

            return null;
        }
    }
}
