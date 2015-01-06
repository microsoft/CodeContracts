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

namespace System.Runtime.InteropServices
{
  // Summary:
  //     Indicates how an assembly should be produced.
  public enum TypeLibImporterFlags
  {
    // Summary:
    //     Specifies no flags. This is the default.
    None = 0,
    //
    // Summary:
    //     Generates a primary interop assembly. See System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute
    //     for details. A keyfile must be specified.
    PrimaryInteropAssembly = 1,
    //
    // Summary:
    //     Imports all interfaces as interfaces that suppress the common language runtime's
    //     stack crawl for System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode
    //     permission. Be sure you understand the responsibilities associated with suppressing
    //     this security check.
    UnsafeInterfaces = 2,
    //
    // Summary:
    //     Imports all SAFEARRAYs as System.Array rather than a typed, single dimensional,
    //     zero-based managed array. This option is useful when dealing with multi dimensional,
    //     non zero-based SAFEARRAYs which otherwise can not be accessed unless you
    //     edit the resulting assembly using the ILDASM and ILASM tools.
    SafeArrayAsSystemArray = 4,
    //
    // Summary:
    //     Transforms [out, retval] parameters of methods on dispatch-only interfaces
    //     (dispinterfaces) into return values.
    TransformDispRetVals = 8,
    //
    // Summary:
    //     Not used.
    PreventClassMembers = 16,
    //
    // Summary:
    //     Specifies the use of serailizable classes.
    SerializableValueClasses = 32,
    //
    // Summary:
    //     Imports a type library for the X86 platform.
    ImportAsX86 = 256,
    //
    // Summary:
    //     Imports a type library for the X86 64 bit platform.
    ImportAsX64 = 512,
    //
    // Summary:
    //     Imports a type library for the Itanuim platform.
    ImportAsItanium = 1024,
    //
    // Summary:
    //     Imports a type library for any platform.
    ImportAsAgnostic = 2048,
    //
    // Summary:
    //     Specifies the use of reflection only loading.
    ReflectionOnlyLoading = 4096,
  }
}
