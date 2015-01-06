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
using System.Text;

namespace System.Xml
{
  // Summary:
  //     Resolves external XML resources named by a Uniform Resource Identifier (URI).
    [ContractClass(typeof(XmlResolverContracts))]
  public abstract class XmlResolver
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.XmlResolver class.
    //protected XmlResolver();

    // Summary:
    //     When overridden in a derived class, sets the credentials used to authenticate
    //     Web requests.
    //
    // Returns:
    //     An System.Net.ICredentials object. If this property is not set, the value
    //     defaults to null; that is, the XmlResolver has no user credentials.
    //public abstract ICredentials Credentials { set; }

    // Summary:
    //     When overridden in a derived class, maps a URI to an object containing the
    //     actual resource.
    //
    // Parameters:
    //   absoluteUri:
    //     The URI returned from System.Xml.XmlResolver.ResolveUri(System.Uri,System.String).
    //
    //   role:
    //     The current version does not use this parameter when resolving URIs. This
    //     is provided for future extensibility purposes. For example, this can be mapped
    //     to the xlink:role and used as an implementation specific argument in other
    //     scenarios.
    //
    //   ofObjectToReturn:
    //     The type of object to return. The current version only returns System.IO.Stream
    //     objects.
    //
    // Returns:
    //     A System.IO.Stream object or null if a type other than stream is specified.
    //
    // Exceptions:
    //   System.Xml.XmlException:
    //     ofObjectToReturn is not a Stream type.
    //
    //   System.UriFormatException:
    //     The specified URI is not an absolute URI.
    //
    //   System.ArgumentNullException:
    //     absoluteUri is null.
    //
    //   System.Exception:
    //     There is a runtime error (for example, an interrupted server connection).
    abstract public object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn);
    
    
    //
    // Summary:
    //     When overridden in a derived class, resolves the absolute URI from the base
    //     and relative URIs.
    //
    // Parameters:
    //   baseUri:
    //     The base URI used to resolve the relative URI
    //
    //   relativeUri:
    //     The URI to resolve. The URI can be absolute or relative. If absolute, this
    //     value effectively replaces the baseUri value. If relative, it combines with
    //     the baseUri to make an absolute URI.
    //
    // Returns:
    //     A System.Uri representing the absolute URI or null if the relative URI cannot
    //     be resolved.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     relativeUri is null
    public virtual Uri ResolveUri(Uri baseUri, string relativeUri)
    {
      // Implementation is very forgiving.  Only restriction seems to be that if we have both a baseUri and relativeUri, base must be an absolute URI
      Contract.Requires(!(baseUri != null && relativeUri != null) || baseUri.IsAbsoluteUri);
      // MSDN says it can return null.  The virtual method on XmlResolver can't, but a subclass theoretically could.
      return null;

    }

    //
    // Summary:
    //     This method adds the ability for the resolver to return other types than
    //     just System.IO.Stream.
    //
    // Parameters:
    //   absoluteUri:
    //     The URI.
    //
    //   type:
    //     The type to return.
    //
    // Returns:
    //     Returns true if the type is supported.
#if NETFRAMEWORK_4_0
    public virtual bool SupportsType(Uri absoluteUri, Type type)
    {
      Contract.Requires(absoluteUri != null);
      // type can be null.
      return false;
    }
#endif
  }

  [ContractClassFor(typeof(XmlResolver))]
  abstract class XmlResolverContracts : XmlResolver
  {
    public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
    {
      Contract.Requires(absoluteUri != null);
      //Contract.Requires(!absoluteUri.IsAbsoluteUri);  // Not in one implementation.
      // No obvious contract on role.
      // Implementation will allow ofObjectToReturn to be null.
      return null;
    }
  }
}
