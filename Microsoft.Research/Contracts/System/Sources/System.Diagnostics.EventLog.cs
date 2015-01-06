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

// File System.Diagnostics.EventLog.cs
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
  public partial class EventLog : System.ComponentModel.Component, System.ComponentModel.ISupportInitialize
  {
    #region Methods and constructors
    public void BeginInit()
    {
    }

    public void Clear()
    {
    }

    public void Close()
    {
    }

    public static void CreateEventSource(string source, string logName, string machineName)
    {
    }

    public static void CreateEventSource(EventSourceCreationData sourceData)
    {
    }

    public static void CreateEventSource(string source, string logName)
    {
    }

    public static void Delete(string logName)
    {
    }

    public static void Delete(string logName, string machineName)
    {
    }

    public static void DeleteEventSource(string source, string machineName)
    {
    }

    public static void DeleteEventSource(string source)
    {
    }

    protected override void Dispose(bool disposing)
    {
    }

    public void EndInit()
    {
    }

    public EventLog()
    {
    }

    public EventLog(string logName)
    {
    }

    public EventLog(string logName, string machineName, string source)
    {
    }

    public EventLog(string logName, string machineName)
    {
    }

    public static bool Exists(string logName)
    {
      return default(bool);
    }

    public static bool Exists(string logName, string machineName)
    {
      return default(bool);
    }

    public static System.Diagnostics.EventLog[] GetEventLogs(string machineName)
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.EventLog[]>() != null);

      return default(System.Diagnostics.EventLog[]);
    }

    public static System.Diagnostics.EventLog[] GetEventLogs()
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.EventLog[]>() != null);

      return default(System.Diagnostics.EventLog[]);
    }

    public static string LogNameFromSourceName(string source, string machineName)
    {
      return default(string);
    }

    public void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
    {
    }

    public void RegisterDisplayName(string resourceFile, long resourceId)
    {
    }

    public static bool SourceExists(string source)
    {
      return default(bool);
    }

    public static bool SourceExists(string source, string machineName)
    {
      return default(bool);
    }

    public void WriteEntry(string message, EventLogEntryType type)
    {
    }

    public static void WriteEntry(string source, string message, EventLogEntryType type)
    {
      Contract.Ensures(0 <= source.Length);
      Contract.Ensures(source.Length <= 212);
    }

    public static void WriteEntry(string source, string message)
    {
      Contract.Ensures(0 <= source.Length);
      Contract.Ensures(source.Length <= 212);
    }

    public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category)
    {
      Contract.Ensures(0 <= source.Length);
      Contract.Ensures(source.Length <= 212);
    }

    public void WriteEntry(string message)
    {
    }

    public void WriteEntry(string message, EventLogEntryType type, int eventID, short category)
    {
    }

    public void WriteEntry(string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
    {
    }

    public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
    {
      Contract.Ensures(0 <= source.Length);
      Contract.Ensures(source.Length <= 212);
    }

    public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID)
    {
      Contract.Ensures(0 <= source.Length);
      Contract.Ensures(source.Length <= 212);
    }

    public void WriteEntry(string message, EventLogEntryType type, int eventID)
    {
    }

    public void WriteEvent(EventInstance instance, byte[] data, Object[] values)
    {
    }

    public static void WriteEvent(string source, EventInstance instance, Object[] values)
    {
      Contract.Ensures(0 <= source.Length);
      Contract.Ensures(source.Length <= 212);
    }

    public static void WriteEvent(string source, EventInstance instance, byte[] data, Object[] values)
    {
      Contract.Ensures(0 <= source.Length);
      Contract.Ensures(source.Length <= 212);
    }

    public void WriteEvent(EventInstance instance, Object[] values)
    {
    }
    #endregion

    #region Properties and indexers
    public bool EnableRaisingEvents
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public EventLogEntryCollection Entries
    {
      get
      {
        return default(EventLogEntryCollection);
      }
    }

    public string Log
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string LogDisplayName
    {
      get
      {
        return default(string);
      }
    }

    public string MachineName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public long MaximumKilobytes
    {
      get
      {
        return default(long);
      }
      set
      {
      }
    }

    public int MinimumRetentionDays
    {
      get
      {
        return default(int);
      }
    }

    public OverflowAction OverflowAction
    {
      get
      {
        return default(OverflowAction);
      }
    }

    public string Source
    {
      get
      {
        return default(string);
      }
      set
      {
        Contract.Ensures(0 <= value.Length);
        Contract.Ensures(value.Length <= 212);
      }
    }

    public System.ComponentModel.ISynchronizeInvoke SynchronizingObject
    {
      get
      {
        return default(System.ComponentModel.ISynchronizeInvoke);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EntryWrittenEventHandler EntryWritten
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
