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
using System.Runtime.Serialization;
using System.Diagnostics.Contracts;

namespace System.Xml.Linq
{
  // Summary:
  //     Represents a name of an XML element or attribute.
  public sealed class XName //: IEquatable<XName>
  {
    private XName() { }
    // Summary:
    //     Returns a value indicating whether two instances of System.Xml.Linq.XName
    //     are not equal.
    //
    // Parameters:
    //   left:
    //     The first System.Xml.Linq.XName to compare.
    //
    //   right:
    //     The second System.Xml.Linq.XName to compare.
    //
    // Returns:
    //     true if left and right are not equal; otherwise false.
    extern public static bool operator !=(XName left, XName right);
    //
    // Summary:
    //     Returns a value indicating whether two instances of System.Xml.Linq.XName
    //     are equal.
    //
    // Parameters:
    //   left:
    //     The first System.Xml.Linq.XName to compare.
    //
    //   right:
    //     The second System.Xml.Linq.XName to compare.
    //
    // Returns:
    //     true if left and right are equal; otherwise false.
    extern public static bool operator ==(XName left, XName right);
    //
    // Summary:
    //     Converts a string formatted as an expanded XML name (that is,{namespace}localname)
    //     to an System.Xml.Linq.XName object.
    //
    // Parameters:
    //   expandedName:
    //     A string that contains an expanded XML name in the format {namespace}localname.
    //
    // Returns:
    //     An System.Xml.Linq.XName object constructed from the expanded name.
    public static implicit operator XName(string expandedName)
    {
      Contract.Requires(!String.IsNullOrEmpty(expandedName));
      Contract.Ensures(Contract.Result<XName>() != null);
      return default(XName);
    }

    // Summary:
    //     Gets the local (unqualified) part of the name.
    //
    // Returns:
    //     A System.String that contains the local (unqualified) part of the name.
    public string LocalName
    {
      get
      {
        Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the namespace part of the fully qualified name.
    //
    // Returns:
    //     An System.Xml.Linq.XNamespace that contains the namespace part of the name.
    public XNamespace Namespace
    {
      get
      {
        Contract.Ensures(Contract.Result<XNamespace>() != null);
        return default(XNamespace);
      }
    }
    //
    // Summary:
    //     Returns the URI of the System.Xml.Linq.XNamespace for this System.Xml.Linq.XName.
    //
    // Returns:
    //     The URI of the System.Xml.Linq.XNamespace for this System.Xml.Linq.XName.
    public string NamespaceName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    //
    // Summary:
    //     Gets an System.Xml.Linq.XName object from an expanded name.
    //
    // Parameters:
    //   expandedName:
    //     A System.String that contains an expanded XML name in the format {namespace}localname.
    //
    // Returns:
    //     An System.Xml.Linq.XName object constructed from the expanded name.
    [Pure]
    public static XName Get(string expandedName)
    {
      Contract.Requires(!String.IsNullOrEmpty(expandedName));
      Contract.Ensures(Contract.Result<XName>() != null);
      return default(XName);
    }
    //
    // Summary:
    //     Gets an System.Xml.Linq.XName object from a local name and a namespace.
    //
    // Parameters:
    //   localName:
    //     A local (unqualified) name.
    //
    //   namespaceName:
    //     An XML namespace.
    //
    // Returns:
    //     An System.Xml.Linq.XName object created from the specified local name and
    //     namespace.
    [Pure]
    public static XName Get(string localName, string namespaceName)
    {
      Contract.Requires(!String.IsNullOrEmpty(localName));
      Contract.Requires(namespaceName != null);
      Contract.Ensures(Contract.Result<XName>() != null);
      return default(XName);
    }
  }
}
