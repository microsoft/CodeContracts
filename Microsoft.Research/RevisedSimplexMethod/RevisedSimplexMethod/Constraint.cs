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

namespace Microsoft.Glee.Optimization
{
  [Serializable]
  internal class Constraint
  {
    internal Constraint() { }

    internal Constraint(double[] coeffs, Relation relation, double rightSide)
    {
      this.coeffs = coeffs;
      this.relation = relation;
      this.rightSide = rightSide;
    }

    readonly internal double[] coeffs;
    readonly internal Relation relation;
    internal double rightSide;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Double.ToString"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1818:DoNotConcatenateStringsInsideLoops")]
    public override string ToString()
    {
      string r = "";
      for (int i = 0; i < coeffs.Length; i++)
      {
        r += coeffs[i];
        if (i < coeffs.Length - 1)
          r += ",";
        else r += " ";
      }

      r += relation.ToString();
      r += " " + rightSide.ToString();
      return r;


    }
  }
}
