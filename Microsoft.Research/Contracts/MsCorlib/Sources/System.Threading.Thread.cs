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

// File System.Threading.Thread.cs
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
  sealed public partial class Thread : System.Runtime.ConstrainedExecution.CriticalFinalizerObject, System.Runtime.InteropServices._Thread
  {
    #region Methods and constructors
    public void Abort()
    {
    }

    public void Abort(Object stateInfo)
    {
    }

    public static LocalDataStoreSlot AllocateDataSlot()
    {
      return default(LocalDataStoreSlot);
    }

    public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
    {
      return default(LocalDataStoreSlot);
    }

    public static void BeginCriticalRegion()
    {
    }

    public static void BeginThreadAffinity()
    {
    }

    public void DisableComObjectEagerCleanup()
    {
    }

    public static void EndCriticalRegion()
    {
    }

    public static void EndThreadAffinity()
    {
    }

    public static void FreeNamedDataSlot(string name)
    {
    }

    public ApartmentState GetApartmentState()
    {
      return default(ApartmentState);
    }

    public CompressedStack GetCompressedStack()
    {
      Contract.Ensures(false);

      return default(CompressedStack);
    }

    public static Object GetData(LocalDataStoreSlot slot)
    {
      return default(Object);
    }

    public static AppDomain GetDomain()
    {
      return default(AppDomain);
    }

    public static int GetDomainID()
    {
      return default(int);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public static LocalDataStoreSlot GetNamedDataSlot(string name)
    {
      return default(LocalDataStoreSlot);
    }

    public void Interrupt()
    {
    }

    public void Join()
    {
    }

    public bool Join(int millisecondsTimeout)
    {
      return default(bool);
    }

    public bool Join(TimeSpan timeout)
    {
      return default(bool);
    }

    public static void MemoryBarrier()
    {
    }

    public static void ResetAbort()
    {
    }

    public void Resume()
    {
    }

    public void SetApartmentState(ApartmentState state)
    {
    }

    public void SetCompressedStack(CompressedStack stack)
    {
      Contract.Ensures(false);
    }

    public static void SetData(LocalDataStoreSlot slot, Object data)
    {
    }

    public static void Sleep(int millisecondsTimeout)
    {
    }

    public static void Sleep(TimeSpan timeout)
    {
    }

    public static void SpinWait(int iterations)
    {
    }

    public void Start()
    {
    }

    public void Start(Object parameter)
    {
    }

    public void Suspend()
    {
    }

    void System.Runtime.InteropServices._Thread.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._Thread.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._Thread.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._Thread.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }

    public Thread(ParameterizedThreadStart start)
    {
    }

    public Thread(ThreadStart start)
    {
    }

    public Thread(ParameterizedThreadStart start, int maxStackSize)
    {
    }

    public Thread(ThreadStart start, int maxStackSize)
    {
    }

    public bool TrySetApartmentState(ApartmentState state)
    {
      return default(bool);
    }

    public static uint VolatileRead(ref uint address)
    {
      Contract.Ensures(Contract.Result<uint>() == address);

      return default(uint);
    }

    public static ushort VolatileRead(ref ushort address)
    {
      Contract.Ensures(Contract.Result<ushort>() == address);

      return default(ushort);
    }

    public static ulong VolatileRead(ref ulong address)
    {
      Contract.Ensures(Contract.Result<ulong>() == address);

      return default(ulong);
    }

    public static UIntPtr VolatileRead(ref UIntPtr address)
    {
      Contract.Ensures(Contract.Result<System.UIntPtr>() == address);

      return default(UIntPtr);
    }

    public static IntPtr VolatileRead(ref IntPtr address)
    {
      Contract.Ensures(Contract.Result<System.IntPtr>() == address);

      return default(IntPtr);
    }

    public static byte VolatileRead(ref byte address)
    {
      Contract.Ensures(Contract.Result<byte>() == address);

      return default(byte);
    }

    public static long VolatileRead(ref long address)
    {
      Contract.Ensures(Contract.Result<long>() == address);

      return default(long);
    }

    public static short VolatileRead(ref short address)
    {
      Contract.Ensures(Contract.Result<short>() == address);

      return default(short);
    }

    public static sbyte VolatileRead(ref sbyte address)
    {
      Contract.Ensures(Contract.Result<sbyte>() == address);

      return default(sbyte);
    }

    public static int VolatileRead(ref int address)
    {
      Contract.Ensures(Contract.Result<int>() == address);

      return default(int);
    }

    public static Object VolatileRead(ref Object address)
    {
      Contract.Ensures(Contract.Result<System.Object>() == address);

      return default(Object);
    }

    public static double VolatileRead(ref double address)
    {
      Contract.Ensures(Contract.Result<double>() == address);

      return default(double);
    }

    public static float VolatileRead(ref float address)
    {
      Contract.Ensures(Contract.Result<float>() == address);

      return default(float);
    }

    public static void VolatileWrite(ref long address, long value)
    {
    }

    public static void VolatileWrite(ref sbyte address, sbyte value)
    {
    }

    public static void VolatileWrite(ref uint address, uint value)
    {
    }

    public static void VolatileWrite(ref byte address, byte value)
    {
    }

    public static void VolatileWrite(ref short address, short value)
    {
    }

    public static void VolatileWrite(ref int address, int value)
    {
    }

    public static void VolatileWrite(ref ushort address, ushort value)
    {
    }

    public static void VolatileWrite(ref float address, float value)
    {
    }

    public static void VolatileWrite(ref double address, double value)
    {
    }

    public static void VolatileWrite(ref Object address, Object value)
    {
    }

    public static void VolatileWrite(ref ulong address, ulong value)
    {
    }

    public static void VolatileWrite(ref IntPtr address, IntPtr value)
    {
    }

    public static void VolatileWrite(ref UIntPtr address, UIntPtr value)
    {
    }

    public static bool Yield()
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public ApartmentState ApartmentState
    {
      get
      {
        return default(ApartmentState);
      }
      set
      {
      }
    }

    public static System.Runtime.Remoting.Contexts.Context CurrentContext
    {
      get
      {
        return default(System.Runtime.Remoting.Contexts.Context);
      }
    }

    public System.Globalization.CultureInfo CurrentCulture
    {
      get
      {
        return default(System.Globalization.CultureInfo);
      }
      set
      {
      }
    }

    public static System.Security.Principal.IPrincipal CurrentPrincipal
    {
      get
      {
        return default(System.Security.Principal.IPrincipal);
      }
      set
      {
      }
    }

    public static System.Threading.Thread CurrentThread
    {
      get
      {
        return default(System.Threading.Thread);
      }
    }

    public System.Globalization.CultureInfo CurrentUICulture
    {
      get
      {
        return default(System.Globalization.CultureInfo);
      }
      set
      {
      }
    }

    public ExecutionContext ExecutionContext
    {
      get
      {
        return default(ExecutionContext);
      }
    }

    public bool IsAlive
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsBackground
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsThreadPoolThread
    {
      get
      {
        return default(bool);
      }
    }

    public int ManagedThreadId
    {
      get
      {
        return default(int);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public ThreadPriority Priority
    {
      get
      {
        return default(ThreadPriority);
      }
      set
      {
      }
    }

    public ThreadState ThreadState
    {
      get
      {
        return default(ThreadState);
      }
    }
    #endregion
  }
}
