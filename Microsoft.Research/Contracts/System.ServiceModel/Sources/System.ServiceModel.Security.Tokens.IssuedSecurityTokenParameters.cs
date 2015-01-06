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

// File System.ServiceModel.Security.Tokens.IssuedSecurityTokenParameters.cs
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
  public partial class IssuedSecurityTokenParameters : SecurityTokenParameters
  {
    #region Methods and constructors
    protected override SecurityTokenParameters CloneCore()
    {
      return default(SecurityTokenParameters);
    }

    protected internal override System.IdentityModel.Tokens.SecurityKeyIdentifierClause CreateKeyIdentifierClause(System.IdentityModel.Tokens.SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
    {
      return default(System.IdentityModel.Tokens.SecurityKeyIdentifierClause);
    }

    public System.Collections.ObjectModel.Collection<System.Xml.XmlElement> CreateRequestParameters(System.ServiceModel.MessageSecurityVersion messageSecurityVersion, System.IdentityModel.Selectors.SecurityTokenSerializer securityTokenSerializer)
    {
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.Xml.XmlElement>>() != null);

      return default(System.Collections.ObjectModel.Collection<System.Xml.XmlElement>);
    }

    protected internal override void InitializeSecurityTokenRequirement(System.IdentityModel.Selectors.SecurityTokenRequirement requirement)
    {
    }

    protected IssuedSecurityTokenParameters(IssuedSecurityTokenParameters other)
    {
      Contract.Requires(other != null);
    }

    public IssuedSecurityTokenParameters(string tokenType, System.ServiceModel.EndpointAddress issuerAddress)
    {
    }

    public IssuedSecurityTokenParameters(string tokenType)
    {
    }

    public IssuedSecurityTokenParameters()
    {
    }

    public IssuedSecurityTokenParameters(string tokenType, System.ServiceModel.EndpointAddress issuerAddress, System.ServiceModel.Channels.Binding issuerBinding)
    {
    }

    #endregion

    #region Properties and indexers
    public System.Collections.ObjectModel.Collection<System.Xml.XmlElement> AdditionalRequestParameters
    {
      get
      {
        return default(System.Collections.ObjectModel.Collection<System.Xml.XmlElement>);
      }
    }

    public System.Collections.ObjectModel.Collection<ClaimTypeRequirement> ClaimTypeRequirements
    {
      get
      {
        return default(System.Collections.ObjectModel.Collection<ClaimTypeRequirement>);
      }
    }

    public System.ServiceModel.MessageSecurityVersion DefaultMessageSecurityVersion
    {
      get
      {
        return default(System.ServiceModel.MessageSecurityVersion);
      }
      set
      {
      }
    }

    internal protected override bool HasAsymmetricKey
    {
      get
      {
        return default(bool);
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

    public System.ServiceModel.EndpointAddress IssuerMetadataAddress
    {
      get
      {
        return default(System.ServiceModel.EndpointAddress);
      }
      set
      {
      }
    }

    public int KeySize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public System.IdentityModel.Tokens.SecurityKeyType KeyType
    {
      get
      {
        return default(System.IdentityModel.Tokens.SecurityKeyType);
      }
      set
      {
      }
    }

    internal protected override bool SupportsClientAuthentication
    {
      get
      {
        return default(bool);
      }
    }

    internal protected override bool SupportsClientWindowsIdentity
    {
      get
      {
        return default(bool);
      }
    }

    internal protected override bool SupportsServerAuthentication
    {
      get
      {
        return default(bool);
      }
    }

    public string TokenType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion
  }
}
