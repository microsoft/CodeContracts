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

//
//  Attributes as used by Joe Duffy
//
//  * Immutable on a method parameter means the method only reads immutable parts reachable through that parameter
//    (and does not modify anything via this parameter reachability)
//  * Readable on a method parameter means the method only reads through this reference, but does not write.
//  * Writable on a method parameter means the method may write through this reference
//
//  NOTES: 
//  * value type arguments  are Immutable by default.
//  * Annotations on methods apply to the receiver (this) parameter on instance methods
//    They do not provide defaults for all parameters.
//    On non-instance methods, the annotation is ignored!
//  * Constructor receivers cannot be annotated. They are Writable!
//  * On a type (struct or class, but not interface) Immutable can be used to declare that the type is immutable.
//    - This implicitly means that any parameter of that type anywhere, in any code is immutable. It thus provides a default
//      annotation for many parameters.
//  * On a delegate, an attribute as above refers to the implicitly captured object.

using System;

namespace System.Diagnostics.Contracts
{
  /// <summary>
  /// Method(s) will only read IMMUTABLE parts reachable via parameter (or this) and not write via parameter (or this)
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Delegate | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Struct)]
  internal class ImmutableAttribute : Attribute
  {
  }

  /// <summary>
  /// Method(s) will only read parts reachable via parameter (or this) and not write via parameter (or this)
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Delegate | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Struct)]
  internal class ReadableAttribute : Attribute
  {
  }

  /// <summary>
  /// Method(s) may write parts reachable via parameter (or this) 
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Delegate | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Struct)]
  internal class WritableAttribute : Attribute
  {
  }
}
