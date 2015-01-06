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

// File System.ComponentModel.TypeConverter.cs
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
  public partial class TypeConverter
  {
    #region Methods and constructors
    public virtual new bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      return default(bool);
    }

    public bool CanConvertFrom(Type sourceType)
    {
      return default(bool);
    }

    public virtual new bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      return default(bool);
    }

    public bool CanConvertTo(Type destinationType)
    {
      return default(bool);
    }

    public virtual new Object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, Object value)
    {
      return default(Object);
    }

    public Object ConvertFrom(Object value)
    {
      return default(Object);
    }

    public Object ConvertFromInvariantString(ITypeDescriptorContext context, string text)
    {
      return default(Object);
    }

    public Object ConvertFromInvariantString(string text)
    {
      return default(Object);
    }

    public Object ConvertFromString(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string text)
    {
      return default(Object);
    }

    public Object ConvertFromString(ITypeDescriptorContext context, string text)
    {
      return default(Object);
    }

    public Object ConvertFromString(string text)
    {
      return default(Object);
    }

    public virtual new Object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, Object value, Type destinationType)
    {
      return default(Object);
    }

    public Object ConvertTo(Object value, Type destinationType)
    {
      return default(Object);
    }

    public string ConvertToInvariantString(ITypeDescriptorContext context, Object value)
    {
      return default(string);
    }

    public string ConvertToInvariantString(Object value)
    {
      return default(string);
    }

    public string ConvertToString(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, Object value)
    {
      return default(string);
    }

    public string ConvertToString(ITypeDescriptorContext context, Object value)
    {
      return default(string);
    }

    public string ConvertToString(Object value)
    {
      return default(string);
    }

    public virtual new Object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
    {
      return default(Object);
    }

    public Object CreateInstance(System.Collections.IDictionary propertyValues)
    {
      return default(Object);
    }

    protected Exception GetConvertFromException(Object value)
    {
      Contract.Ensures(false);

      return default(Exception);
    }

    protected Exception GetConvertToException(Object value, Type destinationType)
    {
      Contract.Requires(destinationType != null);
      Contract.Ensures(false);

      return default(Exception);
    }

    public virtual new bool GetCreateInstanceSupported(ITypeDescriptorContext context)
    {
      return default(bool);
    }

    public bool GetCreateInstanceSupported()
    {
      return default(bool);
    }

    public virtual new PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, Object value, Attribute[] attributes)
    {
      return default(PropertyDescriptorCollection);
    }

    public PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, Object value)
    {
      return default(PropertyDescriptorCollection);
    }

    public PropertyDescriptorCollection GetProperties(Object value)
    {
      return default(PropertyDescriptorCollection);
    }

    public virtual new bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
      return default(bool);
    }

    public bool GetPropertiesSupported()
    {
      return default(bool);
    }

    public virtual new TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return default(TypeConverter.StandardValuesCollection);
    }

    public System.Collections.ICollection GetStandardValues()
    {
      return default(System.Collections.ICollection);
    }

    public virtual new bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return default(bool);
    }

    public bool GetStandardValuesExclusive()
    {
      return default(bool);
    }

    public virtual new bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return default(bool);
    }

    public bool GetStandardValuesSupported()
    {
      return default(bool);
    }

    public virtual new bool IsValid(ITypeDescriptorContext context, Object value)
    {
      return default(bool);
    }

    public bool IsValid(Object value)
    {
      return default(bool);
    }

    protected PropertyDescriptorCollection SortProperties(PropertyDescriptorCollection props, string[] names)
    {
      Contract.Requires(props != null);
      Contract.Ensures(Contract.Result<System.ComponentModel.PropertyDescriptorCollection>() != null);
      Contract.Ensures(Contract.Result<System.ComponentModel.PropertyDescriptorCollection>() == props);

      return default(PropertyDescriptorCollection);
    }

    public TypeConverter()
    {
    }
    #endregion
  }
}
