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

// File System.Runtime.InteropServices.ComTypes.IMoniker.cs
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
  public partial interface IMoniker
  {
    #region Methods and constructors
    void BindToObject(IBindCtx pbc, IMoniker pmkToLeft, ref Guid riidResult, out Object ppvResult);

    void BindToStorage(IBindCtx pbc, IMoniker pmkToLeft, ref Guid riid, out Object ppvObj);

    void CommonPrefixWith(IMoniker pmkOther, out IMoniker ppmkPrefix);

    void ComposeWith(IMoniker pmkRight, bool fOnlyIfNotGeneric, out IMoniker ppmkComposite);

    void Enum(bool fForward, out IEnumMoniker ppenumMoniker);

    void GetClassID(out Guid pClassID);

    void GetDisplayName(IBindCtx pbc, IMoniker pmkToLeft, out string ppszDisplayName);

    void GetSizeMax(out long pcbSize);

    void GetTimeOfLastChange(IBindCtx pbc, IMoniker pmkToLeft, out FILETIME pFileTime);

    void Hash(out int pdwHash);

    void Inverse(out IMoniker ppmk);

    int IsDirty();

    int IsEqual(IMoniker pmkOtherMoniker);

    int IsRunning(IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning);

    int IsSystemMoniker(out int pdwMksys);

    void Load(IStream pStm);

    void ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, string pszDisplayName, out int pchEaten, out IMoniker ppmkOut);

    void Reduce(IBindCtx pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced);

    void RelativePathTo(IMoniker pmkOther, out IMoniker ppmkRelPath);

    void Save(IStream pStm, bool fClearDirty);
    #endregion
  }
}
