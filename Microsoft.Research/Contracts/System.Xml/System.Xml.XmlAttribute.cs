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


namespace System.Xml
{
  // Summary:
  //     Represents an attribute. Valid and default values for the attribute are defined
  //     in a document type definition (DTD) or schema.
  public class XmlAttribute //: XmlNode
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XmlAttribute class.
    //
    // Parameters:
    //   prefix:
    //     The namespace prefix.
    //
    //   localName:
    //     The local name of the attribute.
    //
    //   namespaceURI:
    //     The namespace uniform resource identifier (URI).
    //
    //   doc:
    //     The parent XML document.
    //protected internal XmlAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc);

    // Summary:
    //     Gets the base Uniform Resource Identifier (URI) of the node.
    //
    // Returns:
    //     The location from which the node was loaded or String.Empty if the node has
    //     no base URI. Attribute nodes have the same base URI as their owner element.
    //     If an attribute node does not have an owner element, BaseURI returns String.Empty.
    //// public override string BaseURI { get; }
    //
    // Summary:
    //     Gets or sets the concatenated values of the node and all its children.
    //
    // Returns:
    //     The concatenated values of the node and all its children. For attribute nodes,
    //     this property has the same functionality as the System.Xml.XmlAttribute.Value
    //     property.
    //// public override string InnerText { set; }
    //
    // Summary:
    //     Gets or sets the value of the attribute.
    //
    // Returns:
    //     The attribute value.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     The XML specified when setting this property is not well-formed.
    // public override string InnerXml { set; }
    //
    // Summary:
    //     Gets the local name of the node.
    //
    // Returns:
    //     The name of the attribute node with the prefix removed. In the following
    //     example <book bk:genre= 'novel'>, the LocalName of the attribute is genre.
    // public override string LocalName { get; }
    //
    // Summary:
    //     Gets the qualified name of the node.
    //
    // Returns:
    //     The qualified name of the attribute node.
    // public override string Name { get; }
    //
    // Summary:
    //     Gets the namespace URI of this node.
    //
    // Returns:
    //     The namespace URI of this node. If the attribute is not explicitly given
    //     a namespace, this property returns String.Empty.
    // public override string NamespaceURI { get; }
    //
    // Summary:
    //     Gets the type of the current node.
    //
    // Returns:
    //     The node type for XmlAttribute nodes is XmlNodeType.Attribute.
    // public override XmlNodeType NodeType { get; }
    //
    // Summary:
    //     Gets the System.Xml.XmlDocument to which this node belongs.
    //
    // Returns:
    //     An System.Xml.XmlDocument.
    // public override XmlDocument OwnerDocument { get; }
    //
    // Summary:
    //     Gets the System.Xml.XmlElement to which the attribute belongs.
    //
    // Returns:
    //     The XmlElement that the attribute belongs to or null if this attribute is
    //     not part of an XmlElement.
    //public virtual XmlElement OwnerElement { get; }
    //
    // Summary:
    //     Gets the parent of this node. For XmlAttribute nodes, this property always
    //     returns null.
    //
    // Returns:
    //     For XmlAttribute nodes, this property always returns null.
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
    //     This node is read-only.
    //
    //   System.Xml.XmlException:
    //     The specified prefix contains an invalid character.  The specified prefix
    //     is malformed.  The namespaceURI of this node is null.  The specified prefix
    //     is "xml", and the namespaceURI of this node is different from "http://www.w3.org/XML/1998/namespace".
    //      This node is an attribute, the specified prefix is "xmlns", and the namespaceURI
    //     of this node is different from "http://www.w3.org/2000/xmlns/".  This node
    //     is an attribute, and the qualifiedName of this node is "xmlns" [Namespaces].
    // public override string Prefix { get; set; }
    //
    // Summary:
    //     Gets the post-schema-validation-infoset that has been assigned to this node
    //     as a result of schema validation.
    //
    // Returns:
    //     An System.Xml.Schema.IXmlSchemaInfo containing the post-schema-validation-infoset
    //     of this node.
    // public override IXmlSchemaInfo SchemaInfo { get; }
    //
    // Summary:
    //     Gets a value indicating whether the attribute value was explicitly set.
    //
    // Returns:
    //     true if this attribute was explicitly given a value in the original instance
    //     document; otherwise, false. A value of false indicates that the value of
    //     the attribute came from the DTD.
    //public virtual bool Specified { get; }
    //
    // Summary:
    //     Gets or sets the value of the node.
    //
    // Returns:
    //     The value returned depends on the System.Xml.XmlNode.NodeType of the node.
    //     For XmlAttribute nodes, this property is the value of attribute.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The node is read-only and a set operation is called.
    // public override string Value { get; set; }

    // Summary:
    //     Adds the specified node to the end of the list of child nodes, of this node.
    //
    // Parameters:
    //   newChild:
    //     The System.Xml.XmlNode to add.
    //
    // Returns:
    //     The System.Xml.XmlNode added.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This node is of a type that does not allow child nodes of the type of the
    //     newChild node.  The newChild is an ancestor of this node.
    //
    //   System.ArgumentException:
    //     The newChild was created from a different document than the one that created
    //     this node.  This node is read-only.
    // public override XmlNode AppendChild(XmlNode newChild);
    //
    // Summary:
    //     Creates a duplicate of this node.
    //
    // Parameters:
    //   deep:
    //     true to recursively clone the subtree under the specified node; false to
    //     clone only the node itself
    //
    // Returns:
    //     The duplicate node.
    // public override XmlNode CloneNode(bool deep);
    //
    // Summary:
    //     Inserts the specified node immediately after the specified reference node.
    //
    // Parameters:
    //   newChild:
    //     The System.Xml.XmlNode to insert.
    //
    //   refChild:
    //     The System.Xml.XmlNode that is the reference node. The newChild is placed
    //     after the refChild.
    //
    // Returns:
    //     The System.Xml.XmlNode inserted.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This node is of a type that does not allow child nodes of the type of the
    //     newChild node.  The newChild is an ancestor of this node.
    //
    //   System.ArgumentException:
    //     The newChild was created from a different document than the one that created
    //     this node.  The refChild is not a child of this node.  This node is read-only.
    // public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild);
    //
    // Summary:
    //     Inserts the specified node immediately before the specified reference node.
    //
    // Parameters:
    //   newChild:
    //     The System.Xml.XmlNode to insert.
    //
    //   refChild:
    //     The System.Xml.XmlNode that is the reference node. The newChild is placed
    //     before this node.
    //
    // Returns:
    //     The System.Xml.XmlNode inserted.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The current node is of a type that does not allow child nodes of the type
    //     of the newChild node.  The newChild is an ancestor of this node.
    //
    //   System.ArgumentException:
    //     The newChild was created from a different document than the one that created
    //     this node.  The refChild is not a child of this node.  This node is read-only.
    // public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild);
    //
    // Summary:
    //     Adds the specified node to the beginning of the list of child nodes for this
    //     node.
    //
    // Parameters:
    //   newChild:
    //     The System.Xml.XmlNode to add. If it is an System.Xml.XmlDocumentFragment,
    //     the entire contents of the document fragment are moved into the child list
    //     of this node.
    //
    // Returns:
    //     The System.Xml.XmlNode added.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This node is of a type that does not allow child nodes of the type of the
    //     newChild node.  The newChild is an ancestor of this node.
    //
    //   System.ArgumentException:
    //     The newChild was created from a different document than the one that created
    //     this node.  This node is read-only.
    // public override XmlNode PrependChild(XmlNode newChild);
    //
    // Summary:
    //     Removes the specified child node.
    //
    // Parameters:
    //   oldChild:
    //     The System.Xml.XmlNode to remove.
    //
    // Returns:
    //     The System.Xml.XmlNode removed.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The oldChild is not a child of this node. Or this node is read-only.
    // public override XmlNode RemoveChild(XmlNode oldChild);
    //
    // Summary:
    //     Replaces the child node specified with the new child node specified.
    //
    // Parameters:
    //   newChild:
    //     The new child System.Xml.XmlNode.
    //
    //   oldChild:
    //     The System.Xml.XmlNode to replace.
    //
    // Returns:
    //     The System.Xml.XmlNode replaced.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This node is of a type that does not allow child nodes of the type of the
    //     newChild node.  The newChild is an ancestor of this node.
    //
    //   System.ArgumentException:
    //     The newChild was created from a different document than the one that created
    //     this node.  This node is read-only.  The oldChild is not a child of this
    //     node.
    // public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild);
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
    //     Saves the node to the specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   w:
    //     The XmlWriter to which you want to save.
    // public override void WriteTo(XmlWriter w);
  }
}

#endif