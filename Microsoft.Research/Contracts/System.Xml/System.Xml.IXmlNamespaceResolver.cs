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
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Provides read-only access to a set of prefix and namespace mappings.

  [ContractClass(typeof(IXmlNamespaceResolverContracts))]
  public interface IXmlNamespaceResolver
  {
    // Summary:
    //     Gets a collection of defined prefix-namespace mappings that are currently
    //     in scope.
    //
    // Parameters:
    //   scope:
    //     An System.Xml.XmlNamespaceScope value that specifies the type of namespace
    //     nodes to return.
    //
    // Returns:
    //     An System.Collections.IDictionary that contains the current in-scope namespaces.
    IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope);
    //
    // Summary:
    //     Gets the namespace URI mapped to the specified prefix.
    //
    // Parameters:
    //   prefix:
    //     The prefix whose namespace URI you wish to find.
    //
    // Returns:
    //     The namespace URI that is mapped to the prefix; null if the prefix is not
    //     mapped to a namespace URI.
    //string LookupNamespace(string prefix);
    //
    // Summary:
    //     Gets the prefix that is mapped to the specified namespace URI.
    //
    // Parameters:
    //   namespaceName:
    //     The namespace URI whose prefix you wish to find.
    //
    // Returns:
    //     The prefix that is mapped to the namespace URI; null if the namespace URI
    //     is not mapped to a prefix.
    //string LookupPrefix(string namespaceName);
  }

  [ContractClassFor(typeof(IXmlNamespaceResolver))]
  abstract class IXmlNamespaceResolverContracts : IXmlNamespaceResolver
  {
    #region IXmlNamespaceResolver Members

    IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
    {
      Contract.Ensures(Contract.Result<IDictionary<string, string>>() != null);

      return default(IDictionary<string, string>);
    }

    #endregion
  }
}
