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

namespace System.Xml
{
  // Summary:
  //     Specifies the type of node.
  public enum XmlNodeType
  {
    // Summary:
    //     This is returned by the System.Xml.XmlReader if a Read method has not been
    //     called.
    None = 0,
    //
    // Summary:
    //     An element (for example, <item> ).
    Element = 1,
    //
    // Summary:
    //     An attribute (for example, id='123' ).
    Attribute = 2,
    //
    // Summary:
    //     The text content of a node.
    Text = 3,
    //
    // Summary:
    //     A CDATA section (for example, <![CDATA[my escaped text]]> ).
    CDATA = 4,
    //
    // Summary:
    //     A reference to an entity (for example, &num; ).
    EntityReference = 5,
    //
    // Summary:
    //     An entity declaration (for example, <!ENTITY...> ).
    Entity = 6,
    //
    // Summary:
    //     A processing instruction (for example, <?pi test?> ).
    ProcessingInstruction = 7,
    //
    // Summary:
    //     A comment (for example, <!-- my comment --> ).
    Comment = 8,
    //
    // Summary:
    //     A document object that, as the root of the document tree, provides access
    //     to the entire XML document.
    Document = 9,
    //
    // Summary:
    //     The document type declaration, indicated by the following tag (for example,
    //     <!DOCTYPE...> ).
    DocumentType = 10,
    //
    // Summary:
    //     A document fragment.
    DocumentFragment = 11,
    //
    // Summary:
    //     A notation in the document type declaration (for example, <!NOTATION...>
    //     ).
    Notation = 12,
    //
    // Summary:
    //     White space between markup.
    Whitespace = 13,
    //
    // Summary:
    //     White space between markup in a mixed content model or white space within
    //     the xml:space="preserve" scope.
    SignificantWhitespace = 14,
    //
    // Summary:
    //     An end element tag (for example, </item> ).
    EndElement = 15,
    //
    // Summary:
    //     Returned when XmlReader gets to the end of the entity replacement as a result
    //     of a call to System.Xml.XmlReader.ResolveEntity().
    EndEntity = 16,
    //
    // Summary:
    //     The XML declaration (for example, <?xml version='1.0'?> ).
    XmlDeclaration = 17,
  }
}
