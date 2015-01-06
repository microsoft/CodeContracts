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

// File System.ServiceModel.Dispatcher.DuplexChannelBinder.cs
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
  internal partial class DuplexChannelBinder : IChannelBinder
  {
    #region Methods and constructors
    public void Abort()
    {
    }

    public IAsyncResult BeginRequest(System.ServiceModel.Channels.Message message, TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginSend(System.ServiceModel.Channels.Message message, TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public void CloseAfterFault(TimeSpan timeout)
    {
    }

    internal DuplexChannelBinder()
    {
    }

    public System.ServiceModel.Channels.Message EndRequest(IAsyncResult result)
    {
      return default(System.ServiceModel.Channels.Message);
    }

    public void EndSend(IAsyncResult result)
    {
    }

    public bool EndTryReceive(IAsyncResult result, out System.ServiceModel.Channels.RequestContext requestContext)
    {
      requestContext = default(System.ServiceModel.Channels.RequestContext);

      return default(bool);
    }

    public bool EndWaitForMessage(IAsyncResult result)
    {
      return default(bool);
    }

    public System.ServiceModel.Channels.Message Request(System.ServiceModel.Channels.Message message, TimeSpan timeout)
    {
      return default(System.ServiceModel.Channels.Message);
    }

    public void Send(System.ServiceModel.Channels.Message message, TimeSpan timeout)
    {
    }

    public bool TryReceive(TimeSpan timeout, out System.ServiceModel.Channels.RequestContext requestContext)
    {
      requestContext = default(System.ServiceModel.Channels.RequestContext);

      return default(bool);
    }

    public bool WaitForMessage(TimeSpan timeout)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public System.ServiceModel.Channels.IChannel Channel
    {
      get
      {
        return default(System.ServiceModel.Channels.IChannel);
      }
    }

    public bool HasSession
    {
      get
      {
        return default(bool);
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

    public System.ServiceModel.EndpointAddress RemoteAddress
    {
      get
      {
        return default(System.ServiceModel.EndpointAddress);
      }
    }
    #endregion
  }
}
