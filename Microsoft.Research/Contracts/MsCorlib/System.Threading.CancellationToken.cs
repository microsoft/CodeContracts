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

#if NETFRAMEWORK_4_0

namespace System.Threading
{
  // Summary:
  //     Propogates notification that operations should be canceled.
  public struct CancellationToken
  {
    //
    // Summary:
    //     Initializes the System.Threading.CancellationToken.
    //
    // Parameters:
    //   canceled:
    //     The canceled state for the token.
    //public CancellationToken(bool canceled) { }

    // Summary:
    //     Determines whether two System.Threading.CancellationToken instances are not
    //     equal.
    //
    // Parameters:
    //   left:
    //     The first instance.
    //
    //   right:
    //     The second instance.
    //
    // Returns:
    //     True if the instances are not equal; otherwise, false.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     An associated System.Threading.CancellationTokenSource has been disposed.
    //public static bool operator !=(CancellationToken left, CancellationToken right);
    //
    // Summary:
    //     Determines whether two System.Threading.CancellationToken instances are equal.
    //
    // Parameters:
    //   left:
    //     The first instance.
    //
    //   right:
    //     The second instance.
    //
    // Returns:
    //     True if the instances are equal; otherwise, false.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     An associated System.Threading.CancellationTokenSource has been disposed.
    //public static bool operator ==(CancellationToken left, CancellationToken right);

    // Summary:
    //     Gets whether this token is capable of being in the canceled state.
    //
    // Returns:
    //     true if this token is capable of being in the canceled state; otherwise false.
    //public bool CanBeCanceled { get; }
    //
    // Summary:
    //     Gets whether cancellation has been requested for this token.
    //
    // Returns:
    //     true if cancellation has been requested for this token; otherwise false.
    //public bool IsCancellationRequested { get; }
    //
    // Summary:
    //     Returns an empty CancellationToken value.
    //
    // Returns:
    //     Returns an empty CancellationToken value.
    //public static CancellationToken None { get; }
    //
    // Summary:
    //     Gets a System.Threading.WaitHandle that is signaled when the token is canceled.
    //
    // Returns:
    //     A System.Threading.WaitHandle that is signaled when the token is canceled.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The associated System.Threading.CancellationTokenSource has been disposed.
    //public WaitHandle WaitHandle { get; }

    // Summary:
    //     Determines whether the current System.Threading.CancellationToken instance
    //     is equal to the specified token.
    //
    // Parameters:
    //   other:
    //     The other System.Threading.CancellationToken to which to compare this instance.
    //
    // Returns:
    //     True if the instances are equal; otherwise, false. Two tokens are equal if
    //     they are associated with the same System.Threading.CancellationTokenSource
    //     or if they were both constructed from public CancellationToken constructors
    //     and their System.Threading.CancellationToken.IsCancellationRequested values
    //     are equal.
    //public bool Equals(CancellationToken other);
    //
    //
    // Summary:
    //     Registers a delegate that will be called when this System.Threading.CancellationToken
    //     is canceled.
    //
    // Parameters:
    //   callback:
    //     The delegate to be executed when the System.Threading.CancellationToken is
    //     canceled.
    //
    // Returns:
    //     The System.Threading.CancellationTokenRegistration instance that can be used
    //     to deregister the callback.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The associated System.Threading.CancellationTokenSource has been disposed.
    //
    //   System.ArgumentNullException:
    //     callback is null.
    //public CancellationTokenRegistration Register(Action callback);
    //
    // Summary:
    //     Registers a delegate that will be called when this System.Threading.CancellationToken
    //     is canceled.
    //
    // Parameters:
    //   callback:
    //     The delegate to be executed when the System.Threading.CancellationToken is
    //     canceled.
    //
    //   state:
    //     The state to pass to the callback when the delegate is invoked. This may
    //     be null.
    //
    // Returns:
    //     The System.Threading.CancellationTokenRegistration instance that can be used
    //     to deregister the callback.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The associated System.Threading.CancellationTokenSource has been disposed.
    //
    //   System.ArgumentNullException:
    //     callback is null.
    //public CancellationTokenRegistration Register(Action<object> callback, object state);
    //
    // Summary:
    //     Registers a delegate that will be called when this System.Threading.CancellationToken
    //     is canceled.
    //
    // Parameters:
    //   callback:
    //     The delegate to be executed when the System.Threading.CancellationToken is
    //     canceled.
    //
    //   useSynchronizationContext:
    //     A Boolean value that indicates whether to capture the current System.Threading.SynchronizationContext
    //     and use it when invoking the callback.
    //
    // Returns:
    //     The System.Threading.CancellationTokenRegistration instance that can be used
    //     to deregister the callback.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The associated System.Threading.CancellationTokenSource has been disposed.
    //
    //   System.ArgumentNullException:
    //     callback is null.
    //public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext);
    //
    // Summary:
    //     Registers a delegate that will be called when this System.Threading.CancellationToken
    //     is canceled.
    //
    // Parameters:
    //   callback:
    //     The delegate to be executed when the System.Threading.CancellationToken is
    //     canceled.
    //
    //   state:
    //     The state to pass to the callback when the delegate is invoked. This may
    //     be null.
    //
    //   useSynchronizationContext:
    //     A Boolean value that indicates whether to capture the current System.Threading.SynchronizationContext
    //     and use it when invoking the callback.
    //
    // Returns:
    //     The System.Threading.CancellationTokenRegistration instance that can be used
    //     to deregister the callback.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The associated System.Threading.CancellationTokenSource has been disposed.
    //
    //   System.ArgumentNullException:
    //     callback is null.
    //public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext);
    //
    // Summary:
    //     Throws a System.OperationCanceledException if this token has had cancellation
    //     requested.
    //
    // Exceptions:
    //   System.OperationCanceledException:
    //     The token has had cancellation requested.
    //
    //   System.ObjectDisposedException:
    //     The associated System.Threading.CancellationTokenSource has been disposed.
    //public void ThrowIfCancellationRequested();
  }
}
#endif