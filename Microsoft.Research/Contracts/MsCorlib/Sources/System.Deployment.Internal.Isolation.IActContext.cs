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

// File System.Deployment.Internal.Isolation.IActContext.cs
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
  internal partial interface IActContext
  {
    #region Methods and constructors
    void ApplicationBasePath(uint Flags, out string ApplicationPath);

    void EnumCategories(uint Flags, IReferenceIdentity CategoryToMatch, ref Guid riid, out Object EnumOut);

    void EnumCategoryInstances(uint Flags, IDefinitionIdentity CategoryId, string Subcategory, ref Guid riid, out Object EnumOut);

    void EnumComponents(uint dwFlags, out Object ppIdentityEnum);

    void EnumSubcategories(uint Flags, IDefinitionIdentity CategoryId, string SubcategoryPattern, ref Guid riid, out Object EnumOut);

    void FindReferenceInContext(uint dwFlags, IReferenceIdentity Reference, out Object MatchedDefinition);

    void GetAppId(out Object AppId);

    void GetApplicationProperties(uint Flags, UIntPtr cProperties, string[] PropertyNames, out string[] PropertyValues, out UIntPtr[] ComponentIndicies);

    void GetApplicationStateFilesystemLocation(uint dwFlags, UIntPtr Component, IntPtr pCoordinateList, out string ppszPath);

    void GetComponentManifest(uint Flags, IDefinitionIdentity ComponentId, ref Guid riid, out Object ManifestInteface);

    void GetComponentPayloadPath(uint Flags, IDefinitionIdentity ComponentId, out string PayloadPath);

    void GetComponentStringTableStrings(uint Flags, IntPtr ComponentIndex, IntPtr StringCount, string[] SourceStrings, out string[] DestinationStrings, IntPtr CultureFallbacks);

    void PrepareForExecution(IntPtr Inputs, IntPtr Outputs);

    void ReplaceStringMacros(uint Flags, string Culture, string ReplacementPattern, out string Replaced);

    void SetApplicationRunningState(uint dwFlags, uint ulState, out uint ulDisposition);
    #endregion
  }
}
