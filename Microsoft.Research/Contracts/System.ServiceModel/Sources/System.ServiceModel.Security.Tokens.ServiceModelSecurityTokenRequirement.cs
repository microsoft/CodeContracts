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

// File System.ServiceModel.Security.Tokens.ServiceModelSecurityTokenRequirement.cs
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


namespace System.ServiceModel.Security.Tokens
{
  abstract public partial class ServiceModelSecurityTokenRequirement : System.IdentityModel.Selectors.SecurityTokenRequirement
  {
    #region Methods and constructors
    protected ServiceModelSecurityTokenRequirement()
    {
    }
    #endregion

    #region Properties and indexers
    public static string AuditLogLocationProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string ChannelParametersCollectionProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string DuplexClientLocalAddressProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string EndpointFilterTableProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

#if !NETFRAMEWORK_3_5

    public static string ExtendedProtectionPolicy
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
#endif
    public static string HttpAuthenticationSchemeProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public bool IsInitiator
    {
      get
      {

        return default(bool);
      }
    }

    public static string IsInitiatorProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string IsOutOfBandTokenProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string IssuedSecurityTokenParametersProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public System.ServiceModel.EndpointAddress IssuerAddress
    {
      get
      {

        return default(System.ServiceModel.EndpointAddress);
      }
      set
      {
      }
    }

    public static string IssuerAddressProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public System.ServiceModel.Channels.Binding IssuerBinding
    {
      get
      {

        return default(System.ServiceModel.Channels.Binding);
      }
      set
      {
      }
    }

    public static string IssuerBindingContextProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string IssuerBindingProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string ListenUriProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string MessageAuthenticationAuditLevelProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string MessageDirectionProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public System.IdentityModel.Selectors.SecurityTokenVersion MessageSecurityVersion
    {
      get
      {
        return default(System.IdentityModel.Selectors.SecurityTokenVersion);
      }
      set
      {
      }
    }

    public static string MessageSecurityVersionProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string PrivacyNoticeUriProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string PrivacyNoticeVersionProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public System.ServiceModel.Channels.SecurityBindingElement SecureConversationSecurityBindingElement
    {
      get
      {
        return default(System.ServiceModel.Channels.SecurityBindingElement);
      }
      set
      {
      }
    }

    public static string SecureConversationSecurityBindingElementProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public System.ServiceModel.Security.SecurityAlgorithmSuite SecurityAlgorithmSuite
    {
      get
      {
        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
      set
      {
      }
    }

    public static string SecurityAlgorithmSuiteProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public System.ServiceModel.Channels.SecurityBindingElement SecurityBindingElement
    {
      get
      {

        return default(System.ServiceModel.Channels.SecurityBindingElement);
      }
      set
      {
      }
    }

    public static string SecurityBindingElementProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string SupportingTokenAttachmentModeProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string SupportSecurityContextCancellationProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string SuppressAuditFailureProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string TargetAddressProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public string TransportScheme
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public static string TransportSchemeProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public static string ViaProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
    #endregion

    #region Fields
    #endregion
  }
}
