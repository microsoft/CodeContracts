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

namespace System.Xml.Schema
{
  // Summary:
  //     Provides the collections for contained elements in the System.Xml.Schema.XmlSchema
  //     class (for example, Attributes, AttributeGroups, Elements, and so on).
  public class XmlSchemaObjectTable
  {
    internal XmlSchemaObjectTable() { }

    // Summary:
    //     Gets the number of items contained in the System.Xml.Schema.XmlSchemaObjectTable.
    //
    // Returns:
    //     The number of items contained in the System.Xml.Schema.XmlSchemaObjectTable.
    public int Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }
    
    //
    // Summary:
    //     Returns a collection of all the named elements in the System.Xml.Schema.XmlSchemaObjectTable.
    //
    // Returns:
    //     A collection of all the named elements in the System.Xml.Schema.XmlSchemaObjectTable.
    public ICollection Names
    {
      get
      {
        Contract.Ensures(Contract.Result<ICollection>() != null);

        return default(ICollection);
      }
    }
    //
    // Summary:
    //     Returns a collection of all the values for all the elements in the System.Xml.Schema.XmlSchemaObjectTable.
    //
    // Returns:
    //     A collection of all the values for all the elements in the System.Xml.Schema.XmlSchemaObjectTable.
    public ICollection Values
    {
      get
      {
        Contract.Ensures(Contract.Result<ICollection>() != null);

        return default(ICollection);
      }
    }

    // Summary:
    //     Returns the element in the System.Xml.Schema.XmlSchemaObjectTable specified
    //     by qualified name.
    //
    // Parameters:
    //   name:
    //     The System.Xml.XmlQualifiedName of the element to return.
    //
    // Returns:
    //     The System.Xml.Schema.XmlSchemaObject of the element in the System.Xml.Schema.XmlSchemaObjectTable
    //     specified by qualified name.
    //public XmlSchemaObject this[XmlQualifiedName name] { get; }

    // Summary:
    //     Determines if the qualified name specified exists in the collection.
    //
    // Parameters:
    //   name:
    //     The System.Xml.XmlQualifiedName.
    //
    // Returns:
    //     true if the qualified name specified exists in the collection; otherwise,
    //     false.
    //public bool Contains(XmlQualifiedName name);
    //
    // Summary:
    //     Returns an enumerator that can iterate through the System.Xml.Schema.XmlSchemaObjectTable.
    //
    // Returns:
    //     An System.Collections.IDictionaryEnumerator that can iterate through System.Xml.Schema.XmlSchemaObjectTable.
    public IDictionaryEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<IDictionaryEnumerator>() != null);

      return default(IDictionaryEnumerator);
    }
  }
}

#endif