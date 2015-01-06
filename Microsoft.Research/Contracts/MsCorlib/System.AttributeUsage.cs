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

namespace System
{
  public class AttributeUsageAttribute : Attribute
  {
    // Summary:
    //     Initializes a new instance of the System.AttributeUsageAttribute class with
    //     the specified list of System.AttributeTargets, the System.AttributeUsageAttribute.AllowMultiple
    //     value, and the System.AttributeUsageAttribute.Inherited value.
    //
    // Parameters:
    //   validOn:
    //     The set of values combined using a bitwise OR operation to indicate which
    //     program elements are valid.
    public AttributeUsageAttribute(AttributeTargets validOn) { }

    // Summary:
    //     Gets or sets a Boolean value indicating whether more than one instance of
    //     the indicated attribute can be specified for a single program element.
    //
    // Returns:
    //     true if more than one instance is allowed to be specified; otherwise, false.
    //     The default is false.
    extern public bool AllowMultiple { get; set; }
    //
    // Summary:
    //     Gets or sets a Boolean value indicating whether the indicated attribute can
    //     be inherited by derived classes and overriding members.
    //
    // Returns:
    //     true if the attribute can be inherited by derived classes and overriding
    //     members; otherwise, false. The default is true.
    extern public bool Inherited { get; set; }
    //
    // Summary:
    //     Gets a set of values identifying which program elements that the indicated
    //     attribute can be applied to.
    //
    // Returns:
    //     One or several System.AttributeTargets values. The default is All.
    extern public AttributeTargets ValidOn { get; }
  }
}
