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
using System.Text;
using Microsoft.Research.ClousotRegression;


namespace Microsoft.Research.ClousotRegression
{
  public enum ProofOutcome { Top = 0, Bottom, True, False }


  [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
  public class ClousotRegressionTestAttribute : Attribute
  {
    /// <summary>
    /// Default, valid for all configurations
    /// </summary>
    public ClousotRegressionTestAttribute()
    {
    }

    /// <summary>
    /// Valid when conditional symbol is defined for regression run
    /// </summary>
    public ClousotRegressionTestAttribute(string conditionalSymbol)
    {
    }
  }

  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = true)]
  public class RegressionOutcomeAttribute : Attribute
  {
    /// <summary>What outcome the proof obligation has</summary>
    public ProofOutcome Outcome { get; set; }
    /// <summary>The associated message</summary>
    public string Message { get; set; }
    /// <summary>The primary PC IL offset</summary>
    public int PrimaryILOffset { get; set; }
    /// <summary>The IL offset within this method</summary>
    public int MethodILOffset { get; set; }

    /// <summary>
    /// Stores outcomes on a method.
    /// </summary>
    public RegressionOutcomeAttribute()
    {
    }

    /// <summary>
    /// Stores global error outcomes on assembly
    /// </summary>
    /// <param name="expectedMessage"></param>
    public RegressionOutcomeAttribute(string expectedMessage)
    {
    }

  }

  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = true)]
  public class RegressionReanalysisCountAttribute : Attribute
  {

    public int Count { get; set; }
    
    /// <summary>
    /// Stores outcomes on a method.
    /// </summary>
    public RegressionReanalysisCountAttribute()
    {
      this.Count = 0;
    }

    /// <summary>
    /// Stores global error outcomes on assembly
    /// </summary>
    /// <param name="expectedMessage"></param>
    public RegressionReanalysisCountAttribute(int count)
    {
      this.Count = count;
    }

  }
}
