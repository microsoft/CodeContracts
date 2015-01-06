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

// File System.ServiceModel.Description.DataContractSerializerOperationBehavior.cs
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
  public partial class DataContractSerializerOperationBehavior : IOperationBehavior, IWsdlExportExtension
  {
    #region Methods and constructors
    public virtual new System.Runtime.Serialization.XmlObjectSerializer CreateSerializer(Type type, System.Xml.XmlDictionaryString name, System.Xml.XmlDictionaryString ns, IList<Type> knownTypes)
    {
      return default(System.Runtime.Serialization.XmlObjectSerializer);
    }

    public virtual new System.Runtime.Serialization.XmlObjectSerializer CreateSerializer(Type type, string name, string ns, IList<Type> knownTypes)
    {
      return default(System.Runtime.Serialization.XmlObjectSerializer);
    }

    public DataContractSerializerOperationBehavior(OperationDescription operation)
    {
    }

    public DataContractSerializerOperationBehavior(OperationDescription operation, System.ServiceModel.DataContractFormatAttribute dataContractFormatAttribute)
    {
    }

    void System.ServiceModel.Description.IOperationBehavior.AddBindingParameters(OperationDescription description, System.ServiceModel.Channels.BindingParameterCollection parameters)
    {
    }

    void System.ServiceModel.Description.IOperationBehavior.ApplyClientBehavior(OperationDescription description, System.ServiceModel.Dispatcher.ClientOperation proxy)
    {
    }

    void System.ServiceModel.Description.IOperationBehavior.ApplyDispatchBehavior(OperationDescription description, System.ServiceModel.Dispatcher.DispatchOperation dispatch)
    {
    }

    void System.ServiceModel.Description.IOperationBehavior.Validate(OperationDescription description)
    {
    }

    void System.ServiceModel.Description.IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext contractContext)
    {
    }

    void System.ServiceModel.Description.IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext endpointContext)
    {
    }
    #endregion

    #region Properties and indexers
    public System.ServiceModel.DataContractFormatAttribute DataContractFormatAttribute
    {
      get
      {
        return default(System.ServiceModel.DataContractFormatAttribute);
      }
    }

#if !NETFRAMEWORK_3_5
    public System.Runtime.Serialization.DataContractResolver DataContractResolver
    {
      get
      {
        return default(System.Runtime.Serialization.DataContractResolver);
      }
      set
      {
      }
    }
#endif

    public System.Runtime.Serialization.IDataContractSurrogate DataContractSurrogate
    {
      get
      {
        return default(System.Runtime.Serialization.IDataContractSurrogate);
      }
      set
      {
      }
    }

    public bool IgnoreExtensionDataObject
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int MaxItemsInObjectGraph
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }
    #endregion
  }
}
