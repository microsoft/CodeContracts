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

namespace Microsoft.Research.CodeAnalysis {

  public enum ExpressionCacheMode { None, Mem, Time }


  public enum ProofOutcome : byte { Top = 0, Bottom, True, False }

  public static class ProofOutcomeExtensions
  {
    public static ProofOutcome Meet(this ProofOutcome o1, ProofOutcome o2)
    {
      if (o1 == o2) return o1;
      if (o1 == ProofOutcome.Top || o2 == ProofOutcome.Bottom) return o2;
      if (o2 == ProofOutcome.Top || o1 == ProofOutcome.Bottom) return o1;

      // different and none is top or bottom, so meet of true and false
      return ProofOutcome.Bottom;
    }

    public static ProofOutcome Join(this ProofOutcome o1, ProofOutcome o2)
    {
      if (o1 == o2) return o1;
      if (o1 == ProofOutcome.Top || o2 == ProofOutcome.Bottom) return o1;
      if (o2 == ProofOutcome.Top || o1 == ProofOutcome.Bottom) return o2;

      // different and none is top or bottom, so join of true and false
      return ProofOutcome.Top;
    }

    public static bool IsNormal(this ProofOutcome o)
    {
      return o == ProofOutcome.True || o == ProofOutcome.False;
    }

    public static ProofOutcome Negate(this ProofOutcome o)
    {
      switch (o)
      {
        case ProofOutcome.Bottom:
        case ProofOutcome.Top:
          return o;

        case ProofOutcome.False:
          return ProofOutcome.True;

        case ProofOutcome.True:
          return ProofOutcome.False;

        default: 
          return ProofOutcome.Top;
      }
    }
  }
}
