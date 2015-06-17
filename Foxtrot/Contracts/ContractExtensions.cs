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

//
//  Include this file in your project if your project uses
//  ContractArgumentValidator or ContractAbbreviator methods
//
using System;

namespace System.Diagnostics.Contracts
{
  /// <summary>
  /// Enables factoring legacy if-then-throw into separate methods for reuse and full control over
  /// thrown exception and arguments
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
  [Conditional("CONTRACTS_FULL")]
  internal sealed class ContractArgumentValidatorAttribute : global::System.Attribute
  {
  }

  /// <summary>
  /// Enables writing abbreviations for contracts that get copied to other methods
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
  [Conditional("CONTRACTS_FULL")]
  internal sealed class ContractAbbreviatorAttribute : global::System.Attribute
  {
  }

  /// <summary>
  /// Allows setting contract and tool options at assembly, type, or method granularity.
  /// </summary>
  [AttributeUsage(AttributeTargets.All, AllowMultiple=true, Inherited=false)]
  [Conditional("CONTRACTS_FULL")]
  internal sealed class ContractOptionAttribute : global::System.Attribute
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "category", Justification = "Build-time only attribute")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "setting", Justification = "Build-time only attribute")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "toggle", Justification = "Build-time only attribute")]
    public ContractOptionAttribute(string category, string setting, bool toggle) { }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "category", Justification = "Build-time only attribute")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "setting", Justification = "Build-time only attribute")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "Build-time only attribute")]
    public ContractOptionAttribute(string category, string setting, string value) { }
  }
}

