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
using System.Security;

namespace System.Security.Policy {
  // Summary:
  //     Encapsulates security decisions about an application. This class cannot be
  //     inherited.
  
  
  public sealed class ApplicationTrust : ISecurityEncodable {
    // Summary:
    //     Initializes a new instance of the System.Security.Policy.ApplicationTrust
    //     class.
    extern public ApplicationTrust();
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Policy.ApplicationTrust
    //     class with an System.ApplicationIdentity.
    //
    // Parameters:
    //   applicationIdentity:
    //     An System.ApplicationIdentity that uniquely identifies an application.
    extern public ApplicationTrust(ApplicationIdentity applicationIdentity);

    // Summary:
    //     Gets or sets the application identity for the application trust object.
    //
    // Returns:
    //     An System.ApplicationIdentity for the application trust object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     System.ApplicationIdentity cannot be set because it has a value of null.
    extern public ApplicationIdentity ApplicationIdentity { get; set; }
    //
    // Summary:
    //     Gets or sets the policy statement defining the default grant set.
    //
    // Returns:
    //     A System.Security.Policy.PolicyStatement describing the default grants.
    extern public PolicyStatement DefaultGrantSet { get; set; }
    //
    // Summary:
    //     Gets or sets extra security information about the application.
    //
    // Returns:
    //     An object containing additional security information about the application.
    extern public object ExtraInfo { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the application has the required
    //     permission grants and is trusted to run.
    //
    // Returns:
    //     true if the application is trusted to run; otherwise, false. The default
    //     is false.
    extern public bool IsApplicationTrustedToRun { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether application trust information is
    //     persisted.
    //
    // Returns:
    //     true if application trust information is persisted; otherwise, false. The
    //     default is false.
    extern public bool Persist { get; set; }

    // Summary:
    //     Reconstructs an System.Security.Policy.ApplicationTrust object with a given
    //     state from an XML encoding.
    //
    // Parameters:
    //   element:
    //     The XML encoding to use to reconstruct the System.Security.Policy.ApplicationTrust
    //     object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     element is null.
    //
    //   System.ArgumentException:
    //     The XML encoding used for element is invalid.
    extern public void FromXml(SecurityElement element);
    //
    // Summary:
    //     Creates an XML encoding of the System.Security.Policy.ApplicationTrust object
    //     and its current state.
    //
    // Returns:
    //     An XML encoding of the security object, including any state information.
    extern public SecurityElement ToXml();
  }
}
