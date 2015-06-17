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
using System.Runtime.Serialization;

namespace System.Threading
{
  // Summary:
  //     Provides methods for setting and capturing the compressed stack on the current
  //     thread. This class cannot be inherited.
  public sealed class CompressedStack
  {
    // Summary:
    //     Captures the compressed stack from the current thread.
    //
    // Returns:
    //     A System.Threading.CompressedStack object.
    extern public static CompressedStack Capture();
    //
    // Summary:
    //     Creates a copy of the current compressed stack.
    //
    // Returns:
    //     A System.Threading.CompressedStack object representing the current compressed
    //     stack.
    extern public CompressedStack CreateCopy();
    //
    // Summary:
    //     Gets the compressed stack for the current thread.
    //
    // Returns:
    //     A System.Threading.CompressedStack for the current thread.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     A caller in the call chain does not have permission to access unmanaged code.-or-The
    //     request for System.Security.Permissions.StrongNameIdentityPermission failed.
    extern public static CompressedStack GetCompressedStack();
    //
    // Summary:
    //     Sets the System.Runtime.Serialization.SerializationInfo object with the logical
    //     context information needed to recreate an instance of this execution context.
    //
    // Parameters:
    //   info:
    //     The System.Runtime.Serialization.SerializationInfo object to be populated
    //     with serialization information.
    //
    //   context:
    //     The System.Runtime.Serialization.StreamingContext structure representing
    //     the destination context of the serialization.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     info is null.
    extern public void GetObjectData(SerializationInfo info, StreamingContext context);
    //
    // Summary:
    //     Runs a method in the specified compressed stack on the current thread.
    //
    // Parameters:
    //   compressedStack:
    //     The System.Threading.CompressedStack to set.
    //
    //   callback:
    //     A System.Threading.ContextCallback that represents the method to be run in
    //     the specified security context.
    //
    //   state:
    //     The object to be passed to the callback method.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     compressedStack is null.
#if false
    public static void Run(CompressedStack compressedStack, ContextCallback callback, object state);
#endif
  }
}
