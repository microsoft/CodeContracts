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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Xml;
using System.Xml.Schema;

namespace System.Xml.XPath
{
  // Summary:
  //     Provides a cursor model for navigating and editing XML data.
  // [DebuggerDisplay("{debuggerDisplayProxy}")]
  [ContractClass(typeof(XPathNavigatorContract))]
  public abstract class XPathNavigator// : XPathItem, ICloneable, IXPathNavigable, IXmlNamespaceResolver
  {

    // Summary:
    //     When overridden in a derived class, gets the base URI for the current node.
    //
    // Returns:
    //     The location from which the node was loaded, or System.String.Empty if there
    //     is no value.
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
    //     Gets a value indicating whether the System.Xml.XPath.XPathNavigator can edit
    //     the underlying XML data.
    //
    // Returns:
    //     true if the System.Xml.XPath.XPathNavigator can edit the underlying XML data;
    //     otherwise false.
    //public virtual bool CanEdit { get; }
    //
    // Summary:
    //     Gets a value indicating whether the current node has any attributes.
    //
    // Returns:
    //     Returns true if the current node has attributes; returns false if the current
    //     node has no attributes, or if the System.Xml.XPath.XPathNavigator is not
    //     positioned on an element node.
    //public virtual bool HasAttributes { get; }
    //
    // Summary:
    //     Gets a value indicating whether the current node has any child nodes.
    //
    // Returns:
    //     Returns true if the current node has any child nodes; otherwise, false.
    //public virtual bool HasChildren { get; }
    //
    // Summary:
    //     Gets or sets the markup representing the child nodes of the current node.
    //
    // Returns:
    //     A System.String that contains the markup of the child nodes of the current
    //     node.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Xml.XPath.XPathNavigator.InnerXml property cannot be set.
    //public virtual string InnerXml { get; set; }
    //
    // Summary:
    //     When overridden in a derived class, gets a value indicating whether the current
    //     node is an empty element without an end element tag.
    //
    // Returns:
    //     Returns true if the current node is an empty element; otherwise, false.
    //public abstract bool IsEmptyElement { get; }
    //
    // Summary:
    //     Gets a value indicating if the current node represents an XPath node.
    //
    // Returns:
    //     Always returns true.
    //public override sealed bool IsNode { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets the System.Xml.XPath.XPathNavigator.Name
    //     of the current node without any namespace prefix.
    //
    // Returns:
    //     A System.String that contains the local name of the current node, or System.String.Empty
    //     if the current node does not have a name (for example, text or comment nodes).
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
    //     When overridden in a derived class, gets the qualified name of the current
    //     node.
    //
    // Returns:
    //     A System.String that contains the qualified System.Xml.XPath.XPathNavigator.Name
    //     of the current node, or System.String.Empty if the current node does not
    //     have a name (for example, text or comment nodes).
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
    //     When overridden in a derived class, gets the namespace URI of the current
    //     node.
    //
    // Returns:
    //     A System.String that contains the namespace URI of the current node, or System.String.Empty
    //     if the current node has no namespace URI.
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
    //     When overridden in a derived class, gets the System.Xml.XmlNameTable of the
    //     System.Xml.XPath.XPathNavigator.
    //
    // Returns:
    //     An System.Xml.XmlNameTable object enabling you to get the atomized version
    //     of a System.String within the XML document.
    public abstract XmlNameTable NameTable { get;}

    //
    // Summary:
    //     Gets an System.Collections.IEqualityComparer used for equality comparison
    //     of System.Xml.XPath.XPathNavigator objects.
    //
    // Returns:
    //     An System.Collections.IEqualityComparer used for equality comparison of System.Xml.XPath.XPathNavigator
    //     objects.
    public static IEqualityComparer NavigatorComparer
    {
      get
      {
        Contract.Ensures(Contract.Result<IEqualityComparer>() != null);

        return default(IEqualityComparer);
      }
    }
    //
    // Summary:
    //     When overridden in a derived class, gets the System.Xml.XPath.XPathNodeType
    //     of the current node.
    //
    // Returns:
    //     One of the System.Xml.XPath.XPathNodeType values representing the current
    //     node.
    //public abstract XPathNodeType NodeType { get; }
    //
    // Summary:
    //     Gets or sets the markup representing the opening and closing tags of the
    //     current node and its child nodes.
    //
    // Returns:
    //     A System.String that contains the markup representing the opening and closing
    //     tags of the current node and its child nodes.
    //public virtual string OuterXml { get; set; }
    //
    // Summary:
    //     When overridden in a derived class, gets the namespace prefix associated
    //     with the current node.
    //
    // Returns:
    //     A System.String that contains the namespace prefix associated with the current
    //     node.
    //public abstract string Prefix { get; }
    //
    // Summary:
    //     Gets the schema information that has been assigned to the current node as
    //     a result of schema validation.
    //
    // Returns:
    //     An System.Xml.Schema.IXmlSchemaInfo object that contains the schema information
    //     for the current node.
    //public virtual IXmlSchemaInfo SchemaInfo { get; }
    //
    // Summary:
    //     Gets the current node as a boxed object of the most appropriate .NET Framework
    //     type.
    //
    // Returns:
    //     The current node as a boxed object of the most appropriate .NET Framework
    //     type.
    //public override object TypedValue { get; }
    //
    // Summary:
    //     Used by System.Xml.XPath.XPathNavigator implementations which provide a "virtualized"
    //     XML view over a store, to provide access to underlying objects.
    //
    // Returns:
    //     The default is null.
    //public virtual object UnderlyingObject { get; }
    //
    // Summary:
    //     Gets the current node's value as a System.Boolean.
    //
    // Returns:
    //     The current node's value as a System.Boolean.
    //
    // Exceptions:
    //   System.FormatException:
    //     The current node's string value cannot be converted to a System.Boolean.
    //
    //   System.InvalidCastException:
    //     The attempted cast to System.Boolean is not valid.
    //public override bool ValueAsBoolean { get; }
    //
    // Summary:
    //     Gets the current node's value as a System.DateTime.
    //
    // Returns:
    //     The current node's value as a System.DateTime.
    //
    // Exceptions:
    //   System.FormatException:
    //     The current node's string value cannot be converted to a System.DateTime.
    //
    //   System.InvalidCastException:
    //     The attempted cast to System.DateTime is not valid.
    //public override DateTime ValueAsDateTime { get; }
    ////
    // Summary:
    //     Gets the current node's value as a System.Double.
    //
    // Returns:
    //     The current node's value as a System.Double.
    //
    // Exceptions:
    //   System.FormatException:
    //     The current node's string value cannot be converted to a System.Double.
    //
    //   System.InvalidCastException:
    //     The attempted cast to System.Double is not valid.
    //public override double ValueAsDouble { get; }
    //
    // Summary:
    //     Gets the current node's value as an System.Int32.
    //
    // Returns:
    //     The current node's value as an System.Int32.
    //
    // Exceptions:
    //   System.FormatException:
    //     The current node's string value cannot be converted to a System.Int32.
    //
    //   System.InvalidCastException:
    //     The attempted cast to System.Int32 is not valid.
    //public override int ValueAsInt { get; }
    //
    // Summary:
    //     Gets the current node's value as an System.Int64.
    //
    // Returns:
    //     The current node's value as an System.Int64.
    //
    // Exceptions:
    //   System.FormatException:
    //     The current node's string value cannot be converted to a System.Int64.
    //
    //   System.InvalidCastException:
    //     The attempted cast to System.Int64 is not valid.
    //public override long ValueAsLong { get; }
    //
    // Summary:
    //     Gets the .NET Framework System.Type of the current node.
    //
    // Returns:
    //     The .NET Framework System.Type of the current node. The default value is
    //     System.String.
    //public override Type ValueType { get; }
    ////
    // Summary:
    //     Gets the xml:lang scope for the current node.
    //
    // Returns:
    //     A System.String that contains the value of the xml:lang scope, or System.String.Empty
    //     if the current node has no xml:lang scope value to return.
    public virtual string XmlLang
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the System.Xml.Schema.XmlSchemaType information for the current node.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaType object; default is null.
    //public override XmlSchemaType XmelType { get; }

    // Summary:
    //     Returns an System.Xml.XmlWriter object used to create one or more new child
    //     nodes at the end of the list of child nodes of the current node.
    //
    // Returns:
    //     An System.Xml.XmlWriter object used to create new child nodes at the end
    //     of the list of child nodes of the current node.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The current node the System.Xml.XPath.XPathNavigator is positioned on is
    //     not the root node or an element node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //public virtual XmlWriter AppendChild();
    //
    // Summary:
    //     Creates a new child node at the end of the list of child nodes of the current
    //     node using the XML data string specified.
    //
    // Parameters:
    //   newChild:
    //     The XML data string for the new child node.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The XML data string parameter is null.
    //
    //   System.InvalidOperationException:
    //     The current node the System.Xml.XPath.XPathNavigator is positioned on is
    //     not the root node or an element node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.Xml.XmlException:
    //     The XML data string parameter is not well-formed.
    public virtual void AppendChild(string newChild)
    {
      Contract.Requires(newChild != null);

    }
    //
    // Summary:
    //     Creates a new child node at the end of the list of child nodes of the current
    //     node using the XML contents of the System.Xml.XmlReader object specified.
    //
    // Parameters:
    //   newChild:
    //     An System.Xml.XmlReader object positioned on the XML data for the new child
    //     node.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Xml.XmlReader object is in an error state or closed.
    //
    //   System.ArgumentNullException:
    //     The System.Xml.XmlReader object parameter is null.
    //
    //   System.InvalidOperationException:
    //     The current node the System.Xml.XPath.XPathNavigator is positioned on is
    //     not the root node or an element node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.Xml.XmlException:
    //     The XML contents of the System.Xml.XmlReader object parameter is not well-formed.
    public virtual void AppendChild(XmlReader newChild)
    {
      Contract.Requires(newChild != null);
    }
    //
    // Summary:
    //     Creates a new child node at the end of the list of child nodes of the current
    //     node using the nodes in the System.Xml.XPath.XPathNavigator specified.
    //
    // Parameters:
    //   newChild:
    //     An System.Xml.XPath.XPathNavigator object positioned on the node to add as
    //     the new child node.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Xml.XPath.XPathNavigator object parameter is null.
    //
    //   System.InvalidOperationException:
    //     The current node the System.Xml.XPath.XPathNavigator is positioned on is
    //     not the root node or an element node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    public virtual void AppendChild(XPathNavigator newChild)
    {
      Contract.Requires(newChild != null);
    }
    //
    // Summary:
    //     Creates a new child element node at the end of the list of child nodes of
    //     the current node using the namespace prefix, local name and namespace URI
    //     specified with the value specified.
    //
    // Parameters:
    //   prefix:
    //     The namespace prefix of the new child element node (if any).
    //
    //   localName:
    //     The local name of the new child element node (if any).
    //
    //   namespaceURI:
    //     The namespace URI of the new child element node (if any). System.String.Empty
    //     and null are equivalent.
    //
    //   value:
    //     The value of the new child element node. If System.String.Empty or null are
    //     passed, an empty element is created.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The current node the System.Xml.XPath.XPathNavigator is positioned on is
    //     not the root node or an element node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //public virtual void AppendChildElement(string prefix, string localName, string namespaceURI, string value)
    


    //
    // Summary:
    //     Verifies that the XML data in the System.Xml.XPath.XPathNavigator conforms
    //     to the XML Schema definition language (XSD) schema provided.
    //
    // Parameters:
    //   schemas:
    //     The System.Xml.Schema.XmlSchemaSet containing the schemas used to validate
    //     the XML data contained in the System.Xml.XPath.XPathNavigator.
    //
    //   validationEventHandler:
    //     The System.Xml.Schema.ValidationEventHandler that receives information about
    //     schema validation warnings and errors.
    //
    // Returns:
    //     true if no schema validation errors occurred; otherwise, false.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaValidationException:
    //     A schema validation error occurred, and no System.Xml.Schema.ValidationEventHandler
    //     was specified to handle validation errors.
    //
    //   System.InvalidOperationException:
    //     The System.Xml.XPath.XPathNavigator is positioned on a node that is not an
    //     element, attribute, or the root node or there is not type information to
    //     perform validation.
    //
    //   System.ArgumentException:
    //     The System.Xml.XPath.XPathNavigator.CheckValidity(System.Xml.Schema.XmlSchemaSet,System.Xml.Schema.ValidationEventHandler)
    //     method was called with an System.Xml.Schema.XmlSchemaSet parameter when the
    //     System.Xml.XPath.XPathNavigator was not positioned on the root node of the
    //     XML data.
    //public virtual bool CheckValidity(XmlSchemaSet schemas, ValidationEventHandler validationEventHandler);
    //
    // Summary:
    //     When overridden in a derived class, creates a new System.Xml.XPath.XPathNavigator
    //     positioned at the same node as this System.Xml.XPath.XPathNavigator.
    //
    // Returns:
    //     A new System.Xml.XPath.XPathNavigator positioned at the same node as this
    //     System.Xml.XPath.XPathNavigator.
    public abstract XPathNavigator Clone();

    //
    // Summary:
    //     Compares the position of the current System.Xml.XPath.XPathNavigator with
    //     the position of the System.Xml.XPath.XPathNavigator specified.
    //
    // Parameters:
    //   nav:
    //     The System.Xml.XPath.XPathNavigator to compare against.
    //
    // Returns:
    //     An System.Xml.XmlNodeOrder value representing the comparative position of
    //     the two System.Xml.XPath.XPathNavigator objects.
    //public virtual XmlNodeOrder ComparePosition(XPathNavigator nav);
    //
    // Summary:
    //     Compiles a string representing an XPath expression and returns an System.Xml.XPath.XPathExpression
    //     object.
    //
    // Parameters:
    //   xpath:
    //     A string representing an XPath expression.
    //
    // Returns:
    //     An System.Xml.XPath.XPathExpression object representing the XPath expression.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The xpath parameter contains an XPath expression that is not valid.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    //public virtual XPathExpression Compile(string xpath);
    //
    // Summary:
    //     Creates an attribute node on the current element node using the namespace
    //     prefix, local name and namespace URI specified with the value specified.
    //
    // Parameters:
    //   prefix:
    //     The namespace prefix of the new attribute node (if any).
    //
    //   localName:
    //     The local name of the new attribute node which cannot System.String.Empty
    //     or null.
    //
    //   namespaceURI:
    //     The namespace URI for the new attribute node (if any).
    //
    //   value:
    //     The value of the new attribute node. If System.String.Empty or null are passed,
    //     an empty attribute node is created.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Xml.XPath.XPathNavigator is not positioned on an element node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //public virtual void CreateAttribute(string prefix, string localName, string namespaceURI, string value);
    //
    // Summary:
    //     Returns an System.Xml.XmlWriter object used to create new attributes on the
    //     current element.
    //
    // Returns:
    //     An System.Xml.XmlWriter object used to create new attributes on the current
    //     element.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Xml.XPath.XPathNavigator is not positioned on an element node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //public virtual XmlWriter CreateAttributes();
    //
    // Summary:
    //     Returns a copy of the System.Xml.XPath.XPathNavigator.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNavigator copy of this System.Xml.XPath.XPathNavigator.
    public virtual XPathNavigator CreateNavigator()
    {
      Contract.Ensures(Contract.Result<XPathNavigator>() != null);

      return default(XPathNavigator);
    }
    //
    // Summary:
    //     Deletes a range of sibling nodes from the current node to the node specified.
    //
    // Parameters:
    //   lastSiblingToDelete:
    //     An System.Xml.XPath.XPathNavigator positioned on the last sibling node in
    //     the range to delete.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Xml.XPath.XPathNavigator specified is null.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.InvalidOperationException:
    //     The last node to delete specified is not a valid sibling node of the current
    //     node.
    public virtual void DeleteRange(XPathNavigator lastSiblingToDelete)
    {
      Contract.Requires(lastSiblingToDelete != null);
    }
    //
    // Summary:
    //     Deletes the current node and its child nodes.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Xml.XPath.XPathNavigator is positioned on a node that cannot be
    //     deleted such as the root node or a namespace node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //public virtual void DeleteSelf();
    //
    // Summary:
    //     Evaluates the specified XPath expression and returns the typed result.
    //
    // Parameters:
    //   xpath:
    //     A string representing an XPath expression that can be evaluated.
    //
    // Returns:
    //     The result of the expression (Boolean, number, string, or node set). This
    //     maps to System.Boolean, System.Double, System.String, or System.Xml.XPath.XPathNodeIterator
    //     objects respectively.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The return type of the XPath expression is a node set.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    //public virtual object Evaluate(string xpath);
    //
    // Summary:
    //     Evaluates the System.Xml.XPath.XPathExpression and returns the typed result.
    //
    // Parameters:
    //   expr:
    //     An System.Xml.XPath.XPathExpression that can be evaluated.
    //
    // Returns:
    //     The result of the expression (Boolean, number, string, or node set). This
    //     maps to System.Boolean, System.Double, System.String, or System.Xml.XPath.XPathNodeIterator
    //     objects respectively.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The return type of the XPath expression is a node set.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    //public virtual object Evaluate(XPathExpression expr);
    //
    // Summary:
    //     Evaluates the specified XPath expression and returns the typed result, using
    //     the System.Xml.IXmlNamespaceResolver object specified to resolve namespace
    //     prefixes in the XPath expression.
    //
    // Parameters:
    //   xpath:
    //     A string representing an XPath expression that can be evaluated.
    //
    //   resolver:
    //     The System.Xml.IXmlNamespaceResolver object used to resolve namespace prefixes
    //     in the XPath expression.
    //
    // Returns:
    //     The result of the expression (Boolean, number, string, or node set). This
    //     maps to System.Boolean, System.Double, System.String, or System.Xml.XPath.XPathNodeIterator
    //     objects respectively.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The return type of the XPath expression is a node set.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    //public virtual object Evaluate(string xpath, IXmlNamespaceResolver resolver);
    //
    // Summary:
    //     Uses the supplied context to evaluate the System.Xml.XPath.XPathExpression,
    //     and returns the typed result.
    //
    // Parameters:
    //   expr:
    //     An System.Xml.XPath.XPathExpression that can be evaluated.
    //
    //   context:
    //     An System.Xml.XPath.XPathNodeIterator that points to the selected node set
    //     that the evaluation is to be performed on.
    //
    // Returns:
    //     The result of the expression (Boolean, number, string, or node set). This
    //     maps to System.Boolean, System.Double, System.String, or System.Xml.XPath.XPathNodeIterator
    //     objects respectively.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The return type of the XPath expression is a node set.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    //public virtual object Evaluate(XPathExpression expr, XPathNodeIterator context);
    //
    // Summary:
    //     Gets the value of the attribute with the specified local name and namespace
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
    //     A System.String that contains the value of the specified attribute; System.String.Empty
    //     if a matching attribute is not found, or if the System.Xml.XPath.XPathNavigator
    //     is not positioned on an element node.
    public virtual string GetAttribute(string localName, string namespaceURI)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Returns the value of the namespace node corresponding to the specified local
    //     name.
    //
    // Parameters:
    //   name:
    //     The local name of the namespace node.
    //
    // Returns:
    //     A System.String that contains the value of the namespace node; System.String.Empty
    //     if a matching namespace node is not found, or if the System.Xml.XPath.XPathNavigator
    //     is not positioned on an element node.
    public virtual string GetNamespace(string name)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Returns the in-scope namespaces of the current node.
    //
    // Parameters:
    //   scope:
    //     An System.Xml.XmlNamespaceScope value specifying the namespaces to return.
    //
    // Returns:
    //     An System.Collections.Generic.IDictionary<TKey,TValue> collection of namespace
    //     names keyed by prefix.
    //public virtual IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope);
    //
    // Summary:
    //     Returns an System.Xml.XmlWriter object used to create a new sibling node
    //     after the currently selected node.
    //
    // Returns:
    //     An System.Xml.XmlWriter object used to create a new sibling node after the
    //     currently selected node.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The position of the System.Xml.XPath.XPathNavigator does not allow a new
    //     sibling node to be inserted after the current node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //public virtual XmlWriter InsertAfter();
    //
    // Summary:
    //     Creates a new sibling node after the currently selected node using the XML
    //     string specified.
    //
    // Parameters:
    //   newSibling:
    //     The XML data string for the new sibling node.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The XML string parameter is null.
    //
    //   System.InvalidOperationException:
    //     The position of the System.Xml.XPath.XPathNavigator does not allow a new
    //     sibling node to be inserted after the current node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.Xml.XmlException:
    //     The XML string parameter is not well-formed.
    public virtual void InsertAfter(string newSibling)
    {
      Contract.Requires(newSibling != null);

    }
    //
    // Summary:
    //     Creates a new sibling node after the currently selected node using the XML
    //     contents of the System.Xml.XmlReader object specified.
    //
    // Parameters:
    //   newSibling:
    //     An System.Xml.XmlReader object positioned on the XML data for the new sibling
    //     node.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Xml.XmlReader object is in an error state or closed.
    //
    //   System.ArgumentNullException:
    //     The System.Xml.XmlReader object parameter is null.
    //
    //   System.InvalidOperationException:
    //     The position of the System.Xml.XPath.XPathNavigator does not allow a new
    //     sibling node to be inserted after the current node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.Xml.XmlException:
    //     The XML contents of the System.Xml.XmlReader object parameter is not well-formed.
    public virtual void InsertAfter(XmlReader newSibling)
    {
      Contract.Requires(newSibling != null);

    }

    //
    // Summary:
    //     Creates a new sibling node after the currently selected node using the nodes
    //     in the System.Xml.XPath.XPathNavigator object specified.
    //
    // Parameters:
    //   newSibling:
    //     An System.Xml.XPath.XPathNavigator object positioned on the node to add as
    //     the new sibling node.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Xml.XPath.XPathNavigator object parameter is null.
    //
    //   System.InvalidOperationException:
    //     The position of the System.Xml.XPath.XPathNavigator does not allow a new
    //     sibling node to be inserted after the current node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    public virtual void InsertAfter(XPathNavigator newSibling)
    {
      Contract.Requires(newSibling != null);

    }
    //
    // Summary:
    //     Returns an System.Xml.XmlWriter object used to create a new sibling node
    //     before the currently selected node.
    //
    // Returns:
    //     An System.Xml.XmlWriter object used to create a new sibling node before the
    //     currently selected node.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The position of the System.Xml.XPath.XPathNavigator does not allow a new
    //     sibling node to be inserted before the current node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //public virtual XmlWriter InsertBefore();
    //
    // Summary:
    //     Creates a new sibling node before the currently selected node using the XML
    //     string specified.
    //
    // Parameters:
    //   newSibling:
    //     The XML data string for the new sibling node.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The XML string parameter is null.
    //
    //   System.InvalidOperationException:
    //     The position of the System.Xml.XPath.XPathNavigator does not allow a new
    //     sibling node to be inserted before the current node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.Xml.XmlException:
    //     The XML string parameter is not well-formed.
    public virtual void InsertBefore(string newSibling)
    {
      Contract.Requires(newSibling != null);

    }
    //
    // Summary:
    //     Creates a new sibling node before the currently selected node using the XML
    //     contents of the System.Xml.XmlReader object specified.
    //
    // Parameters:
    //   newSibling:
    //     An System.Xml.XmlReader object positioned on the XML data for the new sibling
    //     node.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Xml.XmlReader object is in an error state or closed.
    //
    //   System.ArgumentNullException:
    //     The System.Xml.XmlReader object parameter is null.
    //
    //   System.InvalidOperationException:
    //     The position of the System.Xml.XPath.XPathNavigator does not allow a new
    //     sibling node to be inserted before the current node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.Xml.XmlException:
    //     The XML contents of the System.Xml.XmlReader object parameter is not well-formed.
    public virtual void InsertBefore(XmlReader newSibling)
    {
      Contract.Requires(newSibling != null);
    }
    //
    // Summary:
    //     Creates a new sibling node before the currently selected node using the nodes
    //     in the System.Xml.XPath.XPathNavigator specified.
    //
    // Parameters:
    //   newSibling:
    //     An System.Xml.XPath.XPathNavigator object positioned on the node to add as
    //     the new sibling node.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Xml.XPath.XPathNavigator object parameter is null.
    //
    //   System.InvalidOperationException:
    //     The position of the System.Xml.XPath.XPathNavigator does not allow a new
    //     sibling node to be inserted before the current node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    public virtual void InsertBefore(XPathNavigator newSibling)
    {
      Contract.Requires(newSibling != null);

    }
    //
    // Summary:
    //     Creates a new sibling element after the current node using the namespace
    //     prefix, local name and namespace URI specified, with the value specified.
    //
    // Parameters:
    //   prefix:
    //     The namespace prefix of the new child element (if any).
    //
    //   localName:
    //     The local name of the new child element (if any).
    //
    //   namespaceURI:
    //     The namespace URI of the new child element (if any). System.String.Empty
    //     and null are equivalent.
    //
    //   value:
    //     The value of the new child element. If System.String.Empty or null are passed,
    //     an empty element is created.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The position of the System.Xml.XPath.XPathNavigator does not allow a new
    //     sibling node to be inserted after the current node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //public virtual void InsertElementAfter(string prefix, string localName, string namespaceURI, string value);
    //
    // Summary:
    //     Creates a new sibling element before the current node using the namespace
    //     prefix, local name, and namespace URI specified, with the value specified.
    //
    // Parameters:
    //   prefix:
    //     The namespace prefix of the new child element (if any).
    //
    //   localName:
    //     The local name of the new child element (if any).
    //
    //   namespaceURI:
    //     The namespace URI of the new child element (if any). System.String.Empty
    //     and null are equivalent.
    //
    //   value:
    //     The value of the new child element. If System.String.Empty or null are passed,
    //     an empty element is created.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The position of the System.Xml.XPath.XPathNavigator does not allow a new
    //     sibling node to be inserted before the current node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //public virtual void InsertElementBefore(string prefix, string localName, string namespaceURI, string value);
    //
    // Summary:
    //     Determines whether the specified System.Xml.XPath.XPathNavigator is a descendant
    //     of the current System.Xml.XPath.XPathNavigator.
    //
    // Parameters:
    //   nav:
    //     The System.Xml.XPath.XPathNavigator to compare to this System.Xml.XPath.XPathNavigator.
    //
    // Returns:
    //     Returns true if the specified System.Xml.XPath.XPathNavigator is a descendant
    //     of the current System.Xml.XPath.XPathNavigator; otherwise, false.
    //public virtual bool IsDescendant(XPathNavigator nav);
    //
    // Summary:
    //     When overridden in a derived class, determines whether the current System.Xml.XPath.XPathNavigator
    //     is at the same position as the specified System.Xml.XPath.XPathNavigator.
    //
    // Parameters:
    //   other:
    //     The System.Xml.XPath.XPathNavigator to compare to this System.Xml.XPath.XPathNavigator.
    //
    // Returns:
    //     Returns true if the two System.Xml.XPath.XPathNavigator objects have the
    //     same position; otherwise, false.
    //public abstract bool IsSamePosition(XPathNavigator other);
    //
    // Summary:
    //     Gets the namespace URI for the specified prefix.
    //
    // Parameters:
    //   prefix:
    //     The prefix whose namespace URI you want to resolve. To match the default
    //     namespace, pass System.String.Empty.
    //
    // Returns:
    //     A System.String that contains the namespace URI assigned to the namespace
    //     prefix specified; null if no namespace URI is assigned to the prefix specified.
    //     The System.String returned is atomized.
    //public virtual string LookupNamespace(string prefix);
    //
    // Summary:
    //     Gets the prefix declared for the specified namespace URI.
    //
    // Parameters:
    //   namespaceURI:
    //     The namespace URI to resolve for the prefix.
    //
    // Returns:
    //     A System.String that contains the namespace prefix assigned to the namespace
    //     URI specified; otherwise, System.String.Empty if no prefix is assigned to
    //     the namespace URI specified. The System.String returned is atomized.
    //public virtual string LookupPrefix(string namespaceURI);
    //
    // Summary:
    //     Determines whether the current node matches the specified XPath expression.
    //
    // Parameters:
    //   xpath:
    //     The XPath expression.
    //
    // Returns:
    //     Returns true if the current node matches the specified XPath expression;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The XPath expression cannot be evaluated.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    //public virtual bool Matches(string xpath);
    //
    // Summary:
    //     Determines whether the current node matches the specified System.Xml.XPath.XPathExpression.
    //
    // Parameters:
    //   expr:
    //     An System.Xml.XPath.XPathExpression object containing the compiled XPath
    //     expression.
    //
    // Returns:
    //     Returns true if the current node matches the System.Xml.XPath.XPathExpression;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The XPath expression cannot be evaluated.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    //public virtual bool Matches(XPathExpression expr);
    ////
    // Summary:
    //     When overridden in a derived class, moves the System.Xml.XPath.XPathNavigator
    //     to the same position as the specified System.Xml.XPath.XPathNavigator.
    //
    // Parameters:
    //   other:
    //     The System.Xml.XPath.XPathNavigator positioned on the node that you want
    //     to move to.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the same position as the specified System.Xml.XPath.XPathNavigator; otherwise,
    //     false. If false, the position of the System.Xml.XPath.XPathNavigator is unchanged.
    //public abstract bool MoveTo(XPathNavigator other);
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the attribute with the matching
    //     local name and namespace URI.
    //
    // Parameters:
    //   localName:
    //     The local name of the attribute.
    //
    //   namespaceURI:
    //     The namespace URI of the attribute; null for an empty namespace.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the attribute; otherwise, false. If false, the position of the System.Xml.XPath.XPathNavigator
    //     is unchanged.
    //public virtual bool MoveToAttribute(string localName, string namespaceURI);
    ////
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the child node of the System.Xml.XPath.XPathNodeType
    //     specified.
    //
    // Parameters:
    //   type:
    //     The System.Xml.XPath.XPathNodeType of the child node to move to.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the child node; otherwise, false. If false, the position of the System.Xml.XPath.XPathNavigator
    //     is unchanged.
    //public virtual bool MoveToChild(XPathNodeType type);
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the child node with the local
    //     name and namespace URI specified.
    //
    // Parameters:
    //   localName:
    //     The local name of the child node to move to.
    //
    //   namespaceURI:
    //     The namespace URI of the child node to move to.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the child node; otherwise, false. If false, the position of the System.Xml.XPath.XPathNavigator
    //     is unchanged.
    //public virtual bool MoveToChild(string localName, string namespaceURI);
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the first sibling node of the
    //     current node.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the first sibling node of the current node; false if there is no first
    //     sibling, or if the System.Xml.XPath.XPathNavigator is currently positioned
    //     on an attribute node. If the System.Xml.XPath.XPathNavigator is already positioned
    //     on the first sibling, System.Xml.XPath.XPathNavigator will return true and
    //     will not move its position.  If System.Xml.XPath.XPathNavigator.MoveToFirst
    //     returns false because there is no first sibling, or if System.Xml.XPath.XPathNavigator
    //     is currently positioned on an attribute, the position of the System.Xml.XPath.XPathNavigator
    //     is unchanged.
    //public virtual bool MoveToFirst();
    //
    // Summary:
    //     When overridden in a derived class, moves the System.Xml.XPath.XPathNavigator
    //     to the first attribute of the current node.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the first attribute of the current node; otherwise, false. If false, the
    //     position of the System.Xml.XPath.XPathNavigator is unchanged.
    //public abstract bool MoveToFirstAttribute();
    //
    // Summary:
    //     When overridden in a derived class, moves the System.Xml.XPath.XPathNavigator
    //     to the first child node of the current node.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the first child node of the current node; otherwise, false. If false,
    //     the position of the System.Xml.XPath.XPathNavigator is unchanged.
    //public abstract bool MoveToFirstChild();
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to first namespace node of the
    //     current node.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the first namespace node; otherwise, false. If false, the position of
    //     the System.Xml.XPath.XPathNavigator is unchanged.
    //public bool MoveToFirstNamespace();
    //
    // Summary:
    //     When overridden in a derived class, moves the System.Xml.XPath.XPathNavigator
    //     to the first namespace node that matches the System.Xml.XPath.XPathNamespaceScope
    //     specified.
    //
    // Parameters:
    //   namespaceScope:
    //     An System.Xml.XPath.XPathNamespaceScope value describing the namespace scope.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the first namespace node; otherwise, false. If false, the position of
    //     the System.Xml.XPath.XPathNavigator is unchanged.
    //public abstract bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope);
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the following element of the
    //     System.Xml.XPath.XPathNodeType specified in document order.
    //
    // Parameters:
    //   type:
    //     The System.Xml.XPath.XPathNodeType of the element. The System.Xml.XPath.XPathNodeType
    //     cannot be System.Xml.XPath.XPathNodeType.Attribute or System.Xml.XPath.XPathNodeType.Namespace.
    //
    // Returns:
    //     true if the System.Xml.XPath.XPathNavigator moved successfully; otherwise
    //     false.
    //public virtual bool MoveToFollowing(XPathNodeType type);
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the element with the local name
    //     and namespace URI specified in document order.
    //
    // Parameters:
    //   localName:
    //     The local name of the element.
    //
    //   namespaceURI:
    //     The namespace URI of the element.
    //
    // Returns:
    //     true if the System.Xml.XPath.XPathNavigator moved successfully; otherwise
    //     false.
    //public virtual bool MoveToFollowing(string localName, string namespaceURI);
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the following element of the
    //     System.Xml.XPath.XPathNodeType specified, to the boundary specified, in document
    //     order.
    //
    // Parameters:
    //   type:
    //     The System.Xml.XPath.XPathNodeType of the element. The System.Xml.XPath.XPathNodeType
    //     cannot be System.Xml.XPath.XPathNodeType.Attribute or System.Xml.XPath.XPathNodeType.Namespace.
    //
    //   end:
    //     The System.Xml.XPath.XPathNavigator object positioned on the element boundary
    //     which the current System.Xml.XPath.XPathNavigator will not move past while
    //     searching for the following element.
    //
    // Returns:
    //     true if the System.Xml.XPath.XPathNavigator moved successfully; otherwise
    //     false.
    //public virtual bool MoveToFollowing(XPathNodeType type, XPathNavigator end);
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the element with the local name
    //     and namespace URI specified, to the boundary specified, in document order.
    //
    // Parameters:
    //   localName:
    //     The local name of the element.
    //
    //   namespaceURI:
    //     The namespace URI of the element.
    //
    //   end:
    //     The System.Xml.XPath.XPathNavigator object positioned on the element boundary
    //     which the current System.Xml.XPath.XPathNavigator will not move past while
    //     searching for the following element.
    //
    // Returns:
    //     true if the System.Xml.XPath.XPathNavigator moved successfully; otherwise
    //     false.
    //public virtual bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end);
    //
    // Summary:
    //     When overridden in a derived class, moves to the node that has an attribute
    //     of type ID whose value matches the specified System.String.
    //
    // Parameters:
    //   id:
    //     A System.String representing the ID value of the node to which you want to
    //     move.
    //
    // Returns:
    //     true if the System.Xml.XPath.XPathNavigator is successful moving; otherwise,
    //     false. If false, the position of the navigator is unchanged.
    //public abstract bool MoveToId(string id);
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the namespace node with the
    //     specified namespace prefix.
    //
    // Parameters:
    //   name:
    //     The namespace prefix of the namespace node.
    //
    // Returns:
    //     true if the System.Xml.XPath.XPathNavigator is successful moving to the specified
    //     namespace; false if a matching namespace node was not found, or if the System.Xml.XPath.XPathNavigator
    //     is not positioned on an element node. If false, the position of the System.Xml.XPath.XPathNavigator
    //     is unchanged.
    //public virtual bool MoveToNamespace(string name);
    //
    // Summary:
    //     When overridden in a derived class, moves the System.Xml.XPath.XPathNavigator
    //     to the next sibling node of the current node.
    //
    // Returns:
    //     true if the System.Xml.XPath.XPathNavigator is successful moving to the next
    //     sibling node; otherwise, false if there are no more siblings or if the System.Xml.XPath.XPathNavigator
    //     is currently positioned on an attribute node. If false, the position of the
    //     System.Xml.XPath.XPathNavigator is unchanged.
    //public abstract bool MoveToNext();
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the next sibling node of the
    //     current node that matches the System.Xml.XPath.XPathNodeType specified.
    //
    // Parameters:
    //   type:
    //     The System.Xml.XPath.XPathNodeType of the sibling node to move to.
    //
    // Returns:
    //     true if the System.Xml.XPath.XPathNavigator is successful moving to the next
    //     sibling node; otherwise, false if there are no more siblings or if the System.Xml.XPath.XPathNavigator
    //     is currently positioned on an attribute node. If false, the position of the
    //     System.Xml.XPath.XPathNavigator is unchanged.
    //public virtual bool MoveToNext(XPathNodeType type);
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the next sibling node with the
    //     local name and namespace URI specified.
    //
    // Parameters:
    //   localName:
    //     The local name of the next sibling node to move to.
    //
    //   namespaceURI:
    //     The namespace URI of the next sibling node to move to.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the next sibling node; false if there are no more siblings, or if the
    //     System.Xml.XPath.XPathNavigator is currently positioned on an attribute node.
    //     If false, the position of the System.Xml.XPath.XPathNavigator is unchanged.
    //public virtual bool MoveToNext(string localName, string namespaceURI);
    //
    // Summary:
    //     When overridden in a derived class, moves the System.Xml.XPath.XPathNavigator
    //     to the next attribute.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the next attribute; false if there are no more attributes. If false, the
    //     position of the System.Xml.XPath.XPathNavigator is unchanged.
    //public abstract bool MoveToNextAttribute();
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the next namespace node.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the next namespace node; otherwise, false. If false, the position of the
    //     System.Xml.XPath.XPathNavigator is unchanged.
    //public bool MoveToNextNamespace();
    //
    // Summary:
    //     When overridden in a derived class, moves the System.Xml.XPath.XPathNavigator
    //     to the next namespace node matching the System.Xml.XPath.XPathNamespaceScope
    //     specified.
    //
    // Parameters:
    //   namespaceScope:
    //     An System.Xml.XPath.XPathNamespaceScope value describing the namespace scope.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the next namespace node; otherwise, false. If false, the position of the
    //     System.Xml.XPath.XPathNavigator is unchanged.
    //public abstract bool MoveToNextNamespace(XPathNamespaceScope namespaceScope);
    //
    // Summary:
    //     When overridden in a derived class, moves the System.Xml.XPath.XPathNavigator
    //     to the parent node of the current node.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the parent node of the current node; otherwise, false. If false, the position
    //     of the System.Xml.XPath.XPathNavigator is unchanged.
    //public abstract bool MoveToParent();
    //
    // Summary:
    //     When overridden in a derived class, moves the System.Xml.XPath.XPathNavigator
    //     to the previous sibling node of the current node.
    //
    // Returns:
    //     Returns true if the System.Xml.XPath.XPathNavigator is successful moving
    //     to the previous sibling node; otherwise, false if there is no previous sibling
    //     node or if the System.Xml.XPath.XPathNavigator is currently positioned on
    //     an attribute node. If false, the position of the System.Xml.XPath.XPathNavigator
    //     is unchanged.
    //public abstract bool MoveToPrevious();
    //
    // Summary:
    //     Moves the System.Xml.XPath.XPathNavigator to the root node that the current
    //     node belongs to.
    //public virtual void MoveToRoot();
    //
    // Summary:
    //     Returns an System.Xml.XmlWriter object used to create a new child node at
    //     the beginning of the list of child nodes of the current node.
    //
    // Returns:
    //     An System.Xml.XmlWriter object used to create a new child node at the beginning
    //     of the list of child nodes of the current node.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The current node the System.Xml.XPath.XPathNavigator is positioned on does
    //     not allow a new child node to be prepended.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //public virtual XmlWriter PrependChild();
    //
    // Summary:
    //     Creates a new child node at the beginning of the list of child nodes of the
    //     current node using the XML string specified.
    //
    // Parameters:
    //   newChild:
    //     The XML data string for the new child node.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The XML string parameter is null.
    //
    //   System.InvalidOperationException:
    //     The current node the System.Xml.XPath.XPathNavigator is positioned on does
    //     not allow a new child node to be prepended.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.Xml.XmlException:
    //     The XML string parameter is not well-formed.
    public virtual void PrependChild(string newChild)
    {
      Contract.Requires(newChild != null);

    }
    //
    // Summary:
    //     Creates a new child node at the beginning of the list of child nodes of the
    //     current node using the XML contents of the System.Xml.XmlReader object specified.
    //
    // Parameters:
    //   newChild:
    //     An System.Xml.XmlReader object positioned on the XML data for the new child
    //     node.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Xml.XmlReader object is in an error state or closed.
    //
    //   System.ArgumentNullException:
    //     The System.Xml.XmlReader object parameter is null.
    //
    //   System.InvalidOperationException:
    //     The current node the System.Xml.XPath.XPathNavigator is positioned on does
    //     not allow a new child node to be prepended.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.Xml.XmlException:
    //     The XML contents of the System.Xml.XmlReader object parameter is not well-formed.
    public virtual void PrependChild(XmlReader newChild)
    {
      Contract.Requires(newChild != null);

    }
    //
    // Summary:
    //     Creates a new child node at the beginning of the list of child nodes of the
    //     current node using the nodes in the System.Xml.XPath.XPathNavigator object
    //     specified.
    //
    // Parameters:
    //   newChild:
    //     An System.Xml.XPath.XPathNavigator object positioned on the node to add as
    //     the new child node.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Xml.XPath.XPathNavigator object parameter is null.
    //
    //   System.InvalidOperationException:
    //     The current node the System.Xml.XPath.XPathNavigator is positioned on does
    //     not allow a new child node to be prepended.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    public virtual void PrependChild(XPathNavigator newChild)
    {
      Contract.Requires(newChild != null);

    }
    //
    // Summary:
    //     Creates a new child element at the beginning of the list of child nodes of
    //     the current node using the namespace prefix, local name, and namespace URI
    //     specified with the value specified.
    //
    // Parameters:
    //   prefix:
    //     The namespace prefix of the new child element (if any).
    //
    //   localName:
    //     The local name of the new child element (if any).
    //
    //   namespaceURI:
    //     The namespace URI of the new child element (if any). System.String.Empty
    //     and null are equivalent.
    //
    //   value:
    //     The value of the new child element. If System.String.Empty or null are passed,
    //     an empty element is created.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The current node the System.Xml.XPath.XPathNavigator is positioned on does
    //     not allow a new child node to be prepended.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //public virtual void PrependChildElement(string prefix, string localName, string namespaceURI, string value);
    //
    // Summary:
    //     Returns an System.Xml.XmlReader object that contains the current node and
    //     its child nodes.
    //
    // Returns:
    //     An System.Xml.XmlReader object that contains the current node and its child
    //     nodes.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Xml.XPath.XPathNavigator is not positioned on an element node
    //     or the root node.
    //public virtual XmlReader ReadSubtree();
    //
    // Summary:
    //     Replaces a range of sibling nodes from the current node to the node specified.
    //
    // Parameters:
    //   lastSiblingToReplace:
    //     An System.Xml.XPath.XPathNavigator positioned on the last sibling node in
    //     the range to replace.
    //
    // Returns:
    //     An System.Xml.XmlWriter object used to specify the replacement range.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Xml.XPath.XPathNavigator specified is null.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.InvalidOperationException:
    //     The last node to replace specified is not a valid sibling node of the current
    //     node.
    public virtual XmlWriter ReplaceRange(XPathNavigator lastSiblingToReplace)
    {
      Contract.Requires(lastSiblingToReplace != null);

      return default(XmlWriter);
    }
    //
    // Summary:
    //     Replaces the current node with the content of the string specified.
    //
    // Parameters:
    //   newNode:
    //     The XML data string for the new node.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The XML string parameter is null.
    //
    //   System.InvalidOperationException:
    //     The System.Xml.XPath.XPathNavigator is not positioned on an element, text,
    //     processing instruction, or comment node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.Xml.XmlException:
    //     The XML string parameter is not well-formed.
    public virtual void ReplaceSelf(string newNode)
    {
      Contract.Requires(newNode != null);

    }
    //
    // Summary:
    //     Replaces the current node with the contents of the System.Xml.XmlReader object
    //     specified.
    //
    // Parameters:
    //   newNode:
    //     An System.Xml.XmlReader object positioned on the XML data for the new node.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Xml.XmlReader object is in an error state or closed.
    //
    //   System.ArgumentNullException:
    //     The System.Xml.XmlReader object parameter is null.
    //
    //   System.InvalidOperationException:
    //     The System.Xml.XPath.XPathNavigator is not positioned on an element, text,
    //     processing instruction, or comment node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.Xml.XmlException:
    //     The XML contents of the System.Xml.XmlReader object parameter is not well-formed.
    public virtual void ReplaceSelf(XmlReader newNode)
    {
      Contract.Requires(newNode != null);

    }
    //
    // Summary:
    //     Replaces the current node with the contents of the System.Xml.XPath.XPathNavigator
    //     object specified.
    //
    // Parameters:
    //   newNode:
    //     An System.Xml.XPath.XPathNavigator object positioned on the new node.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Xml.XPath.XPathNavigator object parameter is null.
    //
    //   System.InvalidOperationException:
    //     The System.Xml.XPath.XPathNavigator is not positioned on an element, text,
    //     processing instruction, or comment node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    //
    //   System.Xml.XmlException:
    //     The XML contents of the System.Xml.XPath.XPathNavigator object parameter
    //     is not well-formed.
    public virtual void ReplaceSelf(XPathNavigator newNode)
    {
      Contract.Requires(newNode != null);

    }
    //
    // Summary:
    //     Selects a node set, using the specified XPath expression.
    //
    // Parameters:
    //   xpath:
    //     A System.String representing an XPath expression.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNodeIterator pointing to the selected node set.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The XPath expression contains an error or its return type is not a node set.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    public virtual XPathNodeIterator Select(string xpath) {
      Contract.Ensures(Contract.Result<XPathNodeIterator>() != null);
      return default(XPathNodeIterator);
    }
    //
    // Summary:
    //     Selects a node set using the specified System.Xml.XPath.XPathExpression.
    //
    // Parameters:
    //   expr:
    //     An System.Xml.XPath.XPathExpression object containing the compiled XPath
    //     query.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNodeIterator that points to the selected node set.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The XPath expression contains an error or its return type is not a node set.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    public virtual XPathNodeIterator Select(XPathExpression expr) {
      Contract.Ensures(Contract.Result<XPathNodeIterator>() != null);
      return default(XPathNodeIterator);
    }
    //
    // Summary:
    //     Selects a node set using the specified XPath expression with the System.Xml.IXmlNamespaceResolver
    //     object specified to resolve namespace prefixes.
    //
    // Parameters:
    //   xpath:
    //     A System.String representing an XPath expression.
    //
    //   resolver:
    //     The System.Xml.IXmlNamespaceResolver object used to resolve namespace prefixes.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNodeIterator that points to the selected node set.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The XPath expression contains an error or its return type is not a node set.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    public virtual XPathNodeIterator Select(string xpath, IXmlNamespaceResolver resolver) {
      Contract.Ensures(Contract.Result<XPathNodeIterator>() != null);
      return default(XPathNodeIterator);
    }
    //
    // Summary:
    //     Selects all the ancestor nodes of the current node that have a matching System.Xml.XPath.XPathNodeType.
    //
    // Parameters:
    //   type:
    //     The System.Xml.XPath.XPathNodeType of the ancestor nodes.
    //
    //   matchSelf:
    //     To include the context node in the selection, true; otherwise, false.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNodeIterator that contains the selected nodes. The
    //     returned nodes are in reverse document order.
    //public virtual XPathNodeIterator SelectAncestors(XPathNodeType type, bool matchSelf);
    //
    // Summary:
    //     Selects all the ancestor nodes of the current node that have the specified
    //     local name and namespace URI.
    //
    // Parameters:
    //   name:
    //     The local name of the ancestor nodes.
    //
    //   namespaceURI:
    //     The namespace URI of the ancestor nodes.
    //
    //   matchSelf:
    //     To include the context node in the selection, true; otherwise, false.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNodeIterator that contains the selected nodes. The
    //     returned nodes are in reverse document order.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     null cannot be passed as a parameter.
    public virtual XPathNodeIterator SelectAncestors(string name, string namespaceURI, bool matchSelf)
    {
      Contract.Requires(name != null);
      Contract.Requires(namespaceURI != null);

      return default(XPathNodeIterator);
    }
    //
    // Summary:
    //     Selects all the child nodes of the current node that have the matching System.Xml.XPath.XPathNodeType.
    //
    // Parameters:
    //   type:
    //     The System.Xml.XPath.XPathNodeType of the child nodes.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNodeIterator that contains the selected nodes.
    //public virtual XPathNodeIterator SelectChildren(XPathNodeType type);
    //
    // Summary:
    //     Selects all the child nodes of the current node that have the local name
    //     and namespace URI specified.
    //
    // Parameters:
    //   name:
    //     The local name of the child nodes.
    //
    //   namespaceURI:
    //     The namespace URI of the child nodes.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNodeIterator that contains the selected nodes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     null cannot be passed as a parameter.
    public virtual XPathNodeIterator SelectChildren(string name, string namespaceURI)
    {
      Contract.Requires(name != null);
      Contract.Requires(namespaceURI != null);

      return default(XPathNodeIterator);
    }
    //
    // Summary:
    //     Selects all the descendant nodes of the current node that have a matching
    //     System.Xml.XPath.XPathNodeType.
    //
    // Parameters:
    //   type:
    //     The System.Xml.XPath.XPathNodeType of the descendant nodes.
    //
    //   matchSelf:
    //     true to include the context node in the selection; otherwise, false.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNodeIterator that contains the selected nodes.
    //public virtual XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf);
    //
    // Summary:
    //     Selects all the descendant nodes of the current node with the local name
    //     and namespace URI specified.
    //
    // Parameters:
    //   name:
    //     The local name of the descendant nodes.
    //
    //   namespaceURI:
    //     The namespace URI of the descendant nodes.
    //
    //   matchSelf:
    //     true to include the context node in the selection; otherwise, false.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNodeIterator that contains the selected nodes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     null cannot be passed as a parameter.
    public virtual XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
    {
      Contract.Requires(name != null);
      Contract.Requires(namespaceURI != null);

      return default(XPathNodeIterator);

    }
    //
    // Summary:
    //     Selects a single node in the System.Xml.XPath.XPathNavigator using the specified
    //     XPath query.
    //
    // Parameters:
    //   xpath:
    //     A System.String representing an XPath expression.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNavigator object that contains the first matching
    //     node for the XPath query specified; otherwise, null if there are no query
    //     results.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An error was encountered in the XPath query or the return type of the XPath
    //     expression is not a node.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath query is not valid.
    //public virtual XPathNavigator SelectSingleNode(string xpath);
    //
    // Summary:
    //     Selects a single node in the System.Xml.XPath.XPathNavigator using the specified
    //     System.Xml.XPath.XPathExpression object.
    //
    // Parameters:
    //   expression:
    //     An System.Xml.XPath.XPathExpression object containing the compiled XPath
    //     query.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNavigator object that contains the first matching
    //     node for the XPath query specified; otherwise null if there are no query
    //     results.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An error was encountered in the XPath query or the return type of the XPath
    //     expression is not a node.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath query is not valid.
    //public virtual XPathNavigator SelectSingleNode(XPathExpression expression);
    //
    // Summary:
    //     Selects a single node in the System.Xml.XPath.XPathNavigator object using
    //     the specified XPath query with the System.Xml.IXmlNamespaceResolver object
    //     specified to resolve namespace prefixes.
    //
    // Parameters:
    //   xpath:
    //     A System.String representing an XPath expression.
    //
    //   resolver:
    //     The System.Xml.IXmlNamespaceResolver object used to resolve namespace prefixes
    //     in the XPath query.
    //
    // Returns:
    //     An System.Xml.XPath.XPathNavigator object that contains the first matching
    //     node for the XPath query specified; otherwise null if there are no query
    //     results.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An error was encountered in the XPath query or the return type of the XPath
    //     expression is not a node.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath query is not valid.
    //public virtual XPathNavigator SelectSingleNode(string xpath, IXmlNamespaceResolver resolver);
    //
    // Summary:
    //     Sets the typed value of the current node.
    //
    // Parameters:
    //   typedValue:
    //     The new typed value of the node.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Xml.XPath.XPathNavigator does not support the type of the object
    //     specified.
    //
    //   System.ArgumentNullException:
    //     The value specified cannot be null.
    //
    //   System.InvalidOperationException:
    //     The System.Xml.XPath.XPathNavigator is not positioned on an element or attribute
    //     node.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    public virtual void SetTypedValue(object typedValue)
    {
      Contract.Requires(typedValue != null);

    }
    //
    // Summary:
    //     Sets the value of the current node.
    //
    // Parameters:
    //   value:
    //     The new value of the node.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value parameter is null.
    //
    //   System.InvalidOperationException:
    //     The System.Xml.XPath.XPathNavigator is positioned on the root node, a namespace
    //     node, or the specified value is invalid.
    //
    //   System.NotSupportedException:
    //     The System.Xml.XPath.XPathNavigator does not support editing.
    public virtual void SetValue(string value)
    {
      Contract.Requires(value != null);

    }
    
    //
    // Summary:
    //     Gets the current node's value as the System.Type specified, using the System.Xml.IXmlNamespaceResolver
    //     object specified to resolve namespace prefixes.
    //
    // Parameters:
    //   returnType:
    //     The System.Type to return the current node's value as.
    //
    //   nsResolver:
    //     The System.Xml.IXmlNamespaceResolver object used to resolve namespace prefixes.
    //
    // Returns:
    //     The value of the current node as the System.Type requested.
    //
    // Exceptions:
    //   System.FormatException:
    //     The current node's value is not in the correct format for the target type.
    //
    //   System.InvalidCastException:
    //     The attempted cast is not valid.
    //public override object ValueAs(Type returnType, IXmlNamespaceResolver nsResolver);
    //
    // Summary:
    //     Streams the current node and its child nodes to the System.Xml.XmlWriter
    //     object specified.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter object to stream to.
    //public virtual void WriteSubtree(XmlWriter writer);
  }

  [ContractClassFor(typeof(XPathNavigator))]
  internal abstract class XPathNavigatorContract : XPathNavigator {
    public override XPathNavigator Clone() {
      Contract.Ensures(Contract.Result<XPathNavigator>() != null);
      return default(XPathNavigator);
    }

    public override XmlNameTable NameTable {
      get {
        Contract.Ensures(Contract.Result<XmlNameTable>() != null);
        return default(XmlNameTable);
      }
    }
  }
}

#endif
