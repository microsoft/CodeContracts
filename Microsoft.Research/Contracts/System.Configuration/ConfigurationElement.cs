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

using System;
using System.Collections;
using System.Text;
using System.Diagnostics.Contracts;
using System.Xml;

namespace System.Configuration
{
 

  public class ConfigurationElement
  {
    protected ConfigurationElement() { }

    protected internal virtual void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
    {
      Contract.Requires(reader != null);

    }

    protected virtual void ListErrors(IList errorList)
    {
      Contract.Requires(errorList != null);

    }

    protected virtual bool OnDeserializeUnrecognizedAttribute(string name, string value)
    {
      Contract.Requires(name != null);
      return default(bool);
    }
    protected virtual bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
    {
      Contract.Requires(elementName != null);
      Contract.Requires(reader != null);
      return default(bool);
    }
    protected virtual object OnRequiredPropertyNotFound(string name)
    {
      Contract.Requires(name != null);
      // likely non-null result expected
      return default(object);
    }

    protected virtual void PreSerialize(XmlWriter writer)
    {
      Contract.Requires(writer != null);
    }

    //protected internal virtual void Reset(ConfigurationElement parentElement)

    protected internal virtual bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
    {
      Contract.Requires(writer != null);
      return default(bool);
    }

    protected internal virtual bool SerializeToXmlElement(XmlWriter writer, string elementName)
    {
      Contract.Requires(writer != null);
      Contract.Requires(elementName != null);
      return default(bool);
    }

    protected void SetPropertyValue(ConfigurationProperty prop, object value, bool ignoreLocks)
    {
      Contract.Requires(prop != null);

    }

    protected internal virtual void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
    {
      Contract.Requires(sourceElement != null);
    }


    public ElementInformation ElementInformation
    {
      get
      {
        Contract.Ensures(Contract.Result<ElementInformation>() != null);
        return default(ElementInformation);
      }
    }

    protected internal virtual ConfigurationElementProperty ElementProperty
    {
      get
      {
        Contract.Ensures(Contract.Result<ConfigurationElementProperty>() != null);
        return default(ConfigurationElementProperty);
      }
    }
    protected ContextInformation EvaluationContext
    {
      get
      {
        Contract.Ensures(Contract.Result<ContextInformation>() != null);
        return default(ContextInformation);
      }
    }
    //protected internal object this[string propertyName] { get; set; }
    protected internal object this[ConfigurationProperty prop]
    {
      get
      {
        Contract.Requires(prop != null);
        return default(object);
      }
      set
      {
        Contract.Requires(prop != null);
      }
    }

    public ConfigurationLockCollection LockAllAttributesExcept
    {
      get
      {
        Contract.Ensures(Contract.Result<ConfigurationLockCollection>() != null);
        return default(ConfigurationLockCollection);
      }
    }

    public ConfigurationLockCollection LockAllElementsExcept
    {
      get
      {
        Contract.Ensures(Contract.Result<ConfigurationLockCollection>() != null);
        return default(ConfigurationLockCollection);
      }
    }

    public ConfigurationLockCollection LockAttributes
    {
      get
      {
        Contract.Ensures(Contract.Result<ConfigurationLockCollection>() != null);
        return default(ConfigurationLockCollection);
      }
    }

    public ConfigurationLockCollection LockElements
    {
      get
      {
        Contract.Ensures(Contract.Result<ConfigurationLockCollection>() != null);
        return default(ConfigurationLockCollection);
      }
    }
    //public bool LockItem { get; set; }

    // protected internal virtual ConfigurationPropertyCollection Properties { get; }

  }
}
