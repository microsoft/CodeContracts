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
using System.ComponentModel;
using System.Runtime;
using System.Security;
using System.Threading;
using System.Diagnostics.Contracts;

namespace System.Windows.Threading
{
  // Summary:
  //     Represents an operation that has been posted to the System.Windows.Threading.Dispatcher
  //     queue.
  public sealed class DispatcherOperation
  {
  }

  public enum DispatcherPriority
  {
  }

  public sealed class Dispatcher
  {
    private Dispatcher() { }

    public static Dispatcher CurrentDispatcher { get { Contract.Ensures(Contract.Result<Dispatcher>() != null); return null; } }

    extern public bool HasShutdownFinished { get; }

    extern public bool HasShutdownStarted { get; }

    //public DispatcherHooks Hooks { get; }
    public Thread Thread { get { Contract.Ensures(Contract.Result<Thread>() != null); return null; } }

    extern public event EventHandler ShutdownFinished;
    extern public event EventHandler ShutdownStarted;

    //extern public event DispatcherUnhandledExceptionEventHandler UnhandledException;

    //extern public event DispatcherUnhandledExceptionFilterEventHandler UnhandledExceptionFilter;

    public DispatcherOperation BeginInvoke(Delegate method, params object[] args)
    {
      Contract.Ensures(Contract.Result<DispatcherOperation>() != null);
      return null;
    }

    public DispatcherOperation BeginInvoke(DispatcherPriority priority, Delegate method)
    {
      Contract.Ensures(Contract.Result<DispatcherOperation>() != null);
      return null;
    }

    public DispatcherOperation BeginInvoke(Delegate method, DispatcherPriority priority, params object[] args)
    {
      Contract.Ensures(Contract.Result<DispatcherOperation>() != null);
      return null;
    }

    public DispatcherOperation BeginInvoke(DispatcherPriority priority, Delegate method, object arg)
    {
      Contract.Ensures(Contract.Result<DispatcherOperation>() != null);
      return null;
    }

    public DispatcherOperation BeginInvoke(DispatcherPriority priority, Delegate method, object arg, params object[] args)
    {
      Contract.Ensures(Contract.Result<DispatcherOperation>() != null);
      return null;
    }

    //    public void BeginInvokeShutdown(DispatcherPriority priority);
    //extern public bool CheckAccess();

    //public DispatcherProcessingDisabled DisableProcessing();

    //public static void ExitAllFrames();

    public static Dispatcher FromThread(Thread thread)
    {
      // May return null!

      return null;
    }

    //
    // Summary:
    //     Executes the specified delegate with the specified arguments synchronously
    //     on the thread the System.Windows.Threading.Dispatcher is associated with.
    //
    // Parameters:
    //   method:
    //     A delegate to a method that takes parameters specified in args, which is
    //     pushed onto the System.Windows.Threading.Dispatcher event queue.
    //
    //   args:
    //     An array of objects to pass as arguments to the given method. Can be null.
    //
    // Returns:
    //     The return value from the delegate being invoked or null if the delegate
    //     has no return value.
 extern   public object Invoke(Delegate method, params object[] args);

    //public object Invoke(DispatcherPriority priority, Delegate method);
//    public object Invoke(Delegate method, DispatcherPriority priority, params object[] args);
  //  public object Invoke(Delegate method, TimeSpan timeout, params object[] args);
//    public object Invoke(DispatcherPriority priority, Delegate method, object arg);
  //  public object Invoke(DispatcherPriority priority, TimeSpan timeout, Delegate method);
    //public object Invoke(Delegate method, TimeSpan timeout, DispatcherPriority priority, params object[] args);
//    public object Invoke(DispatcherPriority priority, Delegate method, object arg, params object[] args);
  //  public object Invoke(DispatcherPriority priority, TimeSpan timeout, Delegate method, object arg);
    // public object Invoke(DispatcherPriority priority, TimeSpan timeout, Delegate method, object arg, params object[] args);
    //
    extern public void InvokeShutdown();
//    public static void PushFrame(DispatcherFrame frame);
  extern  public static void Run();
//    public static void ValidatePriority(DispatcherPriority priority, string parameterName);
  //  public void VerifyAccess();
  }
}