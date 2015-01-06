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

#define DEBUG // The behavior of this contract library should be consistent regardless of build type.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Diagnostics.Contracts {
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
  public class GlobalAccessAttribute : Attribute {
    public GlobalAccessAttribute(bool canAccessGlobals) { }
  }

  public class WriteCompliantAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property)]
  public class EscapesAttribute : Attribute {
    public EscapesAttribute(bool a, bool b) { }
  }

  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
  public class ResultNotNewlyAllocatedAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Method)]
  public class WriteConfinedAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
  public class FreshAttribute : Attribute {
  }

  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
  public class ReadsAttribute : Attribute {
    public enum Reads {
      Nothing = 0,
      Owned = 1,
    }

    public ReadsAttribute(Reads desc) { }
  }

}