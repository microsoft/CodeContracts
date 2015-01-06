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
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Reflection {
  // Summary:
  //     Specifies type attributes.
  [Serializable]
  [Flags]
  [ComVisible(true)]
  public enum TypeAttributes {
    // Summary:
    //     LPTSTR is interpreted as ANSI.
    AnsiClass = 0,
    //
    // Summary:
    //     Specifies that the type is a class.
    Class = 0,
    //
    // Summary:
    //     Specifies that class fields are automatically laid out by the common language
    //     runtime.
    AutoLayout = 0,
    //
    // Summary:
    //     Specifies that the class is not public.
    NotPublic = 0,
    //
    // Summary:
    //     Specifies that the class is public.
    Public = 1,
    //
    // Summary:
    //     Specifies that the class is nested with public visibility.
    NestedPublic = 2,
    //
    // Summary:
    //     Specifies that the class is nested with private visibility.
    NestedPrivate = 3,
    //
    // Summary:
    //     Specifies that the class is nested with family visibility, and is thus accessible
    //     only by methods within its own type and any subtypes.
    NestedFamily = 4,
    //
    // Summary:
    //     Specifies that the class is nested with assembly visibility, and is thus
    //     accessible only by methods within its assembly.
    NestedAssembly = 5,
    //
    // Summary:
    //     Specifies that the class is nested with assembly and family visibility, and
    //     is thus accessible only by methods lying in the intersection of its family
    //     and assembly.
    NestedFamANDAssem = 6,
    //
    // Summary:
    //     Specifies type visibility information.
    VisibilityMask = 7,
    //
    // Summary:
    //     Specifies that the class is nested with family or assembly visibility, and
    //     is thus accessible only by methods lying in the union of its family and assembly.
    NestedFamORAssem = 7,
    //
    // Summary:
    //     Specifies that class fields are laid out sequentially, in the order that
    //     the fields were emitted to the metadata.
    SequentialLayout = 8,
    //
    // Summary:
    //     Specifies that class fields are laid out at the specified offsets.
    ExplicitLayout = 16,
    //
    // Summary:
    //     Specifies class layout information.
    LayoutMask = 24,
    //
    // Summary:
    //     Specifies class semantics information; the current class is contextful (else
    //     agile).
    ClassSemanticsMask = 32,
    //
    // Summary:
    //     Specifies that the type is an interface.
    Interface = 32,
    //
    // Summary:
    //     Specifies that the type is abstract.
    Abstract = 128,
    //
    // Summary:
    //     Specifies that the class is concrete and cannot be extended.
    Sealed = 256,
    //
    // Summary:
    //     Specifies that the class is special in a way denoted by the name.
    SpecialName = 1024,
    //
    // Summary:
    //     Runtime should check name encoding.
    RTSpecialName = 2048,
    //
    // Summary:
    //     Specifies that the class or interface is imported from another module.
    Import = 4096,
    //
    // Summary:
    //     Specifies that the class can be serialized.
    Serializable = 8192,
    //
    // Summary:
    //     LPTSTR is interpreted as UNICODE.
    UnicodeClass = 65536,
    //
    // Summary:
    //     LPTSTR is interpreted automatically.
    AutoClass = 131072,
    //
    // Summary:
    //     LPSTR is interpreted by some implementation-specific means, which includes
    //     the possibility of throwing a System.NotSupportedException.
    CustomFormatClass = 196608,
    //
    // Summary:
    //     Used to retrieve string information for native interoperability.
    StringFormatMask = 196608,
    //
    // Summary:
    //     Type has security associate with it.
    HasSecurity = 262144,
    //
    // Summary:
    //     Attributes reserved for runtime use.
    ReservedMask = 264192,
    //
    // Summary:
    //     Specifies that calling static methods of the type does not force the system
    //     to initialize the type.
    BeforeFieldInit = 1048576,
    //
    // Summary:
    //     Used to retrieve non-standard encoding information for native interop. The
    //     meaning of the values of these 2 bits is unspecified.
    CustomFormatMask = 12582912,
  }
}
