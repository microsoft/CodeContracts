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
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Reflection {
  // Summary:
  //     Discovers the attributes of a method and provides access to method metadata.
  public abstract class MethodInfo 
  {
#if !SILVERLIGHT || SILVERLIGHT_5_0
    extern protected MethodInfo();
#else
    extern internal MethodInfo();
#endif

    // Summary:
    //     Initializes a new instance of the System.Reflection.MethodInfo class.

    //
    // Summary:
    //     Gets a System.Reflection.ParameterInfo object that contains information about
    //     the return type of the method, such as whether the return type has custom
    //     modifiers.
    //
    // Returns:
    //     A System.Reflection.ParameterInfo object that contains information about
    //     the return type.
    //
    // Exceptions:
    //   System.NotImplementedException:
    //     This method is not implemented.
    extern public virtual ParameterInfo ReturnParameter { get; }
    //
    // Summary:
    //     Gets the return type of this method.
    //
    // Returns:
    //     The return type of this method.
    public virtual Type ReturnType
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);
        return default(Type);
      }
    }
    //
    // Summary:
    //     Gets the custom attributes for the return type.
    //
    // Returns:
    //     An ICustomAttributeProvider object representing the custom attributes for
    //     the return type.
    public virtual ICustomAttributeProvider ReturnTypeCustomAttributes
    {
      get
      {
        Contract.Ensures(Contract.Result<ICustomAttributeProvider>() != null);
        return default(ICustomAttributeProvider);
      }
    }

    // Summary:
    //     When overridden in a derived class, returns the MethodInfo object for the
    //     method on the direct or indirect base class in which the method represented
    //     by this instance was first declared.
    //
    // Returns:
    //     A MethodInfo object for the first implementation of this method.
    // public abstract MethodInfo GetBaseDefinition();


    //
    // Summary:
    //     Returns a System.Reflection.MethodInfo object that represents a generic method
    //     definition from which the current method can be constructed.
    //
    // Returns:
    //     A System.Reflection.MethodInfo object representing a generic method definition
    //     from which the current method can be constructed.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The current method is not a generic method. That is, System.Reflection.MethodInfo.IsGenericMethod
    //     returns false.
    //
    //   System.NotSupportedException:
    //     This method is not supported.
    public virtual MethodInfo GetGenericMethodDefinition()
    {
      Contract.Ensures(Contract.Result<MethodInfo>() != null);
      return default(MethodInfo);
    }
    //
    // Summary:
    //     Substitutes the elements of an array of types for the type parameters of
    //     the current generic method definition, and returns a System.Reflection.MethodInfo
    //     object representing the resulting constructed method.
    //
    // Parameters:
    //   typeArguments:
    //     An array of types to be substituted for the type parameters of the current
    //     generic method definition.
    //
    // Returns:
    //     A System.Reflection.MethodInfo object that represents the constructed method
    //     formed by substituting the elements of typeArguments for the type parameters
    //     of the current generic method definition.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The current System.Reflection.MethodInfo does not represent a generic method
    //     definition. That is, System.Reflection.MethodInfo.IsGenericMethodDefinition
    //     returns false.
    //
    //   System.ArgumentNullException:
    //     typeArguments is null.-or- Any element of typeArguments is null.
    //
    //   System.ArgumentException:
    //     The number of elements in typeArguments is not the same as the number of
    //     type parameters of the current generic method definition.-or- An element
    //     of typeArguments does not satisfy the constraints specified for the corresponding
    //     type parameter of the current generic method definition.
    //
    //   System.NotSupportedException:
    //     This method is not supported.
    public virtual MethodInfo MakeGenericMethod(params Type[] typeArguments)
    {
      Contract.Ensures(Contract.Result<MethodInfo>() != null);
      return default(MethodInfo);
    }
  }
}
