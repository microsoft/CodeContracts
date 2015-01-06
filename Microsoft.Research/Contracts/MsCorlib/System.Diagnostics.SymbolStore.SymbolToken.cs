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
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore {
  // Summary:
  //     The System.Diagnostics.SymbolStore.SymbolToken structure is an object representation
  //     of a token that represents symbolic information.
  [ComVisible(true)]
  public struct SymbolToken {
    //
    // Summary:
    //     Initializes a new instance of the System.Diagnostics.SymbolStore.SymbolToken
    //     structure when given a value.
    //
    // Parameters:
    //   val:
    //     The value to be used for the token.
    public SymbolToken(int val);

    // Summary:
    //     Returns a value indicating whether two System.Diagnostics.SymbolStore.SymbolToken
    //     objects are not equal.
    //
    // Parameters:
    //   a:
    //     A System.Diagnostics.SymbolStore.SymbolToken structure.
    //
    //   b:
    //     A System.Diagnostics.SymbolStore.SymbolToken structure.
    //
    // Returns:
    //     true if a and b are not equal; otherwise, false.
    public static bool operator !=(SymbolToken a, SymbolToken b);
    //
    // Summary:
    //     Returns a value indicating whether two System.Diagnostics.SymbolStore.SymbolToken
    //     objects are equal.
    //
    // Parameters:
    //   a:
    //     A System.Diagnostics.SymbolStore.SymbolToken structure.
    //
    //   b:
    //     A System.Diagnostics.SymbolStore.SymbolToken structure.
    //
    // Returns:
    //     true if a and b are equal; otherwise, false.
    public static bool operator ==(SymbolToken a, SymbolToken b);

    //
    // Summary:
    //     Determines whether obj is equal to this instance.
    //
    // Parameters:
    //   obj:
    //     The System.Diagnostics.SymbolStore.SymbolToken to check.
    //
    // Returns:
    //     true if obj is equal to this instance; otherwise, false.
    public bool Equals(SymbolToken obj);
    //
    // Summary:
    //     Gets the value of the current token.
    //
    // Returns:
    //     The value of the current token.
    public int GetToken();
  }
}

