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
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  // Summary:
  //     Represents a local variable within a method or constructor.
  public sealed class LocalBuilder
  { // : LocalVariableInfo, _LocalBuilder
    // Summary:
    //     Sets the name of this local variable.
    //
    // Parameters:
    //   name:
    //     The name of the local variable.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The containing type has been created with System.Reflection.Emit.TypeBuilder.CreateType().-or-
    //     There is no symbolic writer defined for the containing module.
    //
    //   System.NotSupportedException:
    //     This local is defined in a dynamic method, rather than in a method of a dynamic
    //     type.
    extern public void SetLocalSymInfo(string name);
    //
    // Summary:
    //     Sets the name and lexical scope of this local variable.
    //
    // Parameters:
    //   name:
    //     The name of the local variable.
    //
    //   startOffset:
    //     The beginning offset of the lexical scope of the local variable.
    //
    //   endOffset:
    //     The ending offset of the lexical scope of the local variable.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The containing type has been created with System.Reflection.Emit.TypeBuilder.CreateType().-or-
    //     There is no symbolic writer defined for the containing module.
    //
    //   System.NotSupportedException:
    //     This local is defined in a dynamic method, rather than in a method of a dynamic
    //     type.
    extern public void SetLocalSymInfo(string name, int startOffset, int endOffset);
  }
}
