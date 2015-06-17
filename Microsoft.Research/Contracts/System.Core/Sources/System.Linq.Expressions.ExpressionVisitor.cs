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

// File System.Linq.Expressions.ExpressionVisitor.cs
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
  abstract public partial class ExpressionVisitor
  {
    #region Methods and constructors
    protected ExpressionVisitor()
    {
    }

    public static System.Collections.ObjectModel.ReadOnlyCollection<T> Visit<T>(System.Collections.ObjectModel.ReadOnlyCollection<T> nodes, Func<T, T> elementVisitor)
    {
      Contract.Requires(nodes != null);
      Contract.Ensures(0 <= nodes.Count);
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<T>>() != null);

      return default(System.Collections.ObjectModel.ReadOnlyCollection<T>);
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<Expression> Visit(System.Collections.ObjectModel.ReadOnlyCollection<Expression> nodes)
    {
      Contract.Requires(nodes != null);
      Contract.Ensures(0 <= nodes.Count);
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>>() != null);

      return default(System.Collections.ObjectModel.ReadOnlyCollection<Expression>);
    }

    public virtual new Expression Visit(Expression node)
    {
      return default(Expression);
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<T> VisitAndConvert<T>(System.Collections.ObjectModel.ReadOnlyCollection<T> nodes, string callerName)
    {
      Contract.Requires(nodes != null);
      Contract.Ensures(0 <= nodes.Count);
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<T>>() != null);

      return default(System.Collections.ObjectModel.ReadOnlyCollection<T>);
    }

    public T VisitAndConvert<T>(T node, string callerName)
    {
      return default(T);
    }

    protected internal virtual new Expression VisitBinary(BinaryExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitBlock(BlockExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected virtual new CatchBlock VisitCatchBlock(CatchBlock node)
    {
      Contract.Requires(node != null);

      return default(CatchBlock);
    }

    protected internal virtual new Expression VisitConditional(ConditionalExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitConstant(ConstantExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitDebugInfo(DebugInfoExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitDefault(DefaultExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitDynamic(DynamicExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected virtual new ElementInit VisitElementInit(ElementInit node)
    {
      Contract.Requires(node != null);

      return default(ElementInit);
    }

    protected internal virtual new Expression VisitExtension(Expression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitGoto(GotoExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitIndex(IndexExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitInvocation(InvocationExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitLabel(LabelExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected virtual new LabelTarget VisitLabelTarget(LabelTarget node)
    {
      return default(LabelTarget);
    }

    protected internal virtual new Expression VisitLambda<T>(Expression<T> node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitListInit(ListInitExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitLoop(LoopExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitMember(MemberExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected virtual new MemberAssignment VisitMemberAssignment(MemberAssignment node)
    {
      Contract.Requires(node != null);

      return default(MemberAssignment);
    }

    protected virtual new MemberBinding VisitMemberBinding(MemberBinding node)
    {
      Contract.Requires(node != null);

      return default(MemberBinding);
    }

    protected internal virtual new Expression VisitMemberInit(MemberInitExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected virtual new MemberListBinding VisitMemberListBinding(MemberListBinding node)
    {
      Contract.Requires(node != null);

      return default(MemberListBinding);
    }

    protected virtual new MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
    {
      Contract.Requires(node != null);

      return default(MemberMemberBinding);
    }

    protected internal virtual new Expression VisitMethodCall(MethodCallExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitNew(NewExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitNewArray(NewArrayExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitParameter(ParameterExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
    {
      Contract.Requires(node != null);
      Contract.Requires(node.Variables != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitSwitch(SwitchExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected virtual new SwitchCase VisitSwitchCase(SwitchCase node)
    {
      Contract.Requires(node != null);
      Contract.Requires(node.TestValues != null);

      return default(SwitchCase);
    }

    protected internal virtual new Expression VisitTry(TryExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitTypeBinary(TypeBinaryExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }

    protected internal virtual new Expression VisitUnary(UnaryExpression node)
    {
      Contract.Requires(node != null);

      return default(Expression);
    }
    #endregion
  }
}
