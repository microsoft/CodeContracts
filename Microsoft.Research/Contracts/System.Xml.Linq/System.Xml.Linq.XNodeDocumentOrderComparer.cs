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
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Linq
{
  // Summary:
  //     Contains functionality to compare nodes for their document order. This class
  //     cannot be inherited.
  public sealed class XNodeDocumentOrderComparer //: IComparer, IComparer<XNode>
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XNodeDocumentOrderComparer
    //     class.
    extern public XNodeDocumentOrderComparer();

    // Summary:
    //     Compares two nodes to determine their relative document order.
    //
    // Parameters:
    //   x:
    //     The first System.Xml.Linq.XNode to compare.
    //
    //   y:
    //     The second System.Xml.Linq.XNode to compare.
    //
    // Returns:
    //     An System.Int32 that contains 0 if the nodes are equal; -1 if x is before
    //     y; 1 if x is after y.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The two nodes do not share a common ancestor.
    // extern public virtual int Compare(XNode x, XNode y);
  }
}
