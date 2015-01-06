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

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
  // Summary:
  //     Provides the basic functionality for a remoting activator class.
  public interface IActivator
  {
    // Summary:
    //     Gets the System.Runtime.Remoting.Activation.ActivatorLevel where this activator
    //     is active.
    //
    // Returns:
    //     The System.Runtime.Remoting.Activation.ActivatorLevel where this activator
    //     is active.
    ActivatorLevel Level { get; }
    //
    // Summary:
    //     Gets or sets the next activator in the chain.
    //
    // Returns:
    //     The next activator in the chain.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have infrastructure permission.
    IActivator NextActivator { get; set; }

    // Summary:
    //     Creates an instance of the object that is specified in the provided System.Runtime.Remoting.Activation.IConstructionCallMessage.
    //
    // Parameters:
    //   msg:
    //     The information about the object that is needed to activate it, stored in
    //     a System.Runtime.Remoting.Activation.IConstructionCallMessage.
    //
    // Returns:
    //     Status of the object activation contained in a System.Runtime.Remoting.Activation.IConstructionReturnMessage.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller does not have infrastructure permission.
    IConstructionReturnMessage Activate(IConstructionCallMessage msg);
  }
}
