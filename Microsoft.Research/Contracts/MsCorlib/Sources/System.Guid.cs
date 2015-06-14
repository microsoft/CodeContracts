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

// File System.Guid.cs
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


namespace System
{
  public partial struct Guid : IFormattable, IComparable, IComparable<Guid>, IEquatable<Guid>
  {
    #region Methods and constructors
    public static bool operator != (System.Guid a, System.Guid b)
    {
      return default(bool);
    }

    public static bool operator == (System.Guid a, System.Guid b)
    {
      return default(bool);
    }

    public int CompareTo(System.Guid value)
    {
      return default(int);
    }

    public int CompareTo(Object value)
    {
      return default(int);
    }

    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public bool Equals(System.Guid g)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public Guid(byte[] b)
    {
    }

    public Guid(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
    {
    }

    public Guid(string g)
    {
    }

    public Guid(int a, short b, short c, byte[] d)
    {
    }

    public Guid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
    {
    }

    public static System.Guid NewGuid()
    {
      return default(System.Guid);
    }

    public static System.Guid Parse(string input)
    {
      return default(System.Guid);
    }

    public static System.Guid ParseExact(string input, string format)
    {
      Contract.Ensures(format.Length == 1);

      return default(System.Guid);
    }

    public byte[] ToByteArray()
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    public string ToString(string format)
    {
      return default(string);
    }

    public string ToString(string format, IFormatProvider provider)
    {
      return default(string);
    }

    public override string ToString()
    {
      return default(string);
    }

    public static bool TryParse(string input, out System.Guid result)
    {
      result = default(System.Guid);

      return default(bool);
    }

    public static bool TryParseExact(string input, string format, out System.Guid result)
    {
      result = default(System.Guid);

      return default(bool);
    }
    #endregion

    #region Fields
    public readonly static System.Guid Empty;
    #endregion
  }
}
