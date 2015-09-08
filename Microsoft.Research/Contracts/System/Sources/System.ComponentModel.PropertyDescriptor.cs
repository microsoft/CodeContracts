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

// File System.ComponentModel.PropertyDescriptor.cs
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
  abstract public partial class PropertyDescriptor : MemberDescriptor
  {
    #region Methods and constructors
    public virtual new void AddValueChanged(Object component, EventHandler handler)
    {
    }

    public abstract bool CanResetValue(Object component);

    protected Object CreateInstance(Type type)
    {
      Contract.Requires(type != null);

      return default(Object);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    protected override void FillAttributes(System.Collections.IList attributeList)
    {
    }

    public PropertyDescriptorCollection GetChildProperties(Object instance)
    {
      return default(PropertyDescriptorCollection);
    }

    public virtual new PropertyDescriptorCollection GetChildProperties(Object instance, Attribute[] filter)
    {
      return default(PropertyDescriptorCollection);
    }

    public PropertyDescriptorCollection GetChildProperties(Attribute[] filter)
    {
      return default(PropertyDescriptorCollection);
    }

    public PropertyDescriptorCollection GetChildProperties()
    {
      return default(PropertyDescriptorCollection);
    }

    public virtual new Object GetEditor(Type editorBaseType)
    {
      return default(Object);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    protected override Object GetInvocationTarget(Type type, Object instance)
    {
      return default(Object);
    }

    protected Type GetTypeFromName(string typeName)
    {
      return default(Type);
    }

    public abstract Object GetValue(Object component);

    protected internal EventHandler GetValueChangedHandler(Object component)
    {
      return default(EventHandler);
    }

    protected virtual new void OnValueChanged(Object component, EventArgs e)
    {
    }

    protected PropertyDescriptor(MemberDescriptor descr) : base (default(string))
    {
      Contract.Requires(0 <= descr.Attributes.Count);
      Contract.Requires(descr.Name != null);
    }

    protected PropertyDescriptor(MemberDescriptor descr, Attribute[] attrs) : base (default(string))
    {
      Contract.Requires(descr != null);
      Contract.Requires(descr.Attributes != null);
      Contract.Requires(descr.Name != null);
    }

    protected PropertyDescriptor(string name, Attribute[] attrs) : base (default(string))
    {
    }

    public virtual new void RemoveValueChanged(Object component, EventHandler handler)
    {
    }

    public abstract void ResetValue(Object component);

    public abstract void SetValue(Object component, Object value);

    public abstract bool ShouldSerializeValue(Object component);
    #endregion

    #region Properties and indexers
    public abstract Type ComponentType
    {
      get;
    }

    public virtual new TypeConverter Converter
    {
      get
      {
        return default(TypeConverter);
      }
    }

    public virtual new bool IsLocalizable
    {
      get
      {
        Contract.Requires(this.Attributes != null);

        return default(bool);
      }
    }

    public abstract bool IsReadOnly
    {
      get;
    }

    public abstract Type PropertyType
    {
      get;
    }

    public DesignerSerializationVisibility SerializationVisibility
    {
      get
      {
        Contract.Requires(this.Attributes != null);

        return default(DesignerSerializationVisibility);
      }
    }

    public virtual new bool SupportsChangeEvents
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
