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

// File System.ComponentModel.MemberDescriptor.cs
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


namespace System.ComponentModel
{
  abstract public partial class MemberDescriptor
  {
    #region Methods and constructors
    protected virtual new AttributeCollection CreateAttributeCollection()
    {
      return default(AttributeCollection);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    protected virtual new void FillAttributes(System.Collections.IList attributeList)
    {
    }

    protected static System.Reflection.MethodInfo FindMethod(Type componentClass, string name, Type[] args, Type returnType, bool publicOnly)
    {
      Contract.Requires(componentClass != null);

      return default(System.Reflection.MethodInfo);
    }

    protected static System.Reflection.MethodInfo FindMethod(Type componentClass, string name, Type[] args, Type returnType)
    {
      Contract.Requires(componentClass != null);

      return default(System.Reflection.MethodInfo);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    protected virtual new Object GetInvocationTarget(Type type, Object instance)
    {
      return default(Object);
    }

    protected static Object GetInvokee(Type componentClass, Object component)
    {
      Contract.Ensures(Contract.Result<System.Object>() != null);

      return default(Object);
    }

    protected static ISite GetSite(Object component)
    {
      return default(ISite);
    }

    protected MemberDescriptor(string name)
    {
    }

    protected MemberDescriptor(string name, Attribute[] attributes)
    {
    }

    protected MemberDescriptor(System.ComponentModel.MemberDescriptor descr)
    {
      Contract.Requires(0 <= descr.Attributes.Count);
      Contract.Requires(descr != null);
      Contract.Requires(descr.Attributes != null);
      Contract.Requires(descr.Name != null);
    }

    protected MemberDescriptor(System.ComponentModel.MemberDescriptor oldMemberDescriptor, Attribute[] newAttributes)
    {
      Contract.Requires(oldMemberDescriptor != null);
      Contract.Requires(oldMemberDescriptor.Attributes != null);
      Contract.Requires(oldMemberDescriptor.Name != null);
    }
    #endregion

    #region Properties and indexers
    protected virtual new Attribute[] AttributeArray
    {
      get
      {
        return default(Attribute[]);
      }
      set
      {
      }
    }

    public virtual new AttributeCollection Attributes
    {
      get
      {
        return default(AttributeCollection);
      }
    }

    public virtual new string Category
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string Description
    {
      get
      {
        return default(string);
      }
    }

    public virtual new bool DesignTimeOnly
    {
      get
      {
        Contract.Requires(this.Attributes != null);

        return default(bool);
      }
    }

    public virtual new string DisplayName
    {
      get
      {
        Contract.Requires(this.Attributes != null);

        return default(string);
      }
    }

    public virtual new bool IsBrowsable
    {
      get
      {
        Contract.Requires(this.Attributes != null);

        return default(bool);
      }
    }

    public virtual new string Name
    {
      get
      {
        return default(string);
      }
    }

    protected virtual new int NameHashCode
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
