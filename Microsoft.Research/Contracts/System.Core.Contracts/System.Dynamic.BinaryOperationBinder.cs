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
using System.Linq.Expressions;
using System.Runtime;
using System.Diagnostics.Contracts;

namespace System.Dynamic
{
  // Summary:
  //     Represents the binary dynamic operation at the call site, providing the binding
  //     semantic and the details about the operation.
  public abstract class BinaryOperationBinder : DynamicMetaObjectBinder
  {
    // Summary:
    //     Initializes a new instance of the System.Dynamic.BinaryOperationBinder class.
    //
    // Parameters:
    //   operation:
    //     The binary operation kind.
    protected BinaryOperationBinder(ExpressionType operation) { }

    // Summary:
    //     The binary operation kind.
    //
    // Returns:
    //     The System.Linq.Expressions.ExpressionType object representing the kind of
    //     binary operation.
    extern public ExpressionType Operation { get; }
    //
    // Summary:
    //     The result type of the operation.
    //
    // Returns:
    //     The result type of the operation.
    //public override sealed Type ReturnType { get; }

    // Summary:
    //     Performs the binding of the dynamic binary operation.
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
    //public override sealed DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args);
    //
    // Summary:
    //     Performs the binding of the binary dynamic operation if the target dynamic
    //     object cannot bind.
    //
    // Parameters:
    //   target:
    //     The target of the dynamic binary operation.
    //
    //   arg:
    //     The right hand side operand of the dynamic binary operation.
    //
    // Returns:
    //     The System.Dynamic.DynamicMetaObject representing the result of the binding.
    public DynamicMetaObject FallbackBinaryOperation(DynamicMetaObject target, DynamicMetaObject arg)
    {
      Contract.Ensures(Contract.Result<DynamicMetaObject>() != null);

      return null;
    }
    //
    // Summary:
    //     When overridden in the derived class, performs the binding of the binary
    //     dynamic operation if the target dynamic object cannot bind.
    //
    // Parameters:
    //   target:
    //     The target of the dynamic binary operation.
    //
    //   arg:
    //     The right hand side operand of the dynamic binary operation.
    //
    //   errorSuggestion:
    //     The binding result if the binding fails, or null.
    //
    // Returns:
    //     The System.Dynamic.DynamicMetaObject representing the result of the binding.
    public abstract DynamicMetaObject FallbackBinaryOperation(DynamicMetaObject target, DynamicMetaObject arg, DynamicMetaObject errorSuggestion);
  }
}
#endif