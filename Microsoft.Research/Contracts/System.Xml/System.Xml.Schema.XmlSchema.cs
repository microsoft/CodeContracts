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
using System.Diagnostics.Contracts;


namespace System.Xml.Schema
{
  // Summary:
  //     An in-memory representation of an XML Schema as specified in the World Wide
  //     Web Consortium (W3C) XML Schema Part 1: Structures and XML Schema Part 2:
  //     Datatypes specifications.
  //// [XmlRoot("schema", Namespace = "http://www.w3.org/2001/XMLSchema")]
  public class XmlSchema
  {
#if SILVERLIGHT
    private
#else
    public 
#endif
    XmlSchema() { }

#if !SILVERLIGHT
    // Summary:
    //     The XML schema instance namespace. This field is constant.
    public const string InstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";

    //
    // Summary:
    //     The XML schema namespace. This field is constant.
    public const string Namespace = "http://www.w3.org/2001/XMLSchema";

#endif

    // Summary:
    //     Initializes a new instance of the System.Xml.Schema.XmlSchema class.
    //public XmlSchema();

    // Summary:
    //     Gets or sets the form for attributes declared in the target namespace of
    //     the schema.
    //
    // Returns:
    //     The System.Xml.Schema.XmlSchemaForm value that indicates if attributes from
    //     the target namespace are required to be qualified with the namespace prefix.
    //     The default is System.Xml.Schema.XmlSchemaForm.None.
    //// [XmlAttribute("attributeFormDefault")]
    //public XmlSchemaForm AttributeFormDefault { get; set; }
    //
    // Summary:
    //     Gets the post-schema-compilation value of all the global attribute groups
    //     in the schema.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaObjectTable collection of all the global attribute
    //     groups in the schema.
    // [XmlIgnore]
#if !SILVERLIGHT
    public XmlSchemaObjectTable AttributeGroups
    {
      get
      {
        Contract.Ensures(Contract.Result<XmlSchemaObjectTable>() != null);

        return default(XmlSchemaObjectTable);
      }
    }
#endif
    //
    // Summary:
    //     Gets the post-schema-compilation value for all the attributes in the schema.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaObjectTable collection of all the attributes
    //     in the schema.
    // [XmlIgnore]
#if !SILVERLIGHT
    public XmlSchemaObjectTable Attributes
    {
      get
      {
        Contract.Ensures(Contract.Result<XmlSchemaObjectTable>() != null);

        return default(XmlSchemaObjectTable);
      }
    }
#endif
    //
    // Summary:
    //     Gets or sets the blockDefault attribute which sets the default value of the
    //     block attribute on element and complex types in the targetNamespace of the
    //     schema.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaDerivationMethod value representing the different
    //     methods for preventing derivation. The default value is XmlSchemaDerivationMethod.None.
    // [XmlAttribute("blockDefault")]
    //public XmlSchemaDerivationMethod BlockDefault { get; set; }
    //
    // Summary:
    //     Gets or sets the form for elements declared in the target namespace of the
    //     schema.
    //
    // Returns:
    //     The System.Xml.Schema.XmlSchemaForm value that indicates if elements from
    //     the target namespace are required to be qualified with the namespace prefix.
    //     The default is System.Xml.Schema.XmlSchemaForm.None.
    // [XmlAttribute("elementFormDefault")]
    //public XmlSchemaForm ElementFormDefault { get; set; }
    //
    // Summary:
    //     Gets the post-schema-compilation value for all the elements in the schema.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaObjectTable collection of all the elements
    //     in the schema.
    // [XmlIgnore]
#if !SILVERLIGHT
    public XmlSchemaObjectTable Elements
    {
      get
      {
        Contract.Ensures(Contract.Result<XmlSchemaObjectTable>() != null);

        return default(XmlSchemaObjectTable);
      }
    }
#endif
    //
    // Summary:
    //     Gets or sets the finalDefault attribute which sets the default value of the
    //     final attribute on elements and complex types in the target namespace of
    //     the schema.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaDerivationMethod value representing the different
    //     methods for preventing derivation. The default value is XmlSchemaDerivationMethod.None.
    // [XmlAttribute("finalDefault")]
    //public XmlSchemaDerivationMethod FinalDefault { get; set; }
    //
    // Summary:
    //     Gets the post-schema-compilation value of all the groups in the schema.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaObjectTable collection of all the groups in
    //     the schema.
    // [XmlIgnore]
    //public XmlSchemaObjectTable Groups { get; }
    //
    // Summary:
    //     Gets or sets the string ID.
    //
    // Returns:
    //     The ID of the string. The default value is String.Empty.
    // [XmlAttribute("id", DataType = "ID")]
    //public string Id { get; set; }
    //
    // Summary:
    //     Gets the collection of included and imported schemas.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaObjectCollection of the included and imported
    //     schemas.
    // [XmlElement("redefine", typeof(XmlSchemaRedefine))]
    // [XmlElement("include", typeof(XmlSchemaInclude))]
    // [XmlElement("import", typeof(XmlSchemaImport))]
    //public XmlSchemaObjectCollection Includes { get; }
    //
    // Summary:
    //     Indicates if the schema has been compiled.
    //
    // Returns:
    //     true if schema has been compiled, otherwise, false. The default value is
    //     false.
    // [XmlIgnore]
    //public bool IsCompiled { get; }
    //
    // Summary:
    //     Gets the collection of schema elements in the schema and is used to add new
    //     element types at the schema element level.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaObjectCollection of schema elements in the
    //     schema.
    // [XmlElement("notation", typeof(XmlSchemaNotation))]
    // [XmlElement("group", typeof(XmlSchemaGroup))]
    // [XmlElement("annotation", typeof(XmlSchemaAnnotation))]
    // [XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroup))]
    // [XmlElement("attribute", typeof(XmlSchemaAttribute))]
    // [XmlElement("complexType", typeof(XmlSchemaComplexType))]
    // [XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
    // [XmlElement("element", typeof(XmlSchemaElement))]
#if !SILVERLIGHT
    public XmlSchemaObjectCollection Items
    {
      get
      {
        Contract.Ensures(Contract.Result<XmlSchemaObjectCollection>() != null);

        return default(XmlSchemaObjectCollection);
      }
    }
#endif
    //
    // Summary:
    //     Gets the post-schema-compilation value for all notations in the schema.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaObjectTable collection of all notations in
    //     the schema.
    // [XmlIgnore]
    //public XmlSchemaObjectTable Notations { get; }
    //
    // Summary:
    //     Gets the post-schema-compilation value of all schema types in the schema.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaObjectCollection of all schema types in the
    //     schema.
    // [XmlIgnore]
#if !SILVERLIGHT
    public XmlSchemaObjectTable SchemaTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<XmlSchemaObjectTable>() != null);

        return default(XmlSchemaObjectTable);
      }
    }
#endif
    //
    // Summary:
    //     Gets or sets the Uniform Resource Identifier (URI) of the schema target namespace.
    //
    // Returns:
    //     The schema target namespace.
    // [XmlAttribute("targetNamespace", DataType = "anyURI")]
    //public string TargetNamespace { get; set; }
    //
    // Summary:
    //     Gets and sets the qualified attributes which do not belong to the schema
    //     target namespace.
    //
    // Returns:
    //     An array of qualified System.Xml.XmlAttribute objects that do not belong
    //     to the schema target namespace.
    // [XmlAnyAttribute]
    //public XmlAttribute [] UnhandledAttributes { get; set; }
    //
    // Summary:
    //     Gets or sets the version of the schema.
    //
    // Returns:
    //     The version of the schema. The default value is String.Empty.
    // [XmlAttribute("version", DataType = "token")]
    //public string Version { get; set; }

    // Summary:
    //     Compiles the XML Schema Object Model (SOM) into schema information for validation.
    //     Used to check the syntactic and semantic structure of the programmatically
    //     built SOM. Semantic validation checking is performed during compilation.
    //
    // Parameters:
    //   validationEventHandler:
    //     The validation event handler that receives information about XML Schema validation
    //     errors.
    // [Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. http://go.microsoft.com/fwlink/?linkid=14202")]
    //public void Compile(ValidationEventHandler validationEventHandler);
    //
    // Summary:
    //     Compiles the XML Schema Object Model (SOM) into schema information for validation.
    //     Used to check the syntactic and semantic structure of the programmatically
    //     built SOM. Semantic validation checking is performed during compilation.
    //
    // Parameters:
    //   validationEventHandler:
    //     The validation event handler that receives information about the XML Schema
    //     validation errors.
    //
    //   resolver:
    //     The XmlResolver used to resolve namespaces referenced in include and import
    //     elements.
    // [Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. http://go.microsoft.com/fwlink/?linkid=14202")]
    //public void Compile(ValidationEventHandler validationEventHandler, XmlResolver resolver);
    //
    // Summary:
    //     Reads an XML Schema from the supplied stream.
    //
    // Parameters:
    //   stream:
    //     The supplied data stream.
    //
    //   validationEventHandler:
    //     The validation event handler that receives information about XML Schema syntax
    //     errors.
    //
    // Returns:
    //     The System.Xml.Schema.XmlSchema object representing the XML Schema.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaException:
    //     An System.Xml.Schema.XmlSchemaException is raised if no System.Xml.Schema.ValidationEventHandler
    //     is specified.
    //public static XmlSchema Read(Stream stream, ValidationEventHandler validationEventHandler);
    //
    // Summary:
    //     Reads an XML Schema from the supplied System.IO.TextReader.
    //
    // Parameters:
    //   reader:
    //     The TextReader containing the XML Schema to read.
    //
    //   validationEventHandler:
    //     The validation event handler that receives information about the XML Schema
    //     syntax errors.
    //
    // Returns:
    //     The System.Xml.Schema.XmlSchema object representing the XML Schema.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaException:
    //     An System.Xml.Schema.XmlSchemaException is raised if no System.Xml.Schema.ValidationEventHandler
    //     is specified.
    //public static XmlSchema Read(TextReader reader, ValidationEventHandler validationEventHandler);
    //
    // Summary:
    //     Reads an XML Schema from the supplied System.Xml.XmlReader.
    //
    // Parameters:
    //   reader:
    //     The XmlReader containing the XML Schema to read.
    //
    //   validationEventHandler:
    //     The validation event handler that receives information about the XML Schema
    //     syntax errors.
    //
    // Returns:
    //     The System.Xml.Schema.XmlSchema object representing the XML Schema.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaException:
    //     An System.Xml.Schema.XmlSchemaException is raised if no System.Xml.Schema.ValidationEventHandler
    //     is specified.
    //public static XmlSchema Read(XmlReader reader, ValidationEventHandler validationEventHandler);
    //
    // Summary:
    //     Writes the XML Schema to the supplied data stream.
    //
    // Parameters:
    //   stream:
    //     The supplied data stream.
    //public void Write(Stream stream);
    //
    // Summary:
    //     Writes the XML Schema to the supplied System.IO.TextWriter.
    //
    // Parameters:
    //   writer:
    //     The System.IO.TextWriter to write to.
    //public void Write(TextWriter writer);
    //
    // Summary:
    //     Writes the XML Schema to the supplied System.Xml.XmlWriter.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter to write to.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The writer parameter is null.
#if !SILVERLIGHT
    public void Write(XmlWriter writer)
    {
      Contract.Requires(writer != null);

    }
#endif
    //
    // Summary:
    //     Writes the XML Schema to the supplied System.IO.Stream using the System.Xml.XmlNamespaceManager
    //     specified.
    //
    // Parameters:
    //   stream:
    //     The supplied data stream.
    //
    //   namespaceManager:
    //     The System.Xml.XmlNamespaceManager.
    //public void Write(Stream stream, XmlNamespaceManager namespaceManager);
    //
    // Summary:
    //     Writes the XML Schema to the supplied System.IO.TextWriter.
    //
    // Parameters:
    //   writer:
    //     The System.IO.TextWriter to write to.
    //
    //   namespaceManager:
    //     The System.Xml.XmlNamespaceManager.
    //public void Write(TextWriter writer, XmlNamespaceManager namespaceManager);
    //
    // Summary:
    //     Writes the XML Schema to the supplied System.Xml.XmlWriter.
    //
    // Parameters:
    //   writer:
    //     The System.Xml.XmlWriter to write to.
    //
    //   namespaceManager:
    //     The System.Xml.XmlNamespaceManager.
    //public void Write(XmlWriter writer, XmlNamespaceManager namespaceManager);
  }
}
