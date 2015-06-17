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

#if NETFRAMEWORK_4_0

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime;
using System.Diagnostics.Contracts;

namespace System.Dynamic
{
  // Summary:
  //     Provides a base class for specifying dynamic behavior at run time. This class
  //     must be inherited from; you cannot instantiate it directly.
  public class DynamicObject : IDynamicMetaObjectProvider
  {
    // Summary:
    //     Enables derived types to initialize a new instance of the System.Dynamic.DynamicObject
    //     type.
    protected DynamicObject() { }

    // Summary:
    //     Returns the enumeration of all dynamic member names.
    //
    // Returns:
    //     A sequence that contains dynamic member names.
    public virtual IEnumerable<string> GetDynamicMemberNames()
    {
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);

      return default(IEnumerable<string>);
    }
    //
    // Summary:
    //     Provides a System.Dynamic.DynamicMetaObject that dispatches to the dynamic
    //     virtual methods. The object can be encapsulated inside another System.Dynamic.DynamicMetaObject
    //     to provide custom behavior for individual actions. This method supports the
    //     Dynamic Language Runtime infrastructure for language implementers and it
    //     is not intended to be used directly from your code.
    //
    // Parameters:
    //   parameter:
    //     The expression that represents System.Dynamic.DynamicMetaObject to dispatch
    //     to the dynamic virtual methods.
    //
    // Returns:
    //     An object of the System.Dynamic.DynamicMetaObject type.
    public virtual DynamicMetaObject GetMetaObject(Expression parameter)
    {
      Contract.Requires(parameter != null);
      Contract.Ensures(Contract.Result<DynamicMetaObject>() != null);

      return null;
    }
    //
    // Summary:
    //     Provides implementation for binary operations. Classes derived from the System.Dynamic.DynamicObject
    //     class can override this method to specify dynamic behavior for operations
    //     such as addition and multiplication.
    //
    // Parameters:
    //   binder:
    //     Provides information about the binary operation. The binder.Operation property
    //     returns an System.Linq.Expressions.ExpressionType object. For example, for
    //     the sum = first + second statement, where first and second are derived from
    //     the DynamicObject class, binder.Operation returns ExpressionType.Add.
    //
    //   arg:
    //     The right operand for the binary operation. For example, for the sum = first
    //     + second statement, where first and second are derived from the DynamicObject
    //     class, arg is equal to second.
    //
    //   result:
    //     The result of the binary operation.
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a language-specific run-time exception is thrown.)
    public virtual bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
    {
      Contract.Requires(binder != null);
      
      result = null;
      return false;
    }
    //
    // Summary:
    //     Provides implementation for type conversion operations. Classes derived from
    //     the System.Dynamic.DynamicObject class can override this method to specify
    //     dynamic behavior for operations that convert an object from one type to another.
    //
    // Parameters:
    //   binder:
    //     Provides information about the conversion operation. The binder.Type property
    //     provides the type to which the object must be converted. For example, for
    //     the statement (String)sampleObject in C# (CType(sampleObject, Type) in Visual
    //     Basic), where sampleObject is an instance of the class derived from the System.Dynamic.DynamicObject
    //     class, binder.Type returns the System.String type. The binder.Explicit property
    //     provides information about the kind of conversion that occurs. It returns
    //     true for explicit conversion and false for implicit conversion.
    //
    //   result:
    //     The result of the type conversion operation.
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a language-specific run-time exception is thrown.)
    //public virtual bool TryConvert(ConvertBinder binder, out object result);
    //
    // Summary:
    //     Provides the implementation for operations that initialize a new instance
    //     of a dynamic object. This method is not intended for use in C# or Visual
    //     Basic.
    //
    // Parameters:
    //   binder:
    //     Provides information about the initialization operation.
    //
    //   args:
    //     The arguments that are passed to the object during initialization. For example,
    //     for the new SampleType(100) operation, where SampleType is the type derived
    //     from the System.Dynamic.DynamicObject class, args[0] is equal to 100.
    //
    //   result:
    //     The result of the initialization.
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a language-specific run-time exception is thrown.)
    //public virtual bool TryCreateInstance(CreateInstanceBinder binder, object[] args, out object result);
    //
    // Summary:
    //     Provides the implementation for operations that delete an object by index.
    //     This method is not intended for use in C# or Visual Basic.
    //
    // Parameters:
    //   binder:
    //     Provides information about the deletion.
    //
    //   indexes:
    //     The indexes to be deleted.
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a language-specific run-time exception is thrown.)
    //public virtual bool TryDeleteIndex(DeleteIndexBinder binder, object[] indexes);
    //
    // Summary:
    //     Provides the implementation for operations that delete an object member.
    //     This method is not intended for use in C# or Visual Basic.
    //
    // Parameters:
    //   binder:
    //     Provides information about the deletion.
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a language-specific run-time exception is thrown.)
    //public virtual bool TryDeleteMember(DeleteMemberBinder binder);
    //
    // Summary:
    //     Provides the implementation for operations that get a value by index. Classes
    //     derived from the System.Dynamic.DynamicObject class can override this method
    //     to specify dynamic behavior for indexing operations.
    //
    // Parameters:
    //   binder:
    //     Provides information about the operation.
    //
    //   indexes:
    //     The indexes that are used in the operation. For example, for the sampleObject[3]
    //     operation in C# (sampleObject(3) in Visual Basic), where sampleObject is
    //     derived from the DynamicObject class, indexes[0] is equal to 3.
    //
    //   result:
    //     The result of the index operation.
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a run-time exception is thrown.)
    //public virtual bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result);
    //
    // Summary:
    //     Provides the implementation for operations that get member values. Classes
    //     derived from the System.Dynamic.DynamicObject class can override this method
    //     to specify dynamic behavior for operations such as getting a value for a
    //     property.
    //
    // Parameters:
    //   binder:
    //     Provides information about the object that called the dynamic operation.
    //     The binder.Name property provides the name of the member on which the dynamic
    //     operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty)
    //     statement, where sampleObject is an instance of the class derived from the
    //     System.Dynamic.DynamicObject class, binder.Name returns "SampleProperty".
    //     The binder.IgnoreCase property specifies whether the member name is case-sensitive.
    //
    //   result:
    //     The result of the get operation. For example, if the method is called for
    //     a property, you can assign the property value to result.
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a run-time exception is thrown.)
    //public virtual bool TryGetMember(GetMemberBinder binder, out object result);
    //
    // Summary:
    //     Provides the implementation for operations that invoke an object. Classes
    //     derived from the System.Dynamic.DynamicObject class can override this method
    //     to specify dynamic behavior for operations such as invoking an object or
    //     a delegate.
    //
    // Parameters:
    //   binder:
    //     Provides information about the invoke operation.
    //
    //   args:
    //     The arguments that are passed to the object during the invoke operation.
    //     For example, for the sampleObject(100) operation, where sampleObject is derived
    //     from the System.Dynamic.DynamicObject class, args[0] is equal to 100.
    //
    //   result:
    //     The result of the object invocation.
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a language-specific run-time exception is thrown.
    //public virtual bool TryInvoke(InvokeBinder binder, object[] args, out object result);
    //
    // Summary:
    //     Provides the implementation for operations that invoke a member. Classes
    //     derived from the System.Dynamic.DynamicObject class can override this method
    //     to specify dynamic behavior for operations such as calling a method.
    //
    // Parameters:
    //   binder:
    //     Provides information about the dynamic operation. The binder.Name property
    //     provides the name of the member on which the dynamic operation is performed.
    //     For example, for the statement sampleObject.SampleMethod(100), where sampleObject
    //     is an instance of the class derived from the System.Dynamic.DynamicObject
    //     class, binder.Name returns "SampleMethod". The binder.IgnoreCase property
    //     specifies whether the member name is case-sensitive.
    //
    //   args:
    //     The arguments that are passed to the object member during the invoke operation.
    //     For example, for the statement sampleObject.SampleMethod(100), where sampleObject
    //     is derived from the System.Dynamic.DynamicObject class, args[0] is equal
    //     to 100.
    //
    //   result:
    //     The result of the member invocation.
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a language-specific run-time exception is thrown.)
    //public virtual bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result);
    //
    // Summary:
    //     Provides the implementation for operations that set a value by index. Classes
    //     derived from the System.Dynamic.DynamicObject class can override this method
    //     to specify dynamic behavior for operations that access objects by a specified
    //     index.
    //
    // Parameters:
    //   binder:
    //     Provides information about the operation.
    //
    //   indexes:
    //     The indexes that are used in the operation. For example, for the sampleObject[3]
    //     = 10 operation in C# (sampleObject(3) = 10 in Visual Basic), where sampleObject
    //     is derived from the System.Dynamic.DynamicObject class, indexes[0] is equal
    //     to 3.
    //
    //   value:
    //     The value to set to the object that has the specified index. For example,
    //     for the sampleObject[3] = 10 operation in C# (sampleObject(3) = 10 in Visual
    //     Basic), where sampleObject is derived from the System.Dynamic.DynamicObject
    //     class, value is equal to 10.
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a language-specific run-time exception is thrown.
    //public virtual bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value);
    //
    // Summary:
    //     Provides the implementation for operations that set member values. Classes
    //     derived from the System.Dynamic.DynamicObject class can override this method
    //     to specify dynamic behavior for operations such as setting a value for a
    //     property.
    //
    // Parameters:
    //   binder:
    //     Provides information about the object that called the dynamic operation.
    //     The binder.Name property provides the name of the member to which the value
    //     is being assigned. For example, for the statement sampleObject.SampleProperty
    //     = "Test", where sampleObject is an instance of the class derived from the
    //     System.Dynamic.DynamicObject class, binder.Name returns "SampleProperty".
    //     The binder.IgnoreCase property specifies whether the member name is case-sensitive.
    //
    //   value:
    //     The value to set to the member. For example, for sampleObject.SampleProperty
    //     = "Test", where sampleObject is an instance of the class derived from the
    //     System.Dynamic.DynamicObject class, the value is "Test".
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a language-specific run-time exception is thrown.)
    //public virtual bool TrySetMember(SetMemberBinder binder, object value);
    //
    // Summary:
    //     Provides implementation for unary operations. Classes derived from the System.Dynamic.DynamicObject
    //     class can override this method to specify dynamic behavior for operations
    //     such as negation, increment, or decrement.
    //
    // Parameters:
    //   binder:
    //     Provides information about the unary operation. The binder.Operation property
    //     returns an System.Linq.Expressions.ExpressionType object. For example, for
    //     the negativeNumber = -number statement, where number is derived from the
    //     DynamicObject class, binder.Operation returns "Negate".
    //
    //   result:
    //     The result of the unary operation.
    //
    // Returns:
    //     true if the operation is successful; otherwise, false. If this method returns
    //     false, the run-time binder of the language determines the behavior. (In most
    //     cases, a language-specific run-time exception is thrown.)
    //public virtual bool TryUnaryOperation(UnaryOperationBinder binder, out object result);
  }
}
#endif