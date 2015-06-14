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
// File System.Linq.Expressions.SwitchExpression.cs
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
  sealed public partial class SwitchExpression : Expression
  {
    #region Methods and constructors
    private SwitchExpression() {}

    protected internal override Expression Accept (ExpressionVisitor visitor)
    {
      return default(Expression);
    }

    public SwitchExpression Update (Expression switchValue, IEnumerable<SwitchCase> cases, Expression defaultBody)
    {
      return default(SwitchExpression);
    }
    #endregion

    #region Properties and indexers
    public System.Collections.ObjectModel.ReadOnlyCollection<SwitchCase> Cases
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<SwitchCase>);
      }
    }

    public System.Reflection.MethodInfo Comparison
    {
      get
      {
        return default(System.Reflection.MethodInfo);
      }
    }

    public Expression DefaultBody
    {
      get
      {
        return default(Expression);
      }
    }

    public override ExpressionType NodeType
    {
      get
      {
        return default(ExpressionType);
      }
    }

    public Expression SwitchValue
    {
      get
      {
        return default(Expression);
      }
    }

    public override Type Type
    {
      get
      {
        return default(Type);
      }
    }
    #endregion
  }
}
#endif
