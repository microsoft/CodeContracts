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

// File System.ServiceModel.Channels.MessageHeaders.cs
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
  sealed public partial class MessageHeaders : IEnumerable<MessageHeaderInfo>, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public void Add(MessageHeader header)
    {
    }

    public void Clear()
    {
    }

    public void CopyHeaderFrom(Message message, int headerIndex)
    {
      Contract.Requires(message != null);
      Contract.Requires(0 <= headerIndex);
      Contract.Requires(headerIndex < message.Headers.Count);
      Contract.Requires(message.Headers.MessageVersion == this.MessageVersion);
    }

    public void CopyHeaderFrom(System.ServiceModel.Channels.MessageHeaders collection, int headerIndex)
    {
      Contract.Requires(collection != null);
      Contract.Requires(0 <= headerIndex);
      Contract.Requires(headerIndex < collection.Count);
      Contract.Requires(collection.MessageVersion == this.MessageVersion);
    }

    public void CopyHeadersFrom(Message message)
    {
      Contract.Requires(message != null);
      Contract.Requires(message.Headers != null);
      Contract.Requires(message.Headers.MessageVersion == this.MessageVersion);
    }

    public void CopyHeadersFrom(System.ServiceModel.Channels.MessageHeaders collection)
    {
      Contract.Requires(collection != null);
      Contract.Requires(collection.MessageVersion == this.MessageVersion);
    }

    public void CopyTo(MessageHeaderInfo[] array, int index)
    {
      Contract.Requires(array != null);
      Contract.Requires(0 <= index);
      Contract.Requires(index + this.Count <= array.Length);
    }

    public int FindHeader(string name, string ns)
    {
      Contract.Requires(name != null);
      Contract.Requires(ns != null);
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public int FindHeader(string name, string ns, string[] actors)
    {
      Contract.Requires(name != null);
      Contract.Requires(ns != null);
      Contract.Requires(actors != null);
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public IEnumerator<MessageHeaderInfo> GetEnumerator()
    {
      return default(IEnumerator<MessageHeaderInfo>);
    }

    public T GetHeader<T>(int index)
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public T GetHeader<T>(int index, System.Runtime.Serialization.XmlObjectSerializer serializer)
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public T GetHeader<T>(string name, string ns, System.Runtime.Serialization.XmlObjectSerializer serializer)
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public T GetHeader<T>(string name, string ns)
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public T GetHeader<T>(string name, string ns, string[] actors)
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public System.Xml.XmlDictionaryReader GetReaderAtHeader(int headerIndex)
    {
      Contract.Requires(0 <= headerIndex);
      Contract.Requires(headerIndex < this.Count);
      return default(System.Xml.XmlDictionaryReader);
    }

    public bool HaveMandatoryHeadersBeenUnderstood()
    {
      return default(bool);
    }

    public bool HaveMandatoryHeadersBeenUnderstood(string[] actors)
    {
      Contract.Requires(actors != null);
      return default(bool);
    }

    public void Insert(int headerIndex, MessageHeader header)
    {
      Contract.Requires(header != null);
      Contract.Requires(header.IsMessageVersionSupported(this.MessageVersion));
      Contract.Requires(0 <= headerIndex);
      Contract.Requires(headerIndex < this.Count);
    }

    public MessageHeaders(MessageVersion version, int initialSize)
    {
      Contract.Requires(version != null);
      Contract.Requires(0 <= initialSize);
    }

    public MessageHeaders(System.ServiceModel.Channels.MessageHeaders collection)
    {
      Contract.Requires(collection != null);
    }

    public MessageHeaders(MessageVersion version)
    {
      Contract.Requires(version != null);
    }

    public void RemoveAll(string name, string ns)
    {
      Contract.Requires(name != null);
      Contract.Requires(ns != null);
    }

    public void RemoveAt(int headerIndex)
    {
      Contract.Requires(0 <= headerIndex);
      Contract.Requires(headerIndex < this.Count);
    }

    public void SetAction(System.Xml.XmlDictionaryString action)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public void WriteHeader(int headerIndex, System.Xml.XmlDictionaryWriter writer)
    {
      Contract.Requires(0 <= headerIndex);
      Contract.Requires(headerIndex < this.Count);
      Contract.Requires(writer != null);
    }

    public void WriteHeader(int headerIndex, System.Xml.XmlWriter writer)
    {
      Contract.Requires(0 <= headerIndex);
      Contract.Requires(headerIndex < this.Count);
      Contract.Requires(writer != null);
    }

    public void WriteHeaderContents(int headerIndex, System.Xml.XmlDictionaryWriter writer)
    {
      Contract.Requires(0 <= headerIndex);
      Contract.Requires(headerIndex < this.Count);
      Contract.Requires(writer != null);
    }

    public void WriteHeaderContents(int headerIndex, System.Xml.XmlWriter writer)
    {
      Contract.Requires(0 <= headerIndex);
      Contract.Requires(headerIndex < this.Count);
      Contract.Requires(writer != null);
    }

    public void WriteStartHeader(int headerIndex, System.Xml.XmlDictionaryWriter writer)
    {
      Contract.Requires(0 <= headerIndex);
      Contract.Requires(headerIndex < this.Count);
      Contract.Requires(writer != null);
    }

    public void WriteStartHeader(int headerIndex, System.Xml.XmlWriter writer)
    {
      Contract.Requires(0 <= headerIndex);
      Contract.Requires(headerIndex < this.Count);
      Contract.Requires(writer != null);
    }
    #endregion

    #region Properties and indexers
    public string Action
    {
      get
      {
        return default(string);
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

    public System.ServiceModel.EndpointAddress FaultTo
    {
      get
      {
        return default(System.ServiceModel.EndpointAddress);
      }
      set
      {
      }
    }

    public System.ServiceModel.EndpointAddress From
    {
      get
      {
        return default(System.ServiceModel.EndpointAddress);
      }
      set
      {
      }
    }

    public MessageHeaderInfo this [int index]
    {
      get
      {
        return default(MessageHeaderInfo);
      }
    }

    public System.Xml.UniqueId MessageId
    {
      get
      {
        return default(System.Xml.UniqueId);
      }
      set
      {
      }
    }

    public MessageVersion MessageVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<MessageVersion>() != null);

        return default(MessageVersion);
      }
    }

    public System.Xml.UniqueId RelatesTo
    {
      get
      {
        return default(System.Xml.UniqueId);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public System.ServiceModel.EndpointAddress ReplyTo
    {
      get
      {
        return default(System.ServiceModel.EndpointAddress);
      }
      set
      {
      }
    }

    public Uri To
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public UnderstoodHeaders UnderstoodHeaders
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.UnderstoodHeaders>() != null);

        return default(UnderstoodHeaders);
      }
    }
    #endregion
  }
}
