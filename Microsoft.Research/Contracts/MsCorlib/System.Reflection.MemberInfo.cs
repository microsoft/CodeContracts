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

namespace System.Reflection
{
  // Summary:
  //     Obtains information about the attributes of a member and provides access
  //     to member metadata.
  [Immutable]
  [ContractClass(typeof(MemberInfoContracts))]
  public abstract class MemberInfo
  {

#if SILVERLIGHT && !SILVERLIGHT_5_0
    extern internal MemberInfo();
#else
    extern protected MemberInfo();
#endif

    public virtual Type DeclaringType
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null || MemberType == MemberTypes.TypeInfo);
        return default(Type);
      }
    }

    public abstract MemberTypes MemberType { get; }
    // public virtual int MetadataToken { get; }

    public virtual Module Module
    {
      get
      {
        Contract.Ensures(Contract.Result<Module>() != null);
        return default(Module);
      }
    }
    //
    // Summary:
    //     Gets the module in which the type that declares the member represented by
    //     the current System.Reflection.MemberInfo is defined.
    //
    // Returns:
    //     The System.Reflection.Module in which the type that declares the member represented
    //     by the current System.Reflection.MemberInfo is defined.
    // public virtual Module Module { get { return default(Module); } }
    //
    // Summary:
    //     Gets the name of the current member.
    //
    // Returns:
    //     A System.String containing the name of this member.
    public virtual string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    //
    // Summary:
    //     Gets the class object that was used to obtain this instance of MemberInfo.
    //
    // Returns:
    //     The Type object through which this MemberInfo object was obtained.
    public virtual Type ReflectedType
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);
        return default(Type);
      }
    }
 
    public abstract object[] GetCustomAttributes(Type attributeType, bool inherit);
    public abstract object[] GetCustomAttributes(bool inherit);
  }

  [ContractClassFor(typeof(MemberInfo))]
  abstract class MemberInfoContracts : MemberInfo
  {
    //
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
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This member belongs to a type that is loaded into the reflection-only context.
    //     See How to: Load Assemblies into the Reflection-Only Context.
    //
    //   System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    public override object[] GetCustomAttributes(bool inherit)
    {
      Contract.Ensures(Contract.Result<object[]>() != null);

      return null;
    }
    //
    // Summary:
    //     When overridden in a derived class, returns an array of custom attributes
    //     identified by System.Type.
    //
    // Parameters:
    //   attributeType:
    //     The type of attribute to search for. Only attributes that are assignable
    //     to this type are returned.
    //
    //   inherit:
    //     Specifies whether to search this member's inheritance chain to find the attributes.
    //
    // Returns:
    //     An array of custom attributes applied to this member, or an array with zero
    //     (0) elements if no attributes have been applied.
    //
    // Exceptions:
    //   System.TypeLoadException:
    //     A custom attribute type cannot be loaded.
    //
    //   System.ArgumentNullException:
    //     If attributeType is null.
    //
    //   System.InvalidOperationException:
    //     This member belongs to a type that is loaded into the reflection-only context.
    //     See How to: Load Assemblies into the Reflection-Only Context.
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      Contract.Requires(attributeType != null);
      Contract.Ensures(Contract.Result<object[]>() != null);

      return null;
    }

  }
}