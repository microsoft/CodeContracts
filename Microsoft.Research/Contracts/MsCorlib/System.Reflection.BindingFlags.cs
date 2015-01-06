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
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Reflection {
  // Summary:
  //     Specifies flags that control binding and the way in which the search for
  //     members and types is conducted by reflection.
  public enum BindingFlags {
    // Summary:
    //     Specifies no binding flag.
    Default = 0,
    //
    // Summary:
    //     Specifies that the case of the member name should not be considered when
    //     binding.
    IgnoreCase = 1,
    //
    // Summary:
    //     Specifies that only members declared at the level of the supplied type's
    //     hierarchy should be considered. Inherited members are not considered.
    DeclaredOnly = 2,
    //
    // Summary:
    //     Specifies that instance members are to be included in the search.
    Instance = 4,
    //
    // Summary:
    //     Specifies that static members are to be included in the search.
    Static = 8,
    //
    // Summary:
    //     Specifies that public members are to be included in the search.
    Public = 16,
    //
    // Summary:
    //     Specifies that non-public members are to be included in the search.
    NonPublic = 32,
    //
    // Summary:
    //     Specifies that public and protected static members up the hierarchy should
    //     be returned. Private static members in inherited classes are not returned.
    //     Static members include fields, methods, events, and properties. Nested types
    //     are not returned.
    FlattenHierarchy = 64,
    //
    // Summary:
    //     Specifies that a method is to be invoked. This must not be a constructor
    //     or a type initializer.
    InvokeMethod = 256,
    //
    // Summary:
    //     Specifies that Reflection should create an instance of the specified type.
    //     Calls the constructor that matches the given arguments. The supplied member
    //     name is ignored. If the type of lookup is not specified, (Instance | Public)
    //     will apply. It is not possible to call a type initializer.
    CreateInstance = 512,
    //
    // Summary:
    //     Specifies that the value of the specified field should be returned.
    GetField = 1024,
    //
    // Summary:
    //     Specifies that the value of the specified field should be set.
    SetField = 2048,
    //
    // Summary:
    //     Specifies that the value of the specified property should be returned.
    GetProperty = 4096,
    //
    // Summary:
    //     Specifies that the value of the specified property should be set. For COM
    //     properties, specifying this binding flag is equivalent to specifying PutDispProperty
    //     and PutRefDispProperty.
    SetProperty = 8192,
    //
    // Summary:
    //     Specifies that the PROPPUT member on a COM object should be invoked. PROPPUT
    //     specifies a property-setting function that uses a value. Use PutDispProperty
    //     if a property has both PROPPUT and PROPPUTREF and you need to distinguish
    //     which one is called.
    PutDispProperty = 16384,
    //
    // Summary:
    //     Specifies that the PROPPUTREF member on a COM object should be invoked. PROPPUTREF
    //     specifies a property-setting function that uses a reference instead of a
    //     value. Use PutRefDispProperty if a property has both PROPPUT and PROPPUTREF
    //     and you need to distinguish which one is called.
    PutRefDispProperty = 32768,
    //
    // Summary:
    //     Specifies that types of the supplied arguments must exactly match the types
    //     of the corresponding formal parameters. Reflection throws an exception if
    //     the caller supplies a non-null Binder object, since that implies that the
    //     caller is supplying BindToXXX implementations that will pick the appropriate
    //     method.
    ExactBinding = 65536,
    //
    // Summary:
    //     Not implemented.
    SuppressChangeType = 131072,
    //
    // Summary:
    //     Returns the set of members whose parameter count matches the number of supplied
    //     arguments. This binding flag is used for methods with parameters that have
    //     default values and methods with variable arguments (varargs). This flag should
    //     only be used with System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[]).
    OptionalParamBinding = 262144,
    //
    // Summary:
    //     Used in COM interop to specify that the return value of the member can be
    //     ignored.
    IgnoreReturn = 16777216,
  }
}

