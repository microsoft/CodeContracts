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

#if !SILVERLIGHT

using System;
using System.Collections;
using System.Diagnostics.Contracts;

namespace System.ComponentModel
{
  // Summary:
  //     Provides supplemental metadata to the System.ComponentModel.TypeDescriptor.
  public abstract class TypeDescriptionProvider
  {
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.TypeDescriptionProvider
    //     class.
    //protected TypeDescriptionProvider();
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.TypeDescriptionProvider
    //     class using a parent type description provider.
    //
    // Parameters:
    //   parent:
    //     The parent type description provider.
    //protected TypeDescriptionProvider(TypeDescriptionProvider parent);

    // Summary:
    //     Creates an object that can substitute for another data type.
    //
    // Parameters:
    //   provider:
    //     An optional service provider.
    //
    //   objectType:
    //     The type of object to create. This parameter is never null.
    //
    //   argTypes:
    //     An optional array of types that represent the parameter types to be passed
    //     to the object's constructor. This array can be null or of zero length.
    //
    //   args:
    //     An optional array of parameter values to pass to the object's constructor.
    //
    // Returns:
    //     The substitute System.Object.
    public virtual object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
    {
      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
    //
    // Summary:
    //     Gets a per-object cache, accessed as an System.Collections.IDictionary of
    //     key/value pairs.
    //
    // Parameters:
    //   instance:
    //     The object for which to get the cache.
    //
    // Returns:
    //     An System.Collections.IDictionary if the provided object supports caching;
    //     otherwise, null.
    //public virtual IDictionary GetCache(object instance);
    //
    // Summary:
    //     Gets an extended custom type descriptor for the given object.
    //
    // Parameters:
    //   instance:
    //     The object for which to get the extended type descriptor.
    //
    // Returns:
    //     An System.ComponentModel.ICustomTypeDescriptor that can provide extended
    //     metadata for the object.
    //public virtual ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance);
    //
    // Summary:
    //     Gets the name of the specified component, or null if the component has no
    //     name.
    //
    // Parameters:
    //   component:
    //     The specified component.
    //
    // Returns:
    //     The name of the specified component.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    public virtual string GetFullComponentName(object component)
    {
      Contract.Requires(component != null);

      return default(string);
    }
    //
    // Summary:
    //     Performs normal reflection against the given object.
    //
    // Parameters:
    //   instance:
    //     An instance of the type (should not be null).
    //
    // Returns:
    //     A System.Type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     instance is null.
    public Type GetReflectionType(object instance)
    {
      Contract.Requires(instance != null);

      Contract.Ensures(Contract.Result<Type>() != null);

      return default(Type);
    }
    //
    // Summary:
    //     Performs normal reflection against a type.
    //
    // Parameters:
    //   objectType:
    //     The type of object for which to retrieve the System.Reflection.IReflect.
    //
    // Returns:
    //     A System.Type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     objectType is null.
    public Type GetReflectionType(Type objectType)
    {
      Contract.Requires(objectType != null);

      Contract.Ensures(Contract.Result<Type>() != null);

      return default(Type);
    }
    //
    // Summary:
    //     Performs normal reflection against the given object with the given type.
    //
    // Parameters:
    //   objectType:
    //     The type of object for which to retrieve the System.Reflection.IReflect.
    //
    //   instance:
    //     An instance of the type. Can be null.
    //
    // Returns:
    //     A System.Type.
    //public virtual Type GetReflectionType(Type objectType, object instance);
    //
    // Summary:
    //     Gets a custom type descriptor for the given object.
    //
    // Parameters:
    //   instance:
    //     An instance of the type. Can be null if no instance was passed to the System.ComponentModel.TypeDescriptor.
    //
    // Returns:
    //     An System.ComponentModel.ICustomTypeDescriptor that can provide metadata
    //     for the type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     instance is null.
    //public ICustomTypeDescriptor GetTypeDescriptor(object instance);
    //
    // Summary:
    //     Gets a custom type descriptor for the given type.
    //
    // Parameters:
    //   objectType:
    //     The type of object for which to retrieve the type descriptor.
    //
    // Returns:
    //     An System.ComponentModel.ICustomTypeDescriptor that can provide metadata
    //     for the type.
    //public ICustomTypeDescriptor GetTypeDescriptor(Type objectType);
    //
    // Summary:
    //     Gets a custom type descriptor for the given type and object.
    //
    // Parameters:
    //   objectType:
    //     The type of object for which to retrieve the type descriptor.
    //
    //   instance:
    //     An instance of the type. Can be null if no instance was passed to the System.ComponentModel.TypeDescriptor.
    //
    // Returns:
    //     An System.ComponentModel.ICustomTypeDescriptor that can provide metadata
    //     for the type.
    //public virtual ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance);
  }
}

#endif