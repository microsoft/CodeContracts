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

// File System.ServiceModel.ClientBase_1.cs
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
  abstract public partial class ClientBase<TChannel> : ICommunicationObject, IDisposable
  {
    #region Delegates
    protected delegate IAsyncResult BeginOperationDelegate(Object[] inValues, AsyncCallback asyncCallback, Object state);

    protected delegate Object[] EndOperationDelegate(IAsyncResult result);
    #endregion

    #region Methods and constructors
    public void Abort()
    {
    }

    protected ClientBase(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
    {
    }

    protected ClientBase(InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress)
    {
    }

    protected ClientBase(InstanceContext callbackInstance, System.ServiceModel.Description.ServiceEndpoint endpoint)
    {
    }

    protected ClientBase(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
    {
    }

    protected ClientBase(string endpointConfigurationName, EndpointAddress remoteAddress)
    {
    }

    protected ClientBase()
    {
    }

    protected ClientBase(string endpointConfigurationName, string remoteAddress)
    {
    }

    protected ClientBase(string endpointConfigurationName)
    {
    }

    protected ClientBase(InstanceContext callbackInstance)
    {
    }

    protected ClientBase(InstanceContext callbackInstance, string endpointConfigurationName)
    {
    }

    protected ClientBase(System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress)
    {
    }

    protected ClientBase(System.ServiceModel.Description.ServiceEndpoint endpoint)
    {
    }

    public void Close()
    {
    }

    protected virtual new TChannel CreateChannel()
    {
      return default(TChannel);
    }

    public void DisplayInitializationUI()
    {
    }

    protected T GetDefaultValueForInitialization<T>()
    {
      return default(T);
    }

    protected void InvokeAsync(System.ServiceModel.ClientBase<TChannel>.BeginOperationDelegate beginOperationDelegate, Object[] inValues, System.ServiceModel.ClientBase<TChannel>.EndOperationDelegate endOperationDelegate, System.Threading.SendOrPostCallback operationCompletedCallback, Object userState)
    {
    }

    public void Open()
    {
    }

    void System.IDisposable.Dispose()
    {
    }

    IAsyncResult System.ServiceModel.ICommunicationObject.BeginClose(AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    IAsyncResult System.ServiceModel.ICommunicationObject.BeginClose(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    IAsyncResult System.ServiceModel.ICommunicationObject.BeginOpen(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    IAsyncResult System.ServiceModel.ICommunicationObject.BeginOpen(AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    void System.ServiceModel.ICommunicationObject.Close(TimeSpan timeout)
    {
    }

    void System.ServiceModel.ICommunicationObject.EndClose(IAsyncResult result)
    {
    }

    void System.ServiceModel.ICommunicationObject.EndOpen(IAsyncResult result)
    {
    }

    void System.ServiceModel.ICommunicationObject.Open(TimeSpan timeout)
    {
    }
    #endregion

    #region Properties and indexers
    protected TChannel Channel
    {
      get
      {
        Contract.Ensures(Contract.Result<TChannel>() != null);
        return default(TChannel);
      }
    }

    public ChannelFactory<TChannel> ChannelFactory
    {
      get
      {
        return default(ChannelFactory<TChannel>);
      }
    }

    public System.ServiceModel.Description.ClientCredentials ClientCredentials
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Description.ClientCredentials>() == this.ChannelFactory.Credentials);

        return default(System.ServiceModel.Description.ClientCredentials);
      }
    }

    public System.ServiceModel.Description.ServiceEndpoint Endpoint
    {
      get
      {
        return default(System.ServiceModel.Description.ServiceEndpoint);
      }
    }

    public IClientChannel InnerChannel
    {
      get
      {
        Contract.Ensures(Contract.Result<IClientChannel>() != null);

        return default(IClientChannel);
      }
    }

    public CommunicationState State
    {
      get
      {
        return default(CommunicationState);
      }
    }
    #endregion

    #region Events
    event EventHandler System.ServiceModel.ICommunicationObject.Closed
    {
      add
      {
      }
      remove
      {
      }
    }

    event EventHandler System.ServiceModel.ICommunicationObject.Closing
    {
      add
      {
      }
      remove
      {
      }
    }

    event EventHandler System.ServiceModel.ICommunicationObject.Faulted
    {
      add
      {
      }
      remove
      {
      }
    }

    event EventHandler System.ServiceModel.ICommunicationObject.Opened
    {
      add
      {
      }
      remove
      {
      }
    }

    event EventHandler System.ServiceModel.ICommunicationObject.Opening
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
