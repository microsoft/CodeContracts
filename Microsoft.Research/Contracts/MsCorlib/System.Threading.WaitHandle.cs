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

using System.Diagnostics.Contracts;
using System;

namespace System.Threading
{

  public class WaitHandle
  {
    protected WaitHandle() { }

#if !SILVERLIGHT
    extern public virtual IntPtr Handle
    {
      get;
    }
#endif

    public virtual void Close()
    {
    }

    public static int WaitAny(WaitHandle[] waitHandles)
    {
      Contract.Requires(waitHandles != null);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < waitHandles.Length);

      return default(int);
    }

    public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout)
    {
      Contract.Requires(waitHandles != null);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < waitHandles.Length || Contract.Result<int>() == 0x102);

      return default(int);
    }
#if !SILVERLIGHT
    public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
    {
      Contract.Requires(waitHandles != null);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < waitHandles.Length || Contract.Result<int>() == 0x102);

      return default(int);
    }
#endif
    public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout)
    {
      Contract.Requires(waitHandles != null);
      //Contract.Requires(waitHandles.Length <= System.Threading.WaitHandle.MAX_WAITHANDLES);
      //Contract.Requires(millisecondsTimeout >= -1);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < waitHandles.Length || Contract.Result<int>() == 0x102);

      return default(int);
    }
#if !SILVERLIGHT
    public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
    {
      Contract.Requires(waitHandles != null);
      //Contract.Requires(waitHandles.Length <= System.Threading.WaitHandle.MAX_WAITHANDLES);
      //Contract.Requires(millisecondsTimeout >= -1);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < waitHandles.Length || Contract.Result<int>() == 0x102);

      return default(int);
    }
#endif
    public static bool WaitAll(WaitHandle[] waitHandles)
    {
      Contract.Requires(waitHandles != null);

      return default(bool);
    }
#if !SILVERLIGHT
    public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
    {
      Contract.Requires(waitHandles != null);

      return default(bool);
    }
#endif

#if !SILVERLIGHT
    public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
    {
      Contract.Requires(waitHandles != null);
      //Contract.Requires(waitHandles.Length <= System.Threading.WaitHandle.MAX_WAITHANDLES);
      // Contract.Requires(millisecondsTimeout >= -1);

      return default(bool);
    }
#endif
    public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout)
    {
      Contract.Requires(waitHandles != null);

      return default(bool);
    }
    public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout)
    {
      Contract.Requires(waitHandles != null);
      //Contract.Requires(waitHandles.Length <= System.Threading.WaitHandle.MAX_WAITHANDLES);
      // Contract.Requires(millisecondsTimeout >= -1);

      return default(bool);
    }
#if false
    public bool WaitOne()
    {

      return default(bool);
    }
    public bool WaitOne(TimeSpan timeout, bool exitContext)
    {

      return default(bool);
    }
    public bool WaitOne(int millisecondsTimeout, bool exitContext)
    {

      return default(bool);
    }
#endif
  }
}
