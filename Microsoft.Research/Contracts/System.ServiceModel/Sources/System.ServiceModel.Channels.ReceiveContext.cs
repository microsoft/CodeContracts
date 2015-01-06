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

// File System.ServiceModel.Channels.ReceiveContext.cs
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

#if !NETFRAMEWORK_3_5

namespace System.ServiceModel.Channels
{
  abstract public partial class ReceiveContext
  {
    #region Methods and constructors
    public virtual new void Abandon(Exception exception, TimeSpan timeout)
    {
    }

    public virtual new void Abandon(TimeSpan timeout)
    {
    }

    public virtual new IAsyncResult BeginAbandon(Exception exception, TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginAbandon(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginComplete(TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    public virtual new void Complete(TimeSpan timeout)
    {
    }

    public virtual new void EndAbandon(IAsyncResult result)
    {
    }

    public virtual new void EndComplete(IAsyncResult result)
    {
    }

    protected internal virtual new void Fault()
    {
    }

    protected virtual new void OnAbandon(Exception exception, TimeSpan timeout)
    {
    }

    protected abstract void OnAbandon(TimeSpan timeout);

    protected virtual new IAsyncResult OnBeginAbandon(Exception exception, TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    protected abstract IAsyncResult OnBeginAbandon(TimeSpan timeout, AsyncCallback callback, Object state);

    protected abstract IAsyncResult OnBeginComplete(TimeSpan timeout, AsyncCallback callback, Object state);

    protected abstract void OnComplete(TimeSpan timeout);

    protected abstract void OnEndAbandon(IAsyncResult result);

    protected abstract void OnEndComplete(IAsyncResult result);

    protected virtual new void OnFaulted()
    {
    }

    protected ReceiveContext()
    {
    }

    public static bool TryGet(MessageProperties properties, out System.ServiceModel.Channels.ReceiveContext property)
    {
      property = default(System.ServiceModel.Channels.ReceiveContext);

      return default(bool);
    }

    public static bool TryGet(Message message, out System.ServiceModel.Channels.ReceiveContext property)
    {
      property = default(System.ServiceModel.Channels.ReceiveContext);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public ReceiveContextState State
    {
      get
      {
        return default(ReceiveContextState);
      }
      protected set
      {
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
    public event EventHandler Faulted
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public readonly static string Name;
    #endregion
  }
}

#endif