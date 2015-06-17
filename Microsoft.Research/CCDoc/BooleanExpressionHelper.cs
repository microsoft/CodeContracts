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
using Microsoft.Cci.MutableCodeModel;
using Microsoft.Cci;

namespace CCDoc {
  [ContractVerification(true)]
  internal static class BooleanExpressionHelper
  {
    #region Code to negate predicates in bool expression strings
    // Recognize some common predicate forms, and negate them.  Also, fall back to a correct default.
    public static String NegatePredicate(String predicate) {
      if (String.IsNullOrEmpty(predicate)) return "";
      if (predicate.Length < 2) return "!" + predicate;

      // "(p)", but avoiding stuff like "(p && q) || (!p)"
      if (predicate[0] == '(' && predicate[predicate.Length - 1] == ')') {
        if (predicate.IndexOf('(', 1) == -1)
          return '(' + NegatePredicate(predicate.Substring(1, predicate.Length - 2)) + ')';
      }

      // "!p"
      if (predicate[0] == '!' && (ContainsNoOperators(predicate, 1, predicate.Length - 1) || IsSimpleFunctionCall(predicate, 1, predicate.Length - 1)))
        return predicate.Substring(1);

      // "a < b" or "a <= b"
      int ltIndex = predicate.IndexOf('<');
      if (ltIndex >= 0) {
        int aStart = 0, aEnd, bStart, bEnd = predicate.Length;
        bool ltOrEquals = ltIndex < bEnd - 1 ? predicate[ltIndex + 1] == '=' : false;
        aEnd = ltIndex;
        bStart = ltOrEquals ? ltIndex + 2 : ltIndex + 1;

        String a = predicate.Substring(aStart, aEnd - aStart);
        String b = predicate.Substring(bStart, bEnd - bStart);
        if (ContainsNoOperators(a) && ContainsNoOperators(b))
          return a + (ltOrEquals ? ">" : ">=") + b;
      }

      // "a > b" or "a >= b"
      int gtIndex = predicate.IndexOf('>');
      if (gtIndex >= 0) {
        int aStart = 0, aEnd, bStart, bEnd = predicate.Length;
        bool gtOrEquals = gtIndex < bEnd - 1 ? predicate[gtIndex + 1] == '=' : false;
        aEnd = gtIndex;
        bStart = gtOrEquals ? gtIndex + 2 : gtIndex + 1;

        String a = predicate.Substring(aStart, aEnd - aStart);
        String b = predicate.Substring(bStart, bEnd - bStart);
        if (ContainsNoOperators(a) && ContainsNoOperators(b))
          return a + (gtOrEquals ? "<" : "<=") + b;
      }

      // "a == b"  or  "a != b"
      int eqIndex = predicate.IndexOf('=');
      if (eqIndex >= 0) {
        int aStart = 0, aEnd = -1, bStart = -1, bEnd = predicate.Length;
        bool skip = false;
        bool equalsOperator = false;
        if (eqIndex > 0 && predicate[eqIndex - 1] == '!') {
          aEnd = eqIndex - 1;
          bStart = eqIndex + 1;
          equalsOperator = false;
        } else if (eqIndex < bEnd - 1 && predicate[eqIndex + 1] == '=') {
          aEnd = eqIndex;
          bStart = eqIndex + 2;
          equalsOperator = true;
        } else
          skip = true;

        if (!skip) {
          String a = predicate.Substring(aStart, aEnd - aStart);
          String b = predicate.Substring(bStart, bEnd - bStart);
          if (ContainsNoOperators(a) && ContainsNoOperators(b))
            return a + (equalsOperator ? "!=" : "==") + b;
        }
      }
      return NegateConjunctsAndDisjuncts(predicate);
    }

    [ContractVerification(true)]
    private static string NegateConjunctsAndDisjuncts(String predicate)
    {
      Contract.Requires(predicate != null);

      if (predicate.Contains("&&") || predicate.Contains("||"))
      {
        // Consider predicates like "(P) && (Q)", "P || Q", "(P || Q) && R", etc.
        // Apply DeMorgan's law, and recurse to negate both sides of the binary operator.
        int aStart = 0, aEnd, bEnd = predicate.Length;
        int parenCount = 0;
        bool skip = false;
        bool foundAnd = false, foundOr = false;
        aEnd = 0;
        while (aEnd < predicate.Length && ((predicate[aEnd] != '&' && predicate[aEnd] != '|') || parenCount > 0))
        {
          if (predicate[aEnd] == '(')
            parenCount++;
          else if (predicate[aEnd] == ')')
            parenCount--;
          aEnd++;
        }
        if (aEnd >= predicate.Length - 1)
          skip = true;
        else
        {
          if (aEnd + 1 < predicate.Length && predicate[aEnd] == '&' && predicate[aEnd + 1] == '&')
            foundAnd = true;
          else if (aEnd + 1 < predicate.Length && predicate[aEnd] == '|' && predicate[aEnd + 1] == '|')
            foundOr = true;
          if (!foundAnd && !foundOr)
            skip = true;
        }

        if (!skip)
        {
          var bStart = aEnd + 2;
          Contract.Assert(bStart <= bEnd);
          while (aEnd > 0 && Char.IsWhiteSpace(predicate[aEnd - 1]))
            aEnd--;
          while (bStart < bEnd && Char.IsWhiteSpace(predicate[bStart]))
            bStart++;

          String a = predicate.Substring(aStart, aEnd - aStart);
          String b = predicate.Substring(bStart, bEnd - bStart);
          String op = foundAnd ? " || " : " && ";
          return NegatePredicate(a) + op + NegatePredicate(b);
        }
      }

      return String.Format("!({0})", predicate);
    }
    private static bool ContainsNoOperators(String s) {
      Contract.Requires(s != null);
      return ContainsNoOperators(s, 0, s.Length);
    }
    // These aren't operators like + per se, but ones that will cause evaluation order to possibly change,
    // or alter the semantics of what might be in a predicate.
    // @TODO: Consider adding '~'
    static readonly String[] Operators = new String[] { "==", "!=", "=", "<", ">", "(", ")", "//", "/*", "*/" };
    private static bool ContainsNoOperators(String s, int start, int end) {
      Contract.Requires(s != null);
      foreach (String op in Operators) {
        Contract.Assume(op != null, "lack of contract support for collections");
        if (s.IndexOf(op) >= 0)
          return false;
      }
      return true;
    }
    private static bool ArrayContains<T>(T[] array, T item) {
      Contract.Requires(array != null);

      foreach (T x in array)
        if (item.Equals(x))
          return true;
      return false;
    }
    // Recognize only SIMPLE method calls, like "System.String.Equals("", "")".
    private static bool IsSimpleFunctionCall(String s, int start, int end) {
      Contract.Requires(s != null);
      Contract.Requires(start >= 0);
      Contract.Requires(end <= s.Length);
      Contract.Requires(end >= 0);
      char[] badChars = { '+', '-', '*', '/', '~', '<', '=', '>', ';', '?', ':' };
      int parenCount = 0;
      int index = start;
      bool foundMethod = false;
      for (; index < end; index++) {
        if (s[index] == '(')
        {
          parenCount++;
          if (parenCount > 1)
            return false;
          if (foundMethod == true)
            return false;
          foundMethod = true;
        } else if (s[index] == ')') {
          parenCount--;
          if (index != end - 1)
            return false;
        } else if (ArrayContains(badChars, s[index]))
          return false;
      }
      return foundMethod;
    }
    #endregion Code from BrianGru to negate predicates coming from if-then-throw preconditions

    public static IExpression Normalize(IExpression expression) {
      LogicalNot/*?*/ logicalNot = expression as LogicalNot;
      if (logicalNot != null) {
        IExpression operand = logicalNot.Operand;
        #region LogicalNot: !
        LogicalNot/*?*/ operandAsLogicalNot = operand as LogicalNot;
        if (operandAsLogicalNot != null) {
          return Normalize(operandAsLogicalNot.Operand);
        }
        #endregion
        #region BinaryOperations: ==, !=, <, <=, >, >=
        BinaryOperation/*?*/ binOp = operand as BinaryOperation;
        if (binOp != null) {
          BinaryOperation/*?*/ result = null;
          if (binOp is IEquality)
            result = new NotEquality();
          else if (binOp is INotEquality)
            result = new Equality();
          else if (binOp is ILessThan)
            result = new GreaterThanOrEqual();
          else if (binOp is ILessThanOrEqual)
            result = new GreaterThan();
          else if (binOp is IGreaterThan)
            result = new LessThanOrEqual();
          else if (binOp is IGreaterThanOrEqual)
            result = new LessThan();
          if (result != null) {
            result.LeftOperand = Normalize(binOp.LeftOperand);
            result.RightOperand = Normalize(binOp.RightOperand);
            return result;
          }
        }
        #endregion
        #region Conditionals: &&, ||
        Conditional/*?*/ conditional = operand as Conditional;
        if (conditional != null) {
          if (ExpressionHelper.IsIntegralNonzero(conditional.ResultIfTrue) ||
              ExpressionHelper.IsIntegralZero(conditional.ResultIfFalse)) {
            Conditional result = new Conditional();
            LogicalNot not;
            //invert condition
            not = new LogicalNot();
            not.Operand = conditional.Condition;
            result.Condition = Normalize(not);
            //invert false branch and switch with true branch
            not = new LogicalNot();
            not.Operand = conditional.ResultIfFalse;
            result.ResultIfTrue = Normalize(not);
            //invert true branch and switch with false branch
            not = new LogicalNot();
            not.Operand = conditional.ResultIfTrue;
            result.ResultIfFalse = Normalize(not);
            //return
            result.Type = conditional.Type;
            return result;
          }
        }
        #endregion
        #region Constants: true, false
        CompileTimeConstant/*?*/ ctc = operand as CompileTimeConstant;
        if (ctc != null) {
          if (ExpressionHelper.IsIntegralNonzero(ctc)) {  //Is true
            var val = SetBooleanFalse(ctc);
            if (val != null)
              return val;
            else
              return expression;
          } else if (ExpressionHelper.IsIntegralZero(ctc)) {  //Is false
            var val = SetBooleanTrue(ctc);
            if (val != null)
              return val;
            else
              return expression;
          }
        }
        #endregion
      }
      return expression;
    }
    public static CompileTimeConstant/*?*/ SetBooleanFalse(ICompileTimeConstant constExpression) {
      Contract.Requires(constExpression != null);

      IConvertible/*?*/ ic = constExpression.Value as IConvertible;
      if (ic == null) return null;
      CompileTimeConstant result = new CompileTimeConstant(constExpression);
      switch (ic.GetTypeCode()) {
        case System.TypeCode.SByte: result.Value = (SByte)0; break;
        case System.TypeCode.Int16: result.Value = (Int16)0; break;
        case System.TypeCode.Int32: result.Value = (Int32)0; break;
        case System.TypeCode.Int64: result.Value = (Int64)0; break;
        case System.TypeCode.Byte: result.Value = (Byte)0; break;
        case System.TypeCode.UInt16: result.Value = (UInt16)0; break;
        case System.TypeCode.UInt32: result.Value = (UInt32)0; break;
        case System.TypeCode.UInt64: result.Value = (UInt64)0; break;
        default: return null;
      }
      return result;
    }
    public static CompileTimeConstant/*?*/ SetBooleanTrue(ICompileTimeConstant constExpression) {
      Contract.Requires(constExpression != null);

      IConvertible/*?*/ ic = constExpression.Value as IConvertible;
      if (ic == null) return null;
      CompileTimeConstant result = new CompileTimeConstant(constExpression);
      switch (ic.GetTypeCode()) {
        case System.TypeCode.SByte: result.Value = (SByte)1; break;
        case System.TypeCode.Int16: result.Value = (Int16)1; break;
        case System.TypeCode.Int32: result.Value = (Int32)1; break;
        case System.TypeCode.Int64: result.Value = (Int64)1; break;
        case System.TypeCode.Byte: result.Value = (Byte)1; break;
        case System.TypeCode.UInt16: result.Value = (UInt16)1; break;
        case System.TypeCode.UInt32: result.Value = (UInt32)1; break;
        case System.TypeCode.UInt64: result.Value = (UInt64)1; break;
        default: return null;
      }
      return result;
    }
  }

  [ContractVerification(true)]
  internal class ExpressionSimplifier : CodeRewriter {
    public ExpressionSimplifier() : base(Dummy.CompilationHostEnvironment) { }
    public override IExpression Rewrite(ILogicalNot logicalNot) {
      var result = base.Rewrite(logicalNot);
      return BooleanExpressionHelper.Normalize(result);
    }
    public override IExpression Rewrite(IConditional conditional) {
      if (conditional.Type.TypeCode == PrimitiveTypeCode.Boolean && ExpressionHelper.IsIntegralZero(conditional.ResultIfTrue)) {
        var not = new LogicalNot() { Operand = conditional.Condition, };
        var c = new Conditional() {
          Condition = BooleanExpressionHelper.Normalize(not),
          ResultIfTrue = conditional.ResultIfFalse,
          ResultIfFalse = new CompileTimeConstant() { Type = conditional.Type, Value = false, },
          Type = conditional.Type,
        };
        return c;
      }
      return base.Rewrite(conditional);
    }
  }
}
