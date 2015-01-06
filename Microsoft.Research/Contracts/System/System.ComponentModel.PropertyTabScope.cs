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

using System.Diagnostics.Contracts;
using System;

namespace System.ComponentModel {
  // Summary:
  //     Defines identifiers that indicate the persistence scope of a tab in the Properties
  //     window.
  public enum PropertyTabScope {
    // Summary:
    //     This tab is added to the Properties window and cannot be removed.
    Static = 0,
    //
    // Summary:
    //     This tab is added to the Properties window and can only be removed explicitly
    //     by a parent component.
    Global = 1,
    //
    // Summary:
    //     This tab is specific to the current document. This tab is added to the Properties
    //     window and is removed when the currently selected document changes.
    Document = 2,
    //
    // Summary:
    //     This tab is specific to the current component. This tab is added to the Properties
    //     window for the current component only and is removed when the component is
    //     no longer selected.
    Component = 3,
  }
}
