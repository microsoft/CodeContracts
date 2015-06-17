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

// File System.ServiceModel.FederatedMessageSecurityOverHttp.cs
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
  sealed public partial class FederatedMessageSecurityOverHttp
  {
    #region Methods and constructors
    public FederatedMessageSecurityOverHttp()
    {
    }

    public bool ShouldSerializeAlgorithmSuite()
    {
      Contract.Ensures(Contract.Result<bool>() == ((this.AlgorithmSuite == System.ServiceModel.Security.SecurityAlgorithmSuite.Default) == false));

      return default(bool);
    }

    public bool ShouldSerializeClaimTypeRequirements()
    {
      Contract.Ensures(Contract.Result<bool>() == (this.ClaimTypeRequirements.Count > 0));

      return default(bool);
    }

    public bool ShouldSerializeEstablishSecurityContext()
    {
      Contract.Ensures(Contract.Result<bool>() == (!this.EstablishSecurityContext));

      return default(bool);
    }

    public bool ShouldSerializeIssuedKeyType()
    {
      Contract.Ensures(Contract.Result<bool>() == ((this.IssuedKeyType == ((System.IdentityModel.Tokens.SecurityKeyType)(0))) == false));

      return default(bool);
    }

    public bool ShouldSerializeNegotiateServiceCredential()
    {
      Contract.Ensures(Contract.Result<bool>() == (!this.NegotiateServiceCredential));

      return default(bool);
    }

    public bool ShouldSerializeTokenRequestParameters()
    {
      Contract.Ensures(Contract.Result<bool>() == (this.TokenRequestParameters.Count > 0));

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public System.ServiceModel.Security.SecurityAlgorithmSuite AlgorithmSuite
    {
      get
      {
        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
      set
      {
      }
    }

    public System.Collections.ObjectModel.Collection<System.ServiceModel.Security.Tokens.ClaimTypeRequirement> ClaimTypeRequirements
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.ServiceModel.Security.Tokens.ClaimTypeRequirement>>() != null);

        return default(System.Collections.ObjectModel.Collection<System.ServiceModel.Security.Tokens.ClaimTypeRequirement>);
      }
    }

    public bool EstablishSecurityContext
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.IdentityModel.Tokens.SecurityKeyType IssuedKeyType
    {
      get
      {
        return default(System.IdentityModel.Tokens.SecurityKeyType);
      }
      set
      {
      }
    }

    public string IssuedTokenType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public EndpointAddress IssuerAddress
    {
      get
      {
        return default(EndpointAddress);
      }
      set
      {
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

    public EndpointAddress IssuerMetadataAddress
    {
      get
      {
        return default(EndpointAddress);
      }
      set
      {
      }
    }

    public bool NegotiateServiceCredential
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Collections.ObjectModel.Collection<System.Xml.XmlElement> TokenRequestParameters
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.Xml.XmlElement>>() != null);

        return default(System.Collections.ObjectModel.Collection<System.Xml.XmlElement>);
      }
    }
    #endregion
  }
}
