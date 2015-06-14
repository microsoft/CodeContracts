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

// File System.ServiceModel.Dispatcher.ChannelDispatcher.cs
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


namespace System.ServiceModel.Dispatcher
{
  public partial class ChannelDispatcher : ChannelDispatcherBase
  {
    #region Methods and constructors
    protected override void Attach(System.ServiceModel.ServiceHostBase host)
    {
    }

    public ChannelDispatcher(System.ServiceModel.Channels.IChannelListener listener)
    {
    }

    public ChannelDispatcher(System.ServiceModel.Channels.IChannelListener listener, string bindingName)
    {
    }

    public ChannelDispatcher(System.ServiceModel.Channels.IChannelListener listener, string bindingName, System.ServiceModel.IDefaultCommunicationTimeouts timeouts)
    {
    }

    public override void CloseInput()
    {
    }

    protected override void Detach(System.ServiceModel.ServiceHostBase host)
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

    protected override void OnOpening()
    {
    }
    #endregion

    #region Properties and indexers
    public string BindingName
    {
      get
      {
        return default(string);
      }
    }

    public SynchronizedCollection<IChannelInitializer> ChannelInitializers
    {
      get
      {
        return default(SynchronizedCollection<IChannelInitializer>);
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

    public SynchronizedCollection<EndpointDispatcher> Endpoints
    {
      get
      {
        Contract.Ensures(Contract.Result<SynchronizedCollection<EndpointDispatcher>>() != null);
        return default(SynchronizedCollection<EndpointDispatcher>);
      }
    }

    public System.Collections.ObjectModel.Collection<IErrorHandler> ErrorHandlers
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<IErrorHandler>>() != null);
        return default(System.Collections.ObjectModel.Collection<IErrorHandler>);
      }
    }

    public override System.ServiceModel.ServiceHostBase Host
    {
      get
      {
        return default(System.ServiceModel.ServiceHostBase);
      }
    }

    public bool IncludeExceptionDetailInFaults
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsTransactedAccept
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsTransactedReceive
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override System.ServiceModel.Channels.IChannelListener Listener
    {
      get
      {
        return default(System.ServiceModel.Channels.IChannelListener);
      }
    }

    public bool ManualAddressing
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int MaxPendingReceives
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MaxTransactedBatchSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public System.ServiceModel.Channels.MessageVersion MessageVersion
    {
      get
      {
        return default(System.ServiceModel.Channels.MessageVersion);
      }
      set
      {
      }
    }

    public bool ReceiveContextEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool ReceiveSynchronously
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool SendAsynchronously
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ServiceThrottle ServiceThrottle
    {
      get
      {
        return default(ServiceThrottle);
      }
      set
      {
      }
    }

    public System.Transactions.IsolationLevel TransactionIsolationLevel
    {
      get
      {
        return default(System.Transactions.IsolationLevel);
      }
      set
      {
      }
    }

    public TimeSpan TransactionTimeout
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
  }
}
