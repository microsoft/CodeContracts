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

namespace CodeContracts.Samples {

  public class Rational {
    public int Numerator { get; protected set; }
    public int Denominator { get; protected set; }

    public Rational(int n, int d) {
      Contract.Requires(d != 0);

      this.Numerator = n;
      this.Denominator = d;
    }

    [ContractInvariantMethod]
    private void RationalInvariant() {
      Contract.Invariant(Denominator != 0);
    }

    public virtual void Add(Rational other) {
      Contract.Requires(other != null);
      int newN = this.Numerator * other.Denominator + other.Numerator * this.Denominator;
      int newD = this.Denominator * other.Denominator;

      this.Numerator = newN;
      this.Denominator = newD;
    }

    public static Rational operator +(Rational a, Rational b) {
      Contract.Requires(a != null);
      Contract.Requires(b != null);
      return new Rational(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
    }
    public static Rational operator +(Rational a, int b) {
      Contract.Requires(a != null);
      return new Rational(a.Numerator + b * a.Denominator, a.Denominator);
    }

    public virtual void Divide(int divisor)
    {
      Contract.Requires<ArgumentOutOfRangeException>(divisor != 0);

      this.Denominator = this.Denominator * divisor;
    }

    public int Truncate()
    {
      return this.Numerator / this.Denominator;
    }

    public virtual void Invert() {
      Contract.Ensures(Contract.OldValue(this.Numerator) == this.Denominator && Contract.OldValue(this.Denominator) == this.Numerator);

      int num = this.Numerator;
      int den = this.Denominator;
      this.Numerator = den;
      this.Denominator = num;
    }

  }

}
