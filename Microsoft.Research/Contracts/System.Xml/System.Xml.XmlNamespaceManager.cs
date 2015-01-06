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
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Resolves, adds, and removes namespaces to a collection and provides scope
  //     management for these namespaces.
  public class XmlNamespaceManager // : IXmlNamespaceResolver, IEnumerable
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XmlNamespaceManager class with
    //     the specified System.Xml.XmlNameTable.
    //
    // Parameters:
    //   nameTable:
    //     The System.Xml.XmlNameTable to use.
    //
    // Exceptions:
    //   System.NullReferenceException:
    //     null is passed to the constructor
    public XmlNamespaceManager(XmlNameTable nameTable)
    {
      Contract.Requires(nameTable != null);

    }

    // Summary:
    //     Gets the namespace URI for the default namespace.
    //
    // Returns:
    //     Returns the namespace URI for the default namespace, or String.Empty if there
    //     is no default namespace.
    public virtual string DefaultNamespace
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the System.Xml.XmlNameTable associated with this object.
    //
    // Returns:
    //     The System.Xml.XmlNameTable used by this object.
    //public virtual XmlNameTable NameTable { get; }

    // Summary:
    //     Adds the given namespace to the collection.
    //
    // Parameters:
    //   prefix:
    //     The prefix to associate with the namespace being added. Use String.Empty
    //     to add a default namespace.  Note: If the System.Xml.XmlNamespaceManager
    //     will be used for resolving namespaces in an XML Path Language (XPath) expression,
    //     a prefix must be specified. If an XPath expression does not include a prefix,
    //     it is assumed that the namespace Uniform Resource Identifier (URI) is the
    //     empty namespace. For more information about XPath expressions and the System.Xml.XmlNamespaceManager,
    //     refer to the System.Xml.XmlNode.SelectNodes(System.String) and System.Xml.XPath.XPathExpression.SetContext(System.Xml.XmlNamespaceManager)
    //     methods.
    //
    //   uri:
    //     The namespace to add.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value for prefix is "xml" or "xmlns".
    //
    //   System.ArgumentNullException:
    //     The value for prefix or uri is null.
    public virtual void AddNamespace(string prefix, string uri)
    {
      Contract.Requires(prefix != null);
      Contract.Requires(uri != null);


    }

    //
    // Summary:
    //     Gets a value indicating whether the supplied prefix has a namespace defined
    //     for the current pushed scope.
    //
    // Parameters:
    //   prefix:
    //     The prefix of the namespace you want to find.
    //
    // Returns:
    //     true if there is a namespace defined; otherwise, false.
    //public virtual bool HasNamespace(string prefix);
    //
    // Summary:
    //     Gets the namespace URI for the specified prefix.
    //
    // Parameters:
    //   prefix:
    //     The prefix whose namespace URI you want to resolve. To match the default
    //     namespace, pass String.Empty.
    //
    // Returns:
    //     Returns the namespace URI for prefix or null if there is no mapped namespace.
    //     The returned string is atomized.  For more information on atomized strings,
    //     see System.Xml.XmlNameTable.
    //public virtual string LookupNamespace(string prefix);
    //
    // Summary:
    //     Finds the prefix declared for the given namespace URI.
    //
    // Parameters:
    //   uri:
    //     The namespace to resolve for the prefix.
    //
    // Returns:
    //     The matching prefix. If there is no mapped prefix, the method returns String.Empty.
    //     If a null value is supplied, then null is returned.
    //public virtual string LookupPrefix(string uri);
    //
    // Summary:
    //     Pops a namespace scope off the stack.
    //
    // Returns:
    //     true if there are namespace scopes left on the stack; false if there are
    //     no more namespaces to pop.
    //public virtual bool PopScope();
    //
    // Summary:
    //     Pushes a namespace scope onto the stack.
    //public virtual void PushScope();
    //
    // Summary:
    //     Removes the given namespace for the given prefix.
    //
    // Parameters:
    //   prefix:
    //     The prefix for the namespace
    //
    //   uri:
    //     The namespace to remove for the given prefix. The namespace removed is from
    //     the current namespace scope. Namespaces outside the current scope are ignored.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value of prefix or uri is null.
    public virtual void RemoveNamespace(string prefix, string uri)
    {
      Contract.Requires(prefix != null);
      Contract.Requires(uri != null);


    }
  }
}
