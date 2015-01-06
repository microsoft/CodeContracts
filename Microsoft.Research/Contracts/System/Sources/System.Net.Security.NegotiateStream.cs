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

// File System.Net.Security.NegotiateStream.cs
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


namespace System.Net.Security
{
  public partial class NegotiateStream : AuthenticatedStream
  {
    #region Methods and constructors
    public virtual new void AuthenticateAsClient(System.Net.NetworkCredential credential, string targetName)
    {
    }

    public virtual new void AuthenticateAsClient(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ChannelBinding binding, string targetName)
    {
    }

    public virtual new void AuthenticateAsClient(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel allowedImpersonationLevel)
    {
    }

    public virtual new void AuthenticateAsClient(System.Net.NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel allowedImpersonationLevel)
    {
    }

    public virtual new void AuthenticateAsClient()
    {
    }

    public virtual new void AuthenticateAsServer()
    {
    }

    public virtual new void AuthenticateAsServer(System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy policy)
    {
    }

    public virtual new void AuthenticateAsServer(System.Net.NetworkCredential credential, ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel requiredImpersonationLevel)
    {
    }

    public virtual new void AuthenticateAsServer(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel requiredImpersonationLevel)
    {
    }

    public virtual new IAsyncResult BeginAuthenticateAsClient(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel allowedImpersonationLevel, AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginAuthenticateAsClient(System.Net.NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel allowedImpersonationLevel, AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginAuthenticateAsClient(AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginAuthenticateAsClient(System.Net.NetworkCredential credential, string targetName, AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginAuthenticateAsClient(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ChannelBinding binding, string targetName, AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginAuthenticateAsServer(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel requiredImpersonationLevel, AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginAuthenticateAsServer(AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginAuthenticateAsServer(System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy policy, AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    public virtual new IAsyncResult BeginAuthenticateAsServer(System.Net.NetworkCredential credential, ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel requiredImpersonationLevel, AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, Object asyncState)
    {
      return default(IAsyncResult);
    }

    protected override void Dispose(bool disposing)
    {
    }

    public virtual new void EndAuthenticateAsClient(IAsyncResult asyncResult)
    {
    }

    public virtual new void EndAuthenticateAsServer(IAsyncResult asyncResult)
    {
    }

    public override int EndRead(IAsyncResult asyncResult)
    {
      return default(int);
    }

    public override void EndWrite(IAsyncResult asyncResult)
    {
    }

    public override void Flush()
    {
    }

    public NegotiateStream(Stream innerStream) : base (default(Stream), default(bool))
    {
      Contract.Ensures(innerStream.CanWrite == true);
    }

    public NegotiateStream(Stream innerStream, bool leaveInnerStreamOpen) : base (default(Stream), default(bool))
    {
      Contract.Ensures(innerStream.CanWrite == true);
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      return default(int);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      return default(long);
    }

    public override void SetLength(long value)
    {
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
    }
    #endregion

    #region Properties and indexers
    public override bool CanRead
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanSeek
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanTimeout
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanWrite
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new System.Security.Principal.TokenImpersonationLevel ImpersonationLevel
    {
      get
      {
        return default(System.Security.Principal.TokenImpersonationLevel);
      }
    }

    public override bool IsAuthenticated
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsEncrypted
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsMutuallyAuthenticated
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsServer
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsSigned
    {
      get
      {
        return default(bool);
      }
    }

    public override long Length
    {
      get
      {
        return default(long);
      }
    }

    public override long Position
    {
      get
      {
        return default(long);
      }
      set
      {
      }
    }

    public override int ReadTimeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new System.Security.Principal.IIdentity RemoteIdentity
    {
      get
      {
        return default(System.Security.Principal.IIdentity);
      }
    }

    public override int WriteTimeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }
    #endregion
  }
}
