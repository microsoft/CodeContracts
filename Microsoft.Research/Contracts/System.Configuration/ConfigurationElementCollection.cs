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

namespace System.Configuration
{
  public abstract class ConfigurationElementCollection
  {

    protected ConfigurationElementCollection(IComparer comparer)
    {
      Contract.Requires(comparer != null);

    }

    protected virtual void BaseAdd(ConfigurationElement element)
    {
      Contract.Requires(element != null);
    }

    protected internal void BaseAdd(ConfigurationElement element, bool throwIfExists)
    {
      Contract.Requires(element != null);
    }
    protected virtual void BaseAdd(int index, ConfigurationElement element)
    {
      Contract.Requires(element != null);

    }

    protected int BaseIndexOf(ConfigurationElement element)
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<int>() >= -1);
      return default(int);
    }

    public void CopyTo(ConfigurationElement[] array, int index)
    {
      Contract.Requires(array != null);

    }

    protected virtual ConfigurationElement CreateNewElement()
    {
      Contract.Ensures(Contract.Result<ConfigurationElement>() != null);
      return default(ConfigurationElement);
    }

    protected virtual ConfigurationElement CreateNewElement(string elementName)
    {
      Contract.Ensures(Contract.Result<ConfigurationElement>() != null);
      return default(ConfigurationElement);
    }

    protected virtual object GetElementKey(ConfigurationElement element)
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }

    //protected virtual bool IsElementName(string elementName);
    protected virtual bool IsElementRemovable(ConfigurationElement element)
    {
      Contract.Requires(element != null);
      return default(bool);
    }

    // public override bool IsReadOnly();
    // protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader);


    // Properties

    protected virtual string ElementName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    // public bool EmitClear { get; set; }
    // public bool IsSynchronized { get; }

    // protected internal string RemoveElementName { get; set; }
    // protected virtual bool ThrowOnDuplicate { get; }

  }
}
