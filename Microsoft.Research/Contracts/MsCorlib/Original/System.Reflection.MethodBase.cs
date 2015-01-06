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
  //     Provides information about methods and constructors.
  [Immutable] // Base class is immutable.
  public abstract class MethodBase : MemberInfo {
    // Summary:
    //     Initializes a new instance of the System.Reflection.MethodBase class.
    protected MethodBase();

    // Summary:
    //     Gets the attributes associated with this method.
    //
    // Returns:
    //     One of the System.Reflection.MethodAttributes values.
    public abstract MethodAttributes Attributes { get; }
    //
    // Summary:
    //     Gets a value indicating the calling conventions for this method.
    //
    // Returns:
    //     The System.Reflection.CallingConventions for this method.
    public virtual CallingConventions CallingConvention { get; }
    //
    // Summary:
    //     Gets a value indicating whether the generic method contains unassigned generic
    //     type parameters.
    //
    // Returns:
    //     true if the current System.Reflection.MethodBase object represents a generic
    //     method that contains unassigned generic type parameters; otherwise, false.
    public virtual bool ContainsGenericParameters { get; }
    //
    // Summary:
    //     Gets a value indicating whether the method is abstract.
    //
    // Returns:
    //     true if the method is abstract; otherwise, false.
    public bool IsAbstract { get; }
    //
    // Summary:
    //     Gets a value indicating whether this method can be called by other classes
    //     in the same assembly.
    //
    // Returns:
    //     true if this method can be called by other classes in the same assembly;
    //     otherwise, false.
    public bool IsAssembly { get; }
    //
    // Summary:
    //     Gets a value indicating whether the method is a constructor.
    //
    // Returns:
    //     true if this method is a constructor represented by a System.Reflection.ConstructorInfo
    //     object (see note in Remarks about System.Reflection.Emit.ConstructorBuilder
    //     objects); otherwise, false.
    [ComVisible(true)]
    public bool IsConstructor { get; }
    //
    // Summary:
    //     Gets a value indicating whether access to this method is restricted to members
    //     of the class and members of its derived classes.
    //
    // Returns:
    //     true if access to the class is restricted to members of the class itself
    //     and to members of its derived classes; otherwise, false.
    public bool IsFamily { get; }
    //
    // Summary:
    //     Gets a value indicating whether this method can be called by derived classes
    //     if they are in the same assembly.
    //
    // Returns:
    //     true if access to this method is restricted to members of the class itself
    //     and to members of derived classes that are in the same assembly; otherwise,
    //     false.
    public bool IsFamilyAndAssembly { get; }
    //
    // Summary:
    //     Gets a value indicating whether this method can be called by derived classes,
    //     wherever they are, and by all classes in the same assembly.
    //
    // Returns:
    //     true if access to this method is restricted to members of the class itself,
    //     members of derived classes wherever they are, and members of other classes
    //     in the same assembly; otherwise, false.
    public bool IsFamilyOrAssembly { get; }
    //
    // Summary:
    //     Gets a value indicating whether this method is final.
    //
    // Returns:
    //     true if this method is final; otherwise, false.
    public bool IsFinal { get; }
    //
    // Summary:
    //     Gets a value indicating whether the method is generic.
    //
    // Returns:
    //     true if the current System.Reflection.MethodBase represents a generic method;
    //     otherwise, false.
    public virtual bool IsGenericMethod { get; }
    //
    // Summary:
    //     Gets a value indicating whether the method is a generic method definition.
    //
    // Returns:
    //     true if the current System.Reflection.MethodBase object represents the definition
    //     of a generic method; otherwise, false.
    public virtual bool IsGenericMethodDefinition { get; }
    //
    // Summary:
    //     Gets a value indicating whether only a member of the same kind with exactly
    //     the same signature is hidden in the derived class.
    //
    // Returns:
    //     true if the member is hidden by signature; otherwise, false.
    public bool IsHideBySig { get; }
    //
    // Summary:
    //     Gets a value indicating whether this member is private.
    //
    // Returns:
    //     true if access to this method is restricted to other members of the class
    //     itself; otherwise, false.
    public bool IsPrivate { get; }
    //
    // Summary:
    //     Gets a value indicating whether this is a public method.
    //
    // Returns:
    //     true if this method is public; otherwise, false.
    public bool IsPublic { get; }
    //
    // Summary:
    //     Gets a value indicating whether this method has a special name.
    //
    // Returns:
    //     true if this method has a special name; otherwise, false.
    public bool IsSpecialName { get; }
    //
    // Summary:
    //     Gets a value indicating whether the method is static.
    //
    // Returns:
    //     true if this method is static; otherwise, false.
    public bool IsStatic { get; }
    //
    // Summary:
    //     Gets a value indicating whether the method is virtual.
    //
    // Returns:
    //     true if this method is virtual; otherwise, false.
    public bool IsVirtual { get; }
    //
    // Summary:
    //     Gets a handle to the internal metadata representation of a method.
    //
    // Returns:
    //     A System.RuntimeMethodHandle object.
    public abstract RuntimeMethodHandle MethodHandle { get; }

    // Summary:
    //     Returns a MethodBase object representing the currently executing method.
    //
    // Returns:
    //     A MethodBase object representing the currently executing method.
    public static MethodBase GetCurrentMethod() {
    //
    // Summary:
    //     Returns an array of System.Type objects that represent the type arguments
    //     of a generic method or the type parameters of a generic method definition.
    //
    // Returns:
    //     An array of System.Type objects that represent the type arguments of a generic
    //     method or the type parameters of a generic method definition. Returns an
    //     empty array if the current method is not a generic method.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The current object is a System.Reflection.ConstructorInfo. Generic constructors
    //     are not supported in the .NET Framework version 2.0. This exception is the
    //     default behavior if this method is not overridden in a derived class.
      return default(MethodBase);
    }
    [ComVisible(true)]
    public virtual Type[]! GetGenericArguments() {
    //
    // Summary:
    //     When overridden in a derived class, gets a System.Reflection.MethodBody object
    //     that provides access to the MSIL stream, local variables, and exceptions
    //     for the current method.
    //
    // Returns:
    //     A System.Reflection.MethodBody object that provides access to the MSIL stream,
    //     local variables, and exceptions for the current method.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     This method is invalid unless overridden in a derived class.
      return default(virtual);
    }
    public virtual MethodBody GetMethodBody() {
    //
    // Summary:
    //     Gets method information by using the method's internal metadata representation
    //     (handle).
    //
    // Parameters:
    //   handle:
    //     The method's handle.
    //
    // Returns:
    //     A MethodBase containing information about the method.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     handle is invalid.
      return default(virtual);
    }
    public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle) {
    //
    // Summary:
    //     Gets a System.Reflection.MethodBase object for the constructor or method
    //     represented by the specified handle, for the specified generic type.
    //
    // Parameters:
    //   declaringType:
    //     A handle to the generic type that defines the constructor or method.
    //
    //   handle:
    //     A handle to the internal metadata representation of a constructor or method.
    //
    // Returns:
    //     A System.Reflection.MethodBase object representing the method or constructor
    //     specified by handle, in the generic type specified by declaringType.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     handle is invalid.
      return default(MethodBase);
    }
    [ComVisible(false)]
    public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType) {
    //
    // Summary:
    //     When overridden in a derived class, returns the System.Reflection.MethodImplAttributes
    //     flags.
    //
    // Returns:
    //     The MethodImplAttributes flags.
      return default(MethodBase);
    }
    public abstract MethodImplAttributes GetMethodImplementationFlags() {
    //
    // Summary:
    //     When overridden in a derived class, gets the parameters of the specified
    //     method or constructor.
    //
    // Returns:
    //     An array of type ParameterInfo containing information that matches the signature
    //     of the method (or constructor) reflected by this MethodBase instance.
      return default(abstract);
    }
    public abstract ParameterInfo![]! GetParameters() {
    //
    // Summary:
    //     Invokes the method or constructor represented by the current instance, using
    //     the specified parameters.
    //
    // Parameters:
    //   obj:
    //     The object on which to invoke the method or constructor. If a method is static,
    //     this argument is ignored. If a constructor is static, this argument must
    //     be null or an instance of the class that defines the constructor.
    //
    //   parameters:
    //     An argument list for the invoked method or constructor. This is an array
    //     of objects with the same number, order, and type as the parameters of the
    //     method or constructor to be invoked. If there are no parameters, parameters
    //     should be null.If the method or constructor represented by this instance
    //     takes a ref parameter (ByRef in Visual Basic), no special attribute is required
    //     for that parameter in order to invoke the method or constructor using this
    //     function. Any object in this array that is not explicitly initialized with
    //     a value will contain the default value for that object type. For reference-type
    //     elements, this value is null. For value-type elements, this value is 0, 0.0,
    //     or false, depending on the specific element type.
    //
    // Returns:
    //     An object containing the return value of the invoked method, or null in the
    //     case of a constructor.
    //
    // Exceptions:
    //   System.MethodAccessException:
    //     The caller does not have permission to execute the constructor.
    //
    //   System.InvalidOperationException:
    //     The type that declares the method is an open generic type. That is, the System.Type.ContainsGenericParameters
    //     property returns true for the declaring type.
    //
    //   System.Reflection.TargetParameterCountException:
    //     The parameters array does not have the correct number of arguments.
    //
    //   System.Reflection.TargetInvocationException:
    //     The invoked method or constructor throws an exception.
    //
    //   System.ArgumentException:
    //     The elements of the parameters array do not match the signature of the method
    //     or constructor reflected by this instance.
    //
    //   System.Reflection.TargetException:
    //     The obj parameter is null and the method is not static.-or- The method is
    //     not declared or inherited by the class of obj. -or-A static constructor is
    //     invoked, and obj is neither null nor an instance of the class that declared
    //     the constructor.
    [DebuggerStepThrough]
      return default(abstract);
    }
    [DebuggerHidden]
    public object Invoke(object obj, object[] parameters) {
    //
    // Summary:
    //     When overridden in a derived class, invokes the reflected method or constructor
    //     with the given parameters.
    //
    // Parameters:
    //   culture:
    //     An instance of CultureInfo used to govern the coercion of types. If this
    //     is null, the CultureInfo for the current thread is used. (This is necessary
    //     to convert a String that represents 1000 to a Double value, for example,
    //     since 1000 is represented differently by different cultures.)
    //
    //   obj:
    //     The object on which to invoke the method or constructor. If a method is static,
    //     this argument is ignored. If a constructor is static, this argument must
    //     be null or an instance of the class that defines the constructor.
    //
    //   binder:
    //     An object that enables the binding, coercion of argument types, invocation
    //     of members, and retrieval of MemberInfo objects via reflection. If binder
    //     is null, the default binder is used.
    //
    //   invokeAttr:
    //     A bitmask that is a combination of 0 or more bit flags from System.Reflection.BindingFlags.
    //     If binder is null, this parameter is assigned the value System.Reflection.BindingFlags.Default;
    //     thus, whatever you pass in is ignored.
    //
    //   parameters:
    //     An argument list for the invoked method or constructor. This is an array
    //     of objects with the same number, order, and type as the parameters of the
    //     method or constructor to be invoked. If there are no parameters, this should
    //     be null.If the method or constructor represented by this instance takes a
    //     ByRef parameter, there is no special attribute required for that parameter
    //     in order to invoke the method or constructor using this function. Any object
    //     in this array that is not explicitly initialized with a value will contain
    //     the default value for that object type. For reference-type elements, this
    //     value is null. For value-type elements, this value is 0, 0.0, or false, depending
    //     on the specific element type.
    //
    // Returns:
    //     An Object containing the return value of the invoked method, or null in the
    //     case of a constructor, or null if the method's return type is void. Before
    //     calling the method or constructor, Invoke checks to see if the user has access
    //     permission and verify that the parameters are valid.
    //
    // Exceptions:
    //   System.MethodAccessException:
    //     The caller does not have permission to execute the constructor.
    //
    //   System.InvalidOperationException:
    //     The type that declares the method is an open generic type. That is, the System.Type.ContainsGenericParameters
    //     property returns true for the declaring type.
    //
    //   System.Reflection.TargetInvocationException:
    //     The invoked method or constructor throws an exception.
    //
    //   System.Reflection.TargetParameterCountException:
    //     The parameters array does not have the correct number of arguments.
    //
    //   System.ArgumentException:
    //     The type of the parameters parameter does not match the signature of the
    //     method or constructor reflected by this instance.
    //
    //   System.Reflection.TargetException:
    //     The obj parameter is null and the method is not static.-or- The method is
    //     not declared or inherited by the class of obj. -or-A static constructor is
    //     invoked, and obj is neither null nor an instance of the class that declared
    //     the constructor.
      return default(object);
    }
      return default(abstract);
    }
    public abstract object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture) {
  }
}
