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
  //     Represents an XML declaration.
  public class XDeclaration
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XDeclaration class from
    //     another System.Xml.Linq.XDeclaration object.
    //
    // Parameters:
    //   other:
    //     The System.Xml.Linq.XDeclaration used to initialize this System.Xml.Linq.XDeclaration
    //     object.
    public XDeclaration(XDeclaration other)
    {
      Contract.Requires(other != null);

    }
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XDeclaration class with
    //     the specified version, encoding, and standalone status.
    //
    // Parameters:
    //   version:
    //     The version of the XML, usually "1.0".
    //
    //   encoding:
    //     The encoding for the XML document.
    //
    //   standalone:
    //     A string containing "yes" or "no" that specifies whether the XML is standalone
    //     or requires external entities to be resolved.
    extern public XDeclaration(string version, string encoding, string standalone);

    // Summary:
    //     Gets or sets the encoding for this document.
    //
    // Returns:
    //     A System.String containing the code page name for this document.
    extern public string Encoding { get; set; }
    //
    // Summary:
    //     Gets or sets the standalone property for this document.
    //
    // Returns:
    //     A System.String containing the standalone property for this document.
    extern public string Standalone { get; set; }
    //
    // Summary:
    //     Gets or sets the version property for this document.
    //
    // Returns:
    //     A System.String containing the version property for this document.
    extern public string Version { get; set; }

  }
}
