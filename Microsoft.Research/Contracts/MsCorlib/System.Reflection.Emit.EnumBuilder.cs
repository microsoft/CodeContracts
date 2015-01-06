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
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  // Summary:
  //     Describes and represents an enumeration type.
  public sealed class EnumBuilder : Type // , _EnumBuilder
  {
    public TypeToken TypeToken { get; }
    //
    // Summary:
    //     Returns the underlying field for this enum.
    //
    // Returns:
    //     Read-only. The underlying field for this enum.
    public FieldBuilder UnderlyingField { get; }

    // Summary:
    //     Creates a System.Type object for this enum.
    //
    // Returns:
    //     A System.Type object for this enum.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This type has been previously created.-or- The enclosing type has not been
    //     created.
    public Type CreateType();
    //
    // Summary:
    //     Defines the named static field in an enumeration type with the specified
    //     constant value.
    //
    // Parameters:
    //   literalName:
    //     The name of the static field.
    //
    //   literalValue:
    //     The constant value of the literal.
    //
    // Returns:
    //     The defined field.
    public FieldBuilder DefineLiteral(string literalName, object literalValue);

    //
    // Summary:
    //     Sets a custom attribute using a custom attribute builder.
    //
    // Parameters:
    //   customBuilder:
    //     An instance of a helper class to define the custom attribute.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     con is null.
    extern public void SetCustomAttribute(CustomAttributeBuilder customBuilder);
    //
    // Summary:
    //     Sets a custom attribute using a specified custom attribute blob.
    //
    // Parameters:
    //   con:
    //     The constructor for the custom attribute.
    //
    //   binaryAttribute:
    //     A byte blob representing the attributes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     con or binaryAttribute is null.
    extern public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute);
  }
}
