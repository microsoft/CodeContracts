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

// File System.ServiceModel.Channels.BindingContext.cs
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
  public partial class BindingContext
  {
    #region Methods and constructors
    public BindingContext(CustomBinding binding, BindingParameterCollection parameters)
    {
    }

    public BindingContext(CustomBinding binding, BindingParameterCollection parameters, Uri listenUriBaseAddress, string listenUriRelativeAddress, System.ServiceModel.Description.ListenUriMode listenUriMode)
    {
    }

    public IChannelFactory<TChannel> BuildInnerChannelFactory<TChannel>()
    {
      return default(IChannelFactory<TChannel>);
    }

    public IChannelListener<TChannel> BuildInnerChannelListener<TChannel>()
    {
      return default(IChannelListener<TChannel>);
    }

    public bool CanBuildInnerChannelFactory<TChannel>()
    {
      return default(bool);
    }

    public bool CanBuildInnerChannelListener<TChannel>()
    {
      return default(bool);
    }

    public System.ServiceModel.Channels.BindingContext Clone()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.BindingContext>() != null);

      return default(System.ServiceModel.Channels.BindingContext);
    }

    public T GetInnerProperty<T>()
    {
      return default(T);
    }
    #endregion

    #region Properties and indexers
    public CustomBinding Binding
    {
      get
      {
        return default(CustomBinding);
      }
    }

    public BindingParameterCollection BindingParameters
    {
      get
      {
        Contract.Ensures(Contract.Result<BindingParameterCollection>() != null);

        return default(BindingParameterCollection);
      }
    }

    public Uri ListenUriBaseAddress
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public System.ServiceModel.Description.ListenUriMode ListenUriMode
    {
      get
      {
        return default(System.ServiceModel.Description.ListenUriMode);
      }
      set
      {
      }
    }

    public string ListenUriRelativeAddress
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public BindingElementCollection RemainingBindingElements
    {
      get
      {
        return default(BindingElementCollection);
      }
    }
    #endregion
  }
}
