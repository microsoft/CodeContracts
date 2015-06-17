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

// File System.Threading.WaitHandle.cs
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


namespace System.Threading
{
  abstract public partial class WaitHandle : MarshalByRefObject, IDisposable
  {
    #region Methods and constructors
    public virtual new void Close()
    {
    }

    public void Dispose()
    {
    }

    protected virtual new void Dispose(bool explicitDisposing)
    {
    }

    public static bool SignalAndWait(System.Threading.WaitHandle toSignal, System.Threading.WaitHandle toWaitOn, int millisecondsTimeout, bool exitContext)
    {
      return default(bool);
    }

    public static bool SignalAndWait(System.Threading.WaitHandle toSignal, System.Threading.WaitHandle toWaitOn, TimeSpan timeout, bool exitContext)
    {
      return default(bool);
    }

    public static bool SignalAndWait(System.Threading.WaitHandle toSignal, System.Threading.WaitHandle toWaitOn)
    {
      return default(bool);
    }

    public static bool WaitAll(System.Threading.WaitHandle[] waitHandles, TimeSpan timeout)
    {
      return default(bool);
    }

    public static bool WaitAll(System.Threading.WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
    {
      return default(bool);
    }

    public static bool WaitAll(System.Threading.WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
    {
      return default(bool);
    }

    public static bool WaitAll(System.Threading.WaitHandle[] waitHandles, int millisecondsTimeout)
    {
      return default(bool);
    }

    public static bool WaitAll(System.Threading.WaitHandle[] waitHandles)
    {
      return default(bool);
    }

    public static int WaitAny(System.Threading.WaitHandle[] waitHandles, int millisecondsTimeout)
    {
      return default(int);
    }

    public static int WaitAny(System.Threading.WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
    {
      return default(int);
    }

    public static int WaitAny(System.Threading.WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
    {
      return default(int);
    }

    public static int WaitAny(System.Threading.WaitHandle[] waitHandles)
    {
      return default(int);
    }

    public static int WaitAny(System.Threading.WaitHandle[] waitHandles, TimeSpan timeout)
    {
      return default(int);
    }

    protected WaitHandle()
    {
    }

    public virtual new bool WaitOne(int millisecondsTimeout, bool exitContext)
    {
      return default(bool);
    }

    public virtual new bool WaitOne(int millisecondsTimeout)
    {
      return default(bool);
    }

    public virtual new bool WaitOne(TimeSpan timeout, bool exitContext)
    {
      return default(bool);
    }

    public virtual new bool WaitOne(TimeSpan timeout)
    {
      return default(bool);
    }

    public virtual new bool WaitOne()
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public virtual new IntPtr Handle
    {
      get
      {
        return default(IntPtr);
      }
      set
      {
      }
    }

    public Microsoft.Win32.SafeHandles.SafeWaitHandle SafeWaitHandle
    {
      get
      {
        Contract.Ensures(Contract.Result<Microsoft.Win32.SafeHandles.SafeWaitHandle>() != null);

        return default(Microsoft.Win32.SafeHandles.SafeWaitHandle);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    protected readonly static IntPtr InvalidHandle;
    public static int WaitTimeout;
    #endregion
  }
}
