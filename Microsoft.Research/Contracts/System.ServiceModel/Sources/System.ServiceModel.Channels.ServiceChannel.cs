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

// File System.ServiceModel.Channels.ServiceChannel.cs
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


namespace System.ServiceModel.Channels
{
  sealed internal partial class ServiceChannel : CommunicationObject, System.ServiceModel.IClientChannel, IDisposable, System.ServiceModel.IDuplexContextChannel, IOutputChannel, IRequestChannel, System.ServiceModel.IServiceChannel, System.ServiceModel.IContextChannel, IChannel, System.ServiceModel.ICommunicationObject, System.ServiceModel.IExtensibleObject<System.ServiceModel.IContextChannel>
  {
    #region Methods and constructors
    public IAsyncResult BeginDisplayInitializationUI(AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginRequest(Message message, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginSend(Message message, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public void DisplayInitializationUI()
    {
    }

    public void EndDisplayInitializationUI(IAsyncResult result)
    {
    }

    public Message EndRequest(IAsyncResult result)
    {
      return default(Message);
    }

    public void EndSend(IAsyncResult result)
    {
    }

    public T GetProperty<T>()
    {
      return default(T);
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

    protected override void OnEndClose(IAsyncResult result)
    {
    }

    protected override void OnEndOpen(IAsyncResult result)
    {
    }

    protected override void OnOpen(TimeSpan timeout)
    {
    }

    public Message Request(Message message)
    {
      return default(Message);
    }

    public Message Request(Message message, TimeSpan timeout)
    {
      return default(Message);
    }

    public void Send(Message message, TimeSpan timeout)
    {
    }

    public void Send(Message message)
    {
    }

    internal ServiceChannel()
    {
    }

    void System.IDisposable.Dispose()
    {
    }

    IAsyncResult System.ServiceModel.IDuplexContextChannel.BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    void System.ServiceModel.IDuplexContextChannel.CloseOutputSession(TimeSpan timeout)
    {
    }

    void System.ServiceModel.IDuplexContextChannel.EndCloseOutputSession(IAsyncResult result)
    {
    }
    #endregion

    #region Properties and indexers
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

    public Uri ListenUri
    {
      get
      {
        return default(Uri);
      }
    }

    public System.ServiceModel.EndpointAddress LocalAddress
    {
      get
      {
        return default(System.ServiceModel.EndpointAddress);
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

    public System.ServiceModel.EndpointAddress RemoteAddress
    {
      get
      {
        return default(System.ServiceModel.EndpointAddress);
      }
    }

    bool System.ServiceModel.IClientChannel.AllowInitializationUI
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    bool System.ServiceModel.IClientChannel.DidInteractiveInitialization
    {
      get
      {
        return default(bool);
      }
    }

    bool System.ServiceModel.IContextChannel.AllowOutputBatching
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    IInputSession System.ServiceModel.IContextChannel.InputSession
    {
      get
      {
        return default(IInputSession);
      }
    }

    IOutputSession System.ServiceModel.IContextChannel.OutputSession
    {
      get
      {
        return default(IOutputSession);
      }
    }

    string System.ServiceModel.IContextChannel.SessionId
    {
      get
      {
        return default(string);
      }
    }

    bool System.ServiceModel.IDuplexContextChannel.AutomaticInputSessionShutdown
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    System.ServiceModel.InstanceContext System.ServiceModel.IDuplexContextChannel.CallbackInstance
    {
      get
      {
        return default(System.ServiceModel.InstanceContext);
      }
      set
      {
      }
    }

    System.ServiceModel.IExtensionCollection<System.ServiceModel.IContextChannel> System.ServiceModel.IExtensibleObject<System.ServiceModel.IContextChannel>.Extensions
    {
      get
      {
        return default(System.ServiceModel.IExtensionCollection<System.ServiceModel.IContextChannel>);
      }
    }

    public Uri Via
    {
      get
      {
        return default(Uri);
      }
    }
    #endregion

  }
}
