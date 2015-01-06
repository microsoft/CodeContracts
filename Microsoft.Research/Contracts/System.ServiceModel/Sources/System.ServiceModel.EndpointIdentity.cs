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

// File System.ServiceModel.EndpointIdentity.cs
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


namespace System.ServiceModel
{
  abstract public partial class EndpointIdentity
  {
    #region Methods and constructors
    public static System.ServiceModel.EndpointIdentity CreateDnsIdentity(string dnsName)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointIdentity>() != null);

      return default(System.ServiceModel.EndpointIdentity);
    }

    public static System.ServiceModel.EndpointIdentity CreateIdentity(System.IdentityModel.Claims.Claim identity)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointIdentity>() != null);

      return default(System.ServiceModel.EndpointIdentity);
    }

    public static System.ServiceModel.EndpointIdentity CreateRsaIdentity(System.Security.Cryptography.X509Certificates.X509Certificate2 certificate)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointIdentity>() != null);

      return default(System.ServiceModel.EndpointIdentity);
    }

    public static System.ServiceModel.EndpointIdentity CreateRsaIdentity(string publicKey)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointIdentity>() != null);

      return default(System.ServiceModel.EndpointIdentity);
    }

    public static System.ServiceModel.EndpointIdentity CreateSpnIdentity(string spnName)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointIdentity>() != null);

      return default(System.ServiceModel.EndpointIdentity);
    }

    public static System.ServiceModel.EndpointIdentity CreateUpnIdentity(string upnName)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointIdentity>() != null);

      return default(System.ServiceModel.EndpointIdentity);
    }

    public static System.ServiceModel.EndpointIdentity CreateX509CertificateIdentity(System.Security.Cryptography.X509Certificates.X509Certificate2 primaryCertificate, System.Security.Cryptography.X509Certificates.X509Certificate2Collection supportingCertificates)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointIdentity>() != null);

      return default(System.ServiceModel.EndpointIdentity);
    }

    public static System.ServiceModel.EndpointIdentity CreateX509CertificateIdentity(System.Security.Cryptography.X509Certificates.X509Certificate2 certificate)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointIdentity>() != null);

      return default(System.ServiceModel.EndpointIdentity);
    }

    protected EndpointIdentity()
    {
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    protected void Initialize(System.IdentityModel.Claims.Claim identityClaim)
    {
    }

    protected void Initialize(System.IdentityModel.Claims.Claim identityClaim, IEqualityComparer<System.IdentityModel.Claims.Claim> claimComparer)
    {
    }

    #endregion

    #region Properties and indexers
    public System.IdentityModel.Claims.Claim IdentityClaim
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IdentityModel.Claims.Claim>() != null);

        return default(System.IdentityModel.Claims.Claim);
      }
    }
    #endregion
  }
}
