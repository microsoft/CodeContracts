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

namespace System.Xml.Linq
{
  // Summary:
  //     Represents an XML namespace. This class cannot be inherited.
  public sealed class XNamespace
  {
    private XNamespace() { }

    [Pure]
    extern public static bool operator !=(XNamespace left, XNamespace right);

    [Pure]
    public static XName operator +(XNamespace ns, string localName)
    {
      Contract.Requires(ns != null);
      Contract.Requires(localName != null);
      
      Contract.Ensures(Contract.Result<XName>() != null);
      
      return default(XName);
    }
    
    [Pure]
    extern public static bool operator ==(XNamespace left, XNamespace right);

    [Pure]
    public static implicit operator XNamespace(string namespaceName)
    {
      Contract.Ensures((Contract.Result<XNamespace>() != null) == (namespaceName != null));

      return null;
    }

    public string NamespaceName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public static XNamespace None
    {
      get
      {
        Contract.Ensures(Contract.Result<XNamespace>() != null);
        return default(XNamespace);
      }
    }
 
    public static XNamespace Xml
    {
      get
      {
        Contract.Ensures(Contract.Result<XNamespace>() != null);
        return default(XNamespace);
      }
    }
    
    public static XNamespace Xmlns
    {
      get
      {
        Contract.Ensures(Contract.Result<XNamespace>() != null);
        return default(XNamespace);
      }
    }

 
    [Pure]
    public static XNamespace Get(string namespaceName)
    {
      Contract.Requires(namespaceName != null);
      Contract.Ensures(Contract.Result<XNamespace>() != null);
      return default(XNamespace);
    }
    
    [Pure]
    public XName GetName(string localName)
    {
      Contract.Requires(localName != null);
      Contract.Ensures(Contract.Result<XName>() != null);
      return default(XName);
    }
  }
}
