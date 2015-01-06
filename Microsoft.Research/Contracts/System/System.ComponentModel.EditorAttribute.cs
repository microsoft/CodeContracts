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

namespace System.ComponentModel {
  // Summary:
  //     Specifies the editor to use to change a property. This class cannot be inherited.
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
  public sealed class EditorAttribute : Attribute {
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.EditorAttribute class
    //     with the default editor, which is no editor.
    public EditorAttribute () {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.EditorAttribute class
    //     with the type name and base type name of the editor.
    //
    // Parameters:
    //   typeName:
    //     The fully qualified type name of the editor.
    //
    //   baseTypeName:
    //     The fully qualified type name of the base class or interface to use as a
    //     lookup key for the editor. This class must be or derive from System.Drawing.Design.UITypeEditor.
      return default(EditorAttribute);
    }
    public EditorAttribute (string typeName, string baseTypeName) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.EditorAttribute class
    //     with the type name and the base type.
    //
    // Parameters:
    //   typeName:
    //     The fully qualified type name of the editor.
    //
    //   baseType:
    //     The System.Type of the base class or interface to use as a lookup key for
    //     the editor. This class must be or derive from System.Drawing.Design.UITypeEditor.
      return default(EditorAttribute);
    }
    public EditorAttribute (string typeName, Type baseType) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.EditorAttribute class
    //     with the type and the base type.
    //
    // Parameters:
    //   type:
    //     A System.Type that represents the type of the editor.
    //
    //   baseType:
    //     The System.Type of the base class or interface to use as a lookup key for
    //     the editor. This class must be or derive from System.Drawing.Design.UITypeEditor.
      return default(EditorAttribute);
    }
    public EditorAttribute (Type type, Type baseType) {

    // Summary:
    //     Gets the name of the base class or interface serving as a lookup key for
    //     this editor.
    //
    // Returns:
    //     The name of the base class or interface serving as a lookup key for this
    //     editor.
      return default(EditorAttribute);
    }
    public string EditorBaseTypeName { get; }
    //
    // Summary:
    //     Gets the name of the editor class in the System.Type.AssemblyQualifiedName
    //     format.
    //
    // Returns:
    //     The name of the editor class in the System.Type.AssemblyQualifiedName format.
    public string EditorTypeName { get; }
    //
    // Summary:
    //     Gets a unique ID for this attribute type.
    //
    // Returns:
    //     A unique ID for this attribute type.
    public override object TypeId { get; }

    // Summary:
    //     Returns whether the value of the given object is equal to the current System.ComponentModel.EditorAttribute.
    //
    // Parameters:
    //   obj:
    //     The object to test the value equality of.
    //
    // Returns:
    //     true if the value of the given object is equal to that of the current object;
    //     otherwise, false.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public override bool Equals (object obj) {
      return default(override);
    }
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    public override int GetHashCode () {
      return default(override);
    }
  }
}
