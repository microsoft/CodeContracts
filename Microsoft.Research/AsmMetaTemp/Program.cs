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

//-----------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All Rights Reserved.
//
//-----------------------------------------------------------------------------
using System;
using System.IO;
using Microsoft.Cci;
using Microsoft.Cci.MutableCodeModel;

namespace PeToPe {
  class Program {
    static void Main(string[] args) {
      if (args == null || args.Length != 2) {
        Console.WriteLine("usage: asmmeta <input> <output>");
        return;
      }
      HostEnvironment host = new HostEnvironment();
      IModule/*?*/ module = host.LoadUnitFrom(args[0]) as IModule;
      if (module == null || module == Dummy.Module || module == Dummy.Assembly) {
        Console.WriteLine(args[0]+" is not a PE file containing a CLR module or assembly.");
        return;
      }

      string outputFile = args[1];
      string outputPDBFile = Path.ChangeExtension(args[1], "pdb");

      PdbReader/*?*/ pdbReader = null;
      PdbWriter/*?*/ pdbWriter = null;
      string pdbFile = Path.ChangeExtension(module.Location, "pdb");
      if (File.Exists(pdbFile)) {
        Stream pdbStream = File.OpenRead(pdbFile);
        pdbReader = new PdbReader(pdbStream, host);
        pdbWriter = new PdbWriter(Path.GetFullPath(outputPDBFile), pdbReader);
      }

      MetadataMutator mutator = new MetadataMutator(host);
      IAssembly/*?*/ assembly = module as IAssembly;
      if (assembly != null)
      {
        var mutable = mutator.GetMutableCopy(assembly);
        mutable.Name = host.NameTable.GetNameFor(Path.GetFileNameWithoutExtension(args[1]));
        module = mutator.Visit(mutable);
      }
      else
      {
        var mutable = mutator.GetMutableCopy(module);
        mutable.Name = host.NameTable.GetNameFor(Path.GetFileNameWithoutExtension(args[1]));
        module = mutator.Visit(mutable);
      }
      PeWriter.WritePeToStream(module, host, File.Create(Path.GetFullPath(outputFile)), pdbReader, pdbReader, pdbWriter);
    }
  }

  internal class HostEnvironment : MetadataReaderHost {
    PeReader peReader;
    internal HostEnvironment()
      : base(new NameTable(), 4) {
      this.peReader = new PeReader(this);
    }

    public override IUnit LoadUnitFrom(string location) {
      IUnit result = this.peReader.OpenModule(BinaryDocument.GetBinaryDocumentForFile(location, this));
      this.RegisterAsLatest(result);
      return result;
    }

  }

}

