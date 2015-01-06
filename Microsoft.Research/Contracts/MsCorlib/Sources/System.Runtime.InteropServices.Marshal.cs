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

// File System.Runtime.InteropServices.Marshal.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Runtime.InteropServices
{
  static public partial class Marshal
  {
    #region Methods and constructors
    public static int AddRef(IntPtr pUnk)
    {
      return default(int);
    }

    public static IntPtr AllocCoTaskMem(int cb)
    {
      return default(IntPtr);
    }

    public static IntPtr AllocHGlobal(int cb)
    {
      return default(IntPtr);
    }

    public static IntPtr AllocHGlobal(IntPtr cb)
    {
      return default(IntPtr);
    }

    public static bool AreComObjectsAvailableForCleanup()
    {
      return default(bool);
    }

    public static Object BindToMoniker(string monikerName)
    {
      return default(Object);
    }

    public static void ChangeWrapperHandleStrength(Object otp, bool fIsWeak)
    {
    }

    public static void CleanupUnusedObjectsInCurrentContext()
    {
    }

    public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
    {
    }

    public static void Copy(IntPtr source, short[] destination, int startIndex, int length)
    {
    }

    public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
    {
    }

    public static void Copy(IntPtr source, int[] destination, int startIndex, int length)
    {
    }

    public static void Copy(IntPtr source, long[] destination, int startIndex, int length)
    {
    }

    public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
    {
    }

    public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
    {
    }

    public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
    {
    }

    public static void Copy(IntPtr source, double[] destination, int startIndex, int length)
    {
    }

    public static void Copy(short[] source, int startIndex, IntPtr destination, int length)
    {
    }

    public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
    {
    }

    public static void Copy(int[] source, int startIndex, IntPtr destination, int length)
    {
    }

    public static void Copy(long[] source, int startIndex, IntPtr destination, int length)
    {
    }

    public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
    {
    }

    public static void Copy(double[] source, int startIndex, IntPtr destination, int length)
    {
    }

    public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
    {
    }

    public static IntPtr CreateAggregatedObject(IntPtr pOuter, Object o)
    {
      return default(IntPtr);
    }

    public static Object CreateWrapperOfType(Object o, Type t)
    {
      return default(Object);
    }

    public static void DestroyStructure(IntPtr ptr, Type structuretype)
    {
    }

    public static int FinalReleaseComObject(Object o)
    {
      Contract.Ensures(Contract.Result<int>() == 0);

      return default(int);
    }

    public static void FreeBSTR(IntPtr ptr)
    {
    }

    public static void FreeCoTaskMem(IntPtr ptr)
    {
    }

    public static void FreeHGlobal(IntPtr hglobal)
    {
    }

    public static Guid GenerateGuidForType(Type type)
    {
      return default(Guid);
    }

    public static string GenerateProgIdForType(Type type)
    {
      return default(string);
    }

    public static Object GetActiveObject(string progID)
    {
      return default(Object);
    }

    public static IntPtr GetComInterfaceForObject(Object o, Type T)
    {
      return default(IntPtr);
    }

    public static IntPtr GetComInterfaceForObject(Object o, Type T, CustomQueryInterfaceMode mode)
    {
      return default(IntPtr);
    }

    public static IntPtr GetComInterfaceForObjectInContext(Object o, Type t)
    {
      return default(IntPtr);
    }

    public static Object GetComObjectData(Object obj, Object key)
    {
      return default(Object);
    }

    public static int GetComSlotForMethodInfo(System.Reflection.MemberInfo m)
    {
      Contract.Requires(m.DeclaringType != null);

      return default(int);
    }

    public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
    {
      return default(Delegate);
    }

    public static int GetEndComSlot(Type t)
    {
      return default(int);
    }

    public static int GetExceptionCode()
    {
      return default(int);
    }

    public static Exception GetExceptionForHR(int errorCode)
    {
      return default(Exception);
    }

    public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
    {
      return default(Exception);
    }

    public static IntPtr GetExceptionPointers()
    {
      return default(IntPtr);
    }

    public static IntPtr GetFunctionPointerForDelegate(Delegate d)
    {
      return default(IntPtr);
    }

    public static IntPtr GetHINSTANCE(System.Reflection.Module m)
    {
      return default(IntPtr);
    }

    public static int GetHRForException(Exception e)
    {
      return default(int);
    }

    public static int GetHRForLastWin32Error()
    {
      Contract.Ensures(-2147024896 <= Contract.Result<int>());

      return default(int);
    }

    public static IntPtr GetIDispatchForObject(Object o)
    {
      return default(IntPtr);
    }

    public static IntPtr GetIDispatchForObjectInContext(Object o)
    {
      return default(IntPtr);
    }

    public static IntPtr GetITypeInfoForType(Type t)
    {
      return default(IntPtr);
    }

    public static IntPtr GetIUnknownForObject(Object o)
    {
      return default(IntPtr);
    }

    public static IntPtr GetIUnknownForObjectInContext(Object o)
    {
      return default(IntPtr);
    }

    public static int GetLastWin32Error()
    {
      return default(int);
    }

    public static IntPtr GetManagedThunkForUnmanagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
    {
      return default(IntPtr);
    }

    public static System.Reflection.MemberInfo GetMethodInfoForComSlot(Type t, int slot, ref ComMemberType memberType)
    {
      return default(System.Reflection.MemberInfo);
    }

    public static void GetNativeVariantForObject(Object obj, IntPtr pDstNativeVariant)
    {
    }

    public static Object GetObjectForIUnknown(IntPtr pUnk)
    {
      return default(Object);
    }

    public static Object GetObjectForNativeVariant(IntPtr pSrcNativeVariant)
    {
      return default(Object);
    }

    public static Object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars)
    {
      return default(Object[]);
    }

    public static int GetStartComSlot(Type t)
    {
      return default(int);
    }

    public static System.Threading.Thread GetThreadFromFiberCookie(int cookie)
    {
      return default(System.Threading.Thread);
    }

    public static Object GetTypedObjectForIUnknown(IntPtr pUnk, Type t)
    {
      return default(Object);
    }

    public static Type GetTypeForITypeInfo(IntPtr piTypeInfo)
    {
      return default(Type);
    }

    public static string GetTypeInfoName(UCOMITypeInfo pTI)
    {
      return default(string);
    }

    public static string GetTypeInfoName(System.Runtime.InteropServices.ComTypes.ITypeInfo typeInfo)
    {
      return default(string);
    }

    public static Guid GetTypeLibGuid(System.Runtime.InteropServices.ComTypes.ITypeLib typelib)
    {
      return default(Guid);
    }

    public static Guid GetTypeLibGuid(UCOMITypeLib pTLB)
    {
      return default(Guid);
    }

    public static Guid GetTypeLibGuidForAssembly(System.Reflection.Assembly asm)
    {
      return default(Guid);
    }

    public static int GetTypeLibLcid(System.Runtime.InteropServices.ComTypes.ITypeLib typelib)
    {
      return default(int);
    }

    public static int GetTypeLibLcid(UCOMITypeLib pTLB)
    {
      return default(int);
    }

    public static string GetTypeLibName(System.Runtime.InteropServices.ComTypes.ITypeLib typelib)
    {
      return default(string);
    }

    public static string GetTypeLibName(UCOMITypeLib pTLB)
    {
      return default(string);
    }

    public static void GetTypeLibVersionForAssembly(System.Reflection.Assembly inputAssembly, out int majorVersion, out int minorVersion)
    {
      majorVersion = default(int);
      minorVersion = default(int);
    }

    public static Object GetUniqueObjectForIUnknown(IntPtr unknown)
    {
      return default(Object);
    }

    public static IntPtr GetUnmanagedThunkForManagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
    {
      return default(IntPtr);
    }

    public static bool IsComObject(Object o)
    {
      return default(bool);
    }

    public static bool IsTypeVisibleFromCom(Type t)
    {
      return default(bool);
    }

    public static int NumParamBytes(System.Reflection.MethodInfo m)
    {
      return default(int);
    }

    public static IntPtr OffsetOf(Type t, string fieldName)
    {
      return default(IntPtr);
    }

    public static void Prelink(System.Reflection.MethodInfo m)
    {
    }

    public static void PrelinkAll(Type c)
    {
    }

    public static string PtrToStringAnsi(IntPtr ptr)
    {
      return default(string);
    }

    public static string PtrToStringAnsi(IntPtr ptr, int len)
    {
      return default(string);
    }

    public static string PtrToStringAuto(IntPtr ptr, int len)
    {
      return default(string);
    }

    public static string PtrToStringAuto(IntPtr ptr)
    {
      return default(string);
    }

    public static string PtrToStringBSTR(IntPtr ptr)
    {
      return default(string);
    }

    public static string PtrToStringUni(IntPtr ptr)
    {
      return default(string);
    }

    public static string PtrToStringUni(IntPtr ptr, int len)
    {
      return default(string);
    }

    public static void PtrToStructure(IntPtr ptr, Object structure)
    {
    }

    public static Object PtrToStructure(IntPtr ptr, Type structureType)
    {
      return default(Object);
    }

    public static int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv)
    {
      ppv = default(IntPtr);

      return default(int);
    }

    public static byte ReadByte(IntPtr ptr, int ofs)
    {
      return default(byte);
    }

    public static byte ReadByte(Object ptr, int ofs)
    {
      return default(byte);
    }

    public static byte ReadByte(IntPtr ptr)
    {
      return default(byte);
    }

    public static short ReadInt16(IntPtr ptr, int ofs)
    {
      return default(short);
    }

    public static short ReadInt16(IntPtr ptr)
    {
      return default(short);
    }

    public static short ReadInt16(Object ptr, int ofs)
    {
      return default(short);
    }

    public static int ReadInt32(Object ptr, int ofs)
    {
      return default(int);
    }

    public static int ReadInt32(IntPtr ptr, int ofs)
    {
      return default(int);
    }

    public static int ReadInt32(IntPtr ptr)
    {
      return default(int);
    }

    public static long ReadInt64(Object ptr, int ofs)
    {
      return default(long);
    }

    public static long ReadInt64(IntPtr ptr, int ofs)
    {
      return default(long);
    }

    public static long ReadInt64(IntPtr ptr)
    {
      return default(long);
    }

    public static IntPtr ReadIntPtr(IntPtr ptr)
    {
      return default(IntPtr);
    }

    public static IntPtr ReadIntPtr(Object ptr, int ofs)
    {
      return default(IntPtr);
    }

    public static IntPtr ReadIntPtr(IntPtr ptr, int ofs)
    {
      return default(IntPtr);
    }

    public static IntPtr ReAllocCoTaskMem(IntPtr pv, int cb)
    {
      return default(IntPtr);
    }

    public static IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb)
    {
      return default(IntPtr);
    }

    public static int Release(IntPtr pUnk)
    {
      return default(int);
    }

    public static int ReleaseComObject(Object o)
    {
      Contract.Requires(o != null);

      return default(int);
    }

    public static void ReleaseThreadCache()
    {
    }

    public static IntPtr SecureStringToBSTR(System.Security.SecureString s)
    {
      return default(IntPtr);
    }

    public static IntPtr SecureStringToCoTaskMemAnsi(System.Security.SecureString s)
    {
      return default(IntPtr);
    }

    public static IntPtr SecureStringToCoTaskMemUnicode(System.Security.SecureString s)
    {
      return default(IntPtr);
    }

    public static IntPtr SecureStringToGlobalAllocAnsi(System.Security.SecureString s)
    {
      return default(IntPtr);
    }

    public static IntPtr SecureStringToGlobalAllocUnicode(System.Security.SecureString s)
    {
      return default(IntPtr);
    }

    public static bool SetComObjectData(Object obj, Object key, Object data)
    {
      return default(bool);
    }

    public static int SizeOf(Object structure)
    {
      return default(int);
    }

    public static int SizeOf(Type t)
    {
      return default(int);
    }

    public static IntPtr StringToBSTR(string s)
    {
      return default(IntPtr);
    }

    public static IntPtr StringToCoTaskMemAnsi(string s)
    {
      return default(IntPtr);
    }

    public static IntPtr StringToCoTaskMemAuto(string s)
    {
      return default(IntPtr);
    }

    public static IntPtr StringToCoTaskMemUni(string s)
    {
      return default(IntPtr);
    }

    public static IntPtr StringToHGlobalAnsi(string s)
    {
      return default(IntPtr);
    }

    public static IntPtr StringToHGlobalAuto(string s)
    {
      return default(IntPtr);
    }

    public static IntPtr StringToHGlobalUni(string s)
    {
      return default(IntPtr);
    }

    public static void StructureToPtr(Object structure, IntPtr ptr, bool fDeleteOld)
    {
    }

    public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
    {
    }

    public static void ThrowExceptionForHR(int errorCode)
    {
    }

    public static IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index)
    {
      return default(IntPtr);
    }

    public static void WriteByte(IntPtr ptr, int ofs, byte val)
    {
    }

    public static void WriteByte(Object ptr, int ofs, byte val)
    {
    }

    public static void WriteByte(IntPtr ptr, byte val)
    {
    }

    public static void WriteInt16(Object ptr, int ofs, short val)
    {
    }

    public static void WriteInt16(IntPtr ptr, int ofs, short val)
    {
    }

    public static void WriteInt16(IntPtr ptr, short val)
    {
    }

    public static void WriteInt16(IntPtr ptr, int ofs, char val)
    {
    }

    public static void WriteInt16(IntPtr ptr, char val)
    {
    }

    public static void WriteInt16(Object ptr, int ofs, char val)
    {
    }

    public static void WriteInt32(IntPtr ptr, int val)
    {
    }

    public static void WriteInt32(IntPtr ptr, int ofs, int val)
    {
    }

    public static void WriteInt32(Object ptr, int ofs, int val)
    {
    }

    public static void WriteInt64(IntPtr ptr, int ofs, long val)
    {
    }

    public static void WriteInt64(Object ptr, int ofs, long val)
    {
    }

    public static void WriteInt64(IntPtr ptr, long val)
    {
    }

    public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val)
    {
    }

    public static void WriteIntPtr(Object ptr, int ofs, IntPtr val)
    {
    }

    public static void WriteIntPtr(IntPtr ptr, IntPtr val)
    {
    }

    public static void ZeroFreeBSTR(IntPtr s)
    {
    }

    public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
    {
    }

    public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
    {
    }

    public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
    {
    }

    public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
    {
    }
    #endregion

    #region Fields
    public readonly static int SystemDefaultCharSize;
    public readonly static int SystemMaxDBCSCharSize;
    #endregion
  }
}
