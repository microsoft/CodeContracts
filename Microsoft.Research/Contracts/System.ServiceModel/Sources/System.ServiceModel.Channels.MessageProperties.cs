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

// File System.ServiceModel.Channels.MessageProperties.cs
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


namespace System.ServiceModel.Channels
{
  sealed public partial class MessageProperties : IDictionary<string, Object>, ICollection<KeyValuePair<string, Object>>, IEnumerable<KeyValuePair<string, Object>>, System.Collections.IEnumerable, IDisposable
  {
    #region Methods and constructors
    public void Add(string name, Object property)
    {
    }

    public void Clear()
    {
    }

    public bool ContainsKey(string name)
    {
      return default(bool);
    }

    public void CopyProperties(System.ServiceModel.Channels.MessageProperties properties)
    {
    }

    public void Dispose()
    {
    }

    public MessageProperties()
    {
    }

    public MessageProperties(System.ServiceModel.Channels.MessageProperties properties)
    {
    }

    public bool Remove(string name)
    {
      return default(bool);
    }

    void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Add(KeyValuePair<string, Object> pair)
    {
    }

    bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Contains(KeyValuePair<string, Object> pair)
    {
      return default(bool);
    }

    void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.CopyTo(KeyValuePair<string, Object>[] array, int index)
    {
    }

    bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Remove(KeyValuePair<string, Object> pair)
    {
      return default(bool);
    }

    IEnumerator<KeyValuePair<string, Object>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.GetEnumerator()
    {
      return default(IEnumerator<KeyValuePair<string, Object>>);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public bool TryGetValue(string name, out Object value)
    {
      value = default(Object);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public bool AllowOutputBatching
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public MessageEncoder Encoder
    {
      get
      {
        return default(MessageEncoder);
      }
      set
      {
      }
    }

    public bool IsFixedSize
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == false);

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

    public Object this [string name]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public ICollection<string> Keys
    {
      get
      {
        return default(ICollection<string>);
      }
    }

    public System.ServiceModel.Security.SecurityMessageProperty Security
    {
      get
      {
        return default(System.ServiceModel.Security.SecurityMessageProperty);
      }
      set
      {
      }
    }

    public ICollection<Object> Values
    {
      get
      {
        return default(ICollection<Object>);
      }
    }

    public Uri Via
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }
    #endregion
  }
}
