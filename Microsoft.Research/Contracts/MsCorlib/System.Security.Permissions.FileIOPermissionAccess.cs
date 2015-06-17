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
  //     Specifies the type of file access requested.
  [Serializable]
  [Flags]
  [ComVisible(true)]
  public enum FileIOPermissionAccess {
    // Summary:
    //     No access to a file or directory. System.Security.Permissions.FileIOPermissionAccess.NoAccess
    //     represents no valid System.Security.Permissions.FileIOPermissionAccess values
    //     and causes an System.ArgumentException when used as the parameter for System.Security.Permissions.FileIOPermission.GetPathList(System.Security.Permissions.FileIOPermissionAccess),
    //     which expects a single value.
    NoAccess = 0,
    //
    // Summary:
    //     Access to read from a file or directory.
    Read = 1,
    //
    // Summary:
    //     Access to write to or delete a file or directory. System.Security.Permissions.FileIOPermissionAccess.Write
    //     access includes deleting and overwriting files or directories.
    Write = 2,
    //
    // Summary:
    //     Access to append material to a file or directory. System.Security.Permissions.FileIOPermissionAccess.Append
    //     access includes the ability to create a new file or directory.
    Append = 4,
    //
    // Summary:
    //     Access to the information in the path itself. This helps protect sensitive
    //     information in the path, such as user names, as well as information about
    //     the directory structure revealed in the path. This value does not grant access
    //     to files or folders represented by the path.
    PathDiscovery = 8,
    //
    // Summary:
    //     System.Security.Permissions.FileIOPermissionAccess.Append, System.Security.Permissions.FileIOPermissionAccess.Read,
    //     System.Security.Permissions.FileIOPermissionAccess.Write, and System.Security.Permissions.FileIOPermissionAccess.PathDiscovery
    //     access to a file or directory. System.Security.Permissions.FileIOPermissionAccess.AllAccess
    //     represents multiple System.Security.Permissions.FileIOPermissionAccess values
    //     and causes an System.ArgumentException when used as the access parameter
    //     for the System.Security.Permissions.FileIOPermission.GetPathList(System.Security.Permissions.FileIOPermissionAccess)
    //     method, which expects a single value.
    AllAccess = 15,
  }
}
