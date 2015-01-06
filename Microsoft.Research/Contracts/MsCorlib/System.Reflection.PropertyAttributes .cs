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
  //     Defines the attributes that can be associated with a property. These attribute
  //     values are defined in corhdr.h.
  [Serializable]
  [Flags]
  [ComVisible(true)]
  public enum PropertyAttributes {
    // Summary:
    //     Specifies that no attributes are associated with a property.
    None = 0,
    //
    // Summary:
    //     Specifies that the property is special, with the name describing how the
    //     property is special.
    SpecialName = 512,
    //
    // Summary:
    //     Specifies that the metadata internal APIs check the name encoding.
    RTSpecialName = 1024,
    //
    // Summary:
    //     Specifies that the property has a default value.
    HasDefault = 4096,
    //
    // Summary:
    //     Reserved.
    Reserved2 = 8192,
    //
    // Summary:
    //     Reserved.
    Reserved3 = 16384,
    //
    // Summary:
    //     Reserved.
    Reserved4 = 32768,
    //
    // Summary:
    //     Specifies a flag reserved for runtime use only.
    ReservedMask = 62464,
  }
}
