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
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
  // Summary:
  //     Provides type information for an object.
  public interface IRemotingTypeInfo
  {
    // Summary:
    //     Gets or sets the fully qualified type name of the server object in a System.Runtime.Remoting.ObjRef.
    //
    // Returns:
    //     The fully qualified type name of the server object in a System.Runtime.Remoting.ObjRef.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    string TypeName { get; set; }

    // Summary:
    //     Checks whether the proxy that represents the specified object type can be
    //     cast to the type represented by the System.Runtime.Remoting.IRemotingTypeInfo
    //     interface.
    //
    // Parameters:
    //   fromType:
    //     The type to cast to.
    //
    //   o:
    //     The object for which to check casting.
    //
    // Returns:
    //     true if cast will succeed; otherwise, false.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    bool CanCastTo(Type fromType, object o);
  }
}
