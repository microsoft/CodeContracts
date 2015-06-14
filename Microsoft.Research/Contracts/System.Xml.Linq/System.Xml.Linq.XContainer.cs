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
using System.Xml;
using System.Diagnostics.Contracts;

namespace System.Xml.Linq
{
  // Summary:
  //     Represents a node that can contain other nodes.
  public abstract class XContainer : XNode
  {
    extern internal XContainer();

    // Summary:
    //     Get the first child node of this node.
    //
    // Returns:
    //     An System.Xml.Linq.XNode containing the first child node of the System.Xml.Linq.XContainer.
    extern public XNode FirstNode { get; }
    //
    // Summary:
    //     Get the last child node of this node.
    //
    // Returns:
    //     An System.Xml.Linq.XNode containing the last child node of the System.Xml.Linq.XContainer.
    extern public XNode LastNode { get; }

    // Summary:
    //     Adds the specified content as children of this System.Xml.Linq.XContainer.
    //
    // Parameters:
    //   content:
    //     A content object containing simple content or a collection of content objects
    //     to be added.
    extern public void Add(object content);
    //
    // Summary:
    //     Adds the specified content as children of this System.Xml.Linq.XContainer.
    //
    // Parameters:
    //   content:
    //     A parameter list of content objects.
    extern public void Add(params object[] content);
    //
    // Summary:
    //     Adds the specified content as the first children of this document or element.
    //
    // Parameters:
    //   content:
    //     A content object containing simple content or a collection of content objects
    //     to be added.
    extern public void AddFirst(object content);
    //
    // Summary:
    //     Adds the specified content as the first children of this document or element.
    //
    // Parameters:
    //   content:
    //     A parameter list of content objects.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The parent is null.
    extern public void AddFirst(params object[] content);
    //
    // Summary:
    //     Creates an System.Xml.XmlWriter that can be used to add nodes to the System.Xml.Linq.XContainer.
    //
    // Returns:
    //     An System.Xml.XmlWriter that is ready to have content written to it.
    public XmlWriter CreateWriter()
    {
      Contract.Ensures(Contract.Result<XmlWriter>() != null);
      return default(XmlWriter);
    }
    //
    // Summary:
    //     Returns a collection of the descendant nodes for this document or element,
    //     in document order.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode containing
    //     the descendant nodes of the System.Xml.Linq.XContainer, in document order.
    [Pure]
    public IEnumerable<XNode> DescendantNodes()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XNode>>() != null);
      return default(IEnumerable<XNode>);
    }
    //
    // Summary:
    //     Returns a collection of the descendant elements for this document or element,
    //     in document order.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     containing the descendant elements of the System.Xml.Linq.XContainer.
    [Pure]
    public IEnumerable<XElement> Descendants()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Returns a filtered collection of the descendant elements for this document
    //     or element, in document order. Only elements that have a matching System.Xml.Linq.XName
    //     are included in the collection.
    //
    // Parameters:
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     containing the descendant elements of the System.Xml.Linq.XContainer that
    //     match the specified System.Xml.Linq.XName.
    [Pure]
    public IEnumerable<XElement> Descendants(XName name)
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Gets the first (in document order) child element with the specified System.Xml.Linq.XName.
    //
    // Parameters:
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     A System.Xml.Linq.XElement that matches the specified System.Xml.Linq.XName,
    //     or null.
    [Pure]
    extern public XElement Element(XName name);
    //
    // Summary:
    //     Returns a collection of the child elements of this element or document, in
    //     document order.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     containing the child elements of this System.Xml.Linq.XContainer, in document
    //     order.
    [Pure]
    public IEnumerable<XElement> Elements()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Returns a filtered collection of the child elements of this element or document,
    //     in document order. Only elements that have a matching System.Xml.Linq.XName
    //     are included in the collection.
    //
    // Parameters:
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     containing the children of the System.Xml.Linq.XContainer that have a matching
    //     System.Xml.Linq.XName, in document order.
    [Pure]
    public IEnumerable<XElement> Elements(XName name)
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Returns a collection of the child nodes of this element or document, in document
    //     order.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode containing
    //     the contents of this System.Xml.Linq.XContainer, in document order.
    [Pure]
    public IEnumerable<XNode> Nodes()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XNode>>() != null);
      return default(IEnumerable<XNode>);
    }
    //
    // Summary:
    //     Removes the child nodes from this document or element.
    extern public void RemoveNodes();
    //
    // Summary:
    //     Replaces the children nodes of this document or element with the specified
    //     content.
    //
    // Parameters:
    //   content:
    //     A content object containing simple content or a collection of content objects
    //     that replace the children nodes.
    extern public void ReplaceNodes(object content);
    //
    // Summary:
    //     Replaces the children nodes of this document or element with the specified
    //     content.
    //
    // Parameters:
    //   content:
    //     A parameter list of content objects.
    extern public void ReplaceNodes(params object[] content);
  }
}