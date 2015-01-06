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

// File System.ServiceModel.Channels.Message.cs
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
  abstract public partial class Message : IDisposable
  {
    #region Methods and constructors
    public void Close()
    {
    }

    public MessageBuffer CreateBufferedCopy(int maxBufferSize)
    {
      Contract.Requires(0 <= maxBufferSize);
      Contract.Ensures(Contract.Result<MessageBuffer>() != null);
      return default(MessageBuffer);
    }

    public static Message CreateMessage(System.Xml.XmlDictionaryReader envelopeReader, int maxSizeOfHeaders, MessageVersion version)
    {
      Contract.Requires(envelopeReader != null);
      Contract.Requires(version != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(Message);
    }

    public static Message CreateMessage(System.Xml.XmlReader envelopeReader, int maxSizeOfHeaders, MessageVersion version)
    {
      Contract.Requires(envelopeReader != null);
      Contract.Requires(version != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(Message);
    }

    public static Message CreateMessage(MessageVersion version, System.ServiceModel.FaultCode faultCode, string reason, string action)
    {
      Contract.Requires(version != null);
      Contract.Requires(faultCode != null);
      Contract.Requires(reason != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(Message);
    }

    public static Message CreateMessage(MessageVersion version, MessageFault fault, string action)
    {
      Contract.Requires(version != null);
      Contract.Requires(fault != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(Message);
    }

    public static Message CreateMessage(MessageVersion version, System.ServiceModel.FaultCode faultCode, string reason, Object detail, string action)
    {
      Contract.Requires(version != null);
      Contract.Requires(faultCode != null);
      Contract.Requires(reason != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(Message);
    }

    public static Message CreateMessage(MessageVersion version, string action)
    {
      Contract.Requires(version != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(Message);
    }

    public static Message CreateMessage(MessageVersion version, string action, Object body, System.Runtime.Serialization.XmlObjectSerializer serializer)
    {
      Contract.Requires(version != null);
      Contract.Requires(body != null);
      Contract.Requires(serializer != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(Message);
    }

    public static Message CreateMessage(MessageVersion version, string action, Object body)
    {
      Contract.Requires(version != null);
      Contract.Requires(body != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(Message);
    }

    public static Message CreateMessage(MessageVersion version, string action, System.Xml.XmlReader body)
    {
      Contract.Requires(version != null);
      Contract.Requires(body != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(Message);
    }

    public static Message CreateMessage(MessageVersion version, string action, BodyWriter body)
    {
      Contract.Requires(version != null);
      Contract.Requires(body != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(Message);
    }

    public static Message CreateMessage(MessageVersion version, string action, System.Xml.XmlDictionaryReader body)
    {
      Contract.Requires(version != null);
      Contract.Requires(body != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(Message);
    }

    public T GetBody<T>()
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public T GetBody<T>(System.Runtime.Serialization.XmlObjectSerializer serializer)
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public string GetBodyAttribute(string localName, string ns)
    {
      Contract.Requires(localName != null);
      Contract.Requires(ns != null);
      return default(string);
    }

    public System.Xml.XmlDictionaryReader GetReaderAtBodyContents()
    {
      return default(System.Xml.XmlDictionaryReader);
    }

    protected Message()
    {
    }

    protected virtual new void OnBodyToString(System.Xml.XmlDictionaryWriter writer)
    {
      Contract.Requires(writer != null);
    }

    protected virtual new void OnClose()
    {
    }

    protected virtual new MessageBuffer OnCreateBufferedCopy(int maxBufferSize)
    {
      Contract.Requires(0 <= maxBufferSize);
      return default(MessageBuffer);
    }

    protected virtual new string OnGetBodyAttribute(string localName, string ns)
    {
      return default(string);
    }

    protected virtual new System.Xml.XmlDictionaryReader OnGetReaderAtBodyContents()
    {
      return default(System.Xml.XmlDictionaryReader);
    }

    protected abstract void OnWriteBodyContents(System.Xml.XmlDictionaryWriter writer);

    protected virtual new void OnWriteMessage(System.Xml.XmlDictionaryWriter writer)
    {
    }

    protected virtual new void OnWriteStartBody(System.Xml.XmlDictionaryWriter writer)
    {
      Contract.Requires(writer != null);
    }

    protected virtual new void OnWriteStartEnvelope(System.Xml.XmlDictionaryWriter writer)
    {
    }

    protected virtual new void OnWriteStartHeaders(System.Xml.XmlDictionaryWriter writer)
    {
    }

    void System.IDisposable.Dispose()
    {
    }

    public void WriteBody(System.Xml.XmlWriter writer)
    {
    }

    public void WriteBody(System.Xml.XmlDictionaryWriter writer)
    {
      Contract.Requires(writer != null);
    }

    public void WriteBodyContents(System.Xml.XmlDictionaryWriter writer)
    {
    }

    public void WriteMessage(System.Xml.XmlWriter writer)
    {
    }

    public void WriteMessage(System.Xml.XmlDictionaryWriter writer)
    {
    }

    public void WriteStartBody(System.Xml.XmlDictionaryWriter writer)
    {
    }

    public void WriteStartBody(System.Xml.XmlWriter writer)
    {
    }

    public void WriteStartEnvelope(System.Xml.XmlDictionaryWriter writer)
    {
    }
    #endregion

    #region Properties and indexers
    public abstract MessageHeaders Headers
    {
      get;
    }

    protected bool IsDisposed
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsEmpty
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsFault
    {
      get
      {
        return default(bool);
      }
    }

    public abstract MessageProperties Properties
    {
      get;
    }

    public MessageState State
    {
      get
      {
        return default(MessageState);
      }
    }

    public abstract MessageVersion Version
    {
      get;
    }
    #endregion
  }

  #region Message contract binding
  [ContractClass(typeof(MessageContract))]
  public partial class Message {

  }

  [ContractClassFor(typeof(Message))]
  abstract class MessageContract : Message {
    protected override void OnWriteBodyContents(Xml.XmlDictionaryWriter writer) {
      throw new NotImplementedException();
    }

    public override MessageHeaders Headers {
      get {
        Contract.Ensures(Contract.Result<MessageHeaders>() != null);
        throw new NotImplementedException(); }
    }

    public override MessageProperties Properties {
      get {
        Contract.Ensures(Contract.Result<MessageProperties>() != null);
        throw new NotImplementedException(); 
      }
    }

    public override MessageVersion Version {
      get {
        Contract.Ensures(Contract.Result<MessageVersion>() != null);
        throw new NotImplementedException(); }
    }
  }
  #endregion

}
