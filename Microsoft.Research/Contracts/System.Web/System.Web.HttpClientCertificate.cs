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
using System.Collections.Specialized;
using System.Diagnostics.Contracts;

namespace System.Web
{
  // Summary:
  //     Provides the client certificate fields issued by the client in response to
  //     the server's request for the client's identity.
  public class HttpClientCertificate : NameValueCollection
  {
    // Summary:
    //     Gets or sets the certificate issuer, in binary format.
    //
    // Returns:
    //     The certificate issuer, expressed in binary format.
    public byte[] BinaryIssuer
    {
      get
      {
        Contract.Ensures(Contract.Result<byte[]>() != null);
        return default(byte[]);
      }
    }
    //
    // Summary:
    //     Gets the encoding of the certificate.
    //
    // Returns:
    //     One of the CERT_CONTEXT.dwCertEncodingType values.
    extern public int CertEncoding { get; }
    //
    // Summary:
    //     Gets a string containing the binary stream of the entire certificate content,
    //     in ASN.1 format.
    //
    // Returns:
    //     The client certificate.
    public byte[] Certificate
    {
      get
      {
        Contract.Ensures(Contract.Result<byte[]>() != null);
        return default(byte[]);
      }
    }
    //
    // Summary:
    //     Gets the unique ID for the client certificate, if provided.
    //
    // Returns:
    //     The client certificate ID.
    public string Cookie
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     A set of flags that provide additional client certificate information.
    //
    // Returns:
    //     A set of Boolean flags.
    extern public int Flags { get; }
    //
    // Summary:
    //     Gets a value that indicates whether the client certificate is present.
    //
    // Returns:
    //     true if the client certificate is present; otherwise, false.
    extern public bool IsPresent { get; }
    //
    // Summary:
    //     A string that contains a list of subfield values containing information about
    //     the certificate issuer.
    //
    // Returns:
    //     The certificate issuer's information.
    public string Issuer
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets a value that indicates whether the client certificate is valid.
    //
    // Returns:
    //     true if the client certificate is valid; otherwise, false.
    extern public bool IsValid { get; }
    //
    // Summary:
    //     Gets the number of bits in the digital certificate key size. For example,
    //     128.
    //
    // Returns:
    //     The number of bits in the key size.
    extern public int KeySize { get; }
    //
    // Summary:
    //     Gets the public key binary value from the certificate.
    //
    // Returns:
    //     A byte array that contains the public key value.
    public byte[] PublicKey
    {
      get
      {
        Contract.Ensures(Contract.Result<byte[]>() != null);
        return default(byte[]);
      }
    }
    //
    // Summary:
    //     Gets the number of bits in the server certificate private key. For example,
    //     1024.
    //
    // Returns:
    //     The number of bits in the server certificate private key.
    extern public int SecretKeySize { get; }
    //
    // Summary:
    //     Provides the certificate serial number as an ASCII representation of hexadecimal
    //     bytes separated by hyphens. For example, 04-67-F3-02.
    //
    // Returns:
    //     The certificate serial number.
    extern public string SerialNumber { get; }
    //
    // Summary:
    //     Gets the issuer field of the server certificate.
    //
    // Returns:
    //     The issuer field of the server certificate.
    public string ServerIssuer
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the subject field of the server certificate.
    //
    // Returns:
    //     The subject field of the server certificate.
    public string ServerSubject
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the subject field of the client certificate.
    //
    // Returns:
    //     The subject field of the client certificate.
    public string Subject
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the date when the certificate becomes valid. The date varies with international
    //     settings.
    //
    // Returns:
    //     The date when the certificate becomes valid.
    extern public DateTime ValidFrom { get; }
    //
    // Summary:
    //     Gets the certificate expiration date.
    //
    // Returns:
    //     The certificate expiration date.
    extern public DateTime ValidUntil { get; }

    // Summary:
    //     Returns individual client certificate fields by name.
    //
    // Parameters:
    //   field:
    //     The item in the collection to retrieve.
    //
    // Returns:
    //     The value of the item specified by field.
    public override string Get(string field)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
  }
}
