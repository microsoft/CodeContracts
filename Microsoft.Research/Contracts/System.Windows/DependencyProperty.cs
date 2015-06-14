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
using System.Diagnostics.Contracts;

namespace System.Windows
{

  // Summary:
  //     Represents a dependency property that is registered with the Silverlight dependency
  //     property system. Dependency properties provide support for value expressions,
  //     data binding, animation, and property change notification.
  public class DependencyProperty
  {
    // Summary:
    //     Specifies a static value that is used by the property system rather than
    //     null to indicate that the property exists, but does not have its value set
    //     by the property system.
    //
    // Returns:
    //     The sentinel value for an unset value.
    // public static readonly object UnsetValue;

    // Summary:
    //     Registers a dependency property with the specified property name, property
    //     type, owner type, and property metadata for the property.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    //   typeMetadata:
    //     A property metadata instance. This can contain a System.Windows.PropertyChangedCallback
    //     implementation reference.
    //
    // Returns:
    //     A dependency property identifier that should be used to set the value of
    //     a public static readonly field in your class. The identifier is then used
    //     both by your own code and any third-party user code to reference the dependency
    //     property later, for operations such as setting its value programmatically,
    //     or attaching a System.Windows.Data.Binding in code.
    public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata)
    {
      Contract.Ensures(Contract.Result<DependencyProperty>() != null);
      return default(DependencyProperty);
    }
    //
    // Summary:
    //     Registers an attached dependency property with the specified property name,
    //     property type, owner type, and property metadata for the property.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    //   defaultMetadata:
    //     A property metadata instance. This can contain a System.Windows.PropertyChangedCallback
    //     implementation reference.
    //
    // Returns:
    //     A dependency property identifier that should be used to set the value of
    //     a public static readonly field in your class. That identifier is then used
    //     to reference the attached property later, for operations such as setting
    //     its value programmatically, or attaching a System.Windows.Data.Binding.
    public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata)
    {
      Contract.Ensures(Contract.Result<DependencyProperty>() != null);
      return default(DependencyProperty);
    }
  }

}
