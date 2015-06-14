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

// File System.Linq.Expressions.BinaryExpression.cs
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
  public partial class BinaryExpression : Expression
  {
    #region Methods and constructors
    protected internal override Expression Accept(ExpressionVisitor visitor)
    {
      return default(Expression);
    }

    internal BinaryExpression()
    {
    }

    public override Expression Reduce()
    {
      return default(Expression);
    }

    public BinaryExpression Update(Expression left, LambdaExpression conversion, Expression right)
    {
      Contract.Ensures(Contract.Result<System.Linq.Expressions.BinaryExpression>() != null);

      return default(BinaryExpression);
    }
    #endregion

    #region Properties and indexers
    public override bool CanReduce
    {
      get
      {
        return default(bool);
      }
    }

    public LambdaExpression Conversion
    {
      get
      {
        return default(LambdaExpression);
      }
    }

    public bool IsLifted
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsLiftedToNull
    {
      get
      {
        return default(bool);
      }
    }

    public Expression Left
    {
      get
      {
        return default(Expression);
      }
    }

    public System.Reflection.MethodInfo Method
    {
      get
      {
        return default(System.Reflection.MethodInfo);
      }
    }

    public Expression Right
    {
      get
      {
        return default(Expression);
      }
    }
    #endregion
  }
}
