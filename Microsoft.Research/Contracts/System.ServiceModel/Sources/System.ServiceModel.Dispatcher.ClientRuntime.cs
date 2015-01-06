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

// File System.ServiceModel.Dispatcher.ClientRuntime.cs
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
  sealed public partial class ClientRuntime
  {
    #region Methods and constructors
    internal ClientRuntime()
    {
    }
    #endregion

    #region Properties and indexers
    public Type CallbackClientType
    {
      get
      {
        return default(Type);
      }
      set
      {
      }
    }

    public DispatchRuntime CallbackDispatchRuntime
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Dispatcher.DispatchRuntime>() != null);

        return default(DispatchRuntime);
      }
    }

    public SynchronizedCollection<IChannelInitializer> ChannelInitializers
    {
      get
      {
        return default(SynchronizedCollection<IChannelInitializer>);
      }
    }

    public Type ContractClientType
    {
      get
      {
        return default(Type);
      }
      set
      {
      }
    }

    public string ContractName
    {
      get
      {
        return default(string);
      }
    }

    public string ContractNamespace
    {
      get
      {
        return default(string);
      }
    }

    public SynchronizedCollection<IInteractiveChannelInitializer> InteractiveChannelInitializers
    {
      get
      {
        return default(SynchronizedCollection<IInteractiveChannelInitializer>);
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

    public int MaxFaultSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public SynchronizedCollection<IClientMessageInspector> MessageInspectors
    {
      get
      {
        Contract.Ensures(Contract.Result<SynchronizedCollection<IClientMessageInspector>>() != null);
        return default(SynchronizedCollection<IClientMessageInspector>);
      }
    }

    public bool MessageVersionNoneFaultsEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public SynchronizedKeyedCollection<string, ClientOperation> Operations
    {
      get
      {
        return default(SynchronizedKeyedCollection<string, ClientOperation>);
      }
    }

    public IClientOperationSelector OperationSelector
    {
      get
      {
        return default(IClientOperationSelector);
      }
      set
      {
      }
    }

    public ClientOperation UnhandledClientOperation
    {
      get
      {
        return default(ClientOperation);
      }
    }

    public bool ValidateMustUnderstand
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Uri Via
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }
    #endregion
  }
}
