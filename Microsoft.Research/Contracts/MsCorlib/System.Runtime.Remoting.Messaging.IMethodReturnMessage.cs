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

namespace System.Runtime.Remoting.Messaging {
  // Summary:
  //     Defines the method call return message interface.
  [ComVisible(true)]
  public interface IMethodReturnMessage : IMethodMessage, IMessage {
    // Summary:
    //     Gets the exception thrown during the method call.
    //
    // Returns:
    //     The exception object for the method call, or null if the method did not throw
    //     an exception.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    Exception Exception { get; }
    //
    // Summary:
    //     Gets the number of arguments in the method call marked as ref or out parameters.
    //
    // Returns:
    //     The number of arguments in the method call marked as ref or out parameters.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    int OutArgCount { get; }
    //
    // Summary:
    //     Returns the specified argument marked as a ref or an out parameter.
    //
    // Returns:
    //     The specified argument marked as a ref or an out parameter.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    object[] OutArgs { get; }
    //
    // Summary:
    //     Gets the return value of the method call.
    //
    // Returns:
    //     The return value of the method call.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    object ReturnValue { get; }

    // Summary:
    //     Returns the specified argument marked as a ref or an out parameter.
    //
    // Parameters:
    //   argNum:
    //     The number of the requested argument.
    //
    // Returns:
    //     The specified argument marked as a ref or an out parameter.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    object GetOutArg(int argNum);
    //
    // Summary:
    //     Returns the name of the specified argument marked as a ref or an out parameter.
    //
    // Parameters:
    //   index:
    //     The number of the requested argument name.
    //
    // Returns:
    //     The argument name, or null if the current method is not implemented.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    string GetOutArgName(int index);
  }
}
