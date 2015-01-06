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
using System.Reflection;
using System.Xml.XPath;
using System.Xml.Schema;

namespace System.Xml
{
  // Summary:
  //     Represents a single node in the XML document.
  // [DebuggerDisplay("{debuggerDisplayProxy}")]
  public abstract class XmlNode // : ICloneable, IEnumerable, IXPathNavigable
  {
    internal XmlNode() { }

    public virtual XmlNode AppendChild(XmlNode child)
    {
      Contract.Requires(child != null);
      Contract.Ensures(Contract.Result<XmlNode>() == child);
      return child;
    }

    // Summary:
    //     Gets an System.Xml.XmlAttributeCollection containing the attributes of this
    //     node.
    //
    // Returns:
    //     An XmlAttributeCollection containing the attributes of the node.  If the
    //     node is of type XmlNodeType.Element, the attributes of the node are returned.
    //     Otherwise, this property returns null.
    //public virtual XmlAttributeCollection Attributes { get; }
    //
    // Summary:
    //     Gets the base URI of the current node.
    //
    // Returns:
    //     The location from which the node was loaded or String.Empty if the node has
    //     no base URI.
    public virtual string BaseURI 
    { 
      get
    {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
    }
    }
    //
    // Summary:
    //     Gets all the child nodes of the node.
    //
    // Returns:
    //     An System.Xml.XmlNodeList that contains all the child nodes of the node.
    //      If there are no child nodes, this property returns an empty System.Xml.XmlNodeList.
    public virtual XmlNodeList ChildNodes 
    { 
      get 
      {
        Contract.Ensures(Contract.Result<XmlNodeList>() != null);

        return default(XmlNodeList);
      }
    }
    //
    // Summary:
    //     Gets the first child of the node.
    //
    // Returns:
    //     The first child of the node. If there is no such node, null is returned.
    //public virtual XmlNode FirstChild { get; }
    //
    // Summary:
    //     Gets a value indicating whether this node has any child nodes.
    //
    // Returns:
    //     true if the node has child nodes; otherwise, false.
    //public virtual bool HasChildNodes { get; }
    //
    // Summary:
    //     Gets or sets the concatenated values of the node and all its child nodes.
    //
    // Returns:
    //     The concatenated values of the node and all its child nodes.
    public virtual string InnerText
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      // set; value can be null.
    }

    public virtual string InnerXml
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set {
        Contract.Requires(value != null);
      }
    }

    //
    // Summary:
    //     Gets a value indicating whether the node is read-only.
    //
    // Returns:
    //     true if the node is read-only; otherwise false.
    //public virtual bool IsReadOnly { get; }
    //
    // Summary:
    //     Gets the last child of the node.
    //
    // Returns:
    //     The last child of the node. If there is no such node, null is returned.
    //public virtual XmlNode LastChild { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets the local name of the node.
    //
    // Returns:
    //     The name of the node with the prefix removed. For example, LocalName is book
    //     for the element <bk:book>.  The name returned is dependent on the System.Xml.XmlNode.NodeType
    //     of the node: Type Name Attribute The local name of the attribute. CDATA #cdata-section
    //     Comment #comment Document #document DocumentFragment #document-fragment DocumentType
    //     The document type name. Element The local name of the element. Entity The
    //     name of the entity. EntityReference The name of the entity referenced. Notation
    //     The notation name. ProcessingInstruction The target of the processing instruction.
    //     Text #text Whitespace #whitespace SignificantWhitespace #significant-whitespace
    //     XmlDeclaration #xml-declaration
    public virtual string LocalName 
    { 
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
    
    //
    // Summary:
    //     When overridden in a derived class, gets the qualified name of the node.
    //
    // Returns:
    //     The qualified name of the node. The name returned is dependent on the System.Xml.XmlNode.NodeType
    //     of the node: Type Name Attribute The qualified name of the attribute. CDATA
    //     #cdata-section Comment #comment Document #document DocumentFragment #document-fragment
    //     DocumentType The document type name. Element The qualified name of the element.
    //     Entity The name of the entity. EntityReference The name of the entity referenced.
    //     Notation The notation name. ProcessingInstruction The target of the processing
    //     instruction. Text #text Whitespace #whitespace SignificantWhitespace #significant-whitespace
    //     XmlDeclaration #xml-declaration
    public virtual string Name 
    { 
      get
    {
      Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
    }
    }

    
    //
    // Summary:
    //     Gets the namespace URI of this node.
    //
    // Returns:
    //     The namespace URI of this node. If there is no namespace URI, this property
    //     returns String.Empty.
    public virtual string NamespaceURI 
    { 
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    //
    // Summary:
    //     Gets the node immediately following this node.
    //
    // Returns:
    //     The next XmlNode. If there is no next node, null is returned.
    //public virtual XmlNode NextSibling { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets the type of the current node.
    //
    // Returns:
    //     One of the System.Xml.XmlNodeType values.
    //public abstract XmlNodeType NodeType { get; }
    //
    // Summary:
    //     Gets the markup representing this node and all its child nodes.
    //
    // Returns:
    //     The markup containing this node and all its child nodes.  Note:OuterXml does
    //     not return default attributes.
    public virtual string OuterXml {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    //
    // Summary:
    //     Gets the System.Xml.XmlDocument to which this node belongs.
    //
    // Returns:
    //     The System.Xml.XmlDocument to which this node belongs.  If the node is an
    //     System.Xml.XmlDocument (NodeType equals XmlNodeType.Document), this property
    //     returns null.
    //public virtual XmlDocument OwnerDocument { get; }
    //
    // Summary:
    //     Gets the parent of this node (for nodes that can have parents).
    //
    // Returns:
    //     The XmlNode that is the parent of the current node. If a node has just been
    //     created and not yet added to the tree, or if it has been removed from the
    //     tree, the parent is null. For all other nodes, the value returned depends
    //     on the System.Xml.XmlNode.NodeType of the node. The following table describes
    //     the possible return values for the ParentNode property.  NodeType Return
    //     Value of ParentNode Attribute, Document, DocumentFragment, Entity, Notation
    //     Returns null; these nodes do not have parents. CDATA Returns the element
    //     or entity reference containing the CDATA section. Comment Returns the element,
    //     entity reference, document type, or document containing the comment. DocumentType
    //     Returns the document node. Element Returns the parent node of the element.
    //     If the element is the root node in the tree, the parent is the document node.
    //     EntityReference Returns the element, attribute, or entity reference containing
    //     the entity reference. ProcessingInstruction Returns the document, element,
    //     document type, or entity reference containing the processing instruction.
    //     Text Returns the parent element, attribute, or entity reference containing
    //     the text node.
    [Pure]
    public virtual XmlNode ParentNode
    {
      get
      {
        // Can return null.
        return default(XmlNode);
      }
    }

    //
    // Summary:
    //     Gets or sets the namespace prefix of this node.
    //
    // Returns:
    //     The namespace prefix of this node. For example, Prefix is bk for the element
    //     <bk:book>. If there is no prefix, this property returns String.Empty.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     This node is read-only.
    //
    //   System.Xml.XmlException:
    //     The specified prefix contains an invalid character.  The specified prefix
    //     is malformed.  The specified prefix is "xml" and the namespaceURI of this
    //     node is different from "http://www.w3.org/XML/1998/namespace".  This node
    //     is an attribute and the specified prefix is "xmlns" and the namespaceURI
    //     of this node is different from "http://www.w3.org/2000/xmlns/ ".  This node
    //     is an attribute and the qualifiedName of this node is "xmlns".
    //public virtual string Prefix { get; set; }
    //

#if !SILVERLIGHT
    // Summary:
    //     Gets the node immediately preceding this node.
    //
    // Returns:
    //     The preceding XmlNode. If there is no preceding node, null is returned.
    //public virtual XmlNode PreviousSibling { get; }
    //
    // Summary:
    //     Gets the post schema validation infoset that has been assigned to this node
    //     as a result of schema validation.
    //
    // Returns:
    //     An System.Xml.Schema.IXmlSchemaInfo object containing the post schema validation
    //     infoset of this node
    public virtual IXmlSchemaInfo SchemaInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<IXmlSchemaInfo>() != null);

        return default(IXmlSchemaInfo);
      }
    }
#endif
    //
    // Summary:
    //     Gets or sets the value of the node.
    //
    // Returns:
    //     The value returned depends on the System.Xml.XmlNode.NodeType of the node:
    //     Type Value Attribute The value of the attribute. CDATASection The content
    //     of the CDATA Section. Comment The content of the comment. Document null.
    //     DocumentFragment null. DocumentType null. Element null. You can use the System.Xml.XmlElement.InnerText
    //     or System.Xml.XmlElement.InnerXml properties to access the value of the element
    //     node. Entity null. EntityReference null. Notation null. ProcessingInstruction
    //     The entire content excluding the target. Text The content of the text node.
    //     SignificantWhitespace The white space characters. White space can consist
    //     of one or more space characters, carriage returns, line feeds, or tabs. Whitespace
    //     The white space characters. White space can consist of one or more space
    //     characters, carriage returns, line feeds, or tabs. XmlDeclaration The content
    //     of the declaration (that is, everything between <?xml and ?>).
    //
    // Exceptions:
    //   System.ArgumentException:
    //     Setting the value of a node that is read-only.
    //
    //   System.InvalidOperationException:
    //     Setting the value of a node that is not supposed to have a value (for example,
    //     an Element node).
    //public virtual string Value { get; set; }

    // Summary:
    //     Gets the first child element with the specified System.Xml.XmlNode.Name.
    //
    // Parameters:
    //   name:
    //     The qualified name of the element to retrieve.
    //
    // Returns:
    //     The first System.Xml.XmlElement that matches the specified name.
    //public virtual XmlElement this [string name] { get; }
    //
    // Summary:
    //     Gets the first child element with the specified System.Xml.XmlNode.LocalName
    //     and System.Xml.XmlNode.NamespaceURI.
    //
    // Parameters:
    //   localname:
    //     The local name of the element.
    //
    //   ns:
    //     The namespace URI of the element.
    //
    // Returns:
    //     The first System.Xml.XmlElement with the matching localname and ns.
    //public virtual XmlElement this[string localname, string ns] { get; }

    // Summary:
    //     Adds the specified node to the end of the list of child nodes, of this node.
    //
    // Parameters:
    //   newChild:
    //     The node to add. All the contents of the node to be added are moved into
    //     the specified location.
    //
    // Returns:
    //     The node added.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This node is of a type that does not allow child nodes of the type of the
    //     newChild node.  The newChild is an ancestor of this node.
    //
    //   System.ArgumentException:
    //     The newChild was created from a different document than the one that created
    //     this node.  This node is read-only.
    //public virtual XmlNode AppendChild(XmlNode newChild);
    //
    // Summary:
    //     Creates a duplicate of this node.
    //
    // Returns:
    //     The cloned node.
    public virtual XmlNode Clone()
    {
      Contract.Ensures(Contract.Result<XmlNode>() != null);

      return default(XmlNode);
    }
    //
    // Summary:
    //     When overridden in a derived class, creates a duplicate of the node.
    //
    // Parameters:
    //   deep:
    //     true to recursively clone the subtree under the specified node; false to
    //     clone only the node itself.
    //
    // Returns:
    //     The cloned node.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     Calling this method on a node type that cannot be cloned.
    public virtual XmlNode CloneNode(bool deep)
    {
      Contract.Ensures(Contract.Result<XmlNode>() != null);

      return default(XmlNode);
    }
    //
    // Summary:
    //     Creates an System.Xml.XPath.XPathNavigator for navigating this object.
    //
    // Returns:
    //     An XPathNavigator object. The XPathNavigator is positioned on the node from
    //     which the method was called. It is not positioned on the root of the document.
    public virtual XPathNavigator CreateNavigator()
    {
      Contract.Ensures(Contract.Result<XPathNavigator>() != null);

      return default(XPathNavigator);
    }
    //
    // Summary:
    //     Provides support for the for each style iteration over the nodes in the XmlNode.
    //
    // Returns:
    //     An System.Collections.IEnumerator.
    //public IEnumerator GetEnumerator();
    //
    // Summary:
    //     Looks up the closest xmlns declaration for the given prefix that is in scope
    //     for the current node and returns the namespace URI in the declaration.
    //
    // Parameters:
    //   prefix:
    //     Prefix whose namespace URI you want to find.
    //
    // Returns:
    //     The namespace URI of the specified prefix.
    public virtual string GetNamespaceOfPrefix(string prefix)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Looks up the closest xmlns declaration for the given namespace URI that is
    //     in scope for the current node and returns the prefix defined in that declaration.
    //
    // Parameters:
    //   namespaceURI:
    //     Namespace URI whose prefix you want to find.
    //
    // Returns:
    //     The prefix for the specified namespace URI.
    public virtual string GetPrefixOfNamespace(string namespaceURI)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Inserts the specified node immediately after the specified reference node.
    //
    // Parameters:
    //   newChild:
    //     The XmlNode to insert.
    //
    //   refChild:
    //     The XmlNode that is the reference node. The newNode is placed after the refNode.
    //
    // Returns:
    //     The node being inserted.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This node is of a type that does not allow child nodes of the type of the
    //     newChild node.  The newChild is an ancestor of this node.
    //
    //   System.ArgumentException:
    //     The newChild was created from a different document than the one that created
    //     this node.  The refChild is not a child of this node.  This node is read-only.
    public virtual XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
    {
      Contract.Requires(newChild != this);
      Contract.Requires(refChild == null || refChild.ParentNode == this);
      Contract.Ensures(Contract.Result<XmlNode>() != null);

      return default(XmlNode);
    }
    //
    // Summary:
    //     Inserts the specified node immediately before the specified reference node.
    //
    // Parameters:
    //   newChild:
    //     The XmlNode to insert.
    //
    //   refChild:
    //     The XmlNode that is the reference node. The newChild is placed before this
    //     node.
    //
    // Returns:
    //     The node being inserted.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The current node is of a type that does not allow child nodes of the type
    //     of the newChild node.  The newChild is an ancestor of this node.
    //
    //   System.ArgumentException:
    //     The newChild was created from a different document than the one that created
    //     this node.  The refChild is not a child of this node.  This node is read-only.
    public virtual XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
    {
      Contract.Requires(newChild != this);
      Contract.Requires(refChild == null || refChild.ParentNode == this);
      Contract.Ensures(Contract.Result<XmlNode>() != null);

      return default(XmlNode);
    }
    //
    // Summary:
    //     Puts all XmlText nodes in the full depth of the sub-tree underneath this
    //     XmlNode into a "normal" form where only markup (that is, tags, comments,
    //     processing instructions, CDATA sections, and entity references) separates
    //     XmlText nodes, that is, there are no adjacent XmlText nodes.
    //public virtual void Normalize();
    //
    // Summary:
    //     Adds the specified node to the beginning of the list of child nodes for this
    //     node.
    //
    // Parameters:
    //   newChild:
    //     The node to add. All the contents of the node to be added are moved into
    //     the specified location.
    //
    // Returns:
    //     The node added.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This node is of a type that does not allow child nodes of the type of the
    //     newChild node.  The newChild is an ancestor of this node.
    //
    //   System.ArgumentException:
    //     The newChild was created from a different document than the one that created
    //     this node.  This node is read-only.
    public virtual XmlNode PrependChild(XmlNode newChild)
    {
      Contract.Ensures(Contract.Result<XmlNode>() != null);

      return default(XmlNode);
    }
    //
    // Summary:
    //     Removes all the child nodes and/or attributes of the current node.
    //public virtual void RemoveAll();
    //
    // Summary:
    //     Removes specified child node.
    //
    // Parameters:
    //   oldChild:
    //     The node being removed.
    //
    // Returns:
    //     The node removed.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The oldChild is not a child of this node. Or this node is read-only.
    public virtual XmlNode RemoveChild(XmlNode oldChild)
    {
      Contract.Requires(oldChild != null);
      Contract.Ensures(Contract.Result<XmlNode>() != null);

      return default(XmlNode);
    }
    //
    // Summary:
    //     Replaces the child node oldChild with newChild node.
    //
    // Parameters:
    //   newChild:
    //     The new node to put in the child list.
    //
    //   oldChild:
    //     The node being replaced in the list.
    //
    // Returns:
    //     The node replaced.
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
    public virtual XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
    {
      Contract.Ensures(Contract.Result<XmlNode>() != null);

      return default(XmlNode);

    }
    //
    // Summary:
    //     Selects a list of nodes matching the XPath expression.
    //
    // Parameters:
    //   xpath:
    //     The XPath expression.
    //
    // Returns:
    //     An System.Xml.XmlNodeList containing a collection of nodes matching the XPath
    //     query. The XmlNodeList should not be expected to be connected "live" to the
    //     XML document. That is, changes that appear in the XML document may not appear
    //     in the XmlNodeList, and vice versa.
    //
    // Exceptions:
    //   System.Xml.XPath.XPathException:
    //     The XPath expression contains a prefix.
    // F: can return null
    //public XmlNodeList SelectNodes(string xpath);

    //
    // Summary:
    //     Selects a list of nodes matching the XPath expression. Any prefixes found
    //     in the XPath expression are resolved using the supplied System.Xml.XmlNamespaceManager.
    //
    // Parameters:
    //   xpath:
    //     The XPath expression.
    //
    //   nsmgr:
    //     An System.Xml.XmlNamespaceManager to use for resolving namespaces for prefixes
    //     in the XPath expression.
    //
    // Returns:
    //     An System.Xml.XmlNodeList containing a collection of nodes matching the XPath
    //     query. The XmlNodeList should not be expected to be connected "live" to the
    //     XML document. That is, changes that appear in the XML document may not appear
    //     in the XmlNodeList, and vice versa.
    //
    // Exceptions:
    //   System.Xml.XPath.XPathException:
    //     The XPath expression contains a prefix which is not defined in the XmlNamespaceManager.
    //public XmlNodeList SelectNodes(string xpath, XmlNamespaceManager nsmgr);
    //
    // Summary:
    //     Selects the first XmlNode that matches the XPath expression.
    //
    // Parameters:
    //   xpath:
    //     The XPath expression.
    //
    // Returns:
    //     The first XmlNode that matches the XPath query or null if no matching node
    //     is found. The XmlNode should not be expected to be connected "live" to the
    //     XML document. That is, changes that appear in the XML document may not appear
    //     in the XmlNode, and vice versa.
    //
    // Exceptions:
    //   System.Xml.XPath.XPathException:
    //     The XPath expression contains a prefix.
    //public XmlNode SelectSingleNode(string xpath);
    //
    // Summary:
    //     Selects the first XmlNode that matches the XPath expression. Any prefixes
    //     found in the XPath expression are resolved using the supplied System.Xml.XmlNamespaceManager.
    //
    // Parameters:
    //   xpath:
    //     The XPath expression.
    //
    //   nsmgr:
    //     An System.Xml.XmlNamespaceManager to use for resolving namespaces for prefixes
    //     in the XPath expression.
    //
    // Returns:
    //     The first XmlNode that matches the XPath query or null if no matching node
    //     is found. The XmlNode should not be expected to be connected "live" to the
    //     XML document. That is, changes that appear in the XML document may not appear
    //     in the XmlNode, and vice versa.
    //
    // Exceptions:
    //   System.Xml.XPath.XPathException:
    //     The XPath expression contains a prefix which is not defined in the XmlNamespaceManager.
    //public XmlNode SelectSingleNode(string xpath, XmlNamespaceManager nsmgr);
    //
    // Summary:
    //     Test if the DOM implementation implements a specific feature.
    //
    // Parameters:
    //   feature:
    //     The package name of the feature to test. This name is not case-sensitive.
    //
    //   version:
    //     This is the version number of the package name to test. If the version is
    //     not specified (null), supporting any version of the feature causes the method
    //     to return true.
    //
    // Returns:
    //     true if the feature is implemented in the specified version; otherwise, false.
    //     The following table describes the combinations that return true.  Feature
    //     Version XML 1.0 XML 2.0
    //public virtual bool Supports(string feature, string version);
    //
    // Summary:
    //     When overridden in a derived class, saves all the child nodes of the node
    //     to the specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   w:
    //     The XmlWriter to which you want to save.
    //public abstract void WriteContentTo(XmlWriter w);
    //
    // Summary:
    //     When overridden in a derived class, saves the current node to the specified
    //     System.Xml.XmlWriter.
    //
    // Parameters:
    //   w:
    //     The XmlWriter to which you want to save.
    //public abstract void WriteTo(XmlWriter w);
  }
}

#endif