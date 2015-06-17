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

// File System.Runtime.Serialization.SerializationInfo.cs
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


namespace System.Runtime.Serialization
{
  sealed public partial class SerializationInfo
  {
    #region Methods and constructors
    public void AddValue(string name, int value)
    {
    }

    public void AddValue(string name, uint value)
    {
    }

    public void AddValue(string name, short value)
    {
    }

    public void AddValue(string name, ushort value)
    {
    }

    public void AddValue(string name, long value)
    {
    }

    public void AddValue(string name, double value)
    {
    }

    public void AddValue(string name, DateTime value)
    {
    }

    public void AddValue(string name, ulong value)
    {
    }

    public void AddValue(string name, float value)
    {
    }

    public void AddValue(string name, Object value)
    {
    }

    public void AddValue(string name, Object value, Type type)
    {
    }

    public void AddValue(string name, Decimal value)
    {
    }

    public void AddValue(string name, bool value)
    {
    }

    public void AddValue(string name, byte value)
    {
    }

    public void AddValue(string name, sbyte value)
    {
    }

    public void AddValue(string name, char value)
    {
    }

    public bool GetBoolean(string name)
    {
      return default(bool);
    }

    public byte GetByte(string name)
    {
      return default(byte);
    }

    public char GetChar(string name)
    {
      return default(char);
    }

    public DateTime GetDateTime(string name)
    {
      return default(DateTime);
    }

    public Decimal GetDecimal(string name)
    {
      return default(Decimal);
    }

    public double GetDouble(string name)
    {
      return default(double);
    }

    public SerializationInfoEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<System.Runtime.Serialization.SerializationInfoEnumerator>() != null);

      return default(SerializationInfoEnumerator);
    }

    public short GetInt16(string name)
    {
      return default(short);
    }

    public int GetInt32(string name)
    {
      return default(int);
    }

    public long GetInt64(string name)
    {
      return default(long);
    }

    public sbyte GetSByte(string name)
    {
      return default(sbyte);
    }

    public float GetSingle(string name)
    {
      return default(float);
    }

    public string GetString(string name)
    {
      return default(string);
    }

    public ushort GetUInt16(string name)
    {
      return default(ushort);
    }

    public uint GetUInt32(string name)
    {
      return default(uint);
    }

    public ulong GetUInt64(string name)
    {
      return default(ulong);
    }

    public Object GetValue(string name, Type type)
    {
      return default(Object);
    }

    public SerializationInfo(Type type, IFormatterConverter converter)
    {
      Contract.Requires(type.Module != null);
      Contract.Requires(type.Module.Assembly != null);
      Contract.Ensures(type.Module.Assembly != null);
    }

    public void SetType(Type type)
    {
    }
    #endregion

    #region Properties and indexers
    public string AssemblyName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string FullTypeName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool IsAssemblyNameSetExplicit
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsFullTypeNameSetExplicit
    {
      get
      {
        return default(bool);
      }
    }

    public int MemberCount
    {
      get
      {
        return default(int);
      }
    }

    public Type ObjectType
    {
      get
      {
        return default(Type);
      }
    }
    #endregion
  }
}
