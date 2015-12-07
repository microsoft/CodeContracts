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
//using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Security.Cryptography {
  // Summary:
  //     Represents the abstract base class from which all implementations of asymmetric
  //     algorithms must inherit.
  [ComVisible(true)]
  public abstract class AsymmetricAlgorithm : IDisposable {
    // Summary:
    //     Represents the size, in bits, of the key modulus used by the asymmetric algorithm.
    protected int KeySizeValue;
    //
    // Summary:
    //     Specifies the key sizes that are supported by the asymmetric algorithm.
    protected KeySizes[] LegalKeySizesValue;

    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.AsymmetricAlgorithm
    //     class.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     The implementation of the derived class is not valid.
    protected AsymmetricAlgorithm();

    // Summary:
    //     When overridden in a derived class, gets the name of the key exchange algorithm.
    //
    // Returns:
    //     The name of the key exchange algorithm.
    public abstract string KeyExchangeAlgorithm { get; }
    //
    // Summary:
    //     Gets or sets the size, in bits, of the key modulus used by the asymmetric
    //     algorithm.
    //
    // Returns:
    //     The size, in bits, of the key modulus used by the asymmetric algorithm.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     The key modulus size is invalid.
    public virtual int KeySize { get; set; }
    //
    // Summary:
    //     Gets the key sizes that are supported by the asymmetric algorithm.
    //
    // Returns:
    //     An array that contains the key sizes supported by the asymmetric algorithm.
    public virtual KeySizes[] LegalKeySizes { get; }
    //
    // Summary:
    //     Gets the name of the signature algorithm.
    //
    // Returns:
    //     The name of the signature algorithm.
    public abstract string SignatureAlgorithm { get; }

    // Summary:
    //     Releases all resources used by the System.Security.Cryptography.AsymmetricAlgorithm
    //     class.
    public void Clear();
    //
    // Summary:
    //     Creates a default cryptographic object used to perform the asymmetric algorithm.
    //
    // Returns:
    //     The cryptographic object used to perform the asymmetric algorithm.
    public static AsymmetricAlgorithm Create();
    //
    // Summary:
    //     Creates the specified cryptographic object used to perform the asymmetric
    //     algorithm.
    //
    // Parameters:
    //   algName:
    //     The name of the specific implementation of System.Security.Cryptography.AsymmetricAlgorithm
    //     to use.
    //
    // Returns:
    //     A cryptographic object used to perform the asymmetric algorithm.
    public static AsymmetricAlgorithm Create(string algName);
    //
    // Summary:
    //     When overridden in a derived class, releases the unmanaged resources used
    //     by the System.Security.Cryptography.AsymmetricAlgorithm and optionally releases
    //     the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    protected abstract void Dispose(bool disposing);
    //
    // Summary:
    //     When overridden in a derived class, reconstructs an System.Security.Cryptography.AsymmetricAlgorithm
    //     object from an XML string.
    //
    // Parameters:
    //   xmlString:
    //     The XML string to use to reconstruct the System.Security.Cryptography.AsymmetricAlgorithm
    //     object.
    public abstract void FromXmlString(string xmlString);
    //
    // Summary:
    //     When overridden in a derived class, creates and returns an XML string representation
    //     of the current System.Security.Cryptography.AsymmetricAlgorithm object.
    //
    // Parameters:
    //   includePrivateParameters:
    //     true to include private parameters; otherwise, false.
    //
    // Returns:
    //     An XML string encoding of the current System.Security.Cryptography.AsymmetricAlgorithm
    //     object.
    public abstract string ToXmlString(bool includePrivateParameters);
  }
}
