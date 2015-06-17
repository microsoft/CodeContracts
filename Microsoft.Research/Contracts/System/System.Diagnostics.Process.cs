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

namespace System.Diagnostics
{

  public class Process
  {

    public string MainWindowTitle
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);

      }
    }

#if false
        public int ExitCode
        {
          get;
        }

        public System.ComponentModel.ISynchronizeInvoke SynchronizingObject
        {
          get;
          set;
        }

        public bool EnableRaisingEvents
        {
          get;
          set;
        }

        public int WorkingSet
        {
          get;
        }
#endif

    public ProcessModule MainModule
    {
      get
      {
        return default(ProcessModule);
      }
    }

#if false
    public int HandleCount
    {
      get;
    }

    public int PeakPagedMemorySize
    {
      get;
    }

    public DateTime StartTime
    {
      get;
    }
#endif

    public ProcessThreadCollection Threads
    {
      get
      {
        Contract.Ensures(Contract.Result<ProcessThreadCollection>() != null);
        return default(ProcessThreadCollection);
      }
    }

#if false
    public int PeakVirtualMemorySize
    {
      get;
    }
#endif
    public System.IO.StreamReader StandardError
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IO.StreamReader>() != null);
        return default(System.IO.StreamReader);
      }
    }
#if false
    public DateTime ExitTime
    {
      get;
    }

    public int PrivateMemorySize
    {
      get;
    }

    public int ProcessorAffinity
    {
      get;
      set;
    }

    public int VirtualMemorySize
    {
      get;
    }

    public TimeSpan TotalProcessorTime
    {
      get;
    }

    public int PagedMemorySize
    {
      get;
    }

    public int Handle
    {
      get;
    }

    public TimeSpan UserProcessorTime
    {
      get;
    }

    public int MainWindowHandle
    {
      get;
    }

    public int NonpagedSystemMemorySize
    {
      get;
    }

    public bool HasExited
    {
      get;
    }

    public int Id
    {
      get;
    }
#endif

#if false
    public ProcessPriorityClass PriorityClass
    {
      get;
      set;
    }
#endif
    extern public IntPtr MinWorkingSet
    {
      get;
      set;
    }

#if false
    public bool Responding
    {
      get;
    }
#endif

    public System.IO.StreamReader StandardOutput
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IO.StreamReader>() != null);
        return default(System.IO.StreamReader);
      }
    }

#if false
    public int MaxWorkingSet
    {
      get;
      set;
    }

    public int BasePriority
    {
      get;
    }
#endif

    public string ProcessName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

        return default(string);
      }
    }

    public ProcessStartInfo StartInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<ProcessStartInfo>() != null);
        return default(ProcessStartInfo);

      }
      set
      {
        Contract.Requires(value != null);
      }
    }

#if false
        public TimeSpan PrivilegedProcessorTime
        {
          get;
        }

        public bool PriorityBoostEnabled
        {
          get;
          set;
        }

        public int PeakWorkingSet
        {
          get;
        }

        public int PagedSystemMemorySize
        {
          get;
        }
#endif

    public System.IO.StreamWriter StandardInput
    {
      get
      {
        Contract.Ensures(Contract.Result<System.IO.StreamWriter>() != null);
        return default(System.IO.StreamWriter);

      }
    }

    public string MachineName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

#if false
    public ProcessModuleCollection Modules
    {
      get;
    }
#endif
    public bool WaitForInputIdle()
    {

      return default(bool);
    }
    public bool WaitForInputIdle(int milliseconds)
    {

      return default(bool);
    }
    public void WaitForExit()
    {

    }
    public bool WaitForExit(int milliseconds)
    {

      return default(bool);
    }

    public void Kill()
    {

    }
    public static Process Start(ProcessStartInfo startInfo)
    {
      Contract.Requires(startInfo != null);

      // It may return null!
      // Contract.Ensures(Contract.Result<Process>() != null);
      return default(Process);
    }
    public static Process Start(string fileName, string arguments)
    {

      return default(Process);
    }
    public static Process Start(string fileName)
    {
      Contract.Requires(fileName != null);

      return default(Process);
    }
#if false
    public bool Start()
    {

      return default(bool);
    }
    public void Refresh()
    {

    }
#endif
    public static Process GetCurrentProcess()
    {
      Contract.Ensures(Contract.Result<Process>() != null);
      return default(Process);
    }
    public static Process[] GetProcesses(string machineName)
    {
      Contract.Ensures(Contract.Result<Process[]>() != null);
      return default(Process[]);
    }

    public static Process[] GetProcesses()
    {
      Contract.Ensures(Contract.Result<Process[]>() != null);
      return default(Process[]);
    }

    public static Process[] GetProcessesByName(string processName, string machineName)
    {
      Contract.Ensures(Contract.Result<Process[]>() != null);
      return default(Process[]);
    }

    public static Process[] GetProcessesByName(string processName)
    {
      Contract.Ensures(Contract.Result<Process[]>() != null);
      return default(Process[]);
    }

    public static Process GetProcessById(int processId)
    {

      return default(Process);
    }
    public static Process GetProcessById(int processId, string machineName)
    {
      return default(Process);
    }
    public static void LeaveDebugMode()
    {

    }
    public static void EnterDebugMode()
    {

    }
    public void Close()
    {

    }
    public bool CloseMainWindow()
    {

      return default(bool);
    }

  }
}

#endif