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
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore {
  // Summary:
  //     Represents a symbol writer for managed code.
  [ComVisible(true)]
  public interface ISymbolWriter {
    // Summary:
    //     Closes System.Diagnostics.SymbolStore.ISymbolWriter and commits the symbols
    //     to the symbol store.
    void Close();
    //
    // Summary:
    //     Closes the current method.
    void CloseMethod();
    //
    // Summary:
    //     Closes the most recent namespace.
    void CloseNamespace();
    //
    // Summary:
    //     Closes the current lexical scope.
    //
    // Parameters:
    //   endOffset:
    //     The points past the last instruction in the scope.
    void CloseScope(int endOffset);
    //
    // Summary:
    //     Defines a source document.
    //
    // Parameters:
    //   url:
    //     The URL that identifies the document.
    //
    //   language:
    //     The document language. This parameter can be System.Guid.Empty.
    //
    //   languageVendor:
    //     The identity of the vendor for the document language. This parameter can
    //     be System.Guid.Empty.
    //
    //   documentType:
    //     The type of the document. This parameter can be System.Guid.Empty.
    //
    // Returns:
    //     The object that represents the document.
    ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType);
    //
    // Summary:
    //     Defines a field in a type or a global field.
    //
    // Parameters:
    //   parent:
    //     The metadata type or method token.
    //
    //   name:
    //     The field name.
    //
    //   attributes:
    //     A bitwise combination of the field attributes.
    //
    //   signature:
    //     The field signature.
    //
    //   addrKind:
    //     The address types for addr1 and addr2.
    //
    //   addr1:
    //     The first address for the field specification.
    //
    //   addr2:
    //     The second address for the field specification.
    //
    //   addr3:
    //     The third address for the field specification.
    void DefineField(SymbolToken parent, string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);
    //
    // Summary:
    //     Defines a single global variable.
    //
    // Parameters:
    //   name:
    //     The global variable name.
    //
    //   attributes:
    //     A bitwise combination of the global variable attributes.
    //
    //   signature:
    //     The global variable signature.
    //
    //   addrKind:
    //     The address types for addr1, addr2, and addr3.
    //
    //   addr1:
    //     The first address for the global variable specification.
    //
    //   addr2:
    //     The second address for the global variable specification.
    //
    //   addr3:
    //     The third address for the global variable specification.
    void DefineGlobalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);
    //
    // Summary:
    //     Defines a single variable in the current lexical scope.
    //
    // Parameters:
    //   name:
    //     The local variable name.
    //
    //   attributes:
    //     A bitwise combination of the local variable attributes.
    //
    //   signature:
    //     The local variable signature.
    //
    //   addrKind:
    //     The address types for addr1, addr2, and addr3.
    //
    //   addr1:
    //     The first address for the local variable specification.
    //
    //   addr2:
    //     The second address for the local variable specification.
    //
    //   addr3:
    //     The third address for the local variable specification.
    //
    //   startOffset:
    //     The start offset for the variable. If this parameter is zero, it is ignored
    //     and the variable is defined throughout the entire scope. If the parameter
    //     is nonzero, the variable falls within the offsets of the current scope.
    //
    //   endOffset:
    //     The end offset for the variable. If this parameter is zero, it is ignored
    //     and the variable is defined throughout the entire scope. If the parameter
    //     is nonzero, the variable falls within the offsets of the current scope.
    void DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset);
    //
    // Summary:
    //     Defines a single parameter in the current method. The type of each parameter
    //     is taken from its position within the signature of the method.
    //
    // Parameters:
    //   name:
    //     The parameter name.
    //
    //   attributes:
    //     A bitwise combination of the parameter attributes.
    //
    //   sequence:
    //     The parameter signature.
    //
    //   addrKind:
    //     The address types for addr1, addr2, and addr3.
    //
    //   addr1:
    //     The first address for the parameter specification.
    //
    //   addr2:
    //     The second address for the parameter specification.
    //
    //   addr3:
    //     The third address for the parameter specification.
    void DefineParameter(string name, ParameterAttributes attributes, int sequence, SymAddressKind addrKind, int addr1, int addr2, int addr3);
    //
    // Summary:
    //     Defines a group of sequence points within the current method.
    //
    // Parameters:
    //   document:
    //     The document object for which the sequence points are being defined.
    //
    //   offsets:
    //     The sequence point offsets measured from the beginning of methods.
    //
    //   lines:
    //     The document lines for the sequence points.
    //
    //   columns:
    //     The document positions for the sequence points.
    //
    //   endLines:
    //     The document end lines for the sequence points.
    //
    //   endColumns:
    //     The document end positions for the sequence points.
    void DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns);
    //
    // Summary:
    //     Sets the metadata emitter interface to associate with a writer.
    //
    // Parameters:
    //   emitter:
    //     The metadata emitter interface.
    //
    //   filename:
    //     The file name for which the debugging symbols are written. Some writers require
    //     a file name, and others do not. If a file name is specified for a writer
    //     that does not use file names, this parameter is ignored.
    //
    //   fFullBuild:
    //     true indicates that this is a full rebuild; false indicates that this is
    //     an incremental compilation.
    void Initialize(IntPtr emitter, string filename, bool fFullBuild);
    //
    // Summary:
    //     Opens a method to place symbol information into.
    //
    // Parameters:
    //   method:
    //     The metadata token for the method to be opened.
    void OpenMethod(SymbolToken method);
    //
    // Summary:
    //     Opens a new namespace.
    //
    // Parameters:
    //   name:
    //     The name of the new namespace.
    void OpenNamespace(string name);
    //
    // Summary:
    //     Opens a new lexical scope in the current method.
    //
    // Parameters:
    //   startOffset:
    //     The offset, in bytes, from the beginning of the method to the first instruction
    //     in the lexical scope.
    //
    // Returns:
    //     An opaque scope identifier that can be used with System.Diagnostics.SymbolStore.ISymbolWriter.SetScopeRange(System.Int32,System.Int32,System.Int32)
    //     to define the start and end offsets of a scope at a later time. In this case,
    //     the offsets passed to System.Diagnostics.SymbolStore.ISymbolWriter.OpenScope(System.Int32)
    //     and System.Diagnostics.SymbolStore.ISymbolWriter.CloseScope(System.Int32)
    //     are ignored. A scope identifier is valid only in the current method.
    int OpenScope(int startOffset);
    //
    // Summary:
    //     Specifies the true start and end of a method within a source file. Use System.Diagnostics.SymbolStore.ISymbolWriter.SetMethodSourceRange(System.Diagnostics.SymbolStore.ISymbolDocumentWriter,System.Int32,System.Int32,System.Diagnostics.SymbolStore.ISymbolDocumentWriter,System.Int32,System.Int32)
    //     to specify the extent of a method, independent of the sequence points that
    //     exist within the method.
    //
    // Parameters:
    //   startDoc:
    //     The document that contains the starting position.
    //
    //   startLine:
    //     The starting line number.
    //
    //   startColumn:
    //     The starting column.
    //
    //   endDoc:
    //     The document that contains the ending position.
    //
    //   endLine:
    //     The ending line number.
    //
    //   endColumn:
    //     The ending column number.
    void SetMethodSourceRange(ISymbolDocumentWriter startDoc, int startLine, int startColumn, ISymbolDocumentWriter endDoc, int endLine, int endColumn);
    //
    // Summary:
    //     Defines the offset range for the specified lexical scope.
    //
    // Parameters:
    //   scopeID:
    //     The identifier of the lexical scope.
    //
    //   startOffset:
    //     The byte offset of the beginning of the lexical scope.
    //
    //   endOffset:
    //     The byte offset of the end of the lexical scope.
    void SetScopeRange(int scopeID, int startOffset, int endOffset);
    //
    // Summary:
    //     Defines an attribute when given the attribute name and the attribute value.
    //
    // Parameters:
    //   parent:
    //     The metadata token for which the attribute is being defined.
    //
    //   name:
    //     The attribute name.
    //
    //   data:
    //     The attribute value.
    void SetSymAttribute(SymbolToken parent, string name, byte[] data);
    //
    // Summary:
    //     Sets the underlying ISymUnmanagedWriter (the corresponding unmanaged interface)
    //     that a managed System.Diagnostics.SymbolStore.ISymbolWriter uses to emit
    //     symbols.
    //
    // Parameters:
    //   underlyingWriter:
    //     A pointer to code that represents the underlying writer.
    void SetUnderlyingWriter(IntPtr underlyingWriter);
    //
    // Summary:
    //     Identifies the user-defined method as the entry point for the current module.
    //
    // Parameters:
    //   entryMethod:
    //     The metadata token for the method that is the user entry point.
    void SetUserEntryPoint(SymbolToken entryMethod);
    //
    // Summary:
    //     Specifies that the given, fully qualified namespace name is used within the
    //     open lexical scope.
    //
    // Parameters:
    //   fullName:
    //     The fully qualified name of the namespace.
    void UsingNamespace(string fullName);
  }
}
