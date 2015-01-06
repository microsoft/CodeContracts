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

// File System.ServiceModel.NetTcpBinding.cs
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
  public partial class NetTcpBinding : System.ServiceModel.Channels.Binding, System.ServiceModel.Channels.IBindingRuntimePreferences
  {
    #region Methods and constructors
    public override System.ServiceModel.Channels.BindingElementCollection CreateBindingElements()
    {
      return default(System.ServiceModel.Channels.BindingElementCollection);
    }

    public NetTcpBinding()
    {
    }

    public NetTcpBinding(SecurityMode securityMode, bool reliableSessionEnabled)
    {
      Contract.Ensures(reliableSessionEnabled == this.ReliableSession.Enabled);
    }

    public NetTcpBinding(SecurityMode securityMode)
    {
    }

    public NetTcpBinding(string configurationName)
    {
    }

    public bool ShouldSerializeReaderQuotas()
    {
      return default(bool);
    }

    public bool ShouldSerializeReliableSession()
    {
      return default(bool);
    }

    public bool ShouldSerializeSecurity()
    {
      return default(bool);
    }

    public bool ShouldSerializeTransactionProtocol()
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public EnvelopeVersion EnvelopeVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.EnvelopeVersion>() != null);
        Contract.Ensures(Contract.Result<System.ServiceModel.EnvelopeVersion>() == System.ServiceModel.EnvelopeVersion.Soap12);

        return default(EnvelopeVersion);
      }
    }

    public HostNameComparisonMode HostNameComparisonMode
    {
      get
      {
        return default(HostNameComparisonMode);
      }
      set
      {
      }
    }

    public int ListenBacklog
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public long MaxBufferPoolSize
    {
      get
      {
        return default(long);
      }
      set
      {
      }
    }

    public int MaxBufferSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MaxConnections
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public long MaxReceivedMessageSize
    {
      get
      {
        return default(long);
      }
      set
      {
      }
    }

    public bool PortSharingEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Xml.XmlDictionaryReaderQuotas ReaderQuotas
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Xml.XmlDictionaryReaderQuotas>() != null);

        return default(System.Xml.XmlDictionaryReaderQuotas);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public OptionalReliableSession ReliableSession
    {
      get
      {
        Contract.Ensures(Contract.Result<OptionalReliableSession>() != null);
        return default(OptionalReliableSession);
      }
      set
      {
      }
    }

    public override string Scheme
    {
      get
      {
        return default(string);
      }
    }

    public NetTcpSecurity Security
    {
      get
      {
        return default(NetTcpSecurity);
      }
      set
      {
      }
    }

    bool System.ServiceModel.Channels.IBindingRuntimePreferences.ReceiveSynchronously
    {
      get
      {
        return default(bool);
      }
    }

    public bool TransactionFlow
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TransactionProtocol TransactionProtocol
    {
      get
      {
        return default(TransactionProtocol);
      }
      set
      {
      }
    }

    public TransferMode TransferMode
    {
      get
      {
        return default(TransferMode);
      }
      set
      {
      }
    }
    #endregion
  }
}
