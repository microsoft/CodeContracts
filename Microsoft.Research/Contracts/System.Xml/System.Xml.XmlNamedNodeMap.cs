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

namespace System.Xml
{
  // Summary:
  //     Represents a collection of nodes that can be accessed by name or index.
  public class XmlNamedNodeMap //: IEnumerable
  {
    // Summary:
    //     Gets the number of nodes in the XmlNamedNodeMap.
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

    //
    // Summary:
    //     Retrieves an System.Xml.XmlNode specified by name.
    //
    // Parameters:
    //   name:
    //     The qualified name of the node to retrieve. It is matched against the System.Xml.XmlNode.Name
    //     property of the matching node.
    //
    // Returns:
    //     An XmlNode with the specified name or null if a matching node is not found.
    [Pure]
    public virtual XmlNode GetNamedItem(string name)
    {
      return default(XmlNode);
    }
    //
    // Summary:
    //     Retrieves a node with the matching System.Xml.XmlNode.LocalName and System.Xml.XmlNode.NamespaceURI.
    //
    // Parameters:
    //   localName:
    //     The local name of the node to retrieve.
    //
    //   namespaceURI:
    //     The namespace Uniform Resource Identifier (URI) of the node to retrieve.
    //
    // Returns:
    //     An System.Xml.XmlNode with the matching local name and namespace URI or null
    //     if a matching node was not found.
    [Pure]
    public virtual XmlNode GetNamedItem(string localName, string namespaceURI)
    {
      return default(XmlNode);
    }
    //
    // Summary:
    //     Retrieves the node at the specified index in the XmlNamedNodeMap.
    //
    // Parameters:
    //   index:
    //     The index position of the node to retrieve from the XmlNamedNodeMap. The
    //     index is zero-based; therefore, the index of the first node is 0 and the
    //     index of the last node is System.Xml.XmlNamedNodeMap.Count -1.
    //
    // Returns:
    //     The System.Xml.XmlNode at the specified index. If index is less than 0 or
    //     greater than or equal to the System.Xml.XmlNamedNodeMap.Count property, null
    //     is returned.
    // F: relatively complex postcondition here.
    [Pure]
    public virtual XmlNode Item(int index)
    {
      return default(XmlNode);
    }
    //
    // Summary:
    //     Removes the node from the XmlNamedNodeMap.
    //
    // Parameters:
    //   name:
    //     The qualified name of the node to remove. The name is matched against the
    //     System.Xml.XmlNode.Name property of the matching node.
    //
    // Returns:
    //     The XmlNode removed from this XmlNamedNodeMap or null if a matching node
    //     was not found.
    //public virtual XmlNode RemoveNamedItem(string name);
    //
    // Summary:
    //     Removes a node with the matching System.Xml.XmlNode.LocalName and System.Xml.XmlNode.NamespaceURI.
    //
    // Parameters:
    //   localName:
    //     The local name of the node to remove.
    //
    //   namespaceURI:
    //     The namespace URI of the node to remove.
    //
    // Returns:
    //     The System.Xml.XmlNode removed or null if a matching node was not found.
    //public virtual XmlNode RemoveNamedItem(string localName, string namespaceURI);
    //
    // Summary:
    //     Adds an System.Xml.XmlNode using its System.Xml.XmlNode.Name property
    //
    // Parameters:
    //   node:
    //     An XmlNode to store in the XmlNamedNodeMap. If a node with that name is already
    //     present in the map, it is replaced by the new one.
    //
    // Returns:
    //     If the node replaces an existing node with the same name, the old node is
    //     returned; otherwise, null is returned.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The node was created from a different System.Xml.XmlDocument than the one
    //     that created the XmlNamedNodeMap; or the XmlNamedNodeMap is read-only.
    //public virtual XmlNode SetNamedItem(XmlNode node);
  }
}
#endif
