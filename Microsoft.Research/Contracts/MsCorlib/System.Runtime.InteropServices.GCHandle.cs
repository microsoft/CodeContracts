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

#if !SILVERLIGHT
using System;
using System.Security;
using System.Diagnostics.Contracts;

namespace System.Runtime.InteropServices
{
  public struct GCHandle
  {
    [Pure]
    extern public static bool operator !=(GCHandle a, GCHandle b);
    
    [Pure]
    extern public static bool operator ==(GCHandle a, GCHandle b);

    [Pure]
    extern public static explicit operator IntPtr(GCHandle value);

    [Pure]
    public static explicit operator GCHandle(IntPtr value)
    {
      Contract.Requires(value != IntPtr.Zero);

      return default(GCHandle);
    }

    public bool IsAllocated { get { return false; } }
    public object Target
    {
      get { return null; }
      set
      {
        Contract.Requires(!object.Equals(value, IntPtr.Zero));
      }
    }

    [Pure]
    public IntPtr AddrOfPinnedObject()
    {
      Contract.Ensures(Contract.Result<IntPtr>() != IntPtr.Zero);

      return default(IntPtr);
    }

    [Pure]
    extern public static GCHandle Alloc(object value);

    [Pure]
    public static GCHandle Alloc(object value, GCHandleType type)
    {
      Contract.Requires(type <= GCHandleType.Pinned);

      return default(GCHandle);
    }

    [Pure]
    public static GCHandle FromIntPtr(IntPtr value)
    {
      Contract.Requires(value != IntPtr.Zero);

      return default(GCHandle);
    }
    
    [Pure]
    extern public static IntPtr ToIntPtr(GCHandle value);
  }

  public enum GCHandleType
  {
    // Summary:
    //     This handle type is used to track an object, but allow it to be collected.
    //     When an object is collected, the contents of the System.Runtime.InteropServices.GCHandle
    //     are zeroed. Weak references are zeroed before the finalizer runs, so even
    //     if the finalizer resurrects the object, the Weak reference is still zeroed.
    Weak = 0,
    //
    // Summary:
    //     This handle type is similar to System.Runtime.InteropServices.GCHandleType.Weak,
    //     but the handle is not zeroed if the object is resurrected during finalization.
    WeakTrackResurrection = 1,
    //
    // Summary:
    //     This handle type represents an opaque handle, meaning you cannot resolve
    //     the address of the pinned object through the handle. You can use this type
    //     to track an object and prevent its collection by the garbage collector. This
    //     enumeration member is useful when an unmanaged client holds the only reference,
    //     which is undetectable from the garbage collector, to a managed object.
    Normal = 2,
    //
    // Summary:
    //     This handle type is similar to System.Runtime.InteropServices.GCHandleType.Normal,
    //     but allows the address of the pinned object to be taken. This prevents the
    //     garbage collector from moving the object and hence undermines the efficiency
    //     of the garbage collector. Use the System.Runtime.InteropServices.GCHandle.Free()
    //     method to free the allocated handle as soon as possible.
    Pinned = 3,
  }

}
#endif