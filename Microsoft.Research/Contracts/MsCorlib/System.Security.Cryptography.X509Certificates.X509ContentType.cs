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
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates {
  // Summary:
  //     Specifies the format of an X.509 certificate.
  [ComVisible(true)]
  public enum X509ContentType {
    // Summary:
    //     An unknown X.509 Certificate.
    Unknown = 0,
    //
    // Summary:
    //     A single X.509 Certificate.
    Cert = 1,
    //
    // Summary:
    //     A single serialized X.509 Certificate.
    SerializedCert = 2,
    //
    // Summary:
    //     A PKCS #12–formatted certificate. The System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12
    //     value is identical to the System.Security.Cryptography.X509Certificates.X509ContentType.Pfx
    //     value.
    Pkcs12 = 3,
    //
    // Summary:
    //     A PFX-formatted certificate. The System.Security.Cryptography.X509Certificates.X509ContentType.Pfx
    //     value is identical to the System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12
    //     value.
    Pfx = 3,
    //
    // Summary:
    //     A serialized store.
    SerializedStore = 4,
    //
    // Summary:
    //     A PKCS #7–formatted certificate.
    Pkcs7 = 5,
    //
    // Summary:
    //     An Authenticode X.509 certificate.
    Authenticode = 6,
  }
}
