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
using System.Collections;
using System.Xml;
using System.Diagnostics.Contracts;

namespace System.Xml.Schema
{
  // Summary:
  //     Contains a cache of XML Schema definition language (XSD) schemas.
  public class XmlSchemaSet
  {
#if SILVERLIGHT_4_0_WP
    internal XmlSchemaSet() {}
#elif SILVERLIGHT
    private XmlSchemaSet() {}
#endif

    // Summary:
    //     Initializes a new instance of the System.Xml.Schema.XmlSchemaSet class.
    //public XmlSchemaSet();
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Schema.XmlSchemaSet class with
    //     the specified System.Xml.XmlNameTable.
    //
    // Parameters:
    //   nameTable:
    //     The System.Xml.XmlNameTable object to use.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Xml.XmlNameTable object passed as a parameter is null.
#if !SILVERLIGHT
    public XmlSchemaSet(XmlNameTable nameTable)
    {
      Contract.Requires(nameTable != null);
    }
#endif
    // Summary:
    //     Gets or sets the System.Xml.Schema.XmlSchemaCompilationSettings for the System.Xml.Schema.XmlSchemaSet.
    //
    // Returns:
    //     The System.Xml.Schema.XmlSchemaCompilationSettings for the System.Xml.Schema.XmlSchemaSet.
    //     The default is an System.Xml.Schema.XmlSchemaCompilationSettings instance
    //     with the System.Xml.Schema.XmlSchemaCompilationSettings.EnableUpaCheck property
    //     set to true.
    //public XmlSchemaCompilationSettings CompilationSettings { get; set; }
    //
    // Summary:
    //     Gets the number of logical XML Schema definition language (XSD) schemas in
    //     the System.Xml.Schema.XmlSchemaSet.
    //
    // Returns:
    //     The number of logical schemas in the System.Xml.Schema.XmlSchemaSet.
#if !SILVERLIGHT
    public int Count 
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }
#endif
    //
    // Summary:
    //     Gets all the global attributes in all the XML Schema definition language
    //     (XSD) schemas in the System.Xml.Schema.XmlSchemaSet.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaObjectTable.
#if !SILVERLIGHT
    public XmlSchemaObjectTable GlobalAttributes 
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
    //     Gets all the global elements in all the XML Schema definition language (XSD)
    //     schemas in the System.Xml.Schema.XmlSchemaSet.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaObjectTable.
#if !SILVERLIGHT
    public XmlSchemaObjectTable GlobalElements
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
    //     Gets all of the global simple and complex types in all the XML Schema definition
    //     language (XSD) schemas in the System.Xml.Schema.XmlSchemaSet.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchemaObjectTable.
#if !SILVERLIGHT
    public XmlSchemaObjectTable GlobalTypes
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
    //     Indicates if the XML Schema definition language (XSD) schemas in the System.Xml.Schema.XmlSchemaSet
    //     have been compiled.
    //
    // Returns:
    //     Returns true if the schemas in the System.Xml.Schema.XmlSchemaSet have been
    //     compiled since the last time a schema was added or removed from the System.Xml.Schema.XmlSchemaSet;
    //     otherwise, false.
    //public bool IsCompiled { get; }
    //
    // Summary:
    //     Gets the default System.Xml.XmlNameTable used by the System.Xml.Schema.XmlSchemaSet
    //     when loading new XML Schema definition language (XSD) schemas.
    //
    // Returns:
    //     An System.Xml.XmlNameTable.
    //public XmlNameTable NameTable { get; }
    //
    // Summary:
    //     Sets the System.Xml.XmlResolver used to resolve namespaces or locations referenced
    //     in include and import elements of a schema.
    //
    // Returns:
    //     The System.Xml.XmlResolver used to resolve namespaces or locations referenced
    //     in include and import elements of a schema.
    //public XmlResolver XmlResolver { set; }

    // Summary:
    //     Sets an event handler for receiving information about XML Schema definition
    //     language (XSD) schema validation errors.
    // public event ValidationEventHandler ValidationEventHandler;

#if !SILVERLIGHT
    // Summary:
    //     Adds the given System.Xml.Schema.XmlSchema to the System.Xml.Schema.XmlSchemaSet.
    //
    // Parameters:
    //   schema:
    //     The System.Xml.Schema.XmlSchema object to add to the System.Xml.Schema.XmlSchemaSet.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchema object if the schema is valid. If the schema
    //     is not valid and a System.Xml.Schema.ValidationEventHandler is specified,
    //     then null is returned and the appropriate validation event is raised. Otherwise
    //     an System.Xml.Schema.XmlSchemaException is thrown.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaException:
    //     The schema is not valid.
    //
    //   System.ArgumentNullException:
    //     The System.Xml.Schema.XmlSchema object passed as a parameter is null.
    public XmlSchema Add(XmlSchema schema)
    {
      Contract.Requires(schema != null);

      return default(XmlSchema);
    }
    //
    // Summary:
    //     Adds all the XML Schema definition language (XSD) schemas in the given System.Xml.Schema.XmlSchemaSet
    //     to the System.Xml.Schema.XmlSchemaSet.
    //
    // Parameters:
    //   schemas:
    //     The System.Xml.Schema.XmlSchemaSet object.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaException:
    //     A schema in the System.Xml.Schema.XmlSchemaSet is not valid.
    //
    //   System.ArgumentNullException:
    //     The System.Xml.Schema.XmlSchemaSet object passed as a parameter is null.
    public void Add(XmlSchemaSet schemas)
    {
      Contract.Requires(schemas != null);
    }
    //
    // Summary:
    //     Adds the XML Schema definition language (XSD) schema at the URL specified
    //     to the System.Xml.Schema.XmlSchemaSet.
    //
    // Parameters:
    //   targetNamespace:
    //     The schema targetNamespace property, or null to use the targetNamespace specified
    //     in the schema.
    //
    //   schemaUri:
    //     The URL that specifies the schema to load.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchema object if the schema is valid. If the schema
    //     is not valid and a System.Xml.Schema.ValidationEventHandler is specified,
    //     then null is returned and the appropriate validation event is raised. Otherwise,
    //     an System.Xml.Schema.XmlSchemaException is thrown.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaException:
    //     The schema is not valid.
    //
    //   System.ArgumentNullException:
    //     The URL passed as a parameter is null or System.String.Empty.
    public XmlSchema Add(string targetNamespace, string schemaUri)
    {
      Contract.Requires(!String.IsNullOrEmpty(targetNamespace));

      return default(XmlSchema);
    }
    //
    // Summary:
    //     Adds the XML Schema definition language (XSD) schema contained in the System.Xml.XmlReader
    //     to the System.Xml.Schema.XmlSchemaSet.
    //
    // Parameters:
    //   targetNamespace:
    //     The schema targetNamespace property, or null to use the targetNamespace specified
    //     in the schema.
    //
    //   schemaDocument:
    //     The System.Xml.XmlReader object.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchema object if the schema is valid. If the schema
    //     is not valid and a System.Xml.Schema.ValidationEventHandler is specified,
    //     then null is returned and the appropriate validation event is raised. Otherwise,
    //     an System.Xml.Schema.XmlSchemaException is thrown.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaException:
    //     The schema is not valid.
    //
    //   System.ArgumentNullException:
    //     The System.Xml.XmlReader object passed as a parameter is null.
    public XmlSchema Add(string targetNamespace, XmlReader schemaDocument)
    {
      Contract.Requires(schemaDocument != null);

      return default(XmlSchema);
    }
#endif

    //
    // Summary:
    //     Compiles the XML Schema definition language (XSD) schemas added to the System.Xml.Schema.XmlSchemaSet
    //     into one logical schema.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaException:
    //     An error occurred when validating and compiling the schemas in the System.Xml.Schema.XmlSchemaSet.
    //public void Compile();
    //
    // Summary:
    //     Indicates whether an XML Schema definition language (XSD) schema with the
    //     specified target namespace URI is in the System.Xml.Schema.XmlSchemaSet.
    //
    // Parameters:
    //   targetNamespace:
    //     The schema targetNamespace property.
    //
    // Returns:
    //     Returns true if a schema with the specified target namespace URI is in the
    //     System.Xml.Schema.XmlSchemaSet; otherwise, false.
    //public bool Contains(string targetNamespace);
    //
    // Summary:
    //     Indicates whether the specified XML Schema definition language (XSD) System.Xml.Schema.XmlSchema
    //     object is in the System.Xml.Schema.XmlSchemaSet.
    //
    // Parameters:
    //   schema:
    //     The System.Xml.Schema.XmlSchema object.
    //
    // Returns:
    //     Returns true if the System.Xml.Schema.XmlSchema object is in the System.Xml.Schema.XmlSchemaSet;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Xml.Schema.XmlSchemaSet passed as a parameter is null.
#if !SILVERLIGHT
    public bool Contains(XmlSchema schema)
    {
      Contract.Requires(schema != null);

      return default(bool);
    }
#endif
    //
    // Summary:
    //     Copies all the System.Xml.Schema.XmlSchema objects from the System.Xml.Schema.XmlSchemaSet
    //     to the given array, starting at the given index.
    //
    // Parameters:
    //   schemas:
    //     The array to copy the objects to.
    //
    //   index:
    //     The index in the array where copying will begin.
#if !SILVERLIGHT
    public void CopyTo(XmlSchema[] schemas, int index)
    {
      Contract.Requires(schemas != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index < schemas.Length);
    }
#endif
    //
    // Summary:
    //     Removes the specified XML Schema definition language (XSD) schema from the
    //     System.Xml.Schema.XmlSchemaSet.
    //
    // Parameters:
    //   schema:
    //     The System.Xml.Schema.XmlSchema object to remove from the System.Xml.Schema.XmlSchemaSet.
    //
    // Returns:
    //     The System.Xml.Schema.XmlSchema object removed from the System.Xml.Schema.XmlSchemaSet
    //     or null if the schema was not found in the System.Xml.Schema.XmlSchemaSet.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaException:
    //     The schema is not a valid schema.
    //
    //   System.ArgumentNullException:
    //     The System.Xml.Schema.XmlSchema passed as a parameter is null.
#if !SILVERLIGHT
    public XmlSchema Remove(XmlSchema schema)
    {
      Contract.Requires(schema != null);

      return default(XmlSchema);
    }
#endif
    //
    // Summary:
    //     Removes the specified XML Schema definition language (XSD) schema and all
    //     the schemas it imports from the System.Xml.Schema.XmlSchemaSet.
    //
    // Parameters:
    //   schemaToRemove:
    //     The System.Xml.Schema.XmlSchema object to remove from the System.Xml.Schema.XmlSchemaSet.
    //
    // Returns:
    //     Returns true if the System.Xml.Schema.XmlSchema object and all its imports
    //     were successfully removed; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The System.Xml.Schema.XmlSchema passed as a parameter is null.
#if !SILVERLIGHT
    public bool RemoveRecursive(XmlSchema schemaToRemove)
    {
      Contract.Requires(schemaToRemove != null);

      return default(bool);
    }
#endif
    //
    // Summary:
    //     Reprocesses an XML Schema definition language (XSD) schema that already exists
    //     in the System.Xml.Schema.XmlSchemaSet.
    //
    // Parameters:
    //   schema:
    //     The schema to reprocess.
    //
    // Returns:
    //     An System.Xml.Schema.XmlSchema object if the schema is a valid schema. If
    //     the schema is not valid and a System.Xml.Schema.ValidationEventHandler is
    //     specified, null is returned and the appropriate validation event is raised.
    //     Otherwise, an System.Xml.Schema.XmlSchemaException is thrown.
    //
    // Exceptions:
    //   System.Xml.Schema.XmlSchemaException:
    //     The schema is not valid.
    //
    //   System.ArgumentNullException:
    //     The System.Xml.Schema.XmlSchema object passed as a parameter is null.
    //
    //   System.ArgumentException:
    //     The System.Xml.Schema.XmlSchema object passed as a parameter does not already
    //     exist in the System.Xml.Schema.XmlSchemaSet.
#if !SILVERLIGHT
    public XmlSchema Reprocess(XmlSchema schema)
    {
      Contract.Requires(schema != null);

      return default(XmlSchema);
    }
#endif
    //
    // Summary:
    //     Returns a collection of all the XML Schema definition language (XSD) schemas
    //     in the System.Xml.Schema.XmlSchemaSet.
    //
    // Returns:
    //     An System.Collections.ICollection object containing all the schemas that
    //     have been added to the System.Xml.Schema.XmlSchemaSet. If no schemas have
    //     been added to the System.Xml.Schema.XmlSchemaSet, an empty System.Collections.ICollection
    //     object is returned.
#if !SILVERLIGHT
    public ICollection Schemas()
    {
      Contract.Ensures(Contract.Result<ICollection>() != null);

      return default(ICollection);
    }

    //
    // Summary:
    //     Returns a collection of all the XML Schema definition language (XSD) schemas
    //     in the System.Xml.Schema.XmlSchemaSet that belong to the given namespace.
    //
    // Parameters:
    //   targetNamespace:
    //     The schema targetNamespace property.
    //
    // Returns:
    //     An System.Collections.ICollection object containing all the schemas that
    //     have been added to the System.Xml.Schema.XmlSchemaSet that belong to the
    //     given namespace. If no schemas have been added to the System.Xml.Schema.XmlSchemaSet,
    //     an empty System.Collections.ICollection object is returned.
    public ICollection Schemas(string targetNamespace)
    {
      Contract.Ensures(Contract.Result<ICollection>() != null);

      return default(ICollection);
    }
#endif

  }
}
