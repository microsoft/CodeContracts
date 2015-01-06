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
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Diagnostics.Contracts;

namespace System.Dynamic
{
  // Summary:
  //     The dynamic call site binder that participates in the System.Dynamic.DynamicMetaObject
  //     binding protocol.
  public abstract class DynamicMetaObjectBinder : CallSiteBinder
  {
    // Summary:
    //     Initializes a new instance of the System.Dynamic.DynamicMetaObjectBinder
    //     class.
    protected DynamicMetaObjectBinder() { }

    // Summary:
    //     The result type of the operation.
    //
    // Returns:
    //     The System.Type object representing the result type of the operation.
    public virtual Type ReturnType { get { Contract.Ensures(Contract.Result<Type>() != null); return null; } }

    // Summary:
    //     When overridden in the derived class, performs the binding of the dynamic
    //     operation.
    //
    // Parameters:
    //   target:
    //     The target of the dynamic operation.
    //
    //   args:
    //     An array of arguments of the dynamic operation.
    //
    // Returns:
    //     The System.Dynamic.DynamicMetaObject representing the result of the binding.
    public abstract DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args);
    //
    // Summary:
    //     Performs the runtime binding of the dynamic operation on a set of arguments.
    //
    // Parameters:
    //   args:
    //     An array of arguments to the dynamic operation.
    //
    //   parameters:
    //     The array of System.Linq.Expressions.ParameterExpression instances that represent
    //     the parameters of the call site in the binding process.
    //
    //   returnLabel:
    //     A LabelTarget used to return the result of the dynamic binding.
    //
    // Returns:
    //     An Expression that performs tests on the dynamic operation arguments, and
    //     performs the dynamic operation if the tests are valid. If the tests fail
    //     on subsequent occurrences of the dynamic operation, Bind will be called again
    //     to produce a new System.Linq.Expressions.Expression for the new argument
    //     types.
    //public override sealed Expression Bind(object[] args, ReadOnlyCollection<ParameterExpression> parameters, LabelTarget returnLabel);
    //
    // Summary:
    //     Defers the binding of the operation until later time when the runtime values
    //     of all dynamic operation arguments have been computed.
    //
    // Parameters:
    //   args:
    //     An array of arguments of the dynamic operation.
    //
    // Returns:
    //     The System.Dynamic.DynamicMetaObject representing the result of the binding.
    public DynamicMetaObject Defer(params DynamicMetaObject[] args)
    {
      Contract.Ensures(Contract.Result<DynamicMetaObject>() != null);

      return null;
    }
    //
    // Summary:
    //     Defers the binding of the operation until later time when the runtime values
    //     of all dynamic operation arguments have been computed.
    //
    // Parameters:
    //   target:
    //     The target of the dynamic operation.
    //
    //   args:
    //     An array of arguments of the dynamic operation.
    //
    // Returns:
    //     The System.Dynamic.DynamicMetaObject representing the result of the binding.
    public DynamicMetaObject Defer(DynamicMetaObject target, params DynamicMetaObject[] args)
    {
      Contract.Ensures(Contract.Result<DynamicMetaObject>() != null);

      return null;

    }
    //
    // Summary:
    //     Gets an expression that will cause the binding to be updated. It indicates
    //     that the expression's binding is no longer valid. This is typically used
    //     when the "version" of a dynamic object has changed.
    //
    // Parameters:
    //   type:
    //     The System.Linq.Expressions.Expression.Type property of the resulting expression;
    //     any type is allowed.
    //
    // Returns:
    //     The update expression.
    public Expression GetUpdateExpression(Type type)
    {
      Contract.Ensures(Contract.Result<Expression>() != null);

      return null;
    }
  }
}
#endif