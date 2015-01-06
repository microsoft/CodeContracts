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

// File System.Windows.Markup.XmlnsDictionary.cs
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


namespace System.Windows.Markup
{
  public partial class XmlnsDictionary : System.Collections.IDictionary, System.Collections.ICollection, System.Collections.IEnumerable, System.Xaml.IXamlNamespaceResolver
  {
    #region Methods and constructors
    public void Add(string prefix, string xmlNamespace)
    {
    }

    public void Add(Object prefix, Object xmlNamespace)
    {
    }

    public void Clear()
    {
    }

    public bool Contains(Object key)
    {
      return default(bool);
    }

    public void CopyTo(System.Collections.DictionaryEntry[] array, int index)
    {
      Contract.Requires(array != null);
      Contract.Requires(index >= 0);
    }

    public void CopyTo(Array array, int index)
    {
    }

    public string DefaultNamespace()
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    protected System.Collections.IDictionaryEnumerator GetDictionaryEnumerator()
    {
      Contract.Ensures(Contract.Result<System.Collections.IDictionaryEnumerator>() != null);

      return default(System.Collections.IDictionaryEnumerator);
    }

    protected System.Collections.IEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<System.Collections.IEnumerator>() != null);

      return default(System.Collections.IEnumerator);
    }

    public string GetNamespace(string prefix)
    {
      return default(string);
    }

    public IEnumerable<System.Xaml.NamespaceDeclaration> GetNamespacePrefixes()
    {
      return default(IEnumerable<System.Xaml.NamespaceDeclaration>);
    }

    public string LookupNamespace(string prefix)
    {
      return default(string);
    }

    public string LookupPrefix(string xmlNamespace)
    {
      return default(string);
    }

    public void PopScope()
    {
    }

    public void PushScope()
    {
      Contract.Ensures(!this.IsReadOnly);
    }

    public void Remove(string prefix)
    {
    }

    public void Remove(Object prefix)
    {
    }

    public void Seal()
    {
    }

    System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
    {
      return default(System.Collections.IDictionaryEnumerator);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public XmlnsDictionary()
    {
    }

    public XmlnsDictionary(System.Windows.Markup.XmlnsDictionary xmlnsDictionary)
    {
      Contract.Ensures(0 <= xmlnsDictionary.Count);
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

    public bool IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public string this [string prefix]
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Object this [Object prefix]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public System.Collections.ICollection Keys
    {
      get
      {
        return default(System.Collections.ICollection);
      }
    }

    public bool Sealed
    {
      get
      {
        return default(bool);
      }
    }

    public Object SyncRoot
    {
      get
      {
        return default(Object);
      }
    }

    public System.Collections.ICollection Values
    {
      get
      {
        return default(System.Collections.ICollection);
      }
    }
    #endregion
  }
}
