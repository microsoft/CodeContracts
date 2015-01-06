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
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Reflection {
  // Summary:
  //     Obtains information about the attributes of a member and provides access
  //     to member metadata.
  [Immutable]
  public abstract class MemberInfo {
    // Summary:
    //     Initializes a new instance of the System.Reflection.MemberInfo class.
    protected MemberInfo();

    // Summary:
    //     Gets the class that declares this member.
    //
    // Returns:
    //     The Type object for the class that declares this member.
    public abstract Type! DeclaringType { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets a System.Reflection.MemberTypes
    //     value indicating the type of the member � method, constructor, event, and
    //     so on.
    //
    // Returns:
    //     A System.Reflection.MemberTypes value indicating the type of member.
    public abstract MemberTypes MemberType { get; }
    //
    // Summary:
    //     Gets a value that identifies a metadata element.
    //
    // Returns:
    //     A value which, in combination with System.Reflection.MemberInfo.Module, uniquely
    //     identifies a metadata element.
    public virtual int MetadataToken { get; }
    //
    // Summary:
    //     Gets the module in which the type that declares the member represented by
    //     the current System.Reflection.MemberInfo is defined.
    //
    // Returns:
    //     The System.Reflection.Module in which the type that declares the member represented
    //     by the current System.Reflection.MemberInfo is defined.
    public virtual Module Module { get; }
    //
    // Summary:
    //     Gets the name of the current member.
    //
    // Returns:
    //     A System.String containing the name of this member.
    public abstract string! Name { get; }
    //
    // Summary:
    //     Gets the class object that was used to obtain this instance of MemberInfo.
    //
    // Returns:
    //     The Type object through which this MemberInfo object was obtained.
    public abstract Type! ReflectedType { get; }

    // Summary:
    //     When overridden in a derived class, returns an array containing all the custom
    //     attributes.
    //
    // Parameters:
    //   inherit:
    //     Specifies whether to search this member's inheritance chain to find the attributes.
    //
    // Returns:
    //     An array that contains all the custom attributes, or an array with zero elements
    //     if no attributes are defined.
    public abstract object[] GetCustomAttributes(bool inherit) {
    //
    // Summary:
    //     When overridden in a derived class, returns an array of custom attributes
    //     identified by System.Type.
    //
    // Parameters:
    //   inherit:
    //     Specifies whether to search this member's inheritance chain to find the attributes.
    //
    //   attributeType:
    //     The type of attribute to search for. Only attributes that are assignable
    //     to this type are returned.
    //
    // Returns:
    //     An array of custom attributes applied to this member, or an array with zero
    //     (0) elements if no attributes have been applied.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     If attributeType is null.
    //
    //   System.TypeLoadException:
    //     The custom attribute type cannot be loaded.
      return default(abstract);
    }
    public abstract object[]! GetCustomAttributes(Type attributeType, bool inherit) {
    //
    // Summary:
    //     When overridden in a derived class, indicates whether one or more instance
    //     of attributeType is applied to this member.
    //
    // Parameters:
    //   inherit:
    //     Specifies whether to search this member's inheritance chain to find the attributes.
    //
    //   attributeType:
    //     The Type object to which the custom attributes are applied.
    //
    // Returns:
    //     true if one or more instance of attributeType is applied to this member;
    //     otherwise false.
      return default(abstract);
    }
    public abstract bool IsDefined(Type! attributeType, bool inherit) {
      return default(abstract);
    }
  }
}
