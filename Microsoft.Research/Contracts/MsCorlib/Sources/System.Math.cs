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
#pragma warning disable 0067
// Disable the "this event is never assigned to".
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
    #region Methods and constructors
    public static int Abs(int value)
    {
      Contract.Ensures((value - Contract.Result<int>()) <= 0);
      Contract.Ensures(0 <= Contract.Result<int>());

      return default(int);
    }

    public static long Abs(long value)
    {
      return default(long);
    }

    public static sbyte Abs(sbyte value)
    {
      return default(sbyte);
    }

    public static short Abs(short value)
    {
      return default(short);
    }

    public static Decimal Abs(Decimal value)
    {
      return default(Decimal);
    }

    public static double Abs(double value)
    {
      return default(double);
    }

    public static float Abs(float value)
    {
      return default(float);
    }

    public static double Acos(double d)
    {
      return default(double);
    }

    public static double Asin(double d)
    {
      return default(double);
    }

    public static double Atan(double d)
    {
      return default(double);
    }

    public static double Atan2(double y, double x)
    {
      return default(double);
    }

    public static long BigMul(int a, int b)
    {
      Contract.Ensures(-4611686016279904256 <= Contract.Result<long>());
      Contract.Ensures(Contract.Result<long>() <= 4611686018427387904);
      Contract.Ensures(Contract.Result<long>() == ((long)(a) * (long)(b)));

      return default(long);
    }

    public static Decimal Ceiling(Decimal d)
    {
      return default(Decimal);
    }

    public static double Ceiling(double a)
    {
      return default(double);
    }

    public static double Cos(double d)
    {
      return default(double);
    }

    public static double Cosh(double value)
    {
      return default(double);
    }

    public static int DivRem(int a, int b, out int result)
    {
      Contract.Ensures(Contract.Result<int>() == ((a / b)));

      result = default(int);

      return default(int);
    }

    public static long DivRem(long a, long b, out long result)
    {
      Contract.Ensures(Contract.Result<long>() == ((a / b)));

      result = default(long);

      return default(long);
    }

    public static double Exp(double d)
    {
      return default(double);
    }

    public static Decimal Floor(Decimal d)
    {
      return default(Decimal);
    }

    public static double Floor(double d)
    {
      return default(double);
    }

    public static double IEEERemainder(double x, double y)
    {
      return default(double);
    }

    public static double Log(double d)
    {
      return default(double);
    }

    public static double Log(double a, double newBase)
    {
      return default(double);
    }

    public static double Log10(double d)
    {
      return default(double);
    }

    public static int Max(int val1, int val2)
    {
      Contract.Ensures((val1 - Contract.Result<int>()) <= 0);
      Contract.Ensures((val2 - Contract.Result<int>()) <= 0);

      return default(int);
    }

    public static uint Max(uint val1, uint val2)
    {
      Contract.Ensures((val1 - Contract.Result<uint>()) <= 0);
      Contract.Ensures((val2 - Contract.Result<uint>()) <= 0);

      return default(uint);
    }

    public static byte Max(byte val1, byte val2)
    {
      Contract.Ensures((val1 - Contract.Result<byte>()) <= 0);
      Contract.Ensures((val2 - Contract.Result<byte>()) <= 0);

      return default(byte);
    }

    public static sbyte Max(sbyte val1, sbyte val2)
    {
      Contract.Ensures((val1 - Contract.Result<sbyte>()) <= 0);
      Contract.Ensures((val2 - Contract.Result<sbyte>()) <= 0);

      return default(sbyte);
    }

    public static ushort Max(ushort val1, ushort val2)
    {
      Contract.Ensures((val1 - Contract.Result<ushort>()) <= 0);
      Contract.Ensures((val2 - Contract.Result<ushort>()) <= 0);

      return default(ushort);
    }

    public static short Max(short val1, short val2)
    {
      Contract.Ensures((val1 - Contract.Result<short>()) <= 0);
      Contract.Ensures((val2 - Contract.Result<short>()) <= 0);

      return default(short);
    }

    public static Decimal Max(Decimal val1, Decimal val2)
    {
      return default(Decimal);
    }

    public static float Max(float val1, float val2)
    {
      return default(float);
    }

    public static long Max(long val1, long val2)
    {
      Contract.Ensures((val1 - Contract.Result<long>()) <= 0);
      Contract.Ensures((val2 - Contract.Result<long>()) <= 0);
      Contract.Ensures(-9223372036854775807 <= Contract.Result<long>());

      return default(long);
    }

    public static ulong Max(ulong val1, ulong val2)
    {
      Contract.Ensures((val1 - Contract.Result<ulong>()) <= 0);
      Contract.Ensures((val2 - Contract.Result<ulong>()) <= 0);

      return default(ulong);
    }

    public static double Max(double val1, double val2)
    {
      return default(double);
    }

    public static ushort Min(ushort val1, ushort val2)
    {
      Contract.Ensures((Contract.Result<ushort>() - val1) <= 0);
      Contract.Ensures((Contract.Result<ushort>() - val2) <= 0);

      return default(ushort);
    }

    public static long Min(long val1, long val2)
    {
      Contract.Ensures((Contract.Result<long>() - val1) <= 0);
      Contract.Ensures((Contract.Result<long>() - val2) <= 0);
      Contract.Ensures(-9223372036854775807 <= Contract.Result<long>());

      return default(long);
    }

    public static int Min(int val1, int val2)
    {
      Contract.Ensures((Contract.Result<int>() - val1) <= 0);
      Contract.Ensures((Contract.Result<int>() - val2) <= 0);

      return default(int);
    }

    public static short Min(short val1, short val2)
    {
      Contract.Ensures((Contract.Result<short>() - val1) <= 0);
      Contract.Ensures((Contract.Result<short>() - val2) <= 0);

      return default(short);
    }

    public static byte Min(byte val1, byte val2)
    {
      Contract.Ensures((Contract.Result<byte>() - val1) <= 0);
      Contract.Ensures((Contract.Result<byte>() - val2) <= 0);

      return default(byte);
    }

    public static sbyte Min(sbyte val1, sbyte val2)
    {
      Contract.Ensures((Contract.Result<sbyte>() - val1) <= 0);
      Contract.Ensures((Contract.Result<sbyte>() - val2) <= 0);

      return default(sbyte);
    }

    public static double Min(double val1, double val2)
    {
      return default(double);
    }

    public static Decimal Min(Decimal val1, Decimal val2)
    {
      return default(Decimal);
    }

    public static uint Min(uint val1, uint val2)
    {
      Contract.Ensures((Contract.Result<uint>() - val1) <= 0);
      Contract.Ensures((Contract.Result<uint>() - val2) <= 0);

      return default(uint);
    }

    public static float Min(float val1, float val2)
    {
      return default(float);
    }

    public static ulong Min(ulong val1, ulong val2)
    {
      Contract.Ensures((Contract.Result<ulong>() - val1) <= 0);
      Contract.Ensures((Contract.Result<ulong>() - val2) <= 0);

      return default(ulong);
    }

    public static double Pow(double x, double y)
    {
      return default(double);
    }

    public static Decimal Round(Decimal d, int decimals, MidpointRounding mode)
    {
      return default(Decimal);
    }

    public static double Round(double value, int digits, MidpointRounding mode)
    {
      return default(double);
    }

    public static Decimal Round(Decimal d)
    {
      return default(Decimal);
    }

    public static double Round(double value, int digits)
    {
      return default(double);
    }

    public static double Round(double value, MidpointRounding mode)
    {
      return default(double);
    }

    public static double Round(double a)
    {
      return default(double);
    }

    public static Decimal Round(Decimal d, MidpointRounding mode)
    {
      return default(Decimal);
    }

    public static Decimal Round(Decimal d, int decimals)
    {
      return default(Decimal);
    }

    public static int Sign(long value)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public static int Sign(double value)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public static int Sign(Decimal value)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public static int Sign(float value)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public static int Sign(int value)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public static int Sign(short value)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public static int Sign(sbyte value)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 1);

      return default(int);
    }

    public static double Sin(double a)
    {
      return default(double);
    }

    public static double Sinh(double value)
    {
      return default(double);
    }

    public static double Sqrt(double d)
    {
      return default(double);
    }

    public static double Tan(double a)
    {
      return default(double);
    }

    public static double Tanh(double value)
    {
      return default(double);
    }

    public static double Truncate(double d)
    {
      Contract.Ensures(Contract.Result<double>() == d);

      return default(double);
    }

    public static Decimal Truncate(Decimal d)
    {
      return default(Decimal);
    }
    #endregion

    #region Fields
    public static double E;
    public static double PI;
    #endregion
  }
}
