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
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Diagnostics.Contracts;

namespace System.Data.SqlTypes
{
  // Summary:
  //     Represents XML data stored in or retrieved from a server.
  //[Serializable]
  //[XmlSchemaProvider("GetXsdType")]
  public sealed class SqlXml // :INullable, IXmlSerializable
  {
    // Summary:
    //     Creates a new System.Data.SqlTypes.SqlXml instance.
    //public SqlXml();
    //
    // Summary:
    //     Creates a new System.Data.SqlTypes.SqlXml instance, supplying the XML value
    //     from the supplied System.IO.Stream-derived instance.
    //
    // Parameters:
    //   value:
    //     A System.IO.Stream-derived instance (such as System.IO.FileStream) from which
    //     to load the System.Data.SqlTypes.SqlXml instance's Xml content.
    //public SqlXml(Stream value);
    //
    // Summary:
    //     Creates a new System.Data.SqlTypes.SqlXml instance and associates it with
    //     the content of the supplied System.Xml.XmlReader.
    //
    // Parameters:
    //   value:
    //     An System.Xml.XmlReader-derived class instance to be used as the value of
    //     the new System.Data.SqlTypes.SqlXml instance.
    //public SqlXml(XmlReader value);

    // Summary:
    //     Indicates whether this instance represents a null System.Data.SqlTypes.SqlXml
    //     value.
    //
    // Returns:
    //     true if Value is null. Otherwise, false.
    //public bool IsNull { get; }
    //
    // Summary:
    //     Represents a null instance of the System.Data.SqlTypes.SqlXml type.
    //
    // Returns:
    //     A null instance of the System.Data.SqlTypes.SqlXml type..
    //public static SqlXml Null { get; }
    //
    // Summary:
    //     Gets the string representation of the XML content of this System.Data.SqlTypes.SqlXml
    //     instance.
    //
    // Returns:
    //     The string representation of the XML content.
    //public string Value { get; }

    // Summary:
    //     Gets the value of the XML content of this System.Data.SqlTypes.SqlXml as
    //     a System.Xml.XmlReader.
    //
    // Returns:
    //     A System.Xml.XmlReader-derived instance that contains the XML content. The
    //     actual type may vary (for example, the return value might be System.Xml.XmlTextReader)
    //     depending on how the information is represented internally, on the server.
    //
    // Exceptions:
    //   System.Data.SqlTypes.SqlNullValueException:
    //     Attempt was made to access this property on a null instance of System.Data.SqlTypes.SqlXml.
    public XmlReader CreateReader()
    {
      Contract.Ensures(Contract.Result<XmlReader>() != null);

      return default(XmlReader);

    }
    //
    // Summary:
    //     Returns the XML Schema definition language (XSD) of the specified System.Xml.Schema.XmlSchemaSet.
    //
    // Parameters:
    //   schemaSet:
    //     An System.Xml.Schema.XmlSchemaSet.
    //
    // Returns:
    //     A string that indicates the XSD of the specified System.Xml.Schema.XmlSchemaSet.
    public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
    {
      Contract.Ensures(Contract.Result<XmlQualifiedName>() != null);

      return default(XmlQualifiedName);
    }
  }
}
