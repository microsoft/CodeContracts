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

// File System.Diagnostics.SymbolStore.ISymbolWriter.cs
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


namespace System.Diagnostics.SymbolStore
{
  public partial interface ISymbolWriter
  {
    #region Methods and constructors
    void Close();

    void CloseMethod();

    void CloseNamespace();

    void CloseScope(int endOffset);

    ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType);

    void DefineField(SymbolToken parent, string name, System.Reflection.FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

    void DefineGlobalVariable(string name, System.Reflection.FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

    void DefineLocalVariable(string name, System.Reflection.FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset);

    void DefineParameter(string name, System.Reflection.ParameterAttributes attributes, int sequence, SymAddressKind addrKind, int addr1, int addr2, int addr3);

    void DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns);

    void Initialize(IntPtr emitter, string filename, bool fFullBuild);

    void OpenMethod(SymbolToken method);

    void OpenNamespace(string name);

    int OpenScope(int startOffset);

    void SetMethodSourceRange(ISymbolDocumentWriter startDoc, int startLine, int startColumn, ISymbolDocumentWriter endDoc, int endLine, int endColumn);

    void SetScopeRange(int scopeID, int startOffset, int endOffset);

    void SetSymAttribute(SymbolToken parent, string name, byte[] data);

    void SetUnderlyingWriter(IntPtr underlyingWriter);

    void SetUserEntryPoint(SymbolToken entryMethod);

    void UsingNamespace(string fullName);
    #endregion
  }
}
