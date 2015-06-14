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
using System.Xml;
using System.Diagnostics.Contracts;

namespace System.Xml.Linq
{
  // Summary:
  //     Represents an XML Document Type Definition (DTD).
  public class XDocumentType : XNode
  {
    // Summary:
    //     Initializes an instance of the System.Xml.Linq.XDocumentType class from another
    //     System.Xml.Linq.XDocumentType object.
    //
    // Parameters:
    //   other:
    //     An System.Xml.Linq.XDocumentType object to copy from.
    public XDocumentType(XDocumentType other)
    {
      Contract.Requires(other != null);
    }
    //
    // Summary:
    //     Initializes an instance of the System.Xml.Linq.XDocumentType class.
    //
    // Parameters:
    //   name:
    //     A System.String that contains the qualified name of the DTD, which is the
    //     same as the qualified name of the root element of the XML document.
    //
    //   publicId:
    //     A System.String that contains the public identifier of an external public
    //     DTD.
    //
    //   systemId:
    //     A System.String that contains the system identifier of an external private
    //     DTD.
    //
    //   internalSubset:
    //     A System.String that contains the internal subset for an internal DTD.
    public XDocumentType(string name, string publicId, string systemId, string internalSubset)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
    }

    // Summary:
    //     Gets or sets the internal subset for this Document Type Definition (DTD).
    //
    // Returns:
    //     A System.String that contains the internal subset for this Document Type
    //     Definition (DTD).
    extern public string InternalSubset { get; set; }
    //
    // Summary:
    //     Gets or sets the name for this Document Type Definition (DTD).
    //
    // Returns:
    //     A System.String that contains the name for this Document Type Definition
    //     (DTD).
    public string Name
    {
      get
      {
        Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));
        return default(string);
      }
      set
      {
        Contract.Requires(!String.IsNullOrEmpty(value));
      }
    }

    //
    // Summary:
    //     Gets or sets the public identifier for this Document Type Definition (DTD).
    //
    // Returns:
    //     A System.String that contains the public identifier for this Document Type
    //     Definition (DTD).
    extern public string PublicId { get; set; }
    //
    // Summary:
    //     Gets or sets the system identifier for this Document Type Definition (DTD).
    //
    // Returns:
    //     A System.String that contains the system identifier for this Document Type
    //     Definition (DTD).
    extern public string SystemId { get; set; }

  }
}
