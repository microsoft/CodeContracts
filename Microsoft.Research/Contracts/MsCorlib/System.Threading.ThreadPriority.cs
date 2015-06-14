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

namespace System.Threading
{
  // Summary:
  //     Specifies the scheduling priority of a System.Threading.Thread.
  public enum ThreadPriority
  {
    // Summary:
    //     The System.Threading.Thread can be scheduled after threads with any other
    //     priority.
    Lowest = 0,
    //
    // Summary:
    //     The System.Threading.Thread can be scheduled after threads with Normal priority
    //     and before those with Lowest priority.
    BelowNormal = 1,
    //
    // Summary:
    //     The System.Threading.Thread can be scheduled after threads with AboveNormal
    //     priority and before those with BelowNormal priority. Threads have Normal
    //     priority by default.
    Normal = 2,
    //
    // Summary:
    //     The System.Threading.Thread can be scheduled after threads with Highest priority
    //     and before those with Normal priority.
    AboveNormal = 3,
    //
    // Summary:
    //     The System.Threading.Thread can be scheduled before threads with any other
    //     priority.
    Highest = 4,
  }
}
