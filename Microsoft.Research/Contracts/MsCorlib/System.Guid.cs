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

using System;
using System.Diagnostics.Contracts;

namespace System
{

  public struct Guid
  {
    public static readonly Guid Empty;

    [Pure]
    public override string ToString()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length == 36);
#if NETFRAMEWORK_4_0
      Contract.Ensures(!String.IsNullOrWhiteSpace(Contract.Result<string>()));
#endif

      return default(string);
    }

    [Pure]
    public string ToString(string format)
    {

      return default(string);
    }
    [Pure]
    public static bool operator !=(Guid a, Guid b)
    {

      return default(bool);
    }
    [Pure]
    public static bool operator ==(Guid a, Guid b)
    {

      return default(bool);
    }

    [Pure]
    public Byte[] ToByteArray()
    {
      Contract.Ensures(Contract.Result<Byte[]>() != null);
      Contract.Ensures(Contract.Result<Byte[]>().Length == 16);

      return default(Byte[]);
    }
    public Guid(int a, Int16 b, Int16 c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
    {

    }
    public Guid(int a, Int16 b, Int16 c, Byte[] d)
    {
      Contract.Requires(d != null);
      Contract.Requires(d.Length == 8);

    }
    public Guid(string g)
    {
      Contract.Requires(g != null);

    }
#if !SILVERLIGHT
    public Guid(UInt32 a, UInt16 b, UInt16 c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
    {}
#endif

    public Guid(Byte[] b)
    {
      Contract.Requires(b != null);
      Contract.Requires(b.Length == 16);
    }

    public static Guid NewGuid()
    {
      Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
      return default(Guid);
    }
  }
}
