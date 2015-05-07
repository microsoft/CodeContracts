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

// File System.Math.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System
{
  static public partial class Math
  {
    public const double PI = 3.1415926535897931;


    #region Methods and constructors

    [Pure]
    public static int Abs(int value)
    {
      // F: -2147483648 == Int32.MinValue, but here I do not have a name for it
      Contract.Requires(value != -2147483648);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures((value - Contract.Result<int>()) <= 0);

      return default(int);
    }

    [Pure]
    public static long Abs(long value)
    {
      Contract.Requires(value != -9223372036854775808L);
      Contract.Ensures(Contract.Result<long>() >= 0);
      Contract.Ensures((value - Contract.Result<long>()) <= 0);

      return default(long);
    }

    [Pure]
    public static sbyte Abs(sbyte value)
    {
      Contract.Requires(value != -128);
      Contract.Ensures(Contract.Result<SByte>() >= 0);
      Contract.Ensures((value - Contract.Result<sbyte>()) <= 0);

      return default(sbyte);
    }

    [Pure]
    public static short Abs(short value)
    {
      Contract.Requires(value != -32768);
      Contract.Ensures(Contract.Result<short>() >= 0);
      Contract.Ensures((value - Contract.Result<short>()) <= 0);

      return default(short);
    }

    [Pure]
    public static Decimal Abs(Decimal value)
    {
      return default(Decimal);
    }

    [Pure]
    public static float Abs(float value)
    {
      Contract.Ensures((Contract.Result<float>() >= 0.0) || float.IsNaN(Contract.Result<float>()) || float.IsPositiveInfinity(Contract.Result<float>()));

      // 2015-03-26: tom-englert
      // Disabled, since this was too complex for the checker to understand.
      // e.g. new Rect(0, 0, Math.Abs(a), Math.Abs(b)) raised a warning that with and height are unproven to be positive values.

      // !NaN ==>  >= 0
      // Contract.Ensures(float.IsNaN(value) || Contract.Result<float>() >= 0.0);

      // NaN ==> NaN
      // Contract.Ensures(!float.IsNaN(value) || float.IsNaN(Contract.Result<float>()));

      // Infty ==> +Infty
      // Contract.Ensures(!float.IsInfinity(value) || float.IsPositiveInfinity(Contract.Result<float>()));

      return default(float);
    }

    [Pure]
    public static double Abs(double value)
    {
      Contract.Ensures((Contract.Result<double>() >= 0.0) || double.IsNaN(Contract.Result<double>()) || double.IsPositiveInfinity(Contract.Result<double>()));

      // 2015-03-26: tom-englert
      // Disabled, since this was too complex for the checker to understand.
      // e.g. new Rect(0, 0, Math.Abs(a), Math.Abs(b)) raised a warning that with and height are unproven to be positive values.

      // !NaN ==>  >= 0
      // Contract.Ensures(double.IsNaN(value) || Contract.Result<double>() >= 0.0);

      // NaN ==> NaN
      // Contract.Ensures(!double.IsNaN(value) || double.IsNaN(Contract.Result<double>()));
      // Infty ==> +Infty
      // Contract.Ensures(!double.IsInfinity(value) || double.IsPositiveInfinity(Contract.Result<double>()));

      return default(double);
    }

    [Pure]
    public static double Acos(double d)
    {
      // -1 <= d <= 1 ==> 0<= result <= Pi
      Contract.Ensures(!(-1 <= d &&  d <= 1) || Contract.Result<double>() >= 0.0);
      Contract.Ensures(!(-1 <= d && d <= 1) || Contract.Result<double>() <= Math.PI);

      // d < -1 || d > 1 ==> NaN
      Contract.Ensures(!(d < -1 || d > 1) || Double.IsNaN(Contract.Result<double>()));

      // d == NaN ==> NaN
      Contract.Ensures(!Double.IsNaN(d) || Double.IsNaN(Contract.Result<double>()));

      return default(double);
    }

    [Pure]
    public static double Asin(double d)
    {
      // -1 <= d <= 1 ==> -Pi/2<= result <= Pi/2
      Contract.Ensures(!(-1 <= d && d <= 1) || Contract.Result<double>() >= -Math.PI/2);
      Contract.Ensures(!(-1 <= d && d <= 1) || Contract.Result<double>() <= Math.PI/2);

      // d < -1 || d > 1 ==> NaN
      Contract.Ensures(!(d < -1 || d > 1) || Double.IsNaN(Contract.Result<double>()));

      // d == NaN ==> NaN
      Contract.Ensures(!Double.IsNaN(d) || Double.IsNaN(Contract.Result<double>()));

      return default(double);
    }

    [Pure]
    public static double Atan(double d)
    {
      // d != Infty && d != NaN ==> -Pi/2 <= result <= Pi/2
      Contract.Ensures(Double.IsNaN(d) || Double.IsInfinity(d) || Contract.Result<double>() >= -Math.PI/2);
      Contract.Ensures(Double.IsNaN(d) || Double.IsInfinity(d) || Contract.Result<double>() <= Math.PI / 2);

      // d == Infty ==> Pi/2
      Contract.Ensures(!Double.IsNegativeInfinity(d) || Contract.Result<double>() == Math.PI / 2);

      // d == -Infty ==> -Pi/2
      Contract.Ensures(!Double.IsNegativeInfinity(d) || Contract.Result<double>() == -Math.PI / 2);

      // d == NaN ==> NaN
      Contract.Ensures(!Double.IsNaN(d) || Double.IsNaN(Contract.Result<double>()));

      return default(double);
    }

    [Pure]
    public static double Atan2(double y, double x)
    {
      // F: TODO: Add contracts. From the MSDN documentation is unclear what should be the behavior when values are infinty
      return default(double);
    }

#if !SILVERLIGHT
    [Pure]
    public static long BigMul(int a, int b)
    {
      Contract.Ensures(Contract.Result<long>() == ((long)(a) * (long)(b)));

      return default(long);
    }
#endif

    [Pure]
    public static double Ceiling(double a)
    {
      // a == NaN || a is Infty ==> a
      Contract.Ensures(!(Double.IsInfinity(a) || Double.IsNaN(a)) || Contract.Result<double>().Equals(a));

      // Otherwise, result >= a;
      Contract.Ensures((Double.IsInfinity(a) || Double.IsNaN(a)) || Contract.Result<double>() >= a);


      return default(double);
    }

#if !SILVERLIGHT
    [Pure]
    public static Decimal Ceiling(Decimal d)
    {
      return default(Decimal);
    }
#endif

    [Pure]
    public static double Cos(double d)
    {
      // -9223372036854775295 <= d <= 9223372036854775295 ==> -1 <= result <= 1
      Contract.Ensures(!(-9223372036854775295 <= d || d <= 9223372036854775295) || Contract.Result<double>() >= -1.0);
      Contract.Ensures(!(-9223372036854775295 <= d || d <= 9223372036854775295) || Contract.Result<double>() <= 1.0);

      // d == NaN || d is Infty ==> NaN
      Contract.Ensures(!(Double.IsNaN(d)  || Double.IsInfinity(d)) || Double.IsNaN(Contract.Result<double>()));

      return default(double);
    }

    [Pure]
    public static double Cosh(double value)
    {
      // value is Infty == > +Infty
      Contract.Ensures(!Double.IsInfinity(value) || Double.IsPositiveInfinity(Contract.Result<Double>()));

      // value == NaN ==> NaN
      Contract.Ensures(!Double.IsNaN(value) || Double.IsNaN(Contract.Result<double>()));

      return default(double);
    }

#if !SILVERLIGHT
    [Pure]
    public static int DivRem(int a, int b, out int result)
    {
      Contract.Requires(b != 0);

      Contract.Ensures(Contract.Result<int>() == ((a / b)));
      Contract.Ensures(Contract.ValueAtReturn(out result) == (a % b));

      return default(int);
    }

    [Pure]
    public static long DivRem(long a, long b, out long result)
    {
      Contract.Requires(b != 0);

      Contract.Ensures(Contract.Result<long>() == ((a / b)));
      Contract.Ensures(Contract.ValueAtReturn(out result) == (a % b));

      return default(long);
    }
#endif

    [Pure]
    public static double Exp(double d)
    {
      // d != NaN && d is not Infty ==> >= 0.0
      Contract.Ensures(Double.IsNaN(d) || Double.IsInfinity(d) || Contract.Result<Double>() >= 0.0);

      // d == -Infty ==> 0
      Contract.Ensures(!Double.IsNegativeInfinity(d) || Contract.Result<Double>() == 0.0);

      // d == +Inftty ==> +Infty
      Contract.Ensures(!Double.IsPositiveInfinity(d) || Double.IsPositiveInfinity(Contract.Result<Double>()));

      // d == NaN ==> NaN
      Contract.Ensures(!Double.IsNaN(d) || Double.IsNaN(Contract.Result<Double>()));

      return default(double);
    }

#if !SILVERLIGHT
    [Pure]
    public static Decimal Floor(Decimal d)
    {
      return default(Decimal);
    }
#endif

    [Pure]
    public static double Floor(double d)
    {
      // d == NaN || a is Infty ==> d
      Contract.Ensures(!(Double.IsInfinity(d) || Double.IsNaN(d)) || Contract.Result<double>().Equals(d));

      // Otherwise, result <= d;
      Contract.Ensures(Double.IsInfinity(d) || Double.IsNaN(d) || Contract.Result<double>() <= d);

      return default(double);
    }

    [Pure]
    public static double IEEERemainder(double x, double y)
    {
      return default(double);
    }

    [Pure]
    public static double Log(double d)
    {
      // d == 0 ==> -Infty
      Contract.Ensures(d != 0.0 || Double.IsNegativeInfinity(Contract.Result<double>()));

      // 0 < d < 1 ==> < 0
      Contract.Ensures(!(0.0 < d && d < 1.0) || Contract.Result<double>() < 0);

      // d == 1 ==> 1
      Contract.Ensures(d != 1.0 || Contract.Result<double>() == 0.0);

      // d > 1 ==> > 1
      Contract.Ensures(d < 1.0 || Contract.Result<double>() > 0.0);

      // d == NaN || d is -Infty ==> Nan
      Contract.Ensures(!Double.IsNaN(d) || !Double.IsNegativeInfinity(d) || Double.IsNaN(Contract.Result<Double>()));

      // d is +Infty ==> +Infty
      Contract.Ensures(!Double.IsPositiveInfinity(d) || Double.IsPositiveInfinity(Contract.Result<Double>()));

      return default(double);
    }


    [Pure]
    public static double Log10(double d)
    {  // d == 0 ==> -Infty
      Contract.Ensures(d != 0.0 || Double.IsNegativeInfinity(Contract.Result<double>()));

      // 0 < d < 1 ==> < 0
      Contract.Ensures(!(0.0 < d && d < 1.0) || Contract.Result<double>() < 0);

      // d == 1 ==> 1
      Contract.Ensures(d != 1.0 || Contract.Result<double>() == 0.0);

      // d > 1 ==> > 1
      Contract.Ensures(d < 1.0 || Contract.Result<double>() > 0.0);

      // d == NaN || d is -Infty ==> Nan
      Contract.Ensures(!Double.IsNaN(d) || !Double.IsNegativeInfinity(d) || Double.IsNaN(Contract.Result<Double>()));

      // d is +Infty ==> +Infty
      Contract.Ensures(!Double.IsPositiveInfinity(d) || Double.IsPositiveInfinity(Contract.Result<Double>()));

      return default(double);
    }

    [Pure]
    public static double Log(double a, double newBase)
    {
      return default(double);
    }


    [Pure]
    public static short Max(short val1, short val2)
    {
      Contract.Ensures(Contract.Result<short>() == (val1 > val2 ? val1 : val2));

      return default(short);
    }

    [Pure]
    public static byte Max(byte val1, byte val2)
    {
      Contract.Ensures(Contract.Result<byte>() == (val1 > val2 ? val1 : val2));

      return default(byte);
    }

    [Pure]
    public static sbyte Max(sbyte val1, sbyte val2)
    {
      Contract.Ensures(Contract.Result<sbyte>() == (val1 > val2 ? val1 : val2));

      return default(sbyte);
    }

    [Pure]
    public static float Max(float val1, float val2)
    {
      return default(float);
    }

    [Pure]
    public static ulong Max(ulong val1, ulong val2)
    {
      Contract.Ensures(Contract.Result<ulong>() == (val1 > val2 ? val1 : val2));

      return default(ulong);
    }

    [Pure]
    public static Decimal Max(Decimal val1, Decimal val2)
    {
      return default(Decimal);
    }

    [Pure]
    public static double Max(double val1, double val2)
    {
      return default(double);
    }

    [Pure]
    public static long Max(long val1, long val2)
    {
      Contract.Ensures(Contract.Result<long>() == (val1 > val2 ? val1 : val2));

      return default(long);
    }

    [Pure]
    public static ushort Max(ushort val1, ushort val2)
    {
      Contract.Ensures(Contract.Result<ushort>() == (val1 > val2 ? val1 : val2));

      return default(ushort);
    }

    [Pure]
    public static int Max(int val1, int val2)
    {
      Contract.Ensures(Contract.Result<int>() == (val1 > val2 ? val1 : val2));

      return default(int);
    }

    [Pure]
    public static uint Max(uint val1, uint val2)
    {
      Contract.Ensures(Contract.Result<uint>() == (val1 > val2 ? val1 : val2));

      return default(uint);
    }

    [Pure]
    public static ushort Min(ushort val1, ushort val2)
    {
      Contract.Ensures(Contract.Result<ushort>() == (val1 < val2 ? val1 : val2));

      return default(ushort);
    }

    [Pure]
    public static int Min(int val1, int val2)
    {
      Contract.Ensures(Contract.Result<int>() == (val1 < val2 ? val1 : val2));

      return default(int);
    }

    [Pure]
    public static byte Min(byte val1, byte val2)
    {
      Contract.Ensures(Contract.Result<byte>() == (val1 < val2 ? val1 : val2));

      return default(byte);
    }

    [Pure]
    public static short Min(short val1, short val2)
    {
      Contract.Ensures(Contract.Result<short>() == (val1 < val2 ? val1 : val2));

      return default(short);
    }

    [Pure]
    public static uint Min(uint val1, uint val2)
    {
      Contract.Ensures(Contract.Result<uint>() == (val1 < val2 ? val1 : val2));

      return default(uint);
    }

    [Pure]
    public static float Min(float val1, float val2)
    {
      return default(float);
    }

    [Pure]
    public static double Min(double val1, double val2)
    {
      return default(double);
    }

    [Pure]
    public static long Min(long val1, long val2)
    {
      Contract.Ensures(Contract.Result<long>() == (val1 < val2 ? val1 : val2));

      return default(long);
    }

    [Pure]
    public static ulong Min(ulong val1, ulong val2)
    {
      Contract.Ensures(Contract.Result<ulong>() == (val1 < val2 ? val1 : val2));

      return default(ulong);
    }

    [Pure]
    public static Decimal Min(Decimal val1, Decimal val2)
    {
      return default(Decimal);
    }

    [Pure]
    public static sbyte Min(sbyte val1, sbyte val2)
    {
      Contract.Ensures(Contract.Result<sbyte>() == (val1 < val2 ? val1 : val2));

      return default(sbyte);
    }

    [Pure]
    public static double Pow(double x, double y)
    {
      // x == NaN or y == NaN ==> NaN
      Contract.Ensures(!(Double.IsNaN(x) || Double.IsNaN(y)) || Double.IsNaN(Contract.Result<Double>()));

      // y == 0 ==> 1
      Contract.Ensures(!(!Double.IsNaN(x) && y == 0) || Contract.Result<Double>() == 1.0);

      // x == -infty && y < 0 ==> 0
      Contract.Ensures(!(Double.IsNegativeInfinity(x) && y < 0.0) || Contract.Result<Double>() == 0.0);

      // x == -infty && y > 0 ==> Infty
      Contract.Ensures(!(Double.IsNegativeInfinity(x) && y > 0.0) || Double.IsInfinity(Contract.Result<Double>()));

      // x == -1 && y is infty ==> NaN
      Contract.Ensures(!(x == -1 && Double.IsInfinity(y)) || Double.IsNaN(Contract.Result<Double>()));

      // -1 < x < 1 && y = -Infty ==> +Infty
      Contract.Ensures(!(-1 < x && x < 1 && Double.IsNegativeInfinity(y)) || Double.IsPositiveInfinity(Contract.Result<Double>()));

      // -1 < x < 1 && y = +Infty ==> 0
      Contract.Ensures(!(-1 < x && x < 1 && Double.IsPositiveInfinity(y)) || Contract.Result<Double>() == 0.0);

      // x < -1 || x > 1 && y = - Infty ==> 0
      Contract.Ensures(!((x < -1 || x > 1) && Double.IsNegativeInfinity(y)) || Contract.Result<Double>() == 0.0);

      // x < -1 || x > 1 && y = Infty ==> 0
      Contract.Ensures(!((x < -1 || x > 1) && Double.IsPositiveInfinity(y)) || Double.IsPositiveInfinity(Contract.Result<Double>()));

      // x = 0 and y < 0 ==> +Infty
      Contract.Ensures(!(x == 0.0 && y < 0.0) || Double.IsPositiveInfinity(Contract.Result<double>()));

      // x = 0 and y > 0 ==> 0
      Contract.Ensures(!(x == 0.0 && y > 0.0) || Contract.Result<double>() == 0.0);

      // x == 1 && y != NaN
      Contract.Ensures(!(x == 1 && !Double.IsNaN(y)) || Contract.Result<double>() == 1.0);

      // x == +infty && y < 0
      Contract.Ensures(!(Double.IsPositiveInfinity(y) && y < 0) || Contract.Result<double>() == 0.0);

      // x == +infty && y < 0
      Contract.Ensures(!(Double.IsPositiveInfinity(y) && y > 0) || Double.IsPositiveInfinity(Contract.Result<double>()));

      return default(double);
    }

#if !SILVERLIGHT
    [Pure]
    public static double Round(double value, MidpointRounding mode)
    {
      Contract.Requires(Enum.IsDefined(typeof(MidpointRounding), mode));

      return default(double);
    }
#endif
    [Pure]
    public static double Round(double a)
    {
      return default(double);
    }


    [Pure]
    public static Decimal Round(Decimal d)
    {
      return default(Decimal);
    }

    [Pure]
    public static double Round(double value, int digits)
    {
      Contract.Requires(digits >= 0);
      Contract.Requires(digits <= 15);

      return default(double);
    }

#if !SILVERLIGHT
    [Pure]
    public static double Round(double value, int digits, MidpointRounding mode)
    {
      Contract.Requires(digits >= 0);
      Contract.Requires(digits <= 15);

      Contract.Requires(Enum.IsDefined(typeof(MidpointRounding), mode));

      return default(double);
    }

    [Pure]
    public static Decimal Round(Decimal d, int decimals, MidpointRounding mode)
    {
      Contract.Requires(decimals >= 0);
      Contract.Requires(decimals <= 28);

      Contract.Requires(Enum.IsDefined(typeof(MidpointRounding), mode));

      return default(Decimal);
    }

    [Pure]
    public static Decimal Round(Decimal d, MidpointRounding mode)
    {
      Contract.Requires(Enum.IsDefined(typeof(MidpointRounding), mode));

      return default(Decimal);
    }
#endif

    [Pure]
    public static Decimal Round(Decimal d, int decimals)
    {
      Contract.Requires(decimals >= 0);
      Contract.Requires(decimals <= 28);

      return default(Decimal);
    }


    [Pure]
    public static int Sign(Decimal value)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    [Pure]
    public static int Sign(sbyte value)
    {
      Contract.Ensures(Contract.Result<int>() == (value == 0 ? 0 : (value > 0 ? 1 : -1)));

      return default(int);
    }

    [Pure]
    public static int Sign(int value)
    {
      Contract.Ensures(Contract.Result<int>() == (value == 0 ? 0 : (value > 0 ? 1 : -1)));

      return default(int);
    }

    [Pure]
    public static int Sign(double value)
    {
      Contract.Requires(!Double.IsNaN(value));

      Contract.Ensures(Contract.Result<int>() == (value < 0.0 ? -1 : (value > 0.0 ? 1 : 0)));

      return default(int);
    }

    [Pure]
    public static int Sign(short value)
    {
      Contract.Ensures(Contract.Result<int>() == (value == 0 ? 0 : (value > 0 ? 1 : -1)));

      return default(int);
    }

    [Pure]
    public static int Sign(long value)
    {
      Contract.Ensures(Contract.Result<int>() == (value == 0 ? 0 : (value > 0 ? 1 : -1)));

      return default(int);
    }

    [Pure]
    public static int Sign(float value)
    {
      Contract.Requires(!Single.IsNaN(value));

      Contract.Ensures(Contract.Result<int>() == (value < 0.0 ? -1 : (value > 0.0 ? 1 : 0)));

      return default(int);
    }

    [Pure]
    public static double Sin(double a)
    {
      // -9223372036854775295 <= a <= 9223372036854775295 ==> -1 <= result <= 1
      Contract.Ensures(!(-9223372036854775295 <= a || a <= 9223372036854775295) || Contract.Result<double>() >= -1.0);
      Contract.Ensures(!(-9223372036854775295 <= a || a <= 9223372036854775295) || Contract.Result<double>() <= 1.0);

      // d == NaN || d is Infty ==> NaN
      Contract.Ensures(!(Double.IsNaN(a) || Double.IsInfinity(a)) || Double.IsNaN(Contract.Result<double>()));


      return default(double);
    }

    [Pure]
    public static double Sinh(double value)
    {
      // d == NaN || d is Infty ==> NaN
      Contract.Ensures(!(Double.IsNaN(value) || Double.IsInfinity(value)) || Contract.Result<double>().Equals(value));

      return default(double);
    }

    [Pure]
    public static double Sqrt(double d)
    {
      Contract.Ensures(!(d >= 0.0) || Contract.Result<double>() >= 0.0);
      Contract.Ensures(!(d < 0) || Double.IsNaN(Contract.Result<Double>()));
      Contract.Ensures(!Double.IsNaN(d) || Double.IsNaN(Contract.Result<Double>()));
      Contract.Ensures(!Double.IsPositiveInfinity(d) || Double.IsPositiveInfinity(Contract.Result<Double>()));

      return default(double);
    }

    [Pure]
    public static double Tan(double a)
    {
      // d == NaN || d == +Infty ==> d
      Contract.Ensures(!(Double.IsNaN(a) || Double.IsInfinity(a)) || Contract.Result<Double>().Equals(a) );

      return default(double);
    }
    [Pure]
    public static double Tanh(double value)
    {
      Contract.Ensures(!Double.IsPositiveInfinity(value) || Contract.Result<Double>() == 1.0);
      Contract.Ensures(!Double.IsNegativeInfinity(value) || Contract.Result<Double>() == -1.0);
      Contract.Ensures(!Double.IsNaN(value) || Double.IsNaN(Contract.Result<Double>()));

      return default(double);
    }
#if !SILVERLIGHT
    [Pure]
    public static double Truncate(double d)
    {
      Contract.Ensures(!(Double.IsNaN(d) || Double.IsInfinity(d)) || Contract.Result<Double>().Equals(d));

      return default(double);
    }
    [Pure]
    public static Decimal Truncate(Decimal d)
    {
      return default(Decimal);
    }
#endif
    #endregion

  }
}
