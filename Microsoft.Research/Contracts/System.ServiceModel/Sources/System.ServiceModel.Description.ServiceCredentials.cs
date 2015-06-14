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

// File System.ServiceModel.Description.ServiceCredentials.cs
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
  public partial class ServiceCredentials : System.ServiceModel.Security.SecurityCredentialsManager, IServiceBehavior
  {
    #region Methods and constructors
    public System.ServiceModel.Description.ServiceCredentials Clone()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Description.ServiceCredentials>() != null);

      return default(System.ServiceModel.Description.ServiceCredentials);
    }

    protected virtual new System.ServiceModel.Description.ServiceCredentials CloneCore()
    {
      return default(System.ServiceModel.Description.ServiceCredentials);
    }

    public override System.IdentityModel.Selectors.SecurityTokenManager CreateSecurityTokenManager()
    {
      return default(System.IdentityModel.Selectors.SecurityTokenManager);
    }

    protected ServiceCredentials(System.ServiceModel.Description.ServiceCredentials other)
    {
    }

    public ServiceCredentials()
    {
    }

    void System.ServiceModel.Description.IServiceBehavior.AddBindingParameters(ServiceDescription description, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection parameters)
    {
    }

    void System.ServiceModel.Description.IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, System.ServiceModel.ServiceHostBase serviceHostBase)
    {
    }

    void System.ServiceModel.Description.IServiceBehavior.Validate(ServiceDescription description, System.ServiceModel.ServiceHostBase serviceHostBase)
    {
    }
    #endregion

    #region Properties and indexers
    public System.ServiceModel.Security.X509CertificateInitiatorServiceCredential ClientCertificate
    {
      get
      {
        return default(System.ServiceModel.Security.X509CertificateInitiatorServiceCredential);
      }
    }

    public System.ServiceModel.Security.IssuedTokenServiceCredential IssuedTokenAuthentication
    {
      get
      {
        return default(System.ServiceModel.Security.IssuedTokenServiceCredential);
      }
    }

    public System.ServiceModel.Security.PeerCredential Peer
    {
      get
      {
        return default(System.ServiceModel.Security.PeerCredential);
      }
    }

    public System.ServiceModel.Security.SecureConversationServiceCredential SecureConversationAuthentication
    {
      get
      {
        return default(System.ServiceModel.Security.SecureConversationServiceCredential);
      }
    }

    public System.ServiceModel.Security.X509CertificateRecipientServiceCredential ServiceCertificate
    {
      get
      {
        return default(System.ServiceModel.Security.X509CertificateRecipientServiceCredential);
      }
    }

    public System.ServiceModel.Security.UserNamePasswordServiceCredential UserNameAuthentication
    {
      get
      {
        return default(System.ServiceModel.Security.UserNamePasswordServiceCredential);
      }
    }

    public System.ServiceModel.Security.WindowsServiceCredential WindowsAuthentication
    {
      get
      {
        return default(System.ServiceModel.Security.WindowsServiceCredential);
      }
    }
    #endregion
  }
}
