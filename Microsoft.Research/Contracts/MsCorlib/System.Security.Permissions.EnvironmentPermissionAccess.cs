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

namespace System.Security.Permissions {
  // Summary:
  //     Specifies access to environment variables.
  [Serializable]
  [Flags]
  [ComVisible(true)]
  public enum EnvironmentPermissionAccess {
    // Summary:
    //     No access to environment variables. System.Security.Permissions.EnvironmentPermissionAccess.NoAccess
    //     represents no valid System.Security.Permissions.EnvironmentPermissionAccess
    //     values and causes an System.ArgumentException when used as the parameter
    //     for System.Security.Permissions.EnvironmentPermission.GetPathList(System.Security.Permissions.EnvironmentPermissionAccess),
    //     which expects a single value.
    NoAccess = 0,
    //
    // Summary:
    //     Only read access to environment variables is specified. Changing, deleting
    //     and creating environment variables is not included in this access level.
    Read = 1,
    //
    // Summary:
    //     Only write access to environment variables is specified. Write access includes
    //     creating and deleting environment variables as well as changing existing
    //     values. Reading environment variables is not included in this access level.
    Write = 2,
    //
    // Summary:
    //     System.Security.Permissions.EnvironmentPermissionAccess.Read and System.Security.Permissions.EnvironmentPermissionAccess.Write
    //     access to environment variables. System.Security.Permissions.EnvironmentPermissionAccess.AllAccess
    //     represents multiple System.Security.Permissions.EnvironmentPermissionAccess
    //     values and causes an System.ArgumentException when used as the flag parameter
    //     for the System.Security.Permissions.EnvironmentPermission.GetPathList(System.Security.Permissions.EnvironmentPermissionAccess)
    //     method, which expects a single value.
    AllAccess = 3,
  }
}
