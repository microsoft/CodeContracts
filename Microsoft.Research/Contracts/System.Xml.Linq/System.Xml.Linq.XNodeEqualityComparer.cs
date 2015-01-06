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
  //     Compares nodes to determine whether they are equal. This class cannot be
  //     inherited.
  public sealed class XNodeEqualityComparer //: IEqualityComparer, IEqualityComparer<XNode>
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XNodeEqualityComparer class.
    extern public XNodeEqualityComparer();

    // Summary:
    //     Compares the values of two nodes.
    //
    // Parameters:
    //   x:
    //     The first System.Xml.Linq.XNode to compare.
    //
    //   y:
    //     The second System.Xml.Linq.XNode to compare.
    //
    // Returns:
    //     A System.Boolean indicating if the nodes are equal.
    // extern public virtual bool Equals(XNode x, XNode y);
    //
    // Summary:
    //     Returns a hash code based on an System.Xml.Linq.XNode.
    //
    // Parameters:
    //   obj:
    //     The System.Xml.Linq.XNode to hash.
    //
    // Returns:
    //     A System.Int32 that contains a value-based hash code for the node.
    // extern public virtual int GetHashCode(XNode obj);
  }
}
