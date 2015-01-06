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
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis 
{


  [ContractClass(typeof(IOutputContracts))]
  public interface IOutput : ISimpleLineWriter
  {
    /// <summary>
    /// For general output that are suggestions to the user
    /// </summary>
    void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo);

    void EmitError(System.CodeDom.Compiler.CompilerError error);

    IFrameworkLogOptions LogOptions { get; }
  }


  #region Contracts for IOutput
  [ContractClassFor(typeof(IOutput))]
  abstract class IOutputContracts : IOutput
  {
    #region ISimpleLineWriter Members

    public void WriteLine(string format, params object[] args)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IOutput Members

    public void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
    }

    public void EmitError(System.CodeDom.Compiler.CompilerError error)
    {
      Contract.Requires(error != null);
    }

    public IFrameworkLogOptions LogOptions
    {
      get 
      {
        Contract.Ensures(Contract.Result<IFrameworkLogOptions>() != null);

        return default(IFrameworkLogOptions);
      }
    }

    #endregion
  }
  #endregion
}
