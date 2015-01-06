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

// File System.ServiceModel.Channels.MessageFault.cs
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
  abstract public partial class MessageFault
  {
    #region Methods and constructors
    public static MessageFault CreateFault(System.ServiceModel.FaultCode code, System.ServiceModel.FaultReason reason, Object detail, System.Runtime.Serialization.XmlObjectSerializer serializer, string actor, string node)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageFault>() != null);

      return default(MessageFault);
    }

    public static MessageFault CreateFault(Message message, int maxBufferSize)
    {
      return default(MessageFault);
    }

    public static MessageFault CreateFault(System.ServiceModel.FaultCode code, string reason)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageFault>() != null);

      return default(MessageFault);
    }

    public static MessageFault CreateFault(System.ServiceModel.FaultCode code, System.ServiceModel.FaultReason reason)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageFault>() != null);

      return default(MessageFault);
    }

    public static MessageFault CreateFault(System.ServiceModel.FaultCode code, System.ServiceModel.FaultReason reason, Object detail)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageFault>() != null);

      return default(MessageFault);
    }

    public static MessageFault CreateFault(System.ServiceModel.FaultCode code, System.ServiceModel.FaultReason reason, Object detail, System.Runtime.Serialization.XmlObjectSerializer serializer, string actor)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageFault>() != null);

      return default(MessageFault);
    }

    public static MessageFault CreateFault(System.ServiceModel.FaultCode code, System.ServiceModel.FaultReason reason, Object detail, System.Runtime.Serialization.XmlObjectSerializer serializer)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageFault>() != null);

      return default(MessageFault);
    }

    public T GetDetail<T>()
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public T GetDetail<T>(System.Runtime.Serialization.XmlObjectSerializer serializer)
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public System.Xml.XmlDictionaryReader GetReaderAtDetailContents()
    {
      return default(System.Xml.XmlDictionaryReader);
    }

    protected MessageFault()
    {
    }

    protected virtual new System.Xml.XmlDictionaryReader OnGetReaderAtDetailContents()
    {
      return default(System.Xml.XmlDictionaryReader);
    }

    protected virtual new void OnWriteDetail(System.Xml.XmlDictionaryWriter writer, System.ServiceModel.EnvelopeVersion version)
    {
      Contract.Requires(writer != null);
    }

    protected abstract void OnWriteDetailContents(System.Xml.XmlDictionaryWriter writer);

    protected virtual new void OnWriteStartDetail(System.Xml.XmlDictionaryWriter writer, System.ServiceModel.EnvelopeVersion version)
    {
      Contract.Requires(writer != null);
    }

    public static bool WasHeaderNotUnderstood(MessageHeaders headers, string name, string ns)
    {
      return default(bool);
    }

    public void WriteTo(System.Xml.XmlDictionaryWriter writer, System.ServiceModel.EnvelopeVersion version)
    {
    }

    public void WriteTo(System.Xml.XmlWriter writer, System.ServiceModel.EnvelopeVersion version)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new string Actor
    {
      get
      {
        return default(string);
      }
    }

    public abstract System.ServiceModel.FaultCode Code
    {
      get;
    }

    public abstract bool HasDetail
    {
      get;
    }

    public bool IsMustUnderstandFault
    {
      get
      {
        Contract.Requires(this.Code != null);

        return default(bool);
      }
    }

    public virtual new string Node
    {
      get
      {
        return default(string);
      }
    }

    public abstract System.ServiceModel.FaultReason Reason
    {
      get;
    }
    #endregion
  }
}
