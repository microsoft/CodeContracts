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

// File System.Linq.Expressions.Expression.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Linq.Expressions
{
  abstract public partial class Expression
  {
    #region Methods and constructors
    protected internal virtual new System.Linq.Expressions.Expression Accept(ExpressionVisitor visitor)
    {
      Contract.Requires(visitor != null);

      return default(System.Linq.Expressions.Expression);
    }

    public static BinaryExpression Add(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression Add(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression AddAssign(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression AddAssign(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression AddAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression AddAssignChecked(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression AddAssignChecked(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression AddAssignChecked(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression AddChecked(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression AddChecked(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression And(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression And(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression AndAlso(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression AndAlso(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression AndAssign(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression AndAssign(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression AndAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static IndexExpression ArrayAccess(System.Linq.Expressions.Expression array, IEnumerable<System.Linq.Expressions.Expression> indexes)
    {
      Contract.Requires(array != null);
      Contract.Ensures(((System.Linq.Expressions.Expression)array).Type.IsArray == true);
      Contract.Ensures(0 <= ((System.Linq.Expressions.Expression)array).Type.GetArrayRank());
      Contract.Ensures(Contract.Result<System.Linq.Expressions.IndexExpression>() != null);

      return default(IndexExpression);
    }

    public static IndexExpression ArrayAccess(System.Linq.Expressions.Expression array, System.Linq.Expressions.Expression[] indexes)
    {
      Contract.Requires(array != null);
      Contract.Ensures(0 <= array.Type.GetArrayRank());
      Contract.Ensures(array.Type.IsArray == true);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.IndexExpression>() != null);

      return default(IndexExpression);
    }

    public static BinaryExpression ArrayIndex(Expression array, Expression index)
    {
      Contract.Ensures(array.Type.GetArrayRank() == 1);
      Contract.Ensures(array.Type.IsArray == true);

      return default(BinaryExpression);
    }

    public static MethodCallExpression ArrayIndex(System.Linq.Expressions.Expression array, IEnumerable<System.Linq.Expressions.Expression> indexes)
    {
      Contract.Ensures(((System.Linq.Expressions.Expression)array).Type.IsArray == true);
      Contract.Ensures(0 <= ((System.Linq.Expressions.Expression)array).Type.GetArrayRank());

      return default(MethodCallExpression);
    }

    public static MethodCallExpression ArrayIndex(System.Linq.Expressions.Expression array, System.Linq.Expressions.Expression[] indexes)
    {
      Contract.Ensures(0 <= array.Type.GetArrayRank());
      Contract.Ensures(array.Type.IsArray == true);

      return default(MethodCallExpression);
    }

    public static UnaryExpression ArrayLength(System.Linq.Expressions.Expression array)
    {
      Contract.Ensures(array.Type.GetArrayRank() == 1);

      return default(UnaryExpression);
    }

    public static BinaryExpression Assign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static MemberAssignment Bind(System.Reflection.MethodInfo propertyAccessor, System.Linq.Expressions.Expression expression)
    {
      return default(MemberAssignment);
    }

    public static MemberAssignment Bind(System.Reflection.MemberInfo member, System.Linq.Expressions.Expression expression)
    {
      return default(MemberAssignment);
    }

    public static BlockExpression Block(IEnumerable<ParameterExpression> variables, Expression[] expressions)
    {
      return default(BlockExpression);
    }

    public static BlockExpression Block(IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
    {
      return default(BlockExpression);
    }

    public static BlockExpression Block(Type type, IEnumerable<ParameterExpression> variables, Expression[] expressions)
    {
      return default(BlockExpression);
    }

    public static BlockExpression Block(Type type, IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
    {
      return default(BlockExpression);
    }

    public static BlockExpression Block(Expression arg0, Expression arg1)
    {
      return default(BlockExpression);
    }

    public static BlockExpression Block(Type type, IEnumerable<Expression> expressions)
    {
      return default(BlockExpression);
    }

    public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
    {
      return default(BlockExpression);
    }

    public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2, Expression arg3)
    {
      return default(BlockExpression);
    }

    public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2)
    {
      return default(BlockExpression);
    }

    public static BlockExpression Block(Type type, Expression[] expressions)
    {
      return default(BlockExpression);
    }

    public static BlockExpression Block(IEnumerable<Expression> expressions)
    {
      return default(BlockExpression);
    }

    public static BlockExpression Block(Expression[] expressions)
    {
      Contract.Requires(expressions != null);

      return default(BlockExpression);
    }

    public static GotoExpression Break(LabelTarget target, Type type)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static GotoExpression Break(LabelTarget target, System.Linq.Expressions.Expression value, Type type)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static GotoExpression Break(LabelTarget target)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static GotoExpression Break(LabelTarget target, System.Linq.Expressions.Expression value)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.MethodCallExpression>() != null);

      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression[] arguments)
    {
      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Reflection.MethodInfo method, IEnumerable<System.Linq.Expressions.Expression> arguments)
    {
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.MethodCallExpression>() != null);

      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.MethodCallExpression>() != null);

      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);
      Contract.Requires(arg2 != null);
      Contract.Requires(arg3 != null);
      Contract.Requires(arg4 != null);
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.MethodCallExpression>() != null);

      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);
      Contract.Requires(arg2 != null);
      Contract.Requires(arg3 != null);
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.MethodCallExpression>() != null);

      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);
      Contract.Requires(arg2 != null);
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.MethodCallExpression>() != null);

      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method)
    {
      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Linq.Expressions.Expression instance, string methodName, Type[] typeArguments, System.Linq.Expressions.Expression[] arguments)
    {
      Contract.Ensures((arguments.Length - Contract.OldValue(arguments.Length)) <= 0);

      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(Type type, string methodName, Type[] typeArguments, System.Linq.Expressions.Expression[] arguments)
    {
      Contract.Ensures((arguments.Length - Contract.OldValue(arguments.Length)) <= 0);

      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, IEnumerable<System.Linq.Expressions.Expression> arguments)
    {
      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Linq.Expressions.Expression[] arguments)
    {
      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.MethodCallExpression>() != null);

      return default(MethodCallExpression);
    }

    public static MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);
      Contract.Requires(arg2 != null);
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.MethodCallExpression>() != null);

      return default(MethodCallExpression);
    }

    public static CatchBlock Catch(ParameterExpression variable, Expression body, Expression filter)
    {
      Contract.Requires(variable != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.CatchBlock>() != null);

      return default(CatchBlock);
    }

    public static CatchBlock Catch(Type type, Expression body, Expression filter)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.CatchBlock>() != null);

      return default(CatchBlock);
    }

    public static CatchBlock Catch(ParameterExpression variable, Expression body)
    {
      Contract.Requires(variable != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.CatchBlock>() != null);

      return default(CatchBlock);
    }

    public static CatchBlock Catch(Type type, Expression body)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.CatchBlock>() != null);

      return default(CatchBlock);
    }

    public static DebugInfoExpression ClearDebugInfo(SymbolDocumentInfo document)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.DebugInfoExpression>() != null);

      return default(DebugInfoExpression);
    }

    public static BinaryExpression Coalesce(Expression left, Expression right, LambdaExpression conversion)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression Coalesce(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static ConditionalExpression Condition(Expression test, Expression ifTrue, Expression ifFalse)
    {
      return default(ConditionalExpression);
    }

    public static ConditionalExpression Condition(Expression test, Expression ifTrue, Expression ifFalse, Type type)
    {
      Contract.Requires(test != null);

      return default(ConditionalExpression);
    }

    public static ConstantExpression Constant(Object value, Type type)
    {
      return default(ConstantExpression);
    }

    public static ConstantExpression Constant(Object value)
    {
      return default(ConstantExpression);
    }

    public static GotoExpression Continue(LabelTarget target)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static GotoExpression Continue(LabelTarget target, Type type)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static UnaryExpression Convert(System.Linq.Expressions.Expression expression, Type type)
    {
      return default(UnaryExpression);
    }

    public static UnaryExpression Convert(System.Linq.Expressions.Expression expression, Type type, System.Reflection.MethodInfo method)
    {
      return default(UnaryExpression);
    }

    public static UnaryExpression ConvertChecked(System.Linq.Expressions.Expression expression, Type type)
    {
      return default(UnaryExpression);
    }

    public static UnaryExpression ConvertChecked(System.Linq.Expressions.Expression expression, Type type, System.Reflection.MethodInfo method)
    {
      return default(UnaryExpression);
    }

    public static DebugInfoExpression DebugInfo(SymbolDocumentInfo document, int startLine, int startColumn, int endLine, int endColumn)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.DebugInfoExpression>() != null);

      return default(DebugInfoExpression);
    }

    public static UnaryExpression Decrement(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression Decrement(System.Linq.Expressions.Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static DefaultExpression Default(Type type)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.DefaultExpression>() != null);

      return default(DefaultExpression);
    }

    public static BinaryExpression Divide(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression Divide(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression DivideAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression DivideAssign(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression DivideAssign(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static DynamicExpression Dynamic(System.Runtime.CompilerServices.CallSiteBinder binder, Type returnType, Expression arg0)
    {
      Contract.Requires(arg0 != null);

      return default(DynamicExpression);
    }

    public static DynamicExpression Dynamic(System.Runtime.CompilerServices.CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);

      return default(DynamicExpression);
    }

    public static DynamicExpression Dynamic(System.Runtime.CompilerServices.CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);
      Contract.Requires(arg2 != null);

      return default(DynamicExpression);
    }

    public static DynamicExpression Dynamic(System.Runtime.CompilerServices.CallSiteBinder binder, Type returnType, IEnumerable<Expression> arguments)
    {
      return default(DynamicExpression);
    }

    public static DynamicExpression Dynamic(System.Runtime.CompilerServices.CallSiteBinder binder, Type returnType, Expression[] arguments)
    {
      return default(DynamicExpression);
    }

    public static DynamicExpression Dynamic(System.Runtime.CompilerServices.CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);
      Contract.Requires(arg2 != null);
      Contract.Requires(arg3 != null);

      return default(DynamicExpression);
    }

    public static ElementInit ElementInit(System.Reflection.MethodInfo addMethod, Expression[] arguments)
    {
      return default(ElementInit);
    }

    public static System.Linq.Expressions.ElementInit ElementInit(System.Reflection.MethodInfo addMethod, IEnumerable<Expression> arguments)
    {
      return default(System.Linq.Expressions.ElementInit);
    }

    public static DefaultExpression Empty()
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.DefaultExpression>() != null);

      return default(DefaultExpression);
    }

    public static BinaryExpression Equal(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression Equal(Expression left, Expression right, bool liftToNull, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression ExclusiveOr(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression ExclusiveOr(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    protected Expression()
    {
    }

    protected Expression(ExpressionType nodeType, Type type)
    {
    }

    public static MemberExpression Field(System.Linq.Expressions.Expression expression, System.Reflection.FieldInfo field)
    {
      return default(MemberExpression);
    }

    public static MemberExpression Field(System.Linq.Expressions.Expression expression, Type type, string fieldName)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.MemberExpression>() != null);

      return default(MemberExpression);
    }

    public static MemberExpression Field(System.Linq.Expressions.Expression expression, string fieldName)
    {
      return default(MemberExpression);
    }

    public static Type GetActionType(Type[] typeArgs)
    {
      return default(Type);
    }

    public static Type GetDelegateType(Type[] typeArgs)
    {
      Contract.Requires(typeArgs != null);

      return default(Type);
    }

    public static Type GetFuncType(Type[] typeArgs)
    {
      return default(Type);
    }

    public static GotoExpression Goto(LabelTarget target, System.Linq.Expressions.Expression value, Type type)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static GotoExpression Goto(LabelTarget target)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static GotoExpression Goto(LabelTarget target, Type type)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static GotoExpression Goto(LabelTarget target, System.Linq.Expressions.Expression value)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static BinaryExpression GreaterThan(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression GreaterThan(Expression left, Expression right, bool liftToNull, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right, bool liftToNull, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static ConditionalExpression IfThen(Expression test, Expression ifTrue)
    {
      Contract.Requires(test != null);

      return default(ConditionalExpression);
    }

    public static ConditionalExpression IfThenElse(Expression test, Expression ifTrue, Expression ifFalse)
    {
      Contract.Requires(test != null);

      return default(ConditionalExpression);
    }

    public static UnaryExpression Increment(System.Linq.Expressions.Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression Increment(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Linq.Expressions.Expression[] arguments)
    {
      return default(InvocationExpression);
    }

    public static InvocationExpression Invoke(System.Linq.Expressions.Expression expression, IEnumerable<System.Linq.Expressions.Expression> arguments)
    {
      return default(InvocationExpression);
    }

    public static UnaryExpression IsFalse(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression IsFalse(System.Linq.Expressions.Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression IsTrue(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression IsTrue(System.Linq.Expressions.Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static LabelExpression Label(LabelTarget target)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.LabelExpression>() != null);

      return default(LabelExpression);
    }

    public static LabelTarget Label(Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(!type.ContainsGenericParameters);
      Contract.Ensures(!type.IsGenericTypeDefinition);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.LabelTarget>() != null);

      return default(LabelTarget);
    }

    public static LabelTarget Label(Type type, string name)
    {
      Contract.Requires(type != null);
      Contract.Ensures(!type.ContainsGenericParameters);
      Contract.Ensures(!type.IsGenericTypeDefinition);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.LabelTarget>() != null);

      return default(LabelTarget);
    }

    public static LabelTarget Label()
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.LabelTarget>() != null);

      return default(LabelTarget);
    }

    public static LabelExpression Label(LabelTarget target, System.Linq.Expressions.Expression defaultValue)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.LabelExpression>() != null);

      return default(LabelExpression);
    }

    public static LabelTarget Label(string name)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.LabelTarget>() != null);

      return default(LabelTarget);
    }

    public static LambdaExpression Lambda(Type delegateType, System.Linq.Expressions.Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
    {
      return default(LambdaExpression);
    }

    public static LambdaExpression Lambda(System.Linq.Expressions.Expression body, string name, IEnumerable<ParameterExpression> parameters)
    {
      return default(LambdaExpression);
    }

    public static System.Linq.Expressions.Expression<TDelegate> Lambda<TDelegate>(System.Linq.Expressions.Expression body, ParameterExpression[] parameters)
    {
      return default(System.Linq.Expressions.Expression<TDelegate>);
    }

    public static LambdaExpression Lambda(Type delegateType, System.Linq.Expressions.Expression body, string name, IEnumerable<ParameterExpression> parameters)
    {
      return default(LambdaExpression);
    }

    public static LambdaExpression Lambda(Type delegateType, System.Linq.Expressions.Expression body, bool tailCall, ParameterExpression[] parameters)
    {
      return default(LambdaExpression);
    }

    public static LambdaExpression Lambda(Type delegateType, System.Linq.Expressions.Expression body, ParameterExpression[] parameters)
    {
      return default(LambdaExpression);
    }

    public static LambdaExpression Lambda(Type delegateType, System.Linq.Expressions.Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
    {
      return default(LambdaExpression);
    }

    public static LambdaExpression Lambda(Type delegateType, System.Linq.Expressions.Expression body, IEnumerable<ParameterExpression> parameters)
    {
      return default(LambdaExpression);
    }

    public static System.Linq.Expressions.Expression<TDelegate> Lambda<TDelegate>(System.Linq.Expressions.Expression body, bool tailCall, ParameterExpression[] parameters)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.Expression<TDelegate>>() != null);

      return default(System.Linq.Expressions.Expression<TDelegate>);
    }

    public static LambdaExpression Lambda(System.Linq.Expressions.Expression body, bool tailCall, ParameterExpression[] parameters)
    {
      return default(LambdaExpression);
    }

    public static System.Linq.Expressions.Expression<TDelegate> Lambda<TDelegate>(System.Linq.Expressions.Expression body, string name, IEnumerable<ParameterExpression> parameters)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.Expression<TDelegate>>() != null);

      return default(System.Linq.Expressions.Expression<TDelegate>);
    }

    public static LambdaExpression Lambda(System.Linq.Expressions.Expression body, IEnumerable<ParameterExpression> parameters)
    {
      return default(LambdaExpression);
    }

    public static LambdaExpression Lambda(System.Linq.Expressions.Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
    {
      return default(LambdaExpression);
    }

    public static System.Linq.Expressions.Expression<TDelegate> Lambda<TDelegate>(System.Linq.Expressions.Expression body, IEnumerable<ParameterExpression> parameters)
    {
      return default(System.Linq.Expressions.Expression<TDelegate>);
    }

    public static System.Linq.Expressions.Expression<TDelegate> Lambda<TDelegate>(System.Linq.Expressions.Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.Expression<TDelegate>>() != null);

      return default(System.Linq.Expressions.Expression<TDelegate>);
    }

    public static System.Linq.Expressions.Expression<TDelegate> Lambda<TDelegate>(System.Linq.Expressions.Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.Expression<TDelegate>>() != null);

      return default(System.Linq.Expressions.Expression<TDelegate>);
    }

    public static LambdaExpression Lambda(System.Linq.Expressions.Expression body, ParameterExpression[] parameters)
    {
      return default(LambdaExpression);
    }

    public static LambdaExpression Lambda(System.Linq.Expressions.Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
    {
      return default(LambdaExpression);
    }

    public static BinaryExpression LeftShift(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression LeftShift(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression LeftShiftAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression LeftShiftAssign(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression LeftShiftAssign(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression LessThan(Expression left, Expression right, bool liftToNull, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression LessThan(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression LessThanOrEqual(Expression left, Expression right, bool liftToNull, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression LessThanOrEqual(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static MemberListBinding ListBind(System.Reflection.MemberInfo member, System.Linq.Expressions.ElementInit[] initializers)
    {
      return default(MemberListBinding);
    }

    public static MemberListBinding ListBind(System.Reflection.MemberInfo member, IEnumerable<System.Linq.Expressions.ElementInit> initializers)
    {
      return default(MemberListBinding);
    }

    public static MemberListBinding ListBind(System.Reflection.MethodInfo propertyAccessor, System.Linq.Expressions.ElementInit[] initializers)
    {
      Contract.Requires(propertyAccessor.DeclaringType != null);

      return default(MemberListBinding);
    }

    public static MemberListBinding ListBind(System.Reflection.MethodInfo propertyAccessor, IEnumerable<System.Linq.Expressions.ElementInit> initializers)
    {
      Contract.Requires(propertyAccessor.DeclaringType != null);

      return default(MemberListBinding);
    }

    public static ListInitExpression ListInit(NewExpression newExpression, System.Linq.Expressions.Expression[] initializers)
    {
      return default(ListInitExpression);
    }

    public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<System.Linq.Expressions.Expression> initializers)
    {
      return default(ListInitExpression);
    }

    public static ListInitExpression ListInit(NewExpression newExpression, System.Reflection.MethodInfo addMethod, IEnumerable<System.Linq.Expressions.Expression> initializers)
    {
      return default(ListInitExpression);
    }

    public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<System.Linq.Expressions.ElementInit> initializers)
    {
      return default(ListInitExpression);
    }

    public static ListInitExpression ListInit(NewExpression newExpression, System.Reflection.MethodInfo addMethod, System.Linq.Expressions.Expression[] initializers)
    {
      return default(ListInitExpression);
    }

    public static ListInitExpression ListInit(NewExpression newExpression, System.Linq.Expressions.ElementInit[] initializers)
    {
      return default(ListInitExpression);
    }

    public static LoopExpression Loop(System.Linq.Expressions.Expression body)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.LoopExpression>() != null);

      return default(LoopExpression);
    }

    public static LoopExpression Loop(System.Linq.Expressions.Expression body, LabelTarget break, LabelTarget continue)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.LoopExpression>() != null);

      return default(LoopExpression);
    }

    public static LoopExpression Loop(System.Linq.Expressions.Expression body, LabelTarget break)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.LoopExpression>() != null);

      return default(LoopExpression);
    }

    public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static CatchBlock MakeCatchBlock(Type type, ParameterExpression variable, Expression body, Expression filter)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.CatchBlock>() != null);

      return default(CatchBlock);
    }

    public static DynamicExpression MakeDynamic(Type delegateType, System.Runtime.CompilerServices.CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);
      Contract.Requires(arg2 != null);
      Contract.Requires(delegateType != null);

      return default(DynamicExpression);
    }

    public static DynamicExpression MakeDynamic(Type delegateType, System.Runtime.CompilerServices.CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);
      Contract.Requires(arg2 != null);
      Contract.Requires(arg3 != null);
      Contract.Requires(delegateType != null);

      return default(DynamicExpression);
    }

    public static DynamicExpression MakeDynamic(Type delegateType, System.Runtime.CompilerServices.CallSiteBinder binder, Expression arg0, Expression arg1)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(arg1 != null);
      Contract.Requires(delegateType != null);

      return default(DynamicExpression);
    }

    public static DynamicExpression MakeDynamic(Type delegateType, System.Runtime.CompilerServices.CallSiteBinder binder, IEnumerable<Expression> arguments)
    {
      Contract.Requires(delegateType != null);

      return default(DynamicExpression);
    }

    public static DynamicExpression MakeDynamic(Type delegateType, System.Runtime.CompilerServices.CallSiteBinder binder, Expression arg0)
    {
      Contract.Requires(arg0 != null);
      Contract.Requires(delegateType != null);

      return default(DynamicExpression);
    }

    public static DynamicExpression MakeDynamic(Type delegateType, System.Runtime.CompilerServices.CallSiteBinder binder, Expression[] arguments)
    {
      Contract.Requires(delegateType != null);

      return default(DynamicExpression);
    }

    public static GotoExpression MakeGoto(GotoExpressionKind kind, LabelTarget target, System.Linq.Expressions.Expression value, Type type)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static IndexExpression MakeIndex(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, IEnumerable<System.Linq.Expressions.Expression> arguments)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.IndexExpression>() != null);

      return default(IndexExpression);
    }

    public static MemberExpression MakeMemberAccess(System.Linq.Expressions.Expression expression, System.Reflection.MemberInfo member)
    {
      return default(MemberExpression);
    }

    public static TryExpression MakeTry(Type type, System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression finally, System.Linq.Expressions.Expression fault, IEnumerable<CatchBlock> handlers)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.TryExpression>() != null);

      return default(TryExpression);
    }

    public static UnaryExpression MakeUnary(ExpressionType unaryType, System.Linq.Expressions.Expression operand, Type type)
    {
      return default(UnaryExpression);
    }

    public static UnaryExpression MakeUnary(ExpressionType unaryType, System.Linq.Expressions.Expression operand, Type type, System.Reflection.MethodInfo method)
    {
      return default(UnaryExpression);
    }

    public static MemberMemberBinding MemberBind(System.Reflection.MemberInfo member, IEnumerable<MemberBinding> bindings)
    {
      return default(MemberMemberBinding);
    }

    public static MemberMemberBinding MemberBind(System.Reflection.MethodInfo propertyAccessor, MemberBinding[] bindings)
    {
      Contract.Requires(propertyAccessor.DeclaringType != null);

      return default(MemberMemberBinding);
    }

    public static MemberMemberBinding MemberBind(System.Reflection.MethodInfo propertyAccessor, IEnumerable<MemberBinding> bindings)
    {
      Contract.Requires(propertyAccessor.DeclaringType != null);

      return default(MemberMemberBinding);
    }

    public static MemberMemberBinding MemberBind(System.Reflection.MemberInfo member, MemberBinding[] bindings)
    {
      return default(MemberMemberBinding);
    }

    public static MemberInitExpression MemberInit(NewExpression newExpression, MemberBinding[] bindings)
    {
      return default(MemberInitExpression);
    }

    public static MemberInitExpression MemberInit(NewExpression newExpression, IEnumerable<MemberBinding> bindings)
    {
      return default(MemberInitExpression);
    }

    public static BinaryExpression Modulo(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression Modulo(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression ModuloAssign(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression ModuloAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression ModuloAssign(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression Multiply(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression Multiply(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression MultiplyAssign(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression MultiplyAssign(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression MultiplyAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression MultiplyChecked(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression MultiplyChecked(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static UnaryExpression Negate(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      return default(UnaryExpression);
    }

    public static UnaryExpression Negate(System.Linq.Expressions.Expression expression)
    {
      return default(UnaryExpression);
    }

    public static UnaryExpression NegateChecked(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      return default(UnaryExpression);
    }

    public static UnaryExpression NegateChecked(System.Linq.Expressions.Expression expression)
    {
      return default(UnaryExpression);
    }

    public static NewExpression New(System.Reflection.ConstructorInfo constructor, IEnumerable<System.Linq.Expressions.Expression> arguments, System.Reflection.MemberInfo[] members)
    {
      return default(NewExpression);
    }

    public static NewExpression New(System.Reflection.ConstructorInfo constructor, System.Linq.Expressions.Expression[] arguments)
    {
      Contract.Requires(constructor.DeclaringType != null);

      return default(NewExpression);
    }

    public static NewExpression New(System.Reflection.ConstructorInfo constructor, IEnumerable<System.Linq.Expressions.Expression> arguments)
    {
      Contract.Requires(constructor.DeclaringType != null);

      return default(NewExpression);
    }

    public static NewExpression New(System.Reflection.ConstructorInfo constructor)
    {
      Contract.Requires(constructor.DeclaringType != null);

      return default(NewExpression);
    }

    public static NewExpression New(System.Reflection.ConstructorInfo constructor, IEnumerable<System.Linq.Expressions.Expression> arguments, IEnumerable<System.Reflection.MemberInfo> members)
    {
      return default(NewExpression);
    }

    public static NewExpression New(Type type)
    {
      return default(NewExpression);
    }

    public static NewArrayExpression NewArrayBounds(Type type, System.Linq.Expressions.Expression[] bounds)
    {
      return default(NewArrayExpression);
    }

    public static NewArrayExpression NewArrayBounds(Type type, IEnumerable<System.Linq.Expressions.Expression> bounds)
    {
      return default(NewArrayExpression);
    }

    public static NewArrayExpression NewArrayInit(Type type, IEnumerable<System.Linq.Expressions.Expression> initializers)
    {
      return default(NewArrayExpression);
    }

    public static NewArrayExpression NewArrayInit(Type type, System.Linq.Expressions.Expression[] initializers)
    {
      return default(NewArrayExpression);
    }

    public static UnaryExpression Not(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      return default(UnaryExpression);
    }

    public static UnaryExpression Not(System.Linq.Expressions.Expression expression)
    {
      return default(UnaryExpression);
    }

    public static BinaryExpression NotEqual(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression NotEqual(Expression left, Expression right, bool liftToNull, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static UnaryExpression OnesComplement(System.Linq.Expressions.Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression OnesComplement(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static BinaryExpression Or(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression Or(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression OrAssign(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression OrAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression OrAssign(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression OrElse(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression OrElse(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static ParameterExpression Parameter(Type type, string name)
    {
      return default(ParameterExpression);
    }

    public static ParameterExpression Parameter(Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.ParameterExpression>() != null);

      return default(ParameterExpression);
    }

    public static UnaryExpression PostDecrementAssign(System.Linq.Expressions.Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression PostDecrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression PostIncrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression PostIncrementAssign(System.Linq.Expressions.Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static BinaryExpression Power(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression Power(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression PowerAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression PowerAssign(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression PowerAssign(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static UnaryExpression PreDecrementAssign(System.Linq.Expressions.Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression PreDecrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression PreIncrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression PreIncrementAssign(System.Linq.Expressions.Expression expression)
    {
      Contract.Requires(expression != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static IndexExpression Property(System.Linq.Expressions.Expression instance, string propertyName, System.Linq.Expressions.Expression[] arguments)
    {
      Contract.Requires(instance != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.IndexExpression>() != null);

      return default(IndexExpression);
    }

    public static MemberExpression Property(System.Linq.Expressions.Expression expression, Type type, string propertyName)
    {
      Contract.Requires(propertyName != null);
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.MemberExpression>() != null);

      return default(MemberExpression);
    }

    public static MemberExpression Property(System.Linq.Expressions.Expression expression, System.Reflection.PropertyInfo property)
    {
      return default(MemberExpression);
    }

    public static MemberExpression Property(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo propertyAccessor)
    {
      return default(MemberExpression);
    }

    public static IndexExpression Property(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, System.Linq.Expressions.Expression[] arguments)
    {
      Contract.Requires(indexer != null);
      Contract.Ensures(!indexer.PropertyType.IsByRef);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.IndexExpression>() != null);

      return default(IndexExpression);
    }

    public static IndexExpression Property(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, IEnumerable<System.Linq.Expressions.Expression> arguments)
    {
      Contract.Requires(indexer != null);
      Contract.Ensures(!indexer.PropertyType.IsByRef);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.IndexExpression>() != null);

      return default(IndexExpression);
    }

    public static MemberExpression Property(System.Linq.Expressions.Expression expression, string propertyName)
    {
      return default(MemberExpression);
    }

    public static MemberExpression PropertyOrField(System.Linq.Expressions.Expression expression, string propertyOrFieldName)
    {
      return default(MemberExpression);
    }

    public static UnaryExpression Quote(System.Linq.Expressions.Expression expression)
    {
      return default(UnaryExpression);
    }

    public virtual new System.Linq.Expressions.Expression Reduce()
    {
      return default(System.Linq.Expressions.Expression);
    }

    public System.Linq.Expressions.Expression ReduceAndCheck()
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.Expression>() != null);

      return default(System.Linq.Expressions.Expression);
    }

    public System.Linq.Expressions.Expression ReduceExtensions()
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.Expression>() != null);

      return default(System.Linq.Expressions.Expression);
    }

    public static BinaryExpression ReferenceEqual(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression ReferenceNotEqual(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static UnaryExpression Rethrow()
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression Rethrow(Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(!type.ContainsGenericParameters);
      Contract.Ensures(!type.IsGenericTypeDefinition);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static GotoExpression Return(LabelTarget target, System.Linq.Expressions.Expression value)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static GotoExpression Return(LabelTarget target)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static GotoExpression Return(LabelTarget target, Type type)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static GotoExpression Return(LabelTarget target, System.Linq.Expressions.Expression value, Type type)
    {
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.GotoExpression>() != null);

      return default(GotoExpression);
    }

    public static BinaryExpression RightShift(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression RightShift(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression RightShiftAssign(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression RightShiftAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression RightShiftAssign(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static RuntimeVariablesExpression RuntimeVariables(ParameterExpression[] variables)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.RuntimeVariablesExpression>() != null);

      return default(RuntimeVariablesExpression);
    }

    public static RuntimeVariablesExpression RuntimeVariables(IEnumerable<ParameterExpression> variables)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.RuntimeVariablesExpression>() != null);

      return default(RuntimeVariablesExpression);
    }

    public static BinaryExpression Subtract(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression Subtract(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression SubtractAssign(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression SubtractAssign(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression SubtractAssign(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression SubtractAssignChecked(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression SubtractAssignChecked(Expression left, Expression right, System.Reflection.MethodInfo method, LambdaExpression conversion)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression SubtractAssignChecked(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }

    public static BinaryExpression SubtractChecked(Expression left, Expression right, System.Reflection.MethodInfo method)
    {
      return default(BinaryExpression);
    }

    public static BinaryExpression SubtractChecked(Expression left, Expression right)
    {
      return default(BinaryExpression);
    }

    public static SwitchExpression Switch(System.Linq.Expressions.Expression switchValue, System.Linq.Expressions.SwitchCase[] cases)
    {
      Contract.Requires(switchValue != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SwitchExpression>() != null);

      return default(SwitchExpression);
    }

    public static SwitchExpression Switch(System.Linq.Expressions.Expression switchValue, System.Linq.Expressions.Expression defaultBody, System.Reflection.MethodInfo comparison, System.Linq.Expressions.SwitchCase[] cases)
    {
      Contract.Requires(switchValue != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SwitchExpression>() != null);

      return default(SwitchExpression);
    }

    public static SwitchExpression Switch(Type type, System.Linq.Expressions.Expression switchValue, System.Linq.Expressions.Expression defaultBody, System.Reflection.MethodInfo comparison, System.Linq.Expressions.SwitchCase[] cases)
    {
      Contract.Requires(switchValue != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SwitchExpression>() != null);

      return default(SwitchExpression);
    }

    public static SwitchExpression Switch(System.Linq.Expressions.Expression switchValue, System.Linq.Expressions.Expression defaultBody, System.Reflection.MethodInfo comparison, IEnumerable<System.Linq.Expressions.SwitchCase> cases)
    {
      Contract.Requires(switchValue != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SwitchExpression>() != null);

      return default(SwitchExpression);
    }

    public static SwitchExpression Switch(Type type, System.Linq.Expressions.Expression switchValue, System.Linq.Expressions.Expression defaultBody, System.Reflection.MethodInfo comparison, IEnumerable<System.Linq.Expressions.SwitchCase> cases)
    {
      Contract.Requires(switchValue != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SwitchExpression>() != null);

      return default(SwitchExpression);
    }

    public static SwitchExpression Switch(System.Linq.Expressions.Expression switchValue, System.Linq.Expressions.Expression defaultBody, System.Linq.Expressions.SwitchCase[] cases)
    {
      Contract.Requires(switchValue != null);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SwitchExpression>() != null);

      return default(SwitchExpression);
    }

    public static System.Linq.Expressions.SwitchCase SwitchCase(System.Linq.Expressions.Expression body, IEnumerable<System.Linq.Expressions.Expression> testValues)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SwitchCase>() != null);

      return default(System.Linq.Expressions.SwitchCase);
    }

    public static SwitchCase SwitchCase(System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression[] testValues)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SwitchCase>() != null);

      return default(SwitchCase);
    }

    public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language, Guid languageVendor)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SymbolDocumentInfo>() != null);

      return default(SymbolDocumentInfo);
    }

    public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SymbolDocumentInfo>() != null);

      return default(SymbolDocumentInfo);
    }

    public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language, Guid languageVendor, Guid documentType)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SymbolDocumentInfo>() != null);

      return default(SymbolDocumentInfo);
    }

    public static SymbolDocumentInfo SymbolDocument(string fileName)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.SymbolDocumentInfo>() != null);

      return default(SymbolDocumentInfo);
    }

    public static UnaryExpression Throw(System.Linq.Expressions.Expression value)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static UnaryExpression Throw(System.Linq.Expressions.Expression value, Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(!type.ContainsGenericParameters);
      Contract.Ensures(!type.IsGenericTypeDefinition);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public override string ToString()
    {
      return default(string);
    }

    public static TryExpression TryCatch(System.Linq.Expressions.Expression body, CatchBlock[] handlers)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.TryExpression>() != null);

      return default(TryExpression);
    }

    public static TryExpression TryCatchFinally(System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression finally, CatchBlock[] handlers)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.TryExpression>() != null);

      return default(TryExpression);
    }

    public static TryExpression TryFault(System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression fault)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.TryExpression>() != null);

      return default(TryExpression);
    }

    public static TryExpression TryFinally(System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression finally)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.TryExpression>() != null);

      return default(TryExpression);
    }

    public static bool TryGetActionType(Type[] typeArgs, out Type actionType)
    {
      actionType = default(Type);

      return default(bool);
    }

    public static bool TryGetFuncType(Type[] typeArgs, out Type funcType)
    {
      funcType = default(Type);

      return default(bool);
    }

    public static UnaryExpression TypeAs(System.Linq.Expressions.Expression expression, Type type)
    {
      return default(UnaryExpression);
    }

    public static TypeBinaryExpression TypeEqual(System.Linq.Expressions.Expression expression, Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(!type.IsByRef);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.TypeBinaryExpression>() != null);

      return default(TypeBinaryExpression);
    }

    public static TypeBinaryExpression TypeIs(System.Linq.Expressions.Expression expression, Type type)
    {
      Contract.Ensures(!type.IsByRef);

      return default(TypeBinaryExpression);
    }

    public static UnaryExpression UnaryPlus(System.Linq.Expressions.Expression expression)
    {
      return default(UnaryExpression);
    }

    public static UnaryExpression UnaryPlus(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
    {
      return default(UnaryExpression);
    }

    public static UnaryExpression Unbox(System.Linq.Expressions.Expression expression, Type type)
    {
      Contract.Requires(expression != null);
      Contract.Requires(type != null);
      Contract.Ensures(!type.ContainsGenericParameters);
      Contract.Ensures(!type.IsGenericTypeDefinition);
      Contract.Ensures(Contract.Result<System.Linq.Expressions.UnaryExpression>() != null);

      return default(UnaryExpression);
    }

    public static ParameterExpression Variable(Type type, string name)
    {
      return default(ParameterExpression);
    }

    public static ParameterExpression Variable(Type type)
    {
      return default(ParameterExpression);
    }

    protected internal virtual new System.Linq.Expressions.Expression VisitChildren(ExpressionVisitor visitor)
    {
      Contract.Requires(visitor != null);

      return default(System.Linq.Expressions.Expression);
    }
    #endregion

    #region Properties and indexers
    public virtual new bool CanReduce
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new ExpressionType NodeType
    {
      get
      {
        return default(ExpressionType);
      }
    }

    public virtual new Type Type
    {
      get
      {
        return default(Type);
      }
    }
    #endregion
  }
}
