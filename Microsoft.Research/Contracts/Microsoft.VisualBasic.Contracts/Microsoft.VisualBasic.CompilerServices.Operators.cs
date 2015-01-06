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

namespace Microsoft.VisualBasic.CompilerServices
{
  public static class Operators
  {
    [Pure]
    extern public static object AddObject(object Left, object Right);
    [Pure]
    extern public static object AndObject(object Left, object Right);
    [Pure]
    extern public static int CompareObject(object Left, object Right, bool TextCompare);
    [Pure]
    extern public static object CompareObjectEqual(object Left, object Right, bool TextCompare);
    [Pure]
    extern public static object CompareObjectGreater(object Left, object Right, bool TextCompare);
    [Pure]
    extern public static object CompareObjectGreaterEqual(object Left, object Right, bool TextCompare);
    [Pure]
    extern public static object CompareObjectLess(object Left, object Right, bool TextCompare);
    [Pure]
    extern public static object CompareObjectLessEqual(object Left, object Right, bool TextCompare);
    [Pure]
    extern public static object CompareObjectNotEqual(object Left, object Right, bool TextCompare);
    [Pure]
    public static int CompareString(string Left, string Right, bool TextCompare)
    {
      Contract.Ensures(Contract.Result<int>() != 0 ||
  ((Left == null && Right == null) ||
  (Left == null && Right.Length == 0) ||
  (Right == null && Left.Length == 0) ||
  (Left.Length == Right.Length)));

      Contract.Ensures(Contract.Result<int>() == 0 ||
        ((Right == null && Left.Length > 0) ||
        (Left == null && Right.Length > 0) ||
        (Left != null && Right != null && (Left.Length > 0 || Right.Length > 0))));

      return default(int);
    }

    [Pure]
    extern public static object ConcatenateObject(object Left, object Right);
    [Pure]
    extern public static bool ConditionalCompareObjectEqual(object Left, object Right, bool TextCompare);
    [Pure]
    extern public static bool ConditionalCompareObjectGreater(object Left, object Right, bool TextCompare);
    [Pure]
    extern public static bool ConditionalCompareObjectGreaterEqual(object Left, object Right, bool TextCompare);
    [Pure]
    extern public static bool ConditionalCompareObjectLess(object Left, object Right, bool TextCompare);
    [Pure]
    extern public static bool ConditionalCompareObjectLessEqual(object Left, object Right, bool TextCompare);
    [Pure]
    extern public static bool ConditionalCompareObjectNotEqual(object Left, object Right, bool TextCompare);

    [Pure]
    extern public static object DivideObject(object Left, object Right);

    [Pure]
    extern public static object ExponentObject(object Left, object Right);

    [Pure]
    extern public static object IntDivideObject(object Left, object Right);

    [Pure]
    extern public static object LeftShiftObject(object Operand, object Amount);
#if !SILVERLIGHT
    [Pure]
    extern public static object LikeObject(object Source, object Pattern, CompareMethod CompareOption);
    [Pure]
    extern public static bool LikeString(string Source, string Pattern, CompareMethod CompareOption);
#endif
    [Pure]
    extern public static object ModObject(object Left, object Right);
    [Pure]
    extern public static object MultiplyObject(object Left, object Right);
    [Pure]
    extern public static object NegateObject(object Operand);
    [Pure]
    extern public static object NotObject(object Operand);
    [Pure]
    extern public static object OrObject(object Left, object Right);
    [Pure]
    extern public static object PlusObject(object Operand);
    [Pure]
    extern public static object RightShiftObject(object Operand, object Amount);
    [Pure]
    extern public static object SubtractObject(object Left, object Right);
    [Pure]
    extern public static object XorObject(object Left, object Right);
  }
}
