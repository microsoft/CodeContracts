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

// File System.ServiceModel.Dispatcher.DispatchRuntime.cs
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
  sealed public partial class DispatchRuntime
  {
    #region Methods and constructors
    internal DispatchRuntime()
    {
    }
    #endregion

    #region Properties and indexers
    public bool AutomaticInputSessionShutdown
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ClientRuntime CallbackClientRuntime
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Dispatcher.ClientRuntime>() != null);

        return default(ClientRuntime);
      }
    }

    public ChannelDispatcher ChannelDispatcher
    {
      get
      {
        return default(ChannelDispatcher);
      }
    }

    public System.ServiceModel.ConcurrencyMode ConcurrencyMode
    {
      get
      {
        return default(System.ServiceModel.ConcurrencyMode);
      }
      set
      {
      }
    }

    public EndpointDispatcher EndpointDispatcher
    {
      get
      {
        return default(EndpointDispatcher);
      }
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Policy.IAuthorizationPolicy> ExternalAuthorizationPolicies
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Policy.IAuthorizationPolicy>);
      }
      set
      {
      }
    }

    public bool IgnoreTransactionMessageProperty
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool ImpersonateCallerForAllOperations
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public SynchronizedCollection<IInputSessionShutdown> InputSessionShutdownHandlers
    {
      get
      {
        return default(SynchronizedCollection<IInputSessionShutdown>);
      }
    }

    public SynchronizedCollection<IInstanceContextInitializer> InstanceContextInitializers
    {
      get
      {
        return default(SynchronizedCollection<IInstanceContextInitializer>);
      }
    }

    public IInstanceContextProvider InstanceContextProvider
    {
      get
      {
        return default(IInstanceContextProvider);
      }
      set
      {
      }
    }

    public IInstanceProvider InstanceProvider
    {
      get
      {
        return default(IInstanceProvider);
      }
      set
      {
      }
    }

    public System.ServiceModel.AuditLevel MessageAuthenticationAuditLevel
    {
      get
      {
        return default(System.ServiceModel.AuditLevel);
      }
      set
      {
      }
    }

    public SynchronizedCollection<IDispatchMessageInspector> MessageInspectors
    {
      get
      {
        Contract.Ensures(Contract.Result<SynchronizedCollection<IDispatchMessageInspector>>() != null);
        return default(SynchronizedCollection<IDispatchMessageInspector>);
      }
    }

    public SynchronizedKeyedCollection<string, DispatchOperation> Operations
    {
      get
      {
        Contract.Ensures(Contract.Result<SynchronizedKeyedCollection<string, DispatchOperation>>() != null);
        return default(SynchronizedKeyedCollection<string, DispatchOperation>);
      }
    }

    public IDispatchOperationSelector OperationSelector
    {
      get
      {
        return default(IDispatchOperationSelector);
      }
      set
      {
      }
    }

    public bool PreserveMessage
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.ServiceModel.Description.PrincipalPermissionMode PrincipalPermissionMode
    {
      get
      {
        return default(System.ServiceModel.Description.PrincipalPermissionMode);
      }
      set
      {
      }
    }

    public bool ReleaseServiceInstanceOnTransactionComplete
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Web.Security.RoleProvider RoleProvider
    {
      get
      {
        return default(System.Web.Security.RoleProvider);
      }
      set
      {
      }
    }

    public System.ServiceModel.AuditLogLocation SecurityAuditLogLocation
    {
      get
      {
        return default(System.ServiceModel.AuditLogLocation);
      }
      set
      {
      }
    }

#if !NETFRAMEWORK_3_5

    public System.ServiceModel.ServiceAuthenticationManager ServiceAuthenticationManager
    {
      get
      {
        return default(System.ServiceModel.ServiceAuthenticationManager);
      }
      set
      {
      }
    }

#endif

    public System.ServiceModel.AuditLevel ServiceAuthorizationAuditLevel
    {
      get
      {
        return default(System.ServiceModel.AuditLevel);
      }
      set
      {
      }
    }

    public System.ServiceModel.ServiceAuthorizationManager ServiceAuthorizationManager
    {
      get
      {
        return default(System.ServiceModel.ServiceAuthorizationManager);
      }
      set
      {
      }
    }

    public System.ServiceModel.InstanceContext SingletonInstanceContext
    {
      get
      {
        return default(System.ServiceModel.InstanceContext);
      }
      set
      {
      }
    }

    public bool SuppressAuditFailure
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Threading.SynchronizationContext SynchronizationContext
    {
      get
      {
        return default(System.Threading.SynchronizationContext);
      }
      set
      {
      }
    }

    public bool TransactionAutoCompleteOnSessionClose
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Type Type
    {
      get
      {
        return default(Type);
      }
      set
      {
      }
    }

    public DispatchOperation UnhandledDispatchOperation
    {
      get
      {
        return default(DispatchOperation);
      }
      set
      {
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
    #endregion
  }
}
