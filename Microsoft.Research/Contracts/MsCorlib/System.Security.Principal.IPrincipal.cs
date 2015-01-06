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

namespace System.Security.Principal {
  // Summary:
  //     Defines the basic functionality of a principal object.
  //[ComVisible(true)]

  [ContractClass(typeof(IPrincipalContracts))]
  public interface IPrincipal {
    // Summary:
    //     Gets the identity of the current principal.
    //
    // Returns:
    //     The System.Security.Principal.IIdentity object associated with the current
    //     principal.
    IIdentity Identity { get; }

    // Summary:
    //     Determines whether the current principal belongs to the specified role.
    //
    // Parameters:
    //   role:
    //     The name of the role for which to check membership.
    //
    // Returns:
    //     true if the current principal is a member of the specified role; otherwise,
    //     false.
    [Pure]
    bool IsInRole(string role);
  }

  [ContractClassFor(typeof(IPrincipal))]
  abstract class IPrincipalContracts 
    : IPrincipal
  {
    IIdentity IPrincipal.Identity
    {
      get
      {
        Contract.Ensures(Contract.Result<IIdentity>() != null);

        return default(IIdentity);
      }
    }

    [Pure]
    bool IPrincipal.IsInRole(string role)
    {
      return default(bool);
    }
  }

}
