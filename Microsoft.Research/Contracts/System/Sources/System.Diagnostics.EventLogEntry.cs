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

// File System.Diagnostics.EventLogEntry.cs
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


namespace System.Diagnostics
{
  sealed public partial class EventLogEntry : System.ComponentModel.Component, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public bool Equals(EventLogEntry otherEntry)
    {
      return default(bool);
    }

    internal EventLogEntry()
    {
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }
    #endregion

    #region Properties and indexers
    public string Category
    {
      get
      {
        return default(string);
      }
    }

    public short CategoryNumber
    {
      get
      {
        return default(short);
      }
    }

    public byte[] Data
    {
      get
      {
        Contract.Ensures(Contract.Result<byte[]>() != null);

        return default(byte[]);
      }
    }

    public EventLogEntryType EntryType
    {
      get
      {
        return default(EventLogEntryType);
      }
    }

    public int EventID
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 1073741823);

        return default(int);
      }
    }

    public int Index
    {
      get
      {
        return default(int);
      }
    }

    public long InstanceId
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<long>());
        Contract.Ensures(Contract.Result<long>() <= 2147483647);

        return default(long);
      }
    }

    public string MachineName
    {
      get
      {
        return default(string);
      }
    }

    public string Message
    {
      get
      {
        return default(string);
      }
    }

    public string[] ReplacementStrings
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);

        return default(string[]);
      }
    }

    public string Source
    {
      get
      {
        return default(string);
      }
    }

    public DateTime TimeGenerated
    {
      get
      {
        return default(DateTime);
      }
    }

    public DateTime TimeWritten
    {
      get
      {
        return default(DateTime);
      }
    }

    public string UserName
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
