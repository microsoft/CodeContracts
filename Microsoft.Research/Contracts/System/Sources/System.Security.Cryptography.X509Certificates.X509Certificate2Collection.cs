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

// File System.Security.Cryptography.X509Certificates.X509Certificate2Collection.cs
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
  public partial class X509Certificate2Collection : X509CertificateCollection
  {
    #region Methods and constructors
    public int Add(X509Certificate2 certificate)
    {
      return default(int);
    }

    public void AddRange(X509Certificate2[] certificates)
    {
    }

    public void AddRange(System.Security.Cryptography.X509Certificates.X509Certificate2Collection certificates)
    {
    }

    public bool Contains(X509Certificate2 certificate)
    {
      return default(bool);
    }

    public byte[] Export(X509ContentType contentType)
    {
      return default(byte[]);
    }

    public byte[] Export(X509ContentType contentType, string password)
    {
      return default(byte[]);
    }

    public System.Security.Cryptography.X509Certificates.X509Certificate2Collection Find(X509FindType findType, Object findValue, bool validOnly)
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.X509Certificates.X509Certificate2Collection>() != null);

      return default(System.Security.Cryptography.X509Certificates.X509Certificate2Collection);
    }

    public X509Certificate2Enumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<System.Security.Cryptography.X509Certificates.X509Certificate2Enumerator>() != null);

      return default(X509Certificate2Enumerator);
    }

    public void Import(byte[] rawData)
    {
    }

    public void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public void Import(string fileName)
    {
    }

    public void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
    {
    }

    public void Insert(int index, X509Certificate2 certificate)
    {
    }

    public void Remove(X509Certificate2 certificate)
    {
    }

    public void RemoveRange(System.Security.Cryptography.X509Certificates.X509Certificate2Collection certificates)
    {
    }

    public void RemoveRange(X509Certificate2[] certificates)
    {
    }

    public X509Certificate2Collection(X509Certificate2 certificate)
    {
    }

    public X509Certificate2Collection()
    {
    }

    public X509Certificate2Collection(X509Certificate2[] certificates)
    {
    }

    public X509Certificate2Collection(System.Security.Cryptography.X509Certificates.X509Certificate2Collection certificates)
    {
    }
    #endregion

    #region Properties and indexers
    public X509Certificate2 this [int index]
    {
      get
      {
        return default(X509Certificate2);
      }
      set
      {
      }
    }
    #endregion
  }
}
