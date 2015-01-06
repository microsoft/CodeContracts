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
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Represents an element.
  public class XmlElement //: XmlLinkedNode
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XmlElement class.
    //
    // Parameters:
    //   prefix:
    //     The namespace prefix; see the System.Xml.XmlElement.Prefix property.
    //
    //   localName:
    //     The local name; see the System.Xml.XmlElement.LocalName property.
    //
    //   namespaceURI:
    //     The namespace URI; see the System.Xml.XmlElement.NamespaceURI property.
    //
    //   doc:
    //     The parent XML document.
    //protected internal XmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc);

    // Summary:
    //     Gets an System.Xml.XmlAttributeCollection containing the list of attributes
    //     for this node.
    //
    // Returns:
    //     System.Xml.XmlAttributeCollection containing the list of attributes for this
    //     node.
    //// public override XmlAttributeCollection Attributes { get; }
    //
    // Summary:
    //     Gets a boolean value indicating whether the current node has any attributes.
    //
    // Returns:
    //     true if the current node has attributes; otherwise, false.
    //public virtual bool HasAttributes { get; }
    //
    // Summary:
    //     Gets or sets the concatenated values of the node and all its children.
    //
    // Returns:
    //     The concatenated values of the node and all its children.
    //// public override string InnerText { get; set; }
    //
    // Summary:
    //     Gets or sets the markup representing just the children of this node.
    //
    // Returns:
    //     The markup of the children of this node.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     The XML specified when setting this property is not well-formed.
    //// public override string InnerXml { get; set; }
    //
    // Summary:
    //     Gets or sets the tag format of the element.
    //
    // Returns:
    //     Returns true if the element is to be serialized in the short tag format "<item/>";
    //     false for the long format "<item></item>".  When setting this property, if
    //     set to true, the children of the element are removed and the element is serialized
    //     in the short tag format. If set to false, the value of the property is changed
    //     (regardless of whether or not the element has content); if the element is
    //     empty, it is serialized in the long format.  This property is a Microsoft
    //     extension to the Document Object Model (DOM).
    //public bool IsEmpty { get; set; }
    //
    // Summary:
    //     Gets the local name of the current node.
    //
    // Returns:
    //     The name of the current node with the prefix removed. For example, LocalName
    //     is book for the element <bk:book>.
    //// public override string LocalName { get; }
    //
    // Summary:
    //     Gets the qualified name of the node.
    //
    // Returns:
    //     The qualified name of the node. For XmlElement nodes, this is the tag name
    //     of the element.
    //// public override string Name { get; }
    //
    // Summary:
    //     Gets the namespace URI of this node.
    //
    // Returns:
    //     The namespace URI of this node. If there is no namespace URI, this property
    //     returns String.Empty.
    //// public override string NamespaceURI { get; }
    //
    // Summary:
    //     Gets the System.Xml.XmlNode immediately following this element.
    //
    // Returns:
    //     The XmlNode immediately following this element.
    //// public override XmlNode NextSibling { get; }
    //
    // Summary:
    //     Gets the type of the current node.
    //
    // Returns:
    //     The node type. For XmlElement nodes, this value is XmlNodeType.Element.
    //// public override XmlNodeType NodeType { get; }
    //
    // Summary:
    //     Gets the System.Xml.XmlDocument to which this node belongs.
    //
    // Returns:
    //     The XmlDocument to which this element belongs.
    // public override XmlDocument OwnerDocument { get; }
    // public override XmlNode ParentNode { get; }
    //
    // Summary:
    //     Gets or sets the namespace prefix of this node.
    //
    // Returns:
    //     The namespace prefix of this node. If there is no prefix, this property returns
    //     String.Empty.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     This node is read-only
    //
    //   System.Xml.XmlException:
    //     The specified prefix contains an invalid character.  The specified prefix
    //     is malformed.  The namespaceURI of this node is null.  The specified prefix
    //     is "xml" and the namespaceURI of this node is different from http://www.w3.org/XML/1998/namespace.
    // public override string Prefix { get; set; }
    //
    // Summary:
    //     Gets the post schema validation infoset that has been assigned to this node
    //     as a result of schema validation.
    //
    // Returns:
    //     An System.Xml.Schema.IXmlSchemaInfo object containing the post schema validation
    //     infoset of this node.
    // public override IXmlSchemaInfo SchemaInfo { get; }

    // Summary:
    //     Creates a duplicate of this node.
    //
    // Parameters:
    //   deep:
    //     true to recursively clone the subtree under the specified node; false to
    //     clone only the node itself (and its attributes if the node is an XmlElement).
    //
    // Returns:
    //     The cloned node.
    // public override XmlNode CloneNode(bool deep);
    //
    // Summary:
    //     Returns the value for the attribute with the specified name.
    //
    // Parameters:
    //   name:
    //     The name of the attribute to retrieve. This is a qualified name. It is matched
    //     against the Name property of the matching node.
    //
    // Returns:
    //     The value of the specified attribute. An empty string is returned if a matching
    //     attribute is not found or if the attribute does not have a specified or default
    //     value.
    public virtual string GetAttribute(string name)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Returns the value for the attribute with the specified local name and namespace
    //     URI.
    //
    // Parameters:
    //   localName:
    //     The local name of the attribute to retrieve.
    //
    //   namespaceURI:
    //     The namespace URI of the attribute to retrieve.
    //
    // Returns:
    //     The value of the specified attribute. An empty string is returned if a matching
    //     attribute is not found or if the attribute does not have a specified or default
    //     value.
    public virtual string GetAttribute(string localName, string namespaceURI)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Returns the XmlAttribute with the specified name.
    //
    // Parameters:
    //   name:
    //     The name of the attribute to retrieve. This is a qualified name. It is matched
    //     against the Name property of the matching node.
    //
    // Returns:
    //     The specified XmlAttribute or null if a matching attribute was not found.
    //public virtual XmlAttribute GetAttributeNode(string name);
    //
    // Summary:
    //     Returns the System.Xml.XmlAttribute with the specified local name and namespace
    //     URI.
    //
    // Parameters:
    //   localName:
    //     The local name of the attribute.
    //
    //   namespaceURI:
    //     The namespace URI of the attribute.
    //
    // Returns:
    //     The specified XmlAttribute or null if a matching attribute was not found.
    //public virtual XmlAttribute GetAttributeNode(string localName, string namespaceURI);
    //
    // Summary:
    //     Returns an System.Xml.XmlNodeList containing a list of all descendant elements
    //     that match the specified System.Xml.XmlElement.Name.
    //
    // Parameters:
    //   name:
    //     The name tag to match. This is a qualified name. It is matched against the
    //     Name property of the matching node. The asterisk (*) is a special value that
    //     matches all tags.
    //
    // Returns:
    //     An System.Xml.XmlNodeList containing a list of all matching nodes.
    public virtual XmlNodeList GetElementsByTagName(string name)
    {
      Contract.Ensures(Contract.Result<XmlNodeList>() != null);

      return default(XmlNodeList);
    }
    //
    // Summary:
    //     Returns an System.Xml.XmlNodeList containing a list of all descendant elements
    //     that match the specified System.Xml.XmlElement.LocalName and System.Xml.XmlElement.NamespaceURI.
    //
    // Parameters:
    //   localName:
    //     The local name to match. The asterisk (*) is a special value that matches
    //     all tags.
    //
    //   namespaceURI:
    //     The namespace URI to match.
    //
    // Returns:
    //     An System.Xml.XmlNodeList containing a list of all matching nodes.
    public virtual XmlNodeList GetElementsByTagName(string localName, string namespaceURI)
    {
      Contract.Ensures(Contract.Result<XmlNodeList>() != null);

      return default(XmlNodeList);
    }
    //
    // Summary:
    //     Determines whether the current node has an attribute with the specified name.
    //
    // Parameters:
    //   name:
    //     The name of the attribute to find. This is a qualified name. It is matched
    //     against the Name property of the matching node.
    //
    // Returns:
    //     true if the current node has the specified attribute; otherwise, false.
    //public virtual bool HasAttribute(string name);
    //
    // Summary:
    //     Determines whether the current node has an attribute with the specified local
    //     name and namespace URI.
    //
    // Parameters:
    //   localName:
    //     The local name of the attribute to find.
    //
    //   namespaceURI:
    //     The namespace URI of the attribute to find.
    //
    // Returns:
    //     true if the current node has the specified attribute; otherwise, false.
    //public virtual bool HasAttribute(string localName, string namespaceURI);
    //
    // Summary:
    //     Removes all specified attributes and children of the current node. Default
    //     attributes are not removed.
    // public override void RemoveAll();
    //
    // Summary:
    //     Removes all specified attributes from the element. Default attributes are
    //     not removed.
    //public virtual void RemoveAllAttributes();
    //
    // Summary:
    //     Removes an attribute by name.
    //
    // Parameters:
    //   name:
    //     The name of the attribute to remove.This is a qualified name. It is matched
    //     against the Name property of the matching node.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The node is read-only.
    //public virtual void RemoveAttribute(string name);
    //
    // Summary:
    //     Removes an attribute with the specified local name and namespace URI. (If
    //     the removed attribute has a default value, it is immediately replaced).
    //
    // Parameters:
    //   localName:
    //     The local name of the attribute to remove.
    //
    //   namespaceURI:
    //     The namespace URI of the attribute to remove.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The node is read-only.
    //public virtual void RemoveAttribute(string localName, string namespaceURI);
    //
    // Summary:
    //     Removes the attribute node with the specified index from the element. (If
    //     the removed attribute has a default value, it is immediately replaced).
    //
    // Parameters:
    //   i:
    //     The index of the node to remove. The first node has index 0.
    //
    // Returns:
    //     The attribute node removed or null if there is no node at the given index.
    // F: if i < 0 just returns null
    //public virtual XmlNode RemoveAttributeAt(int i);
    //
    // Summary:
    //     Removes the specified System.Xml.XmlAttribute.
    //
    // Parameters:
    //   oldAttr:
    //     The XmlAttribute node to remove. If the removed attribute has a default value,
    //     it is immediately replaced.
    //
    // Returns:
    //     The removed XmlAttribute or null if oldAttr is not an attribute node of the
    //     XmlElement.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     This node is read-only.
    //public virtual XmlAttribute RemoveAttributeNode(XmlAttribute oldAttr);
    //
    // Summary:
    //     Removes the System.Xml.XmlAttribute specified by the local name and namespace
    //     URI. (If the removed attribute has a default value, it is immediately replaced).
    //
    // Parameters:
    //   localName:
    //     The local name of the attribute.
    //
    //   namespaceURI:
    //     The namespace URI of the attribute.
    //
    // Returns:
    //     The removed XmlAttribute or null if the XmlElement does not have a matching
    //     attribute node.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     This node is read-only.
    //public virtual XmlAttribute RemoveAttributeNode(string localName, string namespaceURI);
    //
    // Summary:
    //     Sets the value of the attribute with the specified name.
    //
    // Parameters:
    //   name:
    //     The name of the attribute to create or alter. This is a qualified name. If
    //     the name contains a colon it is parsed into prefix and local name components.
    //
    //   value:
    //     The value to set for the attribute.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     The specified name contains an invalid character.
    //
    //   System.ArgumentException:
    //     The node is read-only.
    //public virtual void SetAttribute(string name, string value);
    //
    // Summary:
    //     Sets the value of the attribute with the specified local name and namespace
    //     URI.
    //
    // Parameters:
    //   localName:
    //     The local name of the attribute.
    //
    //   namespaceURI:
    //     The namespace URI of the attribute.
    //
    //   value:
    //     The value to set for the attribute.
    //
    // Returns:
    //     The attribute value.
    //public virtual string SetAttribute(string localName, string namespaceURI, string value);
    //
    // Summary:
    //     Adds the specified System.Xml.XmlAttribute.
    //
    // Parameters:
    //   newAttr:
    //     The XmlAttribute node to add to the attribute collection for this element.
    //
    // Returns:
    //     If the attribute replaces an existing attribute with the same name, the old
    //     XmlAttribute is returned; otherwise, null is returned.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The newAttr was created from a different document than the one that created
    //     this node. Or this node is read-only.
    //
    //   System.InvalidOperationException:
    //     The newAttr is already an attribute of another XmlElement object. You must
    //     explicitly clone XmlAttribute nodes to re-use them in other XmlElement objects.
    //public virtual XmlAttribute SetAttributeNode(XmlAttribute newAttr);
    //
    // Summary:
    //     Adds the specified System.Xml.XmlAttribute.
    //
    // Parameters:
    //   localName:
    //     The local name of the attribute.
    //
    //   namespaceURI:
    //     The namespace URI of the attribute.
    //
    // Returns:
    //     The XmlAttribute to add.
    //public virtual XmlAttribute SetAttributeNode(string localName, string namespaceURI);
    //
    // Summary:
    //     Saves all the children of the node to the specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   w:
    //     The XmlWriter to which you want to save.
    // public override void WriteContentTo(XmlWriter w);
    //
    // Summary:
    //     Saves the current node to the specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   w:
    //     The XmlWriter to which you want to save.
    // public override void WriteTo(XmlWriter w);
  }
}

#endif