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
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics.Contracts;

namespace System.Xml.Linq
{
  // Summary:
  //     Represents an XML element.
  // [XmlSchemaProvider("", IsAny = true)]
  public class XElement : XContainer //, IXmlSerializable
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XElement class from another
    //     System.Xml.Linq.XElement object.
    //
    // Parameters:
    //   other:
    //     An System.Xml.Linq.XElement object to copy from.
    public XElement(XElement other)
    {
      Contract.Requires(other != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XElement class with the
    //     specified name.
    //
    // Parameters:
    //   name:
    //     An System.Xml.Linq.XName that contains the name of the element.
    public XElement(XName name)
    {
      Contract.Requires(name != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XElement class from an
    //     System.Xml.Linq.XStreamingElement object.
    //
    // Parameters:
    //   other:
    //     An System.Xml.Linq.XStreamingElement that contains unevaluated queries that
    //     will be iterated for the contents of this System.Xml.Linq.XElement.
    public XElement(XStreamingElement other)
    {
      Contract.Requires(other != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XElement class with the
    //     specified name and content.
    //
    // Parameters:
    //   name:
    //     An System.Xml.Linq.XName that contains the element name.
    //
    //   content:
    //     The contents of the element.
    public XElement(XName name, object content)
    {
      Contract.Requires(name != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XElement class with the
    //     specified name and content.
    //
    // Parameters:
    //   name:
    //     An System.Xml.Linq.XName that contains the element name.
    //
    //   content:
    //     The initial content of the element.
    public XElement(XName name, params object[] content)
    {
      Contract.Requires(name != null);
    }

    extern public static explicit operator int?(XElement element);

    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to a System.UInt32.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.UInt32.
    //
    // Returns:
    //     A System.UInt32 that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.UInt32 value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator uint(XElement element)
    {
      Contract.Requires(element != null);
      return default(uint);
    }

    extern public static explicit operator bool?(XElement element);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to a System.Boolean.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.Boolean.
    //
    // Returns:
    //     A System.Boolean that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.Boolean value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator bool(XElement element)
    {
      Contract.Requires(element != null);
      return default(bool);
    }
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to an System.Int32.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.Int32.
    //
    // Returns:
    //     A System.Int32 that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.Int32 value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator int(XElement element)
    {
      Contract.Requires(element != null);
      return default(int);
    }
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to an System.Int64.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.Int64.
    //
    // Returns:
    //     A System.Int64 that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.Int64 value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator long(XElement element)
    {
      Contract.Requires(element != null);
      return default(long);
    }
    extern public static explicit operator long?(XElement element);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to a System.UInt64.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.UInt64.
    //
    // Returns:
    //     A System.UInt64 that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.UInt64 value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator ulong(XElement element)
    {
      Contract.Requires(element != null);
      return default(ulong);
    }
    extern public static explicit operator uint?(XElement element);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to a System.String.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.String.
    //
    // Returns:
    //     A System.String that contains the content of this System.Xml.Linq.XElement.
    extern public static explicit operator string(XElement element);
    extern public static explicit operator Guid?(XElement element);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to a System.Guid.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.Guid.
    //
    // Returns:
    //     A System.Guid that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.Guid value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator Guid(XElement element)
    {
      Contract.Requires(element != null);
      return default(Guid);
    }
    extern public static explicit operator ulong?(XElement element);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to a System.Single.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.Single.
    //
    // Returns:
    //     A System.Single that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.Single value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator float(XElement element)
    {
      Contract.Requires(element != null);
      return default(float);
    }
    extern public static explicit operator float?(XElement element);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to a System.Double.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.Double.
    //
    // Returns:
    //     A System.Double that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.Double value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator double(XElement element)
    {
      Contract.Requires(element != null);
      return default(double);
    }
    extern public static explicit operator double?(XElement element);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to a System.Decimal.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.Decimal.
    //
    // Returns:
    //     A System.Decimal that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.Decimal value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator decimal(XElement element)
    {
      Contract.Requires(element != null);
      return default(decimal);
    }
    extern public static explicit operator TimeSpan?(XElement element);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to a System.TimeSpan.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.TimeSpan.
    //
    // Returns:
    //     A System.TimeSpan that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.TimeSpan value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator TimeSpan(XElement element)
    {
      Contract.Requires(element != null);
      return default(TimeSpan);
    }
    extern public static explicit operator decimal?(XElement element);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XElement to a System.DateTime.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.DateTime.
    //
    // Returns:
    //     A System.DateTime that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.DateTime value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator DateTime(XElement element)
    {
      Contract.Requires(element != null);
      return default(DateTime);
    }
    extern public static explicit operator DateTime?(XElement element);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.DateTimeOffset.
    //
    // Parameters:
    //   element:
    //     The System.Xml.Linq.XElement to cast to System.DateTimeOffset.
    //
    // Returns:
    //     A System.DateTimeOffset that contains the content of this System.Xml.Linq.XElement.
    //
    // Exceptions:
    //   System.FormatException:
    //     The element does not contain a valid System.DateTimeOffset value.
    //
    //   System.ArgumentNullException:
    //     The element parameter is null.
    public static explicit operator DateTimeOffset(XElement element)
    {
      Contract.Requires(element != null);
      return default(DateTimeOffset);
    }
    extern public static explicit operator DateTimeOffset?(XElement element);

    // Summary:
    //     Gets an empty collection of elements.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains an empty collection.
    public static IEnumerable<XElement> EmptySequence
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
        return default(IEnumerable<XElement>);
      }
    }
    //
    // Summary:
    //     Gets the first attribute of this element.
    //
    // Returns:
    //     An System.Xml.Linq.XAttribute that contains the first attribute of this element.
    extern public XAttribute FirstAttribute { get; }
    //
    // Summary:
    //     Gets a value indicating whether this element as at least one attribute.
    //
    // Returns:
    //     true if this element has at least one attribute; otherwise false.
    extern public bool HasAttributes { get; }
    //
    // Summary:
    //     Gets a value indicating whether this element has at least one child element.
    //
    // Returns:
    //     true if this element has at least one child element; otherwise false.
    extern public bool HasElements { get; }
    //
    // Summary:
    //     Gets a value indicating whether this element contains no content.
    //
    // Returns:
    //     true if this element contains no content; otherwise false.
    extern public bool IsEmpty { get; }
    //
    // Summary:
    //     Gets the last attribute of this element.
    //
    // Returns:
    //     An System.Xml.Linq.XAttribute that contains the last attribute of this element.
    extern public XAttribute LastAttribute { get; }
    //
    // Summary:
    //     Gets the name of this element.
    //
    // Returns:
    //     An System.Xml.Linq.XName that contains the name of this element.
    public XName Name
    {
      get
      {
        Contract.Ensures(Contract.Result<XName>() != null);
        return default(XName);
      }

      set
      {
        Contract.Requires(value != null);
      }
    }
    //
    // Summary:
    //     Gets the concatenated text contents of this element.
    //
    // Returns:
    //     A System.String that contains all of the text content of this element. If
    //     there are multiple text nodes, they will be concatenated.
    public string Value
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    // Summary:
    //     Returns a collection of elements that contain this element, and the ancestors
    //     of this element.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     of elements that contain this element, and the ancestors of this element.
    [Pure]
    public IEnumerable<XElement> AncestorsAndSelf()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Returns a filtered collection of elements that contain this element, and
    //     the ancestors of this element. Only elements that have a matching System.Xml.Linq.XName
    //     are included in the collection.
    //
    // Parameters:
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contain this element, and the ancestors of this element. Only elements
    //     that have a matching System.Xml.Linq.XName are included in the collection.
    [Pure]
    public IEnumerable<XElement> AncestorsAndSelf(XName name)
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Returns the System.Xml.Linq.XAttribute of this System.Xml.Linq.XElement that
    //     has the specified System.Xml.Linq.XName.
    //
    // Parameters:
    //   name:
    //     The System.Xml.Linq.XName of the System.Xml.Linq.XAttribute to get.
    //
    // Returns:
    //     An System.Xml.Linq.XAttribute that has the specified System.Xml.Linq.XName;
    //     null if there is no attribute with the specified name.
    [Pure]
    extern public XAttribute Attribute(XName name);
    //
    // Summary:
    //     Returns a collection of attributes of this element.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XAttribute
    //     of attributes of this element.
    [Pure]
    public IEnumerable<XAttribute> Attributes()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XAttribute>>() != null);
      return default(IEnumerable<XAttribute>);
    }
    //
    // Summary:
    //     Returns a filtered collection of attributes of this element. Only elements
    //     that have a matching System.Xml.Linq.XName are included in the collection.
    //
    // Parameters:
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XAttribute
    //     that contains the attributes of this element. Only elements that have a matching
    //     System.Xml.Linq.XName are included in the collection.
    [Pure]
    public IEnumerable<XAttribute> Attributes(XName name)
    {
      Contract.Ensures(Contract.Result<IEnumerable<XAttribute>>() != null);
      return default(IEnumerable<XAttribute>);
    }
    //
    // Summary:
    //     Returns a collection of nodes that contain this element, and all descendant
    //     nodes of this element, in document order.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode that
    //     contain this element, and all descendant nodes of this element, in document
    //     order.
    [Pure]
    public IEnumerable<XNode> DescendantNodesAndSelf()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XNode>>() != null);
      return default(IEnumerable<XNode>);
    }
    //
    // Summary:
    //     Returns a collection of elements that contain this element, and all descendant
    //     elements of this element, in document order.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     of elements that contain this element, and all descendant elements of this
    //     element, in document order.
    [Pure]
    public IEnumerable<XElement> DescendantsAndSelf()
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Returns a filtered collection of elements that contain this element, and
    //     all descendant elements of this element, in document order. Only elements
    //     that have a matching System.Xml.Linq.XName are included in the collection.
    //
    // Parameters:
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contain this element, and all descendant elements of this element, in
    //     document order. Only elements that have a matching System.Xml.Linq.XName
    //     are included in the collection.
    [Pure]
    public IEnumerable<XElement> DescendantsAndSelf(XName name)
    {
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Gets the default System.Xml.Linq.XNamespace of this System.Xml.Linq.XElement.
    //
    // Returns:
    //     An System.Xml.Linq.XNamespace that contains the default namespace of this
    //     System.Xml.Linq.XElement.
    [Pure]
    public XNamespace GetDefaultNamespace()
    {
      Contract.Ensures(Contract.Result<XNamespace>() != null);
      return default(XNamespace);
    }
    //
    // Summary:
    //     Gets the namespace associated with a particular prefix for this System.Xml.Linq.XElement.
    //
    // Parameters:
    //   prefix:
    //     A string that contains the namespace prefix to look up.
    //
    // Returns:
    //     An System.Xml.Linq.XNamespace for the namespace associated with the prefix
    //     for this System.Xml.Linq.XElement.
    [Pure]
    public XNamespace GetNamespaceOfPrefix(string prefix)
    {
      Contract.Requires(!String.IsNullOrEmpty(prefix));
      return default(XNamespace);
    }
    //
    // Summary:
    //     Gets the prefix associated with a namespace for this System.Xml.Linq.XElement.
    //
    // Parameters:
    //   ns:
    //     An System.Xml.Linq.XNamespace to look up.
    //
    // Returns:
    //     A System.String that contains the namespace prefix.
    [Pure]
    public string GetPrefixOfNamespace(XNamespace ns)
    {
      Contract.Requires(ns != null);
      return default(string);
    }
    //
    // Summary:
    //     Loads an System.Xml.Linq.XElement from a file.
    //
    // Parameters:
    //   uri:
    //     A URI string referencing the file to load into a new System.Xml.Linq.XElement.
    //
    // Returns:
    //     An System.Xml.Linq.XElement that contains the contents of the specified file.
    public static XElement Load(string uri)
    {
      Contract.Requires(uri != null);
      Contract.Ensures(Contract.Result<XElement>() != null);
      return default(XElement);
    }
    //
    // Summary:
    //     Loads an System.Xml.Linq.XElement from a System.IO.TextReader.
    //
    // Parameters:
    //   textReader:
    //     A System.IO.TextReader that will be read for the System.Xml.Linq.XElement
    //     content.
    //
    // Returns:
    //     An System.Xml.Linq.XElement that contains the XML that was read from the
    //     specified System.IO.TextReader.
    public static XElement Load(TextReader textReader)
    {
      Contract.Requires(textReader != null);
      Contract.Ensures(Contract.Result<XElement>() != null);
      return default(XElement);
    }
    //
    // Summary:
    //     Loads an System.Xml.Linq.XElement from an System.Xml.XmlReader.
    //
    // Parameters:
    //   reader:
    //     A System.Xml.XmlReader that will be read for the content of the System.Xml.Linq.XElement.
    //
    // Returns:
    //     An System.Xml.Linq.XElement that contains the XML that was read from the
    //     specified System.Xml.XmlReader.
    public static XElement Load(XmlReader reader)
    {
      Contract.Requires(reader != null);
      Contract.Ensures(Contract.Result<XElement>() != null);
      return default(XElement);
    }
    //
    // Summary:
    //     Loads an System.Xml.Linq.XElement from a file, optionally preserving white
    //     space, setting the base URI, and retaining line information.
    //
    // Parameters:
    //   uri:
    //     A URI string referencing the file to load into an System.Xml.Linq.XElement.
    //
    //   options:
    //     A System.Xml.Linq.LoadOptions that specifies white space behavior, and whether
    //     to load base URI and line information.
    //
    // Returns:
    //     An System.Xml.Linq.XElement that contains the contents of the specified file.
    public static XElement Load(string uri, LoadOptions options)
    {
      Contract.Requires(uri != null);
      Contract.Ensures(Contract.Result<XElement>() != null);
      return default(XElement);
    }
    //
    // Summary:
    //     Loads an System.Xml.Linq.XElement from a System.IO.TextReader, optionally
    //     preserving white space and retaining line information.
    //
    // Parameters:
    //   textReader:
    //     A System.IO.TextReader that will be read for the System.Xml.Linq.XElement
    //     content.
    //
    //   options:
    //     A System.Xml.Linq.LoadOptions that specifies white space behavior, and whether
    //     to load base URI and line information.
    //
    // Returns:
    //     An System.Xml.Linq.XElement that contains the XML that was read from the
    //     specified System.IO.TextReader.
    public static XElement Load(TextReader textReader, LoadOptions options)
    {
      Contract.Requires(textReader != null);
      Contract.Ensures(Contract.Result<XElement>() != null);
      return default(XElement);
    }
    //
    // Summary:
    //     Loads an System.Xml.Linq.XElement from an System.Xml.XmlReader, optionally
    //     preserving white space, setting the base URI, and retaining line information.
    //
    // Parameters:
    //   reader:
    //     A System.Xml.XmlReader that will be read for the content of the System.Xml.Linq.XElement.
    //
    //   options:
    //     A System.Xml.Linq.LoadOptions that specifies white space behavior, and whether
    //     to load base URI and line information.
    //
    // Returns:
    //     An System.Xml.Linq.XElement that contains the XML that was read from the
    //     specified System.Xml.XmlReader.
    public static XElement Load(XmlReader reader, LoadOptions options)
    {
      Contract.Requires(reader != null);
      Contract.Ensures(Contract.Result<XElement>() != null);
      return default(XElement);
    }
    //
    // Summary:
    //     Load an System.Xml.Linq.XElement from a string that contains XML.
    //
    // Parameters:
    //   text:
    //     A System.String that contains XML.
    //
    // Returns:
    //     An System.Xml.Linq.XElement populated from the string that contains XML.
    [Pure]
    public static XElement Parse(string text)
    {
      Contract.Requires(text != null);
      Contract.Ensures(Contract.Result<XElement>() != null);
      return default(XElement);
    }
    //
    // Summary:
    //     Load an System.Xml.Linq.XElement from a string that contains XML, optionally
    //     preserving white space and retaining line information.
    //
    // Parameters:
    //   text:
    //     A System.String that contains XML.
    //
    //   options:
    //     A System.Xml.Linq.LoadOptions that specifies white space behavior, and whether
    //     to load base URI and line information.
    //
    // Returns:
    //     An System.Xml.Linq.XElement populated from the string that contains XML.
    [Pure]
    public static XElement Parse(string text, LoadOptions options)
    {
      Contract.Requires(text != null);
      Contract.Ensures(Contract.Result<XElement>() != null);
      return default(XElement);
    }
    //
    // Summary:
    //     Removes nodes and attributes from this System.Xml.Linq.XElement.
    extern public void RemoveAll();
    //
    // Summary:
    //     Removes the attributes of this System.Xml.Linq.XElement.
    extern public void RemoveAttributes();
    //
    // Summary:
    //     Replaces the child nodes and the attributes of this element with the specified
    //     content.
    //
    // Parameters:
    //   content:
    //     The content that will replace the child nodes and attributes of this element.
    extern public void ReplaceAll(object content);
    //
    // Summary:
    //     Replaces the child nodes and the attributes of this element with the specified
    //     content.
    //
    // Parameters:
    //   content:
    //     A parameter list of content objects.
    extern public void ReplaceAll(params object[] content);
    //
    // Summary:
    //     Replaces the attributes of this element with the specified content.
    //
    // Parameters:
    //   content:
    //     The content that will replace the attributes of this element.
    extern public void ReplaceAttributes(object content);
    //
    // Summary:
    //     Replaces the attributes of this element with the specified content.
    //
    // Parameters:
    //   content:
    //     A parameter list of content objects.
    extern public void ReplaceAttributes(params object[] content);
#if !SILVERLIGHT
    //
    // Summary:
    //     Serialize this element to a file.
    //
    // Parameters:
    //   fileName:
    //     A System.String that contains the name of the file.
    public void Save(string fileName)
    {
      Contract.Requires(!String.IsNullOrEmpty(fileName));
    }
#endif
    //
    // Summary:
    //     Serialize this element to a System.IO.TextWriter.
    //
    // Parameters:
    //   textWriter:
    //     A System.IO.TextWriter that the System.Xml.Linq.XElement will be written
    //     to.
    public void Save(TextWriter textWriter)
    {
      Contract.Requires(textWriter != null);
    }
    //
    // Summary:
    //     Serialize this element to an System.Xml.XmlWriter.
    //
    // Parameters:
    //   writer:
    //     A System.Xml.XmlWriter that the System.Xml.Linq.XElement will be written
    //     to.
    public void Save(XmlWriter writer)
    {
      Contract.Requires(writer != null);
    }
#if !SILVERLIGHT
    //
    // Summary:
    //     Serialize this element to a file, optionally disabling formatting.
    //
    // Parameters:
    //   fileName:
    //     A System.String that contains the name of the file.
    //
    //   options:
    //     A System.Xml.Linq.SaveOptions that specifies formatting behavior.
    public void Save(string fileName, SaveOptions options)
    {
      Contract.Requires(!String.IsNullOrEmpty(fileName));
    }
#endif
    //
    // Summary:
    //     Serialize this element to a System.IO.TextWriter, optionally disabling formatting.
    //
    // Parameters:
    //   textWriter:
    //     The System.IO.TextWriter to output the XML to.
    //
    //   options:
    //     A System.Xml.Linq.SaveOptions that specifies formatting behavior.
    public void Save(TextWriter textWriter, SaveOptions options)
    {
      Contract.Requires(textWriter != null);
    }
    //
    // Summary:
    //     Sets the value of an attribute, adds an attribute, or removes an attribute.
    //
    // Parameters:
    //   name:
    //     An System.Xml.Linq.XName that contains the name of the attribute to change.
    //
    //   value:
    //     The value to assign to the attribute. The attribute is removed if the value
    //     is null. Otherwise, the value is converted to its string representation and
    //     assigned to the System.Xml.Linq.XAttribute.Value property of the attribute.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value is an instance of System.Xml.Linq.XObject.
    extern public void SetAttributeValue(XName name, object value);
    //
    // Summary:
    //     Sets the value of a child element, adds a child element, or removes a child
    //     element.
    //
    // Parameters:
    //   name:
    //     An System.Xml.Linq.XName that contains the name of the child element to change.
    //
    //   value:
    //     The value to assign to the child element. The child element is removed if
    //     the value is null. Otherwise, the value is converted to its string representation
    //     and assigned to the System.Xml.Linq.XElement.Value property of the child
    //     element.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value is an instance of System.Xml.Linq.XObject.
    extern public void SetElementValue(XName name, object value);
    //
    // Summary:
    //     Sets the value of this element.
    //
    // Parameters:
    //   value:
    //     The value to assign to this element. The value is converted to its string
    //     representation and assigned to the System.Xml.Linq.XElement.Value property.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value is null.
    //
    //   System.ArgumentException:
    //     The value is an System.Xml.Linq.XObject.
    public void SetValue(object value)
    {
      Contract.Requires(value != null);
    }
  }
}
