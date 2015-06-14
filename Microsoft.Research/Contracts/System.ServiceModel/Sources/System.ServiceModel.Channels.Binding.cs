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

// File System.ServiceModel.Channels.Binding.cs
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
  abstract public partial class Binding : System.ServiceModel.IDefaultCommunicationTimeouts
  {
    #region Methods and constructors
    protected Binding(string name, string ns)
    {
      Contract.Requires(!string.IsNullOrEmpty(name));
      Contract.Requires(ns != null);
    }

    protected Binding()
    {
    }

    public virtual new IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingParameterCollection parameters)
    {
      return default(IChannelFactory<TChannel>);
    }

    public IChannelFactory<TChannel> BuildChannelFactory<TChannel>(Object[] parameters)
    {
      return default(IChannelFactory<TChannel>);
    }

    public virtual new IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, BindingParameterCollection parameters)
    {
      return default(IChannelListener<TChannel>);
    }

    public virtual new IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingParameterCollection parameters)
    {
      return default(IChannelListener<TChannel>);
    }

    public virtual new IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, System.ServiceModel.Description.ListenUriMode listenUriMode, BindingParameterCollection parameters)
    {
      return default(IChannelListener<TChannel>);
    }

    public virtual new IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, BindingParameterCollection parameters)
    {
      return default(IChannelListener<TChannel>);
    }

    public virtual new IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, Object[] parameters)
    {
      return default(IChannelListener<TChannel>);
    }

    public virtual new IChannelListener<TChannel> BuildChannelListener<TChannel>(Object[] parameters)
    {
      return default(IChannelListener<TChannel>);
    }

    public virtual new IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, System.ServiceModel.Description.ListenUriMode listenUriMode, Object[] parameters)
    {
      return default(IChannelListener<TChannel>);
    }

    public virtual new IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, Object[] parameters)
    {
      return default(IChannelListener<TChannel>);
    }

    public virtual new bool CanBuildChannelFactory<TChannel>(BindingParameterCollection parameters)
    {
      return default(bool);
    }

    public bool CanBuildChannelFactory<TChannel>(Object[] parameters)
    {
      return default(bool);
    }

    public virtual new bool CanBuildChannelListener<TChannel>(BindingParameterCollection parameters)
    {
      return default(bool);
    }

    public bool CanBuildChannelListener<TChannel>(Object[] parameters)
    {
      return default(bool);
    }

    public abstract BindingElementCollection CreateBindingElements();

    public T GetProperty<T>(BindingParameterCollection parameters)
    {
      return default(T);
    }

    [Pure]
    public bool ShouldSerializeName()
    {
      return default(bool);
    }

    [Pure]
    public bool ShouldSerializeNamespace()
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
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

    public MessageVersion MessageVersion
    {
      get
      {
        return default(MessageVersion);
      }
    }

    public string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
        Contract.Requires(!string.IsNullOrEmpty(value));
      }
    }

    public string Namespace
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
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

    public TimeSpan ReceiveTimeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public abstract string Scheme
    {
      get;
    }

    public TimeSpan SendTimeout
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
