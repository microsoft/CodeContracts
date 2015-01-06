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
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection {
  // Summary:
  //     Discovers the attributes of a field and provides access to field metadata.

  public abstract class FieldInfo
  {
#if SILVERLIGHT && !SILVERLIGHT_5_0
    extern internal FieldInfo();
#else
    extern protected FieldInfo();
#endif

    // Summary:
    //     Gets the attributes associated with this field.
    //
    // Returns:
    //     The FieldAttributes for this field.
    // public abstract FieldAttributes Attributes { get; }
    //
    // Summary:
    //     Gets a RuntimeFieldHandle, which is a handle to the internal metadata representation
    //     of a field.
    //
    // Returns:
    //     A handle to the internal metadata representation of a field.
    // public abstract RuntimeFieldHandle FieldHandle { get; }
    //
    // Summary:
    //     Gets the type of this field object.
    //
    // Returns:
    //     The type of this field object.
    public virtual Type FieldType
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);
        return default(Type);
      }
    }

    //
    // Summary:
    //     Gets a value indicating whether the potential visibility of this field is
    //     described by System.Reflection.FieldAttributes.Assembly; that is, the field
    //     is visible at most to other types in the same assembly, and is not visible
    //     to derived types outside the assembly.
    //
    // Returns:
    //     true if the visibility of this field is exactly described by System.Reflection.FieldAttributes.Assembly;
    //     otherwise, false.
    // public bool IsAssembly { get; }
    //
    // Summary:
    //     Gets a value indicating whether the visibility of this field is described
    //     by System.Reflection.FieldAttributes.Family; that is, the field is visible
    //     only within its class and derived classes.
    //
    // Returns:
    //     true if access to this field is exactly described by System.Reflection.FieldAttributes.Family;
    //     otherwise, false.
    //     public bool IsFamily { get; }
    //
    // Summary:
    //     Gets a value indicating whether the visibility of this field is described
    //     by System.Reflection.FieldAttributes.FamANDAssem; that is, the field can
    //     be accessed from derived classes, but only if they are in the same assembly.
    //
    // Returns:
    //     true if access to this field is exactly described by System.Reflection.FieldAttributes.FamANDAssem;
    //     otherwise, false.
    // public bool IsFamilyAndAssembly { get; }
    //
    // Summary:
    //     Gets a value indicating whether the potential visibility of this field is
    //     described by System.Reflection.FieldAttributes.FamORAssem; that is, the field
    //     can be accessed by derived classes wherever they are, and by classes in the
    //     same assembly.
    //
    // Returns:
    //     true if access to this field is exactly described by System.Reflection.FieldAttributes.FamORAssem;
    //     otherwise, false.
    // public bool IsFamilyOrAssembly { get; }
    //
    // Summary:
    //     Gets a value indicating whether the field can only be set in the body of
    //     the constructor.
    //
    // Returns:
    //     true if the field has the InitOnly attribute set; otherwise, false.
    // public bool IsInitOnly { get; }
    //
    // Summary:
    //     Gets a value indicating whether the value is written at compile time and
    //     cannot be changed.
    //
    // Returns:
    //     true if the field has the Literal attribute set; otherwise, false.
    // public bool IsLiteral { get; }
    //
    // Summary:
    //     Gets a value indicating whether this field has the NotSerialized attribute.
    //
    // Returns:
    //     true if the field has the NotSerialized attribute set; otherwise, false.
    //public bool IsNotSerialized { get; }
    //
    // Summary:
    //     Gets a value indicating whether the corresponding PinvokeImpl attribute is
    //     set in System.Reflection.FieldAttributes.
    //
    // Returns:
    //     true if the PinvokeImpl attribute is set in System.Reflection.FieldAttributes;
    //     otherwise, false.
    //public bool IsPinvokeImpl { get; }
    //
    // Summary:
    //     Gets a value indicating whether the field is private.
    //
    // Returns:
    //     true if the field is private; otherwise; false.
    //public bool IsPrivate { get; }
    //
    // Summary:
    //     Gets a value indicating whether the field is public.
    //
    // Returns:
    //     true if this field is public; otherwise, false.
    //public bool IsPublic { get; }
    //
    // Summary:
    //     Gets a value indicating whether the corresponding SpecialName attribute is
    //     set in the System.Reflection.FieldAttributes enumerator.
    //
    // Returns:
    //     true if the SpecialName attribute is set in System.Reflection.FieldAttributes;
    //     otherwise, false.
    //public bool IsSpecialName { get; }
    //
    // Summary:
    //     Gets a value indicating whether the field is static.
    //
    // Returns:
    //     true if this field is static; otherwise, false.
    //public bool IsStatic { get; }

    // Summary:
    //     Gets a System.Reflection.FieldInfo for the field represented by the specified
    //     handle.
    //
    // Parameters:
    //   handle:
    //     A System.RuntimeFieldHandle structure that contains the handle to the internal
    //     metadata representation of a field.
    //
    // Returns:
    //     A System.Reflection.FieldInfo object representing the field specified by
    //     handle.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     handle is invalid.
    public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
    {
      Contract.Ensures(Contract.Result<FieldInfo>() != null);
      return default(FieldInfo);
    }
    //
    // Summary:
    //     Gets a System.Reflection.FieldInfo for the field represented by the specified
    //     handle, for the specified generic type.
    //
    // Parameters:
    //   handle:
    //     A System.RuntimeFieldHandle structure that contains the handle to the internal
    //     metadata representation of a field.
    //
    //   declaringType:
    //     A System.RuntimeTypeHandle structure that contains the handle to the generic
    //     type that defines the field.
    //
    // Returns:
    //     A System.Reflection.FieldInfo object representing the field specified by
    //     handle, in the generic type specified by declaringType.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     handle is invalid.

    public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
    {
      Contract.Ensures(Contract.Result<FieldInfo>() != null);
      return default(FieldInfo);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Gets an array of types that identify the optional custom modifiers of the
    //     field.
    //
    // Returns:
    //     An array of System.Type objects that identify the optional custom modifiers
    //     of the current field, such as System.Runtime.CompilerServices.IsConst.
    public virtual Type[] GetOptionalCustomModifiers()
    {
      Contract.Ensures(Contract.Result<Type[]>() != null);
      return default(Type[]);
    }
#endif
    //
    // Summary:
    //     Returns a literal value associated with the field by a compiler.
    //
    // Returns:
    //     An System.Object that contains the literal value associated with the field.
    //     If the literal value is a class type with an element value of zero, the return
    //     value is null.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The Constant table in unmanaged metadata does not contain a constant value
    //     for the current property.
    //
    //   System.FormatException:
    //     The type of the value is not one of the types permitted by the Common Language
    //     Specification (CLS). See the ECMA Partition II specification Metadata Logical
    //     Format: Other Structures, Element Types used in Signatures.
    public virtual object GetRawConstantValue()
    {
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Gets an array of types that identify the required custom modifiers of the
    //     property.
    //
    // Returns:
    //     An array of System.Type objects that identify the required custom modifiers
    //     of the current property, such as System.Runtime.CompilerServices.IsConst
    //     or System.Runtime.CompilerServices.IsImplicitlyDereferenced.
    public virtual Type[] GetRequiredCustomModifiers()
    {
      Contract.Ensures(Contract.Result<Type[]>() != null);
      return default(Type[]);
    }
#endif

    //
    // Summary:
    //     When overridden in a derived class, returns the value of a field supported
    //     by a given object.
    //
    // Parameters:
    //   obj:
    //     The object whose field value will be returned.
    //
    // Returns:
    //     An object containing the value of the field reflected by this instance.
    //
    // Exceptions:
    //   System.Reflection.TargetException:
    //     The field is non-static and obj is null.
    //
    //   System.NotSupportedException:
    //     A field is marked literal, but the field does not have one of the accepted
    //     literal types.
    //
    //   System.FieldAccessException:
    //     The caller does not have permission to access this field.
    //
    //   System.ArgumentException:
    //     The method is neither declared nor inherited by the class of obj.
    // public abstract object GetValue(object obj);
    //
    // Summary:
    //     Returns the value of a field supported by a given object.
    //
    // Parameters:
    //   obj:
    //     A System.TypedReference structure that encapsulates a managed pointer to
    //     a location and a runtime representation of the type that might be stored
    //     at that location.
    //
    // Returns:
    //     An Object containing a field value.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The caller requires the Common Language Specification (CLS) alternative,
    //     but called this method instead.
    // public virtual object GetValueDirect(TypedReference obj);
    //
    // Summary:
    //     Sets the value of the field supported by the given object.
    //
    // Parameters:
    //   obj:
    //     The object whose field value will be set.
    //
    //   value:
    //     The value to assign to the field.
    //
    // Exceptions:
    //   System.FieldAccessException:
    //     The caller does not have permission to access this field.
    //
    //   System.Reflection.TargetException:
    //     The obj parameter is null and the field is an instance field.
    //
    //   System.ArgumentException:
    //     The field does not exist on the object.-or- The value parameter cannot be
    //     converted and stored in the field.
    // public void SetValue(object obj, object value);
    //
    // Summary:
    //     When overridden in a derived class, sets the value of the field supported
    //     by the given object.
    //
    // Parameters:
    //   obj:
    //     The object whose field value will be set.
    //
    //   value:
    //     The value to assign to the field.
    //
    //   invokeAttr:
    //     A field of Binder that specifies the type of binding that is desired (for
    //     example, Binder.CreateInstance or Binder.ExactBinding).
    //
    //   binder:
    //     A set of properties that enables the binding, coercion of argument types,
    //     and invocation of members through reflection. If binder is null, then Binder.DefaultBinding
    //     is used.
    //
    //   culture:
    //     The software preferences of a particular culture.
    //
    // Exceptions:
    //   System.FieldAccessException:
    //     The caller does not have permission to access this field.
    //
    //   System.Reflection.TargetException:
    //     The obj parameter is null and the field is an instance field.
    //
    //   System.ArgumentException:
    //     The field does not exist on the object.-or- The value parameter cannot be
    //     converted and stored in the field.
    // public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);
    //
    // Summary:
    //     Sets the value of the field supported by the given object.
    //
    // Parameters:
    //   obj:
    //     A System.TypedReference structure that encapsulates a managed pointer to
    //     a location and a runtime representation of the type that can be stored at
    //     that location.
    //
    //   value:
    //     The value to assign to the field.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The caller requires the Common Language Specification (CLS) alternative,
    //     but called this method instead.
    // public virtual void SetValueDirect(TypedReference obj, object value);
  }
}
