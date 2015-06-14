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

#if !SILVERLIGHT_4_0_WP

using System;
using System.Diagnostics.Contracts;

namespace System.Runtime.Serialization
{

  public class SerializationInfo
  {
#if !SILVERLIGHT
    public string AssemblyName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public int MemberCount
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }

    public string FullTypeName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public string GetString(string name)
    {
      Contract.Requires(name != null);

      return default(string);
    }
    public DateTime GetDateTime(string name)
    {
      Contract.Requires(name != null);

      return default(DateTime);
    }
    public Decimal GetDecimal(string name)
    {
      Contract.Requires(name != null);

      return default(Decimal);
    }
    public double GetDouble(string name)
    {
      Contract.Requires(name != null);

      return default(double);
    }
    public Single GetSingle(string name)
    {
      Contract.Requires(name != null);

      return default(Single);
    }
    public UInt64 GetUInt64(string name)
    {
      Contract.Requires(name != null);

      return default(UInt64);
    }
    public Int64 GetInt64(string name)
    {
      Contract.Requires(name != null);

      return default(Int64);
    }
    public UInt32 GetUInt32(string name)
    {
      Contract.Requires(name != null);

      return default(UInt32);
    }
    public int GetInt32(string name)
    {
      Contract.Requires(name != null);

      return default(int);
    }
    public UInt16 GetUInt16(string name)
    {
      Contract.Requires(name != null);

      return default(UInt16);
    }
    public Int16 GetInt16(string name)
    {
      Contract.Requires(name != null);

      return default(Int16);
    }
    public byte GetByte(string name)
    {
      Contract.Requires(name != null);

      return default(byte);
    }
    public SByte GetSByte(string name)
    {
      Contract.Requires(name != null);

      return default(SByte);
    }
    public Char GetChar(string name)
    {
      Contract.Requires(name != null);

      return default(Char);
    }
    public bool GetBoolean(string name)
    {
      Contract.Requires(name != null);

      return default(bool);
    }
    public object GetValue(string name, Type type)
    {
      Contract.Requires(name != null);
      Contract.Requires(type != null);

      return default(object);
    }
    public void AddValue(string name, DateTime value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, Decimal value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, double value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, Single value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, UInt64 value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, Int64 value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, UInt32 value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, int value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, UInt16 value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, Int16 value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, byte value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, SByte value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, Char value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, bool value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, object value)
    {
      Contract.Requires(name != null);

    }
    public void AddValue(string name, object value, Type type)
    {
      Contract.Requires(name != null);
      Contract.Requires(type != null);

    }
    [Pure]
    public SerializationInfoEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<SerializationInfoEnumerator>() != null);
      return default(SerializationInfoEnumerator);
    }
    public void SetType(Type type)
    {
      Contract.Requires(type != null);

    }
    public SerializationInfo(Type type, IFormatterConverter converter)
    {
      Contract.Requires(type != null);
      Contract.Requires(converter != null);
    }
#endif
  }
}

#endif
