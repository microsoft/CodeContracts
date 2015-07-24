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
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text;

#if NETFRAMEWORK_4_0
using System.Reflection.Emit;
#endif

namespace System.Linq.Expressions
{
  // Summary:
  //     Describes a lambda expression.
  public class LambdaExpression : Expression
  {
    private LambdaExpression() { }

    // Summary:
    //     Gets the body of the lambda expression.
    //
    // Returns:
    //     An System.Linq.Expressions.Expression that represents the body of the lambda
    //     expression.
    public Expression Body
    {
      get
      {
        Contract.Ensures(Contract.Result<Expression>() != null);
        return default(Expression);
      }
    }
    //
    // Summary:
    //     Gets the parameters of the lambda expression.
    //
    // Returns:
    //     A System.Collections.ObjectModel.ReadOnlyCollection<T> of System.Linq.Expressions.ParameterExpression
    //     objects that represent the parameters of the lambda expression.
    public ReadOnlyCollection<ParameterExpression> Parameters
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyCollection<ParameterExpression>>() != null);
        return default(ReadOnlyCollection<ParameterExpression>);
      }
    }

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    //
    // Summary:
    //     Gets the return type of the lambda expression.
    //
    // Returns:
    //     The System.Type object representing the type of the lambda expression.
    public Type ReturnType
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);
        return default(Type);
      }
    }
#endif

#if !SILVERLIGHT_4_0_WP
    // Summary:
    //     Produces a delegate that represents the lambda expression.
    //
    // Returns:
    //     A System.Delegate that, when it is executed, has the behavior described by
    //     the semantics of the System.Linq.Expressions.LambdaExpression.
    public Delegate Compile()
    {
      Contract.Ensures(Contract.Result<Delegate>() != null);
      return default(Delegate);
    }
#endif

#if NETFRAMEWORK_4_0
    //
    // Summary:
    //     Produces a delegate that represents the lambda expression.
    //
    // Parameters:
    //   debugInfoGenerator:
    //     Debugging information generator used by the compiler to mark sequence points
    //     and annotate local variables.
    //
    // Returns:
    //     A delegate containing the compiled version of the lambda.
    public Delegate Compile(DebugInfoGenerator debugInfoGenerator)
    {
      Contract.Requires(debugInfoGenerator != null);
      Contract.Ensures(Contract.Result<Delegate>() != null);
      return default(Delegate);
    }

    //
    // Summary:
    //     Compiles the lambda into a method definition.
    //
    // Parameters:
    //   method:
    //     A System.Reflection.Emit.MethodBuilder which will be used to hold the lambda's
    //     IL.
    public void CompileToMethod(MethodBuilder method)
    {
      Contract.Requires(method != null);
      Contract.Requires(method.IsStatic);
    }

    //
    // Summary:
    //     Compiles the lambda into a method definition and custom debug information.
    //
    // Parameters:
    //   method:
    //     A System.Reflection.Emit.MethodBuilder which will be used to hold the lambda's
    //     IL.
    //
    //   debugInfoGenerator:
    //     Debugging information generator used by the compiler to mark sequence points
    //     and annotate local variables.
    public void CompileToMethod(MethodBuilder method, DebugInfoGenerator debugInfoGenerator)
    {
      Contract.Requires(method != null);
      Contract.Requires(method.IsStatic);
      Contract.Requires(debugInfoGenerator != null);
    }
#endif
  }
}
