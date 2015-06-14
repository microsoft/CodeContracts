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

namespace System.Security {
  // Summary:
  //     Defines methods implemented by permission types.
  [ComVisible(true)]
  public interface IPermission : ISecurityEncodable {
    // Summary:
    //     Creates and returns an identical copy of the current permission.
    //
    // Returns:
    //     A copy of the current permission.
    IPermission Copy();
    //
    // Summary:
    //     Throws a System.Security.SecurityException at run time if the security requirement
    //     is not met.
    void Demand();
    //
    // Summary:
    //     Creates and returns a permission that is the intersection of the current
    //     permission and the specified permission.
    //
    // Parameters:
    //   target:
    //     A permission to intersect with the current permission. It must be of the
    //     same type as the current permission.
    //
    // Returns:
    //     A new permission that represents the intersection of the current permission
    //     and the specified permission. This new permission is null if the intersection
    //     is empty.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The target parameter is not null and is not an instance of the same class
    //     as the current permission.
    IPermission Intersect(IPermission target);
    //
    // Summary:
    //     Determines whether the current permission is a subset of the specified permission.
    //
    // Parameters:
    //   target:
    //     A permission that is to be tested for the subset relationship. This permission
    //     must be of the same type as the current permission.
    //
    // Returns:
    //     true if the current permission is a subset of the specified permission; otherwise,
    //     false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The target parameter is not null and is not of the same type as the current
    //     permission.
    bool IsSubsetOf(IPermission target);
    //
    // Summary:
    //     Creates a permission that is the union of the current permission and the
    //     specified permission.
    //
    // Parameters:
    //   target:
    //     A permission to combine with the current permission. It must be of the same
    //     type as the current permission.
    //
    // Returns:
    //     A new permission that represents the union of the current permission and
    //     the specified permission.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The target parameter is not null and is not of the same type as the current
    //     permission.
    IPermission Union(IPermission target);
  }
}