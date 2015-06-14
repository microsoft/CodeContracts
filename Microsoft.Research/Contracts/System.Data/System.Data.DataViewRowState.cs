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
using System.ComponentModel;

namespace System.Data
{
  // Summary:
  //     Describes the version of data in a System.Data.DataRow.
  [Flags]
  //[Editor("Microsoft.VSDesigner.Data.Design.DataViewRowStateEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  public enum DataViewRowState
  {
    // Summary:
    //     None.
    None = 0,
    //
    // Summary:
    //     An unchanged row.
    Unchanged = 2,
    //
    // Summary:
    //     A new row.
    Added = 4,
    //
    // Summary:
    //     A deleted row.
    Deleted = 8,
    //
    // Summary:
    //     A current version of original data that has been modified (see ModifiedOriginal).
    ModifiedCurrent = 16,
    //
    // Summary:
    //     Current rows including unchanged, new, and modified rows.
    CurrentRows = 22,
    //
    // Summary:
    //     The original version of the data that was modified. (Although the data has
    //     since been modified, it is available as ModifiedCurrent).
    ModifiedOriginal = 32,
    //
    // Summary:
    //     Original rows including unchanged and deleted rows.
    OriginalRows = 42,
  }
}
