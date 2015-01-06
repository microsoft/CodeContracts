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

// File System.ServiceModel.Channels.SecurityBindingElement.cs
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


namespace System.ServiceModel.Channels
{
  abstract public partial class SecurityBindingElement : BindingElement
  {
    #region Methods and constructors
    public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
    {
      return default(IChannelFactory<TChannel>);
    }

    protected abstract IChannelFactory<TChannel> BuildChannelFactoryCore<TChannel>(BindingContext context);

    public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
    {
      return default(IChannelListener<TChannel>);
    }

    protected abstract IChannelListener<TChannel> BuildChannelListenerCore<TChannel>(BindingContext context);

    public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
    {
      return default(bool);
    }

    public override bool CanBuildChannelListener<TChannel>(BindingContext context)
    {
      return default(bool);
    }

    public static SymmetricSecurityBindingElement CreateAnonymousForCertificateBindingElement()
    {

      return default(SymmetricSecurityBindingElement);
    }

    public static TransportSecurityBindingElement CreateCertificateOverTransportBindingElement()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.TransportSecurityBindingElement>() != null);

      return default(TransportSecurityBindingElement);
    }

    public static TransportSecurityBindingElement CreateCertificateOverTransportBindingElement(System.ServiceModel.MessageSecurityVersion version)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.TransportSecurityBindingElement>() != null);

      return default(TransportSecurityBindingElement);
    }

    public static AsymmetricSecurityBindingElement CreateCertificateSignatureBindingElement()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.AsymmetricSecurityBindingElement>() != null);

      return default(AsymmetricSecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateIssuedTokenBindingElement(System.ServiceModel.Security.Tokens.IssuedSecurityTokenParameters issuedTokenParameters)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SymmetricSecurityBindingElement>() != null);

      return default(SymmetricSecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateIssuedTokenForCertificateBindingElement(System.ServiceModel.Security.Tokens.IssuedSecurityTokenParameters issuedTokenParameters)
    {

      return default(SymmetricSecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateIssuedTokenForSslBindingElement(System.ServiceModel.Security.Tokens.IssuedSecurityTokenParameters issuedTokenParameters, bool requireCancellation)
    {

      return default(SymmetricSecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateIssuedTokenForSslBindingElement(System.ServiceModel.Security.Tokens.IssuedSecurityTokenParameters issuedTokenParameters)
    {

      return default(SymmetricSecurityBindingElement);
    }

    public static TransportSecurityBindingElement CreateIssuedTokenOverTransportBindingElement(System.ServiceModel.Security.Tokens.IssuedSecurityTokenParameters issuedTokenParameters)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.TransportSecurityBindingElement>() != null);

      return default(TransportSecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateKerberosBindingElement()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SymmetricSecurityBindingElement>() != null);

      return default(SymmetricSecurityBindingElement);
    }

    public static TransportSecurityBindingElement CreateKerberosOverTransportBindingElement()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.TransportSecurityBindingElement>() != null);

      return default(TransportSecurityBindingElement);
    }

    public static SecurityBindingElement CreateMutualCertificateBindingElement(System.ServiceModel.MessageSecurityVersion version)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SecurityBindingElement>() != null);

      return default(SecurityBindingElement);
    }

    public static SecurityBindingElement CreateMutualCertificateBindingElement(System.ServiceModel.MessageSecurityVersion version, bool allowSerializedSigningTokenOnReply)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SecurityBindingElement>() != null);

      return default(SecurityBindingElement);
    }

    public static SecurityBindingElement CreateMutualCertificateBindingElement()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SecurityBindingElement>() != null);

      return default(SecurityBindingElement);
    }

    public static AsymmetricSecurityBindingElement CreateMutualCertificateDuplexBindingElement()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.AsymmetricSecurityBindingElement>() != null);

      return default(AsymmetricSecurityBindingElement);
    }

    public static AsymmetricSecurityBindingElement CreateMutualCertificateDuplexBindingElement(System.ServiceModel.MessageSecurityVersion version)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.AsymmetricSecurityBindingElement>() != null);

      return default(AsymmetricSecurityBindingElement);
    }

    public static SecurityBindingElement CreateSecureConversationBindingElement(SecurityBindingElement bootstrapSecurity, bool requireCancellation)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SecurityBindingElement>() != null);

      return default(SecurityBindingElement);
    }

    public static SecurityBindingElement CreateSecureConversationBindingElement(SecurityBindingElement bootstrapSecurity, bool requireCancellation, System.ServiceModel.Security.ChannelProtectionRequirements bootstrapProtectionRequirements)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SecurityBindingElement>() != null);

      return default(SecurityBindingElement);
    }

    public static SecurityBindingElement CreateSecureConversationBindingElement(SecurityBindingElement bootstrapSecurity)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SecurityBindingElement>() != null);

      return default(SecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateSslNegotiationBindingElement(bool requireClientCertificate)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SymmetricSecurityBindingElement>() != null);

      return default(SymmetricSecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateSslNegotiationBindingElement(bool requireClientCertificate, bool requireCancellation)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SymmetricSecurityBindingElement>() != null);

      return default(SymmetricSecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateSspiNegotiationBindingElement(bool requireCancellation)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SymmetricSecurityBindingElement>() != null);

      return default(SymmetricSecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateSspiNegotiationBindingElement()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.SymmetricSecurityBindingElement>() != null);

      return default(SymmetricSecurityBindingElement);
    }

    public static TransportSecurityBindingElement CreateSspiNegotiationOverTransportBindingElement()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.TransportSecurityBindingElement>() != null);

      return default(TransportSecurityBindingElement);
    }

    public static TransportSecurityBindingElement CreateSspiNegotiationOverTransportBindingElement(bool requireCancellation)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.TransportSecurityBindingElement>() != null);

      return default(TransportSecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateUserNameForCertificateBindingElement()
    {
      return default(SymmetricSecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateUserNameForSslBindingElement(bool requireCancellation)
    {
      return default(SymmetricSecurityBindingElement);
    }

    public static SymmetricSecurityBindingElement CreateUserNameForSslBindingElement()
    {
      return default(SymmetricSecurityBindingElement);
    }

    public static TransportSecurityBindingElement CreateUserNameOverTransportBindingElement()
    {
      Contract.Ensures(Contract.Result<TransportSecurityBindingElement>() != null);
      return default(TransportSecurityBindingElement);
    }

    public override T GetProperty<T>(BindingContext context)
    {
      return default(T);
    }

    internal SecurityBindingElement()
    {
    }

    protected static void SetIssuerBindingContextIfRequired(System.ServiceModel.Security.Tokens.SecurityTokenParameters parameters, BindingContext issuerBindingContext)
    {
    }

    public virtual new void SetKeyDerivation(bool requireDerivedKeys)
    {
    }

    #endregion

    #region Properties and indexers
    public bool AllowInsecureTransport
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.ServiceModel.Security.SecurityAlgorithmSuite DefaultAlgorithmSuite
    {
      get
      {
        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
      set
      {
      }
    }

    public bool EnableUnsecuredResponse
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.ServiceModel.Security.Tokens.SupportingTokenParameters EndpointSupportingTokenParameters
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.Tokens.SupportingTokenParameters>() != null);

        return default(System.ServiceModel.Security.Tokens.SupportingTokenParameters);
      }
    }

    public bool IncludeTimestamp
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.ServiceModel.Security.SecurityKeyEntropyMode KeyEntropyMode
    {
      get
      {
        return default(System.ServiceModel.Security.SecurityKeyEntropyMode);
      }
      set
      {
      }
    }

    public LocalClientSecuritySettings LocalClientSettings
    {
      get
      {
        Contract.Ensures(Contract.Result<LocalClientSecuritySettings>() != null);
        return default(LocalClientSecuritySettings);
      }
    }

    public LocalServiceSecuritySettings LocalServiceSettings
    {
      get
      {
        Contract.Ensures(Contract.Result<LocalServiceSecuritySettings>() != null);
        return default(LocalServiceSecuritySettings);
      }
    }

    public System.ServiceModel.MessageSecurityVersion MessageSecurityVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.MessageSecurityVersion>() != null);
        return default(System.ServiceModel.MessageSecurityVersion);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public IDictionary<string, System.ServiceModel.Security.Tokens.SupportingTokenParameters> OperationSupportingTokenParameters
    {
      get
      {
        Contract.Ensures(Contract.Result<IDictionary<string, System.ServiceModel.Security.Tokens.SupportingTokenParameters>>() != null);
        return default(IDictionary<string, System.ServiceModel.Security.Tokens.SupportingTokenParameters>);
      }
    }

    public System.ServiceModel.Security.Tokens.SupportingTokenParameters OptionalEndpointSupportingTokenParameters
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.Tokens.SupportingTokenParameters>() != null);
        return default(System.ServiceModel.Security.Tokens.SupportingTokenParameters);
      }
    }

    public IDictionary<string, System.ServiceModel.Security.Tokens.SupportingTokenParameters> OptionalOperationSupportingTokenParameters
    {
      get
      {
        Contract.Ensures(Contract.Result<IDictionary<string, System.ServiceModel.Security.Tokens.SupportingTokenParameters>>() != null);
        return default(IDictionary<string, System.ServiceModel.Security.Tokens.SupportingTokenParameters>);
      }
    }

    public SecurityHeaderLayout SecurityHeaderLayout
    {
      get
      {
        return default(SecurityHeaderLayout);
      }
      set
      {
      }
    }
    #endregion
  }
}
