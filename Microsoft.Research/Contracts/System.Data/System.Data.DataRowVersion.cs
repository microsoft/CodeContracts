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

namespace System.Data
{
  // Summary:
  //     Describes the version of a System.Data.DataRow.
  public enum DataRowVersion
  {
    // Summary:
    //     The row contains its original values.
    Original = 256,
    //
    // Summary:
    //     The row contains current values.
    Current = 512,
    //
    // Summary:
    //     The row contains a proposed value.
    Proposed = 1024,
    //
    // Summary:
    //     The default version of System.Data.DataRowState. For a DataRowState value
    //     of Added, Modified or Deleted, the default version is Current. For a System.Data.DataRowState
    //     value of Detached, the version is Proposed.
    Default = 1536,
  }
}
