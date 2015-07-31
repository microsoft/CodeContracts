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

#if NETFRAMEWORK_4_5 || SILVERLIGHT_5_0

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace System.Reflection
{
  //
  // Summary:
  //     Contains static methods for retrieving custom attributes.
  public static class CustomAttributeExtensions
  {
    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     member.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A custom attribute that matches attributeType, or null if no such attribute is
    //     found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(Attribute);
    }

    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     parameter.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A custom attribute that matches attributeType, or null if no such attribute is
    //     found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(Attribute);
    }

    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     module.
    //
    // Parameters:
    //   element:
    //     The module to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A custom attribute that matches attributeType, or null if no such attribute is
    //     found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    [Pure]
    public static Attribute GetCustomAttribute(this Module element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(Attribute);
    }

    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     assembly.
    //
    // Parameters:
    //   element:
    //     The assembly to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A custom attribute that matches attributeType, or null if no such attribute is
    //     found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    [Pure]
    public static Attribute GetCustomAttribute(this Assembly element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(Attribute);
    }

    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     parameter, and optionally inspects the ancestors of that parameter.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Returns:
    //     A custom attribute matching attributeType, or null if no such attribute is found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType, bool inherit)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(Attribute);
    }

    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     member, and optionally inspects the ancestors of that member.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Returns:
    //     A custom attribute that matches attributeType, or null if no such attribute is
    //     found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType, bool inherit)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(Attribute);
    }

    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     member.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A custom attribute that matches T, or null if no such attribute is found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
    {
      Contract.Requires(element != null);
      return default(T);
    }

    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     parameter.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A custom attribute that matches T, or null if no such attribute is found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static T GetCustomAttribute<T>(this ParameterInfo element) where T : Attribute
    {
      Contract.Requires(element != null);
      return default(T);
    }

    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     module.
    //
    // Parameters:
    //   element:
    //     The module to inspect.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A custom attribute that matches T, or null if no such attribute is found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    [Pure]
    public static T GetCustomAttribute<T>(this Module element) where T : Attribute
    {
      Contract.Requires(element != null);
      return default(T);
    }

    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     assembly.
    //
    // Parameters:
    //   element:
    //     The assembly to inspect.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A custom attribute that matches T, or null if no such attribute is found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    [Pure]
    public static T GetCustomAttribute<T>(this Assembly element) where T : Attribute
    {
      Contract.Requires(element != null);
      return default(T);
    }

    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     parameter, and optionally inspects the ancestors of that parameter.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A custom attribute that matches T, or null if no such attribute is found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static T GetCustomAttribute<T>(this ParameterInfo element, bool inherit) where T : Attribute
    {
      Contract.Requires(element != null);
      return default(T);
    }

    //
    // Summary:
    //     Retrieves a custom attribute of a specified type that is applied to a specified
    //     member, and optionally inspects the ancestors of that member.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A custom attribute that matches T, or null if no such attribute is found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one of the requested attributes was found.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static T GetCustomAttribute<T>(this MemberInfo element, bool inherit) where T : Attribute
    {
      Contract.Requires(element != null);
      return default(T);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes that are applied to a specified member.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element, or an empty
    //     collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element)
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes that are applied to a specified parameter.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element, or an empty
    //     collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element)
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes that are applied to a specified module.
    //
    // Parameters:
    //   element:
    //     The module to inspect.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element, or an empty
    //     collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this Module element)
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes that are applied to a specified assembly.
    //
    // Parameters:
    //   element:
    //     The assembly to inspect.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element, or an empty
    //     collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element)
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified module.
    //
    // Parameters:
    //   element:
    //     The module to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     attributeType, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this Module element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified member.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     attributeType, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified parameter.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     attributeType, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified assembly.
    //
    // Parameters:
    //   element:
    //     The assembly to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     attributeType, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes that are applied to a specified parameter,
    //     and optionally inspects the ancestors of that parameter.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element, or an empty
    //     collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, bool inherit)
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes that are applied to a specified member,
    //     and optionally inspects the ancestors of that member.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element that match
    //     the specified criteria, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, bool inherit)
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified parameter, and optionally inspects the ancestors of that parameter.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     attributeType, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType, bool inherit)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified member, and optionally inspects the ancestors of that member.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     attributeType, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType, bool inherit)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      Contract.Ensures(Contract.Result<IEnumerable<Attribute>>() != null);
      return default(IEnumerable<Attribute>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified member.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     T, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element) where T : Attribute
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
      return default(IEnumerable<T>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified parameter.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     T, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element) where T : Attribute
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
      return default(IEnumerable<T>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified module.
    //
    // Parameters:
    //   element:
    //     The module to inspect.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     T, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    [Pure]
    public static IEnumerable<T> GetCustomAttributes<T>(this Module element) where T : Attribute
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
      return default(IEnumerable<T>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified assembly.
    //
    // Parameters:
    //   element:
    //     The assembly to inspect.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     T, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    [Pure]
    public static IEnumerable<T> GetCustomAttributes<T>(this Assembly element) where T : Attribute
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
      return default(IEnumerable<T>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified parameter, and optionally inspects the ancestors of that parameter.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     T, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element, bool inherit) where T : Attribute
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
      return default(IEnumerable<T>);
    }

    //
    // Summary:
    //     Retrieves a collection of custom attributes of a specified type that are applied
    //     to a specified member, and optionally inspects the ancestors of that member.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Type parameters:
    //   T:
    //     The type of attribute to search for.
    //
    // Returns:
    //     A collection of the custom attributes that are applied to element and that match
    //     T, or an empty collection if no such attributes exist.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element is null.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    //
    //   T:System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    [Pure]
    public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element, bool inherit) where T : Attribute
    {
      Contract.Requires(element != null);
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
      return default(IEnumerable<T>);
    }

    //
    // Summary:
    //     Indicates whether custom attributes of a specified type are applied to a specified
    //     parameter.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    // Returns:
    //     true if an attribute of the specified type is applied to element; otherwise,
    //     false.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    [Pure]
    public static bool IsDefined(this ParameterInfo element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(bool);
    }

    //
    // Summary:
    //     Indicates whether custom attributes of a specified type are applied to a specified
    //     member.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    // Returns:
    //     true if an attribute of the specified type is applied to element; otherwise,
    //     false.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    [Pure]
    public static bool IsDefined(this MemberInfo element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(bool);
    }

    //
    // Summary:
    //     Indicates whether custom attributes of a specified type are applied to a specified
    //     module.
    //
    // Parameters:
    //   element:
    //     The module to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    // Returns:
    //     true if an attribute of the specified type is applied to element; otherwise,
    //     false.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    [Pure]
    public static bool IsDefined(this Module element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(bool);
    }

    //
    // Summary:
    //     Indicates whether custom attributes of a specified type are applied to a specified
    //     assembly.
    //
    // Parameters:
    //   element:
    //     The assembly to inspect.
    //
    //   attributeType:
    //     The type of the attribute to search for.
    //
    // Returns:
    //     true if an attribute of the specified type is applied to element; otherwise,
    //     false.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    [Pure]
    public static bool IsDefined(this Assembly element, Type attributeType)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(bool);
    }

    //
    // Summary:
    //     Indicates whether custom attributes of a specified type are applied to a specified
    //     parameter, and, optionally, applied to its ancestors.
    //
    // Parameters:
    //   element:
    //     The parameter to inspect.
    //
    //   attributeType:
    //     The type of attribute to search for.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Returns:
    //     true if an attribute of the specified type is applied to element; otherwise,
    //     false.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    [Pure]
    public static bool IsDefined(this ParameterInfo element, Type attributeType, bool inherit)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(bool);
    }

    //
    // Summary:
    //     Indicates whether custom attributes of a specified type are applied to a specified
    //     member, and, optionally, applied to its ancestors.
    //
    // Parameters:
    //   element:
    //     The member to inspect.
    //
    //   attributeType:
    //     The type of the attribute to search for.
    //
    //   inherit:
    //     true to inspect the ancestors of element; otherwise, false.
    //
    // Returns:
    //     true if an attribute of the specified type is applied to element; otherwise,
    //     false.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     element or attributeType is null.
    //
    //   T:System.ArgumentException:
    //     attributeType is not derived from System.Attribute.
    //
    //   T:System.NotSupportedException:
    //     element is not a constructor, method, property, event, type, or field.
    [Pure]
    public static bool IsDefined(this MemberInfo element, Type attributeType, bool inherit)
    {
      Contract.Requires(element != null);
      Contract.Requires(attributeType != null);
      return default(bool);
    }
  }
}

#endif