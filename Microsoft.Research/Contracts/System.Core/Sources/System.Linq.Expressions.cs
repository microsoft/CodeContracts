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

// File System.Linq.Expressions.cs
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
  public enum ExpressionType
  {
    Add = 0, 
    AddChecked = 1, 
    And = 2, 
    AndAlso = 3, 
    ArrayLength = 4, 
    ArrayIndex = 5, 
    Call = 6, 
    Coalesce = 7, 
    Conditional = 8, 
    Constant = 9, 
    Convert = 10, 
    ConvertChecked = 11, 
    Divide = 12, 
    Equal = 13, 
    ExclusiveOr = 14, 
    GreaterThan = 15, 
    GreaterThanOrEqual = 16, 
    Invoke = 17, 
    Lambda = 18, 
    LeftShift = 19, 
    LessThan = 20, 
    LessThanOrEqual = 21, 
    ListInit = 22, 
    MemberAccess = 23, 
    MemberInit = 24, 
    Modulo = 25, 
    Multiply = 26, 
    MultiplyChecked = 27, 
    Negate = 28, 
    UnaryPlus = 29, 
    NegateChecked = 30, 
    New = 31, 
    NewArrayInit = 32, 
    NewArrayBounds = 33, 
    Not = 34, 
    NotEqual = 35, 
    Or = 36, 
    OrElse = 37, 
    Parameter = 38, 
    Power = 39, 
    Quote = 40, 
    RightShift = 41, 
    Subtract = 42, 
    SubtractChecked = 43, 
    TypeAs = 44, 
    TypeIs = 45, 
    Assign = 46, 
    Block = 47, 
    DebugInfo = 48, 
    Decrement = 49, 
    Dynamic = 50, 
    Default = 51, 
    Extension = 52, 
    Goto = 53, 
    Increment = 54, 
    Index = 55, 
    Label = 56, 
    RuntimeVariables = 57, 
    Loop = 58, 
    Switch = 59, 
    Throw = 60, 
    Try = 61, 
    Unbox = 62, 
    AddAssign = 63, 
    AndAssign = 64, 
    DivideAssign = 65, 
    ExclusiveOrAssign = 66, 
    LeftShiftAssign = 67, 
    ModuloAssign = 68, 
    MultiplyAssign = 69, 
    OrAssign = 70, 
    PowerAssign = 71, 
    RightShiftAssign = 72, 
    SubtractAssign = 73, 
    AddAssignChecked = 74, 
    MultiplyAssignChecked = 75, 
    SubtractAssignChecked = 76, 
    PreIncrementAssign = 77, 
    PreDecrementAssign = 78, 
    PostIncrementAssign = 79, 
    PostDecrementAssign = 80, 
    TypeEqual = 81, 
    OnesComplement = 82, 
    IsTrue = 83, 
    IsFalse = 84, 
  }

  public enum GotoExpressionKind
  {
    Goto = 0, 
    Return = 1, 
    Break = 2, 
    Continue = 3, 
  }

  public enum MemberBindingType
  {
    Assignment = 0, 
    MemberBinding = 1, 
    ListBinding = 2, 
  }
}
