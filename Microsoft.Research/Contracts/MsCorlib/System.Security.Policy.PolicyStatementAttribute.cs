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
using System.Runtime.InteropServices;

namespace System.Security.Policy {
  // Summary:
  //     Defines special attribute flags for security policy on code groups.
  [Serializable]
  [Flags]
  [ComVisible(true)]
  public enum PolicyStatementAttribute {
    // Summary:
    //     No flags are set.
    Nothing = 0,
    //
    // Summary:
    //     The exclusive code group flag. When a code group has this flag set, only
    //     the permissions associated with that code group are granted to code belonging
    //     to the code group. At most, one code group matching a given piece of code
    //     can be set as exclusive.
    Exclusive = 1,
    //
    // Summary:
    //     The flag representing a policy statement that causes lower policy levels
    //     to not be evaluated as part of the resolve operation, effectively allowing
    //     the policy level to override lower levels.
    LevelFinal = 2,
    //
    // Summary:
    //     All attribute flags are set.
    All = 3,
  }
}
