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

// File System.ServiceModel.Configuration.ServiceModelExtensionCollectionElement_1.cs
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
  abstract public partial class ServiceModelExtensionCollectionElement<TServiceModelExtensionElement> : System.Configuration.ConfigurationElement, ICollection<TServiceModelExtensionElement>, IEnumerable<TServiceModelExtensionElement>, System.Collections.IEnumerable, IConfigurationContextProviderInternal
  {
    #region Methods and constructors
    public virtual new void Add(TServiceModelExtensionElement element)
    {
    }

    public virtual new bool CanAdd(TServiceModelExtensionElement element)
    {
      return default(bool);
    }

    public void Clear()
    {
    }

    public bool Contains(TServiceModelExtensionElement element)
    {
      return default(bool);
    }

    public bool ContainsKey(string elementName)
    {
      Contract.Requires(!string.IsNullOrEmpty(elementName));

      return default(bool);
    }

    public bool ContainsKey(Type elementType)
    {
      return default(bool);
    }

    public void CopyTo(TServiceModelExtensionElement[] elements, int start)
    {
    }

    protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
    {
    }

    public IEnumerator<TServiceModelExtensionElement> GetEnumerator()
    {
      return default(IEnumerator<TServiceModelExtensionElement>);
    }

    protected override bool IsModified()
    {
      return default(bool);
    }

    protected override bool OnDeserializeUnrecognizedElement(string elementName, System.Xml.XmlReader reader)
    {
      return default(bool);
    }

    public bool Remove(TServiceModelExtensionElement element)
    {
      return default(bool);
    }

    protected override void Reset(System.Configuration.ConfigurationElement parentElement)
    {
    }

    protected override void ResetModified()
    {
    }

    internal ServiceModelExtensionCollectionElement()
    {
    }

    protected void SetIsModified()
    {
    }

    protected override void SetReadOnly()
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    System.Configuration.ContextInformation System.ServiceModel.Configuration.IConfigurationContextProviderInternal.GetEvaluationContext()
    {
      return default(System.Configuration.ContextInformation);
    }

    System.Configuration.ContextInformation System.ServiceModel.Configuration.IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
    {
      return default(System.Configuration.ContextInformation);
    }

    protected override void Unmerge(System.Configuration.ConfigurationElement sourceElement, System.Configuration.ConfigurationElement parentElement, System.Configuration.ConfigurationSaveMode saveMode)
    {
    }
    #endregion

    #region Properties and indexers
    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public TServiceModelExtensionElement this [Type extensionType]
    {
      get
      {
        return default(TServiceModelExtensionElement);
      }
    }

    public TServiceModelExtensionElement this [int index]
    {
      get
      {
        Contract.Requires(index >= 0);

        return default(TServiceModelExtensionElement);
      }
    }

    protected override System.Configuration.ConfigurationPropertyCollection Properties
    {
      get
      {
        return default(System.Configuration.ConfigurationPropertyCollection);
      }
    }

    bool System.Collections.Generic.ICollection<TServiceModelExtensionElement>.IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
