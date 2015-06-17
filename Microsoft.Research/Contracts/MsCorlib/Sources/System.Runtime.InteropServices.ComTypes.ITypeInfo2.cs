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

// File System.Runtime.InteropServices.ComTypes.ITypeInfo2.cs
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


namespace System.Runtime.InteropServices.ComTypes
{
  public partial interface ITypeInfo2 : ITypeInfo
  {
    #region Methods and constructors
    void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

    void CreateInstance(Object pUnkOuter, ref Guid riid, out Object ppvObj);

    void GetAllCustData(IntPtr pCustData);

    void GetAllFuncCustData(int index, IntPtr pCustData);

    void GetAllImplTypeCustData(int index, IntPtr pCustData);

    void GetAllParamCustData(int indexFunc, int indexParam, IntPtr pCustData);

    void GetAllVarCustData(int index, IntPtr pCustData);

    void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

    void GetCustData(ref Guid guid, out Object pVarVal);

    void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

    void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

    void GetDocumentation2(int memid, out string pbstrHelpString, out int pdwHelpStringContext, out string pbstrHelpStringDll);

    void GetFuncCustData(int index, ref Guid guid, out Object pVarVal);

    void GetFuncDesc(int index, out IntPtr ppFuncDesc);

    void GetFuncIndexOfMemId(int memid, INVOKEKIND invKind, out int pFuncIndex);

    void GetIDsOfNames(string[] rgszNames, int cNames, int[] pMemId);

    void GetImplTypeCustData(int index, ref Guid guid, out Object pVarVal);

    void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

    void GetMops(int memid, out string pBstrMops);

    void GetNames(int memid, string[] rgBstrNames, int cMaxNames, out int pcNames);

    void GetParamCustData(int indexFunc, int indexParam, ref Guid guid, out Object pVarVal);

    void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

    void GetRefTypeOfImplType(int index, out int href);

    void GetTypeAttr(out IntPtr ppTypeAttr);

    void GetTypeComp(out ITypeComp ppTComp);

    void GetTypeFlags(out int pTypeFlags);

    void GetTypeKind(out TYPEKIND pTypeKind);

    void GetVarCustData(int index, ref Guid guid, out Object pVarVal);

    void GetVarDesc(int index, out IntPtr ppVarDesc);

    void GetVarIndexOfMemId(int memid, out int pVarIndex);

    void Invoke(Object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

    void ReleaseFuncDesc(IntPtr pFuncDesc);

    void ReleaseTypeAttr(IntPtr pTypeAttr);

    void ReleaseVarDesc(IntPtr pVarDesc);
    #endregion
  }
}
