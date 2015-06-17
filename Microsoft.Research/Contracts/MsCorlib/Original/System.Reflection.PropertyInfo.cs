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

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Reflection {
  // Summary:
  //     Discovers the attributes of a property and provides access to property metadata.
  [Immutable] // Base class is immutable.
  public abstract class PropertyInfo : MemberInfo {
    // Summary:
    //     Initializes a new instance of the System.Reflection.PropertyInfo class.
    protected PropertyInfo();

    // Summary:
    //     Gets the attributes for this property.
    //
    // Returns:
    //     Attributes of this property.
    public abstract PropertyAttributes Attributes { get; }
    //
    // Summary:
    //     Gets a value indicating whether the property can be read.
    //
    // Returns:
    //     true if this property can be read; otherwise, false.
    public abstract bool CanRead { get; }
    //
    // Summary:
    //     Gets a value indicating whether the property can be written to.
    //
    // Returns:
    //     true if this property can be written to; otherwise, false.
    public abstract bool CanWrite { get; }
    //
    // Summary:
    //     Gets a value indicating whether the property is the special name.
    //
    // Returns:
    //     true if this property is the special name; otherwise, false.
    public bool IsSpecialName { get; }
    //
    // Summary:
    //     Gets a System.Reflection.MemberTypes value indicating that this member is
    //     a property.
    //
    // Returns:
    //     A System.Reflection.MemberTypes value indicating that this member is a property.
    public override MemberTypes MemberType { get; }
    //
    // Summary:
    //     Gets the type of this property.
    //
    // Returns:
    //     The type of this property.
    public abstract Type PropertyType { get; }

    // Summary:
    //     Returns an array whose elements reflect the public get, set, and other accessors
    //     of the property reflected by the current instance.
    //
    // Returns:
    //     An array of System.Reflection.MethodInfo objects that reflect the public
    //     get, set, and other accessors of the property reflected by the current instance,
    //     if found; otherwise, this method returns an array with zero (0) elements.
    public MethodInfo[] GetAccessors() {
    //
    // Summary:
      return default(MethodInfo[]);
    }
    //     Returns an array whose elements reflect the public and, if specified, non-public
    //     get, set, and other accessors of the property reflected by the current instance.
    //
    // Parameters:
    //   nonPublic:
    //     Indicates whether non-public methods should be returned in the MethodInfo
    //     array. true if non-public methods are to be included; otherwise, false. 
    //
    // Returns:
    //     An array of System.Reflection.MethodInfo objects whose elements reflect the
    //     get, set, and other accessors of the property reflected by the current instance.
    //     If nonPublic is true, this array contains public and non-public get, set,
    //     and other accessors. If nonPublic is false, this array contains only public
    //     get, set, and other accessors. If no accessors with the specified visibility
    //     are found, this method returns an array with zero (0) elements.
    public abstract MethodInfo[]! GetAccessors(bool nonPublic) {
    //
    // Summary:
    //     Returns a literal value associated with the property by a compiler.
    //
    // Returns:
    //     An System.Object that contains the literal value associated with the property.
    //     If the literal value is a class type with an element value of zero, the return
    //     value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     The type of the value is not one of the types permitted by the Common Language
    //     Specification (CLS). See the ECMA Partition II specification, Metadata, 22.1.15.
    //
    //   System.InvalidOperationException:
    //     The Constant table in unmanaged metadata does not contain a constant value
    //     for the current property.
      return default(abstract);
    }
    public virtual object GetConstantValue() {
    //
    // Summary:
      return default(virtual);
    }
    //     Returns the public get accessor for this property.
    //
    // Returns:
    //     A MethodInfo object representing the public get accessor for this property,
    //     or null if the get accessor is non-public or does not exist.
    public MethodInfo GetGetMethod() {
    //
    // Summary:
      return default(MethodInfo);
    }
    //     When overridden in a derived class, returns the public or non-public get
    //     accessor for this property.
    //
    // Parameters:
    //   nonPublic:
    //     Indicates whether a non-public get accessor should be returned. true if a
    //     non-public accessor is to be returned; otherwise, false.
    //
    // Returns:
    //     A MethodInfo object representing the get accessor for this property, if nonPublic
    //     is true. Returns null if nonPublic is false and the get accessor is non-public,
    //     or if nonPublic is true but no get accessors exist.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The requested method is non-public and the caller does not have System.Security.Permissions.ReflectionPermission
    //     to reflect on this non-public method.
    public abstract MethodInfo GetGetMethod(bool nonPublic) {
    //
    // Summary:
    //     When overridden in a derived class, returns an array of all the index parameters
    //     for the property.
    //
    // Returns:
    //     An array of type ParameterInfo containing the parameters for the indexes.
      return default(abstract);
    }
    public abstract ParameterInfo[]! GetIndexParameters() {
    //
    // Summary:
    //     Returns an array of types representing the optional custom modifiers of the
    //     property.
    //
    // Returns:
    //     An array of System.Type objects that identify the optional custom modifiers
    //     of the current property, such as System.Runtime.CompilerServices.IsConst
    //     or System.Runtime.CompilerServices.IsImplicitlyDeferenced.
      return default(abstract);
    }
    public virtual Type[]! GetOptionalCustomModifiers() {
    //
    // Summary:
    //     Returns a literal value associated with the property by a compiler.
    //
    // Returns:
    //     An System.Object that contains the literal value associated with the property.
    //     If the literal value is a class type with an element value of zero, the return
    //     value is null.
    //
    // Exceptions:
    //   System.FormatException:
    //     The type of the value is not one of the types permitted by the Common Language
    //     Specification (CLS). See the ECMA Partition II specification, Metadata Logical
    //     Format: Other Structures, Element Types used in Signatures.
    //
    //   System.InvalidOperationException:
    //     The Constant table in unmanaged metadata does not contain a constant value
    //     for the current property.
      return default(virtual);
    }
    public virtual object GetRawConstantValue() {
    //
    // Summary:
    //     Returns an array of types representing the required custom modifiers of the
    //     property.
    //
    // Returns:
    //     An array of System.Type objects that identify the required custom modifiers
    //     of the current property, such as System.Runtime.CompilerServices.IsConst
    //     or System.Runtime.CompilerServices.IsImplicitlyDeferenced.
      return default(virtual);
    }
    public virtual Type[]! GetRequiredCustomModifiers() {
    //
    // Summary:
      return default(virtual);
    }
    //     Returns the public set accessor for this property.
    //
    // Returns:
    //     The MethodInfo object representing the Set method for this property if the
    //     set accessor is public, or null if the set accessor is not public.
    public MethodInfo GetSetMethod() {
    //
    // Summary:
    //     When overridden in a derived class, returns the set accessor for this property.
    //
    // Parameters:
    //   nonPublic:
      return default(MethodInfo);
    }
    //     Indicates whether the accessor should be returned if it is non-public. true
    //     if a non-public accessor is to be returned; otherwise, false.
    //
    // Returns:
    //     Value Condition A System.Reflection.MethodInfo object representing the Set
    //     method for this property. The set accessor is public.-or- nonPublic is true
    //     and the set accessor is non-public. nullnonPublic is true, but the property
    //     is read-only.-or- nonPublic is false and the set accessor is non-public.-or-
    //     There is no set accessor.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The requested method is non-public and the caller does not have System.Security.Permissions.ReflectionPermission
    //     to reflect on this non-public method.
    public abstract MethodInfo GetSetMethod(bool nonPublic) {
    //
    // Summary:
    //     Returns the value of the property with optional index values for indexed
    //     properties.
    //
    // Parameters:
    //   obj:
    //     The object whose property value will be returned.
    //
    //   index:
    //     Optional index values for indexed properties. This value should be null for
    //     non-indexed properties.
    //
    // Returns:
    //     The property value for the obj parameter.
    //
    // Exceptions:
    //   System.MethodAccessException:
    //     There was an illegal attempt to access a private or protected method inside
    //     a class.
    //
    //   System.ArgumentException:
    //     The index array does not contain the type of arguments needed.-or- The property's
    //     Get method is not found.
    //
    //   System.Reflection.TargetException:
    //     The object does not match the target type, or a property is an instance property
    //     but obj is null.
    //
    //   System.Reflection.TargetParameterCountException:
    //     The number of parameters in index does not match the number of parameters
    //     the indexed property takes.
    [DebuggerStepThrough]
      return default(abstract);
    }
    [DebuggerHidden]
    public virtual object GetValue(object obj, object[] index) {
    //
    // Summary:
    //     When overridden in a derived class, returns the value of a property having
    //     the specified binding, index, and CultureInfo.
    //
    // Parameters:
    //   culture:
    //     The CultureInfo object that represents the culture for which the resource
    //     is to be localized. Note that if the resource is not localized for this culture,
    //     the CultureInfo.Parent method will be called successively in search of a
    //     match. If this value is null, the CultureInfo is obtained from the CultureInfo.CurrentUICulture
    //     property.
    //
    //   obj:
    //     The object whose property value will be returned.
    //
    //   binder:
    //     An object that enables the binding, coercion of argument types, invocation
    //     of members, and retrieval of MemberInfo objects via reflection. If binder
    //     is null, the default binder is used.
    //
    //   invokeAttr:
    //     The invocation attribute. This must be a bit flag from BindingFlags : InvokeMethod,
    //     CreateInstance, Static, GetField, SetField, GetProperty, or SetProperty.
    //     A suitable invocation attribute must be specified. If a static member is
    //     to be invoked, the Static flag of BindingFlags must be set.
    //
    //   index:
    //     Optional index values for indexed properties. This value should be null for
    //     non-indexed properties.
    //
    // Returns:
    //     The property value for obj.
    //
    // Exceptions:
    //   System.MethodAccessException:
    //     There was an illegal attempt to access a private or protected method inside
    //     a class.
    //
    //   System.ArgumentException:
    //     The index array does not contain the type of arguments needed.-or- The property's
    //     Get method is not found.
    //
    //   System.Reflection.TargetException:
    //     The object does not match the target type, or a property is an instance property
    //     but obj is null.
    //
    //   System.Reflection.TargetParameterCountException:
    //     The number of parameters in index does not match the number of parameters
    //     the indexed property takes.
      return default(virtual);
    }
    public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture) {
    //
    // Summary:
    //     Sets the value of the property with optional index values for index properties.
    //
    // Parameters:
    //   obj:
    //     The object whose property value will be set.
    //
    //   value:
    //     The new value for this property.
    //
    //   index:
    //     Optional index values for indexed properties. This value should be null for
    //     non-indexed properties.
    //
    // Exceptions:
    //   System.MethodAccessException:
    //     There was an illegal attempt to access a private or protected method inside
    //     a class.
    //
    //   System.ArgumentException:
    //     The index array does not contain the type of arguments needed.-or- The property's
    //     Get method is not found.
    //
    //   System.Reflection.TargetException:
    //     The object does not match the target type, or a property is an instance property
    //     but obj is null.
    //
    //   System.Reflection.TargetParameterCountException:
    //     The number of parameters in index does not match the number of parameters
    //     the indexed property takes.
    [DebuggerHidden]
      return default(abstract);
    }
    [DebuggerStepThrough]
    public virtual void SetValue(object obj, object value, object[] index) {
    //
    // Summary:
    //     When overridden in a derived class, sets the property value for the given
    //     object to the given value.
    //
    // Parameters:
    //   culture:
    //     The System.Globalization.CultureInfo object that represents the culture for
    //     which the resource is to be localized. Note that if the resource is not localized
    //     for this culture, the CultureInfo.Parent method will be called successively
    //     in search of a match. If this value is null, the CultureInfo is obtained
    //     from the CultureInfo.CurrentUICulture property.
    //
    //   obj:
    //     The object whose property value will be returned.
    //
    //   binder:
    //     An object that enables the binding, coercion of argument types, invocation
    //     of members, and retrieval of System.Reflection.MemberInfo objects through
    //     reflection. If binder is null, the default binder is used.
    //
    //   invokeAttr:
    //     The invocation attribute. This must be a bit flag from System.Reflection.BindingFlags
    //     : InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty,
    //     or SetProperty. A suitable invocation attribute must be specified. If a static
    //     member is to be invoked, the Static flag of BindingFlags must be set.
    //
    //   value:
    //     The new value for this property.
    //
    //   index:
    //     Optional index values for indexed properties. This value should be null for
    //     non-indexed properties.
    //
    // Exceptions:
    //   System.MethodAccessException:
    //     There was an illegal attempt to access a private or protected method inside
    //     a class.
    //
    //   System.ArgumentException:
    //     The index array does not contain the type of arguments needed.-or- The property's
    //     Get method is not found.
    //
    //   System.Reflection.TargetException:
    //     The object does not match the target type, or a property is an instance property
    //     but obj is null.
    //
    //   System.Reflection.TargetParameterCountException:
    //     The number of parameters in index does not match the number of parameters
    //     the indexed property takes.
      return default(virtual);
    }
      return default(abstract);
    }
    public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture) {
  }
}
