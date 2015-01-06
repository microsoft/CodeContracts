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

// File System.ServiceModel.Description.MetadataExchangeClient.cs
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
  public partial class MetadataExchangeClient
  {
    #region Methods and constructors
    public IAsyncResult BeginGetMetadata(System.ServiceModel.EndpointAddress address, AsyncCallback callback, Object asyncState)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginGetMetadata(Uri address, MetadataExchangeClientMode mode, AsyncCallback callback, Object asyncState)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public IAsyncResult BeginGetMetadata(AsyncCallback callback, Object asyncState)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public MetadataSet EndGetMetadata(IAsyncResult result)
    {
      return default(MetadataSet);
    }

    protected internal virtual new System.ServiceModel.ChannelFactory<IMetadataExchange> GetChannelFactory(System.ServiceModel.EndpointAddress metadataAddress, string dialect, string identifier)
    {
      return default(System.ServiceModel.ChannelFactory<IMetadataExchange>);
    }

    public MetadataSet GetMetadata(System.ServiceModel.EndpointAddress address, Uri via)
    {
      return default(MetadataSet);
    }

    public MetadataSet GetMetadata(System.ServiceModel.EndpointAddress address)
    {
      return default(MetadataSet);
    }

    public MetadataSet GetMetadata(Uri address, MetadataExchangeClientMode mode)
    {
      return default(MetadataSet);
    }

    public MetadataSet GetMetadata()
    {
      return default(MetadataSet);
    }

    protected internal virtual new System.Net.HttpWebRequest GetWebRequest(Uri location, string dialect, string identifier)
    {
      Contract.Requires(location != null);

      return default(System.Net.HttpWebRequest);
    }

    public MetadataExchangeClient(Uri address, MetadataExchangeClientMode mode)
    {
      Contract.Requires(address != null);
    }

    public MetadataExchangeClient(System.ServiceModel.EndpointAddress address)
    {
      Contract.Requires(address != null);
    }

    public MetadataExchangeClient(string endpointConfigurationName)
    {
    }

    public MetadataExchangeClient()
    {
    }

    public MetadataExchangeClient(System.ServiceModel.Channels.Binding mexBinding)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Net.ICredentials HttpCredentials
    {
      get
      {
        return default(System.Net.ICredentials);
      }
      set
      {
      }
    }

    public int MaximumResolvedReferences
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public TimeSpan OperationTimeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public bool ResolveMetadataReferences
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ClientCredentials SoapCredentials
    {
      get
      {
        return default(ClientCredentials);
      }
      set
      {
      }
    }
    #endregion
  }
}
