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
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Reflection.Emit {
  // Summary:
  //     Defines the access modes for a dynamic assembly.
  [Serializable]
  [ComVisible(true)]
  [Flags]
  public enum AssemblyBuilderAccess {
    // Summary:
    //     Represents that the dynamic assembly can be executed, but not saved.
    Run = 1,
    //
    // Summary:
    //     Represents that the dynamic assembly can be saved, but not executed.
    Save = 2,
    //
    // Summary:
    //     Represents that the dynamic assembly can be executed and saved.
    RunAndSave = 3,
    //
    // Summary:
    //     Represents that the dynamic assembly is loaded into the reflection-only context,
    //     and cannot be executed.
    ReflectionOnly = 6,
  }
}
