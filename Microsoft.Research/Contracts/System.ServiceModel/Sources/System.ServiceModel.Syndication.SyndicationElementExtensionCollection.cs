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

// File System.ServiceModel.Syndication.SyndicationElementExtensionCollection.cs
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


namespace System.ServiceModel.Syndication
{
  sealed public partial class SyndicationElementExtensionCollection : System.Collections.ObjectModel.Collection<SyndicationElementExtension>
  {
    #region Methods and constructors
    public void Add(string outerName, string outerNamespace, Object dataContractExtension, System.Runtime.Serialization.XmlObjectSerializer dataContractSerializer)
    {
    }

    public void Add(Object xmlSerializerExtension, System.Xml.Serialization.XmlSerializer serializer)
    {
    }

    public void Add(System.Xml.XmlReader xmlReader)
    {
    }

    public void Add(Object extension)
    {
    }

    public void Add(string outerName, string outerNamespace, Object dataContractExtension)
    {
    }

    public void Add(Object dataContractExtension, System.Runtime.Serialization.DataContractSerializer serializer)
    {
    }

    protected override void ClearItems()
    {
    }

    public System.Xml.XmlReader GetReaderAtElementExtensions()
    {
      Contract.Ensures(Contract.Result<System.Xml.XmlReader>() != null);

      return default(System.Xml.XmlReader);
    }

    protected override void InsertItem(int index, SyndicationElementExtension item)
    {
    }

    public System.Collections.ObjectModel.Collection<TExtension> ReadElementExtensions<TExtension>(string extensionName, string extensionNamespace, System.Xml.Serialization.XmlSerializer serializer)
    {
      Contract.Requires(!string.IsNullOrEmpty(extensionName));
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<TExtension>>() != null);

      return default(System.Collections.ObjectModel.Collection<TExtension>);
    }

    public System.Collections.ObjectModel.Collection<TExtension> ReadElementExtensions<TExtension>(string extensionName, string extensionNamespace)
    {
      Contract.Requires(!string.IsNullOrEmpty(extensionName));
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<TExtension>>() != null);

      return default(System.Collections.ObjectModel.Collection<TExtension>);
    }

    public System.Collections.ObjectModel.Collection<TExtension> ReadElementExtensions<TExtension>(string extensionName, string extensionNamespace, System.Runtime.Serialization.XmlObjectSerializer serializer)
    {
      Contract.Requires(!string.IsNullOrEmpty(extensionName));
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<TExtension>>() != null);

      return default(System.Collections.ObjectModel.Collection<TExtension>);
    }

    protected override void RemoveItem(int index)
    {
    }

    protected override void SetItem(int index, SyndicationElementExtension item)
    {
    }

    internal SyndicationElementExtensionCollection()
    {
    }
    #endregion
  }
}
