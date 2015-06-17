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

// File System.ServiceModel.Description.ClientCredentials.cs
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


namespace System.ServiceModel.Description
{
  public partial class ClientCredentials : System.ServiceModel.Security.SecurityCredentialsManager, IEndpointBehavior
  {
    #region Methods and constructors
    public virtual new void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, System.ServiceModel.Dispatcher.ClientRuntime behavior)
    {
    }

    public ClientCredentials()
    {
    }

    protected ClientCredentials(System.ServiceModel.Description.ClientCredentials other)
    {
    }

    public System.ServiceModel.Description.ClientCredentials Clone()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Description.ClientCredentials>() != null);

      return default(System.ServiceModel.Description.ClientCredentials);
    }

    protected virtual new System.ServiceModel.Description.ClientCredentials CloneCore()
    {
      return default(System.ServiceModel.Description.ClientCredentials);
    }

    public override System.IdentityModel.Selectors.SecurityTokenManager CreateSecurityTokenManager()
    {
      return default(System.IdentityModel.Selectors.SecurityTokenManager);
    }

#if !NETFRAMEWORK_3_5
    protected internal virtual new System.IdentityModel.Tokens.SecurityToken GetInfoCardSecurityToken(bool requiresInfoCard, System.IdentityModel.Selectors.CardSpacePolicyElement[] chain, System.IdentityModel.Selectors.SecurityTokenSerializer tokenSerializer)
    {
      return default(System.IdentityModel.Tokens.SecurityToken);
    }
#endif

    void System.ServiceModel.Description.IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
    {
    }

    void System.ServiceModel.Description.IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
    {
    }

    void System.ServiceModel.Description.IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
    {
    }
    #endregion

    #region Properties and indexers
    public System.ServiceModel.Security.X509CertificateInitiatorClientCredential ClientCertificate
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.X509CertificateInitiatorClientCredential>() != null);

        return default(System.ServiceModel.Security.X509CertificateInitiatorClientCredential);
      }
    }

    public System.ServiceModel.Security.HttpDigestClientCredential HttpDigest
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.HttpDigestClientCredential>() != null);

        return default(System.ServiceModel.Security.HttpDigestClientCredential);
      }
    }

    public System.ServiceModel.Security.IssuedTokenClientCredential IssuedToken
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.IssuedTokenClientCredential>() != null);

        return default(System.ServiceModel.Security.IssuedTokenClientCredential);
      }
    }

    public System.ServiceModel.Security.PeerCredential Peer
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.PeerCredential>() != null);

        return default(System.ServiceModel.Security.PeerCredential);
      }
    }

    public System.ServiceModel.Security.X509CertificateRecipientClientCredential ServiceCertificate
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.X509CertificateRecipientClientCredential>() != null);

        return default(System.ServiceModel.Security.X509CertificateRecipientClientCredential);
      }
    }

    public bool SupportInteractive
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.ServiceModel.Security.UserNamePasswordClientCredential UserName
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.UserNamePasswordClientCredential>() != null);

        return default(System.ServiceModel.Security.UserNamePasswordClientCredential);
      }
    }

    public System.ServiceModel.Security.WindowsClientCredential Windows
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.WindowsClientCredential>() != null);

        return default(System.ServiceModel.Security.WindowsClientCredential);
      }
    }
    #endregion
  }
}
