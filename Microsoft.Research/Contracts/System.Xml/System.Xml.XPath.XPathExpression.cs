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
using System.Xml;
using System.Diagnostics.Contracts;

namespace System.Xml.XPath
{
  // Summary:
  //     Provides a typed class that represents a compiled XPath expression.
  public /*abstract*/ class XPathExpression
  {

    internal XPathExpression() { }

    // Summary:
    //     When overridden in a derived class, gets a string representation of the System.Xml.XPath.XPathExpression.
    //
    // Returns:
    //     A string representation of the System.Xml.XPath.XPathExpression.
    //public abstract string Expression { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets the result type of the XPath expression.
    //
    // Returns:
    //     An System.Xml.XPath.XPathResultType value representing the result type of
    //     the XPath expression.
    //public abstract XPathResultType ReturnType { get; }

    // Summary:
    //     When overridden in a derived class, sorts the nodes selected by the XPath
    //     expression according to the specified System.Collections.IComparer object.
    //
    // Parameters:
    //   expr:
    //     An object representing the sort key. This can be the string value of the
    //     node or an System.Xml.XPath.XPathExpression object with a compiled XPath
    //     expression.
    //
    //   comparer:
    //     An System.Collections.IComparer object that provides the specific data type
    //     comparisons for comparing two objects for equivalence.
    //
    // Exceptions:
    //   System.Xml.XPath.XPathException:
    //     The System.Xml.XPath.XPathExpression or sort key includes a prefix and either
    //     an System.Xml.XmlNamespaceManager is not provided, or the prefix cannot be
    //     found in the supplied System.Xml.XmlNamespaceManager.
    //public abstract void AddSort(object expr, IComparer comparer);
    //
    // Summary:
    //     When overridden in a derived class, sorts the nodes selected by the XPath
    //     expression according to the supplied parameters.
    //
    // Parameters:
    //   expr:
    //     An object representing the sort key. This can be the string value of the
    //     node or an System.Xml.XPath.XPathExpression object with a compiled XPath
    //     expression.
    //
    //   order:
    //     An System.Xml.XPath.XmlSortOrder value indicating the sort order.
    //
    //   caseOrder:
    //     An System.Xml.XPath.XmlCaseOrder value indicating how to sort uppercase and
    //     lowercase letters.
    //
    //   lang:
    //     The language to use for comparison. Uses the System.Globalization.CultureInfo
    //     class that can be passed to the Overload:System.String.Compare method for
    //     the language types, for example, "us-en" for U.S. English. If an empty string
    //     is specified, the system environment is used to determine the System.Globalization.CultureInfo.
    //
    //   dataType:
    //     An System.Xml.XPath.XmlDataType value indicating the sort order for the data
    //     type.
    //
    // Exceptions:
    //   System.Xml.XPath.XPathException:
    //     The System.Xml.XPath.XPathExpression or sort key includes a prefix and either
    //     an System.Xml.XmlNamespaceManager is not provided, or the prefix cannot be
    ////     found in the supplied System.Xml.XmlNamespaceManager.
    //public abstract void AddSort(object expr, XmlSortOrder order, XmlCaseOrder caseOrder, string lang, XmlDataType dataType);
    //
    // Summary:
    //     When overridden in a derived class, returns a clone of this System.Xml.XPath.XPathExpression.
    //
    // Returns:
    //     A new System.Xml.XPath.XPathExpression object.
    public virtual XPathExpression Clone()
    {
      Contract.Ensures(Contract.Result<XPathExpression>() != null);

      return default(XPathExpression);
    }
    //
    // Summary:
    //     Compiles the XPath expression specified and returns an System.Xml.XPath.XPathExpression
    //     object representing the XPath expression.
    //
    // Parameters:
    //   xpath:
    //     An XPath expression.
    //
    // Returns:
    //     An System.Xml.XPath.XPathExpression object.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The XPath expression parameter is not a valid XPath expression.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    public static XPathExpression Compile(string xpath)
    {
      Contract.Ensures(Contract.Result<XPathExpression>() != null);

      return default(XPathExpression);
    }
    //
    // Summary:
    //     Compiles the specified XPath expression, with the System.Xml.IXmlNamespaceResolver
    //     object specified for namespace resolution, and returns an System.Xml.XPath.XPathExpression
    //     object that represents the XPath expression.
    //
    // Parameters:
    //   xpath:
    //     An XPath expression.
    //
    //   nsResolver:
    //     An object that implements the System.Xml.IXmlNamespaceResolver interface
    //     for namespace resolution.
    //
    // Returns:
    //     An System.Xml.XPath.XPathExpression object.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The XPath expression parameter is not a valid XPath expression.
    //
    //   System.Xml.XPath.XPathException:
    //     The XPath expression is not valid.
    public static XPathExpression Compile(string xpath, IXmlNamespaceResolver nsResolver)
    {
      Contract.Ensures(Contract.Result<XPathExpression>() != null);

      return default(XPathExpression);
    }
    //
    // Summary:
    //     When overridden in a derived class, specifies the System.Xml.IXmlNamespaceResolver
    //     object to use for namespace resolution.
    //
    // Parameters:
    //   nsResolver:
    //     An object that implements the System.Xml.IXmlNamespaceResolver interface
    //     to use for namespace resolution.
    //
    // Exceptions:
    //   System.Xml.XPath.XPathException:
    //     The System.Xml.IXmlNamespaceResolver object parameter is not derived from
    //     System.Xml.IXmlNamespaceResolver.
    //public abstract void SetContext(IXmlNamespaceResolver nsResolver);
    //
    // Summary:
    //     When overridden in a derived class, specifies the System.Xml.XmlNamespaceManager
    //     object to use for namespace resolution.
    //
    // Parameters:
    //   nsManager:
    //     An System.Xml.XmlNamespaceManager object to use for namespace resolution.
    //
    // Exceptions:
    //   System.Xml.XPath.XPathException:
    //     The System.Xml.XmlNamespaceManager object parameter is not derived from the
    //     System.Xml.XmlNamespaceManager class.
    //public abstract void SetContext(XmlNamespaceManager nsManager);
  }
}

#endif