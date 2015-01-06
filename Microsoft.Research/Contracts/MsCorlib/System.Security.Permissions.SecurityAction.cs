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

namespace System.Security.Permissions {
  // Summary:
  //     Specifies the security actions that can be performed using declarative security.
  [Serializable]
  [ComVisible(true)]
  public enum SecurityAction {
    // Summary:
    //     All callers higher in the call stack are required to have been granted the
    //     permission specified by the current permission object (see [<topic://cpconmakingsecuritydemands>]).
    Demand = 2,
    //
    // Summary:
    //     The calling code can access the resource identified by the current permission
    //     object, even if callers higher in the stack have not been granted permission
    //     to access the resource (see [<topic://cpconassert>]).
    Assert = 3,
    //
    // Summary:
    //     The ability to access the resource specified by the current permission object
    //     is denied to callers, even if they have been granted permission to access
    //     it (see [<topic://cpcondeny>]).
    Deny = 4,
    //
    // Summary:
    //     Only the resources specified by this permission object can be accessed, even
    //     if the code has been granted permission to access other resources (see [<topic://cpconpermitonly>]).
    PermitOnly = 5,
    //
    // Summary:
    //     The immediate caller is required to have been granted the specified permission.
    LinkDemand = 6,
    //
    // Summary:
    //     The derived class inheriting the class or overriding a method is required
    //     to have been granted the specified permission. For more information, see
    //     Inheritance Demands.
    InheritanceDemand = 7,
    //
    // Summary:
    //     The request for the minimum permissions required for code to run. This action
    //     can only be used within the scope of the assembly.
    RequestMinimum = 8,
    //
    // Summary:
    //     The request for additional permissions that are optional (not required to
    //     run). This request implicitly refuses all other permissions not specifically
    //     requested. This action can only be used within the scope of the assembly.
    RequestOptional = 9,
    //
    // Summary:
    //     The request that permissions that might be misused will not be granted to
    //     the calling code. This action can only be used within the scope of the assembly.
    RequestRefuse = 10,
  }
}
