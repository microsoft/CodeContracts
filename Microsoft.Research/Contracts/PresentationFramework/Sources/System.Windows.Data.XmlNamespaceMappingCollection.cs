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

// File System.Windows.Data.XmlNamespaceMappingCollection.cs
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


namespace System.Windows.Data
{
  public partial class XmlNamespaceMappingCollection : System.Xml.XmlNamespaceManager, ICollection<XmlNamespaceMapping>, IEnumerable<XmlNamespaceMapping>, System.Collections.IEnumerable, System.Windows.Markup.IAddChild
  {
    #region Methods and constructors
    public void Add(XmlNamespaceMapping mapping)
    {
    }

    protected virtual new void AddChild(Object value)
    {
    }

    protected virtual new void AddText(string text)
    {
    }

    public void Clear()
    {
    }

    public bool Contains(XmlNamespaceMapping mapping)
    {
      return default(bool);
    }

    public void CopyTo(XmlNamespaceMapping[] array, int arrayIndex)
    {
    }

    public override System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    protected IEnumerator<XmlNamespaceMapping> ProtectedGetEnumerator()
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerator<System.Windows.Data.XmlNamespaceMapping>>() != null);

      return default(IEnumerator<XmlNamespaceMapping>);
    }

    public bool Remove(XmlNamespaceMapping mapping)
    {
      return default(bool);
    }

    IEnumerator<XmlNamespaceMapping> System.Collections.Generic.IEnumerable<System.Windows.Data.XmlNamespaceMapping>.GetEnumerator()
    {
      return default(IEnumerator<XmlNamespaceMapping>);
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }

    public XmlNamespaceMappingCollection() : base (default(System.Xml.XmlNameTable))
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

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
