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

// File System.Reflection.ParameterInfo.cs
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


namespace System.Reflection
{
  public partial class ParameterInfo : System.Runtime.InteropServices._ParameterInfo, ICustomAttributeProvider, System.Runtime.Serialization.IObjectReference
  {
    #region Methods and constructors
    public virtual new Object[] GetCustomAttributes(bool inherit)
    {
      return default(Object[]);
    }

    public virtual new Object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return default(Object[]);
    }

    public virtual new IList<CustomAttributeData> GetCustomAttributesData()
    {
      return default(IList<CustomAttributeData>);
    }

    public virtual new Type[] GetOptionalCustomModifiers()
    {
      return default(Type[]);
    }

    public Object GetRealObject(System.Runtime.Serialization.StreamingContext context)
    {
      return default(Object);
    }

    public virtual new Type[] GetRequiredCustomModifiers()
    {
      return default(Type[]);
    }

    public virtual new bool IsDefined(Type attributeType, bool inherit)
    {
      return default(bool);
    }

    protected ParameterInfo()
    {
    }

    void System.Runtime.InteropServices._ParameterInfo.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._ParameterInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._ParameterInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._ParameterInfo.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public virtual new ParameterAttributes Attributes
    {
      get
      {
        return default(ParameterAttributes);
      }
    }

    public virtual new Object DefaultValue
    {
      get
      {
        return default(Object);
      }
    }

    public bool IsIn
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == ((((this.Attributes & ((System.Reflection.ParameterAttributes)(1)))) == ((System.Reflection.ParameterAttributes)(0))) == false));

        return default(bool);
      }
    }

    public bool IsLcid
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == ((((this.Attributes & ((System.Reflection.ParameterAttributes)(4)))) == ((System.Reflection.ParameterAttributes)(0))) == false));

        return default(bool);
      }
    }

    public bool IsOptional
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == ((((this.Attributes & ((System.Reflection.ParameterAttributes)(16)))) == ((System.Reflection.ParameterAttributes)(0))) == false));

        return default(bool);
      }
    }

    public bool IsOut
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == ((((this.Attributes & ((System.Reflection.ParameterAttributes)(2)))) == ((System.Reflection.ParameterAttributes)(0))) == false));

        return default(bool);
      }
    }

    public bool IsRetval
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == ((((this.Attributes & ((System.Reflection.ParameterAttributes)(8)))) == ((System.Reflection.ParameterAttributes)(0))) == false));

        return default(bool);
      }
    }

    public virtual new MemberInfo Member
    {
      get
      {
        return default(MemberInfo);
      }
    }

    public virtual new int MetadataToken
    {
      get
      {
        return default(int);
      }
    }

    public virtual new string Name
    {
      get
      {
        return default(string);
      }
    }

    public virtual new Type ParameterType
    {
      get
      {
        return default(Type);
      }
    }

    public virtual new int Position
    {
      get
      {
        return default(int);
      }
    }

    public virtual new Object RawDefaultValue
    {
      get
      {
        return default(Object);
      }
    }
    #endregion

    #region Fields
    protected ParameterAttributes AttrsImpl;
    protected Type ClassImpl;
    protected Object DefaultValueImpl;
    protected MemberInfo MemberImpl;
    protected string NameImpl;
    protected int PositionImpl;
    #endregion
  }
}
