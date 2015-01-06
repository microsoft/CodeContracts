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

// File System.Configuration.ConfigurationElementCollection.cs
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


namespace System.Configuration
{
  abstract public partial class ConfigurationElementCollection : ConfigurationElement, System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    protected virtual new void BaseAdd(int index, ConfigurationElement element)
    {
    }

    protected internal void BaseAdd(ConfigurationElement element, bool throwIfExists)
    {
    }

    protected virtual new void BaseAdd(ConfigurationElement element)
    {
    }

    protected internal void BaseClear()
    {
    }

    protected internal ConfigurationElement BaseGet(int index)
    {
      return default(ConfigurationElement);
    }

    protected internal ConfigurationElement BaseGet(Object key)
    {
      return default(ConfigurationElement);
    }

    protected internal Object[] BaseGetAllKeys()
    {
      Contract.Ensures(0 <= this.Count);

      return default(Object[]);
    }

    protected internal Object BaseGetKey(int index)
    {
      return default(Object);
    }

    protected int BaseIndexOf(ConfigurationElement element)
    {
      return default(int);
    }

    protected internal bool BaseIsRemoved(Object key)
    {
      return default(bool);
    }

    protected internal void BaseRemove(Object key)
    {
    }

    protected internal void BaseRemoveAt(int index)
    {
    }

    protected ConfigurationElementCollection(System.Collections.IComparer comparer)
    {
    }

    protected ConfigurationElementCollection()
    {
    }

    public void CopyTo(ConfigurationElement[] array, int index)
    {
    }

    protected virtual new ConfigurationElement CreateNewElement(string elementName)
    {
      return default(ConfigurationElement);
    }

    protected abstract ConfigurationElement CreateNewElement();

    public override bool Equals(Object compareTo)
    {
      return default(bool);
    }

    protected abstract Object GetElementKey(ConfigurationElement element);

    public System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    protected virtual new bool IsElementName(string elementName)
    {
      return default(bool);
    }

    protected virtual new bool IsElementRemovable(ConfigurationElement element)
    {
      return default(bool);
    }

    protected internal override bool IsModified()
    {
      return default(bool);
    }

    public override bool IsReadOnly()
    {
      return default(bool);
    }

    protected override bool OnDeserializeUnrecognizedElement(string elementName, System.Xml.XmlReader reader)
    {
      return default(bool);
    }

    protected internal override void Reset(ConfigurationElement parentElement)
    {
    }

    protected internal override void ResetModified()
    {
    }

    protected internal override bool SerializeElement(System.Xml.XmlWriter writer, bool serializeCollectionKey)
    {
      return default(bool);
    }

    protected internal override void SetReadOnly()
    {
    }

    void System.Collections.ICollection.CopyTo(Array arr, int index)
    {
    }

    protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
    {
    }
    #endregion

    #region Properties and indexers
    internal protected string AddElementName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    internal protected string ClearElementName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ConfigurationElementCollectionType CollectionType
    {
      get
      {
        return default(ConfigurationElementCollectionType);
      }
    }

    public int Count
    {
      get
      {
        return default(int);
      }
    }

    protected virtual new string ElementName
    {
      get
      {
        return default(string);
      }
    }

    public bool EmitClear
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    internal protected string RemoveElementName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Object SyncRoot
    {
      get
      {
        return default(Object);
      }
    }

    protected virtual new bool ThrowOnDuplicate
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
