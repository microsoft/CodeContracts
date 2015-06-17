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
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  /// <summary>
  /// An implementation of Interval sound w.r.t. IEEE floating points
  /// </summary>
  public sealed class Interval_IEEE754 
    : IntervalBase<Interval_IEEE754, Double>
  {
    #region The kind of this Interval_IEEE754
    private enum Kind
    {
      // ** Special cases
      NotInizialized,    // F: This case is not needed in .NET, but we keep it to make easier the sharing of code with the JScriptAI
      Empty,             // lower > upper
      Top,               // lower == -oo, top == +oo
      TopNotNaN,         // any value, but not NaN
      IncludesNaN,       // lower.IsNaN || upper.IsNaN 
      // ** Normal cases
      Mixed,             // lower < 0, upper > 0
      Zero,              // lower == upper == 0
      Positive_Zero,     // lower == 0, upper > 0
      Positive_One,      // lower > 0, upper > 0
      Negative_Zero,     // lower < 0, upper == 0
      Negative_One       // lower < 0, upper < 0
    };

    #endregion

    #region Statics
    public readonly static Interval_IEEE754 NegativeInterval_IEEE754;
    public readonly static Interval_IEEE754 ZeroInterval_IEEE754;
    public readonly static Interval_IEEE754 OneInterval_IEEE754;

    private readonly static Interval_IEEE754 cachedTop, cachedTopNotNaN, cachedBottom, cachedPositiveInterval, cachedNegativeInterval;

    static Interval_IEEE754()
    {
      cachedTop = new Interval_IEEE754(Kind.Top, Double.NegativeInfinity, Double.PositiveInfinity);
      cachedTopNotNaN = new Interval_IEEE754(Kind.TopNotNaN, Double.NegativeInfinity, Double.PositiveInfinity);
      cachedBottom = new Interval_IEEE754(Kind.Empty, 2.78, -2.78);
      cachedPositiveInterval = new Interval_IEEE754(Kind.Positive_Zero, FloatingPointExtensions.NegativeZero, Double.PositiveInfinity);      

      cachedNegativeInterval = NegativeInterval_IEEE754 = new Interval_IEEE754(Kind.Negative_Zero, Double.NegativeInfinity, FloatingPointExtensions.PositiveZero);
      ZeroInterval_IEEE754 = new Interval_IEEE754(Kind.Zero, FloatingPointExtensions.NegativeZero, FloatingPointExtensions.PositiveZero);
      OneInterval_IEEE754 = new Interval_IEEE754(Kind.Positive_One, 1.0);
    }
    #endregion

    #region Static

    public static Interval_IEEE754 UnknownInterval
    {
      get
      {
        Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

        // It will become a postcondition
        Contract.Assert(cachedTop != null);
        return cachedTop;
      }
    }

    public static Interval_IEEE754 UnknownIntervalNotNaN
    {
      get
      {
        Contract.Ensures(Contract.Result<Interval_IEEE754 >() != null);

        return cachedTopNotNaN;
      }
    }

    public static Interval_IEEE754 UnreachedInterval
    {
      get
      {
        Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

        Contract.Assert(cachedBottom != null);

        return cachedBottom;
      }
    }

    /// <summary>
    /// The interval [0, +oo]
    /// </summary>
    public static Interval_IEEE754 PositiveInterval
    {
      get
      {
        Contract.Ensures(Contract.Result<Interval_IEEE754 >() != null);

        Contract.Assert(cachedPositiveInterval != null);
        return cachedPositiveInterval;
      }
    }

    public static Interval_IEEE754 NegativeInterval
    {
      get
      {
        Contract.Ensures(Contract.Result<Interval_IEEE754 >() != null);

        return cachedNegativeInterval;
      }
    }
    #endregion

    #region Private state

    readonly private Kind kind;

    #endregion

    #region Constructors

    private Interval_IEEE754(Kind kind, Double lower, Double upper)
      : base(lower, upper)
    {
      Contract.Assume(IsConsistent(kind, lower, upper));

      this.kind = kind;
     // this.lowerBound = lower;
     // this.upperBound = upper;
    }

    private Interval_IEEE754(Kind kind, Double constant)
      : base(constant, constant)
    {
      // As it is a constant, we expect the most precise Kind here
      Contract.Assume(IsConsistent(kind, constant));

      this.kind = kind;

      switch (this.kind)
      {
        case Kind.Empty:
          this.lowerBound = 45.0;
          this.upperBound = -44.0;
          break;

        case Kind.IncludesNaN:
        case Kind.Mixed:
          this.lowerBound = this.upperBound = constant;
          break;

        case Kind.Negative_One:
        case Kind.Negative_Zero:
        case Kind.Positive_One:
        case Kind.Positive_Zero:
          this.lowerBound = constant;
          this.upperBound = constant;
          break;

        case Kind.NotInizialized:
          this.lowerBound = this.upperBound = default(Double);
          break;

        case Kind.Top:
        case Kind.TopNotNaN:
          this.lowerBound = Double.NegativeInfinity;
          this.upperBound = Double.PositiveInfinity;
          break;

        case Kind.Zero:
          this.lowerBound = FloatingPointExtensions.NegativeZero;
          this.upperBound = FloatingPointExtensions.PositiveZero;
          break;

        default:
          ShouldBeUnreachable<int>();
          break;
      }
    }

    /// <summary>
    /// The Interval_IEEE754 standing for a value that is not initialized.
    /// This is a special case, and we want to call it just with false
    /// </summary>
    /// <param name="initialized">Should be false!</param>
    private Interval_IEEE754(bool initialized)
      : base(Double.NegativeInfinity, Double.PositiveInfinity)
    {
      Contract.Requires(!initialized);

      this.kind = !initialized ? Kind.NotInizialized : Kind.Top;
    }

    #endregion

    #region Construct an Interval_IEEE754 for Int* and UInt* types
    /// <summary>
    /// Construct an Interval_IEEE754 for an Int* type
    /// </summary>
    static public Interval_IEEE754 For(Int64 lower, Int64 upper)
    {
      #region Case 0: Check for constants
      if (lower == upper)
      {
        // TODO F: check for Int64 corner cases 

        var k = lower;
        if (k == 0)
        {
          return new Interval_IEEE754(Kind.Zero, k);
        }
        else if (k > 0)
        {
          return new Interval_IEEE754(Kind.Positive_One, k);
        }
        else if (k < 0)
        {
          return new Interval_IEEE754(Kind.Negative_One, k);
        }

        return ShouldBeUnreachable<Interval_IEEE754>();
      }
      #endregion

      #region Case 1 : Check for Emptyness
      if (lower > upper)
      {
        return new Interval_IEEE754(Kind.Empty, +3.14, -3.14);
      }
      #endregion

      #region Case 2 : Check for Top

      if (lower == Int64.MinValue && upper == Int64.MaxValue)
      {
        return new Interval_IEEE754(Kind.Empty, Double.NegativeInfinity, Double.PositiveInfinity);
      }
      #endregion

      #region Case 3 : The other cases
      var sign_lower = Math.Sign(lower);
      var sign_upper = Math.Sign(upper);

      switch (sign_lower)
      {
        // lower < 0
        case -1:
          switch (sign_upper)
          {
            // upper < 0
            case -1:
              return new Interval_IEEE754(Kind.Negative_One, lower, upper);

            // upper == 0
            case 0:
              return new Interval_IEEE754(Kind.Negative_Zero, lower, upper);

            // upper > 0
            case +1:
              return new Interval_IEEE754(Kind.Mixed, lower, upper);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        // lower == 0
        case 0:
          switch (sign_upper)
          {
            // upper < 0
            case -1:
              return new Interval_IEEE754(Kind.Empty, lower, upper);

            // upper == 0
            case 0:
              return new Interval_IEEE754(Kind.Zero, 0.0); // We set 0-

            // upper > 0
            case +1:
              return new Interval_IEEE754(Kind.Positive_Zero, lower, upper);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        // lower > 0
        case +1:
          switch (sign_upper)
          {
            // upper < 0
            // upper == 0
            case -1:
            case 0:
              return new Interval_IEEE754(Kind.Empty, lower, upper);

            // upper > 0
            case +1:
              return new Interval_IEEE754(Kind.Positive_One, lower, upper);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        default:
          return ShouldBeUnreachable<Interval_IEEE754>();
      }


      #endregion
    }

    static public Interval_IEEE754 For(Int64 r)
    {
      return For((Int64)r, (Int64)r);
    }

    static public Interval_IEEE754 For(Byte b)
    {
      // We know we have no problems in representing bytes
      return For((Int64)b, (Int64)b);
    }

    static public Interval_IEEE754 For(SByte sByte)
    {
      // We know we have no problems in representing bytes
      return For((Int64)sByte, (Int64)sByte);
    }

    static public Interval_IEEE754 For(UInt16 u16)
    {
      // We know we have no problems in representing uint16
      return For((Int64)u16, (Int64)u16);
    }

    static public Interval_IEEE754 For(UInt32 u32)
    {
      // We know we have no problems in representing uint32
      return For((Int64)u32, (Int64)u32);
    }

    static public Interval_IEEE754 For(Int16 i16)
    {
      // We know we have no problems in representing int16
      return For((Int64)i16, (Int64)i16);
    }

    static public Interval_IEEE754 For(Int32 i32)
    {
      // We know we have no problems in representing int32
      return For((Int64)i32, (Int64)i32);
    }
    #endregion

    #region Construct an Interval_IEEE754 from two Doubles
    static public Interval_IEEE754 For(Double lower, Double upper)
    {
      #region Case 0: Check for constants
      if (lower.Equals(upper))
      {
        // TODO F: check for Int64 corner cases 

        var k = lower;

        if (Double.IsNaN(k))
        {
          return new Interval_IEEE754(Kind.IncludesNaN, k);
        }
        if (k == 0)
        {
          return new Interval_IEEE754(Kind.Zero, k);
        }
        if (k > 0)
        {
          return new Interval_IEEE754(Kind.Positive_One, k);
        }
        if (k < 0)
        {
          return new Interval_IEEE754(Kind.Negative_One, k);
        }

        return ShouldBeUnreachable<Interval_IEEE754>();
      }
      #endregion

      #region Case 1 : One the two operands is NaN
      if (Double.IsNaN(lower) || Double.IsNaN(upper))
      {
        return new Interval_IEEE754(Kind.IncludesNaN, lower, upper);
      }
      #endregion

      #region Case 2 : Check for Emptyness
      if (lower > upper)
      {
        return new Interval_IEEE754(Kind.Empty, +3.14, -3.14);
      }
      #endregion

      #region Case 3 : Check for Top

      if (lower == Double.NegativeInfinity && upper == Double.PositiveInfinity)
      {
        return new Interval_IEEE754(Kind.Top, Double.NegativeInfinity, Double.PositiveInfinity);
      }
      #endregion

      #region Case 3 : The other cases
      var sign_lower = Math.Sign(lower);
      var sign_upper = Math.Sign(upper);

      switch (sign_lower)
      {
        // lower < 0
        case -1:
          switch (sign_upper)
          {
            // upper < 0
            case -1:
              return new Interval_IEEE754(Kind.Negative_One, lower, upper);

            // upper == 0
            case 0:
              return new Interval_IEEE754(Kind.Negative_Zero, lower, upper);

            // upper > 0
            case +1:
              return new Interval_IEEE754(Kind.Mixed, lower, upper);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        // lower == 0
        case 0:
          switch (sign_upper)
          {
            // upper < 0
            case -1:
              return new Interval_IEEE754(Kind.Empty, lower, upper);

            // upper == 0
            case 0:
              return new Interval_IEEE754(Kind.Zero, upper);

            // upper > 0
            case +1:
              return new Interval_IEEE754(Kind.Positive_Zero, lower, upper);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        // lower > 0
        case +1:
          switch (sign_upper)
          {
            // upper < 0
            // upper == 0
            case -1:
            case 0:
              return new Interval_IEEE754(Kind.Empty, lower, upper);

            // upper > 0
            case +1:
              return new Interval_IEEE754(Kind.Positive_One, lower, upper);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        default:
          return ShouldBeUnreachable<Interval_IEEE754>();
      }

      #endregion
    }

    static public Interval_IEEE754 For(Double d)
    {
      return For(d, d);
    }

    static public Interval_IEEE754 For(Single s)
    {
      return For((Double)s, (Double)s);
    }

    #endregion

    #region IAbstractDomain Members

    override public bool IsTop
    {
      get { return this.kind == Kind.Top; }
    }

    public bool IsTopButNotNaN
    {
      get { return this.kind == Kind.TopNotNaN; }
    }

    override public bool IsBottom
    {
      get { return this.kind == Kind.Empty; }
    }

    public override Interval_IEEE754 Bottom
    {
      get { return Interval_IEEE754.cachedBottom;  }
    }

    public override Interval_IEEE754 Top
    {
      get { return Interval_IEEE754.cachedTop; }
    }

    override public bool LessEqual(Interval_IEEE754 other)
    {
      if (AreEqual(this, other))
        return true;

      if (this.kind == Kind.Empty)
      {
        return true;
      }

      switch (other.kind)
      {
        case Kind.Empty:
        case Kind.IncludesNaN:
          return false;

        case Kind.NotInizialized:
          return this.kind == Kind.NotInizialized;

        case Kind.Top:
          return true;

        case Kind.TopNotNaN:
          return this.kind != Kind.IncludesNaN && this.kind != Kind.Top;
      }

      // At this point we know other != Empty, IncludedNaN, TopNotNaN, Top
      switch (this.kind)
      {
        case Kind.IncludesNaN:
          return false;

        case Kind.Mixed:
        case Kind.Negative_One:
        case Kind.Negative_Zero:
        case Kind.NotInizialized:
        case Kind.Positive_One:
        case Kind.Positive_Zero:
          return this.lowerBound >= other.lowerBound && this.upperBound <= other.upperBound;

        case Kind.Top:
          return false;

        case Kind.TopNotNaN:
          Contract.Assert(other.kind != Kind.Top);
          return other.kind != Kind.IncludesNaN;
 
        case Kind.Zero:
          return other.lowerBound <= 0;

        default:
          return ShouldBeUnreachable<bool>();
      }
    }

    static private bool AreEqual(Interval_IEEE754 left, Interval_IEEE754 right)
    {
      if (object.ReferenceEquals(left, right))
      {
        return true;
      }

      if (left.kind == right.kind && left.lowerBound.Equals(right.lowerBound) && left.upperBound.Equals(right.upperBound))
      {
        return true;
      }

      return false;
    }

    override public Interval_IEEE754 Join(Interval_IEEE754 other)
    {
      Interval_IEEE754 value;
      if (IsSpecialCaseForJoin(this, other, out value))
      {
        return value;
      }

      var low = Math.Min(this.lowerBound, other.lowerBound);
      var upp = Math.Max(this.upperBound, other.upperBound);

      return new Interval_IEEE754(InferKind(low, upp), low, upp);
    }


    override public Interval_IEEE754 Meet(Interval_IEEE754 other)
    {
      Interval_IEEE754 value;
      if (IsSpecialCaseForMeet(this, other, out value))
      {
        return value;
      }

      var low = Math.Max(this.lowerBound, other.lowerBound);
      var upp = Math.Min(this.upperBound, other.upperBound);

      return new Interval_IEEE754(InferKind(low, upp), low, upp);
    }

    override public Interval_IEEE754 Widening(Interval_IEEE754 prev)
    {
      Interval_IEEE754 value;
      if (IsSpecialCaseForWidening(this, prev, out value))
      {
        return value;
      }

      var low = this.lowerBound < prev.lowerBound ? ThresholdDB.GetPrevious(this.lowerBound) : prev.lowerBound;
      var upp = this.upperBound > prev.upperBound ? ThresholdDB.GetNext(this.upperBound) : prev.upperBound;

      return new Interval_IEEE754(InferKind(low, upp), low, upp);
    }

    override public Interval_IEEE754 DuplicateMe()
    {
      return new Interval_IEEE754(this.kind, this.lowerBound, this.upperBound);
    }

    #endregion

    #region Common Tools

    public bool IsNotNaN
    {
      get
      {
        switch (this.kind)
        {
          case Kind.Empty:
          case Kind.IncludesNaN:
          case Kind.NotInizialized:
          case Kind.Top:
            return false;

          case Kind.TopNotNaN:
          case Kind.Mixed:
          case Kind.Negative_One:
          case Kind.Negative_Zero:
          case Kind.Positive_One:
          case Kind.Positive_Zero:
          case Kind.Zero:
            return true;

          default:
            return false;
        }
      }
    }

    public override bool IsNormal
    {
      get 
      {
        switch (this.kind)
        {
          case Kind.Empty:
          case Kind.IncludesNaN:
          case Kind.NotInizialized:
          case Kind.Top:
          case Kind.TopNotNaN:
            return false;

          case Kind.Mixed:
          case Kind.Negative_One:
          case Kind.Negative_Zero:
          case Kind.Positive_One:
          case Kind.Positive_Zero:
          case Kind.Zero:
            return this.lowerBound <= this.upperBound;

          default:
            return ShouldBeUnreachable<bool>(this.kind.ToString());
        }
      }
    }

    override public bool IsLowerBoundMinusInfinity
    {
      get
      {
        return Double.IsNegativeInfinity(this.lowerBound);
      }
    }

    override public bool IsUpperBoundPlusInfinity
    {
      get
      {
        return Double.IsPositiveInfinity(this.upperBound);
      }
    }

    /// <returns>
    /// true iff <code>x</code> is not included in this Interval_IEEE754
    /// </returns>
    [ContractVerification(true)]
    [Pure]
    public bool DoesNotInclude(double x)
    {
      switch (this.kind)
      {
        case Kind.Empty:
        case Kind.IncludesNaN:
        case Kind.NotInizialized:
        case Kind.Top:
          return false;

        case Kind.TopNotNaN:
          return !double.IsNaN(x);

        case Kind.Mixed:
        case Kind.Negative_One:
        case Kind.Negative_Zero:
        case Kind.Positive_One:
        case Kind.Positive_Zero:
        case Kind.Zero:
          return x < this.lowerBound || x > this.upperBound;

        default:
          return ShouldBeUnreachable<bool>();
      }
    }

    #endregion

    #region Arithmetic Operations over Interval_IEEE754s

    [ContractVerification(true)]
    [Pure]
    static public Interval_IEEE754 operator +(Interval_IEEE754 left, Interval_IEEE754 right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      Interval_IEEE754 value;
      if (PropagateSpecialCases(left, right, out value))
      {
        return value;
      }

      if (left.IsTopButNotNaN || right.IsTopButNotNaN)
      {
        // We can get e.g. +oo + -oo
        return UnknownInterval;
      }

      var low = FloatingPointExtensions.Add_Low(left.lowerBound, right.lowerBound);
      var upp = FloatingPointExtensions.Add_Up(left.upperBound, right.upperBound);

      return new Interval_IEEE754(InferKind(low, upp), low, upp);
    }

    [ContractVerification(true)]
    [Pure]
    static public Interval_IEEE754 operator -(Interval_IEEE754 left, Interval_IEEE754 right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      Interval_IEEE754 value;
      if (PropagateSpecialCases(left, right, out value))
      {
        return value;
      }

      // oo - oo      
      if (Double.IsInfinity(left.UpperBound) && Double.IsInfinity(right.UpperBound))
      {
        // It can be NaN
        return UnknownInterval;
      }

      // -oo + oo
      if(Double.IsInfinity(left.lowerBound) && Double.IsInfinity(right.lowerBound))
      {
        // It can be NaN
        return UnknownInterval;
      }

      var low = FloatingPointExtensions.Sub_Low(left.lowerBound, right.upperBound);
      var upp = FloatingPointExtensions.Sub_Up(left.upperBound, right.lowerBound);

        // To handle cases a 0 - inf
        if (upp < low)
        {
          var tmp = upp;
          upp = low;
          low = tmp;
        }
        return new Interval_IEEE754(InferKind(low, upp), low, upp);
      
    }

    [ContractVerification(false)]
    [Pure]
    static public Interval_IEEE754 operator *(Interval_IEEE754 left, Interval_IEEE754 right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      Interval_IEEE754 value;
      if (PropagateSpecialCases(left, right, out value))
      {
        return value;
      }

      if (left.kind == Kind.IncludesNaN || right.kind == Kind.IncludesNaN)
      {
        return new Interval_IEEE754(Kind.IncludesNaN, Double.NaN);
      }

      if (left.kind == Kind.Zero)
      {
        return left;
      }

      if (right.kind == Kind.Zero)
      {
        return right;
      }

      if (left.IsTopButNotNaN || right.IsTopButNotNaN)
      {
        // it can be Nan. We should check for infinities
        return UnknownInterval;
      }

      Double low, upp;

      var A = left.lowerBound;
      var B = left.upperBound;
      var C = right.lowerBound;
      var D = right.upperBound;

      switch (left.kind)
      {
        #region All the normal cases
        case Kind.Mixed:
          switch (right.kind)
          {
            case Kind.Negative_One:
            case Kind.Negative_Zero:
              low = FloatingPointExtensions.Mul_Low(B, C);
              upp = FloatingPointExtensions.Mul_Up(A, C);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            case Kind.Mixed:
              low = Math.Min(FloatingPointExtensions.Mul_Low(A, D), FloatingPointExtensions.Mul_Low(B, C));
              upp = Math.Max(FloatingPointExtensions.Mul_Up(B, D), FloatingPointExtensions.Mul_Up(A, C));

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            case Kind.Positive_One:
            case Kind.Positive_Zero:
              low = FloatingPointExtensions.Mul_Low(A, D);
              upp = FloatingPointExtensions.Mul_Up(B, D);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        case Kind.Negative_One:
        case Kind.Negative_Zero:
          switch (right.kind)
          {
            case Kind.Negative_One:
            case Kind.Negative_Zero:
              low = FloatingPointExtensions.Mul_Low(B, D);
              upp = FloatingPointExtensions.Mul_Up(A, C);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            case Kind.Mixed:
              low = FloatingPointExtensions.Mul_Low(A, D);
              upp = FloatingPointExtensions.Mul_Up(A, C);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            case Kind.Positive_One:
            case Kind.Positive_Zero:
              low = FloatingPointExtensions.Mul_Low(A, D);
              upp = FloatingPointExtensions.Mul_Up(B, C);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        case Kind.Positive_One:
        case Kind.Positive_Zero:
          switch (right.kind)
          {
            case Kind.Negative_One:
            case Kind.Negative_Zero:
              low = FloatingPointExtensions.Mul_Low(B, C);
              upp = FloatingPointExtensions.Mul_Up(A, D);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);


            case Kind.Mixed:
              low = FloatingPointExtensions.Mul_Low(B, C);
              upp = FloatingPointExtensions.Mul_Up(B, D);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            case Kind.Positive_One:
            case Kind.Positive_Zero:
              low = FloatingPointExtensions.Mul_Low(A, C);
              upp = FloatingPointExtensions.Mul_Up(B, D);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        case Kind.Zero:
        case Kind.Top:
        case Kind.NotInizialized:
        case Kind.Empty:
          return ShouldBeUnreachable<Interval_IEEE754>("this case is already handled");

        default:
          return ShouldBeUnreachable<Interval_IEEE754>("unknown case");
        #endregion
      }
    }

    [ContractVerification(false)]
    [Pure]
    static public Interval_IEEE754 operator /(Interval_IEEE754 left, Interval_IEEE754 right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      Interval_IEEE754 value;
      if (PropagateSpecialCases(left, right, out value))
      {
        return value;
      }

      if (left.kind == Kind.IncludesNaN || right.kind == Kind.IncludesNaN)
      {
        return new Interval_IEEE754(Kind.IncludesNaN, Double.NaN, Double.NaN);
      }

      if (right.kind == Kind.Zero)
      {
        return new Interval_IEEE754(Kind.IncludesNaN, Double.NaN);
      }

      if (left.kind == Kind.Zero)
      {
        return left;
      }

      if (left.IsTopButNotNaN || right.IsTopButNotNaN)
      {
        // it can be NaN
        return UnknownInterval;
      }

      var A = left.lowerBound;
      var B = left.upperBound;
      var C = right.lowerBound;
      var D = right.upperBound;

      double low, upp;

      double tmp1, tmp2;

      switch (left.kind)
      {
        case Kind.Mixed:
          switch (right.kind)
          {
            case Kind.Negative_One:
            case Kind.Negative_Zero:
              low = FloatingPointExtensions.Div_Low(B, D);
              upp = FloatingPointExtensions.Div_Up(A, D);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            case Kind.Mixed:
              return UnknownInterval;

            case Kind.Positive_One:
            case Kind.Positive_Zero:
              low = FloatingPointExtensions.Div_Low(A, C);
              upp = FloatingPointExtensions.Div_Up(B, C);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }


        case Kind.Negative_One:
          switch (right.kind)
          {
            case Kind.Negative_One:
            case Kind.Negative_Zero:
              low = FloatingPointExtensions.Div_Low(B, C);
              upp = FloatingPointExtensions.Div_Up(A, D);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            case Kind.Mixed:
              tmp1 = FloatingPointExtensions.Div_Up(B, D);
              tmp2 = FloatingPointExtensions.Div_Low(B, C);

              return new Interval_IEEE754(InferKind(Double.NegativeInfinity, tmp1), Double.NegativeInfinity, tmp1).Join
                (new Interval_IEEE754(InferKind(tmp2, Double.PositiveInfinity), tmp2, Double.PositiveInfinity)); // - { 0} 

            case Kind.Positive_One:
            case Kind.Positive_Zero:
              low = FloatingPointExtensions.Div_Low(A, C);
              upp = FloatingPointExtensions.Div_Up(B, D);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        case Kind.Negative_Zero:
          switch (right.kind)
          {
            case Kind.Negative_One:
            case Kind.Negative_Zero:
              low = FloatingPointExtensions.PositiveZero;
              upp = FloatingPointExtensions.Div_Up(A, D);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            case Kind.Mixed:
              return UnknownInterval;

            case Kind.Positive_One:
            case Kind.Positive_Zero:
              low = FloatingPointExtensions.Div_Low(A, C);
              upp = FloatingPointExtensions.NegativeZero;

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }


        case Kind.Positive_One:
          switch (right.kind)
          {
            case Kind.Negative_One:
            case Kind.Negative_Zero:
              low = FloatingPointExtensions.Div_Low(B, D);
              upp = FloatingPointExtensions.Div_Up(A, C);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            case Kind.Mixed:
              tmp1 = FloatingPointExtensions.Div_Up(A, C);
              tmp2 = FloatingPointExtensions.Div_Low(A, D);

              return new Interval_IEEE754(InferKind(Double.NegativeInfinity, tmp1), Double.NegativeInfinity, tmp1).Join(
                new Interval_IEEE754(InferKind(tmp2, Double.PositiveInfinity), tmp2, Double.PositiveInfinity));

            case Kind.Positive_One:
            case Kind.Positive_Zero:
              low = FloatingPointExtensions.Div_Low(A, D);
              upp = FloatingPointExtensions.Div_Up(B, C);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        case Kind.Positive_Zero:

          switch (right.kind)
          {
            case Kind.Negative_One:
            case Kind.Negative_Zero:
              low = FloatingPointExtensions.Div_Low(B, D);
              upp = FloatingPointExtensions.NegativeZero;

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            case Kind.Mixed:
              return UnknownInterval;

            case Kind.Positive_One:
            case Kind.Positive_Zero:
              low = FloatingPointExtensions.PositiveZero;
              upp = FloatingPointExtensions.Div_Up(B, C);

              return new Interval_IEEE754(InferKind(low, upp), low, upp);

            default:
              return ShouldBeUnreachable<Interval_IEEE754>();
          }

        case Kind.Zero:
        case Kind.NotInizialized:
        case Kind.Empty:
        case Kind.IncludesNaN:
          return ShouldBeUnreachable<Interval_IEEE754>("this case whould have been already treated");

        default:
          return ShouldBeUnreachable<Interval_IEEE754>("Unknown case?");
      }
    }

    [ContractVerification(true)]
    [Pure]
    static public Interval_IEEE754 operator -(Interval_IEEE754 left)
    {
      Contract.Requires(left != null);
      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      switch (left.kind)
      {
        case Kind.Empty:
        case Kind.NotInizialized:
        case Kind.Top:
        case Kind.TopNotNaN:
        case Kind.Zero:
          return left;

        case Kind.IncludesNaN:
          return UnknownInterval;

        case Kind.Mixed:
          return new Interval_IEEE754(Kind.Mixed, -left.upperBound, -left.lowerBound);

        case Kind.Negative_One:
          return new Interval_IEEE754(Kind.Positive_One, -left.upperBound);

        case Kind.Negative_Zero:
          return PositiveInterval;

        case Kind.Positive_One:
          return new Interval_IEEE754(Kind.Negative_One, -left.lowerBound);

        case Kind.Positive_Zero:
          return NegativeInterval_IEEE754;

        default:
          return ShouldBeUnreachable<Interval_IEEE754>();
      }
    }

    static public Interval_IEEE754 operator &(Interval_IEEE754 left, Interval_IEEE754 right)
    {
      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      return UnknownInterval;
    }

    static public Interval_IEEE754 operator |(Interval_IEEE754 left, Interval_IEEE754 right)
    {
      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      return UnknownInterval;
    }

    static public Interval_IEEE754 operator %(Interval_IEEE754 left, Interval_IEEE754 right)
    {
      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      return UnknownInterval;
    }

    /// <summary>
    /// F: TODO!!!
    /// </summary>
    static public Interval_IEEE754 operator ^(Interval_IEEE754 left, Interval_IEEE754 right)
    {
      return UnknownInterval;
    }

    #endregion

    #region Helpers for arithmetic operations over Interval_IEEE754s

    [ContractVerification(true)]
    [Pure]
    static private bool PropagateSpecialCases(Interval_IEEE754 left, Interval_IEEE754 right, out Interval_IEEE754 value)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);

      // Top is the strongest
      if (left.IsTop || right.IsTop)
      {
        value = left.Top;
        return true;
      }      

      if (left.IsBottom || right.IsBottom)
      {
        value = left.Bottom;
        return true;
      }

      if (left.kind == Kind.NotInizialized || right.kind == Kind.NotInizialized)
      {
        value = new Interval_IEEE754(false);
        return true;
      }

      value = default(Interval_IEEE754);
      return false;
    }

    [ContractVerification(true)]
    [Pure]
    static private bool PropagateSpecialCaseForIsTopButNotNaN(Interval_IEEE754 left, Interval_IEEE754 right, out Interval_IEEE754 value)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);

      if (left.IsTopButNotNaN && right.IsNotNaN)
      {
        value = left;
        return true;
      }

      if (right.IsTopButNotNaN && left.IsNotNaN)
      {
        value = right;
        return true;
      }

      value = default(Interval_IEEE754);
      return false;
    }

    [ContractVerification(true)]
    static private Kind InferKind(double left, double right)
    {
      if (Double.IsNaN(left) || Double.IsNaN(right))
      {
        return Kind.IncludesNaN;
      }

      var sign_left = Math.Sign(left);
      var sign_right = Math.Sign(right);

      switch (sign_left)
      {
        // left < 0
        case -1:
          switch (sign_right)
          {
            // right < 0
            case -1:
              return Kind.Negative_One;

            // right == 0
            case 0:
              return Kind.Negative_Zero;

            // right > 0
            case +1:
              return Kind.Mixed;

            default:
              return ShouldBeUnreachable<Kind>();
          }

        // left == 0
        case 0:
          switch (sign_right)
          {
            // right < 0
            case -1:
              return Kind.Empty;

            // right == 0
            case 0:
              return Kind.Zero;

            // right > 0
            case +1:
              return Kind.Positive_Zero;

            default:
              return ShouldBeUnreachable<Kind>();
          }

        // left > 0
        case +1:
          switch (sign_right)
          {
            // right < 0
            case -1:
            // right == 0
            case 0:
              return Kind.Empty;

            // right > 0
            case +1:
              return Kind.Positive_One;

            default:
              return ShouldBeUnreachable<Kind>();
          }

        default:
          return ShouldBeUnreachable<Kind>();
      }


    }

    [ContractVerification(true)]
    static private bool IsConsistent(Kind kind, double lower, double upper)
    {
      switch (kind)
      {
        case Kind.Empty:
          return lower > upper;

        case Kind.Mixed:
          return lower < upper;

        case Kind.Negative_One:
          return lower < 0 && upper < 0;

        case Kind.Negative_Zero:
          return lower < 0 && (double)upper == 0;

        case Kind.NotInizialized:
          return false;

        case Kind.Positive_One:
          return lower > 0 && upper > 0;

        case Kind.Positive_Zero:
          return lower == 0 && upper > 0;

        case Kind.Top:
        case Kind.TopNotNaN:
          return double.IsNegativeInfinity(lower) && double.IsPositiveInfinity(upper);

        case Kind.Zero:
          return (double)lower == 0 && (double)upper == 0;

        case Kind.IncludesNaN:
          return Double.IsNaN(lower) || Double.IsNaN(upper);

        default:
          Contract.Assert(false);
          return false;   // Impossible case?
      }
    }

    [ContractVerification(true)]
    static private bool IsConsistent(Kind kind, double k)
    {
      switch (kind)
      {
        case Kind.Empty:
          return false;

        case Kind.IncludesNaN:
          return Double.IsNaN(k);

        case Kind.Mixed:
          return false;

        case Kind.Negative_One:
          return k < 0;

        case Kind.Negative_Zero:
          return false;

        case Kind.NotInizialized:
          return false;

        case Kind.Positive_One:
          return k > 0;

        case Kind.Positive_Zero:
          return false;

        case Kind.Top:
        case Kind.TopNotNaN:
          return false;

        case Kind.Zero:
          return (double)k == 0;

        default:
          Contract.Assert(false);
          return false;
      }
    }

    #endregion

    #region Helpers for the operations over the abstract domains
    [ContractVerification(true)]
    [Pure]
    static private bool IsSpecialCaseForJoin(Interval_IEEE754 left, Interval_IEEE754 right, out Interval_IEEE754 value)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn<Interval_IEEE754>(out value) != null);

      if (AreEqual(left, right))
      {
        value = left;
        return true;
      }

      value = default(Interval_IEEE754);

      return IsSpecialCaseForJoinInternal(left, right, ref value) || IsSpecialCaseForJoinInternal(right, left, ref value);
    }

    [ContractVerification(true)]
    [Pure]
    static private bool IsSpecialCaseForJoinInternal(Interval_IEEE754 left, Interval_IEEE754 right, ref Interval_IEEE754 value)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(!Contract.Result<bool>() || value != null);

      switch (left.kind)
      {
        case Kind.Empty:
          value = right;
          return true;

        case Kind.IncludesNaN:
          // We assume at this point that we've already checked that left != right
          value = UnknownInterval;
          return true;

        case Kind.NotInizialized:
          // We assume at this point that we've already checked that left != right
          switch (right.kind)
          {
            case Kind.Mixed:
            case Kind.Negative_One:
            case Kind.Negative_Zero:
            case Kind.Positive_One:
            case Kind.Positive_Zero:
            case Kind.Zero:
            case Kind.Top:
              value = right;
              return true;

            default:
              value = UnknownInterval;
              return true;
          }

        case Kind.Top:
          value = left;
          return true;

        case Kind.TopNotNaN:
          value = right.kind != Kind.IncludesNaN ? UnknownInterval : left;
          return true;
      }

      return false;
    }

    [ContractVerification(true)]
    [Pure]
    static private bool IsSpecialCaseForMeet(Interval_IEEE754 left, Interval_IEEE754 right, out Interval_IEEE754 value)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn<Interval_IEEE754>(out value) != null);

      if (AreEqual(left, right))
      {
        value = left;
        return true;
      }

      value = default(Interval_IEEE754);

      return IsSpecialCaseForMeetInternal(left, right, ref value) || IsSpecialCaseForMeetInternal(right, left, ref value);
    }

    [ContractVerification(true)]
    [Pure]
    static private bool IsSpecialCaseForMeetInternal(Interval_IEEE754 left, Interval_IEEE754 right, ref Interval_IEEE754 value)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn<Interval_IEEE754>(out value) != null);

      switch (left.kind)
      {
        case Kind.Empty:
          value = left;
          return true;

        case Kind.IncludesNaN:
          // We assume at this point that we've already checked that left != right
          value = UnreachedInterval;
          return true;

        case Kind.NotInizialized:
        case Kind.Top:
          value = right;
          return true;

        case Kind.TopNotNaN:
          value = right.kind == Kind.IncludesNaN ? UnreachedInterval : right;
          return true;
      }

      return false;
    }

    [ContractVerification(true)]
    [Pure]
    static private bool IsSpecialCaseForWidening(Interval_IEEE754 left, Interval_IEEE754 prev, out Interval_IEEE754 value)
    {
      Contract.Requires(left != null);
      Contract.Requires(prev != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn<Interval_IEEE754>(out value) != null);

      if (AreEqual(left, prev))
      {
        value = left;
        return true;
      }

      value = default(Interval_IEEE754);

      return IsSpecialCaseForWideningInternal(left, prev, ref value);
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    // [SuppressMessage("Microsoft.Contracts", "UnreachedCode")]
    [ContractVerification(true)]
    [Pure]
    static private bool IsSpecialCaseForWideningInternal(Interval_IEEE754 left, Interval_IEEE754 prev, ref Interval_IEEE754 value)
    {
      Contract.Requires(left != null);
      Contract.Requires(prev != null);
      Contract.Ensures(!Contract.Result<bool>() || value != null);

      switch (prev.kind)
      {
        case Kind.Empty:
          value = left;
          return true;

        case Kind.TopNotNaN:
          if (left.kind != Kind.IncludesNaN)
          {
            value = left;
            return true;
          }
          else
          {
            break;
          }

        case Kind.Top:
          value = prev;
          return true;
      }

      switch (left.kind)
      {
        case Kind.Empty:
          value = prev;
          return true;

        case Kind.IncludesNaN:
          // We assume at this point that we've already checked that left != right
          value = UnknownInterval;
          return true;

        case Kind.NotInizialized:
          // We assume at this point that we've already checked that left != right
          switch (prev.kind)
          {
            case Kind.Mixed:
            case Kind.Negative_One:
            case Kind.Negative_Zero:
            case Kind.Positive_One:
            case Kind.Positive_Zero:
            case Kind.Zero:
            case Kind.Top:
              value = prev;
              return true;

            default:
              value = UnknownInterval;
              return true;
          }

        case Kind.TopNotNaN:
          if(prev.kind == Kind.IncludesNaN)
          {
            value = UnknownInterval;
            return true;
          }
          else
          {
            value = UnknownIntervalNotNaN;
            return true;
          }

        case Kind.Top:
          value = left;
          return true;
      }

      return false;
    }


    #endregion

    #region Routines for fail
    [ContractVerification(false)]
    static private T ShouldBeUnreachable<T>(string why)
    {
      Contract.Requires(false);
      Contract.Ensures(false);

      throw new Exception(why);
    }

    [ContractVerification(false)]
    static private T ShouldBeUnreachable<T>()
    {
      Contract.Requires(false);
      Contract.Ensures(false);

      return ShouldBeUnreachable<T>("Unexpected case!");
    }
    #endregion

    #region Types
    public override bool IsInt32
    {
      get { return false; }
    }

    public override bool IsInt64
    {
      get { return false; }
    }
    #endregion

    #region ToString
    [ContractVerification(true)]
    public override string ToString()
    {
      switch (this.kind)
      {
        case Kind.Empty:
          return "_|_";

        case Kind.IncludesNaN:
        case Kind.Mixed:
        case Kind.Negative_One:
        case Kind.Negative_Zero:
        case Kind.Positive_One:
        case Kind.Positive_Zero:
        case Kind.Zero:
          return "[" + this.lowerBound + ", " + this.upperBound + "]";

        case Kind.NotInizialized:
          return "_()_";

        case Kind.TopNotNaN:
          return "!= NaN";

        case Kind.Top:
          return "Top";

        default:
          return ShouldBeUnreachable<string>("Unknown case");
      }
    }
    #endregion

    public override Interval_IEEE754 ToUnsigned()
    {
      return this;
    }

    internal static Interval_IEEE754 ShiftLeft(Interval_IEEE754 left, Interval_IEEE754 right)
    {
      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      return UnknownInterval;
    }

    internal static Interval_IEEE754 ShiftRight(Interval_IEEE754 left, Interval_IEEE754 right)
    {
      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      return UnknownInterval;
    }


    public static Interval_IEEE754 For(Rational r)
    {
      Contract.Requires(!object.Equals(r, null));
      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      // F: wrong: should be an interval!
      var asDouble = (double)r;
      return For(asDouble);
    }

    [ContractVerification(true)]
    [Pure]
    public static DisInterval ConvertInterval(Interval_IEEE754 intv)
    {
      Contract.Requires(intv != null);
      Contract.Ensures(Contract.Result<DisInterval>() != null);

      switch (intv.kind)
      {
        case Kind.Empty:
          return DisInterval.UnreachedInterval;

        case Kind.IncludesNaN:
          return DisInterval.UnknownInterval;

        case Kind.Mixed:
        case Kind.Negative_One:
        case Kind.Negative_Zero:
        case Kind.Positive_One:
        case Kind.Positive_Zero:
          return Interval.For(intv.lowerBound, intv.upperBound, true).AsDisInterval;        
        
        case Kind.NotInizialized:
          return DisInterval.UnknownInterval;

        case Kind.Top:
        case Kind.TopNotNaN:
          return DisInterval.UnknownInterval;

        case Kind.Zero:
          return DisInterval.For(0);

        default:
          return ShouldBeUnreachable<DisInterval>();
      }
    }

    [ContractVerification(true)]
    [Pure]
    public static Interval_IEEE754 ConvertInterval(Interval intv)
    {
      Contract.Requires(intv != null);

      Contract.Ensures(Contract.Result<Interval_IEEE754>() != null);

      if (intv.IsBottom)
        return cachedBottom;
      if (intv.IsTop)
        return cachedTop;

      var inf = intv.LowerBound.IsMinusInfinity ? Double.NegativeInfinity : intv.LowerBound.Floor;
      var sup = intv.UpperBound.IsPlusInfinity ? Double.PositiveInfinity : intv.UpperBound.Ceiling;

      return new Interval_IEEE754(InferKind(inf, sup), inf, sup);
    }

    #region Refinements

    public Interval_IEEE754 RefineIntervalWithTypeRanges(Int32 min, Int32 max)
    {
      var low = this.LowerBound.IsInfinity() || !this.LowerBound.IsInRange(min, max)
        ? Double.NegativeInfinity
      : Math.Ceiling(this.LowerBound);

      var upp = this.UpperBound.IsInfinity() || !this.UpperBound.IsInRange(min, max)
        ? Double.PositiveInfinity
        : Math.Floor(this.UpperBound);

      return Interval_IEEE754.For(low, upp);
    }

    public Interval_IEEE754 RefineIntervalWithTypeRanges(Int64 min, Int64 max)
    {
      var low = this.LowerBound.IsInfinity() || !this.LowerBound.IsInRange(min, max)
        ? Double.NegativeInfinity
      : Math.Ceiling(this.LowerBound);

      var upp = this.UpperBound.IsInfinity() || !this.UpperBound.IsInRange(min, max)
        ? Double.PositiveInfinity
        : Math.Floor(this.UpperBound);

      return Interval_IEEE754.For(low, upp);
    }

    public Interval_IEEE754 RefineIntervalWithTypeRanges(UInt32 min, UInt32 max)
    {
      var low = this.LowerBound.IsInfinity() || !this.LowerBound.IsInRange(min, max)
        ? Double.NegativeInfinity
        : Math.Ceiling(this.LowerBound);

      var upp = this.UpperBound.IsInfinity() || !this.UpperBound.IsInRange(min, max)
        ? Double.PositiveInfinity
        : Math.Floor(this.UpperBound);

      return Interval_IEEE754.For(low, upp);
    }
    
    #endregion
  }

  static public class FloatingPointExtensions
  {
    static private readonly Double negativeZero = BitConverter.Int64BitsToDouble(-9223372036854775808L);

    static public bool IsInRange(this Double d, Int32 min, Int32 max)
    {
      return min <= d && d <= max;
    }

    static public bool IsInRange(this Double d, Int64 min, Int64 max)
    {
      return min <= d && d <= max;
    }

    static public bool IsInfinity(this Double d)
    {
      return Double.IsInfinity(d);
    }

    static public Double PositiveZero
    {
      get
      {
        return 0.0D;
      }
    }

    static public Double NegativeZero
    {
      get
      {
        return negativeZero;
      }
    }

    static public bool IsZero(this Single s)
    {
      return s == 0.0f;
    }

    static public bool IsZeroPositive(this Single s)
    {
      return s == 0.0f && BitConverter.DoubleToInt64Bits((double)s) == 0;
    }

    static public bool IsZeroNegative(this Single s)
    {
      return s == 0.0f && BitConverter.DoubleToInt64Bits((double)s) < 0;
    }

    static public bool IsZero(this Double d)
    {
      return d == 0.0D;
    }

    static public bool IsZeroPositive(this Double d)
    {
      return d == 0.0D && BitConverter.DoubleToInt64Bits(d) == 0;
    }

    static public bool IsZeroNegative(this Double d)
    {
      return d == 0.0D && BitConverter.DoubleToInt64Bits(d) < 0;
    }

    static public Double Add_Up(Double left, Double right)
    {
      return left + right;
    }

    static public Double Sub_Up(Double left, Double right)
    {
      return left - right;
    }

    static public Double Mul_Up(Double left, Double right)
    {
      return left * right;
    }

    static public Double Div_Up(Double left, Double right)
    {
      return left / right;
    }

    static public Double Add_Low(Double left, Double right)
    {
      return left + right;
    }

    static public Double Sub_Low(Double left, Double right)
    {
      return left - right;
    }

    static public Double Mul_Low(Double left, Double right)
    {
      return left * right;
    }

    static public Double Div_Low(Double left, Double right)
    {
      return left / right;
    }
  }
}