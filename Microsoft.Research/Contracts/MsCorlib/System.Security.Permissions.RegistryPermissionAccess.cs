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
  //     Specifies the permitted access to registry keys and values.
  [Serializable]
  [ComVisible(true)]
  [Flags]
  public enum RegistryPermissionAccess {
    // Summary:
    //     No access to registry variables. System.Security.Permissions.RegistryPermissionAccess.NoAccess
    //     represents no valid System.Security.Permissions.RegistryPermissionAccess
    //     values and causes an System.ArgumentException when used as the parameter
    //     for System.Security.Permissions.RegistryPermission.GetPathList(System.Security.Permissions.RegistryPermissionAccess),
    //     which expects a single value.
    NoAccess = 0,
    //
    // Summary:
    //     Read access to registry variables.
    Read = 1,
    //
    // Summary:
    //     Write access to registry variables.
    Write = 2,
    //
    // Summary:
    //     Create access to registry variables.
    Create = 4,
    //
    // Summary:
    //     System.Security.Permissions.RegistryPermissionAccess.Create, System.Security.Permissions.RegistryPermissionAccess.Read,
    //     and System.Security.Permissions.RegistryPermissionAccess.Write access to
    //     registry variables. System.Security.Permissions.RegistryPermissionAccess.AllAccess
    //     represents multiple System.Security.Permissions.RegistryPermissionAccess
    //     values and causes an System.ArgumentException when used as the access parameter
    //     for the System.Security.Permissions.RegistryPermission.GetPathList(System.Security.Permissions.RegistryPermissionAccess)
    //     method, which expects a single value.
    AllAccess = 7,
  }
}
