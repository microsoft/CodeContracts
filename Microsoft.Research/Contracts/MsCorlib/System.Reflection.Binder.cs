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
using System.Globalization;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Reflection {
  // Summary:
  //     Selects a member from a list of candidates, and performs type conversion
  //     from actual argument type to formal argument type.
  // [Serializable]
  // [ClassInterface(ClassInterfaceType.AutoDual)]
  // [ComVisible(true)]
  public abstract class Binder {


    // Summary:
    //     Selects a field from the given set of fields, based on the specified criteria.
    //
    // Parameters:
    //   bindingAttr:
    //     A bitwise combination of System.Reflection.BindingFlags values.
    //
    //   match:
    //     The set of fields that are candidates for matching. For example, when a System.Reflection.Binder
    //     object is used by Overload:System.Type.InvokeMember, this is the set of fields
    //     Reflection has determined to be possible matches, typically because they
    //     have the correct member name. The default implementation provided by System.Type.DefaultBinder
    //     changes the order of this array.
    //
    //   value:
    //     The field value used to locate a matching field.
    //
    //   culture:
    //     An instance of System.Globalization.CultureInfo used to control the coercion
    //     of data types, in binder implementations that coerce types. If culture is
    //     null, the System.Globalization.CultureInfo for the current thread is used.For
    //     example, if a binder implementation allows coercion of string values to numeric
    //     types, this parameter is necessary to convert a String that represents 1000
    //     to a Double value, since 1000 is represented differently by different cultures.
    //     The default binder does not do such string coercions.
    //
    // Returns:
    //     A System.Reflection.FieldInfo object containing the matching field.
    //
    // Exceptions:
    //   System.Reflection.AmbiguousMatchException:
    //     For the default binder, bindingAttr includes System.Reflection.BindingFlags.SetField
    //     and match contains multiple fields that are equally good matches for value.
    //     For example, value contains a MyClass object that implements the IMyClass
    //     interface, and match contains a field of type MyClass and a field of type
    //     IMyClass.
    //
    //   System.MissingFieldException:
    //     For the default binder, bindingAttr includes System.Reflection.BindingFlags.SetField
    //     and match contains no fields that can accept value.
    //
    //   System.NullReferenceException:
    //     For the default binder, bindingAttr includes System.Reflection.BindingFlags.SetField
    //     and match is null or an empty array.-or-bindingAttr includes System.Reflection.BindingFlags.SetField
    //     and value is null.
    //public abstract FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture);
    //
    // Summary:
    //     Selects a method to invoke from the given set of methods, based on the supplied
    //     arguments.
    //
    // Parameters:
    //   bindingAttr:
    //     A bitwise combination of System.Reflection.BindingFlags values.
    //
    //   match:
    //     The set of methods that are candidates for matching. For example, when a
    //     System.Reflection.Binder object is used by Overload:System.Type.InvokeMember,
    //     this is the set of methods Reflection has determined to be possible matches,
    //     typically because they have the correct member name. The default implementation
    //     provided by System.Type.DefaultBinder changes the order of this array.
    //
    //   args:
    //     The arguments passed in. The binder can change to order of the arguments
    //     in this array; for example, the default binder changes the order of arguments
    //     if the names parameter is used to specify an order other than positional
    //     order. If a binder implementation coerces argument types, the types and values
    //     of the arguments can be changed, as well.
    //
    //   modifiers:
    //     An array of parameter modifiers that enable binding to work with parameter
    //     signatures in which the types have been modified. The default binder implementation
    //     does not use this.
    //
    //   culture:
    //     An instance of System.Globalization.CultureInfo used to control the coercion
    //     of data types, in binder implementations that coerce types. If culture is
    //     null, the System.Globalization.CultureInfo for the current thread is used.
    //     For example, if a binder implementation allows coercion of string values
    //     to numeric types, this parameter is necessary to convert a String that represents
    //     1000 to a Double value, since 1000 is represented differently by different
    //     cultures. The default binder does not do such string coercions.
    //
    //   names:
    //     The parameter names, if parameter names are to be considered when matching,
    //     or null if arguments are to be treated as purely positional. For example,
    //     parameter names must be used if arguments are not supplied in positional
    //     order.
    //
    //   state:
    //     After the method returns, state contains a binder-provided object that keeps
    //     track of argument reordering. The binder creates this object, and the binder
    //     is the sole consumer of this object. If state is not null when BindToMethod
    //     returns, you must pass state to the System.Reflection.Binder.ReorderArgumentArray(System.Object// []@,System.Object)
    //     method if you want to restore args to its original order, for example so
    //     that you can retrieve the values of ref parameters (ByRef parameters in Visual
    //     Basic).
    //
    // Returns:
    //     A System.Reflection.MethodBase object containing the matching method.
    //
    // Exceptions:
    //   System.Reflection.AmbiguousMatchException:
    //     For the default binder, match contains multiple methods that are equally
    //     good matches for args. For example, args contains a MyClass object that implements
    //     the IMyClass interface, and match contains a method that takes MyClass and
    //     a method that takes IMyClass.
    //
    //   System.MissingMethodException:
    //     For the default binder, match contains no methods that can accept the arguments
    //     supplied in args.
    //
    //   System.ArgumentException:
    //     For the default binder, match is null or an empty array.
    //public abstract MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object [] args, ParameterModifier [] modifiers, CultureInfo culture, string[] names, out object state);
    //
    // Summary:
    //     Changes the type of the given Object to the given Type.
    //
    // Parameters:
    //   value:
    //     The value to change into a new Type.
    //
    //   type:
    //     The new Type that value will become.
    //
    //   culture:
    //     An instance of System.Globalization.CultureInfo used to control the coercion
    //     of data types. If culture is null, the System.Globalization.CultureInfo for
    //     the current thread is used.For example, this parameter is necessary to convert
    //     a String that represents 1000 to a Double value, since 1000 is represented
    //     differently by different cultures.
    //
    // Returns:
    //     An Object containing the given value as the new type.
    //public abstract object ChangeType(object value, Type type, CultureInfo culture);
    //
    // Summary:
    //     Upon returning from System.Reflection.Binder.BindToMethod(System.Reflection.BindingFlags,System.Reflection.MethodBase// [],System.Object// []@,System.Reflection.ParameterModifier// [],System.Globalization.CultureInfo,System.String// [],System.Object@),
    //     restores the args argument to what it was when it came from BindToMethod.
    //
    // Parameters:
    //   args:
    //     The actual arguments passed in. Both the types and values of the arguments
    //     can be changed.
    //
    //   state:
    //     A binder-provided object that keeps track of argument reordering.
    //public abstract void ReorderArgumentArray(ref object [] args, object state);
    //
    // Summary:
    //     Selects a method from the given set of methods, based on the argument type.
    //
    // Parameters:
    //   bindingAttr:
    //     A bitwise combination of System.Reflection.BindingFlags values.
    //
    //   match:
    //     The set of methods that are candidates for matching. For example, when a
    //     System.Reflection.Binder object is used by Overload:System.Type.InvokeMember,
    //     this is the set of methods Reflection has determined to be possible matches,
    //     typically because they have the correct member name. The default implementation
    //     provided by System.Type.DefaultBinder changes the order of this array.
    //
    //   types:
    //     The parameter types used to locate a matching method.
    //
    //   modifiers:
    //     An array of parameter modifiers that enable binding to work with parameter
    //     signatures in which the types have been modified.
    //
    // Returns:
    //     A System.Reflection.MethodBase object containing the matching method, if
    //     found; otherwise, null.
    //
    // Exceptions:
    //   System.Reflection.AmbiguousMatchException:
    //     For the default binder, match contains multiple methods that are equally
    //     good matches for the parameter types described by types. For example, the
    //     array in types contains a System.Type object for MyClass and the array in
    //     match contains a method that takes a base class of MyClass and a method that
    //     takes an interface that MyClass implements.
    //
    //   System.ArgumentException:
    //     For the default binder, match is null or an empty array.-or-An element of
    //     types derives from System.Type, but is not of type RuntimeType.
    //public abstract MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase [] match, Type [] types, ParameterModifier [] modifiers);
    //
    // Summary:
    //     Selects a property from the given set of properties, based on the specified
    //     criteria.
    //
    // Parameters:
    //   bindingAttr:
    //     A bitwise combination of System.Reflection.BindingFlags values.
    //
    //   match:
    //     The set of properties that are candidates for matching. For example, when
    //     a System.Reflection.Binder object is used by Overload:System.Type.InvokeMember,
    //     this is the set of properties Reflection has determined to be possible matches,
    //     typically because they have the correct member name. The default implementation
    //     provided by System.Type.DefaultBinder changes the order of this array.
    //
    //   returnType:
    //     The return value the matching property must have.
    //
    //   indexes:
    //     The index types of the property being searched for. Used for index properties
    //     such as the indexer for a class.
    //
    //   modifiers:
    //     An array of parameter modifiers that enable binding to work with parameter
    //     signatures in which the types have been modified.
    //
    // Returns:
    //     A System.Reflection.PropertyInfo object containing the matching property.
    //
    // Exceptions:
    //   System.Reflection.AmbiguousMatchException:
    //     For the default binder, match contains multiple properties that are equally
    //     good matches for returnType and indexes.
    //
    //   System.ArgumentException:
    //     For the default binder, match is null or an empty array.
    //public abstract PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo [] match, Type returnType, 
      //Type [] indexes, ParameterModifier [] modifiers);
  }
}