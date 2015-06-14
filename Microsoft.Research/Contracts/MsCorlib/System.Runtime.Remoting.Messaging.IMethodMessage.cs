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
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging {
  // Summary:
  //     Defines the method message interface.
  [ComVisible(true)]
  public interface IMethodMessage : IMessage {
    // Summary:
    //     Gets the number of arguments passed to the method.
    //
    // Returns:
    //     The number of arguments passed to the method.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    int ArgCount { get; }
    //
    // Summary:
    //     Gets an array of arguments passed to the method.
    //
    // Returns:
    //     An System.Object array containing the arguments passed to the method.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    object[] Args { get; }
    //
    // Summary:
    //     Gets a value indicating whether the message has variable arguments.
    //
    // Returns:
    //     true if the method can accept a variable number of arguments; otherwise,
    //     false.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    bool HasVarArgs { get; }
    //
    // Summary:
    //     Gets the System.Runtime.Remoting.Messaging.LogicalCallContext for the current
    //     method call.
    //
    // Returns:
    //     Gets the System.Runtime.Remoting.Messaging.LogicalCallContext for the current
    //     method call.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    LogicalCallContext LogicalCallContext { get; }
    //
    // Summary:
    //     Gets the System.Reflection.MethodBase of the called method.
    //
    // Returns:
    //     The System.Reflection.MethodBase of the called method.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    MethodBase MethodBase { get; }
    //
    // Summary:
    //     Gets the name of the invoked method.
    //
    // Returns:
    //     The name of the invoked method.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    string MethodName { get; }
    //
    // Summary:
    //     Gets an object containing the method signature.
    //
    // Returns:
    //     An object containing the method signature.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    object MethodSignature { get; }
    //
    // Summary:
    //     Gets the full System.Type name of the specific object that the call is destined
    //     for.
    //
    // Returns:
    //     The full System.Type name of the specific object that the call is destined
    //     for.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    string TypeName { get; }
    //
    // Summary:
    //     Gets the URI of the specific object that the call is destined for.
    //
    // Returns:
    //     The URI of the remote object that contains the invoked method.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    string Uri { get; }

    // Summary:
    //     Gets a specific argument as an System.Object.
    //
    // Parameters:
    //   argNum:
    //     The number of the requested argument.
    //
    // Returns:
    //     The argument passed to the method.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    object GetArg(int argNum);
    //
    // Summary:
    //     Gets the name of the argument passed to the method.
    //
    // Parameters:
    //   index:
    //     The number of the requested argument.
    //
    // Returns:
    //     The name of the specified argument passed to the method, or null if the current
    //     method is not implemented.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    string GetArgName(int index);
  }
}
