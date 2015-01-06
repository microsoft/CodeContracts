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

// File System.ServiceModel.Channels.MessageEncoder.cs
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
  abstract public partial class MessageEncoder
  {
    #region Methods and constructors
    public virtual new T GetProperty<T>()
    {
      return default(T);
    }

    public virtual new bool IsContentTypeSupported(string contentType)
    {
      return default(bool);
    }

    protected MessageEncoder()
    {
    }

    public abstract Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType);

    public Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager)
    {
      return default(Message);
    }

    public abstract Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType);

    public Message ReadMessage(Stream stream, int maxSizeOfHeaders)
    {
      return default(Message);
    }

    public abstract void WriteMessage(Message message, Stream stream);

    public ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager)
    {
      return default(ArraySegment<byte>);
    }

    public abstract ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset);
    #endregion

    #region Properties and indexers
    public abstract string ContentType
    {
      get;
    }

    public abstract string MediaType
    {
      get;
    }

    public abstract MessageVersion MessageVersion
    {
      get;
    }
    #endregion
  }

  #region MessageEncoder contract binding
  [ContractClass(typeof(MessageEncoderContract))]
  public abstract partial class MessageEncoder
  {

  }

  [ContractClassFor(typeof(MessageEncoder))]
  abstract class MessageEncoderContract : MessageEncoder
  {
    public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
    {
      throw new NotImplementedException();
    }

    public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
    {
      throw new NotImplementedException();
    }

    public override void WriteMessage(Message message, Stream stream)
    {
      throw new NotImplementedException();
    }

    public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
    {
      throw new NotImplementedException();
    }

    public override string ContentType
    {
      get { throw new NotImplementedException(); }
    }

    public override string MediaType
    {
      get { throw new NotImplementedException(); }
    }

    public override MessageVersion MessageVersion
    {
      get {
        Contract.Ensures(Contract.Result<MessageVersion>() != null);

        throw new NotImplementedException(); 
      }
    }
  }
  #endregion

}
