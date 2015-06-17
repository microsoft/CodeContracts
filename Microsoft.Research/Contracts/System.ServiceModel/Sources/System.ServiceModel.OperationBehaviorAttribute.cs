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

// File System.ServiceModel.OperationBehaviorAttribute.cs
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
  [AttributeUsage(AttributeTargets.All)]
  sealed public partial class OperationBehaviorAttribute : Attribute, System.ServiceModel.Description.IOperationBehavior
  {
    #region Methods and constructors
    public OperationBehaviorAttribute()
    {
    }

    void System.ServiceModel.Description.IOperationBehavior.AddBindingParameters(System.ServiceModel.Description.OperationDescription description, System.ServiceModel.Channels.BindingParameterCollection parameters)
    {
    }

    void System.ServiceModel.Description.IOperationBehavior.ApplyClientBehavior(System.ServiceModel.Description.OperationDescription description, System.ServiceModel.Dispatcher.ClientOperation proxy)
    {
    }

    void System.ServiceModel.Description.IOperationBehavior.ApplyDispatchBehavior(System.ServiceModel.Description.OperationDescription description, System.ServiceModel.Dispatcher.DispatchOperation dispatch)
    {
    }

    void System.ServiceModel.Description.IOperationBehavior.Validate(System.ServiceModel.Description.OperationDescription description)
    {
    }
    #endregion

    #region Properties and indexers
    public bool AutoDisposeParameters
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ImpersonationOption Impersonation
    {
      get
      {
        return default(ImpersonationOption);
      }
      set
      {
      }
    }

    public ReleaseInstanceMode ReleaseInstanceMode
    {
      get
      {
        return default(ReleaseInstanceMode);
      }
      set
      {
      }
    }

    public bool TransactionAutoComplete
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool TransactionScopeRequired
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
