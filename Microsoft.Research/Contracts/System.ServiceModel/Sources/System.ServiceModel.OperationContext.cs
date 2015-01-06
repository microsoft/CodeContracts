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

// File System.ServiceModel.OperationContext.cs
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
  sealed public partial class OperationContext : IExtensibleObject<OperationContext>
  {
    #region Methods and constructors
    public T GetCallbackChannel<T>()
    {
      return default(T);
    }

    public OperationContext(IContextChannel channel)
    {
    }

    public void SetTransactionComplete()
    {
    }
    #endregion

    #region Properties and indexers
    public IContextChannel Channel
    {
      get
      {
        return default(IContextChannel);
      }
    }

    public static System.ServiceModel.OperationContext Current
    {
      get
      {
        return default(System.ServiceModel.OperationContext);
      }
      set
      {
      }
    }

    public System.ServiceModel.Dispatcher.EndpointDispatcher EndpointDispatcher
    {
      get
      {
        return default(System.ServiceModel.Dispatcher.EndpointDispatcher);
      }
      set
      {
      }
    }

    public IExtensionCollection<System.ServiceModel.OperationContext> Extensions
    {
      get
      {
        Contract.Ensures(Contract.Result<IExtensionCollection<System.ServiceModel.OperationContext>>() != null);
        return default(IExtensionCollection<System.ServiceModel.OperationContext>);
      }
    }

    public bool HasSupportingTokens
    {
      get
      {
        return default(bool);
      }
    }

    public ServiceHostBase Host
    {
      get
      {
        return default(ServiceHostBase);
      }
    }

    public System.ServiceModel.Channels.MessageHeaders IncomingMessageHeaders
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageHeaders>() != null);
        return default(System.ServiceModel.Channels.MessageHeaders);
      }
    }

    public System.ServiceModel.Channels.MessageProperties IncomingMessageProperties
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageProperties>() != null);
        return default(System.ServiceModel.Channels.MessageProperties);
      }
    }

    public System.ServiceModel.Channels.MessageVersion IncomingMessageVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageVersion>() != null);
        return default(System.ServiceModel.Channels.MessageVersion);
      }
    }

    public InstanceContext InstanceContext
    {
      get
      {
        Contract.Ensures(Contract.Result<InstanceContext>() != null);
        return default(InstanceContext);
      }
    }

    public bool IsUserContext
    {
      get
      {
        return default(bool);
      }
    }

    public System.ServiceModel.Channels.MessageHeaders OutgoingMessageHeaders
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageHeaders>() != null);

        return default(System.ServiceModel.Channels.MessageHeaders);
      }
    }

    public System.ServiceModel.Channels.MessageProperties OutgoingMessageProperties
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageProperties>() != null);

        return default(System.ServiceModel.Channels.MessageProperties);
      }
    }

    public System.ServiceModel.Channels.RequestContext RequestContext
    {
      get
      {
        return default(System.ServiceModel.Channels.RequestContext);
      }
      set
      {
      }
    }

    public ServiceSecurityContext ServiceSecurityContext
    {
      get
      {
        return default(ServiceSecurityContext);
      }
    }

    public string SessionId
    {
      get
      {
        return default(string);
      }
    }

    public ICollection<System.ServiceModel.Security.SupportingTokenSpecification> SupportingTokens
    {
      get
      {
        return default(ICollection<System.ServiceModel.Security.SupportingTokenSpecification>);
      }
    }
    #endregion

    #region Events
    public event EventHandler OperationCompleted
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
