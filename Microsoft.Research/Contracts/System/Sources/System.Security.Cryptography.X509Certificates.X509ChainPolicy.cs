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

// File System.Security.Cryptography.X509Certificates.X509ChainPolicy.cs
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
  sealed public partial class X509ChainPolicy
  {
    #region Methods and constructors
    public void Reset()
    {
    }

    public X509ChainPolicy()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Security.Cryptography.OidCollection ApplicationPolicy
    {
      get
      {
        return default(System.Security.Cryptography.OidCollection);
      }
    }

    public System.Security.Cryptography.OidCollection CertificatePolicy
    {
      get
      {
        return default(System.Security.Cryptography.OidCollection);
      }
    }

    public X509Certificate2Collection ExtraStore
    {
      get
      {
        return default(X509Certificate2Collection);
      }
    }

    public X509RevocationFlag RevocationFlag
    {
      get
      {
        return default(X509RevocationFlag);
      }
      set
      {
      }
    }

    public X509RevocationMode RevocationMode
    {
      get
      {
        return default(X509RevocationMode);
      }
      set
      {
      }
    }

    public TimeSpan UrlRetrievalTimeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public X509VerificationFlags VerificationFlags
    {
      get
      {
        return default(X509VerificationFlags);
      }
      set
      {
      }
    }

    public DateTime VerificationTime
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }
    #endregion
  }
}
