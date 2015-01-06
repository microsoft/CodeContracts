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

// File System.ServiceModel.Security.Tokens.IssuedSecurityTokenProvider.cs
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
  public partial class IssuedSecurityTokenProvider : System.IdentityModel.Selectors.SecurityTokenProvider, System.ServiceModel.ICommunicationObject
  {
    #region Methods and constructors
    public void Abort()
    {
    }

    public IAsyncResult BeginClose(AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    protected override IAsyncResult BeginGetTokenCore(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginOpen(AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public void Close()
    {
    }

    public void Close(TimeSpan timeout)
    {
    }

    public void Dispose()
    {
    }

    public void EndClose(IAsyncResult result)
    {
    }

    protected override System.IdentityModel.Tokens.SecurityToken EndGetTokenCore(IAsyncResult result)
    {
      return default(System.IdentityModel.Tokens.SecurityToken);
    }

    public void EndOpen(IAsyncResult result)
    {
    }

    protected override System.IdentityModel.Tokens.SecurityToken GetTokenCore(TimeSpan timeout)
    {
      return default(System.IdentityModel.Tokens.SecurityToken);
    }

    public IssuedSecurityTokenProvider()
    {
    }

    public void Open()
    {
    }

    public void Open(TimeSpan timeout)
    {
    }
    #endregion

    #region Properties and indexers
    public bool CacheIssuedTokens
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new TimeSpan DefaultCloseTimeout
    {
      get
      {
        return default(TimeSpan);
      }
    }

    public virtual new TimeSpan DefaultOpenTimeout
    {
      get
      {
        return default(TimeSpan);
      }
    }

    public System.ServiceModel.Security.IdentityVerifier IdentityVerifier
    {
      get
      {
        return default(System.ServiceModel.Security.IdentityVerifier);
      }
      set
      {
      }
    }

    public int IssuedTokenRenewalThresholdPercentage
    {
      get
      {
        return default(int);
      }
      set
      {
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

    public KeyedByTypeCollection<System.ServiceModel.Description.IEndpointBehavior> IssuerChannelBehaviors
    {
      get
      {
        return default(KeyedByTypeCollection<System.ServiceModel.Description.IEndpointBehavior>);
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

    public TimeSpan MaxIssuedTokenCachingTime
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public System.ServiceModel.MessageSecurityVersion MessageSecurityVersion
    {
      get
      {
        return default(System.ServiceModel.MessageSecurityVersion);
      }
      set
      {
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

    public System.IdentityModel.Selectors.SecurityTokenSerializer SecurityTokenSerializer
    {
      get
      {
        return default(System.IdentityModel.Selectors.SecurityTokenSerializer);
      }
      set
      {
      }
    }

    public System.ServiceModel.CommunicationState State
    {
      get
      {
        return default(System.ServiceModel.CommunicationState);
      }
    }

    public override bool SupportsTokenCancellation
    {
      get
      {
        return default(bool);
      }
    }

    public System.ServiceModel.EndpointAddress TargetAddress
    {
      get
      {
        return default(System.ServiceModel.EndpointAddress);
      }
      set
      {
      }
    }

    public System.Collections.ObjectModel.Collection<System.Xml.XmlElement> TokenRequestParameters
    {
      get
      {
        return default(System.Collections.ObjectModel.Collection<System.Xml.XmlElement>);
      }
    }
    #endregion

    #region Events
    public event EventHandler Closed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Closing
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Faulted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Opened
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Opening
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
