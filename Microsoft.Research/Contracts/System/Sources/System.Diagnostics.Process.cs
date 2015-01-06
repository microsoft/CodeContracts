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

// File System.Diagnostics.Process.cs
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
  public partial class Process : System.ComponentModel.Component
  {
    #region Methods and constructors
    public void BeginErrorReadLine()
    {
    }

    public void BeginOutputReadLine()
    {
    }

    public void CancelErrorRead()
    {
    }

    public void CancelOutputRead()
    {
    }

    public void Close()
    {
    }

    public bool CloseMainWindow()
    {
      return default(bool);
    }

    protected override void Dispose(bool disposing)
    {
    }

    public static void EnterDebugMode()
    {
    }

    public static System.Diagnostics.Process GetCurrentProcess()
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.Process>() != null);

      return default(System.Diagnostics.Process);
    }

    public static System.Diagnostics.Process GetProcessById(int processId, string machineName)
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.Process>() != null);

      return default(System.Diagnostics.Process);
    }

    public static System.Diagnostics.Process GetProcessById(int processId)
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.Process>() != null);

      return default(System.Diagnostics.Process);
    }

    public static System.Diagnostics.Process[] GetProcesses(string machineName)
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.Process[]>() != null);

      return default(System.Diagnostics.Process[]);
    }

    public static System.Diagnostics.Process[] GetProcesses()
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.Process[]>() != null);

      return default(System.Diagnostics.Process[]);
    }

    public static System.Diagnostics.Process[] GetProcessesByName(string processName)
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.Process[]>() != null);

      return default(System.Diagnostics.Process[]);
    }

    public static System.Diagnostics.Process[] GetProcessesByName(string processName, string machineName)
    {
      Contract.Ensures(Contract.Result<System.Diagnostics.Process[]>() != null);

      return default(System.Diagnostics.Process[]);
    }

    public void Kill()
    {
    }

    public static void LeaveDebugMode()
    {
    }

    protected void OnExited()
    {
    }

    public Process()
    {
    }

    public void Refresh()
    {
    }

    public static System.Diagnostics.Process Start(string fileName, string arguments, string userName, System.Security.SecureString password, string domain)
    {
      Contract.Ensures(false);

      return default(System.Diagnostics.Process);
    }

    public static System.Diagnostics.Process Start(string fileName, string userName, System.Security.SecureString password, string domain)
    {
      Contract.Ensures(false);

      return default(System.Diagnostics.Process);
    }

    public bool Start()
    {
      return default(bool);
    }

    public static System.Diagnostics.Process Start(ProcessStartInfo startInfo)
    {
      return default(System.Diagnostics.Process);
    }

    public static System.Diagnostics.Process Start(string fileName, string arguments)
    {
      return default(System.Diagnostics.Process);
    }

    public static System.Diagnostics.Process Start(string fileName)
    {
      return default(System.Diagnostics.Process);
    }

    public override string ToString()
    {
      return default(string);
    }

    public void WaitForExit()
    {
    }

    public bool WaitForExit(int milliseconds)
    {
      return default(bool);
    }

    public bool WaitForInputIdle(int milliseconds)
    {
      return default(bool);
    }

    public bool WaitForInputIdle()
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public int BasePriority
    {
      get
      {
        return default(int);
      }
    }

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

    public int ExitCode
    {
      get
      {
        return default(int);
      }
    }

    public DateTime ExitTime
    {
      get
      {
        return default(DateTime);
      }
    }

    public IntPtr Handle
    {
      get
      {
        return default(IntPtr);
      }
    }

    public int HandleCount
    {
      get
      {
        return default(int);
      }
    }

    public bool HasExited
    {
      get
      {
        return default(bool);
      }
    }

    public int Id
    {
      get
      {
        return default(int);
      }
    }

    public string MachineName
    {
      get
      {
        return default(string);
      }
    }

    public ProcessModule MainModule
    {
      get
      {
        return default(ProcessModule);
      }
    }

    public IntPtr MainWindowHandle
    {
      get
      {
        return default(IntPtr);
      }
    }

    public string MainWindowTitle
    {
      get
      {
        return default(string);
      }
    }

    public IntPtr MaxWorkingSet
    {
      get
      {
        return default(IntPtr);
      }
      set
      {
      }
    }

    public IntPtr MinWorkingSet
    {
      get
      {
        return default(IntPtr);
      }
      set
      {
      }
    }

    public ProcessModuleCollection Modules
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Diagnostics.ProcessModuleCollection>() != null);

        return default(ProcessModuleCollection);
      }
    }

    public int NonpagedSystemMemorySize
    {
      get
      {
        return default(int);
      }
    }

    public long NonpagedSystemMemorySize64
    {
      get
      {
        return default(long);
      }
    }

    public int PagedMemorySize
    {
      get
      {
        return default(int);
      }
    }

    public long PagedMemorySize64
    {
      get
      {
        return default(long);
      }
    }

    public int PagedSystemMemorySize
    {
      get
      {
        return default(int);
      }
    }

    public long PagedSystemMemorySize64
    {
      get
      {
        return default(long);
      }
    }

    public int PeakPagedMemorySize
    {
      get
      {
        return default(int);
      }
    }

    public long PeakPagedMemorySize64
    {
      get
      {
        return default(long);
      }
    }

    public int PeakVirtualMemorySize
    {
      get
      {
        return default(int);
      }
    }

    public long PeakVirtualMemorySize64
    {
      get
      {
        return default(long);
      }
    }

    public int PeakWorkingSet
    {
      get
      {
        return default(int);
      }
    }

    public long PeakWorkingSet64
    {
      get
      {
        return default(long);
      }
    }

    public bool PriorityBoostEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ProcessPriorityClass PriorityClass
    {
      get
      {
        return default(ProcessPriorityClass);
      }
      set
      {
      }
    }

    public int PrivateMemorySize
    {
      get
      {
        return default(int);
      }
    }

    public long PrivateMemorySize64
    {
      get
      {
        return default(long);
      }
    }

    public TimeSpan PrivilegedProcessorTime
    {
      get
      {
        return default(TimeSpan);
      }
    }

    public string ProcessName
    {
      get
      {
        return default(string);
      }
    }

    public IntPtr ProcessorAffinity
    {
      get
      {
        return default(IntPtr);
      }
      set
      {
      }
    }

    public bool Responding
    {
      get
      {
        return default(bool);
      }
    }

    public int SessionId
    {
      get
      {
        return default(int);
      }
    }

    public StreamReader StandardError
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IO.StreamReader>() != null);

        return default(StreamReader);
      }
    }

    public StreamWriter StandardInput
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IO.StreamWriter>() != null);

        return default(StreamWriter);
      }
    }

    public StreamReader StandardOutput
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IO.StreamReader>() != null);

        return default(StreamReader);
      }
    }

    public ProcessStartInfo StartInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Diagnostics.ProcessStartInfo>() != null);

        return default(ProcessStartInfo);
      }
      set
      {
      }
    }

    public DateTime StartTime
    {
      get
      {
        return default(DateTime);
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

    public ProcessThreadCollection Threads
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Diagnostics.ProcessThreadCollection>() != null);

        return default(ProcessThreadCollection);
      }
    }

    public TimeSpan TotalProcessorTime
    {
      get
      {
        return default(TimeSpan);
      }
    }

    public TimeSpan UserProcessorTime
    {
      get
      {
        return default(TimeSpan);
      }
    }

    public int VirtualMemorySize
    {
      get
      {
        return default(int);
      }
    }

    public long VirtualMemorySize64
    {
      get
      {
        return default(long);
      }
    }

    public int WorkingSet
    {
      get
      {
        return default(int);
      }
    }

    public long WorkingSet64
    {
      get
      {
        return default(long);
      }
    }
    #endregion

    #region Events
    public event DataReceivedEventHandler ErrorDataReceived
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Exited
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataReceivedEventHandler OutputDataReceived
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
