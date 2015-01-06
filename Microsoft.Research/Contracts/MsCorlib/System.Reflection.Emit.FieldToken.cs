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

namespace System.Reflection.Emit {
  // Summary:
  //     The FieldToken struct is an object representation of a token that represents
  //     a field.
  [Serializable]
  [ComVisible(true)]
  public struct FieldToken {
    // Summary:
    //     The default FieldToken with System.Reflection.Emit.FieldToken.Token value
    //     0.
    public static readonly FieldToken Empty;

    // Summary:
    //     Indicates whether two System.Reflection.Emit.FieldToken structures are not
    //     equal.
    //
    // Parameters:
    //   a:
    //     The System.Reflection.Emit.FieldToken to compare to b.
    //
    //   b:
    //     The System.Reflection.Emit.FieldToken to compare to a.
    //
    // Returns:
    //     true if a is not equal to b; otherwise, false.
    extern public static bool operator !=(FieldToken a, FieldToken b);
    //
    // Summary:
    //     Indicates whether two System.Reflection.Emit.FieldToken structures are equal.
    //
    // Parameters:
    //   a:
    //     The System.Reflection.Emit.FieldToken to compare to b.
    //
    //   b:
    //     The System.Reflection.Emit.FieldToken to compare to a.
    //
    // Returns:
    //     true if a is equal to b; otherwise, false.
    extern public static bool operator ==(FieldToken a, FieldToken b);

    // Summary:
    //     Retrieves the metadata token for this field.
    //
    // Returns:
    //     Read-only. Retrieves the metadata token of this field.
    public int Token { get { return default(int); } }

    // Summary:
    //     Indicates whether the current instance is equal to the specified System.Reflection.Emit.FieldToken.
    //
    // Parameters:
    //   obj:
    //     The System.Reflection.Emit.FieldToken to compare to the current instance.
    //
    // Returns:
    //     true if the value of obj is equal to the value of the current instance; otherwise,
    //     false.
    extern public bool Equals(FieldToken obj);
  }
}
