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

namespace System.Xml.Schema
{
  // Summary:
  //     Defines the post-schema-validation infoset of a validated XML node.
  public interface IXmlSchemaInfo
  {
    // Summary:
    //     Gets a value indicating if this validated XML node was set as the result
    //     of a default being applied during XML Schema Definition Language (XSD) schema
    //     validation.
    //
    // Returns:
    //     true if this validated XML node was set as the result of a default being
    //     applied during schema validation; otherwise, false.
    bool IsDefault { get; }
    //
    // Summary:
    //     Gets a value indicating if the value for this validated XML node is nil.
    //
    // Returns:
    //     true if the value for this validated XML node is nil; otherwise, false.
    bool IsNil { get; }
    //
    // Summary:
    //     Gets the dynamic schema type for this validated XML node.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaSimpleType.
    //XmlSchemaSimpleType MemberType { get; }
    //
    // Summary:
    //     Gets the compiled System.Xml.Schema.XmlSchemaAttribute that corresponds to
    //     this validated XML node.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaAttribute.
    //XmlSchemaAttribute SchemaAttribute { get; }
    //
    // Summary:
    //     Gets the compiled System.Xml.Schema.XmlSchemaElement that corresponds to
    //     this validated XML node.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaElement.
    //XmlSchemaElement SchemaElement { get; }
    //
    // Summary:
    //     Gets the static XML Schema Definition Language (XSD) schema type of this
    //     validated XML node.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaType.
    //XmlSchemaType SchemaType { get; }
    //
    // Summary:
    //     Gets the System.Xml.Schema.XmlSchemaValidity value of this validated XML
    //     node.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaValidity value.
    //XmlSchemaValidity Validity { get; }
  }
}

#endif