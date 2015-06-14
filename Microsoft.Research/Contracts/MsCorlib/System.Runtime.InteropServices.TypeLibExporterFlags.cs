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
using System.Diagnostics.Contracts;

namespace System.Runtime.InteropServices {
  // Summary:
  //     Indicates how a type library should be produced.
  [Serializable]
  [Flags]
  [ComVisible(true)]
  public enum TypeLibExporterFlags {
    // Summary:
    //     Specifies no flags. This is the default.
    None = 0,
    //
    // Summary:
    //     Exports references to types that were imported from COM as IUnknown if the
    //     type does not have a registered type library. Set this flag when you want
    //     the type library exporter to look for dependent types in the registry rather
    //     than in the same directory as the input assembly.
    OnlyReferenceRegistered = 1,
    //
    // Summary:
    //     Allows the caller to explicitly resolve type library references without consulting
    //     the registry.
    CallerResolvedReferences = 2,
    //
    // Summary:
    //     When exporting type libraries, the .NET Framework resolves type name conflicts
    //     by decorating the type with the name of the namespace; for example, System.Windows.Forms.HorizontalAlignment
    //     is exported as System_Windows_Forms_HorizontalAlignment. When there is a
    //     conflict with the name of a type that is not visible from COM, the .NET Framework
    //     exports the undecorated name. Set the System.Runtime.InteropServices.TypeLibExporterFlags.OldNames
    //     flag or use the /oldnames option in the Type Library Exporter (Tlbexp.exe)
    //     to force the .NET Framework to export the decorated name. Note that exporting
    //     the decorated name was the default behavior in versions prior to the .NET
    //     Framework version 2.0.
    OldNames = 4,
    //
    // Summary:
    //     When compiling on a 64-bit computer, specifies that the Type Library Exporter
    //     (Tlbexp.exe) generates a 32-bit type library. All data types are transformed
    //     appropriately.
    ExportAs32Bit = 16,
    //
    // Summary:
    //     When compiling on a 32-bit computer, specifies that the Type Library Exporter
    //     (Tlbexp.exe) generates a 64-bit type library. All data types are transformed
    //     appropriately.
    ExportAs64Bit = 32,
  }
}
