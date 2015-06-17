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

// File System.CodeDom.cs
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


namespace System.CodeDom
{
  public enum CodeBinaryOperatorType
  {
    Add = 0, 
    Subtract = 1, 
    Multiply = 2, 
    Divide = 3, 
    Modulus = 4, 
    Assign = 5, 
    IdentityInequality = 6, 
    IdentityEquality = 7, 
    ValueEquality = 8, 
    BitwiseOr = 9, 
    BitwiseAnd = 10, 
    BooleanOr = 11, 
    BooleanAnd = 12, 
    LessThan = 13, 
    LessThanOrEqual = 14, 
    GreaterThan = 15, 
    GreaterThanOrEqual = 16, 
  }

  public enum CodeRegionMode
  {
    None = 0, 
    Start = 1, 
    End = 2, 
  }

  public enum CodeTypeReferenceOptions
  {
    GlobalReference = 1, 
    GenericTypeParameter = 2, 
  }

  public enum FieldDirection
  {
    In = 0, 
    Out = 1, 
    Ref = 2, 
  }

  public enum MemberAttributes
  {
    Abstract = 1, 
    Final = 2, 
    Static = 3, 
    Override = 4, 
    Const = 5, 
    New = 16, 
    Overloaded = 256, 
    Assembly = 4096, 
    FamilyAndAssembly = 8192, 
    Family = 12288, 
    FamilyOrAssembly = 16384, 
    Private = 20480, 
    Public = 24576, 
    AccessMask = 61440, 
    ScopeMask = 15, 
    VTableMask = 240, 
  }
}
