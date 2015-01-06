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


using System.Xml.Schema;
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Represents an XML document.
  public class XmlDocument 
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XmlDocument class.
    //public XmlDocument();
    //
    // Summary:
    //     Initializes a new instance of the XmlDocument class with the specified System.Xml.XmlImplementation.
    //
    // Parameters:
    //   imp:
    //     The XmlImplementation to use.
    //protected internal XmlDocument(XmlImplementation imp);
    //
    // Summary:
    //     Initializes a new instance of the XmlDocument class with the specified System.Xml.XmlNameTable.
    //
    // Parameters:
    //   nt:
    //     The XmlNameTable to use.
    // F: It seems to me that nt can be null
    //public XmlDocument(XmlNameTable nt);

    // Summary:
    //     Gets the base URI of the current node.
    //
    // Returns:
    //     The location from which the node was loaded.
    //public override string BaseURI { get; }
    //
    // Summary:
    //     Gets the root System.Xml.XmlElement for the document.
    //
    // Returns:
    //     The XmlElement that represents the root of the XML document tree. If no root
    //     exists, null is returned.
    //public XmlElement DocumentElement { get; }
    //
    // Summary:
    //     Gets the node containing the DOCTYPE declaration.
    //
    // Returns:
    //     The System.Xml.XmlNode containing the DocumentType (DOCTYPE declaration).
    //public virtual XmlDocumentType DocumentType { get; }
    //
    // Summary:
    //     Gets the System.Xml.XmlImplementation object for the current document.
    //
    // Returns:
    //     The XmlImplementation object for the current document.
    //public XmlImplementation Implementation { get; }
    //
    // Summary:
    //     Gets or sets the markup representing the children of the current node.
    //
    // Returns:
    //     The markup of the children of the current node.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     The XML specified when setting this property is not well-formed.
    //public override string InnerXml { get; set; }
    //
    // Summary:
    //     Gets a value indicating whether the current node is read-only.
    //
    // Returns:
    //     true if the current node is read-only; otherwise false. XmlDocument nodes
    //     always return false.
    //public override bool IsReadOnly { get; }
    //
    // Summary:
    //     Gets the local name of the node.
    //
    // Returns:
    //     For XmlDocument nodes, the local name is #document.
    //public override string LocalName { get; }
    //
    // Summary:
    //     Gets the qualified name of the node.
    //
    // Returns:
    //     For XmlDocument nodes, the name is #document.
    //public override string Name { get; }
    //
    // Summary:
    //     Gets the System.Xml.XmlNameTable associated with this implementation.
    //
    // Returns:
    //     An XmlNameTable enabling you to get the atomized version of a string within
    //     the document.
    //public XmlNameTable NameTable { get; }
    //
    // Summary:
    //     Gets the type of the current node.
    //
    // Returns:
    //     The node type. For XmlDocument nodes, this value is XmlNodeType.Document.
    //public override XmlNodeType NodeType { get; }
    //
    // Summary:
    //     Gets the System.Xml.XmlDocument to which the current node belongs.
    //
    // Returns:
    //     For XmlDocument nodes (System.Xml.XmlDocument.NodeType equals XmlNodeType.Document),
    //     this property always returns null.
    //public override XmlDocument OwnerDocument { get; }
    //
    // Summary:
    //     Gets the parent node of this node (for nodes that can have parents).
    //
    // Returns:
    //     Always returns null.
    //public override XmlNode ParentNode { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to preserve white space in element
    //     content.
    //
    // Returns:
    //     true to preserve white space; otherwise false. The default is false.
    //public bool PreserveWhitespace { get; set; }
    //
    // Summary:
    //     Returns the Post-Schema-Validation-Infoset (PSVI) of the node.
    //
    // Returns:
    //     The System.Xml.Schema.IXmlSchemaInfo object representing the PSVI of the
    //     node.
    //public override IXmlSchemaInfo SchemaInfo { get; }

    
    //
    // Summary:
    //     Gets or sets the System.Xml.Schema.XmlSchemaSet object associated with this
    //     System.Xml.XmlDocument.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaSet object containing the XML Schema Definition
    //     Language (XSD) schemas associated with this System.Xml.XmlDocument; otherwise,
    //     an empty System.Xml.Schema.XmlSchemaSet object.
    public XmlSchemaSet Schemas
    {
      get
      {
        Contract.Ensures(Contract.Result<XmlSchemaSet>() != null);

        return default(XmlSchemaSet);
      }
      
      //set; 
    }
    //
    // Summary:
    //     Sets the System.Xml.XmlResolver to use for resolving external resources.
    //
    // Returns:
    //     The XmlResolver to use.In version 1.1 of the.NET Framework, the caller must
    //     be fully trusted in order to specify an XmlResolver.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     This property is set to null and an external DTD or entity is encountered.
    //public virtual XmlResolver XmlResolver { set; }

    // Summary:
    //     Occurs when the System.Xml.XmlNode.Value of a node belonging to this document
    //     has been changed.
    // // public event XmlNodeChangedEventHandler NodeChanged;
    //
    // Summary:
    //     Occurs when the System.Xml.XmlNode.Value of a node belonging to this document
    //     is about to be changed.
    // public event XmlNodeChangedEventHandler NodeChanging;
    //
    // Summary:
    //     Occurs when a node belonging to this document has been inserted into another
    //     node.
    // public event XmlNodeChangedEventHandler NodeInserted;
    //
    // Summary:
    //     Occurs when a node belonging to this document is about to be inserted into
    //     another node.
    // public event XmlNodeChangedEventHandler NodeInserting;
    //
    // Summary:
    //     Occurs when a node belonging to this document has been removed from its parent.
    // public event XmlNodeChangedEventHandler NodeRemoved;
    //
    // Summary:
    //     Occurs when a node belonging to this document is about to be removed from
    //     the document.
    // public event XmlNodeChangedEventHandler NodeRemoving;

#if !SILVERLIGHT
    // Summary:
    //     Creates a duplicate of this node.
    //
    // Parameters:
    //   deep:
    //     true to recursively clone the subtree under the specified node; false to
    //     clone only the node itself.
    //
    // Returns:
    //     The cloned XmlDocument node.
    //public override XmlNode CloneNode(bool deep);
    //
    // Summary:
    //     Creates an System.Xml.XmlAttribute with the specified System.Xml.XmlDocument.Name.
    //
    // Parameters:
    //   name:
    //     The qualified name of the attribute. If the name contains a colon, the System.Xml.XmlNode.Prefix
    //     property reflects the part of the name preceding the first colon and the
    //     System.Xml.XmlDocument.LocalName property reflects the part of the name following
    //     the first colon. The System.Xml.XmlNode.NamespaceURI remains empty unless
    //     the prefix is a recognized built-in prefix such as xmlns. In this case NamespaceURI
    //     has a value of http://www.w3.org/2000/xmlns/.
    //
    // Returns:
    //     The new XmlAttribute.
    public XmlAttribute CreateAttribute(string name)
    {
      Contract.Ensures(Contract.Result<XmlAttribute>() != null);

      return default(XmlAttribute);
    }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    //     Creates an System.Xml.XmlAttribute with the specified qualified name and
    //     System.Xml.XmlNode.NamespaceURI.
    //
    // Parameters:
    //   qualifiedName:
    //     The qualified name of the attribute. If the name contains a colon then the
    //     System.Xml.XmlNode.Prefix property will reflect the part of the name preceding
    //     the colon and the System.Xml.XmlDocument.LocalName property will reflect
    //     the part of the name after the colon.
    //
    //   namespaceURI:
    //     The namespaceURI of the attribute. If the qualified name includes a prefix
    //     of xmlns, then this parameter must be http://www.w3.org/2000/xmlns/.
    //
    // Returns:
    //     The new XmlAttribute.
    public XmlAttribute CreateAttribute(string qualifiedName, string namespaceURI)
    {
      Contract.Ensures(Contract.Result<XmlAttribute>() != null);

      return default(XmlAttribute);
    }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    //     Creates an System.Xml.XmlAttribute with the specified System.Xml.XmlNode.Prefix,
    //     System.Xml.XmlDocument.LocalName, and System.Xml.XmlNode.NamespaceURI.
    //
    // Parameters:
    //   prefix:
    //     The prefix of the attribute (if any). String.Empty and null are equivalent.
    //
    //   localName:
    //     The local name of the attribute.
    //
    //   namespaceURI:
    //     The namespace URI of the attribute (if any). String.Empty and null are equivalent.
    //     If prefix is xmlns, then this parameter must be http://www.w3.org/2000/xmlns/;
    //     otherwise an exception is thrown.
    //
    // Returns:
    //     The new XmlAttribute.
    public virtual XmlAttribute CreateAttribute(string prefix, string localName, string namespaceURI)
    {
      Contract.Ensures(Contract.Result<XmlAttribute>() != null);

      return default(XmlAttribute);
    }
#endif
    //
    // Summary:
    //     Creates an System.Xml.XmlCDataSection containing the specified data.
    //
    // Parameters:
    //   data:
    //     The content of the new XmlCDataSection.
    //
    // Returns:
    //     The new XmlCDataSection.
    public virtual XmlCDataSection CreateCDataSection(string data)
    {
      Contract.Ensures(Contract.Result<XmlCDataSection>() != null);

      return default(XmlCDataSection);
    }
    //
    // Summary:
    //     Creates an System.Xml.XmlComment containing the specified data.
    //
    // Parameters:
    //   data:
    //     The content of the new XmlComment.
    //
    // Returns:
    //     The new XmlComment.
    public virtual XmlComment CreateComment(string data)
    {
      Contract.Ensures(Contract.Result<XmlComment>() != null);

      return default(XmlComment);
    }
    //
    // Summary:
    //     Creates a default attribute with the specified prefix, local name and namespace
    //     URI.
    //
    // Parameters:
    //   prefix:
    //     The prefix of the attribute (if any).
    //
    //   localName:
    //     The local name of the attribute.
    //
    //   namespaceURI:
    //     The namespace URI of the attribute (if any).
    //
    // Returns:
    //     The new System.Xml.XmlAttribute.
    //protected internal virtual XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI);
    //
    // Summary:
    //     Creates an System.Xml.XmlDocumentFragment.
    //
    // Returns:
    //     The new XmlDocumentFragment.
    public virtual XmlDocumentFragment CreateDocumentFragment()
    {
      Contract.Ensures(Contract.Result<XmlDocumentFragment>() != null);

      return default(XmlDocumentFragment);
    }
    //
    // Summary:
    //     Returns a new System.Xml.XmlDocumentType object.
    //
    // Parameters:
    //   name:
    //     Name of the document type.
    //
    //   publicId:b
    //     The public identifier of the document type or null. You can specify a public
    //     URI and also a system identifier to identify the location of the external
    //     DTD subset.
    //
    //   systemId:
    //     The system identifier of the document type or null. Specifies the URL of
    //     the file location for the external DTD subset.
    //
    //   internalSubset:
    //     The DTD internal subset of the document type or null.
    //
    // Returns:
    //     The new XmlDocumentType.
    public virtual XmlDocumentType CreateDocumentType(string name, string publicId, string systemId, string internalSubset)
    {
      Contract.Ensures(Contract.Result<XmlDocumentType>() != null);

      return default(XmlDocumentType);
    } 
    //
    // Summary:
    //     Creates an element with the specified name.
    //
    // Parameters:
    //   name:
    //     The qualified name of the element. If the name contains a colon then the
    //     System.Xml.XmlNode.Prefix property reflects the part of the name preceding
    //     the colon and the System.Xml.XmlDocument.LocalName property reflects the
    //     part of the name after the colon. The qualified name cannot include a prefix
    //     of'xmlns'.
    //
    // Returns:
    //     The new XmlElement.
    public XmlElement CreateElement(string name)
    {
      Contract.Ensures(Contract.Result<XmlElement>() != null);

      return default(XmlElement);
    }
    //
    // Summary:
    //     Creates an System.Xml.XmlElement with the qualified name and System.Xml.XmlNode.NamespaceURI.
    //
    // Parameters:
    //   qualifiedName:
    //     The qualified name of the element. If the name contains a colon then the
    //     System.Xml.XmlNode.Prefix property will reflect the part of the name preceding
    //     the colon and the System.Xml.XmlDocument.LocalName property will reflect
    //     the part of the name after the colon. The qualified name cannot include a
    //     prefix of'xmlns'.
    //
    //   namespaceURI:
    //     The namespace URI of the element.
    //
    // Returns:
    //     The new XmlElement.
    public XmlElement CreateElement(string qualifiedName, string namespaceURI)
    {
      Contract.Ensures(Contract.Result<XmlElement>() != null);

      return default(XmlElement);
    }
    //
    // Summary:
    //     Creates an element with the specified System.Xml.XmlNode.Prefix, System.Xml.XmlDocument.LocalName,
    //     and System.Xml.XmlNode.NamespaceURI.
    //
    // Parameters:
    //   prefix:
    //     The prefix of the new element (if any). String.Empty and null are equivalent.
    //
    //   localName:
    //     The local name of the new element.
    //
    //   namespaceURI:
    //     The namespace URI of the new element (if any). String.Empty and null are
    //     equivalent.
    //
    // Returns:
    //     The new System.Xml.XmlElement.
    public virtual XmlElement CreateElement(string prefix, string localName, string namespaceURI)
    {
      Contract.Ensures(Contract.Result<XmlElement>() != null);

      return default(XmlElement);
    }

    //
    // Summary:
    //     Creates an System.Xml.XmlEntityReference with the specified name.
    //
    // Parameters:
    //   name:
    //     The name of the entity reference.
    //
    // Returns:
    //     The new XmlEntityReference.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The name is invalid (for example, names starting with'#' are invalid.)
    public virtual XmlEntityReference CreateEntityReference(string name)
    {
      //Contract.Requires(IsLoading || (name == null || name[0] != '#'));
      Contract.Ensures(Contract.Result<XmlEntityReference>() != null);

      return default(XmlEntityReference);
    }

    //
    // Summary:
    //     Creates a new System.Xml.XPath.XPathNavigator object for navigating this
    //     document.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNavigator object.
    //public override XPathNavigator CreateNavigator();
    //
    // Summary:
    //     Creates an System.Xml.XPath.XPathNavigator object for navigating this document
    //     positioned on the System.Xml.XmlNode specified.
    //
    // Parameters:
    //   node:
    //     The System.Xml.XmlNode you want the navigator initially positioned on.
    //
    // Returns:
    ////     An System.Xml.XPath.XPathNavigator object.
    //protected internal virtual XPathNavigator CreateNavigator(XmlNode node);
    //
    // Summary:
    //     Creates an System.Xml.XmlNode with the specified node type, System.Xml.XmlDocument.Name,
    //     and System.Xml.XmlNode.NamespaceURI.
    //
    // Parameters:
    //   nodeTypeString:
    //     String version of the System.Xml.XmlNodeType of the new node. This parameter
    //     must be one of the values listed in the table below.
    //
    //   name:
    //     The qualified name of the new node. If the name contains a colon, it is parsed
    //     into System.Xml.XmlNode.Prefix and System.Xml.XmlDocument.LocalName components.
    //
    //   namespaceURI:
    //     The namespace URI of the new node.
    //
    // Returns:
    //     The new XmlNode.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The name was not provided and the XmlNodeType requires a name; or nodeTypeString
    //     is not one of the strings listed below.
    public virtual XmlNode CreateNode(string nodeTypeString, string name, string namespaceURI)
    {
      Contract.Ensures(Contract.Result<XmlNode>() != null);

      return default(XmlNode);
    }
    //
    // Summary:
    //     Creates an System.Xml.XmlNode with the specified System.Xml.XmlNodeType,
    //     System.Xml.XmlDocument.Name, and System.Xml.XmlNode.NamespaceURI.
    //
    // Parameters:
    //   type:
    //     The XmlNodeType of the new node.
    //
    //   name:
    //     The qualified name of the new node. If the name contains a colon then it
    //     is parsed into System.Xml.XmlNode.Prefix and System.Xml.XmlDocument.LocalName
    //     components.
    //
    //   namespaceURI:
    //     The namespace URI of the new node.
    //
    // Returns:
    //     The new XmlNode.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The name was not provided and the XmlNodeType requires a name.
    public virtual XmlNode CreateNode(XmlNodeType type, string name, string namespaceURI)
    {
      Contract.Ensures(Contract.Result<XmlNode>() != null);

      return default(XmlNode);
    }
    //
    // Summary:
    //     Creates a System.Xml.XmlNode with the specified System.Xml.XmlNodeType, System.Xml.XmlNode.Prefix,
    //     System.Xml.XmlDocument.Name, and System.Xml.XmlNode.NamespaceURI.
    //
    // Parameters:
    //   type:
    //     The XmlNodeType of the new node.
    //
    //   prefix:
    //     The prefix of the new node.
    //
    //   name:
    //     The local name of the new node.
    //
    //   namespaceURI:
    //     The namespace URI of the new node.
    //
    // Returns:
    //     The new XmlNode.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The name was not provided and the XmlNodeType requires a name.
    public virtual XmlNode CreateNode(XmlNodeType type, string prefix, string name, string namespaceURI)
    {
      Contract.Ensures(Contract.Result<XmlNode>() != null);

      return default(XmlNode);
    }
    //
    // Summary:
    //     Creates an System.Xml.XmlProcessingInstruction with the specified name and
    //     data.
    //
    // Parameters:
    //   target:
    //     The name of the processing instruction.
    //
    //   data:
    //     The data for the processing instruction.
    //
    // Returns:
    //     The new XmlProcessingInstruction.
    public virtual XmlProcessingInstruction CreateProcessingInstruction(string target, string data)
    {
      Contract.Ensures(Contract.Result<XmlProcessingInstruction>() != null);

      return default(XmlProcessingInstruction);
    }
    //
    // Summary:
    //     Creates an System.Xml.XmlSignificantWhitespace node.
    //
    // Parameters:
    //   text:
    //     The string must contain only the following characters &#20; &#10; &#13; and
    //     &#9;
    //
    // Returns:
    //     A new XmlSignificantWhitespace node.
    public virtual XmlSignificantWhitespace CreateSignificantWhitespace(string text)
    {
      Contract.Ensures(Contract.Result<XmlSignificantWhitespace>() != null);

      return default(XmlSignificantWhitespace);
    }

    //
    // Summary:
    //     Creates an System.Xml.XmlText with the specified text.
    //
    // Parameters:
    //   text:
    //     The text for the Text node.
    //
    // Returns:
    //     The new XmlText node.
    public virtual XmlText CreateTextNode(string text)
    {
      Contract.Ensures(Contract.Result<XmlText>() != null);

      return default(XmlText);
    }
    //
    // Summary:
    //     Creates an System.Xml.XmlWhitespace node.
    //
    // Parameters:
    //   text:
    //     The string must contain only the following characters &#20; &#10; &#13; and
    //     &#9;
    //
    // Returns:
    //     A new XmlWhitespace node.
    public virtual XmlWhitespace CreateWhitespace(string text)
    {
      Contract.Ensures(Contract.Result<XmlWhitespace>() != null);

      return default(XmlWhitespace);
    }
    //
    // Summary:
    //     Creates an System.Xml.XmlDeclaration node with the specified values.
    //
    // Parameters:
    //   version:
    //     The version must be "1.0".
    //
    //   encoding:
    //     The value of the encoding attribute. This is the encoding that is used when
    //     you save the System.Xml.XmlDocument to a file or a stream; therefore, it
    //     must be set to a string supported by the System.Text.Encoding class, otherwise
    //     System.Xml.XmlDocument.Save(System.String) fails. If this is null or String.Empty,
    //     the Save method does not write an encoding attribute on the XML declaration
    //     and therefore the default encoding, UTF-8, is used.Note: If the XmlDocument
    //     is saved to either a System.IO.TextWriter or an System.Xml.XmlTextWriter,
    //     this encoding value is discarded. Instead, the encoding of the TextWriter
    //     or the XmlTextWriter is used. This ensures that the XML written out can be
    //     read back using the correct encoding.
    //
    //   standalone:
    //     The value must be either "yes" or "no". If this is null or String.Empty,
    //     the Save method does not write a standalone attribute on the XML declaration.
    //
    // Returns:
    //     The new XmlDeclaration node.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The values of version or standalone are something other than the ones specified
    //     above.
    public virtual XmlDeclaration CreateXmlDeclaration(string version, string encoding, string standalone)
    {
      Contract.Ensures(Contract.Result<XmlDeclaration>() != null);

      return default(XmlDeclaration);
    }
    //
    // Summary:
    //     Gets the System.Xml.XmlElement with the specified ID.
    //
    // Parameters:
    //   elementId:
    //     The attribute ID to match.
    //
    // Returns:
    //     The XmlElement with the matching ID or null if no matching element is found.
    // F: can be null
    //    public virtual XmlElement GetElementById(string elementId);
    //
    // Summary:
    //     Returns an System.Xml.XmlNodeList containing a list of all descendant elements
    //     that match the specified System.Xml.XmlDocument.Name.
    //
    // Parameters:
    //   name:
    //     The qualified name to match. It is matched against the Name property of the
    //     matching node. The special value "*" matches all tags.
    //
    // Returns:
    //     An System.Xml.XmlNodeList containing a list of all matching nodes. If no
    //     nodes match name, the returned collection will be empty.
    public virtual XmlNodeList GetElementsByTagName(string name)
    {
      Contract.Ensures(Contract.Result<XmlNodeList>() != null);

      return default(XmlNodeList);
    }
    //
    // Summary:
    //     Returns an System.Xml.XmlNodeList containing a list of all descendant elements
    //     that match the specified System.Xml.XmlDocument.LocalName and System.Xml.XmlNode.NamespaceURI.
    //
    // Parameters:
    //   localName:
    //     The LocalName to match. The special value "*" matches all tags.
    //
    //   namespaceURI:
    //     NamespaceURI to match.
    //
    // Returns:
    //     An System.Xml.XmlNodeList containing a list of all matching nodes. If no
    //     nodes match the specified localName and namespaceURI, the returned collection
    //     will be empty.
    public virtual XmlNodeList GetElementsByTagName(string localName, string namespaceURI)
    {
      Contract.Ensures(Contract.Result<XmlNodeList>() != null);

      return default(XmlNodeList);
    }
    //
    // Summary:
    //     Imports a node from another document to the current document.
    //
    // Parameters:
    //   node:
    //     The node being imported.
    //
    //   deep:
    //     true to perform a deep clone; otherwise, false.
    //
    // Returns:
    //     The imported System.Xml.XmlNode.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     Calling this method on a node type which cannot be imported.
    //public virtual XmlNode ImportNode(XmlNode node, bool deep);
    //
    // Summary:
    //     Loads the XML document from the specified stream.
    //
    // Parameters:
    //   inStream:
    //     The stream containing the XML document to load.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     There is a load or parse error in the XML. In this case, the document remains
    //     empty.
    //public virtual void Load(Stream inStream);
    //
    // Summary:
    //     Loads the XML document from the specified URL.
    //
    // Parameters:
    //   filename:
    //     URL for the file containing the XML document to load. The URL can be either
    //     a local file or an HTTP URL (a Web address).
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     There is a load or parse error in the XML. In this case, the document remains
    //     empty.
    //
    //   System.ArgumentException:
    //     filename is a zero-length string, contains only white space, or contains
    //     one or more invalid characters as defined by System.IO.Path.InvalidPathChars.
    //
    //   System.ArgumentNullException:
    //     filename is null.
    //
    //   System.IO.PathTooLongException:
    //     The specified path, file name, or both exceed the system-defined maximum
    //     length. For example, on Windows-based platforms, paths must be less than
    //     248 characters, and file names must be less than 260 characters.
    //
    //   System.IO.DirectoryNotFoundException:
    //     The specified path is invalid (for example, it is on an unmapped drive).
    //
    //   System.IO.IOException:
    //     An I/O error occurred while opening the file.
    //
    //   System.UnauthorizedAccessException:
    //     filename specified a file that is read-only.-or- This operation is not supported
    //     on the current platform.-or- filename specified a directory.-or- The caller
    //     does not have the required permission.
    //
    //   System.IO.FileNotFoundException:
    //     The file specified in filename was not found.
    //
    //   System.NotSupportedException:
    //     filename is in an invalid format.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    public virtual void Load(string filename)
    {
      Contract.Requires(filename != null);
      Contract.Requires(!String.IsNullOrEmpty(filename));

    }
    //
    // Summary:
    //     Loads the XML document from the specified System.IO.TextReader.
    //
    // Parameters:
    //   txtReader:
    //     The TextReader used to feed the XML data into the document.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     There is a load or parse error in the XML. In this case, the document remains
    //     empty.
    //public virtual void Load(TextReader txtReader);
    //
    // Summary:
    //     Loads the XML document from the specified System.Xml.XmlReader.
    //
    // Parameters:
    //   reader:
    //     The XmlReader used to feed the XML data into the document.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     There is a load or parse error in the XML. In this case, the document remains
    //     empty.
    //public virtual void Load(XmlReader reader);
    //
    // Summary:
    //     Loads the XML document from the specified string.
    //
    // Parameters:
    //   xml:
    //     String containing the XML document to load.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     There is a load or parse error in the XML. In this case, the document remains
    //     empty.
    //public virtual void LoadXml(string xml);
    //
    // Summary:
    //     Creates an System.Xml.XmlNode object based on the information in the System.Xml.XmlReader.
    //     The reader must be positioned on a node or attribute.
    //
    // Parameters:
    //   reader:
    //     The XML source
    //
    // Returns:
    //     The new XmlNode or null if no more nodes exist.
    //
    // Exceptions:
    //   System.NullReferenceException:
    //     The reader is positioned on a node type that does not translate to a valid
    //     DOM node (for example, EndElement or EndEntity).
    //public virtual XmlNode ReadNode(XmlReader reader);
    //
    // Summary:
    //     Saves the XML document to the specified stream.
    //
    // Parameters:
    //   outStream:
    //     The stream to which you want to save.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     The operation would not result in a well formed XML document (for example,
    //     no document element or duplicate XML declarations).
    //public virtual void Save(Stream outStream);
    //
    // Summary:
    //     Saves the XML document to the specified file.
    //
    // Parameters:
    //   filename:
    //     The location of the file where you want to save the document.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     The operation would not result in a well formed XML document (for example,
    //     no document element or duplicate XML declarations).
    //public virtual void Save(string filename);
    //
    // Summary:
    //     Saves the XML document to the specified System.IO.TextWriter.
    //
    // Parameters:
    //   writer:
    //     The TextWriter to which you want to save.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     The operation would not result in a well formed XML document (for example,
    //     no document element or duplicate XML declarations).
    //public virtual void Save(TextWriter writer);
    //
    // Summary:
    //     Saves the XML document to the specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   w:
    //     The XmlWriter to which you want to save.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     The operation would not result in a well formed XML document (for example,
    //     no document element or duplicate XML declarations).
    //public virtual void Save(XmlWriter w);
    //
    // Summary:
    //     Validates the System.Xml.XmlDocument against the XML Schema Definition Language
    //     (XSD) schemas contained in the System.Xml.XmlDocument.Schemas property.
    //
    // Parameters:
    //   validationEventHandler:
    //     The System.Xml.Schema.ValidationEventHandler object that receives information
    //     about schema validation warnings and errors.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaValidationException:
    //     A schema validation event occurred and no System.Xml.Schema.ValidationEventHandler
    //     object was specified.
    //public void Validate(ValidationEventHandler validationEventHandler);
    //
    // Summary:
    //     Validates the System.Xml.XmlNode object specified against the XML Schema
    //     Definition Language (XSD) schemas in the System.Xml.XmlDocument.Schemas property.
    //
    // Parameters:
    //   validationEventHandler:
    //     The System.Xml.Schema.ValidationEventHandler object that receives information
    //     about schema validation warnings and errors.
    //
    //   nodeToValidate:
    //     The System.Xml.XmlNode object created from an System.Xml.XmlDocument to validate.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Xml.XmlNode object parameter was not created from an System.Xml.XmlDocument.
    //
    //   System.InvalidOperationException:
    //     The System.Xml.XmlNode object parameter is not an element, attribute, document
    //     fragment, or the root node.
    //
    //   System.Xml.Schema.XmlSchemaValidationException:
    //     A schema validation event occurred and no System.Xml.Schema.ValidationEventHandler
    //     object was specified.
    //public void Validate(ValidationEventHandler validationEventHandler, XmlNode nodeToValidate);
    //
    // Summary:
    //     Saves all the children of the XmlDocument node to the specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   xw:
    //     The XmlWriter to which you want to save.
    //public override void WriteContentTo(XmlWriter xw);
    //
    // Summary:
    //     Saves the XmlDocument node to the specified System.Xml.XmlWriter.
    //
    // Parameters:
    //   w:
    //     The XmlWriter to which you want to save.
    //public override void WriteTo(XmlWriter w);
  }
}

#endif