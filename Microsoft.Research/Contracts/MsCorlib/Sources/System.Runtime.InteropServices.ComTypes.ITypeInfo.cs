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

// File System.Runtime.InteropServices.ComTypes.ITypeInfo.cs
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
  public partial interface ITypeInfo
  {
    #region Methods and constructors
    void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

    void CreateInstance(Object pUnkOuter, ref Guid riid, out Object ppvObj);

    void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

    void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

    void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

    void GetFuncDesc(int index, out IntPtr ppFuncDesc);

    void GetIDsOfNames(string[] rgszNames, int cNames, int[] pMemId);

    void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

    void GetMops(int memid, out string pBstrMops);

    void GetNames(int memid, string[] rgBstrNames, int cMaxNames, out int pcNames);

    void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

    void GetRefTypeOfImplType(int index, out int href);

    void GetTypeAttr(out IntPtr ppTypeAttr);

    void GetTypeComp(out ITypeComp ppTComp);

    void GetVarDesc(int index, out IntPtr ppVarDesc);

    void Invoke(Object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

    void ReleaseFuncDesc(IntPtr pFuncDesc);

    void ReleaseTypeAttr(IntPtr pTypeAttr);

    void ReleaseVarDesc(IntPtr pVarDesc);
    #endregion
  }
}
