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

using System.Runtime.InteropServices;

namespace System
{
  // Summary:
  //     Specifies the application elements on which it is valid to apply an attribute.
  public enum AttributeTargets
  {
    // Summary:
    //     Attribute can be applied to an assembly.
    Assembly = 1,
    //
    // Summary:
    //     Attribute can be applied to a module.
    Module = 2,
    //
    // Summary:
    //     Attribute can be applied to a class.
    Class = 4,
    //
    // Summary:
    //     Attribute can be applied to a structure; that is, a value type.
    Struct = 8,
    //
    // Summary:
    //     Attribute can be applied to an enumeration.
    Enum = 16,
    //
    // Summary:
    //     Attribute can be applied to a constructor.
    Constructor = 32,
    //
    // Summary:
    //     Attribute can be applied to a method.
    Method = 64,
    //
    // Summary:
    //     Attribute can be applied to a property.
    Property = 128,
    //
    // Summary:
    //     Attribute can be applied to a field.
    Field = 256,
    //
    // Summary:
    //     Attribute can be applied to an event.
    Event = 512,
    //
    // Summary:
    //     Attribute can be applied to an interface.
    Interface = 1024,
    //
    // Summary:
    //     Attribute can be applied to a parameter.
    Parameter = 2048,
    //
    // Summary:
    //     Attribute can be applied to a delegate.
    Delegate = 4096,
    //
    // Summary:
    //     Attribute can be applied to a return value.
    ReturnValue = 8192,
    //
    // Summary:
    //     Attribute can be applied to a generic parameter.
    GenericParameter = 16384,
    //
    // Summary:
    //     Attribute can be applied to any application element.
    All = 32767,
  }
}
