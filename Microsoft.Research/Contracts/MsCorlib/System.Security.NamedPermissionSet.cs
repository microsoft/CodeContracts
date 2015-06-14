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
using System.Security.Permissions;

namespace System.Security
{
  // Summary:
  //     Defines a permission set that has a name and description associated with
  //     it. This class cannot be inherited.
  public sealed class NamedPermissionSet : PermissionSet
  {
    // Summary:
    //     Initializes a new instance of the System.Security.NamedPermissionSet class
    //     from another named permission set.
    //
    // Parameters:
    //   permSet:
    //     The named permission set from which to create the new instance.
    extern public NamedPermissionSet(NamedPermissionSet permSet);
    //
    // Summary:
    //     Initializes a new, empty instance of the System.Security.NamedPermissionSet
    //     class with the specified name.
    //
    // Parameters:
    //   name:
    //     The name for the new named permission set.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The name parameter is null or is an empty string ("").
    extern public NamedPermissionSet(string name);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.NamedPermissionSet class
    //     with the specified name from a permission set.
    //
    // Parameters:
    //   name:
    //     The name for the named permission set.
    //
    //   permSet:
    //     The permission set from which to take the value of the new named permission
    //     set.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The name parameter is null or is an empty string ("").
    extern public NamedPermissionSet(string name, PermissionSet permSet);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.NamedPermissionSet class
    //     with the specified name in either an unrestricted or a fully restricted state.
    //
    // Parameters:
    //   name:
    //     The name for the new named permission set.
    //
    //   state:
    //     One of the System.Security.Permissions.PermissionState values.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The name parameter is null or is an empty string ("").
    extern public NamedPermissionSet(string name, PermissionState state);

    // Summary:
    //     Gets or sets the text description of the current named permission set.
    //
    // Returns:
    //     A text description of the named permission set.
    extern public string Description { get; set; }
    //
    // Summary:
    //     Gets or sets the name of the current named permission set.
    //
    // Returns:
    //     The name of the named permission set.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The name is null or is an empty string ("").
    extern public string Name { get; set; }

    //
    // Summary:
    //     Creates a copy of the named permission set with a different name but the
    //     same permissions.
    //
    // Parameters:
    //   name:
    //     The name for the new named permission set.
    //
    // Returns:
    //     A copy of the named permission set with the new name.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The name parameter is null or is an empty string ("").
    extern public NamedPermissionSet Copy(string name);
  }
}
