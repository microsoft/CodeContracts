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

// File System.ServiceModel.Channels.MessageHeader.cs
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
  abstract public partial class MessageHeader : MessageHeaderInfo
  {
    #region Methods and constructors
    public static MessageHeader CreateHeader(string name, string ns, Object value, bool mustUnderstand, string actor, bool relay)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageHeader>() != null);

      return default(MessageHeader);
    }

    public static MessageHeader CreateHeader(string name, string ns, Object value, System.Runtime.Serialization.XmlObjectSerializer serializer)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageHeader>() != null);

      return default(MessageHeader);
    }

    public static MessageHeader CreateHeader(string name, string ns, Object value)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageHeader>() != null);

      return default(MessageHeader);
    }

    public static MessageHeader CreateHeader(string name, string ns, Object value, bool mustUnderstand, string actor)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
      Contract.Requires(ns != null);
      Contract.Requires(actor != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageHeader>() != null);

      return default(MessageHeader);
    }

    public static MessageHeader CreateHeader(string name, string ns, Object value, System.Runtime.Serialization.XmlObjectSerializer serializer, bool mustUnderstand, string actor, bool relay)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
      Contract.Requires(ns != null);
      Contract.Requires(actor != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageHeader>() != null);

      return default(MessageHeader);
    }

    public static MessageHeader CreateHeader(string name, string ns, Object value, bool mustUnderstand)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
      Contract.Requires(ns != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageHeader>() != null);

      return default(MessageHeader);
    }

    public static MessageHeader CreateHeader(string name, string ns, Object value, System.Runtime.Serialization.XmlObjectSerializer serializer, bool mustUnderstand)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
      Contract.Requires(ns != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageHeader>() != null);

      return default(MessageHeader);
    }

    public static MessageHeader CreateHeader(string name, string ns, Object value, System.Runtime.Serialization.XmlObjectSerializer serializer, bool mustUnderstand, string actor)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
      Contract.Requires(ns != null);
      Contract.Requires(actor != null);
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageHeader>() != null);

      return default(MessageHeader);
    }

    [Pure]
    public virtual new bool IsMessageVersionSupported(MessageVersion messageVersion)
    {
      Contract.Requires(messageVersion != null);
      return default(bool);
    }

    protected MessageHeader()
    {
    }

    protected abstract void OnWriteHeaderContents(System.Xml.XmlDictionaryWriter writer, MessageVersion messageVersion);

    protected virtual new void OnWriteStartHeader(System.Xml.XmlDictionaryWriter writer, MessageVersion messageVersion)
    {
      Contract.Requires(writer != null);
      Contract.Requires(messageVersion != null);
    }

    public void WriteHeader(System.Xml.XmlDictionaryWriter writer, MessageVersion messageVersion)
    {
      Contract.Requires(writer != null);
      Contract.Requires(messageVersion != null);
    }

    public void WriteHeader(System.Xml.XmlWriter writer, MessageVersion messageVersion)
    {
      Contract.Requires(writer != null);
      Contract.Requires(messageVersion != null);
    }

    protected void WriteHeaderAttributes(System.Xml.XmlDictionaryWriter writer, MessageVersion messageVersion)
    {
      Contract.Requires(writer != null);
      Contract.Requires(messageVersion != null);
    }

    public void WriteHeaderContents(System.Xml.XmlDictionaryWriter writer, MessageVersion messageVersion)
    {
      Contract.Requires(writer != null);
      Contract.Requires(messageVersion != null);
    }

    public void WriteStartHeader(System.Xml.XmlDictionaryWriter writer, MessageVersion messageVersion)
    {
      Contract.Requires(writer != null);
      Contract.Requires(messageVersion != null);
    }
    #endregion

    #region Properties and indexers
    public override string Actor
    {
      get
      {
        return default(string);
      }
    }

    public override bool IsReferenceParameter
    {
      get
      {
        return default(bool);
      }
    }

    public override bool MustUnderstand
    {
      get
      {
        return default(bool);
      }
    }

    public override bool Relay
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
