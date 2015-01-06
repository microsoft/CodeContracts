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
  //     Specifies the permitted use of the System.Reflection and System.Reflection.Emit
  //     namespaces.
  [Serializable]
  [ComVisible(true)]
  [Flags]
  public enum ReflectionPermissionFlag {
    // Summary:
    //     Enumeration of types and members is allowed. Invocation operations are allowed
    //     on visible types and members.
    NoFlags = 0,
    //
    // Summary:
    //     This flag is obsolete. No flags are necessary to enumerate types and members
    //     and to examine their metadata. Use System.Security.Permissions.ReflectionPermissionFlag.NoFlags
    //     instead.
    [Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    TypeInformation = 1,
    //
    // Summary:
    //     Invocation operations on all members are allowed, regardless of grant set.
    //     If this flag is not set, invocation operations are allowed only on visible
    //     members.
    MemberAccess = 2,
    //
    // Summary:
    //     Emitting debug symbols is allowed. Beginning with the .NET Framework version
    //     2.0 Service Pack 1, this flag is no longer required to emit code.
    ReflectionEmit = 4,
    //
    // Summary:
    //     TypeInformation, MemberAccess, and ReflectionEmit are set. System.Security.Permissions.ReflectionPermissionFlag.AllFlags
    //     does not include System.Security.Permissions.ReflectionPermissionFlag.RestrictedMemberAccess.
    AllFlags = 7,
    //
    // Summary:
    //     Restricted member access is provided for partially trusted code. Partially
    //     trusted code can access nonpublic types and members, but only if the grant
    //     set of the partially trusted code includes all permissions in the grant set
    //     of the assembly that contains the nonpublic types and members being accessed.
    //     This flag is new in the .NET Framework 2.0 SP1.
    [ComVisible(false)]
    RestrictedMemberAccess = 8,
  }
}
