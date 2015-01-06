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
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Reflection {
  // Summary:
  //     Specifies flags for method attributes. These flags are defined in the corhdr.h
  //     file.
  [Serializable]
  [Flags]
  [ComVisible(true)]
  public enum MethodAttributes {
    // Summary:
    //     Indicates that the member cannot be referenced.
    PrivateScope = 0,
    //
    // Summary:
    //     Indicates that the method will reuse an existing slot in the vtable. This
    //     is the default behavior.
    ReuseSlot = 0,
    //
    // Summary:
    //     Indicates that the method is accessible only to the current class.
    Private = 1,
    //
    // Summary:
    //     Indicates that the method is accessible to members of this type and its derived
    //     types that are in this assembly only.
    FamANDAssem = 2,
    //
    // Summary:
    //     Indicates that the method is accessible to any class of this assembly.
    Assembly = 3,
    //
    // Summary:
    //     Indicates that the method is accessible only to members of this class and
    //     its derived classes.
    Family = 4,
    //
    // Summary:
    //     Indicates that the method is accessible to derived classes anywhere, as well
    //     as to any class in the assembly.
    FamORAssem = 5,
    //
    // Summary:
    //     Indicates that the method is accessible to any object for which this object
    //     is in scope.
    Public = 6,
    //
    // Summary:
    //     Retrieves accessibility information.
    MemberAccessMask = 7,
    //
    // Summary:
    //     Indicates that the managed method is exported by thunk to unmanaged code.
    UnmanagedExport = 8,
    //
    // Summary:
    //     Indicates that the method is defined on the type; otherwise, it is defined
    //     per instance.
    Static = 16,
    //
    // Summary:
    //     Indicates that the method cannot be overridden.
    Final = 32,
    //
    // Summary:
    //     Indicates that the method is virtual.
    Virtual = 64,
    //
    // Summary:
    //     Indicates that the method hides by name and signature; otherwise, by name
    //     only.
    HideBySig = 128,
    //
    // Summary:
    //     Retrieves vtable attributes.
    VtableLayoutMask = 256,
    //
    // Summary:
    //     Indicates that the method always gets a new slot in the vtable.
    NewSlot = 256,
    //
    // Summary:
    //     Indicates that the method can only be overridden when it is also accessible.
    CheckAccessOnOverride = 512,
    //
    // Summary:
    //     Indicates that the class does not provide an implementation of this method.
    Abstract = 1024,
    //
    // Summary:
    //     Indicates that the method is special. The name describes how this method
    //     is special.
    SpecialName = 2048,
    //
    // Summary:
    //     Indicates that the common language runtime checks the name encoding.
    RTSpecialName = 4096,
    //
    // Summary:
    //     Indicates that the method implementation is forwarded through PInvoke (Platform
    //     Invocation Services).
    PinvokeImpl = 8192,
    //
    // Summary:
    //     Indicates that the method has security associated with it. Reserved flag
    //     for runtime use only.
    HasSecurity = 16384,
    //
    // Summary:
    //     Indicates that the method calls another method containing security code.
    //     Reserved flag for runtime use only.
    RequireSecObject = 32768,
    //
    // Summary:
    //     Indicates a reserved flag for runtime use only.
    ReservedMask = 53248,
  }
}