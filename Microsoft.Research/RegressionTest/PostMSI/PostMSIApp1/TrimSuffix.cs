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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Contracts.Regression;

namespace PostMSI
{
  public static class StringExtensions
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 161)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 89, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 103, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 131, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 139)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 31, MethodILOffset = 139)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 56, MethodILOffset = 139)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 154, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 37, MethodILOffset = 169)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 56, MethodILOffset = 169)]
    public static string TrimSuffix(this string source, string suffix)
    {
      Contract.Requires( source != null );
      Contract.Requires( !String.IsNullOrEmpty(suffix) );
      Contract.Ensures(  Contract.Result<string>() != null );
      Contract.Ensures(  !Contract.Result<string>().EndsWith(suffix) );

      #region Body
      var result = source;
      while (result.EndsWith(suffix))
      {
        var oldLength = result.Length;
        Contract.Assert(result.Length >= suffix.Length);
        Contract.Assert(suffix.Length > 0);
        var remainder = result.Length - suffix.Length;
        Contract.Assert(remainder < result.Length);
        result = result.Substring(0, remainder);
        Contract.Assert(result.Length < oldLength);
      }
      return result;
      #endregion
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 159)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 87, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 129, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 137)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 31, MethodILOffset = 137)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 56, MethodILOffset = 137)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 152, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 35, MethodILOffset = 167)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 54, MethodILOffset = 167)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=101,MethodILOffset=0)]
    public static string TrimSuffixBad(string source, string suffix)
    {
      Contract.Requires(source != null);
      Contract.Requires(suffix != null);

      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(!Contract.Result<string>().EndsWith(suffix));


      var result = source;
      while (result.EndsWith(suffix))
      {
        var oldLength = result.Length;
        Contract.Assert(result.Length >= suffix.Length);
        Contract.Assert(suffix.Length > 0);
        var remainder = result.Length - suffix.Length;
        Contract.Assert(remainder < result.Length);
        result = result.Substring(0, remainder);
        Contract.Assert(result.Length < oldLength);
      }
      return result;
    }

  }
}
