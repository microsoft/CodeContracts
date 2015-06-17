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
using System.Diagnostics.Contracts;

namespace System.Xml.XPath
{
  // Summary:
  //     Provides an iterator over a selected set of nodes.
  //[DebuggerDisplay("Position={CurrentPosition}, Current={debuggerDisplayProxy}")]
  public abstract class XPathNodeIterator // : ICloneable, IEnumerable
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XPath.XPathNodeIterator class.
    //protected XPathNodeIterator();

    // Summary:
    //     Gets the index of the last node in the selected set of nodes.
    //
    // Returns:
    //     The int index of the last node in the selected set of nodes, or 0 if there
    //     are no selected nodes.
    public virtual int Count 
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }
    //
    // Summary:
    //     When overridden in a derived class, returns the System.Xml.XPath.XPathNavigator
    //     object for this System.Xml.XPath.XPathNodeIterator, positioned on the current
    //     context node.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNavigator object positioned on the context node
    //     from which the node set was selected. The System.Xml.XPath.XPathNodeIterator.MoveNext()
    //     method must be called to move the System.Xml.XPath.XPathNodeIterator to the
    //     first node in the selected set.
    //public abstract XPathNavigator Current { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets the index of the current position
    //     in the selected set of nodes.
    //
    // Returns:
    //     The int index of the current position.
    // public /*abstract*/ int CurrentPosition

    // Summary:
    //     When overridden in a derived class, returns a clone of this System.Xml.XPath.XPathNodeIterator
    //     object.
    //
    // Returns:
    //     A new System.Xml.XPath.XPathNodeIterator object clone of this System.Xml.XPath.XPathNodeIterator
    //     object.
    //public /*abstract*/ XPathNodeIterator Clone()
    //
    // Summary:
    //     Returns an System.Collections.IEnumerator object to iterate through the selected
    //     node set.
    //
    // Returns:
    //     An System.Collections.IEnumerator object to iterate through the selected
    //     node set.
    //public virtual IEnumerator GetEnumerator();
    //
    // Summary:
    //     When overridden in a derived class, moves the System.Xml.XPath.XPathNavigator
    //     object returned by the System.Xml.XPath.XPathNodeIterator.Current property
    //     to the next node in the selected node set.
    //
    // Returns:
    //     true if the System.Xml.XPath.XPathNavigator object moved to the next node;
    //     false if there are no more selected nodes.
    // public abstract bool MoveNext();
  }
}

#endif