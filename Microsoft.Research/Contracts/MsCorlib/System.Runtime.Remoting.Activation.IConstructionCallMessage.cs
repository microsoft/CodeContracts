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
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation {
  // Summary:
  //     Represents the construction call request of an object.
  [ComVisible(true)]
  public interface IConstructionCallMessage : IMethodCallMessage, IMethodMessage, IMessage {
    // Summary:
    //     Gets the type of the remote object to activate.
    //
    // Returns:
    //     The type of the remote object to activate.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have infrastructure permission.
    Type ActivationType { get; }
    //
    // Summary:
    //     Gets the full type name of the remote type to activate.
    //
    // Returns:
    //     The full type name of the remote type to activate.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have infrastructure permission.
    string ActivationTypeName { get; }
    //
    // Summary:
    //     Gets or sets the activator that activates the remote object.
    //
    // Returns:
    //     The activator that activates the remote object.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have infrastructure permission.
    IActivator Activator { get; set; }
    //
    // Summary:
    //     Gets the call site activation attributes.
    //
    // Returns:
    //     The call site activation attributes.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have infrastructure permission.
    object[] CallSiteActivationAttributes { get; }
    //
    // Summary:
    //     Gets a list of context properties that define the context in which the object
    //     is to be created.
    //
    // Returns:
    //     A list of properties of the context in which to construct the object.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have infrastructure permission.
    IList ContextProperties { get; }
  }
}
