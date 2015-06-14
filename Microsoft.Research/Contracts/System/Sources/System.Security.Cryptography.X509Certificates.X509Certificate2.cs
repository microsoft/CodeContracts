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

// File System.Security.Cryptography.X509Certificates.X509Certificate2.cs
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
  public partial class X509Certificate2 : X509Certificate
  {
    #region Methods and constructors
    public static X509ContentType GetCertContentType(byte[] rawData)
    {
      return default(X509ContentType);
    }

    public static X509ContentType GetCertContentType(string fileName)
    {
      return default(X509ContentType);
    }

    public string GetNameInfo(X509NameType nameType, bool forIssuer)
    {
      return default(string);
    }

    public override void Import(string fileName, System.Security.SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public override void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public override void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public override void Import(byte[] rawData, System.Security.SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public override void Import(byte[] rawData)
    {
    }

    public override void Import(string fileName)
    {
    }

    public override void Reset()
    {
    }

    public override string ToString(bool verbose)
    {
      return default(string);
    }

    public override string ToString()
    {
      return default(string);
    }

    public bool Verify()
    {
      return default(bool);
    }

    public X509Certificate2(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public X509Certificate2(byte[] rawData, System.Security.SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public X509Certificate2(string fileName)
    {
    }

    public X509Certificate2(byte[] rawData, System.Security.SecureString password)
    {
    }

    public X509Certificate2()
    {
    }

    public X509Certificate2(byte[] rawData)
    {
    }

    public X509Certificate2(byte[] rawData, string password)
    {
    }

    public X509Certificate2(IntPtr handle)
    {
    }

    public X509Certificate2(X509Certificate certificate)
    {
    }

    protected X509Certificate2(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public X509Certificate2(string fileName, System.Security.SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public X509Certificate2(string fileName, string password)
    {
    }

    public X509Certificate2(string fileName, System.Security.SecureString password)
    {
    }

    public X509Certificate2(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
    {
    }
    #endregion

    #region Properties and indexers
    public bool Archived
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public X509ExtensionCollection Extensions
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Security.Cryptography.X509Certificates.X509ExtensionCollection>() != null);

        return default(X509ExtensionCollection);
      }
    }

    public string FriendlyName
    {
      get
      {
        return default(string);
      }
      set
      {
        Contract.Ensures(0 <= value.Length);
        Contract.Ensures(value.Length <= 1073741822);
      }
    }

    public bool HasPrivateKey
    {
      get
      {
        return default(bool);
      }
    }

    public X500DistinguishedName IssuerName
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Security.Cryptography.X509Certificates.X500DistinguishedName>() != null);

        return default(X500DistinguishedName);
      }
    }

    public DateTime NotAfter
    {
      get
      {
        return default(DateTime);
      }
    }

    public DateTime NotBefore
    {
      get
      {
        return default(DateTime);
      }
    }

    public System.Security.Cryptography.AsymmetricAlgorithm PrivateKey
    {
      get
      {
        return default(System.Security.Cryptography.AsymmetricAlgorithm);
      }
      set
      {
      }
    }

    public PublicKey PublicKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Security.Cryptography.X509Certificates.PublicKey>() != null);

        return default(PublicKey);
      }
    }

    public byte[] RawData
    {
      get
      {
        return default(byte[]);
      }
    }

    public string SerialNumber
    {
      get
      {
        return default(string);
      }
    }

    public System.Security.Cryptography.Oid SignatureAlgorithm
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Security.Cryptography.Oid>() != null);

        return default(System.Security.Cryptography.Oid);
      }
    }

    public X500DistinguishedName SubjectName
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Security.Cryptography.X509Certificates.X500DistinguishedName>() != null);

        return default(X500DistinguishedName);
      }
    }

    public string Thumbprint
    {
      get
      {
        return default(string);
      }
    }

    public int Version
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
