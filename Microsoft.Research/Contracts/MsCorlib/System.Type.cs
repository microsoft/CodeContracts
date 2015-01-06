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
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System
{

  [Immutable]
  public class Type
  {
#if SILVERLIGHT_3_0 || SILVERLIGHT_4_0 || SILVERLIGHT_4_0_WP
    internal
#else
    protected
#endif
    Type() { }

    extern public virtual string Namespace
    {
      get;
    }

#if !SILVERLIGHT
    extern public virtual bool IsMarshalByRef
    {
      get;
    }
#endif

#if false
    extern public  ConstructorInfo TypeInitializer
    {
      get;
    }
#endif

#if NETFRAMEWORK_4_0 || SILVERLIGHT_5_0
    [Pure]
#if NETFRAMEWORK_4_0
    public
#else
    internal
#endif
    virtual bool IsEquivalentTo(Type other) {
      return default(bool);
    }
#endif

#if !SILVERLIGHT
    extern public virtual bool IsExplicitLayout
    {
      get;
    }
#endif

    public virtual bool IsGenericType
    {
      get {
        Contract.Ensures(!Contract.Result<bool>() || this.GetGenericArguments().Length > 0);
        return default(bool);
      }
    }

    public virtual bool IsGenericTypeDefinition
    {
      get
      {
        Contract.Ensures(!Contract.Result<bool>() || this.GetGenericArguments().Length > 0);
        return default(bool);
      }
    }

    extern public virtual bool IsGenericParameter {
      get;
    }

    extern public virtual bool IsValueType
    {
      get;
    }

    extern public virtual bool IsAutoClass
    {
      get;
    }

    extern public virtual bool IsNestedPrivate
    {
      get;
    }

#if !SILVERLIGHT
    extern public virtual bool IsSerializable
    {
      get;
    }
#endif

    public virtual Assembly Assembly
    {
      get
      {
        Contract.Ensures(Contract.Result< Assembly>() != null);
        return default( Assembly);
      }
    }

    extern public virtual bool IsNestedAssembly
    {
      get;
    }

    extern public virtual bool IsNotPublic
    {
      get;
    }

    extern public virtual bool IsSealed
    {
      get;
    }

#if false
    extern public Guid GUID
    {
      get;
    }
#endif

#if !SILVERLIGHT
    extern public virtual bool IsLayoutSequential
    {
      get;
    }
#endif

    extern public virtual bool IsNestedFamily
    {
      get;
    }

    extern public virtual bool IsNestedFamORAssem
    {
      get;
    }

    public virtual string FullName
    {
      get {
        Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));
        return default(string);
      }
    }

    //
    // Summary:
    //     Returns a System.Type object representing a one-dimensional array of the
    //     current type, with a lower bound of zero.
    //
    // Returns:
    //     A System.Type object representing a one-dimensional array of the current
    //     type, with a lower bound of zero.
    [Pure]
    public virtual Type MakeArrayType()
    {
      Contract.Ensures(Contract.Result<Type>() != null);

      return default(Type);
    }

    //
    // Summary:
    //     Returns a System.Type object representing an array of the current type, with
    //     the specified number of dimensions.
    //
    // Parameters:
    //   rank:
    //     The number of dimensions for the array.
    //
    // Returns:
    //     A System.Type object representing an array of the current type, with the
    //     specified number of dimensions.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     rank is invalid. For example, 0 or negative.
    //
    //   System.NotSupportedException:
    //     The invoked method is not supported in the base class.
    [Pure]
    public virtual Type MakeArrayType(int rank)
    {
      Contract.Requires(0 < rank);
      Contract.Ensures(Contract.Result<Type>() != null);

      return default(Type);
    }

    //
    // Summary:
    //     Returns a System.Type object that represents the current type when passed
    //     as a ref parameter (ByRef parameter in Visual Basic).
    //
    // Returns:
    //     A System.Type object that represents the current type when passed as a ref
    //     parameter (ByRef parameter in Visual Basic).
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The invoked method is not supported in the base class.
    [Pure]
    public virtual Type MakeByRefType()
    {
      Contract.Ensures(Contract.Result<Type>() != null);

      return default(Type);
    }

    //
    // Summary:
    //     Substitutes the elements of an array of types for the type parameters of
    //     the current generic type definition and returns a System.Type object representing
    //     the resulting constructed type.
    //
    // Parameters:
    //   typeArguments:
    //     An array of types to be substituted for the type parameters of the current
    //     generic type.
    //
    // Returns:
    //     A System.Type representing the constructed type formed by substituting the
    //     elements of typeArguments for the type parameters of the current generic
    //     type.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The current type does not represent a generic type definition. That is, System.Type.IsGenericTypeDefinition
    //     returns false.
    //
    //   System.ArgumentNullException:
    //     typeArguments is null.  -or- Any element of typeArguments is null.
    //
    //   System.ArgumentException:
    //     The number of elements in typeArguments is not the same as the number of
    //     type parameters in the current generic type definition.  -or- Any element
    //     of typeArguments does not satisfy the constraints specified for the corresponding
    //     type parameter of the current generic type.
    //
    //   System.NotSupportedException:
    //     The invoked method is not supported in the base class. Derived classes must
    //     provide an implementation.
    [Pure]
    public virtual Type MakeGenericType(params Type[] typeArguments)
    {
      Contract.Requires(this.IsGenericTypeDefinition);
      Contract.Requires(typeArguments.Length == this.GetGenericArguments().Length);
      Contract.Ensures(Contract.Result<Type>() != null);

      return default(Type);
    }

    //
    // Summary:
    //     Returns a System.Type object that represents a pointer to the current type.
    //
    // Returns:
    //     A System.Type object that represents a pointer to the current type.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The invoked method is not supported in the base class.
    [Pure]
    public virtual Type MakePointerType()
    {
      Contract.Ensures(Contract.Result<Type>() != null);

      return default(Type);
    }


#if false
    extern public  MemberTypes MemberType
    {
      get;
    }
#endif
    extern public virtual string AssemblyQualifiedName
    {
      get;
    }

    extern public virtual Type BaseType
    {
      get;
    }

    extern public virtual RuntimeTypeHandle TypeHandle
    {
      get;
    }

    extern public virtual bool IsInterface
    {
      get;
    }

    extern public virtual bool IsAnsiClass
    {
      get;
    }

    extern public virtual bool IsAutoLayout
    {
      get;
    }

    extern public virtual bool IsPointer
    {
      get;
    }

    extern public virtual bool IsEnum
    {
      get;
    }

    // public override Type ReflectedType

#if false
    extern public  TypeAttributes Attributes
    {
      get;
    }
#endif
    extern public virtual Type DeclaringType
    {
      get;
    }

    extern public virtual bool IsNestedFamANDAssem
    {
      get;
    }

#if !SILVERLIGHT
    extern public virtual bool IsContextful
    {
      get;
    }
#endif

    extern public virtual bool IsClass
    {
      get;
    }

    extern public virtual bool IsPublic
    {
      get;
    }

    extern public virtual bool IsAbstract
    {
      get;
    }

    //
    // Summary:
    //     Indicates the type provided by the common language runtime that represents
    //     this type.
    //
    // Returns:
    //     The underlying system type for the System.Type.
    public virtual Type UnderlyingSystemType
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);

        return default(Type);
      }
    }

    extern public virtual bool IsPrimitive
    {
      get;
    }

    public virtual Module Module
    {
      get
      {
        Contract.Ensures(Contract.Result< Module>() != null);

        return default( Module);
      }
    }

    extern public virtual bool IsImport
    {
      get;
    }

    extern public virtual bool IsArray
    {
      get;
    }

    extern public virtual bool IsNestedPublic
    {
      get;
    }

    extern public virtual bool IsByRef
    {
      get;
    }

    extern public virtual bool IsSpecialName
    {
      get;
    }

    extern public virtual bool IsUnicodeClass
    {
      get;
    }
#if false
    extern public static  Binder DefaultBinder
    {
      get;
    }
#endif
    extern public virtual bool HasElementType
    {
      get;
    }

    extern public virtual bool IsCOMObject
    {
      get;
    }
#if false
    public  InterfaceMapping GetInterfaceMap(Type interfaceType)
    {

      return default( InterfaceMapping);
    }
#endif
    [Pure]
    public virtual bool Equals(Type o)
    {

      return default(bool);
    }
#if !SILVERLIGHT
    [Pure]
    public static Type[] GetTypeArray(Object[] args)
    {
      Contract.Requires(args != null);
      Contract.Ensures(Contract.Result<Type[]>() != null);

      return default(Type[]);
    }
#endif
    [Pure]
    public virtual bool IsAssignableFrom(Type c)
    {
      return default(bool);
    }

    [Pure]
    public virtual bool IsInstanceOfType(object o)
    {
      return default(bool);
    }
    [Pure]
    public virtual bool IsSubclassOf(Type c)
    {
      return default(bool);
    }
    [Pure]
    public virtual Type GetElementType()
    {
      return default(Type);
    }

    //public  MemberInfo[] FindMembers( MemberTypes memberType,  BindingFlags bindingAttr,  MemberFilter filter, object filterCriteria)
    //{

    //  return default( MemberInfo[]);
    //}

    //
    // Summary:
    //     Searches for the members defined for the current System.Type whose  DefaultMemberAttribute
    //     is set.
    //
    // Returns:
    //     An array of  MemberInfo objects representing all default
    //     members of the current System.Type.-or- An empty array of type  MemberInfo,
    //     if the current System.Type does not have default members.
    [Pure]
    public virtual MemberInfo[] GetDefaultMembers()
    {
      Contract.Ensures(Contract.Result< MemberInfo[]>() != null);
      return default( MemberInfo[]);
    }
    [Pure]
    public virtual MemberInfo[] GetMembers(BindingFlags arg0)
    {
      Contract.Ensures(Contract.Result<MemberInfo[]>() != null);
      return default( MemberInfo[]);
    }

    //
    // Summary:
    //     Returns all the public members of the current System.Type.
    //
    // Returns:
    //     An array of System.Reflection.MemberInfo objects representing all the public
    //     members of the current System.Type.-or- An empty array of type System.Reflection.MemberInfo,
    //     if the current System.Type does not have public members.
    [Pure]
    public virtual MemberInfo[] GetMembers()
    {
      Contract.Ensures(Contract.Result<MemberInfo[]>() != null);
      return default( MemberInfo[]);
    }

    [Pure]
    public virtual MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      Contract.Ensures(Contract.Result<MemberInfo[]>() != null);
      return default( MemberInfo[]);
    }

    [Pure]
    public virtual MemberInfo[] GetMember(string name,  BindingFlags bindingAttr)
    {
      Contract.Ensures(Contract.Result<MemberInfo[]>() != null);
      return default( MemberInfo[]);
    }

    [Pure]
    public virtual MemberInfo[] GetMember(string name)
    {
      Contract.Ensures(Contract.Result<MemberInfo[]>() != null);
      return default(MemberInfo[]);
    }

    [Pure]
    public virtual Type GetNestedType(string arg0,  BindingFlags arg1)
    {
      return default(Type);
    }

#if !SILVERLIGHT
    [Pure]
    public virtual Type GetNestedType(string name)
    {
      return default(Type);
    }
#endif
    [Pure]
    public virtual Type[] GetNestedTypes( BindingFlags arg0)
    {
      Contract.Ensures(Contract.Result<Type[]>() != null);
      return default(Type[]);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Returns the public types nested in the current System.Type.
    //
    // Returns:
    //     An array of System.Type objects representing the public types nested in the
    //     current System.Type (the search is not recursive), or an empty array of type
    //     System.Type if no public types are nested in the current System.Type.
    [Pure]
    public virtual Type[] GetNestedTypes()
    {
      Contract.Ensures(Contract.Result<Type[]>() != null);
      return default(Type[]);
    }
#endif

    //
    // Summary:
    //     Returns all the public properties of the current System.Type.
    //
    // Returns:
    //     An array of System.Reflection.PropertyInfo objects representing all public
    //     properties of the current System.Type.-or- An empty array of type System.Reflection.PropertyInfo,
    //     if the current System.Type does not have public properties.
    [Pure]
    public virtual PropertyInfo[] GetProperties()
    {
      Contract.Ensures(Contract.Result<PropertyInfo[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<PropertyInfo[]>(), el => el != null));

      return default( PropertyInfo[]);
    }

    //
    // Summary:
    //     When overridden in a derived class, searches for the properties of the current
    //     System.Type, using the specified binding constraints.
    //
    // Parameters:
    //   bindingAttr:
    //     A bitmask comprised of one or more System.Reflection.BindingFlags that specify
    //     how the search is conducted.-or- Zero, to return null.
    //
    // Returns:
    //     An array of System.Reflection.PropertyInfo objects representing all properties
    //     of the current System.Type that match the specified binding constraints.-or-
    //     An empty array of type System.Reflection.PropertyInfo, if the current System.Type
    //     does not have properties, or if none of the properties match the binding
    //     constraints.
    [Pure]
    public virtual PropertyInfo[] GetProperties(BindingFlags arg0)
    {
      Contract.Ensures(Contract.Result<PropertyInfo[]>() != null);
      return default(PropertyInfo[]);
    }

    [Pure]
    public virtual PropertyInfo GetProperty(string name)
    {
      Contract.Requires(name != null);
      return default( PropertyInfo);
    }
    
    [Pure]
    public virtual PropertyInfo GetProperty(string name, Type returnType)
    {
      Contract.Requires(name != null);
      Contract.Requires(returnType != null);

      return default( PropertyInfo);
    }

#if !SILVERLIGHT
    [Pure]
    public virtual PropertyInfo GetProperty(string name, Type[] types)
    {
      Contract.Requires(name != null);
      Contract.Requires(types != null);

      return default( PropertyInfo);
    }
#endif

    [Pure]
    public virtual PropertyInfo GetProperty(string name, Type returnType, Type[] types)
    {
      Contract.Requires(name != null);
      Contract.Requires(types != null);

      return default( PropertyInfo);
    }

    [Pure]
    public virtual PropertyInfo GetProperty(string name,  BindingFlags bindingAttr)
    {
      Contract.Requires(name != null);

      return default( PropertyInfo);
    }

    //public  PropertyInfo GetProperty(string name, Type returnType, Type[] types,  ParameterModifier[] modifiers)
    //{
    //  Contract.Requires(name != null);
    //  Contract.Requires(types != null);

    //  return default( PropertyInfo);
    //}


    //public  PropertyInfo GetProperty(string name,  BindingFlags bindingAttr,  Binder binder, Type returnType, Type[] types,  ParameterModifier[] modifiers)
    //{
    //  Contract.Requires(name != null);
    //  Contract.Requires(types != null);

    //  return default( PropertyInfo);
    //}

    [Pure]
    public virtual EventInfo[] GetEvents(BindingFlags arg0)
    {
      Contract.Ensures(Contract.Result<EventInfo[]>() != null);
      return default( EventInfo[]);
    }

    //
    // Summary:
    //     Returns all the public events that are declared or inherited by the current
    //     System.Type.
    //
    // Returns:
    //     An array of  EventInfo objects representing all the public
    //     events which are declared or inherited by the current System.Type.-or- An
    //     empty array of type  EventInfo, if the current System.Type
    [Pure]
    public virtual EventInfo[] GetEvents()
    {
      Contract.Ensures(Contract.Result< EventInfo[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<EventInfo[]>(), el => el != null));

      return default( EventInfo[]);
    }

    [Pure]
    public virtual EventInfo GetEvent(string arg0, BindingFlags arg1)
    {
      return default( EventInfo);
    }

    [Pure]
    public virtual EventInfo GetEvent(string name)
    {
      return default( EventInfo);
    }

#if !SILVERLIGHT
    [Pure]
    public virtual Type[] FindInterfaces( TypeFilter filter, object filterCriteria)
    {
      Contract.Requires(filter != null);
      Contract.Ensures(Contract.Result<Type[]>() != null);

      return default(Type[]);
    }
#endif

    //
    // Summary:
    //     When overridden in a derived class, gets all the interfaces implemented or
    //     inherited by the current System.Type.
    //
    // Returns:
    //     An array of System.Type objects representing all the interfaces implemented
    //     or inherited by the current System.Type.-or- An empty array of type System.Type,
    //     if no interfaces are implemented or inherited by the current System.Type.
    //
    // Exceptions:
    //   System.Reflection.TargetInvocationException:
    //     A static initializer is invoked and throws an exception.
    [Pure]
    public virtual Type[] GetInterfaces()
    {
      Contract.Ensures(Contract.Result<Type[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<Type[]>(), el => el != null));

      return default(Type[]);
    }

    [Pure]
    public virtual Type GetInterface(string arg0, bool arg1)
    {
      return default(Type);
    }

#if !SILVERLIGHT
    [Pure]
    public virtual Type GetInterface(string name)
    {
      return default(Type);
    }
#endif

    //
    // Summary:
    //     Returns all the public fields of the current System.Type.
    //
    // Returns:
    //     An array of System.Reflection.FieldInfo objects representing all the public
    //     fields defined for the current System.Type.-or- An empty array of type System.Reflection.FieldInfo,
    //     if no public fields are defined for the current System.Type.
    [Pure]
    public virtual FieldInfo[] GetFields( BindingFlags arg0)
    {
      Contract.Ensures(Contract.Result<FieldInfo[]>() != null);

      return default( FieldInfo[]);
    }

    //
    // Summary:
    //     Returns all the public fields of the current System.Type.
    //
    // Returns:
    //     An array of System.Reflection.FieldInfo objects representing all the public
    //     fields defined for the current System.Type.-or- An empty array of type System.Reflection.FieldInfo,
    //     if no public fields are defined for the current System.Type.
    [Pure]
    public virtual FieldInfo[] GetFields()
    {
      Contract.Ensures(Contract.Result<FieldInfo[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<FieldInfo[]>(), el => el != null));

      return default( FieldInfo[]);
    }

    [Pure]
    public virtual FieldInfo GetField(string name)
    {
      return default( FieldInfo);
    }

    [Pure]
    public virtual FieldInfo GetField(string arg0, BindingFlags arg1)
    {
      return default( FieldInfo);
    }

    //
    // Summary:
    //     Returns an array of System.Type objects that represent the type arguments
    //     of a generic type or the type parameters of a generic type definition.
    //
    // Returns:
    //     An array of System.Type objects that represent the type arguments of a generic
    //     type. Returns an empty array if the current type is not a generic type.
    [Pure]
    public virtual Type[] GetGenericArguments()
    {
      // Removed as of Brian request 
      //  Contract.Ensures(Contract.Result<Type[]>() != null);
      // weaker form that should be okay
      Contract.Ensures(!this.IsGenericTypeDefinition || Contract.Result<Type[]>() != null);

      return default(Type[]);
    }

    [Pure]
    public virtual Type GetGenericTypeDefinition()
    {
      Contract.Requires(this.IsGenericType);
      Contract.Ensures(Contract.Result<Type>().IsGenericTypeDefinition);
      Contract.Ensures(Contract.Result<Type>().GetGenericArguments().Length == this.GetGenericArguments().Length);

      return default(Type);
    }
    [Pure]
    public virtual Type[] GetGenericParameterConstraints() {
      Contract.Requires(this.IsGenericParameter);
      Contract.Ensures(Contract.Result<Type[]>() != null);
      
      return default(Type[]);
    }

    [Pure]
    public virtual MethodInfo[] GetMethods(BindingFlags arg0)
    {
      Contract.Ensures(Contract.Result<MethodInfo[]>() != null);

      return default(MethodInfo[]);
    }

    //
    // Summary:
    //     Returns all the public methods of the current System.Type.
    //
    // Returns:
    //     An array of System.Reflection.MethodInfo objects representing all the public
    //     methods defined for the current System.Type.-or- An empty array of type System.Reflection.MethodInfo,
    //     if no public methods are defined for the current System.Type.
    [Pure]
    public virtual MethodInfo[] GetMethods()
    {
      Contract.Ensures(Contract.Result<MethodInfo[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<MethodInfo[]>(), el => el != null));

      return default( MethodInfo[]);
    }

    [Pure]
    public virtual MethodInfo GetMethod(string name)
    {
      Contract.Requires(name != null);

      return default( MethodInfo);
    }

    [Pure]
    public virtual MethodInfo GetMethod(string name, BindingFlags bindingAttr)
    {
      Contract.Requires(name != null);

      return default( MethodInfo);
    }

    [Pure]
    public virtual MethodInfo GetMethod(string name, Type[] types)
    {
      Contract.Requires(name != null);
      Contract.Requires(types != null);

      return default( MethodInfo);
    }
    //public  MethodInfo GetMethod(string name, Type[] types,  ParameterModifier[] modifiers)
    //{
    //  Contract.Requires(name != null);
    //  Contract.Requires(types != null);

    //  return default( MethodInfo);
    //}
    //public  MethodInfo GetMethod(string name,  BindingFlags bindingAttr,  Binder binder, Type[] types,  ParameterModifier[] modifiers)
    //{
    //  Contract.Requires(name != null);
    //  Contract.Requires(types != null);

    //  return default( MethodInfo);
    //}
    //public  MethodInfo GetMethod(string name,  BindingFlags bindingAttr,  Binder binder,  CallingConventions callConvention, Type[] types,  ParameterModifier[] modifiers)
    //{
    //  Contract.Requires(name != null);
    //  Contract.Requires(types != null);

    //  return default( MethodInfo);
    //}

    [Pure]
    public virtual ConstructorInfo[] GetConstructors(BindingFlags arg0)
    {
      Contract.Ensures(Contract.Result< ConstructorInfo[]>() != null);

      return default( ConstructorInfo[]);
    }

    [Pure]
    public virtual ConstructorInfo[] GetConstructors()
    {
      Contract.Ensures(Contract.Result< ConstructorInfo[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<ConstructorInfo[]>(), el => el != null));

      return default( ConstructorInfo[]);
    }

    [Pure]
    public virtual ConstructorInfo GetConstructor(Type[] types)
    {

      return default( ConstructorInfo);
    }

#if false
    [Pure]
    public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
    {
      Contract.Requires(types != null);

      return default( ConstructorInfo);
    }
    [Pure]
    public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      Contract.Requires(types != null);

      return default(ConstructorInfo);
    }
#endif

    [Pure]
    public virtual int GetArrayRank()
    {
      return default(int);
    }

    [Pure]
    public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
    {
      Contract.Ensures(Contract.Result<Type>() != null);
      return default(Type);
    }

#if !SILVERLIGHT
    [Pure]
    public static RuntimeTypeHandle GetTypeHandle(object o)
    {
      Contract.Requires(o != null);

      return default(RuntimeTypeHandle);
    }
#endif

#if false
    public object InvokeMember(string name,  BindingFlags invokeAttr,  Binder binder, object target, Object[] args)
    {
      return default(object);
    }

    public object InvokeMember(string name,  BindingFlags invokeAttr,  Binder binder, object target, Object[] args, System.Globalization.CultureInfo culture)
    {

      return default(object);
    }
    public object InvokeMember(string arg0,  BindingFlags arg1,  Binder arg2, object arg3, Object[] arg4,  ParameterModifier[] arg5, System.Globalization.CultureInfo arg6, String[] arg7)
    {

      return default(object);
    }
#endif
    [Pure]
    public static TypeCode GetTypeCode(Type type)
    {
      return default(TypeCode);
    }
#if false
    public static Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
    {
      return default(Type);
    }

    public static Type GetTypeFromCLSID(Guid clsid, string server)
    {
      return default(Type);
    }
    public static Type GetTypeFromCLSID(Guid clsid, bool throwOnError)
    {
      return default(Type);
    }

    public static Type GetTypeFromCLSID(Guid clsid)
    {
      return default(Type);
    }
#endif

#if !SILVERLIGHT
    [Pure]
    public static Type GetTypeFromProgID(string progID, string server, bool throwOnError)
    {
      Contract.Ensures(!throwOnError || Contract.Result<Type>() != null);
      return default(Type);
    }
    [Pure]
    public static Type GetTypeFromProgID(string progID, string server)
    {
      return default(Type);
    }
    [Pure]
    public static Type GetTypeFromProgID(string progID, bool throwOnError)
    {
      Contract.Ensures(!throwOnError || Contract.Result<Type>() != null);
      return default(Type);
    }
    [Pure]
    public static Type GetTypeFromProgID(string progID)
    {
      return default(Type);
    }
#endif

    //
    // Summary:
    //     Object.GetType is not virtual and some types new it.
    //
    [Pure]
    new public virtual Type GetType()
    {
      Contract.Ensures(Contract.Result<Type>() != null);

      return default(Type);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static Type GetType(string typeName)
    {
      return default(Type);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static Type GetType(string typeName, bool throwOnError)
    {
      Contract.Ensures(!throwOnError || Contract.Result<Type>() != null);

      return default(Type);
    }

    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
    {
      Contract.Ensures(!throwOnError || Contract.Result<Type>() != null);
      return default(Type);
    }
  }
}
