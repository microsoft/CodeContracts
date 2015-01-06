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
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Threading;
using System.Diagnostics.Contracts;

namespace System.Runtime.InteropServices {
  // Summary:
  //     Provides a collection of methods for allocating unmanaged memory, copying
  //     unmanaged memory blocks, and converting managed to unmanaged types, as well
  //     as other miscellaneous methods used when interacting with unmanaged code.
  public static class Marshal {
    // Summary:
    //     Represents the default character size on the system; the default is 2 for
    //     Unicode systems and 1 for ANSI systems. This field is read-only.
    public static readonly int SystemDefaultCharSize;
    //
    // Summary:
    //     Represents the maximum size of a double byte character set (DBCS) size, in
    //     bytes, for the current operating system. This field is read-only.
    public static readonly int SystemMaxDBCSCharSize;

    // Summary:
    //     Increments the reference count on the specified interface.
    //
    // Parameters:
    //   pUnk:
    //     The interface reference count to increment.
    //
    // Returns:
    //     The new value of the reference count on the pUnk parameter.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int AddRef(IntPtr pUnk) {
    //
    // Summary:
    //     Allocates a block of memory of specified size from the COM task memory allocator.
    //
    // Parameters:
    //   cb:
    //     The size of the block of memory to be allocated.
    //
    // Returns:
    //     An integer representing the address of the block of memory allocated. This
    //     memory must be released with System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr).
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory to satisfy the request.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr AllocCoTaskMem(int cb) {
    //
    // Summary:
    //     Allocates memory from the unmanaged memory of the process using GlobalAlloc.
    //
    // Parameters:
    //   cb:
    //     The number of bytes in memory required.
    //
    // Returns:
    //     An System.IntPtr to the newly allocated memory. This memory must be released
    //     using the System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)
    //     method.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory to satisfy the request.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr AllocHGlobal(int cb) {
    //
    // Summary:
    //     Allocates memory from the process's unmanaged memory.
    //
    // Parameters:
    //   cb:
    //     The number of bytes in memory required.
    //
    // Returns:
    //     An System.IntPtr to the newly allocated memory. This memory must be released
    //     using the System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)
    //     method.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory to satisfy the request.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr AllocHGlobal(IntPtr cb) {
    //
    // Summary:
    //     Gets an interface pointer identified by the specified moniker.
    //
    // Parameters:
    //   monikerName:
    //     The moniker corresponding to the desired interface pointer.
    //
    // Returns:
    //     An object containing a reference to the interface pointer identified by the
    //     monikerName parameter. A moniker is a name, and in this case, the moniker
    //     is defined by an interface.
    //
    // Exceptions:
    //   System.Runtime.InteropServices.COMException:
    //     An unrecognized HRESULT was returned by the unmanaged BindToMoniker method.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static object BindToMoniker(string monikerName) {
    //
    // Summary:
    //     Changes the strength of a COM callable wrapper's (CCW) handle on the object
    //     it contains.
    //
    // Parameters:
    //   otp:
    //     The object whose COM callable wrapper (CCW) holds a reference counted handle.
    //     The handle is strong if the reference count on the CCW is greater than zero;
    //     otherwise it is weak.
    //
    //   fIsWeak:
    //     true to change the strength of the handle on the otp parameter to weak, regardless
    //     of its reference count; false to reset the handle strength on otp to be reference
    //     counted.
      return default(object);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void ChangeWrapperHandleStrength(object otp, bool fIsWeak) {
    //
    // Summary:
    //     Copies data from a one-dimensional, managed 8-bit unsigned integer array
    //     to an unmanaged memory pointer.
    //
    // Parameters:
    //   source:
    //     The one-dimensional array to copy from.
    //
    //   destination:
    //     The memory pointer to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     startIndex and length are not valid.
    //
    //   System.ArgumentNullException:
    //     source, startIndex, destination, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(byte[] source, int startIndex, IntPtr destination, int length) {
    //
    // Summary:
    //     Copies data from a one-dimensional, managed character array to an unmanaged
    //     memory pointer.
    //
    // Parameters:
    //   source:
    //     The one-dimensional array to copy from.
    //
    //   destination:
    //     The memory pointer to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     startIndex and length are not valid.
    //
    //   System.ArgumentNullException:
    //     startIndex, destination, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(char[] source, int startIndex, IntPtr destination, int length) {
    //
    // Summary:
    //     Copies data from a one-dimensional, managed double-precision floating-point
    //     number array to an unmanaged memory pointer.
    //
    // Parameters:
    //   source:
    //     The one-dimensional array to copy from.
    //
    //   destination:
    //     The memory pointer to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     startIndex and length are not valid.
    //
    //   System.ArgumentNullException:
    //     source, startIndex, destination, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(double[] source, int startIndex, IntPtr destination, int length) {
    //
    // Summary:
    //     Copies data from a one-dimensional, managed single-precision floating-point
    //     number array to an unmanaged memory pointer.
    //
    // Parameters:
    //   source:
    //     The one-dimensional array to copy from.
    //
    //   destination:
    //     The memory pointer to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     startIndex and length are not valid.
    //
    //   System.ArgumentNullException:
    //     source, startIndex, destination, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(float[] source, int startIndex, IntPtr destination, int length) {
    //
    // Summary:
    //     Copies data from a one-dimensional, managed 32-bit signed integer array to
    //     an unmanaged memory pointer.
    //
    // Parameters:
    //   source:
    //     The one-dimensional array to copy from.
    //
    //   destination:
    //     The memory pointer to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     startIndex or length is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     startIndex and length are not valid.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(int[] source, int startIndex, IntPtr destination, int length) {
    //
    // Summary:
    //     Copies data from an unmanaged memory pointer to a managed 8-bit unsigned
    //     integer array.
    //
    // Parameters:
    //   source:
    //     The memory pointer to copy from.
    //
    //   destination:
    //     The array to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source, destination, startIndex, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, byte[] destination, int startIndex, int length) {
    //
    // Summary:
    //     Copies data from an unmanaged memory pointer to a managed character array.
    //
    // Parameters:
    //   source:
    //     The memory pointer to copy from.
    //
    //   destination:
    //     The array to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source, destination, startIndex, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, char[] destination, int startIndex, int length) {
    //
    // Summary:
    //     Copies data from an unmanaged memory pointer to a managed double-precision
    //     floating-point number array.
    //
    // Parameters:
    //   source:
    //     The memory pointer to copy from.
    //
    //   destination:
    //     The array to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source, destination, startIndex, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, double[] destination, int startIndex, int length) {
    //
    // Summary:
    //     Copies data from an unmanaged memory pointer to a managed single-precision
    //     floating-point number array.
    //
    // Parameters:
    //   source:
    //     The memory pointer to copy from.
    //
    //   destination:
    //     The array to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source, destination, startIndex, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, float[] destination, int startIndex, int length) {
    //
    // Summary:
    //     Copies data from an unmanaged memory pointer to a managed 32-bit signed integer
    //     array.
    //
    // Parameters:
    //   source:
    //     The memory pointer to copy from.
    //
    //   destination:
    //     The array to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source, destination, startIndex, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, int[] destination, int startIndex, int length) {
    //
    // Summary:
    //     Copies data from an unmanaged memory pointer to a managed System.IntPtr array.
    //
    // Parameters:
    //   source:
    //     The memory pointer to copy from.
    //
    //   destination:
    //     The array to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source, destination, startIndex, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length) {
    //
    // Summary:
    //     Copies data from an unmanaged memory pointer to a managed 64-bit signed integer
    //     array.
    //
    // Parameters:
    //   source:
    //     The memory pointer to copy from.
    //
    //   destination:
    //     The array to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source, destination, startIndex, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, long[] destination, int startIndex, int length) {
    //
    // Summary:
    //     Copies data from an unmanaged memory pointer to a managed 16-bit signed integer
    //     array.
    //
    // Parameters:
    //   source:
    //     The memory pointer to copy from.
    //
    //   destination:
    //     The array to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source, destination, startIndex, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, short[] destination, int startIndex, int length) {
    //
    // Summary:
    //     Copies data from a one-dimensional, managed System.IntPtr array to an unmanaged
    //     memory pointer.
    //
    // Parameters:
    //   source:
    //     The one-dimensional array to copy from.
    //
    //   destination:
    //     The memory pointer to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     source, destination, startIndex, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length) {
    //
    // Summary:
    //     Copies data from a one-dimensional, managed 64-bit signed integer array to
    //     an unmanaged memory pointer.
    //
    // Parameters:
    //   source:
    //     The one-dimensional array to copy from.
    //
    //   destination:
    //     The memory pointer to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     startIndex and length are not valid.
    //
    //   System.ArgumentNullException:
    //     source, startIndex, destination, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(long[] source, int startIndex, IntPtr destination, int length) {
    //
    // Summary:
    //     Copies data from a one-dimensional, managed 16-bit signed integer array to
    //     an unmanaged memory pointer.
    //
    // Parameters:
    //   source:
    //     The one-dimensional array to copy from.
    //
    //   destination:
    //     The memory pointer to copy to.
    //
    //   startIndex:
    //     The zero-based index into the array where Copy should start.
    //
    //   length:
    //     The number of array elements to copy.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     startIndex and length are not valid.
    //
    //   System.ArgumentNullException:
    //     source, startIndex, destination, or length is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(short[] source, int startIndex, IntPtr destination, int length) {
    //
    // Summary:
    //     Aggregates a managed object with the specified COM object.
    //
    // Parameters:
    //   o:
    //     An object to aggregate.
    //
    //   pOuter:
    //     The outer IUnknown pointer.
    //
    // Returns:
    //     The inner IUnknown pointer of the managed object.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr CreateAggregatedObject(IntPtr pOuter, object o) {
    //
    // Summary:
    //     Wraps the specified COM object in an object of the specified type.
    //
    // Parameters:
    //   t:
    //     The System.Type of wrapper to create.
    //
    //   o:
    //     The object to be wrapped.
    //
    // Returns:
    //     The newly wrapped object that is an instance of the desired type.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     t must derive from __ComObject.
    //
    //   System.ArgumentNullException:
    //     The t parameter is null.
    //
    //   System.InvalidCastException:
    //     o cannot be converted to the destination type since it does not support all
    //     required interfaces.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static object CreateWrapperOfType(object o, Type! t) {
        CodeContract.Requires(t != null);

    //
    // Summary:
    //     Frees all substructures pointed to by the specified unmanaged memory block.
    //
    // Parameters:
    //   ptr:
    //     A pointer to an unmanaged block of memory.
    //
    //   structuretype:
    //     Type of a formatted class. This provides the layout information necessary
    //     to delete the buffer in the ptr parameter.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     structureType has an automatic layout. Use sequential or explicit instead.
      return default(object);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void DestroyStructure(IntPtr ptr, Type structuretype) {
    //
    // Summary:
    //     Releases all references to a runtime callable wrapper (RCW) by setting the
    //     reference count of the supplied RCW to 0.
    //
    // Parameters:
    //   o:
    //     The RCW to be released.
    //
    // Returns:
    //     The new value of the reference count of the RCW associated with the oparameter,
    //     which is zero if the release is successful.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     o is null.
    //
    //   System.ArgumentException:
    //     o is not a valid COM object.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int FinalReleaseComObject(object o) {
    //
    // Summary:
    //     Frees a BSTR using SysFreeString.
    //
    // Parameters:
    //   ptr:
    //     The address of the BSTR to be freed.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void FreeBSTR(IntPtr ptr) {
    //
    // Summary:
    //     Frees a block of memory allocated by the unmanaged COM task memory allocator
    //     with System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32).
    //
    // Parameters:
    //   ptr:
    //     The address of the memory to be freed.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void FreeCoTaskMem(IntPtr ptr) {
    //
    // Summary:
    //     Frees memory previously allocated from the unmanaged memory of the process
    //     with System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr).
    //
    // Parameters:
    //   hglobal:
    //     The handle returned by the original matching call to System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr).
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void FreeHGlobal(IntPtr hglobal) {
    //
    // Summary:
    //     Returns the globally unique identifier (GUID) for the specified type, or
    //     generates a GUID using the algorithm used by the Type Library Exporter (Tlbexp.exe).
    //
    // Parameters:
    //   type:
    //     The System.Type to generate a GUID for.
    //
    // Returns:
    //     A System.Guid for the specified type.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static Guid GenerateGuidForType(Type type) {
    //
    // Summary:
    //     Returns a programmatic identifier (ProgID) for the specified type.
    //
    // Parameters:
    //   type:
    //     The System.Type to get a ProgID for.
    //
    // Returns:
    //     The ProgID of the specified type.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The type parameter is not a class that can be create by COM. The class must
      return default(Guid);
    }
    //     be public, have a public default constructor, and be COM visible.
    //
    //   System.ArgumentNullException:
    //     The type parameter is null.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string GenerateProgIdForType(Type! type) {
        CodeContract.Requires(type != null);
    //
    // Summary:
    //     Obtains a running instance of the specified object from the Running Object
    //     Table (ROT).
    //
    // Parameters:
    //   progID:
    //     The ProgID of the object being requested.
    //
    // Returns:
    //     The object requested. You can cast this object to any COM interface that
    //     it supports.
      return default(string);
    }
    public static object GetActiveObject(string progID) {
    //
    // Summary:
    //     Returns an interface pointer that represents the specified interface for
    //     an object.
    //
    // Parameters:
    //   T:
    //     The System.Type of interface that is requested.
    //
    //   o:
    //     The object providing the interface.
    //
    // Returns:
    //     The interface pointer representing the interface for the object.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The T parameter is not an interface.-or- The type is not visible to COM.
    //     -or-The T parameter is a generic type.
    //
    //   System.ArgumentNullException:
    //     The o parameter is null-or- The T parameter is null
    //
    //   System.InvalidCastException:
    //     The o parameter does not support the requested interface.
      return default(object);
    }
    public static IntPtr GetComInterfaceForObject(object o, Type T) {
    //
    // Summary:
    //     Returns an interface pointer that represents the specified interface for
    //     an object, if the caller is in the same context as that object.
    //
    // Parameters:
    //   t:
    //     The System.Type of interface that is requested.
    //
    //   o:
    //     The object that provides the interface.
    //
    // Returns:
    //     The interface pointer specified by t that represents the interface for the
    //     specified object, or null if the caller is not in the same context as the
    //     object.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     t is not an interface.-or- The type is not visible to COM.
    //
    //   System.ArgumentNullException:
    //     o is null.-or- t is null.
    //
    //   System.InvalidCastException:
    //     o does not support the requested interface.
      return default(IntPtr);
    }
    public static IntPtr GetComInterfaceForObjectInContext(object o, Type t) {
    //
    // Summary:
    //     Gets data referenced by the specified key from the specified COM object.
    //
    // Parameters:
    //   obj:
    //     The COM object containing the desired data.
    //
    //   key:
    //     The key in the internal hash table of obj to retrieve the data from.
    //
    // Returns:
    //     The data represented by the key parameter in the internal hash table of the
    //     obj parameter.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     obj is not a COM object.
    //
    //   System.ArgumentNullException:
    //     obj is null.-or- key is null.
      return default(IntPtr);
    }
    public static object GetComObjectData (object! obj, object! key) {
        CodeContract.Requires(obj != null);
        CodeContract.Requires(key != null);
    //
    // Summary:
    //     Gets the virtual function table (VTBL) slot for a specified System.Reflection.MemberInfo
    //     when exposed to COM.
    //
    // Parameters:
    //   m:
    //     A System.Reflection.MemberInfo that represents an interface method.
    //
    // Returns:
    //     The VTBL (also called v-table) slot m identifier when it is exposed to COM.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The m parameter is null.
    //
    //   System.ArgumentException:
    //     The m parameter is not a System.Reflection.MethodInfo object.-or-The m parameter
    //     is not an interface method.
      return default(object);
    }
    public static int GetComSlotForMethodInfo(MemberInfo m) {
    //
    // Summary:
    //     Converts an unmanaged function pointer to a delegate.
    //
    // Parameters:
    //   ptr:
    //     An System.IntPtr type that is the unmanaged function pointer to be converted.
    //
    //   t:
    //     The type of the delegate to be returned.
    //
    // Returns:
    //     A delegate instance that can be cast to the appropriate delegate type.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The t parameter is not a delegate.
    //
    //   System.ArgumentNullException:
    //     The ptr parameter is null.-or-The t parameter is null.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t) {
    //
    // Summary:
    //     Gets the last slot in the virtual function table (VTBL) of a type when exposed
    //     to COM.
    //
    // Parameters:
    //   t:
    //     A System.Type representing an interface or class.
    //
    // Returns:
    //     The last VTBL (also called v-table) slot of the interface when exposed to
    //     COM. If the t parameter is a class, the returned VTBL slot is the last slot
    //     in the interface that is generated from the class.
      return default(Delegate);
    }
    public static int GetEndComSlot(Type t) {
    //
    // Summary:
    //     Retrieves a code that identifies the type of the exception that occurred.
    //
    // Returns:
    //     The type of the exception.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int GetExceptionCode() {
    //
    // Summary:
    //     Converts the specified HRESULT error code to a corresponding System.Exception
    //     object.
    //
    // Parameters:
    //   errorCode:
    //     The HRESULT to be converted.
    //
    // Returns:
    //     An System.Exception object representing the converted HRESULT.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static Exception GetExceptionForHR(int errorCode) {
    //
    // Summary:
    //     Converts the specified HRESULT error code to a corresponding System.Exception
    //     object, with additional error information passed in an IErrorInfo interface
    //     for the exception object.
    //
    // Parameters:
    //   errorCode:
    //     The HRESULT to be converted.
    //
    //   errorInfo:
    //     A pointer to the IErrorInfo interface providing more information about the
    //     error.
    //
    // Returns:
    //     An System.Exception object representing the converted HRESULT and information
    //     obtained from errorInfo.
      return default(Exception);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo) {
    //
    // Summary:
    //     Retrieves a computer-independent description of an exception, and information
    //     about the state that existed for the thread when the exception occurred.
    //
    // Returns:
    //     An System.IntPtr to an EXCEPTION_POINTERS structure.
      return default(Exception);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetExceptionPointers() {
    //
    // Summary:
    //     Converts a delegate into a function pointer callable from unmanaged code.
    //
    // Parameters:
    //   d:
    //     The delegate to be passed to unmanaged code.
    //
    // Returns:
    //     An System.IntPtr value that can be passed to unmanaged code, which in turn
    //     can use it to call the underlying managed delegate.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The d parameter is null.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetFunctionPointerForDelegate(Delegate d) {
    //
    // Summary:
    //     Returns the instance handle (HINSTANCE) for the specified module.
    //
    // Parameters:
    //   m:
    //     The System.Reflection.Module whose HINSTANCE is desired.
    //
    // Returns:
    //     The HINSTANCE for m; -1 if the module does not have an HINSTANCE.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The m parameter is null.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetHINSTANCE (Module! m) {
        CodeContract.Requires(m != null);
    //
    // Summary:
    //     Converts the specified exception to an HRESULT.
    //
    // Parameters:
    //   e:
    //     The System.Exception to convert to an HRESULT.
    //
    // Returns:
    //     The HRESULT mapped to the supplied exception.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int GetHRForException(Exception e) {
    //
    // Summary:
    //     Returns the HRESULT corresponding to the last error incurred by Win32 code
    //     executed using System.Runtime.InteropServices.Marshal.
    //
    // Returns:
    //     The HRESULT corresponding to the last Win32 error code.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int GetHRForLastWin32Error() {
    //
    // Summary:
    //     Returns an IDispatch interface from a managed object.
    //
    // Parameters:
    //   o:
    //     The object whose IDispatch interface is requested.
    //
    // Returns:
    //     The IDispatch pointer for the o parameter.
    //
    // Exceptions:
    //   System.InvalidCastException:
    //     o does not support the requested interface.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetIDispatchForObject(object o) {
    //
    // Summary:
    //     Returns an IDispatch interface pointer from a managed object, if the caller
    //     is in the same context as that object.
    //
    // Parameters:
    //   o:
    //     The object whose IDispatch interface is requested.
    //
    // Returns:
    //     The IDispatch interface pointer for the o parameter, or null if the caller
    //     is not in the same context as the specified object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     o is null.
    //
    //   System.InvalidCastException:
    //     o does not support the requested interface.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetIDispatchForObjectInContext(object o) {
    //
    // Summary:
    //     Returns an ITypeInfo interface from a managed type.
    //
    // Parameters:
    //   t:
    //     The System.Type whose ITypeInfo interface is being requested.
    //
    // Returns:
    //     The ITypeInfo pointer for the t parameter.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     t is not a visible type to COM.
    //
    //   System.Runtime.InteropServices.COMException:
    //     A type library is registered for the assembly that contains the type, but
    //     the type definition cannot be found.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetITypeInfoForType(Type t) {
    //
    // Summary:
    //     Returns an IUnknown interface from a managed object.
    //
    // Parameters:
    //   o:
    //     The object whose IUnknown interface is requested.
    //
    // Returns:
    //     The IUnknown pointer for the o parameter.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetIUnknownForObject(object o) {
    //
    // Summary:
    //     Returns an IUnknown interface from a managed object, if the caller is in
    //     the same context as that object.
    //
    // Parameters:
    //   o:
    //     The object whose IUnknown interface is requested.
    //
    // Returns:
    //     The IUnknown pointer for the o parameter, or null if the caller is not in
    //     the same context as the specified object.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetIUnknownForObjectInContext(object o) {
    //
    // Summary:
    //     Returns the error code returned by the last unmanaged function called using
    //     platform invoke that has the System.Runtime.InteropServices.DllImportAttribute.SetLastError
    //     flag set.
    //
    // Returns:
    //     The last error code set by a call to the Win32 SetLastError API method.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int GetLastWin32Error() {
    //
    // Summary:
    //     Gets a pointer to a thunk that marshals a call from managed to unmanaged
    //     code.
    //
    // Parameters:
    //   pfnMethodToWrap:
    //     A pointer to the method to marshal.
    //
    //   pbSignature:
    //     A pointer to the method signature.
    //
    //   cbSignature:
    //     The number of bytes in pbSignature.
    //
    // Returns:
    //     A pointer to the thunk that will marshal a call from the pfnMethodToWrap
    //     parameter.
      return default(int);
    }
    [Obsolete("The GetManagedThunkForUnmanagedMethodPtr method has been deprecated and will be removed in a future release.", false)]
    public static IntPtr GetManagedThunkForUnmanagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature) {
    //
    // Summary:
    //     Retrieves System.Reflection.MethodInfo for the specified virtual function
    //     table (VTBL) slot.
    //
    // Parameters:
    //   slot:
    //     The VTBL slot.
    //
    //   t:
    //     The type for which the MethodInfo is to be retrieved.
    //
    //   memberType:
    //     On successful return, the type of the member. This is one of the System.Runtime.InteropServices.ComMemberType
    //     enumeration members.
    //
    // Returns:
    //     The MemberInfo that represents the member at the specified VTBL (also called
    //     v-table) slot.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     t is not visible from COM.
      return default(IntPtr);
    }
    public static MemberInfo GetMethodInfoForComSlot(Type t, int slot, ref ComMemberType memberType) {
    //
    // Summary:
    //     Converts an object to a COM VARIANT.
    //
    // Parameters:
    //   obj:
    //     The object for which to get a COM VARIANT.
    //
    //   pDstNativeVariant:
    //     An System.IntPtr to receive the VARIANT corresponding to the obj parameter.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The obj parameter is a generic type.
      return default(MemberInfo);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant) {
    //
    // Summary:
    //     Returns an instance of a type that represents a COM object by a pointer to
    //     its IUnknown interface.
    //
    // Parameters:
    //   pUnk:
    //     A pointer to the IUnknown interface.
    //
    // Returns:
    //     An object representing the specified unmanaged COM object.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static object GetObjectForIUnknown(IntPtr pUnk) {
    //
    // Summary:
    //     Converts a COM VARIANT to an object.
    //
    // Parameters:
    //   pSrcNativeVariant:
    //     An System.IntPtr containing a COM VARIANT.
    //
    // Returns:
    //     An object corresponding to the pSrcNativeVariant parameter.
    //
    // Exceptions:
    //   System.Runtime.InteropServices.InvalidOleVariantTypeException:
    //     pSrcNativeVariant is not a valid VARIANT type.
    //
    //   System.NotSupportedException:
    //     pSrcNativeVariant has an unsupported type.
      return default(object);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static object GetObjectForNativeVariant(IntPtr pSrcNativeVariant) {
    //
    // Summary:
    //     Converts an array of COM VARIANTs to an array of objects.
    //
    // Parameters:
    //   cVars:
    //     The count of COM VARIANTs in aSrcNativeVariant.
    //
    //   aSrcNativeVariant:
    //     An System.IntPtr containing the first element of an array of COM VARIANTs.
    //
    // Returns:
    //     An object array corresponding to aSrcNativeVariant.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     cVars cannot be a negative number.
      return default(object);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars) {
    //
    // Summary:
    //     Gets the first slot in the virtual function table (VTBL) that contains user
    //     defined methods.
    //
    // Parameters:
    //   t:
    //     A System.Type representing an interface.
    //
    // Returns:
    //     The first VTBL (also called v-table) slot that contains user defined methods.
    //     The first slot is 3 if the interface is IUnknown based, and 7 if the interface
    //     is IDispatch based.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     t is not visible from COM.
      return default(object[]);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int GetStartComSlot(Type t) {
    //
    // Summary:
    //     Converts a fiber cookie into the corresponding System.Threading.Thread instance.
    //
    // Parameters:
    //   cookie:
    //     An integer representing a fiber cookie.
    //
    // Returns:
    //     A System.Threading.Thread corresponding to the cookie parameter.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The cookie parameter is 0.
      return default(int);
    }
    [Obsolete("The GetThreadFromFiberCookie method has been deprecated.  Use the hosting API to perform this operation.", false)]
    public static Thread GetThreadFromFiberCookie(int cookie) {
    //
    // Summary:
    //     Returns a managed object of a specified type that represents a COM object.
    //
    // Parameters:
    //   pUnk:
    //     A pointer to the IUnknown interface of the unmanaged object.
    //
    //   t:
    //     The System.Type of the requested managed class.
    //
    // Returns:
    //     An instance of the class corresponding to the System.Type object that represents
    //     the requested unmanaged COM object.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     t is not attributed with System.Runtime.InteropServices.ComImportAttribute.
      return default(Thread);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static object GetTypedObjectForIUnknown(IntPtr pUnk, Type t) {
    //
    // Summary:
    //     Converts an ITypeInfo into a managed System.Type object.
    //
    // Parameters:
    //   piTypeInfo:
    //     The ITypeInfo interface to marshal.
    //
    // Returns:
    //     A managed System.Type that represents the unmanaged ITypeInfo.
      return default(object);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static Type GetTypeForITypeInfo(IntPtr piTypeInfo) {
    //
    // Summary:
    //     Retrieves the name of the type represented by an ITypeInfo.
    //
    // Parameters:
    //   typeInfo:
    //     A System.Runtime.InteropServices.ComTypes.ITypeInfo object that represents
    //     an ITypeInfo pointer.
    //
    // Returns:
    //     The name of the type pointed to by the typeInfo parameter.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The typeInfo parameter is null.
      return default(Type);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string GetTypeInfoName(ITypeInfo! typeInfo) {
       CodeContract.Requires(typeInfo != null);
    //
    // Summary:
    //     Retrieves the name of the type represented by an ITypeInfo.
    //
    // Parameters:
    //   pTI:
    //     A System.Runtime.InteropServices.UCOMITypeInfo that represents an ITypeInfo
    //     pointer.
    //
    // Returns:
    //     The name of the type pointed to by the pTI parameter.
      return default(string);
    }
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeInfoName(ITypeInfo pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static string GetTypeInfoName(UCOMITypeInfo pTI) {
    //
    // Summary:
    //     Retrieves the library identifier (LIBID) of a type library.
    //
    // Parameters:
    //   typelib:
    //     An System.Runtime.InteropServices.ComTypes.ITypeLib object that represents
    //     an ITypeLib pointer.
    //
    // Returns:
    //     The LIBID (that is, the System.Guid) of the type library pointed to by the
    //     typelib parameter.
      return default(string);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static Guid GetTypeLibGuid(ITypeLib! typelib) {
      CodeContract.Requires(typelib != null);
    //
    // Summary:
    //     Retrieves the library identifier (LIBID) of a type library.
    //
    // Parameters:
    //   pTLB:
    //     A System.Runtime.InteropServices.UCOMITypeLib that represents an ITypeLib
    //     pointer.
    //
    // Returns:
    //     The LIBID (that is, the System.Guid) of the type library pointed to by the
    //     pTLB parameter.
      return default(Guid);
    }
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibGuid(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static Guid GetTypeLibGuid(UCOMITypeLib pTLB) {
    //
    // Summary:
    //     Retrieves the library identifier (LIBID) that is assigned to a type library
    //     when it was exported from the specified assembly.
    //
    // Parameters:
    //   asm:
    //     A managed System.Reflection.Assembly.
    //
    // Returns:
    //     The LIBID (that is, the System.Guid) that is assigned to a type library when
    //     it is exported from the asm parameter.
      return default(Guid);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static Guid GetTypeLibGuidForAssembly(Assembly asm) {
    //
    // Summary:
    //     Retrieves the LCID of a type library.
    //
    // Parameters:
    //   typelib:
    //     A System.Runtime.InteropServices.ComTypes.ITypeLib object that represents
    //     an ITypeLib pointer.
    //
    // Returns:
    //     The LCID of the type library pointed to by the typelib parameter.
      return default(Guid);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int GetTypeLibLcid(ITypeLib! typelib) {
       CodeContract.Requires(typelib != null);
    //
    // Summary:
    //     Retrieves the LCID of a type library.
    //
    // Parameters:
    //   pTLB:
    //     A System.Runtime.InteropServices.UCOMITypeLib that represents an ITypeLib
    //     pointer.
    //
    // Returns:
    //     The LCID of the type library pointed to by the pTLB parameter.
      return default(int);
    }
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibLcid(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static int GetTypeLibLcid(UCOMITypeLib pTLB) {
    //
    // Summary:
    //     Retrieves the name of a type library.
    //
    // Parameters:
    //   typelib:
    //     An System.Runtime.InteropServices.ComTypes.ITypeLib object that represents
    //     an ITypeLib pointer.
    //
    // Returns:
    //     The name of the type library pointed to by the typelib parameter.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The typelib parameter is null.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string GetTypeLibName(ITypeLib! typelib) {
       CodeContract.Requires(typelib != null);
    //
    // Summary:
    //     Retrieves the name of a type library.
    //
    // Parameters:
    //   pTLB:
    //     A System.Runtime.InteropServices.UCOMITypeLib that represents an ITypeLib
    //     pointer.
    //
    // Returns:
    //     The name of the type library pointed to by the pTLB parameter.
      return default(string);
    }
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibName(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static string GetTypeLibName(UCOMITypeLib pTLB) {
    //
    // Summary:
    //     Retrieves the version number of a type library that will be exported from
    //     the specified assembly.
    //
    // Parameters:
    //   majorVersion:
    //     The major version number.
    //
    //   minorVersion:
    //     The minor version number.
    //
    //   inputAssembly:
    //     A managed System.Reflection.Assembly object.
      return default(string);
    }
    public static void GetTypeLibVersionForAssembly(Assembly inputAssembly, out int majorVersion, out int minorVersion) {
    //
    // Summary:
    //     Creates a unique runtime callable wrapper (RCW) object for a given IUnknown.
    //
    // Parameters:
    //   unknown:
    //     A managed pointer to an IUnknown.
    //
    // Returns:
    //     A unique runtime callable wrapper (RCW) for a given IUnknown.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static object GetUniqueObjectForIUnknown(IntPtr unknown) {
    //
    // Summary:
    //     Gets a pointer to a thunk that marshals a call from unmanaged to managed
    //     code.
    //
    // Parameters:
    //   pfnMethodToWrap:
    //     A pointer to the method to marshal.
    //
    //   pbSignature:
    //     A pointer to the method signature.
    //
    //   cbSignature:
    //     The number of bytes in pbSignature.
    //
    // Returns:
    //     A pointer to the thunk that will marshal a call from pfnMethodToWrap.
      return default(object);
    }
    [Obsolete("The GetUnmanagedThunkForManagedMethodPtr method has been deprecated and will be removed in a future release.", false)]
    public static IntPtr GetUnmanagedThunkForManagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature) {
    //
    // Summary:
    //     Indicates whether a specified object represents a COM object.
    //
    // Parameters:
    //   o:
    //     The object to check.
    //
    // Returns:
    //     true if the o parameter is a COM type; otherwise, false.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static bool IsComObject(object o) {
    //
    // Summary:
    //     Indicates whether a type is visible to COM clients.
    //
    // Parameters:
    //   t:
    //     The System.Type to check for COM visibility.
    //
    // Returns:
    //     true if the type is visible to COM; otherwise, false.
      return default(bool);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static bool IsTypeVisibleFromCom(Type t) {
    //
    // Summary:
    //     Calculates the number of bytes required to hold the parameters for the specified
    //     method.
    //
    // Parameters:
    //   m:
    //     A System.Reflection.MethodInfo that identifies the method to be checked.
    //
    // Returns:
    //     The number of bytes required.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The m parameter is not a System.Reflection.MethodInfo object.
    //
    //   System.ArgumentNullException:
    //     The m parameter is null.
      return default(bool);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int NumParamBytes(MethodInfo m) {
    //
    // Summary:
    //     Returns the field offset of the unmanaged form of the managed class.
    //
    // Parameters:
    //   fieldName:
    //     The field within the t parameter.
    //
    //   t:
    //     A System.Type, specifying the specified class. You must apply the System.Runtime.InteropServices.StructLayoutAttribute
    //     to the class.
    //
    // Returns:
    //     The offset, in bytes, for the fieldName parameter within the platform invoke
    //     declared class t.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The t parameter is null.
    //
    //   System.ArgumentException:
      return default(int);
    }
    //     The class cannot be exported as a structure or the field is nonpublic. Beginning
    //     with the .NET Framework version 2.0, the field may be private.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr OffsetOf(Type! t, string fieldName) {
        CodeContract.Requires(t != null);
    //
    // Summary:
    //     Executes one-time method setup tasks without calling the method.
    //
    // Parameters:
    //   m:
    //     A System.Reflection.MethodInfo that identifies the method to be checked.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The m parameter is null.
    //
    //   System.ArgumentException:
    //     The m parameter is not a System.Reflection.MethodInfo object.
      return default(IntPtr);
    }
    public static void Prelink(MethodInfo m) {
    //
    // Summary:
    //     Performs a pre-link check for all methods on a class.
    //
    // Parameters:
    //   c:
    //     A System.Type that identifies the class whose methods are to be checked.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The c parameter is null.
    }
    public static void PrelinkAll(Type! c) {
        CodeContract.Requires(c != null);
    //
    // Summary:
    //     Copies all characters up to the first null from an unmanaged ANSI string
    //     to a managed System.String. Widens each ANSI character to Unicode.
    //
    // Parameters:
    //   ptr:
    //     The address of the first character of the unmanaged string.
    //
    // Returns:
    //     A managed System.String object that holds a copy of the unmanaged ANSI string.
    //     If ptr is null, the method returns a null string.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string PtrToStringAnsi(IntPtr ptr) {
    //
    // Summary:
    //     Allocates a managed System.String, copies a specified number of characters
    //     from an unmanaged ANSI string into it, and widens each ANSI character to
    //     Unicode.
    //
    // Parameters:
    //   ptr:
    //     The address of the first character of the unmanaged string.
    //
    //   len:
    //     The byte count of the input string to copy.
    //
    // Returns:
    //     A managed System.String that holds a copy of the native ANSI string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     ptr is null.
    //
    //   System.ArgumentException:
    //     len is less than zero.
      return default(string);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string PtrToStringAnsi(IntPtr ptr, int len) {
    //
    // Summary:
    //     Allocates a managed System.String and copies all characters up to the first
    //     null character from a string stored in unmanaged memory into it.
    //
    // Parameters:
    //   ptr:
    //     For Unicode platforms, the address of the first Unicode character.-or- For
    //     ANSI plaforms, the address of the first ANSI character.
    //
    // Returns:
    //     A managed string that holds a copy of the unmanaged string.
      return default(string);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string PtrToStringAuto(IntPtr ptr) {
    //
    // Summary:
    //     Copies a specified number of characters from a string stored in unmanaged
    //     memory to a managed System.String.
    //
    // Parameters:
    //   ptr:
    //     For Unicode platforms, the address of the first Unicode character.-or- For
    //     ANSI plaforms, the address of the first ANSI character.
    //
    //   len:
    //     The number of characters to copy.
    //
    // Returns:
    //     A managed string that holds a copy of the native string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     ptr is null.
    //
    //   System.ArgumentException:
    //     len is less than zero.
      return default(string);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string PtrToStringAuto(IntPtr ptr, int len) {
    //
    // Summary:
    //     Allocates a managed System.String and copies a BSTR string stored in unmanaged
    //     memory into it.
    //
    // Parameters:
    //   ptr:
    //     The address of the first character of the unmanaged string.
    //
    // Returns:
    //     A managed string that holds a copy of the native string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     ptr is null.
      return default(string);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string PtrToStringBSTR(IntPtr ptr) {
    //
    // Summary:
    //     Allocates a managed System.String and copies all characters up to the first
    //     null character from an unmanaged Unicode string into it.
    //
    // Parameters:
    //   ptr:
    //     The address of the first character of the unmanaged string.
    //
    // Returns:
    //     A managed string holding a copy of the native string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     ptr is null.
      return default(string);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string PtrToStringUni(IntPtr ptr) {
    //
    // Summary:
    //     Copies a specified number of characters from a Unicode string stored in native
    //     heap to a managed System.String.
    //
    // Parameters:
    //   ptr:
    //     The address of the first character of the unmanaged string.
    //
    //   len:
    //     The number of Unicode characters to copy.
    //
    // Returns:
    //     A managed string that holds a copy of the native string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     ptr is null.
    //
    //   System.ArgumentException:
    //     len is less than zero.
      return default(string);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string PtrToStringUni(IntPtr ptr, int len) {
    //
    // Summary:
    //     Marshals data from an unmanaged block of memory to a managed object.
    //
    // Parameters:
    //   ptr:
    //     A pointer to an unmanaged block of memory.
    //
    //   structure:
    //     The object to which the data is to be copied. This must be an instance of
    //     a formatted class.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     Structure layout is not sequential or explicit.-or- Structure is a boxed
    //     value type.
      return default(string);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void PtrToStructure(IntPtr ptr, object structure) {
    //
    // Summary:
    //     Marshals data from an unmanaged block of memory to a newly allocated managed
    //     object of the specified type.
    //
    // Parameters:
    //   ptr:
    //     A pointer to an unmanaged block of memory.
    //
    //   structureType:
    //     The System.Type of object to be created. This type object must represent
    //     a formatted class or a structure.
    //
    // Returns:
    //     A managed object containing the data pointed to by the ptr parameter.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The structureType parameter layout is not sequential or explicit. -or-The
    //     structureType parameter is a generic type.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static object PtrToStructure(IntPtr ptr, Type structureType) {
    //
    // Summary:
    //     Requests a pointer to a specified interface from a COM object.
    //
    // Parameters:
    //   pUnk:
    //     The interface to be queried.
    //
    //   iid:
    //     A System.Guid, passed by reference, that is the interface identifier (IID)
    //     of the requested interface.
    //
    //   ppv:
    //     When this method returns, contains a reference to the returned interface.
    //
    // Returns:
    //     An HRESULT that indicates the success or failure of the call.
      return default(object);
    }
    public static int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv) {
    //
    // Summary:
    //     Reads a single byte from an unmanaged pointer.
    //
    // Parameters:
    //   ptr:
    //     The address in unmanaged memory from which to read.
    //
    // Returns:
    //     The byte read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static byte ReadByte(IntPtr ptr) {
    //
    // Summary:
    //     Reads a single byte at a given offset (or index) from an unmanaged pointer.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory from which to read.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before reading.
    //
    // Returns:
    //     The byte read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null.-or-ptr is invalid.
      return default(byte);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public extern static byte ReadByte(IntPtr ptr, int ofs);
    //
    // Summary:
    //     Reads a single byte from an unmanaged pointer.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory of the source object.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before reading.
    //
    // Returns:
    //     The byte read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null.-or-ptr is invalid.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public extern static byte ReadByte(object ptr, int ofs);
    //
    // Summary:
    //     Reads a 16-bit signed integer from the unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The address in unmanaged memory from which to read.
    //
    // Returns:
    //     The 16-bit signed integer read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null.-or-ptr is invalid.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static short ReadInt16(IntPtr ptr) {
    //
    // Summary:
    //     Reads a 16-bit signed integer from unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory from which to read.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before reading.
    //
    // Returns:
    //     The 16-bit signed integer read from ptr.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null.-or-ptr is invalid.
      return default(short);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public extern static short ReadInt16(IntPtr ptr, int ofs);
    //
    // Summary:
    //     Reads a 16-bit signed integer from unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory of the source object.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before reading.
    //
    // Returns:
    //     The 16-bit signed integer read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public extern static short ReadInt16(object ptr, int ofs);
    //
    // Summary:
    //     Reads a 32-bit signed integer from unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The address in unmanaged from which to read.
    //
    // Returns:
    //     The 32-bit signed integer read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int ReadInt32(IntPtr ptr) {
    //
    // Summary:
    //     Reads a 32-bit signed integer from unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory from which to read.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before reading.
    //
    // Returns:
    //     The 32-bit signed integer read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public extern static int ReadInt32(IntPtr ptr, int ofs);
    //
    // Summary:
    //     Reads a 32-bit signed integer from unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory of the source object.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before reading.
    //
    // Returns:
    //     The 32-bit signed integer read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public extern static int ReadInt32(object ptr, int ofs);
    //
    // Summary:
    //     Reads a 64-bit signed integer from unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The address in unmanaged memory from which to read.
    //
    // Returns:
    //     The 64-bit signed integer read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static long ReadInt64(IntPtr ptr) {
    //
    // Summary:
    //     Reads a 64-bit signed integer from unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory from which to read.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before reading.
    //
    // Returns:
    //     The 64-bit signed integer read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null.-or-ptr is invalid.
      return default(long);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public extern static long ReadInt64(IntPtr ptr, int ofs);
    //
    // Summary:
    //     Reads a 64-bit signed integer from unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory of the source object.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before reading.
    //
    // Returns:
    //     The 64-bit signed integer read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public extern static long ReadInt64(object ptr, int ofs);
    //
    // Summary:
    //     Reads a processor native sized integer from unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The address in unmanaged memory from which to read.
    //
    // Returns:
    //     The IntPtr read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr ReadIntPtr(IntPtr ptr) {
    //
    // Summary:
    //     Reads a processor native sized integer from unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory from which to read.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before reading.
    //
    // Returns:
    //     The IntPtr read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr ReadIntPtr(IntPtr ptr, int ofs) {
    //
    // Summary:
    //     Reads a processor native sized integer from unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory of the source object.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before reading.
    //
    // Returns:
    //     The IntPtr read from the ptr parameter.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null.-or-ptr is invalid.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr ReadIntPtr(object ptr, int ofs) {
    //
    // Summary:
    //     Resizes a block of memory previously allocated with System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32).
    //
    // Parameters:
    //   pv:
    //     A pointer to memory allocated with System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32).
    //
    //   cb:
    //     The new size of the allocated block.
    //
    // Returns:
    //     An integer representing the address of the block of memory reallocated. This
    //     memory must be released with System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr).
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory to satisfy the request.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr ReAllocCoTaskMem(IntPtr pv, int cb) {
    //
    // Summary:
    //     Resizes a block of memory previously allocated with System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr).
    //
    // Parameters:
    //   pv:
    //     A pointer to memory allocated with System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr).
    //
    //   cb:
    //     The new size of the allocated block.
    //
    // Returns:
    //     An System.IntPtr to the reallocated memory. This memory must be released
    //     using System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr).
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory to satisfy the request.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb) {
    //
    // Summary:
    //     Decrements the reference count on the specified interface.
    //
    // Parameters:
    //   pUnk:
    //     The interface to release.
    //
    // Returns:
    //     The new value of the reference count on the interface specified by the pUnk
    //     parameter.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int Release(IntPtr pUnk) {
    //
    // Summary:
    //     Decrements the reference count of the supplied runtime callable wrapper.
    //
    // Parameters:
    //   o:
    //     The COM object to release.
    //
    // Returns:
    //     The new value of the reference count of the runtime callable wrapper associated
    //     with o. This value is typically zero since the runtime callable wrapper keeps
    //     just one reference to the wrapped COM object regardless of the number of
    //     managed clients calling it.
    //
    // Exceptions:
    //   System.Runtime.InteropServices.InvalidComObjectException:
    //     o is not a valid COM object.-or-o is null.
    //
    //   System.Runtime.InteropServices.ComObjectInUseException:
    //     The reference count of the RCW is not zero.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int ReleaseComObject(object o) {
    //
    // Summary:
    //     Releases the thread cache.
      return default(int);
    }
    [Obsolete("This API did not perform any operation and will be removed in future versions of the CLR.", false)]
    public static void ReleaseThreadCache() {
    //
    // Summary:
    //     Allocates a BSTR and copies the contents of a managed System.Security.SecureString
    //     object into it.
    //
    // Parameters:
    //   s:
    //     The managed System.Security.SecureString object to be copied.
    //
    // Returns:
    //     The address, in unmanaged memory, where the s parameter was copied to, or
    //     0 if a null System.Security.SecureString object was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
    //
    //   System.NotSupportedException:
    //     The current computer is not running Microsoft Windows 2000 Service Pack 3
    //     or later.
    //
    //   System.ArgumentNullException:
    //     The s parameter is null.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr SecureStringToBSTR(SecureString s) {
    //
    // Summary:
    //     Copies the contents of a managed System.Security.SecureString object to a
    //     block of memory allocated from the unmanaged COM task allocator.
    //
    // Parameters:
    //   s:
    //     The managed System.Security.SecureString object to copy.
    //
    // Returns:
    //     The address, in unmanaged memory, where the s parameter was copied to, or
    //     0 if a null System.Security.SecureString object was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
    //
    //   System.NotSupportedException:
    //     The current computer is not running Microsoft Windows 2000 Service Pack 3
    //     or later.
    //
    //   System.ArgumentNullException:
    //     The s parameter is null.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s) {
    //
    // Summary:
    //     Copies the contents of a managed System.Security.SecureString object to a
    //     block of memory allocated from the unmanaged COM task allocator.
    //
    // Parameters:
    //   s:
    //     The managed System.Security.SecureString object to copy.
    //
    // Returns:
    //     The address, in unmanaged memory, where the s parameter was copied to, or
    //     0 if a null System.Security.SecureString object was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
    //
    //   System.NotSupportedException:
    //     The current computer is not running Microsoft Windows 2000 Service Pack 3
    //     or later.
    //
    //   System.ArgumentNullException:
    //     The s parameter is null.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s) {
    //
    // Summary:
    //     Copies the contents of a managed System.Security.SecureString into unmanaged
    //     memory, converting into ANSI format as it copies.
    //
    // Parameters:
    //   s:
    //     The managed System.Security.SecureString to be copied.
    //
    // Returns:
    //     The address, in unmanaged memory, to where the s parameter was copied, or
    //     0 if a null System.Security.SecureString was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
    //
    //   System.NotSupportedException:
    //     The current computer is not running Microsoft Windows 2000 Service Pack 3
    //     or later.
    //
    //   System.ArgumentNullException:
    //     The s parameter is null.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s) {
    //
    // Summary:
    //     Copies the contents of a managed System.Security.SecureString into unmanaged
    //     memory.
    //
    // Parameters:
    //   s:
    //     The managed System.Security.SecureString to be copied.
    //
    // Returns:
    //     The address, in unmanaged memory, to where the s parameter was copied, or
    //     0 if a null System.Security.SecureString was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
    //
    //   System.NotSupportedException:
    //     The current computer is not running Microsoft Windows 2000 Service Pack 3
    //     or later.
    //
    //   System.ArgumentNullException:
    //     The s parameter is null.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s) {
    //
    // Summary:
    //     Sets data referenced by the specified key in the specified COM object.
    //
    // Parameters:
    //   obj:
    //     The COM object in which to store the data.
    //
    //   data:
    //     The data to set.
    //
    //   key:
    //     The key in the internal hash table of the COM object in which to store the
    //     data.
    //
    // Returns:
    //     true if the data was set successfully; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     obj is not a COM object.
    //
    //   System.ArgumentNullException:
    //     obj is null.-or- key is null.
      return default(IntPtr);
    }
    public static bool SetComObjectData(object! obj, object! key, object data) {
        CodeContract.Requires(obj != null);
        CodeContract.Requires(key != null);

    //
    // Summary:
    //     Returns the unmanaged size of an object in bytes.
    //
    // Parameters:
    //   structure:
    //     The object whose size is to be returned.
    //
    // Returns:
    //     The size of the structure parameter in unmanaged code.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The structure parameter is null.
      return default(bool);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int SizeOf(object structure) {
    //
    // Summary:
    //     Returns the size of an unmanaged type in bytes.
    //
    // Parameters:
    //   t:
    //     The System.Type whose size is to be returned.
    //
    // Returns:
    //     The size of the structure parameter in unmanaged code.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The t parameter is null.
    //
    //   System.ArgumentException:
    //     The t parameter is a generic type.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int SizeOf(Type t) {
    //
    // Summary:
    //     Allocates a BSTR and copies the contents of a managed System.String into
    //     it.
    //
    // Parameters:
    //   s:
    //     The managed string to be copied.
    //
    // Returns:
    //     An unmanaged pointer to the BSTR, or 0 if a null string was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr StringToBSTR(string s) {
    //
    // Summary:
    //     Copies the contents of a managed System.String to a block of memory allocated
    //     from the unmanaged COM task allocator.
    //
    // Parameters:
    //   s:
    //     A managed string to be copied.
    //
    // Returns:
    //     An integer representing a pointer to the block of memory allocated for the
    //     string, or 0 if a null string was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
    //
    //   System.ArgumentOutOfRangeException:
    //     The s parameter exceeds the maximum length allowed by the operating system.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr StringToCoTaskMemAnsi(string s) {
    //
    // Summary:
    //     Copies the contents of a managed System.String to a block of memory allocated
    //     from the unmanaged COM task allocator.
    //
    // Parameters:
    //   s:
    //     A managed string to be copied.
    //
    // Returns:
    //     The allocated memory block, or 0 if a null string was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr StringToCoTaskMemAuto(string s) {
    //
    // Summary:
    //     Copies the contents of a managed System.String to a block of memory allocated
    //     from the unmanaged COM task allocator.
    //
    // Parameters:
    //   s:
    //     A managed string to be copied.
    //
    // Returns:
    //     An integer representing a pointer to the block of memory allocated for the
    //     string, or 0 if a null string was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
    //
    //   System.ArgumentOutOfRangeException:
    //     The s parameter exceeds the maximum length allowed by the operating system.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr StringToCoTaskMemUni(string s) {
    //
    // Summary:
    //     Copies the contents of a managed System.String into unmanaged memory, converting
    //     into ANSI format as it copies.
    //
    // Parameters:
    //   s:
    //     A managed string to be copied.
    //
    // Returns:
    //     The address, in unmanaged memory, to where s was copied, or 0 if a null string
    //     was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
    //
    //   System.ArgumentOutOfRangeException:
    //     The s parameter exceeds the maximum length allowed by the operating system.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr StringToHGlobalAnsi(string s) {
    //
    // Summary:
    //     Copies the contents of a managed System.String into unmanaged memory, converting
    //     into ANSI format if required.
    //
    // Parameters:
    //   s:
    //     A managed string to be copied.
    //
    // Returns:
    //     The address, in unmanaged memory, to where the string was copied, or 0 if
    //     a null string was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr StringToHGlobalAuto(string s) {
    //
    // Summary:
    //     Copies the contents of a managed System.String into unmanaged memory.
    //
    // Parameters:
    //   s:
    //     A managed string to be copied.
    //
    // Returns:
    //     The address, in unmanaged memory, to where the s was copied, or 0 if a null
    //     string was supplied.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     The method could not allocate enough native heap memory.
    //
    //   System.ArgumentOutOfRangeException:
    //     The s parameter exceeds the maximum length allowed by the operating system.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr StringToHGlobalUni(string s) {
    //
    // Summary:
    //     Marshals data from a managed object to an unmanaged block of memory.
    //
    // Parameters:
    //   ptr:
    //     A pointer to an unmanaged block of memory, which must be allocated before
    //     this method is called.
    //
    //   structure:
    //     A managed object holding the data to be marshaled. This object must be an
    //     instance of a formatted class.
    //
    //   fDeleteOld:
    //     true to have the System.Runtime.InteropServices.Marshal.DestroyStructure(System.IntPtr,System.Type)
    //     method called on the ptr parameter before this method executes. Note that
    //     passing false can lead to a memory leak.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The structure parameter is a generic type.
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld) {
    //
    // Summary:
    //     Throws an exception with a specific failure HRESULT value.
    //
    // Parameters:
    //   errorCode:
    //     The HRESULT corresponding to the desired exception.
    }
    public static void ThrowExceptionForHR(int errorCode) {
    //
    // Summary:
    //     Throws an exception with a specific failure HRESULT.
    //
    // Parameters:
    //   errorCode:
    //     The HRESULT corresponding to the desired exception.
    //
    //   errorInfo:
    //     A pointer to the IErrorInfo interface provided by the COM object.
    }
    public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo) {
    //
    // Summary:
    //     Gets the address of the element at the specified index inside the specified
    //     array.
    //
    // Parameters:
    //   arr:
    //     The System.Array containing the desired element.
    //
    //   index:
    //     The index in the arr parameter of the desired element.
    //
    // Returns:
    //     The address of index inside arr.
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index) {
    //
    // Summary:
    //     Writes a single byte value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The address in unmanaged memory from which to write.
    //
    //   val:
    //     The value to write.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null.-or-ptr is invalid.
      return default(IntPtr);
    }
    public static void WriteByte(IntPtr ptr, byte val) {
    //
    // Summary:
    //     Writes a single byte value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory from which to write.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null.-or-ptr is invalid.
    }
    public extern static void WriteByte(IntPtr ptr, int ofs, byte val);
    //
    // Summary:
    //     Writes a single byte value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory of the target object.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    public extern static void WriteByte(object ptr, int ofs, byte val);
    //
    // Summary:
    //     Writes a 16-bit signed integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The address in unmanaged memory from which to write.
    //
    //   val:
    //     The value to write.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null.-or-ptr is invalid.
    public static void WriteInt16(IntPtr ptr, char val) {
    //
    // Summary:
    //     Writes a 16-bit integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The address in unmanaged memory from which to write.
    //
    //   val:
    //     The value to write.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    }
    public static void WriteInt16(IntPtr ptr, short val) {
    //
    // Summary:
    //     Writes a 16-bit signed integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in the native heap from which to write.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    }
    public static void WriteInt16(IntPtr ptr, int ofs, char val) {
    //
    // Summary:
    //     Writes a 16-bit signed integer value into unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory from which to write.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    }
    public extern static void WriteInt16(IntPtr ptr, int ofs, short val);
    //
    // Summary:
    //     Writes a 16-bit signed integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory of the target object.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    public static void WriteInt16(object ptr, int ofs, char val) {
    //
    // Summary:
    //     Writes a 16-bit signed integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory of the target object.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    }
    public extern static void WriteInt16(object ptr, int ofs, short val);
    //
    // Summary:
    //     Writes a 32-bit signed integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The address in unmanaged memory from which to write.
    //
    //   val:
    //     The value to write.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    public static void WriteInt32(IntPtr ptr, int val) {
    //
    // Summary:
    //     Writes a 32-bit signed integer value into unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory from which to write.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    }
    public extern static void WriteInt32(IntPtr ptr, int ofs, int val);
    //
    // Summary:
    //     Writes a 32-bit signed integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory of the target object.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    public extern static void WriteInt32(object ptr, int ofs, int val);
    //
    // Summary:
    //     Writes a 64-bit signed integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The address in unmanaged memory from which to write.
    //
    //   val:
    //     The value to write.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    public static void WriteInt64(IntPtr ptr, long val) {
    //
    // Summary:
    //     Writes a 64-bit signed integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory from which to write.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    }
    public extern static void WriteInt64(IntPtr ptr, int ofs, long val);
    //
    // Summary:
    //     Writes a 64-bit signed integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory of the target object.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    public extern static void WriteInt64(object ptr, int ofs, long val);
    //
    // Summary:
    //     Writes a processor native sized integer value into unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The address in unmanaged memory from which to write.
    //
    //   val:
    //     The value to write.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    public static void WriteIntPtr(IntPtr ptr, IntPtr val) {
    //
    // Summary:
    //     Writes a processor native sized integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory from which to write.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null.-or-ptr is invalid.
    }
    public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val) {
    //
    // Summary:
    //     Writes a processor native sized integer value to unmanaged memory.
    //
    // Parameters:
    //   ptr:
    //     The base address in unmanaged memory of the target object.
    //
    //   val:
    //     The value to write.
    //
    //   ofs:
    //     An additional byte offset, added to the ptr parameter before writing.
    //
    // Exceptions:
    //   System.AccessViolationException:
    //     ptr is not a recognized format. -or-ptr is null. -or-ptr is invalid.
    }
    public static void WriteIntPtr(object ptr, int ofs, IntPtr val) {
    //
    // Summary:
    //     Frees a BSTR pointer that was allocated using the System.Runtime.InteropServices.Marshal.SecureStringToBSTR(System.Security.SecureString)
    //     method.
    //
    // Parameters:
    //   s:
    //     The address of the BSTR to free.
    }
    public static void ZeroFreeBSTR(IntPtr s) {
    //
    // Summary:
    //     Frees an unmanaged string pointer that was allocated using the System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemAnsi(System.Security.SecureString)
    //     method.
    //
    // Parameters:
    //   s:
    //     The address of the unmanaged string to free.
    }
    public static void ZeroFreeCoTaskMemAnsi(IntPtr s) {
    //
    // Summary:
    //     Frees an unmanaged string pointer that was allocated using the System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemUnicode(System.Security.SecureString)
    //     method.
    //
    // Parameters:
    //   s:
    //     The address of the unmanaged string to free.
    }
    public static void ZeroFreeCoTaskMemUnicode(IntPtr s) {
    //
    // Summary:
    //     Frees an unmanaged string pointer that was allocated using the System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocAnsi(System.Security.SecureString)
    //     method.
    //
    // Parameters:
    //   s:
    //     The address of the unmanaged string to free.
    }
    public static void ZeroFreeGlobalAllocAnsi(IntPtr s) {
    //
    // Summary:
    //     Frees an unmanaged string pointer that was allocated using the System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemUnicode(System.Security.SecureString)
    //     method.
    //
    // Parameters:
    //   s:
    //     The address of the unmanaged string to free.
    }
    public static void ZeroFreeGlobalAllocUnicode(IntPtr s) {
    }
  }
}
