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

// File System.Runtime.Serialization.Formatter.cs
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
  abstract public partial class Formatter : IFormatter
  {
    #region Methods and constructors
    public abstract Object Deserialize(Stream serializationStream);

    protected Formatter()
    {
      Contract.Ensures(this.m_idGenerator != null);
      Contract.Ensures(this.m_objectQueue != null);
    }

    protected virtual new Object GetNext(out long objID)
    {
      Contract.Requires(this.m_objectQueue != null);

      objID = default(long);

      return default(Object);
    }

    protected virtual new long Schedule(Object obj)
    {
      return default(long);
    }

    public abstract void Serialize(Stream serializationStream, Object graph);

    protected abstract void WriteArray(Object obj, string name, Type memberType);

    protected abstract void WriteBoolean(bool val, string name);

    protected abstract void WriteByte(byte val, string name);

    protected abstract void WriteChar(char val, string name);

    protected abstract void WriteDateTime(DateTime val, string name);

    protected abstract void WriteDecimal(Decimal val, string name);

    protected abstract void WriteDouble(double val, string name);

    protected abstract void WriteInt16(short val, string name);

    protected abstract void WriteInt32(int val, string name);

    protected abstract void WriteInt64(long val, string name);

    protected virtual new void WriteMember(string memberName, Object data)
    {
    }

    protected abstract void WriteObjectRef(Object obj, string name, Type memberType);

    protected abstract void WriteSByte(sbyte val, string name);

    protected abstract void WriteSingle(float val, string name);

    protected abstract void WriteTimeSpan(TimeSpan val, string name);

    protected abstract void WriteUInt16(ushort val, string name);

    protected abstract void WriteUInt32(uint val, string name);

    protected abstract void WriteUInt64(ulong val, string name);

    protected abstract void WriteValueType(Object obj, string name, Type memberType);
    #endregion

    #region Properties and indexers
    public abstract SerializationBinder Binder
    {
      get;
      set;
    }

    public abstract StreamingContext Context
    {
      get;
      set;
    }

    public abstract ISurrogateSelector SurrogateSelector
    {
      get;
      set;
    }
    #endregion

    #region Fields
    protected ObjectIDGenerator m_idGenerator;
    protected System.Collections.Queue m_objectQueue;
    #endregion
  }
}
