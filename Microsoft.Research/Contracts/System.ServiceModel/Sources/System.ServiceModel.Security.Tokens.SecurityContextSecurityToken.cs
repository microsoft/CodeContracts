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

// File System.ServiceModel.Security.Tokens.SecurityContextSecurityToken.cs
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
  public partial class SecurityContextSecurityToken : System.IdentityModel.Tokens.SecurityToken, System.ServiceModel.Security.TimeBoundedCache.IExpirableItem, IDisposable
  {
    #region Methods and constructors
    public override bool CanCreateKeyIdentifierClause<T>()
    {
      return default(bool);
    }

    public static System.ServiceModel.Security.Tokens.SecurityContextSecurityToken CreateCookieSecurityContextToken(System.Xml.UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo, System.Xml.UniqueId keyGeneration, DateTime keyEffectiveTime, DateTime keyExpirationTime, System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Policy.IAuthorizationPolicy> authorizationPolicies, System.ServiceModel.Security.SecurityStateEncoder securityStateEncoder)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Security.Tokens.SecurityContextSecurityToken>() != null);

      return default(System.ServiceModel.Security.Tokens.SecurityContextSecurityToken);
    }

    public static System.ServiceModel.Security.Tokens.SecurityContextSecurityToken CreateCookieSecurityContextToken(System.Xml.UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo, System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Policy.IAuthorizationPolicy> authorizationPolicies, System.ServiceModel.Security.SecurityStateEncoder securityStateEncoder)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Security.Tokens.SecurityContextSecurityToken>() != null);

      return default(System.ServiceModel.Security.Tokens.SecurityContextSecurityToken);
    }

    public override T CreateKeyIdentifierClause<T>()
    {
      return default(T);
    }

    public void Dispose()
    {
    }

    public override bool MatchesKeyIdentifierClause(System.IdentityModel.Tokens.SecurityKeyIdentifierClause keyIdentifierClause)
    {
      return default(bool);
    }

    public SecurityContextSecurityToken(System.Xml.UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo, System.Xml.UniqueId keyGeneration, DateTime keyEffectiveTime, DateTime keyExpirationTime, System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Policy.IAuthorizationPolicy> authorizationPolicies)
    {
    }

    public SecurityContextSecurityToken(System.Xml.UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo, System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Policy.IAuthorizationPolicy> authorizationPolicies)
    {
    }

    public SecurityContextSecurityToken(System.Xml.UniqueId contextId, byte[] key, DateTime validFrom, DateTime validTo)
    {
    }

    public SecurityContextSecurityToken(System.Xml.UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo)
    {
    }

    #endregion

    #region Properties and indexers
    public System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Policy.IAuthorizationPolicy> AuthorizationPolicies
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Policy.IAuthorizationPolicy>);
      }
    }

    public System.ServiceModel.Security.SecurityMessageProperty BootstrapMessageProperty
    {
      get
      {
        return default(System.ServiceModel.Security.SecurityMessageProperty);
      }
      set
      {
      }
    }

    public System.Xml.UniqueId ContextId
    {
      get
      {
        return default(System.Xml.UniqueId);
      }
    }

    public override string Id
    {
      get
      {
        return default(string);
      }
    }

    public bool IsCookieMode
    {
      get
      {
        return default(bool);
      }
    }

    public DateTime KeyEffectiveTime
    {
      get
      {
        return default(DateTime);
      }
    }

    public DateTime KeyExpirationTime
    {
      get
      {
        return default(DateTime);
      }
    }

    public System.Xml.UniqueId KeyGeneration
    {
      get
      {
        return default(System.Xml.UniqueId);
      }
    }

    public override System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Tokens.SecurityKey> SecurityKeys
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Tokens.SecurityKey>);
      }
    }

    DateTime System.ServiceModel.Security.TimeBoundedCache.IExpirableItem.ExpirationTime
    {
      get
      {
        return default(DateTime);
      }
    }

    public override DateTime ValidFrom
    {
      get
      {
        return default(DateTime);
      }
    }

    public override DateTime ValidTo
    {
      get
      {
        return default(DateTime);
      }
    }
    #endregion
  }
}
