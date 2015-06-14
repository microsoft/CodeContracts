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

#if !NETFRAMEWORK_4_5
// File System.ServiceModel.Security.Tokens.WrappedKeySecurityToken.cs
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
  public partial class WrappedKeySecurityToken : System.IdentityModel.Tokens.SecurityToken
  {
    #region Methods and constructors
    public override bool CanCreateKeyIdentifierClause<T>()
    {
      return default(bool);
    }

    public override T CreateKeyIdentifierClause<T>()
    {
      return default(T);
    }

    public byte[] GetWrappedKey()
    {
      return default(byte[]);
    }

    public override bool MatchesKeyIdentifierClause(System.IdentityModel.Tokens.SecurityKeyIdentifierClause keyIdentifierClause)
    {
      return default(bool);
    }

    public WrappedKeySecurityToken(string id, byte[] keyToWrap, string wrappingAlgorithm, System.IdentityModel.Tokens.SecurityToken wrappingToken, System.IdentityModel.Tokens.SecurityKeyIdentifier wrappingTokenReference)
    {
    }
    #endregion

    #region Properties and indexers
    public override string Id
    {
      get
      {
        return default(string);
      }
    }

    public override System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Tokens.SecurityKey> SecurityKeys
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Tokens.SecurityKey>);
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

    public string WrappingAlgorithm
    {
      get
      {
        return default(string);
      }
    }

    public System.IdentityModel.Tokens.SecurityToken WrappingToken
    {
      get
      {
        return default(System.IdentityModel.Tokens.SecurityToken);
      }
    }

    public System.IdentityModel.Tokens.SecurityKeyIdentifier WrappingTokenReference
    {
      get
      {
        return default(System.IdentityModel.Tokens.SecurityKeyIdentifier);
      }
    }
    #endregion
  }
}
#endif
