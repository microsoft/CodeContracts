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

// File System.ServiceModel.Configuration.BindingsSection.cs
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
  sealed public partial class BindingsSection : System.Configuration.ConfigurationSection, IConfigurationContextProviderInternal
  {
    #region Methods and constructors
    public BindingsSection()
    {
    }

    public static System.ServiceModel.Configuration.BindingsSection GetSection(System.Configuration.Configuration config)
    {
      return default(System.ServiceModel.Configuration.BindingsSection);
    }

    protected override bool OnDeserializeUnrecognizedElement(string elementName, System.Xml.XmlReader reader)
    {
      return default(bool);
    }

    System.Configuration.ContextInformation System.ServiceModel.Configuration.IConfigurationContextProviderInternal.GetEvaluationContext()
    {
      return default(System.Configuration.ContextInformation);
    }

    System.Configuration.ContextInformation System.ServiceModel.Configuration.IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
    {
      return default(System.Configuration.ContextInformation);
    }
    #endregion

    #region Properties and indexers
    public BasicHttpBindingCollectionElement BasicHttpBinding
    {
      get
      {
        return default(BasicHttpBindingCollectionElement);
      }
    }

    public List<BindingCollectionElement> BindingCollections
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Generic.List<System.ServiceModel.Configuration.BindingCollectionElement>>() != null);

        return default(List<BindingCollectionElement>);
      }
    }

    public CustomBindingCollectionElement CustomBinding
    {
      get
      {
        return default(CustomBindingCollectionElement);
      }
    }

    public BindingCollectionElement this [string binding]
    {
      get
      {
        return default(BindingCollectionElement);
      }
    }

    public MsmqIntegrationBindingCollectionElement MsmqIntegrationBinding
    {
      get
      {
        return default(MsmqIntegrationBindingCollectionElement);
      }
    }

    public NetMsmqBindingCollectionElement NetMsmqBinding
    {
      get
      {
        return default(NetMsmqBindingCollectionElement);
      }
    }

    public NetNamedPipeBindingCollectionElement NetNamedPipeBinding
    {
      get
      {
        return default(NetNamedPipeBindingCollectionElement);
      }
    }

    public NetPeerTcpBindingCollectionElement NetPeerTcpBinding
    {
      get
      {
        return default(NetPeerTcpBindingCollectionElement);
      }
    }

    public NetTcpBindingCollectionElement NetTcpBinding
    {
      get
      {
        return default(NetTcpBindingCollectionElement);
      }
    }

    protected override System.Configuration.ConfigurationPropertyCollection Properties
    {
      get
      {
        return default(System.Configuration.ConfigurationPropertyCollection);
      }
    }

    public WS2007FederationHttpBindingCollectionElement WS2007FederationHttpBinding
    {
      get
      {
        return default(WS2007FederationHttpBindingCollectionElement);
      }
    }

    public WS2007HttpBindingCollectionElement WS2007HttpBinding
    {
      get
      {
        return default(WS2007HttpBindingCollectionElement);
      }
    }

    public WSDualHttpBindingCollectionElement WSDualHttpBinding
    {
      get
      {
        return default(WSDualHttpBindingCollectionElement);
      }
    }

    public WSFederationHttpBindingCollectionElement WSFederationHttpBinding
    {
      get
      {
        return default(WSFederationHttpBindingCollectionElement);
      }
    }

    public WSHttpBindingCollectionElement WSHttpBinding
    {
      get
      {
        return default(WSHttpBindingCollectionElement);
      }
    }
    #endregion
  }
}
