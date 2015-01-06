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
using System.Reflection;
using System.Security;
using System.Threading;
using System.Diagnostics.Contracts;

namespace System.Runtime.InteropServices {
  public static class Marshal {

    public static readonly int SystemDefaultCharSize;
    public static readonly int SystemMaxDBCSCharSize;

    [Pure]
    public static int AddRef(IntPtr pUnk) {
      return default(int);
    }
    [Pure]
    public static IntPtr AllocCoTaskMem(int cb) {
      return default(IntPtr);
    }
    [Pure]
    public static IntPtr AllocHGlobal(int cb) {
      return default(IntPtr);
    }
    [Pure]
    public static IntPtr AllocHGlobal(IntPtr cb) {
      return default(IntPtr);
    }
    [Pure]
    public static object BindToMoniker(string monikerName) {
      return default(object);
    }
    [Pure]
    public static void ChangeWrapperHandleStrength(object otp, bool fIsWeak) {
    }
    [Pure]
    public static void Copy(byte[] source, int startIndex, IntPtr destination, int length) {
      Contract.Requires(source != null);
      Contract.Requires(destination != IntPtr.Zero);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(char[] source, int startIndex, IntPtr destination, int length) {
      Contract.Requires(source != null);
      Contract.Requires(destination != IntPtr.Zero);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(double[] source, int startIndex, IntPtr destination, int length) {
      Contract.Requires(source != null);
      Contract.Requires(destination != IntPtr.Zero);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(float[] source, int startIndex, IntPtr destination, int length) {
      Contract.Requires(source != null);
      Contract.Requires(destination != IntPtr.Zero);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(int[] source, int startIndex, IntPtr destination, int length) {
      Contract.Requires(source != null);
      Contract.Requires(destination != IntPtr.Zero);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, byte[] destination, int startIndex, int length) {
      Contract.Requires(source != IntPtr.Zero);
      Contract.Requires(destination != null);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, char[] destination, int startIndex, int length) {
      Contract.Requires(source != IntPtr.Zero);
      Contract.Requires(destination != null);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, double[] destination, int startIndex, int length) {
      Contract.Requires(source != IntPtr.Zero);
      Contract.Requires(destination != null);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, float[] destination, int startIndex, int length) {
      Contract.Requires(source != IntPtr.Zero);
      Contract.Requires(destination != null);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, int[] destination, int startIndex, int length) {
      Contract.Requires(source != IntPtr.Zero);
      Contract.Requires(destination != null);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length) {
      Contract.Requires(source != IntPtr.Zero);
      Contract.Requires(destination != null);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, long[] destination, int startIndex, int length) {
      Contract.Requires(source != IntPtr.Zero);
      Contract.Requires(destination != null);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr source, short[] destination, int startIndex, int length) {
      Contract.Requires(source != IntPtr.Zero);
      Contract.Requires(destination != null);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length) {
      Contract.Requires(source != null);
      Contract.Requires(destination != IntPtr.Zero);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(long[] source, int startIndex, IntPtr destination, int length) {
      Contract.Requires(source != null);
      Contract.Requires(destination != IntPtr.Zero);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void Copy(short[] source, int startIndex, IntPtr destination, int length) {
      Contract.Requires(source != null);
      Contract.Requires(destination != IntPtr.Zero);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr CreateAggregatedObject(IntPtr pOuter, object o) {
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static object CreateWrapperOfType(object o, Type t) {
        Contract.Requires(t != null);
      return default(object);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void DestroyStructure(IntPtr ptr, Type structuretype) {
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int FinalReleaseComObject(object o) {
      Contract.Requires(o != null);
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void FreeBSTR(IntPtr ptr) {
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void FreeCoTaskMem(IntPtr ptr) {
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static void FreeHGlobal(IntPtr hglobal) {
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static Guid GenerateGuidForType(Type type) {
      return default(Guid);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string GenerateProgIdForType(Type type) {
        Contract.Requires(type != null);
      return default(string);
    }
    public static object GetActiveObject(string progID) {
      return default(object);
    }
    public static IntPtr GetComInterfaceForObject(object o, Type T) {
      Contract.Requires(o != null);
      Contract.Requires(T != null);
      return default(IntPtr);
    }
    public static IntPtr GetComInterfaceForObjectInContext(object o, Type t) {
      Contract.Requires(o != null);
      Contract.Requires(t != null);
      return default(IntPtr);
    }
    public static object GetComObjectData (object obj, object key) {
        Contract.Requires(obj != null);
        Contract.Requires(key != null);
      return default(object);
    }
    public static int GetComSlotForMethodInfo(MemberInfo m) {
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t) {
      Contract.Requires(ptr != IntPtr.Zero);
      Contract.Requires(t != null);
      return default(Delegate);
    }
    public static int GetEndComSlot(Type t) {
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int GetExceptionCode() {
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static Exception GetExceptionForHR(int errorCode) {
      return default(Exception);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo) {
      return default(Exception);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetExceptionPointers() {
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetFunctionPointerForDelegate(Delegate d) {
      Contract.Requires(d != null);
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetHINSTANCE (Module m) {
        Contract.Requires(m != null);
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int GetHRForException(Exception e) {
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int GetHRForLastWin32Error() {
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetIDispatchForObject(object o) {
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetIDispatchForObjectInContext(object o) {
      Contract.Requires(o != null);
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetITypeInfoForType(Type t) {
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr GetIUnknownForObject(object o) {
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
//    public static MemberInfo GetMethodInfoForComSlot(Type t, int slot, ref ComMemberType memberType) {
   //   return default(MemberInfo);
    //}
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
      return default(object[]);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int GetStartComSlot(Type t) {
      return default(int);
    }
    public static Thread GetThreadFromFiberCookie(int cookie) {
      Contract.Requires(cookie != 0);
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
//    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
 //   public static string GetTypeInfoName(ITypeInfo typeInfo) {
   //    Contract.Requires(typeInfo != null);
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
//      return default(string);
    //}

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
//    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
 //   public static Guid GetTypeLibGuid(ITypeLib typelib) {
  //    Contract.Requires(typelib != null);
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
    //  return default(Guid);
    //}

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
//    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
 //   public static int GetTypeLibLcid(ITypeLib typelib) {
 //      Contract.Requires(typelib != null);
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
   //   return default(int);
    //}

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

//    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
 //   public static string GetTypeLibName(ITypeLib typelib) {
  //     Contract.Requires(typelib != null);
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
    //  return default(string);
    //}
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
    // public static void GetTypeLibVersionForAssembly(Assembly inputAssembly, out int majorVersion, out int minorVersion) {
    //}
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
    //[Obsolete("The GetUnmanagedThunkForManagedMethodPtr method has been deprecated and will be removed in a future release.", false)]
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
    public static IntPtr OffsetOf(Type t, string fieldName) {
        Contract.Requires(t != null);
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
    public static void PrelinkAll(Type c) {
        Contract.Requires(c != null);
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
      Contract.Ensures(ptr == IntPtr.Zero || Contract.Result<string>() != null);

      return default(string);
    }

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
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static string PtrToStringAnsi(IntPtr ptr, int len) {

      return default(string);
    }
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
      return default(object);
    }
    public static int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv) {
      ppv = default(IntPtr);
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static byte ReadByte(IntPtr ptr) {
      Contract.Requires(ptr != IntPtr.Zero);
      return default(byte);
    }
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static byte ReadByte(IntPtr ptr, int ofs)
    {
      Contract.Requires(ptr != IntPtr.Zero);

      return 0;
    }
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
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int ReleaseComObject(object o) {
      Contract.Requires(o != null); 
      return default(int);
    }
    //[Obsolete("This API did not perform any operation and will be removed in future versions of the CLR.", false)]
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
//    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
  //  public static IntPtr SecureStringToBSTR(SecureString s) {
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
    //  return default(IntPtr);
   // }
   // [Pure][Reads(ReadsAttribute.Reads.Nothing)]
   // public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s) {
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
     // return default(IntPtr);
   // }
    //[Pure][Reads(ReadsAttribute.Reads.Nothing)]
    //public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s) {
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
      //return default(IntPtr);
    //}
    //[Pure][Reads(ReadsAttribute.Reads.Nothing)]
    //public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s) {
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
    //  return default(IntPtr);
   // }
   // [Pure][Reads(ReadsAttribute.Reads.Nothing)]
   // public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s) {
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
     // return default(IntPtr);
   // }
    public static bool SetComObjectData(object obj, object key, object data) {
        Contract.Requires(obj != null);
        Contract.Requires(key != null);

      return default(bool);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int SizeOf(object structure) {
      Contract.Requires(structure != null);
      Contract.Ensures(Contract.Result<int>() > 0);
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static int SizeOf(Type t) {
      Contract.Requires(t != null);
      Contract.Ensures(Contract.Result<int>() > 0);
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr StringToBSTR(string s) {
      return default(IntPtr);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static IntPtr StringToCoTaskMemAnsi(string s) {
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
#endif