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

// File System.ServiceModel.Channels.CorrelationCallbackMessageProperty.cs
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
  abstract public partial class CorrelationCallbackMessageProperty : IMessageProperty
  {
    #region Methods and constructors
    public void AddData(string name, Func<string> value)
    {
      Contract.Requires(!string.IsNullOrEmpty(name));
    }

    public IAsyncResult BeginFinalizeCorrelation(Message message, TimeSpan timeout, AsyncCallback callback, Object state)
    {
      return default(IAsyncResult);
    }

    protected CorrelationCallbackMessageProperty(ICollection<string> neededData)
    {
    }

    protected CorrelationCallbackMessageProperty(System.ServiceModel.Channels.CorrelationCallbackMessageProperty callback)
    {
    }

    public abstract IMessageProperty CreateCopy();

    public Message EndFinalizeCorrelation(IAsyncResult result)
    {
      return default(Message);
    }

    public Message FinalizeCorrelation(Message message, TimeSpan timeout)
    {
      return default(Message);
    }

    protected abstract IAsyncResult OnBeginFinalizeCorrelation(Message message, TimeSpan timeout, AsyncCallback callback, Object state);

    protected abstract Message OnEndFinalizeCorrelation(IAsyncResult result);

    protected abstract Message OnFinalizeCorrelation(Message message, TimeSpan timeout);

    public static bool TryGet(MessageProperties properties, out System.ServiceModel.Channels.CorrelationCallbackMessageProperty property)
    {
      Contract.Ensures(Contract.Result<bool>() == (Contract.ValueAtReturn(out property) != null));

      property = default(System.ServiceModel.Channels.CorrelationCallbackMessageProperty);

      return default(bool);
    }

    public static bool TryGet(Message message, out System.ServiceModel.Channels.CorrelationCallbackMessageProperty property)
    {
      Contract.Ensures(Contract.Result<bool>() == (Contract.ValueAtReturn(out property) != null));

      property = default(System.ServiceModel.Channels.CorrelationCallbackMessageProperty);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public bool IsFullyDefined
    {
      get
      {
        return default(bool);
      }
    }

    public static string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public IEnumerable<string> NeededData
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerable<string>>() != null);

        return default(IEnumerable<string>);
      }
    }
    #endregion
  }
}

#endif