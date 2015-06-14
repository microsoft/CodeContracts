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

namespace System.Runtime.InteropServices.ComTypes {
  // Summary:
  //     Provides the managed definition of the ITypeInfo interface.
  [Guid("00020401-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface ITypeInfo {
    // Summary:
    //     Retrieves the addresses of static functions or variables, such as those defined
    //     in a DLL.
    //
    // Parameters:
    //   memid:
    //     The member ID of the static member's address to retrieve.
    //
    //   invKind:
    //     One of the System.Runtime.InteropServices.ComTypes.INVOKEKIND values that
    //     specifies whether the member is a property, and if so, what kind.
    //
    //   ppv:
    //     When this method returns, contains a reference to the static member. This
    //     parameter is passed uninitialized.
    void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);
    //
    // Summary:
    //     Creates a new instance of a type that describes a component class (coclass).
    //
    // Parameters:
    //   pUnkOuter:
    //     The object that acts as the controlling IUnknown.
    //
    //   riid:
    //     The IID of the interface that the caller uses to communicate with the resulting
    //     object.
    //
    //   ppvObj:
    //     When this method returns, contains a reference to the created object. This
    //     parameter is passed uninitialized.
    void CreateInstance(object pUnkOuter, ref Guid riid, out object ppvObj);
    //
    // Summary:
    //     Retrieves the type library that contains this type description and its index
    //     within that type library.
    //
    // Parameters:
    //   ppTLB:
    //     When this method returns, contains a reference to the containing type library.
    //     This parameter is passed uninitialized.
    //
    //   pIndex:
    //     When this method returns, contains a reference to the index of the type description
    //     within the containing type library. This parameter is passed uninitialized.
    void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);
    //
    // Summary:
    //     Retrieves a description or specification of an entry point for a function
    //     in a DLL.
    //
    // Parameters:
    //   memid:
    //     The ID of the member function whose DLL entry description is to be returned.
    //
    //   invKind:
    //     One of the System.Runtime.InteropServices.ComTypes.INVOKEKIND values that
    //     specifies the kind of member identified by memid.
    //
    //   pBstrDllName:
    //     If not null, the function sets pBstrDllName to a BSTR that contains the name
    //     of the DLL.
    //
    //   pBstrName:
    //     If not null, the function sets lpbstrName to a BSTR that contains the name
    //     of the entry point.
    //
    //   pwOrdinal:
    //     If not null, and the function is defined by an ordinal, then lpwOrdinal is
    //     set to point to the ordinal.
    void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);
    //
    // Summary:
    //     Retrieves the documentation string, the complete Help file name and path,
    //     and the context ID for the Help topic for a specified type description.
    //
    // Parameters:
    //   index:
    //     The ID of the member whose documentation is to be returned.
    //
    //   strName:
    //     When this method returns, contains the name of the item method. This parameter
    //     is passed uninitialized.
    //
    //   strDocString:
    //     When this method returns, contains the documentation string for the specified
    //     item. This parameter is passed uninitialized.
    //
    //   dwHelpContext:
    //     When this method returns, contains a reference to the Help context associated
    //     with the specified item. This parameter is passed uninitialized.
    //
    //   strHelpFile:
    //     When this method returns, contains the fully qualified name of the Help file.
    //     This parameter is passed uninitialized.
    void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);
    //
    // Summary:
    //     Retrieves the System.Runtime.InteropServices.FUNCDESC structure that contains
    //     information about a specified function.
    //
    // Parameters:
    //   index:
    //     The index of the function description to return.
    //
    //   ppFuncDesc:
    //     When this method returns, contains a reference to a FUNCDESC structure that
    //     describes the specified function. This parameter is passed uninitialized.
    void GetFuncDesc(int index, out IntPtr ppFuncDesc);
    //
    // Summary:
    //     Maps between member names and member IDs, and parameter names and parameter
    //     IDs.
    //
    // Parameters:
    //   rgszNames:
    //     An array of names to map.
    //
    //   cNames:
    //     The count of names to map.
    //
    //   pMemId:
    //     When this method returns, contains a reference to an array in which name
    //     mappings are placed. This parameter is passed uninitialized.
    void GetIDsOfNames(string[] rgszNames, int cNames, int[] pMemId);
    //
    // Summary:
    //     Retrieves the System.Runtime.InteropServices.IMPLTYPEFLAGS value for one
    //     implemented interface or base interface in a type description.
    //
    // Parameters:
    //   index:
    //     The index of the implemented interface or base interface.
    //
    //   pImplTypeFlags:
    //     When this method returns, contains a reference to the IMPLTYPEFLAGS enumeration.
    //     This parameter is passed uninitialized.
#if false
    void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);
#endif
    //
    // Summary:
    //     Retrieves marshaling information.
    //
    // Parameters:
    //   memid:
    //     The member ID that indicates which marshaling information is needed.
    //
    //   pBstrMops:
    //     When this method returns, contains a reference to the opcode string used
    //     in marshaling the fields of the structure described by the referenced type
    //     description, or returns null if there is no information to return. This parameter
    //     is passed uninitialized.
    void GetMops(int memid, out string pBstrMops);
    //
    // Summary:
    //     Retrieves the variable with the specified member ID (or the name of the property
    //     or method and its parameters) that corresponds to the specified function
    //     ID.
    //
    // Parameters:
    //   memid:
    //     The ID of the member whose name (or names) is to be returned.
    //
    //   rgBstrNames:
    //     When this method returns, contains the name (or names) associated with the
    //     member. This parameter is passed uninitialized.
    //
    //   cMaxNames:
    //     The length of the rgBstrNames array.
    //
    //   pcNames:
    //     When this method returns, contains the number of names in the rgBstrNames
    //     array. This parameter is passed uninitialized.
    void GetNames(int memid, string[] rgBstrNames, int cMaxNames, out int pcNames);
    //
    // Summary:
    //     Retrieves the referenced type descriptions if a type description references
    //     other type descriptions.
    //
    // Parameters:
    //   hRef:
    //     A handle to the referenced type description to return.
    //
    //   ppTI:
    //     When this method returns, contains the referenced type description. This
    //     parameter is passed uninitialized.
    void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);
    //
    // Summary:
    //     Retrieves the type description of the implemented interface types if a type
    //     description describes a COM class.
    //
    // Parameters:
    //   index:
    //     The index of the implemented type whose handle is returned.
    //
    //   href:
    //     When this method returns, contains a reference to a handle for the implemented
    //     interface. This parameter is passed uninitialized.
    void GetRefTypeOfImplType(int index, out int href);
    //
    // Summary:
    //     Retrieves a System.Runtime.InteropServices.TYPEATTR structure that contains
    //     the attributes of the type description.
    //
    // Parameters:
    //   ppTypeAttr:
    //     When this method returns, contains a reference to the structure that contains
    //     the attributes of this type description. This parameter is passed uninitialized.
    void GetTypeAttr(out IntPtr ppTypeAttr);
    //
    // Summary:
    //     Retrieves the ITypeComp interface for the type description, which enables
    //     a client compiler to bind to the type description's members.
    //
    // Parameters:
    //   ppTComp:
    //     When this method returns, contains a reference to the ITypeComp interface
    //     of the containing type library. This parameter is passed uninitialized.
    void GetTypeComp(out ITypeComp ppTComp);
    //
    // Summary:
    //     Retrieves a VARDESC structure that describes the specified variable.
    //
    // Parameters:
    //   index:
    //     The index of the variable description to return.
    //
    //   ppVarDesc:
    //     When this method returns, contains a reference to the VARDESC structure that
    //     describes the specified variable. This parameter is passed uninitialized.
    void GetVarDesc(int index, out IntPtr ppVarDesc);
    //
    // Summary:
    //     Invokes a method, or accesses a property of an object, that implements the
    //     interface described by the type description.
    //
    // Parameters:
    //   pvInstance:
    //     A reference to the interface described by this type description.
    //
    //   memid:
    //     A value that identifies the interface member.
    //
    //   wFlags:
    //     Flags that describe the context of the invoke call.
    //
    //   pDispParams:
    //     A reference to a structure that contains an array of arguments, an array
    //     of DISPIDs for named arguments, and counts of the number of elements in each
    //     array.
    //
    //   pVarResult:
    //     A reference to the location at which the result is to be stored. If wFlags
    //     specifies DISPATCH_PROPERTYPUT or DISPATCH_PROPERTYPUTREF, pVarResult is
    //     ignored. Set to null if no result is desired.
    //
    //   pExcepInfo:
    //     A pointer to an exception information structure, which is filled in only
    //     if DISP_E_EXCEPTION is returned.
    //
    //   puArgErr:
    //     If Invoke returns DISP_E_TYPEMISMATCH, puArgErr indicates the index within
    //     rgvarg of the argument with the incorrect type. If more than one argument
    //     returns an error, puArgErr indicates only the first argument with an error.
    //     This parameter is passed uninitialized.
    void Invoke(object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);
    //
    // Summary:
    //     Releases a System.Runtime.InteropServices.FUNCDESC structure previously returned
    //     by the System.Runtime.InteropServices.ComTypes.ITypeInfo.GetFuncDesc(System.Int32,System.IntPtr@)
    //     method.
    //
    // Parameters:
    //   pFuncDesc:
    //     A reference to the FUNCDESC structure to release.
    void ReleaseFuncDesc(IntPtr pFuncDesc);
    //
    // Summary:
    //     Releases a System.Runtime.InteropServices.TYPEATTR structure previously returned
    //     by the System.Runtime.InteropServices.ComTypes.ITypeInfo.GetTypeAttr(System.IntPtr@)
    //     method.
    //
    // Parameters:
    //   pTypeAttr:
    //     A reference to the TYPEATTR structure to release.
    void ReleaseTypeAttr(IntPtr pTypeAttr);
    //
    // Summary:
    //     Releases a VARDESC structure previously returned by the System.Runtime.InteropServices.ComTypes.ITypeInfo.GetVarDesc(System.Int32,System.IntPtr@)
    //     method.
    //
    // Parameters:
    //   pVarDesc:
    //     A reference to the VARDESC structure to release.
    void ReleaseVarDesc(IntPtr pVarDesc);
  }
}
