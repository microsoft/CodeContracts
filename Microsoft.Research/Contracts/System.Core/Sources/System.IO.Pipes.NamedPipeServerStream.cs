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

// File System.IO.Pipes.NamedPipeServerStream.cs
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


namespace System.IO.Pipes
{
  sealed public partial class NamedPipeServerStream : PipeStream
  {
    #region Methods and constructors
    public IAsyncResult BeginWaitForConnection(AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public void Disconnect()
    {
    }

    public void EndWaitForConnection(IAsyncResult asyncResult)
    {
      Contract.Ensures(this.IsAsync == true);
    }

    public string GetImpersonationUserName()
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity, HandleInheritability inheritability, PipeAccessRights additionalAccessRights) : base (default(PipeDirection), default(int))
    {
      Contract.Ensures(1 <= pipeName.Length);
    }

    public NamedPipeServerStream(PipeDirection direction, bool isAsync, bool isConnected, Microsoft.Win32.SafeHandles.SafePipeHandle safePipeHandle) : base (default(PipeDirection), default(int))
    {
    }

    public NamedPipeServerStream(string pipeName) : base (default(PipeDirection), default(int))
    {
      Contract.Ensures(1 <= pipeName.Length);
    }

    public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode) : base (default(PipeDirection), default(int))
    {
      Contract.Ensures(1 <= pipeName.Length);
    }

    public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options) : base (default(PipeDirection), default(int))
    {
      Contract.Ensures(1 <= pipeName.Length);
    }

    public NamedPipeServerStream(string pipeName, PipeDirection direction) : base (default(PipeDirection), default(int))
    {
      Contract.Ensures(1 <= pipeName.Length);
    }

    public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances) : base (default(PipeDirection), default(int))
    {
      Contract.Ensures(1 <= pipeName.Length);
    }

    public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity, HandleInheritability inheritability) : base (default(PipeDirection), default(int))
    {
      Contract.Ensures(1 <= pipeName.Length);
    }

    public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity) : base (default(PipeDirection), default(int))
    {
      Contract.Ensures(1 <= pipeName.Length);
    }

    public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize) : base (default(PipeDirection), default(int))
    {
      Contract.Ensures(1 <= pipeName.Length);
    }

    public void RunAsClient(PipeStreamImpersonationWorker impersonationWorker)
    {
    }

    public void WaitForConnection()
    {
    }
    #endregion

    #region Fields
    public static int MaxAllowedServerInstances;
    #endregion
  }
}
