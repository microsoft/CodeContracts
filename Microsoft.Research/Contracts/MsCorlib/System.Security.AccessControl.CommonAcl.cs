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
using System.Reflection;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  // Summary:
  //     Represents an access control list (ACL) and is the base class for the System.Security.AccessControl.DiscretionaryAcl
  //     and System.Security.AccessControl.SystemAcl classes.
  public abstract class CommonAcl : GenericAcl
  {

    //
    // Summary:
    //     Gets a Boolean value that specifies whether the access control entries (ACEs)
    //     in the current System.Security.AccessControl.CommonAcl object are in canonical
    //     order.
    //
    // Returns:
    //     true if the ACEs in the current System.Security.AccessControl.CommonAcl object
    //     are in canonical order; otherwise, false.
    extern public bool IsCanonical { get; }
    //
    // Summary:
    //     Sets whether the System.Security.AccessControl.CommonAcl object is a container.
    //
    // Returns:
    //     true if the current System.Security.AccessControl.CommonAcl object is a container.
    extern public bool IsContainer { get; }
    //
    // Summary:
    //     Sets whether the current System.Security.AccessControl.CommonAcl object is
    //     a directory object access control list (ACL).
    //
    // Returns:
    //     true if the current System.Security.AccessControl.CommonAcl object is a directory
    //     object ACL.
    extern public bool IsDS { get; }

    //
    // Summary:
    //     Removes all access control entries (ACEs) contained by this System.Security.AccessControl.CommonAcl
    //     object that are associated with the specified System.Security.Principal.SecurityIdentifier
    //     object.
    //
    // Parameters:
    //   sid:
    //     The System.Security.Principal.SecurityIdentifier object to check for.
    extern public void Purge(SecurityIdentifier sid);
    //
    // Summary:
    //     Removes all inherited access control entries (ACEs) from this System.Security.AccessControl.CommonAcl
    //     object.
    extern public void RemoveInheritedAces();
  }
}
