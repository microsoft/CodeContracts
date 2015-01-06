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

// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//   Code adapted from Pex
// 
// ==--==

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Contracts
{
  /// <summary>
  /// Helper class to call the UI thread
  /// </summary>
  internal static class UIThreadInvoker
  {
    private static Control threadControl;

    /// <summary>
    /// Invoque this method in the UI thread.
    /// </summary>
    public static void Initialize()
    {
      Contract.Assume(UIThreadInvoker.threadControl == null);

      UIThreadInvoker.threadControl = new Control();
      IntPtr ptr1 = UIThreadInvoker.threadControl.Handle;
    }

    public static object Invoke(MethodInvoker method)
    {
      Contract.Requires(method != null);

      return Invoke((Delegate)method);
    }

    public static object Invoke(Delegate method)
    {
      Contract.Requires(method != null);

      object result = null;
      SafeMethodInvoker invoker = new SafeMethodInvoker(method, null);
      if (UIThreadInvoker.threadControl.InvokeRequired)
        result = UIThreadInvoker.threadControl.Invoke(new Invoker(invoker.Invoke));
      else
        result = invoker.Invoke();
      invoker.Rethrow();

      return result;

    }

    public static object Invoke(Delegate method, params object[] args)
    {
      Contract.Requires(method != null);

      object result = null;

      SafeMethodInvoker invoker = new SafeMethodInvoker(method, args);
      if (UIThreadInvoker.threadControl.InvokeRequired)
        result = UIThreadInvoker.threadControl.Invoke(new Invoker(invoker.Invoke));
      else
        result = invoker.Invoke();
      invoker.Rethrow();

      return result;
    }

    private delegate object Invoker();

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    private struct SafeMethodInvoker
    {
      private readonly Delegate method;
      private readonly object[] args;
      private Exception caughtException;

      [ContractInvariantMethod]
      private void ObjectInvariant() { 
        Contract.Invariant(method != null); 
      }

      public SafeMethodInvoker(Delegate method, object[] args)
      {
        Contract.Requires(method != null);

        this.method = method;
        this.args = args;
        this.caughtException = null;
      }

      public object Invoke()
      {
        try
        {
          return this.method.DynamicInvoke(this.args);
        }
        catch (TargetInvocationException ex)
        {
          this.caughtException = ex.InnerException;
          return null;
        }
        catch (Exception ex)
        {
          this.caughtException = ex;
          return null;
        }
      }

      public void Rethrow()
      {
        if (this.caughtException != null)
          throw new UIThreadInvokerException(
#if DEBUG
                        "Exception in UI thread: " + this.caughtException.ToString(),
#else
                        "An error occurred in the UI thread.",
#endif
                        caughtException);
      }
    }

    [Conditional("DEBUG")]
    public static void AssertUIThread()
    {
      Contract.Assume(!UIThreadInvoker.threadControl.InvokeRequired, "must be called from the UI thread");
    }
  }


  [global::System.Serializable]
  public class UIThreadInvokerException : Exception
  {
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //

    public UIThreadInvokerException() { }
    public UIThreadInvokerException(string message) : base(message) { }
    public UIThreadInvokerException(string message, Exception inner) : base(message, inner) { }
    protected UIThreadInvokerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
      : base(info, context) { }
  }
}
