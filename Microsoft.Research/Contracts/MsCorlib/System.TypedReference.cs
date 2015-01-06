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

using System.Diagnostics.Contracts;
using System;

namespace System
{
#if !SILVERLIGHT_4_0_WP
  public struct TypedReference
  {
#if !SILVERLIGHT
    public static void SetTypedReference(TypedReference target, object value)
    {
      Contract.Requires(value != null);

    }
    public static RuntimeTypeHandle TargetTypeToken(TypedReference value)
    {

      return default(RuntimeTypeHandle);
    }

    public static Type GetTargetType(TypedReference value)
    {
      return default(Type);
    }
    public static object ToObject(TypedReference arg0)
    {

      return default(object);
    }
#endif

#if false
    public static TypedReference MakeTypedReference(object target, System.Reflection.FieldInfo[] flds)
    {
      Contract.Requires(target != null);
      Contract.Requires(flds != null);
      Contract.Requires(flds.Length != 0);
      return default(TypedReference);
    }
#endif
  }
#endif
}
