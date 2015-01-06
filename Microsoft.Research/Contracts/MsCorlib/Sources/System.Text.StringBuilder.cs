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

// File System.Text.StringBuilder.cs
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


namespace System.Text
{
  sealed public partial class StringBuilder : System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public System.Text.StringBuilder Append(Decimal value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(double value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(bool value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(string value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(string value, int startIndex, int count)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(ushort value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(sbyte value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(int value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(long value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(float value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(byte value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(char value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(short value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(Object value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(uint value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(ulong value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(char[] value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(char value, int repeatCount)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Append(char[] value, int startIndex, int charCount)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder AppendFormat(string format, Object arg0, Object arg1, Object arg2)
    {
      Contract.Ensures(0 <= format.Length);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder AppendFormat(IFormatProvider provider, string format, Object[] args)
    {
      Contract.Ensures(0 <= format.Length);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder AppendFormat(string format, Object[] args)
    {
      Contract.Ensures(0 <= format.Length);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder AppendFormat(string format, Object arg0, Object arg1)
    {
      Contract.Ensures(0 <= format.Length);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder AppendFormat(string format, Object arg0)
    {
      Contract.Ensures(0 <= format.Length);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder AppendLine(string value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(System.Environment.NewLine.Length == 2);
      Contract.Ensures(this == Contract.Result<System.Text.StringBuilder>());

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder AppendLine()
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Environment.NewLine.Length == 2);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Clear()
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(this.Length == 0);

      return default(System.Text.StringBuilder);
    }

    public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
    }

    public int EnsureCapacity(int capacity)
    {
      Contract.Ensures((capacity - Contract.Result<int>()) <= 0);
      Contract.Ensures(0 <= Contract.Result<int>());

      return default(int);
    }

    public bool Equals(System.Text.StringBuilder sb)
    {
      return default(bool);
    }

    public System.Text.StringBuilder Insert(int index, float value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, int value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, long value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, uint value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, ulong value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, ushort value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, Decimal value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, Object value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, char[] value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, string value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, bool value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, double value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, string value, int count)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, char value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, short value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, sbyte value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Insert(int index, byte value)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);
      Contract.Ensures(System.Globalization.CultureInfo.CurrentCulture == System.Threading.Thread.CurrentThread.CurrentCulture);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Remove(int startIndex, int length)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Replace(string oldValue, string newValue)
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Replace(char oldChar, char newChar)
    {
      Contract.Ensures(0 <= this.Length);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public System.Text.StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() == this);

      return default(System.Text.StringBuilder);
    }

    public StringBuilder(int capacity)
    {
    }

    public StringBuilder()
    {
    }

    public StringBuilder(string value, int startIndex, int length, int capacity)
    {
      Contract.Ensures((startIndex - value.Length) <= 0);
    }

    public StringBuilder(int capacity, int maxCapacity)
    {
      Contract.Ensures((Contract.OldValue(capacity) - maxCapacity) <= 0);
    }

    public StringBuilder(string value)
    {
    }

    public StringBuilder(string value, int capacity)
    {
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public override string ToString()
    {
      return default(string);
    }

    public string ToString(int startIndex, int length)
    {
      Contract.Ensures((startIndex - this.Length) <= 0);
      Contract.Ensures(0 <= this.Length);

      return default(string);
    }
    #endregion

    #region Properties and indexers
    public int Capacity
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    [System.Runtime.CompilerServices.IndexerName("Chars")]
    public char this [int index]
    {
      get
      {
        return default(char);
      }
      set
      {
      }
    }

    public int Length
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MaxCapacity
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
