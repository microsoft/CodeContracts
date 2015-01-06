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

using System.Diagnostics.Contracts;
using System;


namespace System.Diagnostics
{
  // Summary:
  //     Represents an operating system process thread.
  public class ProcessThread 
  {
    // Summary:
    //     Gets the base priority of the thread.
    //
    // Returns:
    //     The base priority of the thread, which the operating system computes by combining
    //     the process priority class with the priority level of the associated thread.
#if false
    public int BasePriority { get; }
#endif
    //
    // Summary:
    //     Gets the current priority of the thread.
    //
    // Returns:
    //     The current priority of the thread, which may deviate from the base priority
    //     based on how the operating system is scheduling the thread. The priority
    //     may be temporarily boosted for an active thread.
#if false
    public int CurrentPriority { get; }
#endif
    //
    // Summary:
    //     Gets the unique identifier of the thread.
    //
    // Returns:
    //     The unique identifier associated with a specific thread.
#if false
    public int Id { get; }
#endif
    //
    // Summary:
    //     Sets the preferred processor for this thread to run on.
    //
    // Returns:
    //     The preferred processor for the thread, used when the system schedules threads,
    //     to determine which processor to run the thread on.
    //
    // Exceptions:
    //   System.ComponentModel.Win32Exception:
    //     The system could not set the thread to start on the specified processor.
    //
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public int IdealProcessor { set; }
#endif
    //
    // Summary:
    //     Gets or sets a value indicating whether the operating system should temporarily
    //     boost the priority of the associated thread whenever the main window of the
    //     thread's process receives the focus.
    //
    // Returns:
    //     true to boost the thread's priority when the user interacts with the process's
    //     interface; otherwise, false. The default is false.
    //
    // Exceptions:
    //   System.ComponentModel.Win32Exception:
    //     The priority boost information could not be retrieved.  -or- The priority
    //     boost information could not be set.
    //
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public bool PriorityBoostEnabled { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets the priority level of the thread.
    //
    // Returns:
    //     One of the System.Diagnostics.ThreadPriorityLevel values, specifying a range
    //     that bounds the thread's priority.
    //
    // Exceptions:
    //   System.ComponentModel.Win32Exception:
    //     The thread priority level information could not be retrieved. -or- The thread
    //     priority level could not be set.
    //
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public ThreadPriorityLevel PriorityLevel { get; set; }
#endif
    //
    // Summary:
    //     Gets the amount of time that the thread has spent running code inside the
    //     operating system core.
    //
    // Returns:
    //     A System.TimeSpan indicating the amount of time that the thread has spent
    //     running code inside the operating system core.
    //
    // Exceptions:
    //   System.ComponentModel.Win32Exception:
    //     The thread time could not be retrieved.
    //
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public TimeSpan PrivilegedProcessorTime { get; }
#endif
    //
    // Summary:
    //     Sets the processors on which the associated thread can run.
    //
    // Returns:
    //     An System.IntPtr that points to a set of bits, each of which represents a
    //     processor that the thread can run on.
    //
    // Exceptions:
    //   System.ComponentModel.Win32Exception:
    //     The processor affinity could not be set.
    //
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public IntPtr ProcessorAffinity { set; }
#endif
    //
    // Summary:
    //     Gets the memory address of the function that the operating system called
    //     that started this thread.
    //
    // Returns:
    //     The thread's starting address, which points to the application-defined function
    //     that the thread executes.
    //
    // Exceptions:
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public IntPtr StartAddress { get; }
#endif
    //
    // Summary:
    //     Gets the time that the operating system started the thread.
    //
    // Returns:
    //     A System.DateTime representing the time that was on the system when the operating
    //     system started the thread.
    //
    // Exceptions:
    //   System.ComponentModel.Win32Exception:
    //     The thread time could not be retrieved.
    //
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public DateTime StartTime { get; }
#endif
    //
    // Summary:
    //     Gets the current state of this thread.
    //
    // Returns:
    //     A System.Diagnostics.ThreadState that indicates the thread's execution, for
    //     example, running, waiting, or terminated.
    //
    // Exceptions:
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public ThreadState ThreadState { get; }
#endif
    //
    // Summary:
    //     Gets the total amount of time that this thread has spent using the processor.
    //
    // Returns:
    //     A System.TimeSpan that indicates the amount of time that the thread has had
    //     control of the processor.
    //
    // Exceptions:
    //   System.ComponentModel.Win32Exception:
    //     The thread time could not be retrieved.
    //
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public TimeSpan TotalProcessorTime { get; }
#endif
    //
    // Summary:
    //     Gets the amount of time that the associated thread has spent running code
    //     inside the application.
    //
    // Returns:
    //     A System.TimeSpan indicating the amount of time that the thread has spent
    //     running code inside the application, as opposed to inside the operating system
    //     core.
    //
    // Exceptions:
    //   System.ComponentModel.Win32Exception:
    //     The thread time could not be retrieved.
    //
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public TimeSpan UserProcessorTime { get; }
#endif
    //
    // Summary:
    //     Gets the reason that the thread is waiting.
    //
    // Returns:
    //     A System.Diagnostics.ThreadWaitReason representing the reason that the thread
    //     is in the wait state.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The thread is not in the wait state.
    //
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public ThreadWaitReason WaitReason { get; }
#endif
    // Summary:
    //     Resets the ideal processor for this thread to indicate that there is no single
    //     ideal processor. In other words, so that any processor is ideal.
    //
    // Exceptions:
    //   System.ComponentModel.Win32Exception:
    //     The ideal processor could not be reset.
    //
    //   System.PlatformNotSupportedException:
    //     The platform is Windows 98 or Windows Millennium Edition.
    //
    //   System.NotSupportedException:
    //     The process is on a remote computer.
#if false
    public void ResetIdealProcessor() {
    }
#endif
  }
}

#endif