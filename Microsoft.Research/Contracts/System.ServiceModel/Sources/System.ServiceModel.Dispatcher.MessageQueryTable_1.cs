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

// File System.ServiceModel.Dispatcher.MessageQueryTable_1.cs
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

#if !NETFRAMEWORK_3_5


namespace System.ServiceModel.Dispatcher
{
  public partial class MessageQueryTable<TItem> : IDictionary<MessageQuery, TItem>, ICollection<KeyValuePair<MessageQuery, TItem>>, IEnumerable<KeyValuePair<MessageQuery, TItem>>, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public void Add(MessageQuery key, TItem value)
    {
    }

    public void Add(KeyValuePair<MessageQuery, TItem> item)
    {
    }

    public void Clear()
    {
    }

    public bool Contains(KeyValuePair<MessageQuery, TItem> item)
    {
      return default(bool);
    }

    public bool ContainsKey(MessageQuery key)
    {
      return default(bool);
    }

    public void CopyTo(KeyValuePair<MessageQuery, TItem>[] array, int arrayIndex)
    {
    }

    public IEnumerable<KeyValuePair<MessageQuery, TResult>> Evaluate<TResult>(System.ServiceModel.Channels.MessageBuffer buffer)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.ServiceModel.Dispatcher.MessageQuery, TResult>>>() != null);

      return default(IEnumerable<KeyValuePair<MessageQuery, TResult>>);
    }

    public IEnumerable<KeyValuePair<MessageQuery, TResult>> Evaluate<TResult>(System.ServiceModel.Channels.Message message)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.ServiceModel.Dispatcher.MessageQuery, TResult>>>() != null);

      return default(IEnumerable<KeyValuePair<MessageQuery, TResult>>);
    }

    public IEnumerator<KeyValuePair<MessageQuery, TItem>> GetEnumerator()
    {
      return default(IEnumerator<KeyValuePair<MessageQuery, TItem>>);
    }

    public MessageQueryTable()
    {
    }

    public bool Remove(MessageQuery key)
    {
      return default(bool);
    }

    public bool Remove(KeyValuePair<MessageQuery, TItem> item)
    {
      return default(bool);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public bool TryGetValue(MessageQuery key, out TItem value)
    {
      value = default(TItem);

      return default(bool);
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

    public TItem this [MessageQuery key]
    {
      get
      {
        return default(TItem);
      }
      set
      {
      }
    }

    public ICollection<MessageQuery> Keys
    {
      get
      {
        return default(ICollection<MessageQuery>);
      }
    }

    public ICollection<TItem> Values
    {
      get
      {
        return default(ICollection<TItem>);
      }
    }
    #endregion
  }
}

#endif
