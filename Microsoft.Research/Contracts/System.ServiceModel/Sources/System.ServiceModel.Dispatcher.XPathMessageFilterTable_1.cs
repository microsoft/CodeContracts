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

// File System.ServiceModel.Dispatcher.XPathMessageFilterTable_1.cs
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


namespace System.ServiceModel.Dispatcher
{
  public partial class XPathMessageFilterTable<TFilterData> : IMessageFilterTable<TFilterData>, IDictionary<MessageFilter, TFilterData>, ICollection<KeyValuePair<MessageFilter, TFilterData>>, IEnumerable<KeyValuePair<MessageFilter, TFilterData>>, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public void Add(MessageFilter filter, TFilterData data)
    {
    }

    public void Add(XPathMessageFilter filter, TFilterData data)
    {
    }

    public void Add(KeyValuePair<MessageFilter, TFilterData> item)
    {
    }

    public void Clear()
    {
    }

    public bool Contains(KeyValuePair<MessageFilter, TFilterData> item)
    {
      return default(bool);
    }

    public bool ContainsKey(MessageFilter filter)
    {
      return default(bool);
    }

    public void CopyTo(KeyValuePair<MessageFilter, TFilterData>[] array, int arrayIndex)
    {
    }

    public IEnumerator<KeyValuePair<MessageFilter, TFilterData>> GetEnumerator()
    {
      return default(IEnumerator<KeyValuePair<MessageFilter, TFilterData>>);
    }

    public bool GetMatchingFilter(System.ServiceModel.Channels.Message message, out MessageFilter filter)
    {
      filter = default(MessageFilter);

      return default(bool);
    }

    public bool GetMatchingFilter(SeekableXPathNavigator navigator, out MessageFilter filter)
    {
      filter = default(MessageFilter);

      return default(bool);
    }

    public bool GetMatchingFilter(System.ServiceModel.Channels.MessageBuffer messageBuffer, out MessageFilter filter)
    {
      filter = default(MessageFilter);

      return default(bool);
    }

    public bool GetMatchingFilter(System.Xml.XPath.XPathNavigator navigator, out MessageFilter filter)
    {
      filter = default(MessageFilter);

      return default(bool);
    }

    public bool GetMatchingFilters(System.Xml.XPath.XPathNavigator navigator, ICollection<MessageFilter> results)
    {
      return default(bool);
    }

    public bool GetMatchingFilters(System.ServiceModel.Channels.Message message, ICollection<MessageFilter> results)
    {
      return default(bool);
    }

    public bool GetMatchingFilters(System.ServiceModel.Channels.MessageBuffer messageBuffer, ICollection<MessageFilter> results)
    {
      return default(bool);
    }

    public bool GetMatchingFilters(SeekableXPathNavigator navigator, ICollection<MessageFilter> results)
    {
      return default(bool);
    }

    public bool GetMatchingValue(System.ServiceModel.Channels.MessageBuffer messageBuffer, out TFilterData data)
    {
      data = default(TFilterData);

      return default(bool);
    }

    public bool GetMatchingValue(SeekableXPathNavigator navigator, out TFilterData data)
    {
      data = default(TFilterData);

      return default(bool);
    }

    public bool GetMatchingValue(System.Xml.XPath.XPathNavigator navigator, out TFilterData data)
    {
      data = default(TFilterData);

      return default(bool);
    }

    public bool GetMatchingValue(System.ServiceModel.Channels.Message message, out TFilterData data)
    {
      data = default(TFilterData);

      return default(bool);
    }

    public bool GetMatchingValues(System.ServiceModel.Channels.MessageBuffer messageBuffer, ICollection<TFilterData> results)
    {
      return default(bool);
    }

    public bool GetMatchingValues(System.Xml.XPath.XPathNavigator navigator, ICollection<TFilterData> results)
    {
      return default(bool);
    }

    public bool GetMatchingValues(SeekableXPathNavigator navigator, ICollection<TFilterData> results)
    {
      return default(bool);
    }

    public bool GetMatchingValues(System.ServiceModel.Channels.Message message, ICollection<TFilterData> results)
    {
      return default(bool);
    }

    public bool Remove(XPathMessageFilter filter)
    {
      return default(bool);
    }

    public bool Remove(MessageFilter filter)
    {
      return default(bool);
    }

    public bool Remove(KeyValuePair<MessageFilter, TFilterData> item)
    {
      return default(bool);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public void TrimToSize()
    {
    }

    public bool TryGetValue(MessageFilter filter, out TFilterData data)
    {
      data = default(TFilterData);

      return default(bool);
    }

    public XPathMessageFilterTable()
    {
    }

    public XPathMessageFilterTable(int capacity)
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

    public TFilterData this [MessageFilter filter]
    {
      get
      {
        return default(TFilterData);
      }
      set
      {
      }
    }

    public ICollection<MessageFilter> Keys
    {
      get
      {
        return default(ICollection<MessageFilter>);
      }
    }

    public int NodeQuota
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public ICollection<TFilterData> Values
    {
      get
      {
        return default(ICollection<TFilterData>);
      }
    }
    #endregion
  }
}
