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

// File System.Reflection.Emit.PropertyBuilder.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Reflection.Emit
{
  sealed public partial class PropertyBuilder : System.Reflection.PropertyInfo, System.Runtime.InteropServices._PropertyBuilder
  {
    #region Methods and constructors
    public void AddOtherMethod(MethodBuilder mdBuilder)
    {
    }

    public override System.Reflection.MethodInfo[] GetAccessors(bool nonPublic)
    {
      return default(System.Reflection.MethodInfo[]);
    }

    public override Object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return default(Object[]);
    }

    public override Object[] GetCustomAttributes(bool inherit)
    {
      return default(Object[]);
    }

    public override System.Reflection.MethodInfo GetGetMethod(bool nonPublic)
    {
      return default(System.Reflection.MethodInfo);
    }

    public override System.Reflection.ParameterInfo[] GetIndexParameters()
    {
      return default(System.Reflection.ParameterInfo[]);
    }

    public override System.Reflection.MethodInfo GetSetMethod(bool nonPublic)
    {
      return default(System.Reflection.MethodInfo);
    }

    public override Object GetValue(Object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object[] index, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    public override Object GetValue(Object obj, Object[] index)
    {
      return default(Object);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return default(bool);
    }

    internal PropertyBuilder()
    {
    }

    public void SetConstant(Object defaultValue)
    {
    }

    public void SetCustomAttribute(System.Reflection.ConstructorInfo con, byte[] binaryAttribute)
    {
    }

    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
    }

    public void SetGetMethod(MethodBuilder mdBuilder)
    {
    }

    public void SetSetMethod(MethodBuilder mdBuilder)
    {
    }

    public override void SetValue(Object obj, Object value, Object[] index)
    {
    }

    public override void SetValue(Object obj, Object value, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object[] index, System.Globalization.CultureInfo culture)
    {
    }

    void System.Runtime.InteropServices._PropertyBuilder.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._PropertyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._PropertyBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._PropertyBuilder.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }
    #endregion

    #region Properties and indexers
    public override System.Reflection.PropertyAttributes Attributes
    {
      get
      {
        return default(System.Reflection.PropertyAttributes);
      }
    }

    public override bool CanRead
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanWrite
    {
      get
      {
        return default(bool);
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return default(Type);
      }
    }

    public override System.Reflection.Module Module
    {
      get
      {
        return default(System.Reflection.Module);
      }
    }

    public override string Name
    {
      get
      {
        return default(string);
      }
    }

    public PropertyToken PropertyToken
    {
      get
      {
        return default(PropertyToken);
      }
    }

    public override Type PropertyType
    {
      get
      {
        return default(Type);
      }
    }

    public override Type ReflectedType
    {
      get
      {
        return default(Type);
      }
    }
    #endregion
  }
}
