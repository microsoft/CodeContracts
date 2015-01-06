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

//#define DEBUG_SLICE
#define TRACE_PERFORMANCE

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using Microsoft.Cci.MutableCodeModel;
using Microsoft.Research.CodeAnalysis;
using System.Linq;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Cci.Analysis {

  public class CCI2Slicer : IDisposable,
    ICodeWriter<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, IPropertyDefinition, IEventDefinition, TypeReferenceAdaptor, ICustomAttribute, IAssemblyReference>
  {

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.disposableObjectAllocatedByThisHost != null);
    }


    private readonly HostEnvironment host;
    protected readonly List<IDisposable> disposableObjectAllocatedByThisHost = new List<IDisposable>();
    readonly private INamespaceTypeReference systemString;
    readonly private INamespaceTypeReference systemInt;

    private CCI2Slicer(HostEnvironment host) {
      Contract.Requires(host != null);

      this.host = host;
      this.systemString = this.host.PlatformType.SystemString;
      this.systemInt = this.host.PlatformType.SystemInt32;
    }

    public static CCI2Slicer CreateSlicer(HostEnvironment host)
    {
      Contract.Requires(host != null);
      // MB: no singleton please
      return new CCI2Slicer(host);
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public bool WriteSliceToFile(ISlice<MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, IAssemblyReference> slice, string directory, out string dll)
    {
#if TRACE_PERFORMANCE
      var stopWatch = new Stopwatch();
      stopWatch.Start();
#endif
      
      var newAssembly = Prune.PruneAssembly(host, slice);

#if TRACE_PERFORMANCE
      Console.WriteLine("Time to prune the assembly: {0}", stopWatch.Elapsed);
#endif
      
      var errors = ValidateAssembly(host, newAssembly);
      if (/*errors != null && */ 0 < errors.Count)
      {
#if !DEBUG_SLICE
        dll = null;
        return false;
#endif
      }
      
      //Get a PDB reader if there is a PDB file.
      PdbReader/*?*/ pdbReader = null;
      string pdbFile = slice.ContainingAssembly.ResolvedAssembly.DebugInformationLocation;
      if (string.IsNullOrEmpty(pdbFile) || !File.Exists(pdbFile))
        pdbFile = Path.ChangeExtension(slice.ContainingAssembly.ResolvedAssembly.Location, "pdb");
      if (File.Exists(pdbFile)) {
        using (var pdbStream = File.OpenRead(pdbFile)) {
          pdbReader = new PdbReader(pdbStream, host);
        }
      }
      using (pdbReader) {

        ISourceLocationProvider sourceLocationProvider = pdbReader;
        ILocalScopeProvider localScopeProvider = pdbReader;

        Contract.Assume(sourceLocationProvider != null, "why??");
        if (!MakeSureSliceHasAtLeastMethodSourceLocation(slice, sourceLocationProvider)) {
          dll = null;
          return false;
        }

        dll = Path.Combine(directory, slice.Name + ".dll");

#if TRACE_PERFORMANCE
        stopWatch.Reset();
#endif

        using (var peStream = File.Create(dll)) {
          if (pdbReader == null) {
            PeWriter.WritePeToStream(newAssembly, host, peStream);
          } else {
            using (var pdbWriter = new PdbWriter(dll.Replace(".dll", ".pdb"), pdbReader, emitTokenSourceInfo: true)) {
              PeWriter.WritePeToStream(newAssembly, host, peStream, sourceLocationProvider, localScopeProvider, pdbWriter);
            }
          }
        }

#if TRACE_PERFORMANCE
        Console.WriteLine("Time spent to write on the disk: {0}", stopWatch.Elapsed);
#endif
      }

#if !DEBUG_SLICE
      if (errors != null && 0 < errors.Count)
      {
        using (var tw = new StreamWriter(File.Create(Path.Combine(directory, slice.Name + ".errors.txt"))))
        {
          // something is performed asynchronously and may not be terminated here, that is wrong!
          lock (errors)
          {
            foreach (var err in errors)
            {
              tw.WriteLine(err.Location);
              foreach (var e in err.Errors)
                tw.WriteLine("{0} {1} {2}", e.IsWarning ? "WARNING" : "ERROR ", e.Code, e.Message);
              tw.WriteLine();
            }
          }
        }

        return false;
      }
#endif

      // Can this be checked before writing it out?
      if (newAssembly.AssemblyReferences.Any(ar => ar.AssemblyIdentity.Equals(slice.ContainingAssembly.AssemblyIdentity))) {
      }

      return true;
    }

    private bool MakeSureSliceHasAtLeastMethodSourceLocation(ISlice<MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, IAssemblyReference> slice, ISourceLocationProvider sourceLocationProvider) {
      Contract.Requires(slice != null);
      Contract.Requires(sourceLocationProvider != null);

      foreach (var m in slice.Methods) {
        var methodDefinition = m.reference.ResolvedMethod;
        if (sourceLocationProvider.GetPrimarySourceLocationsFor(methodDefinition.Locations).Any())
          return true;
      }
      return false;
    }

    private static bool IsGetter(IMethodDefinition methodDefinition) {
      Contract.Requires(methodDefinition != null);

      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("get_");
    }

    private static bool IsSetter(IMethodDefinition methodDefinition) {
      Contract.Requires(methodDefinition != null);
      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("set_");
    }

    private static IPropertyDefinition GetPropertyFromAccessor(IMethodDefinition methodDefinition) {
      Contract.Requires(methodDefinition != null);
      if (!IsGetter(methodDefinition) && !IsSetter(methodDefinition)) return null;
      // TODO: Need to cache this information. This is expensive.
      foreach (var p in methodDefinition.ContainingTypeDefinition.Properties) {
        if (p.Setter != null && p.Setter.ResolvedMethod.InternedKey == methodDefinition.InternedKey) {
          return p;
        } else if (p.Getter != null && p.Getter.ResolvedMethod.InternedKey == methodDefinition.InternedKey) {
          return p;
        }
      }
      return null;
    }

    private static List<Microsoft.Cci.ErrorEventArgs> ValidateAssembly(HostEnvironment host, IAssembly assembly) {
      Contract.Requires(host != null);
      Contract.Ensures(Contract.Result<List<Microsoft.Cci.ErrorEventArgs>>() != null);

      List<Microsoft.Cci.ErrorEventArgs> errorEvents = new List<Microsoft.Cci.ErrorEventArgs>();
      host.Errors += (object sender, Microsoft.Cci.ErrorEventArgs e) => { lock (errorEvents) errorEvents.Add(e); }; // MB: without lock I'm getting exceptions
      var mv = new MetadataValidator(host);
      mv.Validate(assembly);
      return errorEvents;
    }

    #region IDisposable members

    private void Close()
    {
      foreach (var disposable in this.disposableObjectAllocatedByThisHost)
        disposable.Dispose();
    }

    public virtual void Dispose()
    {
      this.Close();
      GC.SuppressFinalize(this);
    }

    ~CCI2Slicer()
    {
      this.Close();
    }

    #endregion

  }

}