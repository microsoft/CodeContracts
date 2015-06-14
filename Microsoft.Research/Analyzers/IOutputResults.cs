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
using System.Diagnostics.Contracts;
using System.Text;

namespace Microsoft.Research.CodeAnalysis {

  public enum WarningKind
  {
    Nonnull,
    MinValueNegation,
    DivByZero,
    ArithmeticOverflow,
    FloatEqualityPrecisionMismatch,
    ArrayCreation,
    ArrayLowerBound,
    ArrayUpperBound,
    Unsafe,
    Requires,
    Ensures,
    Invariant,
    Assume,
    Assert,
    Purity,
    Informational,
    UnreachedCode,
    Enum,
    MissingPrecondition,
    Suggestion
  }

  [ContractClass(typeof(IOutputResultsContracts))]
  public interface IOutputResults : IOutput 
  {
    /// <returns>false if the witness has been filtered</returns>
    bool EmitOutcome(Witness witness, string format, params object[] args);
    // special case for baselining
    //bool EmitOutcome(Witness witness, string format, byte[] hash, params object[] args);

    /// <returns>false if the witness has been filtered</returns>
    bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args);

    void Statistic(string format, params object[] args);

    /// <summary>
    /// Print final stats. This is separate, so in VS we can pump it as a message
    /// </summary>
    void FinalStatistic(string assemblyName, string message);

    /// <summary>
    /// Called once at end to make sure any output files are closed.
    /// </summary>
    void Close();

    /// <summary>If this is a regression output, it returns the number of regressions.</summary>
    int RegressionErrors();

    bool ErrorsWereEmitted { get; }

    /// <returns>
    /// True if the particular witness is masked 
    /// </returns>
    bool IsMasked(Witness witness);

    new ILogOptions LogOptions { get; }
  }

  #region Contracts for IOutputResults
  [ContractClassFor(typeof(IOutputResults))]
  abstract class IOutputResultsContracts : IOutputResults
  {
    #region IOutputResults Members

    public bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      Contract.Requires(witness != null);
      Contract.Requires(format != null);
    
      return false;
    }

    public bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      Contract.Requires(witness != null);
      Contract.Requires(format != null);

      return false;
    }

    public void Statistic(string format, params object[] args)
    {
      Contract.Requires(format != null);
    }

    public void FinalStatistic(string assemblyName, string message)
    {
      Contract.Requires(assemblyName != null);
    }

    public bool IsMasked(Witness witness)
    {
      Contract.Requires(witness != null);

      return false;
    }


    public void Close()
    {
    }

    public int RegressionErrors()
    {
      return default(int);
    }

    public ILogOptions LogOptions
    {
      get 
      {
        Contract.Ensures(Contract.Result<ILogOptions>() != null);

        return default(ILogOptions);
      }
    }

    #endregion

    #region IOutput Members - No contracts on them

    public void WriteLine(string format, params object[] args)
    {
      throw new NotImplementedException();
    }

    public void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      throw new NotImplementedException();
    }

    public void EmitError(System.CodeDom.Compiler.CompilerError error)
    {
      throw new NotImplementedException();
    }

    IFrameworkLogOptions IOutput.LogOptions
    {
      get { throw new NotImplementedException(); }
    }

    public bool ErrorsWereEmitted
    {
      get { throw new NotImplementedException(); }
    }
    
    #endregion


  }
  #endregion

}
