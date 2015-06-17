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

// File System.ServiceModel.Security.SecurityMessageProperty.cs
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
  public partial class SecurityMessageProperty : System.ServiceModel.Channels.IMessageProperty, IDisposable
  {
    #region Methods and constructors
    public System.ServiceModel.Channels.IMessageProperty CreateCopy()
    {
      return default(System.ServiceModel.Channels.IMessageProperty);
    }

    public void Dispose()
    {
    }

    public static System.ServiceModel.Security.SecurityMessageProperty GetOrCreate(System.ServiceModel.Channels.Message message)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Security.SecurityMessageProperty>() != null);

      return default(System.ServiceModel.Security.SecurityMessageProperty);
    }

    public SecurityMessageProperty()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Policy.IAuthorizationPolicy> ExternalAuthorizationPolicies
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Policy.IAuthorizationPolicy>);
      }
      set
      {
      }
    }

    public bool HasIncomingSupportingTokens
    {
      get
      {
        return default(bool);
      }
    }

    public System.Collections.ObjectModel.Collection<SupportingTokenSpecification> IncomingSupportingTokens
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.ServiceModel.Security.SupportingTokenSpecification>>() != null);

        return default(System.Collections.ObjectModel.Collection<SupportingTokenSpecification>);
      }
    }

    public SecurityTokenSpecification InitiatorToken
    {
      get
      {
        return default(SecurityTokenSpecification);
      }
      set
      {
      }
    }

    public System.Collections.ObjectModel.Collection<SupportingTokenSpecification> OutgoingSupportingTokens
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.ServiceModel.Security.SupportingTokenSpecification>>() != null);

        return default(System.Collections.ObjectModel.Collection<SupportingTokenSpecification>);
      }
    }

    public SecurityTokenSpecification ProtectionToken
    {
      get
      {
        return default(SecurityTokenSpecification);
      }
      set
      {
      }
    }

    public SecurityTokenSpecification RecipientToken
    {
      get
      {
        return default(SecurityTokenSpecification);
      }
      set
      {
      }
    }

    public string SenderIdPrefix
    {
      get
      {
        return default(string);
      }
      set
      {
        Contract.Requires(!String.IsNullOrEmpty(value));
      }
    }

    public System.ServiceModel.ServiceSecurityContext ServiceSecurityContext
    {
      get
      {
        return default(System.ServiceModel.ServiceSecurityContext);
      }
      set
      {
      }
    }

    public SecurityTokenSpecification TransportToken
    {
      get
      {
        return default(SecurityTokenSpecification);
      }
      set
      {
      }
    }
    #endregion
  }
}
