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

// File System.ServiceModel.Channels.CommunicationObject.cs
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
  abstract public partial class CommunicationObject : System.ServiceModel.ICommunicationObject
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

    protected CommunicationObject(Object mutex)
    {
    }

    protected CommunicationObject()
    {
    }

    public void EndClose(IAsyncResult result)
    {
    }

    public void EndOpen(IAsyncResult result)
    {
    }

    protected void Fault()
    {
    }

    protected virtual new Type GetCommunicationObjectType()
    {
      return default(Type);
    }

    protected abstract void OnAbort();

    protected abstract IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, Object state);

    protected abstract IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, Object state);

    protected abstract void OnClose(TimeSpan timeout);

    protected virtual new void OnClosed()
    {
    }

    protected virtual new void OnClosing()
    {
    }

    protected abstract void OnEndClose(IAsyncResult result);

    protected abstract void OnEndOpen(IAsyncResult result);

    protected virtual new void OnFaulted()
    {
    }

    protected abstract void OnOpen(TimeSpan timeout);

    protected virtual new void OnOpened()
    {
    }

    protected virtual new void OnOpening()
    {
    }

    public void Open()
    {
    }

    public void Open(TimeSpan timeout)
    {
    }

    protected internal void ThrowIfDisposed()
    {
    }

    protected internal void ThrowIfDisposedOrImmutable()
    {
    }

    protected internal void ThrowIfDisposedOrNotOpen()
    {
    }
    #endregion

    #region Properties and indexers
    protected abstract TimeSpan DefaultCloseTimeout
    {
      get;
    }

    protected abstract TimeSpan DefaultOpenTimeout
    {
      get;
    }

    protected bool IsDisposed
    {
      get
      {
        return default(bool);
      }
    }

    public System.ServiceModel.CommunicationState State
    {
      get
      {
        return default(System.ServiceModel.CommunicationState);
      }
    }

    protected Object ThisLock
    {
      get
      {
        return default(Object);
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
