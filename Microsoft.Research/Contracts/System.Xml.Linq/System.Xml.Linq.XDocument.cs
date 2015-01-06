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
using System.IO;
using System.Xml;
using System.Diagnostics.Contracts;

namespace System.Xml.Linq
{

  // Summary:
  //     Specifies load options when parsing XML.
  [Flags]
  public enum LoadOptions
  {
    // Summary:
    //     Does not preserve insignificant white space or load base URI and line information.
    None = 0,
    //
    // Summary:
    //     Preserves insignificant white space while parsing.
    PreserveWhitespace = 1,
    //
    // Summary:
    //     Requests the base URI information from the System.Xml.XmlReader, and makes
    //     it available via the System.Xml.Linq.XObject.BaseUri property.
    SetBaseUri = 2,
    //
    // Summary:
    //     Requests the line information from the System.Xml.XmlReader and makes it
    //     available via properties on System.Xml.Linq.XObject.
    SetLineInfo = 4,
  }

  // Summary:
  //     Represents an XML document.
  public class XDocument : XContainer
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XDocument class.
    extern public XDocument();
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XDocument class with the
    //     specified content.
    //
    // Parameters:
    //   content:
    //     A parameter list of content objects to add to this document.
    extern public XDocument(params object[] content);
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XDocument class from an
    //     existing System.Xml.Linq.XDocument object.
    //
    // Parameters:
    //   other:
    //     The System.Xml.Linq.XDocument object that will be copied.
    public XDocument(XDocument other)
    {
      Contract.Requires(other != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XDocument class with the
    //     specified System.Xml.Linq.XDeclaration and content.
    //
    // Parameters:
    //   declaration:
    //     An System.Xml.Linq.XDeclaration for the document.
    //
    //   content:
    //     The content of the document.
    extern public XDocument(XDeclaration declaration, params object[] content);

    // Summary:
    //     Gets or sets the XML declaration for this document.
    //
    // Returns:
    //     An System.Xml.Linq.XDeclaration that contains the XML declaration for this
    //     document.
    extern public XDeclaration Declaration { get; set; }
    //
    // Summary:
    //     Gets the Document Type Definition (DTD) for this document.
    //
    // Returns:
    //     A System.Xml.Linq.XDocumentType that contains the DTD for this document.
    extern public XDocumentType DocumentType { get; }
    //
    // Summary:
    //     Gets the root element of the XML Tree for this document.
    //
    // Returns:
    //     The root System.Xml.Linq.XElement of the XML tree.
    extern public XElement Root { get; }

    // Summary:
    //     Creates a new System.Xml.Linq.XDocument from a file.
    //
    // Parameters:
    //   uri:
    //     A URI string that references the file to load into a new System.Xml.Linq.XDocument.
    //
    // Returns:
    //     An System.Xml.Linq.XDocument that contains the contents of the specified
    //     file.
    [Pure]
    public static XDocument Load(string uri)
    {
      Contract.Requires(!String.IsNullOrEmpty(uri));
      Contract.Ensures(Contract.Result<XDocument>() != null);
      return default(XDocument);
    }
    //
    // Summary:
    //     Creates a new System.Xml.Linq.XDocument from a System.IO.TextReader.
    //
    // Parameters:
    //   textReader:
    //     A System.IO.TextReader that contains the content for the System.Xml.Linq.XDocument.
    //
    // Returns:
    //     An System.Xml.Linq.XDocument that contains the contents of the specified
    //     System.IO.TextReader.
    public static XDocument Load(TextReader textReader)
    {
      Contract.Requires(textReader != null);
      Contract.Ensures(Contract.Result<XDocument>() != null);
      return default(XDocument);
    }
    //
    // Summary:
    //     Creates a new System.Xml.Linq.XDocument from an System.Xml.XmlReader.
    //
    // Parameters:
    //   reader:
    //     A System.Xml.XmlReader that contains the content for the System.Xml.Linq.XDocument.
    //
    // Returns:
    //     An System.Xml.Linq.XDocument that contains the contents of the specified
    //     System.Xml.XmlReader.
    public static XDocument Load(XmlReader reader)
    {
      Contract.Requires(reader != null);
      Contract.Ensures(Contract.Result<XDocument>() != null);
      return default(XDocument);
    }
    //
    // Summary:
    //     Creates a new System.Xml.Linq.XDocument from a file, optionally preserving
    //     white space, setting the base URI, and retaining line information.
    //
    // Parameters:
    //   uri:
    //     A URI string that references the file to load into a new System.Xml.Linq.XDocument.
    //
    //   options:
    //     A System.Xml.Linq.LoadOptions that specifies white space behavior, and whether
    //     to load base URI and line information.
    //
    // Returns:
    //     An System.Xml.Linq.XDocument that contains the contents of the specified
    //     file.
    [Pure]
    public static XDocument Load(string uri, LoadOptions options)
    {
      Contract.Requires(!String.IsNullOrEmpty(uri));
      Contract.Ensures(Contract.Result<XDocument>() != null);
      return default(XDocument);
    }
    //
    // Summary:
    //     Creates a new System.Xml.Linq.XDocument from a System.IO.TextReader, optionally
    //     preserving white space, setting the base URI, and retaining line information.
    //
    // Parameters:
    //   textReader:
    //     A System.IO.TextReader that contains the content for the System.Xml.Linq.XDocument.
    //
    //   options:
    //     A System.Xml.Linq.LoadOptions that specifies white space behavior, and whether
    //     to load base URI and line information.
    //
    // Returns:
    //     An System.Xml.Linq.XDocument that contains the XML that was read from the
    //     specified System.IO.TextReader.
    public static XDocument Load(TextReader textReader, LoadOptions options)
    {
      Contract.Requires(textReader != null);
      Contract.Ensures(Contract.Result<XDocument>() != null);
      return default(XDocument);
    }
    //
    // Summary:
    //     Loads an System.Xml.Linq.XElement from an System.Xml.XmlReader, optionally
    //     setting the base URI, and retaining line information.
    //
    // Parameters:
    //   reader:
    //     A System.Xml.XmlReader that will be read for the content of the System.Xml.Linq.XDocument.
    //
    //   options:
    //     A System.Xml.Linq.LoadOptions that specifies whether to load base URI and
    //     line information.
    //
    // Returns:
    //     An System.Xml.Linq.XDocument that contains the XML that was read from the
    //     specified System.Xml.XmlReader.
    public static XDocument Load(XmlReader reader, LoadOptions options)
    {
      Contract.Requires(reader != null);
      Contract.Ensures(Contract.Result<XDocument>() != null);
      return default(XDocument);
    }
    //
    // Summary:
    //     Creates a new System.Xml.Linq.XDocument from a string.
    //
    // Parameters:
    //   text:
    //     A string that contains XML.
    //
    // Returns:
    //     An System.Xml.Linq.XDocument populated from the string that contains XML.
    [Pure]
    public static XDocument Parse(string text)
    {
      Contract.Requires(text != null);
      Contract.Ensures(Contract.Result<XDocument>() != null);
      return default(XDocument);
    }
    //
    // Summary:
    //     Creates a new System.Xml.Linq.XDocument from a string, optionally preserving
    //     white space, setting the base URI, and retaining line information.
    //
    // Parameters:
    //   text:
    //     A string that contains XML.
    //
    //   options:
    //     A System.Xml.Linq.LoadOptions that specifies white space behavior, and whether
    //     to load base URI and line information.
    //
    // Returns:
    //     An System.Xml.Linq.XDocument populated from the string that contains XML.
    [Pure]
    public static XDocument Parse(string text, LoadOptions options)
    {
      Contract.Requires(text != null);
      Contract.Ensures(Contract.Result<XDocument>() != null);
      return default(XDocument);
    }
#if !SILVERLIGHT
    //
    // Summary:
    //     Serialize this System.Xml.Linq.XDocument to a file.
    //
    // Parameters:
    //   fileName:
    //     A string that contains the name of the file.
    public void Save(string fileName)
    {
      Contract.Requires(!String.IsNullOrEmpty(fileName));
    }
#endif
    //
    // Summary:
    //     Serialize this System.Xml.Linq.XDocument to a System.IO.TextWriter.
    //
    // Parameters:
    //   textWriter:
    //     A System.IO.TextWriter that the System.Xml.Linq.XDocument will be written
    //     to.
    public void Save(TextWriter textWriter)
    {
      Contract.Requires(textWriter != null);
    }
    //
    // Summary:
    //     Serialize this System.Xml.Linq.XDocument to an System.Xml.XmlWriter.
    //
    // Parameters:
    //   writer:
    //     A System.Xml.XmlWriter that the System.Xml.Linq.XDocument will be written
    //     to.
    public void Save(XmlWriter writer)
    {
      Contract.Requires(writer != null);
    }
#if !SILVERLIGHT
    //
    // Summary:
    //     Serialize this System.Xml.Linq.XDocument to a file, optionally disabling
    //     formatting.
    //
    // Parameters:
    //   fileName:
    //     A string that contains the name of the file.
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
    //     Serialize this System.Xml.Linq.XDocument to a System.IO.TextWriter, optionally
    //     disabling formatting.
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
  }
}
