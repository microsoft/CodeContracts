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

// File System.Deployment.Internal.Isolation.IIdentityAuthority.cs
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
  internal partial interface IIdentityAuthority
  {
    #region Methods and constructors
    bool AreDefinitionsEqual(uint Flags, IDefinitionIdentity Definition1, IDefinitionIdentity Definition2);

    bool AreReferencesEqual(uint Flags, IReferenceIdentity Reference1, IReferenceIdentity Reference2);

    bool AreTextualDefinitionsEqual(uint Flags, string IdentityLeft, string IdentityRight);

    bool AreTextualReferencesEqual(uint Flags, string IdentityLeft, string IdentityRight);

    IDefinitionIdentity CreateDefinition();

    IReferenceIdentity CreateReference();

    string DefinitionToText(uint Flags, IDefinitionIdentity DefinitionIdentity);

    uint DefinitionToTextBuffer(uint Flags, IDefinitionIdentity DefinitionIdentity, uint BufferSize, char[] Buffer);

    bool DoesDefinitionMatchReference(uint Flags, IDefinitionIdentity DefinitionIdentity, IReferenceIdentity ReferenceIdentity);

    bool DoesTextualDefinitionMatchTextualReference(uint Flags, string Definition, string Reference);

    string GenerateDefinitionKey(uint Flags, IDefinitionIdentity DefinitionIdentity);

    string GenerateReferenceKey(uint Flags, IReferenceIdentity ReferenceIdentity);

    ulong HashDefinition(uint Flags, IDefinitionIdentity DefinitionIdentity);

    ulong HashReference(uint Flags, IReferenceIdentity ReferenceIdentity);

    string ReferenceToText(uint Flags, IReferenceIdentity ReferenceIdentity);

    uint ReferenceToTextBuffer(uint Flags, IReferenceIdentity ReferenceIdentity, uint BufferSize, char[] Buffer);

    IDefinitionIdentity TextToDefinition(uint Flags, string Identity);

    IReferenceIdentity TextToReference(uint Flags, string Identity);
    #endregion
  }
}
