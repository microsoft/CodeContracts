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

using System;
using System.Globalization;
using System.Diagnostics.Contracts;

namespace System.Threading
{

  public delegate void ThreadStart();
  public class Thread
  {

#if !SILVERLIGHT
    public Thread(ThreadStart start) { }
#else
    extern internal Thread();
#endif

    public CultureInfo CurrentUICulture
    {
      get
      {
        Contract.Ensures(Contract.Result<CultureInfo>() != null);
        return default(CultureInfo);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    extern public bool IsBackground
    {
      get;
      set;
    }

#if false
    public static System.Runtime.Remoting.Contexts.Context CurrentContext
    {
      get;
    }

    public static System.Security.Principal.IPrincipal CurrentPrincipal
    {
      get;
      set;
    }
#endif

#if !SILVERLIGHT
    extern public bool IsThreadPoolThread
    {
      get;
    }
#endif

    public static Thread CurrentThread
    {
      get
      {
        Contract.Ensures(Contract.Result<Thread>() != null);
        return default(Thread);
      }
    }

#if false
    public ThreadState ThreadState
    {
      get;
    }


    public bool IsAlive
    {
      get;
    }
#endif

    public string Name
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

#if false
    public ApartmentState ApartmentState
    {
      get;
      set;
    }


    public ThreadPriority Priority
    {
      get;
      set;
    }
#endif

    public System.Globalization.CultureInfo CurrentCulture
    {
      get
      {
        Contract.Ensures(Contract.Result<CultureInfo>() != null);
        return default(CultureInfo);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

#if false
    public static void MemoryBarrier()
    {

    }
    public static void VolatileWrite(ref object arg0, object arg1)
    {

    }
    public static void VolatileWrite(ref double arg0, double arg1)
    {

    }
    public static void VolatileWrite(ref Single arg0, Single arg1)
    {

    }
    public static void VolatileWrite(ref UInt64 arg0, UInt64 arg1)
    {

    }
    public static void VolatileWrite(ref UInt32 arg0, UInt32 arg1)
    {

    }
    public static void VolatileWrite(ref int arg0, int arg1)
    {

    }
    //public static void VolatileWrite (ref UInt32 arg0, UInt32 arg1) {  WHERE DID THE REDUNDANT ONE COME FROM?

    //}
    public static void VolatileWrite(ref UInt16 arg0, UInt16 arg1)
    {

    }
    public static void VolatileWrite(ref SByte arg0, SByte arg1)
    {

    }
    public static void VolatileWrite(ref Int64 arg0, Int64 arg1)
    {

    }
    public static void VolatileWrite(ref IntPtr arg0, IntPtr arg1)
    {

    }
    public static void VolatileWrite(ref Int16 arg0, Int16 arg1)
    {

    }
    public static void VolatileWrite(ref byte arg0, byte arg1)
    {

    }
    public static object VolatileRead(ref object arg0)
    {

      return default(object);
    }
    public static double VolatileRead(ref double arg0)
    {

      return default(double);
    }
    public static Single VolatileRead(ref Single arg0)
    {

      return default(Single);
    }
    public static UInt64 VolatileRead(ref UInt64 arg0)
    {

      return default(UInt64);
    }
    public static UInt32 VolatileRead(ref UInt32 arg0)
    {

      return default(UInt32);
    }
    public static int VolatileRead(ref int arg0)
    {

      return default(int);
    }
    //public static UInt32 VolatileRead (ref UInt32 arg0) {   WHERE DID THE REDUNDANT ONE COME FROM?

    //  return default(UInt32);
    //}
    public static UInt16 VolatileRead(ref UInt16 arg0)
    {

      return default(UInt16);
    }
    public static SByte VolatileRead(ref SByte arg0)
    {

      return default(SByte);
    }
    public static Int64 VolatileRead(ref Int64 arg0)
    {

      return default(Int64);
    }
    public static int VolatileRead(ref IntPtr arg0)
    {

      return default(int);
    }
    public static Int16 VolatileRead(ref Int16 arg0)
    {

      return default(Int16);
    }
    public static byte VolatileRead(ref byte arg0)
    {

      return default(byte);
    }
    public static int GetDomainID()
    {

      return default(int);
    }
    public static AppDomain GetDomain()
    {

      return default(AppDomain);
    }
    public static void SetData(LocalDataStoreSlot slot, object data)
    {

    }
    public static object GetData(LocalDataStoreSlot slot)
    {

      return default(object);
    }
    public static void FreeNamedDataSlot(string name)
    {

    }
    public static LocalDataStoreSlot GetNamedDataSlot(string name)
    {

      return default(LocalDataStoreSlot);
    }
    public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
    {

      return default(LocalDataStoreSlot);
    }
    public static LocalDataStoreSlot AllocateDataSlot()
    {

      return default(LocalDataStoreSlot);
    }
    public static void SpinWait(int arg0)
    {

    }
    public static void Sleep(TimeSpan timeout)
    {

    }
    public static void Sleep(int arg0)
    {

    }
    public bool Join(TimeSpan timeout)
    {

      return default(bool);
    }
    public bool Join(int arg0)
    {

      return default(bool);
    }
    public void Join()
    {

    }
    public void Interrupt()
    {

    }
    public void Resume()
    {

    }
    public void Suspend()
    {

    }
    public static void ResetAbort()
    {

    }
    public void Abort()
    {

    }
    public void Abort(object stateInfo)
    {

    }
    public CompressedStack GetCompressedStack()
    {

      return default(CompressedStack);
    }
    public void SetCompressedStack(CompressedStack stack)
    {

    }
    public void Start()
    {

    }
    public Thread([Delayed] ThreadStart start)
    {
      Contract.Requires(start != null);
      return default(Thread);
    }
#endif

  }
}
