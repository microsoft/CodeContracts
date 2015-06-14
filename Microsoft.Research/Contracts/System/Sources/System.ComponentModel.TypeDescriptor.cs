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

// File System.ComponentModel.TypeDescriptor.cs
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
  sealed public partial class TypeDescriptor
  {
    #region Methods and constructors
    public static TypeDescriptionProvider AddAttributes(Object instance, Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.TypeDescriptionProvider>() != null);

      return default(TypeDescriptionProvider);
    }

    public static TypeDescriptionProvider AddAttributes(Type type, Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.TypeDescriptionProvider>() != null);

      return default(TypeDescriptionProvider);
    }

    public static void AddEditorTable(Type editorBaseType, System.Collections.Hashtable table)
    {
    }

    public static void AddProvider(TypeDescriptionProvider provider, Object instance)
    {
    }

    public static void AddProvider(TypeDescriptionProvider provider, Type type)
    {
    }

    public static void AddProviderTransparent(TypeDescriptionProvider provider, Object instance)
    {
    }

    public static void AddProviderTransparent(TypeDescriptionProvider provider, Type type)
    {
      Contract.Requires(type.Assembly != null);
      Contract.Requires(type.Assembly.PermissionSet != null);
    }

    public static void CreateAssociation(Object primary, Object secondary)
    {
    }

    public static System.ComponentModel.Design.IDesigner CreateDesigner(IComponent component, Type designerBaseType)
    {
      return default(System.ComponentModel.Design.IDesigner);
    }

    public static EventDescriptor CreateEvent(Type componentType, EventDescriptor oldEventDescriptor, Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.EventDescriptor>() != null);

      return default(EventDescriptor);
    }

    public static EventDescriptor CreateEvent(Type componentType, string name, Type type, Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.EventDescriptor>() != null);

      return default(EventDescriptor);
    }

    public static Object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, Object[] args)
    {
      return default(Object);
    }

    public static PropertyDescriptor CreateProperty(Type componentType, string name, Type type, Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.PropertyDescriptor>() != null);

      return default(PropertyDescriptor);
    }

    public static PropertyDescriptor CreateProperty(Type componentType, PropertyDescriptor oldPropertyDescriptor, Attribute[] attributes)
    {
      Contract.Requires(oldPropertyDescriptor != null);
      Contract.Ensures(Contract.Result<System.ComponentModel.PropertyDescriptor>() != null);

      return default(PropertyDescriptor);
    }

    public static Object GetAssociation(Type type, Object primary)
    {
      Contract.Ensures(Contract.Result<System.Object>() != null);

      return default(Object);
    }

    public static AttributeCollection GetAttributes(Object component)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.AttributeCollection>() != null);

      return default(AttributeCollection);
    }

    public static AttributeCollection GetAttributes(Object component, bool noCustomTypeDesc)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.AttributeCollection>() != null);

      return default(AttributeCollection);
    }

    public static AttributeCollection GetAttributes(Type componentType)
    {
      return default(AttributeCollection);
    }

    public static string GetClassName(Type componentType)
    {
      return default(string);
    }

    public static string GetClassName(Object component, bool noCustomTypeDesc)
    {
      return default(string);
    }

    public static string GetClassName(Object component)
    {
      return default(string);
    }

    public static string GetComponentName(Object component)
    {
      return default(string);
    }

    public static string GetComponentName(Object component, bool noCustomTypeDesc)
    {
      return default(string);
    }

    public static TypeConverter GetConverter(Type type)
    {
      return default(TypeConverter);
    }

    public static TypeConverter GetConverter(Object component, bool noCustomTypeDesc)
    {
      return default(TypeConverter);
    }

    public static TypeConverter GetConverter(Object component)
    {
      return default(TypeConverter);
    }

    public static EventDescriptor GetDefaultEvent(Object component)
    {
      return default(EventDescriptor);
    }

    public static EventDescriptor GetDefaultEvent(Type componentType)
    {
      return default(EventDescriptor);
    }

    public static EventDescriptor GetDefaultEvent(Object component, bool noCustomTypeDesc)
    {
      return default(EventDescriptor);
    }

    public static PropertyDescriptor GetDefaultProperty(Type componentType)
    {
      return default(PropertyDescriptor);
    }

    public static PropertyDescriptor GetDefaultProperty(Object component)
    {
      return default(PropertyDescriptor);
    }

    public static PropertyDescriptor GetDefaultProperty(Object component, bool noCustomTypeDesc)
    {
      return default(PropertyDescriptor);
    }

    public static Object GetEditor(Type type, Type editorBaseType)
    {
      return default(Object);
    }

    public static Object GetEditor(Object component, Type editorBaseType)
    {
      return default(Object);
    }

    public static Object GetEditor(Object component, Type editorBaseType, bool noCustomTypeDesc)
    {
      return default(Object);
    }

    public static EventDescriptorCollection GetEvents(Object component, Attribute[] attributes, bool noCustomTypeDesc)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.EventDescriptorCollection>() != null);

      return default(EventDescriptorCollection);
    }

    public static EventDescriptorCollection GetEvents(Type componentType)
    {
      return default(EventDescriptorCollection);
    }

    public static EventDescriptorCollection GetEvents(Object component, Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.EventDescriptorCollection>() != null);

      return default(EventDescriptorCollection);
    }

    public static EventDescriptorCollection GetEvents(Type componentType, Attribute[] attributes)
    {
      return default(EventDescriptorCollection);
    }

    public static EventDescriptorCollection GetEvents(Object component)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.EventDescriptorCollection>() != null);

      return default(EventDescriptorCollection);
    }

    public static EventDescriptorCollection GetEvents(Object component, bool noCustomTypeDesc)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.EventDescriptorCollection>() != null);

      return default(EventDescriptorCollection);
    }

    public static string GetFullComponentName(Object component)
    {
      return default(string);
    }

    public static PropertyDescriptorCollection GetProperties(Object component, Attribute[] attributes, bool noCustomTypeDesc)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.PropertyDescriptorCollection>() != null);

      return default(PropertyDescriptorCollection);
    }

    public static PropertyDescriptorCollection GetProperties(Type componentType, Attribute[] attributes)
    {
      return default(PropertyDescriptorCollection);
    }

    public static PropertyDescriptorCollection GetProperties(Object component, Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.PropertyDescriptorCollection>() != null);

      return default(PropertyDescriptorCollection);
    }

    public static PropertyDescriptorCollection GetProperties(Type componentType)
    {
      return default(PropertyDescriptorCollection);
    }

    public static PropertyDescriptorCollection GetProperties(Object component)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.PropertyDescriptorCollection>() != null);

      return default(PropertyDescriptorCollection);
    }

    public static PropertyDescriptorCollection GetProperties(Object component, bool noCustomTypeDesc)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.PropertyDescriptorCollection>() != null);

      return default(PropertyDescriptorCollection);
    }

    public static TypeDescriptionProvider GetProvider(Object instance)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.TypeDescriptionProvider>() != null);

      return default(TypeDescriptionProvider);
    }

    public static TypeDescriptionProvider GetProvider(Type type)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.TypeDescriptionProvider>() != null);

      return default(TypeDescriptionProvider);
    }

    public static Type GetReflectionType(Object instance)
    {
      return default(Type);
    }

    public static Type GetReflectionType(Type type)
    {
      return default(Type);
    }

    public static void Refresh(Object component)
    {
    }

    public static void Refresh(Type type)
    {
    }

    public static void Refresh(System.Reflection.Module module)
    {
    }

    public static void Refresh(System.Reflection.Assembly assembly)
    {
    }

    public static void RemoveAssociation(Object primary, Object secondary)
    {
    }

    public static void RemoveAssociations(Object primary)
    {
    }

    public static void RemoveProvider(TypeDescriptionProvider provider, Object instance)
    {
    }

    public static void RemoveProvider(TypeDescriptionProvider provider, Type type)
    {
    }

    public static void RemoveProviderTransparent(TypeDescriptionProvider provider, Object instance)
    {
    }

    public static void RemoveProviderTransparent(TypeDescriptionProvider provider, Type type)
    {
      Contract.Requires(type.Assembly != null);
      Contract.Requires(type.Assembly.PermissionSet != null);
    }

    public static void SortDescriptorArray(System.Collections.IList infos)
    {
    }

    internal TypeDescriptor()
    {
    }
    #endregion

    #region Properties and indexers
    public static IComNativeDescriptorHandler ComNativeDescriptorHandler
    {
      get
      {
        return default(IComNativeDescriptorHandler);
      }
      set
      {
      }
    }

    public static Type ComObjectType
    {
      get
      {
        return default(Type);
      }
    }

    public static Type InterfaceType
    {
      get
      {
        return default(Type);
      }
    }
    #endregion

    #region Events
    public static event RefreshEventHandler Refreshed
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
