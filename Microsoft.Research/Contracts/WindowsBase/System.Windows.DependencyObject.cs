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
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime;
using System.Windows.Threading;

namespace System.Windows
{
  public class DependencyObject : DispatcherObject
  {
    //public DependencyObject() { }

    // Summary:
    //     Gets the System.Windows.DependencyObjectType that wraps the CLR type of this
    //     instance.
    //
    // Returns:
    //     A System.Windows.DependencyObjectType that wraps the CLR type of this instance.
    //public DependencyObjectType DependencyObjectType { get; }
    //
    // Summary:
    //     Gets a value that indicates whether this instance is currently sealed (read-only).
    //
    // Returns:
    //     true if this instance is sealed; otherwise, false.
    extern public bool IsSealed { get; }

    // Summary:
    //     Clears the local value of a property. The property to be cleared is specified
    //     by a System.Windows.DependencyProperty identifier.
    //
    // Parameters:
    //   dp:
    //     The dependency property to be cleared, identified by a System.Windows.DependencyProperty
    //     object reference.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     Attempted to call System.Windows.DependencyObject.ClearValue(System.Windows.DependencyProperty)
    //     on a sealed System.Windows.DependencyObject.
    public void ClearValue(DependencyProperty dp)
    {
      Contract.Requires(dp != null);
    }
    //
    // Summary:
    //     Clears the local value of a read-only property. The property to be cleared
    //     is specified by a System.Windows.DependencyPropertyKey.
    //
    // Parameters:
    //   key:
    //     The key for the dependency property to be cleared.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     Attempted to call System.Windows.DependencyObject.ClearValue(System.Windows.DependencyProperty)
    //     on a sealed System.Windows.DependencyObject.
    //public void ClearValue(DependencyPropertyKey key);
    //
    // Summary:
    //     Coerces the value of the specified dependency property. This is accomplished
    //     by invoking any System.Windows.CoerceValueCallback function specified in
    //     property metadata for the dependency property as it exists on the calling
    //     System.Windows.DependencyObject.
    //
    // Parameters:
    //   dp:
    //     The identifier for the dependency property to coerce.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The specified dp or its value were invalid or do not exist.
    public void CoerceValue(DependencyProperty dp)
    {
      Contract.Requires(dp != null);
    }
    //public LocalValueEnumerator GetLocalValueEnumerator();
    //
    // Summary:
    //     Returns the current effective value of a dependency property on this instance
    //     of a System.Windows.DependencyObject.
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
    public object GetValue(DependencyProperty dp)
    {
      Contract.Requires(dp != null);
      Contract.Ensures(Contract.Result<object>() != null);

      return null;
    }

    //
    // Summary:
    //     Re-evaluates the effective value for the specified dependency property
    //
    // Parameters:
    //   dp:
    //     The System.Windows.DependencyProperty identifier of the property to invalidate.
    public void InvalidateProperty(DependencyProperty dp)
    {
      Contract.Requires(dp != null);
    }

    //
    // Summary:
    //     Invoked whenever the effective value of any dependency property on this System.Windows.DependencyObject
    //     has been updated. The specific dependency property that changed is reported
    //     in the event data.
    //
    // Parameters:
    //   e:
    //     Event data that will contain the dependency property identifier of interest,
    //     the property metadata for the type, and old and new values.
//    protected virtual void OnPropertyChanged(DependencyPropertyChangedEventArgs e);
    //
    // Summary:
    //     Returns the local value of a dependency property, if it exists.
    //
    // Parameters:
    //   dp:
    //     The System.Windows.DependencyProperty identifier of the property to retrieve
    //     the value for.
    //
    // Returns:
    //     Returns the local value, or returns the sentinel value System.Windows.DependencyProperty.UnsetValue
    //     if no local value is set.
    public object ReadLocalValue(DependencyProperty dp)
    {
      Contract.Requires(dp != null);
      Contract.Ensures(Contract.Result<object>() != null);

      return null;
    }

    //
    // Summary:
    //     Sets the value of a dependency property without changing its value source.
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
    //     Attempted to modify a read-only dependency property, or a property on a sealed
    //     System.Windows.DependencyObject.
    //
    //   System.ArgumentException:
    //     value was not the correct type as registered for the dp property.

    /*public void SetCurrentValue(DependencyProperty dp, object value)
    {
      Contract.Requires(dp != null);
    }
    */
    //
    // Summary:
    //     Sets the local value of a dependency property, specified by its dependency
    //     property identifier.
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
    //     Attempted to modify a read-only dependency property, or a property on a sealed
    //     System.Windows.DependencyObject.
    //
    //   System.ArgumentException:
    //     value was not the correct type as registered for the dp property.
    public void SetValue(DependencyProperty dp, object value)
    {
      Contract.Requires(dp != null);
    }

    //
    // Summary:
    //     Sets the local value of a read-only dependency property, specified by the
    //     System.Windows.DependencyPropertyKey identifier of the dependency property.
    //
    // Parameters:
    //   key:
    //     The System.Windows.DependencyPropertyKey identifier of the property to set.
    //
    //   value:
    //     The new local value.
    //public void SetValue(DependencyPropertyKey key, object value);
    //
    // Summary:
    //     Returns a value that indicates whether serialization processes should serialize
    //     the value for the provided dependency property.
    //
    // Parameters:
    //   dp:
    //     The identifier for the dependency property that should be serialized.
    //
    // Returns:
    //     true if the dependency property that is supplied should be value-serialized;
    //     otherwise, false.
   // protected internal virtual bool ShouldSerializeProperty(DependencyProperty dp);
  }
}
