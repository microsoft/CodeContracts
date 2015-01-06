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
  //     Provides the managed definition of the ITypeLib interface.
  [Guid("00020402-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface ITypeLib {
    // Summary:
    //     Finds occurrences of a type description in a type library.
    //
    // Parameters:
    //   szNameBuf:
    //     The name to search for. This is an in/out parameter.
    //
    //   lHashVal:
    //     A hash value to speed up the search, computed by the LHashValOfNameSys function.
    //     If lHashVal is 0, a value is computed.
    //
    //   ppTInfo:
    //     When this method returns, contains an array of pointers to the type descriptions
    //     that contain the name specified in szNameBuf. This parameter is passed uninitialized.
    //
    //   rgMemId:
    //     An array of the MEMBERID 's of the found items; rgMemId [i] is the MEMBERID
    //     that indexes into the type description specified by ppTInfo [i]. Cannot be
    //     null.
    //
    //   pcFound:
    //     On entry, indicates how many instances to look for. For example, pcFound
    //     = 1 can be called to find the first occurrence. The search stops when one
    //     instance is found.On exit, indicates the number of instances that were found.
    //     If the in and out values of pcFound are identical, there might be more type
    //     descriptions that contain the name.
    void FindName(string szNameBuf, int lHashVal, ITypeInfo[] ppTInfo, int[] rgMemId, ref short pcFound);
    //
    // Summary:
    //     Retrieves the library's documentation string, the complete Help file name
    //     and path, and the context identifier for the library Help topic in the Help
    //     file.
    //
    // Parameters:
    //   index:
    //     The index of the type description whose documentation is to be returned.
    //
    //   strName:
    //     When this method returns, contains a string that represents the name of the
    //     specified item. This parameter is passed uninitialized.
    //
    //   strDocString:
    //     When this method returns, contains a string that represents the documentation
    //     string for the specified item. This parameter is passed uninitialized.
    //
    //   dwHelpContext:
    //     When this method returns, contains the Help context identifier associated
    //     with the specified item. This parameter is passed uninitialized.
    //
    //   strHelpFile:
    //     When this method returns, contains a string that represents the fully qualified
    //     name of the Help file. This parameter is passed uninitialized.
    void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);
    //
    // Summary:
    //     Retrieves the structure that contains the library's attributes.
    //
    // Parameters:
    //   ppTLibAttr:
    //     When this method returns, contains a structure that contains the library's
    //     attributes. This parameter is passed uninitialized.
    void GetLibAttr(out IntPtr ppTLibAttr);
    //
    // Summary:
    //     Enables a client compiler to bind to a library's types, variables, constants,
    //     and global functions.
    //
    // Parameters:
    //   ppTComp:
    //     When this method returns, contains an instance of a ITypeComp instance for
    //     this ITypeLib. This parameter is passed uninitialized.
    void GetTypeComp(out ITypeComp ppTComp);
    //
    // Summary:
    //     Retrieves the specified type description in the library.
    //
    // Parameters:
    //   index:
    //     The index of the ITypeInfo interface to return.
    //
    //   ppTI:
    //     When this method returns, contains an ITypeInfo describing the type referenced
    //     by index. This parameter is passed uninitialized.
    void GetTypeInfo(int index, out ITypeInfo ppTI);
    //
    // Summary:
    //     Returns the number of type descriptions in the type library.
    //
    // Returns:
    //     The number of type descriptions in the type library.
    int GetTypeInfoCount();
    //
    // Summary:
    //     Retrieves the type description that corresponds to the specified GUID.
    //
    // Parameters:
    //   guid:
    //     The IID of the interface of CLSID of the class whose type info is requested.
    //
    //   ppTInfo:
    //     When this method returns, contains the requested ITypeInfo interface. This
    //     parameter is passed uninitialized.
    void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);
    //
    // Summary:
    //     Retrieves the type of a type description.
    //
    // Parameters:
    //   index:
    //     The index of the type description within the type library.
    //
    //   pTKind:
    //     When this method returns, contains a reference to the TYPEKIND enumeration
    //     for the type description. This parameter is passed uninitialized.
    void GetTypeInfoType(int index, out TYPEKIND pTKind);
    //
    // Summary:
    //     Indicates whether a passed-in string contains the name of a type or member
    //     described in the library.
    //
    // Parameters:
    //   szNameBuf:
    //     The string to test. This is an in/out parameter.
    //
    //   lHashVal:
    //     The hash value of szNameBuf.
    //
    // Returns:
    //     true if szNameBuf was found in the type library; otherwise, false.
    bool IsName(string szNameBuf, int lHashVal);
    //
    // Summary:
    //     Releases the System.Runtime.InteropServices.TYPELIBATTR structure originally
    //     obtained from the System.Runtime.InteropServices.ComTypes.ITypeLib.GetLibAttr(System.IntPtr@)
    //     method.
    //
    // Parameters:
    //   pTLibAttr:
    //     The TLIBATTR structure to release.
    void ReleaseTLibAttr(IntPtr pTLibAttr);
  }
}