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


#if !SILVERLIGHT

using System;
using System.Diagnostics.Contracts;

namespace System.Net
{

  public class SocketAddress
  {

    extern public System.Net.Sockets.AddressFamily Family
    {
      get;
    }

    extern public int Size
    {
      get;
    }

    public byte this[int offset]
    {
      get
      {
        Contract.Requires(offset >= 0);
        Contract.Requires(offset < this.Size);

        return default(byte);
      }
      set
      {
        Contract.Requires(offset >= 0);
        Contract.Requires(offset < this.Size);
      }
    }

    public SocketAddress(System.Net.Sockets.AddressFamily family, int size)
    {
      Contract.Requires(size >= 2);
    }

    public SocketAddress(System.Net.Sockets.AddressFamily family)
    {
      
    }
  }
}

#endif