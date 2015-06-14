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

// File System.Security.Cryptography.X509Certificates.X509Certificate.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Security.Cryptography.X509Certificates
{
  public partial class X509Certificate : System.Runtime.Serialization.IDeserializationCallback, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public static System.Security.Cryptography.X509Certificates.X509Certificate CreateFromCertFile(string filename)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.X509Certificates.X509Certificate>() != null);

      return default(System.Security.Cryptography.X509Certificates.X509Certificate);
    }

    public static System.Security.Cryptography.X509Certificates.X509Certificate CreateFromSignedFile(string filename)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.X509Certificates.X509Certificate>() != null);

      return default(System.Security.Cryptography.X509Certificates.X509Certificate);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public virtual new bool Equals(System.Security.Cryptography.X509Certificates.X509Certificate other)
    {
      return default(bool);
    }

    public virtual new byte[] Export(X509ContentType contentType)
    {
      return default(byte[]);
    }

    public virtual new byte[] Export(X509ContentType contentType, string password)
    {
      return default(byte[]);
    }

    public virtual new byte[] Export(X509ContentType contentType, System.Security.SecureString password)
    {
      return default(byte[]);
    }

    protected static string FormatDate(DateTime date)
    {
      Contract.Ensures(System.Threading.Thread.CurrentThread.CurrentCulture == System.Globalization.CultureInfo.CurrentCulture);

      return default(string);
    }

    public virtual new byte[] GetCertHash()
    {
      return default(byte[]);
    }

    public virtual new string GetCertHashString()
    {
      return default(string);
    }

    public virtual new string GetEffectiveDateString()
    {
      return default(string);
    }

    public virtual new string GetExpirationDateString()
    {
      return default(string);
    }

    public virtual new string GetFormat()
    {
      return default(string);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public virtual new string GetIssuerName()
    {
      return default(string);
    }

    public virtual new string GetKeyAlgorithm()
    {
      return default(string);
    }

    public virtual new byte[] GetKeyAlgorithmParameters()
    {
      return default(byte[]);
    }

    public virtual new string GetKeyAlgorithmParametersString()
    {
      return default(string);
    }

    public virtual new string GetName()
    {
      return default(string);
    }

    public virtual new byte[] GetPublicKey()
    {
      return default(byte[]);
    }

    public virtual new string GetPublicKeyString()
    {
      return default(string);
    }

    public virtual new byte[] GetRawCertData()
    {
      return default(byte[]);
    }

    public virtual new string GetRawCertDataString()
    {
      return default(string);
    }

    public virtual new byte[] GetSerialNumber()
    {
      return default(byte[]);
    }

    public virtual new string GetSerialNumberString()
    {
      return default(string);
    }

    public virtual new void Import(string fileName, System.Security.SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public virtual new void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public virtual new void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public virtual new void Import(byte[] rawData, System.Security.SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public virtual new void Import(byte[] rawData)
    {
    }

    public virtual new void Import(string fileName)
    {
    }

    public virtual new void Reset()
    {
    }

    void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(Object sender)
    {
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public override string ToString()
    {
      return default(string);
    }

    public virtual new string ToString(bool fVerbose)
    {
      return default(string);
    }

    public X509Certificate(byte[] rawData, System.Security.SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public X509Certificate(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public X509Certificate(byte[] rawData, System.Security.SecureString password)
    {
    }

    public X509Certificate()
    {
    }

    public X509Certificate(byte[] data)
    {
    }

    public X509Certificate(byte[] rawData, string password)
    {
    }

    public X509Certificate(IntPtr handle)
    {
    }

    public X509Certificate(string fileName, System.Security.SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public X509Certificate(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
      Contract.Requires(info != null);
    }

    public X509Certificate(System.Security.Cryptography.X509Certificates.X509Certificate cert)
    {
    }

    public X509Certificate(string fileName, string password)
    {
    }

    public X509Certificate(string fileName)
    {
    }

    public X509Certificate(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public X509Certificate(string fileName, System.Security.SecureString password)
    {
    }
    #endregion

    #region Properties and indexers
    public IntPtr Handle
    {
      get
      {
        return default(IntPtr);
      }
    }

    public string Issuer
    {
      get
      {
        return default(string);
      }
    }

    public string Subject
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
