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

// File System.ServiceModel.Security.WSSecurityTokenSerializer.cs
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


namespace System.ServiceModel.Security
{
  public partial class WSSecurityTokenSerializer : System.IdentityModel.Selectors.SecurityTokenSerializer
  {
    #region Methods and constructors
    protected override bool CanReadKeyIdentifierClauseCore(System.Xml.XmlReader reader)
    {
      return default(bool);
    }

    protected override bool CanReadKeyIdentifierCore(System.Xml.XmlReader reader)
    {
      return default(bool);
    }

    protected override bool CanReadTokenCore(System.Xml.XmlReader reader)
    {
      return default(bool);
    }

    protected override bool CanWriteKeyIdentifierClauseCore(System.IdentityModel.Tokens.SecurityKeyIdentifierClause keyIdentifierClause)
    {
      return default(bool);
    }

    protected override bool CanWriteKeyIdentifierCore(System.IdentityModel.Tokens.SecurityKeyIdentifier keyIdentifier)
    {
      return default(bool);
    }

    protected override bool CanWriteTokenCore(System.IdentityModel.Tokens.SecurityToken token)
    {
      return default(bool);
    }

    public virtual new System.IdentityModel.Tokens.SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXml(System.Xml.XmlElement element, System.ServiceModel.Security.Tokens.SecurityTokenReferenceStyle tokenReferenceStyle)
    {
      return default(System.IdentityModel.Tokens.SecurityKeyIdentifierClause);
    }

    protected internal virtual new string GetTokenTypeUri(Type tokenType)
    {
      return default(string);
    }

    protected override System.IdentityModel.Tokens.SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(System.Xml.XmlReader reader)
    {
      return default(System.IdentityModel.Tokens.SecurityKeyIdentifierClause);
    }

    protected override System.IdentityModel.Tokens.SecurityKeyIdentifier ReadKeyIdentifierCore(System.Xml.XmlReader reader)
    {
      return default(System.IdentityModel.Tokens.SecurityKeyIdentifier);
    }

    protected override System.IdentityModel.Tokens.SecurityToken ReadTokenCore(System.Xml.XmlReader reader, System.IdentityModel.Selectors.SecurityTokenResolver tokenResolver)
    {
      return default(System.IdentityModel.Tokens.SecurityToken);
    }

    public virtual new bool TryCreateKeyIdentifierClauseFromTokenXml(System.Xml.XmlElement element, System.ServiceModel.Security.Tokens.SecurityTokenReferenceStyle tokenReferenceStyle, out System.IdentityModel.Tokens.SecurityKeyIdentifierClause securityKeyIdentifierClause)
    {
      securityKeyIdentifierClause = default(System.IdentityModel.Tokens.SecurityKeyIdentifierClause);

      return default(bool);
    }

    protected override void WriteKeyIdentifierClauseCore(System.Xml.XmlWriter writer, System.IdentityModel.Tokens.SecurityKeyIdentifierClause keyIdentifierClause)
    {
    }

    protected override void WriteKeyIdentifierCore(System.Xml.XmlWriter writer, System.IdentityModel.Tokens.SecurityKeyIdentifier keyIdentifier)
    {
    }

    protected override void WriteTokenCore(System.Xml.XmlWriter writer, System.IdentityModel.Tokens.SecurityToken token)
    {
    }

    public WSSecurityTokenSerializer(SecurityVersion securityVersion)
    {
    }

    public WSSecurityTokenSerializer(SecurityVersion securityVersion, TrustVersion trustVersion, SecureConversationVersion secureConversationVersion, bool emitBspRequiredAttributes, System.IdentityModel.Tokens.SamlSerializer samlSerializer, SecurityStateEncoder securityStateEncoder, IEnumerable<Type> knownTypes, int maximumKeyDerivationOffset, int maximumKeyDerivationLabelLength, int maximumKeyDerivationNonceLength)
    {
    }

    public WSSecurityTokenSerializer()
    {
    }

    public WSSecurityTokenSerializer(bool emitBspRequiredAttributes)
    {
    }

    public WSSecurityTokenSerializer(SecurityVersion securityVersion, bool emitBspRequiredAttributes)
    {
    }

    public WSSecurityTokenSerializer(SecurityVersion securityVersion, bool emitBspRequiredAttributes, System.IdentityModel.Tokens.SamlSerializer samlSerializer, SecurityStateEncoder securityStateEncoder, IEnumerable<Type> knownTypes, int maximumKeyDerivationOffset, int maximumKeyDerivationLabelLength, int maximumKeyDerivationNonceLength)
    {
    }

    public WSSecurityTokenSerializer(SecurityVersion securityVersion, TrustVersion trustVersion, SecureConversationVersion secureConversationVersion, bool emitBspRequiredAttributes, System.IdentityModel.Tokens.SamlSerializer samlSerializer, SecurityStateEncoder securityStateEncoder, IEnumerable<Type> knownTypes)
    {
    }

    public WSSecurityTokenSerializer(SecurityVersion securityVersion, bool emitBspRequiredAttributes, System.IdentityModel.Tokens.SamlSerializer samlSerializer)
    {
    }

    public WSSecurityTokenSerializer(SecurityVersion securityVersion, bool emitBspRequiredAttributes, System.IdentityModel.Tokens.SamlSerializer samlSerializer, SecurityStateEncoder securityStateEncoder, IEnumerable<Type> knownTypes)
    {
    }
    #endregion

    #region Properties and indexers
    public static System.ServiceModel.Security.WSSecurityTokenSerializer DefaultInstance
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Security.WSSecurityTokenSerializer>() != null);

        return default(System.ServiceModel.Security.WSSecurityTokenSerializer);
      }
    }

    public bool EmitBspRequiredAttributes
    {
      get
      {
        return default(bool);
      }
    }

    public int MaximumKeyDerivationLabelLength
    {
      get
      {
        return default(int);
      }
    }

    public int MaximumKeyDerivationNonceLength
    {
      get
      {
        return default(int);
      }
    }

    public int MaximumKeyDerivationOffset
    {
      get
      {
        return default(int);
      }
    }

    public SecurityVersion SecurityVersion
    {
      get
      {
        return default(SecurityVersion);
      }
    }
    #endregion
  }
}
