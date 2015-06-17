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

// File System.CodeDom.Compiler.cs
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


namespace System.CodeDom.Compiler
{
  public enum GeneratorSupport
  {
    ArraysOfArrays = 1, 
    EntryPointMethod = 2, 
    GotoStatements = 4, 
    MultidimensionalArrays = 8, 
    StaticConstructors = 16, 
    TryCatchStatements = 32, 
    ReturnTypeAttributes = 64, 
    DeclareValueTypes = 128, 
    DeclareEnums = 256, 
    DeclareDelegates = 512, 
    DeclareInterfaces = 1024, 
    DeclareEvents = 2048, 
    AssemblyAttributes = 4096, 
    ParameterAttributes = 8192, 
    ReferenceParameters = 16384, 
    ChainedConstructorArguments = 32768, 
    NestedTypes = 65536, 
    MultipleInterfaceMembers = 131072, 
    PublicStaticMembers = 262144, 
    ComplexExpressions = 524288, 
    Win32Resources = 1048576, 
    Resources = 2097152, 
    PartialTypes = 4194304, 
    GenericTypeReference = 8388608, 
    GenericTypeDeclaration = 16777216, 
    DeclareIndexerProperties = 33554432, 
  }

  public enum LanguageOptions
  {
    None = 0, 
    CaseInsensitive = 1, 
  }
}
