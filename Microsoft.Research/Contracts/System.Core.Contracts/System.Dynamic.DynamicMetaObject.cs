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
using System.Diagnostics.Contracts;

namespace System.Dynamic
{
  // Summary:
  //     Represents the dynamic binding and a binding logic of an object participating
  //     in the dynamic binding.
  public class DynamicMetaObject
  {
    // Summary:
    //     Represents an empty array of type System.Dynamic.DynamicMetaObject. This
    //     field is read only.
    public static readonly DynamicMetaObject[] EmptyMetaObjects;

    // Summary:
    //     Initializes a new instance of the System.Dynamic.DynamicMetaObject class.
    //
    // Parameters:
    //   expression:
    //     The expression representing this System.Dynamic.DynamicMetaObject during
    //     the dynamic binding process.
    //
    //   restrictions:
    //     The set of binding restrictions under which the binding is valid.
    public DynamicMetaObject(Expression expression, BindingRestrictions restrictions)
    {
      Contract.Requires(expression != null);
      Contract.Requires(restrictions != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Dynamic.DynamicMetaObject class.
    //
    // Parameters:
    //   expression:
    //     The expression representing this System.Dynamic.DynamicMetaObject during
    //     the dynamic binding process.
    //
    //   restrictions:
    //     The set of binding restrictions under which the binding is valid.
    //
    //   value:
    //     The runtime value represented by the System.Dynamic.DynamicMetaObject.
    public DynamicMetaObject(Expression expression, BindingRestrictions restrictions, object value)
    {
      Contract.Requires(expression != null);
      Contract.Requires(restrictions != null);
    }

    // Summary:
    //     The expression representing the System.Dynamic.DynamicMetaObject during the
    //     dynamic binding process.
    //
    // Returns:
    //     The expression representing the System.Dynamic.DynamicMetaObject during the
    //     dynamic binding process.
    public Expression Expression { get { Contract.Ensures(Contract.Result<Expression>() != null); return null; }  }
    //
    // Summary:
    //     Gets a value indicating whether the System.Dynamic.DynamicMetaObject has
    //     the runtime value.
    //
    // Returns:
    //     True if the System.Dynamic.DynamicMetaObject has the runtime value, otherwise
    //     false.
    // public bool HasValue { get; }
    //
    // Summary:
    //     Gets the limit type of the System.Dynamic.DynamicMetaObject.
    //
    // Returns:
    //     System.Dynamic.DynamicMetaObject.RuntimeType if runtime value is available,
    //     a type of the System.Dynamic.DynamicMetaObject.Expression otherwise.
    public Type LimitType { get { Contract.Ensures(Contract.Result<Type>() != null); return null; } }
    //
    // Summary:
    //     The set of binding restrictions under which the binding is valid.
    //
    // Returns:
    //     The set of binding restrictions.
    public BindingRestrictions Restrictions { get { Contract.Ensures(Contract.Result<BindingRestrictions>() != null); return null; } }
    //
    // Summary:
    //     Gets the System.Type of the runtime value or null if the System.Dynamic.DynamicMetaObject
    //     has no value associated with it.
    //
    // Returns:
    //     The System.Type of the runtime value or null.
    //public Type RuntimeType { get; }
    //
    // Summary:
    //     The runtime value represented by this System.Dynamic.DynamicMetaObject.
    //
    // Returns:
    //     The runtime value represented by this System.Dynamic.DynamicMetaObject.
    //public object Value { get; }

    // Summary:
    //     Performs the binding of the dynamic binary operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.BinaryOperationBinder that represents the
    //     details of the dynamic operation.
    //
    //   arg:
    //     An instance of the System.Dynamic.DynamicMetaObject representing the right
    //     hand side of the binary operation.
    //
    // Returns:
    //     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg);
    //
    // Summary:
    //     Performs the binding of the dynamic conversion operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.ConvertBinder that represents the details
    //     of the dynamic operation.
    //
    // Returns:
    //     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindConvert(ConvertBinder binder);
    //
    // Summary:
    //     Performs the binding of the dynamic create instance operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.CreateInstanceBinder that represents the
    //     details of the dynamic operation.
    //
    //   args:
    //     An array of System.Dynamic.DynamicMetaObject instances - arguments to the
    //     create instance operation.
    //
    // Returns:
    //     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args);
    //
    // Summary:
    //     Performs the binding of the dynamic delete index operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.DeleteIndexBinder that represents the details
    //     of the dynamic operation.
    //
    //   indexes:
    //     An array of System.Dynamic.DynamicMetaObject instances - indexes for the
    //     delete index operation.
    //
    // Returns:
    //     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes);
    //
    // Summary:
    //     Performs the binding of the dynamic delete member operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.DeleteMemberBinder that represents the
    //     details of the dynamic operation.
    //
    // Returns:
    //     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder);
    //
    // Summary:
    //     Performs the binding of the dynamic get index operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.GetIndexBinder that represents the details
    //     of the dynamic operation.
    //
    //   indexes:
    //     An array of System.Dynamic.DynamicMetaObject instances - indexes for the
    //     get index operation.
    //
    // Returns:
    //     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes);
    //
    // Summary:
    //     Performs the binding of the dynamic get member operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.GetMemberBinder that represents the details
    //     of the dynamic operation.
    //
    // Returns:
    //     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindGetMember(GetMemberBinder binder);
    //
    // Summary:
    //     Performs the binding of the dynamic invoke operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.InvokeBinder that represents the details
    //     of the dynamic operation.
    //
    //   args:
    //     An array of System.Dynamic.DynamicMetaObject instances - arguments to the
    //     invoke operation.
    //
    // Returns:
    //     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args);
    //
    // Summary:
    //     Performs the binding of the dynamic invoke member operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.InvokeMemberBinder that represents the
    //     details of the dynamic operation.
    //
    //   args:
    //     An array of System.Dynamic.DynamicMetaObject instances - arguments to the
    //     invoke member operation.
    //
    // Returns:
    ////     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args);
    //
    // Summary:
    //     Performs the binding of the dynamic set index operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.SetIndexBinder that represents the details
    //     of the dynamic operation.
    //
    //   indexes:
    //     An array of System.Dynamic.DynamicMetaObject instances - indexes for the
    //     set index operation.
    //
    //   value:
    //     The System.Dynamic.DynamicMetaObject representing the value for the set index
    //     operation.
    //
    // Returns:
    //     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value);
    //
    // Summary:
    //     Performs the binding of the dynamic set member operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.SetMemberBinder that represents the details
    //     of the dynamic operation.
    //
    //   value:
    //     The System.Dynamic.DynamicMetaObject representing the value for the set member
    //     operation.
    //
    // Returns:
    //     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value);
    //
    // Summary:
    //     Performs the binding of the dynamic unary operation.
    //
    // Parameters:
    //   binder:
    //     An instance of the System.Dynamic.UnaryOperationBinder that represents the
    //     details of the dynamic operation.
    //
    // Returns:
    //     The new System.Dynamic.DynamicMetaObject representing the result of the binding.
    //public virtual DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder);
    //
    // Summary:
    //     Creates a meta-object for the specified object.
    //
    // Parameters:
    //   value:
    //     The object to get a meta-object for.
    //
    //   expression:
    //     The expression representing this System.Dynamic.DynamicMetaObject during
    //     the dynamic binding process.
    //
    // Returns:
    //     If the given object implements System.Dynamic.IDynamicMetaObjectProvider
    //     and is not a remote object from outside the current AppDomain, returns the
    //     object's specific meta-object returned by System.Dynamic.IDynamicMetaObjectProvider.GetMetaObject(System.Linq.Expressions.Expression).
    //     Otherwise a plain new meta-object with no restrictions is created and returned.
    public static DynamicMetaObject Create(object value, Expression expression)
    {
      Contract.Ensures(Contract.Result<DynamicMetaObject>() != null);

      return null;
    }
    //
    // Summary:
    //     Returns the enumeration of all dynamic member names.
    //
    // Returns:
    //     The list of dynamic member names.
    public virtual IEnumerable<string> GetDynamicMemberNames()
    {
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);

      return null;
    }
  }
}
#endif