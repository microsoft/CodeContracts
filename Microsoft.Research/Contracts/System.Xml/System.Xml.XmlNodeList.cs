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

#if !SILVERLIGHT

using System;
using System.Collections;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Represents an ordered collection of nodes.
  public abstract class XmlNodeList // : IEnumerable
  {
    

    // Summary:
    //     Gets the number of nodes in the XmlNodeList.
    //
    // Returns:
    //     The number of nodes.
    public virtual int Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }

    // Summary:
    //     Retrieves a node at the given index.
    //
    // Parameters:
    //   i:
    //     Zero-based index into the list of nodes.
    //
    // Returns:
    //     The System.Xml.XmlNode in the collection. If index is greater than or equal
    //     to the number of nodes in the list, this returns null.
    //public virtual XmlNode this[int i] { get; }

    
    //
    // Summary:
    //     Retrieves a node at the given index.
    //
    // Parameters:
    //   index:
    //     Zero-based index into the list of nodes.
    //
    // Returns:
    //     The System.Xml.XmlNode in the collection. If index is greater than or equal
    //     to the number of nodes in the list, this returns null.
    //public abstract XmlNode Item(int index);
  }
}

#endif