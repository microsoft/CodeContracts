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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Windows
{
  // Summary:
  //     Provides data for a System.Windows.PropertyChangedCallback implementation.
  public struct DependencyPropertyChangedEventArgs
  {

    // Summary:
    //     Gets the value of the property after the change.
    //
    // Returns:
    //     The property value after the change.
    public object NewValue
    {
      get
      {
        return default(object);
      }
    }
    //
    // Summary:
    //     Gets the value of the property before the change.
    //
    // Returns:
    //     The property value before the change.
    public object OldValue
    {
      get
      {
        return default(object);
      }
    }

    //
    // Summary:
    //     Gets the identifier for the dependency property where the value change occurred.
    //
    // Returns:
    //     The identifier field of the dependency property where the value change occurred.
    public DependencyProperty Property
    {
      get
      {
        Contract.Ensures(Contract.Result<DependencyProperty>() != null);
        return default(DependencyProperty);
      }
    }
  }

  // Summary:
  //     Represents the callback that is invoked when the effective property value
  //     of a dependency property changes.
  //
  // Parameters:
  //   d:
  //     The System.Windows.DependencyObject on which the property has changed value.
  //
  //   e:
  //     Event data that is issued by any event that tracks changes to the effective
  //     value of this property.
  public delegate void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e);

  // Summary:
  //     Defines certain behavior aspects of a dependency property, including conditions
  //     it was registered with.
  public class PropertyMetadata
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.PropertyMetadata class,
    //     using a provided property default value.
    //
    // Parameters:
    //   defaultValue:
    //     A default value for the property where this System.Windows.PropertyMetadata
    //     is applied.
    extern public PropertyMetadata(object defaultValue);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.PropertyMetadata class,
    //     using the supplied property changed callback reference.
    //
    // Parameters:
    //   propertyChangedCallback:
    //     A reference to the callback to call for property changed behavior.
    extern public PropertyMetadata(PropertyChangedCallback propertyChangedCallback);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.PropertyMetadata class,
    //     using a provided property default value and property changed callback reference.
    //
    // Parameters:
    //   defaultValue:
    //     A default value for the property where this System.Windows.PropertyMetadata
    //     is applied.
    //
    //   propertyChangedCallback:
    //     A reference to the callback to call for property changed behavior.
    extern public PropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback);
  }
  
}
