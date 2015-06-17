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
  //     Represents the abstract concept of a node (one of: element, comment, document
  //     type, processing instruction, or text node) in the XML tree.
  public abstract class XNode : XObject
  {
    extern internal XNode();

    // Summary:
    //     Gets a comparer that can compare the relative position of two nodes.
    //
    // Returns:
    //     A System.Xml.Linq.XNodeDocumentOrderComparer that can compare the relative
    //     position of two nodes.
    public static XNodeDocumentOrderComparer DocumentOrderComparer
    {
      get
      {
        Contract.Ensures(Contract.Result<XNodeDocumentOrderComparer>() != null);
        return default(XNodeDocumentOrderComparer);
      }
    }
    //
    // Summary:
    //     Gets a comparer that can compare two nodes for value equality.
    //
    // Returns:
    //     A System.Xml.Linq.XNodeEqualityComparer that can compare two nodes for value
    //     equality.
    public static XNodeEqualityComparer EqualityComparer
    {
      get
      {
        Contract.Ensures(Contract.Result<XNodeEqualityComparer>() != null);
        return default(XNodeEqualityComparer);
      }
    }
    //
    // Summary:
    //     Gets the next sibling node of this node.
    //
    // Returns:
    //     The System.Xml.Linq.XNode that contains the next sibling node.
    extern public XNode NextNode { get; }
    //
    // Summary:
    //     Gets the previous sibling node of this node.
    //
    // Returns:
    //     The System.Xml.Linq.XNode that contains the previous sibling node.
    extern public XNode PreviousNode { get; }

    // Summary:
    //     Adds the specified content immediately after this node.
    //
    // Parameters:
    //   content:
    //     A content object that contains simple content or a collection of content
    //     objects to be added after this node.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The parent is null.
    extern public void AddAfterSelf(object content);
    //
    // Summary:
    //     Adds the specified content immediately after this node.
    //
    // Parameters:
    //   content:
    //     A parameter list of content objects.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The parent is null.
    extern public void AddAfterSelf(params object[] content);
    //
    // Summary:
    //     Adds the specified content immediately before this node.
    //
    // Parameters:
    //   content:
    //     A content object that contains simple content or a collection of content
    //     objects to be added before this node.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The parent is null.
    extern public void AddBeforeSelf(object content);
    //
    // Summary:
    //     Adds the specified content immediately before this node.
    //
    // Parameters:
    //   content:
    //     A parameter list of content objects.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The parent is null.
    extern public void AddBeforeSelf(params object[] content);
    //
    // Summary:
    //     Returns a collection of the ancestor elements of this node.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     of the ancestor elements of this node.
    [Pure]
    public IEnumerable<XElement> Ancestors()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Returns a filtered collection of the ancestor elements of this node. Only
    //     elements that have a matching System.Xml.Linq.XName are included in the collection.
    //
    // Parameters:
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     of the ancestor elements of this node. Only elements that have a matching
    //     System.Xml.Linq.XName are included in the collection.  The nodes in the returned
    //     collection are in reverse document order.  This method uses deferred execution.
    [Pure]
    public IEnumerable<XElement> Ancestors(XName name)
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }

    //
    // Summary:
    //     Compares two nodes to determine their relative XML document order.
    //
    // Parameters:
    //   n1:
    //     First System.Xml.Linq.XNode to compare.
    //
    //   n2:
    //     Second System.Xml.Linq.XNode to compare.
    //
    // Returns:
    //     An int containing 0 if the nodes are equal; -1 if n1 is before n2; 1 if n1
    //     is after n2.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The two nodes do not share a common ancestor.
    [Pure]
    extern public static int CompareDocumentOrder(XNode n1, XNode n2);
    //
    // Summary:
    //     Creates an System.Xml.XmlReader for this node.
    //
    // Returns:
    //     An System.Xml.XmlReader that can be used to read this node and its descendants.
    public XmlReader CreateReader()
    {
      Contract.Ensures(Contract.Result<XmlReader>() != null);
      return default(XmlReader);
    }
    //
    // Summary:
    //     Compares the values of two nodes, including the values of all descendant
    //     nodes.
    //
    // Parameters:
    //   n1:
    //     The first System.Xml.Linq.XNode to compare.
    //
    //   n2:
    //     The second System.Xml.Linq.XNode to compare.
    //
    // Returns:
    //     true if the nodes are equal; otherwise false.
    [Pure]
    extern public static bool DeepEquals(XNode n1, XNode n2);
    //
    // Summary:
    //     Returns a collection of the sibling elements after this node, in document
    //     order.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     of the sibling elements after this node, in document order.
    [Pure]
    public IEnumerable<XElement> ElementsAfterSelf()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Returns a filtered collection of the sibling elements after this node, in
    //     document order. Only elements that have a matching System.Xml.Linq.XName
    //     are included in the collection.
    //
    // Parameters:
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     of the sibling elements after this node, in document order. Only elements
    //     that have a matching System.Xml.Linq.XName are included in the collection.
    [Pure]
    public IEnumerable<XElement> ElementsAfterSelf(XName name)
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Returns a collection of the sibling elements before this node, in document
    //     order.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     of the sibling elements before this node, in document order.
    [Pure]
    public IEnumerable<XElement> ElementsBeforeSelf()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Returns a filtered collection of the sibling elements before this node, in
    //     document order. Only elements that have a matching System.Xml.Linq.XName
    //     are included in the collection.
    //
    // Parameters:
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     of the sibling elements before this node, in document order. Only elements
    //     that have a matching System.Xml.Linq.XName are included in the collection.
    [Pure]
    public IEnumerable<XElement> ElementsBeforeSelf(XName name)
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Determines if the current node appears after a specified node in terms of
    //     document order.
    //
    // Parameters:
    //   node:
    //     The System.Xml.Linq.XNode to compare for document order.
    //
    // Returns:
    //     true if this node appears after the specified node; otherwise false.
    [Pure]
    extern public bool IsAfter(XNode node);
    //
    // Summary:
    //     Determines if the current node appears before a specified node in terms of
    //     document order.
    //
    // Parameters:
    //   node:
    //     The System.Xml.Linq.XNode to compare for document order.
    //
    // Returns:
    //     true if this node appears before the specified node; otherwise false.
    [Pure]
    extern public bool IsBefore(XNode node);
    //
    // Summary:
    //     Returns a collection of the sibling nodes after this node, in document order.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode of
    //     the sibling nodes after this node, in document order.
    [Pure]
    public IEnumerable<XNode> NodesAfterSelf()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XNode>>() != null);
      return default(IEnumerable<XNode>);
    }
    //
    // Summary:
    //     Returns a collection of the sibling nodes before this node, in document order.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode of
    //     the sibling nodes before this node, in document order.
    [Pure]
    public IEnumerable<XNode> NodesBeforeSelf()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XNode>>() != null);
      return default(IEnumerable<XNode>);
    }
    //
    // Summary:
    //     Creates an System.Xml.Linq.XNode from an System.Xml.XmlReader.
    //
    // Parameters:
    //   reader:
    //     An System.Xml.XmlReader positioned at the node to read into this System.Xml.Linq.XNode.
    //
    // Returns:
    //     An System.Xml.Linq.XNode that contains the node and its descendant nodes
    //     that were read from the reader. The runtime type of the node is determined
    //     by the node type (System.Xml.Linq.XObject.NodeType) of the first node encountered
    //     in the reader.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Xml.XmlReader is not positioned on a recognized node type.
    //
    //   System.Xml.XmlException:
    //     The underlying System.Xml.XmlReader throws an exception.
    public static XNode ReadFrom(XmlReader reader)
    {
      Contract.Requires(reader != null);
      Contract.Ensures(Contract.Result<XNode>() != null);
      return default(XNode);
    }
    //
    // Summary:
    //     Removes this node from its parent.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The parent is null.
    extern public void Remove();
    //
    // Summary:
    //     Replaces this node with the specified content.
    //
    // Parameters:
    //   content:
    //     Content that replaces this node.
    extern public void ReplaceWith(object content);
    //
    // Summary:
    //     Replaces this node with the specified content.
    //
    // Parameters:
    //   content:
    //     A parameter list of the new content.
    extern public void ReplaceWith(params object[] content);
    //
    // Summary:
    //     Returns the XML for this node, optionally disabling formatting.
    //
    // Parameters:
    //   options:
    //     A System.Xml.Linq.SaveOptions that specifies formatting behavior.
    //
    // Returns:
    //     A System.String containing the XML.
    [Pure]
    public string ToString(SaveOptions options)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Writes this node to an System.Xml.XmlWriter.
    //
    // Parameters:
    //   writer:
    //     An System.Xml.XmlWriter into which this method will write.
    public virtual void WriteTo(XmlWriter writer)
    {
      Contract.Requires(writer != null);
    }
  }
}
