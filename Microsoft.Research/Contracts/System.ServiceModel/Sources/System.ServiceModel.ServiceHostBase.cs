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

// File System.ServiceModel.ServiceHostBase.cs
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
  abstract public partial class ServiceHostBase : System.ServiceModel.Channels.CommunicationObject, IExtensibleObject<ServiceHostBase>, IDisposable
  {
    #region Methods and constructors
    protected void AddBaseAddress(Uri baseAddress)
    {
    }

    public virtual new System.Collections.ObjectModel.ReadOnlyCollection<System.ServiceModel.Description.ServiceEndpoint> AddDefaultEndpoints()
    {
      return default(System.Collections.ObjectModel.ReadOnlyCollection<System.ServiceModel.Description.ServiceEndpoint>);
    }

    public System.ServiceModel.Description.ServiceEndpoint AddServiceEndpoint(string implementedContract, System.ServiceModel.Channels.Binding binding, Uri address, Uri listenUri)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Description.ServiceEndpoint>() != null);

      return default(System.ServiceModel.Description.ServiceEndpoint);
    }

    public virtual new void AddServiceEndpoint(System.ServiceModel.Description.ServiceEndpoint endpoint)
    {
    }

    public System.ServiceModel.Description.ServiceEndpoint AddServiceEndpoint(string implementedContract, System.ServiceModel.Channels.Binding binding, Uri address)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Description.ServiceEndpoint>() != null);

      return default(System.ServiceModel.Description.ServiceEndpoint);
    }

    public System.ServiceModel.Description.ServiceEndpoint AddServiceEndpoint(string implementedContract, System.ServiceModel.Channels.Binding binding, string address)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Description.ServiceEndpoint>() != null);

      return default(System.ServiceModel.Description.ServiceEndpoint);
    }

    public System.ServiceModel.Description.ServiceEndpoint AddServiceEndpoint(string implementedContract, System.ServiceModel.Channels.Binding binding, string address, Uri listenUri)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Description.ServiceEndpoint>() != null);

      return default(System.ServiceModel.Description.ServiceEndpoint);
    }

    protected virtual new void ApplyConfiguration()
    {
    }

    protected abstract System.ServiceModel.Description.ServiceDescription CreateDescription(out IDictionary<string, System.ServiceModel.Description.ContractDescription> implementedContracts);

    public int IncrementManualFlowControlLimit(int incrementBy)
    {
      return default(int);
    }

    protected void InitializeDescription(UriSchemeKeyedCollection baseAddresses)
    {
      Contract.Requires(baseAddresses != null);
    }

    protected virtual new void InitializeRuntime()
    {
    }

    protected void LoadConfigurationSection(System.ServiceModel.Configuration.ServiceElement serviceSection)
    {
    }

    protected override void OnAbort()
    {
    }

    protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    protected override void OnClose(TimeSpan timeout)
    {
    }

    protected override void OnClosed()
    {
    }

    protected override void OnEndClose(IAsyncResult result)
    {
    }

    protected override void OnEndOpen(IAsyncResult result)
    {
    }

    protected override void OnOpen(TimeSpan timeout)
    {
    }

    protected override void OnOpened()
    {
    }

    protected void ReleasePerformanceCounters()
    {
    }

    protected ServiceHostBase()
    {
    }

    public void SetEndpointAddress(System.ServiceModel.Description.ServiceEndpoint endpoint, string relativeAddress)
    {
    }

    void System.IDisposable.Dispose()
    {
    }
    #endregion

    #region Properties and indexers
#if !NETFRAMEWORK_3_5

    public System.ServiceModel.Description.ServiceAuthenticationBehavior Authentication
    {
      get
      {
        return default(System.ServiceModel.Description.ServiceAuthenticationBehavior);
      }
    }
#endif
    public System.ServiceModel.Description.ServiceAuthorizationBehavior Authorization
    {
      get
      {
        return default(System.ServiceModel.Description.ServiceAuthorizationBehavior);
      }
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<Uri> BaseAddresses
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<System.Uri>>() != null);

        return default(System.Collections.ObjectModel.ReadOnlyCollection<Uri>);
      }
    }

    public System.ServiceModel.Dispatcher.ChannelDispatcherCollection ChannelDispatchers
    {
      get
      {
        return default(System.ServiceModel.Dispatcher.ChannelDispatcherCollection);
      }
    }

    public TimeSpan CloseTimeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public System.ServiceModel.Description.ServiceCredentials Credentials
    {
      get
      {
        return default(System.ServiceModel.Description.ServiceCredentials);
      }
    }

    protected override TimeSpan DefaultCloseTimeout
    {
      get
      {
        return default(TimeSpan);
      }
    }

    protected override TimeSpan DefaultOpenTimeout
    {
      get
      {
        return default(TimeSpan);
      }
    }

    public System.ServiceModel.Description.ServiceDescription Description
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Description.ServiceDescription>() != null);

        return default(System.ServiceModel.Description.ServiceDescription);
      }
    }

    public IExtensionCollection<System.ServiceModel.ServiceHostBase> Extensions
    {
      get
      {
        return default(IExtensionCollection<System.ServiceModel.ServiceHostBase>);
      }
    }

#if !NETFRAMEWORK_3_5

    internal protected IDictionary<string, System.ServiceModel.Description.ContractDescription> ImplementedContracts
    {
      get
      {
        return default(IDictionary<string, System.ServiceModel.Description.ContractDescription>);
      }
    }

#endif

    public int ManualFlowControlLimit
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public TimeSpan OpenTimeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EventHandler<UnknownMessageReceivedEventArgs> UnknownMessageReceived
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
