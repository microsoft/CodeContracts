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

// File System.Dynamic.DynamicObject.cs
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


namespace System.Dynamic
{
  public partial class DynamicObject : IDynamicMetaObjectProvider
  {
    #region Methods and constructors
    protected DynamicObject()
    {
    }

    public virtual new IEnumerable<string> GetDynamicMemberNames()
    {
      return default(IEnumerable<string>);
    }

    public virtual new DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter)
    {
      return default(DynamicMetaObject);
    }

    public virtual new bool TryBinaryOperation(BinaryOperationBinder binder, Object arg, out Object result)
    {
      result = default(Object);

      return default(bool);
    }

    public virtual new bool TryConvert(ConvertBinder binder, out Object result)
    {
      result = default(Object);

      return default(bool);
    }

    public virtual new bool TryCreateInstance(CreateInstanceBinder binder, Object[] args, out Object result)
    {
      result = default(Object);

      return default(bool);
    }

    public virtual new bool TryDeleteIndex(DeleteIndexBinder binder, Object[] indexes)
    {
      return default(bool);
    }

    public virtual new bool TryDeleteMember(DeleteMemberBinder binder)
    {
      return default(bool);
    }

    public virtual new bool TryGetIndex(GetIndexBinder binder, Object[] indexes, out Object result)
    {
      result = default(Object);

      return default(bool);
    }

    public virtual new bool TryGetMember(GetMemberBinder binder, out Object result)
    {
      result = default(Object);

      return default(bool);
    }

    public virtual new bool TryInvoke(InvokeBinder binder, Object[] args, out Object result)
    {
      result = default(Object);

      return default(bool);
    }

    public virtual new bool TryInvokeMember(InvokeMemberBinder binder, Object[] args, out Object result)
    {
      result = default(Object);

      return default(bool);
    }

    public virtual new bool TrySetIndex(SetIndexBinder binder, Object[] indexes, Object value)
    {
      return default(bool);
    }

    public virtual new bool TrySetMember(SetMemberBinder binder, Object value)
    {
      return default(bool);
    }

    public virtual new bool TryUnaryOperation(UnaryOperationBinder binder, out Object result)
    {
      result = default(Object);

      return default(bool);
    }
    #endregion
  }
}
