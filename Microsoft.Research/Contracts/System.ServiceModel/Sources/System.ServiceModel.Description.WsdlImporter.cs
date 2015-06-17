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

// File System.ServiceModel.Description.WsdlImporter.cs
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


namespace System.ServiceModel.Description
{
  public partial class WsdlImporter : MetadataImporter
  {
    #region Methods and constructors
    public System.Collections.ObjectModel.Collection<System.ServiceModel.Channels.Binding> ImportAllBindings()
    {
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.ServiceModel.Channels.Binding>>() != null);

      return default(System.Collections.ObjectModel.Collection<System.ServiceModel.Channels.Binding>);
    }

    public override System.Collections.ObjectModel.Collection<ContractDescription> ImportAllContracts()
    {
      return default(System.Collections.ObjectModel.Collection<ContractDescription>);
    }

    public override ServiceEndpointCollection ImportAllEndpoints()
    {
      return default(ServiceEndpointCollection);
    }

    public System.ServiceModel.Channels.Binding ImportBinding(System.Web.Services.Description.Binding wsdlBinding)
    {
      return default(System.ServiceModel.Channels.Binding);
    }

    public ContractDescription ImportContract(System.Web.Services.Description.PortType wsdlPortType)
    {
      return default(ContractDescription);
    }

    public ServiceEndpoint ImportEndpoint(System.Web.Services.Description.Port wsdlPort)
    {
      return default(ServiceEndpoint);
    }

    public ServiceEndpointCollection ImportEndpoints(System.Web.Services.Description.PortType wsdlPortType)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Description.ServiceEndpointCollection>() != null);

      return default(ServiceEndpointCollection);
    }

    public ServiceEndpointCollection ImportEndpoints(System.Web.Services.Description.Binding wsdlBinding)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Description.ServiceEndpointCollection>() != null);

      return default(ServiceEndpointCollection);
    }

    public ServiceEndpointCollection ImportEndpoints(System.Web.Services.Description.Service wsdlService)
    {
      Contract.Requires(wsdlService.Ports != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Description.ServiceEndpointCollection>() != null);

      return default(ServiceEndpointCollection);
    }

    public WsdlImporter(MetadataSet metadata, IEnumerable<IPolicyImportExtension> policyImportExtensions, IEnumerable<IWsdlImportExtension> wsdlImportExtensions)
    {
    }

    public WsdlImporter(MetadataSet metadata, IEnumerable<IPolicyImportExtension> policyImportExtensions, IEnumerable<IWsdlImportExtension> wsdlImportExtensions, MetadataImporterQuotas quotas)
    {
    }

    public WsdlImporter(MetadataSet metadata)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Web.Services.Description.ServiceDescriptionCollection WsdlDocuments
    {
      get
      {
        return default(System.Web.Services.Description.ServiceDescriptionCollection);
      }
    }

    public KeyedByTypeCollection<IWsdlImportExtension> WsdlImportExtensions
    {
      get
      {
        return default(KeyedByTypeCollection<IWsdlImportExtension>);
      }
    }

    public System.Xml.Schema.XmlSchemaSet XmlSchemas
    {
      get
      {
        return default(System.Xml.Schema.XmlSchemaSet);
      }
    }
    #endregion
  }
}
