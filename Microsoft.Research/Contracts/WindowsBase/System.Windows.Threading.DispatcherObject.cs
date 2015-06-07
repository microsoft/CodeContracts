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
using System.Diagnostics.Contracts;

namespace System.Windows.Threading
{
  public abstract class DispatcherObject
  {
    protected DispatcherObject() { }

    // DetachFromDispatcher is e.g. called by Freezable.Freeze()
    //
    // This method allows certain derived classes to break the dispatcher affinity
    // of our objects.
    // [FriendAccessAllowed] // Built into Base, also used by Framework.
    internal void DetachFromDispatcher()
    {
        // _dispatcher = null;
    } 

    public Dispatcher Dispatcher
    {
        get
        {
            // Note: a DispatcherObject that is not associated with a
            // dispatcher is considered to be free-threaded.
            // => maybe null!
            return null;
        }
    }

    extern public bool CheckAccess();

    extern public void VerifyAccess();
  }
}
