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
using System.Xml.Linq;
using System.Collections.Generic;

namespace System.Xml.XPath
{
#if !SILVERLIGHT

  public static class Extensions
  {
    public static XPathNavigator CreateNavigator(this XNode node)
    {
      Contract.Requires(node != null);
      Contract.Ensures(Contract.Result<XPathNavigator>() != null);
      return default(XPathNavigator);
    }
    public static XPathNavigator CreateNavigator(this XNode node, XmlNameTable nameTable)
    {
      Contract.Requires(node != null);
      Contract.Ensures(Contract.Result<XPathNavigator>() != null);
      return default(XPathNavigator);
    }
    public static object XPathEvaluate(this XNode node, string expression)
    {
      Contract.Requires(node != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
    public static object XPathEvaluate(this XNode node, string expression, IXmlNamespaceResolver resolver)
    {
      Contract.Requires(node != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
    public static XElement XPathSelectElement(this XNode node, string expression)
    {
      Contract.Requires(node != null);
      return default(XElement);      
    }
    public static XElement XPathSelectElement(this XNode node, string expression, IXmlNamespaceResolver resolver)
    {
      Contract.Requires(node != null);
      return default(XElement);
    }
    public static IEnumerable<XElement> XPathSelectElements(this XNode node, string expression)
    {
      Contract.Requires(node != null);
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
    public static IEnumerable<XElement> XPathSelectElements(this XNode node, string expression, IXmlNamespaceResolver resolver)
    {
      Contract.Requires(node != null);
      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);
      return default(IEnumerable<XElement>);
    }
  }
#endif

}