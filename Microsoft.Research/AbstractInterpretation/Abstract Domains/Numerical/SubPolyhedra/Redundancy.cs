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


namespace Microsoft.Research.AbstractDomains.Numerical
{
  public struct LinearEqualityRedundancy<Variable>
  {
    readonly public Variable V1;
    readonly public Variable V2;
    readonly public Rational Coeff2;
    readonly public Rational Offset;

    readonly public int Row;

    public LinearEqualityRedundancy(Variable v1, Rational coeff2, Variable v2, Rational offset)
      : this(v1, coeff2, v2, offset, -23456)
    {
    }

    public LinearEqualityRedundancy(Variable v1, Rational coeff2, Variable v2, Rational offset, int row)
    {
      this.V1 = v1;
      this.Coeff2 = coeff2;
      this.V2 = v2;
      this.Offset = offset;
      this.Row = row;
    }

    public override string ToString()
    {
      return string.Format("<{0}, {1}, {2}, {3}, row: {4}>", V1, Coeff2, V2, Offset, Row);
    }
  }

}