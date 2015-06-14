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

// File System.Deployment.Internal.Isolation.IStore.cs
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


namespace System.Deployment.Internal.Isolation
{
  internal partial interface IStore
  {
    #region Methods and constructors
    Object EnumAssemblies(uint Flags, IReferenceIdentity ReferenceIdentity_ToMatch, ref Guid riid);

    Object EnumCategories(uint Flags, IReferenceIdentity ReferenceIdentity_ToMatch, ref Guid riid);

    Object EnumCategoryInstances(uint Flags, IDefinitionIdentity CategoryId, string SubcategoryPath, ref Guid riid);

    Object EnumFiles(uint Flags, IDefinitionIdentity DefinitionIdentity, ref Guid riid);

    Object EnumInstallationReferences(uint Flags, IDefinitionIdentity DefinitionIdentity, ref Guid riid);

    Object EnumPrivateFiles(uint Flags, IDefinitionAppId Application, IDefinitionIdentity DefinitionIdentity, ref Guid riid);

    Object EnumSubcategories(uint Flags, IDefinitionIdentity CategoryId, string SubcategoryPathPattern, ref Guid riid);

    Object GetAssemblyInformation(uint Flags, IDefinitionIdentity DefinitionIdentity, ref Guid riid);

    string LockApplicationPath(uint Flags, IDefinitionAppId ApId, out IntPtr Cookie);

    string LockAssemblyPath(uint Flags, IDefinitionIdentity DefinitionIdentity, out IntPtr Cookie);

    ulong QueryChangeID(IDefinitionIdentity DefinitionIdentity);

    void ReleaseApplicationPath(IntPtr Cookie);

    void ReleaseAssemblyPath(IntPtr Cookie);
    #endregion
  }
}
