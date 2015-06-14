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

namespace System.Reflection
{
  // Summary:
  //     Discovers the attributes of a class constructor and provides access to constructor
  //     metadata.
  ////[Serializable]
  ////[ClassInterface(ClassInterfaceType.None)]
  ////[ComDefaultInterface(typeof(_ConstructorInfo))]
  ////[ComVisible(true)]
  public abstract class ConstructorInfo //: MethodBase, _ConstructorInfo
  {
#if SILVERLIGHT && !SILVERLIGHT_5_0
    internal
#else
    protected
#endif
    ConstructorInfo() { }

    // Summary:
    //     Represents the name of the class constructor method as it is stored in metadata.
    //     This name is always ".ctor". This field is read-only.
    ////[ComVisible(true)]
    public static readonly string ConstructorName;
    //
    // Summary:
    //     Represents the name of the type constructor method as it is stored in metadata.
    //     This name is always ".cctor". This property is read-only.
    //[ComVisible(true)]
    public static readonly string TypeConstructorName;

 

    // Summary:
    //     Gets a System.Reflection.MemberTypes value indicating that this member is
    //     a constructor.
    //
    // Returns:
    //     A System.Reflection.MemberTypes value indicating that this member is a constructor.
    //[ComVisible(true)]
    //public override MemberTypes MemberType { get; }

    // Summary:
    //     Invokes the constructor reflected by the instance that has the specified
    //     parameters, providing default values for the parameters not commonly used.
    //
    // Parameters:
    //   parameters:
    //     An array of values that matches the number, order and type (under the constraints
    //     of the default binder) of the parameters for this constructor. If this constructor
    //     takes no parameters, then use either an array with zero elements or null,
    //     as in Object//[] parameters = new Object//[0]. Any object in this array that
    //     is not explicitly initialized with a value will contain the default value
    //     for that object type. For reference-type elements, this value is null. For
    //     value-type elements, this value is 0, 0.0, or false, depending on the specific
    //     element type.
    //
    // Returns:
    //     An instance of the class associated with the constructor.
    //
    // Exceptions:
    //   System.MemberAccessException:
    //     The class is abstract.-or- The constructor is a class initializer.
    //
    //   System.MethodAccessException:
    //     The constructor is private or protected, and the caller lacks System.Security.Permissions.ReflectionPermissionFlag.MemberAccess.
    //
    //   System.ArgumentException:
    //     The parameters array does not contain values that match the types accepted
    //     by this constructor.
    //
    //   System.Reflection.TargetInvocationException:
    //     The invoked constructor throws an exception.
    //
    //   System.Reflection.TargetParameterCountException:
    //     An incorrect number of parameters was passed.
    //
    //   System.NotSupportedException:
    //     Creation of System.TypedReference, System.ArgIterator, and System.RuntimeArgumentHandle
    //     types is not supported.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the necessary code access permission.
    //[DebuggerStepThrough]
    //[DebuggerHidden]
    //public object Invoke(object[] parameters);
    //
    // Summary:
    //     When implemented in a derived class, invokes the constructor reflected by
    //     this ConstructorInfo with the specified arguments, under the constraints
    //     of the specified Binder.
    //
    // Parameters:
    //   invokeAttr:
    //     One of the BindingFlags values that specifies the type of binding.
    //
    //   binder:
    //     A Binder that defines a set of properties and enables the binding, coercion
    //     of argument types, and invocation of members using reflection. If binder
    //     is null, then Binder.DefaultBinding is used.
    //
    //   parameters:
    //     An array of type Object used to match the number, order and type of the parameters
    //     for this constructor, under the constraints of binder. If this constructor
    //     does not require parameters, pass an array with zero elements, as in Object//[]
    //     parameters = new Object//[0]. Any object in this array that is not explicitly
    //     initialized with a value will contain the default value for that object type.
    //     For reference-type elements, this value is null. For value-type elements,
    //     this value is 0, 0.0, or false, depending on the specific element type.
    //
    //   culture:
    //     A System.Globalization.CultureInfo used to govern the coercion of types.
    //     If this is null, the System.Globalization.CultureInfo for the current thread
    //     is used.
    //
    // Returns:
    //     An instance of the class associated with the constructor.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The parameters array does not contain values that match the types accepted
    //     by this constructor, under the constraints of the binder.
    //
    //   System.Reflection.TargetInvocationException:
    //     The invoked constructor throws an exception.
    //
    //   System.Reflection.TargetParameterCountException:
    //     An incorrect number of parameters was passed.
    //
    //   System.NotSupportedException:
    //     Creation of System.TypedReference, System.ArgIterator, and System.RuntimeArgumentHandle
    //     types is not supported.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the necessary code access permissions.
    //
    //   System.MemberAccessException:
    //     The class is abstract.-or- The constructor is a class initializer.
    //
    //   System.MethodAccessException:
    //     The constructor is private or protected, and the caller lacks System.Security.Permissions.ReflectionPermissionFlag.MemberAccess.
    //public abstract object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);
  }
}
