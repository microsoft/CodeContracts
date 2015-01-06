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
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Security.Cryptography.X509Certificates {
  // Summary:
  //     Provides methods that help you use X.509 v.3 certificates.
  public class X509Certificate 
  {
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class.
    extern public X509Certificate();
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class defined from a sequence of bytes representing an X.509v3 certificate.
    //
    // Parameters:
    //   data:
    //     A byte array containing data from an X.509 certificate.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentException:
    //     The rawData parameter is null.-or-The length of the rawData parameter is
    //     0.
    extern public X509Certificate(byte[] data);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using a handle to an unmanaged PCCERT_CONTEXT structure.
    //
    // Parameters:
    //   handle:
    //     A handle to an unmanaged PCCERT_CONTEXT structure.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentException:
    //     The handle parameter does not represent a valid PCCERT_CONTEXT structure.
    extern public X509Certificate(IntPtr handle);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using a using a certificate file name.
    //
    // Parameters:
    //   fileName:
    //     The name of a certificate file.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentException:
    //     The fileName parameter is null.
    extern public X509Certificate(string fileName);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using another System.Security.Cryptography.X509Certificates.X509Certificate
    //     class.
    //
    // Parameters:
    //   cert:
    //     A System.Security.Cryptography.X509Certificates.X509Certificate class from
    //     which to initialize this class.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentNullException:
    //     The value of the cert parameter is null.
    extern public X509Certificate(X509Certificate cert);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using a byte array and a password.
    //
    // Parameters:
    //   rawData:
    //     A byte array that contains data from an X.509 certificate.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentException:
    //     The rawData parameter is null.-or-The length of the rawData parameter is
    //     0.
    extern public X509Certificate(byte[] rawData, SecureString password);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using a byte array and a password.
    //
    // Parameters:
    //   rawData:
    //     A byte array containing data from an X.509 certificate.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentException:
    //     The rawData parameter is null.-or-The length of the rawData parameter is
    //     0.
    extern public X509Certificate(byte[] rawData, string password);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using a System.Runtime.Serialization.SerializationInfo object and a
    //     System.Runtime.Serialization.StreamingContext structure.
    //
    // Parameters:
    //   info:
    //     A System.Runtime.Serialization.SerializationInfo object that describes serialization
    //     information.
    //
    //   context:
    //     A System.Runtime.Serialization.StreamingContext structure that describes
    //     how serialization should be performed.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    extern public X509Certificate(SerializationInfo info, StreamingContext context);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using a certificate file name and a password.
    //
    // Parameters:
    //   fileName:
    //     The name of a certificate file.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentException:
    //     The fileName parameter is null.
    extern public X509Certificate(string fileName, SecureString password);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using a using a certificate file name and a password used to access
    //     the certificate.
    //
    // Parameters:
    //   fileName:
    //     The name of a certificate file.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentException:
    //     The fileName parameter is null.
    extern public X509Certificate(string fileName, string password);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using a byte array, a password, and a key storage flag.
    //
    // Parameters:
    //   rawData:
    //     A byte array that contains data from an X.509 certificate.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    //   keyStorageFlags:
    //     One of the System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
    //     values that controls where and how to import the private key.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentException:
    //     The rawData parameter is null.-or-The length of the rawData parameter is
    //     0.
    extern public X509Certificate(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using a byte array, a password, and a key storage flag.
    //
    // Parameters:
    //   rawData:
    //     A byte array containing data from an X.509 certificate.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    //   keyStorageFlags:
    //     One of the System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
    //     values.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentException:
    //     The rawData parameter is null.-or-The length of the rawData parameter is
    //     0.
    extern public X509Certificate(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using a certificate file name, a password, and a key storage flag.
    //
    // Parameters:
    //   fileName:
    //     The name of a certificate file.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    //   keyStorageFlags:
    //     One of the System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
    //     values that controls where and how to import the private key.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentException:
    //     The fileName parameter is null.
    extern public X509Certificate(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class using a certificate file name, a password used to access the certificate,
    //     and a key storage flag.
    //
    // Parameters:
    //   fileName:
    //     The name of a certificate file.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    //   keyStorageFlags:
    //     One of the System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
    //     values.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    //
    //   System.ArgumentException:
    //     The fileName parameter is null.
    extern public X509Certificate(string fileName, string password, X509KeyStorageFlags keyStorageFlags);

    // Summary:
    //     Gets a handle to a Microsoft Cryptographic API certificate context described
    //     by an unmanaged PCCERT_CONTEXT structure.
    //
    // Returns:
    //     An System.IntPtr structure that represents an unmanaged PCCERT_CONTEXT structure.
    extern public IntPtr Handle { get; }
    //
    // Summary:
    //     Gets the name of the certificate authority that issued the X.509v3 certificate.
    //
    // Returns:
    //     The name of the certificate authority that issued the X.509v3 certificate.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     The certificate handle is invalid.
    extern public string Issuer { get; }
    //
    // Summary:
    //     Gets the subject distinguished name from the certificate.
    //
    // Returns:
    //     The subject distinguished name from the certificate.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     The certificate handle is invalid.
    extern public string Subject { get; }

    // Summary:
    //     Creates an X.509v3 certificate from the specified certification file.
    //
    // Parameters:
    //   filename:
    //     The path of the certification file from which to create the X.509 certificate.
    //
    // Returns:
    //     The newly created X.509 certificate.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The filename parameter is null.
    extern public static X509Certificate CreateFromCertFile(string filename);
    //
    // Summary:
    //     Creates an X.509v3 certificate from the specified signed file.
    //
    // Parameters:
    //   filename:
    //     The path of the signed file from which to create the X.509 certificate.
    //
    // Returns:
    //     The newly created X.509 certificate.
    extern public static X509Certificate CreateFromSignedFile(string filename);
    //
    // Summary:
    //     Compares two System.Security.Cryptography.X509Certificates.X509Certificate
    //     objects for equality.
    //
    // Parameters:
    //   other:
    //     An System.Security.Cryptography.X509Certificates.X509Certificate object to
    //     compare to the current object.
    //
    // Returns:
    //     true if the current System.Security.Cryptography.X509Certificates.X509Certificate
    //     object is equal to the object specified by the other parameter; otherwise,
    //     false.
    extern public virtual bool Equals(X509Certificate other);
    //
    // Summary:
    //     Exports the current System.Security.Cryptography.X509Certificates.X509Certificate
    //     object to a byte array in a format described by one of the System.Security.Cryptography.X509Certificates.X509ContentType
    //     values.
    //
    // Parameters:
    //   contentType:
    //     One of the System.Security.Cryptography.X509Certificates.X509ContentType
    //     values that describes how to format the output data.
    //
    // Returns:
    //     An array of bytes that represents the current System.Security.Cryptography.X509Certificates.X509Certificate
    //     object.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     A value other than System.Security.Cryptography.X509Certificates.X509ContentType.Cert,
    //     System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert,
    //     or System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12 was
    //     passed to the contentType parameter.-or-The certificate could not be exported.
    extern public virtual byte[] Export(X509ContentType contentType);
    //
    // Summary:
    //     Exports the current System.Security.Cryptography.X509Certificates.X509Certificate
    //     object to a byte array using the specified format and a password.
    //
    // Parameters:
    //   contentType:
    //     One of the System.Security.Cryptography.X509Certificates.X509ContentType
    //     values that describes how to format the output data.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    // Returns:
    //     A byte array that represents the current System.Security.Cryptography.X509Certificates.X509Certificate
    //     object.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     A value other than System.Security.Cryptography.X509Certificates.X509ContentType.Cert,
    //     System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert,
    //     or System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12 was
    //     passed to the contentType parameter.-or-The certificate could not be exported.
    extern public virtual byte[] Export(X509ContentType contentType, SecureString password);
    //
    // Summary:
    //     Exports the current System.Security.Cryptography.X509Certificates.X509Certificate
    //     object to a byte array in a format described by one of the System.Security.Cryptography.X509Certificates.X509ContentType
    //     values, and using the specified password.
    //
    // Parameters:
    //   contentType:
    //     One of the System.Security.Cryptography.X509Certificates.X509ContentType
    //     values that describes how to format the output data.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    // Returns:
    //     An array of bytes that represents the current System.Security.Cryptography.X509Certificates.X509Certificate
    //     object.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     A value other than System.Security.Cryptography.X509Certificates.X509ContentType.Cert,
    //     System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert,
    //     or System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12 was
    //     passed to the contentType parameter.-or-The certificate could not be exported.
    extern public virtual byte[] Export(X509ContentType contentType, string password);
    //
    // Summary:
    //     Returns the hash value for the X.509v3 certificate as an array of bytes.
    //
    // Returns:
    //     The hash value for the X.509 certificate.
    extern public virtual byte[] GetCertHash();
    //
    // Summary:
    //     Returns the hash value for the X.509v3 certificate as a hexadecimal string.
    //
    // Returns:
    //     The hexadecimal string representation of the X.509 certificate hash value.
    extern public virtual string GetCertHashString();
    //
    // Summary:
    //     Returns the effective date of this X.509v3 certificate.
    //
    // Returns:
    //     The effective date for this X.509 certificate.
    extern public virtual string GetEffectiveDateString();
    //
    // Summary:
    //     Returns the expiration date of this X.509v3 certificate.
    //
    // Returns:
    //     The expiration date for this X.509 certificate.
    extern public virtual string GetExpirationDateString();
    //
    // Summary:
    //     Returns the name of the format of this X.509v3 certificate.
    //
    // Returns:
    //     The format of this X.509 certificate.
    extern public virtual string GetFormat();
    //
    // Summary:
    //     Returns the name of the certification authority that issued the X.509v3 certificate.
    //
    // Returns:
    //     The name of the certification authority that issued the X.509 certificate.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error with the certificate occurs. For example:The certificate file does
    //     not exist.The certificate is invalid.The certificate's password is incorrect.
    extern public virtual string GetIssuerName();
    //
    // Summary:
    //     Returns the key algorithm information for this X.509v3 certificate.
    //
    // Returns:
    //     The key algorithm information for this X.509 certificate as a string.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     The certificate context is invalid.
    extern public virtual string GetKeyAlgorithm();
    //
    // Summary:
    //     Returns the key algorithm parameters for the X.509v3 certificate.
    //
    // Returns:
    //     The key algorithm parameters for the X.509 certificate as an array of bytes.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     The certificate context is invalid.
    extern public virtual byte[] GetKeyAlgorithmParameters();
    //
    // Summary:
    //     Returns the key algorithm parameters for the X.509v3 certificate.
    //
    // Returns:
    //     The key algorithm parameters for the X.509 certificate as a hexadecimal string.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     The certificate context is invalid.
    extern public virtual string GetKeyAlgorithmParametersString();
    //
    // Summary:
    //     Returns the public key for the X.509v3 certificate.
    //
    // Returns:
    //     The public key for the X.509 certificate as an array of bytes.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     The certificate context is invalid.
    extern public virtual byte[] GetPublicKey();
    //
    // Summary:
    //     Returns the public key for the X.509v3 certificate.
    //
    // Returns:
    //     The public key for the X.509 certificate as a hexadecimal string.
    extern public virtual string GetPublicKeyString();
    //
    // Summary:
    //     Returns the raw data for the entire X.509v3 certificate.
    //
    // Returns:
    //     A byte array containing the X.509 certificate data.
    extern public virtual byte[] GetRawCertData();
    //
    // Summary:
    //     Returns the raw data for the entire X.509v3 certificate.
    //
    // Returns:
    //     The X.509 certificate data as a hexadecimal string.
    extern public virtual string GetRawCertDataString();
    //
    // Summary:
    //     Returns the serial number of the X.509v3 certificate.
    //
    // Returns:
    //     The serial number of the X.509 certificate as an array of bytes.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     The certificate context is invalid.
    extern public virtual byte[] GetSerialNumber();
    //
    // Summary:
    //     Returns the serial number of the X.509v3 certificate.
    //
    // Returns:
    //     The serial number of the X.509 certificate as a hexadecimal string.
    extern public virtual string GetSerialNumberString();
    //
    // Summary:
    //     Populates the System.Security.Cryptography.X509Certificates.X509Certificate
    //     object with data from a byte array.
    //
    // Parameters:
    //   rawData:
    //     A byte array containing data from an X.509 certificate.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The rawData parameter is null.-or-The length of the rawData parameter is
    //     0.
    extern public virtual void Import(byte[] rawData);
    //
    // Summary:
    //     Populates the System.Security.Cryptography.X509Certificates.X509Certificate
    //     object with information from a certificate file.
    //
    // Parameters:
    //   fileName:
    //     The name of a certificate file represented as a string.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The fileName parameter is null.
    extern public virtual void Import(string fileName);
    //
    // Summary:
    //     Populates an System.Security.Cryptography.X509Certificates.X509Certificate
    //     object using data from a byte array, a password, and a key storage flag.
    //
    // Parameters:
    //   rawData:
    //     A byte array that contains data from an X.509 certificate.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    //   keyStorageFlags:
    //     One of the System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
    //     values that controls where and how to import the private key.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The rawData parameter is null.-or-The length of the rawData parameter is
    //     0.
    extern public virtual void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags);
    //
    // Summary:
    //     Populates the System.Security.Cryptography.X509Certificates.X509Certificate
    //     object using data from a byte array, a password, and flags for determining
    //     how the private key is imported.
    //
    // Parameters:
    //   rawData:
    //     A byte array containing data from an X.509 certificate.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    //   keyStorageFlags:
    //     One of the System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
    //     values that controls where and how the private key is imported.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The rawData parameter is null.-or-The length of the rawData parameter is
    //     0.
    extern public virtual void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags);
    //
    // Summary:
    //     Populates an System.Security.Cryptography.X509Certificates.X509Certificate
    //     object with information from a certificate file, a password, and a key storage
    //     flag.
    //
    // Parameters:
    //   fileName:
    //     The name of a certificate file.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    //   keyStorageFlags:
    //     One of the System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
    //     values that controls where and how to import the private key.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The fileName parameter is null.
    extern public virtual void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags);
    //
    // Summary:
    //     Populates the System.Security.Cryptography.X509Certificates.X509Certificate
    //     object with information from a certificate file, a password, and a System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
    //     value.
    //
    // Parameters:
    //   fileName:
    //     The name of a certificate file represented as a string.
    //
    //   password:
    //     The password required to access the X.509 certificate data.
    //
    //   keyStorageFlags:
    //     One of the System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
    //     values that controls where and how the private key is imported.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The fileName parameter is null.
    extern public virtual void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags);
    //
    // Summary:
    //     Resets the state of the System.Security.Cryptography.X509Certificates.X509Certificate2
    //     object.
    extern public virtual void Reset();
    //
    // Summary:
    //     Returns a string representation of the current System.Security.Cryptography.X509Certificates.X509Certificate
    //     object, with extra information, if specified.
    //
    // Parameters:
    //   fVerbose:
    //     true to produce the verbose form of the string representation; otherwise,
    //     false.
    //
    // Returns:
    //     A string representation of the current System.Security.Cryptography.X509Certificates.X509Certificate
    //     object.
    extern public virtual string ToString(bool fVerbose);
  }
}
