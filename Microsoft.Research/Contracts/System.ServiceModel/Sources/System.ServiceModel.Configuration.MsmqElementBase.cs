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

// File System.ServiceModel.Configuration.MsmqElementBase.cs
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


namespace System.ServiceModel.Configuration
{
  abstract public partial class MsmqElementBase : TransportElement
  {
    #region Methods and constructors
    public override void ApplyConfiguration(System.ServiceModel.Channels.BindingElement bindingElement)
    {
    }

    public override void CopyFrom(ServiceModelExtensionElement from)
    {
    }

    protected internal override void InitializeFrom(System.ServiceModel.Channels.BindingElement bindingElement)
    {
    }

    protected MsmqElementBase()
    {
    }
    #endregion

    #region Properties and indexers
    public Uri CustomDeadLetterQueue
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public System.ServiceModel.DeadLetterQueue DeadLetterQueue
    {
      get
      {
        return default(System.ServiceModel.DeadLetterQueue);
      }
      set
      {
      }
    }

    public bool Durable
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool ExactlyOnce
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int MaxRetryCycles
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public MsmqTransportSecurityElement MsmqTransportSecurity
    {
      get
      {
        return default(MsmqTransportSecurityElement);
      }
    }

    protected override System.Configuration.ConfigurationPropertyCollection Properties
    {
      get
      {
        return default(System.Configuration.ConfigurationPropertyCollection);
      }
    }

    public bool ReceiveContextEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.ServiceModel.ReceiveErrorHandling ReceiveErrorHandling
    {
      get
      {
        return default(System.ServiceModel.ReceiveErrorHandling);
      }
      set
      {
      }
    }

    public int ReceiveRetryCount
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public TimeSpan RetryCycleDelay
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public TimeSpan TimeToLive
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public bool UseMsmqTracing
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool UseSourceJournal
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TimeSpan ValidityDuration
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
