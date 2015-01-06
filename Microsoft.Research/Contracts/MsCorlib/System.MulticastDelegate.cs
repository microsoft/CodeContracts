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
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System
{
  // Summary:
  //     Represents a multicast delegate; that is, a delegate that can have more than
  //     one element in its invocation list.
  public abstract class MulticastDelegate : Delegate
  {
    // Summary:
    //     Initializes a new instance of the System.MulticastDelegate class.
    //
    // Parameters:
    //   target:
    //     The object on which method is defined.
    //
    //   method:
    //     The name of the method for which a delegate is created.
    //
    // Exceptions:
    //   System.MemberAccessException:
    //     Cannot create an instance of an abstract class or this member was invoked
    //     with a late-binding mechanism.
#if SILVERLIGHT_4_0_WP
    extern internal MulticastDelegate();
#else
    extern protected MulticastDelegate(object target, string method);
#endif

#if !SILVERLIGHT
    //
    // Summary:
    //     Initializes a new instance of the System.MulticastDelegate class.
    //
    // Parameters:
    //   target:
    //     The type of object on which method is defined.
    //
    //   method:
    //     The name of the static method for which a delegate is created.
    //
    // Exceptions:
    //   System.MemberAccessException:
    //     Cannot create an instance of an abstract class or this member was invoked
    //     with a late-binding mechanism.
    extern protected MulticastDelegate(Type target, string method);
#endif

    // Summary:
    //     Determines whether two System.MulticastDelegate objects are not equal.
    //
    // Parameters:
    //   d1:
    //     The left operand.
    //
    //   d2:
    //     The right operand.
    //
    // Returns:
    //     true if d1 and d2 do not have the same invocation lists; otherwise false.
    //
    // Exceptions:
    //   System.MemberAccessException:
    //     Cannot create an instance of an abstract class or this member was invoked
    //     with a late-binding mechanism.
    public static bool operator !=(MulticastDelegate d1, MulticastDelegate d2)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Determines whether two System.MulticastDelegate objects are equal.
    //
    // Parameters:
    //   d1:
    //     The left operand.
    //
    //   d2:
    //     The right operand.
    //
    // Returns:
    //     true if d1 and d2 have the same invocation lists; otherwise false.
    //
    // Exceptions:
    //   System.MemberAccessException:
    //     Cannot create an instance of an abstract class or this member was invoked
    //     with a late-binding mechanism.
    public static bool operator ==(MulticastDelegate d1, MulticastDelegate d2)
    {
      return default(bool);
    }

  }
}