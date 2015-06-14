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

namespace System.Runtime.Remoting.Contexts {
  // Summary:
  //     Gathers naming information from the context property and determines whether
  //     the new context is ok for the context property.
  [ComVisible(true)]
  public interface IContextProperty {
    // Summary:
    //     Gets the name of the property under which it will be added to the context.
    //
    // Returns:
    //     The name of the property.
    string Name { get; }

    // Summary:
    //     Called when the context is frozen.
    //
    // Parameters:
    //   newContext:
    //     The context to freeze.
    void Freeze(Context newContext);
    //
    // Summary:
    //     Returns a Boolean value indicating whether the context property is compatible
    //     with the new context.
    //
    // Parameters:
    //   newCtx:
    //     The new context in which the System.Runtime.Remoting.Contexts.ContextProperty
    //     has been created.
    //
    // Returns:
    //     true if the context property can coexist with the other context properties
    //     in the given context; otherwise, false.
    bool IsNewContextOK(Context newCtx);
  }
}
