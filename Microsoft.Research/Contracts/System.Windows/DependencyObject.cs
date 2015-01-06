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
using System.Text;
using System.Windows.Threading;
using System.Diagnostics.Contracts;

namespace System.Windows
{

  // Summary:
  //     Represents an object that participates in the Silverlight dependency property
  //     system.
  public abstract class DependencyObject
  {
    // Summary:
    //     Gets the System.Windows.Threading.Dispatcher this object is associated with.
    //
    // Returns:
    //     The System.Windows.Threading.Dispatcher this object is associated with.
    public Dispatcher Dispatcher
    {
      get
      {
        Contract.Ensures(Contract.Result<Dispatcher>() != null);
        return default(Dispatcher);
      }
    }

    // Summary:
    //     Determines whether the calling thread has access to this object.
    //
    // Returns:
    //     true if the calling thread has access to this object; otherwise, false.
    [Pure]
    extern public bool CheckAccess();
    //
    // Summary:
    //     Clears the local value of a property.
    //
    // Parameters:
    //   dp:
    //     The System.Windows.DependencyProperty identifier of the property to clear
    //     the value for.
    extern public void ClearValue(DependencyProperty dp);
    //
    // Summary:
    //     Returns any base value established for a Silverlight dependency property,
    //     which would apply in cases where an animation is not active.
    //
    // Parameters:
    //   dp:
    //     The identifier for the desired dependency property.
    //
    // Returns:
    //     The returned base value.
    extern public object GetAnimationBaseValue(DependencyProperty dp);
    //
    // Summary:
    //     Returns the current effective value of a dependency property from a System.Windows.DependencyObject.
    //
    // Parameters:
    //   dp:
    //     The System.Windows.DependencyProperty identifier of the property to retrieve
    //     the value for.
    //
    // Returns:
    //     Returns the current effective value.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The specified dp or its value was invalid, or the specified dp does not exist.
    extern public object GetValue(DependencyProperty dp);
    //
    // Summary:
    //     Returns the local value of a dependency property, if a local value is set.
    //
    // Parameters:
    //   dp:
    //     The System.Windows.DependencyProperty identifier of the property to retrieve
    //     the local value for.
    //
    // Returns:
    //     Returns the local value, or returns the sentinel value System.Windows.DependencyProperty.UnsetValue
    //     if no local value is set.
    public object ReadLocalValue(DependencyProperty dp)
    {
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
    //
    // Summary:
    //     Sets the local value of a dependency property on a System.Windows.DependencyObject.
    //
    // Parameters:
    //   dp:
    //     The identifier of the dependency property to set.
    //
    //   value:
    //     The new local value.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     Attempted to modify a read-only or private dependency property, or a property
    //     on a sealed System.Windows.DependencyObject.
    //
    //   System.ArgumentException:
    //     value was not the correct type as registered for the dp property.
    extern public void SetValue(DependencyProperty dp, object value);
  }
}
