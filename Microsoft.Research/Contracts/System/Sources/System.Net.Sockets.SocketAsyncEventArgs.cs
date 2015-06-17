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

// File System.Net.Sockets.SocketAsyncEventArgs.cs
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


namespace System.Net.Sockets
{
  public partial class SocketAsyncEventArgs : EventArgs, IDisposable
  {
    #region Methods and constructors
    public void Dispose()
    {
    }

    protected virtual new void OnCompleted(System.Net.Sockets.SocketAsyncEventArgs e)
    {
    }

    public void SetBuffer(int offset, int count)
    {
    }

    public void SetBuffer(byte[] buffer, int offset, int count)
    {
    }

    public SocketAsyncEventArgs()
    {
    }
    #endregion

    #region Properties and indexers
    public Socket AcceptSocket
    {
      get
      {
        return default(Socket);
      }
      set
      {
      }
    }

    public byte[] Buffer
    {
      get
      {
        return default(byte[]);
      }
    }

    public IList<ArraySegment<byte>> BufferList
    {
      get
      {
        return default(IList<ArraySegment<byte>>);
      }
      set
      {
      }
    }

    public int BytesTransferred
    {
      get
      {
        return default(int);
      }
    }

    public Exception ConnectByNameError
    {
      get
      {
        return default(Exception);
      }
    }

    public Socket ConnectSocket
    {
      get
      {
        return default(Socket);
      }
    }

    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public bool DisconnectReuseSocket
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public SocketAsyncOperation LastOperation
    {
      get
      {
        return default(SocketAsyncOperation);
      }
    }

    public int Offset
    {
      get
      {
        return default(int);
      }
    }

    public IPPacketInformation ReceiveMessageFromPacketInfo
    {
      get
      {
        return default(IPPacketInformation);
      }
    }

    public System.Net.EndPoint RemoteEndPoint
    {
      get
      {
        return default(System.Net.EndPoint);
      }
      set
      {
      }
    }

    public SendPacketsElement[] SendPacketsElements
    {
      get
      {
        return default(SendPacketsElement[]);
      }
      set
      {
      }
    }

    public TransmitFileOptions SendPacketsFlags
    {
      get
      {
        return default(TransmitFileOptions);
      }
      set
      {
      }
    }

    public int SendPacketsSendSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public SocketError SocketError
    {
      get
      {
        return default(SocketError);
      }
      set
      {
      }
    }

    public SocketFlags SocketFlags
    {
      get
      {
        return default(SocketFlags);
      }
      set
      {
      }
    }

    public Object UserToken
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EventHandler<System.Net.Sockets.SocketAsyncEventArgs> Completed
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
