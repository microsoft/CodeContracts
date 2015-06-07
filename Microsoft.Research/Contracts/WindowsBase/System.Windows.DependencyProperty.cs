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

#region Assembly WindowsBase.dll, v4.0.0.0
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\WindowsBase.dll
#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime;

namespace System.Windows
{
  // Summary:
  //     Represents a property that can be set through methods such as, styling, data
  //     binding, animation, and inheritance.
  public sealed class DependencyProperty
  {
    private DependencyProperty() { }

    // Summary:
    //     Specifies a static value that is used by the WPF property system rather than
    //     null to indicate that the property exists, but does not have its value set
    //     by the property system.
    //
    // Returns:
    //     An unset value. This is effectively the result of a call to the System.Object
    //     constructor.
    public static readonly object UnsetValue;

    // Summary:
    //     Gets the default metadata of the dependency property.
    //
    // Returns:
    //     The default metadata of the dependency property.
    //public PropertyMetadata DefaultMetadata { get; }
    //
    // Summary:
    //     Gets an internally generated value that uniquely identifies the dependency
    //     property.
    //
    // Returns:
    //     A unique numeric identifier.
    extern public int GlobalIndex { get; }
    //
    // Summary:
    //     Gets the name of the dependency property.
    //
    // Returns:
    //     The name of the property.
    extern public string Name { get; }
    //
    // Summary:
    //     Gets the type of the object that registered the dependency property with
    //     the property system, or added itself as owner of the property.
    //
    // Returns:
    //     The type of the object that registered the property or added itself as owner
    //     of the property.
      public Type OwnerType
      {
          get
          {
              Contract.Ensures(Contract.Result<Type>() != null);
              return null;
          }
      }
    //
    // Summary:
    //     Gets the type that the dependency property uses for its value.
    //
    // Returns:
    //     The System.Type of the property value.
      public Type PropertyType
      {
          get
          {
              Contract.Ensures(Contract.Result<Type>() != null);
              return null;
          }
      }
    //
    // Summary:
    //     Gets a value that indicates whether the dependency property identified by
    //     this System.Windows.DependencyProperty instance is a read-only dependency
    //     property.
    //
    // Returns:
    //     true if the dependency property is read-only; otherwise, false.
    extern public bool ReadOnly { get; }
    //
    // Summary:
    //     Gets the value validation callback for the dependency property.
    //
    // Returns:
    //     The value validation callback for this dependency property, as provided for
    //     the validateValueCallback parameter in the original dependency property registration.
    //public ValidateValueCallback ValidateValueCallback { get; }

    // Summary:
    //     Adds another type as an owner of a dependency property that has already been
    //     registered.
    //
    // Parameters:
    //   ownerType:
    //     The type to add as an owner of this dependency property.
    //
    // Returns:
    //     A reference to the original System.Windows.DependencyProperty identifier
    //     that identifies the dependency property. This identifier should be exposed
    //     by the adding class as a public static readonly field.
    public DependencyProperty AddOwner(Type ownerType)
    {
      Contract.Ensures(Contract.Result<DependencyProperty>() != null);

      return null;
    }
    //
    // Summary:
    //     Adds another type as an owner of a dependency property that has already been
    //     registered, providing dependency property metadata for the dependency property
    //     as it will exist on the provided owner type.
    //
    // Parameters:
    //   ownerType:
    //     The type to add as owner of this dependency property.
    //
    //   typeMetadata:
    //     The metadata that qualifies the dependency property as it exists on the provided
    //     type.
    //
    // Returns:
    //     A reference to the original System.Windows.DependencyProperty identifier
    //     that identifies the dependency property. This identifier should be exposed
    //     by the adding class as a public static readonly field.
    /*public DependencyProperty AddOwner(Type ownerType, PropertyMetadata typeMetadata)
    {
    }
     */

    //public PropertyMetadata GetMetadata(DependencyObjectType dependencyObjectType);
    //
    //    public PropertyMetadata GetMetadata(Type forType);
    //
    // Summary:
    //     Determines whether a specified value is acceptable for this dependency property's
    //     type, as checked against the property type provided in the original dependency
    //     property registration.
    //
    // Parameters:
    //   value:
    //     The value to check.
    //
    // Returns:
    //     true if the specified value is the registered property type or an acceptable
    //     derived type; otherwise, false.
    public bool IsValidType(object value)
    {
      return false;
    }
    //
    // Summary:
    //     Determines whether the provided value is accepted for the type of property
    //     through basic type checking, and also potentially if it is within the allowed
    //     range of values for that type.
    //
    // Parameters:
    //   value:
    //     The value to check.
    //
    // Returns:
    //     true if the value is acceptable and is of the correct type or a derived type;
    //     otherwise, false.
    public bool IsValidValue(object value)
    {
      return false;

    }

    //
    // Summary:
    //     Specifies alternate metadata for this dependency property when it is present
    //     on instances of a specified type, overriding the metadata that existed for
    //     the dependency property as it was inherited from base types.
    //
    // Parameters:
    //   forType:
    //     The type where this dependency property is inherited and where the provided
    //     alternate metadata will be applied.
    //
    //   typeMetadata:
    //     The metadata to apply to the dependency property on the overriding type.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     An attempt was made to override metadata on a read-only dependency property
    //     (that operation cannot be done using this signature).
    //
    //   System.ArgumentException:
    //     Metadata was already established for the dependency property as it exists
    //     on the provided type.
    //    public void OverrideMetadata(Type forType, PropertyMetadata typeMetadata);
    //
    // Summary:
    //     Supplies alternate metadata for a read-only dependency property when it is
    //     present on instances of a specified type, overriding the metadata that was
    //     provided in the initial dependency property registration. You must pass the
    //     System.Windows.DependencyPropertyKey for the read-only dependency property
    //     to avoid raising an exception.
    //
    // Parameters:
    //   forType:
    //     The type where this dependency property is inherited and where the provided
    //     alternate metadata will be applied.
    //
    //   typeMetadata:
    //     The metadata to apply to the dependency property on the overriding type.
    //
    //   key:
    //     The access key for a read-only dependency property.
    //  public void OverrideMetadata(Type forType, PropertyMetadata typeMetadata, DependencyPropertyKey key);
    //
    // Summary:
    //     Registers a dependency property with the specified property name, property
    //     type, and owner type.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register. The name must be unique
    //     within the registration namespace of the owner type.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    // Returns:
    //     A dependency property identifier that should be used to set the value of
    //     a public static readonly field in your class. That identifier is then used
    //     to reference the dependency property later, for operations such as setting
    //     its value programmatically or obtaining metadata.
    public static DependencyProperty Register(string name, Type propertyType, Type ownerType)
    {
      Contract.Requires(name != null);
      Contract.Requires(propertyType != null);
      Contract.Requires(ownerType != null);
      Contract.Ensures(Contract.Result<DependencyProperty>() != null);

      return null;
    }
    //
    // Summary:
    //     Registers a dependency property with the specified property name, property
    //     type, owner type, and property metadata.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    //   typeMetadata:
    //     Property metadata for the dependency property.
    //
    // Returns:
    //     A dependency property identifier that should be used to set the value of
    //     a public static readonly field in your class. That identifier is then used
    //     to reference the dependency property later, for operations such as setting
    //     its value programmatically or obtaining metadata.
    //    public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata);
    //
    // Summary:
    //     Registers a dependency property with the specified property name, property
    //     type, owner type, property metadata, and a value validation callback for
    //     the property.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    //   typeMetadata:
    //     Property metadata for the dependency property.
    //
    //   validateValueCallback:
    //     A reference to a callback that should perform any custom validation of the
    //     dependency property value beyond typical type validation.
    //
    // Returns:
    //     A dependency property identifier that should be used to set the value of
    //     a public static readonly field in your class. That identifier is then used
    //     to reference the dependency property later, for operations such as setting
    //     its value programmatically or obtaining metadata.
    //    public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback);
    //
    // Summary:
    //     Registers an attached property with the specified property name, property
    //     type, and owner type.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    // Returns:
    //     A dependency property identifier that should be used to set the value of
    //     a public static readonly field in your class. That identifier is then used
    //     to reference the dependency property later, for operations such as setting
    //     its value programmatically or obtaining metadata.
    //    public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType);
    //
    // Summary:
    //     Registers an attached property with the specified property name, property
    //     type, owner type, and property metadata.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    //   defaultMetadata:
    //     Property metadata for the dependency property. This can include the default
    //     value as well as other characteristics.
    //
    // Returns:
    //     A dependency property identifier that should be used to set the value of
    //     a public static readonly field in your class. That identifier is then used
    //     to reference the dependency property later, for operations such as setting
    //     its value programmatically or obtaining metadata.
    // public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata);
    //
    // Summary:
    //     Registers an attached property with the specified property type, owner type,
    //     property metadata, and value validation callback for the property.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    //   defaultMetadata:
    //     Property metadata for the dependency property. This can include the default
    //     value as well as other characteristics.
    //
    //   validateValueCallback:
    //     A reference to a callback that should perform any custom validation of the
    //     dependency property value beyond typical type validation.
    //
    // Returns:
    //     A dependency property identifier that should be used to set the value of
    //     a public static readonly field in your class. That identifier is then used
    //     to reference the dependency property later, for operations such as setting
    //     its value programmatically or obtaining metadata.
    //public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata, ValidateValueCallback validateValueCallback);
    //
    // Summary:
    //     Registers a read-only attached property, with the specified property type,
    //     owner type, and property metadata.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    //   defaultMetadata:
    //     Property metadata for the dependency property.
    //
    // Returns:
    //     A dependency property key that should be used to set the value of a static
    //     read-only field in your class, which is then used to reference the dependency
    //     property later.
    // public static DependencyPropertyKey RegisterAttachedReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata);
    //
    // Summary:
    //     Registers a read-only attached property, with the specified property type,
    //     owner type, property metadata, and a validation callback.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    //   defaultMetadata:
    //     Property metadata for the dependency property.
    //
    //   validateValueCallback:
    //     A reference to a user-created callback that should perform any custom validation
    //     of the dependency property value beyond typical type validation.
    //
    // Returns:
    //     A dependency property key that should be used to set the value of a static
    //     read-only field in your class, which is then used to reference the dependency
    //     property.
    //    public static DependencyPropertyKey RegisterAttachedReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata, ValidateValueCallback validateValueCallback);
    //
    // Summary:
    //     Registers a read-only dependency property, with the specified property type,
    //     owner type, and property metadata.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    //   typeMetadata:
    //     Property metadata for the dependency property.
    //
    // Returns:
    //     A dependency property key that should be used to set the value of a static
    //     read-only field in your class, which is then used to reference the dependency
    //     property.
    //public static DependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata);
    //
    // Summary:
    //     Registers a read-only dependency property, with the specified property type,
    //     owner type, property metadata, and a validation callback.
    //
    // Parameters:
    //   name:
    //     The name of the dependency property to register.
    //
    //   propertyType:
    //     The type of the property.
    //
    //   ownerType:
    //     The owner type that is registering the dependency property.
    //
    //   typeMetadata:
    //     Property metadata for the dependency property.
    //
    //   validateValueCallback:
    //     A reference to a user-created callback that should perform any custom validation
    //     of the dependency property value beyond typical type validation.
    //
    // Returns:
    //     A dependency property key that should be used to set the value of a static
    //     read-only field in your class, which is then used to reference the dependency
    //     property later.
    // public static DependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback);
    //
    // Summary:
    //     Returns the string representation of the dependency property.
    //
    // Returns:
    //     The string representation of the dependency property.
  }
}
