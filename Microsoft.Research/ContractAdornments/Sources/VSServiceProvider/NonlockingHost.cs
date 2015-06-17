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
using System.Diagnostics.Contracts;
using System.IO;
using Microsoft.Cci;
using Microsoft.Cci.MutableContracts;

namespace ContractAdornments {
  /// <summary>
  /// A custom host enviornment that makes sure all openned resources can be disposed. This is needed so that this host doesn't interfere with VS's build process.
  /// </summary>
  public class NonlockingHost : CodeContractAwareHostEnvironment {
    readonly Dictionary<string, AssemblyIdentity> _locationsToAssemblyReferences;
    private string[] _originalLibpaths;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_locationsToAssemblyReferences != null);
      Contract.Invariant(_originalLibpaths != null);
    }


    public NonlockingHost(params string[] libpaths) : base(libpaths, false, false) 
    {
      Contract.Requires(libpaths != null);

      _originalLibpaths = libpaths;
      _locationsToAssemblyReferences = new Dictionary<string, AssemblyIdentity>();
    }

    public void ConfigurationChanged()
    {
      // clear the libpaths and reset
      this.LibPaths.Clear();
      this.LibPaths.AddRange(_originalLibpaths);
      
    }

    public override IBinaryDocumentMemoryBlock OpenBinaryDocument(IBinaryDocument sourceDocument) {
      VSServiceProvider.Current.Logger.WriteToLog("Opening document: " + sourceDocument.Name + " from: " + sourceDocument.Location);
      try {
        IBinaryDocumentMemoryBlock binDocMemoryBlock = UnmanagedBinaryMemoryBlock.CreateUnmanagedBinaryMemoryBlock(sourceDocument.Location, sourceDocument);
        return binDocMemoryBlock;
      } catch (IOException) {
        VSServiceProvider.Current.Logger.WriteToLog("Failed to open document.");
        return null;
      }
    }
    public override IUnit LoadUnitFrom(string location) {
      Contract.Requires(!String.IsNullOrEmpty(location));

      var unit = base.LoadUnitFrom(location);
      var assemblyIdentity = unit.UnitIdentity as AssemblyIdentity;
      if(assemblyIdentity != null)
        _locationsToAssemblyReferences[location] = assemblyIdentity;
      return unit;
    }

    public AssemblyIdentity LoadIfNotLoaded(string location) {
      Contract.Requires(!String.IsNullOrEmpty(location));

      AssemblyIdentity result;
      if (_locationsToAssemblyReferences.TryGetValue(location, out result)) {
        return result;
      } else {
        return LoadUnitFrom(location, true).UnitIdentity as AssemblyIdentity;
      }
    }
    public AssemblyIdentity ReloadIfLoaded(AssemblyIdentity assemblyIdentity) {
      Contract.Requires(assemblyIdentity != null);
      Contract.Requires(!String.IsNullOrEmpty(assemblyIdentity.Location));

      return ReloadIfLoaded(assemblyIdentity.Location);
    }
    public AssemblyIdentity ReloadIfLoaded(string location) {
      Contract.Requires(!String.IsNullOrEmpty(location));

#if false
      if (!_locationsToAssemblyReferences.ContainsKey(location))
        return null;
#endif
      var unit = LoadUnitFrom(location, true);
      RegisterAsLatest(unit);

      return unit.UnitIdentity as AssemblyIdentity;
    }

    public override void AddLibPath(string path) {
      if (String.IsNullOrEmpty(path)) return;

      if(!LibPaths.Contains(path))
        base.AddLibPath(path);
    }
  }
}
