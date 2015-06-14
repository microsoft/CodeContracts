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

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
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
    protected ExpressionVisitor ()
    {
    }

    public static System.Collections.ObjectModel.ReadOnlyCollection<T> Visit<T> (System.Collections.ObjectModel.ReadOnlyCollection<T> nodes, Func<T, T> elementVisitor)
    {
      return default(System.Collections.ObjectModel.ReadOnlyCollection<T>);
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<Expression> Visit (System.Collections.ObjectModel.ReadOnlyCollection<Expression> nodes)
    {
      return default(System.Collections.ObjectModel.ReadOnlyCollection<Expression>);
    }

    public virtual new Expression Visit (Expression node)
    {
      return default(Expression);
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<T> VisitAndConvert<T> (System.Collections.ObjectModel.ReadOnlyCollection<T> nodes, string callerName)
    {
      return default(System.Collections.ObjectModel.ReadOnlyCollection<T>);
    }

    public T VisitAndConvert<T> (T node, string callerName)
    {
      return default(T);
    }

    protected internal virtual new Expression VisitBinary (BinaryExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitBlock (BlockExpression node)
    {
      return default(Expression);
    }

    protected virtual new CatchBlock VisitCatchBlock (CatchBlock node)
    {
      return default(CatchBlock);
    }

    protected internal virtual new Expression VisitConditional (ConditionalExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitConstant (ConstantExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitDebugInfo (DebugInfoExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitDefault (DefaultExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitDynamic (DynamicExpression node)
    {
      return default(Expression);
    }

    protected virtual new ElementInit VisitElementInit (ElementInit node)
    {
      return default(ElementInit);
    }

    protected internal virtual new Expression VisitExtension (Expression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitGoto (GotoExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitIndex (IndexExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitInvocation (InvocationExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitLabel (LabelExpression node)
    {
      return default(Expression);
    }

    protected virtual new LabelTarget VisitLabelTarget (LabelTarget node)
    {
      return default(LabelTarget);
    }

    protected internal virtual new Expression VisitLambda<T> (Expression<T> node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitListInit (ListInitExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitLoop (LoopExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitMember (MemberExpression node)
    {
      return default(Expression);
    }

    protected virtual new MemberAssignment VisitMemberAssignment (MemberAssignment node)
    {
      return default(MemberAssignment);
    }

    protected virtual new MemberBinding VisitMemberBinding (MemberBinding node)
    {
      return default(MemberBinding);
    }

    protected internal virtual new Expression VisitMemberInit (MemberInitExpression node)
    {
      return default(Expression);
    }

    protected virtual new MemberListBinding VisitMemberListBinding (MemberListBinding node)
    {
      return default(MemberListBinding);
    }

    protected virtual new MemberMemberBinding VisitMemberMemberBinding (MemberMemberBinding node)
    {
      return default(MemberMemberBinding);
    }

    protected internal virtual new Expression VisitMethodCall (MethodCallExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitNew (NewExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitNewArray (NewArrayExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitParameter (ParameterExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitRuntimeVariables (RuntimeVariablesExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitSwitch (SwitchExpression node)
    {
      return default(Expression);
    }

    protected virtual new SwitchCase VisitSwitchCase (SwitchCase node)
    {
      return default(SwitchCase);
    }

    protected internal virtual new Expression VisitTry (TryExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitTypeBinary (TypeBinaryExpression node)
    {
      return default(Expression);
    }

    protected internal virtual new Expression VisitUnary (UnaryExpression node)
    {
      return default(Expression);
    }
    #endregion
  }
}
#endif