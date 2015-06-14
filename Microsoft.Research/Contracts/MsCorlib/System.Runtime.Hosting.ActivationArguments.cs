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

namespace System.Runtime.Hosting {
  // Summary:
  //     Provides data for manifest-based activation of an application. This class
  //     cannot be inherited.
  
  
  public sealed class ActivationArguments {
    // Summary:
    //     Initializes a new instance of the System.Runtime.Hosting.ActivationArguments
    //     class with the specified activation context.
    //
    // Parameters:
    //   activationData:
    //     An System.ActivationContext object identifying the manifest-based activation
    //     application.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     activationData is null.
    extern public ActivationArguments(ActivationContext activationData);
    //
    // Summary:
    //     Initializes a new instance of the System.Runtime.Hosting.ActivationArguments
    //     class with the specified application identity.
    //
    // Parameters:
    //   applicationIdentity:
    //     An System.ApplicationIdentity object identifying the manifest-based activation
    //     application.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     applicationIdentity is null.
    extern public ActivationArguments(ApplicationIdentity applicationIdentity);
    //
    // Summary:
    //     Initializes a new instance of the System.Runtime.Hosting.ActivationArguments
    //     class with the specified activation context and activation data.
    //
    // Parameters:
    //   activationContext:
    //     An System.ActivationContext object identifying the manifest-based activation
    //     application.
    //
    //   activationData:
    //     An array of strings containing host-provided activation data.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     activationContext is null.
    extern public ActivationArguments(ActivationContext activationContext, string[] activationData);
    //
    // Summary:
    //     Initializes a new instance of the System.Runtime.Hosting.ActivationArguments
    //     class with the specified application identity and activation data.
    //
    // Parameters:
    //   applicationIdentity:
    //     An System.ApplicationIdentity object identifying the manifest-based activation
    //     application.
    //
    //   activationData:
    //     An array of strings containing host-provided activation data.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     applicationIdentity is null.
    extern public ActivationArguments(ApplicationIdentity applicationIdentity, string[] activationData);

    // Summary:
    //     Gets the activation context for manifest-based activation of an application.
    //
    // Returns:
    //     An System.ActivationContext object identifying a manifest-based activation
    //     application.
    extern public ActivationContext ActivationContext { get; }
    //
    // Summary:
    //     Gets activation data from the host.
    //
    // Returns:
    //     An array of strings containing host-provided activation data.
    extern public string[] ActivationData { get; }
    //
    // Summary:
    //     Gets the application identity for a manifest-activated application.
    //
    // Returns:
    //     An System.ApplicationIdentity object identifying an application for manifest-based
    //     activation.
    extern public ApplicationIdentity ApplicationIdentity { get; }
  }
}
